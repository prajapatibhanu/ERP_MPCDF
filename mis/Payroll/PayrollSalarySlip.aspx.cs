﻿using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
public partial class mis_Payroll_PayrollSalarySlip : System.Web.UI.Page
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
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                string aaaa = HttpContext.Current.Session.ToString();
                DivSlip.Visible = false;
                lblNotGenerated.Visible = false;
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string Year = ddlYear.SelectedValue.ToString();
            string Month = ddlMonth.SelectedValue.ToString();
            string Office = ViewState["Office_ID"].ToString();
            string Emp_ID = ViewState["Emp_ID"].ToString();            
            //string QueryString = "SalarySlipSingle.aspx?Year=" + objdb.Encrypt(Year) + "&Month=" + objdb.Encrypt(Month) + "&Office=" + objdb.Encrypt(Office) + "&Emp_ID=" + objdb.Encrypt(Emp_ID);
            string QueryString = "SalarySlipCDFSingle.aspx?Year=" + objdb.Encrypt(Year) + "&Month=" + objdb.Encrypt(Month) + "&Office=" + objdb.Encrypt(Office) + "&Emp_ID=" + objdb.Encrypt(Emp_ID);
            Response.Write("<script>");
            Response.Write("window.open('" + QueryString + "','_blank')");
            Response.Write("</script>");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DivSlip.Visible = false;
    //        lblNotGenerated.Visible = false;
    //        if (ddlYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
    //        {
    //            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Year", "MonthNo" }, new string[] { "3", ViewState["Emp_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                FillDetail(ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());
                   
    //            }
    //            else
    //            {
    //                lblNotGenerated.Visible = true;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void FillDetail(string Emp_ID, string Office_ID, string Year, string MonthNo)
    //{
    //    try
    //    {

    //        DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", Office_ID.ToString() }, "dataset");
    //        if (ds2.Tables[0].Rows.Count != 0)
    //        {
    //            ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
    //        }
    //        else
    //        {
    //            ViewState["OfficeName"] = "";
    //        }


    //        lblofficename.Text = ViewState["OfficeName"].ToString() + "<br/>";


    //        ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID, Office_ID, Year, MonthNo }, "dataset");
    //        if (ds.Tables.Count > 0)
    //        {

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lblEmp_Name.Text = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
    //                lblDesignation_Name.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
    //                lblUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
    //                lblBank_AccountNo.Text = ds.Tables[0].Rows[0]["Bank_AccountNo"].ToString();
    //                //lblEmp_GpfType.Text = ds.Tables[0].Rows[0]["Emp_GpfType"].ToString();
    //                //lblEmp_GpfNo.Text = ds.Tables[0].Rows[0]["Emp_GpfNo"].ToString();
    //                lblFinancialYear.InnerHtml = ds.Tables[0].Rows[0]["FinancialYear"].ToString();
    //                lblMonth.InnerHtml = ds.Tables[0].Rows[0]["Month"].ToString();
    //                lblSalary_Basic.Text = ds.Tables[0].Rows[0]["Salary_Basic"].ToString();
    //                // lblSalary_NoDayEarnAmt.Text = ds.Tables[0].Rows[0]["Salary_NoDayEarnAmt"].ToString();
    //                lblSalary_NoDayDeduAmt.Text = ds.Tables[0].Rows[0]["Salary_NoDayDeduAmt"].ToString();
    //                lblSalary_NetSalary.Text = ds.Tables[0].Rows[0]["Salary_NetSalary"].ToString();
    //                lblSalary_EarningTotal.Text = ds.Tables[0].Rows[0]["Salary_EarningTotal"].ToString();
    //                lblPolicyDeduction.Text = ds.Tables[0].Rows[0]["PolicyDeduction"].ToString();
    //                lblSalary_DeductionTotal.Text = ds.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString();
    //                lblBank_Name.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
    //                lblIFSCCode.Text = ds.Tables[0].Rows[0]["Bank_IfscCode"].ToString();
    //                lblGroupInsurance_No.Text = ds.Tables[0].Rows[0]["GroupInsurance_No"].ToString();
    //                lblEPF_No.Text = ds.Tables[0].Rows[0]["EPF_No"].ToString();

    //                DivSlip.Visible = true;
    //            }
    //            if (ds.Tables[1].Rows.Count > 0)
    //            {
    //                RepeaterEarning.DataSource = ds.Tables[1];
    //                RepeaterEarning.DataBind();
    //            }
    //            if (ds.Tables[2].Rows.Count > 0)
    //            {
    //                RepeaterDeduction.DataSource = ds.Tables[2];
    //                RepeaterDeduction.DataBind();
    //            }
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}


}