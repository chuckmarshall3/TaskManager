using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["username"] != null)
        {
            lbLogin.Text = "Welcome "+ Session["username"].ToString();
            lbLogin.Visible = true;
            LinkButton1.Text = "Logout";
        }
        else{
            lbLogin.Visible = false;
            LinkButton1.Text = "Login";
        }
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        //User Logs in
        if (LinkButton1.Text == "Login")
        {
            lbLogin.Visible = false;
            LinkButton1.Visible = false;
            Response.Redirect("~/Views/UserLogin.aspx");

        }
        else
        {
            //User Logs out
            Session.Clear();
            Response.Redirect("~/Views/Home.aspx");

        }
    }

}
