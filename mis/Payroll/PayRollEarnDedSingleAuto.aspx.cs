using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayRollEarnDedSingleAuto : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillFinancialYear();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice_Name.Enabled = true;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                else
                {
                    ddlOffice_Name.Enabled = false;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                fillEarnDeductionHead();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "23" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_Name.DataSource = ds;
                ddlOffice_Name.DataTextField = "Office_Name";
                ddlOffice_Name.DataValueField = "Office_ID";
                ddlOffice_Name.DataBind();

            }
            else
            {
                ddlOffice_Name.DataSource = null;
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Clear();

            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }

    protected void FillFinancialYear()
    {
        try
        {


            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = null;

            string msg = "";
            lblDeductionDetails.Text = "";
            //if(ddlOffice_Name.SelectedIndex <= 0)
            //{
            //    msg += "Select Office Name \\n";
            //}
            if (ddlFinancialYear.SelectedIndex <= 0)
            {
                msg += "Select Financial Year \\n";
            }
            if (ddlMonth.SelectedIndex <= 0)
            {
                msg += "Select Month \\n";
            }
            if (ddlEarnDeducHead.SelectedIndex <= 0)
            {
                msg += "Select Earning Deduction Head \\n";
            }
            //if (txtmonth.Text == "")
            //{
            //    msg += "Select Month \\n";
            //}
            if (msg == "")
            {
                lblDeductionDetails.Text = ddlEarnDeducHead.SelectedItem.ToString() + " Details (" + ddlMonth.SelectedItem.ToString() + "-" + ddlFinancialYear.SelectedItem.ToString() + ") of " + ddlOffice_Name.SelectedItem.ToString();

                ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
                    new string[] { "6", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    int NetDa = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Amount"));
                    decimal NetCalculated = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Emp_CalculatedHead"));

                    GridView1.FooterRow.Cells[2].Text = "Total";
                    GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
					GridView1.FooterRow.Cells[4].Text = NetCalculated.ToString("N2");
                    //GridView1.FooterRow.Cells[4].Text = NetDa.ToString("N2");
                    GridView1.FooterRow.Cells[4].CssClass = "TotalDa";
                    GridView1.FooterRow.Cells[5].Text = NetCalculated.ToString("N2");
                    GridView1.FooterRow.Cells[5].CssClass = "NetCalculated";
                    btnSave.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void fillEarnDeductionHead()
    {
        ds = null;
        ddlEarnDeducHead.Items.Clear();
        ds = objdb.ByProcedure("SpPayrollEarnDedOfficeWiseMaster", new string[] { "flag", "Office_ID" }, new string[] { "5", ddlOffice_Name.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlEarnDeducHead.DataSource = ds;
            ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
            ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
            ddlEarnDeducHead.DataBind();
            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void ddlOffice_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            //ddlFinancialYear.ClearSelection();
            //ddlMonth.ClearSelection();
            fillEarnDeductionHead();
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
                Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
                string Emp_ID = lblRowNumber.ToolTip.ToString();
                TextBox TotalAmount = (TextBox)row.FindControl("txtAmount");
                Label lblGenStatus = (Label)row.FindControl("lblGenStatus");


                if (chkSelect.Checked == true && lblGenStatus.Text != "Generated")
                {
                    ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail",
                                      new string[] { "flag", "EarnDeduction_ID" },
                                      new string[] { "4", ddlEarnDeducHead.SelectedValue.ToString() }, "dataset");

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objdb.ByProcedure("SpPayrollEarnDeductionDetail",
                                     new string[] { "flag", "Emp_ID", "EarnDeduction_ID", "Calculation_Type", "Amount", "EarnDeduction_Type", "UpdatedBy", "Year", "SalaryMonth" },
                                     new string[] { "5", Emp_ID.ToString(), ddlEarnDeducHead.SelectedValue.ToString(), ds.Tables[0].Rows[0]["EarnDeduction_Calculation"].ToString(), TotalAmount.Text.ToString(), ds.Tables[0].Rows[0]["EarnDeduction_Type"].ToString(), ViewState["Emp_ID"].ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");

                    }
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
}