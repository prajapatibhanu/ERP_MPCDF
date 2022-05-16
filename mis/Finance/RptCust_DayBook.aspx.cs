using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptCust_DayBook : System.Web.UI.Page
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
                    DateTime ToDate = new DateTime(int.Parse(YEAR[1]), 3, 31);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    FillDropdown();
                    btnPrint.Visible = false;
                    //FillGrid();

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
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
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
			string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
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
            btnPrint.Visible = false;
            string headingFirst = "<p class='text-center' style='font-weight:600'>" + ViewState["Office_FinAddress"].ToString() + " <br />  Custom Day Book<br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            //  GridView1.DataSource = new string[] { };

            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate", "FinancialYear" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"),FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnPrint.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                foreach (GridViewRow rows in GridView1.Rows)
                {
                    LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                    LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                    Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
                    LinkButton bankreceiptprint = (LinkButton)rows.FindControl("bankreceiptprint");
                    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
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
                        //if (lblVoucherTx_Type.Text == "Bank Receipt")
                        //{
                        //    bankreceiptprint.Visible = true;
                        //}
                        //else
                        //{
                        //    bankreceiptprint.Visible = false;
                        //}
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

                        //lnkDelete.Visible = true;
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
                        lnkEdit.Visible = false;
                        lnkDelete.Visible = false;
                        //hpEdit.Visible = false;
                }
                }
				
					

                //DataView dv = ds.Tables[0].DefaultView;





                //int i = 0;
                //foreach (GridViewRow rows in GridView1.Rows)
                //{

                //    HyperLink lnkEdit = (HyperLink)rows.FindControl("hpEdit");
                //    HyperLink hpView = (HyperLink)rows.FindControl("hpView");
                //    HyperLink hpprint1 = (HyperLink)rows.FindControl("hpprint1");
                //    HyperLink hpprint2 = (HyperLink)rows.FindControl("hpprint2");
                //    HiddenField HF_VoucherTx_ID = (HiddenField)rows.FindControl("HF_VoucherTx_ID");


                //    dv.RowFilter = " VoucherTx_ID = " + HF_VoucherTx_ID.Value.ToString();

                //    DataTable dt = dv.ToTable();

                //    string VoucherTx_Type = dt.Rows[0]["VoucherTx_Type"].ToString();
                //    string Url = dt.Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(dt.Rows[0]["VoucherTx_ID"].ToString());

                //    hpView.NavigateUrl = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dt.Rows[0]["Office_ID"].ToString());
                //    if (VoucherTx_Type == "CreditNote Voucher" || VoucherTx_Type == "DebitNote Voucher")
                //    {
                //        lnkEdit.Visible = false;
                //    }
                //    else
                //    {
                //        lnkEdit.Visible = true;
                //        lnkEdit.NavigateUrl = Url + "&Action=" + objdb.Encrypt("2");

                //    }
                //    if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
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
                //    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO")
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
                //    i++;

                //}

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            FillGrid();
            foreach (GridViewRow rows in GridView1.Rows)
            {
                LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
				Label lblV_Editright = (Label)rows.FindControl("lblV_Editright");
                if (lblOfficeID.Text == ViewState["Office_ID"].ToString())
                {
                    lnkEdit.Visible = true;
                    lnkDelete.Visible = true;
					if (lblV_Editright.Text == "No")
                    {
                        lnkEdit.Visible = false;
                        lnkDelete.Visible = false;
                        //hpEdit.Visible = false;
                    }
                }
                else
                {

                    lnkEdit.Visible = false;
                    lnkDelete.Visible = false;
                }

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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    lblMsg.Text = "";
        //    FillGrid();
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        GridView1.AllowPaging = false;
        ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Columns[7].Visible = false;
        }
        StringBuilder sb1 = new StringBuilder();
        sb1.Append("<p class='text-center' style='font-weight:600; font-size:16px; text-align:center'> Custom Day Book<br /> Madhya Pradesh State Cooperative Dairy Federation Ltd. <br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>");
        //sb1.Append("<div style='padding-bottom:20px; font-size:20px;'>Custom DayBook:  "+ddlOffice.SelectedItem.Text+"  (Date: " + txtFromDate.Text + " - " + txtToDate.Text + ")</div>");
        string gridHTML = sb1.ToString();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.RenderControl(hw);


        gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("window.onload = new function(){");

        sb.Append("var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');");

        sb.Append("WinPrint.document.write(\"");


        sb.Append(gridHTML);

        sb.Append("\");");

        sb.Append("WinPrint.document.close();");

        sb.Append("WinPrint.focus();");

        sb.Append("WinPrint.print();");

        sb.Append("WinPrint.close();};");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

        ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            GridView1.AllowPaging = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Columns[7].Visible = true;

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
    protected void btngraphical_Click(object sender, EventArgs e)
    {
        try
        {
            string OfficeID = objdb.Encrypt(ddlOffice.SelectedValue.ToString());
            string FromDate = objdb.Encrypt(txtFromDate.Text);
            string ToDate = objdb.Encrypt(txtToDate.Text);

            string url = "RptGraphicalCustDayBook.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}