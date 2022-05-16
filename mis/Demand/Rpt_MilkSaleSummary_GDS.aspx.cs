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


public partial class mis_Demand_Rpt_MilkSaleSummary_GDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5, ds7 = new DataSet();
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
                GetItem();

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

           int Fmonth = fdate.Month;
            int Tmonth = tdate.Month;
            if ((fdate <= tdate) && (Fmonth == Tmonth))
            {
                lblMsg.Text = string.Empty;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                GetPlantWiseReport();
            }
            else
            {
                Fmonth = 0;
                Tmonth = 0;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate and Month must be same(ex : 01/01/2021 - 31/01/2021).");
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
    private void GetItem()
    {
        try
        {
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "Item_id";
            ddlItemName.DataSource = objdb.ByProcedure("USP_Trn_MilkSupplySummary_GDS",
                 new string[] { "Flag", "ItemCat_id", "Office_ID" },
                   new string[] { "2",objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    private void GetPlantWiseReport()
    {
        try
        {
            int fdays = 0, tdays = 0;
            DateTime fmonth = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime tmonth = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            DateTime enddate = tmonth.AddDays(1);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            TimeSpan ts = (enddate - fmonth);

            int differenceInDays = ts.Days; // This is in int

            fdays = fmonth.Day;
            tdays = tmonth.Day;

            ds5 = objdb.ByProcedure("USP_Trn_MilkSupplySummary_GDS",
                     new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "Item_id", "Office_ID", "DifferenceInDays", "fdays", "tdays" },
                       new string[] { "1", fromnonth, tomonth,objdb.GetMilkCatId(),ddlItemName.SelectedValue, objdb.Office_ID(), differenceInDays.ToString(), fdays.ToString(), tdays.ToString() }, "dataset");

            if (ds5.Tables[0].Rows.Count > 0)
            {


                pnlData.Visible = true;
                btnExportAll.Visible = true;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: left;border:1px solid black;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6'style='text-align: center;border:1px solid black;'><b>Milk Sale Summary Report</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Advance</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Credit</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Cash</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                sb.Append("</tr>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["SDate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["AdvCardQtyInLtr"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["CreditQtyInLtr"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["CashQtyInLtr"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["TotalQtyInLtr"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='border:1px solid black;border:1px solid black;text-align:right;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("AdvCardQtyInLtr") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("CreditQtyInLtr") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("CashQtyInLtr") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalQtyInLtr") ?? 0)) + "</b></td>");
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
                div_page_content.Visible = false;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found");
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
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + "MilkSaleSummary.xls");
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