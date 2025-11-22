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
    public partial class FormLuongThang : Form
    {
        private readonly int _loggedInNhanVienId;
        public FormLuongThang(int nhanVienId)
        {
            InitializeComponent();
            _loggedInNhanVienId = nhanVienId;
            this.Text = "Lịch Sử Lương Của Tôi"; // Tên form
        }

        private void FormLuongThang_Load(object sender, EventArgs e)
        {
            LoadLichSuLuong();
        }
        private void LoadLichSuLuong() // <--- Giả định nhận nhanVienId
        {
            // --- KHAI BÁO VÀ KHỞI TẠO BIẾN THIẾU ---
            // Khởi tạo DateTime.Now chỉ một lần
            DateTime now = DateTime.Now;

            // Khởi tạo DataTable để chứa lịch sử lương
            DataTable dtLuong = new DataTable();
            dtLuong.Columns.Add("ThangNam", typeof(string));
            dtLuong.Columns.Add("TongLuong", typeof(decimal));

            // 1. Lấy ngày bắt đầu làm việc
            object ngayBatDauObj = NhanVienDB.GetNgayBatDauLamViec(_loggedInNhanVienId);
            DateTime ngayBatDauLamViec;

            // 2. Xử lý kết quả trả về từ DB (Giải quyết lỗi 'object' to 'DateTime')
            if (ngayBatDauObj == null || ngayBatDauObj == DBNull.Value)
            {
                // Gán ngày mặc định nếu không có trong DB (ví dụ: ngày đầu tháng hiện tại)
                ngayBatDauLamViec = new DateTime(now.Year, now.Month, 1);
                MessageBox.Show("Không tìm thấy ngày bắt đầu làm việc. Sử dụng ngày đầu tháng hiện tại làm mặc định.", "Cảnh báo");
            }
            else
            {
                // Ép kiểu an toàn (Convert.ToDateTime giải quyết lỗi C#)
                ngayBatDauLamViec = Convert.ToDateTime(ngayBatDauObj);
            }

            // Khởi tạo biến lặp 'current' là ngày đầu tiên của tháng bắt đầu làm việc
            DateTime current = new DateTime(ngayBatDauLamViec.Year, ngayBatDauLamViec.Month, 1);

            // --- KHỐI XỬ LÝ CHÍNH ---
            try
            {
                // 3. Lặp qua từng tháng cho đến tháng hiện tại
                while (current <= now)
                {
                    int month = current.Month;
                    int year = current.Year;

                    // Bỏ qua tháng hiện tại nếu nó chưa kết thúc (chỉ tính lương cho các tháng ĐÃ KẾT THÚC)
                    if (year == now.Year && month == now.Month)
                    {
                        break;
                    }

                    // 4. Gọi hàm Oracle để tính lương cho tháng đó
                    // Giả định _loggedInNhanVienId = nhanVienId
                    decimal luongThang = NhanVienDB.TinhLuongThang(_loggedInNhanVienId, month, year);

                    // 5. Thêm kết quả vào DataTable
                    dtLuong.Rows.Add($"{month}/{year}", luongThang);

                    // Chuyển sang tháng tiếp theo
                    current = current.AddMonths(1);
                }

                // 6. Hiển thị kết quả lên DataGridView
                // 6. Hiển thị kết quả lên DataGridView
                dgvLichSuLuong.AutoGenerateColumns = true; // Đảm bảo DGV tạo lại cột
                dgvLichSuLuong.DataSource = dtLuong;

                // Đảm bảo các cột được tạo đúng tên trước khi định dạng
                if (dgvLichSuLuong.Columns.Contains("ThangNam"))
                {
                    dgvLichSuLuong.Columns["ThangNam"].HeaderText = "Tháng/Năm";
                    dgvLichSuLuong.Columns["TongLuong"].HeaderText = "Tổng Lương (VNĐ)";
                    dgvLichSuLuong.Columns["TongLuong"].DefaultCellStyle.Format = "N0";
                    dgvLichSuLuong.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy cột 'ThangNam' trong DataGridView. Kiểm tra lại tên cột trong DataTable.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử lương: " + ex.Message, "Lỗi Database");
            }
        }
    }
    }

