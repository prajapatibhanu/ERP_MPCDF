using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class mis_Payroll_PayRollPayBillMonth_Wise : System.Web.UI.Page
{
    DataSet ds, ds5, ds3 = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                //ds = objdb.ByProcedure("SpAdminOffice",
                //       new string[] { "flag" },
                //       new string[] { "10" }, "dataset");
                //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlOffice.DataSource = ds;
                //    ddlOffice.DataTextField = "Office_Name";
                //    ddlOffice.DataValueField = "Office_ID";
                //    ddlOffice.DataBind();
                //    ddlOffice.Items.Insert(0, new ListItem("All", "0"));

                //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                //    //if (ddlOfficeName.SelectedIndex > 0)
                //    //{
                //    //    FillEmployee();
                //    //}

                //}

                ds = objdb.ByProcedure("SpAdminOffice",
       new string[] { "flag" },
       new string[] { "23" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                    ddlOffice.Items.Insert(0, new ListItem("All", "0"));

                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                }
                GetOfficeDetails();
                FillDropdown();
                
                ViewState["MontylyBill"] = "";
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null)
            {
                ds3.Dispose();
            }
        }
    }
    protected void FillBank()
    {
        try
        {

            ddlBank.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Month", "Year" }, new string[] { "38", ddlOffice.SelectedValue, ddlMonth.SelectedValue.ToString(), ddlFinancialYear.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlBank.DataSource = ds.Tables[0];
                ddlBank.DataTextField = "BBName";
                ddlBank.DataValueField = "Bank_IfscCode";
                ddlBank.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds!= null) { ds.Dispose(); }
        }
    }
    protected void FillDropdown()
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
    //protected void FillGrid()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        GridView1.DataSource = null;
    //        GridView1.DataBind();

    //        ds = null;
    //        ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_Month", "Emp_TypeOfPost" }, new string[] { "4", ddlOffice.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //                GridView1.DataSource = ds;
    //                GridView1.DataBind();
    //                decimal NetSalary = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_NetSalary"));
    //                GridView1.FooterRow.Cells[2].Text = "| TOTAL |";
    //                GridView1.FooterRow.Cells[3].Text = ""+NetSalary.ToString()+"";
    //                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                GridView1.UseAccessibleHeader = true;

    //                if (ViewState["Office_ID"].ToString() == "1")
    //                {
    //                    GridView1.Columns[6].Visible = false;
    //                }

    //                DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice.SelectedValue.ToString() }, "dataset");
    //                if (ds2.Tables[0].Rows.Count != 0)
    //                {
    //                    ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
    //                }
    //                else
    //                {
    //                    ViewState["OfficeName"] = "";
    //                }

    //                lblDeductionDetails.Text = "<p class='text-center' style='font-size:20px;'>" + ViewState["OfficeName"].ToString() + "</br>  Bank List For " + ddlMonth.SelectedItem.ToString() + " / " + ddlFinancialYear.SelectedItem.ToString() + " </p>";

    //                lblDeductionDetails.ToolTip = "<p class='text-center' style='font-size:20px; '>" + ViewState["OfficeName"].ToString() + "</br>  Bank List For " + ddlMonth.SelectedItem.ToString() + " / " + ddlFinancialYear.SelectedItem.ToString() + " </p>";

    //            }

    //        }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
            {
               // FillGrid();
                lblMsg.Text = string.Empty;
                div_page_content.InnerHtml = "";
                string bname = "",branchname="",bbname="",ifsc;
                ViewState["PStatus"] = "";
                foreach (ListItem item in ddlBank.Items)
                {
                    if (item.Selected)
                    {
                        ifsc = item.Value;
                        bbname = item.Text;
                        string[] bb = bbname.Split('-');
                        bname=bb[0];
                        branchname = bb[1];
                        GetMonthlyPayBill(bname, branchname, ifsc);
                       
                    }
                }
                if (ViewState["MontylyBill"].ToString() != "" && ViewState["MontylyBill"].ToString() != null && ViewState["MontylyBill"].ToString() != "0")
                {
                    pnlshow.Visible = true;
                    div_page_content.Visible = true;
                    div_page_content.InnerHtml = ViewState["MontylyBill"].ToString();
                    Print.InnerHtml = ViewState["MontylyBill"].ToString();
                    ViewState["MontylyBill"] = "";
                }
                else
                {
                    pnlshow.Visible = false;
                    div_page_content.InnerHtml = "";
                    div_page_content.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void GetMonthlyPayBill(string bname, string bbname, string ifsc)
    {
        try
        {

            ds5 = null;
            ds5 = objdb.ByProcedure("SpPayrollSalaryDetail",
                new string[] { "flag", "Office_ID", "Salary_Year", "Salary_Month", "Emp_TypeOfPost", "Bank_IfscCode" }, 
                new string[] { "4", ddlOffice.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), 
                    ddlMonth.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString(),ifsc.ToString() }, "dataset");         

            if (ds5.Tables[0].Rows.Count > 0)
            {
               
                StringBuilder sb = new StringBuilder();
                if (ViewState["PStatus"].ToString() != "")
                {
                    sb.Append("<p style='page-break-after: always'>");
                }
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align: left;border:1px solid black;'>Bank :<b>" + bname + " - " + bbname + "</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Month <b>:" + ddlMonth.SelectedItem.Text + "</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Year <b>:" + ddlFinancialYear.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;width:200px'><b>EMPLOYEE CODE & NAME</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>DESIGNATION</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Bank A/C</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>AMOUNT(Rs)</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["SalaryEmp_No"] + "-" + ds5.Tables[0].Rows[i]["Emp_Name"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["Designation_Name"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["Bank_AccountNo"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["Salary_NetSalary"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='border:1px solid black;border:1px solid black;text-align:right;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Salary_NetSalary") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                //div_page_content.Visible = true;
                //div_page_content.InnerHtml = sb.ToString();
                //Print.InnerHtml = sb.ToString();
                ViewState["MontylyBill"] += sb.ToString();
                ViewState["PStatus"] = "1";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlOffice.SelectedItem.Text + "-" + ddlMonth.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + "-" + "MonthlyPayBill.xls");
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
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBank();
    }
}