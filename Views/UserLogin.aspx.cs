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


        //Output Session Variables
            for (int i = 0; i < Session.Count; i++)
            {

                Response.Write(Session[i]);
            }
        Session.Clear();
       
        

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        

        User user = dbConnection.LoginUser(txtLogin.Text, txtPassword.Text);

        if (user != null)
        {
            Session["username"] = user.username;
            Session["password"] = user.password;
            Response.Redirect("~/Views/TaskMain.aspx");
        }
        else
        {
            lblError.Text = "Login Failed";
        }
        

    }

}