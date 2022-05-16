using System;
using System.Collections.Generic;
using System.Data;  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Common_SubDistributorReg : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetSubDistributorDetails();
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
        txtApprovedRate.Text = string.Empty;
        txtDistRate.Text = string.Empty;
        txtTransRate.Text = string.Empty;
        txtLimit.Text = string.Empty;

        pnlBankName.Visible = false;
        pnlNewIFSC.Visible = false;
        pnlAccntNo.Visible = false;
        ddlSubDistributorType.SelectedIndex = 0;
        txtGSTIN.Text = string.Empty;
        txtPanCard.Text = string.Empty;
        ddlDistributor.SelectedIndex = -1;
        txtPincode.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        txtSubDistributorName.Text = string.Empty;
        txtSDCode.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtSDContactNo.Text = string.Empty;
        txtTownOrvillage.Text = string.Empty;

        txtContactPerson.Text = string.Empty;
        txtContactPersonMobileNo.Text = string.Empty;
        ddlDivision.SelectedIndex = 0;
        ddlDistrict.ClearSelection();
        GetDatatableHeaderDesign();

        if (ddlBank.SelectedValue != "0")
        {
            ddlBank.SelectedIndex = 0;
            txtBranchName.Text = string.Empty;
            //ddlBranchName.SelectedIndex = 0;
        }
        txtIFSCCode.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        ddlDistrict.SelectedIndex = 0;
        ddlBlock_Name.ClearSelection();
        GetBlock();

        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void GetSubDistributorDetails()
    {
        try
        {

            ds = objdb.ByProcedure("USP_Mst_SubDistributorReg",
                    new string[] { "flag", "Office_ID" },
                    new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GetDatatableHeaderDesign();
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
    private void GetDistributor()
    {
        try
        {
            ddlDistributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorReg",
                        new string[] { "flag", "Office_ID" },
                        new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlDistributor.DataTextField = "DTBName";
            ddlDistributor.DataValueField = "DistributorId";
            ddlDistributor.DataBind();
            ddlDistributor.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



   private void  GetSubDistributorType()
    {

        try
        {


            ddlSubDistributorType.DataSource = objdb.ByProcedure("USP_Mst_VendorType",
                       new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            ddlSubDistributorType.DataTextField = "VendorTypeName";
            ddlSubDistributorType.DataValueField = "VendorTypeId";

            ddlSubDistributorType.DataBind();
            ddlSubDistributorType.Items.Insert(0, new ListItem("Select", "0"));



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
    private void InsertorUpdateDistributorReg()
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
                        ds = objdb.ByProcedure("USP_Mst_SubDistributorReg",
                            new string[] { "flag","DistributorId","UserTypeId", "SDName","SDCode","SDContactNo", "SDCPersonName"
                                , "SDCPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "SDAddress",
                                "SDPincode","ComsApprovedRate","ComsTransRate","ComsDistRate","Limit","Bank_ID","BranchName","IFSC_Code"
                                , "VendorTypeId", "PANNo","GSTNo" ,"BankAccountNo"
                               , "OfficeType_ID","Office_ID","CreatedBy", "CreatedBy_IP" },
                            new string[] { "2",ddlDistributor.SelectedValue,objddl.GetSubDistributorType_id()
                                , txtSubDistributorName.Text.Trim()
                                ,txtSDCode.Text.Trim().ToUpper(),txtSDContactNo.Text.Trim(),txtContactPerson.Text.Trim()
                                ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim(),ddlDivision.SelectedValue,ddlDistrict.SelectedValue
                                ,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text,txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),txtApprovedRate.Text.Trim(),txtTransRate.Text.Trim(),txtDistRate.Text.Trim()
                               ,txtLimit.Text.Trim()
                              ,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim()
                              ,ddlSubDistributorType.SelectedValue
                               ,txtPanCard.Text.Trim(), txtGSTIN.Text.Trim() ,txtBankAccountNo.Text.Trim()
                              , Session["OfficeType_ID"].ToString(),objdb.Office_ID()
                               , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),objdb.Office_ID() }, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSubDistributorDetails();
                            GetDatatableHeaderDesign();
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
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            }
                        }
                        ds.Clear();
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
                        ds = objdb.ByProcedure("USP_Mst_SubDistributorReg",
                            new string[] { "flag","SubDistributorId","DistributorId","SDName","SDCode","SDContactNo", "SDCPersonName"
                                , "SDCPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "SDAddress",
                                "SDPincode","ComsApprovedRate","ComsTransRate","ComsDistRate","Limit","Bank_ID","BranchName","IFSC_Code"
                                , "VendorTypeId", "PANNo","GSTNo","BankAccountNo"
                               ,"CreatedBy", "CreatedBy_IP","PageName","Remark" },
                            new string[] { "3", ViewState["rowid"].ToString(),ddlDistributor.SelectedValue,txtSubDistributorName.Text.Trim()
                                ,txtSDCode.Text.Trim().ToUpper(),txtSDContactNo.Text.Trim(),txtContactPerson.Text.Trim()
                                ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim(),ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text.Trim(),txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),txtApprovedRate.Text.Trim(),txtTransRate.Text.Trim(),txtDistRate.Text.Trim(),txtLimit.Text.Trim()
                               ,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim()
                               ,ddlSubDistributorType.SelectedValue
                               ,txtPanCard.Text.Trim(), txtGSTIN.Text.Trim() ,txtBankAccountNo.Text.Trim()
                                , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Sub Distributor Registration Details Updated"}, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetSubDistributorDetails();
                            GetDatatableHeaderDesign();
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
                       
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter SuDistributor Name");
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

    protected void ddlDistributor_Init(object sender, EventArgs e)
    {
        GetDistributor();
    }
    
      protected void  ddlSubDistributorType_Init (object sender, EventArgs e)
    {
        GetSubDistributorType();
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
        if (ddlBank.SelectedIndex != 0)
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
            RequiredFieldValidator4.Enabled=false;
            RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator9.Enabled = false;

        }
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
                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblBlock_ID = (Label)row.FindControl("lblBlock_ID");
                    Label lblTownOrVillage = (Label)row.FindControl("lblTownOrVillage");
                    Label lblSDName = (Label)row.FindControl("lblSDName");
                    Label lblSDCode = (Label)row.FindControl("lblSDCode");
                    Label lblSDContactNo = (Label)row.FindControl("lblSDContactNo");
                    Label lblSDCPersonName = (Label)row.FindControl("lblSDCPersonName");
                    Label lblSDCPersonMobileNo = (Label)row.FindControl("lblSDCPersonMobileNo");
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

                    ddlDistributor.SelectedValue = lblDistributorId.Text;
                    txtSubDistributorName.Text = lblSDName.Text;
                    txtSDCode.Text = lblSDCode.Text;
                    txtSDContactNo.Text = lblSDContactNo.Text;
                    txtContactPerson.Text = lblSDCPersonName.Text;
                    txtContactPersonMobileNo.Text = lblSDCPersonMobileNo.Text;
                    txtEmail.Text = lblEmail.Text;
                    txtAddress.Text = lblDAddress.Text;
                    txtPincode.Text = lblDPincode.Text;
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlock_ID.Text;
                    txtTownOrvillage.Text = lblTownOrVillage.Text;
                    ddlSubDistributorType.SelectedValue = lblVendorTypeId.Text;
                    txtPanCard.Text = lblPANNo.Text;
                    txtGSTIN.Text = lblGSTNo.Text;
                    txtApprovedRate.Text = lblComsApprovedRate.Text;
                    txtTransRate.Text = lblComsTransRate.Text;
                    txtDistRate.Text = lblComsDistRate.Text;
                    txtLimit.Text = lblLimit.Text;
                    GetDatatableHeaderDesign();
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
                    ds = objdb.ByProcedure("USP_Mst_SubDistributorReg",
                                new string[] { "flag", "SubDistributorId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Sub Distributor Registration Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSubDistributorDetails();
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
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    #endregion=============end of button click funciton==================



   
}