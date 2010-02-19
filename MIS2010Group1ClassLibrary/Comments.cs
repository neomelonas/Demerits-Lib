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
        public bool procAddNewComment(string description, int adID, int commentLink, out string error) {
            bool success = false;
            error = "none";

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procAddNewComment", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Parameter timez
            command.Parameters.Add("@commentDesc", SqlDbType.Text).Value = description;
            command.Parameters.Add("@assignedDemeritID", SqlDbType.Int).Value = adID;
            if (commentLink != null) {
                command.Parameters.Add("@commentLink", SqlDbType.Int).Value = commentLink;
            }
            SqlParameter parameter = new SqlParameter("@errorMessage", SqlDbType.VarChar, 100); 
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = error;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            connection.Close();
            return success;
        }
    }
}
