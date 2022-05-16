using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
public partial class mis_Finance_VoucherClosingStockAdjustment : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Office_Code"] = Session["Office_Code"].ToString();
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");

                    FillParticularsDropDown();


                    ds = null;
                    ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

                    }

                    FillData();

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

    //Fill LedgerDropdown
    protected void FillParticularsDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID" },
                new string[] { "67", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger_ID.DataSource = ds;
                ddlLedger_ID.DataTextField = "Ledger_Name";
                ddlLedger_ID.DataValueField = "Ledger_ID";
                ddlLedger_ID.DataBind();
                ddlLedger_ID.Items.Insert(0, "Select");

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Ledger Current Balance
    protected void ddlLedger_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlLedger_ID.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedger_ID.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalance.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ClearData()
    {
        try
        {

            ddlLedger_ID.ClearSelection();
            ddlcreditdebit.Enabled = false;
            ddlcreditdebit.SelectedValue = "Dr";
            ddlLedger_ID.ClearSelection();
            txtCurrentBalance.Text = "";
            txtLedgerTx_Amount.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillData()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (txtVoucherTx_Date.Text != "")
            {
                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
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
                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Office_ID", "LedgerTx_FY" }, new string[] { "12", ViewState["Office_ID"].ToString(), FinancialYear.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void btnRefreshLedgerList_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            FillParticularsDropDown();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date.\\n";
            }
            if (ddlLedger_ID.SelectedIndex == 0)
            {
                msg += "Select Ledger Name. ";
            }
            if (txtLedgerTx_Amount.Text == "")
            {
                msg += "Enter Amount.\\n";
            }

            if (msg.Trim() == "")
            {

                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
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
                string LedgerTx_Amount = txtLedgerTx_Amount.Text;
                if (ddlcreditdebit.SelectedItem.Text == "Debit")
                {
                    LedgerTx_Amount = "-" + LedgerTx_Amount;
                }
                string LedgerTx_IsActive = "1";
                if (btnAddLedger.Text == "Save")
                {
                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "VoucherTx_Date" },
                    new string[] { "9", ddlLedger_ID.SelectedValue.ToString(), "Closing Diff", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "0", "Sub Ledger", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                }
                else
                {
                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "LedgerTx_ID", "Ledger_ID", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "LedgerTx_InsertedBy", "VoucherTx_Date" },
                    new string[] { "13", ViewState["LedgerTx_ID"].ToString(), ddlLedger_ID.SelectedValue.ToString(), LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                }

                btnAddLedger.Text = "Save";
                ddlLedger_ID.ClearSelection();
                txtLedgerTx_Amount.Text = "";
                FillData();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["LedgerTx_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "LedgerTx_ID" }, new string[] { "14", ViewState["LedgerTx_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger_ID.ClearSelection();
                ddlLedger_ID.Items.FindByValue(ds.Tables[0].Rows[0]["Ledger_ID"].ToString()).Selected = true;

                ddlcreditdebit.ClearSelection();
                ddlcreditdebit.Items.FindByText(ds.Tables[0].Rows[0]["DrCr"].ToString()).Selected = true;

                txtLedgerTx_Amount.Text = ds.Tables[0].Rows[0]["LedgerTx_Amount"].ToString();

                if (ddlLedger_ID.SelectedIndex > 0)
                {

                    DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedger_ID.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                    }
                }
                btnAddLedger.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FillData();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}