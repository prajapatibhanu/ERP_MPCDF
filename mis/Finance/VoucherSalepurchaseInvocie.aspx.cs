using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_VoucherSalepurchaseInvocie : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
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
                    //FillPrint1();
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
            ds = objdb.ByProcedure("SpFinSalePurchasePrint", new string[] { "flag", "VoucherTx_ID" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                decimal CRAmount = 0;
                decimal DRAmount = 0;
                StringBuilder sb = new StringBuilder();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    spnbranch.InnerHtml = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    spnemail.InnerHtml = ds.Tables[0].Rows[0]["Office_Email"].ToString();
                    // OffcAddress.InnerHtml = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                    spninvno.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    spndate.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();

                    spnSupplierinvno.InnerHtml = ds.Tables[0].Rows[0]["SupplierINVNo"].ToString();
                    spnSupplierdate.InnerHtml = ds.Tables[0].Rows[0]["SupplierinvoiceDate"].ToString();

                    spnorderno.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString();
                    spnorddate.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_OrderDate"].ToString();
                    spnschemename.InnerHtml = ds.Tables[0].Rows[0]["SchemeTx_Name"].ToString();
                    spnNarration.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    spnbankname.InnerHtml = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
                    spnactno.InnerHtml = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                    spnifsccode.InnerHtml = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    spnRegNo.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString();
                    spnGSTNo.InnerHtml = ds.Tables[0].Rows[0]["Office_GstNumber"].ToString();

                    spnPANNo.InnerHtml = ds.Tables[0].Rows[0]["Office_PanNumber"].ToString();

                    spnVoucherTx_Type.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();


                    spnoffice1.InnerHtml = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    spnOffice2.InnerHtml = ds.Tables[0].Rows[0]["Office_Name"].ToString();

                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    int Count = ds.Tables[2].Rows.Count;


                    for (int i = 0; i < Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; text-align:center; line-height:1.1'>" + (i + 1).ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:left; line-height:1.1'>" + ds.Tables[2].Rows[i]["ItemName"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:center; line-height:1.1'>" + ds.Tables[2].Rows[i]["HSN_Code"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Rate"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Quantity"].ToString() + " " + ds.Tables[2].Rows[i]["UnitName"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["PER"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["CGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["SGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["IGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["TOTALAMOUNT"].ToString() + "</td>");
                        sb.Append("</tr>");

                    }

                    CRAmount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                }
                sb.Append("<tr>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1 '></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("</tr>");
                if (ds.Tables[3].Rows.Count > 0)
                {
                    int LedgerCount = ds.Tables[3].Rows.Count;
                    sb.Append("<tr>");
                    sb.Append("</tr>");
                    for (int i = 0; i < LedgerCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='PSemibold cssborder' colspan='5'  style='text-align:left'>" + ds.Tables[3].Rows[i]["Ledger_Name"].ToString() + "(" + ds.Tables[3].Rows[i]["CRDR"].ToString() + ")</td>");
                        sb.Append("<td class='PSemibold cssborder'   style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold cssborder' colspan='5'  style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    int DebtorCount = ds.Tables[1].Rows.Count;
                    string HTML = "";
                    if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "Goods Purchase Tax Free")
                    {
                        HTML += ("<b>Buy,</b>");
                    }
                    else
                    {
                        HTML += ("<b>To,</b>");
                    }
                    for (int i = 0; i < DebtorCount; i++)
                    {

                        if (ds.Tables[1].Rows[i]["Mailing_Address"].ToString() != "")
                        {
                            if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "Goods Purchase Tax Free")
                            {
                                HTML += "<br/>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + ds.Tables[1].Rows[i]["Mailing_Address"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                            }
                            else
                            {
                                HTML += "<br/>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + ds.Tables[1].Rows[i]["Mailing_Address"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                            }
                        }
                        else
                        {
                            HTML += "<br/>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                        }
                    }
                    if (ds.Tables[0].Rows[0]["SupplyTo"].ToString() !="")
                    {
                        HTML += "<b>Supply To </b>" + " " + ds.Tables[0].Rows[0]["SupplyTo"].ToString() + "";
                    }
                    if (ds.Tables[0].Rows[0]["Location"].ToString() != "")
                    {
                        HTML += "<br><b>Location </b>" + " " + ds.Tables[0].Rows[0]["Location"].ToString() + "";
                    }
                    
                    spnto.InnerHtml = HTML.ToString();




                }
                if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "Goods Purchase Tax Free")
                {
                    DRAmount = ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_CreditAmt"));
                }
                else
                {
                    DRAmount = ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_DebitAmt"));
                }
                if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "Goods Purchase Tax Free")
                {
                    CRAmount = CRAmount + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_DebitAmt"));
                    }
                else
                {
                    CRAmount = CRAmount + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_CreditAmt"));
                }

                CRAmount = decimal.Parse(ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString());
                DRAmount = decimal.Parse(ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString());
                spnAmount.InnerHtml = GenerateWordsinRs(DRAmount.ToString());
                sb.Append("<tr>");
                sb.Append("<td class='cssborder Pbold' colspan='5' style='text-align:right'>GRAND TOTAL</td>");
                //sb.Append("<td class='cssborder Pbold' colspan='6' style='text-align:center'>" + CRAmount.ToString() + "</td>");
                sb.Append("<td class='cssborder Pbold' colspan='6' style='text-align:center'>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</td>");
                
                sb.Append("</tr>");
                divitem.InnerHtml = sb.ToString();


            }
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
            b = " and " + strwords2 + " Paise Only ";
        }

        return a + b;
    }

}