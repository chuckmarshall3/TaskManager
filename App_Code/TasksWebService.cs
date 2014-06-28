using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


/// <summary>
/// Summary description for TasksWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TasksWebService : System.Web.Services.WebService {

    List<Tasks> list = new List<Tasks>();


    public TasksWebService () {


        string sql = string.Format("SELECT * FROM tasks ;");
         
        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);

        while (results.Read())
        {
            

            string id_tasks = results.GetString(0);
            string description = results.GetString(1);
            string createdby = results.GetString(1);
            string createddate = results.GetString(1);
            string completeddate = results.GetString(1);
            string finalapprovaldate = results.GetString(1);
            string priority = results.GetString(1);

            list.Add(new Tasks { id_tasks = id_tasks, description = description, createdby = createdby, createddate = createddate, completeddate = completeddate, finalapprovaldate = finalapprovaldate, priority = priority });


        }
        results.Close();



        
    }

    [WebMethod]
    public List<Tasks> GetTasks() {
        return list;
    }
    
}
