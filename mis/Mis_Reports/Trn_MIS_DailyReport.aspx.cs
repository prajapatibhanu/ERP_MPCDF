using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;

public partial class mis_Mis_Reports_Trn_MIS_DailyReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Text = Date;
                txtEntryDate.Attributes.Add("readonly", "readonly");
                GetOfficeDetails();
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
    private void GetMISReport()
    {
        try
        {
            DateTime date1 = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
            string entrydate = date1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds1 = objdb.ByProcedure("USP_Trn_Manage_MIS_Data",
                new string[] { "Flag", "Office_ID", "Searchdate" },
                new string[] { "2", objdb.Office_ID(), entrydate.ToString() }, "dataset");
            if (ds1.Tables[0].Rows.Count != 0 && ds1 != null)
            {  
                
                // start of partywise MILK PURCHASE RATE
                // ViewState["DailyMISReport"] = "";
                
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                   lblMsg.Text = string.Empty;
                
                    pnlData.Visible = true;
                    btnExportAll.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='text-align: center;border:1px solid black;'><b>MILK PURCHASE RATE</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;padding: 5px ;'></td>");
                    sb.Append("<td style='border:1px solid black;padding: 5px ;'><b></b></td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>Effective Date</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds1.Tables[0].Rows[0]["EntryDate"] + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>Buff (Kg FAT)</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds1.Tables[0].Rows[0]["Buff_FAT_InKG"] + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>Cow (Kg TS)</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds1.Tables[0].Rows[0]["Cow_TS_InKG"] + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>Comm (Kg FAT)</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds1.Tables[0].Rows[0]["Comm_FAT_InKG"] + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>Payment upto </b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds1.Tables[0].Rows[0]["PaymentUpto"] + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>Dues Amount (Rs in Lkh) </b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: left;padding-right:5px ;'>" + ds1.Tables[0].Rows[0]["DuesAmount"] + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    ViewState["Sb"] = sb.ToString();
                    div_page_content.InnerHtml = sb.ToString();
                    Print.InnerHtml = ViewState["Sb"].ToString();
                    }
                // End of partywise MILK PURCHASE RATE

                 // start of OWN PROCUREMENT (IN KG)
                    if (ds1.Tables[1].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='4' style='text-align: center;border:1px solid black;'><b>OWN PROCUREMENT (IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>QUANTITY (IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>FAT %</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SNF%</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[1].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[1].Rows[i]["Office_Name"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["Qty"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["FAT_Per"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["SNF_Per"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("Qty") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + (Convert.ToDecimal(ds1.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("FAT_Per") ?? 0)) / Count).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + (Convert.ToDecimal(ds1.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("SNF_Per") ?? 0)) / Count).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        Count = 0;
                        div_page_content1.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                  // END of OWN PROCUREMENT (IN KG)

                    // start of OTHER UNION / PARTY PROCUREMENT (IN KG)
                    if (ds1.Tables[2].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='4' style='text-align: center;border:1px solid black;'><b>OTHER UNION / PARTY PROCUREMENT (IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>QUANTITY (IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>FAT %</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SNF %</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[2].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[2].Rows[i]["ThirdPartyUnion_Name"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[2].Rows[i]["Qty"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[2].Rows[i]["FAT_Per"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[2].Rows[i]["SNF_Per"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[2].AsEnumerable().Sum(r => r.Field<decimal?>("Qty") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + (Convert.ToDecimal(ds1.Tables[2].AsEnumerable().Sum(r => r.Field<decimal?>("FAT_Per") ?? 0)) / Count).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + (Convert.ToDecimal(ds1.Tables[2].AsEnumerable().Sum(r => r.Field<decimal?>("SNF_Per") ?? 0)) / Count).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content2.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                        Count = 0;
                    }

                // ENT of OTHER UNION / PARTY PROCUREMENT (IN KG)

                    // start of CONVERSION
                    if (ds1.Tables[3].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='4' style='text-align: center;border:1px solid black;'><b>CONVERSION (IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>QUANTITY (IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SMP QTY (IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SNF (IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[3].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[3].Rows[i]["ThirdPartyUnion_Name"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[3].Rows[i]["Qty"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[3].Rows[i]["SMP_QTY"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[3].Rows[i]["WB_QTY"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[3].AsEnumerable().Sum(r => r.Field<decimal?>("Qty") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[3].AsEnumerable().Sum(r => r.Field<decimal?>("SMP_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[3].AsEnumerable().Sum(r => r.Field<decimal?>("WB_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content3.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of CONVERSION

                    // start of CFP ACCOUNTING
                    if (ds1.Tables[4].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='4' style='text-align: center;border:1px solid black;'><b>CFP ACCOUNTING </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>PRODUCTION</b></td>");
                        sb1.Append("<td colspan='2' style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SALE</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>DCS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>OTHERS</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[4].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[4].Rows[i]["ItemName"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[4].Rows[i]["Production"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[4].Rows[i]["DCS_Sale"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[4].Rows[i]["Other_Sale"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[4].AsEnumerable().Sum(r => r.Field<decimal?>("Production") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[4].AsEnumerable().Sum(r => r.Field<decimal?>("DCS_Sale") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[4].AsEnumerable().Sum(r => r.Field<decimal?>("Other_Sale") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content4.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of CFP ACCOUNTING


                    // start of GHEE ACCOUNT
                    if (ds1.Tables[5].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='2' style='text-align: center;border:1px solid black;'><b>GHEE ACCOUNT (IN KG) </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>STOCK POSITION	(IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[5].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[5].Rows[i]["Particular"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[5].Rows[i]["StockPosition"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[5].AsEnumerable().Sum(r => r.Field<decimal?>("StockPosition") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content5.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of GHEE ACCOUNT

                    // start of COMODITIES CONSUMPTION
                    if (ds1.Tables[6].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='3' style='text-align: center;border:1px solid black;'><b>COMODITIES CONSUMPTION (IN KG) </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SMP QTY.(IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>WB QTY.(IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[6].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[6].Rows[i]["Particular"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[6].Rows[i]["SMP_QTY"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[6].Rows[i]["WB_QTY"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[6].AsEnumerable().Sum(r => r.Field<decimal?>("SMP_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[6].AsEnumerable().Sum(r => r.Field<decimal?>("WB_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content6.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of COMODITIES CONSUMPTION

                    // start of COMODITIES STOCK POSITION
                    if (ds1.Tables[7].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='4' style='text-align: center;border:1px solid black;'><b>COMODITIES STOCK POSITION (IN KG) </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td colspan='3' style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>COMODITIES QUANTITY</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='text-align: center;border:1px solid black;'></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SMP QTY.(IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>WB QTY.(IN KG)</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>WMP QTY.(IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[7].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[7].Rows[i]["ThirdPartyUnion_Name"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[7].Rows[i]["SMP_QTY"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[7].Rows[i]["WB_QTY"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[7].Rows[i]["WMP_QTY"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[7].AsEnumerable().Sum(r => r.Field<decimal?>("SMP_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[7].AsEnumerable().Sum(r => r.Field<decimal?>("WB_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[7].AsEnumerable().Sum(r => r.Field<decimal?>("WMP_QTY") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content7.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of COMODITIES STOCK POSITION



                    // start of IP PRODUCT
                    if (ds1.Tables[8].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='2' style='text-align: center;border:1px solid black;'><b>IP PRODUCT (IN KG) </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>SALE (IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[8].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[8].Rows[i]["ProductName"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[8].Rows[i]["ProductSale"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[8].AsEnumerable().Sum(r => r.Field<decimal?>("ProductSale") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content8.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of IP PRODUCT


                    // start of MILK SALE TOWN WISE (LTR / DAY)
                    if (ds1.Tables[9].Rows.Count > 0)
                    {
                        DataTable dt3 = new DataTable();
                        dt3 = ds1.Tables[9];
                        int Count = dt3.Rows.Count;
                        int ColCount = dt3.Columns.Count;
                        decimal minus_Rate_Amount = 0;
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='" + (ColCount) + "' style='text-align: center;border:1px solid black;'><b>MILK SALE TOWN WISE (LTR / DAY) </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='width:3px !important;border: 1px solid #000000 !important;'><b>S.No</b></td>");
                        sb1.Append("<td style='width:20px !important;border: 1px solid #000000 !important;'><b>CITY NAME</b></td>");
                        for (int j = 0; j < ColCount; j++)
                        {

                            if (dt3.Columns[j].ToString() != "RouteHeadId" && dt3.Columns[j].ToString() != "RouteHeadName")
                            {
                                string ColName = dt3.Columns[j].ToString();
                                sb1.Append("<td style='width:15px !important;border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                            }

                        }
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        for (int i = 0; i < Count; i++)
                        {

                            sb1.Append("<tr>");
                          
                            if(i!=0)
                            {
                                sb1.Append("<td style='border: 1px solid #000000 !important;'>" + i.ToString() + "</td>");
                                sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + dt3.Rows[i]["RouteHeadName"] + "</td>");
                            }
                            else
                            {
                                sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + dt3.Rows[i]["RouteHeadName"] + "</b></td>");
                            }
                          
                            for (int K = 0; K < ColCount; K++)
                            {
                                if (dt3.Columns[K].ToString() != "RouteHeadId" && dt3.Columns[K].ToString() != "RouteHeadName")
                                {
                                    string ColName = dt3.Columns[K].ToString();
                                    
                                    if(i==0)
                                    {
                                        if (ColName == "Total")
                                        {
                                            sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0</b></td>");
                                            minus_Rate_Amount = Convert.ToDecimal(dt3.Rows[i][ColName]);
                                        }
                                        else
                                        {
                                            sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + dt3.Rows[i][ColName].ToString() + "</b></td>");
                                        }
                                      
                                    }
                                    else
                                    {
                                        sb1.Append("<td style='border: 1px solid #000000 !important;'>" + dt3.Rows[i][ColName].ToString() + "</td>");
                                    }
                                  


                                }

                            }
                            sb1.Append("</tr>");
                        }
                      
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='2' style='border:1px solid black;'><b>Total</b></td>");
                        decimal sum13 = 0;
                        foreach (DataColumn column2 in dt3.Columns)
                        {
                            if (column2.ToString() != "RouteHeadId" && column2.ToString() != "RouteHeadName")
                            {
                                if (column2.ToString() != "Total")
                                {
                                    sum13 = dt3.AsEnumerable().Sum(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                                    sb1.Append("<td style='border:1px solid black;border-right:2px solid black !important;'><b>" + sum13.ToString("0.00") + "</b></td>");
                                }
                                else
                                {
                                    sum13 = dt3.AsEnumerable().Sum(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                                    sb1.Append("<td style='border:1px solid black;border-right:2px solid black !important;'><b>" + (sum13 - minus_Rate_Amount).ToString("0.00") + "</b></td>");
                                }
                               
                                

                            }
                        }
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        dt3.Dispose();
                        minus_Rate_Amount = 0;
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content9.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                      
                    }

                // END of MILK SALE TOWN WISE (LTR / DAY)


                    // start of MILK CONSUMED IN INDIGNEOS PRODUCT MAKING
                    if (ds1.Tables[10].Rows.Count > 0)
                    {
                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<div class='pagebreak'></div>");
                        sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb1.Append("<thead>");
                        sb1.Append("<tr>");
                        sb1.Append("<td colspan='2' style='text-align: center;border:1px solid black;'><b>MILK CONSUMED IN INDIGNEOS PRODUCT MAKING (IN Kgs) </b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: left;padding-left:5px'><b>PARTICULARS</b></td>");
                        sb1.Append("<td style='border:1px solid black;width:10px;text-align: center;padding-left:5px'><b>Quanity	(IN KG)</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        int Count = ds1.Tables[10].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border:1px solid black;text-align: left;'>" + ds1.Tables[10].Rows[i]["Particular"] + "</td>");
                            sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[10].Rows[i]["Quanity_InKG"] + "</td>");
                            sb1.Append("</tr>");
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                        sb1.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds1.Tables[10].AsEnumerable().Sum(r => r.Field<decimal?>("Quanity_InKG") ?? 0)).ToString("0.00") + "</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</table>");
                        ViewState["Sb"] += sb1.ToString();
                        div_page_content9.InnerHtml = sb1.ToString();
                        Print.InnerHtml = ViewState["Sb"].ToString();
                    }

                // END of MILK CONSUMED IN INDIGNEOS PRODUCT MAKING
                  
               
            }
            else
            {
                ViewState["Sb"] = "";
                pnlData.Visible = false;
                btnExportAll.Visible = false;
                div_page_content.InnerHtml = "";
                div_page_content1.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! ", "No Record Found.");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Townwise Milk Sale ", ex.Message.ToString());
            pnlData.Visible = false;
            btnExportAll.Visible = false;
            div_page_content.InnerHtml = "";
            div_page_content1.InnerHtml = "";
            Print.InnerHtml = "";
            ViewState["Sb"] = "";
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename="  + txtEntryDate.Text + "-" + "UnionWiseDailyMIS " + DateTime.Now + ".xls");
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetMISReport();
        }
      
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
     
      
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}