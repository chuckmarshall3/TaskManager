using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_UserLogin : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
       
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        

        User user = dbconn.LoginUser(txtLogin.Text, txtPassword.Text);

        if (user != null)
        {
            Session["id_users"] = user.id_users;
            Session["username"] = user.username;
            Session["position"] = user.position;
            Session["fname"] = user.fname;
            Session["lname"] = user.lname;
            
            Response.Redirect("~/Views/TaskMain.html");
        }
        else
        {
            lblError.Text = "Login Failed";
        }
        

    }

}