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
    public partial class FormGiaoNhiemVu : Form
    {
        private readonly int _adminNhanVienId;
        public FormGiaoNhiemVu(int adminNhanVienId)
        {
            InitializeComponent();
            this._adminNhanVienId = adminNhanVienId;
            LoadInitialData();
        }
        private void LoadInitialData()
        {
            // 1. Tải danh sách nhân viên
            DataTable dtNhanVien = NhiemVuDB.GetNhanVienForAssignment();
            cboNhanVien.DataSource = dtNhanVien;
            cboNhanVien.DisplayMember = "HO_TEN";
            cboNhanVien.ValueMember = "NHANVIEN_ID";
            cboNhanVien.SelectedIndex = -1;

            // 2. Tải danh sách hệ số
           

            // 3. Deadline tối thiểu
            dtpDeadline.MinDate = DateTime.Today.AddDays(1);
        }
        private void FormGiaoNhiemVu_Load(object sender, EventArgs e)
        {

        }

        private void btnGiaoNhiemVu_Click(object sender, EventArgs e)
        {
            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            string tenTask = txtTenNhiemVu.Text.Trim();
            string moTa = rtxtMoTa.Text;
            DateTime deadline = dtpDeadline.Value;

            // Lấy giá trị Hệ số (decimal)
            decimal heSoGoc;

            // 1. Kiểm tra liệu người dùng đã nhập một số hợp lệ hay chưa
            if (!decimal.TryParse(txtHeSoNhiemVu.Text.Trim(), out heSoGoc) || heSoGoc <= 0)
            {
                MessageBox.Show("Vui lòng nhập Hệ số (Điểm Gốc) nhiệm vụ hợp lệ (> 0).", "Lỗi Nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHeSoNhiemVu.Focus();
                return;
            }

            // 2. Tiếp tục logic Giao Nhiệm Vụ (giữ nguyên)
            string message = NhiemVuDB.GiaoNhiemVu(
                nhanVienId,
                _adminNhanVienId,
                tenTask,
                moTa,
                heSoGoc, // Sử dụng heSoGoc từ TextBox
                deadline
            );

            MessageBox.Show(message, "Thông báo");

            // Nếu thành công, reset Form
            if (message.Contains("thành công"))
            {
                txtTenNhiemVu.Clear();
                rtxtMoTa.Clear();
                txtHeSoNhiemVu.Clear(); // Clear cả textbox Hệ số
                cboNhanVien.SelectedIndex = -1; // Tùy chọn: Reset luôn nhân viên
            }
        }
    }
}
