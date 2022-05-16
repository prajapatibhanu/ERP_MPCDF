using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrievanceMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        spnDate.InnerHtml = @DateTime.Now.ToString("D");
        //if (Request.Cookies["EmpID"] == null)
        //{
        //    Response.Redirect("../EmpLogin.aspx");
        //}
        //EmpName = Request.Cookies["EmpID"].Value;
    }
}
