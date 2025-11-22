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

namespace BTL_LTTQ_QLPM.Forms.NhanVien
{
    public partial class FormMainNhanVien : Form
    {
        private readonly int _currentNhanVienId;
        public FormMainNhanVien(int nhanVienId, UserSessionInfo userSessionInfo)
        {
            InitializeComponent();
            this._currentNhanVienId = nhanVienId;
            picAvatar.Paint += new PaintEventHandler(picAvatar_Paint);
            UpdateUserInfo(userSessionInfo);
            this.IsMdiContainer = true;
            // Tải ảnh mặc định khi Form khởi tạo
            LoadDefaultAvatar();
        }

        private void FormMainNhanVien_Load(object sender, EventArgs e)
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
            e.Graphics.DrawEllipse(Pens.Black, 0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
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
       

        // (2) FormCapNhatTienDo
        


        // (4) FormBaoCaoSuCo (Gửi báo cáo lỗi máy)
       

        // (5) FormThongTinPhongMay (Chỉ xem)
        

        // (6) FormLuongThang
        

        private void btnMayTinh_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form menu

            using (FormGiamSatMayTinh f = new FormGiamSatMayTinh())
            {
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();  // Form con chạy dạng modal
            }

            this.Show(); // Hiện lại form menu sau khi form con đóng
        }

        private void btnThongTinPhongMay_Click_1(object sender, EventArgs e)
        {

        }

        private void btnNhiemVuCuaToi_Click_1(object sender, EventArgs e)
        {
            if (_currentNhanVienId <= 0)
            {
                MessageBox.Show("Lỗi: Không tìm thấy ID Nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();

            // Tên Form giả định: NhanVien.FormNhiemVuCuaToi
            BTL_LTTQ_QLPM.Forms.NhanVien.FormNhiemVuCuaToi f =
                new BTL_LTTQ_QLPM.Forms.NhanVien.FormNhiemVuCuaToi(_currentNhanVienId);

            f.ShowDialog();
            this.Show();
        }

        private void btnLichLamViecNV_Click_1(object sender, EventArgs e)
        {
            if (_currentNhanVienId <= 0)
            {
                MessageBox.Show("Lỗi: Không tìm thấy ID Nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();

            // Tên Form giả định: NhanVien.FormLichLamViecNhanVien
            BTL_LTTQ_QLPM.Forms.NhanVien.FormLichLamViecNhanVien f =
                new BTL_LTTQ_QLPM.Forms.NhanVien.FormLichLamViecNhanVien(_currentNhanVienId);

            f.ShowDialog();
            this.Show();
        }

        private void btnLuongThang_Click_1(object sender, EventArgs e)
        {
            if (_currentNhanVienId <= 0)
            {
                MessageBox.Show("Lỗi: Không tìm thấy ID Nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();

            // Tên Form giả định: NhanVien.FormLuongThang
            BTL_LTTQ_QLPM.Forms.NhanVien.FormLuongThang f =
                new BTL_LTTQ_QLPM.Forms.NhanVien.FormLuongThang(_currentNhanVienId);

            f.ShowDialog();
            this.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Ẩn form hiện tại
            this.Hide();

            // Hiển thị lại form đăng nhập
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
        }

        private void lblRole_Click(object sender, EventArgs e)
        {


        }
    }
}
