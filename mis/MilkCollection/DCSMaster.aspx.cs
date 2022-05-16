using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;

public partial class mis_Common_DCSMaster : System.Web.UI.Page
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
                lblMsg.Text = "";
                GetDSCDetails();
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
        RblContainerTypeName.ClearSelection();
        ddlMilkCollectionUnit.SelectedIndex = 0;
        snapName.Visible = false;
        ddlDivision.SelectedIndex = 0;
        if (ddlDistrict.SelectedValue != "" || ddlDistrict.SelectedValue != "0")
        {
            ddlDistrict.SelectedIndex = 0;
        }
        txtDCSContactNo.Text = string.Empty;
        txtDCSName.Text = string.Empty;
        txtDCS_Code.Text = string.Empty;
        txtSecretaryPerson.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtDCS_Email.Text = string.Empty;
        txtDCSAddress.Text = string.Empty;
        txtDCSPincode.Text = string.Empty;
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
    private void GetDSCDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("Sp_tblDCSMaster",
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
            lblMsg.Text = "";
            lblName.Text = ViewState["OfficeTypeName"].ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDropdownOfMilkCollectionUnit()
    {
        try
        {
            lblMsg.Text = "";
          
            if (ddlMilkCollectionUnit.SelectedValue == "3")
            {
                snapName.Visible = true;
                spanUnit.Visible = true;
                spanCapacity.Visible = true;
                GetOfficeWiselabelset();
            }
            else if (ddlMilkCollectionUnit.SelectedValue == "4")
            {
                snapName.Visible = true;
                spanUnit.Visible = true;
                spanCapacity.Visible = true;
                GetOfficeWiselabelset();
            }
            else if (ddlMilkCollectionUnit.SelectedValue == "5")
            {
                snapName.Visible = true;
                spanUnit.Visible = true;
                spanCapacity.Visible = true;
                GetOfficeWiselabelset();

            }
            else if (ddlMilkCollectionUnit.SelectedValue == "2")
            {
                snapName.Visible = true;
                spanUnit.Visible = true;
                spanCapacity.Visible = true;
                GetOfficeWiselabelset();
            }
            else
            {
                snapName.Visible = false;
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
            lblMsg.Text = "";
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
    private void GetMilkCollectionUnitName()
    {
        try
        {
            lblMsg.Text = "";
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ddlMilkColUnitName.DataSource = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "OfficeType_ID" },
                           new string[] { "5", ddlMilkCollectionUnit.SelectedValue }, "dataset");
                ddlMilkColUnitName.DataTextField = "Office_Name";
                ddlMilkColUnitName.DataValueField = "OfficeType_ID";

                ddlMilkColUnitName.DataBind();
                ddlMilkColUnitName.Items.Insert(0, new ListItem("Select", "0"));
                ddlMilkColUnitName.Focus();
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
    private void GetMilkCollectionUnit()
    {
        try
        {
            lblMsg.Text = "";
            //ddlMilkCollectionUnit.DataSource = objddl.OfficeTypeFill();
            //ddlMilkCollectionUnit.DataTextField = "OfficeTypeName";
            //ddlMilkCollectionUnit.DataValueField = "OfficeType_ID";
            //ddlMilkCollectionUnit.DataBind();
            DataSet DsNew = objdb.ByProcedure("Sp_CommonTables",
                          new string[] { "flag" },
                          new string[] { "4" }, "dataset");
            if (DsNew.Tables[1] != null)
            {
                if (DsNew.Tables[1].Rows.Count > 0)
                {
                    ddlMilkCollectionUnit.DataSource = DsNew.Tables[1];
                    ddlMilkCollectionUnit.DataTextField = "OfficeTypeName";
                    ddlMilkCollectionUnit.DataValueField = "OfficeType_ID";

                    ddlMilkCollectionUnit.DataBind();
                    ddlMilkCollectionUnit.Items.Insert(0, new ListItem("Select", "0"));
                    // ddlOfficeType.Focus();


                }
            }

            if (objdb.OfficeType_ID() == objdb.GetHOType_Id().ToString())
            {
                for (int i = ddlMilkCollectionUnit.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlMilkCollectionUnit.Items[i].Value != objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlMilkCollectionUnit.Items.RemoveAt(i);
                    }
                }

            }
            else if (objdb.OfficeType_ID() == objdb.GetDSType_Id().ToString())
            {
                for (int i = ddlMilkCollectionUnit.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlMilkCollectionUnit.Items[i].Value == objdb.GetDCSType_Id().ToString() || ddlMilkCollectionUnit.Items[i].Value == objdb.GetHOType_Id().ToString() || ddlMilkCollectionUnit.Items[i].Value == objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlMilkCollectionUnit.Items.RemoveAt(i);
                    }
                }
            }
            ddlMilkCollectionUnit.Items.Insert(0, new ListItem("Select", "0"));
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
    private void GetOfficeTypeCode()
    {
        try
        {
            if (ds != null) { ds.Clear(); }
            ds = objdb.ByProcedure("SpAdminOfficeType",
                     new string[] { "flag", "OfficeType_ID" },
                     new string[] { "1", ddlMilkCollectionUnit.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["OfficeTypeName"] = ds.Tables[0].Rows[0]["OfficeTypeName"];
            }
            ds.Clear();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateDCSReg()
    {
        lblMsg.Text = "";
        string DCS_IsActive = "1";
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        ds = objdb.ByProcedure("Sp_tblDCSMaster",
                            new string[] { "flag", "OfficeType_ID", "OfficeType_Code", "MilkCollectionUnit_ID", "MilkCollectionUnit_Name", "DCS_Name", "DCS_Code", 
		"Secretary_Person","Contact_Person", "Division_ID", "District_ID", "DCSContact_No", "DCS_Email", "DCS_Address", "DCS_Pincode", 
		"GST_Number", "PAN_Number", "Unit_id", "Capacity", "Branch_id", "BankAccountNo", "DCS_IsActive", "CreatedBy", "CreatedBy_IP" },
                            new string[] { "2", RblContainerTypeName.SelectedValue,RblContainerTypeName.SelectedItem.Text, ddlMilkCollectionUnit.SelectedValue,ddlMilkColUnitName.SelectedItem.Text, txtDCSName.Text
                                ,txtDCS_Code.Text , txtSecretaryPerson.Text, txtContactPerson.Text, ddlDivision.SelectedValue,ddlDistrict.SelectedValue,txtDCSContactNo.Text, txtDCS_Email.Text
                                ,txtDCSAddress.Text , txtDCSPincode.Text, txtGstNumber.Text, txtPanNumber.Text, ddlUnit.SelectedValue, txtCapacity.Text,
                                ddlBank.SelectedValue, txtBankAccountNo.Text,DCS_IsActive, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");
                         if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                             Clear();
                           GetDSCDetails();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            GetDSCDetails();
                        }
                        ds.Clear();
                        }

                    else if (btnSubmit.Text == "Update" && ViewState["DCSMaster_ID"].ToString() != "0")
                    {
                        ds = objdb.ByProcedure("Sp_tblDCSMaster",
                             new string[] { "flag","DCSMaster_ID", "OfficeType_ID", "OfficeType_Code", "MilkCollectionUnit_ID", "MilkCollectionUnit_Name", "DCS_Name", "DCS_Code", 
		"Secretary_Person","Contact_Person", "Division_ID", "District_ID", "DCSContact_No", "DCS_Email", "DCS_Address", "DCS_Pincode", 
		"GST_Number", "PAN_Number", "Unit_id", "Capacity", "Branch_id", "BankAccountNo","DCS_IsActive", "CreatedBy", "CreatedBy_IP","PageName","Remark" },
                             new string[] { "3",ViewState["rowid"].ToString(), RblContainerTypeName.SelectedValue,RblContainerTypeName.SelectedItem.Text, ddlMilkCollectionUnit.SelectedValue,ddlMilkColUnitName.SelectedItem.Text, txtDCSName.Text
                                ,txtDCS_Code.Text , txtSecretaryPerson.Text, txtContactPerson.Text, ddlDivision.SelectedValue,ddlDistrict.SelectedValue,txtDCSContactNo.Text, txtDCS_Email.Text
                                ,txtDCSAddress.Text , txtDCSPincode.Text, txtGstNumber.Text, txtPanNumber.Text, ddlUnit.SelectedValue, txtCapacity.Text,
                                ddlBranchName.SelectedValue, txtBankAccountNo.Text, DCS_IsActive,  objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Office Registration Details Updated"}, "TableSave");

                       if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                             Clear();
                           GetDSCDetails();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            GetDSCDetails();
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

    #endregion====================================end of user defined function

    #region=============== changed event for controls =================
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetOfficeTypeCode();
        GetDropdownOfMilkCollectionUnit();
        GetMilkCollectionUnitName();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDistrict();
    }
    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIFSCByBranch();
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchByBank();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblDCSMaster_ID = (Label)row.FindControl("lblDCSMaster_ID");
                    Label lblOfficeType_ID = (Label)row.FindControl("lblOfficeType_ID");
                    Label lblMilkCollectionUnit_ID = (Label)row.FindControl("lblMilkCollectionUnit_ID");
                    Label lblUnit_Name = (Label)row.FindControl("lblUnit_Name");
                    Label lblDCS_Name = (Label)row.FindControl("lblDCS_Name");
                    Label lblSecretary_Person = (Label)row.FindControl("lblSecretary_Person");
                    Label lblContact_Person = (Label)row.FindControl("lblContact_Person");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblDCSContact_No = (Label)row.FindControl("lblDCSContact_No");
                    Label lblDCS_Email = (Label)row.FindControl("lblDCS_Email");
                    Label lblDCS_Address = (Label)row.FindControl("lblDCS_Address");
                    Label lblDCS_Pincode = (Label)row.FindControl("lblDCS_Pincode");
                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                    Label lblBranch_id = (Label)row.FindControl("lblBranch_id");
                    Label lblBranchName = (Label)row.FindControl("lblBranchName");
                    Label lblIFSC = (Label)row.FindControl("lblIFSC");
                    Label lblBankAccountNo = (Label)row.FindControl("lblBankAccountNo");
                    Label lblCapacity = (Label)row.FindControl("lblCapacity");
                    Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
                    Label lblDCS_Code = (Label)row.FindControl("lblDCS_Code");
                    Label lblOfficeTypeName = (Label)row.FindControl("lblOfficeTypeName");
                    Label lblGST_Number = (Label)row.FindControl("lblGST_Number");
                    Label lblPAN_Number = (Label)row.FindControl("lblPAN_Number");

                    RblContainerTypeName.SelectedValue = lblOfficeType_ID.Text;
                    ddlMilkCollectionUnit.SelectedValue = lblMilkCollectionUnit_ID.Text;
                    GetMilkCollectionUnitName();
                    GetDropdownOfMilkCollectionUnit();
                    GetOfficeWiselabelset();
                    lblName.Text = lblOfficeTypeName.Text;
                    ddlMilkColUnitName.Items.FindByText(lblUnit_Name.Text).Selected = true;
                    txtDCSName.Text = lblDCS_Name.Text;
                    txtDCS_Code.Text = lblDCS_Code.Text;
                    txtSecretaryPerson.Text = lblSecretary_Person.Text;
                    txtContactPerson.Text = lblContact_Person.Text;
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    txtDCSContactNo.Text = lblDCSContact_No.Text;
                    txtDCS_Email.Text = lblDCS_Email.Text;
                    txtDCSAddress.Text = lblDCS_Address.Text;
                    txtDCSPincode.Text = lblDCS_Pincode.Text;
                    txtGstNumber.Text = lblGST_Number.Text;
                    txtPanNumber.Text = lblPAN_Number.Text;
                    txtCapacity.Text = lblCapacity.Text;
                    ddlUnit.SelectedValue = lblUnit_id.Text;
                    ddlBank.SelectedValue = lblBank_id.Text;
                    GetBranchByBank();
                    ddlBranchName.SelectedValue = lblBranch_id.Text;
                    txtIFSCCode.Text = lblIFSC.Text;


                    txtBankAccountNo.Text = lblBankAccountNo.Text;
                    ViewState["DCSMaster_ID"] = lblDCSMaster_ID.Text;
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
                    ds = objdb.ByProcedure("Sp_tblDCSMaster",
                                new string[] { "flag", "DCSMaster_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Dept. Designation Mapping Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetDSCDetails();
                        Clear();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================
    protected void ddlMilkCollectionUnit_Init(object sender, EventArgs e)
    {
        GetMilkCollectionUnit();
    }
    protected void ddlDivision_Init(object sender, EventArgs e)
    {
        GetDivision();
    }
    protected void ddlBank_Init(object sender, EventArgs e)
    {
        GetBank();
    }
    protected void ddlUnit_Init(object sender, EventArgs e)
    {
        GetUnit();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateDCSReg();
    }
    #endregion=============end of button click funciton==================
}