using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_VoucherGoodsTransferOutward : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    //static DataSet dsItem;
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
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["Action"] = "";


                    FillParticularsDropDown();
                    FillItem();
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    // ddlLedgerDr.Enabled = false;
                    
                    txtLedgerDr_Amount.Attributes.Add("readonly", "readonly");
                    txtLedgerCr_Amount.Attributes.Add("readonly", "readonly");
                    txtVoucherTx_No.Attributes.Add("readonly", "readonly");

                    txtLedgerDr_Amount.Text = "0.00";
                    txtLedgerCr_Amount.Text = "0.00";

                    txtCurrentBalanceCr.Text = "0.00 Cr";
                    txtCurrentBalanceDr.Text = "0.00 Cr";

                    FillVoucherDate();
                    FillVoucherNo();

                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            FillDetail();
                            //string ValidStatus = ValidDate();
                            //if (ValidStatus == "No")
                            //{
                            //    Response.Redirect("~/mis/Login.aspx");
                            //}
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

    //Fill Item DropDown
    protected void FillItem()
    {
        try
        {
            ds = objdb.ByProcedure("SpItemMaster", new string[] { "flag", "Office_ID" }, new string[] { "24", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemName.Items.Clear();
                ddlItemName.DataSource = ds.Tables[0];
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));

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
                //ViewState["Voucher_FY"] = ds.Tables[0].Rows[0]["Voucher_FY"].ToString();
                FillVoucherNo();
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
                string VoucherTx_Names_ForSno = "Stock Issue To Plant";

                DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                    new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");
                string Office_Code = "";
                if (ds1.Tables[1].Rows.Count != 0)
                {
                    Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
                }
                int VoucherTx_SNo = 0;
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

                }

                //ViewState["PreVoucherNo"] = Office_Code + FinancialYear.ToString().Substring(2) + "SP" + VoucherTx_SNo.ToString();
                VoucherTx_SNo = VoucherTx_SNo + 1;

                //ViewState["VoucherTx_SNo"] = VoucherTx_SNo;

                //txtVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "SP" + VoucherTx_SNo.ToString();
                lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "SP";
                txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
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
                if (ViewState["FY"].ToString() != FinancialYear.ToString())
                {
                    txtVoucherTx_Date.Text = ViewState["VoucherTx_Date"].ToString();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Date selection should be according to Financial Year(" + ViewState["FY"].ToString() + ")');", true);
                    
                }
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
            //ds = objdb.ByProcedure("SpFinLedgerMaster",
            //   new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
            //   new string[] { "61", ViewState["Office_ID"].ToString(), "160" }, "dataset");
            ds = objdb.ByProcedure("SpFinLedgerMaster",
              new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
              new string[] { "69", ViewState["Office_ID"].ToString(), "160" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedgerCr.DataSource = ds;
                ddlLedgerCr.DataTextField = "Ledger_Name";
                ddlLedgerCr.DataValueField = "Ledger_ID";
                ddlLedgerCr.DataBind();
                ddlLedgerCr.Items.Insert(0, "Select");
                ddlLedgerCr.SelectedValue = "79105";
                
            }
            else
            {

            }
            //ds = objdb.ByProcedure("SpFinLedgerMaster",
            //   new string[] { "flag" },
            //   new string[] { "62" }, "dataset");
            ds = objdb.ByProcedure("SpFinLedgerMaster",
             new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
             new string[] { "69", ViewState["Office_ID"].ToString(), "160" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedgerDr.DataSource = ds;
                ddlLedgerDr.DataTextField = "Ledger_Name";
                ddlLedgerDr.DataValueField = "Ledger_ID";
                ddlLedgerDr.DataBind();
                ddlLedgerDr.Items.Insert(0, "Select");
                ddlLedgerDr.SelectedValue = "79104";
                
              

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

    //Ledger CurrentBalance
    protected void ddlLedgerCr_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            txtCurrentBalanceCr.Text = "";
            if (ddlLedgerCr.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedgerCr.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalanceCr.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    //Fill Previous VoucherNarration
    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                 new string[] { "14", "Stock Issue To Plant", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //change voucher series according to FY
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        FillVoucherNo();
        //string ValidStatus = ValidDate();
        //if (ValidStatus == "No")
        //{

        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
        //    FillVoucherDate();
        //}
        //else
        //{
        //    FillVoucherNo();
        //}
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (ddlItemName.SelectedIndex == 0)
            {
                msg += "Select Item Name.\\n";
            }
            if (txtQuantity.Text == "")
            {
                msg += "Enter Quantity.\\n";
            }
            if (txtRate.Text == "")
            {
                msg += "Enter Rate.\\n";
            }
            if (txtTotalAmount.Text == "")
            {
                msg += "Enter Amount.\\n";
            }
            if (msg == "")
            {
                decimal LedgerAmount = 0;



                string Item_ID = ddlItemName.SelectedValue.ToString();
                string Unit_id = "";
                string UQCCode = "";
                ds = objdb.ByProcedure("SpItemMaster", new string[] { "flag", "ItemId" }, new string[] { "23", Item_ID }, "dataset");
                if (ds != null)
                {
                    Unit_id = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                    UQCCode = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                }





                DataTable dt_ItemTable = new DataTable();
                dt_ItemTable.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                dt_ItemTable.Columns.Add(new DataColumn("Item", typeof(string)));
                dt_ItemTable.Columns.Add(new DataColumn("UnitID", typeof(string)));
                dt_ItemTable.Columns.Add(new DataColumn("UQCCode", typeof(string)));
                dt_ItemTable.Columns.Add(new DataColumn("Quantity", typeof(float)));
                dt_ItemTable.Columns.Add(new DataColumn("Rate", typeof(decimal)));
                dt_ItemTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));


                decimal Amount = 0;
                int i = 0;

                foreach (GridViewRow rows in GridViewItem.Rows)
                {

                    Label lblItemID = (Label)rows.FindControl("lblItemID");
                    Label lblItem = (Label)rows.FindControl("lblItem");
                    Label lblUnitID = (Label)rows.FindControl("lblUnitID");
                    Label lblUQCCode = (Label)rows.FindControl("lblUQCCode");
                    Label lblQuantity = (Label)rows.FindControl("lblQuantity");

                    Label lblRate = (Label)rows.FindControl("lblRate");
                    Label lblAmount = (Label)rows.FindControl("lblAmount");

                   
                    dt_ItemTable.Rows.Add(lblItemID.Text, lblItem.Text, lblUnitID.Text, lblUQCCode.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text);
                    Amount = decimal.Parse(lblAmount.Text);
                    LedgerAmount = LedgerAmount + Amount;

                    if (Item_ID == lblItemID.Text)
                        i = 1;
                }



                dt_ItemTable.Rows.Add(ddlItemName.SelectedValue.ToString(), ddlItemName.SelectedItem.Text, Unit_id, UQCCode, txtQuantity.Text, txtRate.Text, txtTotalAmount.Text);

                Amount = decimal.Parse(txtTotalAmount.Text);
                LedgerAmount = LedgerAmount + Amount;

                if(i == 0)
                {
                    GridViewItem.DataSource = dt_ItemTable;
                    GridViewItem.DataBind();

                    txtLedgerDr_Amount.Text = LedgerAmount.ToString();
                    txtLedgerCr_Amount.Text = LedgerAmount.ToString();

                    ddlItemName.ClearSelection();
                    txtQuantity.Text = "";
                    txtRate.Text = "";
                    txtTotalAmount.Text = "";
                }
                else
                {
                    ddlItemName.ClearSelection();
                    txtQuantity.Text = "";
                    txtRate.Text = "";
                    txtTotalAmount.Text = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Name Already Exists.');", true);
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


    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg = "Enter Voucher/Bill No.\\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Select Voucher Date.\\n";
            }
            if (ddlLedgerDr.SelectedIndex == 0)
            {
                msg += "Select Particulars (DR).\\n";
            }           
            if (GridViewItem.Rows.Count == 0)
            {
                msg += "Select at least one Item.\\n";
            }
            if (ddlLedgerCr.SelectedIndex == 0)
            {
                msg += "Select Particulars (CR).\\n";
            }
            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration.\\n";
            }
            //string ValidStatus = ValidDate();
            //if (ValidStatus == "No")
            //{
            //    Response.Redirect("~/mis/Login.aspx");
            //}
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
                string VoucherTx_IsActive = "1";
                string LedgerTx_IsActive = "1";
                string ItemTx_IsActive = "1";
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
                    VoucherTx_IsActive = "0";
                    LedgerTx_IsActive = "0";
                    ItemTx_IsActive = "0";
                    string VoucherTx_SNo = txtVoucherTx_No.Text;
                   
                    
                    
                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Narration",
                        "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY",
                        "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SNo" },
                      new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Stock Issue To Plant", "Stock Issue To Plant", VoucherTx_No, txtVoucherTx_Narration.Text,
                          txtLedgerDr_Amount.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), 
                          VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), VoucherTx_SNo }, "dataset");
                    string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        //string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        //string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        //string LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString(); ;
                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                        new string[] { "0", ddlLedgerCr.SelectedValue.ToString(), VoucherTx_ID, "Stock Issue To Plant", txtLedgerCr_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), "1", "Main Ledger", "None" }, "dataset");


                        int i = 0;
                        foreach (GridViewRow rows in GridViewItem.Rows)
                        {

                            Label lblItemID = (Label)rows.FindControl("lblItemID");
                            Label lblItem = (Label)rows.FindControl("lblItem");
                            Label lblUnitID = (Label)rows.FindControl("lblUnitID");
                            Label lblUQCCode = (Label)rows.FindControl("lblUQCCode");
                            Label lblQuantity = (Label)rows.FindControl("lblQuantity");

                            Label lblRate = (Label)rows.FindControl("lblRate");
                            Label lblAmount = (Label)rows.FindControl("lblAmount");

                            i = i + 1;



                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Ledger_ID", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" },
                                                             new string[] { "0", VoucherTx_ID, "Stock Issue To Plant", "Stock Issue To Plant", ddlLedgerCr.SelectedValue.ToString(), lblItemID.Text, lblUnitID.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (i).ToString() }, "dataset");
                            objdb.ByProcedure("SpFinItemTx",
                        new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "CreatedBy", "TranDt", "Amount" }
                       , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, VoucherTx_ID, "Stock Issue To Plant", VoucherTx_No, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), lblAmount.Text }, "dataset");


                        }




                        string DebitAmount = txtLedgerDr_Amount.Text;
                        DebitAmount = "-" + DebitAmount.ToString();
                        objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                                new string[] { "0", ddlLedgerDr.SelectedValue.ToString(), VoucherTx_ID, "Stock Issue To Plant", DebitAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), "2", "Main Ledger", "None" }, "dataset");

                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");

                        ClearText();
                    }
                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    string VoucherTx_SNo = txtVoucherTx_No.Text;
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SNo" },
                    new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Stock Issue To Plant", "Stock Issue To Plant", VoucherTx_No, "", txtVoucherTx_Narration.Text, txtLedgerDr_Amount.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString(), VoucherTx_SNo }, "dataset");

                    string VoucherTx_ID = ViewState["VoucherTx_ID"].ToString();

                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "35", ViewState["VoucherTx_ID"].ToString() }, "dataset");



                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                    new string[] { "0", ddlLedgerCr.SelectedValue.ToString(), VoucherTx_ID, "Stock Issue To Plant", txtLedgerCr_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), "1", "Main Ledger", "None" }, "dataset");

                    int i = 0;
                    foreach (GridViewRow rows in GridViewItem.Rows)
                    {

                        Label lblItemID = (Label)rows.FindControl("lblItemID");
                        Label lblItem = (Label)rows.FindControl("lblItem");
                        Label lblUnitID = (Label)rows.FindControl("lblUnitID");
                        Label lblUQCCode = (Label)rows.FindControl("lblUQCCode");
                        Label lblQuantity = (Label)rows.FindControl("lblQuantity");

                        Label lblRate = (Label)rows.FindControl("lblRate");
                        Label lblAmount = (Label)rows.FindControl("lblAmount");

                        i = i + 1;



                        objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Ledger_ID", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" },
                                                         new string[] { "0", VoucherTx_ID, "Stock Issue To Plant", "Stock Issue To Plant", ddlLedgerCr.SelectedValue.ToString(), lblItemID.Text, lblUnitID.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (i).ToString() }, "dataset");
                        objdb.ByProcedure("SpFinItemTx",
                    new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "CreatedBy", "TranDt", "Amount" }
                   , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, VoucherTx_ID, "Stock Issue To Plant", VoucherTx_No, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), lblAmount.Text }, "dataset");


                    }

                    int Count = 0;
                    string DebitAmount = txtLedgerDr_Amount.Text;
                    DebitAmount = "-" + DebitAmount.ToString();



                    objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                            new string[] { "0", ddlLedgerDr.SelectedValue.ToString(), VoucherTx_ID, "Stock Issue To Plant", DebitAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), "2", "Main Ledger", "None" }, "dataset");



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


    protected void ClearText()
    {
        try
        {

            FillVoucherDate();
            GetPreviousVoucherNo();

            txtVoucherTx_Narration.Text = "";
            ddlLedgerCr.ClearSelection();
            txtCurrentBalanceCr.Text = "";
            txtLedgerCr_Amount.Text = "";
            ddlItemName.ClearSelection();
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtTotalAmount.Text = "";
            ddlLedgerDr.ClearSelection();
            txtCurrentBalanceDr.Text = "";
            txtLedgerDr_Amount.Text = "";

            GridViewItem.DataSource = null;
            GridViewItem.DataBind();

            FillVoucherNo();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "36", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtVoucherTx_No.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    var rx = new System.Text.RegularExpressions.Regex("SP");
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    var array = rx.Split(str);
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_No.Text = array[1];
                    lblVoucherTx_No.Text = array[0] + "SP";
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();

                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();

                }


                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlLedgerCr.ClearSelection();
                    ddlLedgerCr.Items.FindByValue(ds.Tables[1].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                    txtLedgerCr_Amount.Text = ds.Tables[1].Rows[0]["LedgerTx_Credit"].ToString();

                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    ddlLedgerDr.ClearSelection();
                    ddlLedgerDr.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                    txtLedgerDr_Amount.Text = ds.Tables[2].Rows[0]["LedgerTx_Amount"].ToString();

                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[3];
                    GridViewItem.DataBind();

                    //DataSet dsItem = (DataSet)ViewState["dsItem"];
                    //int rowscount = ds.Tables[3].Rows.Count;
                    //for (int i = 0; i < rowscount; i++)
                    //{

                    //    string Item_id = ds.Tables[3].Rows[i]["Item_id"].ToString();
                    //    string Unit_id = ds.Tables[3].Rows[i]["Unit_id"].ToString();
                    //    string Item = ds.Tables[3].Rows[i]["Item"].ToString();
                    //    string Quantity = ds.Tables[3].Rows[i]["Quantity"].ToString();
                    //    string Rate = ds.Tables[3].Rows[i]["Rate"].ToString();
                    //    string UQCCode = ds.Tables[3].Rows[i]["UQCCode"].ToString();
                    //    string Amount = ds.Tables[3].Rows[i]["Amount"].ToString();
                    //    string TNO = ds.Tables[3].Rows[i]["LedgerTx_OrderBy"].ToString();
                    //    DataTable dt_ItemTable = new DataTable(TNO.ToString());
                    //    dt_ItemTable.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                    //    dt_ItemTable.Columns.Add(new DataColumn("Item", typeof(string)));
                    //    dt_ItemTable.Columns.Add(new DataColumn("UnitID", typeof(string)));
                    //    dt_ItemTable.Columns.Add(new DataColumn("UQCCode", typeof(string)));
                    //    dt_ItemTable.Columns.Add(new DataColumn("Quantity", typeof(float)));
                    //    dt_ItemTable.Columns.Add(new DataColumn("Rate", typeof(decimal)));
                    //    dt_ItemTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
                    //    dt_ItemTable.Rows.Add(Item_id, Item, Unit_id, UQCCode, Quantity, Rate, Amount);
                    //    ViewState["ItemTable"] = dt_ItemTable;

                    //    GridViewItem.DataSource = dt_ItemTable;
                    //    GridViewItem.DataBind();
                    //    dsItem.Merge((DataTable)ViewState["ItemTable"]);


                    //}


                }


            }
            btnAccept.Text = "Update";
            btnClear.Visible = false;
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
            // divparticular.Visible = false;
            btnAdd.Visible = false;
            GridViewItem.Columns[5].Visible = false;
            txtVoucherTx_Narration.Attributes.Add("readonly", "readonly");
            txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
            txtVoucherTx_No.Attributes.Add("readonly", "readonly");

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtTotalAmount.Text = "";
            if (ddlItemName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "ItemId" },
                        new string[] { "20", ddlItemName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    lblUnit.Text = ds.Tables[0].Rows[0]["NoOfDecimalPlace"].ToString();

                }
            }
            //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAddItemModal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

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
            string VoucherTx_Type = "Stock Issue To Plant";
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

            DataSet dsValidDate = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "Office_ID", "VoucherDate", "FinancialYear" }, new string[] { "3", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (dsValidDate.Tables.Count != 0 && dsValidDate.Tables[0].Rows.Count != 0)
            {
                validDays = dsValidDate.Tables[0].Rows[0]["ValidStatus"].ToString();
            }
        }
        return validDays;
    }

    protected void ddlLedgerDr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtCurrentBalanceDr.Text = "";
            if (ddlLedgerDr.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" },
                      new string[] { "1", ddlLedgerDr.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalanceDr.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void GridViewItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewItem.DataKeys[e.RowIndex].Value.ToString();
            DataTable dt_ItemTable = new DataTable();
            dt_ItemTable.Columns.Add(new DataColumn("Item_ID", typeof(string)));
            dt_ItemTable.Columns.Add(new DataColumn("Item", typeof(string)));
            dt_ItemTable.Columns.Add(new DataColumn("UnitID", typeof(string)));
            dt_ItemTable.Columns.Add(new DataColumn("UQCCode", typeof(string)));
            dt_ItemTable.Columns.Add(new DataColumn("Quantity", typeof(float)));
            dt_ItemTable.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_ItemTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));

            decimal LedgerAmount = 0;
            decimal Amount = 0;
            int i = 0;
            foreach (GridViewRow rows in GridViewItem.Rows)
            {

                Label lblItemID = (Label)rows.FindControl("lblItemID");
                Label lblItem = (Label)rows.FindControl("lblItem");
                Label lblUnitID = (Label)rows.FindControl("lblUnitID");
                Label lblUQCCode = (Label)rows.FindControl("lblUQCCode");
                Label lblQuantity = (Label)rows.FindControl("lblQuantity");

                Label lblRate = (Label)rows.FindControl("lblRate");
                Label lblAmount = (Label)rows.FindControl("lblAmount");

                i = i + 1;
                if (lblItemID.Text != ID)
                {
                    dt_ItemTable.Rows.Add(lblItemID.Text, lblItem.Text, lblUnitID.Text, lblUQCCode.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text);
                    Amount = decimal.Parse(lblAmount.Text);
                    LedgerAmount = LedgerAmount + Amount;
                }


            }
            GridViewItem.DataSource = dt_ItemTable;
            GridViewItem.DataBind();


            txtLedgerDr_Amount.Text = LedgerAmount.ToString();
            txtLedgerCr_Amount.Text = LedgerAmount.ToString();



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}