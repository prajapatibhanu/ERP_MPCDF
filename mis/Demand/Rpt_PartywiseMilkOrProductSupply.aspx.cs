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

public partial class mis_Demand_Rpt_PartywiseMilkOrProductSupply : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5, ds7 = new DataSet();
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
                GetShift();
                GetCategory();
                GetLocation();
                GetSS();
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
    protected void GetShift()
    {
        try
        {
            ddlShift.DataTextField = "ShiftName";
            ddlShift.DataValueField = "Shift_id";
            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    protected void GetCategory()
    {
        try
        {

            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetSS()
    {
        try
        {
            ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "8", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlSuperStockist.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlSuperStockist.DataTextField = "SSName";
                ddlSuperStockist.DataValueField = "SuperStockistId";
                ddlSuperStockist.DataSource = ds7.Tables[0];
                ddlSuperStockist.DataBind();
                ddlSuperStockist.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSuperStockist.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds7 != null) { ds7.Dispose(); }
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetSS();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetSS();
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
                ViewState["SummaryProduct"] = "";
                if(ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
                {

                    DistributorMilkSummaryRpt();
                    
                }
                else
                {
                    DistributorProductSummaryRpt();
                }
                
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
    private void DistributorMilkSummaryRpt()
    {
        try
        {
            string distributorId = "";
            //if (ddlDitributor.SelectedValue == "0")
            //{
            //    distributorId = "0";
            //}
            //else
            //{
            //    string[] SSRDId = ddlDitributor.SelectedValue.Split('-');
            //    distributorId = SSRDId[2];
            //}

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "AreaId", "SuperStockistId", "Office_ID" },
                       new string[] { "16", fromdat, todat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlLocation.SelectedValue, ddlSuperStockist.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<div class='pagebreak'></div>");
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: center;'colspan='3'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: right;'colspan='2'><b>MILK SUPPLY SUMMARY</b></td>");
                sb1.Append("<td style='text-align: right;'><b>Period  :-</b>" + txtFromDate.Text + " To " + txtToDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;'><b>Party Code:- </b>" + ds5.Tables[0].Rows[0]["SSCode"] + "</td>");
                sb1.Append("<td style='padding: 2px 5px;'><b>Party Name:- </b>" + ds5.Tables[0].Rows[0]["SSName"] + "</td>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb1.Append("<thead>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>S.No.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>PRODUCT NAME</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>CASH QTY(In Pkt)</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>ADV CARD QTY(In Pkt)</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TOTAL QTY(In Pkt)</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TOTAL QTY(In Ltr.)</b></td>");

                sb1.Append("</thead>");
                for (int i = 0; i < Count1; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: left;'>" + (i + 1).ToString() + "</b></td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: left;'>" + ds5.Tables[0].Rows[i]["ProductName"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: left;'>" + ds5.Tables[0].Rows[i]["CashQty"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: left;'>" + ds5.Tables[0].Rows[i]["AdvCardQty"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: left;'>" + ds5.Tables[0].Rows[i]["TotalQty"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: left;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TotalQtyInLtr"]).ToString("0.00") + "</td>");
                    sb1.Append("</tr>");
                }
                sb1.Append("<tr>");
                sb1.Append("<td colspan='2' style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:left;'><b>Grand Total</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: left;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("CashQty") ?? 0)) + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: left;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("AdvCardQty") ?? 0)) + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: left;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("TotalQty") ?? 0)) + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: left;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalQtyInLtr") ?? 0)).ToString("0.00") + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                ViewState["SummaryProduct"] = sb1.ToString();
            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["SummaryProduct"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
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
    private void DistributorProductSummaryRpt()
    {
        try
        {
           // string distributorId = "";
            //if (ddlDitributor.SelectedValue == "0")
            //{
            //    distributorId = "0";
            //}
            //else
            //{
            //    string[] SSRDId = ddlDitributor.SelectedValue.Split('-');
            //    distributorId = SSRDId[2];
            //}

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "AreaId", "SuperStockistId", "Office_ID" },
                       new string[] { "18", fromdat, todat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlLocation.SelectedValue, ddlSuperStockist.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<div class='pagebreak'></div>");
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: center;'colspan='3'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: right;'colspan='2'><b>PRODUCT SUPPLY SUMMARY</b></td>");
                sb1.Append("<td style='text-align: right;'><b>Period  :-</b>" + txtFromDate.Text + " To " + txtToDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;'><b>Party Code:- </b>" + ds5.Tables[0].Rows[0]["SSCode"] + "</td>");
                sb1.Append("<td style='padding: 2px 5px;'><b>Party Name:- </b>" + ds5.Tables[0].Rows[0]["SSName"] + "</td>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb1.Append("<thead>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>S.No.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>PRODUCT NAME</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>QUANTITY</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In KG/LTR.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>AMOUNT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>CGST AMT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>SGST AMT</b></td>");
               // sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCS Sales on @ " + ds5.Tables[0].Rows[0]["TcsTaxPer"] + "</b></td>");
			   sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCS TAX AMT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>FINAL AMOUNT</b></td>");

                sb1.Append("</thead>");
                for (int i = 0; i < Count1; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</b></td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ProductName"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["SupplyQty"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["QtyInKGLtR"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + ds5.Tables[0].Rows[i]["AmountWithoutGST"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["CGSTAmt"]).ToString("0.00") + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["SGSTAmt"]).ToString("0.00") + "</td>");

                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0.00") + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["GrossValue"]).ToString("0.00") + "</td>");
                    sb1.Append("</tr>");
                }
                sb1.Append("<tr>");
                sb1.Append("<td colspan='2' style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: left;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("SupplyQty") ?? 0)) + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("AmountWithoutGST") ?? 0)).ToString("0.00") + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("CGSTAmt") ?? 0)).ToString("0.00") + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("SGSTAmt") ?? 0)).ToString("0.00") + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TCSTaxAmt") ?? 0)).ToString("0.00") + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("GrossValue") ?? 0)).ToString("0.00") + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                ViewState["SummaryProduct"] = sb1.ToString();
            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["SummaryProduct"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
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
        ViewState["SummaryProduct"] = "";
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
       
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        Print.InnerHtml = ViewState["SummaryProduct"].ToString();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "Location-" + ddlLocation.SelectedItem.Text + DateTime.Now + ".xls");
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