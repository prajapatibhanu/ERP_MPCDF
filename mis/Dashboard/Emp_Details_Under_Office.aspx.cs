using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Dashboard_Emp_Details_Under_Office : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        FillDetail();
    }
    protected void FillDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_tblHREmployee_By_Office_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
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
        FillDetail();
    }
}