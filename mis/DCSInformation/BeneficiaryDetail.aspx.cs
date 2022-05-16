using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DCSInformation_BeneficiaryDetail : System.Web.UI.Page
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
                    lblMsg.Text = "";
                    FillDistrict();
                    FillDCS();
                    FillCC();
                    FillBank();
                    FillBranch();
                    coldcsname.Visible = false;
                    colccname.Visible = false;
                  
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDistrict()
    {
        try
        {
            
            ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDCS()
    {
        try
        {

            ddlDCS.Items.Clear();
            int Count = 0;
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag", "District_ID" }, new string[] { "4", ddlDistrict.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Count = ds.Tables[0].Rows.Count;
                ddlDCS.DataSource = ds;
                ddlDCS.DataValueField = "Office_ID";
                ddlDCS.DataTextField = "Office_Name";
                ddlDCS.DataBind();
                
            }
            ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
            ddlDCS.Items.Insert((Count + 1), new ListItem("Other's", "-1"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCC()
    {
        try
        {
           // lblMsg.Text = "";
            ddlCC.Items.Clear();
            int Count = 0;
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag", "District_ID" }, new string[] { "10", ddlDistrict.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Count = ds.Tables[0].Rows.Count;
                ddlCC.DataSource = ds;
                ddlCC.DataValueField = "Office_ID";
                ddlCC.DataTextField = "Office_Name";
                ddlCC.DataBind();

            }
            ddlCC.Items.Insert(0, new ListItem("Select", "0"));
            ddlCC.Items.Insert((Count + 1), new ListItem("Other's", "-1"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBank()
    {
        try
        {
            ddlBank.Items.Clear();
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBank.DataSource = ds;
                ddlBank.DataValueField = "BankId";
                ddlBank.DataTextField = "BankName";
                ddlBank.DataBind();

            }
            ddlBank.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBranch()
    {
        try
        {
            ddlBranchName.Items.Clear();
            txtIFSC.Text = "";
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag", "BankID" }, new string[] { "6", ddlBank.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBranchName.DataSource = ds;
                ddlBranchName.DataValueField = "ID";
                ddlBranchName.DataTextField = "BranchName";
                ddlBranchName.DataBind();

            }
            ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDCS();
            FillCC();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillBranch();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtIFSC.Text = "";
            if (ddlBranchName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag", "ID" }, new string[] { "7", ddlBranchName.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtIFSC.Text = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            string DCSName = "";
            string CCName = "";
            if (ddlDCS.SelectedValue == "-1")
            {
                DCSName = txtDCSName.Text;
            }
            else
            {
                DCSName = ddlDCS.SelectedItem.Text;
            }
            if (ddlCC.SelectedValue == "-1")
            {
                CCName = txtCCName.Text;
            }
            else
            {
                CCName = ddlCC.SelectedItem.Text;
            }
            ds = objdb.ByProcedure("SpKCCBeneficiaryDetail", new string[] { "flag", 
                                                                            "District_ID", 
                                                                            "DCS_ID", 
                                                                            "CC_ID",
                                                                            "DCS_Name",
                                                                            "CC_Name",
                                                                            "BeneficiaryName", 
                                                                            "Gender", 
                                                                            "Bank_ID", 
                                                                            "Branch_ID", 
                                                                            "IFSCCode", 
                                                                            "AccountNo", 
                                                                            "MobileNo", 
                                                                            "Submitforincreaseinlimit", 
                                                                            "ApplicationforNewKCChavingland", 
                                                                            "ApplicationforNewKCChavingnoland", 
                                                                            "CardIssued", 
                                                                            "IsActive", 
                                                                            "UpdatedBy" }, 
                                                                             new string[] 
                                                                             {"0",
                                                                              ddlDistrict.SelectedValue.ToString(),
                                                                              ddlDCS.SelectedValue.ToString(),
                                                                              ddlCC.SelectedValue.ToString(),
                                                                              DCSName,
                                                                              CCName,
                                                                              txtBeneficiaryName.Text,
                                                                              rbtnGender.SelectedValue.ToString(),
                                                                              ddlBank.SelectedValue.ToString(),
                                                                              ddlBranchName.SelectedValue.ToString(),
                                                                              txtIFSC.Text,
                                                                              txtBankAccountNo.Text,
                                                                              txtMobileNo.Text,
                                                                              rbtnsubmitforincreaseinlimit.SelectedValue.ToString(),
                                                                              rbtnAppfornewkcchavingland.SelectedValue.ToString(),
                                                                              rbtnAppfornewkcchavingnoland.SelectedValue.ToString(),
                                                                              rbtnCardissued.SelectedValue.ToString(),
                                                                              IsActive,
                                                                              Session["Emp_ID"].ToString()
                                                                             }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Saved Successfully.");
            Clear();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Clear()
    {
        ddlDistrict.ClearSelection();
        ddlDCS.ClearSelection();
        ddlDCS.SelectedItem.Text= "";
        txtBeneficiaryName.Text= "";
        rbtnGender.ClearSelection();
        ddlBank.ClearSelection();
        ddlBranchName.ClearSelection();
        txtIFSC.Text= "";
        txtBankAccountNo.Text= "";
        txtMobileNo.Text= "";
        rbtnsubmitforincreaseinlimit.SelectedValue = "No";
        rbtnAppfornewkcchavingland.SelectedValue = "No";
        rbtnAppfornewkcchavingnoland.SelectedValue = "No";
        rbtnCardissued.SelectedValue = "No";
        ddlCC.Items.Clear();
        ddlCC.Items.Insert(0, new ListItem("Select", "0"));
        ddlDCS.Items.Clear();
        ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
        coldcsname.Visible = false;
        colccname.Visible = false;
    }
    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            colccname.Visible = false;
            txtCCName.Text = "";
            if (ddlCC.SelectedValue == "-1")
            {
                colccname.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDCS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            coldcsname.Visible = false;
            txtDCSName.Text = "";
            if (ddlDCS.SelectedValue == "-1")
            {
                coldcsname.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}