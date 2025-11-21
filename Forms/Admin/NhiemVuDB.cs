using BTL_LTTQ_QLPM.Databases;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public  class NhiemVuDB
    {
        public static DataTable GetNhanVienForAssignment()
        {
            // Cần JOIN với TAIKHOAN và ROLES để lọc ra các nhân viên KHÔNG phải Admin.
            string sql = @"
        SELECT 
            NV.NHANVIEN_ID, 
            NV.HO_TEN 
        FROM NHANVIEN NV
        -- BƯỚC 1: JOIN với TAIKHOAN để lấy ROLE_ID
        LEFT JOIN TAIKHOAN TK ON NV.NHANVIEN_ID = TK.NHANVIEN_ID 
        -- BƯỚC 2: JOIN với ROLES để lấy TÊN VAI TRÒ
        LEFT JOIN ROLES R ON TK.ROLE_ID = R.ROLE_ID
        -- Lọc nhân viên có TÊN VAI TRÒ không phải 'ADMIN'
        WHERE R.ROLE_NAME != 'ADMIN'
        ORDER BY NV.HO_TEN";

            return OracleHelper.ExecuteQuery(sql);
        }


        public static string GiaoNhiemVu(int nhanVienId, int adminId, string tenTask, string moTa,
                                 decimal heSoGoc, DateTime deadline)
        {
            // 1. Tạo Mã TASK duy nhất
            string maTask = "TASK_" + DateTime.Now.ToString("yyMMddHHmmss");

            // Lấy kết nối
            using (OracleConnection conn = OracleHelper.GetConnection())
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction();
                try
                {
                    // BƯỚC 1: CHÈN VÀO BẢNG TASK
                    string sqlTask = @"
                INSERT INTO TASK (MA_TASK, TEN_TASK, HE_SO, NGUOI_GIAO, NGAY_GIAO, NGAY_HET_HAN, TIEN_DO, GHI_CHU, TRANG_THAI)
                VALUES (:maTask, :tenTask, :heSo, :adId, SYSDATE, :dl, 0, :moTa, 'Chưa bắt đầu')
                RETURNING TASK_ID INTO :taskId";

                    // SỬA LỖI: Chỉ truyền 2 tham số: SQL và Connection
                    OracleCommand cmdTask = new OracleCommand(sqlTask, conn);
                    // Gán Transaction thủ công
                    cmdTask.Transaction = transaction;

                    // Tham số đầu vào cho TASK
                    cmdTask.Parameters.Add("maTask", maTask);
                    cmdTask.Parameters.Add("tenTask", tenTask);
                    cmdTask.Parameters.Add("heSo", heSoGoc);
                    cmdTask.Parameters.Add("adId", adminId);
                    cmdTask.Parameters.Add("dl", deadline);
                    cmdTask.Parameters.Add("moTa", moTa);

                    // Tham số đầu ra để lấy TASK_ID
                    // Lưu ý: ParameterDirection.ReturnValue không dùng cho mệnh đề RETURNING INTO.
                    // Để sử dụng RETURNING INTO, bạn cần dùng ParameterDirection.Output.
                    OracleParameter taskIdParam = new OracleParameter("taskId", OracleDbType.Decimal, ParameterDirection.Output);
                    cmdTask.Parameters.Add(taskIdParam);

                    cmdTask.ExecuteNonQuery();

                    // Lấy ID vừa được tạo từ tham số Output
                    int newTaskId = Convert.ToInt32(taskIdParam.Value.ToString());

                    if (newTaskId <= 0) throw new Exception("Không thể lấy TASK_ID sau khi tạo.");

                    // BƯỚC 2: CHÈN VÀO BẢNG TASK_ASSIGN
                    string sqlAssign = @"
                INSERT INTO TASK_ASSIGN (TASK_ID, NHANVIEN_ID, NGAY_GIAO, NGAY_HET_HAN, HE_SO_GIAO)
                VALUES (:taskId, :nvId, SYSDATE, :dl, :heSoGiao)";

                    // SỬA LỖI: Chỉ truyền 2 tham số: SQL và Connection
                    OracleCommand cmdAssign = new OracleCommand(sqlAssign, conn);
                    // Gán Transaction thủ công
                    cmdAssign.Transaction = transaction;

                    cmdAssign.Parameters.Add("taskId", newTaskId);
                    cmdAssign.Parameters.Add("nvId", nhanVienId);
                    cmdAssign.Parameters.Add("dl", deadline);
                    cmdAssign.Parameters.Add("heSoGiao", heSoGoc);

                    int rowsAssigned = cmdAssign.ExecuteNonQuery();

                    if (rowsAssigned == 0) throw new Exception("Không thể gán nhiệm vụ cho nhân viên.");

                    transaction.Commit();
                    return $"Đã giao nhiệm vụ thành công (Task ID: {newTaskId}, Mã: {maTask}) cho Nhân viên ID: {nhanVienId}.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Lỗi khi giao nhiệm vụ (Đã Rollback): " + ex.Message;
                }
            }
        }
            public static DataTable GetTasksToEvaluate()
        {
            // Lấy các nhiệm vụ đã được gán, đã được nhân viên cập nhật 'Hoàn thành'
            // nhưng chưa có đánh giá (DANH_GIA IS NULL)
            string sql = @"
        SELECT 
            TA.ASSIGN_ID, 
            T.TEN_TASK,
            NV.HO_TEN AS NHANVIEN_NHAN,
            TA.NGAY_HET_HAN,
            TA.HE_SO_GIAO, -- Điểm Gốc của nhiệm vụ
            T.MA_TASK,
            T.GHI_CHU AS MO_TA_TASK 
        FROM TASK_ASSIGN TA
        JOIN TASK T ON TA.TASK_ID = T.TASK_ID
        JOIN NHANVIEN NV ON TA.NHANVIEN_ID = NV.NHANVIEN_ID
        WHERE T.TRANG_THAI = 'Hoàn thành' -- Trạng thái task hoàn thành
          AND TA.DANH_GIA IS NULL -- Chưa có đánh giá
        ORDER BY TA.NGAY_HET_HAN ASC";

            return OracleHelper.ExecuteQuery(sql);
        }
        public static string UpdateTaskEvaluation(int assignId, string danhGiaMa, decimal phanTram,
                                            int adminId, string ghiChu)
        {
            // Cập nhật các trường đánh giá trong TASK_ASSIGN
            string sql = @"
        UPDATE TASK_ASSIGN
        SET 
            DANH_GIA = :dgMa,
            DANH_GIA_PHAN_TRAM = :dgPhanTram,
            NGUOI_DANH_GIA = :adId,
            NGAY_DANH_GIA = SYSDATE,
            GHI_CHU = :gc -- Giả sử tên cột GHI_CHU_DANH_GIA
        WHERE ASSIGN_ID = :aId";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("dgMa", danhGiaMa),
        new OracleParameter("dgPhanTram", phanTram),
        new OracleParameter("adId", adminId),
        new OracleParameter("gc", ghiChu),
        new OracleParameter("aId", assignId)
            };

            try
            {
                int rows = OracleHelper.ExecuteNonQuery(sql, parameters);
                return rows > 0 ? "Đánh giá nhiệm vụ thành công." : "Cập nhật đánh giá thất bại. ASSIGN_ID không tồn tại.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi đánh giá: " + ex.Message;
            }
        }
    }
    }

