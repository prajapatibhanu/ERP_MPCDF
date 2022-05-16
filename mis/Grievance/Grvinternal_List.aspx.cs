using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Grievance_Grvinternal_List : System.Web.UI.Page
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
            ds = objdb.ByProcedure("SpGrvInternalDiscussion", new string[] { "flag", "Emp_ID" }, new string[] { "4", ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                lblMsg.Text = "There Is No Grievance Detail Available.";
                lblMsg.Style.Add("color", "Red");
                lblMsg.Style.Add("font-size", "16px");
                GridView1.DataSource = null;
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
        string Application_ID = GridView1.SelectedDataKey.Value.ToString();
        Response.Redirect("Grvinternal_Discussion.aspx?Application_ID=" + objdb.Encrypt(Application_ID));
    }
}