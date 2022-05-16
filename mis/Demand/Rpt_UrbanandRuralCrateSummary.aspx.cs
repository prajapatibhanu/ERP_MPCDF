using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_UrbanandRuralCrateSummary : System.Web.UI.Page
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
                
                GetOfficeDetails();
                txtMonth.Attributes.Add("readonly", "readonly");
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
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    
    
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtMonth.Text = string.Empty;
        lblMsg.Text = "";
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";

    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateReport();
        }
    }

    protected void GenerateReport()
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            div_page_content.InnerHtml = "";
            string fm = "01/" + txtMonth.Text;

            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt"
                // , new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "Office_ID" }
                // , new string[] { "11", fromnonth, tomonth,objdb.GetMilkCatId(), objdb.Office_ID(), }, "dataset");
				ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt_BDS_urben"
                , new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "Office_ID" }
                , new string[] { "11", fromnonth, tomonth,objdb.GetMilkCatId(), objdb.Office_ID(), }, "dataset");
				
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    int Total_Issue_Urban = 0, Total_Return_Urban = 0, Total_SE_Urban = 0;
                    int Total_Issue_Rural = 0, Total_Return_Rural = 0, Total_SE_Rural = 0;
                    int Total_Issue_UR = 0, Total_Return_UR = 0, Total_SE_UR = 0;
                    int Total_Issue_MPCDF = 0, Total_Return_MPCDF = 0, Total_SE_MPCDF = 0;

                    int Count = ds2.Tables[0].Rows.Count;
                    sb.Append("<table class='table'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='11'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='3'><b>" + fmonth.ToString("MMM-yyyy") + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='8'><b>All Route Total</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='5'><b>Urban Route Shift M/E Total Crate Account</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='3'><b>Rural Marketing Shift M/E Total Crate Account</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='3'><b>Urban & Rural Crate Summary</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Date</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Shift</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Issue</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Return</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Short/Excess</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Issue</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Return</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Short/Excess</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Issue</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Return</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Short/Excess</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    string Date = "";
                    for (int i = 0; i < Count; i++)
                    {
                        
                        sb.Append("<tr>");
                        if (Date == ds2.Tables[0].Rows[i]["Date"].ToString())
                        {
                        }
                        else
                        {
                            sb.Append("<td rowspan='2' style='border:1px solid black;text-align: center;padding-top:20px;'>" + ds2.Tables[0].Rows[i]["Date"] + "</td>");
                        }
                       
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ShiftName"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["IssueCrate_Urban"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReturnCrate_Urban"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ShortExcessCrate_Urban"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["IssueCrate_Rural"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReturnCrate_Rural"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ShortExcessCrate_Rural"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["IssueCrate_Urban+Rural"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReturnCrate_Urban+Rural"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ShortExcessCrate_Urban+Rural"] + "</td>");
                        sb.Append("</tr>");

                        //urban
                        Total_Issue_Urban += Convert.ToInt32(ds2.Tables[0].Rows[i]["IssueCrate_Urban"]);
                        Total_Return_Urban += Convert.ToInt32(ds2.Tables[0].Rows[i]["ReturnCrate_Urban"]);
                        Total_SE_Urban += Convert.ToInt32(ds2.Tables[0].Rows[i]["ShortExcessCrate_Urban"]);

                        //rural
                        Total_Issue_Rural += Convert.ToInt32(ds2.Tables[0].Rows[i]["IssueCrate_Rural"]);
                        Total_Return_Rural += Convert.ToInt32(ds2.Tables[0].Rows[i]["ReturnCrate_Rural"]);
                        Total_SE_Rural += Convert.ToInt32(ds2.Tables[0].Rows[i]["ShortExcessCrate_Rural"]);

                        //urban + rural
                        Total_Issue_UR += Convert.ToInt32(ds2.Tables[0].Rows[i]["IssueCrate_Urban+Rural"]);
                        Total_Return_UR += Convert.ToInt32(ds2.Tables[0].Rows[i]["ReturnCrate_Urban+Rural"]);
                        Total_SE_UR += Convert.ToInt32(ds2.Tables[0].Rows[i]["ShortExcessCrate_Urban+Rural"]);


                        Date = ds2.Tables[0].Rows[i]["Date"].ToString();
                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='2'><b>Total</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Issue_Urban + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Return_Urban + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SE_Urban + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Issue_Rural + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Return_Rural + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SE_Rural + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Issue_UR + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Return_UR + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SE_UR + "</b></td>");
                    sb.Append("</tr>");
                    
                    if(objdb.Office_ID()=="2")
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>MPCDF</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>DM</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[1].Rows[0]["IssueCrate_MPDCF"] + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[1].Rows[0]["ReturnCrate_MPDCF"] + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[1].Rows[0]["ShortExcessCrate_MPDCF"] + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[1].Rows[0]["IssueCrate_MPDCF"] + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[1].Rows[0]["ReturnCrate_MPDCF"] + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[1].Rows[0]["ShortExcessCrate_MPDCF"] + "</b></td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='border:1px solid black;text-align: center;' colspan='2'><b>Grand Total</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (Total_Issue_Urban + Convert.ToInt32(ds2.Tables[1].Rows[0]["IssueCrate_MPDCF"])) + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (Total_Return_Urban + Convert.ToInt32(ds2.Tables[1].Rows[0]["ReturnCrate_MPDCF"])) + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (Total_SE_Urban + Convert.ToInt32(ds2.Tables[1].Rows[0]["ShortExcessCrate_MPDCF"])) + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Issue_Rural + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Return_Rural + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SE_Rural + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (Total_Issue_UR + Convert.ToInt32(ds2.Tables[1].Rows[0]["IssueCrate_MPDCF"])) + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (Total_Return_UR + Convert.ToInt32(ds2.Tables[1].Rows[0]["ReturnCrate_MPDCF"])) + "</b></td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (Total_SE_UR + Convert.ToInt32(ds2.Tables[1].Rows[0]["ShortExcessCrate_MPDCF"])) + "</b></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    div_page_content.InnerHtml = sb.ToString();
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CumulativeCrateSummary" + DateTime.Now + ".xls");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}