using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptDetailedDaybook : System.Web.UI.Page
{
    DataSet ds;
    static DataSet dsStatic;
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
                    btnExportExcel.Visible = false;
					txtSearch.Visible = false;
                    txtDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    FillDropdown();
                    FillGrid();

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
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
            else
            {
                ddlOffice.Items.Clear();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
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
                txtDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
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
			string sDate = (Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            btnExportExcel.Visible = false;
			txtSearch.Visible = false;
            // string headingFirst = "<p class='text-center' style='font-weight:600; font-size:16px;'>Day Book<br /> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            string headingFirst = "<p class='text-center' style='font-weight:600;'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Day Book<br /> " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Date", "FinancialYear" }, new string[] { "10", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"),FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                btnExportExcel.Visible = true;
				 txtSearch.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();


            }
            //}

            foreach (GridViewRow rows in GridView1.Rows)
            {
                LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
                Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                LinkButton bankreceiptprint = (LinkButton)rows.FindControl("bankreceiptprint");
				Label lblV_Editright = (Label)rows.FindControl("lblV_Editright");
                if (ViewState["Office_ID"].ToString() != lblOfficeID.Text)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkPrint.Visible = false;
                    bankreceiptprint.Visible = false;
                }
                else
                {
                    if (lblVoucherTx_Type.Text == "Bank Receipt")
                    {
                        bankreceiptprint.Visible = true;
                    }
                    else if (lblVoucherTx_Type.Text == "Money Receipt")
                    {
                        bankreceiptprint.Visible = true;
                    }
                    else
                    {
                        bankreceiptprint.Visible = false;
                    }
                    lnkPrint.Visible = true;


                    lnkPrint.Visible = true;
                    //  lnkDelete.Visible = true;
                    //if (lblVoucherTx_Type.Text == "CreditNote Voucher" || lblVoucherTx_Type.Text == "DebitNote Voucher")
                    //{
                    //    lnkEdit.Visible = false;
                    //}
                    //else
                    //{
                    //    lnkEdit.Visible = true;
                    //}


                }
				 if (lblV_Editright.Text == "No")
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                }
            }
            //foreach (GridViewRow rows in GridView1.Rows)
            //{

            //    Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
            //    HyperLink lnkEdit = (HyperLink)rows.FindControl("hpEdit");
            //    HyperLink hpprint1 = (HyperLink)rows.FindControl("hpprint1");
            //    HyperLink hpprint2 = (HyperLink)rows.FindControl("hpprint2");
            //    if (lblVoucherTx_Type.Text == "CreditNote Voucher" || lblVoucherTx_Type.Text == "DebitNote Voucher")
            //    {
            //        lnkEdit.Visible = false;
            //    }
            //    else
            //    {
            //        lnkEdit.Visible = true;

            //    }
            //    if (lblVoucherTx_Type.Text == "Payment" || lblVoucherTx_Type.Text == "Contra" || lblVoucherTx_Type.Text == "GSTService Purchase" || lblVoucherTx_Type.Text == "Cash Payment")
            //    {
            //        if (ddlOffice.SelectedValue == ViewState["Office_ID"].ToString())
            //        {
            //            hpprint1.Visible = true;
            //            hpprint2.Visible = false;
            //        }
            //        else
            //        {
            //            hpprint1.Visible = false;
            //            hpprint2.Visible = false;
            //        }
            //    }
            //    else if (lblVoucherTx_Type.Text == "Receipt" || lblVoucherTx_Type.Text == "Journal" || lblVoucherTx_Type.Text == "Bank Receipt" || lblVoucherTx_Type.Text == "Journal HO")
            //    {
            //        if (ddlOffice.SelectedValue == ViewState["Office_ID"].ToString())
            //        {
            //            hpprint1.Visible = false;
            //            hpprint2.Visible = true;
            //        }
            //        else
            //        {
            //            hpprint1.Visible = false;
            //            hpprint2.Visible = false;
            //        }

            //    }
            //    else
            //    {
            //        hpprint1.Visible = false;
            //        hpprint2.Visible = false;
            //    }

            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            string VoucherTx_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
                   new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
            FillGrid();

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
                    if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                    {

                        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);


                    }
                    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO" || VoucherTx_Type == "CreditNote Voucher" || VoucherTx_Type == "DebitNote Voucher" || VoucherTx_Type == "Money Receipt")
                    {

                        string Url = "VoucherJournalInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);



                    }
                    else if (VoucherTx_Type == "CashSale Voucher" || VoucherTx_Type == "CreditSale Voucher" || VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free" || VoucherTx_Type == "CC Sale Voucher" || VoucherTx_Type == "JV Sale Voucher" || VoucherTx_Type == "GST Sale Voucher" || VoucherTx_Type == "DCS Sale Voucher" || VoucherTx_Type == "Item Credit Note Voucher" || VoucherTx_Type == "Item Debit Note Voucher")
                    {
                        string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    else
                    {

                    }

                }

            }
            if (e.CommandName == "ReceiptPrint")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                string Url = "ReceivingInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Response.Redirect(Url);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblVID = (Label)e.Row.FindControl("lblVID");
            Label lblLID = (Label)e.Row.FindControl("lblLID");

            Label lblLedgerTx_ID = (Label)e.Row.FindControl("lblLedgerTx_ID");

            Label lblLedger_Name = (Label)e.Row.FindControl("lblLedger_Name");
            Label lblNarration = (Label)e.Row.FindControl("lblNarration");
            GridView gvLedger = (GridView)e.Row.FindControl("gvLedger");
            GridView GvItem = (GridView)e.Row.FindControl("GvItem");
            GridView gvSubLedger = (GridView)e.Row.FindControl("gvSubLedger");
            GridView GVLBillbyBill = (GridView)e.Row.FindControl("GVLBillbyBill");
            DataSet ds1 = null;

            //ds1 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "13", lblVID.Text, lblLID.Text }, "dataset");
            ds1 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "LedgerTx_ID" }, new string[] { "16", lblVID.Text, lblLID.Text, lblLedgerTx_ID.Text }, "dataset");
            if (ds1 != null)
            {
                gvLedger.DataSource = ds1.Tables[0];
                gvLedger.DataBind();
                GvItem.DataSource = ds1.Tables[1];
                GvItem.DataBind();
                gvSubLedger.DataSource = ds1.Tables[2];
                gvSubLedger.DataBind();
                GVLBillbyBill.DataSource = ds1.Tables[4];
                GVLBillbyBill.DataBind();
                lblNarration.Text = "<b>Narration:</b>" + " " + ds1.Tables[3].Rows[0]["VoucherTx_Narration"].ToString();
            }

            //int Count1 = ds1.Tables[0].Rows.Count;

            //string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();
            //StringBuilder htmlStr = new StringBuilder();
            //htmlStr.Append("<table  id='DetailGrid' class='table table-hover table-bordered' style='width:100%'>");
            //htmlStr.Append("<tbody>");
            //htmlStr.Append("<tr>");

            //if (Count1 > 1)
            //{

            //    htmlStr.Append("<td><p class='HideRecord'>(As per Details) <br/></p>");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        htmlStr.Append("<table width='100%'>");
            //        for (int j = 0; j < Count1; j++)
            //        {
            //            htmlStr.Append("<tr>");
            //            if (j == 0)
            //            {
            //                htmlStr.Append("<td><p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span></p></td>");
            //            }
            //            else
            //            {
            //                htmlStr.Append("<td><p class='subledger HideRecord' ><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span></p></td>");
            //            }

            //            htmlStr.Append("<td style='float:right;'><span class='Ledger_Amt HideRecord'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
            //            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></td>");

            //            htmlStr.Append("</tr>");
            //        }
            //        htmlStr.Append("</table>");
            //    }

            //    htmlStr.Append("\n<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
            //    htmlStr.Append("</td>");

            //}
            //else
            //{
            //    htmlStr.Append("<td>" + lblLedger_Name.Text + "");
            //    htmlStr.Append("\n<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
            //    htmlStr.Append("</td>");

            //}
            //htmlStr.Append("</tr>");
            //htmlStr.Append("</tbody>");
            //htmlStr.Append("</table>");
            //HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("div");
            //div.InnerHtml = htmlStr.ToString();

        }

    }
    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBVID = (Label)e.Row.FindControl("lblBVID");
            Label lblBLID = (Label)e.Row.FindControl("lblBLID");
            GridView GVMLBillbyBill = (GridView)e.Row.FindControl("GVMLBillbyBill");
            DataSet ds2 = null;
            ds2 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "BVoucherTx_ID", "BLedger_ID", }, new string[] { "14", lblBVID.Text, lblBLID.Text }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {

                GVMLBillbyBill.DataSource = ds2;
                GVMLBillbyBill.DataBind();

            }

        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            FillGrid();
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;

            //lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtDate.Text != "")
            {
                // THE EXCEL FILE.
                string sFileName = "Detail Daybook.xls";
                sFileName = sFileName.Replace("/", "");

                // SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                Response.ContentType = "application/vnd.ms-excel";
                EnableViewState = false;

                System.IO.StringWriter objSW = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

                //dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
                //dg.RenderControl(objHTW);

                string HTMLTABLE = "";
                // DataTable dtVoucherTx = Session["dtVoucherTx"] as DataTable;
                DataSet ds1, ds2;

                DataView dv = new DataView();
                ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "45", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    //if (dtVoucherTx.Rows.Count != 0)
                    //{
                    int rowcount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string VoucherTx_ID = ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString();
                        string VoucherTx_Date = ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString();
                        string VoucherTx_Type = ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString();
                        string VoucherTx_No = ds.Tables[0].Rows[i]["VoucherTx_No"].ToString();
                        string VoucherTx_Narration = ds.Tables[0].Rows[i]["VoucherTx_Narration"].ToString();
                        string ItemCount = ds.Tables[0].Rows[i]["ItemCount"].ToString();

                        //  ds1 = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "45", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString() }, "dataset");
                        dsStatic = ds;
                        dv = dsStatic.Tables[1].DefaultView;
                        dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'";
                        DataTable dt1 = dv.ToTable();
                        int vouchercount = 0;
                        /****Main Ledger****/
                        if (dt1.Rows.Count != 0)
                        {
                            int tab1Count = dt1.Rows.Count;
                            for (int j = 0; j < tab1Count; j++)
                            {
                                /**********Main Ledger Details**************/
                                if (j == 0 && vouchercount == 0)
                                {
                                    vouchercount = 1;
                                    HTMLTABLE += "<tr>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_Date + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt1.Rows[j]["Ledger_Name"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_Type + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_No + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt1.Rows[j]["DebitAmt"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt1.Rows[j]["CreditAmt"].ToString() + "<b></td>";
                                    HTMLTABLE += "</tr>";
                                }
                                else
                                {
                                    HTMLTABLE += "<tr>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt1.Rows[j]["Ledger_Name"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt1.Rows[j]["DebitAmt"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt1.Rows[j]["CreditAmt"].ToString() + "<b></td>";
                                    HTMLTABLE += "</tr>";
                                }

                                if (dt1.Rows[j]["BillType"].ToString() == "Yes")
                                {
                                    dv = dsStatic.Tables[5].DefaultView;
                                    dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'  AND  Ledger_ID = '" + dt1.Rows[j]["Ledger_ID"].ToString() + "'";
                                    DataTable dtBill = dv.ToTable();

                                    if (dtBill.Rows.Count != 0)
                                    {
                                        ///**********Bill By Bill Ref Details**************////
                                        int tab2Count = dtBill.Rows.Count;
                                        for (int k = 0; k < tab2Count; k++)
                                        {
                                            HTMLTABLE += "<tr>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBill.Rows[k]["BillByBillTx_RefType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBill.Rows[k]["BillByBillTx_Ref"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBill.Rows[k]["BillByBillTx_Amount"].ToString() + " " + dtBill.Rows[k]["AmtType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "</tr>";
                                        }

                                    }
                                }
                                else if (dt1.Rows[j]["BankType"].ToString() == "Yes")
                                {
                                    dv = dsStatic.Tables[6].DefaultView;
                                    dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'  AND  Ledger_ID = '" + dt1.Rows[j]["Ledger_ID"].ToString() + "'";
                                    DataTable dtBank = dv.ToTable();

                                    if (dtBank.Rows.Count != 0)
                                    {
                                        ///**********Cheque/DD Details**************/
                                        int tab3Count = dtBank.Rows.Count;
                                        for (int k = 0; k < tab3Count; k++)
                                        {
                                            HTMLTABLE += "<tr>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                           // HTMLTABLE += "<td style='border:solid 1px #999; '>Cheque/DD</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["InstrumentType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["ChequeTx_No"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["ChequeTx_Date"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["ChequeTx_Amount"].ToString() + " " + dtBank.Rows[k]["AmtType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "</tr>";
                                        }
                                    }
                                }
                                else if (dt1.Rows[j]["CostCentre"].ToString() == "Yes")
                                {
                                    dv = dsStatic.Tables[7].DefaultView;
                                    dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'  AND  Ledger_ID = '" + dt1.Rows[j]["Ledger_ID"].ToString() + "'";
                                    DataTable dtBank = dv.ToTable();

                                    if (dtBank.Rows.Count != 0)
                                    {
                                        ///**********Cheque/DD Details**************/
                                        int tab3Count = dtBank.Rows.Count;
                                        for (int k = 0; k < tab3Count; k++)
                                        {
                                            HTMLTABLE += "<tr>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            //HTMLTABLE += "<td style='border:solid 1px #999; '>Cheque/DD</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["CategoryName"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["SubCategoryName"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["Amount"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "</tr>";
                                        }
                                    }
                                }

                                //    ///**********On Account Details**************/
                                //    //HTMLTABLE += "<tr>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "<td>On Account</td>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "<td>3186</td>";
                                //    //HTMLTABLE += "<td>Dr</td>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "<td></td>";
                                //    //HTMLTABLE += "</tr>";




                            }
                        }


                        /****Item Ledger****/
                        dv = dsStatic.Tables[2].DefaultView;
                        dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'";
                        DataTable dt2 = dv.ToTable();
                        if (dt2.Rows.Count != 0)
                        {
                            int tab1Count = dt2.Rows.Count;
                            for (int j = 0; j < tab1Count; j++)
                            {
                                /**********Item Ledger Details**************/
                                if (j == 0 && vouchercount == 0)
                                {
                                    vouchercount = 1;
                                    HTMLTABLE += "<tr>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_Date + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt2.Rows[j]["Ledger_Name"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_Type + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_No + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt2.Rows[j]["DebitAmt"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt2.Rows[j]["CreditAmt"].ToString() + "<b></td>";
                                    HTMLTABLE += "</tr>";
                                }
                                else
                                {
                                    HTMLTABLE += "<tr>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt2.Rows[j]["Ledger_Name"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt2.Rows[j]["DebitAmt"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt2.Rows[j]["CreditAmt"].ToString() + "<b></td>";
                                    HTMLTABLE += "</tr>";
                                }

                            }
                        }
                        /****Item Details****/
                        dv = dsStatic.Tables[3].DefaultView;
                        dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'";
                        DataTable dt3 = dv.ToTable();
                        if (dt3.Rows.Count != 0)
                        {
                            int tab1Count = dt3.Rows.Count;
                            for (int j = 0; j < tab1Count; j++)
                            {
                                /**********Item Details**************/
                                HTMLTABLE += "<tr>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '>" + dt3.Rows[j]["ItemName"].ToString() + "</td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '>" + dt3.Rows[j]["Quantity"].ToString() + " " + dt3.Rows[j]["UQCCode"].ToString() + "</td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '>" + dt3.Rows[j]["Rate"].ToString() + " / " + dt3.Rows[j]["UQCCode"].ToString() + "</td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '>" + dt3.Rows[j]["Amount"].ToString() + "</td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                HTMLTABLE += "</tr>";

                            }
                        }
                        /****Sub Ledger****/

                        dv = dsStatic.Tables[4].DefaultView;
                        dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'";
                        DataTable dt4 = dv.ToTable();
                        if (dt4.Rows.Count != 0)
                        {
                            int tab1Count = dt4.Rows.Count;
                            for (int j = 0; j < tab1Count; j++)
                            {
                                /**********Main Ledger Details**************/
                                if (j == 0 && vouchercount == 0)
                                {
                                    vouchercount = 1;
                                    HTMLTABLE += "<tr>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_Date + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt4.Rows[j]["Ledger_Name"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_Type + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + VoucherTx_No + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt4.Rows[j]["DebitAmt"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt4.Rows[j]["CreditAmt"].ToString() + "<b></td>";
                                    HTMLTABLE += "</tr>";
                                }
                                else
                                {
                                    HTMLTABLE += "<tr>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt4.Rows[j]["Ledger_Name"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt4.Rows[j]["DebitAmt"].ToString() + "</b></td>";
                                    HTMLTABLE += "<td style='border:solid 1px #999; '><b>" + dt4.Rows[j]["CreditAmt"].ToString() + "<b></td>";
                                    HTMLTABLE += "</tr>";
                                }

                                if (dt4.Rows[j]["BillType"].ToString() == "Yes")
                                {
                                    dv = dsStatic.Tables[5].DefaultView;
                                    dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "' AND  Ledger_ID = '" + dt4.Rows[j]["Ledger_ID"].ToString() + "'";
                                    DataTable dtBill = dv.ToTable();

                                    if (dtBill.Rows.Count != 0)
                                    {
                                        ///**********Bill By Bill Ref Details**************////
                                        int tab2Count = dtBill.Rows.Count;
                                        for (int k = 0; k < tab2Count; k++)
                                        {
                                            HTMLTABLE += "<tr>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBill.Rows[k]["BillByBillTx_RefType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBill.Rows[k]["BillByBillTx_Ref"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBill.Rows[k]["BillByBillTx_Amount"].ToString() + " " + dtBill.Rows[k]["AmtType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td></td>";
                                            HTMLTABLE += "</tr>";
                                        }

                                    }
                                }
                                else if (dt4.Rows[j]["BankType"].ToString() == "Yes")
                                {
                                    dv = dsStatic.Tables[6].DefaultView;
                                    dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "' AND Ledger_ID = '" + dt4.Rows[j]["Ledger_ID"].ToString() + "'";
                                    DataTable dtBank = dv.ToTable();

                                    if (dtBank.Rows.Count != 0)
                                    {
                                        ///**********Cheque/DD Details**************/
                                        int tab3Count = dtBank.Rows.Count;
                                        for (int k = 0; k < tab3Count; k++)
                                        {
                                            HTMLTABLE += "<tr>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                           // HTMLTABLE += "<td style='border:solid 1px #999; '>Cheque/DD</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["InstrumentType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["ChequeTx_No"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["ChequeTx_Date"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["ChequeTx_Amount"].ToString() + " " + dtBank.Rows[k]["AmtType"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "</tr>";
                                        }
                                    }
                                }
                                else if (dt4.Rows[j]["CostCentre"].ToString() == "Yes")
                                {
                                    dv = dsStatic.Tables[7].DefaultView;
                                    dv.RowFilter = "VoucherTx_ID = '" + VoucherTx_ID + "'  AND  Ledger_ID = '" + dt1.Rows[j]["Ledger_ID"].ToString() + "'";
                                    DataTable dtBank = dv.ToTable();

                                    if (dtBank.Rows.Count != 0)
                                    {
                                        ///**********Cheque/DD Details**************/
                                        int tab3Count = dtBank.Rows.Count;
                                        for (int k = 0; k < tab3Count; k++)
                                        {
                                            HTMLTABLE += "<tr>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            //HTMLTABLE += "<td style='border:solid 1px #999; '>Cheque/DD</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["CategoryName"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["SubCategoryName"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '>" + dtBank.Rows[k]["Amount"].ToString() + "</td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                                            HTMLTABLE += "</tr>";
                                        }
                                    }
                                }

                            }
                        }

                        /****Narration Details****/

                        HTMLTABLE += "<tr>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '><b>Narration : </b>" + VoucherTx_Narration + "</td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "<td style='border:solid 1px #999; '></td>";
                        HTMLTABLE += "</tr>";


                    }


                }

                string headingFirst = "<p class='text-center' style='font-weight:600;'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Day Book<br /> " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                Response.Write("<table style='border:solid 1px #999; border-collapse: collapse;'>" +
                     "<tr>"
                + "<th style='border:solid 1px #999; background-color: peachpuff;' colspan='10'>" + headingFirst + "</th>"

                + "</tr>" +

                "<tr>"
                + "<th style='border:solid 1px #999; background-color: peachpuff;'>Date</th>"
                + "<th style='border:solid 1px #999; background-color: peachpuff;'>Particulars</th>"

                 + "<th style='border:solid 1px #999; background-color: peachpuff;'></th>"
                 + "<th style='border:solid 1px #999; background-color: peachpuff;'></th>"
                 + "<th style='border:solid 1px #999; background-color: peachpuff;'></th>"
                 + "<th style='border:solid 1px #999; background-color: peachpuff;'></th>"


                + "<th style='border:solid 1px #999; background-color: peachpuff;'>Vch Type</th>"
                + "<th style='border:solid 1px #999; background-color: peachpuff;'>Vch No.</th>"
                + "<th style='border:solid 1px #999; background-color: peachpuff;'>Debit  Amt</th>"
                + "<th style='border:solid 1px #999; background-color: peachpuff;'>Credit Amt</th>"

                + "</tr>"
                + HTMLTABLE

                + "</table>");

                // STYLE THE SHEET AND WRITE DATA TO IT.

                Response.Write("<style> TABLE { border:dotted 1px #999; border-collapse: collapse; } " +
                    "td { border:solid 1px #999; } </style>");
                Response.Write(objSW.ToString());

                Response.End();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
