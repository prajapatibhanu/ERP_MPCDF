using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_VoucherJournalInvoice : System.Web.UI.Page
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
            string VoucherTx_Amount = "";
            string Amount = "";
            //htmlStr.Append("<div class='invoice p-3 mb-3'>");
            //htmlStr.Append("<div class='row'>");
            //htmlStr.Append("<div class='col-12'>");
            //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:28px;'>Madhya Pradesh State Cooperative Dairy Federation Ltd.<br /><span style='font-size:17px;'>BHOPAL</span><br /><span style='font-size:20px;'>JOURNAL VOUCHER</span> </h2>");
            
            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");
            string VoucherTx_NameOfReceipt = "";
            string VoucherType = "";
            ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            
            if(ds!= null)
            {
                VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                VoucherTx_NameOfReceipt = ds.Tables[0].Rows[0]["VoucherTx_NameOfReceipt"].ToString();
                Amount = GenerateWordsinRs(VoucherTx_Amount);
                if(ds.Tables[0].Rows.Count> 0)
                {
                     VoucherType = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                    if (VoucherType == "CREDITNOTE VOUCHER")
                    {
                        VoucherType = "CREDITNOTE";
                    }
                    else if (VoucherType == "DEBITNOTE VOUCHER")
                    {
                        VoucherType = "DEBITNOTE";
                    }
                    else
                    {

                    }
                    htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>" + ds.Tables[0].Rows[0]["Office_NameE"].ToString() + "<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name_Hindi"].ToString() + "</span><br /><span style='font-size:17px;'>" + VoucherType.ToString() + "  VOUCHER</span> </h2>");

                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</label>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                }
                if(ds.Tables[1].Rows.Count> 0)
                {
                    htmlStr.Append(" <div class='row'>");
                    htmlStr.Append("<div class='col-12 table-responsive'>");
                    htmlStr.Append(" <table class='table'>");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr >");
                    htmlStr.Append("<th class='text-center lead' style='border-bottom:1px solid black'>PARTICLARS</th>");
                    htmlStr.Append("<th class='text-center lead' style='border-bottom:1px solid black'>DEBIT</th>");
                    htmlStr.Append("<th class='text-center lead' style='border-bottom:1px solid black'>CREDIT</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");
                    
                    int count = ds.Tables[1].Rows.Count;

                    for (int i = 0; i < count;i++)
                    {

                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='text-center small'>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        htmlStr.Append("<td class='text-center small'>" + ds.Tables[1].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                        htmlStr.Append("<td class='text-center small'>" + ds.Tables[1].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                        htmlStr.Append("</tr>");
                    }
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td align='right' class='lead' >TOTAL</td>");
                    htmlStr.Append("<td class='text-center small'>" + VoucherTx_Amount + "</td>");
                    htmlStr.Append("<td class='text-center small'>" + VoucherTx_Amount + "</td>");
                    htmlStr.Append("</tr>");
                    
                    
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                }
            }


            htmlStr.Append("<div class='row'>");
            htmlStr.Append("<div class='col-12'>");
            htmlStr.Append("<label class='lead'>AMOUNT (IN WORDS) :&nbsp;&nbsp;&nbsp;<span class='small'>" + Amount + "</span></label>");
            htmlStr.Append("</div>");
            htmlStr.Append("</div>");

            if (VoucherTx_NameOfReceipt != "")
            {
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<label class='lead'>NAME OF RECEIPT :&nbsp;&nbsp;&nbsp;<span class='small'>" + VoucherTx_NameOfReceipt + "</span></label>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
            }

            
            htmlStr.Append("<div class='row'>");
            htmlStr.Append("<div class='col-12'>");
            htmlStr.Append("<label class='lead'>NARRATION :&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</span></label>");
            htmlStr.Append("</div>");
            htmlStr.Append("</div>");
            
            htmlStr.Append("<div class='invoice p-3 mb-3'>");
            htmlStr.Append("<div class='row' style='padding-top:80px;'>");
            htmlStr.Append("<div class='col-12'>");
            if (ViewState["Office_ID"].ToString() == "1")
            {
                
                    htmlStr.Append("<span class='lead' style='padding-right:50px;'>Sr. Accountant</span><span class='lead' style='text-center'></span><span class='float-right lead'>Sr. Accountant</span>");
             
                
            }
            else
            {
                if (VoucherType == "RECEIPT")
                {
                    htmlStr.Append("<table style='width: 100%; border-color: white;'><tr><td style='width:33%;border-color: white;'><span class='lead' style='padding-right:50px;'>Sr. Accountant</span></td><td style='width:33%;border-color: white;text-align: center;'><span class='lead'>AGM / Manager</span></td><td style='width:33%; border-color: white;text-align: right;padding-right: 50px;'><span class='lead'>Name & Sign of Receiver</span></td></tr></table>");
                }
                else
                {
                    htmlStr.Append("<span class='lead' style='padding-right:50px;'>Sr. Accountant</span><span class='lead' style='text-center'></span><span class='float-right lead'>AGM / Manager</span>");
                }

               
            }
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