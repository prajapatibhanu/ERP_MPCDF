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

public partial class mis_Demand_DistributorDMCumInvoice_Rpt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2, ds3, ds4, ds9, ds5, dsInvo = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void CClear()
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
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
                GetDMDetails();
                
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
    private void GetDMDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_ProductDistRpt",
                     new string[] { "flag", "FromDate", "ToDate", "DistributorId", "Office_ID" },
                       new string[] { "3", fromdat, todat, objdb.createdBy(), objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                pnldata.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void PrintDMDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
                     new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
                     new string[] { "0", objdb.Office_ID(), cid }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                StringBuilder sb = new StringBuilder();
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');
                sb.Append("<div class='invoice'>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td  style='text-align:center:'> <b>GSTIN NO: " + ds3.Tables[0].Rows[0]["Office_Gst"].ToString() + "</b></td>");
                sb.Append("<td colspan='2' style='text-align:right:'><b> Phone No: " + ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td><b>Date</b>&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='font-size:17px;'><b>" + Dairyplant[0] + " </b></td>");
                sb.Append("<td><b>" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr></tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'><span style='font-weight:600;'>D.M Cum/ Gate Pass no." + ds3.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</span></td>");
                sb.Append("<td style='font-size:17px;'><b> To=>" + ds3.Tables[0].Rows[0]["DName"].ToString() + "</b></td>");
                sb.Append("<td><b>Vehicle No:</b> &nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                sb.Append("</tr>");
                // sb.Append("<tr>");
                //sb.Append("<td colspan='3' style='text-align:left'>सुपरवाइजर का नाम&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["SupervisorName"].ToString() + "</span></td>");
                // sb.Append("</tr>");
                sb.Append("<tr>");
                // sb.Append("<td style='text-align:left'>आने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleIn_Time"].ToString() + "</span></td>");
                sb.Append("<td><b>Route No:</b> &nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["RName"].ToString() + "</span></td>");
                //  sb.Append("<td>जाने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</span></td>");
                sb.Append("<td style='font-size:17px;'><b> Party GST No : " + ds3.Tables[0].Rows[0]["partygstn"].ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:center'>Please receive the follwing goods and acknowledge</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='table table1-bordered'>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center'>Name of Product</th>");
                sb.Append("<th style='text-align:center'>QTY.</th>");
                sb.Append("<th style='text-align:center'>Crate</th>");
                sb.Append("<th style='text-align:center'>Rate</th>");
                sb.Append("<th style='text-align:center'>Amount</th>");
                sb.Append("</tr>");
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                double TotalAmt = 0;
                double TCSTAX_Amt = 0, FinalAmt_withTCSTax = 0;
                for (int i = 0; i < Count; i++)
                {
                    TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["SupplyQty"].ToString());
                    TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                    TotalAmt += Double.Parse(ds3.Tables[0].Rows[i]["Amount"].ToString());
                    sb.Append("<tr>");
                    sb.Append("<td>" + ds3.Tables[0].Rows[i]["ItemName"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'></td>");
                    //sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueCrate"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["RateincludingGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["Amount"].ToString() + "</td>");
                    sb.Append("</tr>");

                }
                TCSTAX_Amt = ((TotalAmt * Convert.ToDouble(ds3.Tables[0].Rows[0]["TcsTaxPer"])) / 100);
                FinalAmt_withTCSTax = TotalAmt + TCSTAX_Amt;
                sb.Append("<tr>");
                sb.Append("<td><b>Total</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TotalAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Tcs on Sales @ " + (ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</b></td>");
                sb.Append("<td style='text-align:center'></td>");
                sb.Append("<td style='text-align:center'></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX_Amt.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Grand Total</b></td>");
                sb.Append("<td style='text-align:center'></td>");
                sb.Append("<td style='text-align:center'></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + FinalAmt_withTCSTax.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                //sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वाहन सुपरवाइजर</span></td>");
                //sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                //sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                sb.Append("<td colspan='3' <span style='text-align:right; padding-top:20px; font-weight:700;'>GM/AGM/SPO/DC</br>&nbsp;&nbsp;(MKIG)&nbsp;&nbsp;</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='width:100%;' ></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'>Product received as per<br>above details along with <b>  " + ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString() + " </b><br><br> Receiver Signature<br>(Party)</td>");
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td style='text-align:right'>Issued By:<br><br><br><br> GM/AGM/MGR<br>(Product Section)</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                Print.InnerHtml = sb.ToString();
                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordPrint")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    PrintDMDetails(e.CommandArgument.ToString());
                    GetDatatableHeaderDesign();
                }
            }           
            else if (e.CommandName == "RecordPrintInvoice")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    PrintInvoiceCumBillDetails(e.CommandArgument.ToString());
                    GetDatatableHeaderDesign();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    private void PrintInvoiceCumBillDetails(string dmid)
    {
        try
        {
            dsInvo = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
                     new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
                     new string[] { "4", objdb.Office_ID(), dmid }, "dataset");

            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='3'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Delivery Note</td>");
                sb.Append("<td colspan='3'>Mode/Terms of Payment</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Supplier's Ref</td>");
                sb.Append("<td colspan='3'>Other Reference(s)</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Buyer's Order No.</td>");
                sb.Append("<td colspan='3'>Dated</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Dispatch Document No.</td>");
                sb.Append("<td colspan='2'>Delivery Note Date</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Dispatched through</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='3'>Destination</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6'>Terms of Delivery</td>");

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>S.No</td>");
                sb.Append("<td colspan='2' style='text-align:center'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td style='text-align:center'>Quantity</td>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Per</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td colspan='2'><b>" + dsInvo.Tables[0].Rows[i]["ItemName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSNCode"].ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["SupplyQty"].ToString() + "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["ItemRate"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'><b>" + dsInvo.Tables[0].Rows[i]["Amount"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TAmount = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal TCSTAX = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TCSTaxAmt"));
                decimal TCGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CentralTax"));
                decimal TSGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("StateTax"));

                decimal Total = TAmount + TCGST + TSGST + TCSTAX;
                string Amount = GenerateWordsinRs(Total.ToString());
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>" + TAmount.ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>CGST</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>SGST</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
                sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX.ToString() : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + Total.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' rowspan='2' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td rowspan='2' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='2' style='text-align:center'>Central Tax</td>");
                sb.Append("<td colspan='2' style='text-align:center'>State Tax</td>");
                sb.Append("<td rowspan='2' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'>" + dsInvo.Tables[1].Rows[i]["HSNCode"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TaxableValue"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["CGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["CentralTax"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["SGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["StateTax"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TotalTaxAmount"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString());
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td style='text-align:right'><b>" + TTaxableValue.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
                sb.Append("<td style='text-align:right'><b>" + TotalTaxAmount.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br></br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                Print.InnerHtml = sb.ToString();
                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (dsInvo != null) { dsInvo.Dispose(); }
        }

    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
}