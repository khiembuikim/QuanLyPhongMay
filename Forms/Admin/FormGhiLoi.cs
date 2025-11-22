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
    public partial class FormGhiLoi : Form
    {
        private int _mayId;
        public FormGhiLoi(int mayId)
        {
            InitializeComponent();
            _mayId = mayId;
            this.Load += FormGhiLoi_Load;
        }

        private void FormGhiLoi_Load(object sender, EventArgs e)
        {
            // Lấy thông tin máy tính cũ (Dùng hàm MayTinhDB.GetMayTinhById đã tạo ở bước Sửa Vị Trí)
            DataRow row = MayTinhDB.GetMayTinhById(_mayId);

            if (row != null)
            {
                txtMaMay.Text = row["MA_MAY"].ToString();
                txtMaMay.ReadOnly = true;

                // Nếu đã có lỗi cũ, hiển thị để người dùng dễ chỉnh sửa/ghi đè
                string ghiChuCu = row["GHI_CHU"].ToString();
                if (!string.IsNullOrEmpty(ghiChuCu))
                {
                    txtChiTietLoi.Text = ghiChuCu;
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin máy tính này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string chiTietLoi = txtChiTietLoi.Text.Trim();

            if (string.IsNullOrEmpty(chiTietLoi))
            {
                MessageBox.Show("Vui lòng nhập chi tiết lỗi để ghi nhận.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm ghi lỗi
            string result = MayTinhDB.GhiLoi(_mayId, chiTietLoi);

            if (result.Contains("thành công"))
            {
                MessageBox.Show(result, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Báo Form cha tải lại DataGridView
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Lỗi cập nhật Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
