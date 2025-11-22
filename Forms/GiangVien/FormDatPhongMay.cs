using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LTTQ_QLPM.Databases;

namespace BTL_LTTQ_QLPM.Forms.GiangVien
{
    public partial class FormDatPhongMay : Form
    {
        private readonly int _giangVienId;
        public FormDatPhongMay(int giangVienId)
        {
            InitializeComponent();
            _giangVienId = giangVienId;
            this.Text = "Đăng Ký Đặt Phòng Máy";
        }

        private void FormDatPhongMay_Load(object sender, EventArgs e)
        {
            LoadPhongMay();

            // Cài đặt hiển thị cho các DateTimePicker chỉ chọn giờ/ngày
            dtpGioBatDau.Format = DateTimePickerFormat.Custom;
            dtpGioBatDau.CustomFormat = "HH:mm";
            dtpGioBatDau.ShowUpDown = true;

            dtpGioKetThuc.Format = DateTimePickerFormat.Custom;
            dtpGioKetThuc.CustomFormat = "HH:mm";
            dtpGioKetThuc.ShowUpDown = true;

            dtpNgay.Format = DateTimePickerFormat.Long;
        }
        private void LoadPhongMay()
        {
            try
            {
                DataTable dt = LichDatDB.GetPhongMayList();
                cboPhong.DataSource = dt;
                cboPhong.DisplayMember = "TEN_PHONG";
                cboPhong.ValueMember = "PHONG_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi DB");
            }
        }

        private void btnGuiYeuCau_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu bắt buộc
            if (cboPhong.SelectedValue == null || string.IsNullOrWhiteSpace(txtNguoiDat.Text) || string.IsNullOrWhiteSpace(txtMonHoc.Text))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin Phòng, Người đặt và Môn học.", "Thiếu thông tin");
                return;
            }

            try
            {
                int phongId = Convert.ToInt32(cboPhong.SelectedValue);
                string nguoiDat = txtNguoiDat.Text;

                // Ghép Môn học và Mục đích (Giả định Mục đích chính là Môn học)
                string mucDich = $"Giảng dạy môn: {txtMonHoc.Text}. Số SV: {nudSoSV.Value}. Ghi chú: {txtGhiChu.Text}";

                // Lấy Ngày, Giờ Bắt đầu và Giờ Kết thúc
                DateTime ngay = dtpNgay.Value.Date; // Chỉ lấy phần ngày

                // Ghép Ngày (từ dtpNgay) và Giờ (từ dtpGioBatDau)
                DateTime gioBatDau = ngay.AddHours(dtpGioBatDau.Value.Hour)
                                        .AddMinutes(dtpGioBatDau.Value.Minute);

                DateTime gioKetThuc = ngay.AddHours(dtpGioKetThuc.Value.Hour)
                                        .AddMinutes(dtpGioKetThuc.Value.Minute);

                // 2. Gọi Stored Procedure
                string resultMessage = LichDatDB.CreateLichDat(
                    phongId,
                    _giangVienId,
                    nguoiDat,
                    mucDich,
                    gioBatDau,
                    gioKetThuc
                );

                // 3. Hiển thị kết quả
                if (resultMessage.Contains("thanh cong"))
                {
                    MessageBox.Show(resultMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form nếu đặt thành công
                }
                else
                {
                    MessageBox.Show(resultMessage, "Lỗi đặt lịch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

