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
public partial class mis_Demand_Rpt_ProductItemWiseSale_WithinvoiceNo_IUB : System.Web.UI.Page
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
                GetLocation();
                GetSS();
                GetOfficeDetails();
                GetSection();
                GetItem();
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
    protected void GetSection()
    {
        try
        {
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataValueField = "MOrPSection_id";
            ddlSection.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSectionName",
                 new string[] { "Flag", "Office_ID", "ItemCat_id" },
                   new string[] { "5", objdb.Office_ID(), objdb.GetProductCatId() }, "dataset");
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetItem()
    {
        try
        {

            ddlItemName.Items.Clear();
            ds3 = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                                new string[] { "Flag", "Office_ID", "MOrPSection_id", "ItemCat_id" },
                                new string[] { "6", objdb.Office_ID(), ddlSection.SelectedValue, objdb.GetProductCatId() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlItemName.DataSource = ds3.Tables[0];
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
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
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
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
                  new string[] { "8", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
            ddlSuperStockist.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlSuperStockist.DataTextField = "SSName";
                ddlSuperStockist.DataValueField = "SuperStockistId";
                ddlSuperStockist.DataSource = ds7.Tables[0];
                ddlSuperStockist.DataBind();
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetSS();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
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

                DistributorProductSummaryRpt();
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
    private void DistributorProductSummaryRpt()
    {
        try
        {
            lblMsg.Text = string.Empty;
            div_page_content.InnerHtml = "";
            Print.InnerHtml = "";
            string SSid = "";
            int sddata = 0;
            ViewState["PStatus"] = "";
            ViewState["SuperData"] = "";
            string itemid = "";
            int iddata = 0;
            foreach (ListItem item in ddlItemName.Items)
            {
                if (item.Selected)
                {
                    ++iddata;
                    if (iddata == 1)
                    {
                        itemid = item.Value;
                    }
                    else
                    {
                        itemid += "," + item.Value;
                    }


                }
            }
            foreach (ListItem item in ddlSuperStockist.Items)
            {
                if (item.Selected)
                {
                    ++sddata;
                    if (sddata == 1)
                    {
                        SSid = item.Value;
                    }
                    else
                    {
                        SSid += "," + item.Value;
                    }


                }
            }
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "Item_id_Str", "SuperStockistId_Str", "Office_ID" },
                       new string[] { "22", fromdat, todat, ddlShift.SelectedValue, objdb.GetProductCatId(), itemid.ToString(), SSid.ToString(), objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                StringBuilder sb1 = new StringBuilder();

                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: center;'colspan='3'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: right;'colspan='2'><b>PRODUCT ITEM WISE SALE REPORT</b></td>");
                sb1.Append("<td style='text-align: right;'><b>Period  :-</b>" + txtFromDate.Text + " To " + txtToDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb1.Append("<thead>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Party Name</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Date</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Invoice No.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>PRODUCT NAME</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>QUANTITY</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>In KG/LTR.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>AMOUNT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>CGST AMT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>SGST AMT</b></td>");
                //sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCS Sales on @ " + ds5.Tables[0].Rows[0]["TcsTaxPer"] + "</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCS TAX AMOUNT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>FINAL AMOUNT</b></td>");

                sb1.Append("</thead>");
                string ssname = "";
                for (int i = 0; i < Count1; i++)
                {

                    sb1.Append("<tr>");
                    if (i == 0)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["SSName"] + " - " + ds5.Tables[0].Rows[i]["SSCode"] + "</td>");
                    }
                    else
                    {
                        if (ssname == ds5.Tables[0].Rows[i]["SSName"].ToString())
                        {
                            sb1.Append("<td style='border-top:1px dashed black;'></td>");
                        }
                        else
                        {
                            sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["SSName"] + " - " + ds5.Tables[0].Rows[i]["SSCode"] + "</td>");
                        }
                    }
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["Delivary_Date"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ProductName"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["SupplyQty"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["QtyInKGLtR"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + ds5.Tables[0].Rows[i]["AmountWithoutGST"] + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["CGSTAmt"]).ToString("0.00") + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["SGSTAmt"]).ToString("0.00") + "</td>");

                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0.00") + "</td>");
                    sb1.Append("<td style='border-top:1px dashed black;text-align: center;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["GrossValue"]).ToString("0.00") + "</td>");
                    sb1.Append("</tr>");

                    ssname = ds5.Tables[0].Rows[i]["SSName"].ToString();

                }
                sb1.Append("<tr>");
                sb1.Append("<td colspan='4' style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
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
                Print.InnerHtml = sb1.ToString();

            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
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
        ViewState["ItemWiseSaleReport"] = "";
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;

    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
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