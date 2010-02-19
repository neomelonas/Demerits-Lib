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
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public DataSet GetUserList(string @type)
        {
            DataSet userList = DUser.GetUserList(@type);
            return userList;
        }
    }
}
