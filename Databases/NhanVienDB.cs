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
                OracleHelper.ExecuteNonQuery(sql, parameters);

                // 4. Lấy giá trị trả về từ tham số pResult
                if (pResult.Value != null && pResult.Value != DBNull.Value)
                {
                    if (pResult.Value is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                    {
                        // ⭐ SỬA: Sử dụng thuộc tính .Value để lấy System.Decimal
                        // Thay vì .ToDecimal() không tồn tại.

                        // ⭐ Xử lý Overflow xảy ra ở đây
                        try
                        {
                            return oracleDecimal.Value; // Trả về System.Decimal
                        }
                        catch (OverflowException ex)
                        {
                            // Chú ý: Lỗi tràn số (Overflow) này xảy ra khi OracleDecimal.Value 
                            // cố gắng chuyển đổi giá trị quá lớn của Oracle NUMBER sang System.Decimal
                            throw new Exception($"Giá trị lương quá lớn ({oracleDecimal.ToString()} VNĐ) gây tràn số trong C#. Chi tiết: {ex.Message}");
                        }
                    }
                    // Trường hợp dự phòng nếu kiểu dữ liệu trả về khác OracleDecimal
                    return Convert.ToDecimal(pResult.Value);
                }
                return 0m; // Giá trị mặc định nếu không có dữ liệu trả về
            }
            catch (Exception ex)
            {
                // Compiler vẫn cần một lệnh return ở đây, nhưng vì ta đang ném ngoại lệ
                // nên ta giữ nguyên throw.
                throw new Exception("Lỗi khi gọi FN_TINH_LUONG_THANG: " + ex.Message);
            }
        }
    }
}
