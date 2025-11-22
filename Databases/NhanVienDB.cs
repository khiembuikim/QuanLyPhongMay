using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LTTQ_QLPM.Databases
{
    public class NhanVienDB
    {
        public static object GetNgayBatDauLamViec(int nhanVienId)
        {
            // SỬA: Thay NGAY_BAT_DAU bằng CREATED_AT (Tên cột thực tế trong bảng NHANVIEN)
            string sql = @"
    SELECT CREATED_AT 
    FROM NHANVIEN
    WHERE NHANVIEN_ID = :nhanVienId";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("nhanVienId", OracleDbType.Decimal) { Value = nhanVienId }
            };

            // Hàm này trả về giá trị kiểu object (ngày)
            return OracleHelper.ExecuteScalar(sql, parameters);
        }
        
        public static decimal TinhLuongThang(int nhanVienId, int month, int year)
        {
            // Câu lệnh gọi hàm/function trong Oracle và lấy giá trị trả về
            string sql = "BEGIN :result := FN_TINH_LUONG_THANG(:p_nvId, :p_month, :p_year); END;";

            // 1. Khai báo tham số đầu vào
            OracleParameter pNvId = new OracleParameter("p_nvId", OracleDbType.Decimal) { Value = nhanVienId };
            OracleParameter pMonth = new OracleParameter("p_month", OracleDbType.Decimal) { Value = month };
            OracleParameter pYear = new OracleParameter("p_year", OracleDbType.Decimal) { Value = year };

            // 2. Khai báo tham số trả về (RETURN parameter)
            OracleParameter pResult = new OracleParameter("result", OracleDbType.Decimal)
            {
                Direction = ParameterDirection.ReturnValue // Quan trọng: Đặt hướng là giá trị trả về
            };

            // 3. Gói TẤT CẢ tham số vào một MẢNG (array)
            OracleParameter[] parameters = new OracleParameter[] { pResult, pNvId, pMonth, pYear };

            try
            {
                // Thực thi stored function/procedure. 
                // Giả định OracleHelper.ExecuteNonQuery có thể xử lý tham số RETURN.
                OracleHelper.ExecuteNonQuery(sql, parameters);

                // 4. Lấy giá trị trả về từ tham số pResult
                if (pResult.Value != null && pResult.Value != DBNull.Value)
                {
                    return Convert.ToDecimal(pResult.Value);
                }
                return 0m;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi trong DB Helper, ném lại ngoại lệ để Form có thể bắt
                throw new Exception("Lỗi khi gọi FN_TINH_LUONG_THANG: " + ex.Message);
            }
        }
    }
}
