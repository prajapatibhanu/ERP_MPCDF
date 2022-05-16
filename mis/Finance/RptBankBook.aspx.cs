using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_RptBankBook : System.Web.UI.Page
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
                    ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();

                    ddlOffice.Enabled = false;
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
                    }


                    chkOpeningBal.Checked = true;
                    chkDebitAmt.Checked = true;
                    chkCreditAmt.Checked = true;
                    chkClosingBal.Checked = false;

                    ViewState["DayBookVisible"] = "true";

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
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag" },
                   new string[] { "26" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
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
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "7", Office }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
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
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "7", Office }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
            }

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
            lblTab.Text = "";


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



            string msg = "";

            string Office = "";
            int SerialNo = 0;
            string OfficeName = "";
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                    SerialNo = SerialNo + 1;
                    OfficeName += " <span style='color:tomato;'>" + (SerialNo).ToString() + ".</span>" + item.Text + " ,";
                }
            }
            if (Office == "")
            {
                msg = "Select at least one Office.";
            }
            if (ddlLedger.SelectedIndex == 0)
            {
                msg += "Select List Of Ledger.\\n";
            }
            if (msg == "")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    //lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

                    if (totalListItem == SerialNo)
                    {
                        OfficeName = "All Offices";
                    }

                    OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);

                    string headingFirst = "<p class='text-center' style='font-weight:700;font-size: 18px;'>" + ddlLedger.SelectedItem.Text + " Book <br /> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + OfficeName + " ] <br />  " + "Ledger Of : " + ddlLedger.SelectedItem.Text + " ( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "</p>";

                    // lblReportName.Text = headingFirst;

                    FillGridDetail();
                }

                //watch.Stop();
                //var elapsedMs = watch.ElapsedMilliseconds;
                //lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
    protected void FillGridDetail()
    {
        try
        {
            ViewState["DayBookVisible"] = "true";

            DivTable.InnerHtml = "";
            string Office = "";
            int SerialNo = 0;
            string OfficeName = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                    SerialNo = SerialNo + 1;
                    OfficeName += " <span style='color:tomato;'>" + (SerialNo).ToString() + ".</span>" + item.Text;
                    if (SerialNo > 1)
                        OfficeName += " ,";
                }
            }
            decimal OpeningBalance = 0;
            ds = objdb.ByProcedure("SpFinRptLedgerBook", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "1", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
            {
                if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

                OfficeName = ds.Tables[1].Rows[0]["Office_Name"].ToString();
            }

            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                DivTable.Visible = true;

                StringBuilder htmlStr = new StringBuilder();
                string BreakVoucherDate = "";
                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                {
                    BreakVoucherDate = ds.Tables[2].Rows[k]["VoucherTx_Date"].ToString();
                    if (k != 0)
                    {

                        htmlStr.Append("<div class='pagebreak'></<div>");
                    }
                    //htmlStr.Append("<p class='text-center' style='font-weight:700;font-size: 18px;'> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + OfficeName + " ] <br /> " + ddlLedger.SelectedItem.Text + " Book <br /> " + "For : " + BreakVoucherDate + "</p>");
                    htmlStr.Append("<p class='text-center' style='font-weight:700;font-size: 15px;'>  " + ViewState["Office_FinAddress"].ToString() + "  <br /> " + ddlLedger.SelectedItem.Text + " Book <br /> " + "For : " + txtFromDate.Text + " To " + txtToDate.Text + "</p>");
                    htmlStr.Append("<table  id='DetailGrid' class='table' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th style='width: 65px;'>Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th  style='min-width: 100px;text-align: center;'>Vch Type</th>");
                    htmlStr.Append("<th style='min-width:  65px;text-align: center;'>Vch No.</th>");
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<th class='align-right'>Debit</th>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<th class='align-right'>Credit</th>");

                    int Count = ds.Tables[0].Rows.Count;

                    decimal TDebitAmt = 0;
                    decimal TCreditAmt = 0;
                    string AmtType = "";
                    // decimal OpeningBalance = 0;



                    //ViewState["OpeningBalance"] = OpeningBalance.ToString();

                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<th class='ClosingBal align-right'>Closing</th>");



                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");

                    //if (chkOpeningBal.Checked == true)
                    //{

                    //    if (OpeningBalance >= 0)
                    //    {

                    //        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //    }
                    //    else
                    //    {                       
                    //        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    //    }
                    //}

                    decimal OpenDr = 0;
                    decimal OpenCr = 0;

                    if (chkOpeningBal.Checked == true)
                    {
                        htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td>Opening Balance</td>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");

                        string ODr = "";
                        string OCr = "";

                        if (OpeningBalance >= 0)
                        {
                            OCr = Math.Abs(OpeningBalance).ToString();
                            OpenCr = Math.Abs(OpeningBalance);
                        }
                        else
                        {
                            ODr = Math.Abs(OpeningBalance).ToString();
                            OpenDr = Math.Abs(OpeningBalance);
                        }

                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td  class='align-right'>" + ODr + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td  class='align-right'>" + OCr + "</td>");

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
                        else if (chkClosingBal.Checked == true)
                        {
                            htmlStr.Append("<td  class='align-right'></td>");
                        }



                        htmlStr.Append("</tr>");
                    }
                    for (int i = 0; i < Count; i++)
                    {
                        if (BreakVoucherDate == ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString())
                        {
                            decimal DebitAmt = 0;
                            decimal CreditAmt = 0;
                            decimal OpeningBal = 0;
                            decimal tempOpeningBal = 0;

                            if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                                DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                            if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                                CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());


                            DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerBook", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "2", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
                            int Count1 = ds1.Tables[0].Rows.Count;

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
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                                if (Count1 > 1)
                                {

                                    htmlStr.Append("<td>(As per Details) <br/>");
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {
                                        htmlStr.Append("<table style='width:100%;'>");
                                        for (int j = 0; j < Count1; j++)
                                        {
                                            htmlStr.Append("<tr>");
                                            htmlStr.Append("<td>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("<td style='width:50%;text-align:right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append(" " + ds1.Tables[0].Rows[j]["AmtType"].ToString());
                                            htmlStr.Append("</td>");
                                            htmlStr.Append("</tr>");
                                        }
                                        htmlStr.Append("</table>");
                                    }

                                    htmlStr.Append("\n<p class='subledger'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                                    htmlStr.Append("</td>");
                                }
                                else
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                    htmlStr.Append("\n<p class='subledger'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                                    htmlStr.Append("</td>");
                                }
                                htmlStr.Append("<td class='align-center'>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td class='align-center'>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString().Substring(9) + "</td>");

                                if (chkDebitAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                                if (chkCreditAmt.Checked == true)
                                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");

                                if (chkClosingBal.Checked == true)
                                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");


                                htmlStr.Append("</tr>");
                            }

                        }
                    }

                    htmlStr.Append("</tbody>");
                    //htmlStr.Append("<tfoot >");
                    htmlStr.Append("<tr  style='font-weight: 700;'>");
                    //htmlStr.Append("<td></td>");
                    //htmlStr.Append("<td></td>");
                    htmlStr.Append("<td colspan='4' rowspan='2' class='align-center' style='vertical-align: inherit;'>Closing Balance :</td>");

                    //if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                    //    AmtType = "Cr";
                    //else
                    //    AmtType = "Dr";

                    //htmlStr.Append("<td></td>");

                    if (OpeningBalance >= 0)
                        AmtType = "Cr";
                    else
                        AmtType = "Dr";

                    string closeValue = "";
                    decimal totalDr = 0;
                    decimal totalCr = 0;
                    if (chkDebitAmt.Checked == true)
                    {
                        totalDr = TDebitAmt + OpenDr;
                        if (AmtType == "Cr")
                        {
                            closeValue = Math.Abs(OpeningBalance).ToString();

                            totalDr = totalDr + Math.Abs(OpeningBalance);
                        }

                        htmlStr.Append("<td class='align-right'>" + (TDebitAmt + OpenDr).ToString() + " <br/>" + closeValue + "</td>");
                    }
                    closeValue = "";
                    if (chkCreditAmt.Checked == true)
                    {
                        totalCr = TCreditAmt + OpenCr;
                        if (AmtType == "Dr")
                        {
                            closeValue = Math.Abs(OpeningBalance).ToString();

                            totalCr = totalCr + Math.Abs(OpeningBalance);
                        }

                        htmlStr.Append("<td class='align-right'>" + (TCreditAmt + OpenCr).ToString() + " <br/>" + closeValue + "</td>");
                    }

                    //if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                    //{
                    //    decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                    //    decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                    //    Dr = decimal.Parse("-" + Dr.ToString());
                    //    OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                    //    if (OpeningBalance >= 0)
                    //    {
                    //        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    //    }
                    //    else
                    //    {
                    //        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                    //    }

                    //}
                    //else if (chkClosingBal.Checked == true)
                    //    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");



                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<td></td>");

                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr  style='font-weight: 700;'>");
                    htmlStr.Append("<td class='align-right'>" + totalDr + "</td>");
                    htmlStr.Append("<td class='align-right'>" + totalCr + "</td>");
                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<td></td>");

                    htmlStr.Append("</tr>");

                    // htmlStr.Append("</tfoot>");
                    htmlStr.Append("</table>");
                }


                DivTable.InnerHtml = htmlStr.ToString();



            }
            else
            {
                if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
                {

                    DivTable.Visible = true;

                    StringBuilder htmlStr = new StringBuilder();

                    htmlStr.Append("<p class='text-center' style='font-weight:700;font-size: 15px;'>  " + ViewState["Office_FinAddress"].ToString() + "  <br /> " + ddlLedger.SelectedItem.Text + " Book <br /> " + "For : " + txtFromDate.Text + " To " + txtToDate.Text + "</p>");
                    htmlStr.Append("<table  id='DetailGrid' class='table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th style='width: 65px;'>Date</th>");
                    htmlStr.Append("<th>Particulars</th>");
                    htmlStr.Append("<th style='min-width: 100px;text-align: center;'>Vch Type</th>");
                    htmlStr.Append("<th style='min-width:  65px;text-align: center;'>Vch No.</th>");
                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<th class='align-right'>Debit</th>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<th class='align-right'>Credit</th>");

                    int Count = ds.Tables[0].Rows.Count;
                    // decimal OpeningBalance = 0;
                    decimal TDebitAmt = 0;
                    decimal TCreditAmt = 0;
                    string AmtType = "";


                    ViewState["OpeningBalance"] = OpeningBalance.ToString();

                    if (chkClosingBal.Checked == true)
                        htmlStr.Append("<th class='ClosingBal align-right'>Closing</th>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");

                    //if (chkOpeningBal.Checked == true)
                    //{

                    //    if (OpeningBalance >= 0)
                    //    {

                    //        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //    }
                    //    else
                    //    {                       
                    //        lblReportName.Text = lblReportName.Text + "<br/><span style='color: #002a4e; font-weight: 700; font-size: 20px;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    //    }
                    //}


                    if (chkOpeningBal.Checked == true)
                    {
                        htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td>Opening Balance</td>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");

                        string ODr = "";
                        string OCr = "";
                        if (OpeningBalance >= 0)
                            OCr = Math.Abs(OpeningBalance).ToString();
                        else
                            ODr = Math.Abs(OpeningBalance).ToString();

                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ODr + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + OCr + "</td>");

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
                        else if (chkClosingBal.Checked == true)
                        {
                            htmlStr.Append("<td  class='align-right'></td>");
                        }



                        //  htmlStr.Append("<td  class='hide_print'></td>");
                        htmlStr.Append("</tr>");
                    }


                    htmlStr.Append("</tbody>");
                    htmlStr.Append("<tfoot>");
                    htmlStr.Append("<tr style='font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Total :</td>");

                    //if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                    //    AmtType = "Cr";
                    //else
                    //    AmtType = "Dr";

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



                    htmlStr.Append("</tr>");

                    htmlStr.Append("</tfoot>");
                    htmlStr.Append("</table>");

                    DivTable.InnerHtml = htmlStr.ToString();



                }
                // lblTab.Text = "No record found.";


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}
