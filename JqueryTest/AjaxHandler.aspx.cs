using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JqueryTest_AjaxHandler : System.Web.UI.Page
{


    protected string processingresult;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            

            //if (Request.Form["username"] == "Chuck")
            //{
                //processingresult = "Communicating Correctly";
            //}

            User user = dbConnection.LoginUser(Request.Form["username"], Request.Form["password"]);

            if (user != null)
            {
                //Session["username"] = user.username;
                //Session["password"] = user.password;

                //Response.Redirect("~/Views/TaskMain.aspx");
                processingresult = "Login Succeded";
            }
            else
            {
                processingresult = "Login Failed";
                //JavaScriptSerializer jss = new JavaScriptSerializer();

                //jss.Serialize(0);
            }

        }

    }
}