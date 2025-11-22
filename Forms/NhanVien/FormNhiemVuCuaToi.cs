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
    public partial class FormNhiemVuCuaToi : Form
    {
        private readonly int _nhanVienId;
        private int _currentTaskId = -1; // Biến lưu TASK_ID đang chọn
        public FormNhiemVuCuaToi(int nhanVienId)
        {
            InitializeComponent();
            _nhanVienId = nhanVienId;
            dgvNhiemVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhiemVu.MultiSelect = false;
            dgvNhiemVu.CellClick += dgvNhiemVu_CellClick; // Đảm bảo gán sự kiện click
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormNhiemVuCuaToi_Load(object sender, EventArgs e)
        {
            LoadTaskList();
        }
        private void LoadTaskList()
        {
            try
            {
                DataTable dt = TaskDB.GetTasksByNhanVienId(_nhanVienId);
                dgvNhiemVu.DataSource = dt;

                // Cấu hình hiển thị cột theo yêu cầu
                dgvNhiemVu.Columns["ASSIGN_ID"].Visible = false;
                dgvNhiemVu.Columns["TASK_ID"].Visible = false; // Ẩn cột ID

                // Bảng theo yêu cầu: | Mã NV (không có) | Tên nhiệm vụ | Hệ số | Deadline | Trạng thái |
                dgvNhiemVu.Columns["TEN_NHIEM_VU"].HeaderText = "Tên nhiệm vụ";
                dgvNhiemVu.Columns["HE_SO_GIAO"].HeaderText = "Hệ số";
                dgvNhiemVu.Columns["DEADLINE"].HeaderText = "Hạn chót";
                dgvNhiemVu.Columns["TIEN_DO"].HeaderText = "Tiến độ (%)";
                dgvNhiemVu.Columns["TRANG_THAI"].HeaderText = "Trạng thái";
                dgvNhiemVu.Columns["GHI_CHU"].Visible = false; // Có thể ẩn cột ghi chú

                // Xóa thông tin chi tiết đang hiển thị
                ClearDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhiệm vụ: " + ex.Message, "Lỗi DB");
            }
        }
        private void ClearDetails()
        {
            _currentTaskId = -1;
            lblNhiemVuDangChon.Text = "--- Vui lòng chọn nhiệm vụ ---";
            txtTienDo.Text = "0";
            txtGhiChuBaoCao.Text = string.Empty;
            btnCapNhatTienDo.Enabled = false;
        }

        private void dgvNhiemVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhiemVu.Rows[e.RowIndex];

                // 1. Lấy TASK_ID để cập nhật
                _currentTaskId = Convert.ToInt32(row.Cells["TASK_ID"].Value);

                string tenNhiemVu = row.Cells["TEN_NHIEM_VU"].Value.ToString();
                string trangThai = row.Cells["TRANG_THAI"].Value.ToString();

                // 2. Điền thông tin vào khu vực cập nhật
                lblNhiemVuDangChon.Text = "Đang chọn: " + tenNhiemVu;
                txtTienDo.Text = row.Cells["TIEN_DO"].Value.ToString();
                txtGhiChuBaoCao.Text = row.Cells["GHI_CHU"].Value.ToString();

                // 3. Vô hiệu hóa cập nhật nếu đã Hoàn thành
                if (trangThai == "HOAN_THANH")
                {
                    MessageBox.Show("Nhiệm vụ này đã HOÀN THÀNH (100%), không thể cập nhật thêm.", "Thông báo");
                    btnCapNhatTienDo.Enabled = false;
                    txtTienDo.ReadOnly = true;
                    txtGhiChuBaoCao.ReadOnly = true;
                }
                else
                {
                    btnCapNhatTienDo.Enabled = true;
                    txtTienDo.ReadOnly = false;
                    txtGhiChuBaoCao.ReadOnly = false;
                }
            }
        }

        private void btnCapNhatTienDo_Click(object sender, EventArgs e)
        {
            if (_currentTaskId <= 0 || !btnCapNhatTienDo.Enabled)
            {
                MessageBox.Show("Vui lòng chọn một nhiệm vụ đang thực hiện để cập nhật.", "Lỗi");
                return;
            }

            if (!decimal.TryParse(txtTienDo.Text, out decimal tienDo) || tienDo < 0 || tienDo > 100)
            {
                MessageBox.Show("Tiến độ phải là số nguyên hoặc thập phân từ 0 đến 100.", "Lỗi nhập liệu");
                return;
            }

            string ghiChu = txtGhiChuBaoCao.Text;

            // Yêu cầu: Xác nhận khi tiến độ = 100
            if (tienDo == 100)
            {
                DialogResult confirm = MessageBox.Show("Bạn đang cập nhật tiến độ 100%. Nhiệm vụ sẽ được đánh dấu là HOÀN THÀNH. Bạn có chắc chắn?",
                                                     "Xác nhận Hoàn thành",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                int rowsAffected = TaskDB.UpdateTaskProgress(_currentTaskId, tienDo, ghiChu);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật tiến độ thành công!");
                    LoadTaskList(); // Tải lại danh sách để thấy trạng thái mới
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi DB khi cập nhật tiến độ: " + ex.Message, "Lỗi Database");
            }
        }
    }
}
