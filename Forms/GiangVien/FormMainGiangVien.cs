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

namespace BTL_LTTQ_QLPM.Forms.GiangVien
{
    public partial class FormMainGiangVien : Form
    {
        private readonly int _currentGiangVienId;
        public FormMainGiangVien(int giangVienId, UserSessionInfo userSessionInfo)
        {
            InitializeComponent();
            this._currentGiangVienId = giangVienId;

            // Nếu có PictureBox picAvatar, thêm Paint event
            picAvatar.Paint += new PaintEventHandler(picAvatar_Paint); 

            // Nếu có label Role và FullName, gọi hàm cập nhật
            UpdateUserInfo(userSessionInfo); 

            this.IsMdiContainer = true;
            LoadDefaultAvatar(); // Tải ảnh mặc định
        }
        public void UpdateUserInfo(UserSessionInfo user)
        {
            if (user != null)
            {
                // Giả định bạn có hai Label: lblRole và lblFullName

                // 1. Cập nhật Label Chức vụ
                // Ví dụ: "Giảng viên"
                lblRole.Text = user.RoleName;

                // 2. Cập nhật Label Họ tên
                lblFullName.Text = user.FullName;
            }
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
        private void picAvatar_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
            picAvatar.Region = new Region(gp);
            e.Graphics.DrawEllipse(Pens.Black, 0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FormMainGiangVien_Load(object sender, EventArgs e)
        {

        }

        private void btnDatPhongMay_Click(object sender, EventArgs e)
        {
            if (_currentGiangVienId <= 0)
            {
                MessageBox.Show("Lỗi: Không tìm thấy ID Giảng viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide(); // Ẩn form chính

            // Tên Form giả định: GiangVien.FormDatPhongMay
            using (BTL_LTTQ_QLPM.Forms.GiangVien.FormDatPhongMay f =
                   new BTL_LTTQ_QLPM.Forms.GiangVien.FormDatPhongMay(_currentGiangVienId))
            {
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog(); // Form con chạy dạng modal
            }

            this.Show(); // Hiện lại form chính sau khi form con đóng
        }

        private void btnLichDatCuaToi_Click(object sender, EventArgs e)
        {
            if (_currentGiangVienId <= 0)
            {
                MessageBox.Show("Lỗi: Không tìm thấy ID Giảng viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide(); // Ẩn form chính

            // Tên Form giả định: GiangVien.FormLichCuaToi
            using (BTL_LTTQ_QLPM.Forms.GiangVien.FormLichCuaToi f =
                   new BTL_LTTQ_QLPM.Forms.GiangVien.FormLichCuaToi(_currentGiangVienId))
            {
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog(); // Form con chạy dạng modal
            }

            this.Show(); // Hiện lại form chính sau khi form con đóng
        }

        private void btnTinhTrangPhong_Click(object sender, EventArgs e)
        {
            // Form này không cần ID giảng viên

            this.Hide(); // Ẩn form chính

            // Tên Form giả định: GiangVien.FormTinhTrangPhong
            using (BTL_LTTQ_QLPM.Forms.GiangVien.FormTinhTrangPhong f =
                   new BTL_LTTQ_QLPM.Forms.GiangVien.FormTinhTrangPhong())
            {
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog(); // Form con chạy dạng modal
            }

            this.Show(); // Hiện lại form chính sau khi form con đóng
        }

        private void pnlUserInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Ẩn form hiện tại
            this.Hide();

            // Hiển thị lại form đăng nhập
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
        }
    }
}
