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
                    // Cập nhật SQL: THÊM T.NHANVIEN_ID vào danh sách SELECT
                    string sql = @"
                SELECT 
                    T.NHANVIEN_ID,        -- 0: Cột mới THÊM
                    T.PASSWORD_HASH,      -- 1: Mật khẩu đã hash
                    R.ROLE_NAME,          -- 2: Tên vai trò
                    NVL(NV.HO_TEN, T.USERNAME) AS FULL_NAME -- 3: Họ tên
                FROM TAIKHOAN T 
                JOIN ROLES R ON T.ROLE_ID = R.ROLE_ID
                LEFT JOIN NHANVIEN NV ON T.NHANVIEN_ID = NV.NHANVIEN_ID
                WHERE T.USERNAME = :u";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":u", user);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // 1. Lấy tất cả thông tin cần thiết
                        int nhanVienId = reader.IsDBNull(0) ? -1 : reader.GetInt32(0); // Lấy NHANVIEN_ID
                        string storedHash = reader.GetString(1);
                        string roleName = reader.GetString(2);
                        string fullName = reader.GetString(3);

                        // 2. Hash mật khẩu người dùng nhập và so sánh
                        string enteredHash = HASHING_FUNCTION(pass);

                        if (storedHash == enteredHash)
                        {
                            // Đăng nhập thành công!

                            // TODO: Tạo/Cập nhật class UserSessionInfo (Nếu chưa có, cần tạo)
                            // Hiện tại, ta sẽ bỏ qua UserSessionInfo và chỉ tập trung vào việc chuyển form.

                            this.Hide();
                            UserSessionInfo currentUser = new UserSessionInfo
                            {
                                // Bạn cần thêm thuộc tính NhanVienID vào class UserSessionInfo
                                // NhanVienID = nhanVienId, 
                                Username = user,
                                FullName = fullName,
                                RoleName = roleName
                            };

                            if (roleName == "ADMIN")
                            {
                                // *** ĐIỀU CHỈNH QUAN TRỌNG: TRUYỀN ID vào Constructor FormMainAdmin ***
                                BTL_LTTQ_QLPM.Forms.Admin.FormMainAdmin adminForm =
            new BTL_LTTQ_QLPM.Forms.Admin.FormMainAdmin(nhanVienId, currentUser);

                                // KHÔNG CẦN gọi adminForm.UpdateUserInfo(currentUser) nữa,
                                // vì nó đã được gọi trong Constructor của FormMainAdmin.

                                adminForm.Show();
              
                            }
                            else if (roleName == "GIANGVIEN")
                            {
                                // Cần tạo Constructor tương tự cho FormMainGiangVien nếu có logic sử dụng ID
                                new GiangVien.FormMainGiangVien().Show();
                            }
                            else // Các vai trò khác (Ví dụ: Nhân viên)
                            {
                                new NhanVien.FormMainNhanVien().Show();
                            }

                            // Form Login sẽ đóng khi các Form Main mở ra.
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

