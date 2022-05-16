using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Dashboard_Payroll_Detal_Employee : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        if (Request.QueryString["Rid"] != null)
        {
            if (objdb.Decrypt(Request.QueryString["Rid"]) != null)
            {
                string Rid = objdb.Decrypt(Request.QueryString["Rid"].ToString());
                txtDate.Text = Rid;
                fillgrd();
            }
            else
                txtDate.Text = string.Empty;
        }
        else
            txtDate.Text = string.Empty;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrd();
    }
    private void fillgrd()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_tblHREmployeeDetail_Salay_Generated", new string[] { "flag", "OfficeID", "CurnDate" }, new string[] { "0", objdb.Office_ID(), txtDate.Text }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrd();
    }
}