using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_LTTQ_QLPM.Utils;

namespace BTL_LTTQ_QLPM.Databases
{
    public class TaiKhoanDB
    {
       

public static string CreateUserAndEntity(
    string username,
    string password,
    int roleId,
    string hoTen,
    string sdt,
    string email,
    string chucVu = null,
    decimal? luongCung = null)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hoTen))
            return "Lỗi: Thông tin cơ bản không được rỗng.";

        string hashedPassword = Utils.Utils.HashPassword(password);

        using (OracleConnection conn = OracleHelper.GetConnection())
        {
            conn.Open();
            OracleTransaction transaction = conn.BeginTransaction();

            try
            {
                int? newNhanVienId = null;
                int? newGiangVienId = null;
                string entityType = "";

                // --- BƯỚC 0: CHUẨN BỊ MÃ NHÂN VIÊN (Khắc phục ORA-01400) ---
                string newMaNhanVien = null;
                if (roleId == 1 || roleId == 2)
                {
                    // Gọi hàm để tạo ra mã nhân viên mới, duy nhất (NV001, NV002, ...)
                    newMaNhanVien = GetNextMaNhanVien();
                }

                // --- BƯỚC 1: CHÈN THÔNG TIN CÁ NHÂN VÀO NHANVIEN HOẶC GIANGVIEN ---
                if (roleId == 1 || roleId == 2) // ADMIN hoặc NHANVIEN
                {
                    // Tham số đầu ra cho ID mới được tạo
                    OracleParameter outNhanVienId = new OracleParameter("out_nv_id", OracleDbType.Decimal, ParameterDirection.Output);
                    entityType = "Nhân viên";

                        // ⚠️ THÊM MA_NHANVIEN VÀO LỆNH INSERT VÀO NHANVIEN
                        string sqlInsertNV = "INSERT INTO NHANVIEN (MA_NHANVIEN, HO_TEN, SDT, EMAIL, CHUC_VU, LUONG_CUNG) VALUES (:maNV, :hoTen, :sdt, :email, :chucVu, :luongCung) RETURNING NHANVIEN_ID INTO :out_nv_id";

                        OracleCommand cmdNV = new OracleCommand(sqlInsertNV, conn);
                    cmdNV.Transaction = transaction;

                    // ⚠️ THÊM PARAMETER CHO MA_NHANVIEN
                    cmdNV.Parameters.Add("maNV", newMaNhanVien);

                        cmdNV.Parameters.Add(new OracleParameter("hoTen", OracleDbType.NVarchar2) { Value = hoTen });
                        cmdNV.Parameters.Add(new OracleParameter("sdt", OracleDbType.NVarchar2) { Value = sdt ?? (object)DBNull.Value });
                        cmdNV.Parameters.Add(new OracleParameter("email", OracleDbType.NVarchar2) { Value = email ?? (object)DBNull.Value });
                        cmdNV.Parameters.Add(new OracleParameter("chucVu", OracleDbType.NVarchar2) { Value = chucVu ?? (object)DBNull.Value });
                        cmdNV.Parameters.Add("luongCung", luongCung ?? (object)DBNull.Value);

                    cmdNV.Parameters.Add(outNhanVienId);

                    cmdNV.ExecuteNonQuery();

                        // ❌ ĐÃ XÓA dòng code bị thừa: OracleParameter newIdParam = new OracleParameter("newId", OracleDbType.Decimal, ParameterDirection.Output);
                        // ❌ ĐÃ XÓA dòng code bị thừa: cmdNV.Parameters.Add(newIdParam);

                        Oracle.ManagedDataAccess.Types.OracleDecimal oracleIdNV = (Oracle.ManagedDataAccess.Types.OracleDecimal)outNhanVienId.Value;
                        newNhanVienId = oracleIdNV.IsNull ? (int?)null : Convert.ToInt32(oracleIdNV.Value);
                    }
                else if (roleId == 3) // USER (Giảng viên)
                {
                    // Tham số đầu ra cho ID mới được tạo
                    OracleParameter outGiangVienId = new OracleParameter("out_gv_id", OracleDbType.Decimal, ParameterDirection.Output);
                    entityType = "Giảng viên";

                    string sqlInsertGV = @"
                    INSERT INTO GIANGVIEN (HO_TEN, SDT, EMAIL) 
                    VALUES (:hoTen, :sdt, :email)
                    RETURNING GIANGVIEN_ID INTO :out_gv_id"; // Sửa tên output parameter

                    OracleCommand cmdGV = new OracleCommand(sqlInsertGV, conn);
                    cmdGV.Transaction = transaction;

                        cmdGV.Parameters.Add(new OracleParameter("hoTen", OracleDbType.NVarchar2) { Value = hoTen });
                        cmdGV.Parameters.Add(new OracleParameter("sdt", OracleDbType.NVarchar2) { Value = sdt ?? (object)DBNull.Value });
                        cmdGV.Parameters.Add(new OracleParameter("email", OracleDbType.NVarchar2) { Value = email ?? (object)DBNull.Value });

                        cmdGV.Parameters.Add(outGiangVienId);

                    cmdGV.ExecuteNonQuery();
                        Oracle.ManagedDataAccess.Types.OracleDecimal oracleIdGV = (Oracle.ManagedDataAccess.Types.OracleDecimal)outGiangVienId.Value;
                        newGiangVienId = oracleIdGV.IsNull ? (int?)null : Convert.ToInt32(oracleIdGV.Value); ;
                    }

                    // --- BƯỚC 2: CHÈN VÀO TAIKHOAN ---
                    string sqlInsertTK = "INSERT INTO TAIKHOAN (USERNAME, PASSWORD_HASH, ROLE_ID, NHANVIEN_ID, GIANGVIEN_ID, IS_ACTIVE) VALUES (:uName, :pHash, :rId, :nvId, :gvId, 1)";// THÊM GIANGVIEN_ID VÀO LỆNH SQL

                    OracleCommand cmdTK = new OracleCommand(sqlInsertTK, conn);
                cmdTK.Transaction = transaction;

                cmdTK.Parameters.Add("uName", username);
                cmdTK.Parameters.Add("pHash", hashedPassword);
                cmdTK.Parameters.Add("rId", roleId);

                // Xử lý NHANVIEN_ID (dùng ID vừa tạo hoặc NULL)
                cmdTK.Parameters.Add(new OracleParameter("nvId", OracleDbType.Decimal)
                {
                    Value = newNhanVienId.HasValue ? newNhanVienId.Value : (object)DBNull.Value
                });

                // Xử lý GIANGVIEN_ID (dùng ID vừa tạo hoặc NULL)
                cmdTK.Parameters.Add(new OracleParameter("gvId", OracleDbType.Decimal)
                {
                    Value = newGiangVienId.HasValue ? newGiangVienId.Value : (object)DBNull.Value
                });

                cmdTK.ExecuteNonQuery();

                transaction.Commit();
                return $"Tạo tài khoản '{username}' ({entityType}) thành công.";
            }
            catch (OracleException ex) when (ex.Number == 1) // ORA-00001: unique constraint violated (Username)
            {
                transaction.Rollback();
                return $"Lỗi: Tên đăng nhập '{username}' đã tồn tại.";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return "Lỗi tích hợp khi tạo người dùng: " + ex.Message;
            }
        }
    }
    public static DataTable GetAllUsers()
        {
            // Cần đảm bảo cột IS_ACTIVE tồn tại trong bảng TAIKHOAN
            string sql = @"
        SELECT 
            TK.USERNAME,
            R.ROLE_NAME AS TEN_QUYEN,
            NVL(TK.IS_ACTIVE, 1) AS IS_ACTIVE,
            TK.ROLE_ID,
            TK.NHANVIEN_ID,     
            TK.GIANGVIEN_ID,
            
            -- ⚠️ DÒNG CẦN CHỈNH SỬA: Lấy Tên từ Nhân viên hoặc Giảng viên
            CASE 
                WHEN TK.NHANVIEN_ID IS NOT NULL THEN NV.HO_TEN
                WHEN TK.GIANGVIEN_ID IS NOT NULL THEN GV.HO_TEN
                ELSE N'N/A' -- Nếu không liên kết với ai (hiếm)
            END AS HO_TEN
            
        FROM TAIKHOAN TK
        -- Sử dụng LEFT JOIN để đảm bảo tài khoản vẫn được hiển thị dù chưa có NV/GV
        LEFT JOIN ROLES R ON TK.ROLE_ID = R.ROLE_ID 
        LEFT JOIN NHANVIEN NV ON TK.NHANVIEN_ID = NV.NHANVIEN_ID
        LEFT JOIN GIANGVIEN GV ON TK.GIANGVIEN_ID = GV.GIANGVIEN_ID
        ORDER BY TK.USERNAME";

            return OracleHelper.ExecuteQuery(sql);
        }
        public static DataTable GetAllRoles()
        {
            // SQL: Chọn ID và Tên Quyền, sắp xếp theo ID
            string sql = "SELECT ROLE_ID, ROLE_NAME FROM ROLES ORDER BY ROLE_ID";

            try
            {
                // Giả định OracleHelper.ExecuteQuery là hàm chạy câu lệnh SELECT và trả về DataTable
                return OracleHelper.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý lỗi tại đây
                // Trong môi trường phát triển, bạn có thể ném lại lỗi để debug
                throw new Exception("Lỗi khi lấy danh sách vai trò: " + ex.Message);
            }
        }
        public static DataTable GetUserDetails(string username, int? nhanVienId, int? giangVienId)
        {
            // Dùng SUB-QUERY để lấy thông tin chi tiết của NV hoặc GV
            string sql = $@"
        SELECT
            TK.USERNAME,
            TK.ROLE_ID,
            TK.NHANVIEN_ID,
            TK.GIANGVIEN_ID,
            R.ROLE_NAME,
            -- Lấy thông tin chi tiết tùy theo liên kết
            CASE 
                WHEN TK.NHANVIEN_ID IS NOT NULL THEN (SELECT HO_TEN FROM NHANVIEN WHERE NHANVIEN_ID = TK.NHANVIEN_ID)
                WHEN TK.GIANGVIEN_ID IS NOT NULL THEN (SELECT HO_TEN FROM GIANGVIEN WHERE GIANGVIEN_ID = TK.GIANGVIEN_ID)
            END AS HO_TEN,
            CASE 
                WHEN TK.NHANVIEN_ID IS NOT NULL THEN (SELECT SDT FROM NHANVIEN WHERE NHANVIEN_ID = TK.NHANVIEN_ID)
                WHEN TK.GIANGVIEN_ID IS NOT NULL THEN (SELECT SDT FROM GIANGVIEN WHERE GIANGVIEN_ID = TK.GIANGVIEN_ID)
            END AS SDT,
            CASE 
                WHEN TK.NHANVIEN_ID IS NOT NULL THEN (SELECT EMAIL FROM NHANVIEN WHERE NHANVIEN_ID = TK.NHANVIEN_ID)
                WHEN TK.GIANGVIEN_ID IS NOT NULL THEN (SELECT EMAIL FROM GIANGVIEN WHERE GIANGVIEN_ID = TK.GIANGVIEN_ID)
            END AS EMAIL,
            -- Thông tin đặc thù của Nhân viên
            (SELECT CHUC_VU FROM NHANVIEN WHERE NHANVIEN_ID = TK.NHANVIEN_ID) AS CHUC_VU,
            (SELECT LUONG_CUNG FROM NHANVIEN WHERE NHANVIEN_ID = TK.NHANVIEN_ID) AS LUONG_CUNG
        FROM TAIKHOAN TK
        JOIN ROLES R ON TK.ROLE_ID = R.ROLE_ID
        WHERE TK.USERNAME = :uName";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("uName", username)
            };

            return OracleHelper.ExecuteQuery(sql, parameters);
        }
        public static string UpdateUserAndEntity(
    string username,
    int newRoleId,
    int? currentNhanVienId,
    int? currentGiangVienId,
    string hoTen,
    string sdt,
    string email,
    string chucVu,
    decimal? luongCung)
        {
            using (OracleConnection conn = OracleHelper.GetConnection())
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();

                try
                {
                    // BƯỚC 1: CẬP NHẬT THÔNG TIN CÁ NHÂN (NHANVIEN HOẶC GIANGVIEN)
                    if (currentNhanVienId.HasValue) // Là Nhân viên/Admin
                    {
                        string sqlUpdateNV = @"
                    UPDATE NHANVIEN SET 
                        HO_TEN = :hoTen, 
                        SDT = :sdt, 
                        EMAIL = :email, 
                        CHUC_VU = :chucVu, 
                        LUONG_CUNG = :luongCung
                    WHERE NHANVIEN_ID = :nvId";

                        OracleCommand cmdNV = new OracleCommand(sqlUpdateNV, conn);
                        cmdNV.Transaction = transaction;

                        cmdNV.Parameters.Add(new OracleParameter("hoTen", OracleDbType.NVarchar2) { Value = hoTen });
                        cmdNV.Parameters.Add(new OracleParameter("sdt", OracleDbType.NVarchar2) { Value = sdt ?? (object)DBNull.Value });
                        cmdNV.Parameters.Add(new OracleParameter("email", OracleDbType.NVarchar2) { Value = email ?? (object)DBNull.Value });
                        cmdNV.Parameters.Add(new OracleParameter("chucVu", OracleDbType.NVarchar2) { Value = chucVu ?? (object)DBNull.Value });
                        cmdNV.Parameters.Add("luongCung", luongCung ?? (object)DBNull.Value);
                        cmdNV.Parameters.Add("nvId", currentNhanVienId.Value);
                        cmdNV.ExecuteNonQuery();
                    }
                    else if (currentGiangVienId.HasValue) // Là Giảng viên/User
                    {
                        string sqlUpdateGV = @"
                    UPDATE GIANGVIEN SET 
                        HO_TEN = :hoTen, 
                        SDT = :sdt, 
                        EMAIL = :email
                    WHERE GIANGVIEN_ID = :gvId";

                        OracleCommand cmdGV = new OracleCommand(sqlUpdateGV, conn);
                        cmdGV.Transaction = transaction;

                        cmdGV.Parameters.Add(new OracleParameter("hoTen", OracleDbType.NVarchar2) { Value = hoTen });
                        cmdGV.Parameters.Add(new OracleParameter("sdt", OracleDbType.NVarchar2) { Value = sdt ?? (object)DBNull.Value });
                        cmdGV.Parameters.Add(new OracleParameter("email", OracleDbType.NVarchar2) { Value = email ?? (object)DBNull.Value });
                        cmdGV.Parameters.Add("gvId", currentGiangVienId.Value);
                        cmdGV.ExecuteNonQuery();
                    }
                    // Lưu ý: Nếu có đổi Role từ NV sang GV (hoặc ngược lại), bạn cần logic phức tạp hơn
                    // (DELETE/INSERT entity mới), nhưng tạm thời ta chỉ UPDATE entity hiện tại.

                    // BƯỚC 2: CẬP NHẬT THÔNG TIN TAIKHOAN (Chủ yếu là ROLE_ID)
                    string sqlUpdateAccount = @"
                UPDATE TAIKHOAN SET 
                    ROLE_ID = :rId
                WHERE USERNAME = :uName";

                    OracleCommand cmdAccount = new OracleCommand(sqlUpdateAccount, conn);
                    cmdAccount.Transaction = transaction;

                    cmdAccount.Parameters.Add("rId", newRoleId);
                    cmdAccount.Parameters.Add("uName", username);
                    cmdAccount.ExecuteNonQuery();

                    transaction.Commit();
                    return $"Cập nhật tài khoản '{username}' thành công.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Lỗi Database khi cập nhật tài khoản: " + ex.Message;
                }
            }
        }
        /// <summary>
        /// Vô hiệu hóa (khóa) một tài khoản bằng cách đặt IS_ACTIVE = 0.
        /// </summary>
        /// <param name="username">Tên đăng nhập cần vô hiệu hóa.</param>
        /// <returns>Thông báo kết quả thao tác.</returns>
        public static string DisableUser(string username)
        {
            string sql = @"
            UPDATE TAIKHOAN 
            SET IS_ACTIVE = 0 
            WHERE USERNAME = :uName";

            try
            {
                // Giả định OracleHelper.ExecuteNonQuery là hàm thực thi lệnh UPDATE/DELETE/INSERT
                OracleParameter[] parameters = new OracleParameter[]
                {
                new OracleParameter("uName", username)
                };

                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);

                if (rowsAffected > 0)
                {
                    return $"Tài khoản '{username}' đã được vô hiệu hóa thành công.";
                }
                else
                {
                    return $"Không tìm thấy tài khoản '{username}' để vô hiệu hóa.";
                }
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi nếu có lỗi DB xảy ra
                return "Lỗi Database khi vô hiệu hóa tài khoản: " + ex.Message;
            }
        }
        public static string GetNextMaNhanVien()
        {
            string sql = "SELECT MAX(MA_NHANVIEN) FROM NHANVIEN WHERE MA_NHANVIEN LIKE 'NV%'";

            // Giả định OracleHelper.ExecuteScalar là hàm chạy câu lệnh trả về giá trị đơn
            object result = OracleHelper.ExecuteScalar(sql);

            // Nếu không có bản ghi nào, bắt đầu từ 001
            if (result == null || result == DBNull.Value)
            {
                return "NV001";
            }

            string lastMa = result.ToString(); // Ví dụ: "NV005"

            // Lấy phần số và tăng lên 1
            if (lastMa.StartsWith("NV") && int.TryParse(lastMa.Substring(2), out int currentNum))
            {
                return "NV" + (currentNum + 1).ToString("D3"); // Định dạng 3 chữ số: NV006
            }

            // Nếu định dạng không khớp, trả về mã mặc định
            return "NV001";
        }
    }
}
