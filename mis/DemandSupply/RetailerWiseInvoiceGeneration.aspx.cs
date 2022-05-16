using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_RetailerWiseInvoiceGeneration : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6,ds3 = new DataSet();



    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null &&  objdb.GetItemCat_id()!=null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetCategory();
                GetRouteIDByDistributor();
                GetRetailerDetails();
                GetOfficeDetails();
                ViewState["RetailerData"] = "";
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

    private void GetRouteIDByDistributor()
    {
        try
        {
            // ds3 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     // new string[] { "flag", "Office_ID", "DistributorId" },
                       // new string[] { "4", objdb.Office_ID(), objdb.createdBy() }, "dataset");
					   
					   ds3 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "DistributorId", "ItemCat_id" },
                       new string[] { "4", objdb.Office_ID(), objdb.createdBy(),ddlItemCategory.SelectedValue }, "dataset");

            if (ds3.Tables[0].Rows.Count != 0)
            {
                ViewState["RouteId"] = ds3.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
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
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_GST"] = ds2.Tables[0].Rows[0]["Office_Gst"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
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
            if (objdb.GetItemCat_id() == objdb.GetProductCatId())
            {
                ddlShift.SelectedValue = objdb.GetShiftMorId();
                ddlShift.Enabled = false;
            }
            else
            {
                ddlShift.Enabled = true;
                ddlShift.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }



    private void GetRetailerDetails()
    {
        try
        {
            if (ViewState["RouteId"].ToString() != "" && ViewState["RouteId"] != null && ViewState["RouteId"].ToString()!="0")
            {
                ddlRetailer.DataTextField = "BoothName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                     new string[] { "Flag", "Office_ID", "RouteId", "ItemCat_id" },
                       new string[] { "18", objdb.Office_ID(), ViewState["RouteId"].ToString(),ddlItemCategory.SelectedValue }, "dataset");
                ddlRetailer.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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
            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            ddlItemCategory.SelectedValue = objdb.GetItemCat_id();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }

    #endregion========================================================
    #region=========== init or changed even===========================



    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ViewState["RetailerData"] = "";
            FillRetailerDetails();

        }
    }

    private void FillRetailerDetails()
    {
        try
        {
            if (ViewState["RouteId"].ToString() != "" && ViewState["RouteId"] != null && ViewState["RouteId"].ToString() != "0")
            {
                lblMsg.Text = string.Empty;
                div_page_content.InnerHtml = "";
                string distid = "";
                ViewState["PStatus"] = "";
                foreach (ListItem item in ddlRetailer.Items)
                {
                    if (item.Selected)
                    {
                        distid = item.Value;
                        GetInvoiceDetails(Convert.ToInt32(distid));
                    }
                }
                if (ViewState["RetailerData"].ToString() != "" && ViewState["RetailerData"].ToString() != null && ViewState["RetailerData"].ToString() != "0")
                {
                    btnPrint.Visible = true;
                    div_page_content.InnerHtml = ViewState["RetailerData"].ToString();
                    ViewState["RetailerData"] = "";
                }
                else
                {
                    btnPrint.Visible = false;
                    div_page_content.InnerHtml = "";
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    private void GetInvoiceDetails(int BoothId)
    {
        try
        {
           
            DateTime DeliveryDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string DeliveryDat = DeliveryDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                         new string[] { "Flag","Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "BoothId", "OrganizationId" },
                           new string[] { "5", DeliveryDat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ViewState["RouteId"].ToString(), BoothId.ToString(), "0" }, "dataset");
            string pstatus = "";
            if (ds6.Tables[0].Rows.Count > 0 && ds6.Tables[0].Rows[0]["Msg"].ToString() == "Found")
            {
                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0, paybleAmt = 0, toalAdvCrdAmt = 0;

                if (ViewState["PStatus"].ToString() != "")
                {
                    sb.Append("<p style='page-break-after: always'>");
                }

                sb.Append("<div class='content' style='border: 1px solid black'>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td class='text-left'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left:50px;'><b>Bill Book</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>No" + txtDate.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>M/s  :-" + ds6.Tables[0].Rows[0]["BName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + txtDate.Text  + "</td>");
                sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>Particulars</td>");
                sb.Append("<td>Qty(In Pkt)</td>");
                sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty (In Ltr.)</td>");
                sb.Append("<td>Adv. Card Margin</td>");
                sb.Append("<td>Adv. Card Amount</td>");
                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr.)</td>");
                sb.Append("<td>Amount</td>");
                sb.Append("<td>Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"])) + "</td>");
                    sb.Append("</tr>");

                    toalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
                    paybleAmt += ((Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"])));
                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count;

                
                for (int i = 0; i < ColumnCount - 12; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td><b>Total</b></td>");
                    }
                    else if (i == ColumnCount - 18)
                    {
                        sb.Append("<td><b>" + toalAdvCrdAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 14)
                    {
                        sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 13)
                    {
                        sb.Append("<td><b>" + paybleAmt.ToString("0.000") + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                sb.Append("<td style='padding-left:270px;'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");

                ViewState["RetailerData"] += sb.ToString();
                ViewState["PStatus"] = "1";
                ////////////////End Of Route Wise Print Code   ///////////////////////
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }

    }
    #endregion===========================
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetRetailerDetails();
    //}
}