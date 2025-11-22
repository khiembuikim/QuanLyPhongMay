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
    public partial class FormSuaViTri : Form
    {
        private int _mayId;
        public FormSuaViTri(int mayId)
        {
            InitializeComponent();
            _mayId = mayId;
            this.Load += FormSuaViTri_Load;
        }

        private void FormSuaViTri_Load(object sender, EventArgs e)
        {
            try
            {
                // Giả định bạn có hàm GetLookupPhongMay() trong PhongMayDB
                DataTable dtPhong = Databases.PhongMayDB.GetLookupPhongMay();

                cboPhongMayMoi1.DataSource = dtPhong;
                cboPhongMayMoi1.DisplayMember = "TEN_PHONG";
                cboPhongMayMoi1.ValueMember = "PHONG_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Không đóng Form, nhưng cần đảm bảo phòng máy được chọn sau đó
            }

            // 2. Tải thông tin máy tính cũ
            DataRow row = Databases.MayTinhDB.GetMayTinhById(_mayId);

            if (row != null)
            {
                txtMaMay.Text = row["MA_MAY"].ToString();
                txtMaMay.ReadOnly = true;
                txtViTriMoi.Text = row["VI_TRI"].ToString();

                // 🌟 THAY ĐỔI: Đặt giá trị phòng hiện tại vào ComboBox
                object currentPhongId = row["PHONG_ID"];
                if (currentPhongId != DBNull.Value)
                {
                    // Ép kiểu SelectedValue thành int hoặc object tùy thuộc vào thuộc tính ValueMember
                    cboPhongMayMoi1.SelectedValue = Convert.ToInt32(currentPhongId);
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
            string newViTri = txtViTriMoi.Text.Trim();

            // 1. Kiểm tra và lấy ID phòng mới
            if (cboPhongMayMoi1.SelectedValue == null || cboPhongMayMoi1.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Vui lòng chọn phòng máy mới cho thiết bị.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int newPhongId = Convert.ToInt32(cboPhongMayMoi1.SelectedValue);

            // 2. Kiểm tra Vị trí trống (giữ nguyên logic cũ)
            if (string.IsNullOrEmpty(newViTri))
            {
                if (MessageBox.Show("Vị trí để trống, bạn có muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            // 3. Gọi hàm cập nhật vị trí VÀ phòng
            // 🌟 THAY ĐỔI: Thêm newPhongId vào lời gọi hàm SuaViTri
            string result = Databases.MayTinhDB.SuaViTri(_mayId, newViTri, newPhongId);

            if (result.Contains("thành công"))
            {
                MessageBox.Show(result, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Báo Form cha tải lại DataGridView
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
