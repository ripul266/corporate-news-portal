using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace CorporateNewsPortal.Data
{
    public class DbHandler
    {
        const string connectionString = @"Data Source=HYD-6RQH6Q3\SQLEXPRESS; Initial Catalog=cnp;Integrated Security=True;MultipleActiveResultSets=true;";
        private SqlConnection sqlCon = null;
        // INitial Catalog is database names
        //data Source is server name taken from microsoft sql server

        // Two Methods here one to open the connection other to close the connection
        public SqlConnection OpenConnection()
        {
            sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            return sqlCon;
        }
        public bool CloseConnection()
        {
            bool isClosed = false;
            if (sqlCon.State != ConnectionState.Closed)
            {
                sqlCon.Close();
                isClosed = true;
            }
            else
            {
                Console.WriteLine("Connection");
            }
            return isClosed;
        }
    }
}
