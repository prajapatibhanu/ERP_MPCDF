using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Payroll_Rpt_PayRollMonthlyWagesSummary : System.Web.UI.Page
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
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
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void WagesSummaryReport()
    {
        DataSet ds5 = new DataSet();

        try
        {
            lblMsg.Text = string.Empty;
            ds5 = objdb.ByProcedure("USP_Payroll_WagesSummary",
            new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear" },
           new string[] { "1", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, 
             }, "dataset");

            if (ds5.Tables[0].Rows.Count > 0)
            {
                pnlshow.Visible = true;
                StringBuilder sb = new StringBuilder();

                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8' style='text-align: center;border:1px solid black;'><b>" + ddlOffice_Name.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: left;border:1px solid black;'><b>WAGES SUMMARY</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Month <b>:" + ddlMonth.SelectedItem.Text + "</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Year <b>:" + ddlFinancialYear.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;break-after: auto'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>EMPLOYEE CODE</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>EMPLOYEE NAME</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>DESIGNATION</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>SECTION CODE</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>GROSS</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>NET</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["SalaryEmp_No"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["Emp_Name"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;'>" + ds5.Tables[0].Rows[i]["Designation_Name"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["SalarySec_No"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["Gross_Salary"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["NetSalary"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='5' style='border:1px solid black;text-align:center;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Gross_Salary") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("NetSalary") ?? 0)) + "</b></td>");
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
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            WagesSummaryReport();
        }

    }

    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlOffice_Name.SelectedItem.Text + "-" + ddlMonth.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + "-" + "Payroll_WagesSummary.xls");
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