using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptStatistics : System.Web.UI.Page
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
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ddlOffice.Enabled = false;
                    //txtFromDate.Attributes.Add("readonly", "readonly");
                    //txtFromDate.Enabled = false;
                    FillFromDate();
                    ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    ViewState["ToDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    FillDropdown();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
                    fill_details();

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
                txtToDate.Text = "01/04/" + (yy - 1).ToString();

            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
                txtToDate.Text = "01/04/" + (yy).ToString();
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
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
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        GridView1.DataSource = new string[] { };
        GridView1.DataBind();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            fill_details();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
    }

    private void fill_details()
    {
        try
        {
            GridView1.DataSource = null;
            GridView2.DataSource = null;
            ds = objdb.ByProcedure("SpFinRptStatistics",
                   new string[] { "flag", "Office_ID", "ToDate", "FromDate" },
                   new string[] { "0", ddlOffice.SelectedValue.ToString(), ViewState["ToDate"].ToString(), ViewState["FromDate"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblcurrencies.Text = "1";
                lblGroups.Text = ds.Tables[0].Rows[0]["GroupMaster"].ToString();
                lblStockGroups.Text = ds.Tables[0].Rows[0]["ItemGroupMaster"].ToString();
                lblVoucherType.Text = "8";
                lblUnits.Text = ds.Tables[0].Rows[0]["UnitMaster"].ToString();
                lblStockIems.Text = ds.Tables[0].Rows[0]["ItemMaster"].ToString();
                lblLedgers.Text = ds.Tables[0].Rows[0]["TotalLedger"].ToString();
            }

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();
                int TotalVoucher = ds.Tables[1].AsEnumerable().Sum(row => row.Field<int>("TotalVouchers"));
                GridView2.FooterRow.Cells[1].Text = "<b>" + TotalVoucher + "</b>";
                GridView2.FooterRow.Cells[0].Text = "Total";
            }
        }

        catch (Exception ex)
        {

            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        int index = GridView2.SelectedRow.RowIndex;
        GridViewRow row = GridView2.Rows[index];
        LinkButton Voucher_Name = (LinkButton)row.Cells[0].FindControl("LinkButton1");

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string VoucherTx_Type = Voucher_Name.Text;
            if (VoucherTx_Type == "Bank Payment")
            {
                VoucherTx_Type = "Payment";
            }
            else if (VoucherTx_Type == "Cash Receipt")
            {
                VoucherTx_Type = "Receipt";
            }
            FillGridNextLedger(VoucherTx_Type);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
    }
    protected void FillGridNextLedger(string Voucher_Name)
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpFinRptStatistics", new string[] { "flag", "Office_ID", "VoucherName", "FromDate", "ToDate" }, new string[] { "1", ddlOffice.SelectedValue.ToString(), Voucher_Name, ViewState["FromDate"].ToString(), ViewState["ToDate"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            //GridView2.DataSource = new string[] { };
            //GridView2.DataBind();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
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
        }
        catch (Exception ex)
        {
            ///lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}