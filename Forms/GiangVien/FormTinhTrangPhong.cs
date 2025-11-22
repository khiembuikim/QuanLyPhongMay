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

namespace BTL_LTTQ_QLPM.Forms.GiangVien
{
    public partial class FormTinhTrangPhong : Form
    {
        public FormTinhTrangPhong()
        {
            InitializeComponent();
            this.Text = "Tình Trạng Chung Các Phòng Máy";

            // Đăng ký sự kiện CellFormatting
            // Giả định tên DataGridView là dgvTinhTrangPhong
            if (this.Controls.Find("dgvTinhTrangPhong", true).Length > 0)
            {
                DataGridView dgv = (DataGridView)this.Controls.Find("dgvTinhTrangPhong", true)[0];
                dgv.CellFormatting += dgvTinhTrangPhong_CellFormatting;
            }
        }

        private void FormTinhTrangPhong_Load(object sender, EventArgs e)
        {
            LoadTinhTrang();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTinhTrang();
        }
        private void LoadTinhTrang()
        {
            try
            {
                // 1. Lấy dữ liệu tổng hợp từ DB
                DataTable dt = PhongMayDB.GetTinhTrangPhongMay();

                // Giả định tên DataGridView là dgvTinhTrangPhong
                dgvTinhTrangPhong.DataSource = dt;

                // 2. Định dạng hiển thị
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin tình trạng phòng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatDataGridView()
        {
            // Thiết lập tên hiển thị cho các cột
            dgvTinhTrangPhong.Columns["MA_PHONG"].HeaderText = "Mã Phòng";
            dgvTinhTrangPhong.Columns["TEN_PHONG"].HeaderText = "Tên Phòng";
            dgvTinhTrangPhong.Columns["TONG_SO_MAY"].HeaderText = "Tổng Số Máy";
            dgvTinhTrangPhong.Columns["TRANG_THAI_HIENTAI"].HeaderText = "Trạng Thái";
            dgvTinhTrangPhong.Columns["SO_MAY_TOT"].HeaderText = "Máy Tốt";
            dgvTinhTrangPhong.Columns["SO_MAY_LOI"].HeaderText = "Máy Lỗi";

            // Ẩn cột ID
            dgvTinhTrangPhong.Columns["PHONG_ID"].Visible = false;

            dgvTinhTrangPhong.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dgvTinhTrangPhong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTinhTrangPhong.Columns[e.ColumnIndex].Name == "TRANG_THAI_HIENTAI")
            {
                string status = e.Value?.ToString();
                if (status == "Rảnh")
                {
                    e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
                else if (status == "Bận")
                {
                    e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
                }
                else if (status == "Bảo trì")
                {
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }
    }
}
