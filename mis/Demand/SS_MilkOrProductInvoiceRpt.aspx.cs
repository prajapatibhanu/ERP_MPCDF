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

public partial class mis_Demand_SS_MilkOrProductInvoiceRpt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2, ds7, dsInvo = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                GetOfficeDetails();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
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
    private void GetDatatableHeaderDesign1()
    {
        try
        {
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void CClear()
    {
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
                if(ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
                {
                    GridView1.Visible = true;
                    GridView2.Visible = false;
                    GetInvoiceDetails();
                     GetDatatableHeaderDesign();
                }
                else
                {
                    GridView2.Visible = true;
                    GridView1.Visible = false;
                    GetProductInvoiceDetails();
                    GetDatatableHeaderDesign1();
                }
                pnlcategoryname.InnerHtml = ddlItemCategory.SelectedItem.Text;
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
    private void GetInvoiceDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string ssid = "";
           
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "SuperStockistId", "Office_ID" },
                       new string[] { "7", fromdat, todat, objdb.GetMilkCatId(),"0", objdb.createdBy(), objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
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

    private void GetProductInvoiceDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            
           
            ds1 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "SuperStockistId", "Office_ID" },
                       new string[] { "15", fromdat, todat, objdb.GetProductCatId(), "0", objdb.createdBy(), objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView2.DataSource = ds1.Tables[0];
                GridView2.DataBind();
            }
            else
            {
                pnldata.Visible = true;
                GridView2.DataSource = null;
                GridView2.DataBind();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordPrintInvoice")
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
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordPrintInvoice")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    PrintProductInvoiceCumBillDetails(e.CommandArgument.ToString());
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
        lblMsg.Text = string.Empty;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
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
    private void PrintInvoiceCumBillDetails(string dmid)
    {
        try
        {
            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                    new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                    new string[] { "6", objdb.Office_ID(), dmid }, "dataset");

            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='6'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Delivery Note</td>");
                sb.Append("<td colspan='6'>Mode/Terms of Payment</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Supplier's Ref</td>");
                sb.Append("<td colspan='6'>Other Reference(s)</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Buyer's Order No.</td>");
                sb.Append("<td colspan='6'>Dated</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Dispatch Document No.</td>");
                sb.Append("<td colspan='6'>Delivery Note Date</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Dispatched through</br></td>");
                sb.Append("<td colspan='6'>Destination</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='11'>Terms of Delivery</td>");

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>SN</td>");
                sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Qty(In Pkt.).</td>");
                sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Inst Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Qty(In Ltr).</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Margin(In Ltr).</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Margin Amount(D)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Amount</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Amount(A)</td>");
                sb.Append("<td style='text-align:center'>Payble Amount(A-D)</td>");

                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardComm"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardAmt"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardTotalAmount"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["BillingAmount"]).ToString("0.00") + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
                decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
                decimal TAdvCardQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalAdvCardQty"));
                decimal TInstQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalInstSupplyQty"));
                decimal TAdvCardQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalAdvCardQtyInLtr"));
                decimal TAdvCardAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
                //decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardTotalAmount"));
                decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("AdvCardTotalAmount") ?? 0);
                decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
                decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
                decimal TAamt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingAmount"));
                decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                decimal tcstaxamt = ((TPaybleAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
                decimal FTPaybleAmt = TPaybleAmt + tcstaxamt;
                string Amount = GenerateWordsinRs(FTPaybleAmt.ToString());

                decimal tcstaxamt_AdvCard = ((TAdvCardTotalAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
                decimal FAdvCardwithtcstax = TAdvCardTotalAmt + tcstaxamt_AdvCard;
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TAdvCardQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TInstQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TAdvCardQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TAdvCardAmt.ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TAdvCardTotalAmt.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TAamt.ToString("0.00") + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
                sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
                sb.Append("<td colspan='7' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt_AdvCard.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("<td colspan='12' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td colspan='8' style='text-align:right'><b>" + FAdvCardwithtcstax.ToString("0.00") + "</b></td>");
                sb.Append("<td colspan='13' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='16'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td colspan='7' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='7' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
                    sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString());
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td colspan='7' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

                sb.Append("<td colspan='7' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='9' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

                //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
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
    private void PrintProductInvoiceCumBillDetails(string dmid)
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
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

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
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["BCName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
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
                sb.Append("<td colspan='2' style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
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
                sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
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
}