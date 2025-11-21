using BTL_LTTQ_QLPM.Databases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public partial class FormSuaTaiKhoan : Form
    {
        private string _currentUsername;
        private int? _currentNhanVienId;
        private int? _currentGiangVienId;
        public FormSuaTaiKhoan(DataRow row)
        {
            InitializeComponent();
            LoadRoles();
            LoadDataToControls(row);

            // Ngăn không cho sửa Username sau khi tạo
            txtUsername.ReadOnly = true;

            // Ẩn trường Mật khẩu nếu không có chức năng Reset Password
            // Hoặc thay bằng nút 'Đổi mật khẩu'
            label5.Visible = txtPassword.Visible = false;
        }
        private void LoadRoles()
        {
            try
            {
                // 1. Lấy dữ liệu vai trò từ Database
                DataTable dtRoles = TaiKhoanDB.GetAllRoles();

                // 2. Gán dữ liệu vào ComboBox (cboRole)
                cboRole.DataSource = dtRoles;

                // Cấu hình:
                // ValueMember: Giá trị sẽ được lưu (ROLE_ID)
                cboRole.ValueMember = "ROLE_ID";

                // DisplayMember: Tên sẽ được hiển thị cho người dùng xem (ROLE_NAME)
                cboRole.DisplayMember = "ROLE_NAME";

                // Tùy chọn: Chọn một giá trị mặc định (ví dụ: User - RoleId = 3)
                // cboRole.SelectedValue = 3; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vai trò: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDataToControls(DataRow row)
        {
            _currentUsername = row["USERNAME"].ToString();
            _currentNhanVienId = row["NHANVIEN_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["NHANVIEN_ID"]);
            _currentGiangVienId = row["GIANGVIEN_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["GIANGVIEN_ID"]);

            // Điền dữ liệu vào các controls
            txtUsername.Text = _currentUsername;
            txtHoTen.Text = row["HO_TEN"].ToString();
            txtSDT.Text = row["SDT"].ToString();
            txtEmail.Text = row["EMAIL"].ToString();

            // Chọn quyền hiện tại
            cboRole.SelectedValue = Convert.ToInt32(row["ROLE_ID"]);

            // Điền thông tin đặc thù của Nhân viên (nếu có)
            if (row["CHUC_VU"] != DBNull.Value)
            {
                txtChucVu.Text = row["CHUC_VU"].ToString();
                txtLuongCung.Text = row["LUONG_CUNG"].ToString();
            }

            // Kích hoạt logic ẩn/hiện trường (Bạn cần gọi hàm này sau khi SelectedValue được set)
            cboRole_SelectedIndexChanged(null, null);
        }

        private void FormSuaTaiKhoan_Load(object sender, EventArgs e)
        {

        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboRole.SelectedValue is int roleId)
            {
                // Vai trò là Nhân viên (ADMIN = 1, NHANVIEN = 2)
                bool isNhanVien = (roleId == 1 || roleId == 2);

                // Hiển thị/Ẩn các trường Nhân viên
                label10.Visible = txtChucVu.Visible = isNhanVien;
                label11.Visible = txtLuongCung.Visible = isNhanVien;

                // Reset giá trị nếu không phải NV
                if (!isNhanVien)
                {
                    txtChucVu.Clear();
                    txtLuongCung.Clear();
                }
            }
        }
        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;
        public int RoleId => Convert.ToInt32(cboRole.SelectedValue);
        public string HoTen => txtHoTen.Text;
        public string SDT => txtSDT.Text;
        public string Email => txtEmail.Text;
        public string ChucVu => txtChucVu.Text;
        // Sử dụng decimal? để chấp nhận NULL
        public decimal? LuongCung
        {
            get
            {
                if (decimal.TryParse(txtLuongCung.Text, out decimal lc)) return lc;
                return null;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            decimal? luongCungValue = null;
            // Đảm bảo tên TextBox Lương Cứng là chính xác (ví dụ: txtLuongCung)
            string luongCungText = txtLuongCung.Text.Trim();
            decimal parsedValue;

            if (!string.IsNullOrEmpty(luongCungText))
            {
                // Kiểm tra xem chuỗi có phải là số thập phân hợp lệ không
                if (decimal.TryParse(luongCungText, out parsedValue))
                {
                    luongCungValue = parsedValue;
                }
                else
                {
                    MessageBox.Show("Lương Cứng phải là một giá trị số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLuongCung.Focus();
                    return; // Dừng nếu nhập liệu sai
                }
            }
            // Nếu luongCungText rỗng, luongCungValue sẽ là null (để truyền DBNull.Value vào DB)
            int newRoleId = Convert.ToInt32(cboRole.SelectedValue);
            // Dòng 132 (Đã sửa lỗi ép kiểu)
            string message = TaiKhoanDB.UpdateUserAndEntity(
                _currentUsername,
               newRoleId,
                _currentNhanVienId,
                _currentGiangVienId,
                txtHoTen.Text,
                txtSDT.Text,
                txtEmail.Text,
                txtChucVu.Text,
                luongCungValue // <<< SỬ DỤNG GIÁ TRỊ ĐÃ ĐƯỢC XỬ LÝ
            );

            if (message.Contains("thành công"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
