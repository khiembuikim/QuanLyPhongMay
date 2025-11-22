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

namespace BTL_LTTQ_QLPM.Forms.NhanVien
{
    public partial class FormLichLamViecNhanVien : Form
    {
        private readonly int _nhanVienId;
        public FormLichLamViecNhanVien(int nhanVienId)
        {
            InitializeComponent();
            _nhanVienId = nhanVienId;

            // Chỉ cần gán sự kiện cho DateTimePicker
            this.dtpChonNgay.ValueChanged += dtpChonNgay_ValueChanged;
        }
        

        private void FormLichLamViecNhanVien_Load(object sender, EventArgs e)
        {
            FilterLich();
        }

        private void dtpChonNgay_ValueChanged(object sender, EventArgs e)
        {
            FilterLich();
        }
        private void FilterLich()
        {
            DateTime startDate;
            DateTime endDate;
            DateTime selectedDate = dtpChonNgay.Value.Date;

            // 1. Xác định khoảng thời gian của TUẦN (Từ Thứ Hai đến Chủ Nhật)

            // Lấy DayOfWeek của ngày đã chọn (Thứ Hai = 1, Chủ Nhật = 0)
            int dayOfWeek = (int)selectedDate.DayOfWeek;

            // Tính số ngày cần lùi để về ngày đầu tuần (Thứ Hai)
            // Nếu là Chủ Nhật (0) -> lùi 6 ngày
            // Nếu là Thứ Hai (1) -> lùi 0 ngày
            int daysToSubtract = (dayOfWeek == 0) ? 6 : dayOfWeek - 1;

            // Ngày bắt đầu tuần (Thứ Hai)
            startDate = selectedDate.AddDays(-daysToSubtract);

            // Ngày kết thúc tuần (Chủ Nhật)
            endDate = startDate.AddDays(7).AddMilliseconds(-1);

            // 2. Tải dữ liệu từ Database
            LoadLichLamViec(startDate, endDate);
        }
        private void LoadLichLamViec(DateTime start, DateTime end)
        {
            // ... (Phần code này giữ nguyên như trước)
            try
            {
                DataTable dtLich = TaskDB.GetLichLamViecByNhanVienId(_nhanVienId, start, end);
                dgvLichLamViec.DataSource = dtLich;

                // Cấu hình hiển thị cột (Giữ nguyên)
                dgvLichLamViec.Columns["EVENT_ID"].Visible = false;

                dgvLichLamViec.Columns["THOI_GIAN"].HeaderText = "Thời Gian (Mốc)";
                dgvLichLamViec.Columns["TEN_SU_KIEN"].HeaderText = "Chi tiết sự kiện";
                dgvLichLamViec.Columns["LOAI_SU_KIEN"].HeaderText = "Loại";
                dgvLichLamViec.Columns["TIEN_DO"].HeaderText = "Tiến độ (%)";
                dgvLichLamViec.Columns["TRANG_THAI"].HeaderText = "Trạng thái";
                dgvLichLamViec.Columns["THOI_GIAN"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Tùy chỉnh màu sắc (Giữ nguyên)
                foreach (DataGridViewRow row in dgvLichLamViec.Rows)
                {
                    string loai = row.Cells["LOAI_SU_KIEN"].Value?.ToString();
                    string trangThai = row.Cells["TRANG_THAI"].Value?.ToString();

                    if (loai == "Lịch trực ca")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCyan;
                    }
                    else if (loai == "Deadline Nhiệm vụ" && trangThai != "HOAN_THANH")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch làm việc: " + ex.Message, "Lỗi Database");
            }
        }
    }
}
