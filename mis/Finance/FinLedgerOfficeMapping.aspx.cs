using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_FinLedgerOfficeMapping : System.Web.UI.Page
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
                    FillOfficeDropdown();
                    FillLedgerName();
                    GetVoucherDate();
                    FillOffice();
                    panel1.Enabled = false;
                    btnDel.Visible = false;
                    
                   
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
    protected void FillLedgerName()
    {
        try
        {
            ddlLedgerName.Items.Clear();
            ds = objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Office_ID" }, new string[] { "5",ddlOffice.SelectedValue.ToString()}, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                ddlLedgerName.DataSource = ds;
                ddlLedgerName.DataTextField = "Ledger_Name";
                ddlLedgerName.DataValueField = "Ledger_ID";
                ddlLedgerName.DataBind();
                ddlLedgerName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlLedgerName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOfficeDropdown()
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
                ddlOffice.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillData();
            ShowHideDeleteBtn();
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


                //Start For Voucher No

                //End

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    } 
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                new string[] { "flag" },
                new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkOffice.DataSource = ds.Tables[0];
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();

                //chkAllProductionUnit.DataSource = ds.Tables[1];
                //chkAllProductionUnit.DataTextField = "Office_Name";
                //chkAllProductionUnit.DataValueField = "Office_ID";
                //chkAllProductionUnit.DataBind();


                //chkOtherOffice.DataSource = ds.Tables[1];
                //chkOtherOffice.DataTextField = "Office_Name";
                //chkOtherOffice.DataValueField = "Office_ID";
                //chkOtherOffice.DataBind();

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
    protected void FillData()
    {
        try
        {
            if (ddlLedgerName.SelectedIndex > 0)
            {
                panel1.Visible = true;
                lblMsg.Text = "";
                           
                chkOffice.ClearSelection();
                
                chkOtherOffice.ClearSelection();
                chkHeadOffice.Checked = false;
                ds = objdb.ByProcedure("SpFinLedgerMaster",
                    new string[] { "flag", "Ledger_ID" },
                    new string[] { "32", ddlLedgerName.SelectedValue.ToString() }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string Value = ds.Tables[0].Rows[i]["Office_ID"].ToString();
                            if (Value == "1")
                            {
                                chkHeadOffice.Checked = true;
                                DataSet ds11 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "38", ddlLedgerName.SelectedValue.ToString(), Value }, "dataset");
                                if (ds11 != null)
                                {
                                    if (ds11.Tables[0].Rows[0]["Status"].ToString() == "True")
                                    {
                                        chkHeadOffice.Enabled = false;
                                    }
                                    else
                                    {
                                        chkHeadOffice.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                foreach (ListItem item in chkOffice.Items)
                                {
                                    if (item.Value == Value)
                                    {
                                        item.Selected = true;
                                        DataSet ds11 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "38", ddlLedgerName.SelectedValue.ToString(), item.Value }, "dataset");
                                        if (ds11 != null)
                                        {
                                            if (ds11.Tables[0].Rows[0]["Status"].ToString() == "True")
                                            {
                                                item.Enabled = false;
                                            }
                                            else
                                            {
                                                item.Enabled = true;
                                            }
                                        }
                                    }
                                }
                               
                                
                            }


                        }
                        
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                panel1.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ShowHideDeleteBtn()
    {
        try
        {
            btnDel.Visible = true;
           
            if (chkHeadOffice.Checked == true)
            {
                btnDel.Visible = false;
                return;
            }
            foreach (ListItem item in chkOffice.Items)
            {
                if (item.Selected == true)
                {
                    btnDel.Visible = false;
                    return;
                }
            }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                
                objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Ledger_ID", "Ledger_UpdatedBy", "Office_ID" }, new string[] { "4", ddlLedgerName.SelectedValue.ToString(), ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                FillLedgerName();
                FillData();
                btnDel.Visible = false;
                
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            FillLedgerName();
            panel1.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}