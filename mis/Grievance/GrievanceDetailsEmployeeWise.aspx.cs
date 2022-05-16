using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Grievance_GrievanceDetailsEmployeeWise : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["OfficeType_ID"] = Session["OfficeType_ID"].ToString();
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlEmployee.Enabled = true;
                FillEmployeeName();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillEmployeeName()
    {
        try
        {
            ddlEmployee.ClearSelection();
            ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag" },
                 new string[] { "17" }, "datatset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataTextField = "Name";
                ddlEmployee.DataValueField = "UserId";
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataBind();
                if (ViewState["OfficeType_ID"].ToString() != "1")
                {
                    ddlEmployee.SelectedValue = ViewState["Emp_ID"].ToString();
                    ddlEmployee.Enabled = false;
                }
                else
                {
                    ddlEmployee.Enabled = true;
                    ddlEmployee.ClearSelection();
                }
            }
            ddlEmployee.Items.Insert(0, new ListItem("All", "0"));
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpGrvApplicantDetail", new string[] { "flag", "Emp_ID", "FromDate", "ToDate" },
                new string[] { "16", ddlEmployee.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString(), Convert.ToDateTime(txtTodate.Text, cult).ToString() }, "dataset");
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}