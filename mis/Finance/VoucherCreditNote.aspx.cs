﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_VoucherCreditNote : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    //static DataSet dsBillByBill;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                ddlBillByBillTx_Ref.Visible = false;
                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["LedgerTotal"] = "0";
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["Action"] = "";
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    ddlcreditdebit.Enabled = false;
                    FillParticularsDropDown();
                    CreateLedgerTable();
                    CreateBillByBillDataSet();
                    FillVoucherDate();
                    FillVoucherNo();
                    GridViewBillByBillDetail.DataSource = new string[] { };
                    GridViewBillByBillDetail.DataBind();

                    GridViewLedgerDetail.DataSource = new string[] { };
                    GridViewLedgerDetail.DataBind();

                    DataTable dt_BillByBillData = new DataTable();
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    btnAccept.Enabled = false;

                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            FillDetail();
							string ValidStatus = ValidDate();
                            if (ValidStatus == "No")
                            {
                                Response.Redirect("~/mis/Login.aspx");
                            }
                        }
                        else if (Action == "1")
                        {
                            if (Request.QueryString["Office_ID"] != null)
                            {
                                ViewState["Office_ID"] = objdb.Decrypt(Request.QueryString["Office_ID"].ToString());
                                FillDetail();
                                ViewVoucher();
                            }
                            else
                            {
                                FillDetail();
                                ViewVoucher();
                            }
                        }

                    }
                    else
                    {
                        GetPreviousVoucherNo();
                    }

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

    //Fill Ledger DropDown
    protected void FillParticularsDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger_ID.DataSource = ds;
                ddlLedger_ID.DataTextField = "Ledger_Name";
                ddlLedger_ID.DataValueField = "Ledger_ID";
                ddlLedger_ID.DataBind();
                ddlLedger_ID.Items.Insert(0, "Select");

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill VoucherDate
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                FillVoucherNo();
            }
            //ds = null;
            //ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "7", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            //if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            //{
            //    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            //    FillVoucherNo();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    //Fill VoucherSeries
    protected void FillVoucherNo()
    {
        try
        {
            if (ViewState["VoucherTx_ID"].ToString() == "0")
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
                string VoucherTx_Names_ForSno = "Sales Return Voucher";

                DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                    new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");
                string Office_Code = "";
                if (ds1.Tables[1].Rows.Count != 0)
                {
                    Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
                }
                lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "CN";
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill LedgerCurrentBalance
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

    //Add Ledger/Amount Detail Event & Function
    protected void CreateLedgerTable()
    {
        try
        {
            //lblMsg.Text = "";
            ViewState["LedgerTable"] = "";
            DataTable dt_LedgerTable = new DataTable();
            DataColumn RowNo = dt_LedgerTable.Columns.Add("RowNo", typeof(int));
            dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
            dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
            //dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));
            RowNo.AutoIncrement = true;
            RowNo.AutoIncrementSeed = 1;
            RowNo.AutoIncrementStep = 1;
            ViewState["LedgerTable"] = dt_LedgerTable;


            GridViewLedgerDetail.DataSource = dt_LedgerTable;
            GridViewLedgerDetail.DataBind();
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
            if (ddlLedger_ID.SelectedIndex <= 0)
            {
                msg = "Select Particulars.\\n";
            }
            if (txtLedgerTx_Amount.Text == "")
            {
                msg += "Enter Amount.\\n";
            }

            if (msg == "")
            {
                string LedgerId = ddlLedger_ID.SelectedValue;
                int status = 0;
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                lnkView.Visible = false;
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                BindBillByBillData();
                if (ddlcreditdebit.SelectedValue == "Dr")
                {
                    ViewState["LedgerAmount"] = "-" + txtLedgerTx_Amount.Text;
                }
                else
                {
                    ViewState["LedgerAmount"] = txtLedgerTx_Amount.Text;
                }
                ViewState["Amount"] = txtLedgerTx_Amount.Text;
                ViewState["BillByBillAmount"] = "0";
                txtBillByBillTx_Amount.Text = txtLedgerTx_Amount.Text;
                ddlRefType.ClearSelection();
                ddlBillByBillTx_crdr.SelectedValue = ddlcreditdebit.SelectedValue;
                txtBillByBillTx_Ref.Enabled = true;
                txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;

                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                    {
                        CreateBillByBillTable();

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
                    }
                    else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                    {
                        btnAddCheque.Enabled = true;
                        CreatTableFinChequeTx();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                    }
                    else
                    {
                        CreateBillByBillTable();
                        DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                        decimal LedgerTx_Credit;
                        decimal LedgerTx_Debit;
                        if (ddlcreditdebit.SelectedItem.Text == "Credit")
                        {
                            LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                            LedgerTx_Debit = 0;
                        }
                        else
                        {
                            LedgerTx_Credit = 0;
                            LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                        }
                        string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                        dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);

                        GridViewLedgerDetail.DataSource = dt_LedgerTable;
                        GridViewLedgerDetail.DataBind();

                        decimal LedgerCreditTotal = 0;
                        decimal LedgerDebitTotal = 0;

                        LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                        LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

                        GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                        GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                        GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";


                        ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                        ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
                        if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                        {
                            btnAccept.Enabled = true;
                        }
                        else
                        {
                            btnAccept.Enabled = false;
                        }
                        ViewState["LedgerTable"] = dt_LedgerTable;
                        ClearBillByBillModal();
                        txtCurrentBalance.Text = "";
                        decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                        if (ReaminingBal.ToString().Contains("-"))
                        {
                            ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                            txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                            ddlcreditdebit.SelectedValue = "Dr";
                        }
                        else
                        {
                            txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                            ddlcreditdebit.SelectedValue = "Cr";

                        }
                        ddlcreditdebit.Enabled = true;
                    }


                }

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
    protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

            //int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            int RowNo = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            int rowindex = int.Parse(GridViewLedgerDetail.SelectedRow.RowIndex.ToString());
            Label lbl = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("lblMaintainType");
            if (lbl.Text == "BillByBill")
            {
                GridViewBillByBillViewDetail.DataSource = dsBillByBill.Tables[RowNo.ToString()];
                GridViewBillByBillViewDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillByBillViewModal();", true);
            }

            else if (lbl.Text == "Cheque")
            {
                GVViewFinChequeTx.DataSource = dsBillByBill.Tables[RowNo.ToString()];
                GVViewFinChequeTx.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);
            }
            else
            {

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }



    }
    protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int RowNo = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            DataSet dsBillByBillTemp = new DataSet();
            dsBillByBillTemp = dsBillByBill;
            for (int i = 0; i < dsBillByBillTemp.Tables.Count;


                i++)
            {
                if (dsBillByBillTemp.Tables[i].TableName == RowNo.ToString())
                {
                    dsBillByBill.Tables.Remove(dsBillByBillTemp.Tables[i].TableName);
                    //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                }
            }



            DataTable dt_LedgerTableTemp = new DataTable();
            DataColumn TempRowNo = dt_LedgerTableTemp.Columns.Add("RowNo", typeof(int));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
            //dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));
            TempRowNo.AutoIncrement = true;
            TempRowNo.AutoIncrementSeed = 1;
            TempRowNo.AutoIncrementStep = 1;

            int gridRows = GridViewLedgerDetail.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
                Label lblMaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
                Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
                Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
                //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
                if (lblRowNumber.Text != RowNo.ToString())
                {

                    dt_LedgerTableTemp.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);
                }
            }
            GridViewLedgerDetail.DataSource = null;
            GridViewLedgerDetail.DataBind();
            GridViewLedgerDetail.DataSource = dt_LedgerTableTemp;
            GridViewLedgerDetail.DataBind();
            decimal LedgerCreditTotal = 0;
            decimal LedgerDebitTotal = 0;

            LedgerCreditTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
            LedgerDebitTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

            GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
            GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
            GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
            GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;


            ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
            ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
            if (ViewState["LedgerCreditTotal"] == "0")
            {
                ddlcreditdebit.Enabled = false;
            }
            else
            {
                ddlcreditdebit.Enabled = true;
            }
            if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
            {
                btnAccept.Enabled = true;
            }
            else
            {
                btnAccept.Enabled = false;
            }

            ViewState["LedgerTable"] = dt_LedgerTableTemp;
            if (dt_LedgerTableTemp.Rows.Count == 0)
            {
                ddlcreditdebit.Enabled = false;
                ddlcreditdebit.SelectedValue = "Dr";
                FillParticularsDropDown();
                txtLedgerTx_Amount.Text = "";

            }
            else
            {
                decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                if (ReaminingBal.ToString().Contains("-"))
                {
                    ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Dr";
                }
                else
                {
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Cr";

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //Add BillByBillDetail Event & Function
    protected void CreateBillByBillDataSet()
    {
        DataSet dsBillByBill = new DataSet();
        ViewState["dsBillByBill"] = dsBillByBill;

    }
    protected void CreateBillByBillTable()
    {
        int TNO = 0;
        int count = GridViewLedgerDetail.Rows.Count;
        if (count > 0)
        {
            foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
            {
                Label rowno = (Label)rows.FindControl("lblRowNumber");
                TNO = int.Parse(rowno.Text);
            }
            TNO = TNO + 1;
        }
        else
        {
            TNO = TNO + 1;
        }
        //ViewState["TableId"] = Convert.ToInt32(ViewState["TableId"].ToString()) + 1;
        DataTable dt_BillByBillTable = new DataTable(TNO.ToString());
        dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
        ViewState["BillByBillTable"] = dt_BillByBillTable;

        GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        GridViewBillByBillDetail.DataBind();
    }
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
            if (ddlRefType.SelectedValue.ToString() == "1")
            {
                txtBillByBillTx_Ref.Visible = false;
                ddlBillByBillTx_Ref.Visible = true;
                lnkView.Visible = true;
                //txtBillByBillTx_Ref.Enabled = false;

            }
            else if (ddlRefType.SelectedValue.ToString() == "3")
            {
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                txtBillByBillTx_Ref.Enabled = false;
                txtBillByBillTx_Ref.Text = "On Account";
            }
            else
            {
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                if (GridViewBillByBillDetail.Rows.Count < 1)
                {
                    txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    ddlBillByBillTx_crdr.SelectedValue = ddlcreditdebit.SelectedValue;
                }
                else
                {

                }
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                txtBillByBillTx_Ref.Enabled = true;
                lnkView.Visible = false;


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Bind Ledger AgstRef Detail in BillByBillModal
    protected void BindBillByBillData()
    {
        try
        {
            if (btnAccept.Text == "Accept")
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                ddlBillByBillTx_Ref.Items.Clear();
                string LedgerID = ddlLedger_ID.SelectedValue.ToString();
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "2", LedgerID, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["AgnstRef"] = ds.Tables[0].Rows[0]["AgnstRef"].ToString();
                    dt_BillByBillData = ds.Tables[0];
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
                    ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
                    ddlBillByBillTx_Ref.DataBind();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = ds.Tables[0];
                    GridViewRefDetail.DataBind();

                }
                else
                {

                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }
            }
            else
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                ddlBillByBillTx_Ref.Items.Clear();
                string LedgerID = ddlLedger_ID.SelectedValue.ToString();
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID", "VoucherTx_ID" }, new string[] { "12", LedgerID, ViewState["Office_ID"].ToString(), ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["AgnstRef"] = ds.Tables[0].Rows[0]["AgnstRef"].ToString();
                    dt_BillByBillData = ds.Tables[0];
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
                    ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
                    ddlBillByBillTx_Ref.DataBind();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = ds.Tables[0];
                    GridViewRefDetail.DataBind();

                }
                else
                {

                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnAddBillByBill_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string LedgerAmount = BillAmount("0");

            // if (float.Parse(txtLedgerTx_Amount.Text) != float.Parse(LedgerAmount))
            decimal Status = decimal.Parse(ViewState["LedgerAmount"].ToString()) - decimal.Parse(LedgerAmount.ToString());
            if (Status == 0)
            {
                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);

                ViewState["dsBillByBill"] = dsBillByBill;

                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                decimal LedgerTx_Credit;
                decimal LedgerTx_Debit;
                if (ddlcreditdebit.SelectedItem.Text == "Credit")
                {
                    LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                    LedgerTx_Debit = 0;
                }
                else
                {
                    LedgerTx_Credit = 0;
                    LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                }
                string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "BillByBill", LedgerTx_Credit, LedgerTx_Debit);

                GridViewLedgerDetail.DataSource = dt_LedgerTable;
                GridViewLedgerDetail.DataBind();

                decimal LedgerCreditTotal = 0;
                decimal LedgerDebitTotal = 0;

                LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

                GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

                ViewState["LedgerTable"] = dt_LedgerTable;
                if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                {
                    btnAccept.Enabled = true;
                }
                else
                {
                    btnAccept.Enabled = false;
                }
                ClearBillByBillModal();


                //LedgerDebitTotal = decimal.Parse("-" + LedgerDebitTotal);
                decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                if (ReaminingBal.ToString().Contains("-"))
                {
                    ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Dr";
                }
                else
                {
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Cr";

                }
                txtCurrentBalance.Text = "";
                //if (ddlcreditdebit.SelectedValue == "Cr")
                //{
                //    ddlcreditdebit.SelectedValue = "Dr";
                //}
                //else
                //{
                //    ddlcreditdebit.SelectedValue = "Cr";

                //}
                ddlcreditdebit.Enabled = true;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
                txtBillByBillTx_Ref.Visible = true;
                txtBillByBillTx_Ref.Text = lblVoucherNo.Text + txtVoucherTx_No.Text;
                if (Status.ToString().Contains("-"))
                {

                    txtBillByBillTx_Amount.Text = Status.ToString();
                    txtBillByBillTx_Amount.Text = txtBillByBillTx_Amount.Text.Replace(@"-", string.Empty);

                }
                else
                {
                    txtBillByBillTx_Amount.Text = Status.ToString();
                }
                //BindBillByBillData();
                //ddlRefType.ClearSelection();
                //ddlBillByBillTx_crdr.ClearSelection();
                if (Status.ToString().Contains("-"))
                {

                    ddlBillByBillTx_crdr.SelectedValue = "Dr";


                }
                else
                {
                    ddlBillByBillTx_crdr.SelectedValue = "Cr";
                }

                ddlBillByBillTx_Ref.ClearSelection();
                if (ddlRefType.SelectedValue == "1")
                {
                    ddlRefType.SelectedValue = "1";
                    lnkView.Visible = true;
                    txtBillByBillTx_Ref.Visible = false;
                    ddlBillByBillTx_Ref.Visible = true;
                }
                else
                {
                    lnkView.Visible = false;
                    txtBillByBillTx_Ref.Visible = true;
                    ddlBillByBillTx_Ref.Visible = false;
                }
                //txtBillByBillTx_Ref.Enabled = true;
                //ddlBillByBillTx_Ref.Visible = false;
                //ddlBillByBillTx_crdr.SelectedValue = ddlcreditdebit.SelectedValue;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected string BillAmount(string ID)
    {
        decimal LedgerAmount = 0;
        try
        {

            DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
            string Type = "";
            if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
            {
                Type = "Cr";
            }
            else
            {
                Type = "Dr";
            }

            if (ddlRefType.SelectedItem.Text == "Agst Ref")
            {
                dt_BillByBillTable.Rows.Add(ddlLedger_ID.SelectedValue.ToString(), ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedValue, txtBillByBillTx_Amount.Text, Type);
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

            }
            else
            {
                dt_BillByBillTable.Rows.Add(ddlLedger_ID.SelectedValue.ToString(), ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text, Type);
            }


            //if (ddlBillByBillTx_crdr.SelectedValue == ddlcreditdebit.SelectedValue)
            //{

            //    Decimal Amnt = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtBillByBillTx_Amount.Text)); 
            //    //ViewState["Amount"] = Amnt.ToString();
            //    txtBillByBillTx_Amount.Text = Amnt.ToString();

            //}
            //else
            //{
            //    Decimal Amnt = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(txtBillByBillTx_Amount.Text));
            //    //ViewState["Amount"] = Amnt.ToString();
            //    txtBillByBillTx_Amount.Text = Amnt.ToString();
            //}
            GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
            GridViewBillByBillDetail.DataBind();
            foreach (GridViewRow rows in GridViewBillByBillDetail.Rows)
            {
                decimal Amount = 0;
                Label lblAmount = (Label)rows.FindControl("lblAmount");
                Label lblType = (Label)rows.FindControl("lblType");
                if (lblType.Text == "Dr")
                {
                    Amount = decimal.Parse("-" + lblAmount.Text);
                }
                else
                {
                    Amount = decimal.Parse(lblAmount.Text);
                }
                LedgerAmount = LedgerAmount + Amount;
            }

            //if (dt_BillByBillTable.Rows.Count > 0)
            //{

            //    decimal Amt = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));
            //    LedgerAmount = Amt.ToString();
            //}
            //if (ViewState["Amount"].ToString() == "0" || ViewState["Amount"].ToString() == "0.00")
            //{
            //    btnAddBillByBill.Enabled = false;
            //    btnBillByBillSave.Enabled = true;
            //}

            ViewState["BillByBillTable"] = dt_BillByBillTable;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        //return ViewState["BillByBillAmount"].ToString();
        return LedgerAmount.ToString();
    }
    protected void ClearBillByBillModal()
    {
        ddlRefType.ClearSelection();
        ddlBillByBillTx_Ref.ClearSelection();
        txtBillByBillTx_Ref.Text = "";
        ddlLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";
        ViewState["BillByBillTable"] = "";
        txtCurrentBalance.Text = "";

    }

    //AddChequeDetail Event & Function
    protected void CreatTableFinChequeTx()
    {
        int TNO = 0;
        int count = GridViewLedgerDetail.Rows.Count;
        if (count > 0)
        {
            foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
            {
                Label rowno = (Label)rows.FindControl("lblRowNumber");
                TNO = int.Parse(rowno.Text);
            }
            TNO = TNO + 1;
        }
        else
        {
            TNO = TNO + 1;
        }
        DataTable dt_FinChequeTx = new DataTable(TNO.ToString());
        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));

        ViewState["FinChequeTx"] = dt_FinChequeTx;

        GVFinChequeTx.DataSource = dt_FinChequeTx;
        GVFinChequeTx.DataBind();
    }
    protected void btnAddCheque_Click(object sender, EventArgs e)
    {

        string msg = "";
        //if (txtChequeTx_No.Text == "")
        //{
        //    msg += "Enter Cheque/ DD No \\n";
        //}
        //if (txtChequeTx_Date.Text == "")
        //{
        //    msg += "Enter Cheque/ DD Date \\n";
        //}
        if (txtChequeTx_Amount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (msg == "")
        {
            string CheqAmount = ChequeAmount("0");
            if (float.Parse(txtLedgerTx_Amount.Text) != float.Parse(CheqAmount))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);

            }
            else
            {
                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                dsBillByBill.Merge((DataTable)ViewState["FinChequeTx"]);

                ViewState["dsBillByBill"] = dsBillByBill;

                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                decimal LedgerTx_Credit;
                decimal LedgerTx_Debit;
                if (ddlcreditdebit.SelectedItem.Text == "Credit")
                {
                    LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                    LedgerTx_Debit = 0;
                }
                else
                {
                    LedgerTx_Credit = 0;
                    LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                }
                string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "Cheque", LedgerTx_Credit, LedgerTx_Debit);

                GridViewLedgerDetail.DataSource = dt_LedgerTable;
                GridViewLedgerDetail.DataBind();

                decimal LedgerCreditTotal = 0;
                decimal LedgerDebitTotal = 0;
                LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

                if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                {
                    btnAccept.Enabled = true;
                }
                else
                {
                    btnAccept.Enabled = false;
                }



                ViewState["LedgerTable"] = dt_LedgerTable;

                ClearFinChequeTxModal();
                //LedgerDebitTotal = decimal.Parse("-" + LedgerDebitTotal);
                decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                if (ReaminingBal.ToString().Contains("-"))
                {
                    ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Dr";
                }
                else
                {
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Cr";

                }
                txtCurrentBalance.Text = "";
                //if (ddlcreditdebit.SelectedValue == "Cr")
                //{
                //    ddlcreditdebit.SelectedValue = "Dr";
                //}
                //else
                //{
                //    ddlcreditdebit.SelectedValue = "Cr";

                //}
                ddlcreditdebit.Enabled = true;
            }

        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected string ChequeAmount(string ID)
    {

        string CheqAmount = "0";
        try
        {
            DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];

            dt_FinChequeTx.Rows.Add(ddlLedger_ID.SelectedValue.ToString(), txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text);


            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();

            decimal ChequeTx_AmountTotal = 0;

            ChequeTx_AmountTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));

            //GVFinChequeTx.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GVFinChequeTx.FooterRow.Cells[3].Text = "<b>" + ChequeTx_AmountTotal.ToString() + "</b>";

            txtChequeTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - ChequeTx_AmountTotal).ToString();

            txtChequeTx_No.Text = "";
            txtChequeTx_Date.Text = "";
            if (dt_FinChequeTx.Rows.Count > 0)
            {

                decimal Amt = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));
                CheqAmount = Amt.ToString();
            }

            ViewState["FinChequeTx"] = dt_FinChequeTx;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return CheqAmount.ToString();
    }
    protected void ClearFinChequeTxModal()
    {
        txtChequeTx_No.Text = "";
        txtChequeTx_Date.Text = "";
        txtChequeTx_Amount.Text = "";

        ViewState["FinChequeTx"] = "";

        GVFinChequeTx.DataSource = new string[] { };
        GVFinChequeTx.DataBind();


        ddlLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";
        txtCurrentBalance.Text = "";



    }

    //Save Data   
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            decimal CGST = 0;
            decimal SGST = 0;
            decimal IGST = 0;
            decimal CGSTAmt = 0;
            decimal SGSTAmt = 0;
            decimal IGSTAmt = 0;
            string HSN_Code = "";
            string Isreversechargeapplicable = "";
            string GSTApplicable = "No";
            string Taxbility = "";
            string IsIneligibleforinputcredit = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
			else
            {
                string ValidStatus = ValidDate();
                if (ValidStatus == "No")
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
            if (msg == "")
            {
                string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
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

                int Status = 0;
                DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                    new string[] { "9", VoucherTx_No, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
                {
                    string GSTVoucher = "No";
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                  new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "GSTVoucher" },
                  new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "CreditNote Voucher", "CreditNote Voucher", VoucherTx_No, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", ViewState["Emp_ID"].ToString(), "No" }, "dataset");

                    string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();

                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                    {
                        string LedgerTx_Amount = "";
                        string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", Ledger_ID.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                            {
                                GSTVoucher = "Yes";

                                if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                                {
                                    GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                                }
                                Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                                CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                                Isreversechargeapplicable = ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString();
                                IsIneligibleforinputcredit = ds.Tables[0].Rows[0]["IsIneligibleforinputcredit"].ToString();
                                if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                {
                                    LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();

                                }
                                else
                                {
                                    LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                }
                                DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", Ledger_ID.ToString() }, "dataset");
                                {
                                    if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                                        {
                                            CGST = 0;
                                            SGST = 0;

                                        }
                                        else
                                        {
                                            IGST = 0;

                                        }
                                    }
                                }


                                decimal Amount = decimal.Parse(LedgerTx_Amount);
                                CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                                SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                                IGSTAmt = 0;
                                HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                            }
                            else
                            {
                                CGST = 0;
                                SGST = 0;
                                IGST = 0;
                                CGSTAmt = 0;
                                SGSTAmt = 0;
                                IGSTAmt = 0;
                                HSN_Code = "";
                                Isreversechargeapplicable = "";
                                GSTApplicable = "No";
                                Taxbility = "";
                                IsIneligibleforinputcredit = "";
                                objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", VoucherTx_ID, "No" }, "dataset");

                            }
                        }

                        if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                        {
                            if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                            }

                            int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                            new string[] { "0", Ledger_ID, VoucherTx_ID, "CreditNote Voucher", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "BillByBill", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                            DataSet dsBillByBillTemp = new DataSet();
                            dsBillByBillTemp = dsBillByBill;
                            for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                            {
                                if (dsBillByBillTemp.Tables[j].TableName == RowNo.ToString())
                                {
                                    for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                    {
                                        string Type = dsBillByBillTemp.Tables[j].Rows[k]["Type"].ToString();
                                        string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                                        string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                                        string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                                        if (Type == "Dr")
                                        {
                                            BillByBillTx_Amount = "-" + BillByBillTx_Amount;
                                        }

                                        objdb.ByProcedure("SpFinBillByBillTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "LedgerTx_OrderBy" },
                                        new string[] { "3", VoucherTx_ID, Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", (k + 1).ToString(), RowNo.ToString() }, "dataset");
                                    }
                                }
                            }
                        }

                        else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "Cheque")
                        {
                            if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                            }

                            int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                            new string[] { "0", Ledger_ID, VoucherTx_ID, "CreditNote Voucher", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "Cheque", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");

                            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                            for (int j = 0; j < dsBillByBill.Tables.Count; j++)
                            {
                                if (dsBillByBill.Tables[j].TableName == RowNo.ToString())
                                {
                                    for (int k = 0; k < dsBillByBill.Tables[j].Rows.Count; k++)
                                    {
                                        string ChequeTx_No = dsBillByBill.Tables[j].Rows[k]["ChequeTx_No"].ToString().Trim();
                                        if (ChequeTx_No == "")
                                        {
                                            ChequeTx_No = "";
                                        }
                                        else
                                        {

                                        }
                                        string ChequeTx_Date = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                        if (ChequeTx_Date == "")
                                        {
                                            ChequeTx_Date = "";
                                        }
                                        else
                                        {
                                            ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                        }
                                        string ChequeTx_Amount = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Amount"].ToString().Trim();
                                        objdb.ByProcedure("SpFinChequeTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy", "LedgerTx_OrderBy" },
                                        new string[] { "1", VoucherTx_ID, Ledger_ID, "CreditNote Voucher", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString(), RowNo.ToString() }, "dataset");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                            }
                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                            new string[] { "0", Ledger_ID, VoucherTx_ID, "CreditNote Voucher", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                        }
                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", VoucherTx_ID, GSTVoucher }, "dataset");

                    }
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    ClearData();



                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    string GSTVoucher = "No";
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "GSTVoucher" },
                 new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "CreditNote Voucher", "CreditNote Voucher", VoucherTx_No, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString(), "No" }, "dataset");

                    objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");

                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                    {
                        string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        string LedgerTx_Amount = "";
                        ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", Ledger_ID.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                            {
                                GSTVoucher = "Yes";

                                if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                                {
                                    GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                                }
                                Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                                CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                                Isreversechargeapplicable = ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString();
                                if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                {
                                    LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();

                                }
                                else
                                {
                                    LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                }

                                DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", Ledger_ID.ToString() }, "dataset");
                                {
                                    if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                                        {
                                            CGST = 0;
                                            SGST = 0;

                                        }
                                        else
                                        {
                                            IGST = 0;

                                        }
                                    }
                                }


                                decimal Amount = decimal.Parse(LedgerTx_Amount);
                                CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                                SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                                IGSTAmt = 0;
                                HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                            }
                            else
                            {
                                CGST = 0;
                                SGST = 0;
                                IGST = 0;
                                CGSTAmt = 0;
                                SGSTAmt = 0;
                                IGSTAmt = 0;
                                HSN_Code = "";
                                Isreversechargeapplicable = "";
                                GSTApplicable = "No";
                                Taxbility = "";
                                IsIneligibleforinputcredit = "";
                                objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", ViewState["VoucherTx_ID"].ToString(), "No" }, "dataset");

                            }
                        }


                        if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "Cheque")
                        {
                            if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                            }
                            int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                            new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "CreditNote Voucher", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "Cheque", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");

                            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                            for (int j = 0; j < dsBillByBill.Tables.Count; j++)
                            {
                                if (dsBillByBill.Tables[j].TableName == RowNo.ToString())
                                {
                                    for (int k = 0; k < dsBillByBill.Tables[j].Rows.Count; k++)
                                    {

                                        string ChequeTx_No = dsBillByBill.Tables[j].Rows[k]["ChequeTx_No"].ToString().Trim();
                                        if (ChequeTx_No == "")
                                        {
                                            ChequeTx_No = "";
                                        }
                                        else
                                        {

                                        }
                                        string ChequeTx_Date = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Date"].ToString().Trim();
                                        if (ChequeTx_Date == "")
                                        {
                                            ChequeTx_Date = "";
                                        }
                                        else
                                        {
                                            ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                        }
                                        string ChequeTx_Amount = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                        objdb.ByProcedure("SpFinChequeTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy", "LedgerTx_OrderBy" },
                                        new string[] { "1", ViewState["VoucherTx_ID"].ToString(), Ledger_ID, "CreditNote Voucher", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString(), RowNo.ToString() }, "dataset");
                                    }
                                }
                            }
                        }
                        else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                        {
                            if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                            }

                            int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                            new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "CreditNote Voucher", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "BillByBill", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                            DataSet dsBillByBillTemp = new DataSet();
                            dsBillByBillTemp = dsBillByBill;
                            for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                            {
                                if (dsBillByBillTemp.Tables[j].TableName == RowNo.ToString())
                                {
                                    for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                    {
                                        string Type = dsBillByBillTemp.Tables[j].Rows[k]["Type"].ToString();
                                        string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                                        string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                                        string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                                        if (Type == "Dr")
                                        {
                                            BillByBillTx_Amount = "-" + BillByBillTx_Amount;
                                        }

                                        objdb.ByProcedure("SpFinBillByBillTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "LedgerTx_OrderBy" },
                                        new string[] { "3", ViewState["VoucherTx_ID"].ToString(), Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", (k + 1).ToString(), RowNo.ToString() }, "dataset");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                            }
                            objdb.ByProcedure("SpFinLedgerTx",
                           new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                           new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "CreditNote Voucher", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                        }
                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", ViewState["VoucherTx_ID"].ToString(), GSTVoucher }, "dataset");

                    }

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No already exists');", true);
                }


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


    protected void ClearData()
    {
        try
        {
            txtVoucherTx_No.Text = "";
            ddlLedger_ID.ClearSelection();
            txtVoucherTx_Narration.Text = "";
            GridViewLedgerDetail.DataSource = new string[] { };
            GridViewLedgerDetail.DataBind();
            ddlcreditdebit.Enabled = false;
            ddlcreditdebit.SelectedValue = "Cr";
            CreateLedgerTable();
            ddlLedger_ID.ClearSelection();
            txtCurrentBalance.Text = "";
            txtLedgerTx_Amount.Text = "";
            btnAccept.Enabled = false;
            FillVoucherNo();
            GetPreviousVoucherNo();
            FillParticularsDropDown();
            CreateBillByBillDataSet();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                new string[] { "14", "Payment", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }


    //View RefDetail
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowRefDetailModal();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //Add New Ledger
    protected void lbkbtnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"].ToString() == "1")
            {
                Response.Redirect("LedgerMasterB.aspx");
            }
            else
            {
                Response.Redirect("LedgerMaster_Forotherofc.aspx");
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
    //Voucher Detail 
    protected void FillDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "11", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Fstring = "";
                    string Lstring = "";
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    if (ds.Tables[0].Rows[0]["Office_ID"].ToString() == "1")
                    {

                        Fstring = str.Substring(0, 9);
                        Lstring = str.Substring(9, str.Length - 9);


                    }
                    else
                    {
                        Fstring = str.Substring(0, 10);
                        Lstring = str.Substring(10, str.Length - 10);
                    }
                    //txtVoucherTx_No.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_No.Text = Lstring;
                    lblVoucherTx_No.Text = Fstring;


                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewLedgerDetail.DataSource = ds.Tables[1];
                    GridViewLedgerDetail.DataBind();
                    decimal LedgerCreditTotal = 0;
                    decimal LedgerDebitTotal = 0;
                    LedgerCreditTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                    LedgerDebitTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                    GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                    ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                    ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

                    if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                    {
                        btnAccept.Enabled = true;
                    }
                    else
                    {
                        btnAccept.Enabled = false;
                    }
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    //GridViewLedgerDetail.DataSource = ds.Tables[1];
                    //GridViewLedgerDetail.DataBind();
                    int gridRows = GridViewLedgerDetail.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                        Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                        Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
                        Label lblMaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
                        Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                        Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
                        Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
                        //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");

                        dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);

                    }

                    ViewState["LedgerTable"] = dt_LedgerTable;



                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    int rowcount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string Ledger_ID = ds.Tables[2].Rows[i]["Ledger_ID"].ToString();
                        string ChequeTx_No = ds.Tables[2].Rows[i]["ChequeTx_No"].ToString();
                        string ChequeTx_Date = ds.Tables[2].Rows[i]["ChequeTx_Date"].ToString();
                        string ChequeTx_Amount = ds.Tables[2].Rows[i]["ChequeTx_Amount"].ToString();
                        string TNO = ds.Tables[2].Rows[i]["LedgerTx_OrderBy"].ToString();

                        DataTable dt_FinChequeTx = new DataTable(TNO);
                        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));

                        dt_FinChequeTx.Rows.Add(Ledger_ID, ChequeTx_No, ChequeTx_Date, ChequeTx_Amount);
                        dsBillByBill.Merge(dt_FinChequeTx);
                        ViewState["FinChequeTx"] = dt_FinChequeTx;
                        ViewState["dsBillByBill"] = dsBillByBill;

                    }
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    int rowscount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < rowscount; i++)
                    {
                        string Ledger_ID = ds.Tables[3].Rows[i]["Ledger_ID"].ToString();
                        string BillByBillTx_RefType = ds.Tables[3].Rows[i]["BillByBillTx_RefType"].ToString();
                        string BillByBillTx_Ref = ds.Tables[3].Rows[i]["BillByBillTx_Ref"].ToString();
                        string BillByBillTx_Amount = ds.Tables[3].Rows[i]["BillByBillTx_Amount"].ToString();
                        string Type = ds.Tables[3].Rows[i]["BillByBillTxType"].ToString();
                        string TNO = ds.Tables[3].Rows[i]["LedgerTx_OrderBy"].ToString();
                        DataTable dt_BillByBillTable = new DataTable(TNO);
                        dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));

                        dt_BillByBillTable.Rows.Add(Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type);
                        dsBillByBill.Merge(dt_BillByBillTable);
                        ViewState["BillByBillTable"] = dt_BillByBillTable;
                        ViewState["dsBillByBill"] = dsBillByBill;


                    }


                }

                btnAccept.Text = "Update";
                btnClear.Visible = false;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Narration
    protected void FillNarration()
    {
        try
        {
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            if (dsBillByBill != null)
            {
                int count = dsBillByBill.Tables.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (dsBillByBill.Tables[i].Rows.Count > 0)
                        {
                            if (dsBillByBill.Tables[i].Columns.Contains("ChequeTx_No"))
                            {
                                string ChequeTx_No = dsBillByBill.Tables[i].Rows[0]["ChequeTx_No"].ToString();
                                string ChequeTx_Date = dsBillByBill.Tables[i].Rows[0]["ChequeTx_Date"].ToString();
                                string ChequeTx_Amount = dsBillByBill.Tables[i].Rows[0]["ChequeTx_Amount"].ToString();
                                if (ChequeTx_No == "")
                                {
                                    ChequeTx_No = "NA";
                                }
                                if (ChequeTx_Date == "")
                                {
                                    ChequeTx_Date = "NA";
                                }
                                txtVoucherTx_Narration.Text = "ChequeNo:" + ChequeTx_No + "  " + "ChequeDate:" + ChequeTx_Date + "  " + "ChequeAmount:" + ChequeTx_Amount;
                                break;
                            }

                        }
                        else
                        {
                            txtVoucherTx_Narration.Text = "";
                        }
                    }
                }


            }

            else
            {
                txtVoucherTx_Narration.Text = "";
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //View Voucher
    protected void ViewVoucher()
    {
        try
        {
            lblVoucherTx_No.Visible = false;
            txtVoucherTx_No.Visible = false;
            lblVoucherNo.Visible = true;
            btnAccept.Visible = false;
            btnClear.Visible = false;
            ddlcreditdebit.Visible = false;
            txtLedgerTx_Amount.Visible = false;
            divparticular.Visible = false;
            txtVoucherTx_Narration.Attributes.Add("readonly", "readonly");
            txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
            txtVoucherTx_No.Attributes.Add("readonly", "readonly");
            lbkbtnAddLedger.Visible = false;
            GridViewLedgerDetail.Columns[7].Visible = true;
            GridViewLedgerDetail.Columns[8].Visible = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //change voucher series according to FY
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        string ValidStatus = ValidDate();
        if (ValidStatus == "No")
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            FillVoucherDate();
        }
        else
        {
            FillVoucherNo();
        }
    }

    //GetPreviousVoucherNo
    protected void GetPreviousVoucherNo()
    {
        try
        {
            //lblMsg.Text = "";
            lblPreviousVoucherNo.Text = "";
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
            string VoucherTx_Type = "CreditNote Voucher";
            DataSet ds = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Type" },
                new string[] { "39", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Type }, "dataset");
            //ds = objdb.ByProcedure("", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblPreviousVoucherNo.Text = "(Previous VoucherNo :" + " " + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + ")";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
	protected string ValidDate()
    {
        string validDays = "No";
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

            DataSet dsValidDate = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "Office_ID", "VoucherDate", "FinancialYear", "VoucherTx_Type" }, new string[] { "3", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, "CreditNote Voucher" }, "dataset");
            if (dsValidDate.Tables.Count != 0 && dsValidDate.Tables[0].Rows.Count != 0)
            {
                validDays = dsValidDate.Tables[0].Rows[0]["ValidStatus"].ToString();
            }
        }
        return validDays;
    }
}