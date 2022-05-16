using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;



public partial class mis_Common_OfficeRegistration : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds2;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "alert('Record Saved Successfully');", true);
                    }
                }
                if (Session["IsUpdate"] != null)
                {
                    if ((Boolean)Session["IsUpdate"] == true)
                    {
                        Session["IsUpdate"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "alert('Record Updated Successfully');", true);
                    }
                }
                GetOfficeDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtRegistrationDate.Attributes.Add("readonly", "readonly");
                MDPFields.Visible = false;
                DCSFileds.Visible = false;
                DivMDPOrCC.Visible = false;
                pnldcs.Visible = false;
                pnlDataDiv.Visible = false;
                ReportingUnit.Visible = false;
                FillSupplyUnit();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillSupplyUnit()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("ProcCommTablesFill",
                            new string[] { "type" },
                            new string[] { "6" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplyUnit.DataTextField = "UnitName";
                ddlSupplyUnit.DataValueField = "Unit_id";
                ddlSupplyUnit.DataSource = ds;
                ddlSupplyUnit.DataBind();
                ddlSupplyUnit.Items.Insert(0, new ListItem("Select Supply Unit", "0"));
                ddlSupplyUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    #region=======================user defined function========================

    private void Clear()
    {
		ddlOfficeType.Enabled = true;
        ddlDivision.SelectedIndex = 0;
        txtOfficeName.Text = string.Empty;
        txtOffice_Code.Text = string.Empty;
        txtOfficeContactNo.Text = string.Empty;
        txtOffice_Email.Text = string.Empty;
        txtRegistrationNo.Text = "";
        txtRegistrationDate.Text = "";
        txtScheme.Text = "";
        txtElectronicEquip.Text = "";
        ddlcashless.SelectedIndex = 0;
        ddlWomenDCS.SelectedIndex = 0;
        DdlAICenter.SelectedIndex = 0;
        txtAHWMobile.Text = "";
        txtAhwName.Text = "";
        txtOfficerName.Text = "";
        txtofficermobileNo.Text = "";
        ddlBMCAvail.SelectedIndex = 0;
        txtBranchName.Text = "";
        txtCapacity.Text = string.Empty;
        if (ddlBank.SelectedValue != "0")
        {
            ddlBank.SelectedIndex = 0;
            txtBranchName.Text = "";
        }
        txtIFSCCode.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        txtElectronicEquip.Text = "";
        txtRegisteredProducers.Text = "";
        txtRegistrationNo.Text = "";
        txtRegistrationDate.Text = "";
        txtCapacity.Text = "";
        ddlSupplyUnit.ClearSelection();
        txtAddress.Text = "";
        txtContactPerson.Text = "";
        txtNoOfEmployees.Text = "";
        txtParmanent_Concontractual.Text = "";
        txtSanctionedMPEB.Text = "";
        txtActualMPEB.Text = "";
        txtProcessingCapacity.Text = "";
        ddlManufacturing.ClearSelection();
        txtProductionName.Text = "";
        txtProductionCapacity.Text = "";
        txtFSSI.Text = "";
        txtFactoryLicence.Text = "";
        txtBoilerLicence.Text = "";
        txtScheme.Text = "";
        ddlWomenDCS.ClearSelection();
        ddlBMCAvail.ClearSelection();
        txtAHWMobile.Text = "";
        txtAhwName.Text = "";
        txtAssetDetail.Text = "";
    }
    private void GetOfficeDetails()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag", "CreatedBy", "OfficeType_ID" },
                    new string[] { "1", objdb.createdBy(), ddlOfficeType.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
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
            if (ddlOfficeType.SelectedValue == "3")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                //txtOfficeName.Enabled = false;
                //spanUnit.Visible = true;
                spanCapacity.Visible = true;
                //GetOfficeWiselabelset();
            }
            else if (ddlOfficeType.SelectedValue == "4")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                //txtOfficeName.Enabled = false;
                //spanUnit.Visible = true;
                spanCapacity.Visible = true;
                //GetOfficeWiselabelset();
            }
            else if (ddlOfficeType.SelectedValue == "5")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                // txtOfficeName.Enabled = false;
                //spanUnit.Visible = true;
                spanCapacity.Visible = true;
                //GetOfficeWiselabelset();

            }
            else if (ddlOfficeType.SelectedValue == "6")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                //txtOfficeName.Enabled = false;
                // spanUnit.Visible = true;
                spanCapacity.Visible = true;
                //GetOfficeWiselabelset();
            }
            else if (ddlOfficeType.SelectedValue == "2")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                //txtOfficeName.Enabled = false;
                // spanUnit.Visible = false;
                spanCapacity.Visible = false;
                // GetOfficeWiselabelset();
                txtCapacity.Text = "0.00";

            }
            //else if (ddlOfficeType.SelectedValue == "7")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    //txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            //else if (ddlOfficeType.SelectedValue == "8")
            //{
            //    spanDivision.Visible = true;
            //    spanDistrict.Visible = true;
            //    //txtOfficeName.Enabled = false;
            //    spanUnit.Visible = true;
            //    spanCapacity.Visible = true;
            //    GetOfficeWiselabelset();
            //}
            else
            {
                spanDivision.Visible = false;
                spanDistrict.Visible = false;
                txtOfficeName.Text = string.Empty;
                //txtOfficeName.Enabled = false;
                //spanUnit.Visible = false;
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

            ds = objdb.ByProcedure("SpAdminDivision",
                    new string[] { "flag", "State_ID" },
                    new string[] { "7", "12" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_ID";
                ddlDivision.DataSource = ds;
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                ddlBlock_ID.Items.Insert(0, new ListItem("Select", "0"));
                ddlAssembly_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                ddlBlock_ID.Items.Insert(0, new ListItem("Select", "0"));
                ddlAssembly_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
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
                ds = objdb.ByProcedure("SpAdminDistrict",
                           new string[] { "flag", "Division_ID" },
                           new string[] { "9", ddlDivision.SelectedValue }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataSource = ds;
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                    ddlDistrict.Focus();
                }
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
            ddlOfficeType.DataSource = objddl.OfficeTypeFill();
            ddlOfficeType.DataTextField = "OfficeType_Title";
            ddlOfficeType.DataValueField = "OfficeType_ID";
            ddlOfficeType.DataBind();

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
                    if (ddlOfficeType.Items[i].Value == objdb.GetHOType_Id().ToString() || ddlOfficeType.Items[i].Value == objdb.GetDSType_Id().ToString())// for show only dugdh sangh
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
            //ddlBranchName.DataSource = objdb.ByProcedure("sp_tblPUBankBranchMaster",
            //                new string[] { "flag", "Bank_id" },
            //                new string[] { "6", ddlBank.SelectedValue }, "dataset");
            //ddlBranchName.DataTextField = "BranchName";
            //ddlBranchName.DataValueField = "Branch_id";
            //ddlBranchName.DataBind();
            //ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
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
            //    if (ds != null) { ds.Clear(); }
            //    ds = objdb.ByProcedure("sp_tblPUBankBranchMaster",
            //             new string[] { "flag", "Branch_id" },
            //             new string[] { "1", ddlBranchName.SelectedValue }, "dataset");

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
            //    }
            //    else
            //    {
            //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "ooops!", "IFSC Code not found.");
            //    }
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
                        if (ddlOfficeType.SelectedValue == "3") //MDP
                        {
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_Parant_ID","OfficeType_ID","Office_Name","Office_Code","Office_ContactNo","Office_Email","Division_ID","District_ID","Block_ID",
                                "Capacity","ProcessingCapacity","ContactPerson","NoOfEmployees","NoOfParmanent_Concontractual","SanctionedMPEB","ActualMPEB","ProductionCapacity"
                                ,"IsManufacturingUnit","ProductionName","FSSILicenceNo","FactoryLicenceNo","BoilerLicenceNo","OfficerName","OfficerMobileNo","Bank","Branch","IFSC","BankAccountNo","CreatedBy","CreatedBy_IP", "Accounting" },
                       new string[] { "2", objdb.Office_ID(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim() ,txtOffice_Code.Text.Trim() ,txtOfficeContactNo.Text.Trim(),txtOffice_Email.Text.Trim() ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_ID.SelectedValue,txtCapacity.Text.Trim(), txtProcessingCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtNoOfEmployees.Text.Trim(), txtParmanent_Concontractual.Text.Trim(), txtSanctionedMPEB.Text.Trim(), txtActualMPEB.Text.Trim(), txtProductionCapacity.Text.Trim(), 
                            ddlManufacturing.SelectedValue.ToString(), txtProductionName.Text.Trim(), txtFSSI.Text.Trim(), txtFactoryLicence.Text.Trim(), txtBoilerLicence.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), ddlBank.SelectedValue.ToString(),txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value}, "TableSave");
                        }
                        else if (ddlOfficeType.SelectedValue == "4") //CC
                        {
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_Parant_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo", "Office_Email", "Division_ID", "District_ID", "Block_ID", "Capacity", "ContactPerson", "NoOfEmployees", "NoOfParmanent_Concontractual", "SanctionedMPEB ", "ActualMPEB", "OfficerName", "OfficerMobileNo", "Bank", "Branch", "IFSC", "BankAccountNo", "CreatedBy", "CreatedBy_IP", "Accounting" },
                                 new string[] { "2", objdb.Office_ID(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOffice_Code.Text.Trim(), txtOfficeContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ddlBlock_ID.SelectedValue, txtCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtNoOfEmployees.Text.Trim(), txtParmanent_Concontractual.Text.Trim(), txtSanctionedMPEB.Text.Trim(), txtActualMPEB.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), ddlBank.SelectedValue.ToString(), txtBranchName.Text.Trim(), txtIFSCCode.Text.Trim(), txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value }, "TableSave");
                        }
                        else if (ddlOfficeType.SelectedValue == "5") // BMC
                        {
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_Parant_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo", "Office_Email", "Division_ID", "District_ID", "Block_ID", "ElectronicEquipment", "RegisteredProducersCount", "MilkSupplyto", "supplyUnit", "WhetherCashless", "AICenter", "AHWName", "AHWMobile", "AssetDetails", "Capacity", "ContactPerson", "OfficerName", "OfficerMobileNo", "Bank", "Branch", "IFSC", "BankAccountNo", "CreatedBy", "CreatedBy_IP", "Accounting" },
                                                 new string[] { "2", objdb.Office_ID(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOffice_Code.Text.Trim(), txtOfficeContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ddlBlock_ID.SelectedValue, txtElectronicEquip.Text.Trim(), txtRegisteredProducers.Text.Trim(), ddlMilkSupply.SelectedValue.ToString(), ddlSupplyUnit.SelectedValue.ToString(), ddlcashless.SelectedValue.ToString(), DdlAICenter.SelectedValue.ToString(), txtAhwName.Text.Trim(), txtAHWMobile.Text.Trim(), txtAssetDetail.Text.Trim(), txtCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), ddlBank.SelectedValue.ToString(), txtBranchName.Text.Trim(), txtIFSCCode.Text.Trim(), txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value }, "TableSave");
                        }
                        else if (ddlOfficeType.SelectedValue == "6") //DCS
                        {
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_Parant_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo", "Office_Email", "Division_ID", "District_ID", "Block_ID", "ElectronicEquipment", "RegistrationNo", "RegistrationDate", "RegisteredProducersCount", "MilkSupplyto", "supplyUnit", "WhetherCashless", "AICenter", "AHWName", "AHWMobile", "AssetDetails", "Capacity", "ContactPerson", "OfficerName", "OfficerMobileNo", "SchemeName", "WomenDcs", "BMCAvailable", "Bank", "Branch", "IFSC", "BankAccountNo", "CreatedBy", "CreatedBy_IP", "Accounting" },
                                                     new string[] { "2", objdb.Office_ID(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOffice_Code.Text.Trim(), txtOfficeContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ddlBlock_ID.SelectedValue, txtElectronicEquip.Text.Trim(), txtRegistrationNo.Text, Convert.ToDateTime(txtRegistrationDate.Text, cult).ToString("yyyy/MM/dd"), txtRegisteredProducers.Text.Trim(), ddlMilkSupply.SelectedValue.ToString(), ddlSupplyUnit.SelectedValue.ToString(), ddlcashless.SelectedValue.ToString(), DdlAICenter.SelectedValue.ToString(), txtAhwName.Text.Trim(), txtAHWMobile.Text.Trim(), txtAssetDetail.Text.Trim(), txtCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), txtScheme.Text.Trim(), ddlWomenDCS.SelectedValue.ToString(), ddlBMCAvail.SelectedValue.ToString(), ddlBank.SelectedValue.ToString(), txtBranchName.Text.Trim(), txtIFSCCode.Text.Trim(), txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value }, "TableSave");
                        }
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetOfficeDetails();
                            Session["IsSuccess"] = true;
                            Response.Redirect("OfficeRegistration.aspx", false);
                            //string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            //Clear();
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            pnlDataDiv.Visible = false;
                        }
                        else
                        {
                            Session["IsSuccess"] = false;
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Record Already Exists.")
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
                        if (txtCapacity.Text == "")
                        {
                            txtCapacity.Text = "0";
                        }
                        if (ddlOfficeType.SelectedValue == "3") //MDP
                        {
                            if (txtProductionCapacity.Text == "")
                            {
                                txtProductionCapacity.Text = "0";
                            }
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID","OfficeType_ID","Office_Name","Office_Code","Office_ContactNo","Office_Email","Office_Address","Division_ID","District_ID","Block_ID",
                                "Capacity","ProcessingCapacity","ContactPerson","NoOfEmployees","NoOfParmanent_Concontractual","SanctionedMPEB","ActualMPEB","ProductionCapacity"
                                ,"IsManufacturingUnit","ProductionName","FSSILicenceNo","FactoryLicenceNo","BoilerLicenceNo","OfficerName","OfficerMobileNo","Assembly_ID","Bank","Branch","IFSC","BankAccountNo","CreatedBy","CreatedBy_IP","Accounting" },
                       new string[] { "3", ViewState["rowid"].ToString(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim() ,txtOffice_Code.Text.Trim() ,txtOfficeContactNo.Text.Trim(),txtOffice_Email.Text.Trim(), txtAddress.Text ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_ID.SelectedValue, txtCapacity.Text.Trim(), txtProcessingCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtNoOfEmployees.Text.Trim(), txtParmanent_Concontractual.Text.Trim(), txtSanctionedMPEB.Text.Trim(), txtActualMPEB.Text.Trim(), txtProductionCapacity.Text.Trim(), 
                            ddlManufacturing.SelectedValue.ToString(), txtProductionName.Text.Trim(), txtFSSI.Text.Trim(), txtFactoryLicence.Text.Trim(), txtBoilerLicence.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(),ddlAssembly_ID.SelectedValue, ddlBank.SelectedValue.ToString(),txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value}, "TableSave");
                        }
                        else if (ddlOfficeType.SelectedValue == "4") //CC
                        {
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo", "Office_Email", "Office_Address", "Division_ID", "District_ID", "Block_ID", "Capacity", "ContactPerson", "NoOfEmployees", "NoOfParmanent_Concontractual", "SanctionedMPEB ", "ActualMPEB", "OfficerName", "OfficerMobileNo", "Assembly_ID", "Bank", "Branch", "IFSC", "BankAccountNo", "CreatedBy", "CreatedBy_IP", "Accounting" },
                                 new string[] { "3", ViewState["rowid"].ToString(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOffice_Code.Text.Trim(), txtOfficeContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), txtAddress.Text, ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ddlBlock_ID.SelectedValue, txtCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtNoOfEmployees.Text.Trim(), txtParmanent_Concontractual.Text.Trim(), txtSanctionedMPEB.Text.Trim(), txtActualMPEB.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), ddlAssembly_ID.SelectedValue, ddlBank.SelectedValue.ToString(), txtBranchName.Text.Trim(), txtIFSCCode.Text.Trim(), txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value }, "TableSave");
                        }
                        else if (ddlOfficeType.SelectedValue == "5") // BMC
                        {
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo", "Office_Email", "Office_Address", "Division_ID", "District_ID", "Block_ID", "ElectronicEquipment", "RegisteredProducersCount", "MilkSupplyto", "supplyUnit", "WhetherCashless", "AICenter", "AHWName", "AHWMobile", "AssetDetails", "Capacity", "ContactPerson", "OfficerName", "OfficerMobileNo", "Assembly_ID", "Bank", "Branch", "IFSC", "BankAccountNo", "CreatedBy", "CreatedBy_IP", "Accounting" },
                                                 new string[] { "3", ViewState["rowid"].ToString(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOffice_Code.Text.Trim(), txtOfficeContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), txtAddress.Text, ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ddlBlock_ID.SelectedValue, txtElectronicEquip.Text.Trim(), txtRegisteredProducers.Text.Trim(), ddlMilkSupply.SelectedValue.ToString(), ddlSupplyUnit.SelectedValue.ToString(), ddlcashless.SelectedValue.ToString(), DdlAICenter.SelectedValue.ToString(), txtAhwName.Text.Trim(), txtAHWMobile.Text.Trim(), txtAssetDetail.Text.Trim(), txtCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), ddlAssembly_ID.SelectedValue, ddlBank.SelectedValue.ToString(), txtBranchName.Text.Trim(), txtIFSCCode.Text.Trim(), txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value }, "TableSave");
                        }
                        else if (ddlOfficeType.SelectedValue == "6") //DCS
                        {
                            string RegistrationDate;
                            if (txtRegistrationDate.Text != "")
                            {
                                RegistrationDate = Convert.ToDateTime(txtRegistrationDate.Text, cult).ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                RegistrationDate = Convert.ToDateTime(DateTime.Now, cult).ToString("yyyy/MM/dd");
                            }
                            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "OfficeType_ID", "Office_Name", "Office_Code", "Office_ContactNo", "Office_Email", "Office_Address", "Division_ID", "District_ID", "Block_ID", "ElectronicEquipment", "RegistrationNo", "RegistrationDate", "RegisteredProducersCount", "MilkSupplyto", "supplyUnit", "WhetherCashless", "AICenter", "AHWName", "AHWMobile", "AssetDetails", "Capacity", "ContactPerson", "OfficerName", "OfficerMobileNo", "Assembly_ID", "SchemeName", "WomenDcs", "BMCAvailable", "Bank", "Branch", "IFSC", "BankAccountNo", "CreatedBy", "CreatedBy_IP", "Accounting" },
                                                                    new string[] { "3", ViewState["rowid"].ToString(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOffice_Code.Text.Trim(), txtOfficeContactNo.Text.Trim(), txtOffice_Email.Text.Trim(), txtAddress.Text, ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ddlBlock_ID.SelectedValue, txtElectronicEquip.Text.Trim(), txtRegistrationNo.Text, RegistrationDate, txtRegisteredProducers.Text.Trim(), ddlMilkSupply.SelectedValue.ToString(), ddlSupplyUnit.SelectedValue.ToString(), ddlcashless.SelectedValue.ToString(), DdlAICenter.SelectedValue.ToString(), txtAhwName.Text.Trim(), txtAHWMobile.Text.Trim(), txtAssetDetail.Text.Trim(), txtCapacity.Text.Trim(), txtContactPerson.Text.Trim(), txtOfficerName.Text.Trim(), txtofficermobileNo.Text.Trim(), ddlAssembly_ID.SelectedValue, txtScheme.Text.Trim(), ddlWomenDCS.SelectedValue.ToString(), ddlBMCAvail.SelectedValue.ToString(), ddlBank.SelectedValue.ToString(), txtBranchName.Text.Trim(), txtIFSCCode.Text.Trim(), txtBankAccountNo.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), rbAccounting.SelectedItem.Value }, "TableSave");
                        }
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetOfficeDetails();
                            Session["IsUpdate"] = true;
                            Response.Redirect("OfficeRegistration.aspx", false);
                            pnlDataDiv.Visible = false;
                            spanDivision.Visible = false;
                            divAssembly.Visible = false;
                            divBlock.Visible = false;
                            spanDistrict.Visible = false;
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Record Already Exists.")
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
                    ddlOfficeType.ClearSelection();
                }
                else
                {

                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Clear();
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

                if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "DCS")
                {
                    pnldcs.Visible = true;
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, "Select");
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "CC");
                    ddlMilkSupply.Items.Insert(3, "BMC");
                    ddlMilkSupply.Items.Insert(4, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "MDP")
                {
                    pnldcs.Visible = false;
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, "Select");
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "CC");
                    ddlMilkSupply.Items.Insert(3, "BMC");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "BMC")
                {
                    pnldcs.Visible = false;
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, "Select");
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "CC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "CC")
                {
                    pnldcs.Visible = false;
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, "Select");
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "BMC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "DS")
                {
                    pnldcs.Visible = false;
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, "Select");
                    ddlMilkSupply.Items.Insert(1, "CC");
                    ddlMilkSupply.Items.Insert(2, "BMC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "CC")
                {
                    pnldcs.Visible = false;
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, "Select");
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "BMC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
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
        //GetUnit();
    }
    protected void ddlBank_Init(object sender, EventArgs e)
    {
        GetBank();
    }

    #endregion=====================end of control======================

    #region=============== changed event for controls =================

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        lblMsg.Text = string.Empty;
        if (ddlDivision.SelectedValue != "0")
        {
            divBlock.Visible = true;
            divAssembly.Visible = true;
            GetDistrict();
        }
        else
        {
            divBlock.Visible = false;
            divAssembly.Visible = false;

        }

    }

    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblMsg.Text = string.Empty;
        spanDivision.Visible = false;
        spanDistrict.Visible = false;
        pnlDataDiv.Visible = false;
        OnChangeOffice();
        ReportingUnit.Visible = false;
        if (ddlOfficeType.SelectedIndex > 0)
        {
            GetOfficeDetails();
            spanDivision.Visible = false;
            spanDistrict.Visible = false;
            GetOfficeTypeCode();
            GetDropdownOfDivisionOrDistrict();
            ReportingUnit.Visible = true;
            Clear();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (ddlDistrict.SelectedValue != "0")
        {
            divBlock.Visible = true;
            divAssembly.Visible = true;

            getblock();
        }

        else
        {
            divBlock.Visible = false;
            divAssembly.Visible = false;

        }
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void getblock()
    {
        ds = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag", "District_ID" },
                          new string[] { "13", ddlDistrict.SelectedValue }, "dataset");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBlock_ID.DataSource = ds;
            ddlBlock_ID.DataTextField = "Block_Name";
            ddlBlock_ID.DataValueField = "Block_ID";
            ddlBlock_ID.DataBind();
            ddlBlock_ID.Items.Insert(0, new ListItem("Select", "0"));
        }

        ds = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag" },
                          new string[] { "14" }, "dataset");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAssembly_ID.DataSource = ds;
            ddlAssembly_ID.DataTextField = "Assembly_Name";
            ddlAssembly_ID.DataValueField = "Assembly_ID";
            ddlAssembly_ID.DataBind();
            ddlAssembly_ID.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            pnlDataDiv.Visible = true;
            ReportingUnit.Visible = true;
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                DataSet ds1 = new DataSet();
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    string officeid = "";
                    officeid = e.CommandArgument.ToString();
                    ds = null;

                    ds1 = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag", "Office_ID" },
                          new string[] { "16", officeid }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlOfficeType.SelectedValue = ds1.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                        if (ddlOfficeType.SelectedValue != "0")
                        {
                            ddlOfficeType.Enabled = false;
                        }
                        else
                        {
                            ddlOfficeType.Enabled = true;
                        }
                        GetOfficeTypeCode();
                        GetDropdownOfDivisionOrDistrict();
                        if (ds1.Tables[0].Rows[0]["Division_ID"].ToString() != "0")
                        {
                            divBlock.Visible = true;
                            divAssembly.Visible = true;
                            ddlDivision.SelectedValue = ds1.Tables[0].Rows[0]["Division_ID"].ToString();
                            GetDistrict();
                            ddlDistrict.SelectedValue = ds1.Tables[0].Rows[0]["District_ID"].ToString();
                            getblock();
                            if (ds1.Tables[0].Rows[0]["Block_ID"].ToString() != "" && ds1.Tables[0].Rows[0]["Block_ID"].ToString() != null)
                            {
                                ddlBlock_ID.SelectedValue = ds1.Tables[0].Rows[0]["Block_ID"].ToString();
                            }
                            if (ds1.Tables[0].Rows[0]["Assembly_ID"].ToString() != "" && ds1.Tables[0].Rows[0]["Assembly_ID"].ToString() != null)
                            {
                                ddlAssembly_ID.SelectedValue = ds1.Tables[0].Rows[0]["Assembly_ID"].ToString();
                            }
                        }
                        txtOfficeName.Text = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                        txtOffice_Code.Text = ds1.Tables[0].Rows[0]["Office_Code"].ToString();
                        txtOfficeContactNo.Text = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                        txtOffice_Email.Text = ds1.Tables[0].Rows[0]["Office_Email"].ToString();
                        txtAddress.Text = ds1.Tables[0].Rows[0]["Office_Address"].ToString();
                        txtContactPerson.Text = ds1.Tables[0].Rows[0]["ContactPerson"].ToString();
                        txtCapacity.Text = ds1.Tables[0].Rows[0]["Capacity"].ToString();
                        if (ds1.Tables[0].Rows[0]["Accounting"].ToString() != "" && ds1.Tables[0].Rows[0]["Accounting"].ToString() != null)
                        {
                            if (ds1.Tables[0].Rows[0]["Accounting"].ToString() == "True")
                            {
                                rbAccounting.SelectedValue = "1";
                            }
                            else
                            {
                                rbAccounting.SelectedValue = "0";
                            }
                        }
                        if (ds1.Tables[0].Rows[0]["Bank"].ToString() != "0" && ds1.Tables[0].Rows[0]["Bank"].ToString() != "" && ds1.Tables[0].Rows[0]["Bank"].ToString() != null)
                        {
                            GetBranchByBank();
                            ddlBank.SelectedValue = ds1.Tables[0].Rows[0]["Bank"].ToString();
                            txtBranchName.Text = ds1.Tables[0].Rows[0]["Branch"].ToString();
                            txtIFSCCode.Text = ds1.Tables[0].Rows[0]["IFSC"].ToString();
                            txtBankAccountNo.Text = ds1.Tables[0].Rows[0]["BankAccountNo"].ToString();
                        }
                        txtOfficerName.Text = ds1.Tables[0].Rows[0]["OfficerName"].ToString();
                        txtofficermobileNo.Text = ds1.Tables[0].Rows[0]["OfficerMobileNo"].ToString();
                        if (ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != "" && ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != null)
                        {
                            ddlMilkSupply.SelectedIndex = ddlMilkSupply.Items.IndexOf(ddlMilkSupply.Items.FindByText(ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString()));
                        }
                        if (ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != "" && ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != null)
                        {
                            ddlSupplyUnit.SelectedIndex = ddlSupplyUnit.Items.IndexOf(ddlSupplyUnit.Items.FindByText(ds1.Tables[0].Rows[0]["supplyUnit"].ToString()));
                        }
                        OnChangeOffice();
                        if (ds1.Tables[0].Rows[0]["OfficeType_ID"].ToString() == "3") //MDP
                        {
                            txtNoOfEmployees.Text = ds1.Tables[0].Rows[0]["NoOfEmployees"].ToString();
                            txtParmanent_Concontractual.Text = ds1.Tables[0].Rows[0]["NoOfParmanent_Concontractual"].ToString();
                            txtSanctionedMPEB.Text = ds1.Tables[0].Rows[0]["SanctionedMPEB"].ToString();
                            txtActualMPEB.Text = ds1.Tables[0].Rows[0]["ActualMPEB"].ToString();
                            txtProcessingCapacity.Text = ds1.Tables[0].Rows[0]["ProcessingCapacity"].ToString();
                            if (ds1.Tables[0].Rows[0]["IsManufacturingUnit"].ToString() != "" && ds1.Tables[0].Rows[0]["IsManufacturingUnit"].ToString() != null)
                            {
                                ddlManufacturing.SelectedValue = ds1.Tables[0].Rows[0]["IsManufacturingUnit"].ToString();
                            }
                            txtProductionName.Text = ds1.Tables[0].Rows[0]["ProductionName"].ToString();
                            txtProductionCapacity.Text = ds1.Tables[0].Rows[0]["ProductionCapacity"].ToString();
                            txtFSSI.Text = ds1.Tables[0].Rows[0]["FSSILicenceNo"].ToString();
                            txtFactoryLicence.Text = ds1.Tables[0].Rows[0]["FactoryLicenceNo"].ToString();
                            txtBoilerLicence.Text = ds1.Tables[0].Rows[0]["BoilerLicenceNo"].ToString();
                        }
                        else if (ddlOfficeType.SelectedValue == "4") //CC
                        {
                            txtNoOfEmployees.Text = ds1.Tables[0].Rows[0]["NoOfEmployees"].ToString();
                            txtParmanent_Concontractual.Text = ds1.Tables[0].Rows[0]["NoOfParmanent_Concontractual"].ToString();
                            txtSanctionedMPEB.Text = ds1.Tables[0].Rows[0]["SanctionedMPEB"].ToString();
                            txtActualMPEB.Text = ds1.Tables[0].Rows[0]["ActualMPEB"].ToString();
                        }
                        else if (ddlOfficeType.SelectedValue == "5") // BMC
                        {
                            txtRegistrationNo.Text = ds1.Tables[0].Rows[0]["RegistrationNo"].ToString();
                            if (ds1.Tables[0].Rows[0]["RegistrationDate"].ToString() != "" && ds1.Tables[0].Rows[0]["RegistrationDate"].ToString() != null)
                            {
                                txtRegistrationDate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["RegistrationDate"].ToString(), cult).ToString("dd/MM/yyyy")).ToString();
                            }
                            if (ds1.Tables[0].Rows[0]["WhetherCashless"].ToString() != "" && ds1.Tables[0].Rows[0]["WhetherCashless"].ToString() != null)
                            {
                                ddlcashless.SelectedIndex = ddlcashless.Items.IndexOf(ddlcashless.Items.FindByText(ds1.Tables[0].Rows[0]["WhetherCashless"].ToString()));
                            }
                            if (ds1.Tables[0].Rows[0]["AICenter"].ToString() != "" && ds1.Tables[0].Rows[0]["AICenter"].ToString() != null)
                            {
                                DdlAICenter.SelectedIndex = DdlAICenter.Items.IndexOf(DdlAICenter.Items.FindByText(ds1.Tables[0].Rows[0]["AICenter"].ToString()));
                            }
                            txtAhwName.Text = ds1.Tables[0].Rows[0]["AHWName"].ToString();
                            txtAHWMobile.Text = ds1.Tables[0].Rows[0]["AHWMobile"].ToString();
                            txtAssetDetail.Text = ds1.Tables[0].Rows[0]["AssetDetails"].ToString();
                            txtElectronicEquip.Text = ds1.Tables[0].Rows[0]["ElectronicEquipment"].ToString();
                            txtRegisteredProducers.Text = ds1.Tables[0].Rows[0]["RegisteredProducersCount"].ToString();
                            if (ds1.Tables[0].Rows[0]["BMCAvailable"].ToString() != "" && ds1.Tables[0].Rows[0]["BMCAvailable"].ToString() != null)
                            {
                                ddlBMCAvail.SelectedIndex = ddlBMCAvail.Items.IndexOf(ddlBMCAvail.Items.FindByText(ds1.Tables[0].Rows[0]["BMCAvailable"].ToString()));
                            }
                        }
                        else if (ddlOfficeType.SelectedValue == "6") //DCS
                        {
                            txtRegistrationNo.Text = ds1.Tables[0].Rows[0]["RegistrationNo"].ToString();
                            if (ds1.Tables[0].Rows[0]["RegistrationDate"].ToString() != "" && ds1.Tables[0].Rows[0]["RegistrationDate"].ToString() != null)
                            {
                                txtRegistrationDate.Text = (Convert.ToDateTime(ds1.Tables[0].Rows[0]["RegistrationDate"].ToString(), cult).ToString("dd/MM/yyyy")).ToString();
                            }
                            if (ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != "" && ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != null)
                            {
                                ddlMilkSupply.SelectedIndex = ddlMilkSupply.Items.IndexOf(ddlMilkSupply.Items.FindByText(ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString()));
                            }
                            if (ds1.Tables[0].Rows[0]["WhetherCashless"].ToString() != "" && ds1.Tables[0].Rows[0]["WhetherCashless"].ToString() != null)
                            {
                                ddlcashless.SelectedValue = ds1.Tables[0].Rows[0]["WhetherCashless"].ToString();
                            }
                            if (ds1.Tables[0].Rows[0]["AICenter"].ToString() != "" && ds1.Tables[0].Rows[0]["AICenter"].ToString() != null)
                            {
                                DdlAICenter.SelectedValue = ds1.Tables[0].Rows[0]["AICenter"].ToString();
                            }
                            txtAhwName.Text = ds1.Tables[0].Rows[0]["AHWName"].ToString();
                            txtAHWMobile.Text = ds1.Tables[0].Rows[0]["AHWMobile"].ToString();
                            txtAssetDetail.Text = ds1.Tables[0].Rows[0]["AssetDetails"].ToString();
                            txtElectronicEquip.Text = ds1.Tables[0].Rows[0]["ElectronicEquipment"].ToString();
                            txtRegisteredProducers.Text = ds1.Tables[0].Rows[0]["RegisteredProducersCount"].ToString();
                            txtContactPerson.Text = ds1.Tables[0].Rows[0]["ContactPerson"].ToString();
                            txtScheme.Text = ds1.Tables[0].Rows[0]["SchemeName"].ToString();
                            if (ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != "" && ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != null)
                            {
                                GetSupplyUnit();
                                ddlSupplyUnit.SelectedValue = ds1.Tables[0].Rows[0]["supplyUnit"].ToString();
                            }
                            if (ds1.Tables[0].Rows[0]["BMCAvailable"].ToString() != "" && ds1.Tables[0].Rows[0]["BMCAvailable"].ToString() != null)
                            {
                                ddlBMCAvail.SelectedIndex = ddlBMCAvail.Items.IndexOf(ddlBMCAvail.Items.FindByText(ds1.Tables[0].Rows[0]["BMCAvailable"].ToString()));
                            }

                            if (ds1.Tables[0].Rows[0]["WomenDCS"].ToString() != "" && ds1.Tables[0].Rows[0]["WomenDCS"].ToString() != null)
                            {
                                ddlWomenDCS.SelectedValue = ds1.Tables[0].Rows[0]["WomenDCS"].ToString();
                            }
                        }

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


    protected void ddlMilkSupply_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetSupplyUnit();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSupplyUnit()
    {
        try
        {
            string code = "";
            if (ddlMilkSupply.SelectedItem.Text.Trim() == "Select")
            {
                code = string.Empty;
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "Plant")
            {
                code = "2";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "DCS")
            {
                code = "6";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "MDP")
            {
                code = "3";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "BMC")
            {
                code = "5";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "CC")
            {
                code = "4";
            }

            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                       new string[] { "flag", "Office_ID" },
                                       new string[] { "17", code }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSupplyUnit.DataTextField = "Office_Name";
                    ddlSupplyUnit.DataValueField = "Office_ID";
                    ddlSupplyUnit.DataSource = ds;
                    ddlSupplyUnit.DataBind();
                    ddlSupplyUnit.Items.Insert(0, new ListItem("Select Supply Unit", "0"));
                    ddlSupplyUnit.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void OnChangeOffice()
    {
        try
        {

            if (ddlOfficeType.SelectedIndex > 0)
            {
                pnlDataDiv.Visible = true;
                if (ddlOfficeType.SelectedValue == "3") //MDP
                {
                    MDPFields.Visible = true;
                    DCSFileds.Visible = false;
                    DivMDPOrCC.Visible = true;
                    pnldcs.Visible = false;
                    DivDCSandBMC.Visible = false;
                    noOfEmp.Visible = true;
                    DivDCS.Visible = false;
                    DivAHW.Visible = false;
                    lblCapacity.Text = "Storege Capacity";
                    txtCapacity.Attributes.Add("placeholder", "Enter Storege Capacity");
                    SupplyUnit.Visible = false;
                    //spanCapacity.Visible = true;
                }
                else if (ddlOfficeType.SelectedValue == "4") //CC
                {
                    MDPFields.Visible = false;
                    DCSFileds.Visible = false;
                    DivMDPOrCC.Visible = true;
                    pnldcs.Visible = false;
                    DivDCSandBMC.Visible = false;
                    noOfEmp.Visible = true;
                    DivDCS.Visible = false;
                    DivAHW.Visible = false;
                    lblCapacity.Text = "Storege Capacity";
                    txtCapacity.Attributes.Add("placeholder", "Enter Storege Capacity");
                    SupplyUnit.Visible = false;
                    // spanCapacity.Visible = true;
                }
                else if (ddlOfficeType.SelectedValue == "5") // BMC
                {
                    DCSFileds.Visible = false;
                    MDPFields.Visible = false;
                    DivMDPOrCC.Visible = false;
                    pnldcs.Visible = true;
                    DivDCSandBMC.Visible = true;
                    noOfEmp.Visible = false;
                    DivDCS.Visible = true;
                    DivAHW.Visible = true;
                    lblCapacity.Text = "BMC Capacity";
                    txtCapacity.Attributes.Add("placeholder", "Enter BMC Capacity");
                    SupplyUnit.Visible = true;
                    // spanCapacity.Visible = true;
                }
                else if (ddlOfficeType.SelectedValue == "6") //DCS
                {
                    DCSFileds.Visible = true;
                    MDPFields.Visible = false;
                    DivMDPOrCC.Visible = true;
                    pnldcs.Visible = true;
                    DivDCSandBMC.Visible = true;
                    noOfEmp.Visible = false;
                    DivAHW.Visible = true;
                    DivDCS.Visible = true;
                    SupplyUnit.Visible = true;
                    lblCapacity.Text = "Capacity";
                    txtCapacity.Attributes.Add("placeholder", "Enter Capacity");
                    //spanCapacity.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBlock_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBlock_ID.SelectedIndex == 0)
        {
            ddlAssembly_ID.ClearSelection();
        }
    }
}