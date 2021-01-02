using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.DataAccess
{
    public class Connection
    {

        private OracleConnection connection;
        public OracleConnection conn()
        {
            connection = new OracleConnection();
            connection.ConnectionString = "User Id = BOYUT_2008;Password=123;Data Source=192.168.0.10:1521/boyutdb2019";
            connection.Open();
            return connection;
        }

        public void close()
        {
            connection.Close();
        }
    }
}
