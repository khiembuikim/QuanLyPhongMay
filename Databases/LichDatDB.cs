using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LTTQ_QLPM.Databases
{
    public class LichDatDB
    {
        public static string CreateLichDat(int phongId, int giangVienId, string nguoiDat, string mucDich, DateTime gioBatDau, DateTime gioKetThuc)
        {
            using (OracleConnection conn = OracleHelper.GetConnection()) // Thay DatabaseHelper bằng class kết nối của bạn
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SP_CREATE_LICH";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 1. Tham số đầu vào
                    cmd.Parameters.Add("p_phong_id", OracleDbType.Decimal).Value = phongId;
                    cmd.Parameters.Add("p_giangvien_id", OracleDbType.Decimal).Value = giangVienId;
                    cmd.Parameters.Add("p_nguoi_dat", OracleDbType.NVarchar2).Value = nguoiDat;
                    cmd.Parameters.Add("p_muc_dich", OracleDbType.NVarchar2).Value = mucDich;

                    // Gán DateTime cho tham số DATE trong Oracle
                    cmd.Parameters.Add("p_start", OracleDbType.Date).Value = gioBatDau;
                    cmd.Parameters.Add("p_end", OracleDbType.Date).Value = gioKetThuc;

                    // 2. Tham số đầu ra
                    OracleParameter pOutMsg = new OracleParameter("p_out_msg", OracleDbType.Varchar2, 200);
                    pOutMsg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pOutMsg);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Trả về thông báo từ SP
                        return pOutMsg.Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu không thể kết nối hoặc lỗi cú pháp SP
                        return "Lỗi thực thi SP: " + ex.Message;
                    }
                }
            }
        }
            public static DataTable GetPhongMayList()
        {
            // Code SQL để lấy ID và Tên phòng (ví dụ)
            string sql = "SELECT PHONG_ID, TEN_PHONG FROM PHONGMAY ORDER BY TEN_PHONG";
            return OracleHelper.ExecuteQuery(sql); // Giả định có hàm ExecuteQuery trả về DataTable
        }
        public static DataTable GetLichDatByGiangVien(int giangVienId, int monthFilter, string statusFilter)
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT 
                    LD.LICH_ID,
                    PM.TEN_PHONG, 
                    TO_CHAR(LD.GIO_BATDAU, 'HH24:MI') || ' - ' || TO_CHAR(LD.GIO_KETTHUC, 'HH24:MI') AS THOI_GIAN,
                    LD.NGAY_DAT,
                    LD.MUC_DICH,
                    LD.TRANG_THAI
                FROM 
                    LICHDAT LD
                JOIN 
                    PHONGMAY PM ON LD.PHONG_ID = PM.PHONG_ID
                WHERE 
                    LD.GIANGVIEN_ID = :p_giangvien_id
            ";

            // Xử lý Lọc theo Tháng
            if (monthFilter > 0 && monthFilter <= 12)
            {
                // Dùng hàm trích xuất tháng/năm của Oracle
                sql += $" AND EXTRACT(MONTH FROM LD.NGAY_DAT) = :p_month ";
            }

            // Xử lý Lọc theo Trạng thái (Nếu không phải "TatCa")
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "TatCa")
            {
                sql += $" AND LD.TRANG_THAI = :p_status ";
            }

            sql += " ORDER BY LD.NGAY_DAT DESC, LD.GIO_BATDAU DESC";

            // Khởi tạo Parameters
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter(":p_giangvien_id", giangVienId));

            if (monthFilter > 0 && monthFilter <= 12)
            {
                parameters.Add(new OracleParameter(":p_month", monthFilter));
            }
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "TatCa")
            {
                parameters.Add(new OracleParameter(":p_status", statusFilter));
            }

            // Giả định bạn có hàm ExecuteQueryWithParameters trong DatabaseHelper
            // Hàm này thực hiện truy vấn và trả về DataTable
            // Nếu không có, bạn cần tự viết logic kết nối/thực thi.
            dt = OracleHelper.ExecuteQuery(sql, parameters.ToArray());

            return dt;
        }
    }
    }

