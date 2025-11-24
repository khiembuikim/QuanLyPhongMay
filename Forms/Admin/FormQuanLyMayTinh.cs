using BTL_LTTQ_QLPM.Databases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public partial class FormQuanLyMayTinh : Form
    {


        public FormQuanLyMayTinh()
        {
            InitializeComponent();
            LoadDataToComboBoxes();
            LoadMayTinh();

            // Gán sự kiện lọc
            this.cboFilterPhong.SelectedIndexChanged += cboLoc_SelectedIndexChanged;
            this.cboFilterTrangThai.SelectedIndexChanged += cboLoc_SelectedIndexChanged; // Khởi tạo các components (controls UI)
                                                                                      // Thiết lập sự kiện cho nút Refresh/Lọc

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

            cboFilterPhong.DataSource = dtPhong;
            cboFilterPhong.DisplayMember = "TEN_PHONG";
            cboFilterPhong.ValueMember = "PHONG_ID";
            cboFilterPhong.SelectedIndex = 0;

            // 2. Lọc theo Trạng Thái
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Display");
            dtTrangThai.Columns.Add("Value");

            dtTrangThai.Rows.Add("Tất cả Trạng thái", "TatCa"); // Lọc không cần điều kiện WHERE
            dtTrangThai.Rows.Add("Hoạt động tốt (TOT)", "TOT");
            dtTrangThai.Rows.Add("Đang Bảo trì", "BAOTRI");
            dtTrangThai.Rows.Add("Bị Lỗi (LOI:XXX)", "LOI"); // Sẽ lọc LIKE 'LOI:%'

            cboFilterTrangThai.DataSource = dtTrangThai;
            cboFilterTrangThai.DisplayMember = "Display";
            cboFilterTrangThai.ValueMember = "Value";
            cboFilterTrangThai.SelectedIndex = 0;
        }

        private void LoadMayTinh()
        {
            try
            {
                // 1. Lấy giá trị PHÒNG MÁY an toàn
                int phongIdSelected = 0;
                // Kiểm tra cboLocPhong và SelectedValue KHÔNG NULL
                if (cboFilterPhong != null && cboFilterPhong.SelectedValue != null && cboFilterPhong.SelectedValue != DBNull.Value)
                {
                    phongIdSelected = Convert.ToInt32(cboFilterPhong.SelectedValue);
                }

                // 2. Lấy giá trị TRẠNG THÁI an toàn
                string trangThai = "TatCa"; // Mặc định là 'TatCa'
                                            // Kiểm tra cboLocTrangThai và SelectedValue KHÔNG NULL
                if (cboFilterTrangThai != null && cboFilterTrangThai.SelectedValue != null)
                {
                    // Nếu có giá trị, gán giá trị
                    trangThai = cboFilterTrangThai.SelectedValue.ToString();
                }

                // 3. Chuẩn bị tham số int?
                int? filterPhongId = (phongIdSelected != 0) ? (int?)phongIdSelected : null;

                // 4. Gọi hàm lọc (Giả định hàm GetMayTinhFiltered(int?, string) đã được giữ lại và hàm object đã bị xóa)
                DataTable dt = MayTinhDB.GetMayTinhFiltered(filterPhongId, trangThai);

                // 5. Gán và cấu hình (Đã đúng)
                dgvDanhSachMay.DataSource = dt;

                // Cấu hình cột
                // ... (phần cấu hình cột giữ nguyên) ...
                dgvDanhSachMay.Columns["MAY_ID"].Visible = false;
                dgvDanhSachMay.Columns["PHONG_ID"].Visible = false;

                dgvDanhSachMay.Columns["MA_MAY"].HeaderText = "Mã Máy";
                dgvDanhSachMay.Columns["TEN_PHONG"].HeaderText = "Phòng Máy";
                dgvDanhSachMay.Columns["VI_TRI"].HeaderText = "Vị Trí";
                dgvDanhSachMay.Columns["TRANG_THAI"].HeaderText = "Trạng Thái";
                dgvDanhSachMay.Columns["GHI_CHU"].HeaderText = "Ghi Chú Chi Tiết";
                dgvDanhSachMay.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu máy tính: " + ex.Message, "Lỗi Database");
            }
        }
             private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tải lại dữ liệu khi người dùng thay đổi bộ lọc
            LoadMayTinh();
        }
        private void FormQuanLyMayTinh_Load(object sender, EventArgs e)
        {
            // 1. Cấu hình bộ lọc
            SetupFilters();

            // 2. Tải dữ liệu lần đầu với bộ lọc mặc định ("Tất cả")
            object initialPhongId = cboFilterPhong.SelectedValue;
            string initialTrangThai = cboFilterTrangThai.SelectedValue?.ToString();
            LoadDanhSachMayTinh(initialPhongId, initialTrangThai);

            // 3. Cấu hình giao diện lưới
            SetupDataGridView();
        }
        private void SetupFilters()
        {
          
            SetupPhongFilter();

            // 2. Gọi hàm thiết lập Lọc Trạng Thái
            SetupTrangThaiFilter();

            // Không cần SelectedIndex = 0 ở đây vì đã gọi trong Setup*Filter()

            // Gắn sự kiện SelectedIndexChanged ở đây
            this.cboFilterPhong.SelectedIndexChanged += new System.EventHandler(this.cboFilterPhong_SelectedIndexChanged);
            this.cboFilterTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboFilterTrangThai_SelectedIndexChanged);
        }
        private void SetupDataGridView()
        {
            // Đảm bảo có dữ liệu trước khi định dạng
            if (dgvDanhSachMay.DataSource != null && dgvDanhSachMay.Columns.Count > 0)
            {
                // Định dạng Header Text
                dgvDanhSachMay.Columns["MA_MAY"].HeaderText = "Mã Máy";
                dgvDanhSachMay.Columns["TEN_PHONG"].HeaderText = "Phòng Máy";
                dgvDanhSachMay.Columns["VI_TRI"].HeaderText = "Vị Trí";
                dgvDanhSachMay.Columns["TRANG_THAI"].HeaderText = "Trạng Thái Chi Tiết";
                dgvDanhSachMay.Columns["GHI_CHU"].HeaderText = "Ghi Chú";

                // Ẩn các cột ID
                dgvDanhSachMay.Columns["MAY_ID"].Visible = false;
                dgvDanhSachMay.Columns["PHONG_ID"].Visible = false;

                // Định dạng độ rộng
                dgvDanhSachMay.Columns["MA_MAY"].Width = 80;
                dgvDanhSachMay.Columns["TEN_PHONG"].Width = 100;
                dgvDanhSachMay.Columns["VI_TRI"].Width = 80;
                dgvDanhSachMay.Columns["TRANG_THAI"].Width = 150;
                dgvDanhSachMay.Columns["GHI_CHU"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void LoadDanhSachMayTinh(object filterPhongId, string filterTrangThai)
        {
            // --- 1. Xử lý PHONG_ID ---
            int phongIdSelected = 0;
            if (filterPhongId != null && filterPhongId != DBNull.Value)
            {
                // Chuyển PHONG_ID sang int
                if (int.TryParse(filterPhongId.ToString(), out int id))
                {
                    phongIdSelected = id;
                }
            }

            // Chuyển sang int? (null nếu là "Tất cả" = 0)
            int? filterPhongIdInt = (phongIdSelected != 0) ? (int?)phongIdSelected : null;

            // --- 2. Xử lý TRANG THÁI ---
            string trangThai = filterTrangThai ?? "TatCa";

            try
            {
                // 🌟 GỌI HÀM DB
                DataTable dtMayTinh = MayTinhDB.GetMayTinhWithFilter(filterPhongIdInt, trangThai);
                dgvDanhSachMay.DataSource = dtMayTinh;

                // 💡 Định dạng DataGridView sau khi đã tải dữ liệu thành công
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách máy tính: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddMay_Click(object sender, EventArgs e)
        {
            // Mở Form thêm máy tính
            FormThemMayTinh formThem = new FormThemMayTinh();

            // Tải lại dữ liệu nếu thêm thành công
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                // Lấy lại bộ lọc hiện tại để tải dữ liệu
                object phongId = cboFilterPhong.SelectedValue;
                string trangThai = cboFilterTrangThai.SelectedValue?.ToString();

                // Tải lại DataGridView
                LoadDanhSachMayTinh(phongId, trangThai);
            }
        }

      

        private void btnSuaViTri_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachMay.SelectedRows.Count > 0)
            {
                try
                {
                    // 1. Lấy MAY_ID từ dòng được chọn
                    // Đảm bảo cột MAY_ID đã được trả về từ hàm GetAllMayTinh/GetMayTinhFiltered
                    int mayId = Convert.ToInt32(dgvDanhSachMay.SelectedRows[0].Cells["MAY_ID"].Value);

                    // 2. Mở Form sửa và truyền ID máy
                    FormSuaViTri formSua = new FormSuaViTri(mayId);

                    // 3. Tải lại dữ liệu nếu sửa thành công
                    if (formSua.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy lại bộ lọc hiện tại để tải dữ liệu
                        object phongId = cboFilterPhong.SelectedValue;
                        string trangThai = cboFilterTrangThai.SelectedValue?.ToString();
                        LoadDanhSachMayTinh(phongId, trangThai);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: Vui lòng chọn một dòng hợp lệ để sửa vị trí.\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một máy tính để sửa vị trí.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void cboFilterPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy ID phòng được chọn (có thể là null/DBNull nếu có tùy chọn "Tất cả")
            object phongId = cboFilterPhong.SelectedValue;

            // Lấy Trạng thái được chọn
            string trangThai = cboFilterTrangThai.SelectedValue?.ToString();

            LoadDanhSachMayTinh(phongId, trangThai);
        }

        private void cboFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy ID phòng được chọn
            object phongId = cboFilterPhong.SelectedValue;

            // Lấy Trạng thái được chọn
            string trangThai = cboFilterTrangThai.SelectedValue?.ToString();

            LoadDanhSachMayTinh(phongId, trangThai);
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachMay.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để khôi phục.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Lấy ID và xác nhận
            int mayId = Convert.ToInt32(dgvDanhSachMay.SelectedRows[0].Cells["MAY_ID"].Value);
            string maMay = dgvDanhSachMay.SelectedRows[0].Cells["MA_MAY"].Value.ToString();

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn khôi phục máy '{maMay}' về trạng thái TỐT không? (Mọi ghi chú lỗi sẽ bị xóa)",
                "Xác nhận Khôi phục",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // 2. Gọi hàm DB Khôi phục (dùng hàm xóa Ghi chú)
                string resultMessage = MayTinhDB.CapNhatTrangThaiVaXoaGhiChu(mayId, "TOT"); // Thiết lập trạng thái là TOT

                if (resultMessage.Contains("thành công"))
                {
                    MessageBox.Show(resultMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 3. Tải lại dữ liệu
                    LoadDanhSachMayTinh(cboFilterPhong.SelectedValue, cboFilterTrangThai.SelectedValue?.ToString());
                }
                else
                {
                    MessageBox.Show(resultMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBaoTri_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachMay.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để chuyển sang Bảo trì.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Lấy ID và xác nhận
            int mayId = Convert.ToInt32(dgvDanhSachMay.SelectedRows[0].Cells["MAY_ID"].Value);
            string maMay = dgvDanhSachMay.SelectedRows[0].Cells["MA_MAY"].Value.ToString();

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn chuyển máy '{maMay}' sang trạng thái BẢO TRÌ không? (Ghi chú lỗi cũ sẽ được giữ lại)",
                "Xác nhận Bảo trì",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // 2. Gọi hàm DB Bảo trì (dùng hàm giữ Ghi chú)
                string resultMessage = MayTinhDB.CapNhatTrangThaiGiuGhiChu(mayId, "BAOTRI"); // Thiết lập trạng thái là BAOTRI

                if (resultMessage.Contains("thành công"))
                {
                    MessageBox.Show(resultMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 3. Tải lại dữ liệu
                    LoadDanhSachMayTinh(cboFilterPhong.SelectedValue, cboFilterTrangThai.SelectedValue?.ToString());
                }
                else
                {
                    MessageBox.Show(resultMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGhiLoi_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachMay.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để ghi lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Lấy MAY_ID từ dòng được chọn
                int mayId = Convert.ToInt32(dgvDanhSachMay.SelectedRows[0].Cells["MAY_ID"].Value);

                // 2. Mở Form ghi lỗi (FormGhiLoi) và truyền ID máy
                // ⚠️ Đảm bảo FormGhiLoi đã được tạo và có code gọi MayTinhDB.GhiLoi()
                FormGhiLoi formGhiLoi = new FormGhiLoi(mayId);

                // 3. Tải lại dữ liệu nếu ghi lỗi thành công
                if (formGhiLoi.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachMayTinh(cboFilterPhong.SelectedValue, cboFilterTrangThai.SelectedValue?.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Vui lòng chọn một dòng hợp lệ để ghi lỗi.\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetupTrangThaiFilter()
        {
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Code", typeof(string));
            dtTrangThai.Columns.Add("Name", typeof(string));

            // Thêm tùy chọn "Tất cả"
            dtTrangThai.Rows.Add("TatCa", "--- Tất cả Trạng Thái ---");
            dtTrangThai.Rows.Add("TOT", "Tốt");
            dtTrangThai.Rows.Add("LOI", "Lỗi (Bao gồm lỗi chi tiết)");
            dtTrangThai.Rows.Add("BAOTRI", "Đang bảo trì");

            // Gán và cấu hình ComboBox
            cboFilterTrangThai.DataSource = dtTrangThai;
            cboFilterTrangThai.DisplayMember = "Name";
            cboFilterTrangThai.ValueMember = "Code";

            // Gán SelectedIndex = 0 ở đây là thừa vì đã có trong SetupFilters(), 
            // nhưng ta giữ lại để đảm bảo ComboBox có giá trị mặc định ngay khi được gán.
            cboFilterTrangThai.SelectedIndex = 0;
        }
        private void SetupPhongFilter()
        {
            // 1. Lấy dữ liệu từ DB 
            DataTable dtPhong = Databases.PhongMayDB.GetLookupPhongMay();

            // 🚨 Sửa lỗi: Nếu dtPhong bị NULL hoặc không có cấu trúc cột
            if (dtPhong == null || dtPhong.Columns.Count == 0 || !dtPhong.Columns.Contains("PHONG_ID"))
            {
                // Nếu DB bị lỗi, ta tạo cấu trúc mặc định để không crash khi gọi NewRow()
                DataTable tempDt = new DataTable();
                // Dùng typeof(int) vì giá trị 0 được sử dụng
                tempDt.Columns.Add("PHONG_ID", typeof(int));
                tempDt.Columns.Add("TEN_PHONG", typeof(string));
                dtPhong = tempDt;
            }

            // --- 3. Thêm dòng "Tất cả Phòng" (PHONG_ID = 0) ---
            DataRow allRow = dtPhong.NewRow();
            allRow["PHONG_ID"] = 0;
            allRow["TEN_PHONG"] = "--- Tất cả Phòng ---";
            dtPhong.Rows.InsertAt(allRow, 0);

            // 4. Gán và cấu hình ComboBox
            cboFilterPhong.DataSource = dtPhong;
            cboFilterPhong.DisplayMember = "TEN_PHONG";
            cboFilterPhong.ValueMember = "PHONG_ID";
            cboFilterPhong.SelectedIndex = 0;
        }
    }
}
