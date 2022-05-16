using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_VoucherPurchaseService : System.Web.UI.Page
{
    DataSet ds;
    //static DataSet DS_GridViewParticulars = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["RowNo"] = "0";
                    ViewState["CGST"] = "1";
                    ViewState["SGST"] = "2";
                    ViewState["RoundOff"] = "3";
                    ViewState["IGST"] = "737";
                    // ViewState["IGST"] = "35";
                    ViewState["VoucherTx_ID"] = "0";
                    CreateCostCentreTable();
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    //itempanel.Visible = false;
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    //txtVoucherTx_Date.Enabled = false;
                    ViewState["CostCentre"] = "No";
                    FillCategory(); // for cost center
                    FillDropDown();
                    FillPartyLedger();
                    FillState();
                    AddItem("NA");
                    CreateCrCostCentreTable();
                    ViewState["HSN_CGST"] = "0";
                    ViewState["HSN_SGST"] = "0";
                    ViewState["HSN_Code"] = "";
                    ViewState["HSN_IntegratedTax"] = "";
                    ViewState["Ledger_ID"] = "0";
                    ViewState["Typeofsupply"] = "";
                    ViewState["Type"] = "NA";
                    //txtVoucherTx_No.Attributes.Add("readonly", "readonly");
                    GridViewRef.DataSource = new string[] { };
                    GridViewRef.DataBind();

                    ds = null;
                    ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                        FillVoucherNo();
                    }

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
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //Fill PatryLedger DropDown
    protected void FillPartyLedger()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID" },
                new string[] { "44", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.DataSource = ds;
                ddlPartyName.DataTextField = "Ledger_Name";
                ddlPartyName.DataValueField = "Ledger_ID";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, "Select");


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

    //Fill Ledger DropDown in AMOUNT Section
    protected void FillDropDown()
    {
        try
        {

            //ds = objdb.ByProcedure("SpFinLedgerMaster",
            //      new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
            //      new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");

            ds = objdb.ByProcedure("SpFinLedgerMaster",
               new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
               new string[] { "66", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {

                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataSource = ds.Tables[1];
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));


                }

            }
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID", "Emp_ID" }, new string[] { "7", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                FillVoucherNo();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkPreviousVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
           
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Type" }, new string[] { "33", ViewState["Office_ID"].ToString(), "GSTService Purchase" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string VoucherID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                ViewState["VoucherTx_ID"] = VoucherID.ToString();
                FillPreviousDetail();
                ViewState["VoucherTx_ID"] = "0";
            }
            GetPreviousVoucherNo();
            lnkPreviousVoucher.Visible = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Fill Previous Voucher Detail
    protected void FillPreviousDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "16", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    //var rx = new System.Text.RegularExpressions.Regex("VR");
                    //string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //var array = rx.Split(str);
                    //lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //txtVoucherTx_No.Text = array[1];
                    //lblVoucherTx_No.Text = array[0] + "VR";
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    txtInvoice.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    ViewState["Amount"] = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    if (ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString() != "")
                    {
                        txtSupplierInvoiceDate.Text = ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString();
                    }

                }
                
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlPartyName.ClearSelection();
                        ddlPartyName.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                        ViewState["Ledger_ID"] = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        ddlPartyName.Visible = true;
                        txtPartyName.Visible = false;
                        FillCurrentBalance();

                    }
                    else
                    {
                        ddlPartyName.Visible = false;
                        txtPartyName.Visible = true;
                        txtPartyName.Text = ds.Tables[2].Rows[0]["Ledger_Name"].ToString();
                        ViewState["Ledger_ID"] = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        ViewState["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                        }
                    }


                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    dt_GridViewLedger = ds.Tables[3];
                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();
                }
                if (ds.Tables[7].Rows.Count > 0)
                {

                    txtSuplierName.Text = ds.Tables[7].Rows[0]["SupplierName"].ToString();
                    txtsupplieraddress.Text = ds.Tables[7].Rows[0]["SupplierAddress"].ToString();
                    ddlState.ClearSelection();
                    ddlState.Items.FindByValue(ds.Tables[7].Rows[0]["State_ID"].ToString()).Selected = true;
                    txtCity.Text = ds.Tables[7].Rows[0]["City"].ToString();
                    ddlRegistrationType.ClearSelection();
                    ddlRegistrationType.Items.FindByText(ds.Tables[7].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                    txtGSTNo.Text = ds.Tables[7].Rows[0]["GST_No"].ToString();
                }

                btnAccept.Text = "Accept";
                btnAccept.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Ledger Current Balance
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCurrentBalance();
            txtSuplierName.Text = ddlPartyName.SelectedItem.Text;
            DataSet ds1 = objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "Ledger_ID" }, new string[] { "3", ddlPartyName.SelectedValue.ToString() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                txtsupplieraddress.Text = ds1.Tables[0].Rows[0]["Mailing_Address"].ToString();
                ddlState.ClearSelection();
                ddlState.Items.FindByValue(ds1.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                txtCity.Text = ds1.Tables[0].Rows[0]["City"].ToString();
                ddlRegistrationType.ClearSelection();
                ddlRegistrationType.Items.FindByText(ds1.Tables[0].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                txtGSTNo.Text = ds1.Tables[0].Rows[0]["GST_No"].ToString();

            }
            GridViewBillByBillDetail.DataSource = new string[] { };
            GridViewBillByBillDetail.DataBind();
            GridViewChequeDetail.DataSource = new string[] { };
            GridViewChequeDetail.DataBind();
            GridCostCentreViewDetail.DataSource = new string[] { };
            GridCostCentreViewDetail.DataBind();
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", ddlPartyName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                {
                    BillByBillDetail.Visible = true;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = true;
                    pnlCostCentre.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = true;
                }
                else
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = false;
                }

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCurrentBalance()
    {
        try
        {
            lblMsg.Text = "";
            if (ddlPartyName.SelectedIndex > 0)
            {

                DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlPartyName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }
            else
            {

                txtCurrentBalance.Text = "";
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill State Dropdown in Supplier Detail
    protected void FillState()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminState",
                          new string[] { "flag" },
                          new string[] { "6" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlState.DataTextField = "State_Name";
                ddlState.DataValueField = "State_ID";
                ddlState.DataSource = ds;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select", "0"));
            }
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
            //string VoucherTx_Names_ForSno = "'Payment,Journal,Contra'";
            //string VoucherTx_Names_ForSno = "'Receipt'";
            //string VoucherTx_Names_ForSno = "Purchase Voucher";//last
            string VoucherTx_Names_ForSno = "GSTService Purchase,Contra,Payment";
            //string VoucherTx_Names_ForSno = "Sales Voucher";

            DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                new string[] { "41", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");
            /****Auto Series Office Wise**********/
            int VoucherTx_SNo = 0;
            ViewState["VoucherTx_SNo"] = 0;
            if (ViewState["Office_ID"].ToString() != "1")
            {
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

                }
                VoucherTx_SNo++;
                ViewState["VoucherTx_SNo"] = VoucherTx_SNo;

            }
            /*************************************/

            string Office_Code = "";
            if (ds1.Tables[1].Rows.Count != 0)
            {
                Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
            }

            // if (ds1.Tables[0].Rows.Count != 0)
            // {
            //VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

            //}
            //ViewState["PreVoucherNo"] = Office_Code + FinancialYear.ToString().Substring(2) + "PV" + VoucherTx_SNo.ToString();
            //VoucherTx_SNo++;
            ViewState["VoucherTx_SNo"] = VoucherTx_SNo;
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "VR";
            // txtVoucherTx_No.Text = VoucherTx_SNo.ToString();

            /****Auto Series Office Wise**********/
            if (ViewState["Office_ID"].ToString() != "1")
            {
                txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
                txtVoucherTx_No.Enabled = false;
            }
            /*************************************/

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void AddItem(string ID)
    {
        try
        {

            if (ID != "0")
            {

                string status = "0";
                string HSN_Code = null;
                decimal CGST_Per = 0;
                decimal SGST_Per = 0;
                decimal IGST_Per = 0;
                decimal CGSTAmt = 0;
                decimal SGSTAmt = 0;
                decimal IGSTAmt = 0;
                DataTable dt_GridViewLedger = new DataTable();
                dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Isreversechargeapplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IsIneligibleforinputcredit", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Taxbility", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CostCenter", typeof(string)));

                int gridRows = GridViewLedger.Rows.Count;
                if (gridRows > 3)
                {
                    for (int rowIndex = 3; rowIndex < gridRows; rowIndex++)
                    {
                        if (rowIndex > 3)
                        {
                            status = "1";
                        }
                        Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                        Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                        Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                        Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                        Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                        Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                        Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                        Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                        Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                        Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                        TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                        Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                        Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                        Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                        dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text, lblCostCenter.Text);
                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblCGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "SGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblSGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "IGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblIGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                        }
                    }
                    dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");
                    dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");
                    dt_GridViewLedger.Rows.Add(ViewState["IGST"].ToString(), "IGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");

                }
                else
                {
                    dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");
                    dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");
                    dt_GridViewLedger.Rows.Add(ViewState["IGST"].ToString(), "IGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");

                    dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "", "No");
                }

                GridViewLedger.DataSource = dt_GridViewLedger;
                GridViewLedger.DataBind();
                ViewState["LedgerAmount"] = dt_GridViewLedger;

                ViewState["RowNo"] = "0";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                // GridViewLedger
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Add Ledger/Amount Detail Event & Function
    protected void btnAddLedgerAmt_Click(object sender, EventArgs e)
    {
        try
        {

            string msg = "";
            int LedgerStatus = 0;
            lblMsg.Text = "";
            if (ddlLedger.SelectedIndex > 0 && txtLedgerAmt.Text != "")
            {
                //int rowIndex = 0;
                //int gridRows = GridViewLedger.Rows.Count;
                //if (gridRows > 0)
                //{
                //    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                //    {
                //        Label lblLedgerID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                //        if (ddlLedger.SelectedIndex > 0)
                //        {
                //            if (lblLedgerID.Text == ddlLedger.SelectedValue.ToString())
                //            {
                //                LedgerStatus = 1;
                //            }
                //            else
                //            {


                //            }
                //        }

                //    }
                //}
                if (LedgerStatus == 0)
                {
                 // comment by pawan 22-04-2022
                   
                    //FillLedgerAmount("0");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                    //ViewState["GrandTotal"] = lblGrandTotal.Text;
                    ////btnAcceptEnable();
                    //ddlLedger.ClearSelection();
                    //txtLedgerAmt.Text = "";

                    // comment by pawan 22-04-2022

                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", ddlLedger.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");
                    {
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                                {
                                    
                                    ViewState["CostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();
                                    lblCostCentreModal.Text = "";
                                    ddlCategory.ClearSelection();
                                    ddlSubCategory.Items.Clear();
                                    ViewState["Amount"] = Math.Abs(Convert.ToDecimal(txtLedgerAmt.Text)).ToString();
                                    txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                                }
                                else
                                {
                                    ViewState["CostCentre"] = "No";
                                    FillLedgerAmount("0");
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                                    ViewState["GrandTotal"] = lblGrandTotal.Text;
                                    //btnAcceptEnable();
                                    ddlLedger.ClearSelection();
                                    txtLedgerAmt.Text = "";
                                }
                            }
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
                }
                //FillLedgerAmount("0");
                //ddlLedger.ClearSelection();
                //txtLedgerAmt.Text = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedgerAmount(string ID)
    {

        //string Type = "NA";
        try
        {

            int gridRows = GridViewLedger.Rows.Count;
            if (ID == "0")
            {
                string status = "0";
                DataTable dt_GridViewLedger = new DataTable();
                dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Isreversechargeapplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IsIneligibleforinputcredit", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Taxbility", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CostCenter", typeof(string)));
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
                ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", ddlLedger.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                    {
                        Isreversechargeapplicable = ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString();
                        if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                        {
                            GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                        }
                        Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                        IsIneligibleforinputcredit = ds.Tables[0].Rows[0]["IsIneligibleforinputcredit"].ToString();
                        if (ddlState.SelectedIndex == 0)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select State First');", true);
                        }
                        else
                        {
                            CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                            if (int.Parse(ddlState.SelectedValue.ToString()) != 12)
                            {
                                CGST = 0;
                                SGST = 0;
                            }
                            else
                            {
                                IGST = 0;
                            }
                            decimal Amount = decimal.Parse(txtLedgerAmt.Text);
                            CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                            SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                            IGSTAmt = Math.Round((Amount * IGST) / 100, 2);

                            //if (ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString() == "Yes")
                            //{
                            //    CGST = 0;
                            //    SGST = 0;
                            //    IGST = 0;
                            //}
                            //else
                            //{
                            //    CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            //    SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            //    IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                            //    if (int.Parse(ddlState.SelectedValue.ToString()) != 12)
                            //    {
                            //        CGST = 0;
                            //        SGST = 0;
                            //    }
                            //    else
                            //    {
                            //        IGST = 0;
                            //    }
                            //}


                            HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                            dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", Isreversechargeapplicable.ToString(), GSTApplicable, IsIneligibleforinputcredit, Taxbility, ViewState["CostCentre"].ToString());

                            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                            {

                                Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                                Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                                Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                                Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                                Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                                Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                                Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                                Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                                Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                                TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                                Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                                Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                                Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                                Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                                Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                                if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "737")
                                {
                                    status = "0";
                                }
                                else
                                {
                                    status = "1";
                                }
                                dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text, lblCostCenter.Text);
                            }

                            foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                            {
                                if (dr["LedgerName"].ToString() == "CGST") // if id==2
                                {
                                    if (Isreversechargeapplicable == "Yes")
                                    {

                                    }
                                    else
                                    {
                                        dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + CGSTAmt;
                                    }
                                    //change the name
                                    //break; break or not depending on you
                                }
                                if (dr["LedgerName"].ToString() == "SGST") // if id==2
                                {
                                    if (Isreversechargeapplicable == "Yes")
                                    {

                                    }
                                    else
                                    {
                                        dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + SGSTAmt; //change the name
                                        //break; break or not depending on you
                                    }

                                }
                                if (dr["LedgerName"].ToString() == "IGST") // if id==2
                                {
                                    if (Isreversechargeapplicable == "Yes")
                                    {

                                    }
                                    else
                                    {
                                        dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + IGSTAmt; //change the name
                                        //break; break or not depending on you
                                    }

                                }
                            }
                            ViewState["LedgerAmount"] = dt_GridViewLedger;
                            GridViewLedger.DataSource = dt_GridViewLedger;
                            GridViewLedger.DataBind();
                        }

                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", Isreversechargeapplicable.ToString(), GSTApplicable, IsIneligibleforinputcredit, Taxbility, ViewState["CostCentre"].ToString());
                        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                        {

                            Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                            Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                            Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                            Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                            Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                            Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                            Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                            Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                            Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                            TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                            Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                            Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                            Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                            Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                            Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "737")
                            {
                                status = "0";
                            }
                            else
                            {
                                status = "1";
                            }
                            dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text, lblCostCenter.Text);
                        }
                        ViewState["LedgerAmount"] = dt_GridViewLedger;
                        GridViewLedger.DataSource = dt_GridViewLedger;
                        GridViewLedger.DataBind();
                    }


                }


            }
            else
            {
                DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                {

                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                    Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                    Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                    Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                    Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                    Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                    Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                    Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                    Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                    Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                    Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    if (int.Parse(lblID.Text) == int.Parse(ID))
                    {
                        for (int i = dt_GridViewLedger.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dt_GridViewLedger.Rows[i];
                            if (dr["LedgerID"].ToString() == ID)
                            {
                                dr.Delete();
                            }

                        }
                        dt_GridViewLedger.AcceptChanges();
                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST") // if id==2
                            {
                                if (lblrcm.Text == "Yes")
                                {

                                }
                                else
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblCGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }

                            }
                            if (dr["LedgerName"].ToString() == "SGST") // if id==2
                            {
                                if (lblrcm.Text == "Yes")
                                {

                                }
                                else
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblSGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }

                            }
                            if (dr["LedgerName"].ToString() == "IGST") // if id==2
                            {
                                if (lblrcm.Text == "Yes")
                                {

                                }
                                else
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblIGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }

                            }
                        }

                        for (int i = dt_CostCentreTable.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dt_CostCentreTable.Rows[i];
                            if (dr["Ledger_ID"].ToString() == ID)
                            {
                                dr.Delete();
                            }

                        }
                        dt_CostCentreTable.AcceptChanges();
                        ViewState["CostCentreTable"] = dt_CostCentreTable;
                    }



                }
                GridViewLedger.DataSource = dt_GridViewLedger;
                GridViewLedger.DataBind();

            }





        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewLedger_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewLedger.DataKeys[e.RowIndex].Value.ToString();
            FillLedgerAmount(ID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewRecord")
            {
                string LedgerID = e.CommandArgument.ToString();
                DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                DataView dv = new DataView();

                dv = dt_CostCentreTable.DefaultView;
                dv.RowFilter = "Ledger_ID = '" + LedgerID + "'";
                DataTable dt = dv.ToTable();
                gvCostCentreDetail.DataSource = dt;
                gvCostCentreDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCrCostCentreModal();", true);



            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    //Add BillByBillDetail Event & Function
    protected void CreateBillByBillTable()
    {
        DataTable dt_BillByBillTable = new DataTable();
        dt_BillByBillTable.Columns.Add(new DataColumn("RID", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTxType", typeof(string)));

        ViewState["BillByBillTable"] = dt_BillByBillTable;

        //GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        //GridViewBillByBillDetail.DataBind();
    }
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBillByBillData();
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
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
                string LedgerID = ddlPartyName.SelectedValue.ToString();
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
                    ddlBillByBillTx_Ref.Items.Clear();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }
            }
            else
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                ddlBillByBillTx_Ref.Items.Clear();
                string LedgerID = ddlPartyName.SelectedValue.ToString();
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
                    ddlBillByBillTx_Ref.Items.Clear();
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
        string msg = "";
        if (txtBillByBillTx_Ref.Text == "")
        {
            msg += "Enter Name.\n";
        }
        if (txtBillByBillTx_Amount.Text == "")
        {
            msg += "Enter Amount.\n";
        }
        if (ddlBillByBillTx_crdr.SelectedIndex == 0)
        {
            msg += "Select Cr/Dr.\n";
        }
        if (msg == "")
        {
            ViewState["Amount"] = Math.Abs(Convert.ToDecimal(lblGrandTotal.Text));
            FillRefAmount("0");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }


    }
    protected void FillRefAmount(string ID)
    {
        try
        {
            DataTable dt_BillByBillTable = new DataTable();
            dt_BillByBillTable.Columns.Add(new DataColumn("RID", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTxType", typeof(string)));
            string Type = "";

            if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
            {
                Type = "Cr";
            }
            else
            {
                Type = "Dr";
            }
            int gridRows = GridViewRef.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblID = (Label)GridViewRef.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblTypeOfRef = (Label)GridViewRef.Rows[rowIndex].Cells[0].FindControl("lblTypeOfRef");
                Label lblRefNo = (Label)GridViewRef.Rows[rowIndex].Cells[1].FindControl("lblRefNo");
                Label lblAmount = (Label)GridViewRef.Rows[rowIndex].Cells[2].FindControl("lblAmount");
                Label lblType = (Label)GridViewRef.Rows[rowIndex].Cells[2].FindControl("lblType");
                if (lblID.Text != ID)
                {
                    dt_BillByBillTable.Rows.Add(lblID.Text, lblTypeOfRef.Text, lblRefNo.Text, lblAmount.Text, lblType.Text);
                    if (lblType.Text == "Cr")
                    {
                        if (lblGrandTotal.Text.Contains("-"))
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }
                        else
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }

                    }
                    else
                    {
                        if (lblGrandTotal.Text.Contains("-"))
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }
                        else
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }

                    }
                }

            }
            if (ID == "0")
            {
                int RefType = ddlRefType.SelectedIndex;
                string RefNo = "";
                if (RefType == 0 || RefType == 2)
                {
                    RefNo = txtBillByBillTx_Ref.Text;
                }
                else if (RefType == 3)
                {
                    RefNo = "OnAccount";
                }
                else
                {
                    RefNo = ddlBillByBillTx_Ref.SelectedValue;
                }
                dt_BillByBillTable.Rows.Add((gridRows + 1).ToString(), ddlRefType.SelectedItem.Text, RefNo, txtBillByBillTx_Amount.Text, Type);
                if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
                {
                    if (lblGrandTotal.Text.Contains("-"))
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }
                    else
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }

                }
                else
                {

                    if (lblGrandTotal.Text.Contains("-"))
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }
                    else
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }


                }
            }


            DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];
            //foreach (DataRow rows in dt_BillByBillData.Rows)
            //{
            //    if (rows["BillByBillTx_Ref"].ToString().Equals(ddlBillByBillTx_Ref.SelectedValue))
            //    {
            //        dt_BillByBillData.Rows.Remove(rows);
            //        dt_BillByBillData.AcceptChanges();
            //        break;
            //    }
            //}
            ViewState["dt_BillByBillData"] = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
            ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
            ddlBillByBillTx_Ref.DataBind();
            ddlBillByBillTx_Ref.Items.Insert(0, "Select");




            GridViewRef.DataSource = dt_BillByBillTable;
            GridViewRef.DataBind();

            //decimal RefTotal = 0;
            //RefTotal = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtBillByBillTx_Ref.Text = "";
            ddlRefType.ClearSelection();
            ViewState["BillByBillTable"] = dt_BillByBillTable;
            if (decimal.Parse(ViewState["Amount"].ToString()) == 0)
            {
                Save("BillByBill");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
            }
            //ViewState["BillByBillTable"] = dt_BillByBillTable;



            txtBillByBillTx_Ref.Visible = true;
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            //BindBillByBillData();
            ddlRefType.ClearSelection();
            ddlBillByBillTx_crdr.SelectedValue = "Cr";
            txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
            txtBillByBillTx_Ref.Enabled = true;
            ddlBillByBillTx_Ref.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //AddChequeDetail Event & Function
    protected void CreatTableFinChequeTx()
    {

        DataTable dt_FinChequeTx = new DataTable();
        dt_FinChequeTx.Columns.Add(new DataColumn("RID", typeof(string)));
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
            ViewState["Amount"] = lblGrandTotal.Text;
            FillChequeAmount("0");
        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected void FillChequeAmount(string ID)
    {
        try
        {
            DataTable dt_FinChequeTx = new DataTable();
            dt_FinChequeTx.Columns.Add(new DataColumn("RID", typeof(string)));
            dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
            dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
            dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));


            int gridRows = GVFinChequeTx.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label RID = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label ChequeTx_No = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblChequeTx_No");
                Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblChequeTx_Date");
                Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblChequeTx_Amount");

                if (RID.Text != ID)
                {
                    dt_FinChequeTx.Rows.Add(RID.Text, ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text);

                }

            }
            if (ID == "0")
            {

                dt_FinChequeTx.Rows.Add((gridRows + 1).ToString(), txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text);
            }

            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();
            ViewState["FinChequeTx"] = dt_FinChequeTx;

            decimal RefTotal = 0;
            RefTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));
            ViewState["Amount"] = decimal.Parse(ViewState["Amount"].ToString()) - RefTotal;
            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtChequeTx_No.Text = "";
            txtChequeTx_Amount.Text = "";
            txtChequeTx_Date.Text = "";
            if (decimal.Parse(ViewState["Amount"].ToString()) == 0)
            {
                Save("Cheque");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                txtChequeTx_No.Text = "";
                txtChequeTx_Amount.Text = ViewState["Amount"].ToString();
                txtChequeTx_Date.Text = "";
            }
            //ViewState["BillByBillTable"] = dt_BillByBillTable;




        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Save Data
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CostCentre"] = "No";
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            txtBillByBillTx_Ref.Visible = true;
            ddlBillByBillTx_Ref.Visible = false;
            txtBillByBillTx_Ref.Enabled = true;
            lnkView.Visible = false;
            string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            lblMsg.Text = "";
            string msg = "";
            string msg1 = "";
            string GSTApplicable = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtSupplierInvoiceDate.Text == "")
            {
                msg += "Enter Supplier's Invoice Date. \\n";
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
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party A/c Name. \\n";
            }

            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration. \\n";
            }
            if (ddlRegistrationType.SelectedIndex > 0)
            {
                if (ddlRegistrationType.SelectedItem.Text == "Composition" || ddlRegistrationType.SelectedItem.Text == "Regular")
                {
                    if (txtGSTNo.Text.Trim() == "")
                    {
                        msg += "Enter GST No";
                        gstvisible.Visible = true;
                    }

                }
                else
                {
                    gstvisible.Visible = false;
                }
            }
            /*if(GridViewLedger.Rows.Count < 4)
            {
                msg += "Enter Ledger Detail. \\n";
            }
            if (GridViewLedger.Rows.Count >3)
            {
                foreach (GridViewRow rows in GridViewLedger.Rows)
                {
                    Label lblID = (Label)rows.FindControl("lblID");
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID" }, new string[] { "49", lblID.Text }, "dataset");
                    {
                        if(ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                        {
                            GSTApplicable = "Yes";
                            break;
                        }
                    }
                }
                if(GSTApplicable == "Yes")
                {

                }
                else
                {
                    msg += "Select One Service Ledger. \\n";
                }

            }*/
            if (msg.Trim() == "")
            {
                string LedgerId = ddlPartyName.SelectedValue.ToString();
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
                    ViewState["Amount"] = Math.Abs(Convert.ToDecimal(lblGrandTotal.Text));
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                        {
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Dr";
                            }
                            else
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Cr";
                            }
                            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                            txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();

                            ddlRefType.SelectedValue = "2";
                            //    txtRefAmount.Text = lblGrandTotal.Text;
                            // CreateBillByBillTable();
                            GridViewRef.DataSource = new string[] { };
                            GridViewRef.DataBind();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);


                        }
                        else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                        {
                            btnAddCheque.Enabled = true;
                            //btnAddChequeDetail.Enabled = false;
                            CreatTableFinChequeTx();
                            txtChequeTx_Amount.Text = ViewState["Amount"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                        }
                        else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                        {
                            ViewState["CrCostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();

                            CreateCrCostCentreTable();
                            lblCostCentreModal.Text = "";
                            ddlCategory.ClearSelection();
                            ddlSubCategory.Items.Clear();
                            ViewState["Amount"] = lblGrandTotal.Text;
                            txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                        }
                        else
                        {
                            Save("None");
                        }

                    }



                }
                else if (btnAccept.Text == "Update")
                {
                    if (ViewState["Amount"].ToString() == (Math.Abs(Convert.ToDecimal(lblGrandTotal.Text))).ToString() && ViewState["Ledger_ID"].ToString() == ddlPartyName.SelectedValue.ToString())
                    {
                        txtBillByBillTx_Amount.Text = "0.00";
                        btnAddBillByBill.Enabled = false;
                    }
                    else
                    {
                        ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                            {
                                if (lblGrandTotal.Text.Contains("-"))
                                {
                                    ddlBillByBillTx_crdr.SelectedValue = "Dr";
                                }
                                else
                                {
                                    ddlBillByBillTx_crdr.SelectedValue = "Cr";
                                }
                                GridViewRef.DataSource = new string[] { };
                                GridViewRef.DataBind();
                                //decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblGrandTotal.Text));
                                txtBillByBillTx_Amount.Text = lblGrandTotal.Text;
                                btnAddBillByBill.Enabled = true;
                                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;

                                //    txtRefAmount.Text = lblGrandTotal.Text;
                                // CreateBillByBillTable();

                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);


                            }
                            else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                            {
                                btnAddCheque.Enabled = true;
                                //btnAddChequeDetail.Enabled = false;
                                CreatTableFinChequeTx();
                                GVFinChequeTx.DataSource = new string[] { };
                                GVFinChequeTx.DataBind();
                                txtChequeTx_Amount.Text = lblGrandTotal.Text;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                            }
                            else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                            {
                                ViewState["CrCostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();

                                CreateCrCostCentreTable();
                                lblCostCentreModal.Text = "";
                                ddlCategory.ClearSelection();
                                ddlSubCategory.Items.Clear();
                                ViewState["Amount"] = lblGrandTotal.Text;
                                txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                            }
                            else
                            {
                                Save("None");
                            }



                        }

                    }
                }
                else
                {
                    /****Auto Series Office Wise**********/
                    GetPreviousVoucherNo();
                    FillVoucherNo();

                    ClientScriptManager CSM = Page.ClientScript;
                    string strScript = "<script>";
                    strScript += "alert('Voucher No [  " + VoucherTx_No + "  ]  already exist.  Please re-check Voucher No. and then submit.');";
                    strScript += "CalculateGrandTotal();";

                    strScript += "</script>";
                    /************************************/
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No is already exist.');", true);
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
    protected void Save(string Type)
    {
        try
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


            string VoucherTx_Type = "GSTService Purchase";
            string VoucherTx_Name = "Purchase Voucher";
            string VoucherTx_Ref = txtInvoice.Text;
            string VoucherTx_Amount = lblGrandTotal.Text;
            string ItemTx_IsActive = "1";
            string LedgerTx_IsActive = "1";
            string VoucherTx_IsActive = "1";
            string Supplier_IsActive = "1";

            if (btnAccept.Text == "Accept")
            {
                ItemTx_IsActive = "0";
                LedgerTx_IsActive = "0";
                VoucherTx_IsActive = "0";
                Supplier_IsActive = "0";

                /****Auto Series Office Wise**********/
                DataSet ds2;
                if (ViewState["Office_ID"].ToString() != "1")
                {
                    ds2 = objdb.ByProcedure("SpFinVoucherTx",
            new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SupplierinvoiceDate","GSTVoucher","VoucherTx_SNo"},
            new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),Convert.ToDateTime(txtSupplierInvoiceDate.Text, cult).ToString("yyyy/MM/dd"),"Yes",ViewState["VoucherTx_SNo"].ToString()}, "dataset");

                }
                else
                {
                    ds2 = objdb.ByProcedure("SpFinVoucherTx",
       new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SupplierinvoiceDate","GSTVoucher" },
       new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),Convert.ToDateTime(txtSupplierInvoiceDate.Text, cult).ToString("yyyy/MM/dd"),"Yes"}, "dataset");
                }
                string VoucherTx_ID = "0";
                /*************************/
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                    objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "SupplierName", "SupplierAddress", "State_ID", "City", "RegistrationTypes", "GST_No", "Supplier_IsActive", "UpdatedBy" }, new string[] { "0", VoucherTx_ID, txtSuplierName.Text, txtsupplieraddress.Text, ddlState.SelectedValue, txtCity.Text, ddlRegistrationType.SelectedItem.Text, txtGSTNo.Text, Supplier_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                    int gridLedgerRows = GridViewLedger.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
                    {
                        string LedgerTx_Amount = "0";
                        Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                        Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                        Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                        Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                        Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                        Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                        Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                        Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                        Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                        TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                        Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                        Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                        Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                        if (txtAmount.Text.Contains("-"))
                        {
                            LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                        }
                        else
                        {
                            LedgerTx_Amount = "-" + txtAmount.Text;
                        }

                        // string LedgerTx_Amount = "-" + txtAmount.Text;
                        if (lblCostCenter.Text == "Yes")
                        {
                            DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                            int Count = dt_CostCentreTable.Rows.Count;
                            for (int i = 0; i < Count; i++)
                            {
                                DataRow dr = dt_CostCentreTable.Rows[i];
                                if (dr["Ledger_ID"].ToString() == lblID.Text)
                                {
                                    string Category_ID = dr["Category_ID"].ToString();
                                    string CategoryName = dr["CategoryName"].ToString();
                                    string SubCategory_ID = dr["SubCategory_ID"].ToString();
                                    string SubCategoryName = dr["SubCategoryName"].ToString();
                                    string AmountShow = dr["AmountShow"].ToString();
                                    string Amount = dr["Amount"].ToString();
                                    //Amount = "-" + Amount;
                                    //AmountShow = "-" + AmountShow;
                                    if (txtAmount.Text.Contains("-"))
                                    {
                                        decimal dd = Convert.ToDecimal(Amount);
                                        decimal dd1 = Convert.ToDecimal(AmountShow);
                                        Amount = Convert.ToString(Math.Abs(dd));
                                        AmountShow = Convert.ToString(Math.Abs(dd1));
                                        
                                    }
                                    else
                                    {
                                        Amount = "-" + Amount;
                                        AmountShow = "-" + AmountShow;
                                    }
                                    objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                              new string[] { "1", VoucherTx_ID, lblID.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), Category_ID, SubCategory_ID, Amount, "GSTService Purchase", "0", ViewState["Emp_ID"].ToString() }, "dataset");
                                }
                            }

                            objdb.ByProcedure("SpFinLedgerTx",
                                  new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType","HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                                  new string[] { "0", lblID.Text,"Sub Ledger",VoucherTx_ID,VoucherTx_Type,LedgerTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","CostCentre",lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text}, "dataset");
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                        }
                        else
                        {
                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "IsIneligibleforinputcredit", "Taxbility" },
                            new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text }, "dataset");
                        }

                    }
                    //DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                    //for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                    //{
                    //    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                    //    {
                    //        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                    //        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                    //        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                    //        Amount = "-" + Amount;
                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                    //        new string[] { "0", ParticularID, "Item Ledger", VoucherTx_ID, Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                    //    }
                    //}
                    if (Type == "BillByBill")
                    {

                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","BillByBill"}, "dataset");


                        int gridBillbyBillRows = GridViewRef.Rows.Count;
                        for (int k = 0; k < gridBillbyBillRows; k++)
                        {


                            Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
                            Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
                            Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
                            Label BillType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
                            if (BillType.Text == "Dr")
                            {
                                BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
                            }

                            objdb.ByProcedure("SpFinBillByBillTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                                        new string[] { "3", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", (k + 1).ToString() }, "dataset");

                        }
                    }
                    else if (Type == "Cheque")
                    {

                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","Cheque"}, "dataset");


                        int gridChequeRows = GVFinChequeTx.Rows.Count;
                        for (int k = 0; k < gridChequeRows; k++)
                        {


                            Label RID = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblID");
                            Label ChequeTx_No = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_No");
                            Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Date");
                            Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Amount");

                            if (ChequeTx_No.Text == "")
                            {
                                ChequeTx_No.Text = null;
                            }
                            else
                            {

                            }

                            if (ChequeTx_Date.Text == "")
                            {
                                ChequeTx_Date.Text = null;
                            }
                            else
                            {
                                ChequeTx_Date.Text = Convert.ToDateTime(ChequeTx_Date.Text, cult).ToString("yyyy/MM/dd");
                            }

                            //objdb.ByProcedure("SpFinChequeTx",
                            //                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                            //                    new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), "CreditSale Voucher", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");

                            objdb.ByProcedure("SpFinChequeTx",
                                                 new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                                 new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), "GSTService Purchase", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");

                        }

                    }
                    else if (Type == "CostCentre")
                    {
                        //  objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
                        //new string[] { "3", VoucherTx_ID }, "dataset");
                        foreach (GridViewRow row in GridCostCentreDetail.Rows)
                        {
                            Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                            Label lblCategoryName = (Label)row.FindControl("lblCategoryName");
                            Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
                            Label lblSubCategoryName = (Label)row.FindControl("lblSubCategoryName");
                            Label lblAmountShow = (Label)row.FindControl("lblAmountShow");
                            Label lblAmount = (Label)row.FindControl("lblAmount");

                            objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                          new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), lblCategory_ID.Text, lblSubCategory_ID.Text, lblAmount.Text, "GSTService Purchase", "0", ViewState["Emp_ID"].ToString() }, "dataset");
                        }
                       
                        objdb.ByProcedure("SpFinLedgerTx",
                              new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                              new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","CostCentre"}, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    }
                    else
                    {

                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
                    }
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");

                }

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                ClearData();
                if (rbtPrint.SelectedValue.ToString() == "Yes")
                {
                    rbtPrint.SelectedValue = "No";
                    string url = "VoucherContraInvoice.aspx?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID).ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.open('");
                    sb.Append(url);
                    sb.Append("');");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
                }
            }
            else if (btnAccept.Text == "Update")
            {
                string VoucherTx_ID = ViewState["VoucherTx_ID"].ToString();

                objdb.ByProcedure("SpFinVoucherTx",
        new string[] { "flag","VoucherTx_ID","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SupplierinvoiceDate" },
        new string[] { "7",ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),Convert.ToDateTime(txtSupplierInvoiceDate.Text, cult).ToString("yyyy/MM/dd")}, "dataset");
                objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "SupplierName", "SupplierAddress", "State_ID", "City", "RegistrationTypes", "GST_No", "Supplier_IsActive", "UpdatedBy" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString(), txtSuplierName.Text, txtsupplieraddress.Text, ddlState.SelectedValue, txtCity.Text, ddlRegistrationType.SelectedItem.Text, txtGSTNo.Text, Supplier_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "3", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                int gridLedgerRows = GridViewLedger.Rows.Count;
                for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
                {
                    string LedgerTx_Amount = "0";
                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                    Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                    Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                    Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                    Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                    Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                    Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                    Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                    Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                    if (txtAmount.Text.Contains("-"))
                    {
                        LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                    }
                    else
                    {
                        LedgerTx_Amount = "-" + txtAmount.Text;
                    }

                    // string LedgerTx_Amount = "-" + txtAmount.Text;
                    if (lblCostCenter.Text == "Yes")
                    {
                        DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                        int Count = dt_CostCentreTable.Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            DataRow dr = dt_CostCentreTable.Rows[i];
                            if (dr["Ledger_ID"].ToString() == lblID.Text)
                            {
                                string Category_ID = dr["Category_ID"].ToString();
                                string CategoryName = dr["CategoryName"].ToString();
                                string SubCategory_ID = dr["SubCategory_ID"].ToString();
                                string SubCategoryName = dr["SubCategoryName"].ToString();
                                string AmountShow = dr["AmountShow"].ToString();
                                string Amount = dr["Amount"].ToString();
                                //Amount = "-" + Amount;
                                //AmountShow = "-" + AmountShow;
                                if (txtAmount.Text.Contains("-"))
                                {
                                    decimal dd = Convert.ToDecimal(Amount);
                                    decimal dd1 = Convert.ToDecimal(AmountShow);
                                    Amount = Convert.ToString(Math.Abs(dd));
                                    AmountShow = Convert.ToString(Math.Abs(dd1));

                                }
                                else
                                {
                                    Amount = "-" + Amount;
                                    AmountShow = "-" + AmountShow;
                                }
                                objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                          new string[] { "1", VoucherTx_ID, lblID.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), Category_ID, SubCategory_ID, Amount, "GSTService Purchase", "0", ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                        }

                        objdb.ByProcedure("SpFinLedgerTx",
                              new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType","HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                              new string[] { "0", lblID.Text,"Sub Ledger",VoucherTx_ID,VoucherTx_Type,LedgerTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","CostCentre",lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text}, "dataset");
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    }
                    else
                    {
                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "IsIneligibleforinputcredit", "Taxbility" },
                        new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text }, "dataset");
                    }

                }

                //DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                //for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                //{
                //    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                //    {
                //        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                //        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                //        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                //        Amount = "-" + Amount;
                //        objdb.ByProcedure("SpFinLedgerTx",
                //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                //        new string[] { "0", ParticularID, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                //    }
                //}
                objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (Type == "BillByBill")
                {
                    DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
                    GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
                    GridViewBillByBillDetail.DataBind();
                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","BillByBill"}, "dataset");


                    int gridBillbyBillRows = GridViewRef.Rows.Count;
                    for (int k = 0; k < gridBillbyBillRows; k++)
                    {


                        Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
                        Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
                        Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
                        Label BillType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
                        if (BillType.Text == "Dr")
                        {
                            BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
                        }

                        objdb.ByProcedure("SpFinBillByBillTx",
                                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                                    new string[] { "3", ViewState["VoucherTx_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", (k + 1).ToString() }, "dataset");

                    }
                }
                else if (Type == "Cheque")
                {
                    DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];
                    GridViewChequeDetail.DataSource = dt_FinChequeTx;
                    GridViewChequeDetail.DataBind();
                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","Cheque"}, "dataset");


                    int gridChequeRows = GVFinChequeTx.Rows.Count;
                    for (int k = 0; k < gridChequeRows; k++)
                    {


                        Label RID = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblID");
                        Label ChequeTx_No = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_No");
                        Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Date");
                        Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Amount");

                        if (ChequeTx_No.Text == "")
                        {
                            ChequeTx_No.Text = null;
                        }
                        else
                        {

                        }

                        if (ChequeTx_Date.Text == "")
                        {
                            ChequeTx_Date.Text = null;
                        }
                        else
                        {
                            ChequeTx_Date.Text = Convert.ToDateTime(ChequeTx_Date.Text, cult).ToString("yyyy/MM/dd");
                        }

                        //objdb.ByProcedure("SpFinChequeTx",
                        //                     new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                        //                     new string[] { "1", ViewState["VoucherTx_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), "CreditSale Voucher", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                        objdb.ByProcedure("SpFinChequeTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                            new string[] { "1", ViewState["VoucherTx_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), "GSTService Purchase", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                    }

                }
                else if (Type == "CostCentre")
                {
                    //  objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
                    //new string[] { "3", VoucherTx_ID }, "dataset");
                    foreach (GridViewRow row in GridCostCentreDetail.Rows)
                    {
                        Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                        Label lblCategoryName = (Label)row.FindControl("lblCategoryName");
                        Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
                        Label lblSubCategoryName = (Label)row.FindControl("lblSubCategoryName");
                        Label lblAmountShow = (Label)row.FindControl("lblAmountShow");
                        Label lblAmount = (Label)row.FindControl("lblAmount");

                        objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                      new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), lblCategory_ID.Text, lblSubCategory_ID.Text, lblAmount.Text, "GSTService Purchase", "0", ViewState["Emp_ID"].ToString() }, "dataset");
                    }

                    objdb.ByProcedure("SpFinLedgerTx",
                          new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                          new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","CostCentre"}, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                }
                else
                {

                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
                }



                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                //ClearData();
                if (rbtPrint.SelectedValue.ToString() == "Yes")
                {
                    rbtPrint.SelectedValue = "No";
                    string url = "VoucherContraInvoice.aspx?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID).ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.open('");
                    sb.Append(url);
                    sb.Append("');");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
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
            txtVoucherTx_No.Text = "";
            txtInvoice.Text = "";
            ddlPartyName.ClearSelection();
            txtCurrentBalance.Text = "";

            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            //GridViewParticulars.DataSource = new string[] { };
            //GridViewParticulars.DataBind();
            GridViewRef.DataSource = new string[] { };
            GridViewRef.DataBind();
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            GridViewChequeDetail.DataSource = new string[] { };
            GridViewChequeDetail.DataBind();
            AddItem("NA");
            FillVoucherNo();
            GetPreviousVoucherNo();
            txtVoucherTx_Narration.Text = "";
            txtSuplierName.Text = "";
            txtsupplieraddress.Text = "";
            ddlState.ClearSelection();
            txtCity.Text = "";
            ddlRegistrationType.ClearSelection();
            txtGSTNo.Text = "";
            txtSupplierInvoiceDate.Text = "";
            CreateCostCentreTable();
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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "16", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //if (ds.Tables[0].Rows[0]["VoucherTx_No"].ToString().Contains("VR"))
                    //{
                    //    var rx = new System.Text.RegularExpressions.Regex("VR");
                    //    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    var array = rx.Split(str);
                    //    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    txtVoucherTx_No.Text = array[1];
                    //    lblVoucherTx_No.Text = array[0] + "VR";
                    //}
                    //else
                    //{
                    //    var rx = new System.Text.RegularExpressions.Regex("PV");
                    //    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    var array = rx.Split(str);
                    //    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    txtVoucherTx_No.Text = array[1];
                    //    lblVoucherTx_No.Text = array[0] + "PV";
                    //}
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
                    txtVoucherTx_No.Text = Lstring;
                    lblVoucherTx_No.Text = Fstring;
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    txtInvoice.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    ViewState["Amount"] = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    if (ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString() != "")
                    {
                        txtSupplierInvoiceDate.Text = ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString();
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlPartyName.ClearSelection();
                        ddlPartyName.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                        ddlPartyName.Visible = true;
                        txtPartyName.Visible = false;
                        FillCurrentBalance();
                    }
                    else
                    {
                        ddlPartyName.Visible = false;
                        txtPartyName.Visible = true;

                        txtPartyName.Text = ds.Tables[2].Rows[0]["Ledger_Name"].ToString();
                        string LedgerID = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        string Office_ID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", LedgerID.ToString(), Office_ID.ToString() }, "dataset");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                        }
                    }

                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    DataTable dt_GridViewLedger = new DataTable();
                    dt_GridViewLedger = ds.Tables[3];
                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();
                    ViewState["LedgerAmount"] = dt_GridViewLedger;
                 

                 

                }

                if (ds.Tables[5].Rows.Count > 0)
                {
                    BillByBillDetail.Visible = true;
                    pnlChequeDetail.Visible = false;
                    GridViewBillByBillDetail.DataSource = ds.Tables[5];
                    GridViewBillByBillDetail.DataBind();
                }
                if (ds.Tables[6].Rows.Count > 0)
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = true;
                    GridViewChequeDetail.DataSource = ds.Tables[6];
                    GridViewChequeDetail.DataBind();
                }
                if (ds.Tables[7].Rows.Count > 0)
                {

                    txtSuplierName.Text = ds.Tables[7].Rows[0]["SupplierName"].ToString();
                    txtsupplieraddress.Text = ds.Tables[7].Rows[0]["SupplierAddress"].ToString();
                    ddlState.ClearSelection();
                    ddlState.Items.FindByValue(ds.Tables[7].Rows[0]["State_ID"].ToString()).Selected = true;
                    txtCity.Text = ds.Tables[7].Rows[0]["City"].ToString();
                    ddlRegistrationType.ClearSelection();
                    ddlRegistrationType.Items.FindByText(ds.Tables[7].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                    txtGSTNo.Text = ds.Tables[7].Rows[0]["GST_No"].ToString();
                }
                btnAccept.Text = "Update";
                btnAccept.Enabled = true;
                //FillCurrentBalance();
            }

            ds = objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
              new string[] { "5", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCostCentreViewDetail.DataSource = ds;
                    GridCostCentreViewDetail.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                    dt_CostCentreTable = ds.Tables[1];
                    ViewState["CostCentreTable"] = dt_CostCentreTable;
                }
                BillByBillDetail.Visible = false;
                pnlChequeDetail.Visible = false;
                pnlCostCentre.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    //View VoucherDetail
    protected void ViewVoucher()
    {
        try
        {
            lblVoucherTx_No.Visible = false;
            txtVoucherTx_No.Visible = false;
            lblVoucherNo.Visible = true;
            btnAccept.Visible = false;
            btn_Clear.Visible = false;
            divamount.Visible = false;

            //panel1.Enabled = false;
            rbtPrint.Enabled = false;         
            ddlPartyName.Enabled = false;
            lbkbtnAddLedger.Visible = false;
            foreach (GridViewRow rows in GridViewLedger.Rows)
            {
                LinkButton delete = (LinkButton)rows.FindControl("Delete");
                delete.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

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

            FillDropDown();
            FillState();

            FillPartyLedger();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
		string ValidStatus = ValidDate();
        if (ValidStatus == "No")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
		else
		{
			
        if (ViewState["VoucherTx_ID"].ToString() == "0")
        {
            FillVoucherNo();
        }
        else
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
            //if (ViewState["FY"].ToString() == FinancialYear.ToString())
            //{
            //    FillVoucherNo();
            //}
            //else
            if (ViewState["FY"].ToString() != FinancialYear.ToString())
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Date selection should be according to Financial Year(" + ViewState["FY"].ToString() + ")');", true);
                txtVoucherTx_Date.Text = ViewState["VoucherTx_Date"].ToString();
            }
        }

		}
    }

    //Fill Previous VoucherNarration
    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                 new string[] { "14", "GSTService Purchase", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //GetPreviousVoucherNo
    protected void GetPreviousVoucherNo()
    {
        try
        {
            // lblMsg.Text = "";
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
            string VoucherTx_Type = "Payment,Contra,GSTService Purchase,Goods Transfer Inward,Goods Transfer Outward";
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

            DataSet dsValidDate = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "Office_ID", "VoucherDate", "FinancialYear", "VoucherTx_Type" }, new string[] { "3", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, "GSTService Purchase" }, "dataset");
            if (dsValidDate.Tables.Count != 0 && dsValidDate.Tables[0].Rows.Count != 0)
            {
                validDays = dsValidDate.Tables[0].Rows[0]["ValidStatus"].ToString();
            }
        }
        return validDays;
    }


    // code for cost center

    protected void FillCategory()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinCategoryMaster",
                 new string[] { "flag", "OfficeID" },
                 new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataSource = ds;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void CreateCostCentreTable()
    {
        //DataTable dt_CostCentreTable = new DataTable();
        //int CNO = 0;
        //int count = GridViewLedgerDetail.Rows.Count;
        //if (count > 0)
        //{
        //    foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
        //    {
        //        Label rowno = (Label)rows.FindControl("lblRowNumber");
        //        CNO = int.Parse(rowno.Text);
        //    }
        //    CNO = CNO + 1;
        //}
        //else
        //{
        //    CNO = CNO + 1;
        //}
        DataTable dt_CostCentreTable = new DataTable();
        DataColumn RowNo = dt_CostCentreTable.Columns.Add("RowNo", typeof(string));
        dt_CostCentreTable.Columns.Add(new DataColumn("Ledger_ID", typeof(decimal)));
        dt_CostCentreTable.Columns.Add(new DataColumn("Category_ID", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("CategoryName", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("SubCategory_ID", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("SubCategoryName", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("AmountShow", typeof(decimal)));
        dt_CostCentreTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["CostCentreTable"] = dt_CostCentreTable;

        GridCostCentreDetail.DataSource = dt_CostCentreTable;
        GridCostCentreDetail.DataBind();
    }
    protected void CreateCrCostCentreTable()
    {
        //DataTable dt_CostCentreTable = new DataTable();
        //int CNO = 0;
        //int count = GridViewLedgerDetail.Rows.Count;
        //if (count > 0)
        //{
        //    foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
        //    {
        //        Label rowno = (Label)rows.FindControl("lblRowNumber");
        //        CNO = int.Parse(rowno.Text);
        //    }
        //    CNO = CNO + 1;
        //}
        //else
        //{
        //    CNO = CNO + 1;
        //}
        DataTable dt_CrCostCentreTable = new DataTable();
        DataColumn RowNo = dt_CrCostCentreTable.Columns.Add("RowNo", typeof(string));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("Ledger_ID", typeof(decimal)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("Category_ID", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("CategoryName", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("SubCategory_ID", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("SubCategoryName", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("AmountShow", typeof(decimal)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;

        gvCostCentreDetail.DataSource = dt_CrCostCentreTable;
        gvCostCentreDetail.DataBind();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblCostCentreModal.Text = "";
            ddlSubCategory.Items.Clear();
            ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                 new string[] { "flag", "CategoryId" },
                 new string[] { "8", ddlCategory.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataSource = ds;
                ddlSubCategory.DataBind();
            }
            ddlSubCategory.Items.Insert(0, new ListItem("Select", "0"));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnCostCentreAdd_Click(object sender, EventArgs e)
    {
        string msg = "";
        lblCostCentreModal.Text = "";
        if (ddlCategory.SelectedIndex == 0)
        {
            msg += "Select Category \\n";
        }
        if (ddlSubCategory.SelectedIndex == 0)
        {
            msg += "Select Sub Category \\n";
        }
        if (txtCostCentreAmount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (msg == "")
        {
            if (ViewState["CostCentre"].ToString() == "Yes")
            {
                AddCostCentre();
            }
            else
            {
                DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                int status = 0;
                decimal CostCentreAmount = 0;
                foreach (GridViewRow row in GridCostCentreDetail.Rows)
                {
                    Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                    Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
                    if (lblCategory_ID.Text == ddlCategory.SelectedValue.ToString() && lblSubCategory_ID.Text == ddlSubCategory.SelectedValue.ToString())
                    {
                        status = 1;
                    }
                }
                if (status == 0)
                {
                    if (Convert.ToDecimal(ViewState["Amount"].ToString()) > Convert.ToDecimal(txtCostCentreAmount.Text) || Convert.ToDecimal(ViewState["Amount"].ToString()) == Convert.ToDecimal(txtCostCentreAmount.Text))
                    {
                        ViewState["Amount"] = Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtCostCentreAmount.Text);
                        dt_CostCentreTable.Rows.Add(null, ddlPartyName.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));


                        ddlCategory.ClearSelection();
                        ddlSubCategory.ClearSelection();
                        txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                        ViewState["CostCentreTable"] = dt_CostCentreTable;

                        GridCostCentreDetail.DataSource = dt_CostCentreTable;
                        GridCostCentreDetail.DataBind();


                        CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        if ((Convert.ToDecimal(lblGrandTotal.Text) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                        }
                        else
                        {
                            Save("CostCentre");
                        }
                    }
                    else
                    {
                        lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Amount Can't Be Greater Than Pending Amount.");
                        txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                    }
                }
                else
                {
                    lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Catgeory & Sub-Category Is Already Exists.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                }
            }

        }
        else
        {
            lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", msg);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }
    protected void AddCostCentre()
    {

        DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
        int status = 0;
        decimal CostCentreAmount = 0;
        foreach (GridViewRow row in GridCostCentreDetail.Rows)
        {
            Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
            Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
            if (lblCategory_ID.Text == ddlCategory.SelectedValue.ToString() && lblSubCategory_ID.Text == ddlSubCategory.SelectedValue.ToString())
            {
                status = 1;
            }
        }
        if (status == 0)
        {
            if (Convert.ToDecimal(ViewState["Amount"].ToString()) > Convert.ToDecimal(txtCostCentreAmount.Text) || Convert.ToDecimal(ViewState["Amount"].ToString()) == Convert.ToDecimal(txtCostCentreAmount.Text))
            {
                ViewState["Amount"] = Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtCostCentreAmount.Text);
                dt_CostCentreTable.Rows.Add(null, ddlLedger.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));

                ViewState["CostCentreTable"] = dt_CostCentreTable;
                DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                dt_CrCostCentreTable.Rows.Add(null, ddlLedger.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));
                //dt_CrCostCentreTable.Merge(dt_CostCentreTable);
                ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;
                GridCostCentreDetail.DataSource = dt_CostCentreTable;
                GridCostCentreDetail.DataBind();

                ddlCategory.ClearSelection();
                ddlSubCategory.ClearSelection();
                txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                if ((Math.Abs(Convert.ToDecimal(txtLedgerAmt.Text)) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                }
                else
                {
                   // CreateCostCentreTable();
                    FillLedgerAmount("0");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                    ViewState["GrandTotal"] = lblGrandTotal.Text;
                    //btnAcceptEnable();
                    ddlLedger.ClearSelection();
                    txtLedgerAmt.Text = "";
                }
            }
            else
            {
                lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Amount Can't Be Greater Than Pending Amount.");
                txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
            }
        }
        else
        {
            lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Catgeory & Sub-Category Is Already Exists.");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }
    protected void GridCostCentreDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblCostCentreModal.Text = "";
        string ID = e.CommandArgument.ToString();
        if (e.CommandName == "RecordDelete")
        {
            DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
            int Count = dt_CostCentreTable.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = dt_CostCentreTable.Rows[i];
                if (dr["RowNo"].ToString() == ID.ToString())
                {
                    decimal Amount = Math.Abs(Convert.ToDecimal(dr["Amount"].ToString()));
                    ViewState["Amount"] = Convert.ToDecimal(ViewState["Amount"]) + Amount;
                    txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                    dr.Delete();
                    break;
                }
            }
            dt_CostCentreTable.AcceptChanges();
            ViewState["CostCentreTable"] = dt_CostCentreTable;

            GridCostCentreDetail.DataSource = dt_CostCentreTable;
            GridCostCentreDetail.DataBind();

            decimal CostCentreAmount = 0;
            CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            if ((Convert.ToDecimal(lblGrandTotal.Text) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
            }
            else
            {
                Save("CostCentre");
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }
    // end of code cost center
}