using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace BTL_LTTQ_QLPM.Utils
{
    public static class Utils
    {
        // Hàm Hashing Password (SHA256)
        public static string HashPassword(string pass)
        {
            // Kiểm tra nếu chuỗi đầu vào rỗng/null, trả về chuỗi rỗng
            if (string.IsNullOrEmpty(pass))
            {
                return string.Empty;
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 1. Chuyển chuỗi mật khẩu thành mảng byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));

                // 2. Chuyển mảng byte Hash thành chuỗi Hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // Định dạng "x2" (hexa 2 ký tự)
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Hàm Kiểm tra mật khẩu (optional, nhưng cần thiết cho đăng nhập)
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Băm mật khẩu người dùng nhập vào
            string hashedEnteredPassword = HashPassword(enteredPassword);

            // So sánh Hash
            return hashedEnteredPassword.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
