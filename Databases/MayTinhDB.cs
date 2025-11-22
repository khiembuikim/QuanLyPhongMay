using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BTL_LTTQ_QLPM.Databases
{
    public static class MayTinhDB
    {
        /// <summary>
        /// Lấy danh sách máy tính có áp dụng bộ lọc.
        /// </summary>
        /// <param name="phongId">ID phòng máy để lọc (có thể là DBNull.Value nếu chọn "Tất cả").</param>
        /// <param name="trangThai">Mã trạng thái để lọc (VD: "TOT", "LOI").</param>
        
        public static string ThemMayTinh(string maMay, int phongId, string viTri, string ghiChu)
        {
            // Câu lệnh SQL: MAY_ID được trigger tự động tạo.
            string sql = @"
            INSERT INTO MAYTINH (MA_MAY, PHONG_ID, VI_TRI, TRANG_THAI, GHI_CHU) 
            VALUES (:maMay, :phongId, :viTri, 'TOT', :ghiChu)";

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
                new OracleParameter("maMay", OracleDbType.Varchar2) { Value = maMay },
                new OracleParameter("phongId", OracleDbType.Decimal) { Value = phongId },
                new OracleParameter("viTri", OracleDbType.NVarchar2) { Value = viTri ?? (object)DBNull.Value },
                // Sử dụng DBNull.Value nếu ghi chú là chuỗi rỗng hoặc null
                new OracleParameter("ghiChu", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu }
                };

                // Giả định bạn gọi OracleHelper.ExecuteNonQuery để thực thi
                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                return rowsAffected > 0 ? $"Thêm máy '{maMay}' thành công." : "Không thể thêm máy tính.";
            }
            catch (OracleException ex) when (ex.Number == 1) // Lỗi ORA-00001: unique constraint violated (trùng MA_MAY)
            {
                return $"Lỗi: Mã máy '{maMay}' đã tồn tại trong hệ thống.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi thêm máy: " + ex.Message;
            }
        }
        public static DataRow GetMayTinhById(int mayId)
        {
            string sql = @"
        SELECT MAY_ID, MA_MAY, PHONG_ID, VI_TRI, TRANG_THAI, GHI_CHU 
        FROM MAYTINH
        WHERE MAY_ID = :id";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("id", OracleDbType.Decimal) { Value = mayId }
            };

            DataTable dt = OracleHelper.ExecuteQuery(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]; // Trả về dòng dữ liệu đầu tiên
            }
            return null;
        }

        public static string CapNhatTrangThai(int mayId, string newTrangThai)
        {
            // Nếu trạng thái mới là TỐT ('TOT'), ta cũng nên xóa GHI_CHU và đảm bảo TRANG_THAI chỉ là 'TOT'.
            string sql;
            if (newTrangThai.ToUpper() == "TOT")
            {
                sql = @"
                UPDATE MAYTINH 
                SET TRANG_THAI = 'TOT', 
                    GHI_CHU = NULL
                WHERE MAY_ID = :id";
            }
            else
            {
                // Dùng cho các trạng thái khác như 'BAOTRI'
                sql = @"
                UPDATE MAYTINH 
                SET TRANG_THAI = :trangThaiMoi
                WHERE MAY_ID = :id";
            }

            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();
                if (newTrangThai.ToUpper() != "TOT")
                {
                    parameters.Add(new OracleParameter("trangThaiMoi", OracleDbType.Varchar2) { Value = newTrangThai });
                }
                parameters.Add(new OracleParameter("id", OracleDbType.Decimal) { Value = mayId });

                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters.ToArray());

                return rowsAffected > 0
                    ? $"Cập nhật trạng thái máy ID {mayId} thành '{newTrangThai}' thành công."
                    : "Lỗi: Không tìm thấy máy để cập nhật trạng thái.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi cập nhật trạng thái: " + ex.Message;
            }
        }
        public static string GhiLoi(int mayId, string chiTietLoi)
        {
            // Trạng thái mới: Bắt đầu bằng 'LOI: '
            string newTrangThai = "LOI: " + chiTietLoi;

            // Cập nhật TRANG_THAI và GHI_CHU
            string sql = @"
            UPDATE MAYTINH 
            SET TRANG_THAI = :trangThaiMoi,
                GHI_CHU = :ghiChuMoi
            WHERE MAY_ID = :id";

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
                new OracleParameter("trangThaiMoi", OracleDbType.Varchar2) { Value = newTrangThai },
                new OracleParameter("ghiChuMoi", OracleDbType.NVarchar2) { Value = chiTietLoi },
                new OracleParameter("id", OracleDbType.Decimal) { Value = mayId }
                };

                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                return rowsAffected > 0
                    ? $"Ghi lỗi cho máy ID {mayId} ('{chiTietLoi}') thành công."
                    : "Lỗi: Không tìm thấy máy để ghi lỗi.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi ghi lỗi: " + ex.Message;
            }
        }
        public static string SuaViTri(int mayId, string newViTri, int newPhongId)
        {
            // Cập nhật cả VI_TRI và PHONG_ID
            string sql = @"
        UPDATE MAYTINH 
        SET VI_TRI = :viTriMoi,
            PHONG_ID = :phongIdMoi
        WHERE MAY_ID = :id";

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
            // 1. Vị trí mới
            new OracleParameter("viTriMoi", OracleDbType.NVarchar2) { Value = newViTri }, 
            // 2. ID Phòng mới
            new OracleParameter("phongIdMoi", OracleDbType.Decimal) { Value = newPhongId }, 
            // 3. ID máy cần sửa
            new OracleParameter("id", OracleDbType.Decimal) { Value = mayId }
                };

                // Giả định bạn gọi OracleHelper.ExecuteNonQuery
                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                return rowsAffected > 0
                    ? $"Cập nhật vị trí và phòng máy ID {mayId} thành công."
                    : "Lỗi: Không tìm thấy máy để cập nhật.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi cập nhật vị trí và phòng: " + ex.Message;
            }
            // Bạn cần bổ sung các hàm: ThemPhong, SuaPhong, XoaPhong
        }
        public static DataTable GetMayTinhWithFilter(int? phongId, string trangThai)
        {
            // Cấu trúc SQL sử dụng tên tham số DUY NHẤT cho mỗi lần kiểm tra.
            string sql = @"
        SELECT 
            MT.MAY_ID, MT.MA_MAY, PM.PHONG_ID, PM.TEN_PHONG, MT.VI_TRI, MT.TRANG_THAI, MT.GHI_CHU
        FROM MAYTINH MT
        JOIN PHONGMAY PM ON MT.PHONG_ID = PM.PHONG_ID
        WHERE 
            -- Lọc PHONG_ID: Cần 2 tham số riêng biệt
            (:phongId_check IS NULL OR MT.PHONG_ID = :phongId_value) 
            AND
            -- Lọc TRANG_THAI: Cần 5 tham số riêng biệt
            (:trangThai_null IS NULL 
             OR (:trangThai_loi = 'LOI' AND MT.TRANG_THAI LIKE 'LOI:%')
             OR ((:trangThai_tot = 'TOT' OR :trangThai_baotri = 'BAOTRI') AND MT.TRANG_THAI = :trangThai_value)
            )
        ORDER BY PM.TEN_PHONG, MT.MA_MAY";

            // Chuyển đổi giá trị C# sang object/DBNull để bind
            object phongIdValue = phongId.HasValue ? (object)phongId.Value : DBNull.Value;
            object trangThaiValue = string.IsNullOrEmpty(trangThai) ? DBNull.Value : (object)trangThai;

            List<OracleParameter> parameters = new List<OracleParameter>();

            // 1. Bind các tham số PHONG_ID
            parameters.Add(new OracleParameter("phongId_check", OracleDbType.Decimal) { Value = phongIdValue });
            parameters.Add(new OracleParameter("phongId_value", OracleDbType.Decimal) { Value = phongIdValue });

            // 2. Bind các tham số TRANG_THAI
            parameters.Add(new OracleParameter("trangThai_null", OracleDbType.Varchar2) { Value = trangThaiValue });
            parameters.Add(new OracleParameter("trangThai_loi", OracleDbType.Varchar2) { Value = trangThaiValue });
            parameters.Add(new OracleParameter("trangThai_tot", OracleDbType.Varchar2) { Value = trangThaiValue });
            parameters.Add(new OracleParameter("trangThai_baotri", OracleDbType.Varchar2) { Value = trangThaiValue });
            parameters.Add(new OracleParameter("trangThai_value", OracleDbType.Varchar2) { Value = trangThaiValue });

            return OracleHelper.ExecuteQuery(sql, parameters.ToArray());
        }


        // Trong MayTinhDB.cs

        /// <summary>
        /// Chuyển máy về trạng thái TỐT và xóa trường GHI_CHU.
        /// </summary>
        public static string CapNhatTrangThaiVaXoaGhiChu(int mayId, string trangThai)
        {
            // Cập nhật TRANG_THAI và đặt GHI_CHU về NULL
            string sql = @"
        UPDATE MAYTINH 
        SET TRANG_THAI = :trangThaiMoi,
            GHI_CHU = NULL
        WHERE MAY_ID = :id";

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
            new OracleParameter("trangThaiMoi", OracleDbType.Varchar2) { Value = trangThai }, // Giá trị thường là "TOT"
            new OracleParameter("id", OracleDbType.Decimal) { Value = mayId }
                };

                // Giả định bạn gọi OracleHelper.ExecuteNonQuery
                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                return rowsAffected > 0
                    ? $"Khôi phục máy ID {mayId} thành trạng thái '{trangThai}' thành công."
                    : "Lỗi: Không tìm thấy máy để khôi phục.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi khôi phục: " + ex.Message;
            }
        }
        public static string CapNhatTrangThaiGiuGhiChu(int mayId, string newTrangThai)
        {
            string sql = @"
        UPDATE MAYTINH 
        SET TRANG_THAI = :trangThaiMoi
        WHERE MAY_ID = :id";

            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
            new OracleParameter("trangThaiMoi", OracleDbType.Varchar2) { Value = newTrangThai },
            new OracleParameter("id", OracleDbType.Decimal) { Value = mayId }
                };

                // Giả định bạn gọi OracleHelper.ExecuteNonQuery
                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                return rowsAffected > 0
                    ? $"Cập nhật trạng thái máy ID {mayId} thành '{newTrangThai}' thành công."
                    : "Lỗi: Không tìm thấy máy để cập nhật trạng thái.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi cập nhật trạng thái: " + ex.Message;
            }
        }
        public static DataTable GetLookupPhongMay()
        {
            // Cần trả về PHONG_ID và TEN_PHONG
            string sql = "SELECT PHONG_ID, TEN_PHONG FROM PHONGMAY ORDER BY TEN_PHONG";
            return OracleHelper.ExecuteQuery(sql, null);
        }
        public static DataTable GetMayTinhFiltered(int? phongId, string trangThai) // <--- Đã sửa tham số
        {
            string sql = @"
    SELECT 
        mt.MAY_ID, mt.MA_MAY, mt.PHONG_ID, pm.TEN_PHONG, mt.VI_TRI, 
        mt.TRANG_THAI, mt.GHI_CHU
    FROM MAYTINH mt
    JOIN PHONGMAY pm ON mt.PHONG_ID = pm.PHONG_ID
    WHERE 1 = 1 ";

            List<OracleParameter> parameters = new List<OracleParameter>();

            // 1. Lọc theo Phòng ID
            if (phongId.HasValue) // Kiểm tra nếu có giá trị (không phải null)
            {
                sql += " AND mt.PHONG_ID = :phongId ";
                // Gán trực tiếp giá trị int, không còn DBNull.Value
                parameters.Add(new OracleParameter("phongId", OracleDbType.Decimal) { Value = phongId.Value });
            }

            // 2. Lọc theo Trạng Thái (Giữ nguyên logic này)
            if (!string.IsNullOrEmpty(trangThai) && trangThai != "TatCa")
            {
                if (trangThai == "LOI")
                {
                    sql += " AND mt.TRANG_THAI LIKE 'LOI:%' ";
                }
                else
                {
                    sql += " AND mt.TRANG_THAI = :trangThai ";
                    parameters.Add(new OracleParameter("trangThai", OracleDbType.Varchar2) { Value = trangThai });
                }
            }

            sql += " ORDER BY mt.PHONG_ID, mt.MA_MAY";

            return OracleHelper.ExecuteQuery(sql, parameters.ToArray());
        }
    }
}


