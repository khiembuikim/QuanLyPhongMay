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
    public partial class FormQuanLyTaiKhoan : Form
    {
        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
            LoadInitialData();
        }
        private void LoadInitialData()
        {
            // Tải dữ liệu chính lên DataGridView
            LoadUserData();

            // Thiết lập các sự kiện cho Form
            SetupEventHandlers();
        }
        private void SetupEventHandlers()
        {
            // Bổ sung các sự kiện cần thiết
            dgvUserList.SelectionChanged += dgvUserList_SelectionChanged;

            // Kết nối các nút chức năng (Đảm bảo các hàm click này đã tồn tại)
            btnThemUser.Click += btnThemUser_Click;
            btnSuaUser.Click += btnSuaUser_Click;
            btnXoaUser.Click += btnXoaUser_Click;
            // ... (Thêm các nút ResetPassword, KhoaMoKhoa nếu có)
        }
        public void LoadUserData() // Đặt là public nếu muốn gọi từ Form khác, hoặc private nếu chỉ dùng nội bộ
        {
            try
            {
                // 1. Lấy dữ liệu từ DB
                DataTable dtUsers = TaiKhoanDB.GetAllUsers();

                // Gán nguồn dữ liệu
                dgvUserList.DataSource = dtUsers;

                // 2. Cấu hình các cột nếu chưa cấu hình
                if (dgvUserList.Columns.Count == 0)
                {
                    ConfigureDataGridView();
                }

                // Tùy chọn: Chọn hàng đầu tiên nếu danh sách không rỗng
                if (dgvUserList.Rows.Count > 0)
                {
                    dgvUserList.Rows[0].Selected = true;
                    dgvUserList_SelectionChanged(dgvUserList, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tài khoản: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigureDataGridView()
        {
            dgvUserList.AutoGenerateColumns = false;
            dgvUserList.Columns.Clear();

            // 1. Tên đăng nhập (Khóa chính)
            dgvUserList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "USERNAME", HeaderText = "Tên Đăng Nhập", Name = "USERNAME", Width = 150, ReadOnly = true });

            // 2. Họ Tên/Đối tượng (Sử dụng cột đã NVL trong DB)
            dgvUserList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TEN_NGUOI_DUNG", HeaderText = "Họ Tên/Đối tượng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            // 3. Quyền
            dgvUserList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TEN_QUYEN", HeaderText = "Quyền", Width = 100, ReadOnly = true });

            // 4. Trạng thái (IS_ACTIVE) - Cần dùng CellFormatting để hiện thị rõ ràng
            DataGridViewTextBoxColumn statusCol = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IS_ACTIVE",
                HeaderText = "Trạng Thái",
                Name = "IS_ACTIVE_COL",
                Width = 120,
                ReadOnly = true
            };
            dgvUserList.Columns.Add(statusCol);

            // 5. Cột Ẩn (Thông tin liên kết và Role ID)
            dgvUserList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ROLE_ID", Name = "ROLE_ID", Visible = false });
            dgvUserList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NHANVIEN_ID", Name = "NHANVIEN_ID", Visible = false });
            dgvUserList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GIANGVIEN_ID", Name = "GIANGVIEN_ID", Visible = false });

            // Gắn sự kiện định dạng cột Trạng Thái (1 -> "Đang hoạt động", 0 -> "Đã bị khóa")
            dgvUserList.CellFormatting += dgvUserList_CellFormatting;
        }

        private void btnThemUser_Click(object sender, EventArgs e)
        {
            // Mở FormThêmTàiKhoản dưới dạng một Dialog (cửa sổ độc lập)
            using (FormThemTaiKhoan fAdd = new FormThemTaiKhoan())
            {
                // fAdd.ShowDialog() sẽ tạm dừng Form cha cho đến khi Form con đóng
                if (fAdd.ShowDialog() == DialogResult.OK)
                {
                    // Điều kiện này chỉ đúng khi:
                    // 1. Dữ liệu được nhập hợp lệ.
                    // 2. Nút Xác nhận trong Form con được nhấn.
                    // 3. Hàm TaiKhoanDB.CreateUserAndEntity() chạy thành công.
                    // 4. Form con đã thiết lập this.DialogResult = DialogResult.OK;

                    MessageBox.Show("Thêm tài khoản mới thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách sau khi thêm mới
                    LoadUserData();
                }
                else
                {
                    // Nếu người dùng nhấn nút Hủy, đóng Form, hoặc gặp lỗi trước khi lưu DB
                    // Không làm gì, hoặc có thể hiển thị thông báo hủy.
                }
            }
        }
        private string _selectedUsername = null; // Đảm bảo biến này tồn tại
        private void dgvUserList_SelectionChanged(object sender, EventArgs e)
        {
            // Đặt lại biến chọn
            _selectedUsername = null;

            // Kiểm tra xem có hàng nào được chọn không
            if (dgvUserList.SelectedRows.Count > 0)
            {
                // Lấy hàng đầu tiên trong danh sách các hàng được chọn
                DataGridViewRow selectedRow = dgvUserList.SelectedRows[0];

                // Lấy giá trị từ cột 'USERNAME' (Đảm bảo tên cột chính xác)
                object usernameObj = selectedRow.Cells["USERNAME"].Value;

                if (usernameObj != null)
                {
                    _selectedUsername = usernameObj.ToString();

                    // Tùy chọn: Kích hoạt/Vô hiệu hóa các nút Sửa/Xóa
                    btnSuaUser.Enabled = true;
                    btnXoaUser.Enabled = true;
                }
            }
            else
            {
                // Nếu không có hàng nào được chọn, vô hiệu hóa các nút thao tác
                btnSuaUser.Enabled = false;
                btnXoaUser.Enabled = false;
            }
        }

        private void btnSuaUser_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvUserList.SelectedRows[0];

            // Lấy các khóa chính và ID liên kết từ các cột ẩn
            string username = selectedRow.Cells["USERNAME"].Value.ToString();
            int roleId = Convert.ToInt32(selectedRow.Cells["ROLE_ID"].Value);

            // ID liên kết (có thể NULL)
            object nvIdObj = selectedRow.Cells["NHANVIEN_ID"].Value;
            object gvIdObj = selectedRow.Cells["GIANGVIEN_ID"].Value;

            int? nhanVienId = nvIdObj == DBNull.Value ? (int?)null : Convert.ToInt32(nvIdObj);
            int? giangVienId = gvIdObj == DBNull.Value ? (int?)null : Convert.ToInt32(gvIdObj);

            // 1. TẠO HÀM DB MỚI ĐỂ LẤY CHI TIẾT DỮ LIỆU ĐẦY ĐỦ (Họ tên, SDT, Email,...)
            // Hàm này phải trả về một đối tượng chứa tất cả thông tin cần hiển thị
            // Giả định bạn đã tạo một hàm DB: TaiKhoanDB.GetUserDetails(username, nhanVienId, giangVienId)

            DataTable dtDetails = TaiKhoanDB.GetUserDetails(username, nhanVienId, giangVienId);

            if (dtDetails == null || dtDetails.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy chi tiết người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow row = dtDetails.Rows[0];

            // 2. MỞ FORM SỬA VÀ TRUYỀN DỮ LIỆU
            // Bạn nên tạo một Form mới FormSuaTaiKhoan.cs dựa trên FormThemTaiKhoan.cs
            // và truyền DataRow vào constructor để Form Sửa tự động điền dữ liệu.

            using (FormSuaTaiKhoan fEdit = new FormSuaTaiKhoan(row))
            {
                if (fEdit.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserData(); // Tải lại danh sách sau khi sửa
                }
            }
        }

        private void btnXoaUser_Click(object sender, EventArgs e)
        {
            if (dgvUserList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để vô hiệu hóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy Username của dòng được chọn
            string usernameToDisable = dgvUserList.SelectedRows[0].Cells["USERNAME"].Value.ToString();

            // Lấy trạng thái hiện tại (Đang hoạt động/Đã bị khóa)
            string statusText = dgvUserList.SelectedRows[0].Cells["IS_ACTIVE"].Value.ToString();

            // 1. KIỂM TRA TÀI KHOẢN HIỆN TẠI
            if (statusText == "Đã bị khóa")
            {
                MessageBox.Show($"Tài khoản '{usernameToDisable}' đã bị khóa rồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. XÁC NHẬN TỪ NGƯỜI DÙNG (ADMIN)
            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn vô hiệu hóa (khóa) tài khoản '{usernameToDisable}' này không?",
                "Xác nhận vô hiệu hóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // 3. GỌI HÀM DB
                string resultMessage = TaiKhoanDB.DisableUser(usernameToDisable);

                if (resultMessage.Contains("thành công"))
                {
                    MessageBox.Show(resultMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 4. TẢI LẠI DỮ LIỆU ĐỂ CẬP NHẬT TRẠNG THÁI
                    LoadUserData();
                }
                else
                {
                    // Thông báo lỗi DB
                    MessageBox.Show(resultMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUserList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Chỉ xử lý khi cột là cột Trạng Thái (IS_ACTIVE_COL) và giá trị không rỗng
            if (dgvUserList.Columns[e.ColumnIndex].Name == "IS_ACTIVE_COL" && e.Value != null)
            {
                // Chuyển đổi giá trị sang chuỗi để so sánh
                string statusValue = e.Value.ToString();

                if (statusValue == "1")
                {
                    e.Value = "Đang hoạt động";
                    e.CellStyle.ForeColor = Color.Green;
                }
                else if (statusValue == "0")
                {
                    e.Value = "Đã bị khóa";
                    e.CellStyle.ForeColor = Color.Red;
                }

                // Xác nhận đã định dạng xong
                e.FormattingApplied = true;
            }
        }
    }
}
