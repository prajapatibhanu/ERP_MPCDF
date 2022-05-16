using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
public partial class mis_Finance_RptLedgerSummaryF : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                    ViewState["Office_FinAddress"] = "M.P. STATE COOPERATIVE DAIRY FEDERATION LIMITED";

                    ddlOffice.Enabled = false;
                    divExcel.Visible = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");


                    FillVoucherDate();
                    FillFromDate();

                    FillDropdown();
                    HeadList();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                        ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                    }
                    btnBack.Enabled = false;
                    btnBackN.Enabled = false;

                    chkOpeningBal.Checked = false;
                    chkDebitAmt.Checked = false;
                    chkCreditAmt.Checked = false;
                    chkClosingBal.Checked = true;

                    ViewState["DayBookVisible"] = "true";
                    btnShowDetailBook.Enabled = false;

                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillFromDate()
    {
        try
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd")));
            //String dy = datevalue.Day.ToString();
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void HeadList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Head_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("MonthID", typeof(string)));
            ViewState["Heads"] = dt;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillDropdown()
    {
        try
        {
            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice.Enabled = true;
            //}
            //ds = objdb.ByProcedure("SpFinVoucherTx",
            //       new string[] { "flag" },
            //       new string[] { "26" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    //ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
            //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //}
            //string Office = "";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}
            //if (Office != "")
            //{
            //    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "1", Office }, "dataset");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        ddlLedger.DataSource = ds1;
            //        ddlLedger.DataTextField = "Ledger_Name";
            //        ddlLedger.DataValueField = "Ledger_ID";
            //        ddlLedger.DataBind();
            //        ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //}

            DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                //ddlLedger.DataSource = ds1;
                //ddlLedger.DataTextField = "Ledger_Name";
                //ddlLedger.DataValueField = "Ledger_ID";
                //ddlLedger.DataBind();
                //ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                Session["getLedger"] = ds1;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlLedger.DataSource = null;
            ddlLedger.DataBind();
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            lblTab.Text = "";
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office != "")
            {
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "1", Office }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
            }
            //if (ddlOffice.SelectedIndex > 0)
            //{
            //    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice.SelectedValue.ToString() }, "dataset");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        ddlLedger.DataSource = ds1;
            //        ddlLedger.DataTextField = "Ledger_Name";
            //        ddlLedger.DataValueField = "Ledger_ID";
            //        ddlLedger.DataBind();
            //        ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblReportName.Text = "";
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            lblTab.Text = "";
            //if (ddlOffice.SelectedIndex >0 && ddlLedger.SelectedIndex > 0 && txtFromDate.Text !="" && txtToDate.Text !="")
            //{
            //    lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            //    FillGrid();
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            chkNarration.Checked = false;

            string LedgerId = hfLedgerID.Value;

            string OfficeID = Encrypt(ViewState["Office_ID"].ToString());
            string EditStr = Encrypt("2");
            string ViewStr = Encrypt("1");

            //string FromDate = Encrypt(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"));
            //string ToDate = Encrypt(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"));
            //string Ledger_ID = Encrypt(ddlLedger.SelectedValue.ToString());

            DivTable.InnerHtml = "";
            //string Office = "";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}
            //ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" },
            //    new string[] { "2", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" },
              new string[] { "2", ViewState["Office_ID"].ToString(), LedgerId, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            //ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" },
            //   new string[] { "2", ViewState["Office_ID"].ToString(), ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //string clsHide = "";
                //if (chkClosingBal.Checked =true)
                //    clsHide = "hidden";
                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;
                divExcel.Visible = true;
                btnBack.Enabled = true;
                btnBackN.Enabled = true;
                StringBuilder htmlStr = new StringBuilder();

                if (ViewState["Office_ID"].ToString() == "1")
                {
					htmlStr.Append("<input type='text' placeholder='Search' id='txtbox' class='search-input' data-table='customers-list'>");
                    htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered mt32 customers-list' >");
                    //htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th>Voucher Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th>Vch Type</th>");
                    htmlStr.Append("<th>Vch No.</th>");

                    //htmlStr.Append("<th>Taxable Amount</th>");
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<th>Debit Amt.</th>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<th>Credit Amt.</th>");

                    int Count = ds.Tables[0].Rows.Count;
                    decimal OpeningBalance = 0;
                    decimal TDebitAmt = 0;
                    decimal TCreditAmt = 0;
                    string AmtType = "";

                    if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                        OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                    ViewState["OpeningBalance"] = OpeningBalance.ToString();


                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                    //htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");
                    htmlStr.Append("<th style='width: 65px;'  class='hide_print'>Action</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");

                    if (chkOpeningBal.Checked == true)
                    {
                        //if (OpeningBalance >= 0)
                        //    lblReportName.Text = lblReportName.Text + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        //else
                        //    lblReportName.Text = lblReportName.Text + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";

                        if (OpeningBalance >= 0)
                        {
                            //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                            lblReportName.Text = lblReportName.Text;// +"<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;' class='hidden'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        }
                        else
                        {
                            //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                            lblReportName.Text = lblReportName.Text;// +"<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;' class='hidden'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        }


                    }
                    //if (chkOpeningBal.Checked == true)
                    //{
                    //    htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td>Opening Balance</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");


                    //    if (chkDebitAmt.Checked == true)
                    //        htmlStr.Append("<td></td>");
                    //    if (chkCreditAmt.Checked == true)
                    //        htmlStr.Append("<td></td>");
                    //    if (OpeningBalance >= 0)
                    //        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    //    else
                    //        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("</tr>");
                    //}

                    //Opening Balance Row Start
                    if (chkOpeningBal.Checked == true)
                    {
                        htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td>Opening Balance</td>");
                        htmlStr.Append("<td></td>");
                        //htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                        if (ViewState["Office_ID"].ToString() != "1")
                        {
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                        }


                        string ODr = "";
                        string OCr = "";
                        if (OpeningBalance >= 0)
                            OCr = Math.Abs(OpeningBalance).ToString();
                        else
                            ODr = Math.Abs(OpeningBalance).ToString();

                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td>" + ODr + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td>" + OCr + "</td>");

                        if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OpeningBalance >= 0)
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == true && chkClosingBal.Checked == true)
                        {
                            if (ODr != "")
                                htmlStr.Append("<td  class='align-right'>" + ODr + " Dr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == true && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OCr != "")
                                htmlStr.Append("<td  class='align-right'>" + OCr + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == false)
                        {

                        }
                        else
                        {
                            htmlStr.Append("<td  class='align-right'></td>");
                        }



                        htmlStr.Append("<td  class='hide_print'></td>");
                        htmlStr.Append("</tr>");
                    }

                    for (int i = 0; i < Count; i++)
                    {
                        decimal DebitAmt = 0;
                        decimal CreditAmt = 0;
                        decimal OpeningBal = 0;
                        decimal tempOpeningBal = 0;

                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

                        if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "<span class='Narration'><br/><b>Narration</b>\t : \t" + ds.Tables[0].Rows[i]["Narration"].ToString() + "</span></td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (ViewState["Office_ID"].ToString() != "1")
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                            }
                            //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                            htmlStr.Append("<td class='hide_print'>");
                            //   htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                            htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");
                            // htmlStr.Append("</td>");
                            htmlStr.Append("</tr>");
                        }
                        else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        {
                            decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                            if (CreditAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";

                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "<span class='Narration'><br/><b>Narration</b>\t : \t" + ds.Tables[0].Rows[i]["Narration"].ToString() + "</span></td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");

                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
                                htmlStr.Append("<td class='hide_print'>");
                                // htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");
                                // htmlStr.Append("</td>");
                                htmlStr.Append("</tr>");
                            }

                        }
                        else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        {
                            decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                            if (DebitAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";

                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "<span class='Narration'><br/><b>Narration</b>\t : \t" + ds.Tables[0].Rows[i]["Narration"].ToString() + "</span></td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");
                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
                                htmlStr.Append("<td class='hide_print'>");
                                //htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");
                                // htmlStr.Append("</td>");
                                htmlStr.Append("</tr>");
                            }
                        }

                    }


                    // htmlStr.Append("</tbody>");
                    // htmlStr.Append("<tfoot>");
                    htmlStr.Append("<tr style='font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Total :</td>");
                    htmlStr.Append("<td></td>");
                    //htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                    }

                    if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";


                    if (OpeningBalance >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                    if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                    {
                        decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                        decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                        Dr = decimal.Parse("-" + Dr.ToString());
                        OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                        if (OpeningBalance >= 0)
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                        }
                        else
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }

                    }
                    else if (chkClosingBal.Checked == true)
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                    }

                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");

                    //htmlStr.Append("</tfoot>");
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                }
                else
                {
					htmlStr.Append("<input type='text' placeholder='Search' id='txtbox' class='search-input' data-table='customers-list'>");
                    htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered mt32 customers-list' >");
                    //htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th>Voucher Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th>Vch Type</th>");
                    htmlStr.Append("<th>Vch No.</th>");
                    htmlStr.Append("<th>Supplier's Invoice No.</th>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<th>GST No.</th>");
                        htmlStr.Append("<th>Pan No.</th>");
                    }
                    htmlStr.Append("<th>Taxable Amount</th>");
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<th>Debit Amt.</th>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<th>Credit Amt.</th>");

                    int Count = ds.Tables[0].Rows.Count;
                    decimal OpeningBalance = 0;
                    decimal TDebitAmt = 0;
                    decimal TCreditAmt = 0;
                    string AmtType = "";

                    if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                        OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                    ViewState["OpeningBalance"] = OpeningBalance.ToString();


                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                    //htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");
                    htmlStr.Append("<th style='width: 65px;'  class='hide_print'>Action</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");

                    if (chkOpeningBal.Checked == true)
                    {
                        //if (OpeningBalance >= 0)
                        //    lblReportName.Text = lblReportName.Text + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        //else
                        //    lblReportName.Text = lblReportName.Text + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";

                        if (OpeningBalance >= 0)
                        {
                            //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                            lblReportName.Text = lblReportName.Text;// +"<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        }
                        else
                        {
                            //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                            lblReportName.Text = lblReportName.Text;// +"<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        }


                    }
                    //if (chkOpeningBal.Checked == true)
                    //{
                    //    htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td>Opening Balance</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");


                    //    if (chkDebitAmt.Checked == true)
                    //        htmlStr.Append("<td></td>");
                    //    if (chkCreditAmt.Checked == true)
                    //        htmlStr.Append("<td></td>");
                    //    if (OpeningBalance >= 0)
                    //        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    //    else
                    //        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("</tr>");
                    //}

                    //Opening Balance Row Start
                    if (chkOpeningBal.Checked == true)
                    {
                        htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td>Opening Balance</td>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                        if (ViewState["Office_ID"].ToString() != "1")
                        {
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                        }
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");

                        string ODr = "";
                        string OCr = "";
                        if (OpeningBalance >= 0)
                            OCr = Math.Abs(OpeningBalance).ToString();
                        else
                            ODr = Math.Abs(OpeningBalance).ToString();

                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td>" + ODr + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td>" + OCr + "</td>");

                        if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OpeningBalance >= 0)
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == true && chkClosingBal.Checked == true)
                        {
                            if (ODr != "")
                                htmlStr.Append("<td  class='align-right'>" + ODr + " Dr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == true && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OCr != "")
                                htmlStr.Append("<td  class='align-right'>" + OCr + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == false)
                        {

                        }
                        else
                        {
                            htmlStr.Append("<td  class='align-right'></td>");
                        }



                        htmlStr.Append("<td  class='hide_print'></td>");
                        htmlStr.Append("</tr>");
                    }

                    for (int i = 0; i < Count; i++)
                    {
                        decimal DebitAmt = 0;
                        decimal CreditAmt = 0;
                        decimal OpeningBal = 0;
                        decimal tempOpeningBal = 0;

                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

                        if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "<span class='Narration'><br/><b>Narration</b>\t : \t" + ds.Tables[0].Rows[i]["Narration"].ToString() + "</span></td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString() + "</td>");
                            if (ViewState["Office_ID"].ToString() != "1")
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                            htmlStr.Append("<td class='hide_print'>");
                            // htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                            htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");
                            //htmlStr.Append("</td>");
                            htmlStr.Append("</tr>");
                        }
                        else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        {
                            decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                            if (CreditAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";

                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                                // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "<span class='Narration'><br/><b>Narration</b>\t : \t" + ds.Tables[0].Rows[i]["Narration"].ToString() + "</span></td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");

                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");


                                htmlStr.Append("<td class='hide_print'>");
                                // htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");
                                //htmlStr.Append("</td>");
                                htmlStr.Append("</tr>");
                            }

                        }
                        else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        {
                            decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                            if (DebitAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";

                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                //  htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "<span class='Narration'> <br/><b>Narration</b>\t : \t" + ds.Tables[0].Rows[i]["Narration"].ToString() + "</span></td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");
                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");


                                htmlStr.Append("<td class='hide_print'>");
                                // htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");
                                //  htmlStr.Append("</td>");
                                htmlStr.Append("</tr>");
                            }
                        }

                    }


                    // htmlStr.Append("</tbody>");
                    // htmlStr.Append("<tfoot>");
                    htmlStr.Append("<tr style='font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Total :</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                    }
                    htmlStr.Append("<td></td>");

                    if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";

                    htmlStr.Append("<td></td>");

                    if (OpeningBalance >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                    if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                    {
                        decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                        decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                        Dr = decimal.Parse("-" + Dr.ToString());
                        OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                        if (OpeningBalance >= 0)
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                        }
                        else
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }

                    }
                    else if (chkClosingBal.Checked == true)
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                    }

                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");

                    //htmlStr.Append("</tfoot>");
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                }

                DivTable.InnerHtml = htmlStr.ToString();

            }
            else if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
            {
                decimal OpeningBalance = 0;

                if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());
                if (chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>  & " + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>  & " + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    }
                    else
                    {
                        //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span> & " + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";

                        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span> & " + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    }

                }
                else if (chkOpeningBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    }
                    else
                    {
                        //  lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    }

                }
                else if (chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    }
                    else
                    {
                        // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    }
                }
            }
            else
            {
                lblTab.Text = "No record found.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridDetail()
    {
        try
        {
            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            chkNarration.Checked = false;
            DivTable.InnerHtml = "";


            string OfficeID = Encrypt(ViewState["Office_ID"].ToString());
            string EditStr = Encrypt("2");
            string ViewStr = Encrypt("1");

            string LedgerId = hfLedgerID.Value;
            //string Office = "";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}

            //ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "2", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            //ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "2", ViewState["Office_ID"].ToString(), ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "2", ViewState["Office_ID"].ToString(), LedgerId, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;
                divExcel.Visible = true;
                btnBack.Enabled = true;
                btnBackN.Enabled = true;
                StringBuilder htmlStr = new StringBuilder();

                if (ViewState["Office_ID"].ToString() == "1")
                {
                    htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th style='width:70px;'>Voucher Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th>Vch Type</th>");
                    htmlStr.Append("<th style='min-width: 10%;'>Vch No.</th>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<th>GST No.</th>");
                        htmlStr.Append("<th>Pan No.</th>");
                    }
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<th>Debit Amt.</th>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<th>Credit Amt.</th>");

                    int Count = ds.Tables[0].Rows.Count;
                    decimal OpeningBalance = 0;
                    decimal TDebitAmt = 0;
                    decimal TCreditAmt = 0;
                    string AmtType = "";

                    if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                        OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                    ViewState["OpeningBalance"] = OpeningBalance.ToString();
                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                    // htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");

                    htmlStr.Append("<th class='Hiderow'>Action</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");

                    if (chkOpeningBal.Checked == true)
                    {
                        if (OpeningBalance >= 0)
                        {
                            // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                            // lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        }
                        else
                        {
                            // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                            // lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        }
                    }
                    if (chkOpeningBal.Checked == true)
                    {
                        htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                        htmlStr.Append("<td style='width:70px;'></td>");
                        htmlStr.Append("<td>Opening Balance</td>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                        if (ViewState["Office_ID"].ToString() != "1")
                        {
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                        }
                        string ODr = "";
                        string OCr = "";
                        if (OpeningBalance >= 0)
                            OCr = Math.Abs(OpeningBalance).ToString();
                        else
                            ODr = Math.Abs(OpeningBalance).ToString();

                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td>" + ODr + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td>" + OCr + "</td>");

                        if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OpeningBalance >= 0)
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == true && chkClosingBal.Checked == true)
                        {
                            if (ODr != "")
                                htmlStr.Append("<td  class='align-right'>" + ODr + " Dr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == true && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OCr != "")
                                htmlStr.Append("<td  class='align-right'>" + OCr + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == false)
                        {

                        }
                        else
                        {
                            htmlStr.Append("<td  class='align-right'></td>");
                        }



                        htmlStr.Append("<td class='Hiderow'></td>");
                        htmlStr.Append("</tr>");
                    }
                    for (int i = 0; i < Count; i++)
                    {
                        decimal DebitAmt = 0;
                        decimal CreditAmt = 0;
                        decimal OpeningBal = 0;
                        decimal tempOpeningBal = 0;

                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());


                        //DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "3", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
                        DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "3", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), LedgerId }, "dataset");
                        int Count1 = ds1.Tables[5].Rows.Count;

                        string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();
                        if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";



                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td  style='width:70px;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            if (Count1 > 1)
                            {

                                htmlStr.Append("<td>(As per Details) <br/>");
                                //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                //{

                                //    for (int j = 0; j < Count1; j++)
                                //    {
                                //        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                //        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                //        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                //        htmlStr.Append("<br/>");

                                //    }
                                //}

                                /********Main Ledger********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                {
                                    int countMainledger = ds1.Tables[0].Rows.Count;
                                    htmlStr.Append("<table style='width:100%;'>");
                                    for (int j = 0; j < countMainledger; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Item Ledger********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[3].Rows.Count != 0)
                                {
                                    int countitem = ds1.Tables[3].Rows.Count;
                                    htmlStr.Append("<table style='width:100%;'>");
                                    for (int j = 0; j < countitem; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #e6e6e6; background-color: antiquewhite;'>");
                                        htmlStr.Append("<td>" + ds1.Tables[3].Rows[j]["Ledger_Name"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append(" " + ds1.Tables[3].Rows[j]["AmtType"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Item Details********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[2].Rows.Count != 0)
                                {
                                    int countitem = ds1.Tables[2].Rows.Count;
                                    htmlStr.Append("<table style='width:100%; background-color: wheat;'>");
                                    for (int j = 0; j < countitem; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #c6b8b8d1;'>");
                                        htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["ItemName"].ToString() + "</td>");
                                        htmlStr.Append("<td style='width:20%;'>" + ds1.Tables[2].Rows[j]["Quantity"].ToString() + "</td>");
                                        htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["Rate"].ToString() + " / " + ds1.Tables[2].Rows[j]["UQCCode"].ToString() + "</td>");
                                        htmlStr.Append("<td style='width:20%; text-align:right;'>" + ds1.Tables[2].Rows[j]["Amount"].ToString() + "</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Sub Ledger********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[4].Rows.Count != 0)
                                {
                                    int countitem = ds1.Tables[4].Rows.Count;
                                    htmlStr.Append("<table style='width:100%;'>");
                                    for (int j = 0; j < countitem; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                        htmlStr.Append("<td>" + ds1.Tables[4].Rows[j]["Ledger_Name"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[4].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append(" " + ds1.Tables[4].Rows[j]["AmtType"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Narration********/
                                htmlStr.Append("\n <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</td>");
                            }
                            else
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (ViewState["Office_ID"].ToString() != "1")
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                            }
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");


                            htmlStr.Append("<td class='Hiderow'>");
                            //  htmlStr.Append("<td  class='Hiderow'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                            htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");

                            htmlStr.Append("</tr>");
                        }
                        else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        {
                            decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                            if (CreditAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";

                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td style='width:70px;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                if (Count1 > 1)
                                {

                                    htmlStr.Append("<td>(As per Details) <br/>");
                                    //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    //{

                                    //    for (int j = 0; j < Count1; j++)
                                    //    {


                                    //        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                    //        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                    //        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                    //        htmlStr.Append("<br/>");

                                    //    }
                                    //}
                                    /********Main Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {
                                        int countMainledger = ds1.Tables[0].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countMainledger; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Item Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[3].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[3].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e6e6e6; background-color: antiquewhite;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[3].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[3].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Item Details********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[2].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[2].Rows.Count;
                                        htmlStr.Append("<table style='width:100%; background-color: wheat;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #c6b8b8d1;'>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["ItemName"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%;'>" + ds1.Tables[2].Rows[j]["Quantity"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["Rate"].ToString() + " / " + ds1.Tables[2].Rows[j]["UQCCode"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%; text-align:right;'>" + ds1.Tables[2].Rows[j]["Amount"].ToString() + "</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Sub Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[4].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[4].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[4].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[4].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[4].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Narration********/
                                    //  htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                else
                                {
                                    // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                    //  htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }

                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");


                                htmlStr.Append("<td class='Hiderow'>");
                                // htmlStr.Append("<td  class='Hiderow'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append("  <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");

                                htmlStr.Append("</tr>");



                            }

                        }
                        else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        {
                            decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                            if (DebitAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";



                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td style='width:70px;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                if (Count1 > 1)
                                {

                                    htmlStr.Append("<td>(As per Details) <br/>");
                                    //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    //{

                                    //    for (int j = 0; j < Count1; j++)
                                    //    {


                                    //        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                    //        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                    //        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                    //        htmlStr.Append("<br/>");

                                    //    }
                                    //}
                                    /********Main Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {
                                        int countMainledger = ds1.Tables[0].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countMainledger; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }

                                    /********Item Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[3].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[3].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e6e6e6; background-color: antiquewhite;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[3].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[3].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Item Details********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[2].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[2].Rows.Count;
                                        htmlStr.Append("<table style='width:100%; background-color: wheat;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #c6b8b8d1;'>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["ItemName"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%;'>" + ds1.Tables[2].Rows[j]["Quantity"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["Rate"].ToString() + " / " + ds1.Tables[2].Rows[j]["UQCCode"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%; text-align:right;'>" + ds1.Tables[2].Rows[j]["Amount"].ToString() + "</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }

                                    /********Sub Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[4].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[4].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[4].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[4].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[4].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Narration********/
                                    // htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                else
                                {
                                    // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                    // htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                                htmlStr.Append("<td class='Hiderow'>");
                                // htmlStr.Append("<td class='Hiderow'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append("  <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");

                                htmlStr.Append("</tr>");
                            }
                        }
                    }

                    // htmlStr.Append("</tbody>");
                    //htmlStr.Append("<tfoot>");
                    htmlStr.Append("<tr style='font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Total :</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                    }
                    if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";



                    if (OpeningBalance >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                    if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                    {
                        decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                        decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                        Dr = decimal.Parse("-" + Dr.ToString());
                        OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                        if (OpeningBalance >= 0)
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                        }
                        else
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }

                    }
                    else if (chkClosingBal.Checked == true)
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                    htmlStr.Append("<td class='Hiderow'></td>");
                    htmlStr.Append("</tr>");

                    // htmlStr.Append("</tfoot>");
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");

                }
                else
                {
                    htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th style='width:70px;'>Voucher Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th>Vch Type</th>");
                    htmlStr.Append("<th style='min-width: 10%;'>Vch No.</th>");
                    htmlStr.Append("<th>Supplier's Invoice No.</th>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<th>GST No.</th>");
                        htmlStr.Append("<th>Pan No.</th>");
                    }
                    htmlStr.Append("<th>Taxable Amount</th>");

                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<th>Debit Amt.</th>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<th>Credit Amt.</th>");

                    int Count = ds.Tables[0].Rows.Count;
                    decimal OpeningBalance = 0;
                    decimal TDebitAmt = 0;
                    decimal TCreditAmt = 0;
                    string AmtType = "";

                    if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                        OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                    ViewState["OpeningBalance"] = OpeningBalance.ToString();
                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                    // htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");

                    htmlStr.Append("<th class='Hiderow'>Action</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");

                    if (chkOpeningBal.Checked == true)
                    {

                        //if (OpeningBalance >= 0)
                        //    lblReportName.Text = lblReportName.Text + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        //else
                        //    lblReportName.Text = lblReportName.Text + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        if (OpeningBalance >= 0)
                        {
                            // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                            // lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        }
                        else
                        {
                            // lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                            // lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        }
                    }

                    //if (chkOpeningBal.Checked == true)
                    //{
                    //    htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td>Opening Balance</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");


                    //    if (chkDebitAmt.Checked == true)
                    //        htmlStr.Append("<td></td>");
                    //    if (chkCreditAmt.Checked == true)
                    //        htmlStr.Append("<td></td>");

                    //    if (OpeningBalance >= 0)
                    //        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    //    else
                    //        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("</tr>");
                    //}
                    if (chkOpeningBal.Checked == true)
                    {
                        htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                        htmlStr.Append("<td style='width:70px;'></td>");
                        htmlStr.Append("<td>Opening Balance</td>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                        if (ViewState["Office_ID"].ToString() != "1")
                        {
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                        }
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");

                        string ODr = "";
                        string OCr = "";
                        if (OpeningBalance >= 0)
                            OCr = Math.Abs(OpeningBalance).ToString();
                        else
                            ODr = Math.Abs(OpeningBalance).ToString();

                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td>" + ODr + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td>" + OCr + "</td>");

                        if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OpeningBalance >= 0)
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == true && chkClosingBal.Checked == true)
                        {
                            if (ODr != "")
                                htmlStr.Append("<td  class='align-right'>" + ODr + " Dr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == true && chkCreditAmt.Checked == false && chkClosingBal.Checked == true)
                        {
                            if (OCr != "")
                                htmlStr.Append("<td  class='align-right'>" + OCr + " Cr</td>");
                            else
                                htmlStr.Append("<td  class='align-right'></td>");
                        }
                        else if (chkDebitAmt.Checked == false && chkCreditAmt.Checked == false && chkClosingBal.Checked == false)
                        {

                        }
                        else
                        {
                            htmlStr.Append("<td  class='align-right'></td>");
                        }



                        htmlStr.Append("<td class='Hiderow'></td>");
                        htmlStr.Append("</tr>");
                    }
                    for (int i = 0; i < Count; i++)
                    {
                        decimal DebitAmt = 0;
                        decimal CreditAmt = 0;
                        decimal OpeningBal = 0;
                        decimal tempOpeningBal = 0;

                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());


                        //DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "3", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
                        DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "3", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), LedgerId }, "dataset");
                        int Count1 = ds1.Tables[5].Rows.Count;

                        string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();
                        if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";



                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td  style='width:70px;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            if (Count1 > 1)
                            {

                                htmlStr.Append("<td>(As per Details) <br/>");
                                //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                //{

                                //    for (int j = 0; j < Count1; j++)
                                //    {
                                //        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                //        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                //        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                //        htmlStr.Append("<br/>");

                                //    }
                                //}

                                /********Main Ledger********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                {
                                    int countMainledger = ds1.Tables[0].Rows.Count;
                                    htmlStr.Append("<table style='width:100%;'>");
                                    for (int j = 0; j < countMainledger; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Item Ledger********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[3].Rows.Count != 0)
                                {
                                    int countitem = ds1.Tables[3].Rows.Count;
                                    htmlStr.Append("<table style='width:100%;'>");
                                    for (int j = 0; j < countitem; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #e6e6e6; background-color: antiquewhite;'>");
                                        htmlStr.Append("<td>" + ds1.Tables[3].Rows[j]["Ledger_Name"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append(" " + ds1.Tables[3].Rows[j]["AmtType"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Item Details********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[2].Rows.Count != 0)
                                {
                                    int countitem = ds1.Tables[2].Rows.Count;
                                    htmlStr.Append("<table style='width:100%; background-color: wheat;'>");
                                    for (int j = 0; j < countitem; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #c6b8b8d1;'>");
                                        htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["ItemName"].ToString() + "</td>");
                                        htmlStr.Append("<td style='width:20%;'>" + ds1.Tables[2].Rows[j]["Quantity"].ToString() + "</td>");
                                        htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["Rate"].ToString() + " / " + ds1.Tables[2].Rows[j]["UQCCode"].ToString() + "</td>");
                                        htmlStr.Append("<td style='width:20%; text-align:right;'>" + ds1.Tables[2].Rows[j]["Amount"].ToString() + "</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Sub Ledger********/
                                if (ds1.Tables.Count != 0 && ds1.Tables[4].Rows.Count != 0)
                                {
                                    int countitem = ds1.Tables[4].Rows.Count;
                                    htmlStr.Append("<table style='width:100%;'>");
                                    for (int j = 0; j < countitem; j++)
                                    {
                                        htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                        htmlStr.Append("<td>" + ds1.Tables[4].Rows[j]["Ledger_Name"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[4].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append(" " + ds1.Tables[4].Rows[j]["AmtType"].ToString());
                                        htmlStr.Append("</td>");
                                        htmlStr.Append("</tr>");
                                    }
                                    htmlStr.Append("</table>");
                                }
                                /********Narration********/
                                //  htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</td>");
                            }
                            else
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                // htmlStr.Append("\n  <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString() + "</td>");
                            if (ViewState["Office_ID"].ToString() != "1")
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                            htmlStr.Append("<td class='Hiderow'>");
                            //htmlStr.Append("<td  class='Hiderow'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                            htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");

                            htmlStr.Append("</tr>");
                        }
                        else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        {
                            decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                            if (CreditAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";

                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td style='width:70px;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                if (Count1 > 1)
                                {

                                    htmlStr.Append("<td>(As per Details) <br/>");
                                    //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    //{

                                    //    for (int j = 0; j < Count1; j++)
                                    //    {


                                    //        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                    //        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                    //        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                    //        htmlStr.Append("<br/>");

                                    //    }
                                    //}
                                    /********Main Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {
                                        int countMainledger = ds1.Tables[0].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countMainledger; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Item Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[3].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[3].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e6e6e6; background-color: antiquewhite;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[3].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[3].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Item Details********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[2].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[2].Rows.Count;
                                        htmlStr.Append("<table style='width:100%; background-color: wheat;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #c6b8b8d1;'>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["ItemName"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%;'>" + ds1.Tables[2].Rows[j]["Quantity"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["Rate"].ToString() + " / " + ds1.Tables[2].Rows[j]["UQCCode"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%; text-align:right;'>" + ds1.Tables[2].Rows[j]["Amount"].ToString() + "</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Sub Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[4].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[4].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[4].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[4].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[4].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Narration********/
                                    // htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                else
                                {
                                    // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                    //  htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");

                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                                htmlStr.Append("<td class='Hiderow'>");
                                // htmlStr.Append("<td  class='Hiderow'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append("  <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");

                                htmlStr.Append("</tr>");



                            }

                        }
                        else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        {
                            decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                            if (DebitAmt1 > 0)
                            {
                                tempOpeningBal = OpeningBalance;
                                OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                                OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                                TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                                TCreditAmt = TCreditAmt + CreditAmt;

                                if (OpeningBal >= 0)
                                    AmtType = "Cr";
                                else
                                    AmtType = "Dr";



                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td style='width:70px;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                if (Count1 > 1)
                                {

                                    htmlStr.Append("<td>(As per Details) <br/>");
                                    //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    //{

                                    //    for (int j = 0; j < Count1; j++)
                                    //    {


                                    //        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                    //        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                    //        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                    //        htmlStr.Append("<br/>");

                                    //    }
                                    //}
                                    /********Main Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {
                                        int countMainledger = ds1.Tables[0].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countMainledger; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }

                                    /********Item Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[3].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[3].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e6e6e6; background-color: antiquewhite;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[3].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[3].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Item Details********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[2].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[2].Rows.Count;
                                        htmlStr.Append("<table style='width:100%; background-color: wheat;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #c6b8b8d1;'>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["ItemName"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%;'>" + ds1.Tables[2].Rows[j]["Quantity"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:50%;'>" + ds1.Tables[2].Rows[j]["Rate"].ToString() + " / " + ds1.Tables[2].Rows[j]["UQCCode"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:20%; text-align:right;'>" + ds1.Tables[2].Rows[j]["Amount"].ToString() + "</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }

                                    /********Sub Ledger********/
                                    if (ds1.Tables.Count != 0 && ds1.Tables[4].Rows.Count != 0)
                                    {
                                        int countitem = ds1.Tables[4].Rows.Count;
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < countitem; j++)
                                        {
                                            htmlStr.Append("<tr style='border-bottom: 1px solid #e2d9d9d1;'>");
                                            htmlStr.Append("<td>" + ds1.Tables[4].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='text-align:right;'>" + ds1.Tables[4].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[4].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }
                                    /********Narration********/
                                    //  htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                else
                                {
                                    // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                    // htmlStr.Append("\n <br/><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("\n  <br/><span class='HideRecord Narration'><b>Narration</b>\t : \t" + Narration + "</span>");
                                    htmlStr.Append("</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString() + "</td>");
                                if (ViewState["Office_ID"].ToString() != "1")
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["GST_No"].ToString() + "</td>");
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Pan_No"].ToString() + "</td>");
                                }
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["TaxableAmount"].ToString() + "</td>");
                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'></td>");
                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                                htmlStr.Append("<td class='Hiderow'>");
                                //  htmlStr.Append("<td class='Hiderow'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + ViewStr + "&Office_ID=" + OfficeID + "' target='_blank'>View</a> ");
                                htmlStr.Append("  <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + EditStr + "' target='_blank'>Edit</a> </td>");

                                htmlStr.Append("</tr>");
                            }
                        }
                    }

                    // htmlStr.Append("</tbody>");
                    //htmlStr.Append("<tfoot>");
                    htmlStr.Append("<tr style='font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Total :</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                    }
                    htmlStr.Append("<td></td>");

                    if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";

                    htmlStr.Append("<td></td>");

                    if (OpeningBalance >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                    if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                    {
                        decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                        decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                        Dr = decimal.Parse("-" + Dr.ToString());
                        OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                        if (OpeningBalance >= 0)
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                        }
                        else
                        {
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                        }

                    }
                    else if (chkClosingBal.Checked == true)
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                    htmlStr.Append("<td class='Hiderow'></td>");
                    htmlStr.Append("</tr>");

                    // htmlStr.Append("</tfoot>");
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                }
                DivTable.InnerHtml = htmlStr.ToString();



            }
            else
            {
                lblTab.Text = "No record found.";

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            DivTable.InnerHtml = "";
            lblTab.Text = "";
            // lblReportName.Text = "";

            //if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            if (txtFromDate.Text != "" && txtToDate.Text != "" && txtLedgerName.Text.Trim() != "" && txtLedgerName.Text.Trim() == hfLedgerName.Value.Trim())
            {
                FillGridDetail();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnBackN_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    var watch = System.Diagnostics.Stopwatch.StartNew();

        //    lblMsg.Text = "";
        //    DivTable.InnerHtml = "";
        //    lblTab.Text = "";
        //   // lblReportName.Text = "";
        //    if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
        //    {
        //        FillGrid();
        //    }

        //    watch.Stop();
        //    var elapsedMs = watch.ElapsedMilliseconds;

        //    lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        //}
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            lblReportName.Text = "";
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            //if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            //    FillGrid();
            //}



            string msg = "";

            //string Office = "";
            //int SerialNo = 0;
            //string OfficeName = "";
            //int totalListItem = ddlOffice.Items.Count;
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //        OfficeName += " <span style='color:tomato;'>" + (SerialNo + 1).ToString() + ".</span>" + item.Text + " ,";
            //    }
            //}
            //if (Office == "")
            //{
            //    msg = "Select at least one Office.";
            //}
            //if (ddlLedger.SelectedIndex == 0)
            //{
            //    msg += "Select List Of Ledger.\\n";
            //}
            if (txtLedgerName.Text.Trim() != hfLedgerName.Value.Trim())
            {
                msg = "Select  List Of Ledger.\\n";
            }
            if (txtLedgerName.Text.Trim() == "")
            {
                msg = "Select  List Of Ledger.\\n";
            }
            if (msg == "")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

                    //if (totalListItem == SerialNo)
                    //{
                    //    OfficeName = "All Offices";
                    //}

                    //OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);

                   // string headingFirst = "<p class='text-center' style='font-weight:700;font-size: 18px;'>Ledger Summary <br /> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + ViewState["Office_FinAddress"].ToString() + " ] <br />  " + "Ledger Of : " + ddlLedger.SelectedItem.Text + " ( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "</p>";
                    string headingFirst = "<p class='text-center' style='font-weight:700;font-size: 18px;'>Ledger Summary <br />  [ " + ViewState["Office_FinAddress"].ToString() + " ] <br />  " + "Ledger Of : " + txtLedgerName.Text.Trim() + " ( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "</p>";
                    lblReportName.Text = headingFirst;

                    FillGrid();
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "hideshow();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            lblReportName.Text = "";
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            //if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            //    FillGrid();
            //}



            string msg = "";

            //string Office = "";
            //int SerialNo = 0;
            //string OfficeName = "";
            //int totalListItem = ddlOffice.Items.Count;
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //        OfficeName += " <span style='color:tomato;'>" + (SerialNo + 1).ToString() + ".</span>" + item.Text + " ,";
            //    }
            //}
            //if (Office == "")
            //{
            //    msg = "Select at least one Office.";
            //}
            //if (ddlLedger.SelectedIndex == 0)
            //{
            //    msg += "Select List Of Ledger.\\n";
            //}
            if (txtLedgerName.Text.Trim() != hfLedgerName.Value.Trim())
            {
                msg = "Select  List Of Ledger.\\n";
            }
            if (msg == "")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

                    //if (totalListItem == SerialNo)
                    //{
                    //    OfficeName = "All Offices";
                    //}

                    //OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);

                    // string headingFirst = "<p class='text-center' style='font-weight:700;font-size: 18px;'>Ledger Summary <br /> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + OfficeName + " ] <br />  " + "Ledger Of : " + ddlLedger.SelectedItem.Text + " ( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "</p>";
                    // string headingFirst = "<p class='text-center' style='font-weight:600'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Ledger Summary<br />" + "Ledger Of : " + ddlLedger.SelectedItem.Text + " ( " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " )" + "</p>";
                    string headingFirst = "<p class='text-center' style='font-weight:600'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Ledger Summary<br />" + "Ledger Of : " + txtLedgerName.Text.Trim() + " ( " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " )" + "</p>";
                    lblReportName.Text = headingFirst;

                    FillGrid();
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "hideshow();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btngraphical_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            //string Office = "";
            string OfficeID = "";
            string heading = "";

            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();


            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}
            //if (Office == "")
            //{
            //    msg = "Select at least one Office.\\n";
            //}
            if (ddlLedger.SelectedIndex == 0)
            {
                msg += "Select List Of Ledger.";
            }
            if (msg == "")
            {

                // int totalListItem = ddlOffice.Items.Count;
                heading = "<p class='text-center' style='font-weight:600'>Closing Balance : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )</p>";
                Session["heading"] = heading.ToString();
                //foreach (ListItem item in ddlOffice.Items)
                //{
                //    if (item.Selected)
                //    {
                //        OfficeID += item.Value + ",";
                //    }
                //}
                // OfficeID = Encrypt(OfficeID);


                OfficeID = Encrypt(ViewState["Office_ID"].ToString());
                string FromDate = Encrypt(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"));
                string ToDate = Encrypt(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"));
                string Ledger_ID = Encrypt(ddlLedger.SelectedValue.ToString());
                string url = "RptGraphicalLedgerSummary.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&Ledger_ID=" + Ledger_ID;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnShowDetailBook_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (ViewState["DayBookVisible"].ToString() == "true")
            {
                DivTable.Visible = false;
                ViewState["DayBookVisible"] = "false";



                GridView4.DataSource = null;
                GridView4.DataBind();


                //string Office = "";

                //foreach (ListItem item in ddlOffice.Items)
                //{
                //    if (item.Selected)
                //    {
                //        Office += item.Value + ",";
                //    }
                //}


                //ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                ds = objdb.ByProcedure("SpFinRptLedgerSummary_F", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", ViewState["Office_ID"].ToString(), ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    GridView4.Visible = true;

                    GridView4.DataSource = ds;
                    GridView4.DataBind();

                    decimal PreOpening = decimal.Parse(ViewState["OpeningBalance"].ToString());
                    decimal OpeningBal = 0;
                    string PreClosing = "";



                    int rowcount = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < rowcount; i++)
                    {

                        string DebitAmt = "0";
                        decimal CreditAmt = 0;

                        //if (i != 0)
                        //{
                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();

                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        //}

                        if (i == 0)
                        {

                            OpeningBal = PreOpening + decimal.Parse(DebitAmt) + CreditAmt;

                        }
                        else
                        {
                            OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        }

                        //  OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        if (OpeningBal >= 0)
                        {
                            GridView4.Rows[i].Cells[3].Text = OpeningBal.ToString() + " Cr";
                            //  GridView4.Rows[i].BackColor = System.Drawing.Color.Bisque;
                        }
                        else
                        {
                            GridView4.Rows[i].Cells[3].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }
                    }
                    GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView4.UseAccessibleHeader = true;
                }
            }
            else
            {
                DivTable.Visible = true;
                GridView4.Visible = false;
                ViewState["DayBookVisible"] = "true";
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected string Encrypt(string sData)
    {

        string EncryptionKey = "%&$:";
        byte[] clearBytes = Encoding.Unicode.GetBytes(sData.Replace(" ", ""));
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64 });//, 0x76, 0x65, 0x64, 0x65, 0x76 
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                sData = Convert.ToBase64String(ms.ToArray());
            }
        }
        return sData;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchLedger(string Ledger_Name)
    {
        // List<string> Officedetail = new List<string>();
        List<string> Ledgers = new List<string>();
        try
        {
            HttpContext context = HttpContext.Current;
            DataSet DsLedgerList = context.Session["getLedger"] as DataSet;
            DataView dv = new DataView();
            dv = DsLedgerList.Tables[0].DefaultView;
            // dv = dsStaticLedger.Tables[0].DefaultView;
            dv.RowFilter = "Ledger_Name like '%" + Ledger_Name + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                // Ledgers.Add(string.Format("{0}-Ledger_Name-{1}", rs[0], rs[1]));
                Ledgers.Add(string.Format("{0}-Ledger_Name-{1}", rs[1], rs[0]));
                //foreach (DataColumn col in dt.Columns)
                //{
                //    Ledgers.Add(rs[col].ToString());
                //}

            }


        }
        catch { }
        return Ledgers;
    }
}
