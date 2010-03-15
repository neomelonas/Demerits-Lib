using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MIS2010Group1ClassLibrary
{
    public class AssignedDemerits
    {
        public static DataSet GetStudentDemeritList(int? userID)
        {
            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procGetStudentDemeritList", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            connection.Close();
            return dataSet;
        }
        public static DataSet GetDemeritInformation(int demeritID)
        {
            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procGetDemeritInformation", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@demeritID", SqlDbType.Int).Value = demeritID;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            connection.Close();
            return dataSet;
        }
        public static bool AddAssignedDemerit(int teacherID, int studentID, double adWeight, out string error)
        {
            bool success = false;
            error = "none";

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procAddAssignedDemerit", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@teacherID", SqlDbType.Int).Value = teacherID;
            command.Parameters.Add("@studentID", SqlDbType.Int).Value = studentID;
            command.Parameters.Add("@adWeight", SqlDbType.Decimal, 3).Value = adWeight;

            SqlParameter parameter = new SqlParameter("@errorMessage", SqlDbType.VarChar, 100);
            parameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter("@success", SqlDbType.Bit);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            success = Convert.ToBoolean(command.Parameters["@success"].Value);
            error = Convert.ToString(command.Parameters["@errorMessage"].Value);

            connection.Close();
            return success;
        }
        public static bool ADTestCleanup()
        {
            bool success;

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procADTestCleanup", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter("@success", SqlDbType.Bit);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);
            command.ExecuteNonQuery();

            success = Convert.ToBoolean(command.Parameters["@success"].Value);

            connection.Close();

            return success;
        }
    }
}
