using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for user
/// </summary>
public class User
{

    public string id_users { get; set; }
    public string fname { get; set; }
    public string lname { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string position { get; set; }


    public User(string id_users, string username, string password, string fname, string lname, string position)
	{
        this.id_users = id_users;
        this.fname = fname;
        this.lname = lname;
        this.username = username;
        this.password = password;
        this.position = position;
	}

    public User(string username, string password, string fname, string lname, string position)
    {
        this.fname = fname;
        this.lname = lname;
        this.username = username;
        this.password = password;
        this.position = position;
    }

    


}