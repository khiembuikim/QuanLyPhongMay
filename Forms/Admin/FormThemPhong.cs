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
    public partial class FormThemPhong : Form
    {
        public FormThemPhong()
        {
            InitializeComponent();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu
            string maPhong = txtMaPhong.Text.Trim();
            string tenPhong = txtTenPhong.Text.Trim();

            // 2. Kiểm tra dữ liệu bắt buộc
            if (string.IsNullOrEmpty(maPhong) || string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Mã phòng và Tên phòng không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra SO_MAY (Phải là số nguyên)
            int soMay;
            // Đảm bảo bạn đang dùng đúng tên TextBox, ví dụ: txtSoMay
            if (!int.TryParse(txtSoMay.Text.Trim(), out soMay) || soMay <= 0)
            {
                MessageBox.Show("Số máy phải là một số nguyên dương hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoMay.Focus();
                return;
            }

            // 4. Gọi hàm thêm phòng vào Database với tham số soMay
            string result = PhongMayDB.ThemPhong(maPhong, tenPhong, soMay);

            if (result.Contains("thành công"))
            {
                MessageBox.Show(result, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
