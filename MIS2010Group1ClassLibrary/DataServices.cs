using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace MIS2010Group1ClassLibrary
{
    public class DataServices
    {
        public static SqlConnection SetDatabaseConnection()
        {
            string connectionString =
//                @"server=Watergate;database=MIS2010Group1;Integrated Security=true;";
                @"server=157.182.146.196\MisSqlServer2008;database=MIS2010Group1;uid=MIS2010Group1;password=MIS2010Group1";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
