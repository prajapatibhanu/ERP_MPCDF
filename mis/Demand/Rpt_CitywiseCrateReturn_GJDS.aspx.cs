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

public partial class mis_Demand_Rpt_CitywiseCrateReturn_GJDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    decimal TotalissueCaret = 0, TotalClosing = 0, TotalOpening = 0, Totalreturncaret = 0, Total = 0, Totaldiff = 0;
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
                // GetCityWiseReport();

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
                // MergePayment_Report();
                GetCityWiseReport();

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
    private void GetCityWiseReport()
    {
        try
        {
            DateTime date1 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string fromdate = date1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime date2 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string todate = date2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DataSet ds = objdb.ByProcedure("USP_Citywise_merge_Caret_report_GJDS"
                , new string[] { "flag", "Office_ID", "FromDate", "ToDate" }
                , new string[] { "1", objdb.Office_ID(), fromdate.ToString(), todate.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlData.Visible = true;
                    btnExportAll.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table style='width:100%; height:100%;padding: 2px ;' >");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='8' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='8' style='text-align: left;border:1px solid black;padding-left:5px'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='8'style='text-align: center;border:1px solid black;'><b>City Wise Report</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");

                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;padding: 5px ;'><b>S.No.</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>Party Name</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>OPENING BALANCE</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>ISSUED CRATE(A)</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>TOTAL</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>RETURN CRATE(B)</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>CLOSING BALANCE</b></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b>DIFFERENCE(A-B)</b></td>");
                    sb.Append("</tr>");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'>" + (i + 1).ToString() + "</td>");

                        sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds.Tables[0].Rows[i]["PartyName"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'>" + Convert.ToInt32(ds.Tables[0].Rows[i]["opening"]) + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'>" + Convert.ToInt32(ds.Tables[0].Rows[i]["IssueCrate"]) + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'>" + (Convert.ToInt32(ds.Tables[0].Rows[i]["opening"]) + Convert.ToInt32(ds.Tables[0].Rows[i]["IssueCrate"])) + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'>" +  Convert.ToInt32(ds.Tables[0].Rows[i]["Return_caret"]) + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'>" + Convert.ToInt32(ds.Tables[0].Rows[i]["Closing"]) + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'>" + (Convert.ToInt32(ds.Tables[0].Rows[i]["IssueCrate"]) - Convert.ToInt32(ds.Tables[0].Rows[i]["Return_caret"])) + "</td>");
                        sb.Append("</tr>");


                        TotalissueCaret += Convert.ToInt32(ds.Tables[0].Rows[i]["IssueCrate"]);
                        TotalClosing += Convert.ToInt32(ds.Tables[0].Rows[i]["Closing"]);
                        TotalOpening += Convert.ToInt32(ds.Tables[0].Rows[i]["opening"]);
                        Totalreturncaret += Convert.ToInt32(ds.Tables[0].Rows[i]["Return_caret"]);
                        Total += (Convert.ToInt32(ds.Tables[0].Rows[i]["opening"]) + Convert.ToInt32(ds.Tables[0].Rows[i]["IssueCrate"]));
                        Totaldiff += (Convert.ToInt32(ds.Tables[0].Rows[i]["IssueCrate"]) - Convert.ToInt32(ds.Tables[0].Rows[i]["Return_caret"]));
                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding:5px ;'><b>Total</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'><b></b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'><b>" + TotalOpening + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'><b>" + TotalissueCaret + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'><b>" + Total + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'><b>" + Totalreturncaret + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'><b>" + TotalClosing + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: right;padding-right:5px ;'><b>" + Totaldiff + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    div_page_content.Visible = true;
                    div_page_content.InnerHtml = sb.ToString();
                    Print.InnerHtml = sb.ToString();
                }
                else
                {
                    pnlData.Visible = false;
                    btnExportAll.Visible = false;
                    div_page_content.InnerHtml = "";
                    Print.InnerHtml = "";
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-Info", "Sorry!  ", "No Record Found");
                }
            }
            else
            {
                pnlData.Visible = false;
                btnExportAll.Visible = false;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-ban", "alert-Info", "Sorry!  ", "No Record Found");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-Info", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }

    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + "CityWiseCrateReport.xls");
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