using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Common_DistributorReg : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds,ds1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                txtRegistrationDate.Attributes.Add("readonly", "readonly");
                GetDistributor();
                GetProductRateType();
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
        txtRegistrationDate.Attributes.Add("readonly", "readonly");
        txtAadhaarNo.Text = string.Empty;
        txtApprovedRate.Text = string.Empty;
        txtDistRate.Text = string.Empty;
        txtTransRate.Text = string.Empty;
        txtLimit.Text = string.Empty;
		 txtSecurityDeposit.Text = string.Empty;
        txtBankGuarantee.Text = string.Empty;
        ddlDistributorType.SelectedIndex = 0;
        txtGSTIN.Text = string.Empty;
        txtPanCard.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        txtDistributorName.Text = string.Empty;
        txtDistributorCode.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtDistributorContactNo.Text = string.Empty;
        txtContactPersonMobileNo.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtTownOrvillage.Text = string.Empty;
        pnlBankName.Visible = false;
        pnlNewIFSC.Visible = false;
        pnlAccntNo.Visible = false;
        GetDatatableHeaderDesign();
        if (ddlBank.SelectedValue != "0")
        {
            ddlBank.SelectedIndex = 0;
           // ddlBranchName.SelectedIndex = 0;
            txtBranchName.Text = string.Empty;
        }
        txtIFSCCode.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        ddlDivision.SelectedIndex = 0;
        ddlDistrict.SelectedIndex = 0;
        //ddlBlock_Name.SelectedIndex = 0;
        GetBlock();
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        ddlProductRateType.SelectedIndex = 0;
        chkIsTcsTax.Checked = false;
    }
    protected void GetDistributor()
    {
        try
        {
            ddlDist.DataTextField = "DTName";
            ddlDist.DataValueField = "DistributorId";
            ddlDist.DataSource = objdb.ByProcedure("USP_Mst_DistributorReg",
                   new string[] { "flag", "Office_ID" },
                   new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlDist.DataBind();
            ddlDist.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
    }
    protected void GetProductRateType()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_ProductRateType",
                   new string[] { "Flag", "Office_ID" },
                   new string[] { "4", objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlProductRateType.DataTextField = "ProductRateTypeName";
                ddlProductRateType.DataValueField = "ProductRateTypeId";
                ddlProductRateType.DataSource = ds1.Tables[0];
                ddlProductRateType.DataBind();
                ddlProductRateType.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlProductRateType.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null ) { ds1.Dispose(); }
        }
    }
    private void GetDistributorDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_DistributorReg",
                    new string[] { "flag", "Office_ID", "DistributorId" },
                    new string[] { "1", objdb.Office_ID(),ddlDist.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
        }
    }



     private void GetDistributor_Type()
    {
        try
        {
          
          
                ddlDistributorType.DataSource = objdb.ByProcedure("USP_Mst_VendorType",
                           new string[] { "flag"},
                           new string[] { "1" }, "dataset");

                ddlDistributorType.DataTextField = "VendorTypeName";
                ddlDistributorType.DataValueField = "VendorTypeId";

                ddlDistributorType.DataBind();
                ddlDistributorType.Items.Insert(0, new ListItem("Select", "0"));
               
            
          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
        }
    }
    protected void GetDivision()
    {
        try
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_ID";
            ddlDivision.DataSource = objdb.ByProcedure("SpAdminDivision",
                    new string[] { "flag" },
                    new string[] { "10" }, "dataset");
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
    protected void GetBlock()
    {
        try
        {
            lblMsg.Text = "";
            ddlBlock_Name.ClearSelection();
            ddlBlock_Name.DataTextField = "Block_Name";
            ddlBlock_Name.DataValueField = "Block_ID";
            ddlBlock_Name.DataSource = objdb.ByProcedure("SpAdminBlock",
                       new string[] { "flag", "District_ID" },
                       new string[] { "2", ddlDistrict.SelectedValue }, "dataset");
            ddlBlock_Name.DataBind();
            ddlBlock_Name.Items.Insert(0, new ListItem("Select", "0"));

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
    //private void GetBranchByBank()
    //{
    //    try
    //    {
    //        ddlBranchName.DataSource = objdb.ByProcedure("sp_tblPUBankBranchMaster",
    //                        new string[] { "flag", "Bank_id" },
    //                        new string[] { "6", ddlBank.SelectedValue }, "dataset");
    //        ddlBranchName.DataTextField = "BranchName";
    //        ddlBranchName.DataValueField = "Branch_id";
    //        ddlBranchName.DataBind();
    //        ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //private void GetIFSCByBranch()
    //{

    //    try
    //    {

    //        ds = objdb.ByProcedure("sp_tblPUBankBranchMaster",
    //                          new string[] { "flag", "Branch_id" },
    //                          new string[] { "1", ddlBranchName.SelectedValue }, "dataset");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
    //        }
    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "ooops!", "IFSC Code not found.");
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        pnlifsc.Visible = true;
    //        if (ds != null) { ds.Dispose(); }
    //    }
    //}



    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void InsertorUpdateDistributorReg()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string tcstax = "";
                    string tdstax = "";
                    string SSS = "";
                    if (chkIsTcsTax.Checked == true)
                    {
                        tcstax = "1";
                    }
                    else
                    {
                        tcstax = "0";
                    } 
                    if (chkIsTdsTax.Checked == true)
                    {
                        tdstax = "1";
                    }
                    else
                    {
                        tdstax = "0";
                    }
                    if (ChkSelfSuperStockist.Checked == true)
                    {
                        SSS = "1";
                    }
                    else
                    {
                        SSS = "0";
                    }
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                       
                        if (txtApprovedRate.Text.Trim() == "")
                        {
                            txtApprovedRate.Text = "0";
                        }
                        if (txtTransRate.Text.Trim() == "")
                        {
                            txtTransRate.Text = "0";
                        }
                        if (txtDistRate.Text.Trim() == "")
                        {
                            txtDistRate.Text = "0";
                        }
                        if (txtLimit.Text.Trim() == "")
                        {
                            txtLimit.Text = "0";
                        }

                        ds = objdb.ByProcedure("USP_Mst_DistributorReg",
                            new string[] { "flag","UserTypeId", "DName","DCode","DContactNo", "DCPersonName"
                                , "DCPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage"
                                , "DAddress", "DPincode","ComsApprovedRate","ComsTransRate","ComsDistRate","Limit","SecurityDeposit","BankGuarantee"
                                ,"Bank_ID","BranchName","IFSC_Code", "VendorTypeId", "PANNo","GSTNo","BankAccountNo"
                                , "OfficeType_ID","Office_ID","CreatedBy", "CreatedBy_IP","AadhaarNo","ProductRateTypeId","IsTcsTax","IsTdsTax","SelfSuperStockist","Registration_Date" },
                            new string[] { "2",objddl.GetDistributorType_id(), txtDistributorName.Text.Trim()
                                 ,txtDistributorCode.Text.ToUpper(),txtDistributorContactNo.Text,txtContactPerson.Text.Trim()
                                ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim(),ddlDivision.SelectedValue,ddlDistrict.SelectedValue
                                ,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text.Trim(),txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),txtApprovedRate.Text.Trim(),txtTransRate.Text.Trim(),txtDistRate.Text.Trim()
                               ,txtLimit.Text.Trim(),txtSecurityDeposit.Text,txtBankGuarantee.Text
                               ,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),ddlDistributorType.SelectedValue
                               ,txtPanCard.Text.Trim(), txtGSTIN.Text.Trim() ,txtBankAccountNo.Text.Trim()
                              , Session["OfficeType_ID"].ToString(),objdb.Office_ID()
                               , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),txtAadhaarNo.Text.Trim()
                           , ddlProductRateType.SelectedValue,tcstax,tdstax,SSS,txtRegistrationDate.Text}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                          //  GetDistributorDetails();
                          //  GetDatatableHeaderDesign();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!","Distributor Code "+ error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            }
                        }
                        
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        if (txtApprovedRate.Text.Trim() == "")
                        {
                            txtApprovedRate.Text = "0";
                        }
                        if (txtTransRate.Text.Trim() == "")
                        {
                            txtTransRate.Text = "0";
                        }
                        if (txtDistRate.Text.Trim() == "")
                        {
                            txtDistRate.Text = "0";
                        }
                        if (txtLimit.Text.Trim() == "")
                        {
                            txtLimit.Text = "0";
                        }
                        ds = objdb.ByProcedure("USP_Mst_DistributorReg",
                            new string[] { "flag", "DistributorId","DName","DCode","DContactNo", "DCPersonName"
                                , "DCPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "DAddress",
                                "DPincode","ComsApprovedRate","ComsTransRate","ComsDistRate","Limit","SecurityDeposit","BankGuarantee","Bank_ID","BranchName","IFSC_Code", "VendorTypeId", "PANNo","GSTNo","BankAccountNo"
                               ,"Office_ID","CreatedBy", "CreatedBy_IP","PageName","Remark","AadhaarNo","ProductRateTypeId","IsTcsTax","IsTdsTax","SelfSuperStockist","Registration_Date","SDcode"  },
                            new string[] { "3", ViewState["rowid"].ToString(),txtDistributorName.Text.Trim()
                                ,txtDistributorCode.Text.Trim().ToUpper(),txtDistributorContactNo.Text,txtContactPerson.Text.Trim()
                                ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim(),ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text.Trim(),txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),txtApprovedRate.Text.Trim(),txtTransRate.Text.Trim(),txtDistRate.Text.Trim(),txtLimit.Text.Trim()
                               ,txtSecurityDeposit.Text,txtBankGuarantee.Text,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim()
                               ,ddlDistributorType.SelectedValue
                               ,txtPanCard.Text.Trim(), txtGSTIN.Text.Trim()  ,txtBankAccountNo.Text.Trim()
                               ,objdb.Office_ID() , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Distributor Registration Details Updated",
                             txtAadhaarNo.Text.Trim() , ddlProductRateType.SelectedValue,tcstax,tdstax,SSS,txtRegistrationDate.Text,lblDCoDE.Text}, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetDistributorDetails();
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!","Distributor Code "+ error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Distributor Name");
                }
               
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null) { ds.Dispose(); }
            }
        }
    }
    #endregion====================================end of user defined function

    #region=====================Init event for controls===========================

    protected void ddlDivision_Init(object sender, EventArgs e)
    {
        GetDivision();
    }
    protected void ddlBank_Init(object sender, EventArgs e)
    {
        GetBank();
    }


    //protected void ddlBranchName_Init(object sender, EventArgs e)
    //{
    //    ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));

    //}


  

    #endregion=====================end of control======================

    #region=============== changed event for controls =================

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDistrict();
        GetDatatableHeaderDesign();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBlock();
        GetDatatableHeaderDesign();
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDatatableHeaderDesign();
        if(ddlBank.SelectedIndex!=0)
        {
            pnlBankName.Visible = true;
            pnlNewIFSC.Visible = true;
            pnlAccntNo.Visible = true;

            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
            RequiredFieldValidator9.Enabled = true;

        }
        else
        {
            pnlBankName.Visible = false;
            pnlNewIFSC.Visible = false;
            pnlAccntNo.Visible = false;
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            RequiredFieldValidator8.Enabled = false;

        }
        
        
      // GetBranchByBank();
    }
    //protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetIFSCByBranch();
    //}
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateDistributorReg();
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
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblBlock_ID = (Label)row.FindControl("lblBlock_ID");
                    Label lblTownOrVillage = (Label)row.FindControl("lblTownOrVillage");
                    Label lblDName = (Label)row.FindControl("lblDName");
                    Label lblDCode = (Label)row.FindControl("lblDCode");
					lblDCoDE.Text = lblDCode.Text;//for self superstockist
                    Label lblDContactNo = (Label)row.FindControl("lblDContactNo");
                    Label lblDCPersonName = (Label)row.FindControl("lblDCPersonName");
                    Label lblDCPersonMobileNo = (Label)row.FindControl("lblDCPersonMobileNo");
                    Label lblEmail = (Label)row.FindControl("lblEmail");
                    Label lblDAddress = (Label)row.FindControl("lblDAddress");
                    Label lblDPincode = (Label)row.FindControl("lblDPincode");
                    Label lblVendorTypeId = (Label)row.FindControl("lblVendorTypeId");
                    Label lblPANNo = (Label)row.FindControl("lblPANNo");
                    Label lblGSTNo = (Label)row.FindControl("lblGSTNo");
                    

                    Label lblComsApprovedRate = (Label)row.FindControl("lblComsApprovedRate");
                    Label lblComsTransRate = (Label)row.FindControl("lblComsTransRate");
                    Label lblComsDistRate = (Label)row.FindControl("lblComsDistRate");
                    Label lblLimit = (Label)row.FindControl("lblLimit");

                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                   // Label lblBranch_id = (Label)row.FindControl("lblBranch_id");
                    Label lblBranchName = (Label)row.FindControl("lblBranchName");
                    Label lblIFSCCode = (Label)row.FindControl("lblIFSCCode");
                    Label lblBankAccountNo = (Label)row.FindControl("lblBankAccountNo");
                    Label lblAadhaarNo = (Label)row.FindControl("lblAadhaarNo");
                    Label lblProductRateTypeId = (Label)row.FindControl("lblProductRateTypeId");
                    Label lblIsTcsTax = (Label)row.FindControl("lblIsTcsTax"); 
                    Label lblIsTdsTax = (Label)row.FindControl("lblIsTdsTax");
                    Label lblSelfSuperStockist = (Label)row.FindControl("lblSelfSuperStockist");
                    Label lblRegistrationDate = (Label)row.FindControl("lblRegistrationDate");
					Label lblSecurityDeposit = (Label)row.FindControl("lblSecurityDeposit");
                    Label lblBankGuarantee = (Label)row.FindControl("lblBankGuarantee");

                    txtDistributorName.Text = lblDName.Text;
                    txtDistributorCode.Text = lblDCode.Text;
                    txtDistributorContactNo.Text = lblDContactNo.Text;
                    txtContactPerson.Text = lblDCPersonName.Text;
                    txtContactPersonMobileNo.Text = lblDCPersonMobileNo.Text;
                    txtEmail.Text = lblEmail.Text;
                    txtAddress.Text = lblDAddress.Text;
                    txtPincode.Text = lblDPincode.Text;
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlock_ID.Text;
                    txtTownOrvillage.Text = lblTownOrVillage.Text;
                    ddlDistributorType.SelectedValue = lblVendorTypeId.Text;
                    txtPanCard.Text = lblPANNo.Text;
                    txtGSTIN.Text = lblGSTNo.Text;
                    txtApprovedRate.Text = lblComsApprovedRate.Text;
                    txtTransRate.Text = lblComsTransRate.Text;
                    txtDistRate.Text = lblComsDistRate.Text;
                    txtLimit.Text = lblLimit.Text;
					txtSecurityDeposit.Text = lblSecurityDeposit.Text;
                    txtBankGuarantee.Text = lblBankGuarantee.Text;
                    txtAadhaarNo.Text = lblAadhaarNo.Text;
                    txtRegistrationDate.Text = lblRegistrationDate.Text;
                    //txtRegistrationDate.Attributes.Clear();
                    txtRegistrationDate.Attributes.Remove("readonly");

                    if (lblBank_id.Text != "" && lblBank_id.Text != "0")
                    {

                        pnlBankName.Visible = true;
                        pnlNewIFSC.Visible = true;
                        pnlAccntNo.Visible = true;
                        ddlBank.SelectedValue = lblBank_id.Text;
                        //GetBranchByBank();
                        //ddlBranchName.SelectedValue = lblBranch_id.Text;
                        txtBranchName.Text = lblBranchName.Text;
                        txtIFSCCode.Text = lblIFSCCode.Text;
                       // pnlifsc.Visible = true;
                        txtBankAccountNo.Text = lblBankAccountNo.Text;
                    }
                    if(lblProductRateTypeId.Text!="")
                    {
                        ddlProductRateType.SelectedValue = lblProductRateTypeId.Text;
                    }
                    else
                    {
                        ddlProductRateType.SelectedIndex = 0;
                    }
                    if (lblIsTcsTax.Text=="True")
                    {
                        chkIsTcsTax.Checked = true;
                    }
                    else
                    {
                        chkIsTcsTax.Checked = false;
                    }
                    if (lblIsTdsTax.Text=="True")
                    {
                        chkIsTdsTax.Checked = true;
                    }
                    else
                    {
                        chkIsTdsTax.Checked = false;
                    }
                    if (lblSelfSuperStockist.Text=="True")
                    {
                        ChkSelfSuperStockist.Checked = true;
                    }
                    else
                    {
                        ChkSelfSuperStockist.Checked = false;
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
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("USP_Mst_DistributorReg",
                                new string[] { "flag", "DistributorId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Distributor Registration Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetDistributorDetails();
                        GetDatatableHeaderDesign();
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
    
    protected void ddlDistributorType_Init(object sender, EventArgs e)
    
    {
        GetDistributor_Type();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetDistributorDetails();
        }
    }
}