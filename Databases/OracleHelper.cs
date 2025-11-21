using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
namespace BTL_LTTQ_QLPM.Databases
{
    internal class OracleHelper
    {
        private static string conStr = "User Id=QLPM;Password=qlpm123;Data Source=localhost:1521/orcl;";

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(conStr);
        }

        public static int ExecuteNonQuery(string sql, OracleParameter[] parameters = null)
        {
            using (OracleConnection conn = GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string sql, OracleParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public static object ExecuteScalar(string sql, OracleParameter[] parameters = null)
        {
            using (OracleConnection conn = GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    // Phương thức ExecuteScalar của OracleCommand trả về giá trị object
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}