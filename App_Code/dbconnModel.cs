using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Model Layer and Database Connection
/// </summary>
public class dbconn
{
    private MySqlConnection conn = null;

    List<Tasks> list = new List<Tasks>();
    List<Employees> employeeNameList = new List<Employees>();



	public dbconn()
	{
        //MySqlConnection conn = null;

        string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        conn = new MySqlConnection(ConnectionString);



	}



    public MySqlDataReader select(string query)
    {
        string sql = string.Format(query);

        conn.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();

            return myReader;



    }

    public List<Tasks> GetTasks(string session_username)
    {
        

        string sql = string.Format("SELECT t.*,u.fname, a.assignedto as assignedtoid,(SELECT r.fname FROM users r WHERE r.id_users=a.assignedto) as assignedto FROM tasks t LEFT JOIN users u on t.createdby=u.id_users LEFT JOIN task_assignment a ON a.task_id = t.id_tasks WHERE u.username='" + session_username + "' AND t.deleted=0 AND t.completeddate is NULL AND a.archivedate is NULL ORDER BY priority desc, duedate desc;");


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
            if (!results.IsDBNull(11)) { assignedto = results.GetString(11); }

            string createdby = results.GetString(9);

            list.Add(new Tasks { id_tasks = id_tasks, description = description, priority = priority, duedate = duedate, createdby = createdby, createddate = createddate, completeddate = completeddate, finalapprovaldate = finalapprovaldate, deleted = deleted, assignedtoid = assignedtoid, assignedto = assignedto });


        }
        results.Close();



        return list;
    }

    public void InsertUpdateTask(string description, string assignedto, string priority, string duedate, string completiondate, string finalapprovaldate, string submittype, string id_tasks)
    {


        duedate = DateTime.Parse(duedate).ToString("yyyy-MM-dd HH:mm:ss");




        string sql = "";
        if (submittype == "add")
        {

            sql = string.Format("INSERT INTO tasks (description,  priority, duedate, completeddate, finalapprovaldate, createdby, createddate) VALUES('" + description + "','" + priority + "','" + duedate + "'");

            if (completiondate != "")
            {
                completiondate = DateTime.Parse(completiondate).ToString("yyyy-MM-dd HH:mm:ss");
                sql += ",'" + completiondate + "'";
            }
            else
            {
                sql += ",NULL";
            }

            if (finalapprovaldate != "")
            {
                finalapprovaldate = DateTime.Parse(finalapprovaldate).ToString("yyyy-MM-dd HH:mm:ss");
                sql += ",'" + finalapprovaldate + "'";
            }
            else
            {
                sql += ",NULL";
            }


            sql += " ,'4',NOW() );";



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

            if (completiondate != "")
            {
                completiondate = DateTime.Parse(completiondate).ToString("yyyy-MM-dd HH:mm:ss");
                sql += ",completeddate='" + completiondate + "'";

            }
            else
            {
                sql += ",completeddate=NULL";

            }

            if (finalapprovaldate != "")
            {
                finalapprovaldate = DateTime.Parse(finalapprovaldate).ToString("yyyy-MM-dd HH:mm:ss");
                sql += ",finalapprovaldate='" + finalapprovaldate + "'";
            }
            else
            {
                sql += ",finalapprovaldate=NULL";

            }




            sql += " WHERE id_tasks=" + id_tasks + ";";



            sql += "UPDATE task_assignment SET archivedate=NOW() WHERE task_id='" + id_tasks + "';";
            sql += "INSERT INTO task_assignment (task_id,assignedto,assigndate,assignedby) VALUES('" + id_tasks + "'," + assignedto + ",NOW(),4);";



            dbconn ds = new dbconn();
            MySqlDataReader results = ds.select(sql);




        }


    }


    public List<Tasks> TaskInfo(string recid)
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

    public void DeleteTask(string recid, string name)
    {

        string sql = string.Format("UPDATE tasks SET deleted=1 WHERE id_tasks=" + recid + ";");


        dbconn ds = new dbconn();
        MySqlDataReader results = ds.select(sql);


    }


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

    //User Login
    public static User LoginUser(string username, string password)
    {

        User user = null;

        // step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        HttpContext.Current.Response.Write(sb);//Output Hash to Browser

        string sql = string.Format("Select COUNT(*) from users where username='" + username + "' AND password='" + sb + "';");

        dbconn ds = new dbconn();

        MySqlDataReader results = ds.select(sql);
        while (results.Read())
        {
            
            if (results.GetString(0) == "0")
            {
                break;
            }

            sql = string.Format("SELECT fname, lname, position, password FROM users WHERE  username='" + username + "' AND password='" + sb + "';");

            dbconn command = new dbconn();

            MySqlDataReader result = command.select(sql);

            

            while (result.Read())
            {
                string fname = result.GetString(0);
                string lname = result.GetString(1);
                string position = result.GetString(2);



                user = new User(username, password, fname, lname, position);
            }
            result.Close();
            



        }
        return user;


    }

    public int LoginUser2(string username, string password)
    {
        int checkauth = 0;

        // step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        //HttpContext.Current.Response.Write(sb);//Output Hash to Browser

        string sql = string.Format("Select COUNT(*) from users where username='" + username + "' AND password='" + sb + "';");

        dbconn ds = new dbconn();

        MySqlDataReader results = ds.select(sql);
        while (results.Read())
        {

            if (results.GetString(0) == "0")
            {
                
                break;
            }

            checkauth = 1;


        }


        return checkauth;
    }













}