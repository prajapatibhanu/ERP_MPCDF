using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptCashPaymentRegister : System.Web.UI.Page
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
                    FillDropdown();
                    btnDayBookExcel.Visible = false;
                    btnDayBookPrint.Visible = false;
                    btnMonthExcel.Visible = false;
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

                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
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

    protected void FillGridNextLedger()
    {
        try
        {
            GridView3.DataSource = new string[] { };
            GridView3.DataBind();
            MonthGridView.DataSource = new string[] { };
            MonthGridView.DataBind();
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("SpFinRptBankPaymentRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "FromDate", "ToDate" }, new string[] { "3", Office, "Cash Payment", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnDayBookPrint.Visible = true;
                btnDayBookExcel.Visible = false;
                btnMonthExcel.Visible = true;
                GridView3.DataSource = ds;
                GridView3.DataBind();
                /*********************/
                GridView3.FooterRow.Cells[1].Text = "Total";
                GridView3.FooterRow.Cells[1].Font.Bold = true;
                GridView3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                GridView3.FooterRow.Cells[2].Text = ds.Tables[1].Rows[0]["TVcCount"].ToString();
                GridView3.FooterRow.Cells[2].Font.Bold = true;
                GridView3.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                GridView3.FooterRow.Cells[3].Text = ds.Tables[1].Rows[0]["TCRAmount"].ToString();
                GridView3.FooterRow.Cells[3].Font.Bold = true;
                GridView3.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;


                GridView3.FooterRow.Cells[4].Text = ds.Tables[1].Rows[0]["TDRAmount"].ToString();
                GridView3.FooterRow.Cells[4].Font.Bold = true;
                GridView3.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                GridView3.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TClosingBalance"].ToString();
                GridView3.FooterRow.Cells[5].Font.Bold = true;
                GridView3.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                MonthGridView.DataSource = ds;
                MonthGridView.DataBind();
                /*********************/
                MonthGridView.FooterRow.Cells[1].Text = "Total";
                MonthGridView.FooterRow.Cells[1].Font.Bold = true;
                MonthGridView.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                MonthGridView.FooterRow.Cells[2].Text = ds.Tables[1].Rows[0]["TVcCount"].ToString();
                MonthGridView.FooterRow.Cells[2].Font.Bold = true;
                MonthGridView.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                MonthGridView.FooterRow.Cells[3].Text = ds.Tables[1].Rows[0]["TCRAmount"].ToString();
                MonthGridView.FooterRow.Cells[3].Font.Bold = true;
                MonthGridView.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;


                MonthGridView.FooterRow.Cells[4].Text = ds.Tables[1].Rows[0]["TDRAmount"].ToString();
                MonthGridView.FooterRow.Cells[4].Font.Bold = true;
                MonthGridView.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                MonthGridView.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TClosingBalance"].ToString();
                MonthGridView.FooterRow.Cells[5].Font.Bold = true;
                MonthGridView.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;


                /*********************/
                GridView4.DataSource = null;
                GridView4.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridNextLedgerMonth(string MonthID)
    {
        try
        {
            lblMsg.Text = "";
            GridView3.DataSource = null;
            GridView4.DataSource = null;
            MonthGridView.DataSource = null;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("SpFinRptBankPaymentRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "4", Office, "Cash Payment", MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnDayBookExcel.Visible = true;
                btnDayBookPrint.Visible = true;
                btnMonthExcel.Visible = false;
                GridView4.DataSource = ds;
                GridView4.DataBind();
                /*********************/
                GridView4.FooterRow.Cells[1].Text = "Total";
                GridView4.FooterRow.Cells[1].Font.Bold = true;
                GridView4.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;


                GridView4.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TDebitAmt"].ToString();
                GridView4.FooterRow.Cells[5].Font.Bold = true;
                GridView4.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                GridView4.FooterRow.Cells[6].Text = ds.Tables[1].Rows[0]["TCreditAmt"].ToString();
                GridView4.FooterRow.Cells[6].Font.Bold = true;
                GridView4.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;


                /*********************/
                GridView3.DataBind();
                MonthGridView.DataBind();

                foreach (GridViewRow rows in GridView4.Rows)
                {
                    LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                    //  LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                    LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                    LinkButton bankreceiptprint = (LinkButton)rows.FindControl("bankreceiptprint");
                    Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
                    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                    if (ViewState["Office_ID"].ToString() != lblOfficeID.Text)
                    {
                        // lnkDelete.Visible = false;
                        lnkEdit.Visible = false;
                    }
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];

                Label lblMonthID = (Label)row.Cells[0].FindControl("lblMonthID");


                string MonthID = lblMonthID.Text;
                ViewState["MonthID"] = MonthID.ToString();
                FillGridNextLedgerMonth(MonthID);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView4_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            string VoucherTx_ID = GridView4.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
                   new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");

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
            FillGridNextLedger();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }

            if (Office != "")
            {
                if (totalListItem == SerialNo)
                {
                    OfficeName = "All Offices";
                }
                else if (SerialNo == 0)
                {
                    OfficeName = "---Office Not Selected---";
                }
                else
                {
                    OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
                }
                // string headingFirst = "<p class='text-center' style='font-weight:600'> [ " + OfficeName + " ] <br />Dairy Plant<br/>BANK PAYMENT REGISTER<br/>" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                string headingFirst = "<p class='text-center' style='font-weight:600'>" + ViewState["Office_FinAddress"].ToString() + " <br /> Cash Payment Register<br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                lblheadingFirst.Text = headingFirst;
                FillGridNextLedger();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select at least one Office.');", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Editing")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID" },
                    new string[] { "30", VoucherTx_ID },
                    "dataset");

                if (dsPageURL != null)
                {

                    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    Url = Url + "&Action=" + objdb.Encrypt("2");
                    Response.Redirect(Url);

                }
            }
            if (e.CommandName == "View")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID" },
                    new string[] { "30", VoucherTx_ID },
                    "dataset");

                if (dsPageURL != null)
                {

                    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dsPageURL.Tables[1].Rows[0]["Office_ID"].ToString());

                    Response.Redirect(Url);

                }

            }
            if (e.CommandName == "Print")
            {

                string VoucherTx_ID = e.CommandArgument.ToString();
                ds = objdb.ByProcedure("SpFinVoucherTx",
                  new string[] { "flag", "VoucherTx_ID" },
                  new string[] { "31", VoucherTx_ID },
                  "dataset");

                if (ds != null)
                {
                    string VoucherTx_Type = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                    if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO" || VoucherTx_Type == "CreditNote Voucher" || VoucherTx_Type == "DebitNote Voucher" || VoucherTx_Type == "Money Receipt")
                    {

                        string Url = "VoucherJournalInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    else if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                    {

                        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);


                    }
                    else if (VoucherTx_Type == "CashSale Voucher" || VoucherTx_Type == "CreditSale Voucher" || VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free" || VoucherTx_Type == "CC Sale Voucher" || VoucherTx_Type == "JV Sale Voucher" || VoucherTx_Type == "GST Sale Voucher" || VoucherTx_Type == "DCS Sale Voucher")
                    {
                        string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    else
                    {

                    }

                }
            }
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
            string OfficeID = "";
            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            string headingFirst = "";
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }

            if (Office != "")
            {
                if (totalListItem == SerialNo)
                {
                    OfficeName = "All Offices";
                }
                else if (SerialNo == 0)
                {
                    OfficeName = "---Office Not Selected---";
                }
                else
                {
                    OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
                }
                headingFirst = "<p class='text-center' style='font-weight:600'>Sales Register<br /> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                Session["SalesHeading"] = headingFirst.ToString();
                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        OfficeID += item.Value + ",";
                    }
                }
                OfficeID = objdb.Encrypt(OfficeID);
                string FromDate = objdb.Encrypt(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"));
                string ToDate = objdb.Encrypt(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"));

                string url = "RptGraphicalSalesRegister.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnDayBookExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView4.Columns[7].Visible = false;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=BankCashPaymentRegister.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView4.GridLines = GridLines.Both;
            GridView4.HeaderStyle.Font.Bold = true;
            GridView4.RenderControl(htmlWrite);
            //tblExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            //tblExport.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnMonthExcel_Click(object sender, EventArgs e)
    {
        try
        {
            tblExport.Visible = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=BankPaymentRegisterMonthReport.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //GridView3.GridLines = GridLines.Both;
            //GridView3.HeaderStyle.Font.Bold = true;
            //GridView3.RenderControl(htmlWrite);
            tblExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            tblExport.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}