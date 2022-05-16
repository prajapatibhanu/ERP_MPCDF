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

public partial class mis_DemandSupply_Rpt_MisMilkOrProduct_JBL : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
            }
        }
        else
        {
            objdb.redirectToHome();
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
                GetJDSMIS_Rpt();
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
    private void GetJDSMIS_Rpt()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_MisReport_JBL",
                     new string[] { "Flag", "FromDate", "ToDate"
                         , "DelivaryShift_idM", "DelivaryShift_idE", "ItemCat_idP", "ItemCat_idM"
                         , "Office_ID" },
                       new string[] { "1", fromdat, todat
                       , objdb.GetShiftMorId(),objdb.GetShiftEveId(), objdb.GetProductCatId(),objdb.GetMilkCatId()
                       , objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                // print for Jabalpur MIS Milk or product Mis Report Morning
                ViewState["JDSMP_MIS"] = "";
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                lblMsg.Text = string.Empty;
                StringBuilder sb = new StringBuilder();


                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: center;'><u><b>MIS Report</b></u></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'><b>Morning<b></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table'>");

                int Count = ds5.Tables[0].Rows.Count;

                sb.Append("<thead>");
                sb.Append("<tr>");
                // sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>S.No.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Product.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Total Qty.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In Ltr.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In KG(Ghee).</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Ghee Amt.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Amt in Rs.</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                for (int i = 0; i < Count; i++)
                {
                    sb.Append("<tr>");
                    // sb.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ProductName"] + "</td>");
                    sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["SupplyQty"] + "</td>");
                    sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["QtyInLtr"] + "</td>");
                    sb.Append("<td style='border-top:1px dashed black;'>" + ((ds5.Tables[0].Rows[i]["InKG"]).ToString() == "0.00" ? "" : ds5.Tables[0].Rows[i]["InKG"]) + "</td>");
                    sb.Append("<td style='border-top:1px dashed black;'>" + ((ds5.Tables[0].Rows[i]["InKG"]).ToString() == "0.00" ? "" : ds5.Tables[0].Rows[i]["GheeAmt"]) + "</td>");
                    sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
                sb.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'></td>");
                sb.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("InKG") ?? 0)) + "</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("GheeAmt") ?? 0)) + "</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                ViewState["Sb"] = sb.ToString();
                div_page_content.InnerHtml = sb.ToString();
                ViewState["JDSMP_MIS"] = sb.ToString();
                //Print.InnerHtml= sb.ToString();
                // End of Mis Report Morning

                // print for Mis Report Evening
                if (ds5.Tables[1].Rows.Count > 0)
                {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<div class='pagebreak'></div>");
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: center;'><u><b>MIS Report</b></u></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: left;'><b>Evening</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                int Count1 = ds5.Tables[1].Rows.Count;
                sb1.Append("<thead>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Product.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Total Qty.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In Ltr.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In KG(Ghee).</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Ghee Amt.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Amt in Rs.</b></td>");
                sb1.Append("</thead>");
                for (int i = 0; i < Count1; i++)
                {
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["ProductName"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["SupplyQty"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["QtyInLtr"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ((ds5.Tables[1].Rows[i]["InKG"]).ToString() == "0.00" ? "" : ds5.Tables[1].Rows[i]["InKG"]) + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ((ds5.Tables[1].Rows[i]["InKG"]).ToString() == "0.00" ? "" : ds5.Tables[1].Rows[i]["GheeAmt"]) + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["Amount"] + "</td>");
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("InKG") ?? 0)) + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("GheeAmt") ?? 0)) + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)) + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_contnt2.InnerHtml = sb1.ToString();
                ViewState["JDSMP_MIS"] += sb1.ToString();
                Print.InnerHtml = ViewState["JDSMP_MIS"].ToString();
            }
                // End of MIS Evenning Report

                

                
                // print for Day Report Evening
                if (ds5.Tables[2].Rows.Count > 0)
                {
                    StringBuilder sb2 = new StringBuilder();
                    sb2.Append("<div class='pagebreak'></div>");
                    sb2.Append("<table style='width:100%; height:100%'>");
                    sb2.Append("<tr>");
                    sb2.Append("<td style='text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb2.Append("</tr>");
                    sb2.Append("<tr>");
                    sb2.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                    sb2.Append("</tr>");
                    sb2.Append("<tr>");
                    sb2.Append("<td style='text-align: center;'><u><b>MIS Report</b></u></td>");
                    sb2.Append("</tr>");
                    sb2.Append("<tr>");
                    sb2.Append("<td style='text-align: left;'><b>Day</td>");
                    sb2.Append("</tr>");
                    sb2.Append("<tr>");
                    sb2.Append("<td style='padding: 2px 5px;'></td>");
                    sb2.Append("<td style='padding: 2px 5px;'></td>");
                    sb2.Append("</tr>");
                    sb2.Append("</table>");
                    sb2.Append("<table class='table'>");
                    int Count3 = ds5.Tables[2].Rows.Count;
                    sb2.Append("<thead>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Product.</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Total Qty.</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In Ltr.</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In KG(Ghee).</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Ghee Amt.</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Amt in Rs.</b></td>");
                    sb2.Append("</thead>");
                    for (int i = 0; i < Count3; i++)
                    {
                        sb2.Append("<tr>");
                        sb2.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[2].Rows[i]["ProductName"] + "</td>");
                        sb2.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[2].Rows[i]["SupplyQty"] + "</td>");
                        sb2.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[2].Rows[i]["QtyInLtr"] + "</td>");
                        sb2.Append("<td style='border-top:1px dashed black;'>" + ((ds5.Tables[2].Rows[i]["InKG"]).ToString() == "0.00" ? "" : ds5.Tables[2].Rows[i]["InKG"]) + "</td>");
                        sb2.Append("<td style='border-top:1px dashed black;'>" + ((ds5.Tables[2].Rows[i]["InKG"]).ToString() == "0.00" ? "" : ds5.Tables[2].Rows[i]["GheeAmt"]) + "</td>");
                        sb2.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[2].Rows[i]["Amount"] + "</td>");
                        sb2.Append("</tr>");

                    }
                    sb2.Append("<tr>");
                    sb2.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'></td>");
                    sb2.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'></td>");
                    sb2.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[2].AsEnumerable().Sum(r => r.Field<decimal?>("InKG") ?? 0)) + "</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[2].AsEnumerable().Sum(r => r.Field<decimal?>("GheeAmt") ?? 0)) + "</b></td>");
                    sb2.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[2].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)) + "</b></td>");
                    sb2.Append("</tr>");
                    sb2.Append("</table>");
                    div_page_contnt3.InnerHtml = sb2.ToString();
                    ViewState["JDSMP_MIS"] += sb2.ToString();
                    Print.InnerHtml = ViewState["JDSMP_MIS"].ToString();
                }
                // End of MIS Day Report
            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["JDSMP_MIS"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
                div_page_contnt2.InnerHtml = "";
                div_page_contnt3.InnerHtml = "";
                Print.InnerHtml = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrintRoutWise.Visible = false;
        btnExportAll.Visible = false;
        ViewState["JDSMP_MIS"] = "";
        div_page_content.InnerHtml = "";
        div_page_contnt2.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        Print.InnerHtml = ViewState["JDSMP_MIS"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "JDS_MIS_" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Print.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
}