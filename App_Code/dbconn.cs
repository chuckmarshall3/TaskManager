using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for dbconn
/// </summary>
public class dbconn
{
    private MySqlConnection conn = null;

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

    public void insert(string query)
    {
        string sql = string.Format(query);

        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //cmd.Parameters.Add("?val", 10);
        //cmd.Prepare();
        //cmd.ExecuteNonQuery();

        //cmd.Parameters[0].Value = 20;
        //cmd.ExecuteNonQuery();
    }

    public MySqlDataReader update(string query)
    {

        
        string sql = string.Format(query);
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //cmd.Parameters.Add("?val", 10);
        //cmd.Prepare();
        //cmd.ExecuteNonQuery();

        //cmd.Parameters[0].Value = 20;
        //cmd.ExecuteNonQuery();

        //object returnValue = cmd.ExecuteScalar();
        MySqlDataReader myReader;
        myReader = cmd.ExecuteReader();

        return myReader;
    }


}