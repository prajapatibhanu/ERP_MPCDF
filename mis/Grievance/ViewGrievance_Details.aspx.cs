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
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["OfficeType_ID"] = Session["OfficeType_ID"].ToString();
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
            ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "OfficeTypeId", "Office_ID" }, new string[] { "6", ViewState["OfficeType_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Application_ID = GridView1.SelectedDataKey.Value.ToString();
        Response.Redirect("GrievanceRequest.aspx?Application_ID=" + objdb.Encrypt(Application_ID));
    }
}