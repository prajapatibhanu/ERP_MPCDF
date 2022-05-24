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
using System.Collections;

public partial class mis_DemandSupply_Rpt_Multi_MilkInvoice_UDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2, ds7, dsInvo = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["DistData"] += "";
            ViewState["PStatus"] = "";
            GetOfficeDetails();
            multiInvoice();
        }
    }
    private void multiInvoice()
    {
        try
        {
            //StringBuilder sb = new StringBuilder();
            ViewState["PStatus"] = "";
            if (Session["MultiInvoice"] != null)
            {

                List<String> MUlinvoice = new List<String>();
                MUlinvoice = (List<string>)Session["MultiInvoice"];
                foreach (string invoice in MUlinvoice)
                {
                    //ViewState["DistData"] += PrintInvoices(invoice.ToString());
                    ViewState["DistData"] += PrintInvoices(invoice.ToString());
                    //Print.InnerHtml += PrintInvoices(invoice.ToString());
                }
            }
            invoicediv.InnerHtml = ViewState["DistData"].ToString();
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
    protected string PrintInvoices(string dmid)
    {
        StringBuilder sb = new StringBuilder();
        if (objdb.Office_ID() == "6")
        {
            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                 new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
        }
        else
        {
            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                 new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
        }


        if (dsInvo.Tables[0].Rows.Count > 0)
        {

            if (ViewState["PStatus"].ToString() != "")
            {
                sb.Append("<p style='page-break-after: always'>");
            }
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
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='5'></td>");
                //sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
            }
            else
            {
                sb.Append("<td colspan='5'>Delivery Note</td>");
                sb.Append("<td colspan='6'>Mode/Terms of Payment</td>");
            }

            sb.Append("</tr>");
            sb.Append("<tr>");
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='5'></td>");
            }
            else
            {
                sb.Append("<td colspan='5'>Supplier's Ref</td>");
            }
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='6'</td>");
            }
            else
            {
                sb.Append("<td colspan='6'>Other Reference(s)</td>");
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan='5' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='5'></td>");
            }
            else
            {
                sb.Append("<td colspan='5'>Buyer's Order No.</td>");
            }
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='6'></td>");
            }
            else
            {
                sb.Append("<td colspan='6'>Dated</td>");
            }

            sb.Append("</tr>");
            sb.Append("<tr>");
            if (objdb.Office_ID() == "6")
            {
                sb.Append("<td colspan='5'>Delivery Note</td>");
            }
            else
            {
                sb.Append("<td colspan='5'>Dispatch Document No.</td>");
            }

            if (objdb.Office_ID() == "6")
            {
                sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");

            }
            else
            {
                sb.Append("<td colspan='6'>Delivery Note Date</td>");
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='5'></br></td>");
            }
            else
            {
                sb.Append("<td colspan='5'>Dispatched through</br></td>");
            }
            if (objdb.Office_ID() == "6")
            {
                //sb.Append("<td colspan='6'></td>");
            }
            else
            {
                sb.Append("<td colspan='6'>Destination</td>");
            }

            sb.Append("</tr>");
            sb.Append("<tr>");
            if (objdb.Office_ID() == "6")
            {
                sb.Append("<td colspan='11'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
            }
            else
            {
                sb.Append("<td colspan='11'>Terms of Delivery</td>");
            }


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
            string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));

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
            string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
            sb.Append("<tr>");
            sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
            sb.Append("<td colspan='7' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

            sb.Append("<td colspan='7' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  rowspan ='2' colspan='9' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

            //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
            if (objdb.Office_ID() == "6")
            {
                sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No : <b> " + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
            }
            else
            {
                sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
            }

            sb.Append("</tr>");

            sb.Append("</table>");
            //Print.InnerHtml = sb.ToString();

            //ClientScriptManager CSM = Page.ClientScript;
            //string strScript = "<script>";
            //strScript += "window.print();";

            //strScript += "</script>";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
            ViewState["PStatus"] = "1";

        }


        if (dsInvo != null) { dsInvo.Dispose(); }
        return sb.ToString();


    }
    protected void btnMultiInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            
            string strScript = "<script>";
            strScript += "window.print();";
            strScript += "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}