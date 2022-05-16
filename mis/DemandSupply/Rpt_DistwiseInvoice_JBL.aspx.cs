using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Rpt_DistwiseInvoice_JBL : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6, ds7 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0, totalsupply = 0.00, finalsupply = 0.00, totalAdvAmt = 0.00, advcardtotalamt = 0.000, instamtotalt = 0.000, instmarginamt = 0.000, tcstax = 0.000, tcstaxAmt = 0.000, PaybleAmtWithTcsTax = 0.000;

    int cellIndex = 2;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.GetItemCat_id() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtToDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
                GetShift();
                GetCategory();
                GetOfficeDetails();

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

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
                GetDistOrIstDetails();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6: ", ex.Message.ToString());
        }
    }
    private void GetDistOrIstDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime Fdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string Fromdate = Fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime Tdate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string Todate = Tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "Flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "Office_ID", "DistributorId" },
                       new string[] { "4", Fromdate.ToString(), Todate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), objdb.createdBy() }, "dataset");


            if (ds1.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

            }
            else
            {
                pnlData.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();

                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :", " Record not found");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    #endregion========================================================
    
    #region=========== click event for grdiview row bound event===========================
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "View")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;

    //                GridView3.Visible = true;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                // Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
    //                Label lblDistName = (Label)row.FindControl("lblDistName");
    //                Label lblDelivaryDate = (Label)row.FindControl("lblDelivaryDate");
    //                Label lblShiftName = (Label)row.FindControl("lblShiftName");
    //                Label lblItemCatid = (Label)row.FindControl("lblItemCatid");

    //                modalBoothName.InnerHtml = lblDistName.Text;
    //                modaldate.InnerHtml = lblDelivaryDate.Text;
    //                modelShift.InnerHtml = lblShiftName.Text;

    //                ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
    //                                       new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
    //                                       new string[] { "3", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");

    //                if (ds6.Tables[0].Rows.Count > 0)
    //                {
    //                    lblMsName.Text = lblDistName.Text;
    //                    lblRouteName.Text = ds6.Tables[0].Rows[0]["RName"].ToString();
    //                    if (ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString() == "")
    //                    {
    //                        ViewState["Tval"] = "0.000";
    //                    }
    //                    else
    //                    {
    //                        ViewState["Tval"] = ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString();
    //                    }
    //                    lblOName1.Text = ViewState["Office_Name"].ToString();
    //                    lblOName2.Text = ViewState["Office_Name"].ToString();
    //                    lblOName3.Text = ViewState["Office_Name"].ToString();
    //                    lblGST.Text = ViewState["Office_GST"].ToString();
    //                    lblDelishift.Text = lblShiftName.Text;
    //                    lbldelidate.Text = lblDelivaryDate.Text;
    //                    lblDelivarydate.Text = lblDelivaryDate.Text;

    //                    GridView3.DataSource = ds6.Tables[0];
    //                    GridView3.DataBind();


    //                    ////////////////Start Of Route Wise Print Code   ///////////////////////
    //                    StringBuilder sb = new StringBuilder();
    //                    decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, tcstamt = 0, fpaybleamt = 0, totaladvAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0;
    //                    sb.Append("<div class='content' style='border: 1px solid black'>");
    //                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
    //                    sb.Append("<td class='text-left'><b>" + lblOName1.Text + "<b></td>");
    //                    sb.Append("</tr>");
    //                    //sb.Append("<tr>");
    //                    //sb.Append("<td class='text-center' colspan='2'><b>" +  + "<b></td>");
    //                    //sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td style='padding-left:50px;'><b>Bill Book</b></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td><b>G.S.T/U.I.N NO:-" + lblGST.Text + "<b></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left' colspan='2'>No" + lblDelivarydate.Text + "</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left' colspan='2'>M/s  :-" + lblMsName.Text + "</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left' colspan='2'>" + lblDelishift.Text + "</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left'>" + lbldelidate.Text + "</td>");
    //                    sb.Append("<td class='text-right'>" + lblRouteName.Text + "</td>");
    //                    sb.Append("</tr>");

    //                    sb.Append("<tr>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("</tr>");

    //                    sb.Append("</table>");
    //                    sb.Append("<table class='table table1-bordered'>");
    //                    int Count = ds6.Tables[0].Rows.Count;
    //                    int ColCount = ds6.Tables[0].Columns.Count;
    //                    sb.Append("<thead>");
    //                    sb.Append("<td>Particulars</td>");
    //                    sb.Append("<td>Qty(In Pkt)</td>");
    //                    sb.Append("<td>Return Qty (In Pkt.)</td>");
    //                    sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
    //                    sb.Append("<td>Adv. Card Price</td>");
    //                    sb.Append("<td>Total Adv. Card Amt</td>");
    //                    sb.Append("<td>Adv. Card Margin</td>");
    //                    sb.Append("<td>Adv. Card Margin Amt</td>");
    //                    sb.Append("<td>Inst. Qty</td>");
    //                    sb.Append("<td>Inst. Total Amt</td>");
    //                    sb.Append("<td>Inst. Margin</td>");
    //                    sb.Append("<td>Inst. Tran Margin Amt</td>");
    //                    sb.Append("<td>Billing Qty(In Pkt.)</td>");
    //                    sb.Append("<td>Billing Qty(In Ltr.)</td>");
    //                    sb.Append("<td>Rate (Per Ltr.)</td>");
    //                    sb.Append("<td>Amount</td>");
    //                    sb.Append("<td>Payble Amount</td>");
    //                    sb.Append("</thead>");

    //                    for (int i = 0; i < Count; i++)
    //                    {

    //                        sb.Append("<tr>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
    //                        sb.Append("<td>" + (ds6.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() == "0" ? "0.000" : ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]) + "</td>");
    //                        sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"])).ToString("0.000") + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
    //                        sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"])).ToString("0.000") + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingAmount"] + "</td>");
    //                        sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
    //                        sb.Append("</tr>");

    //                        totaladvAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]));
    //                        totalInstAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"]));
    //                        totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
    //                        totalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
    //                        totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingAmount"]);
    //                        paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);
    //                    }
    //                    sb.Append("<tr>");
    //                    int ColumnCount = GridView3.FooterRow.Cells.Count;
    //                    for (int i = 0; i < ColumnCount; i++)
    //                    {
    //                        if (i == 0)
    //                        {
    //                            sb.Append("<td><b>" + GridView3.FooterRow.Cells[i].Text + "</b></td>");
    //                        }
    //                        else if (i == ColumnCount - 12)
    //                        {
    //                            sb.Append("<td><b>" + totaladvAmt.ToString("0.000") + "</b></td>");
    //                        }
    //                        else if (i == ColumnCount - 10)
    //                        {
    //                            sb.Append("<td><b>" + totalAdvCrdAmt + "</b></td>");
    //                        }
    //                        else if (i == ColumnCount - 8)
    //                        {
    //                            sb.Append("<td><b>" + totalInstAmt.ToString("0.000") + "</b></td>");
    //                        }
    //                        else if (i == ColumnCount - 6)
    //                        {
    //                            sb.Append("<td><b>" + totalInstTranCommAmt + "</b></td>");
    //                        }
    //                        else if (i == ColumnCount - 2)
    //                        {
    //                            sb.Append("<td><b>" + totalamt + "</b></td>");
    //                        }
    //                        else if (i == ColumnCount - 1)
    //                        {
    //                            sb.Append("<td><b>" + paybleAmt + "</b></td>");
    //                        }
    //                        else
    //                        {
    //                            sb.Append("<td>" + GridView3.FooterRow.Cells[i].Text + "</td>");
    //                        }



    //                    }
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()) / 100));
    //                    fpaybleamt = paybleAmt + tcstamt;
    //                    sb.Append("<td><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td><b>Grand Total</b></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("</table>");
    //                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td style='text-left'>Prepared & Checked by</td>");
    //                    sb.Append("<td style='padding-left:270px;'>For :-" + lblOName2.Text + "</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left' colspan='2'>Note:</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");

    //                    sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + lblOName3.Text + "</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("</table>");
    //                    sb.Append("</div>");
    //                    sb.Append("</div>");

    //                    Print.InnerHtml = sb.ToString();
    //                    ////////////////End Of Route Wise Print Code   ///////////////////////
    //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
    //    }

    //    finally
    //    {
    //        if (ds6 != null) { ds6.Dispose(); }
    //    }
    //}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    // Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblDistName = (Label)row.FindControl("lblDistName");
                    Label lblDelivaryDate = (Label)row.FindControl("lblDelivaryDate");
                    Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");

                    modalBoothName.InnerHtml = lblDistName.Text;
                    modaldate.InnerHtml = lblDelivaryDate.Text;
                    modelShift.InnerHtml = lblShiftName.Text;

                    ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");

                    if (ds6.Tables[0].Rows.Count > 0)
                    {
                        lblMsName.Text = lblDistName.Text;
                        lblRouteName.Text = ds6.Tables[0].Rows[0]["RName"].ToString();
                        if (ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString() == "")
                        {
                            ViewState["Tval"] = "0.000";
                        }
                        else
                        {
                            ViewState["Tval"] = ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString();
                        }
                        lblOName1.Text = ViewState["Office_Name"].ToString();
                        lblOName2.Text = ViewState["Office_Name"].ToString();
                        lblOName3.Text = ViewState["Office_Name"].ToString();
                        lblGST.Text = ViewState["Office_GST"].ToString();
                        lblDelishift.Text = lblShiftName.Text;
                        lbldelidate.Text = lblDelivaryDate.Text;
                        lblDelivarydate.Text = lblDelivaryDate.Text;

                        GridView3.DataSource = ds6.Tables[0];
                        GridView3.DataBind();


                        ////////////////Start Of Route Wise Print Code   ///////////////////////
                        StringBuilder sb = new StringBuilder();
                        decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, totalAdvCrdCmmAmt = 0, tcstamt = 0, fpaybleamt = 0, totaladvAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0;
                        decimal totaldiscomamt = 0, totaltranscomamt = 0, totalcashsale = 0, totalbillingqtyinltr = 0, totalsupplyqtyinltr = 0;
                        int totalbillingqty = 0, totalsupplyqty = 0, totaladvancecardqty = 0, totalinstituteqty = 0, totalreturnqty = 0;

                        sb.Append("<div class='table-responsive'");
                        sb.Append("<div class='content' style='border: 0px solid black'>");

                        sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
                        sb.Append("<tr>");
                        sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                        sb.Append("<td class='text-left'><b>" + lblOName1.Text + "<b></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        if (ds6.Tables[0].Rows[0]["InvoiceEditStatus"].ToString() == "")
                        {
                            sb.Append("<td style='padding-left:50px;'><b>Bill</b></td>");
                        }
                        else
                        {
                            sb.Append("<td style='padding-left:50px;'><b>Revised Bill</b></td>");
                        }

                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><b>G.S.T/U.I.N NO:-" + lblGST.Text + "<b></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>Invoice No. :" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>M/s  :-" + lblMsName.Text + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>" + lblDelishift.Text + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left'>" + lbldelidate.Text + "</td>");
                        sb.Append("<td class='text-right'>" + lblRouteName.Text + "</td>");
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
                        sb.Append("<thead style='padding-left:0px;'>");
                        sb.Append("<td style='width:120px'>Particulars</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Qty(In Pkt)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Qty(In Ltr.)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Return Qty (In Pkt.)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Adv. Card Qty(In Pkt.)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Adv. Card Price</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Total Adv. Card Amt</td>");
                        sb.Append("<td style='display:none'>Adv. Card Margin</td>");
                        sb.Append("<td style='display:none'>Adv. Card Margin Amt</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Inst. Qty</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Inst. Total Amt</td>");
                        sb.Append("<td style='display:none'>Inst. Margin</td>");
                        sb.Append("<td style='display:none'>Inst. Tran Margin Amt</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Billing Qty(In Pkt.)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Billing Qty(In Ltr.)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Rate (Per Ltr.)</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Amount</td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 11px;vertical-align: middle;text-align: right;font-weight:600;'>Payble Amount</td>");
                        sb.Append("</thead>");

                        for (int i = 0; i < Count; i++)
                        {

                            sb.Append("<tr>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + (ds6.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() == "0" ? "0.000" : ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]) + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"])).ToString("0.000") + "</td>");
                            sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                            sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"])).ToString("0.000") + "</td>");
                            sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
                            sb.Append("<td style='display:none'>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["BillingAmount"] + "</td>");
                            sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                            sb.Append("</tr>");



                            totaladvAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]));
                            totalInstAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"]));
                            totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
                            totalAdvCrdCmmAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                            totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingAmount"]);
                            paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);


                            totaldiscomamt += Convert.ToDecimal((double.Parse(ds6.Tables[0].Rows[i]["DistOrSSComm"].ToString()) + double.Parse(ds6.Tables[0].Rows[i]["TransComm"].ToString())) * double.Parse(ds6.Tables[0].Rows[i]["BillingQty"].ToString()));
                            totalbillingqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["BillingQty"]);
                            totalsupplyqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["SupplyTotalQty"]);
                            totalsupplyqtyinltr += Convert.ToDecimal(ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"]);
                            totalbillingqtyinltr += Convert.ToInt32(ds6.Tables[0].Rows[i]["BillingQtyInLtr"]);
                            totaladvancecardqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalAdvCardQty"]);
                            totalinstituteqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalInstSupplyQty"]);
                            totalreturnqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalReturnQty"]);
                            totalcashsale += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingQty"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["FinalRetailerrate"]));

                        }
                        sb.Append("<tr>");
                        // int ColumnCount = ds6.Tables[0].Rows.Count;
                        int ColumnCount = GridView3.Columns.Count;

                        //for (int i = 0; i < ColumnCount; i++)
                        //{
                        //    if (i == 0)
                        //    {
                        //        sb.Append("<td><b>" + GridView3.FooterRow.Cells[i].Text + "</b></td>");
                        //    }
                        //    else if (i == ColumnCount - 12)
                        //    {
                        //        sb.Append("<td><b>" + totaladvAmt.ToString("0.000") + "</b></td>");
                        //    }
                        //    else if (i == ColumnCount - 10)
                        //    {
                        //        sb.Append("<td><b>" + totalAdvCrdCmmAmt + "</b></td>");
                        //    }
                        //    else if (i == ColumnCount - 8)
                        //    {
                        //        sb.Append("<td><b>" + totalInstAmt.ToString("0.000") + "</b></td>");
                        //    }
                        //    else if (i == ColumnCount - 6)
                        //    {
                        //        sb.Append("<td><b>" + totalInstTranCommAmt + "</b></td>");
                        //    }
                        //    else if (i == ColumnCount - 2)
                        //    {
                        //        sb.Append("<td><b>" + totalamt + "</b></td>");
                        //    }
                        //    else if (i == ColumnCount - 1)
                        //    {
                        //        sb.Append("<td><b>" + paybleAmt + "</b></td>");
                        //    }
                        //    else
                        //    {
                        //        sb.Append("<td>" + GridView3.FooterRow.Cells[i].Text + "</td>");
                        //    }



                        //}
                        // for (int i = 0; i < ColumnCount; i++)
                        for (int i = 0; i < 17; i++)
                        {
                            if (i == 0)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>Total</b></td>");
                            }
                            else if (i == 1)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalsupplyqty.ToString() + "</b></td>");
                            }
                            else if (i == 2)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalsupplyqtyinltr.ToString() + "</b></td>");
                            }
                            else if (i == 3)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalreturnqty.ToString() + "</b></td>");
                            }
                            else if (i == 4)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totaladvancecardqty.ToString() + "</b></td>");
                            }
                            else if (i == 5)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'></td>");
                            }
                            else if (i == 6)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totaladvAmt.ToString("0.000") + "</b></td>");
                            }

                            else if (i == 7)
                            {
                                sb.Append("<td style='display:none'><b>" + totalAdvCrdCmmAmt.ToString("0.000") + "</b></td>");
                            }
                            else if (i == 8)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalinstituteqty.ToString() + "</b></td>");
                            }
                            else if (i == 9)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalInstAmt.ToString("0.000") + "</b></td>");
                            }
                            else if (i == 10)
                            {
                                sb.Append("<td style='display:none'><b>" + totalInstTranCommAmt.ToString("0.000") + "</b></td>");
                            }
                            else if (i == 11)
                            {
                                sb.Append("<td style='display:none'><b>" + totaldiscomamt.ToString("0.000") + "</b></td>");
                            }
                            else if (i == 12)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalbillingqty.ToString() + "</b></td>");
                            }
                            else if (i == 13)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalbillingqtyinltr.ToString() + "</b></td>");
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
                            else if (i == 15)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + totalamt.ToString("0.000") + "</b></td>");
                            }

                            else if (i == 16)
                            {
                                sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + paybleAmt.ToString("0.000") + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }



                        }
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()) / 100));
                        fpaybleamt = paybleAmt + tcstamt;
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>Grand Total</b></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        sb.Append("<td style='padding: 1px 2px 1px 1px;font-size: 10px;vertical-align: middle;text-align: left;'><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");

                        sb.Append("<tr>");
                        sb.Append("<td style='text-left;width:250px'>Prepared by</td>");
                        sb.Append("<td style='text-right'><b>Advance Card Total</b></td>");
                        sb.Append("<td style='text-right'><b>" + totaladvAmt.ToString("0.00") + "</b></td>");
                        sb.Append("<td style='display:none' class='text-right'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style='text-left'></td>");
                        sb.Append("<td style='text-right'><b>Cash Milk Sale Total</b></td>");
                        sb.Append("<td style='text-right'><b>" + totalcashsale.ToString("0.00") + "</b></td>");
                        sb.Append("<td style='display:none' class='text-right' ><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
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
                        sb.Append("<td style='text-right'><b>" + (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt).ToString("0.00") + "</b></td>");
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
                        sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
                        sb.Append("<td class='text-center' ></td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                        //sb.Append("<td style='padding-left:270px;'>For :-" + lblOName2.Text + "</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td></td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");

                        //sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + lblOName3.Text + "</td>");
                        //sb.Append("</tr>");

                        sb.Append("</br><tr>");
                        sb.Append("<td class='text-left' >Remark :" + lblremark.Text + "</td>");


                        sb.Append("</tr></br>");

                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='4'>Note:</td>");
                        sb.Append("<td class='text-right' colspan='4'>For :-" + ViewState["Office_Name"].ToString() + "</td>");

                        sb.Append("</tr>");
                        sb.Append("<tr>");

                        sb.Append("<td class='text-left' colspan='4'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                        sb.Append("<td class='text-right' colspan='4'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
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

                        Print.InnerHtml = sb.ToString();
                        ////////////////End Of Route Wise Print Code   ///////////////////////
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }

        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotalPaybleAmount = (e.Row.FindControl("lblTotalPaybleAmount") as Label);
                totalsupply += Convert.ToDouble(lblTotalPaybleAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblPAmount = (e.Row.FindControl("lblPAmount") as Label);
                lblPAmount.Text = totalsupply.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAdvCardAmt = (e.Row.FindControl("lblAdvCardAmt") as Label);
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                Label lblPaybleAmount = (e.Row.FindControl("lblPaybleAmount") as Label);
                Label lblInstTotalAmt = (e.Row.FindControl("lblInstTotalAmt") as Label);
                Label lblInstTransCommAmt = (e.Row.FindControl("lblInstTransCommAmt") as Label);
                Label lblTotalAdvCardAmount = (e.Row.FindControl("lblTotalAdvCardAmount") as Label);
                advcardtotalamt += Convert.ToDouble(lblTotalAdvCardAmount.Text);
                totalsupply += Convert.ToDouble(lblAmount.Text);
                finalsupply += Convert.ToDouble(lblPaybleAmount.Text);
                totalAdvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
                instamtotalt += Convert.ToDouble(lblInstTotalAmt.Text);
                instmarginamt += Convert.ToDouble(lblInstTransCommAmt.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblTAmount = (e.Row.FindControl("lblTAmount") as Label);
                Label lblTotalPAmount = (e.Row.FindControl("lblTotalPAmount") as Label);
                Label lblTAdvCAmt = (e.Row.FindControl("lblTAdvCAmt") as Label);
                Label lblFITAmt = (e.Row.FindControl("lblFITAmt") as Label);
                Label lblFInstMarAmt = (e.Row.FindControl("lblFInstMarAmt") as Label);
                lblTAdvCAmt.Text = advcardtotalamt.ToString("0.000");
                lblFITAmt.Text = instamtotalt.ToString("0.000");
                lblTAmount.Text = totalsupply.ToString("0.000");
                lblFInstMarAmt.Text = instmarginamt.ToString("0.000");
                lblTotalPAmount.Text = finalsupply.ToString("0.000");
                lblTotalAdvCardAmt.Text = totalAdvAmt.ToString("0.000");
                if (ViewState["Tval"].ToString() == "")
                {
                    tcstax = 0.000;
                }
                else
                {
                    tcstax = Convert.ToDouble(ViewState["Tval"].ToString());
                }
                tcstaxAmt = ((tcstax * finalsupply) / 100);
                PaybleAmtWithTcsTax = tcstaxAmt + finalsupply;
                // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
                lblTcsTax.Text = ViewState["Tval"].ToString();
                lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.000");
                lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
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
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        // ddlItemCategory.SelectedIndex = 0;
        pnlData.Visible = false;
        ddlShift.SelectedIndex = 0;

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "InvoiceList" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            GridView1.Columns[6].Visible = false;
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }

    #endregion===========================
}