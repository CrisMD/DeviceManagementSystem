using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace Repository.DbHelper
{
    public class DbUtils
    {
        private static OracleConnection _instance = null;

        private static OracleConnection GetNewConnection()
        {
            string conString = "User Id=c##cristiana; password=090896;Data Source=localhost:1521/orcl; Pooling=false;";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = conString;

            return con;
        }

        public static OracleConnection GetConnection()
        {
            if (_instance == null || _instance.State == System.Data.ConnectionState.Closed)
            {
                _instance = GetNewConnection();
                _instance.Open();
            }

            return _instance;
        }
    }
}
