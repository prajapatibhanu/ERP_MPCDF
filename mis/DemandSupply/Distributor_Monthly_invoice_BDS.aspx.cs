using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Distributor_Monthly_invoice_BDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();



    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtMonth.Attributes.Add("readonly", "readonly");
                //txtFromDate.Text = Date;
                //txtFromDate.Attributes.Add("readonly", "true");
                //txtToDate.Text = Date;
                //txtToDate.Attributes.Add("readonly", "true");
                //GetLocation();
               // GetShift();
                
                GetOfficeDetails();
                GetLocation();
               // GetDisOrSS();
                ViewState["DistData"] = "";
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

    //protected void GetLocation()
    //{
    //    try
    //    {
    //        ddlLocation.DataTextField = "AreaName";
    //        ddlLocation.DataValueField = "AreaId";
    //        ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
    //             new string[] { "flag" },
    //               new string[] { "1" }, "dataset");
    //        ddlLocation.DataBind();
    //        ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //}
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
    //protected void GetShift()
    //{
    //    try
    //    {


    //        ddlShift.DataTextField = "ShiftName";
    //        ddlShift.DataValueField = "Shift_id";
    //        ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
    //             new string[] { "flag" },
    //               new string[] { "1" }, "dataset");
    //        ddlShift.DataBind();
    //        ddlShift.Items.Insert(0, new ListItem("All", "0"));


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
    //    }
    //}
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetDisOrSS();
            //GetRoute();
        }
    }
    private void GetDisOrSS()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            //ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
            //     new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
            //       new string[] { "8", "0", objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "8", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlDitributor.DataBind();
            ddlDitributor.Items.Insert(0, new ListItem("--Select Distributor--", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    //private void GetDisOrSS()
  

    #endregion========================================================
    #region=========== init or changed even===========================



    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ViewState["DistData"] = "";
           // GetCompareDate();
            if (ddlDitributor.SelectedIndex > 0)
            {
                FillDistributorDetails();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Please select Distributor.");
            }

        }
    }

    private void FillDistributorDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            div_page_content.InnerHtml = "";
            string distid = "";
            ViewState["PStatus"] = "";
            distid = ddlDitributor.SelectedValue;
            GetTcsTax(distid);
            GetInvoiceDetails(Convert.ToInt32(distid));
            //foreach (ListItem item in ddlDitributor.Items)
            //{
            //    if (item.Selected)
            //    {
            //        distid = item.Value;
            //        GetTcsTax(distid);
            //        GetInvoiceDetails(Convert.ToInt32(distid));
            //    }
            //}
            if (ViewState["DistData"].ToString() != "" && ViewState["DistData"].ToString() != null && ViewState["DistData"].ToString() != "0")
            {
                btnPrint.Visible = true;
                div_page_content.InnerHtml = ViewState["DistData"].ToString();
                ViewState["DistData"] = "";
            }
            else
            {
                btnPrint.Visible = false;
                div_page_content.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                return;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    // private void GetInvoiceDetails(int distid)
    // {
        // try
        // {
            // string fm = "01/" + txtMonth.Text;
             // DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            // string MDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            // ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductMonthlyInvoiceDetail"
                // , new string[] { "flag", "FromDate", "DistributorId", "ItemCat_id", "Office_ID" }
                // , new string[] { "1", MDate, ddlDitributor.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), }, "dataset");
            // string pstatus = "";
            // if (ds6.Tables.Count > 0)
            // {
                // if (ds6.Tables[0].Rows.Count > 0 )
                // {

                    // StringBuilder sb = new StringBuilder();
                    // decimal totalamt = 0, toalCashAmt = 0, toalAdvCrdAmt = 0, paybleAmt = 0, tcstamt = 0, fpaybleamt = 0, tcstadvanceamt = 0, tcstCashamt = 0, fadvanceamt = 0, fCashamt = 0;

                    // if (ViewState["PStatus"].ToString() != "")
                    // {
                        // sb.Append("<p style='page-break-after: always'>");
                    // }

                    // sb.Append("<div class='content' style='border: 1px solid black'>");
                    // sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    // sb.Append("<tr>");
                    // sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    // sb.Append("<td class='text-left'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td style='padding-left:50px;'><b>Monthly Bill</b></td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td class='text-left' colspan='2'>" + ddlDitributor.SelectedItem.Text + "</td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td class='text-left' colspan='2'>Month :-" + txtMonth.Text + "</td>");
                    // sb.Append("</tr>");


                    // sb.Append("<tr>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("</tr>");

                    // sb.Append("</table>");
                    // sb.Append("<table class='table table1-bordered'>");
                    // int Count = ds6.Tables[0].Rows.Count;
                    // int ColCount = ds6.Tables[0].Columns.Count;
                    // sb.Append("<thead>");
                    // sb.Append("<td>Particulars</td>");
                    // sb.Append("<td>Cash Qty(In Pkt)</td>");
                    // sb.Append("<td>Cash Qty(In Ltr)</td>");
                    // sb.Append("<td>Cash Rate(In Ltr)</td>");
                    // sb.Append("<td>Cash Amount (In Pkt.)</td>");
                    // sb.Append("<td>Adv. Card Qty(In Pkt)</td>");
                    // sb.Append("<td>Adv. Card Qty(In Ltr)</td>");
                    // sb.Append("<td>Adv. Card Rate(In Ltr)</td>");
                    // sb.Append("<td>Adv. Card Amount (In Pkt.)</td>");
                    // sb.Append("<td>Total Amount</td>");
                    // sb.Append("</thead>");

                    // for (int i = 0; i < Count; i++)
                    // {
                        // if (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashQty"]) > 0)
                        // {
                            // sb.Append("<tr>");
                            // sb.Append("<td>" + ds6.Tables[0].Rows[i]["ItemName"] + "</td>");
                            // sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[i]["CashQty"] + "</td>");
                            // sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashQtyInLtr"]).ToString("0.000") + "</td>");
                            // sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]).ToString("0.000") + "</td>");
                            // sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]).ToString("0.000") + "</td>");
                            // sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[i]["AdvCardQty"] + "</td>");
                            // sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardQtyInLtr"]).ToString("0.000") + "</td>");
                            // sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardRateperLtr"]).ToString("0.000") + "</td>");
                            // sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"]).ToString("0.000") + "</td>");

                            // sb.Append("<td class='text-right'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]) + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"])).ToString("0.000") + "</td>");

                            // sb.Append("</tr>");

                            // toalCashAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]);
                            // toalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"]);
                            // //totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
                            // paybleAmt += ((Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"])) + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"])));
                        // }
                    // }
                    // sb.Append("<tr>");
                    // int ColumnCount = ds6.Tables[0].Columns.Count;

                    // //for (int i =0; i < ColumnCount-9; i++) // privious
                    // for (int i = 0; i < ColumnCount; i++)
                    // {
                        // if (i == 0)
                        // {
                            // sb.Append("<td><b>Total</b></td>");
                        // }
                        // else if (i == 4)
                        // {
                            // sb.Append("<td class='text-right'><b>" + toalCashAmt.ToString("0.000") + "</b></td>");
                        // }
                        // //else if (i == ColumnCount-10)
                        // else if (i == 8)
                        // {
                            // sb.Append("<td class='text-right'><b>" + toalAdvCrdAmt.ToString("0.000") + "</b></td>");
                        // }
                        // // else if (i == ColumnCount-10)
                        // else if (i == 9)
                        // {
                            // sb.Append("<td class='text-right'><b>" + (toalCashAmt + toalAdvCrdAmt).ToString("0.000") + "</b></td>");
                        // }
                        // else
                        // {
                            // sb.Append("<td></td>");
                        // }



                    // }
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                    // tcstadvanceamt = ((toalAdvCrdAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                    // tcstCashamt = ((toalCashAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                    // fpaybleamt = paybleAmt + tcstamt;
                    // fadvanceamt = toalAdvCrdAmt + tcstadvanceamt;
                    // fCashamt = toalCashAmt + tcstCashamt;
                    // sb.Append("<td><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                    
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td class='text-right'><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td><b>Adv. Card Value</b></td>");

                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td class='text-right'><b>" + fadvanceamt.ToString("0.000") + "</b></td>");
                    // sb.Append("</tr>");
                    // //sb.Append("</table>");
                    // //sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    // //sb.Append("<tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td><b>Cash Value</b></td>");

                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td class='text-right'><b>" + fCashamt.ToString("0.000") + "</b></td>");
                    // sb.Append("</tr>");
                    // //sb.Append("</table>");
                    // //sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    // //sb.Append("<tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td><b>Grand Total</b></td>");
                    
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td></td>");
                    // sb.Append("<td class='text-right'><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                    // sb.Append("</tr>");
                    // sb.Append("</table>");
                    // sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    // sb.Append("<tr>");
                    // sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                    // sb.Append("<td style='padding-left:270px;'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td></td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");

                    // sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
                    // sb.Append("</tr>");
                    // sb.Append("<tr>");
                    // sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
                    // sb.Append("</tr>");
                    // sb.Append("</table>");
                    // sb.Append("</div>");

                    // ViewState["DistData"] += sb.ToString();
                    // ViewState["PStatus"] = "1";
                    // ////////////////End Of Route Wise Print Code   ///////////////////////
                // }
            // }
            // else
            // {
                // btnPrint.Visible = false;
                // div_page_content.InnerHtml = "";
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                // return;
            // }
        // }
        // catch (Exception ex)
        // {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        // }
        // finally
        // {
            // if (ds6 != null) { ds6.Dispose(); }
        // }

    // }
	
	private void GetInvoiceDetails(int distid)
    {
        try
        {
            string fm = "01/" + txtMonth.Text;
             DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string MDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductMonthlyInvoiceDetail"
                , new string[] { "flag", "FromDate", "DistributorId", "ItemCat_id", "Office_ID" }
                , new string[] { "1", MDate, ddlDitributor.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), }, "dataset");
            string pstatus = "";
            if (ds6.Tables.Count > 0)
            {
                if (ds6.Tables[0].Rows.Count > 0 )
                {

                    StringBuilder sb = new StringBuilder();
                    decimal totalamt = 0, toalCashAmt = 0, toalAdvCrdAmt = 0, paybleAmt = 0, tcstamt = 0, tdstamt, tdsAdvcardtamt = 0, tdsCashamt = 0, fpaybleamt = 0, tcstadvanceamt = 0, tcstCashamt = 0, fadvanceamt = 0, fCashamt = 0;

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
                    sb.Append("<td style='padding-left:50px;'><b>Monthly Bill</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td class='text-left' colspan='2'>" + ddlDitributor.SelectedItem.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td class='text-left' colspan='2'>Month :-" + txtMonth.Text + "</td>");
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
                    sb.Append("<td>Cash Qty(In Pkt)</td>");
                    sb.Append("<td>Cash Qty(In Ltr)</td>");
                    sb.Append("<td>Cash Rate(In Ltr)</td>");
                    sb.Append("<td>Cash Amount (In Pkt.)</td>");
                    sb.Append("<td>Adv. Card Qty(In Pkt)</td>");
                    sb.Append("<td>Adv. Card Qty(In Ltr)</td>");
                    sb.Append("<td>Adv. Card Rate(In Ltr)</td>");
                    sb.Append("<td>Adv. Card Amount (In Pkt.)</td>");
                    sb.Append("<td>Total Amount</td>");
                    sb.Append("</thead>");

                    for (int i = 0; i < Count; i++)
                    {
                        if (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashQty"]) > 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["ItemName"] + "</td>");
                            sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[i]["CashQty"] + "</td>");
                            sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashQtyInLtr"]).ToString("0.000") + "</td>");
                            sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]).ToString("0.000") + "</td>");
                            sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]).ToString("0.000") + "</td>");
                            sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[i]["AdvCardQty"] + "</td>");
                            sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardQtyInLtr"]).ToString("0.000") + "</td>");
                            //sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardRateperLtr"]).ToString("0.000") + "</td>");
                            //sb.Append("<td class='text-right'>" + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"]).ToString("0.000") + "</td>");
                            sb.Append("<td class='text-right'>" + Convert.ToDecimal(Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvRebateRatePerLtr"])).ToString("0.000") + "</td>");
                            sb.Append("<td class='text-right'>" + Convert.ToDecimal(Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardQtyInLtr"]) * (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvRebateRatePerLtr"]))).ToString("0.000") + "</td>");
                            //sb.Append("<td class='text-right'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]) + Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"])).ToString("0.000") + "</td>");
                            sb.Append("<td class='text-right'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]) + (Convert.ToDecimal(Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardQtyInLtr"]) * (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvRebateRatePerLtr"]))))).ToString("0.000") + "</td>");

                            sb.Append("</tr>");

                            toalCashAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]);
                            toalAdvCrdAmt += Convert.ToDecimal(Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardQtyInLtr"]) * (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvRebateRatePerLtr"])));
                            //toalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmount"]);
                            //totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
                            paybleAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"])) + (Convert.ToDecimal(Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardQtyInLtr"]) * (Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashRateperLtr"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvRebateRatePerLtr"]))));
                        }
                    }
                    sb.Append("<tr>");
                    int ColumnCount = ds6.Tables[0].Columns.Count;

                    //for (int i =0; i < ColumnCount-9; i++) // privious
                    for (int i = 0; i < ColumnCount-2; i++)
                    {
                        if (i == 0)
                        {
                            sb.Append("<td><b>Total</b></td>");
                        }
                        else if (i == 4)
                        {
                            sb.Append("<td class='text-right'><b>" + toalCashAmt.ToString("0.000") + "</b></td>");
                        }
                        //else if (i == ColumnCount-10)
                        else if (i == 8)
                        {
                            sb.Append("<td class='text-right'><b>" + toalAdvCrdAmt.ToString("0.000") + "</b></td>");
                        }
                        // else if (i == ColumnCount-10)
                        else if (i == 9)
                        {
                            sb.Append("<td class='text-right'><b>" + (toalCashAmt + toalAdvCrdAmt).ToString("0.000") + "</b></td>");
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }



                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                    tdstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tdsval"].ToString())) / 100);
                    tcstadvanceamt = ((toalAdvCrdAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                    tdsAdvcardtamt = ((toalAdvCrdAmt * Convert.ToDecimal(ViewState["Tdsval"].ToString())) / 100);
                    tcstCashamt = ((toalCashAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                    tdsCashamt = ((toalCashAmt * Convert.ToDecimal(ViewState["Tdsval"].ToString())) / 100);
                    fpaybleamt = paybleAmt + tcstamt - tdstamt;
                    fadvanceamt = toalAdvCrdAmt + tcstadvanceamt - tdsAdvcardtamt;
                    fCashamt = toalCashAmt + tcstCashamt - tdsCashamt;
                    sb.Append("<td><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                    
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td class='text-right'><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b>Tds on Sales @</b>" + (ViewState["Tdsval"].ToString() != "0.000" ? ViewState["Tdsval"].ToString() : "NA") + "</td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td class='text-right'><b>" + (ViewState["Tdsval"].ToString() != "0.000" ? tdstamt.ToString("0.000") : "NA") + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b>Adv. Card Value</b></td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td class='text-right'><b>" + fadvanceamt.ToString("0.000") + "</b></td>");
                    sb.Append("</tr>");
                    //sb.Append("</table>");
                    //sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    //sb.Append("<tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b>Cash Value</b></td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td class='text-right'><b>" + fCashamt.ToString("0.000") + "</b></td>");
                    sb.Append("</tr>");
                    //sb.Append("</table>");
                    //sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    //sb.Append("<tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b>Grand Total</b></td>");
                    
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td class='text-right'><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
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

                    ViewState["DistData"] += sb.ToString();
                    ViewState["PStatus"] = "1";
                    ////////////////End Of Route Wise Print Code   ///////////////////////
                }
            }
            else
            {
                btnPrint.Visible = false;
                div_page_content.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                return;
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
    //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLocation.SelectedValue != "0")
    //    {
    //        lblMsg.Text = string.Empty;
    //        GetDisOrSS();
    //    }
    //}

    private void GetTcsTax(string distid)
    {
        try
        {
            if (distid != "0")
            {
                ViewState["Tval"] = "";
                string fm = "01/" + txtMonth.Text;
                DateTime Ddate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
                //DateTime Ddate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds5 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deliverydate, distid }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds5.Tables[0].Rows[0]["Tval"].ToString();
					ViewState["Tdsval"] = ds5.Tables[0].Rows[0]["Tdsval"].ToString();
                }
                else
                {
                    ViewState["Tval"] = "0.000";
					ViewState["Tdsval"] = "0.000";
                }
            }
            else
            {
                ViewState["Tval"] = "0.000";
				ViewState["Tdsval"] = "0.000";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    //private void GetCompareDate()
    //{
    //    try
    //    {
    //        string myStringfromdat = txtFromDate.Text; // From Database
    //        DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //        string myStringtodate = txtToDate.Text; // From Database
    //        DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //        if (fdate <= tdate)
    //        {
    //            lblMsg.Text = string.Empty;
    //            FillDistributorDetails();
    //        }
    //        else
    //        {
    //            txtToDate.Text = string.Empty;
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
    //    }
    //}
    #endregion===========================
}