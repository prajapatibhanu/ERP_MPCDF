using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Dashboard_RMRD_Details_under_Office : System.Web.UI.Page
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrd();
    }
    private void fillgrd()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_tbl_MilkCollectionChallanEntry_By_Date_List", new string[] { "flag", "OfficeID", "CurnDate" }, new string[] { "0", objdb.Office_ID(), txtDate.Text }, "dataset");
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrd();
    }
}