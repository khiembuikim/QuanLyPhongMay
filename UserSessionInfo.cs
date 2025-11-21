using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LTTQ_QLPM
{
    public class UserSessionInfo
    {
        public static int NhanVienID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; } // Ví dụ: "ADMIN", "GIANGVIEN", "NHANVIEN"
    }
}
