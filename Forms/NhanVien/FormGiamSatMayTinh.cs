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
    public partial class FormGiamSatMayTinh : Form
    {
        public FormGiamSatMayTinh()
        {
            InitializeComponent();
            LoadDataToComboBoxes();
            LoadMayTinh();

            // Gán sự kiện lọc
            this.cboLocPhong.SelectedIndexChanged += cboLoc_SelectedIndexChanged;
            this.cboLocTrangThai.SelectedIndexChanged += cboLoc_SelectedIndexChanged;
        }
        private void LoadDataToComboBoxes()
        {
            // 1. Lọc theo Phòng Máy (Sử dụng hàm GetLookupPhongMay)
            DataTable dtPhong = MayTinhDB.GetLookupPhongMay();

            // Thêm tùy chọn "Tất cả Phòng" (PHONG_ID = 0)
            DataRow allRow = dtPhong.NewRow();
            allRow["PHONG_ID"] = 0;
            allRow["TEN_PHONG"] = "Tất cả Phòng";
            dtPhong.Rows.InsertAt(allRow, 0);

            cboLocPhong.DataSource = dtPhong;
            cboLocPhong.DisplayMember = "TEN_PHONG";
            cboLocPhong.ValueMember = "PHONG_ID";
            cboLocPhong.SelectedIndex = 0;

            // 2. Lọc theo Trạng Thái
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Display");
            dtTrangThai.Columns.Add("Value");

            dtTrangThai.Rows.Add("Tất cả Trạng thái", "TatCa"); // Lọc không cần điều kiện WHERE
            dtTrangThai.Rows.Add("Hoạt động tốt (TOT)", "TOT");
            dtTrangThai.Rows.Add("Đang Bảo trì", "BAOTRI");
            dtTrangThai.Rows.Add("Bị Lỗi (LOI:XXX)", "LOI"); // Sẽ lọc LIKE 'LOI:%'

            cboLocTrangThai.DataSource = dtTrangThai;
            cboLocTrangThai.DisplayMember = "Display";
            cboLocTrangThai.ValueMember = "Value";
            cboLocTrangThai.SelectedIndex = 0;
        }
       
           private void LoadMayTinh()
        {
            try
            {
                // 1. Lấy giá trị PHÒNG MÁY an toàn
                int phongIdSelected = 0;
                // Kiểm tra cboLocPhong và SelectedValue KHÔNG NULL
                if (cboLocPhong != null && cboLocPhong.SelectedValue != null && cboLocPhong.SelectedValue != DBNull.Value)
                {
                    phongIdSelected = Convert.ToInt32(cboLocPhong.SelectedValue);
                }

                // 2. Lấy giá trị TRẠNG THÁI an toàn
                string trangThai = "TatCa"; // Mặc định là 'TatCa'
                                            // Kiểm tra cboLocTrangThai và SelectedValue KHÔNG NULL
                if (cboLocTrangThai != null && cboLocTrangThai.SelectedValue != null)
                {
                    // Nếu có giá trị, gán giá trị
                    trangThai = cboLocTrangThai.SelectedValue.ToString();
                }

                // 3. Chuẩn bị tham số int?
                int? filterPhongId = (phongIdSelected != 0) ? (int?)phongIdSelected : null;

                // 4. Gọi hàm lọc (Giả định hàm GetMayTinhFiltered(int?, string) đã được giữ lại và hàm object đã bị xóa)
                DataTable dt = MayTinhDB.GetMayTinhFiltered(filterPhongId, trangThai);

                // 5. Gán và cấu hình (Đã đúng)
                dgvMayTinh.DataSource = dt;

                // Cấu hình cột
                // ... (phần cấu hình cột giữ nguyên) ...
                dgvMayTinh.Columns["MAY_ID"].Visible = false;
                dgvMayTinh.Columns["PHONG_ID"].Visible = false;

                dgvMayTinh.Columns["MA_MAY"].HeaderText = "Mã Máy";
                dgvMayTinh.Columns["TEN_PHONG"].HeaderText = "Phòng Máy";
                dgvMayTinh.Columns["VI_TRI"].HeaderText = "Vị Trí";
                dgvMayTinh.Columns["TRANG_THAI"].HeaderText = "Trạng Thái";
                dgvMayTinh.Columns["GHI_CHU"].HeaderText = "Ghi Chú Chi Tiết";
                dgvMayTinh.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu máy tính: " + ex.Message, "Lỗi Database");
            }
        }
        private void UpdateTrangThaiSelectedMay(string trangThaiMoi, string message, bool isKhôiPhục = false)
        {
            if (dgvMayTinh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để cập nhật trạng thái.", "Cảnh báo");
                return;
            }

            // Lấy MAY_ID và MA_MAY từ dòng được chọn
            int mayId = Convert.ToInt32(dgvMayTinh.SelectedRows[0].Cells["MAY_ID"].Value);
            string maMay = dgvMayTinh.SelectedRows[0].Cells["MA_MAY"].Value.ToString();
            string result;

            if (MessageBox.Show($"Xác nhận chuyển máy {maMay} sang trạng thái '{message}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (isKhôiPhục)
                    {
                        // Sử dụng hàm xóa Ghi Chú cho trạng thái TỐT
                        result = MayTinhDB.CapNhatTrangThaiVaXoaGhiChu(mayId, trangThaiMoi);
                    }
                    else
                    {
                        // Sử dụng hàm giữ lại Ghi Chú cho trạng thái BAOTRI/LOI KHAC
                        result = MayTinhDB.CapNhatTrangThaiGiuGhiChu(mayId, trangThaiMoi);
                    }

                    if (result.StartsWith("Cập nhật") || result.StartsWith("Khôi phục"))
                    {
                        MessageBox.Show(result, "Thành công");
                        LoadMayTinh(); // Tải lại lưới dữ liệu
                    }
                    else
                    {
                        MessageBox.Show(result, "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi");
                }
            }
        }
        private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tải lại dữ liệu khi người dùng thay đổi bộ lọc
            LoadMayTinh();
        }
        private void FormGiamSatMayTinh_Load(object sender, EventArgs e)
        {

        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            UpdateTrangThaiSelectedMay("TOT", "Hoạt động tốt (Khôi phục)", isKhôiPhục: true);
        }

        private void btnBaoTri_Click(object sender, EventArgs e)
        {
            UpdateTrangThaiSelectedMay("BAOTRI", "Đang Bảo trì", isKhôiPhục: false);
        }

        private void btnLoiKhac_Click(object sender, EventArgs e)
        {
            // Lỗi Khác: Cần gọi hàm GhiLoi riêng để cập nhật TRANG_THAI thành 'LOI:...' và GHI_CHU
            if (dgvMayTinh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để ghi lỗi.", "Cảnh báo");
                return;
            }

            int mayId = Convert.ToInt32(dgvMayTinh.SelectedRows[0].Cells["MAY_ID"].Value);
            string maMay = dgvMayTinh.SelectedRows[0].Cells["MA_MAY"].Value.ToString();

            // Mở hộp thoại nhập chi tiết lỗi
            string chiTietLoi = Microsoft.VisualBasic.Interaction.InputBox(
                $"Nhập chi tiết lỗi cho máy {maMay}:",
                "Ghi Nhận Lỗi Máy Tính",
                "Lỗi phần cứng/mềm...");

            if (!string.IsNullOrEmpty(chiTietLoi))
            {
                string result = MayTinhDB.GhiLoi(mayId, chiTietLoi);

                if (result.StartsWith("Ghi lỗi"))
                {
                    MessageBox.Show(result, "Thành công");
                    LoadMayTinh();
                }
                else
                {
                    MessageBox.Show(result, "Lỗi");
                }
            }
        }

        private void cboLocPhong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    }
    

