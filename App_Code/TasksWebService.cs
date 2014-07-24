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


        string sql = string.Format("SELECT t.*,u.fname, a.assignedto as assignedtoid,(SELECT r.fname FROM users r WHERE r.id_users=a.assignedto) as assignedto FROM tasks t LEFT JOIN users u on t.createdby=u.id_users LEFT JOIN task_assignment a ON a.task_id = t.id_tasks WHERE u.username='" + Session["username"] + "' AND t.deleted=0 AND t.completeddate is NULL AND a.archivedate is NULL ORDER BY priority desc, duedate desc;");
        

        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);



        while (results.Read())
        {


            string id_tasks = results.GetString(0);
            string description = results.GetString(1);
            string priority = results.GetString(2);
            string duedate = "";
            string createddate = "";
            string completeddate = "";
            string finalapprovaldate = "";
            string deleted = "";
            string assignedtoid = "";
            string assignedto = "";

            if (!results.IsDBNull(3)) {  duedate = results.GetString(3); } 
            if (!results.IsDBNull(5)) {  createddate = results.GetString(5); } 
            if (!results.IsDBNull(6)) {  completeddate = results.GetString(6); } 
            if (!results.IsDBNull(7)) {  finalapprovaldate = results.GetString(7); } 
            if (!results.IsDBNull(8)) {  deleted = results.GetString(8); }
            if (!results.IsDBNull(10)) { assignedtoid = results.GetString(10); }
            if (!results.IsDBNull(11)) { assignedto = results.GetString(11); } 

            string createdby = results.GetString(9);

            list.Add(new Tasks { id_tasks = id_tasks, description = description, priority = priority, duedate = duedate, createdby = createdby, createddate = createddate, completeddate = completeddate, finalapprovaldate = finalapprovaldate, deleted = deleted, assignedtoid = assignedtoid, assignedto = assignedto });


        }
        results.Close();



        return list;
    }

    [WebMethod]
    public void AddTask(string description, string assignedto, string priority, string duedate, string completiondate, string finalapprovaldate, string submittype, string id_tasks)
    {

      
        duedate = DateTime.Parse(duedate).ToString("yyyy-MM-dd HH:mm:ss");
        
        
        

        string sql = "";
        if(submittype=="add"){

             sql = string.Format("INSERT INTO tasks (description,  priority, duedate, completeddate, finalapprovaldate, createdby, createddate) VALUES('" + description + "','" + priority + "','" + duedate + "'");

            if (completiondate != ""){
                completiondate = DateTime.Parse(completiondate).ToString("yyyy-MM-dd HH:mm:ss");
                sql+=",'" + completiondate + "'";
            }
            else{
                sql +=",NULL";
            }

            if(finalapprovaldate !=""){
                finalapprovaldate = DateTime.Parse(finalapprovaldate).ToString("yyyy-MM-dd HH:mm:ss");
                sql+=",'" + finalapprovaldate + "'";
            }
            else{
                sql +=",NULL";
            }


                sql+=" ,'4',NOW() );";

             //Used for Debug SQL
             //HttpContext.Current.Response.BufferOutput = true;
             //HttpContext.Current.Response.Write(sql);
             //HttpContext.Current.Response.Flush();


             dbconn ds = new dbconn();
             MySqlDataReader results = ds.select(sql);

             sql = string.Format("SELECT MAX(id_tasks) FROM tasks ;");
             dbconn data = new dbconn();
             MySqlDataReader r = data.select(sql);

             string maxid = "";
             while (r.Read())
             {
                  maxid = r.GetString(0);
             }
             sql = string.Format("INSERT INTO task_assignment (task_id,assignedto,assigndate,assignedby) VALUES('" + maxid + "'," + assignedto + ",NOW(),4);");

             dbconn val = new dbconn();
             MySqlDataReader resultset = val.select(sql);


        }
        else if (submittype == "edit")
        {
            sql = string.Format("UPDATE tasks SET description='" + description + "',priority='" + priority + "',duedate='" + duedate + "'");

            if (completiondate != ""){
                completiondate = DateTime.Parse(completiondate).ToString("yyyy-MM-dd HH:mm:ss");
                sql+=",completeddate='" + completiondate + "'";

            }
            else{
                sql += ",completeddate=NULL";

            }

            if(finalapprovaldate !=""){
                finalapprovaldate = DateTime.Parse(finalapprovaldate).ToString("yyyy-MM-dd HH:mm:ss");
                sql+=",finalapprovaldate='" + finalapprovaldate + "'";
            }
            else{
                sql += ",finalapprovaldate=NULL";

            }
                
                
            
            
            sql+=" WHERE id_tasks=" + id_tasks + ";";



            sql += "UPDATE task_assignment SET archivedate=NOW() WHERE task_id='" + id_tasks + "';";
            sql += "INSERT INTO task_assignment (task_id,assignedto,assigndate,assignedby) VALUES('" + id_tasks + "'," + assignedto + ",NOW(),4);";



             dbconn ds = new dbconn();
             MySqlDataReader results = ds.select(sql);


            

        }

       
    }

    [WebMethod]
    public List<Tasks> GetTaskInfo(string recid)
    {


        string sql = string.Format("SELECT t.*,u.fname, a.assignedto as assignedtoid,(SELECT r.fname FROM users r WHERE r.id_users=a.assignedto) as assignedto FROM tasks t LEFT JOIN users u on t.createdby=u.id_users LEFT JOIN task_assignment a ON a.task_id = t.id_tasks WHERE t.id_tasks='" + recid + "';");


        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);


        while (results.Read())
        {


            string id_tasks = results.GetString(0);
            string description = results.GetString(1);
            string priority = results.GetString(2);
            string duedate = "";
            string createddate = "";
            string completeddate = "";
            string finalapprovaldate = "";
            string deleted = "";
            string assignedtoid = "";
            string assignedto = "";

            if (!results.IsDBNull(3)) { duedate = results.GetString(3); }
            if (!results.IsDBNull(5)) { createddate = results.GetString(5); }
            if (!results.IsDBNull(6)) { completeddate = results.GetString(6); }
            if (!results.IsDBNull(7)) { finalapprovaldate = results.GetString(7); }
            if (!results.IsDBNull(8)) { deleted = results.GetString(8); }
            if (!results.IsDBNull(10)) { assignedtoid = results.GetString(10); }
            if (!results.IsDBNull(9)) { assignedto = results.GetString(9); }
   

            string createdby = results.GetString(4);

            list.Add(new Tasks { id_tasks = id_tasks, description = description, priority = priority, duedate = duedate, createdby = createdby, createddate = createddate, completeddate = completeddate, finalapprovaldate = finalapprovaldate, deleted = deleted, assignedtoid = assignedtoid, assignedto = assignedto });


        }
        results.Close();

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
