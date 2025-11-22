using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LTTQ_QLPM.Databases
{
    public class TaskDB
    {
        public static int UpdateTaskProgress(int taskId, decimal tienDo, string ghiChu)
        {
            string sql = @"
        UPDATE TASK SET 
            TIEN_DO = :tienDo, 
            GHI_CHU = :ghiChu,
            TRANG_THAI = CASE 
                            WHEN :tienDoCheck = 100 THEN 'HOAN_THANH'
                            ELSE 'DANG_THUC_HIEN'
                         END
        WHERE TASK_ID = :taskId";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("tienDo", OracleDbType.Decimal) { Value = tienDo },
        new OracleParameter("ghiChu", OracleDbType.NVarchar2) { Value = ghiChu },
        new OracleParameter("tienDoCheck", OracleDbType.Decimal) { Value = tienDo }, // 👈 thêm tham số thứ 2
        new OracleParameter("taskId", OracleDbType.Decimal) { Value = taskId }
            };

            return OracleHelper.ExecuteNonQuery(sql, parameters);
        }
        
        public static DataTable GetTasksByNhanVienId(int nhanVienId)
        {
            string sql = @"
        SELECT 
            TA.ASSIGN_ID,
            T.TASK_ID, -- 👈 CẦN THIẾT cho việc cập nhật tiến độ
            T.TEN_TASK AS TEN_NHIEM_VU,
            TA.HE_SO_GIAO AS HE_SO,
            TA.NGAY_HET_HAN AS DEADLINE,
            T.TIEN_DO,    -- Tiến độ hiện tại
            T.TRANG_THAI, -- Trạng thái chung
            T.GHI_CHU     -- Ghi chú báo cáo gần nhất
        FROM TASK_ASSIGN TA
        JOIN TASK T ON TA.TASK_ID = T.TASK_ID
        WHERE TA.NHANVIEN_ID = :nhanVienId
        ORDER BY TA.NGAY_HET_HAN DESC";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("nhanVienId", OracleDbType.Decimal) { Value = nhanVienId }
            };

            return OracleHelper.ExecuteQuery(sql, parameters);
        }
    public static DataTable GetLichLamViecByNhanVienId(int nhanVienId, DateTime startDate, DateTime endDate)
        {
            // 1. Lấy sự kiện từ TASK_ASSIGN (Deadline)
            string sqlTask = @"
            SELECT 
                T.TASK_ID AS EVENT_ID,
                T.TEN_TASK AS TEN_SU_KIEN,
                TA.NGAY_HET_HAN AS THOI_GIAN, -- Lấy Deadline
                'Deadline Nhiệm vụ' AS LOAI_SU_KIEN,
                T.TIEN_DO,
                T.TRANG_THAI
            FROM TASK_ASSIGN TA
            JOIN TASK T ON TA.TASK_ID = T.TASK_ID
            WHERE 
                TA.NHANVIEN_ID = :nvIdTask 
                AND TA.NGAY_HET_HAN >= :startDateTask 
                AND TA.NGAY_HET_HAN <= :endDateTask";

            // 2. Lấy sự kiện từ LICHTRUC (Lịch trực ca)
            string sqlLichTruc = @"
            SELECT 
                LT.TRUC_ID AS EVENT_ID, 
                'Trực ca: ' || LT.CA_TRUC AS TEN_SU_KIEN, 
                LT.NGAY_TRUC AS THOI_GIAN, -- Lấy Ngày trực
                'Lịch trực ca' AS LOAI_SU_KIEN,
                100 AS TIEN_DO, -- Giả định ca trực là 100%
                'HOAN_THANH' AS TRANG_THAI -- Trạng thái ca trực (đã lên lịch)
            FROM LICHTRUC LT
            WHERE 
                LT.NHANVIEN_ID = :nvIdTruc 
                AND LT.NGAY_TRUC >= :startDateTruc 
                AND LT.NGAY_TRUC <= :endDateTruc";

            // 3. Kết hợp hai truy vấn bằng UNION ALL
            string sql = sqlTask + " UNION ALL " + sqlLichTruc + " ORDER BY THOI_GIAN ASC";

            // 4. Chuẩn bị Tham số (Phải khai báo tham số cho cả hai truy vấn)
            List<OracleParameter> parameters = new List<OracleParameter>();

            // Tham số cho TASK
            parameters.Add(new OracleParameter("nvIdTask", OracleDbType.Decimal) { Value = nhanVienId });
            parameters.Add(new OracleParameter("startDateTask", OracleDbType.Date) { Value = startDate.Date });
            parameters.Add(new OracleParameter("endDateTask", OracleDbType.Date) { Value = endDate.Date });

            // Tham số cho LICHTRUC
            parameters.Add(new OracleParameter("nvIdTruc", OracleDbType.Decimal) { Value = nhanVienId });
            parameters.Add(new OracleParameter("startDateTruc", OracleDbType.Date) { Value = startDate.Date });
            parameters.Add(new OracleParameter("endDateTruc", OracleDbType.Date) { Value = endDate.Date });

            // Sử dụng OracleHelper để thực thi truy vấn
            return OracleHelper.ExecuteQuery(sql, parameters.ToArray());
        }
    }
}