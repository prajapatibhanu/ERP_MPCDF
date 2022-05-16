using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;

public partial class mis_Common_BoothReg : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds,ds1,ds2;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                txtRegistrationDate.Attributes.Add("readonly", "readonly");
                GetLocation();
                GetSearchLocation();
                GetRetailerType();
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
        txtPanCard.Text = string.Empty;
        txtAadhaarNo.Text = string.Empty;
        pnlBankName.Visible = false;
        pnlNewIFSC.Visible = false;
        pnlAccntNo.Visible = false;
        
        
      
        txtLatitude.Text = string.Empty;
        txtLongitude.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        txtBoothName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtBCode.Text = string.Empty;
        txtContactPersonMobileNo.Text = string.Empty;
        
        txtContactPerson.Text = string.Empty;
        txtTownOrvillage.Text = string.Empty;
        ddlRetailerType.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        ddlDistrict.SelectedIndex=0;
        ddlBlock_Name.ClearSelection();
        GetBlock();
        GetDatatableHeaderDesign();
        if (ddlBank.SelectedValue != "0")
        {
            ddlBank.SelectedIndex = 0;
            //ddlBranchName.SelectedIndex = 0;
            txtBranchName.Text = string.Empty;
        }
        txtIFSCCode.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;

        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        txtBoothNameHindi.Text = string.Empty;
        ddlDeliveryType.SelectedIndex = 0;
        ddlOrganizationType.SelectedIndex = 0;
        pnlInstitution.Visible = false;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetSearchLocation()
    {
        try
        {
            ddlSearchLocation.DataTextField = "AreaName";
            ddlSearchLocation.DataValueField = "AreaId";
            ddlSearchLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlSearchLocation.DataBind();
            ddlSearchLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Search Location ", ex.Message.ToString());
        }
    }
    private void GetBoothDetails()
    {
      
        try
        {
           
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "Office_ID","AreaId","RouteId" },
                    new string[] { "1", objdb.Office_ID(),ddlSearchLocation.SelectedValue,ddlSearchRoute.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
                     new string[] { "flag", "Office_ID","AreaId" },
                     new string[] { "6", objdb.Office_ID(),ddlLocation.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetSearchRoute()
    {
        try
        {
            ddlSearchRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
                     new string[] { "flag", "Office_ID", "AreaId" },
                     new string[] { "6", objdb.Office_ID(), ddlSearchLocation.SelectedValue }, "dataset");
            ddlSearchRoute.DataTextField = "RName";
            ddlSearchRoute.DataValueField = "RouteId";
            ddlSearchRoute.DataBind();
            ddlSearchRoute.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetRetailerType()
    {
        try
        {
            ddlRetailerType.DataSource = objdb.ByProcedure("USP_Mst_RetailerType",
                     new string[] { "flag" },
                     new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlRetailerType.DataTextField = "RetailerTypeName";
            ddlRetailerType.DataValueField = "RetailerTypeID";
            ddlRetailerType.DataBind();
            ddlRetailerType.Items.Insert(0, new ListItem("Select", "0"));

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
                    new string[] { "flag"},
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
    //private void GetDSOfficeDetails()
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
    //private void GetIFSCByBranch()t
    //{
    //    try
    //    {
    //        if (ds != null)
    //        {
    //            ds.Clear();
    //        }
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
       
            try    
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string regdate = "";
                    if(txtRegistrationDate.Text=="")
                    {
                        regdate = "01/01/1900";
                    }
                    else
                    {
                        regdate=txtRegistrationDate.Text;
                    }
                    DateTime rdat = DateTime.ParseExact(regdate, "dd/MM/yyyy", culture);
                    string regdat = rdat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_BoothReg",
                            new string[] { "flag","UserTypeId","RouteId","RetailerTypeID", "BName","BCode","BContactNo", "CPersonName"
                                , "CPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "BAddress",
                                "BPincode","B_Latitude","B_Longitude","Bank_ID","BranchName","IFSC_Code","BankAccountNo"
                               , "OfficeType_ID","Office_ID","CreatedBy", "CreatedBy_IP","RetailerName_Hi","Organization_Type","Delivery_Type","PANNo","AadhaarNo","AreaId","Registration_Date" },
                            new string[] { "2",objddl.GetBoothType_id(),ddlRoute.SelectedValue,ddlRetailerType.SelectedValue
                                , txtBoothName.Text.Trim(),
                               txtBCode.Text.Trim().ToUpper(),txtBContactNo.Text,txtContactPerson.Text.Trim()
                                ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim(),ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text,txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),txtLatitude.Text.Trim(),txtLongitude.Text.Trim()
                              ,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),
                               txtBankAccountNo.Text.Trim()
                              , objdb.OfficeType_ID(),objdb.Office_ID()
                               , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),txtBoothNameHindi.Text.Trim() 
                            ,ddlOrganizationType.SelectedValue,ddlDeliveryType.SelectedValue,txtPanCard.Text.Trim(),txtAadhaarNo.Text.Trim(),ddlLocation.SelectedValue,regdat.ToString()}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            //GetBoothDetails();
                            //GetDatatableHeaderDesign();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Retailer Code " + error);
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
                        ds = objdb.ByProcedure("USP_Mst_BoothReg",
                            new string[] { "flag","BoothId","RouteId","RetailerTypeID","BName","BCode","BContactNo", "CPersonName"
                                , "CPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "BAddress",
                                "BPincode","B_Latitude","B_Longitude","Bank_ID","BranchName","IFSC_Code","BankAccountNo",
                               "Office_ID","CreatedBy", "CreatedBy_IP","PageName","Remark","RetailerName_Hi","Organization_Type","Delivery_Type","PANNo","AadhaarNo","AreaId","Registration_Date" },
                            new string[] { "3", ViewState["rowid"].ToString(),ddlRoute.SelectedValue,ddlRetailerType.SelectedValue,txtBoothName.Text.Trim(),txtBCode.Text.Trim().ToUpper(),txtBContactNo.Text
                                 ,txtContactPerson.Text.Trim(),txtContactPersonMobileNo.Text
                                ,txtEmail.Text.Trim(),ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text.Trim(),txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),txtLatitude.Text.Trim(),txtLongitude.Text.Trim()
                            ,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),txtBankAccountNo.Text.Trim()
                               ,objdb.Office_ID() , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Booth Registration Details Updated",txtBoothNameHindi.Text.Trim()
                            ,ddlOrganizationType.SelectedValue,ddlDeliveryType.SelectedValue,txtPanCard.Text.Trim(),txtAadhaarNo.Text.Trim(),ddlLocation.SelectedValue,regdat.ToString()}, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetBoothDetails();
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Retailer Code " + error);
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
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!","Enter Retailer Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        
    }
    #endregion====================================end of user defined function

    #region=====================Init event for controls===========================

    protected void ddlRoute_Init(object sender, EventArgs e)
    {
        GetRoute();
    }
   
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
        GetDatatableHeaderDesign();
        lblMsg.Text = string.Empty;
        GetDistrict();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetDatatableHeaderDesign(); 
        GetBlock();
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDatatableHeaderDesign();
        if (ddlBank.SelectedIndex != 0)
        {
            pnlBankName.Visible = true;
            pnlNewIFSC.Visible = true;
            pnlAccntNo.Visible = true;
            GetDatatableHeaderDesign();
            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
            RequiredFieldValidator9.Enabled = true;

        }


        else
        {
            pnlBankName.Visible = false;
            pnlNewIFSC.Visible = false;
            pnlAccntNo.Visible = false;
            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator9.Enabled = false;

        }
    }
    //protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblMsg.Text = string.Empty;
    //    GetIFSCByBranch();
    //}
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0" )
        {
            GetRoute();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            lblMsg.Text = string.Empty;
          InsertorUpdateDistributorReg();
        }
        
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

                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblRetailerTypeID = (Label)row.FindControl("lblRetailerTypeID");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblBName = (Label)row.FindControl("lblBName");
                    Label lblBCode = (Label)row.FindControl("lblBCode");
                    Label lblBContactNo = (Label)row.FindControl("lblBContactNo");
                    Label lblBlock_ID = (Label)row.FindControl("lblBlock_ID");
                    Label lblTownOrVillage = (Label)row.FindControl("lblTownOrVillage");
                    Label lblCPersonName = (Label)row.FindControl("lblCPersonName");
                    Label lblCPersonMobileNo = (Label)row.FindControl("lblCPersonMobileNo");
                    Label lblEmail = (Label)row.FindControl("lblEmail");
                    Label lblBAddress = (Label)row.FindControl("lblBAddress");
                    Label lblBPincode = (Label)row.FindControl("lblBPincode");

                    Label lblBLatitude = (Label)row.FindControl("lblBLatitude");
                    Label lblBLongitude = (Label)row.FindControl("lblBLongitude");

                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                    // Label lblBranch_id = (Label)row.FindControl("lblBranch_id");
                    Label lblBranchName = (Label)row.FindControl("lblBranchName");
                    Label lblIFSCCode = (Label)row.FindControl("lblIFSCCode");
                    Label lblBankAccountNo = (Label)row.FindControl("lblBankAccountNo");
                    Label lblRetailerName_Hi = (Label)row.FindControl("lblRetailerName_Hi");

                    Label lblOrganization_Type = (Label)row.FindControl("lblOrganization_Type");
                    Label lblDeliveryType = (Label)row.FindControl("lblDeliveryType");

                    Label lblPANNo = (Label)row.FindControl("lblPANNo");
                    Label lblAadhaarNo = (Label)row.FindControl("lblAadhaarNo");
                    Label lblAreaId = (Label)row.FindControl("lblAreaId");
                    Label lblRegistrationDate = (Label)row.FindControl("lblRegistrationDate");

                    ddlLocation.SelectedValue = lblAreaId.Text;
                    GetRoute();
                    ddlRoute.SelectedValue = lblRouteId.Text;
                    ddlRetailerType.SelectedValue = lblRetailerTypeID.Text;
                    txtBoothName.Text = lblBName.Text;
                    txtBCode.Text = lblBCode.Text;
                    txtBContactNo.Text = lblBContactNo.Text;
                    txtContactPerson.Text = lblCPersonName.Text;
                    txtContactPersonMobileNo.Text = lblCPersonMobileNo.Text;
                    txtEmail.Text = lblEmail.Text;
                    txtAddress.Text = lblBAddress.Text;
                    txtPincode.Text = lblBPincode.Text;
                    txtLatitude.Text = lblBLatitude.Text;
                    txtLongitude.Text = lblBLongitude.Text;
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlock_ID.Text;
                    txtTownOrvillage.Text = lblTownOrVillage.Text;
                    txtPanCard.Text = lblPANNo.Text;
                    txtAadhaarNo.Text = lblAadhaarNo.Text;
                    txtRegistrationDate.Text = lblRegistrationDate.Text;
                   // txtRegistrationDate.ReadOnly = false;
                   // txtRegistrationDate.Attributes.Clear();
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

                    txtBoothNameHindi.Text = lblRetailerName_Hi.Text;
                    if(ddlRetailerType.SelectedValue==objdb.GetInstRetailerTypeId())
                    {
                        pnlInstitution.Visible = true;
                        ddlOrganizationType.SelectedValue = lblOrganization_Type.Text;
                        ddlDeliveryType.SelectedValue = lblDeliveryType.Text;
                    }
                    else
                    {
                        pnlInstitution.Visible = false;
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
                    ds = objdb.ByProcedure("USP_Mst_BoothReg",
                                new string[] { "flag", "BoothId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Booth Registration Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetBoothDetails();
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
            if (e.CommandName == "CitizenCervices")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    HiddenField HFCitizenServices = (HiddenField)row.FindControl("HFCitizenCervices");

                    if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                    {
                        lblMsg.Text = string.Empty;
                        string CS_Status = "";
                        if (HFCitizenServices.Value=="True")
                        {
                            CS_Status="False";
                        }
                        else if (HFCitizenServices.Value=="False")
                        {
                            CS_Status = "True";
                        }
                        else
                        {
                            CS_Status = "True";
                        }

                        ds2 = objdb.ByProcedure("USP_Mst_BoothReg",
                                    new string[] { "flag", "BoothId", "CitizenServices" },
                                    new string[] { "19", e.CommandArgument.ToString(), CS_Status }, "TableSave");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetBoothDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds2.Dispose();
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
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



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRStatus = e.Row.FindControl("lblRStatus") as Label;

                if (lblRStatus.Text== "False")
                {
                    e.Row.Cells[4].CssClass = "columnred";
                }
                else
                {
                    //if (lblRStatus.Text == "False")
                    //{
                    //    e.Row.CssClass = "columnmilk";
                    //}
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void ddlRetailerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (ddlRetailerType.SelectedValue == objdb.GetInstRetailerTypeId())
        {
            pnlInstitution.Visible = true;
            ddlOrganizationType.SelectedIndex = 0;
            ddlDeliveryType.SelectedIndex = 0;
        }
        else
        {
            pnlInstitution.Visible = false;
            ddlOrganizationType.SelectedIndex = 0;
            ddlDeliveryType.SelectedIndex = 0;
        }
        GetDatatableHeaderDesign();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetBoothDetails();
        }
    }
    protected void ddlSearchLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlSearchLocation.SelectedValue!="0")
        {
            GetSearchRoute();
        }
    }
}