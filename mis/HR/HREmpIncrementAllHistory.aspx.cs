using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpIncrementAllHistory : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillOffice();
                    //FillGrid();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOldOffice.DataSource = ds;
                ddlOldOffice.DataTextField = "Office_Name";
                ddlOldOffice.DataValueField = "Office_ID";
                ddlOldOffice.DataBind();
                ddlOldOffice.Items.Insert(0, new ListItem("Select Office", "0"));
            }
            ddlOldOffice.SelectedValue = ViewState["Office_ID"].ToString();
            if (ViewState["Office_ID"].ToString() == "1")
            {

                ddlOldOffice.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillGrid()
    {
        try
        {
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");

            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmpIncreament", new string[] { "flag", "Office_ID", "FromOrder_Date", "ToOrder_Date" }, new string[] { "10", ddlOldOffice.SelectedValue.ToString(), ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}