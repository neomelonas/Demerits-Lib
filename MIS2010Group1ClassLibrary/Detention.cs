using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MIS2010Group1ClassLibrary
{
    public class Detention
    {
        public static DataSet GetStudentDetentions(int? studentID, bool servedStatus)
        {
            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procGetStudentDetentions", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@studentID", SqlDbType.Int).Value = studentID;
            command.Parameters.Add("@servedStatus", SqlDbType.Bit).Value = servedStatus;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            connection.Close();
            return dataSet;
        }
    }
}
