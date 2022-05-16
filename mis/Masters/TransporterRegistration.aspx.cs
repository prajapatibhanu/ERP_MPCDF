using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class mis_Common_TransporterRegistration : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetAllTransporter();
                GetVendorType();
                GetLocation();
                GetSearchLocation();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
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
    protected void GetVendorType()
    {
        try
        {
            ddlVendorType.DataTextField = "VendorTypeName";
            ddlVendorType.DataValueField = "VendorTypeId";
            ddlVendorType.DataSource = objdb.ByProcedure("USP_Mst_VendorType",
                           new string[] { "flag" },
                           new string[] { "1" }, "dataset");
            ddlVendorType.DataBind();
            ddlVendorType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    #region=======================user defined function========================
    protected void GetAllTransporter()
    {
        try
        {
            ddlSearchTransporter.DataTextField = "Contact_Person";
            ddlSearchTransporter.DataValueField = "TransporterId";
            ddlSearchTransporter.DataSource = objdb.ByProcedure("USP_Mst_TransporterMaster",
                   new string[] { "flag", "Office_ID","AreaId" },
                   new string[] { "5", objdb.Office_ID(),ddlSearchLocation.SelectedValue }, "dataset");
            ddlSearchTransporter.DataBind();
            ddlSearchTransporter.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
    }
    private void Clear()
    {


        pnlBankName.Visible = false;
       
        pnlAccntNo.Visible = false;
        pnlNewIFSC.Visible = false;

        txtIFSCCode.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtTransCompanyName.Text = string.Empty;
        txtContactPersonName.Text = string.Empty;
        txtContactNumber.Text = string.Empty;
        txtEmailID.Text = string.Empty;
        txtPanCard.Text = string.Empty;
        txtGSTIN.Text = string.Empty;
        ddlDivision.SelectedIndex = 0;
        txtAddress.Text = string.Empty;
        txtPincode.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        ddlBlock_Name.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        if(ddlBank.SelectedValue!="0")
        {
            txtBranchName.Text = string.Empty;
            txtIFSCCode.Text = string.Empty;
        }
        txtBankAccountNo.Text = string.Empty;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        GetDatatableHeaderDesign();
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
            ddlSearchLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
    private void GetDistrictByDivison()
    {
        try
        {
            if (ddlDivision.SelectedValue!="0")
            {
                ddlDistrict.DataSource = objdb.ByProcedure("SpAdminDistrict",
                                        new string[] { "flag", "Division_ID" },
                                          new string[] { "14", ddlDivision.SelectedValue }, "dataset");
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 2", ex.Message.ToString());
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
    private void GetTransporterDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_TransporterMaster",
                            new string[] { "flag", "Office_ID", "TransporterId","AreaId" },
                            new string[] { "1",objdb.Office_ID(),ddlSearchTransporter.SelectedValue,ddlSearchLocation.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertOrUpdateTransporter()
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
                        ds = objdb.ByProcedure("USP_Mst_TransporterMaster",
                            new string[] { "flag", "Transporter_Company", "Contact_Person",
                                        "Contact_Mobile","ContactNo","Email","PAN_Card",
                                        "GSTIN","DivisionId","DistrictId" ,"Block_ID","TownOrVillage","Address",
                                        "Pincode","Bank_ID","BranchName","IFSC_Code","BankAccountNo","Office_ID","CreatedBy", "CreatedBy_IP","VendorTypeId","AreaId" },
                            new string[] { "2", txtTransCompanyName.Text.Trim(),txtContactPersonName.Text.Trim(),
                                           txtMobileNo.Text.Trim(),txtContactNumber.Text,
                                           txtEmailID.Text ,txtPanCard.Text,txtGSTIN.Text,
                                           ddlDivision.SelectedValue,
                                           ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text.Trim(),txtAddress.Text.Trim(),
                                          txtPincode.Text,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim(),txtBankAccountNo.Text.Trim(),
                                          objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),ddlVendorType.SelectedValue,ddlLocation.SelectedValue }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                         
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "PAN Number "+ txtPanCard.Text.Trim()+" " + error);
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
                        ds = objdb.ByProcedure("USP_Mst_TransporterMaster",
                            new string[] { "flag",
                                "TransporterId", "Transporter_Company", "Contact_Person",
                                        "Contact_Mobile","ContactNo","Email","PAN_Card",
                                        "GSTIN","DivisionId","DistrictId","Block_ID","TownOrVillage","Address",
                                        "Pincode","Bank_ID","BranchName","IFSC_Code","BankAccountNo","Office_ID","CreatedBy", "CreatedBy_IP"
                                , "PageName", "Remark","VendorTypeId","AreaId" },
                            new string[] { "3", ViewState["rowid"].ToString(),
                                txtTransCompanyName.Text.Trim(),txtContactPersonName.Text.Trim(),
                                           txtMobileNo.Text.Trim(),txtContactNumber.Text,
                                           txtEmailID.Text ,txtPanCard.Text,txtGSTIN.Text,
                                          ddlDivision.SelectedValue,
                                           ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text.Trim(),
                                           txtAddress.Text.Trim(),
                                          txtPincode.Text,ddlBank.SelectedValue,txtBranchName.Text.Trim()
                                          ,txtIFSCCode.Text.Trim(),txtBankAccountNo.Text.Trim(),
                                          objdb.Office_ID(), objdb.createdBy(),
                                         objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                         , Path.GetFileName(Request.Url.AbsolutePath)
                                         , "Transporter registration record Updated",ddlVendorType.SelectedValue,ddlLocation.SelectedValue }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetTransporterDetails();
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
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
    #endregion====================================end of user defined function



    #region=============== changed event for init function for controls =================

    //protected void ddlState_Init(object sender, EventArgs e)
    //{
    //    GetState();
    //}
    protected void ddlDivision_Init(object sender, EventArgs e)
    {
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void ddlBank_Init(object sender, EventArgs e)
    {
        GetBank();
    }
    protected void ddlDistrict_Init(object sender, EventArgs e)
    {
        GetDivision();
    }
    //protected void ddlBranchName_Init(object sender, EventArgs e)
    //{
    //    ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
    //}
 
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrictByDivison();
        GetDatatableHeaderDesign();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBlock();
        GetDatatableHeaderDesign();
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBank.SelectedIndex != 0)
        {
            pnlBankName.Visible = true;
            pnlNewIFSC.Visible = true;
            pnlAccntNo.Visible = true;

            RequiredFieldValidator10.Enabled = true;
            RequiredFieldValidator9.Enabled = true;
            RequiredFieldValidator11.Enabled = true;
            GetDatatableHeaderDesign();
        }

        else
        {
            pnlBankName.Visible = false;
            pnlNewIFSC.Visible = false;
            pnlAccntNo.Visible = false;

            RequiredFieldValidator10.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            RequiredFieldValidator11.Enabled = false;

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
        InsertOrUpdateTransporter();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    #endregion=============end of button click funciton==================

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    btnSubmit.Text = "Update";
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblVendorTypeId = (Label)row.FindControl("lblVendorTypeId");
                    Label lblDivisionId = (Label)row.FindControl("lblDivisionId");
                    Label lblDistrictId = (Label)row.FindControl("lblDistrictId");
                    Label lblBlock_ID = (Label)row.FindControl("lblBlock_ID");
                    Label lblTownOrVillage = (Label)row.FindControl("lblTownOrVillage");
                    Label lblAddress = (Label)row.FindControl("lblAddress");
                    Label lblPincode = (Label)row.FindControl("lblPincode");
                    Label lblTransporter_Company = (Label)row.FindControl("lblTransporter_Company");
                    Label lblContact_Person = (Label)row.FindControl("lblContact_Person");
                    Label lblContact_Mobile = (Label)row.FindControl("lblContact_Mobile");
                    Label lblEmail = (Label)row.FindControl("lblEmail");
                    Label lblPAN_Card = (Label)row.FindControl("lblPAN_Card");
                    Label lblGSTIN = (Label)row.FindControl("lblGSTIN");

                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                    // Label lblBranch_id = (Label)row.FindControl("lblBranch_id");
                    Label lblBranchName = (Label)row.FindControl("lblBranchName");
                    Label lblIFSCCode = (Label)row.FindControl("lblIFSCCode");
                    Label lblBankAccountNo = (Label)row.FindControl("lblBankAccountNo");
                    Label lblAreaId = (Label)row.FindControl("lblAreaId");

                    txtTransCompanyName.Text = lblTransporter_Company.Text;
                    txtContactPersonName.Text = lblContact_Person.Text;
                    txtMobileNo.Text = lblContact_Mobile.Text;
                    txtEmailID.Text = lblEmail.Text;
                    txtPanCard.Text = lblPAN_Card.Text;
                    txtGSTIN.Text = lblGSTIN.Text;
                   
                    GetDivision();
                    ddlDivision.SelectedValue = lblDivisionId.Text; 
                    GetDistrictByDivison();
                    ddlDistrict.SelectedValue = lblDistrictId.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlock_ID.Text;
                    txtTownOrvillage.Text = lblTownOrVillage.Text;
                    txtAddress.Text = lblAddress.Text;
                    txtPincode.Text = lblPincode.Text;
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

                     ddlVendorType.SelectedValue = lblVendorTypeId.Text;
                     ddlLocation.SelectedValue = lblAreaId.Text;
                     ViewState["rowid"] = e.CommandArgument;
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
                    ds = objdb.ByProcedure("USP_Mst_TransporterMaster",
                                new string[] { "flag", "TransporterId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Transporter registration record deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        GetTransporterDetails();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetTransporterDetails();
    }
    protected void ddlSearchLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAllTransporter();
    }
}