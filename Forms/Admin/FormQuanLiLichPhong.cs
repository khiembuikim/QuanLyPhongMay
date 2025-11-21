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
    public partial class FormQuanLiLichPhong : Form
    {
        private readonly int _adminNhanVienId;
        public FormQuanLiLichPhong(int nhanVienId)
        {
            InitializeComponent();
            _adminNhanVienId = nhanVienId;
            LoadFilterControls();
        }
        private void LoadFilterControls()
        {
            // Khởi tạo ComboBox trạng thái
            cboTrangThaiLoc.Items.AddRange(new string[] { "Tất cả", "CHO_DUYET", "DA_DUYET", "DA_HUY" });
            cboTrangThaiLoc.SelectedIndex = 1; // Mặc định hiển thị CHỜ DUYỆT

            // Khởi tạo DatePicker
            dtpNgayLoc.Value = DateTime.Now.Date;

            // Gọi hàm tải dữ liệu ban đầu
            LoadLichDat();
        }

        private void LoadLichDat()
        {
            try
            {
                DateTime ngayLoc = dtpNgayLoc.Value.Date;
                string trangThai = cboTrangThaiLoc.SelectedItem.ToString();

                // Tải dữ liệu từ DB
                dgvLichDat.DataSource = LichDatDB.GetLichDat(ngayLoc, trangThai);

                // Tùy chỉnh DataGridView (ví dụ: đổi tên cột, ẩn cột)
                dgvLichDat.Columns["Mã"].Visible = false; // Ẩn cột ID

                // Đổi tên trạng thái cho dễ đọc
                foreach (DataGridViewRow row in dgvLichDat.Rows)
                {
                    if (row.Cells["Trạng thái"].Value != null)
                    {
                        string tt = row.Cells["Trạng thái"].Value.ToString();
                        if (tt == "CHO_DUYET") row.Cells["Trạng thái"].Value = "Chờ duyệt";
                        else if (tt == "DA_DUYET") row.Cells["Trạng thái"].Value = "Đã duyệt";
                        else if (tt == "DA_HUY") row.Cells["Trạng thái"].Value = "Đã hủy";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch: " + ex.Message);
            }
        }

        private void FormQuanLiLichPhong_Load(object sender, EventArgs e)
        {

        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadLichDat();
        }
        private int GetSelectedLichId()
        {
            if (dgvLichDat.SelectedRows.Count > 0)
            {
                // Lấy giá trị của cột "Mã" (LICH_ID)
                return Convert.ToInt32(dgvLichDat.SelectedRows[0].Cells["Mã"].Value);
            }
            return -1;
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            int lichId = GetSelectedLichId();
            if (lichId != -1)
            {
                // SỬ DỤNG ID THỰC TẾ
                string message = LichDatDB.DuyetLich(lichId, _adminNhanVienId, "APPROVE", "Đã duyệt bởi Admin");
                MessageBox.Show(message);
                LoadLichDat();
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            int lichId = GetSelectedLichId();
            if (lichId == -1)
            {
                MessageBox.Show("Vui lòng chọn một lịch cần Từ chối/Hủy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Xác nhận hành động
            if (MessageBox.Show($"Bạn có chắc chắn muốn TỪ CHỐI lịch đặt phòng ID: {lichId}?", "Xác nhận Hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 2. Tùy chọn: Mở hộp thoại nhập lý do (Giả định bạn có hàm InputBox hoặc Form nhập lý do)
            // Nếu chưa có, ta dùng giá trị cố định.
            // string ghiChu = InputBox.Show("Nhập lý do từ chối:", "Lý do");
            string ghiChu = "Đã từ chối do trùng lịch hoặc không hợp lệ."; // Giá trị mặc định

            // 3. Gọi hàm DB để từ chối (REJECT)
            try
            {
                // Sử dụng _adminNhanVienId (đã được truyền qua Constructor)
                string message = LichDatDB.DuyetLich(lichId, _adminNhanVienId, "REJECT", ghiChu);
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLichDat(); // Tải lại DataGridView để cập nhật trạng thái
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi từ chối lịch: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int lichId = GetSelectedLichId();
            if (lichId == -1)
            {
                MessageBox.Show("Vui lòng chọn một lịch cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Kiểm tra trạng thái lịch trước khi xóa
            // Lấy trạng thái từ hàng đang chọn trong DataGridView
            string trangThaiHienTai = dgvLichDat.SelectedRows[0].Cells["Trạng thái"].Value.ToString();

            // CHỈ CHO PHÉP XÓA LỊCH ĐÃ HỦY (DA_HUY)
            if (trangThaiHienTai != "Đã hủy" && trangThaiHienTai != "DA_HUY")
            {
                MessageBox.Show("Chỉ được phép xóa các lịch có trạng thái Đã Hủy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Xác nhận
            if (MessageBox.Show($"Xác nhận XÓA vĩnh viễn lịch ID: {lichId}? Hành động này không thể hoàn tác.", "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
            {
                return;
            }

            // 3. Gọi hàm DB để xóa
            string message = LichDatDB.XoaLich(lichId);
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadLichDat(); // Tải lại DataGridView
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            int lichId = GetSelectedLichId();
            if (lichId == -1)
            {
                MessageBox.Show("Vui lòng chọn một lịch để xem chi tiết.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // TODO: Viết hàm LichDatDB.GetLichChiTiet(lichId) trả về DataRow hoặc Custom Object
                // Ví dụ: DataRow lichChiTiet = LichDatDB.GetLichChiTiet(lichId);

                // Khởi tạo Form chi tiết và truyền ID hoặc đối tượng dữ liệu vào
                // Giả sử FormLichChiTiet có Constructor nhận ID:
                // Admin.FormLichChiTiet formChiTiet = new Admin.FormLichChiTiet(lichId);
                // formChiTiet.ShowDialog();

                MessageBox.Show($"Chức năng: Mở Form chi tiết cho Lịch ID: {lichId} (Cần code FormLichChiTiet).", "Xem Chi Tiết");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết lịch: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}