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
    
    }

    [WebMethod(EnableSession = true)]
    public List<Tasks> GetUserTasks() {


            string sql = string.Format("SELECT t.*,u.fname FROM tasks t LEFT JOIN users u on t.createdby=u.id_users WHERE u.username='" + Session["username"]+ "' ORDER BY priority desc, createddate;");
        

        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);

        while (results.Read())
        {


            string id_tasks = results.GetString(0);
            string description = results.GetString(1);
            string createdby = results.GetString(7);
            string createddate = results.GetString(3);
            string completeddate = results.GetString(4);
            string finalapprovaldate = results.GetString(5);
            string priority = results.GetString(6);

            list.Add(new Tasks { id_tasks = id_tasks, description = description, createdby = createdby, createddate = createddate, completeddate = completeddate, finalapprovaldate = finalapprovaldate, priority = priority });


        }
        results.Close();



        return list;
    }

    [WebMethod]
    public List<Tasks> CreateTask()
    {
        return list;
    }

    [WebMethod]
    public List<Tasks> UpdateTask()
    {
        return list;
    }

    [WebMethod]
    public List<Tasks> DeleteTask(string rec_id)
    {
        int id_tasks = Int32.Parse(rec_id);
        string sql = string.Format("DELETE FROM tasks WHERE id_tasks=" + id_tasks + ";");

        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);
        return list;
    }

    
}
