using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_TaskMain : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["username"] != null)
        {
           
            
        }
        else
        {
            Response.Redirect("~/Views/UserLogin.aspx");
        }
        for (int i = 0; i < Session.Count; i++)
        {

            Response.Write(Session[i]);
        }
    }
}