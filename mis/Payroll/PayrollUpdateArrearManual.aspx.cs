using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollUpdateArrearManual : System.Web.UI.Page
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
                ddlYear.DataTextField = "Financial_Year";
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
            ds = objdb.ByProcedure("SpPayrollArrearManual", new string[] { "flag", "Office_ID", "ArrearYear", "ArrearType" }, new string[] { "0", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
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
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                Label Emp_Name = (Label)row.FindControl("Emp_Name");
                string Emp_Id = Emp_Name.ToolTip.ToString();
                //Label UAN = (Label)row.FindControl("lblUAN");
                //Label EPF = (Label)row.FindControl("lblEPF");
                TextBox BasicSalary = (TextBox)row.FindControl("txtBasicSalary");
                TextBox DA = (TextBox)row.FindControl("txtDA");
                TextBox HRA = (TextBox)row.FindControl("txtHRA");
                TextBox Conv = (TextBox)row.FindControl("txtConv");
                TextBox Ord = (TextBox)row.FindControl("txtOrd");
                TextBox Wash = (TextBox)row.FindControl("txtWash");
                TextBox OtherEarning = (TextBox)row.FindControl("txtOtherEarning");
                TextBox TotalEarning = (TextBox)row.FindControl("txtTotalEarning");
                TextBox EPF = (TextBox)row.FindControl("txtEPF");
                TextBox ADA = (TextBox)row.FindControl("txtADA");
                TextBox ITax = (TextBox)row.FindControl("txtITax");
                TextBox PTax = (TextBox)row.FindControl("txtPTax");
                TextBox OtherDeduction = (TextBox)row.FindControl("txtOtherDeduction");
                TextBox TotalDeduction = (TextBox)row.FindControl("txtTotalDeduction");

                if (chkSelect.Checked == true)
                {
                    objdb.ByProcedure("SpPayrollArrearManual", new string[] { "flag", "UpdatedBy", "Emp_ID", "Office_ID", "BasicSalary", "DA", "HRA", "Conv", "Ord", "Wash", "OtherEarning", "TotalEarning", "EPF", "ADA", "ITax", "PTax", "OtherDeduction", "TotalDeduction", "ArrearYear", "ArrearType", "IsActive" },
                    new string[] { "1", ViewState["Emp_ID"].ToString(), Emp_Id, ViewState["Office_ID"].ToString(), BasicSalary.Text, DA.Text, HRA.Text, Conv.Text, Ord.Text, Wash.Text, OtherEarning.Text, TotalEarning.Text, EPF.Text, ADA.Text, ITax.Text, PTax.Text, OtherDeduction.Text, TotalDeduction.Text, ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), "1" }, "dataset");
                }

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