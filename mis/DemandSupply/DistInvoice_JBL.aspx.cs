using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_DistInvoice_JBL : System.Web.UI.Page
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
                GetLocation();
                GetShift();
                GetCategory();

                GetOfficeDetails();
                ViewState["DistData"] = "";
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
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
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string Officeid = objdb.Office_ID();
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


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }


    private void GetDisOrSS()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "8", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlDitributor.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    //private void GetDisOrSS()
    //{
    //    try
    //    {
    //        ddlDitributor.DataTextField = "DTName";
    //        ddlDitributor.DataValueField = "DistributorId";
    //        ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorReg",
    //             new string[] { "flag", "Office_ID" },
    //               new string[] { "1", objdb.Office_ID() }, "dataset");
    //        ddlDitributor.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
    //    }
    //}
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
            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


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
            ViewState["DistData"] = "";
            FillDistributorDetails();
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
            foreach (ListItem item in ddlDitributor.Items)
            {
                if (item.Selected)
                {
                    distid = item.Value;
                    GetTcsTax(distid);
                    GetInvoiceDetails(Convert.ToInt32(distid));
                }
            }
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
    private void GetInvoiceDetails(int distid)
    {
        try
        {
            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                         new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "DistributorId", "OrganizationId", "AreaId" },
                           new string[] { "6", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), distid.ToString(), "0", ddlLocation.SelectedValue }, "dataset");
                string pstatus = "";
            if (ds6.Tables[0].Rows.Count > 0 && ds6.Tables[0].Rows[0]["Msg"].ToString() == "Found")
            {
                
                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0, paybleAmt = 0, totaladvAmt = 0, totalAdvCrdCmmAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0, tcstamt = 0, fpaybleamt = 0, totaldiscomamt = 0, totaltranscomamt = 0, totalcashsale = 0, totalsupplyqtyinltr=0;
                int totalbillingqty = 0, totalbillingqtyinltr = 0, totalsupplyqty = 0, totaladvancecardqty = 0, totalinstituteqty = 0, totalreturnqty = 0;

                if (ViewState["PStatus"].ToString() != "")
                {
                    sb.Append("<p style='page-break-after: always'>");
                }
                sb.Append("<div class='table-responsive'");
                sb.Append("<div class='content' style='border: 0px solid black'>");
                sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
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
                sb.Append("<td class='text-left' colspan='2'>No" + txtDeliveryDate.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>M/s  :-" + ds6.Tables[0].Rows[0]["DName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
                sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered' > ");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead style='padding-left:0px;'>");
                sb.Append("<td style='width:120px'>Particulars</td>");
                sb.Append("<td>Qty(In Pkt)</td>");
                sb.Append("<td>Qty(In Ltr.)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td>Adv. Card Price</td>");
                sb.Append("<td>Total Adv. Card Amt</td>");
                sb.Append("<td style='display:none'>Adv. Card Margin</td>");
                sb.Append("<td style='display:none'>Adv. Card Margin Amt</td>");
                sb.Append("<td>Inst. Qty</td>");
                sb.Append("<td>Inst. Total Amt</td>");
                sb.Append("<td style='display:none'>Inst. Margin</td>");
                sb.Append("<td style='display:none'>Inst. Tran Margin Amt</td>");

                sb.Append("<td style='display:none'>(Distri. + trans) Margin on cash supply</td>");
                sb.Append("<td style='display:none'>(Distri. + trans) Margin Amt on cash supply</td>");

                //sb.Append("<td>Trans Margin</td>");
                //sb.Append("<td>Trans Margin Amt</td>");

                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr.)</td>");
                sb.Append("<td>Amount</td>");
                sb.Append("<td>Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:left'>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td>" + (ds6.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() == "0" ? "0.000" : ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]) + "</td>");
                    sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"])).ToString("0.000") + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
                    sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"])).ToString("0.000") + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
                    sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
                    //distcommission
                    sb.Append("<td style='display:none'>" + (double.Parse(ds6.Tables[0].Rows[i]["DistOrSSComm"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString())) + "</td>");
                    sb.Append("<td style='display:none'>" + (double.Parse(ds6.Tables[0].Rows[i]["DistOrSSComm"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString())) * double.Parse(ds6.Tables[0].Rows[i]["BillingQty"].ToString()) + "</td>");

                    ////transcommission
                    //sb.Append("<td>" + ds6.Tables[0].Rows[i]["TransComm"] + "</td>");
                    //sb.Append("<td>" + double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString()) * (double.Parse(ds6.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["BillingQty"].ToString())) + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"])) + "</td>");
                    sb.Append("</tr>");


                    decimal distranmargin = (Convert.ToDecimal(ds6.Tables[0].Rows[i]["DistOrSSComm"].ToString()) + Convert.ToDecimal(ds6.Tables[0].Rows[i]["TransComm"].ToString())) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingQty"].ToString());
                    totaladvAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]));
                    totalInstAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"]));
                    totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
                    totalAdvCrdCmmAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
                    paybleAmt += ((Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"])));
                    totaldiscomamt += Convert.ToDecimal((double.Parse(ds6.Tables[0].Rows[i]["DistOrSSComm"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString())) * double.Parse(ds6.Tables[0].Rows[i]["BillingQty"].ToString()));
                    //totaltranscomamt += Convert.ToDecimal(double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString()) *  (double.Parse(ds6.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["BillingQty"].ToString())));
                    //qty total
                    totalbillingqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["BillingQty"]);
                    totalbillingqtyinltr += Convert.ToInt32(ds6.Tables[0].Rows[i]["BillingQtyInLtr"]);
                    totalsupplyqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["SupplyTotalQty"]);
                    totalsupplyqtyinltr += Convert.ToDecimal(ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"]);
                    totaladvancecardqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalAdvCardQty"]);
                    totalinstituteqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalInstSupplyQty"]);
                    totalreturnqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalReturnQty"]);
                    totalcashsale += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingQty"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["Finalconsumerrate"]));
                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count-2;

                //for (int i =0; i < ColumnCount-9; i++) 
                for (int i = 0; i < ColumnCount - 15; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td style='text-align:left'><b>Total</b></td>");
                    }
                    else if (i == ColumnCount - 31)
                    {
                        sb.Append("<td><b>" + totalsupplyqty.ToString() + "</b></td>");
                    }
                    else if (i == ColumnCount - 30)
                    {
                        sb.Append("<td><b>" + totalsupplyqtyinltr.ToString() + "</b></td>");
                    }
                    else if (i == ColumnCount - 29)
                    {
                        sb.Append("<td><b>" + totalreturnqty.ToString() + "</b></td>");
                    }
                    else if (i == ColumnCount - 28)
                    {
                        sb.Append("<td><b>" + totaladvancecardqty.ToString() + "</b></td>");
                    }
                    else if (i == ColumnCount - 26)
                    {
                        sb.Append("<td><b>" + totaladvAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 25)
                    {
                        sb.Append("<td style='display:none'><b>" + totalAdvCrdCmmAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 24)
                    {
                        sb.Append("<td><b>" + totalinstituteqty.ToString() + "</b></td>");
                    }
                    else if (i == ColumnCount - 23)
                    {
                        sb.Append("<td><b>" + totalInstAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 22)
                    {
                        sb.Append("<td style='display:none'><b>" + totalInstTranCommAmt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 21)
                    {
                        sb.Append("<td style='display:none'><b>" + totaldiscomamt.ToString("0.000") + "</b></td>");
                    }
                    else if (i == ColumnCount - 20)
                    {
                        sb.Append("<td><b>" + totalbillingqty.ToString() + "</b></td>");
                    }
                    else if (i == ColumnCount - 19)
                    {
                        sb.Append("<td><b>" + totalbillingqtyinltr.ToString() + "</b></td>");
                    }
                    //else if (i == ColumnCount - 17)
                    //{
                    //    sb.Append("<td><b>" + totaltranscomamt.ToString("0.000") + "</b></td>");
                    //}
                    //else if (i == ColumnCount - 17)
                    //{
                    //    sb.Append("<td></td>");
                    //}

                    //else if (i == ColumnCount - 16)
                    //{
                    //    sb.Append("<td></td>");
                    //}
                    else if (i == ColumnCount - 17)
                    {
                        sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                    }

                    else if (i == ColumnCount - 16)
                    {
                        sb.Append("<td><b>" + paybleAmt.ToString("0.000") + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                tcstamt = (paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()));
                fpaybleamt = paybleAmt + tcstamt;
                sb.Append("<td style='text-align:left'><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                //sb.Append("<td></td>");
                //sb.Append("<td></td>");
                sb.Append("<td><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td style='display:none'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td>Amount For Cheque Preparation</td>");
                //sb.Append("<td></td>");
                //sb.Append("<td></td>");
                sb.Append("<td><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left;width:250px'>Prepared by</td>");
                sb.Append("<td style='text-right'><b>Advance Card Total</b></td>");
                sb.Append("<td style='text-right'><b>" + totaladvAmt.ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-right'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Cash Milk Sale Total</b></td>");
                sb.Append("<td style='text-right'><b>" + totalcashsale.ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-right' ><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Total Milk Sale Amount</b></td>");
                sb.Append("<td style='text-right'><b>" + (totaladvAmt + totalcashsale).ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-center' ></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left;width:250px'>Checked By</td>");
                sb.Append("<td style='text-right'><b>Transportation Charges(-)</b></td>");
                sb.Append("<td style='text-right'><b>" + (totaldiscomamt  + totalAdvCrdCmmAmt + totalInstTranCommAmt).ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-center' ></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left;width:250px'></td>");
                sb.Append("<td style='text-right'><b>Net Milk Sale Amount</b></td>");
                sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
                sb.Append("<td ></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Total Product Sale Amount(+)</b></td>");
                sb.Append("<td style='text-right'><b>" + 0.00 + "</b></td>");
                sb.Append("<td class='text-right' ><b></b></td>");
                sb.Append("</tr>");

                //Net Receivable Amount=Net Milk Sale Amount+Total Product Sale Amount
                sb.Append("<tr>");
                sb.Append("<td style='text-left'></td>");
                sb.Append("<td style='text-right'><b>Net Receivable Amount</b></td>");
                sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt  + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
                sb.Append("<td class='text-center' ></td>");
                sb.Append("</tr>");


                //sb.Append("<tr>");
                //sb.Append("<td style='text-left;width:400px'>Prepared & Checked by</td>");
                //sb.Append("<td style='text-right'><b>Advance Card Margin Amt Total</b></td>");

                //sb.Append("<td style='text-right'><b>" + totalAdvCrdCmmAmt.ToString() + "</b></td>");
                //sb.Append("<td style='padding-left:270px;'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td style='text-left'></td>");
                //sb.Append("<td style='text-right'><b>Inst. Tran Margin Amt</b></td>");

                //sb.Append("<td style='text-right'><b>" + totalInstTranCommAmt.ToString() + "</b></td>");
                //sb.Append("<td class='text-right' ><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td style='text-left'></td>");
                //sb.Append("<td style='text-right'><b>Distributor Margin Amt Total</b></td>");

                //sb.Append("<td style='text-right'><b>" + totaldiscomamt.ToString() + "</b></td>");
                //sb.Append("<td class='text-center' ></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td style='text-left'></td>");
                //sb.Append("<td style='text-right'><b>Transport Margin Amt Total</b></td>");

                //sb.Append("<td style='text-right'><b>" + totaltranscomamt.ToString() + "</b></td>");
                //sb.Append("<td class='text-center' ></td>");
                //sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='4'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='4'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='4'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='4'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div>");

                ViewState["DistData"] += sb.ToString();
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetDisOrSS();
        }
    }

    private void GetTcsTax(string distid)
    {
        try
        {
            if (distid != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds5 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deliverydate, distid }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds5.Tables[0].Rows[0]["Tval"].ToString();
                }
                else
                {
                    ViewState["Tval"] = "0.000";
                }
            }
            else
            {
                ViewState["Tval"] = "0.000";
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
    #endregion===========================
}