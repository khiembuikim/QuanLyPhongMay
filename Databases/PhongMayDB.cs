using BTL_LTTQ_QLPM.Forms.Admin;
using BTL_LTTQ_QLPM.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BTL_LTTQ_QLPM.Databases
{
    public class PhongMayDB
    {
        public static DataTable GetAllPhongMay()
        {
            // Cập nhật câu truy vấn SQL để lấy đúng tên cột mới
            string sql = @"
        SELECT 
            PHONG_ID,       -- Lấy ID phòng (khóa chính)
            MA_PHONG,       -- Mã phòng
            TEN_PHONG,      -- Tên phòng
            SO_MAY,         -- Số lượng máy (thay thế SUC_CHUA)
            TRANG_THAI      -- Trạng thái hiện tại ('RANH', 'BAN', 'BAO_TRI')
        FROM PHONGMAY
        ORDER BY PHONG_ID";

            // Sử dụng hàm helper của bạn để thực thi truy vấn
            // Ví dụ: return OracleHelper.ExecuteQuery(sql, null);
            // Hoặc logic kết nối trực tiếp như sau:
            using (OracleConnection conn = OracleHelper.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static string CapNhatTrangThai(int phongMayId, string trangThaiMoi)
        {
            string sql = "UPDATE PHONGMAY SET TRANG_THAI = :trangThai WHERE PHONG_ID = :id";

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
                new OracleParameter("trangThai", OracleDbType.NVarchar2) { Value = trangThaiMoi },
                new OracleParameter("id", OracleDbType.Decimal) { Value = phongMayId }
                };

                // Sử dụng hàm helper của bạn để thực thi NonQuery
                // Ví dụ: int rows = OracleHelper.ExecuteNonQuery(sql, parameters);
                using (OracleConnection conn = OracleHelper.GetConnection())
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        // Thêm Parameters
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0 ? "Cập nhật trạng thái thành công." : "Không tìm thấy phòng để cập nhật.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi Database: " + ex.Message;
            }
        }
        public static string ThemPhong(string maPhong, string tenPhong, int soMay)
        {
            // Lệnh SQL đã loại bỏ VI_TRI và đổi SUC_CHUA thành SO_MAY
            string sql = @"
        INSERT INTO PHONGMAY (MA_PHONG, TEN_PHONG, SO_MAY, TRANG_THAI) 
        VALUES (:maPhong, :tenPhong, :soMay, 'RANH')"; // TRANG_THAI mặc định là 'RANH' (Rảnh)

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
            new OracleParameter("maPhong", OracleDbType.Varchar2) { Value = maPhong },
            new OracleParameter("tenPhong", OracleDbType.NVarchar2) { Value = tenPhong },
            // Thay đổi từ sucChua sang soMay
            new OracleParameter("soMay", OracleDbType.Int32) { Value = soMay }
                };

                // Giả định bạn gọi OracleHelper.ExecuteNonQuery hoặc logic tương đương
                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                if (rowsAffected > 0)
                {
                    return $"Thêm phòng '{tenPhong}' thành công.";
                }
                else
                {
                    return "Lỗi: Không thể thêm phòng máy.";
                }
            }
            catch (OracleException ex) when (ex.Number == 1) // ORA-00001: unique constraint violated (trùng MA_PHONG)
            {
                return $"Lỗi: Mã phòng '{maPhong}' đã tồn tại.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database: " + ex.Message;
            }
        }
        public static string SuaPhong(int phongMayId, string maPhong, string tenPhong, int soMay)
        {
            // Lệnh SQL đã loại bỏ VI_TRI và đổi SUC_CHUA thành SO_MAY
            string sql = @"
        UPDATE PHONGMAY SET 
            MA_PHONG = :maPhong, 
            TEN_PHONG = :tenPhong, 
            SO_MAY = :soMay
        WHERE PHONG_ID = :id"; // Dùng PHONG_ID làm khóa chính

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
            new OracleParameter("maPhong", OracleDbType.Varchar2) { Value = maPhong },
            new OracleParameter("tenPhong", OracleDbType.NVarchar2) { Value = tenPhong },
            new OracleParameter("soMay", OracleDbType.Int32) { Value = soMay }, // Thay đổi
            new OracleParameter("id", OracleDbType.Decimal) { Value = phongMayId }
                };

                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                if (rowsAffected > 0)
                {
                    return $"Cập nhật thông tin phòng '{tenPhong}' thành công.";
                }
                else
                {
                    return "Lỗi: Không tìm thấy phòng để cập nhật.";
                }
            }
            catch (OracleException ex) when (ex.Number == 1) // ORA-00001: unique constraint violated (trùng MA_PHONG)
            {
                return $"Lỗi: Mã phòng '{maPhong}' đã tồn tại.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database: " + ex.Message;
            }
        }
        public static string XoaPhong(int phongMayId)
        {
            // Dùng DELETE để xóa vật lý. Cần đảm bảo không vi phạm khóa ngoại.
            string sqlDelete = "DELETE FROM PHONGMAY WHERE PHONG_ID = :id";

            // Nếu phòng có dữ liệu liên quan, bạn phải xóa/cập nhật dữ liệu liên quan trước.
            // Ví dụ: string sqlDeleteLichSu = "DELETE FROM LICH_SU_SD WHERE PHONGMAY_ID = :id";

            using (OracleConnection conn = OracleHelper.GetConnection())
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Bước 1: (Tùy chọn) Xóa các bản ghi liên quan (nếu cần)
                    // (Thêm code xóa LICH_SU_SD nếu cần)

                    // Bước 2: Xóa phòng máy
                    OracleParameter[] parameters = new OracleParameter[]
                    {
                new OracleParameter("id", OracleDbType.Decimal) { Value = phongMayId }
                    };

                    // Sử dụng logic thực thi NonQuery với Transaction
                    OracleCommand cmd = new OracleCommand(sqlDelete, conn);
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddRange(parameters);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        return "Xóa phòng máy thành công.";
                    }
                    else
                    {
                        transaction.Rollback();
                        return "Lỗi: Không tìm thấy phòng để xóa.";
                    }
                }
                catch (OracleException ex) when (ex.Number == 2292) // ORA-02292: child record found
                {
                    transaction.Rollback();
                    return "Lỗi: Phòng này đã có lịch sử sử dụng. Không thể xóa vật lý.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Lỗi Database: " + ex.Message;
                }
            }
        }


        // Trong PhongMayDB.cs

        public static DataRow GetPhongMayById(int phongId)
        {
            string sql = @"
        SELECT 
            PHONG_ID, MA_PHONG, TEN_PHONG, SO_MAY, TRANG_THAI 
        FROM PHONGMAY
        WHERE PHONG_ID = :id";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("id", OracleDbType.Decimal) { Value = phongId }
            };

            // Giả định ExecuteQuery trả về DataTable
            DataTable dt = OracleHelper.ExecuteQuery(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]; // Trả về dòng dữ liệu đầu tiên
            }
            return null;
        }
        public static DataTable GetLookupPhongMay()
        {
            // Cần trả về PHONG_ID và TEN_PHONG
            string sql = "SELECT PHONG_ID, TEN_PHONG FROM PHONGMAY ORDER BY TEN_PHONG";
            return OracleHelper.ExecuteQuery(sql, null);
        }
        public static DataTable GetTinhTrangPhongMay()
        {
            // Lấy thời gian hiện tại của C# để truyền vào truy vấn SQL
            DateTime currentTime = DateTime.Now;

            // Sử dụng tham số (Parameter) là cách an toàn nhất thay vì nối chuỗi
            // Tuy nhiên, vì hàm ExecuteQuery của bạn nhận mảng OracleParameter, chúng ta sẽ sử dụng cách đó.

            string sql = @"
                SELECT 
                    PM.PHONG_ID,
                    PM.MA_PHONG,
                    PM.TEN_PHONG,
                    PM.SO_MAY AS TONG_SO_MAY,
                    
                    -- Xác định Trạng thái (Ưu tiên: Bảo trì > Bận > Rảnh)
                    CASE
                        -- 1. Trạng thái Bảo trì (Lấy từ cột TRANG_THAI của bảng PHONGMAY)
                        WHEN PM.TRANG_THAI = 'BAO_TRI' THEN 'Bảo trì'
                        
                        -- 2. Trạng thái Bận (Kiểm tra lịch đặt đã được duyệt và đang diễn ra)
                        WHEN EXISTS (
                            SELECT 1 FROM LICHDAT LD
                            WHERE LD.PHONG_ID = PM.PHONG_ID
                              AND LD.TRANG_THAI = 'DA_DUYET'
                              -- Kiểm tra trùng thời gian với giờ hiện tại
                              AND :current_time BETWEEN LD.GIO_BATDAU AND LD.GIO_KETTHUC
                        ) THEN 'Bận'
                        
                        -- 3. Trạng thái Rảnh (Mặc định)
                        ELSE 'Rảnh'
                    END AS TRANG_THAI_HIENTAI,
                    
                    -- *************************************************************
                    -- GIẢ ĐỊNH: Bảng MAYTINH có cột PHONG_ID và TRANG_THAI ('TOT', 'LOI')
                    -- *************************************************************
                    NVL(SUM(CASE WHEN MT.TRANG_THAI = 'TOT' THEN 1 ELSE 0 END), 0) AS SO_MAY_TOT,
                    NVL(SUM(CASE WHEN MT.TRANG_THAI = 'LOI' THEN 1 ELSE 0 END), 0) AS SO_MAY_LOI
                    
                FROM 
                    PHONGMAY PM
                LEFT JOIN 
                    MAYTINH MT ON PM.PHONG_ID = MT.PHONG_ID
                GROUP BY
                    PM.PHONG_ID, PM.MA_PHONG, PM.TEN_PHONG, PM.SO_MAY, PM.TRANG_THAI
                ORDER BY
                    PM.MA_PHONG
            ";

            // Thiết lập tham số thời gian hiện tại
            OracleParameter[] parameters = new OracleParameter[]
            {
                new OracleParameter("current_time", OracleDbType.Date) { Value = currentTime }
            };

            // Sử dụng hàm ExecuteQuery của bạn để thực thi truy vấn có tham số
            return OracleHelper.ExecuteQuery(sql, parameters);
        }

    }
}
