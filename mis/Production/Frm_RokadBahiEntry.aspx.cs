using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


public partial class mis_Production_Frm_RokadBahiEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                CreateTable();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtTAmount.Attributes.Add("readonly", "readonly");
                txtTotalAmount.Attributes.Add("readonly", "readonly");
                txtDate.Attributes.Add("readonly", "readonly");
                txtDebitTotal.Attributes.Add("readonly", "readonly");
                txtLastAmount.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void CreateTable()
    {
        ViewState["ExpenseTable"] = "";
        DataTable dt_ExpenseTable = new DataTable();
        DataColumn RowNo = dt_ExpenseTable.Columns.Add("RowNo", typeof(int));
        dt_ExpenseTable.Columns.Add(new DataColumn("ClAccountName", typeof(string)));
        dt_ExpenseTable.Columns.Add(new DataColumn("Description", typeof(string)));
        dt_ExpenseTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["ExpenseTable"] = dt_ExpenseTable;

        GrExpenseTable.DataSource = dt_ExpenseTable;
        GrExpenseTable.DataBind();

        ViewState["OpeningDetail"] = "";
        DataTable dt_OpeningDetail = new DataTable();
        DataColumn OpRowNo = dt_OpeningDetail.Columns.Add("OpRowNo", typeof(int));
        dt_OpeningDetail.Columns.Add(new DataColumn("AccountName", typeof(string)));
        dt_OpeningDetail.Columns.Add(new DataColumn("OpDescription", typeof(string)));
        dt_OpeningDetail.Columns.Add(new DataColumn("OpAmount", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["OpeningDetail"] = dt_OpeningDetail;

        GvOpeningDetail.DataSource = dt_OpeningDetail;
        GvOpeningDetail.DataBind();
    }
    //protected void chkAddExpense_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkAddExpense.Checked == true)
    //    {
    //        ExpenseDetail.Visible = true;
    //    }
    //    else
    //    {
    //        ExpenseDetail.Visible = false;
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                string msg = "";
                int chkExpense = 0;
                if (txtDate.Text == "")
                {
                    msg += "Select Date. \\n";
                }
                if (txtTotalAmount.Text == "")
                {
                    msg += "Enter Total Amount. \\n";
                }
                if (txtClosingAccountName.Text == "")
                {
                    msg += "Enter Account Name. \\n";
                }
                if (txtMCCollection.Text == "")
                {
                    msg += "Enter Morning Cow Collection. \\n";
                }
                if (txtMCAmount.Text == "")
                {
                    msg += "Enter  Morning Buffalo Collection Amount. \\n";
                }
                if (txtMBCollection.Text == "")
                {
                    msg += "Enter Morning Buffalo Collection. \\n";
                }
                if (txtMBAmount.Text == "")
                {
                    msg += "Enter Morning Buffalo Collection Amount. \\n";
                }
                if (txtECCollection.Text == "")
                {
                    msg += "Enter Evening Cow Collection. \\n";
                }
                if (txtECAmount.Text == "")
                {
                    msg += "Enter Evening Cow Collection Amount. \\n";
                }
                if (txtEBCollection.Text == "")
                {
                    msg += "Enter Evening Buffalo Collection. \\n";
                }
                if (txtEBAmount.Text == "")
                {
                    msg += "Enter Evening Buffalo Collection Amount. \\n";
                }

                if (GrExpenseTable.Rows.Count == 0)
                {
                    msg += "Add Atlease one row in Expense Detail. \\n";
                }
                if (msg == "")
                {
                    if (GrExpenseTable.Rows.Count > 0 && GvOpeningDetail.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("USP_tblRokadBahiEntry", new string[] {"flag", "Date","TotalAmount","ClosingBalAccountName","MCCollection",  "MCCollectionAmt", "MBCollection","MBCollectionAmt",
					"ECCollection" ,"ECCollectionAmt" ,"EBCollection" ,"EBCollectionAmt" ,"CollectionTotalAmount","AddExpense","DebitTotalBal", "CurrentDayClosingBal","CreatedBy" },
                     new string[] { "1", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), txtTotalAmount.Text, txtClosingAccountName.Text, txtMCCollection.Text, txtMCAmount.Text, txtMBCollection.Text, txtMBAmount.Text,
                        txtECCollection.Text,  txtECAmount.Text, txtEBCollection.Text,  txtEBAmount.Text, txtTAmount.Text, "1", txtDebitTotal.Text, txtLastAmount.Text, objdb.createdBy() }, "dataset");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Status"].ToString() == "TRUE")
                            {
                                string RokadId = ds.Tables[0].Rows[0]["RokadID"].ToString();
                                foreach (GridViewRow row in GrExpenseTable.Rows)
                                {
                                    Label AccountName = (Label)row.FindControl("lblAccountName");
                                    Label Description = (Label)row.FindControl("lblDescription");
                                    Label Amount = (Label)row.FindControl("lblAmount");
                                    objdb.ByProcedure("USP_tblRokadBahiEntry", new string[] { "flag", "RokadID", "Date", "AccountName", "Description", "Amount", "CreatedBy" },
                        new string[] { "3", RokadId, Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), AccountName.Text, Description.Text, Amount.Text, objdb.createdBy() }, "dataset");
                                }

                                foreach (GridViewRow row in GvOpeningDetail.Rows)
                                {
                                    Label AccountName = (Label)row.FindControl("lblAccountName");
                                    Label Description = (Label)row.FindControl("lblDescription");
                                    Label Amount = (Label)row.FindControl("lblAmount");
                                    objdb.ByProcedure("USP_tblRokadBahiEntry", new string[] { "flag", "RokadID", "Date", "AccountName", "Description", "Amount", "CreatedBy" },
                        new string[] { "7", RokadId, Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), AccountName.Text, Description.Text, Amount.Text, objdb.createdBy() }, "dataset");
                                }
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Saved Successfully..");
                                ClearText();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                            }
                        }
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dt_ExpenseTable = (DataTable)ViewState["ExpenseTable"];
            dt_ExpenseTable.Rows.Add(null, txtExpensesAccName.Text, txtExpensesDescription.Text, txtExpenseAmount.Text);

            ViewState["ExpenseTable"] = dt_ExpenseTable;

            GrExpenseTable.DataSource = dt_ExpenseTable;
            GrExpenseTable.DataBind();

            decimal OpeningAmt = 0;

            decimal TotalAmt = dt_ExpenseTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            if (decimal.Parse(txtTAmount.Text) != 0)
            {
                OpeningAmt = decimal.Parse(txtTAmount.Text);
            }
            else
            {
                OpeningAmt = 0;
            }
            GrExpenseTable.FooterRow.Cells[2].Text = "<b>Total: </b>";
            GrExpenseTable.FooterRow.Cells[3].Text = "<b>" + TotalAmt.ToString() + "</b>";
            GrExpenseTable.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            GrExpenseTable.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            txtDebitTotal.Text = (TotalAmt + OpeningAmt).ToString();

            txtLastAmount.Text = (decimal.Parse(txtTotalAmount.Text) - decimal.Parse(txtDebitTotal.Text)).ToString();

            txtExpensesAccName.Text = "";
            txtExpensesDescription.Text = "";
            txtExpenseAmount.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtDate.Text = "";
        txtBankAccountName.Text = "";
        txtDescription.Text = "";
        txtAmount.Text = "";
        txtTotalAmount.Text = "";
        txtClosingAccountName.Text = "";
        txtMCCollection.Text = "";
        txtMCAmount.Text = "";
        txtMBCollection.Text = "";
        txtMBAmount.Text = "";
        txtECCollection.Text = "";
        txtECAmount.Text = "";
        txtEBCollection.Text = "";
        txtEBAmount.Text = "";
        txtTAmount.Text = "";
        txtExpensesAccName.Text = "";
        txtExpensesDescription.Text = "";
        txtExpensesDescription.Text = "";
        txtExpenseAmount.Text = "";
        CreateTable();
    }
    protected void GrExpenseTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dt_ExpenseTable = (DataTable)ViewState["ExpenseTable"];
            string ID = GrExpenseTable.DataKeys[e.RowIndex].Value.ToString();

            foreach (DataRow dr in dt_ExpenseTable.Rows)
            {
                if (dr["RowNo"].ToString() == ID)
                {
                    dt_ExpenseTable.Rows.Remove(dr);
                    break;
                }
            }
            dt_ExpenseTable.AcceptChanges();

            ViewState["ExpenseTable"] = dt_ExpenseTable;

            GrExpenseTable.DataSource = dt_ExpenseTable;
            GrExpenseTable.DataBind();

            decimal OpeningAmt = 0;

            decimal TotalAmt = dt_ExpenseTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            if (decimal.Parse(txtTAmount.Text) != 0)
            {
                OpeningAmt = decimal.Parse(txtTAmount.Text);
            }
            else
            {
                OpeningAmt = 0;
            }
            GrExpenseTable.FooterRow.Cells[2].Text = "<b>Total: </b>";
            GrExpenseTable.FooterRow.Cells[3].Text = "<b>" + TotalAmt.ToString() + "</b>";
            GrExpenseTable.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            GrExpenseTable.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            txtDebitTotal.Text = (TotalAmt + OpeningAmt).ToString();

            txtLastAmount.Text = (decimal.Parse(txtDebitTotal.Text) - decimal.Parse(txtTotalAmount.Text)).ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnOpAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dt_OpeningDetail = (DataTable)ViewState["OpeningDetail"];
            dt_OpeningDetail.Rows.Add(null, txtBankAccountName.Text, txtDescription.Text, txtAmount.Text);

            ViewState["OpeningDetail"] = dt_OpeningDetail;

            GvOpeningDetail.DataSource = dt_OpeningDetail;
            GvOpeningDetail.DataBind();

            decimal TotalAmt = dt_OpeningDetail.AsEnumerable().Sum(row => row.Field<decimal>("OpAmount"));

            GvOpeningDetail.FooterRow.Cells[2].Text = "<b>Total: </b>";
            GvOpeningDetail.FooterRow.Cells[3].Text = "<b>" + TotalAmt.ToString() + "</b>";
            GvOpeningDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            GvOpeningDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            txtTotalAmount.Text = TotalAmt.ToString();

            txtBankAccountName.Text = "";
            txtDescription.Text = "";
            txtAmount.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GvOpeningDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dt_OpeningDetail = (DataTable)ViewState["OpeningDetail"];
            string ID = GvOpeningDetail.DataKeys[e.RowIndex].Value.ToString();

            foreach (DataRow dr in dt_OpeningDetail.Rows)
            {
                if (dr["OpRowNo"].ToString() == ID)
                {
                    dt_OpeningDetail.Rows.Remove(dr);
                    break;
                }
            }
            dt_OpeningDetail.AcceptChanges();

            ViewState["ExpenseTable"] = dt_OpeningDetail;

            GvOpeningDetail.DataSource = dt_OpeningDetail;
            GvOpeningDetail.DataBind();

            decimal TotalAmt = dt_OpeningDetail.AsEnumerable().Sum(row => row.Field<decimal>("OpAmount"));

            GvOpeningDetail.FooterRow.Cells[2].Text = "<b>Total: </b>";
            GvOpeningDetail.FooterRow.Cells[3].Text = "<b>" + TotalAmt.ToString() + "</b>";
            GvOpeningDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            GvOpeningDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            txtTotalAmount.Text = TotalAmt.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("USP_tblRokadBahiEntry", new string[] { "flag", "Date" },
                   new string[] { "6", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtAmount.Text = ds.Tables[0].Rows[0]["CurrentDayClosingBal"].ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}