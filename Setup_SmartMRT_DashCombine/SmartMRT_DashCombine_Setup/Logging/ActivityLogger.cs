using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.Security
{
    public static class ActivityLogger
    {
        public static void Log(
        string connectionString,
        string action,
        string target,
        string remark)
        {
            try
            {
                string sql = @"INSERT INTO DC_LOG (USERID, ACTION,TARGET,REMARK,DATEMODIFIED)
                            VALUES (@UserID,@Action,@Target,@Remark,GETDATE())";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(
                        "@UserID",
                        SqlDbType.Int)
                        .Value = AppSession.UserID > 0
                            ? (object)AppSession.UserID
                            : DBNull.Value;

                        cmd.Parameters.Add(
                            "@Action",
                            SqlDbType.NVarChar, 100)
                            .Value = action;

                        cmd.Parameters.Add(
                            "@Target",
                            SqlDbType.NVarChar, 100)
                            .Value = (object)target ?? DBNull.Value;

                        cmd.Parameters.Add(
                            "@Remark",
                            SqlDbType.NVarChar, -1)
                            .Value = (object)remark ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Activity log failed:\n\n" + ex.Message,
                    "Logging Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    
    
}
}
