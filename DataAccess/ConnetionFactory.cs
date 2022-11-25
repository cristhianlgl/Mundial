using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    internal static class ConnetionFactory
    {
        public static IDbConnection CreateConnection(IDbConnection conn = null)
        {
            if (conn == null)
            {
                conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString());
                conn.Open();
                return conn;
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return conn;
            }
        }

    }
}
