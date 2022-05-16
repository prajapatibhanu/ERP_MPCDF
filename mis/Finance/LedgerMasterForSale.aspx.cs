using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;


public partial class mis_Finance_LedgerMasterForSale : System.Web.UI.Page
{
    DataSet ds;
    static DataSet ds5, ds6;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    static string prevPage = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                if (!IsPostBack)
                {
                    if (Request.UrlReferrer != null)
                    {
                        prevPage = Request.UrlReferrer.ToString();
                    }
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Current_Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Ledger_ID"] = "0";
                    ViewState["ST"] = "0";
                    ViewState["Amount"] = "0";

                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    FillHead();
                    GetVoucherDate();
                    setDate();
                    if (Request.QueryString["Ledger_ID"] != null)
                    {
                        string mode = objdb.Decrypt(Request.QueryString["Mode"].ToString());
                        {
                            if (mode == "View")
                            {
                                ViewState["Ledger_ID"] = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                                pnbody.Enabled = false;
                                FillLedgerDetail();
                            }
                            else
                            {
                                ViewState["Ledger_ID"] = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "17", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["status"].ToString() == "true")
                                    {
                                        ViewState["ST"] = "2";
                                        ViewState["Office_ID"] = ds.Tables[1].Rows[0]["Office_ID"].ToString();
                                        if (ViewState["ST"].ToString() == "2")
                                        {
                                            FillLedgerDetail();
                                            pnlOB.Enabled = false;

                                            ddlBalBillByBill.Enabled = false;
                                            ddlInventoryAffected.Enabled = false;


                                        }


                                    }
                                    else
                                    {
                                        FillLedgerDetail();
                                    }
                                }
                                else
                                {

                                    //ViewState["Office_ID"] = Session["Office_ID"].ToString();
                                }





                            }


                        }

                    }


                    GetLedgerName();
                    //ShowHideSetDefault(); 

                    // 
                }
                else
                {
                    WebControl wcICausedPostBack = (WebControl)GetControlThatCausedPostBack(sender as Page);
                    int indx = wcICausedPostBack.TabIndex;
                    var ctrl = from control in wcICausedPostBack.Parent.Controls.OfType<WebControl>()
                               where control.TabIndex > indx
                               select control;
                    ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus();
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
    protected Control GetControlThatCausedPostBack(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button || c is System.Web.UI.WebControls.ImageButton)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;

    }
    protected void FillHead()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinMasterHead",
                          new string[] { "flag" },
                          new string[] { "15" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlHeadName.DataTextField = "Head_Name";
                ddlHeadName.DataValueField = "Head_ID";
                ddlHeadName.DataSource = ds;
                ddlHeadName.DataBind();
                ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearText()
    {
        ddlHeadName.ClearSelection();
        ddlHeadName.Enabled = true;
        txtLedgerName.Text = "";
        txtLedgerAlias.Text = "";
        ddlInventoryAffected.ClearSelection();
        txtOpeningBalance.Text = "0";
        ViewState["ST"] = "0";
        pnlOB.Enabled = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Active = "";
            string ExistMsg = "";
            string LedgerChild_IsActive = "1";
            string LedgerStatusActive = "1";
            string Ledger_IsActive = "1";
            string EffectiveDate = "";
            string LedgerTx_IsActive = "1"; 
            if (txtLedgerName.Text == "")
            {
                msg += "Enter Ledger Name.\\n";
            }
            if (ddlHeadName.SelectedIndex == 0)
            {
                msg += "Select Head Name. ";
            }          
            if (ddlFinancialYear.SelectedIndex == 0)
            {
                msg += "Select Financial Year";
            }
            if (txtOpeningBalance.Text == "")
            {
                msg += "Enter Opening Balance ";
            }
           
            if (msg.Trim() == "")
            {

                int Status = 0;
                //txtLedgerName.Text = FirstLetterToUpper(txtLedgerName.Text);               
                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_Name", "Ledger_ID", "Ledger_HeadID", "Office_ID" }, new string[] { "10", txtLedgerName.Text, ViewState["Ledger_ID"].ToString(), ddlHeadName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }                   
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

                if (btnSave.Text == "Save & Next" && ViewState["Ledger_ID"].ToString() == "0" && Status == 0)
                {

                    ds = objdb.ByProcedure("SpFinLedgerMaster",
                       new string[] { "flag", "Ledger_Name", "Ledger_HeadID", "Office_ID", "Ledger_Alias", "InventoryAffected",  "Ledger_IsActive", "Ledger_UpdatedBy", "MaintainBalancesBillByBill", "FinancialYear", "Year"},
                       new string[] { "0", txtLedgerName.Text.Trim(), ddlHeadName.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), txtLedgerAlias.Text,  ddlInventoryAffected.SelectedValue.ToString(), Ledger_IsActive, ViewState["Emp_ID"].ToString(), ddlBalBillByBill.SelectedValue, FinancialYear.ToString(), Year.ToString() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string Ledger_ID = ds.Tables[0].Rows[0]["Ledger_ID"].ToString();
                       if (Ledger_ID != "")
                       {                          
                          
                               string Value = ViewState["Office_ID"].ToString();
                               string LedgerTx_Amount = "0";
                               if (ddlBalBillByBill.SelectedValue == "Yes")
                               {
                                   if (decimal.Parse(txtOpeningBalance.Text) == 0)
                                   {
                                       LedgerStatusActive = "1";
                                       LedgerTx_IsActive = "1";
                                   }
                                   else
                                   {
                                       LedgerStatusActive = "0";
                                       LedgerTx_IsActive = "0";
                                   }
                               }
                               else
                               {
                                   LedgerStatusActive = "1";
                                   LedgerTx_IsActive = "1";
                               }
                               LedgerTx_Amount = txtOpeningBalance.Text;
                               if (ddlDrCr.SelectedItem.Text == "Credit")
                               {
                                   LedgerTx_Amount = LedgerTx_Amount;
                               }
                               else
                               {
                                   LedgerTx_Amount = "-" + LedgerTx_Amount;
                               }


                              objdb.ByProcedure("SpFinLedgerChild", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy", "LedgerStatusActive" }, new string[] { "0", Ledger_ID, Value.ToString(), LedgerChild_IsActive, ViewState["Emp_ID"].ToString(), LedgerStatusActive }, "dataset");

                              objdb.ByProcedure("SpFinLedgerTx",
                                   new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type","VoucherTx_Date" },
                                   new string[] { "9", Ledger_ID, "Closing Balance", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), Value.ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "0", "Sub Ledger", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                              if (ddlBalBillByBill.SelectedValue == "Yes")
                              {
                                  Response.Redirect("AlterLedgerReference.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID.ToString()));
                              }
                              {
                                  lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation SuccessfullyCompleted");
                                  ClearText();
                              }
                               
                       }
                    }
                }
                else if (btnSave.Text == "Update" && ViewState["Ledger_ID"].ToString() != "0" && Status == 0)
                {


                    objdb.ByProcedure("SpFinLedgerMaster",
                    new string[] { "flag", "Ledger_ID", "Ledger_Name", "Ledger_HeadID", "Ledger_Alias", "InventoryAffected","Ledger_UpdatedBy", "MaintainBalancesBillByBill", "FinancialYear", "Year"},
                    new string[] { "6", ViewState["Ledger_ID"].ToString(), txtLedgerName.Text.Trim(), ddlHeadName.SelectedValue.ToString(), txtLedgerAlias.Text, ddlInventoryAffected.SelectedValue.ToString(), ViewState["Emp_ID"].ToString(), ddlBalBillByBill.SelectedValue, FinancialYear.ToString(), Year.ToString()}, "dataset");

                   
                  
                       if (ddlBalBillByBill.SelectedValue == "Yes")
                       {
                           Response.Redirect("AlterLedgerReference.aspx?Ledger_ID=" + objdb.Encrypt(ViewState["Ledger_ID"].ToString()));
                       }
                       {
                           Response.Redirect("LedgerDetailB.aspx");
                           lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                       }
                   

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger name is already exist.');", true);
                    lblMsg.Text = "";
                    ClearText();
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
    protected void FillLedgerDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID", "Cuurent_Office_ID" }, new string[] { "18", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString(), ViewState["Current_Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlHeadName.ClearSelection();
                ddlHeadName.Items.FindByValue(ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString()).Selected = true;
                string HeadID = ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString();
                ddlHeadName.Enabled = false;
                txtLedgerAlias.Text = ds.Tables[0].Rows[0]["Ledger_Alias"].ToString();
                txtLedgerName.Text = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
                ddlInventoryAffected.ClearSelection();
                ddlInventoryAffected.Items.FindByValue(ds.Tables[0].Rows[0]["InventoryAffected"].ToString()).Selected = true;
                ddlBalBillByBill.ClearSelection();
                ddlBalBillByBill.Items.FindByValue(ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString()).Selected = true;
                
                if (ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString() != "")
                {
                    ddlBalBillByBill.ClearSelection();
                    ddlBalBillByBill.Items.FindByText(ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString()).Selected = true;
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["LedgerTx_Amount"].ToString() != "")
                {
                    txtOpeningBalance.Text = ds.Tables[1].Rows[0]["LedgerTx_Amount"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[1].Rows[0]["VoucherTx_Date"].ToString();
                    if (ds.Tables[1].Rows[0]["DrCr"].ToString() != "")
                    {
                        ddlDrCr.ClearSelection();
                        ddlDrCr.Items.FindByValue(ds.Tables[1].Rows[0]["DrCr"].ToString()).Selected = true;
                    }
                }

            }


            btnSave.Text = "Update";
            clear.Visible = false;
            ddlHeadName.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GetVoucherDate()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                ViewState["VoucherDate"] = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchCustomers(string Ledger_Name)
    {
        List<string> customers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "Ledger_Name like '%" + Ledger_Name + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    customers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return customers;
    }
    private void GetLedgerName()
    {
        try
        {
            string Office_ID = Session["Office_ID"].ToString();
            DataSet ds4 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "50", Office_ID }, "dataset");
            ds6 = ds4;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void setDate()
    {
        try
        {
            string VoucherDate = ViewState["VoucherDate"].ToString();
            string sDate = (Convert.ToDateTime(VoucherDate, cult).ToString("yyyy/MM/dd")).ToString();
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
            txtVoucherTx_Date.Text = "01" + '/' + "04" + '/' + FY.ToString();



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
            lblMsg.Text = "";
            if (txtVoucherTx_Date.Text != "")
            {

                ds = objdb.ByProcedure("SpFinVoucherDate",
                    new string[] { "flag", "Office_ID", "VoucherDate" },
                    new string[] { "5", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");


                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "TRUE")
                {
                    //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select Date according to Financial Year which has been set from Voucher Date.');", true);
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert!", "Please select valid date.");
                    setDate();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
