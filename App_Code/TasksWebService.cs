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




        dbconn ds = new dbconn();
        string session_username = Session["username"].ToString();
        list = ds.GetTasks(session_username);

        return list;
    }

    [WebMethod(EnableSession = true)]
    public void AddTask(string description, string assignedto, string priority, string duedate, string completiondate, string finalapprovaldate, string submittype, string id_tasks)
    {


        dbconn ds = new dbconn();
        ds.InsertUpdateTask( description,  assignedto,  priority,  duedate,  completiondate,  finalapprovaldate,  submittype,  id_tasks);

        //Used for Debug SQL
        //HttpContext.Current.Response.BufferOutput = true;
        //HttpContext.Current.Response.Write(sql);
        //HttpContext.Current.Response.Flush();
 
    }

    [WebMethod(EnableSession = true)]
    public List<Tasks> GetTaskInfo(string recid)
    {



        dbconn ds = new dbconn();
        list = ds.TaskInfo(recid);



        return list;

    }
    [WebMethod(EnableSession = true)]
    public void DeleteTask(string recid, string name)
    {


        dbconn ds = new dbconn();
        ds.DeleteTask( recid,  name);

            
  
    }


    /********************Employee Information*****************/

    [WebMethod(EnableSession = true)]
    public List<Employees> GetEmployeeNames()
    {




        dbconn ds = new dbconn();
        employeeNameList = ds.GetEmployeeNames();



        return employeeNameList;
    }


    [WebMethod(EnableSession = true)]
    public int LoginUser(string username, string password)
    {


        dbconn ds = new dbconn();
        int checkauth = ds.LoginUser2( username,  password);

        if (checkauth == 1)
        {
            Session["username"] = username;

        }

        return checkauth;

    }

    

    [WebMethod(EnableSession = true)]
    public void LogoutUser()
    {
        Session["username"] = "";
        //Session.Clear();

    }

    [WebMethod(EnableSession = true)]
    public int CheckAuth()
    {
        if (Session["username"] == "" || Session["username"] == "")
        {

            return 0;
        }

        return 1;

    }


    
}
