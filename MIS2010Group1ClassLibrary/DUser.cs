using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MIS2010Group1ClassLibrary
{
    public class DUser
    {
        public static DataSet GetUserList(string role)
        {
            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procUserList", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Parameter time
            command.Parameters.Add("@role", SqlDbType.VarChar, 10).Value = role;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            connection.Close();
            return dataSet;
        }
    }
}
