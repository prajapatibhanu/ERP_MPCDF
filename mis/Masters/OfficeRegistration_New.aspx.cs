
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;


public partial class mis_Masters_OfficeRegistration_New : System.Web.UI.Page
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
                //GetOfficeDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetCCDetails();
                pnlDataDiv.Visible = false;
                ReportingUnit.Visible = false;
                // FillSupplyUnit();
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
    
    private void Clear()
    {
        txtOfficeName.Enabled = true;
        txtOffice_Code.Enabled = true;
        ddlOfficeType.Enabled = true;
        ddlDivision.SelectedIndex = 0;
        txtOfficeName.Text = "";
        txtOffice_Code.Text = "";
        txtOfficeContactNo.Text = "";
        txtOffice_Email.Text = "";
        
        txtOfficerName.Text = "";
        txtofficermobileNo.Text = "";
       
        txtBranchName.Text = "";
        
        if (ddlBank.SelectedValue != "0")
        {
            ddlBank.SelectedIndex = 0;
            txtBranchName.Text = "";
        }
        txtIFSCCode.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
		ddlDCSMember.ClearSelection();
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        
    }
    private void GetOfficeDetails()
    {
        try
        {
           
            ds = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag", "CreatedBy", "OfficeType_ID", "Office_Parant_ID", "Supplyunitparant_ID" },
                    new string[] { "40", objdb.createdBy(), ddlOfficeType_flt.SelectedValue.ToString(), objdb.Office_ID(),ddlChillingCenter.SelectedValue }, "dataset");
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
                
            }
            else if (ddlOfficeType.SelectedValue == "4")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                
            }
            else if (ddlOfficeType.SelectedValue == "5")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                

            }
            else if (ddlOfficeType.SelectedValue == "6")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                
            }
            else if (ddlOfficeType.SelectedValue == "2")
            {
                spanDivision.Visible = true;
                spanDistrict.Visible = true;
                

            }
            
            else
            {
                spanDivision.Visible = false;
                spanDistrict.Visible = false;
                txtOfficeName.Text = string.Empty;
                
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
    private void GetOfficeTypeFilter()
    {
        try
        {
            ddlOfficeType_flt.DataSource = objddl.OfficeTypeFill();
            ddlOfficeType_flt.DataTextField = "OfficeType_Title";
            ddlOfficeType_flt.DataValueField = "OfficeType_ID";
            ddlOfficeType_flt.DataBind();

            if (objdb.OfficeType_ID() == objdb.GetHOType_Id().ToString())
            {
                for (int i = ddlOfficeType_flt.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType_flt.Items[i].Value != objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlOfficeType_flt.Items.RemoveAt(i);
                    }
                }
            }
            else if (objdb.OfficeType_ID() == objdb.GetDSType_Id().ToString())
            {
                for (int i = ddlOfficeType_flt.Items.Count - 1; i >= 0; i--)
                {
                    if (ddlOfficeType_flt.Items[i].Value == objdb.GetHOType_Id().ToString() || ddlOfficeType_flt.Items[i].Value == objdb.GetDSType_Id().ToString())// for show only dugdh sangh
                    {
                        ddlOfficeType_flt.Items.RemoveAt(i);
                    }
                }
            }
            ddlOfficeType_flt.Items.Insert(0, new ListItem("Select", "0"));

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
                                       new string[] { "flag", "Office_ID", "Office_Parant_ID" },
                                       new string[] { "47", code, objdb.Office_ID() }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSupplyUnit.DataTextField = "Office_Name";
                    ddlSupplyUnit.DataValueField = "Office_ID";
                    ddlSupplyUnit.DataSource = ds;
                    ddlSupplyUnit.DataBind();
                    ddlSupplyUnit.Items.Insert(0, new ListItem("Select Supply Unit", "0"));
                }
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
                ddlSupplyUnit.Items.Insert(0, new ListItem("Select", "0"));
                if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "DCS")
                {
                    
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, new ListItem("Select", "0"));
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "CC");
                    ddlMilkSupply.Items.Insert(3, "BMC");
                    ddlMilkSupply.Items.Insert(4, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "MDP")
                {
                    
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, new ListItem("Select", "0"));
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "CC");
                    ddlMilkSupply.Items.Insert(3, "BMC");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "BMC")
                {
                    
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, new ListItem("Select", "0"));
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "CC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "CC")
                {
                    
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, new ListItem("Select", "0"));
                    ddlMilkSupply.Items.Insert(1, "Plant");
                    ddlMilkSupply.Items.Insert(2, "BMC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "DS")
                {
                    
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, new ListItem("Select", "0"));
                    ddlMilkSupply.Items.Insert(1, "CC");
                    ddlMilkSupply.Items.Insert(2, "BMC");
                    ddlMilkSupply.Items.Insert(3, "MDP");
                    ddlMilkSupply.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Rows[0]["OfficeTypeCode"].ToString() == "CC")
                {
                    
                    ddlMilkSupply.Items.Clear();
                    ddlMilkSupply.Items.Insert(0, new ListItem("Select", "0"));
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
                                new string[] { "flag", "Office_Parant_ID","OfficeType_ID","Office_Name","Office_Name_E",
                                               "Office_Code","SocietyCode","Office_ContactNo","Office_Email","Division_ID","Assembly_ID",
                                               "District_ID","Block_ID","OfficerName", "OfficerMobileNo","Bank","Branch","IFSC","SocietyCategory",
                                               "BankAccountNo","AccountHolderName","CreatedBy","CreatedBy_IP","MilkSupplyto","supplyUnit","Category","DCSMember"},
                       new string[] { "2", objdb.Office_ID(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim() , txtOffice_Name_E.Text,
                                      txtOffice_Code.Text.Trim() ,txtSocietyCode.Text.Trim(),txtOfficeContactNo.Text.Trim(),txtOffice_Email.Text.Trim() ,ddlDivision.SelectedValue,ddlAssembly_ID.SelectedValue,
                                      ddlDistrict.SelectedValue,ddlBlock_ID.SelectedValue,  txtOfficerName.Text.Trim(), 
                                      txtofficermobileNo.Text.Trim(), ddlBank.SelectedValue.ToString(),txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),ddlSocietyCategory.SelectedValue,
                                      txtBankAccountNo.Text.Trim(),txtAccountHolderName.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),ddlMilkSupply.SelectedValue,ddlSupplyUnit.SelectedValue,ddlDeathClaimCategory.SelectedValue,ddlDCSMember.SelectedItem.Text}, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetOfficeDetails();
                            Session["IsSuccess"] = true;
                            Response.Redirect("OfficeRegistration_New.aspx", false);
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
                        ds = objdb.ByProcedure("SpAdminOffice",
                                new string[] { "flag","Office_ID", "Office_Parant_ID","OfficeType_ID","Office_Name","Office_Name_E",
                                               "Office_Code","SocietyCode","Office_ContactNo","Office_Email","Division_ID","Assembly_ID",
                                               "District_ID","Block_ID","OfficerName", "OfficerMobileNo","Bank","Branch","IFSC","SocietyCategory",
                                               "BankAccountNo","AccountHolderName","CreatedBy","CreatedBy_IP","MilkSupplyto","supplyUnit","Category","DCSMember"},
                       new string[] { "39",ViewState["rowid"].ToString(), objdb.Office_ID(), ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim() , txtOffice_Name_E.Text,
                                      txtOffice_Code.Text.Trim() ,txtSocietyCode.Text.Trim(),txtOfficeContactNo.Text.Trim(),txtOffice_Email.Text.Trim() ,ddlDivision.SelectedValue,ddlAssembly_ID.SelectedValue,
                                      ddlDistrict.SelectedValue,ddlBlock_ID.SelectedValue,  txtOfficerName.Text.Trim(), 
                                      txtofficermobileNo.Text.Trim(), ddlBank.SelectedValue.ToString(),txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),
                                     ddlSocietyCategory.SelectedValue, txtBankAccountNo.Text.Trim(),txtAccountHolderName.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),ddlMilkSupply.SelectedValue,ddlSupplyUnit.SelectedValue,ddlDeathClaimCategory.SelectedValue,ddlDCSMember.SelectedItem.Text}, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetOfficeDetails();
                            //Session["IsUpdate"] = true;
                            //Response.Redirect("OfficeRegistration.aspx", false);
                            Clear();
                            btnSubmit.Text = "Save";
                            pnlDataDiv.Visible = false;
                            spanDivision.Visible = false;
                            divAssembly.Visible = false;
                            divBlock.Visible = false;
                            spanDistrict.Visible = false;
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            //if (error == "Record Already Exists.")
                            //{
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            //}
                            //else
                            //{
                                //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                           // }
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
        
        ReportingUnit.Visible = false;
        if (ddlOfficeType.SelectedIndex > 0)
        {
            //GetOfficeDetails();
            spanDivision.Visible = false;
            spanDistrict.Visible = false;
            GetOfficeTypeCode();
            GetDropdownOfDivisionOrDistrict();
            ReportingUnit.Visible = true;
            pnlDataDiv.Visible = true;
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
    protected void ddlMilkSupply_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMilkSupply.SelectedIndex > 0)
            {
                GetSupplyUnit();
            }
            else
            {
                ddlMilkSupply.ClearSelection();
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
    #endregion============ end of changed event for controls===========
    protected void ddlOfficeType_flt_SelectedIndexChanged(object sender, EventArgs e)
    {
        divcc.Visible = false;
        ddlChillingCenter.ClearSelection();
        if (ddlOfficeType_flt.SelectedValue == "5" || ddlOfficeType_flt.SelectedValue == "6")
        {
            divcc.Visible = true;
            GetOfficeDetails();
        }
        else
        {
            GetOfficeDetails();
        }
        
    }
    protected void ddlOfficeType_flt_Init(object sender, EventArgs e)
    {
        GetOfficeTypeFilter();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            pnlDataDiv.Visible = true;
            //ReportingUnit.Visible = true;
            lblMsg.Text = "";
			GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
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
                       btnSubmit.Text = "Update";
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
                        //GetDropdownOfDivisionOrDistrict();
                        //if (ds1.Tables[0].Rows[0]["Division_ID"].ToString() != "0")
                        //{
                        //    divBlock.Visible = false;
                        //    divAssembly.Visible = false;
                        //    ddlDivision.SelectedValue = ds1.Tables[0].Rows[0]["Division_ID"].ToString();
                        //    GetDistrict();
                        //    ddlDistrict.SelectedValue = ds1.Tables[0].Rows[0]["District_ID"].ToString();
                        //    getblock();
                        //     if (ds1.Tables[0].Rows[0]["Block_ID"].ToString() != "" && ds1.Tables[0].Rows[0]["Block_ID"].ToString() != null)
                        //    {
                        //        foreach(ListItem var in ddlBlock_ID.Items)
                        //        {
                        //            if(var.Value == ds1.Tables[0].Rows[0]["Block_ID"].ToString())
                        //            {
                        //                var.Selected = true;
                        //                break;
                        //            }
                        //        }
                        //       // ddlBlock_ID.SelectedValue = ds1.Tables[0].Rows[0]["Block_ID"].ToString();
                        //    }
                        //    if (ds1.Tables[0].Rows[0]["Assembly_ID"].ToString() != "" && ds1.Tables[0].Rows[0]["Assembly_ID"].ToString() != null)
                        //    {
                        //        ddlAssembly_ID.SelectedValue = ds1.Tables[0].Rows[0]["Assembly_ID"].ToString();
                        //    }
                        //}
                        if (ds1.Tables[0].Rows[0]["Office_Name"].ToString() != "" && ds1.Tables[0].Rows[0]["Office_Name"].ToString() != null)
                        {
                            txtOfficeName.Text = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                            txtOfficeName.Enabled = true;
                        }
                        else
                        {
                            txtOfficeName.Enabled = true;
                        }
                        if (ds1.Tables[0].Rows[0]["Office_Name_E"].ToString() != "" && ds1.Tables[0].Rows[0]["Office_Name_E"].ToString() != null)
                        {
                            txtOffice_Name_E.Text = ds1.Tables[0].Rows[0]["Office_Name_E"].ToString();
                            txtOffice_Name_E.Enabled = true;
                        }
                        else
                        {
                            txtOffice_Name_E.Enabled = true;
                            txtOffice_Name_E.Text = "";
                        }
                        if (ds1.Tables[0].Rows[0]["Office_Code"].ToString() != "" && ds1.Tables[0].Rows[0]["Office_Code"].ToString() != null)
                        {
                            txtOffice_Code.Text = ds1.Tables[0].Rows[0]["Office_Code"].ToString();
                            txtOffice_Code.Enabled = true;
                        }
                        else
                        {
                            txtOffice_Code.Enabled = true;
                        }
                        if (ds1.Tables[0].Rows[0]["SocietyCode"].ToString() != "" && ds1.Tables[0].Rows[0]["SocietyCode"].ToString() != null)
                        {
                            txtSocietyCode.Text = ds1.Tables[0].Rows[0]["SocietyCode"].ToString();
                            txtSocietyCode.Enabled = true;
                        }
                        else
                        {
                            txtSocietyCode.Enabled = true;
                        }
                        txtOfficeContactNo.Text = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                        txtOffice_Email.Text = ds1.Tables[0].Rows[0]["Office_Email"].ToString();
                        
                        if (ds1.Tables[0].Rows[0]["Bank"].ToString() != "0" && ds1.Tables[0].Rows[0]["Bank"].ToString() != "" && ds1.Tables[0].Rows[0]["Bank"].ToString() != null)
                        {
                            ddlBank.SelectedValue = ds1.Tables[0].Rows[0]["Bank"].ToString();
                            
                        }
						txtBranchName.Text = ds1.Tables[0].Rows[0]["Branch"].ToString();
                            txtIFSCCode.Text = ds1.Tables[0].Rows[0]["IFSC"].ToString();
                            txtBankAccountNo.Text = ds1.Tables[0].Rows[0]["BankAccountNo"].ToString();
                            txtAccountHolderName.Text = ds1.Tables[0].Rows[0]["AccountHolderName"].ToString();
                        txtOfficerName.Text = ds1.Tables[0].Rows[0]["OfficerName"].ToString();
                        txtofficermobileNo.Text = ds1.Tables[0].Rows[0]["OfficerMobileNo"].ToString();
                        //if (ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != "" && ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != null)
                        //{
                        //    ddlMilkSupply.SelectedValue = ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString();
                        //}
                        //if (ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != "" && ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != null)
                        //{
                        //    GetSupplyUnit();
                        //    ddlSupplyUnit.SelectedValue = ds1.Tables[0].Rows[0]["supplyUnit"].ToString();
                        //}
                        
                        //if (ds1.Tables[0].Rows[0]["OfficeType_ID"].ToString() == "3") //MDP
                        //{
                            
                        //}
                        //else if (ddlOfficeType.SelectedValue == "4") //CC
                        //{
                            
                        //}
                        //else if (ddlOfficeType.SelectedValue == "5") // BMC
                        //{
                        //    if (ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != "" && ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != null)
                        //    {
                        //        ddlMilkSupply.SelectedValue = ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString();
                        //    }
                        //     ddlSocietyCategory.ClearSelection();
                        //     ddlSocietyCategory.Items.FindByValue(ds1.Tables[0].Rows[0]["SocietyCategory"].ToString()).Selected = true;
                        //}
                        //else if (ddlOfficeType.SelectedValue == "6") //DCS
                        //{
                           
                        //    if (ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != "" && ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != null)
                        //    {
                        //        ddlMilkSupply.SelectedValue = ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString();
                        //    }
                            
                        //    ddlSocietyCategory.ClearSelection();
                        //     ddlSocietyCategory.Items.FindByValue(ds1.Tables[0].Rows[0]["SocietyCategory"].ToString()).Selected = true;
                        //}

                    }
                    ddlSocietyCategory.ClearSelection();
                    ddlSocietyCategory.Items.FindByValue(ds1.Tables[0].Rows[0]["SocietyCategory"].ToString()).Selected = true;
                    ddlDeathClaimCategory.ClearSelection();
                    ddlDeathClaimCategory.Items.FindByValue(ds1.Tables[0].Rows[0]["Category"].ToString()).Selected = true;
					ddlDCSMember.ClearSelection();
                    ddlDCSMember.Items.FindByText(ds1.Tables[0].Rows[0]["DCSMember"].ToString()).Selected = true;
                    ViewState["rowid"] = e.CommandArgument;
                   
                    
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GetOfficeDetails();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        InsertorUpdateOfficeReg();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "34", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlChillingCenter.DataTextField = "Office_Name";
                        ddlChillingCenter.DataValueField = "Office_ID";


                        ddlChillingCenter.DataSource = ds;
                        ddlChillingCenter.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlChillingCenter.Items.Insert(0, new ListItem("All", "0"));
                        }
                        else
                        {
                            ddlChillingCenter.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlChillingCenter.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ddlChillingCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOfficeDetails();
    }
}
