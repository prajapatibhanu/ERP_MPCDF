using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayRollMonthlyEarnDedDetail : System.Web.UI.Page
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
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
            }
            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice_Name.Enabled = true;
                
            //}
            FillHeads();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillHeads()
    {
        try
        {
            lblDeductionDetails.Text = "";
            ddlEarnDeducHead.Items.Clear();
            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Office_ID" }, new string[] { "20", ddlOffice_Name.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEarnDeducHead.DataSource = ds;
                ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                ddlEarnDeducHead.DataBind();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillFinancialYear()
    {
        try
        {

            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetEsiReport()
    {
        DataSet ds5 = new DataSet();

        try
        {
            ds5 = objdb.ByProcedure("SpPayrollSingleHeadReport",
            new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
           new string[] { "4", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, 
            ddlEarnDeducHead.SelectedValue }, "dataset");

            if (ds5.Tables[0].Rows.Count > 0)
            {
                pnlshow.Visible = true;
                StringBuilder sb = new StringBuilder();
               
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8' style='text-align: center;border:1px solid black;'><b>" + ddlOffice_Name.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: left;border:1px solid black;'>Earning & Deduction Head :<b>" + ddlEarnDeducHead.SelectedItem.Text + "</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Month <b>:" + ddlMonth.SelectedItem.Text + "</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Year <b>:" + ddlFinancialYear.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:20px;break-after: auto'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;width:20px;overflow-wrap: break-word;'><b>EMP. CODE</b></td>");
                sb.Append("<td style='border:1px solid black;width:100px;overflow-wrap: break-word;'><b>EMPLOYEE NAME</b></td>");
                sb.Append("<td style='border:1px solid black;width:20px;overflow-wrap: break-word;'><b>PRESENT DAYS</b></td>");
                sb.Append("<td style='border:1px solid black;width:20px;overflow-wrap: break-word;'><b>GROSS</b></td>");
                sb.Append("<td style='border:1px solid black;width:20px;overflow-wrap: break-word;'><b>EMPLOYEE DEDUCTION (A)</b></td>");
                sb.Append("<td style='border:1px solid black;width:20px;overflow-wrap: break-word;'><b>EMPLOYER CONTRIBUTION( B)</b></td>");
                sb.Append("<td style='border:1px solid black;width:20px;overflow-wrap: break-word;'><b>TOTAL (A+B)</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["SalaryEmp_No"]+ "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" +  ds5.Tables[0].Rows[i]["Emp_Name"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["PayableDays"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["GrossSalary"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["employeedecution"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["employeercontribution"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["TotalDecduction"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='border:1px solid black;border:1px solid black;text-align:right;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PayableDays") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("GrossSalary") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("employeedecution") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("employeercontribution") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalDecduction") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();
            }
            else
            {
                pnlshow.Visible = false;
                div_page_content.Visible = false;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                return;
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
            lblMsg.Text = "";
            string msg = "";

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Columns[7].Visible = true;
            GridView1.Columns[5].Visible = true;
            GridView1.Columns[6].Visible = true;
            lblDeductionDetails.Text = "";
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
            if (msg == "")
            {
                lblDeductionDetails.Text = ddlEarnDeducHead.SelectedItem.ToString() + " Details (" + ddlMonth.SelectedItem.ToString() + "-" + ddlFinancialYear.SelectedItem.ToString() + ") of " + ddlOffice_Name.SelectedItem.ToString();
                if (ddlEarnDeducHead.SelectedValue.ToString()=="8")
                {
                    ds = objdb.ByProcedure("SpPayrollSingleHeadReport",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
                    new string[] { "3", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                }
               
                else{
                    ds = objdb.ByProcedure("SpPayrollSingleHeadReport",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
                    new string[] { "2", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                }                
                
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice_Name.SelectedValue.ToString() }, "dataset");
                    if (ds2.Tables[0].Rows.Count != 0)
                    {
                        ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                    }
                    else
                    {
                        ViewState["OfficeName"] = "";
                    }


                    lblDeductionDetails.ToolTip = "<p class='text-center' style='font-size:17px; font-family:verdana; text-decoration:underline;'>" + ViewState["OfficeName"].ToString() + " <br/> " + ddlEarnDeducHead.SelectedItem.ToString() + " </br> " + ddlMonth.SelectedItem.ToString() + " / " + ddlFinancialYear.SelectedItem.ToString() + " </p>";
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        Label lblEarn = (Label)row.FindControl("lblEarnDed");
    
                        if (lblEarn.Text == "")
                        {
                            lblEarn.Text = "0.00";
                        }
                    }

                    //Decimal ArrearEarningAmt = 0;
                    Decimal totalEarnDedAmt = 0;
                    Decimal totalEarnDedAmtBalance = 0;
                    Decimal totalSalaryDA = 0;
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {

                        if (ds.Tables[0].Rows[i]["InstallAmount"].ToString() != "")
                        {
                            totalEarnDedAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["InstallAmount"].ToString());
                            totalEarnDedAmtBalance += Convert.ToDecimal(ds.Tables[0].Rows[i]["BalanceAmount"].ToString());
                            totalSalaryDA += Convert.ToDecimal(ds.Tables[0].Rows[i]["SalaryAndDA"].ToString());
                            
                        }

                    }

                    ViewState["totalEarnDedAmt"] = totalEarnDedAmt.ToString();
                    ViewState["totalEarnDedAmtBalance"] = totalEarnDedAmtBalance.ToString();
                    ViewState["totalSalaryDA"] = totalSalaryDA.ToString();
                    GridView1.FooterRow.Cells[3].Text = "<b>| TOTAL |</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + ViewState["totalEarnDedAmtBalance"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + ViewState["totalEarnDedAmt"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[7].Text = "<b>" + ViewState["totalSalaryDA"].ToString() + "</b>";

                }
                else if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                if (ddlEarnDeducHead.SelectedValue.ToString() == "8")
                {
                    GridView1.Columns[5].Visible = false;
                    GridView1.Columns[6].Visible = false;
                    GridView1.Columns[7].Visible = true;
                }
                else
                {
                    GridView1.Columns[5].Visible = true;
                    GridView1.Columns[6].Visible = true;
                    GridView1.Columns[7].Visible = false;
                }

            }
            else
            { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (ddlEarnDeducHead.SelectedValue.ToString() !="45")
        {
            pnlshow.Visible = false;
            div_page_content.Visible = false;
            div_page_content.InnerHtml = "";
            Print.InnerHtml = "";
            GridView1.Visible = true;
            FillGrid();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            GetEsiReport();
        }
        
    }
    protected void ddlOffice_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            ddlFinancialYear.ClearSelection();
            ddlMonth.ClearSelection();
            ddlEarnDeducHead.ClearSelection();
            FillHeads();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlOffice_Name.SelectedItem.Text + "-" + ddlMonth.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + "-" + "MonthlyPayBill.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}