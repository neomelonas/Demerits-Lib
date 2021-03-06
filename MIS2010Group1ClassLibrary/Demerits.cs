﻿using MIS2010Group1ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MIS2010Group1ClassLibrary
{
    public class Demerits
    {
        public static DataSet GetAllDemerits()
        {
            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procGetAllDemerits", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            connection.Close();

            return dataSet;
        }

        public static bool ADtoDemeritList(int adID, int demeritID, out string error)
        {
            bool success = false;
            error = "none";

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procADtoDemeritList", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@assignedDemeritID", SqlDbType.Int).Value = adID;
            command.Parameters.Add("@demeritID", SqlDbType.Int).Value = demeritID;

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

        public static bool RemoveAssignedDemerit(int? teacherID, int? studentID, int? assignedDemeritID)
        {
            bool success;

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procDeleteAssignedDemerit", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@teacherID", SqlDbType.Int).Value = teacherID;
            command.Parameters.Add("@studentID", SqlDbType.Int).Value = studentID;
            command.Parameters.Add("@assignedDemeritID", SqlDbType.Int).Value = assignedDemeritID;

            SqlParameter parameter = new SqlParameter("@success", SqlDbType.Bit);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            success = Convert.ToBoolean(command.Parameters["@success"].Value);

            connection.Close();

            return success;
        }

        public static bool DeleteFromDemeritList(int demeritID, int assignedDemeritID)
        {
            bool success;

            //Establish DBconn
            SqlConnection connection = DataServices.SetDatabaseConnection();

            //What DLO to use
            SqlCommand command = new SqlCommand("procDeleteFromDemeritList", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@demeritID", SqlDbType.Int).Value = demeritID;
            command.Parameters.Add("@assignedDemeritID", SqlDbType.Int).Value = assignedDemeritID;

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
