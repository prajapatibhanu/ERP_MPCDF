using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComplaintsFeedback : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = objdb.Decrypt(Request.QueryString["Id"].ToString());
        string Status = objdb.Decrypt(Request.QueryString["Status"].ToString());
        ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Application_ID" }, new string[] { "14", ID }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count != 0)
        {
            if (ID != null)
            {
                ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Application_ID", "FeedBack" }, new string[] { "13", ID, Status }, "dataset");
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                {
                    lblmsg.Text = "FeedBack Submitted";
                }
            }
        }
        else
        {
            lblmsg.Text = "FeedBack already Submitted";
        }
    }
}