using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public partial class FormMainAdmin : Form
    {
        private readonly int _currentNhanVienId;
        public FormMainAdmin(int nhanVienId, UserSessionInfo userSessionInfo)
        {
            InitializeComponent();
            this._currentNhanVienId = nhanVienId;
            picAvatar.Paint += new PaintEventHandler(picAvatar_Paint);
            UpdateUserInfo(userSessionInfo);

            // Tải ảnh mặc định khi Form khởi tạo
            LoadDefaultAvatar();
        }

        private void FormMainAdmin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void picAvatar_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);

            // Đặt vùng cắt của PictureBox theo đường dẫn hình tròn này
            // Điều này đảm bảo rằng bất kỳ nội dung nào được vẽ bên trong PictureBox
            // sẽ chỉ hiển thị trong hình tròn.
            picAvatar.Region = new Region(gp);

            // (Tùy chọn) Vẽ một đường viền quanh hình tròn nếu bạn muốn
            // e.Graphics.DrawEllipse(Pens.Black, 0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
        }
        private void LoadDefaultAvatar()
        {
            // CẤU HÌNH QUAN TRỌNG:
            // Tên file của bạn là: avartar-fb-mac-dinh-14.jpg
            // Folder chứa file là: Resources (nằm cùng cấp với file .exe của bạn)

            // Đường dẫn tuyệt đối sẽ là: [Thư mục ứng dụng]\Resources\avartar-fb-mac-dinh-14.jpg
            string defaultImagePath = Path.Combine(
                Application.StartupPath,
                "Resources",
                "avatar-fb-mac-dinh-14.jpg" // TÊN FILE GỐC CỦA BẠN
            );

            if (File.Exists(defaultImagePath))
            {
                try
                {
                    // Tải ảnh từ đường dẫn file và tạo bản sao Bitmap 
                    // để đảm bảo file gốc không bị khóa (lock file)
                    using (Image tempImage = Image.FromFile(defaultImagePath))
                    {
                        picAvatar.Image = new Bitmap(tempImage);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu file tồn tại nhưng bị lỗi (ví dụ: thiếu quyền, file hỏng)
                    MessageBox.Show($"Lỗi khi tải ảnh từ folder Resources: {ex.Message}", "Lỗi Tải Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    picAvatar.Image = null;
                    picAvatar.BackColor = Color.LightGray;
                }
            }
            else
            {
                // Thông báo nếu file không tìm thấy (thường là do chưa đặt 'Copy to Output Directory')
                MessageBox.Show($"Không tìm thấy file ảnh tại đường dẫn: {defaultImagePath}. Vui lòng kiểm tra lại cấu hình 'Copy to Output Directory' cho file ảnh.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                picAvatar.Image = null;
                picAvatar.BackColor = Color.LightGray;
            }
        }

        private void pnlUserInfo_Paint(object sender, PaintEventArgs e)
        {

        }
        public void UpdateUserInfo(UserSessionInfo user)
        {
            if (user != null)
            {
                // Giả định bạn có hai Label: lblRole và lblFullName

                // 1. Cập nhật Label Chức vụ
                // Ví dụ: "Quản trị viên"
                lblRole.Text = user.RoleName;

                // 2. Cập nhật Label Họ tên
                // Ví dụ: "Đặng Huy Cửu"
                lblFullName.Text = user.FullName;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyLichPhong_Click(object sender, EventArgs e)
        {
            if (_currentNhanVienId <= 0)
            {
                MessageBox.Show("Không tìm thấy thông tin ID nhân viên Admin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide(); // Ẩn FormMainAdmin

            // 2. KHỞI TẠO Form mới và TRUYỀN ID vào Constructor
            Admin.FormQuanLiLichPhong formQuanLyLichPhong = new Admin.FormQuanLiLichPhong(_currentNhanVienId);

            formQuanLyLichPhong.ShowDialog(); // Mở form con dạng modal

            this.Show(); // Hiện lại FormMainAdmin sau khi Form con đóng
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Ẩn form hiện tại
            this.Hide();

            // Hiển thị lại form đăng nhập
            FormLogin loginForm = new FormLogin();
            loginForm.Show();

        }

        private void btnGiaoNhiemVu_Click(object sender, EventArgs e)
        {
            if (_currentNhanVienId <= 0)
            {
                MessageBox.Show("Không tìm thấy thông tin ID nhân viên Admin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide(); // Ẩn FormMainAdmin

            // 2. KHỞI TẠO Form mới và TRUYỀN ID vào Constructor
            Admin.FormGiaoNhiemVu fgnv = new Admin.FormGiaoNhiemVu(_currentNhanVienId);

            fgnv.ShowDialog(); // Mở form con dạng modal

            this.Show(); // Hiện lại FormMainAdmin sau khi Form con đóng
        }

        private void btnDanhGiaNhiemVu_Click(object sender, EventArgs e)
        {
            if (_currentNhanVienId <= 0) // Giả sử đây là ID Admin
            {
                MessageBox.Show("Lỗi: Không tìm thấy ID người đánh giá (Admin).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();

            // SỬA LỖI: TRUYỀN ID ADMIN VÀO CONSTRUCTOR
            BTL_LTTQ_QLPM.Forms.Admin.FormDanhGiaNhiemVu fDGNV =
                new BTL_LTTQ_QLPM.Forms.Admin.FormDanhGiaNhiemVu(_currentNhanVienId);

            fDGNV.ShowDialog();
            this.Show();
        }

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn FormMain

            Admin.FormQuanLyTaiKhoan fqltk = new Admin.FormQuanLyTaiKhoan();
            fqltk.ShowDialog(); // Mở form con dạng modal

            this.Show();
        }

        private void btnQuanLyPhongMay_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn FormMain

            Admin.FormQuanLyPhongMay fqlpm = new Admin.FormQuanLyPhongMay();
            fqlpm.ShowDialog(); // Mở form con dạng modal

            this.Show();
        }

        private void btnQuanLyMayTinh_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn FormMain

            Admin.FormQuanLyMayTinh fqlmt = new Admin.FormQuanLyMayTinh();
            fqlmt.ShowDialog(); // Mở form con dạng modal

            this.Show();
        }
    }
}


