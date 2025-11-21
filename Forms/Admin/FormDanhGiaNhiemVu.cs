using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LTTQ_QLPM.Forms.Admin;
namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public partial class FormDanhGiaNhiemVu : Form
    {
        private readonly int _adminNhanVienId;
        private int _selectedAssignId = -1; // Lưu ASSIGN_ID của nhiệm vụ đang chọn

        // Map Hệ số nhân thưởng theo yêu cầu bài toán
        private Dictionary<string, (string MaDB, decimal PhanTram)> DanhGiaMap =
            new Dictionary<string, (string, decimal)>
        {
            {"Xuất sắc (120%)", ("XS", 1.20m)},
            {"Rất tốt (100%)", ("RT", 1.00m)},
            {"Tốt (80%)", ("T", 0.80m)},
            {"Khá (60%)", ("KHA", 0.60m)},
            {"Trên Trung bình (40%)", ("TTB", 0.40m)},
            {"Trung bình (20%)", ("TB", 0.20m)},
            {"Dưới Trung bình (0%)", ("DTB", 0.00m)},
            {"Kém (-25%)", ("KEM", -0.25m)},
            {"Rất Kém (-50%)", ("RKEM", -0.50m)},
            {"Thất bại (-75%)", ("TBai", -0.75m)}
        };
        public FormDanhGiaNhiemVu(int adminNhanVienId)
        {
            InitializeComponent();
            this._adminNhanVienId = adminNhanVienId;
            LoadInitialData();
        }
        private void LoadInitialData()
        {
            // 1. Tải danh sách các mức đánh giá vào ComboBox
            cboMucDanhGia.DataSource = DanhGiaMap.Keys.ToList();
            cboMucDanhGia.SelectedIndex = -1;

            // 2. Tải danh sách nhiệm vụ cần đánh giá vào DataGridView
            LoadTasksToEvaluate();

            // Khởi tạo các sự kiện
            dgvTaskList.SelectionChanged += dgvTaskList_SelectionChanged;

            cboMucDanhGia.SelectedIndexChanged += cboMucDanhGia_SelectedIndexChanged;
            // Giả sử nút Xác nhận/Đánh giá của bạn có tên là 'btnDanhGia'
            // Nếu tên khác, ví dụ: 'btnXacNhan', bạn cần đổi tên dưới đây.
            btnDanhGia.Click += btnDanhGia_Click;
        }
        private void LoadTasksToEvaluate()
        {
            // Gọi hàm DB mới để lấy danh sách nhiệm vụ chờ đánh giá
            dgvTaskList.DataSource = NhiemVuDB.GetTasksToEvaluate();
            // Cài đặt lại trạng thái khi load lại
            _selectedAssignId = -1;
            txtHeSoNhanDuoc.Text = ""; // Giả sử bạn có TextBox này
            // ... (Cấu hình hiển thị cột cho dgvTaskList)
        }
        private void ConfigureDataGridView()
        {
            // Thiết lập AutoGenerateColumns = false nếu bạn muốn kiểm soát cột
            dgvTaskList.AutoGenerateColumns = false;
            dgvTaskList.Columns.Clear();

            // Cột 1: ASSIGN_ID (Ẩn - dùng để lấy ID khi đánh giá)
            dgvTaskList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ASSIGN_ID",
                HeaderText = "ID Gán",
                Name = "ASSIGN_ID",
                Visible = false
            });

            // Cột 2: Tên Task
            dgvTaskList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TEN_TASK",
                HeaderText = "Tên Nhiệm Vụ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            });

            // Cột 3: Nhân viên nhận
            dgvTaskList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NHANVIEN_NHAN",
                HeaderText = "Nhân Viên",
                Name = "NHANVIEN_NHAN", // Để truy cập tên nhân viên
                Width = 150
            });

            // Cột 4: Điểm Gốc (HE_SO_GIAO)
            dgvTaskList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HE_SO_GIAO",
                HeaderText = "Điểm Gốc",
                Name = "HE_SO_GIAO",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
            });
        }
        private void cboMucDanhGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMucDanhGia.SelectedItem != null && _selectedAssignId != -1)
            {
                // Lấy Điểm Gốc (HE_SO_GIAO) từ hàng đang được chọn trong DataGridView
                if (dgvTaskList.SelectedRows.Count == 0) return;
                DataGridViewRow selectedRow = dgvTaskList.SelectedRows[0];

                // Cần kiểm tra giá trị HE_SO_GIAO có hợp lệ không
                if (!decimal.TryParse(selectedRow.Cells["HE_SO_GIAO"].Value.ToString(), out decimal heSoGoc))
                {
                    MessageBox.Show("Lỗi: Không lấy được Điểm Gốc nhiệm vụ hợp lệ.", "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy Hệ số nhân thưởng từ ComboBox
                string selectedKey = cboMucDanhGia.SelectedItem.ToString();
                decimal phanTram = DanhGiaMap[selectedKey].PhanTram; // Ví dụ: 1.2m

                // Công thức: Hệ số cuối cùng nhận được = Điểm Gốc * Hệ số nhân thưởng
                decimal tongHeSoNhan = heSoGoc * phanTram;

                // Hiển thị kết quả tính toán vào TextBox
                // Lưu ý: Tên control bạn dùng là 'txtHeSoNhanDuoc'
                txtHeSoNhanDuoc.Text = tongHeSoNhan.ToString("N2");
            }
            else
            {
                // Reset nếu không có nhiệm vụ hoặc mức đánh giá được chọn
                txtHeSoNhanDuoc.Clear();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvTaskList_SelectionChanged(object sender, EventArgs e)
        {
            // Reset các control hiển thị thông tin khi không có hàng nào được chọn
            if (dgvTaskList.SelectedRows.Count == 0)
            {
                _selectedAssignId = -1;
                lblTaskName.Text = "";
                lblNhanVien.Text = "";
                txtHeSoNhanDuoc.Clear();
                cboMucDanhGia.SelectedIndex = -1;
                return;
            }

            DataGridViewRow selectedRow = dgvTaskList.SelectedRows[0];

            // Lấy ASSIGN_ID và Điểm Gốc
            if (int.TryParse(selectedRow.Cells["ASSIGN_ID"].Value.ToString(), out int assignId))
            {
                _selectedAssignId = assignId;

                // Cập nhật các Label
                lblTaskName.Text = selectedRow.Cells["TEN_TASK"].Value.ToString();
                lblNhanVien.Text = selectedRow.Cells["NHANVIEN_NHAN"].Value.ToString();

                // Reset kết quả tính toán
                txtHeSoNhanDuoc.Clear();
                cboMucDanhGia.SelectedIndex = -1;
            }
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            if (_selectedAssignId == -1)
            {
                MessageBox.Show("Vui lòng chọn một nhiệm vụ cần đánh giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboMucDanhGia.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn mức đánh giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy các giá trị cần thiết
            string selectedKey = cboMucDanhGia.SelectedItem.ToString();
            string danhGiaMa = DanhGiaMap[selectedKey].MaDB;
            decimal phanTram = DanhGiaMap[selectedKey].PhanTram;

            // Giả sử Ghi chú là từ rtxtGhiChuRichTextBox (hoặc control rtxtGhiChu của bạn)
            // Nếu bạn có control rtxtGhiChu, hãy dùng tên đó
            string ghiChu = rtxtGhiChu.Text;

            // Gọi hàm DB để cập nhật đánh giá
            // (Đảm bảo hàm NhiemVuDB.UpdateTaskEvaluation đã được định nghĩa đúng như trước)
            string message = NhiemVuDB.UpdateTaskEvaluation(
                _selectedAssignId,
                danhGiaMa,
                phanTram,
                _adminNhanVienId,
                ghiChu
            );

            MessageBox.Show(message, "Thông báo");

            if (message.Contains("thành công"))
            {
                LoadTasksToEvaluate(); // Tải lại danh sách sau khi đánh giá
            }
        }
    }
}
