using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BTL_LTTQ_QLPM.Databases;
namespace BTL_LTTQ_QLPM.Forms.Admin
{
    public class LichDatDB
    {
        public static DataTable GetLichDat(DateTime ngayLoc, string trangThai)
        {
            string sqlTrangThai = (trangThai == "Tất cả" || string.IsNullOrEmpty(trangThai))
                                  ? "%"
                                  : trangThai;

            string sql = @"
            SELECT 
                L.LICH_ID AS ""Mã"", 
                P.MA_PHONG AS ""Phòng"", 
                TO_CHAR(L.GIO_BATDAU, 'HH24:MI') || ' - ' || TO_CHAR(L.GIO_KETTHUC, 'HH24:MI') AS ""Ca"", 
                GV.HO_TEN AS ""Giảng viên"",
                L.MUC_DICH AS ""Mục đích"", 
                L.NGAY_DAT AS ""Ngày đặt"",
                L.TRANG_THAI AS ""Trạng thái""
            FROM LICHDAT L
            JOIN PHONGMAY P ON L.PHONG_ID = P.PHONG_ID
            JOIN GIANGVIEN GV ON L.GIANGVIEN_ID = GV.GIANGVIEN_ID
            WHERE TRUNC(L.NGAY_DAT) = TRUNC(:p_ngay_dat)
            AND L.TRANG_THAI LIKE :p_trang_thai
            ORDER BY L.GIO_BATDAU";

            OracleParameter[] parameters = new OracleParameter[]
            {
            new OracleParameter("p_ngay_dat", ngayLoc.Date),
            new OracleParameter("p_trang_thai", sqlTrangThai)
            };

            // GỌI TRỰC TIẾP OracleHelper.ExecuteQuery()
            return OracleHelper.ExecuteQuery(sql, parameters);
        }
        public static string DuyetLich(int lichId, int nguoiDuyetId, string hanhDong, string ghiChu)
        {
            string message = "";
            using (var conn = OracleHelper.GetConnection())
            using (var cmd = new OracleCommand("SP_DUYET_LICH", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.Add("p_lich_id", OracleDbType.Int32).Value = lichId;
                cmd.Parameters.Add("p_nguo_duyet", OracleDbType.Int32).Value = nguoiDuyetId;
                cmd.Parameters.Add("p_duyet", OracleDbType.Varchar2).Value = hanhDong; // 'APPROVE' hoặc 'REJECT'
                cmd.Parameters.Add("p_note", OracleDbType.NVarchar2).Value = ghiChu;

                // Output parameter
                var outMsg = cmd.Parameters.Add("p_out_msg", OracleDbType.Varchar2, 2000);
                outMsg.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                message = outMsg.Value.ToString();
            }
            return message;
        }
        public static string XoaLich(int lichId)
        {
            string sql = "DELETE FROM LICHDAT WHERE LICH_ID = :p_lich_id";

            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("p_lich_id", lichId)
            };

            try
            {
                int rowsAffected = OracleHelper.ExecuteNonQuery(sql, parameters);
                if (rowsAffected > 0)
                {
                    return $"Đã xóa thành công lịch ID: {lichId}.";
                }
                return $"Không tìm thấy lịch ID: {lichId} để xóa.";
            }
            catch (Exception ex)
            {
                return "Lỗi Database khi xóa lịch: " + ex.Message;
            }
        }
    }
}
