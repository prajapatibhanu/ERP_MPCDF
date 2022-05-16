using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;

public partial class mis_Finance_RptMultiAccountPrinting : System.Web.UI.Page
{
    DataSet ds, ds2;
    StringBuilder htmlStr = new StringBuilder();
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //FillFromDate();


                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    FillDropdown();

                    ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillFromDate()
    {
        try
        {
            ds = null;
            string firstDateOfYear = "";
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                firstDateOfYear = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(firstDateOfYear, cult).ToString("yyyy/MM/dd")));
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
                //txtToDate.Text = "01/04/" + (yy - 1).ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
                //txtToDate.Text = "01/04/" + (yy).ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinRptMultiAccountPrinting",
                   new string[] { "flag", "Office_ID" },
                   new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlVoucherType.DataSource = ds;
                ddlVoucherType.DataTextField = "VoucherName";
                ddlVoucherType.DataValueField = "VoucherType";
                ddlVoucherType.DataBind();
               // ddlVoucherType.Items.Insert(0, new ListItem("All", "0"));
                //ddlVoucherType.SelectedValue = ViewState["Office_ID"].ToString();
            }

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            fill_details();
        }

    }

    private void fill_details()
    {
        lblpagenote.Text = "";
        div_page_content.InnerHtml = "";
        string VoucherType = "";

        int totalListItem = ddlVoucherType.Items.Count;
        foreach (ListItem item in ddlVoucherType.Items)
        {
            if (item.Selected)
            {
                VoucherType += item.Value + ",";
            }
        }


        ds2 = objdb.ByProcedure("SpFinRptMultiAccountPrinting",
                   new string[] { "flag", "Office_ID", "ToDate", "FromDate", "VoucherTx_Type" },
                   new string[] { "1", ViewState["Office_ID"].ToString(), ViewState["ToDate"].ToString(), ViewState["FromDate"].ToString(), VoucherType }, "dataset");

        if (ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
        {
            string PageNote = "<p><b style='color: #607D8B;font-size: 15px;'> Total vouchers between seleted date range: </b></p>";

            int VCount = ds2.Tables[1].Rows.Count;
            for (int VLoop = 0; VLoop < VCount; VLoop++)
            {
                PageNote += "<p class='note_main'><b class='note_key'>" + ds2.Tables[1].Rows[VLoop]["VoucherTx_Type"].ToString() + " : </b>" + ds2.Tables[1].Rows[VLoop]["VoucherTxCount"].ToString() + "</p>";
            }

            PageNote += "<br/><p><button class='btn btn-flat btn-primary' onclick='window.print()'>Print</button></p><br/>";
            lblpagenote.Text = PageNote;

        }

        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        {

            /******************Add Voucher Count********************/



            /*******************************************************/

            //htmlStr.Append("<table  id='DetailGrid' class='datatable table table-bordered table-hover GridView2' style='font-family:verdana; font-size:11px; width:100%'>");
            //htmlStr.Append("<thead>");
            //htmlStr.Append("<tr>");
            //htmlStr.Append("<td>Voucher Amount</td>");
            //htmlStr.Append("<td>Voucher Date</td>");
            //htmlStr.Append("</tr>");
            //htmlStr.Append("</thead>");
            //htmlStr.Append("<tbody>");
            int RCount = ds2.Tables[0].Rows.Count;
            for (int i = 0; i < RCount; i++)
            {
                //htmlStr.Append("<tr>");
                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Amount"].ToString() + "</td>");
                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                //htmlStr.Append("</tr>");
                string VoucherTx_Type = ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString();
                int VoucherTx_ID = int.Parse(ds2.Tables[0].Rows[i]["VoucherTx_ID"].ToString());
                if (VoucherTx_Type == "Journal" || VoucherTx_Type == "Journal HO" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Receipt" || VoucherTx_Type == "CreditNote Voucher" || VoucherTx_Type == "DebitNote Voucher" || VoucherTx_Type == "Money Receipt")
                {
                    fill_JournalHo(VoucherTx_ID);

                }
                else if (VoucherTx_Type == "Payment" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment" || VoucherTx_Type == "Contra")
                {
                    fill_PaymentContra(VoucherTx_ID);
                }
                else if (VoucherTx_Type == "CashSale Voucher" || VoucherTx_Type == "CreditSale Voucher" || VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free" || VoucherTx_Type == "CC Sale Voucher" || VoucherTx_Type == "JV Sale Voucher" || VoucherTx_Type == "GST Sale Voucher" || VoucherTx_Type == "DCS Sale Voucher" || VoucherTx_Type == "Item Credit Note Voucher" || VoucherTx_Type == "Item Debit Note Voucher")
                {

                    Fill_SalePurchaseNew(VoucherTx_ID);
                }

            }



            //htmlStr.Append("</tbody>");
            //htmlStr.Append("</table>");


        }
        else
        {
            htmlStr.Append("<p><b style='color: blue;font-size: 15px;'> Voucher Not Found </b></p>");
        }
      
        div_page_content.InnerHtml = htmlStr.ToString();
    }

    private void fill_PaymentContra(int VoucherTx_ID)
    {
        string VoucherTx_Amount = "";
        string Amount = "";
        ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "2", VoucherTx_ID.ToString(), ViewState["Office_ID"].ToString() }, "dataset");

        if (ds != null)
        {
            VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
            Amount = GenerateWordsinRs(VoucherTx_Amount);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>THE M.P STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");
                htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_NameE"].ToString() + "<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name_Hindi"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");

                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</span></label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</span></label>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                htmlStr.Append(" <div class='row'>");
                htmlStr.Append("<div class='col-12 table-responsive'>");
                htmlStr.Append("<table class='table no-border'>");
                htmlStr.Append("<tbody>");

                int count = ds.Tables[1].Rows.Count;

                for (int i = 0; i < count; i++)
                {

                    //if (i == 0)
                    //{
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='lead' style='width:1px'>DEBIT</td>");
                    htmlStr.Append("<td class='small' >" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                    htmlStr.Append("<td class='small' align='center' style='width:80px'>" + ds.Tables[1].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                    htmlStr.Append("<td align='center' style='width:80px'></td>");
                    htmlStr.Append("</tr>");
                    //}
                    //else
                    //{
                    //    htmlStr.Append("<tr>");
                    //    htmlStr.Append("<td class='lead' style='width:1px'></td>");
                    //    htmlStr.Append("<td class='small'  style='width:80px'>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                    //    htmlStr.Append("<td class='small' align='center' style='width:80px'>" + ds.Tables[1].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                    //    htmlStr.Append("<td align='center' style='width:80px'></td>");
                    //    htmlStr.Append("</tr>");
                    //}


                }
                int count1 = ds.Tables[2].Rows.Count;
                for (int i = 0; i < count1; i++)
                {

                    //if (i == 0)
                    //{
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='lead' style='width:10px'>CREDIT</td>");
                    htmlStr.Append("<td class='small' >" + ds.Tables[2].Rows[i]["Ledger_Name"].ToString() + "</td>");
                    htmlStr.Append("<td  align='center' style='width:80px'></td>");
                    htmlStr.Append("<td align='center' class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                    htmlStr.Append("</tr>");
                    //}
                    //else
                    //{
                    //    htmlStr.Append("<tr>");
                    //    htmlStr.Append("<td class='lead' style='width:10px'></td>");
                    //    htmlStr.Append("<td class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["Ledger_Name"].ToString() + "</td>");
                    //    htmlStr.Append("<td  align='center' style='width:80px'></td>");
                    //    htmlStr.Append("<td align='center' class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                    //    htmlStr.Append("</tr>");
                    //}

                }
                if (ds.Tables[4].Rows.Count > 0)
                {

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='lead' style='width:10px'>PAY TO&nbsp;&nbsp;<span class='small'>" + ds.Tables[4].Rows[0]["SupplierName"].ToString() + "</span></td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("</tr>");
                }
                else
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='lead' style='width:10px'>PAY TO</td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("</tr>");
                }

                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='lead' style='width:10px'>PARTICULARS</td>");
                htmlStr.Append("<td class='lead' style='width:80px; text-align:left;'></td>");
                htmlStr.Append("<td style='width:80px'></td>");
                htmlStr.Append("<td class='lead' align='center' style='width:80px'>AMOUNT</td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td colspan='3' ><span class='lead'>Rs:&nbsp;&nbsp;</span><span class='small'>" + Amount + "</span></td>");

                htmlStr.Append("<td style='border-left: 1px solid black' class='small' align='center' rowspan='2'>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</td>");

                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='small' colspan='3'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</td>");
                //htmlStr.Append("<td  style='border-left: 1px solid black' class='small' align='center'>" "</td>");                  
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");

                if (ds.Tables[3].Rows.Count > 0)
                {
                    htmlStr.Append("<td style='width:10px' class='lead'>INSTRUMENT NO..&nbsp;&nbsp;");
                    int cheqcount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < cheqcount; i++)
                    {
                        htmlStr.Append("<span class='small'>" + ds.Tables[3].Rows[i]["ChequeTx_No"].ToString() + "</span>&nbsp&nbsp");

                    }
                    htmlStr.Append("</td>");
                    htmlStr.Append("<td class='lead' style='width:80px' align='center'>DATE&nbsp;&nbsp;");
                    for (int i = 0; i < cheqcount; i++)
                    {
                        htmlStr.Append("<span class='small'>" + ds.Tables[3].Rows[i]["ChequeTx_Date"].ToString() + "</span>&nbsp&nbsp");
                    }
                    htmlStr.Append("</td>");
                }
                else
                {
                    htmlStr.Append("<td style='width:10px' class='lead'>INSTRUMENT NO.</td>");
                    htmlStr.Append("<td class='lead' style='width:80px' align='center'>DATE</td>");
                }

                htmlStr.Append("<td class='lead' style='width:80px' align='right'>TOTAL</td>");
                htmlStr.Append("<td style='border-left: 1px solid black' class='small' align='center'>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr >");
                htmlStr.Append("<td style='padding-top:100px;' class='small'>Sr.Accountant</td>");
                if (ViewState["Office_ID"].ToString() == "1")
                {

                    htmlStr.Append("<td style='padding-top:100px;' class='small'>Sr.Accountant</td>");
                }
                else
                {
                    htmlStr.Append("<td style='padding-top:100px;' class='small'>AGM / Manager</td>");
                }
                //htmlStr.Append("<td style='padding-top:100px;' class='small' >AGM / Manager</td>");

                htmlStr.Append("<td colspan='2' class='small' style='padding-top:100px;' align='right'>Signature (Name of Receiver)</td>");

                htmlStr.Append("</tr>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div><p style='page-break-before: always'>");
            }
        }

    }
    private void fill_JournalHo(int VoucherTx_ID)
    {
        //htmlStr.Append(VoucherTx_ID);
        string VoucherTx_Amount = "";
        string Amount = "";
        ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "1", VoucherTx_ID.ToString(), ViewState["Office_ID"].ToString() }, "dataset");

        if (ds != null)
        {
            VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
            Amount = GenerateWordsinRs(VoucherTx_Amount);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>THE M.P STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");
                htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_NameE"].ToString() + "<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name_Hindi"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");

                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                htmlStr.Append("<div class=''>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</span></label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</span></label>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
            }
            if (ds.Tables[1].Rows.Count > 0)
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

                for (int i = 0; i < count; i++)
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
        htmlStr.Append("<label class='lead'>NARRATION:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</span></label>");
        htmlStr.Append("</div>");
        htmlStr.Append("</div>");
        htmlStr.Append("<div class='row'>");
        htmlStr.Append("<div class='col-12'>");
        htmlStr.Append("<label class='lead'>AMOUNT (IN WORDS):&nbsp;&nbsp;&nbsp;<span class='small'>" + Amount + "</span></label>");
        htmlStr.Append("</div>");
        htmlStr.Append("</div>");
        htmlStr.Append("<div class='invoice p-3 mb-3'>");
        htmlStr.Append("<div class='row' style='padding-top:80px;'>");
        htmlStr.Append("<div class='col-12'>");
      
        //htmlStr.Append("<span class='lead' style='padding-right:50px;'>Sr. Accountant</span><span class='lead' style='text-center'>AGM / Manager </span><span class='float-right lead'>Name & Sign of Receiver</span>");
        if (ViewState["Office_ID"].ToString() == "1")
        {

            htmlStr.Append("<span class='lead' style='padding-right:50px;'>Sr. Accountant</span><span class='lead' style='text-center'></span><span class='float-right lead'>Sr. Accountant</span>");


        }
        else
        {
            if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "RECEIPT")
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
        htmlStr.Append("</div><p style='page-break-before: always'>");
    }
    protected void Fill_SalePurchase(int VoucherTx_ID)
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSalePurchasePrint", new string[] { "flag", "VoucherTx_ID" }, new string[] { "0", VoucherTx_ID.ToString() }, "dataset");
            if (ds != null)
            {
                decimal CRAmount = 0;
                decimal DRAmount = 0;
                StringBuilder sb = new StringBuilder();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    htmlStr.Append("<table  style='width: 100%; margin-left: 15px; margin: auto;'>");
                    htmlStr.Append("<tbody><tr>");
                    htmlStr.Append("<td colspan='5' style='border-top: 1px solid black; border-left: 1px solid black; border-right: 1px solid black; width: 70%' class='Pbold'>");
                    htmlStr.Append("<div class='image_section' style='float: left; width: 20%'>");
                    htmlStr.Append("<img class='pull-left' src='../image/sanchi_logo_blue.png' style='margin-top: 1px;'>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("<div class='content_section' style='float: right; width: 79%'>");
                    htmlStr.Append("<span id='spnbranch' class='Pbold' style='font-size: 18px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span>");
                    htmlStr.Append("<br>");

                    htmlStr.Append("Email:&nbsp;&nbsp;<span id='spnemail'>" + ds.Tables[0].Rows[0]["Office_Email"].ToString() + "</span><br>");
                    htmlStr.Append("CIN No.");
                    htmlStr.Append("<br>");
                    htmlStr.Append("GSTIN - &nbsp;<span id='spnGSTNo'>" + ds.Tables[0].Rows[0]["Office_GstNumber"].ToString() + "</span><br>");
                    htmlStr.Append("PAN No. - &nbsp;<span id='spnPANNo'>" + ds.Tables[0].Rows[0]["Office_PanNumber"].ToString() + "</span><br>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("<td colspan='6' style='border-top: 1px solid black; border-left: 1px solid black; border-right: 1px solid black; width: 30%; padding-bottom: 70px;' rowspan='2' class='Pbold'>To,<br>");


                    string HTML = "";
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        int DebtorCount = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < DebtorCount; i++)
                        {
                            if (ds.Tables[1].Rows[i]["Mailing_Address"].ToString() != "")
                            {
                                HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + ds.Tables[1].Rows[i]["Mailing_Address"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                            }
                            else
                            {
                                HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                            }
                        }

                    }
                    htmlStr.Append("<span id='spnto' class='PSemibold'>" + HTML.ToString() + "</span></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' class='Pbold' style='border-bottom: 1px solid black; border-left: 1px solid black;'>Invoice No.&nbsp;&nbsp;<span id='spninvno' class='Pbold'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</span><br>");
                    htmlStr.Append("Voucher Name:&nbsp;&nbsp;<span id='spnVoucherTx_Type' class='Pbold'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "</span><br>");
                    htmlStr.Append("Supplier's Invoice No.&nbsp;&nbsp;<span id='spnSupplierinvno' class='Pbold'>" + ds.Tables[0].Rows[0]["SupplierINVNo"].ToString() + "</span>");

                    htmlStr.Append("</td>");
                    htmlStr.Append("<td colspan='2' class='Pbold' style='border-bottom: 1px solid black; border-right: 1px solid black;'>DATE&nbsp;&nbsp;<span id='spndate' class='Pbold'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</span><br>");
                    htmlStr.Append("Supplier's Invoice Date&nbsp;&nbsp;<span id='spnSupplierdate' class='Pbold'>" + ds.Tables[0].Rows[0]["SupplierinvoiceDate"].ToString() + "</span>");

                    htmlStr.Append("</td>");
                    // htmlStr.Append("<td colspan='6'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' class='Pbold' style='border-top: 1px solid black; border-left: 1px solid black;'>ORDER No.&nbsp;&nbsp;<span id='spnorderno' class='PSemibold'>" + ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='2' class='Pbold' style='border-top: 1px solid black;'>DATED&nbsp;&nbsp;<span id='spnorddate' class='PSemibold'>" + ds.Tables[0].Rows[0]["VoucherTx_OrderDate"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='6' class='Pbold' style='border-top: 1px solid black; border-right: 1px solid black;'>GR/RR No:&nbsp;&nbsp;<span id='spngrrrno' class='PSemibold'></span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='5' class='Pbold' style='border-left: 1px solid black;'>NAME OF TRANSPORTER.</td>");
                    htmlStr.Append("<td colspan='6' class='Pbold' style='border-right: 1px solid black;'>FREIGHT PAID/TO PAY Rs.</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='5' class='Pbold' style='border-left: 1px solid black; border-bottom: 1px solid black;'>NAME OF THE SCHEME&nbsp;&nbsp;<span id='spnschemename' class='PSemibold'>" + ds.Tables[0].Rows[0]["SchemeTx_Name"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='6' class='Pbold' style='border-right: 1px solid black; border-bottom: 1px solid black;'>Registration No&nbsp;&nbsp;<span id='spnRegNo' class='PSemibold'>" + ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    // OffcAddress.InnerHtml = ds.Tables[0].Rows[0]["Office_Address"].ToString();


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>S.No.</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>NAME OF THE ITEM</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>HSN/SAC CODE</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>BASIC RATE</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>QTY</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>AMOUNT</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center' colspan='4'>GST DETAILS</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='padding: 5px; text-align: center' rowspan='2'>TOTAL AMOUNT</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");

                    htmlStr.Append("<td class='cssborder Pbold'>%</td>");
                    htmlStr.Append("<td class='cssborder Pbold'>CGST</td>");
                    htmlStr.Append("<td class='cssborder Pbold'>SGST</td>");
                    htmlStr.Append("<td class='cssborder Pbold'>IGST</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>1</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>2</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>3</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>4</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>5</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>6(4 * 5)</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>7</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>8</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>9</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>10</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>11(6+8+9+10)</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr></tr>");
                    //htmlStr.Append("<tr><td class='PSemibold' style='border-left:1px solid black; text-align:center; line-height:1.1'>1</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:left; line-height:1.1'>Smart Dtm 500 Ml</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:center; line-height:1.1'>04011000</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>21.25</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>246.00</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>5227.50</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>0.00</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>0.00</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>0.00</td><td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>0.00</td><td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1.1'>5227.50</td></tr><tr><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1 '></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td><td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td></tr><tr></tr><tr><td class='PSemibold cssborder' colspan='5' style='text-align:left'>Round Off(Cr)</td><td class='PSemibold cssborder' style='border-left:1px solid black; text-align:right'>0.50</td><td class='PSemibold cssborder' colspan='5' style='border-left:1px solid black; text-align:right'></td></tr><tr><td class='PSemibold cssborder' colspan='5' style='text-align:left'>Dist.Hospital,Satna(Civil Surzan)(Dr)</td><td class='PSemibold cssborder' style='border-left:1px solid black; text-align:right'></td><td class='PSemibold cssborder' colspan='5' style='border-left:1px solid black; text-align:right'>5228.00</td></tr>");

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        int Count = ds.Tables[2].Rows.Count;


                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; text-align:center; line-height:1.1'>" + (i + 1).ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:left; line-height:1.1'>" + ds.Tables[2].Rows[i]["ItemName"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:center; line-height:1.1'>" + ds.Tables[2].Rows[i]["HSN_Code"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Rate"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Quantity"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["PER"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["CGSTAmt"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["SGSTAmt"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["IGSTAmt"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["TOTALAMOUNT"].ToString() + "</td>");
                            htmlStr.Append("</tr>");

                        }

                        CRAmount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    }
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1 '></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("</tr>");
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        int LedgerCount = ds.Tables[3].Rows.Count;
                        htmlStr.Append("<tr>");
                        htmlStr.Append("</tr>");
                        for (int i = 0; i < LedgerCount; i++)
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='PSemibold cssborder' colspan='5'  style='text-align:left'>" + ds.Tables[3].Rows[i]["Ledger_Name"].ToString() + "(" + ds.Tables[3].Rows[i]["CRDR"].ToString() + ")</td>");
                            htmlStr.Append("<td class='PSemibold cssborder'   style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold cssborder' colspan='5'  style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                            htmlStr.Append("</td>");
                            htmlStr.Append("</tr>");

                        }

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


                    htmlStr.Append("<tr><td class='cssborder Pbold' colspan='5' style='text-align:right'>GRAND TOTAL</td>");
                    htmlStr.Append("<td class='cssborder Pbold' colspan='6' style='text-align:center'>" + CRAmount.ToString() + "</td></tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='2' rowspan='2' style='text-align: center;' class='cssborder Pbold'>E &amp; O.E. ACCEPTED</td>");
                    htmlStr.Append("<td colspan='8' style='text-align: center;' class='cssborder Pbold'>LESS: ADVANCE RECEIVED (IF ANY)</td>");
                    htmlStr.Append("<td class='cssborder'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='8' style='text-align: center;' class='cssborder Pbold'>NET AMOUNT DUE</td>");
                    htmlStr.Append("<td class='cssborder'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='11' class='cssborder Pbold'>Amount in words&nbsp;&nbsp;<span id='spnAmount' class='PSemibold'>" + GenerateWordsinRs(DRAmount.ToString()) + " </span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='11' class='cssborder Pbold'>Remark&nbsp;&nbsp;<span id='spnNarration' class='PSemibold'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='11' class='cssborder Pbold'>Cheques/Draft Should be in the name of THE ");

                    htmlStr.Append("<span id='spnoffice1'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span>");
                    htmlStr.Append("<br>");
                    htmlStr.Append("PAYABLE AT</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;'>Name of the Bank&nbsp;&nbsp;<span id='spnbankname' class='PSemibold'>" + ds.Tables[0].Rows[0]["Bank_Name"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='7' class='Pbold' style='border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black; padding-bottom: 30px; width: 70%' rowspan='3'>For: ");


                    htmlStr.Append("<span id='spnOffice2'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-right: 1px solid black; border-left: 1px solid black;'>A/c No.&nbsp;&nbsp;<span id='spnactno' class='PSemibold'>" + ds.Tables[0].Rows[0]["Acnt_No"].ToString() + "</span></td>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-right: 1px solid black; border-left: 1px solid black;'>IFSC Code&nbsp;&nbsp;<span id='spnifsccode' class='PSemibold'>" + ds.Tables[0].Rows[0]["IFSC"].ToString() + "</span></td>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-top: 1px solid black; border-bottom: 1px solid black; border-left: 1px solid black; padding-top: 50px;'>Receiver's Signature</td>");
                    htmlStr.Append("<td colspan='7' class='Pbold' style='border-bottom: 1px solid black; border-right: 1px solid black; padding-top: 50px; width: 70%'>AUTHORISED SIGNATORY(Designation)</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</tbody></table>");

                    htmlStr.Append("<p style='page-break-before: always'>");
                }

            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Fill_SalePurchaseNew(int VoucherTx_ID)
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSalePurchasePrint", new string[] { "flag", "VoucherTx_ID" }, new string[] { "0", VoucherTx_ID.ToString() }, "dataset");
            if (ds != null)
            {
                decimal CRAmount = 0;
                decimal DRAmount = 0;
                StringBuilder sb = new StringBuilder();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    htmlStr.Append("<div style='width: 100%; margin-left: 15px; margin: auto;'>");
                    htmlStr.Append("<section class='content'>");
                   // htmlStr.Append("<div class='invoice'>");
                    htmlStr.Append("<div class='row'>");
                  //  htmlStr.Append("<div class='col-md-2'></div>");
                    htmlStr.Append("<div class='col-md-12'>");
                    htmlStr.Append("<table style='font-size: 11.5px;'>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='5' style='border-top: 1px solid black; border-left: 1px solid black; border-right: 1px solid black; width: 70%' class=''>");
                    htmlStr.Append("<div class='image_section' style='float: left; width: 20%'>");
                    htmlStr.Append("<img class='pull-left' src='../image/sanchi_logo_blue.png' style='margin-top: 1px;' />");
                    htmlStr.Append("</div>");
                    htmlStr.Append("<div class='content_section' style='float: right; width: 79%'>");
                    htmlStr.Append("<span id='spnbranch' class='Pbold' style='font-size: 18px;' runat='server'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span>");
                    htmlStr.Append("<br />");
                    htmlStr.Append("<b>Email:</b>&nbsp;&nbsp;<span id='spnemail' runat='server'>" + ds.Tables[0].Rows[0]["Office_Email"].ToString() + "</span><br />");
                    htmlStr.Append("<b>CIN No.</b>");
                    htmlStr.Append("<br />");
                    htmlStr.Append("<b>GSTIN - </b>&nbsp;<span id='spnGSTNo' runat='server'>" + ds.Tables[0].Rows[0]["Office_GstNumber"].ToString() + "</span><br />");
                    htmlStr.Append("<b>PAN No. - </b>&nbsp;<span id='spnPANNo' runat='server'>" + ds.Tables[0].Rows[0]["Office_PanNumber"].ToString() + "</span><br />");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("<td colspan='6' style='border-top: 1px solid black; border-left: 1px solid black; border-right: 1px solid black; width: 30%; padding-bottom: 70px;' rowspan='2' ><b>To,</b><br />");

                    string HTML = "";
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        int DebtorCount = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < DebtorCount; i++)
                        {
                            if (ds.Tables[1].Rows[i]["Mailing_Address"].ToString() != "")
                            {
                                HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + ds.Tables[1].Rows[i]["Mailing_Address"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                            }
                            else
                            {
                                HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                            }
                        }

                    }
                 

                    htmlStr.Append("<span id='spnto' runat='server' class='PSemibold'>" + HTML.ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' class='Pbold' style='border-bottom: 1px solid black; border-left: 1px solid black;'>Invoice No.&nbsp;&nbsp;<span id='spninvno' class='Pbold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</span><br />");
                    htmlStr.Append("Voucher Name:&nbsp;&nbsp;<span id='spnVoucherTx_Type' class='Pbold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "</span><br />");
                    htmlStr.Append("Supplier's Invoice No.&nbsp;&nbsp;<span id='spnSupplierinvno' class='Pbold' runat='server'>" + ds.Tables[0].Rows[0]["SupplierINVNo"].ToString() + "</span>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("<td colspan='2' class='Pbold' style='border-bottom: 1px solid black; border-right: 1px solid black;'>DATE&nbsp;&nbsp;<span id='spndate' class='Pbold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</span><br />");
                    htmlStr.Append("Supplier's Invoice Date&nbsp;&nbsp;<span id='spnSupplierdate' class='Pbold' runat='server'>" + ds.Tables[0].Rows[0]["SupplierinvoiceDate"].ToString() + "</span>");
                    htmlStr.Append("</td>");
                  //  htmlStr.Append("<td colspan='6'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' class='Pbold' style='border-top: 1px solid black; border-left: 1px solid black;'>ORDER No.&nbsp;&nbsp;<span id='spnorderno' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='2' class='Pbold' style='border-top: 1px solid black;'>DATED&nbsp;&nbsp;<span id='spnorddate' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_OrderDate"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='6' class='Pbold' style='border-top: 1px solid black; border-right: 1px solid black;'>GR/RR No:&nbsp;&nbsp;<span id='spngrrrno' class='PSemibold' runat='server'></span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='5' class='Pbold' style='border-left: 1px solid black;'>NAME OF TRANSPORTER.</td>");
                    htmlStr.Append("<td colspan='6' class='Pbold' style='border-right: 1px solid black;'>FREIGHT PAID/TO PAY Rs.</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='5' class='Pbold' style='border-left: 1px solid black; border-bottom: 1px solid black;'>NAME OF THE SCHEME&nbsp;&nbsp;<span id='spnschemename' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["SchemeTx_Name"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='6' class='Pbold' style='border-right: 1px solid black; border-bottom: 1px solid black;'>Registration No&nbsp;&nbsp;<span id='spnRegNo' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>S.No.</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>NAME OF THE ITEM</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>HSN/SAC CODE</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>BASIC RATE</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>QTY</td>");
                    htmlStr.Append("<td class='cssborder Pbold' rowspan='2' style='text-align: center'>AMOUNT</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center' colspan='4'>GST DETAILS</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='padding: 5px; text-align: center' rowspan='2'>TOTAL AMOUNT</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='cssborder Pbold'>%</td>");
                    htmlStr.Append("<td class='cssborder Pbold'>CGST</td>");
                    htmlStr.Append("<td class='cssborder Pbold'>SGST</td>");
                    htmlStr.Append("<td class='cssborder Pbold'>IGST</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>1</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>2</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>3</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>4</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>5</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>6(4 * 5)</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>7</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>8</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>9</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>10</td>");
                    htmlStr.Append("<td class='cssborder Pbold' style='text-align: center;'>11(6+8+9+10)</td>");
                    htmlStr.Append("</tr>");

                    //htmlStr.Append("<tr>");
                    //htmlStr.Append("<div id='divitem' runat='server'>");
                    //htmlStr.Append("</div>");
                    //htmlStr.Append("</tr>");

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        int Count = ds.Tables[2].Rows.Count;


                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; text-align:center; line-height:1.1'>" + (i + 1).ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:left; line-height:1.1'>" + ds.Tables[2].Rows[i]["ItemName"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:center; line-height:1.1'>" + ds.Tables[2].Rows[i]["HSN_Code"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Rate"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Quantity"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["PER"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["CGSTAmt"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["SGSTAmt"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["IGSTAmt"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["TOTALAMOUNT"].ToString() + "</td>");
                            htmlStr.Append("</tr>");

                        }

                        CRAmount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    }
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1 '></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                    htmlStr.Append("</tr>");
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        int LedgerCount = ds.Tables[3].Rows.Count;
                        htmlStr.Append("<tr>");
                        htmlStr.Append("</tr>");
                        for (int i = 0; i < LedgerCount; i++)
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='PSemibold cssborder' colspan='5'  style='text-align:left'>" + ds.Tables[3].Rows[i]["Ledger_Name"].ToString() + "(" + ds.Tables[3].Rows[i]["CRDR"].ToString() + ")</td>");
                            htmlStr.Append("<td class='PSemibold cssborder'   style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                            htmlStr.Append("<td class='PSemibold cssborder' colspan='5'  style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                            htmlStr.Append("</td>");
                            htmlStr.Append("</tr>");

                        }

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

                    htmlStr.Append("<tr><td class='cssborder Pbold' colspan='5' style='text-align:right'>GRAND TOTAL</td>");
                    htmlStr.Append("<td class='cssborder Pbold' colspan='6' style='text-align:center'>" + CRAmount.ToString() + "</td></tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='2' rowspan='2' style='text-align: center;' class='cssborder Pbold'>E & O.E. ACCEPTED</td>");
                    htmlStr.Append("<td colspan='8' style='text-align: center;' class='cssborder Pbold'>LESS: ADVANCE RECEIVED (IF ANY)</td>");
                    htmlStr.Append("<td class='cssborder'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='8' style='text-align: center;' class='cssborder Pbold'>NET AMOUNT DUE</td>");
                    htmlStr.Append("<td class='cssborder'></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='11' class='cssborder Pbold'>Amount in words&nbsp;&nbsp;<span id='spnAmount' class='PSemibold' runat='server'>" + GenerateWordsinRs(DRAmount.ToString()) + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='11' class='cssborder Pbold'>Remark&nbsp;&nbsp;<span id='spnNarration' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='11' class='cssborder Pbold'>Cheques/Draft Should be in the name of THE ");
                    htmlStr.Append("<span id='spnoffice1' runat='server'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span>");
                    htmlStr.Append("<br />");
                    htmlStr.Append("PAYABLE AT</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;'>Name of the Bank&nbsp;&nbsp;<span id='spnbankname' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["Bank_Name"].ToString() + "</span></td>");
                    htmlStr.Append("<td colspan='7' class='Pbold' style='border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black; padding-bottom: 30px; width: 70%' rowspan='3'>For: ");
                    htmlStr.Append("<span id='spnOffice2' runat='server'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-right: 1px solid black; border-left: 1px solid black;'>A/c No.&nbsp;&nbsp;<span id='spnactno' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["Acnt_No"].ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-right: 1px solid black; border-left: 1px solid black;'>IFSC Code&nbsp;&nbsp;<span id='spnifsccode' class='PSemibold' runat='server'>" + ds.Tables[0].Rows[0]["IFSC"].ToString() + "</span></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='4' class='Pbold' style='border-top: 1px solid black; border-bottom: 1px solid black; border-left: 1px solid black; padding-top: 50px;'>Receiver's Signature</td>");
                    htmlStr.Append("<td colspan='7' class='Pbold' style='border-bottom: 1px solid black; border-right: 1px solid black; padding-top: 50px; width: 70%'>AUTHORISED SIGNATORY(Designation)</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</table>");
                    htmlStr.Append("</div><div class='col-md-2'></div>");
                    htmlStr.Append("</div>");
                    //htmlStr.Append("</div>");
                   // htmlStr.Append("<div id='DivTable' runat='server'></div>");


                    htmlStr.Append("</section>");
                    htmlStr.Append("<div class='clearfix'></div>");
                    htmlStr.Append("</div>");

                    htmlStr.Append("<p style='page-break-before: always'>");
                }

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
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
}