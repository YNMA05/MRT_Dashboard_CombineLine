using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ConnectionTester
{
    public static async Task<bool> TestAsync(string connectionString)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}