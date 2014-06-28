using System;
using System.Collections;
using System.Linq;
using System.Web;
//using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;




/// <summary>
/// Summary description for dbConnection
/// </summary>
public static class dbConnection
{

    private static MySqlConnection conn = null;

    static dbConnection()
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        conn = new MySqlConnection(ConnectionString);
        
       
    }




    public static User LoginUser(string username, string password)
    {



        string query = string.Format("Select COUNT(*) from users where username='" + username + "' AND password='" + password + "';");

        conn.Open();
        MySqlCommand command = new MySqlCommand(query, conn);

        try
        {
            DataSet ds = new DataSet();
            if (ds.Tables.Count.Equals(99))
            {
                return null;

            }
            else
            {

                query = string.Format("SELECT fname, lname, position, password FROM users WHERE  username='" + username + "' AND password='" + password + "';");
                //HttpContext.Current.Response.Write(query);
                command = new MySqlCommand(query, conn);

                MySqlDataReader myReader;
                myReader = command.ExecuteReader();
                // Always call Read before accessing data.
                
               // if (password == myReader.GetString(3))
                //{
                    User user = null;

                    while (myReader.Read())
                    {
                        string fname = myReader.GetString(0);
                        string lname = myReader.GetString(1);
                        string position = myReader.GetString(2);

                        

                        user = new User(username, password, fname, lname, position);
                    }
                    myReader.Close();
                    return user;
   
                //}
               // else
               // {
                    //return null;
                //}
                
      
            }
            
        }
        finally
        {
            conn.Close();
        }
    }

}