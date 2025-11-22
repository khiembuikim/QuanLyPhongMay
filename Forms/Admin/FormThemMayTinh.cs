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
    public partial class FormThemMayTinh : Form
    {
        public FormThemMayTinh()
        {
            InitializeComponent();
            this.Load += FormThemMayTinh_Load;
        }

        private void FormThemMayTinh_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPhong = PhongMayDB.GetLookupPhongMay();

                cboPhongMay.DataSource = dtPhong;
                cboPhongMay.DisplayMember = "TEN_PHONG";
                cboPhongMay.ValueMember = "PHONG_ID";

                if (dtPhong.Rows.Count > 0)
                {
                    cboPhongMay.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Đóng Form nếu không thể tải dữ liệu phòng
                this.Close();
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu
            string maMay = txtMaMay.Text.Trim();
            string viTri = txtViTri.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            // 2. Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrEmpty(maMay))
            {
                MessageBox.Show("Mã máy không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra đã chọn Phòng Máy chưa
            if (cboPhongMay.SelectedValue == null || cboPhongMay.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Vui lòng chọn phòng máy cho thiết bị.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy ID phòng đã chọn (Giả định PHONG_ID là Decimal/Number trong DB)
            int phongId = Convert.ToInt32(cboPhongMay.SelectedValue);

            // 3. Gọi hàm thêm máy vào Database
            // Trạng thái mặc định là 'TOT' được thiết lập trong hàm DB
            string result = MayTinhDB.ThemMayTinh(maMay, phongId, viTri, ghiChu);

            if (result.Contains("thành công"))
            {
                MessageBox.Show(result, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Báo hiệu Form cha (FormQuanLyMayTinh) tải lại danh sách
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Hiển thị lỗi từ Database (Ví dụ: Mã máy bị trùng)
                MessageBox.Show(result, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

