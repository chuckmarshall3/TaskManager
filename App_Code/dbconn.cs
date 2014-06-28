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
	public dbconn()
	{
        

	}

    public MySqlDataReader select(string query)
    {
        

        MySqlConnection conn = null;

        string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        conn = new MySqlConnection(ConnectionString);

        string sql = string.Format(query);
        
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader myReader;
        myReader = command.ExecuteReader();


        return myReader;

    }
}