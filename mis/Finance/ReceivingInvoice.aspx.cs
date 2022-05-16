using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_ReceivingInvoice : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, Dset;
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //["VoucherTx_ID"] = "646";
                    //FillPrint();
                    ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                    if (Request.QueryString["VoucherTx_ID"] != null)
                    {
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        FillPrint();
                    }

                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPrint()
    {
        try
        {
            //lblMsg.Text = "";
            StringBuilder htmlStr = new StringBuilder();
            string LedgerTx_Amount = "";
            string Amount = "";
            //htmlStr.Append("<div class='invoice p-3 mb-3'>");
            //htmlStr.Append("<div class='row'>");
            //htmlStr.Append("<div class='col-12'>");
            //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:28px;'>Madhya Pradesh State Cooperative Dairy Federation Ltd.<br /><span style='font-size:17px;'>BHOPAL</span><br /><span style='font-size:20px;'>JOURNAL VOUCHER</span> </h2>");

            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");

            ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID"}, new string[] { "3", ViewState["VoucherTx_ID"].ToString()}, "dataset");

            if (ds != null)
            {
                LedgerTx_Amount = ds.Tables[0].Rows[0]["LedgerTx_Amount"].ToString();
                Amount = GenerateWordsinRs(LedgerTx_Amount);
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    
                    htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-12'>");
                    //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>Madhya Pradesh State Cooperative Dairy Federation Ltd.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + VoucherType.ToString() + "  VOUCHER</span> </h2>");
                    
                    //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>" + ds.Tables[1].Rows[0]["Office_Name"].ToString() + "<br /><span style='font-size:17px;'>Dairy Plant</span><br /><br />");

                    htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>" + ViewState["Office_FinAddress"].ToString() + "<br /><br />");
                        //htmlStr.Append("<span style='font-size:17px;'>" + ds.Tables[1].Rows[0]["Office_Address"].ToString() + "</span> <br /><span style='font-size:17px;'><u>State Name: Madhya Pradesh, Code :23</u></span></h2>");
                    htmlStr.Append("<span style='font-size:17px;'><table  style='Width:100%;'><tr><td style='Width:50%; text-align:left;'>MR. No. :  " + ds.Tables[1].Rows[0]["VoucherTx_No"].ToString() + "</td><td style='Width:50%; text-align:right;'>Date : " + ds.Tables[1].Rows[0]["VoucherTx_Date"].ToString() + "</td></tr></table></span> </h2>");

                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'><u>Money Receipt</u></h2>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                }
                
                    htmlStr.Append(" <div class='row'>");
                    htmlStr.Append("<div class='col-12 table-responsive'>");
                    htmlStr.Append(" <table class='table' style='border:none'>");                   
                    htmlStr.Append("<tbody>");

                   // int count = ds.Tables[1].Rows.Count;                    
                     htmlStr.Append("<tr>");
                     htmlStr.Append("<td class='text-left small'>Received with thanks from:</td>");
                     htmlStr.Append("<td class='text-left small'>" + ds.Tables[0].Rows[0]["Ledger_Name"].ToString() + "</td>");                   
                     htmlStr.Append("</tr>");

                     htmlStr.Append("<tr>");
                     htmlStr.Append("<td class='text-left small'>The Sum of:</td>");
                     htmlStr.Append("<td class='text-left small'>" + Amount + "</td>");
                     htmlStr.Append("</tr>");

                   
                     htmlStr.Append("<tr>");
                     htmlStr.Append("<td class='text-left small'>Remarks:</td>");
                     htmlStr.Append("<td class='text-left small'>" + ds.Tables[1].Rows[0]["VoucherTx_Narration"].ToString() + "</td>");
                     htmlStr.Append("</tr>");


                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
              
            }
           
            htmlStr.Append("<div class='invoice p-3 mb-3'>");
            htmlStr.Append("<div class='row' style='padding-top:80px;'>");
            htmlStr.Append("<div class='col-12'>");
            htmlStr.Append("<span class='lead' style='padding-right:50px;'><u>**Rs. " + LedgerTx_Amount + " /-</span></u><div class='float-right lead'>Authorised Signatory<span style='padding-right: 100px;'>&nbsp;</span>AGM / Manager</div><br><span class='lead' style='text-left'>**Subject to Realisation</span>");
            htmlStr.Append("</div>");
            htmlStr.Append("</div>");
            htmlStr.Append("</div>");





            DivTable.InnerHtml = htmlStr.ToString();

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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