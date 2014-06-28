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


            string fname = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["username"].ToString());


            lbLogin.Text = "Welcome " + fname+"&nbsp;&nbsp;";
            lbLogin.Visible = true;
            LinkButton1.Text = "Logout";
        }
        else{
            lbLogin.Visible = false;
            LinkButton1.Visible = false;
        }
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        //User Logs in
        if (LinkButton1.Text == "Logout")
        {
            Session.Clear();
            lbLogin.Visible = false;
            LinkButton1.Visible = false;
            Response.Redirect("~/Views/UserLogin.aspx");

        }
        else
        {
            //User Logs out
            lbLogin.Visible = true;
            LinkButton1.Visible = true;
           
            Response.Redirect("~/Views/TaskMain.aspx");

        }
    }

}
