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

namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public partial class FormQuanLyMayTinh : Form
    {


        public FormQuanLyMayTinh()
        {
            InitializeComponent(); // Khởi tạo các components (controls UI)
                                   // Thiết lập sự kiện cho nút Refresh/Lọc
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            // --- 1. Gọi hàm thiết lập Lọc Phòng (Đã được định nghĩa ở dưới) ---
            SetupPhongFilter();

            // --- 2. Gọi hàm thiết lập Lọc Trạng Thái (Đã được định nghĩa ở dưới) ---
            SetupTrangThaiFilter();

            // Thiết lập chọn mặc định là dòng "Tất cả" (Index 0)
            cboFilterPhong.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;

            // 💡 Gắn sự kiện SelectedIndexChanged ở đây hoặc trong Designer
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
            // --- Xử lý PHONG_ID ---
            int? phongId = null;
            if (filterPhongId != null && filterPhongId != DBNull.Value)
            {
                // 🚨 CHỈ THỰC HIỆN TRY PARSE NẾU KHÔNG PHẢI NULL/DBNULL
                if (int.TryParse(filterPhongId.ToString(), out int id))
                {
                    phongId = id;
                }
                // Nếu không phải int hợp lệ, nó sẽ vẫn là null. Điều này giúp an toàn.
            }

            // --- Xử lý TRANG THÁI ---
            string trangThai = null;
            // Kiểm tra nếu giá trị không phải null, không phải rỗng, và không phải DBNull
            if (filterTrangThai != null && filterTrangThai != DBNull.Value.ToString() && !string.IsNullOrEmpty(filterTrangThai))
            {
                // Gán mã trạng thái (TOT, LOI, BAOTRI)
                trangThai = filterTrangThai;
            }
            // Chú ý: Nếu cboFilterTrangThai.SelectedValue là DBNull.Value, thì filterTrangThai (là string)
            // sẽ là null, điều này đã được xử lý bằng code LoadDanhSachMayTinh ở trên.

            // 🌟 GỌI HÀM DB
            // Đảm bảo rằng hàm GetMayTinhWithFilter đã được sửa ở trên
            DataTable dtMayTinh = MayTinhDB.GetMayTinhWithFilter(phongId, trangThai);
            dgvDanhSachMay.DataSource = dtMayTinh;

            // 💡 Thêm bước này để định dạng DataGridView sau khi đã tải dữ liệu thành công
            SetupDataGridView();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Lấy PHONG_ID đã chọn. SelectedValue là object, có thể là DBNull.Value
            object phongId = cboFilterPhong.SelectedValue;

            // Lấy mã Trạng Thái đã chọn. 
            // Dùng ?.ToString() để tránh lỗi nếu SelectedValue là null
            string trangThai = cboFilterTrangThai.SelectedValue?.ToString();

            // Gọi hàm tải dữ liệu với các giá trị lọc mới
            LoadDanhSachMayTinh(phongId, trangThai);
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
            // ✅ THÊM KIỂU DỮ LIỆU
            dtTrangThai.Columns.Add("Code", typeof(string)); // Mã trạng thái dùng trong DB
            dtTrangThai.Columns.Add("Name", typeof(string)); // Tên hiển thị cho người dùng

            // Thêm tùy chọn "Tất cả"
            dtTrangThai.Rows.Add(DBNull.Value, "--- Tất cả Trạng Thái ---"); // Đổi thành format dễ nhìn

            // Thêm các trạng thái cụ thể
            dtTrangThai.Rows.Add("TOT", "Tốt");
            dtTrangThai.Rows.Add("LOI", "Lỗi (Bao gồm lỗi chi tiết)");
            dtTrangThai.Rows.Add("BAOTRI", "Đang bảo trì");

            // Gán và cấu hình ComboBox
            cboFilterTrangThai.DataSource = dtTrangThai;
            cboFilterTrangThai.DisplayMember = "Name";
            cboFilterTrangThai.ValueMember = "Code";
        }
        private void SetupPhongFilter()
        {
            // 1. Lấy dữ liệu từ DB (Hàm này đang hoạt động tốt ở các Form khác)
            DataTable dtPhong = Databases.PhongMayDB.GetLookupPhongMay();

            // 2. Đảm bảo cấu trúc cột (Đã có trong code gốc của bạn, rất tốt)
            if (!dtPhong.Columns.Contains("PHONG_ID")) dtPhong.Columns.Add("PHONG_ID", typeof(int));
            if (!dtPhong.Columns.Contains("TEN_PHONG")) dtPhong.Columns.Add("TEN_PHONG", typeof(string));

            // --- 3. Thêm dòng "Tất cả Phòng" ---
            DataRow allRow = dtPhong.NewRow();
            allRow["PHONG_ID"] = DBNull.Value;
            allRow["TEN_PHONG"] = "--- Tất cả Phòng ---";
            dtPhong.Rows.InsertAt(allRow, 0);

            // 4. Gán và cấu hình ComboBox
            cboFilterPhong.DataSource = dtPhong;
            cboFilterPhong.DisplayMember = "TEN_PHONG";
            cboFilterPhong.ValueMember = "PHONG_ID";

            // 🚨 QUAN TRỌNG: Cần đặt lại SelectedIndex sau khi gán DataSource
            cboFilterPhong.SelectedIndex = 0; // Đảm bảo chọn "Tất cả Phòng"
        }
    }
}
