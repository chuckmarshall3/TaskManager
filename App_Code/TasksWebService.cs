using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Net;

using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Web.Script.Serialization;





/// <summary>
/// Summary description for TasksWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TasksWebService : System.Web.Services.WebService {

    List<Tasks> list = new List<Tasks>();
    List<Employees> employeeNameList = new List<Employees>();
   

    public TasksWebService () {
    
    }

    [WebMethod(EnableSession = true)]
    public List<Tasks> GetUserTasks() {


            string sql = string.Format("SELECT t.*,u.fname FROM tasks t LEFT JOIN users u on t.createdby=u.id_users WHERE u.username='" + Session["username"]+ "' AND t.deleted=0 ORDER BY priority desc, createddate desc;");
        

        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);

        while (results.Read())
        {


            string id_tasks = results.GetString(0);
            string description = results.GetString(1);
            string priority = results.GetString(2);
            string duedate = results.GetString(3);
            
            string createdby = results.GetString(9);
            string createddate = results.GetString(5);
            string completeddate = results.GetString(6);
            string finalapprovaldate = results.GetString(7);
            string deleted = results.GetString(8);

            list.Add(new Tasks { id_tasks = id_tasks, description = description, priority = priority, duedate = duedate, createdby = createdby, createddate = createddate, completeddate = completeddate, finalapprovaldate = finalapprovaldate, deleted = deleted });


        }
        results.Close();



        return list;
    }

    [WebMethod]
    public void AddTask(string description, string assignedto, string priority, string duedate, string completiondate, string finalapprovaldate)
    {

        string sql = string.Format("INSERT INTO tasks (description,  priority, duedate, completeddate, finalapprovaldate) VALUES('" + description +  "','" + priority + "','" + duedate + "','" + completiondate + "','" + finalapprovaldate + "' );");

        //HttpContext.Current.Response.BufferOutput = true;
        //HttpContext.Current.Response.Write(sql);
        //HttpContext.Current.Response.Flush();


        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);

        
    }

    [WebMethod]
    public List<Tasks> UpdateTask()
    {
        return list;
    }


    [WebMethod]
    public void DeleteTask(string recid, string name)
    {

        //string results = recid + "," + name;


        string sql = string.Format("UPDATE tasks SET deleted=1 WHERE id_tasks=" + recid + ";");


            dbconn ds = new dbconn();
            MySqlDataReader results = ds.select(sql);
            //return results.ToString();
  
    }


    /********************Employee Information*****************/

    [WebMethod(EnableSession = true)]
    public List<Employees> GetEmployeeNames()
    {


        string sql = string.Format("SELECT fname, lname, id_users FROM users  WHERE deleted=0 AND reports_to=4 ORDER BY lname;");


        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);

        while (results.Read())
        {


            string fname = results.GetString(0);
            string lname = results.GetString(1);
            string id_users = results.GetString(2);


            employeeNameList.Add(new Employees { fname = fname, lname = lname, id_users = id_users, });


        }
        results.Close();



        return employeeNameList;
    }





    
}
