using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LTTQ_QLPM.Databases;

namespace BTL_LTTQ_QLPM.Forms.GiangVien
{
    public partial class FormLichCuaToi : Form
    {
        private readonly int _giangVienId;
        public FormLichCuaToi(int giangVienId)
        {
            InitializeComponent();
            _giangVienId = giangVienId;
            this.Text = "Lịch Đặt Phòng Của Tôi";
        }

        private void FormLichCuaToi_Load(object sender, EventArgs e)
        {
            InitializeFilters();
            LoadLichDat(0, "TatCa"); // Tải ban đầu (Tháng hiện tại, Tất cả trạng thái)
        }
        private void InitializeFilters()
        {
            // Nạp dữ liệu cho Lọc Tháng
            cboLocThang.Items.Add("Tất cả tháng");
            for (int i = 1; i <= 12; i++)
            {
                cboLocThang.Items.Add($"Tháng {i}");
            }
            cboLocThang.SelectedIndex = 0; // Mặc định: Tất cả tháng

            // Nạp dữ liệu cho Lọc Trạng thái
            cboLocTrangThai.Items.Add("Tất cả");
            cboLocTrangThai.Items.Add("CHO_DUYET");
            cboLocTrangThai.Items.Add("DA_DUYET");
            cboLocTrangThai.Items.Add("DA_HUY");
            cboLocTrangThai.SelectedIndex = 0; // Mặc định: Tất cả
        }
        private void LoadLichDat(int month, string status)
        {
            try
            {
                // Gọi hàm DB với các tham số lọc
                dgvLichDat.DataSource = LichDatDB.GetLichDatByGiangVien(_giangVienId, month, status);

                // Định dạng DataGridView
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch đặt: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatDataGridView()
        {
            // Thiết lập tên hiển thị và định dạng cho các cột
            dgvLichDat.Columns["TEN_PHONG"].HeaderText = "Phòng Máy";
            dgvLichDat.Columns["THOI_GIAN"].HeaderText = "Thời Gian";
            dgvLichDat.Columns["NGAY_DAT"].HeaderText = "Ngày Đặt";
            dgvLichDat.Columns["NGAY_DAT"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichDat.Columns["MUC_DICH"].HeaderText = "Nội Dung/Môn Học";
            dgvLichDat.Columns["TRANG_THAI"].HeaderText = "Trạng Thái";

            // Ẩn cột ID không cần thiết
            dgvLichDat.Columns["LICH_ID"].Visible = false;

            dgvLichDat.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            int selectedMonth = cboLocThang.SelectedIndex; // 0 là 'Tất cả tháng'
            string selectedStatus = cboLocTrangThai.SelectedItem.ToString();

            // Nếu chọn 'Tất cả tháng', truyền 0. Ngược lại, truyền chỉ số tháng (1-12)
            int monthToFilter = (selectedMonth == 0) ? 0 : selectedMonth;

            LoadLichDat(monthToFilter, selectedStatus);
        }
    }
}
