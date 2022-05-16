using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Data;

public partial class mis_Demand_Rpt_CrateForDistributor_GJDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0;
    decimal TotalMvalue = 0, TotalPvalue = 0, TotalMPay = 0, TotalPPay = 0, transpotttotal = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                GetDistributorReport();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GetCompareDate();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Search: ", ex.Message.ToString());
        }

    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
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
    private void GetDistributorReport()
    {
        try
        {
            

           
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductCrateReturnMgmt",
                     new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "DistributorId" },
                       new string[] { "11", fromdat, todat, objdb.Office_ID(), objdb.createdBy() }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                btnExportAll.Visible = true;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: left;border:1px solid black;'>Date  <b>:" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6'style='text-align: left;border:1px solid black;'>Party Name: <b>" + objdb.Emp_Name() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                int Count1 = ds2.Tables[0].Rows.Count;
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>GATEPASS NO.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>ISSUED CRATE</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>SLIP NO.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>RETURN CRATE</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>BALANCE</b></td>");
                sb.Append("</tr>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["DDate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: Left;'>" + ds2.Tables[0].Rows[i]["GatePassNo"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["IssueCrate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["SlipNo"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReturnCrate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["CBalance"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='border:1px solid black;border:1px solid black;text-align:Center;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("IssueCrate") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;'></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("ReturnCrate") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("CBalance") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + objdb.Emp_Name() + "-" + "_Crate_Report.xls");
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