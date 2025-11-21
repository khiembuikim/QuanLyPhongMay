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
    public partial class FormThemTaiKhoan : Form
    {
        public FormThemTaiKhoan()
        {
            InitializeComponent();
            LoadRoles();
            // Setup sự kiện để ẩn/hiện các trường
            cboRole.SelectedIndexChanged += cboRole_SelectedIndexChanged;
        }
        private void LoadRoles()
        {
            // Sử dụng hàm đã có trong TaiKhoanDB
            DataTable dtRoles = TaiKhoanDB.GetAllRoles();

            cboRole.DataSource = dtRoles;
            cboRole.DisplayMember = "ROLE_NAME";
            cboRole.ValueMember = "ROLE_ID";
            cboRole.SelectedIndex = -1; // Chưa chọn gì
        }

        // Thuộc tính công khai để Form cha lấy dữ liệu
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

        private void FormThemTaiKhoan_Load(object sender, EventArgs e)
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

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if (cboRole.SelectedIndex == -1 || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(HoTen))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập, Mật khẩu, Họ tên và chọn Vai trò.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra lỗi định dạng Lương
            if ((RoleId == 1 || RoleId == 2) && LuongCung == null && !string.IsNullOrEmpty(txtLuongCung.Text))
            {
                MessageBox.Show("Lương cứng không hợp lệ. Vui lòng nhập số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm DB
            string message = TaiKhoanDB.CreateUserAndEntity(
                Username,
                Password,
                RoleId,
                HoTen,
                SDT,
                Email,
                ChucVu,
                LuongCung
            );

            MessageBox.Show(message, "Thông báo");

            if (message.Contains("thành công"))
            {
                this.DialogResult = DialogResult.OK; // Báo cho Form cha biết đã tạo thành công
                this.Close();
            }
        }
    }
}
