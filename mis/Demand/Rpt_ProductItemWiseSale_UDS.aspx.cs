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

public partial class mis_Demand_Rpt_ProductItemWiseSale_UDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3,ds5, ds7,ds8 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);  
    DataTable dt1, dt2, dt3, dt4, dt5, dt6, dt7=new DataTable();
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
                GetAllData();
            
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void GetAllData()
    {
        
        try
        {
            
            if (dt1 != null) { dt1.Clear(); dt1.Dispose(); }
            if (dt2 != null) { dt2.Clear(); dt2.Dispose(); }
            if (dt3 != null) { dt3.Clear(); dt3.Dispose(); }
            if (dt4 != null) { dt4.Clear(); dt4.Dispose(); }
            if (dt5 != null) { dt5.Clear(); dt5.Dispose(); }
            if (dt6 != null) { dt6.Clear(); dt6.Dispose(); }
            if (dt7 != null) { dt7.Clear(); dt7.Dispose(); }
            Session["GetSection"] = "";
            Session["GetItem"] = "";
            Session["GetSS"] = "";
            ds8 = objdb.ByProcedure("USP_RetailerTypeWiseSaleReport_UDS",
                 new string[] { "Flag", "Office_ID" },
                   new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds8 != null)
            {
                
                if(ds8.Tables[0].Rows.Count>0) // shift master
                {
                    dt1 = ds8.Tables[0];
                    GetShift();
                }
                if (ds8.Tables[1].Rows.Count > 0) // item category
                {
                    dt2 = ds8.Tables[1];
                    GetCategory();
                }
                if (ds8.Tables[2].Rows.Count > 0) // AreaId
                {
                    dt3 = ds8.Tables[2];
                    GetLocation();
                }
                if (ds8.Tables[3].Rows.Count > 0) // Office Details
                {
                    hfvofficename.Value = Convert.ToString(ds8.Tables[3].Rows[0]["Office_Name"]);
                   // officecontaceno = Convert.ToString(ds8.Tables[3].Rows[0]["Office_ContactNo"]);
                   
                }
                if (ds8.Tables[4].Rows.Count > 0) // Section bind
                {
                    dt4 = ds8.Tables[4];
                    Session["GetSection"] = dt4;
                    GetSection();
                   
                }
                if (ds8.Tables[5].Rows.Count > 0) // Item Bind
                {
                    dt5 = ds8.Tables[5];
                    Session["GetItem"] = dt5;
                    GetItem();
                }

                if (ds8.Tables[6].Rows.Count > 0) // GetRetailer Type bind
                {
                    dt6 = ds8.Tables[6];
                    GetRetailerType();
                }
                if (ds8.Tables[7].Rows.Count > 0) // Get SS Details
                {
                    dt7 = ds8.Tables[7];
                    Session["GetSS"] = dt7;
                    GetSS();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Bind All Data", ex.Message.ToString());
        }
        finally
        {
            if (ds8 != null) { ds8.Dispose(); }
        }
    }
   
    protected void GetShift()
    {
        try
        {
            ddlShift.DataTextField = "ShiftName";
            ddlShift.DataValueField = "Shift_id";
            ddlShift.DataSource = dt1;
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
            ddlItemCategory.DataSource = dt2;
            ddlItemCategory.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ITemCategory ", ex.Message.ToString());
        }
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = dt3;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
   
    protected void GetSection()
    {
        try
        {
            DataTable dt4 = (DataTable)Session["GetSection"];           
            DataView dataView = dt4.DefaultView;
            ddlSection.Items.Clear();
            if (dt4.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ddlItemCategory.SelectedValue))
                {
                    dataView.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "'";
                }
                ddlSection.DataTextField = "SectionName";
                ddlSection.DataValueField = "MOrPSection_id";
                ddlSection.DataSource = dataView;
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlSection.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

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
            DataTable dt5 = (DataTable)Session["GetItem"];
            DataView dataView1 = dt5.DefaultView;
            if (dt5.Rows.Count > 0)
            {
                if (ddlItemCategory.SelectedValue != "0" && ddlSection.SelectedValue != "0")
                {
                    dataView1.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "' and MOrPSection_id='" + ddlSection.SelectedValue + "'";
                }
                else if (ddlItemCategory.SelectedValue != "0")
                {
                    dataView1.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "'";
                }


                ddlItemName.Items.Clear();
                ddlItemName.DataSource = dataView1;
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataBind();
            }
            else
            {
                ddlItemName.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
                  
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Item 1st ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    protected void GetRetailerType()
    {
        try
        {
            ddlRetailerType.DataTextField = "RetailerTypeName";
            ddlRetailerType.DataValueField = "RetailerTypeID";
            ddlRetailerType.DataSource = dt6;
            ddlRetailerType.DataBind();
            ddlRetailerType.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error RetailerType ", ex.Message.ToString());
        }
    }
    private void GetSS()
    {
        try
        {
            DataTable dt7 = (DataTable)Session["GetSS"];
            DataView dataView2 = dt7.DefaultView;
            if(dt7.Rows.Count>0)
            {
            
            if(ddlLocation.SelectedValue!="0" && ddlItemCategory.SelectedValue!="0" && ddlRetailerType.SelectedValue!="0")
            {
                dataView2.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "' and AreaId='" + ddlLocation.SelectedValue + "'and RetailerTypeID='" + ddlRetailerType.SelectedValue + "'";
            }            
            else if(ddlLocation.SelectedValue!="0" && ddlItemCategory.SelectedValue!="0")
            {
                dataView2.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "' and AreaId='" + ddlLocation.SelectedValue + "'";
            }
            else if (ddlLocation.SelectedValue == "0" && ddlRetailerType.SelectedValue != "0")
            {
                dataView2.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "' and RetailerTypeID = '" + ddlRetailerType.SelectedValue + "'";
            }
            else if (ddlRetailerType.SelectedValue != "0")
            {
                dataView2.RowFilter = "RetailerTypeID = '" + ddlRetailerType.SelectedValue + "'";
            }
            else if (ddlItemCategory.SelectedValue != "0")
            {
                dataView2.RowFilter = "ItemCat_id = '" + ddlItemCategory.SelectedValue + "'";
            }
            ddlSuperStockist.Items.Clear();
            if (dataView2.Count > 0)
            {
                ddlSuperStockist.DataTextField = "SSName";
                ddlSuperStockist.DataValueField = "SuperStockistId";
                ddlSuperStockist.DataSource = dataView2;
                ddlSuperStockist.DataBind();
            }
            }
            else
            {
                ddlSuperStockist.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    //protected void GetOfficeDetails()
    //{
    //    try
    //    {
    //        ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
    //        if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
    //        {
    //            ViewState["Office_Name"] = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
    //            ViewState["Office_ContactNo"] = ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds3 != null)
    //        {
    //            ds3.Dispose();
    //        }
    //    }
    //}
  
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetSS();
        
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSection();
        GetItem();
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
                if(ddlItemCategory.SelectedValue==objdb.GetProductCatId())
                {
                    SSProductSummaryRpt();
                }
                else
                {
                    SSMilkSectionVariantwiseRpt();
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
    private void SSProductSummaryRpt()
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

            ds5 = objdb.ByProcedure("USP_RetailerTypeWiseSaleReport_UDS",
                     new string[] { "Flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "Item_id_Str", "SuperStockistId_Str", "Office_ID" },
                       new string[] { "2", fromdat, todat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, itemid.ToString(), SSid.ToString(), objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                StringBuilder sb1 = new StringBuilder();

                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: center;'colspan='3'><b>" + hfvofficename.Value + "</b></td>");
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

    protected void ddlRetailerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSS();
    }
    private void SSMilkSectionVariantwiseRpt()
    {
        try
        {

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

            ds5 = objdb.ByProcedure("USP_RetailerTypeWiseSaleReport_UDS",
                     new string[] { "Flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "Item_id_Str", "SuperStockistId_Str", "Office_ID" },
                       new string[] { "3", fromdat, todat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, itemid.ToString(), SSid.ToString(), objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = string.Empty;
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                int Count = ds5.Tables[0].Rows.Count;
                int ColCount = ds5.Tables[0].Columns.Count;
                StringBuilder sb1 = new StringBuilder();

            
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align:center' colspan='2'><b>" + hfvofficename.Value + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: lefg;'><b>Section  :-" + ddlSection.SelectedItem.Text + "</b></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Period  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                sb1.Append("<thead>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>SS Name</b></td>");
                for (int j = 0; j < ColCount; j++)
                {

                    if (ds5.Tables[0].Columns[j].ToString() != "SuperStockistId" && ds5.Tables[0].Columns[j].ToString() != "SSNam")
                    {
                        string ColName = ds5.Tables[0].Columns[j].ToString();
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb1.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["SSNam"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds5.Tables[0].Columns[K].ToString() != "SuperStockistId" && ds5.Tables[0].Columns[K].ToString() != "SSNam")
                        {
                            string ColName = ds5.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");

                        }

                    }
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='2'><b>Total</b></td>");
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];
                int sum11 = 0;
                decimal dsum11 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "SuperStockistId" && column.ToString() != "SSNam")
                    {
                        if (column.ToString() == "In Litre" || column.ToString() == "Amount" || column.ToString() == "TCS TAx %" || column.ToString() == "TcsTax Amt" || column.ToString() =="Total Amount")
                        {
                            dsum11 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + dsum11.ToString() + "</b></td>");
                        }
                        else
                        {
                            sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");
                        }


                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
                if (dt != null) { dt.Dispose(); }

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
}