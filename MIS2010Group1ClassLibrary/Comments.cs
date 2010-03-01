using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MIS2010Group1ClassLibrary
{
    public class Comments
    {
        public static bool AddNewComment(string description, int assignedDemeritID, int? commentLink, int userID, out string error) {
            bool success = false;
            error = "None";

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procAddNewComment", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Parameter timez
            command.Parameters.Add("@commentDesc", SqlDbType.Text).Value = description;
            command.Parameters.Add("@assignedDemeritID", SqlDbType.Int).Value = assignedDemeritID;
            command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@commentLink", SqlDbType.Int).Value = commentLink;
                        
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

        public static DataSet GetComments(int? adID, int? userID)
        {
            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procGetComments", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@assignedDemeritID", SqlDbType.Int).Value = adID;
            command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            connection.Close();

            return dataSet;
        }
        public static bool CommentsTestCleanup()
        {
            bool success;

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procCommentsTestCleanup", connection);
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
