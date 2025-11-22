using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LTTQ_QLPM.Models
{
    // File: Models/PhongMay.cs
    public class PhongMay
    {
        public int PhongMayId { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public int SucChua { get; set; }
        public string ViTri { get; set; }
        public string TrangThai { get; set; } // Ví dụ: 'Hoạt động', 'Bảo trì', 'Đã hủy'
    }
}
