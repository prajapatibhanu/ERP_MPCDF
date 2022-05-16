using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollUpdateEPFExt : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    btnSave.Visible = false;
                    FillYear();
                    //FillGrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillYear()
    {
        try
        { 
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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

            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("spPayrollExternalEmployee", new string[] { "flag", "Office_ID","Epf_Year","Epf_Month" }, new string[] { "2", ViewState["Office_ID"].ToString(),ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label Emp_Name = (Label)row.FindControl("Emp_Name");
                string Emp_Id = Emp_Name.ToolTip.ToString();
                Label UAN = (Label)row.FindControl("lblUAN");
                Label EPF = (Label)row.FindControl("lblEPF");
                 TextBox GROSS_WAGES = (TextBox)row.FindControl("txtGROSS_WAGES");
                 TextBox EPF_WAGES = (TextBox)row.FindControl("txtEPF_WAGES");
                 TextBox EPS_WAGES = (TextBox)row.FindControl("txtEPS_WAGES");
                 TextBox EDLI_WAGES = (TextBox)row.FindControl("txtEDLI_WAGES");
                 TextBox EPF_CONTRI_REMITTED = (TextBox)row.FindControl("txtEPF_CONTRI_REMITTED");
                 TextBox EPS_CONTRI_REMITTED = (TextBox)row.FindControl("txtEPS_CONTRI_REMITTED");
                 TextBox EPF_EPS_DIFF_REMITTED = (TextBox)row.FindControl("txtEPF_EPS_DIFF_REMITTED");
                 TextBox NCP_DAYS = (TextBox)row.FindControl("txtNCP_DAYS");
                 TextBox REFUND_OF_ADVANCES = (TextBox)row.FindControl("txtREFUND_OF_ADVANCES");

                 objdb.ByProcedure("spPayrollExternalEmployee", new string[] { "flag", "UpdatedBy","Emp_ID", "Office_ID", "GROSS_WAGES", "EPF_WAGES", "EPS_WAGES", "EDLI_WAGES", "EPF_CONTRI_REMITTED", "EPS_CONTRI_REMITTED", "EPF_EPS_DIFF_REMITTED", "NCP_DAYS", "REFUND_OF_ADVANCES", "Emp_Name", "EPF","UAN","Epf_Year","Epf_Month","IsActive","IsRegular"},
                 new string[] { "3", ViewState["Emp_ID"].ToString(), Emp_Id, ViewState["Office_ID"].ToString(), GROSS_WAGES.Text, EPF_WAGES.Text, EPS_WAGES.Text, EDLI_WAGES.Text, EPF_CONTRI_REMITTED.Text, EPS_CONTRI_REMITTED.Text, EPF_EPS_DIFF_REMITTED.Text, NCP_DAYS.Text, REFUND_OF_ADVANCES.Text,Emp_Name.Text,EPF.Text,UAN.Text,ddlYear.SelectedValue.ToString(),ddlMonth.SelectedValue.ToString(),"1","1" }, "dataset");

            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}