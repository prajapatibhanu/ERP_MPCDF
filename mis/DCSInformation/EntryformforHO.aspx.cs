using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DCSInformation_EntryformforHO : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN",true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    FillUnion();
                    FillDistrict();
                   // FillDCS();
                    FillBank();
                    FillBranch();
                   // FillCC();
                    CreateBankTable();
                    txtTotalNoofmembersinunion.Attributes.Add("readonly", "readonly");
                    txtOutofwhichTotalNoofformsSubmittedforincreaseinlimit.Attributes.Add("readonly", "readonly");
                    txtTotalApplicationforNewKCChavingLand.Attributes.Add("readonly", "readonly");
                    txtTotalApplicationforNewKCChavingnoLand.Attributes.Add("readonly", "readonly");
                    txtTotalNoofformsSubmittedbyUniontotheBank.Attributes.Add("readonly", "readonly");
                    txtTotalNoofCardIssued.Attributes.Add("readonly", "readonly");
                    txtAnyOthersByTotal.Attributes.Add("readonly", "readonly");
                    txtTotalNoofformsSubmittedbyDCS.Attributes.Add("readonly", "readonly");
                    txtIFSC.Attributes.Add("readonly", "readonly");
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
    protected void FillUnion()
    {
        try
        {
            
            ddlUnion.Items.Clear();
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag"}, new string[] {"1" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlUnion.DataSource = ds;
                    ddlUnion.DataTextField = "Office_Name";
                    ddlUnion.DataValueField = "Office_ID";
                    ddlUnion.DataBind();
                   //ddlUnion.SelectedValue = Session["Office_ID"].ToString();
                    
                }
                
            }
            ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
            
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
            ddlDistrict.Items.Clear();
            txtTotalNoofmembersinunion.Text = "";
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag", "Office_ID" }, new string[] { "3", ddlUnion.SelectedValue.ToString() }, "dataset");
            if(ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataSource = ds;
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataBind();
                    
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    txtTotalNoofmembersinunion.Text = ds.Tables[1].Rows[0]["NoofMemebrsinUnion"].ToString();
                }
            }

            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
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
            lblMsg.Text = "";
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
                ;
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
            lblMsg.Text = "";
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
           // lblMsg.Text = "";
            ddlBank.Items.Clear();
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag"}, new string[] { "5"}, "dataset");
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
            lblMsg.Text = "";
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
    protected void ddlUnion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
                FillDistrict();
                FillDCS();
           
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
                if(ds != null && ds.Tables[0].Rows.Count > 0)
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
            string EntryDate = Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd");
            string DCSName = "";
            string CCName = "";
            if(ddlDCS.SelectedValue == "-1")
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
            ds = objdb.ByProcedure("SpKCCDCSWiseInformation", new string[] { "flag",
                                                                             "Office_ID",
                                                                             "EntryDate",
                                                                             "TotalNoofmembersinunion", 
                                                                             "District_ID", 
                                                                             "DCS_ID",
                                                                             "DCS_Name",
                                                                             "CC_Name",
                                                                             "CC_ID",
                                                                             "TotalNoofmembersinDCS",                                                                              
                                                                             "NoofMalesformsSubmittedbyDCS", 
                                                                             "NoofFemalesformsSubmittedbyDCS", 
                                                                             "TotalNoofformsSubmittedbyDCS", 
                                                                             "OutofwhichNoofMalesformsSubmittedforincreaseinlimit", 
                                                                             "OutofwhichNoofFemalesformsSubmittedforincreaseinlimit", 
                                                                             "OutofwhichTotalNoofformsSubmittedforincreaseinlimit", 
                                                                             "MaleApplicationforNewKCChavingLand", 
                                                                             "FemaleApplicationforNewKCChavingLand", 
                                                                             "TotalApplicationforNewKCChavingLand", 
                                                                             "MaleApplicationforNewKCChavingnoLand", 
                                                                             "FemaleApplicationforNewKCChavingnoLand", 
                                                                             "TotalApplicationforNewKCChavingnoLand", 
                                                                             "NoofMaleformsSubmittedbyUniontotheBank", 
                                                                             "NoofFemaleformsSubmittedbyUniontotheBank", 
                                                                             "TotalNoofformsSubmittedbyUniontotheBank", 
                                                                             "NoofCardIssuedByMale", 
                                                                             "NoofCardIssuedByFemale", 
                                                                             "TotalNoofCardIssued", 
                                                                             "AnyOthersByMale", 
                                                                             "AnyOthersByFemale", 
                                                                             "AnyOthersByTotal", 
                                                                             "AnyOthersRemark", 
                                                                             "IsActive", 
                                                                             "UpdatedBy", 
                                                                             }, 
                                                              new string[] {"0",
                                                                             ddlUnion.SelectedValue.ToString(),
                                                                             EntryDate,
                                                                             txtTotalNoofmembersinunion.Text,
                                                                             ddlDistrict.SelectedValue.ToString(),
                                                                             ddlDCS.SelectedValue.ToString(),
                                                                             DCSName,
                                                                             CCName,
                                                                             ddlCC.SelectedValue.ToString(),
                                                                             txtTotalNoofmembersinDCS.Text,                                                                             
                                                                             txtNoofMalesformsSubmittedbyDCS.Text,
                                                                             txtNoofFemalesformsSubmittedbyDCS.Text,
                                                                             txtTotalNoofformsSubmittedbyDCS.Text,
                                                                             txtOutofwhichNoofMalesformsSubmittedforincreaseinlimit.Text,
                                                                             txtOutofwhichNoofFemalesformsSubmittedforincreaseinlimit.Text,
                                                                             txtOutofwhichTotalNoofformsSubmittedforincreaseinlimit.Text,
                                                                             txtMaleApplicationforNewKCChavingLand.Text,
                                                                             txtFemaleApplicationforNewKCChavingLand.Text,
                                                                             txtTotalApplicationforNewKCChavingLand.Text,
                                                                             txtMaleApplicationforNewKCChavingnoLand.Text,
                                                                             txtFemaleApplicationforNewKCChavingnoLand.Text,
                                                                             txtTotalApplicationforNewKCChavingnoLand.Text,                                                                       
                                                                             txtNoofMaleformsSubmittedbyUniontotheBank.Text,
                                                                             txtNoofFemaleformsSubmittedbyUniontotheBank.Text,
                                                                             txtTotalNoofformsSubmittedbyUniontotheBank.Text,
                                                                             txtNoofCardIssuedByMale.Text,
                                                                             txtNoofCardIssuedByFemale.Text,
                                                                             txtTotalNoofCardIssued.Text,
                                                                             txtAnyOthersByMale.Text,
                                                                             txtAnyOthersByFemale.Text,
                                                                             txtAnyOthersByTotal.Text,
                                                                             txtAnyOthersRemark.Text,
                                                                             IsActive,
                                                                             Session["Emp_ID"].ToString()
                                                                             }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                
                string ID = ds.Tables[0].Rows[0]["ID"].ToString();
                foreach (GridViewRow rows in gvBankDetails.Rows)
                {
                    Label Bank_ID = (Label)rows.FindControl("lblBankID");
                    Label Branch_ID = (Label)rows.FindControl("lblBranchID");
                    Label IFSCCode = (Label)rows.FindControl("lblIFSCCode");
                    Label Noofform = (Label)rows.FindControl("lblNoofform");
                    objdb.ByProcedure("SpKCCDCSChildBankInformation", new string[] { "flag", "ID", "Bank_ID", "Branch_ID", "IFSCCode", "NoofForms", "IsActive", "UpdatedBy" }, new string[] { "0", ID, Bank_ID.Text, Branch_ID.Text, IFSCCode.Text,Noofform.Text,"1", Session["Emp_ID"].ToString() }, "dataset");
                }
                
            }
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
        try
        {
            txtDate.Text = "";
            ddlUnion.ClearSelection();
            txtTotalNoofmembersinunion.Text = "";
            ddlDistrict.ClearSelection();
            ddlDCS.ClearSelection();
            txtTotalNoofmembersinDCS.Text = "";
            ddlBank.ClearSelection();
            ddlBranchName.ClearSelection();
            txtIFSC.Text = "";
            txtNoofMalesformsSubmittedbyDCS.Text = "";
            txtNoofFemalesformsSubmittedbyDCS.Text = "";
            txtTotalNoofformsSubmittedbyDCS.Text = "";
            txtOutofwhichNoofMalesformsSubmittedforincreaseinlimit.Text = "";
            txtOutofwhichNoofFemalesformsSubmittedforincreaseinlimit.Text = "";
            txtOutofwhichTotalNoofformsSubmittedforincreaseinlimit.Text = "";
            txtMaleApplicationforNewKCChavingLand.Text = "";
            txtFemaleApplicationforNewKCChavingLand.Text = "";
            txtTotalApplicationforNewKCChavingLand.Text = "";
            txtMaleApplicationforNewKCChavingnoLand.Text = "";
            txtFemaleApplicationforNewKCChavingnoLand.Text = "";
            txtTotalApplicationforNewKCChavingnoLand.Text = "";
            txtNoofMaleformsSubmittedbyUniontotheBank.Text = "";
            txtNoofFemaleformsSubmittedbyUniontotheBank.Text = "";
            txtTotalNoofformsSubmittedbyUniontotheBank.Text = "";
            txtNoofCardIssuedByMale.Text = "";
            txtNoofCardIssuedByFemale.Text = "";
            txtTotalNoofCardIssued.Text = "";
            txtAnyOthersByMale.Text = "";
            txtAnyOthersByFemale.Text = "";
            txtAnyOthersByTotal.Text = "";
            txtAnyOthersRemark.Text = "";
            ddlCC.Items.Clear();
            ddlCC.Items.Insert(0, new ListItem("Select", "0"));
            ddlDCS.Items.Clear();
            ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
            ddlDCS.Items.Clear();
            ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
            FillUnion();
            FillDistrict();         
            FillBank();
            ddlBranchName.Items.Clear();
            ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));            
            CreateBankTable();
            coldcsname.Visible = false;
            colccname.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CreateBankTable()
    {
        DataTable dtBank = new DataTable();
        dtBank.Columns.Add("BankID", typeof(int));
        dtBank.Columns.Add("BankName", typeof(string));
        dtBank.Columns.Add("BranchID", typeof(int));
        dtBank.Columns.Add("BranchName", typeof(string));
        dtBank.Columns.Add("IFSCCode", typeof(string));
        dtBank.Columns.Add("Noofforms", typeof(string));

        ViewState["dtBank"] = dtBank;
        gvBankDetails.DataSource = dtBank;
        gvBankDetails.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        var msg = "";
        if(ddlBank.SelectedIndex == 0)
        {
            msg += "Select Bank";
        }
        if (ddlBranchName.SelectedIndex == 0)
        {
            msg += "Select Branch";
        }
        if (txtIFSC.Text == "")
        {
            msg += "Enter IFSC";
        }
        string Branch = ddlBranchName.SelectedValue.ToString();
        foreach (GridViewRow rows in gvBankDetails.Rows)
        {
            Label BranchID = (Label)rows.FindControl("lblBranchID");
             if (Branch.ToString() == BranchID.Text)
            {
                msg = "Branch Name Already Exists";
                break;
            }

        }
        if(msg == "")
        {
            DataTable dtBank = (DataTable)ViewState["dtBank"];
            dtBank.Rows.Add(ddlBank.SelectedValue.ToString(), ddlBank.SelectedItem.Text, ddlBranchName.SelectedValue.ToString(), ddlBranchName.SelectedItem.Text, txtIFSC.Text, txtNoofForm.Text);
            ViewState["dtBank"] = dtBank;
            gvBankDetails.DataSource = dtBank;
            gvBankDetails.DataBind();
            ddlBank.ClearSelection();
            FillBranch();        
            txtNoofForm.Text = "";
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }
       
    }
    protected void gvBankDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if(e.CommandName == "Delet")
            {
                string BranchID = e.CommandArgument.ToString();
                DataTable dtBank = (DataTable)ViewState["dtBank"];
                int Count = dtBank.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                     DataRow dr = dtBank.Rows[i];
                     if (dr["BranchID"].ToString() == BranchID.ToString())
                     {
                         dr.Delete();
                         break;
                     }
                         
                }
                dtBank.AcceptChanges();
                ViewState["dtBank"] = dtBank;
                gvBankDetails.DataSource = dtBank;
                gvBankDetails.DataBind();
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
            if(ddlDCS.SelectedValue == "-1")
            {
                coldcsname.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
}