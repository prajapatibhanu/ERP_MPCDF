using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;


public partial class mis_Common_MCURegistration : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                GetOfficeType();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
    #region=======================user defined function========================

    private void Clear()
    {
        ddlOfficeType.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        if (ddlDistrict.SelectedValue != "" || ddlDistrict.SelectedValue != "0")
        {
            ddlDistrict.SelectedIndex = 0;
        }

        txtOfficeName.Text = string.Empty;
        txtOffice_Code.Text = string.Empty;
        txtOfficeContactNo.Text = string.Empty;
        txtOffice_Email.Text = string.Empty;
        txtOfficeAddress.Text = string.Empty;
        txtOfficePincode.Text = string.Empty;
        txtGstNumber.Text = string.Empty;
        txtPanNumber.Text = string.Empty;
        ddlUnit.SelectedIndex = 0;
        txtCapacity.Text = string.Empty;

        if (ddlBank.SelectedValue != "0")
        {
            ddlBank.SelectedIndex = 0;
            ddlBranchName.SelectedIndex = 0;
        }
        txtIFSCCode.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;

        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void GetOfficeDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag", "CreatedBy" },
                    new string[] { "1", objdb.createdBy() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetOfficeWiselabelset()
    {
        try
        {
            txtOfficeName.Text = ddlOfficeType.SelectedItem.Text;
            lblOfficeName.Text = ViewState["OfficeTypeCode"].ToString() + " Name";
            rfvofficename.ErrorMessage = "Enter" + ViewState["OfficeTypeCode"].ToString() + " Name";
            rfvofficename.Text = "<i class='fa fa-exclamation-circle' title='Enter " + ViewState["OfficeTypeCode"].ToString() + "Name !'></i>";
            lblOfficeCode.Text = ViewState["OfficeTypeCode"].ToString() + " Code";
            rfvrfvofficcode.ErrorMessage = "Enter " + ViewState["OfficeTypeCode"].ToString() + " Code";
            rfvrfvofficcode.Text = "<i class='fa fa-exclamation-circle' title='Enter " + ViewState["OfficeTypeCode"].ToString() + " Code !'></i>";
            revofficecode.ErrorMessage = "Only Alphanumeric Allow in" + ViewState["OfficeTypeCode"].ToString() + " Code";
            revofficecode.Text = "<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in " + ViewState["OfficeTypeCode"].ToString() + " Code !'></i>";
            lblOfficeContactNo.Text = ViewState["OfficeTypeCode"].ToString() + " Contact No.";
            rfvofficecontactno.ErrorMessage = "Enter " + ViewState["OfficeTypeCode"].ToString() + " Contact No.";
            rfvofficecontactno.Text = "<i class='fa fa-exclamation-circle' title='Enter " + ViewState["OfficeTypeCode"].ToString() + " Contact No. !'></i>";
            lblOfficeEmail.Text = ViewState["OfficeTypeCode"].ToString() + " Email";
            rfvOfficeEmail.ErrorMessage = "Enter " + ViewState["OfficeTypeCode"].ToString() + " Email";
            rfvOfficeEmail.Text = "<i class='fa fa-exclamation-circle' title='Enter " + ViewState["OfficeTypeCode"].ToString() + " Email !'></i>";
            lblOfficeAddress.Text = ViewState["OfficeTypeCode"].ToString() + " Address";
            rfvofficeaddress.ErrorMessage = "Enter " + ViewState["OfficeTypeCode"].ToString() + " Address";
            rfvofficeaddress.Text = "<i class='fa fa-exclamation-circle' title='Enter " + ViewState["OfficeTypeCode"].ToString() + " Address !'></i>";
            revofficeaddress.ErrorMessage = "Alphanumeric ,space and some special symbols like .,/-: allow";
            revofficeaddress.Text = "<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like .,/-: allow '></i>";
            lblOfficePincode.Text = ViewState["OfficeTypeCode"].ToString() + " Pincode";
            rfvofficepincode.ErrorMessage = "Enter " + ViewState["OfficeTypeCode"].ToString() + " Pincode";
            rfvofficepincode.Text = "<i class='fa fa-exclamation-circle' title='Enter " + ViewState["OfficeTypeCode"].ToString() + " Pincode !'></i>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDropdownOfDivisionOrDistrict()
    {
        try
        {
            if (ddlOfficeType.SelectedValue == "1")
            {
                spanDivision.Visible = false;
                spanDistrict.Visible = false;
                txtOfficeName.Enabled = false;
                spanUnit.Visible = false;
                spanCapacity.Visible = false;
                GetOfficeWiselabelset();
                txtCapacity.Text = "0.00";
            }
            else if (ddlOfficeType.SelectedValue == "2")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                txtOfficeName.Enabled = false;
                spanUnit.Visible = false;
                spanCapacity.Visible = false;
                GetOfficeWiselabelset();
                txtCapacity.Text = "0.00";

            }
            //if (ddlOfficeType.SelectedValue == "3")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            //else if (ddlOfficeType.SelectedValue == "4")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            //else if (ddlOfficeType.SelectedValue == "5")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();

            //}
            //else if (ddlOfficeType.SelectedValue == "6")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            //else if (ddlOfficeType.SelectedValue == "7")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            //else if (ddlOfficeType.SelectedValue == "8")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            else
            {
                spanDivision.Visible = false;
                spanDistrict.Visible = false;
                txtOfficeName.Text = string.Empty;
                txtOfficeName.Enabled = false;
                spanUnit.Visible = false;
                spanCapacity.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetDivision()
    {
        try
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_ID";
            ddlDivision.DataSource = objdb.ByProcedure("SpAdminDivision",
                    new string[] { "flag", "State_ID" },
                    new string[] { "7", "12" }, "dataset");
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDistrict()
    {
        try
        {
            if (ddlDivision.SelectedValue != "0")
            {
                ddlDistrict.DataSource = objdb.ByProcedure("SpAdminDistrict",
                           new string[] { "flag", "Division_ID" },
                           new string[] { "9", ddlDivision.SelectedValue }, "dataset");
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";

                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                ddlDistrict.Focus();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Division Name");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetOfficeType()
    {
        try
        {
            DataSet DsNew = objdb.ByProcedure("Sp_CommonTables",
                           new string[] { "flag" },
                           new string[] { "4" }, "dataset");
            if (DsNew.Tables[1] != null)
            {
                if (DsNew.Tables[1].Rows.Count > 0)
                {
                    ddlOfficeType.DataSource = DsNew.Tables[1];
                    ddlOfficeType.DataTextField = "OfficeTypeName";
                    ddlOfficeType.DataValueField = "OfficeType_ID";

                    ddlOfficeType.DataBind();
                    ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
                   // ddlOfficeType.Focus();
                    
                
                }
            }
          

            if (objdb.OfficeType_ID() == objdb.GetHOType_Id().ToString())
            {
                for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType.Items[i].Value != objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlOfficeType.Items.RemoveAt(i);
                    }
                }
            }
            else if (objdb.OfficeType_ID() == objdb.GetDSType_Id().ToString())
            {
                for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType.Items[i].Value == objdb.GetDCSType_Id().ToString() || ddlOfficeType.Items[i].Value == objdb.GetHOType_Id().ToString() || ddlOfficeType.Items[i].Value == objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlOfficeType.Items.RemoveAt(i);
                    }
                }
            }
            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));

            // if(objdb.Office_ID)
            //ddlOfficeType.Items.Remove(ddlOfficeType.Items.FindByValue("1"));
            //ddlOfficeType.Items.Remove(ddlOfficeType.Items.FindByValue(objdb.OfficeType_ID()));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetUnit()
    {
        try
        {
            ddlUnit.DataSource = objddl.UnitFill();
            ddlUnit.DataTextField = "UQCCode";
            ddlUnit.DataValueField = "Unit_id";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetBank()
    {
        try
        {
            ddlBank.DataSource = objdb.ByProcedure("sp_tblPUBankMaster",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "Bank_id";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetBranchByBank()
    {
        try
        {
            ddlBranchName.DataSource = objdb.ByProcedure("sp_tblPUBankBranchMaster",
                            new string[] { "flag", "Bank_id" },
                            new string[] { "6", ddlBank.SelectedValue }, "dataset");
            ddlBranchName.DataTextField = "BranchName";
            ddlBranchName.DataValueField = "Branch_id";
            ddlBranchName.DataBind();
            ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetIFSCByBranch()
    {
        try
        {
            if (ds != null) { ds.Clear(); }
            ds = objdb.ByProcedure("sp_tblPUBankBranchMaster",
                     new string[] { "flag", "Branch_id" },
                     new string[] { "1", ddlBranchName.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "ooops!", "IFSC Code not found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateOfficeReg()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("SpAdminOffice",
                            new string[] { "flag", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo"
                                , "Office_Email", "Division_ID", "District_ID", "Office_Address",
                                "Office_Pincode","Office_Gst","Office_Pan","Branch_id","BankAccountNo", 
                                "Capacity","Unit_id","CreatedBy", "CreatedBy_IP","Office_Parant_ID" },
                            new string[] { "2", ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim()
                                ,txtOffice_Code.Text.Trim() ,txtOfficeContactNo.Text.Trim(),txtOffice_Email.Text.Trim()
                                ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue,txtOfficeAddress.Text.Trim()
                               ,txtOfficePincode.Text.Trim(),txtGstNumber.Text.Trim(),txtPanNumber.Text.Trim()
                               ,ddlBranchName.SelectedValue,txtBankAccountNo.Text.Trim()
                                , txtCapacity.Text.Trim(),ddlUnit.SelectedValue
                                , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),objdb.Office_ID() }, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetOfficeDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("SpAdminOffice",
                            new string[] { "flag","Office_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo"
                                , "Office_Email", "Division_ID", "District_ID", "Office_Address",
                                "Office_Pincode","Office_Gst","Office_Pan","Branch_id","BankAccountNo", 
                                "Capacity","Unit_id","CreatedBy", "CreatedBy_IP","PageName","Remark" },
                            new string[] { "3",ViewState["rowid"].ToString(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(),txtOffice_Code.Text.Trim()
                                ,txtOfficeContactNo.Text.Trim(),txtOffice_Email.Text,ddlDivision.SelectedValue,ddlDistrict.SelectedValue
                               ,txtOfficeAddress.Text,txtOfficePincode.Text.Trim(),txtGstNumber.Text.Trim(),txtPanNumber.Text.Trim()
                               ,ddlBranchName.SelectedValue,txtBankAccountNo.Text.Trim()
                                , txtCapacity.Text.Trim() ,ddlUnit.SelectedValue
                                , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Office Registration Details Updated"}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetOfficeDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Bank Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    private void GetOfficeTypeCode()
    {
        try
        {
            if (ds != null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("SpAdminOfficeType",
                           new string[] { "flag", "OfficeType_ID" },
                           new string[] { "1", ddlOfficeType.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["OfficeTypeCode"] = ds.Tables[0].Rows[0]["OfficeTypeCode"];
            }
            ds.Clear();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion====================================end of user defined function

    #region=====================Init event for controls===========================

    protected void ddlOfficeType_Init(object sender, EventArgs e)
    {
        GetOfficeType();
    }
    protected void ddlDivision_Init(object sender, EventArgs e)
    {
        GetDivision();
    }
    protected void ddlUnit_Init(object sender, EventArgs e)
    {
        GetUnit();
    }  
    protected void ddlBank_Init1(object sender, EventArgs e)
    {
        GetBank();
    }
    #endregion=====================end of control======================

    #region=============== changed event for controls =================

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDistrict();
    }
    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetOfficeTypeCode();
        GetDropdownOfDivisionOrDistrict();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (ddlDistrict.SelectedValue != "0")
        {
            txtOfficeName.Text = ddlDistrict.SelectedItem.Text + "-" + ddlOfficeType.SelectedItem.Text;
        }
    }

    protected void ddlBank_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetBranchByBank();
    }
    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIFSCByBranch();
    }

    #endregion============ end of changed event for controls===========

    #region============ button click event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateOfficeReg();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblOfficeType_ID = (Label)row.FindControl("lblOfficeType_ID");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblOfficeName = (Label)row.FindControl("lblOfficeName");
                    Label lblOffice_Code = (Label)row.FindControl("lblOffice_Code");
                    Label lblOffice_ContactNo = (Label)row.FindControl("lblOffice_ContactNo");
                    Label lblOffice_Email = (Label)row.FindControl("lblOffice_Email");
                    Label lblOffice_Address = (Label)row.FindControl("lblOffice_Address");
                    Label lblOffice_Pincode = (Label)row.FindControl("lblOffice_Pincode");
                    Label lblOfficeGst = (Label)row.FindControl("lblOfficeGst");
                    Label lblOfficePan = (Label)row.FindControl("lblOfficePan");

                    Label lblCapacity = (Label)row.FindControl("lblCapacity");
                    Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                    Label lblBranch_id = (Label)row.FindControl("lblBranch_id");
                    Label lblIFSC = (Label)row.FindControl("lblIFSC");
                    Label lblBankAccountNo = (Label)row.FindControl("lblBankAccountNo");

                    ddlOfficeType.SelectedValue = lblOfficeType_ID.Text;
                    GetOfficeTypeCode();
                    GetDropdownOfDivisionOrDistrict();
                    if (lblDivision_ID.Text != "")
                    {
                        ddlDivision.SelectedValue = lblDivision_ID.Text;
                        GetDistrict();
                        ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    }

                    txtOfficeName.Text = lblOfficeName.Text;
                    txtOffice_Code.Text = lblOffice_Code.Text;
                    txtOfficeContactNo.Text = lblOffice_ContactNo.Text;
                    txtOffice_Email.Text = lblOffice_Email.Text;
                    txtOfficeAddress.Text = lblOffice_Address.Text;
                    txtOfficePincode.Text = lblOffice_Pincode.Text;
                    txtGstNumber.Text = lblOfficeGst.Text;
                    txtPanNumber.Text = lblOfficePan.Text;
                    if (lblUnit_id.Text != "")
                    {
                        ddlUnit.SelectedValue = lblUnit_id.Text;
                        txtCapacity.Text = lblCapacity.Text;
                    }

                    if (lblBranch_id.Text != "0")
                    {
                        ddlBank.SelectedValue = lblBank_id.Text;
                        GetBranchByBank();
                        ddlBranchName.SelectedValue = lblBranch_id.Text;
                        txtIFSCCode.Text = lblIFSC.Text;
                        txtBankAccountNo.Text = lblBankAccountNo.Text;
                    }


                    ViewState["rowid"] = e.CommandArgument;

                    btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("SpAdminOffice",
                                new string[] { "flag", "Office_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Office Registration Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetOfficeDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    #endregion=============end of button click funciton==================

    protected void ddlUnit_Init1(object sender, EventArgs e)
    {
        GetUnit();
    }

}