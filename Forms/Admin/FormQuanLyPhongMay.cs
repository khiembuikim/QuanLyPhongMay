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
    public partial class FormQuanLyPhongMay : Form
    {
        public FormQuanLyPhongMay()
        {
            
            InitializeComponent();
            this.Load += FormQuanLyPhongMay_Load;
        }

        private void FormQuanLyPhongMay_Load(object sender, EventArgs e)
        {
            LoadDanhSachPhongMay();
        }
        private void LoadDanhSachPhongMay()
        {
            try
            {
                DataTable dt = PhongMayDB.GetAllPhongMay();
                dgvDanhSachPhong.DataSource = dt;

                // --- Định dạng lại tên cột ---

                // Ẩn cột ID nhưng vẫn giữ để dùng cho các thao tác Sửa/Xóa
                dgvDanhSachPhong.Columns["PHONG_ID"].Visible = false;

                dgvDanhSachPhong.Columns["MA_PHONG"].HeaderText = "Mã Phòng";
                dgvDanhSachPhong.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
                dgvDanhSachPhong.Columns["SO_MAY"].HeaderText = "Số Máy"; // Tên cột mới

                // Chuyển đổi trạng thái từ mã (RANH/BAN/BAO_TRI) sang tiếng Việt (Tùy chọn)
                dgvDanhSachPhong.Columns["TRANG_THAI"].HeaderText = "Trạng Thái";

                // 💡 Tùy chọn: Thay thế giá trị mã trạng thái bằng tên tiếng Việt
                foreach (DataRow row in dt.Rows)
                {
                    string trangThaiCode = row["TRANG_THAI"].ToString().ToUpper();
                    if (trangThaiCode == "RANH")
                        row["TRANG_THAI"] = "Rảnh";
                    else if (trangThaiCode == "BAN")
                        row["TRANG_THAI"] = "Bận";
                    else if (trangThaiCode == "BAO_TRI")
                        row["TRANG_THAI"] = "Bảo trì";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phòng máy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormThemPhong formThem = new FormThemPhong();
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachPhongMay(); // Tải lại danh sách sau khi thêm
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhong.SelectedRows.Count > 0)
            {
                // Lấy PHONG_ID từ dòng được chọn
                // Lưu ý: Cột này phải có tên là PHONG_ID (như trong SQL của bạn)
                int phongMayId = Convert.ToInt32(dgvDanhSachPhong.SelectedRows[0].Cells["PHONG_ID"].Value);

                // Mở Form sửa và truyền ID phòng
                FormSuaPhong formSua = new FormSuaPhong(phongMayId);
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachPhongMay(); // Tải lại danh sách sau khi sửa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhong.SelectedRows.Count > 0 &&
            MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int phongMayId = Convert.ToInt32(dgvDanhSachPhong.SelectedRows[0].Cells["PHONG_ID"].Value);
                    string result = PhongMayDB.XoaPhong(phongMayId); // Bạn cần viết hàm XoaPhong này

                    if (result.Contains("thành công"))
                    {
                        MessageBox.Show(result, "Thành công");
                        LoadDanhSachPhongMay();
                    }
                    else
                    {
                        MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhatTT_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhong.SelectedRows.Count > 0)
            {
                int phongMayId = Convert.ToInt32(dgvDanhSachPhong.SelectedRows[0].Cells["PHONG_ID"].Value);
                string trangThaiHienTai = dgvDanhSachPhong.SelectedRows[0].Cells["TRANG_THAI"].Value.ToString();

                // Logic đơn giản: Đảo trạng thái giữa 'Hoạt động' và 'Bảo trì'
                string trangThaiMoi = (trangThaiHienTai == "Hoạt động") ? "Bảo trì" : "Hoạt động";

                string result = PhongMayDB.CapNhatTrangThai(phongMayId, trangThaiMoi);

                if (result.Contains("thành công"))
                {
                    MessageBox.Show(result, "Thành công");
                    LoadDanhSachPhongMay();
                }
                else
                {
                    MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        }
    }
    

