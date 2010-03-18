using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using MIS2010Group1ClassLibrary;

namespace MIS2010Group1WebService
{
    /// <summary>
    /// Summary description for MIS2010Group1Service
    /// </summary>
    [WebService(Namespace = "http://dothehustle.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MIS2010Group1Service : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet GetUserList(string @type)
        {
            DataSet userList = DUser.GetUserList(@type);
            return userList;
        }
        [WebMethod]
        public DataSet GetStudentDetentions(int? @studentID, bool @servedStatus)
        {
            DataSet studentDetenitons= Detention.GetStudentDetentions(@studentID, @servedStatus);
            return studentDetenitons;
        }
        [WebMethod]
        public DataSet GetAllDemerits()
        {
            DataSet allDemerits = Demerits.GetAllDemerits();
            return allDemerits;
        }
        [WebMethod]
        public bool AddAssignedDemerit(int teacherID, int studentID, double adWeight, out string error, out int adID)
        {
            bool success;
            error = "";

            success = AssignedDemerits.AddAssignedDemerit(teacherID, studentID, adWeight, out error, out adID);
            return success;
        }
        [WebMethod]
        public bool ADtoDemeritList(int adID, int demeritID, out string error)
        {
            bool success;
            error = "";

            success = Demerits.ADtoDemeritList(adID, demeritID, out error);
            return success;
        }
        [WebMethod]
        public DataSet GetDemeritInformation(int demeritID)
        {
            DataSet demeritInfo = AssignedDemerits.GetDemeritInformation(demeritID);
            return demeritInfo;
        }
        [WebMethod]
        public DataSet GetStudentDemeritList(int? userID)
        {
            DataSet studentDemerits = AssignedDemerits.GetStudentDemeritList(userID);
            return studentDemerits;
        }
        [WebMethod]
        public bool RemoveAssignedDemerit(int assignedDemeritID)
        {
            bool success = AssignedDemerits.RemoveAssignedDemerit(assignedDemeritID);
            return success;
        }
        [WebMethod]
        public bool AddNewComment(string description, int assignedDemeritID, int? commentLink, int userID, out string error)
        {
            error = "";
            bool success = Comments.AddNewComment(description, assignedDemeritID, commentLink, userID, out error);
            return success;
        }
        [WebMethod]
        public DataSet GetComments(int? adID, int? userID)
        {
            DataSet comments = Comments.GetComments(adID, userID);
            return comments;
        }
    }
}
