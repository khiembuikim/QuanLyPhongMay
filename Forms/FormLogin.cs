using BTL_LTTQ_QLPM.Databases;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LTTQ_QLPM.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rdoHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHienMatKhau.Checked)
            {
                txtPass.PasswordChar = '\0'; // Hiển thị ký tự gốc
            }
            else
            {
                txtPass.PasswordChar = '*'; // Ẩn bằng dấu '*'
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng?", "Xác nhận Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit(); // Thoát toàn bộ ứng dụng
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim(); // Mật khẩu người dùng nhập

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = OracleHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    // KHÔNG CẦN CẬP NHẬT SQL NÀY NỮA, NÓ ĐÃ ĐÚNG:
                    string sql = @"
SELECT 
    T.NHANVIEN_ID,      -- Vị trí 0: ID cho ADMIN/NHANVIEN
    T.GIANGVIEN_ID,     -- Vị trí 1: ID cho GIANGVIEN
    T.PASSWORD_HASH,    -- Vị trí 2: HASH MẬT KHẨU
    R.ROLE_NAME,        -- Vị trí 3
    NVL(NV.HO_TEN, T.USERNAME) AS FULL_NAME -- Vị trí 4
FROM TAIKHOAN T 
JOIN ROLES R ON T.ROLE_ID = R.ROLE_ID
LEFT JOIN NHANVIEN NV ON T.NHANVIEN_ID = NV.NHANVIEN_ID
WHERE T.USERNAME = :u";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":u", user);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // 1. Lấy tất cả thông tin cần thiết và SỬA LỖI VỊ TRÍ CỘT:

                        // Lấy NHANVIEN_ID (Vị trí 0)
                        int nhanVienId = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);

                        // LẤY GIANGVIEN_ID (Vị trí 1) - CỘT THIẾT YẾU CẦN DÙNG CHO GIẢNG VIÊN
                        int giangVienId = reader.IsDBNull(1) ? -1 : reader.GetInt32(1);

                        // LẤY HASH MẬT KHẨU (Vị trí 2) - ĐÃ SỬA VỊ TRÍ CỘT
                        string storedHash = reader.GetString(2);

                        // LẤY ROLE VÀ TÊN (Vị trí 3, 4)
                        string roleName = reader.GetString(3);
                        string fullName = reader.GetString(4);

                        // 2. Hash mật khẩu người dùng nhập và so sánh
                        string enteredHash = HASHING_FUNCTION(pass);

                        if (storedHash == enteredHash)
                        {
                            // XÁC ĐỊNH ID SẼ DÙNG DỰA TRÊN VAI TRÒ
                            int currentUserId = -1;
                            if (roleName == "GIANGVIEN")
                            {
                                currentUserId = giangVienId; // SỬ DỤNG GIANGVIEN_ID
                            }
                            else // ADMIN, NHANVIEN, etc.
                            {
                                currentUserId = nhanVienId; // SỬ DỤNG NHANVIEN_ID
                            }

                            if (currentUserId <= 0)
                            {
                                // Hiển thị lỗi ID nếu ID là NULL/0 (Sau khi đăng nhập thành công)
                                MessageBox.Show("Lỗi: ID người dùng không hợp lệ sau khi xác thực.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Đăng nhập thành công và ID hợp lệ
                            this.Hide();
                            // ... (Tạo UserSessionInfo) ...
                            UserSessionInfo currentUser = new UserSessionInfo
                            {
                                // ... (Gán các thuộc tính khác)
                                Username = user,
                                FullName = fullName,
                                RoleName = roleName
                            };

                            // CHUYỂN FORM DÙNG currentUserId
                            if (roleName == "ADMIN")
                            {
                                BTL_LTTQ_QLPM.Forms.Admin.FormMainAdmin adminForm =
                                    new BTL_LTTQ_QLPM.Forms.Admin.FormMainAdmin(currentUserId, currentUser);
                                adminForm.Show();
                            }
                            else if (roleName == "GIANGVIEN")
                            {
                                GiangVien.FormMainGiangVien f = new GiangVien.FormMainGiangVien(currentUserId, currentUser);
                                f.Show();
                            }
                            else // Các vai trò khác (Ví dụ: Nhân viên)
                            {
                                BTL_LTTQ_QLPM.Forms.NhanVien.FormMainNhanVien nvForm =
                                    new BTL_LTTQ_QLPM.Forms.NhanVien.FormMainNhanVien(currentUserId, currentUser);
                                nvForm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sai mật khẩu!", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập không tồn tại!", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối hoặc xử lý dữ liệu: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string HASHING_FUNCTION(string pass)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 1. Chuyển chuỗi mật khẩu thành mảng byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));

                // 2. Chuyển mảng byte Hash thành chuỗi Hexadecimal (chuỗi Hash)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // Định dạng "x2" để đảm bảo mỗi byte luôn có 2 ký tự (hexa)
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void UpdateAllPlainTextPasswordsToHash()
        {
            string selectSql = "SELECT USERNAME, PASSWORD_HASH FROM TAIKHOAN";
            var updates = new List<Tuple<string, string>>();

            // --- PHẦN 1: Đọc và Băm Mật khẩu ---
            using (var conn = OracleHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand(selectSql, conn);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string username = reader.GetString(0);
                        string plainTextPass = reader.GetString(1);

                        // 1. Tính toán Hash của mật khẩu thuần
                        string newHash = HASHING_FUNCTION(plainTextPass);

                        // 2. Lưu lại để cập nhật
                        updates.Add(new Tuple<string, string>(username, newHash));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc tài khoản: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // --- PHẦN 2: Cập nhật Hash trở lại Database ---
            using (var conn = OracleHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string updateSql = "UPDATE TAIKHOAN SET PASSWORD_HASH = :h WHERE USERNAME = :u";

                    // Sử dụng Transaction để đảm bảo tất cả cập nhật đều thành công
                    OracleTransaction transaction = conn.BeginTransaction();
                    OracleCommand cmd = new OracleCommand(updateSql, conn);
                    cmd.Transaction = transaction;

                    foreach (var item in updates)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(":h", item.Item2); // New Hash
                        cmd.Parameters.Add(":u", item.Item1); // Username
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    MessageBox.Show($"Đã cập nhật thành công {updates.Count} tài khoản sang định dạng Hash!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật Hash: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    }

