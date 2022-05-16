using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Grievance_ViewGrievance_Details : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpGrvApplicationDetail", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GrvApplication_ID = GridView1.SelectedValue.ToString();
        string App_ID = "0";
        ds = objdb.ByProcedure("SpGrvApplicationDetail", new string[] { "flag", "GrvApplication_ID" }, new string[] { "4", GrvApplication_ID }, "dataset");
        if (ds.Tables[0].Rows.Count > 0)
        {

            App_ID = ds.Tables[0].Rows[0]["App_ID"].ToString();
            Response.Redirect("GrievanceRequest.aspx?GrvApplication_ID=" + objdb.Encrypt(GrvApplication_ID) + "&App_ID=" + objdb.Encrypt(App_ID));
        }



    }
}