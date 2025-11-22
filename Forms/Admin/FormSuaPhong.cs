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
    public partial class FormSuaPhong : Form
    {
        private int _phongMayId;
        public FormSuaPhong(int phongId)
        {
            InitializeComponent();
            _phongMayId = phongId;
            this.Load += FormSuaPhong_Load;
        }

        private void FormSuaPhong_Load(object sender, EventArgs e)
        {
            DataRow row = PhongMayDB.GetPhongMayById(_phongMayId);

            if (row != null)
            {
                // Điền dữ liệu vào các controls
                txtMaPhong.Text = row["MA_PHONG"].ToString();
                txtTenPhong.Text = row["TEN_PHONG"].ToString();
                // Cần kiểm tra NULL nếu SO_MAY cho phép NULL, nhưng ta giả định là NOT NULL
                txtSoMay.Text = row["SO_MAY"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin phòng máy này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu
            string maPhong = txtMaPhong.Text.Trim();
            string tenPhong = txtTenPhong.Text.Trim();

            // 2. Kiểm tra dữ liệu bắt buộc và Sức chứa
            if (string.IsNullOrEmpty(maPhong) || string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Mã phòng và Tên phòng không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soMay;
            if (!int.TryParse(txtSoMay.Text.Trim(), out soMay) || soMay <= 0)
            {
                MessageBox.Show("Số máy phải là một số nguyên dương hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoMay.Focus();
                return;
            }

            // 3. Gọi hàm sửa phòng vào Database
            string result = PhongMayDB.SuaPhong(_phongMayId, maPhong, tenPhong, soMay);

            if (result.Contains("thành công"))
            {
                MessageBox.Show(result, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Báo Form cha tải lại DataGridView
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
