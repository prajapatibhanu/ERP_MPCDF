using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;

public partial class mis_Common_ProducerMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeTypeName();
                GetProdcuerDetails();
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

    private void GetProdcuerDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("SptblPUProducerReg",
                        new string[] { "flag", "Office_ID" },
                        new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void FillDivision()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminDivision",
                        new string[] { "flag", "State_ID" },
                        new string[] { "7", "12" }, "dataset"); //for Madhya Pradesh State only

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDivision_Name.DataSource = ds;
                ddlDivision_Name.DataTextField = "Division_Name";
                ddlDivision_Name.DataValueField = "Division_ID";
                ddlDivision_Name.DataBind();
                ddlDivision_Name.Items.Insert(0, new ListItem("Select", "0"));
                ddlDivision_Name.ClearSelection();
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
            if (ddlDivision_Name.SelectedValue != "0")
            {
                ddlDistrict.DataSource = objdb.ByProcedure("SpAdminDistrict",
                           new string[] { "flag", "Division_ID" },
                           new string[] { "9", ddlDivision_Name.SelectedValue }, "dataset");
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                ddlDistrict.Focus();
            }
            else
            {
                ddlDistrict.DataSource = string.Empty;
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
    private void GetOfficeTypeName()
    {
        try
        {
            if (ds != null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("SpAdminOfficeType",
                    new string[] { "flag", "OfficeType_ID" },
                    new string[] { "1", objdb.UserTypeID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeTypeName.Text = ds.Tables[0].Rows[0]["OfficeTypeName"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void GetDSOfficeDetails()
    {
        try
        {
            ddlDS.DataSource = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag" },
                    new string[] { "1" }, "dataset");
            ddlDS.DataTextField = "Office_Name";
            ddlDS.DataValueField = "Office_ID";
            ddlDS.DataBind();
            //ddlDS.Items.Insert(0, new ListItem("Select", "0"));
            ddlDS.SelectedValue = objddl.GetOfficeParant_ID().Tables[0].Rows[0]["Office_Parant_ID"].ToString();
            ddlDS.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void GetDCSOfficeDetails()
    {
        try
        {
            ddlDCS.DataSource = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag" },
                    new string[] { "1" }, "dataset");
            ddlDCS.DataTextField = "Office_Name";
            ddlDCS.DataValueField = "Office_ID";
            ddlDCS.DataBind();
            //ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
            ddlDCS.SelectedValue = objdb.Office_ID();
            ddlDCS.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void GetGender()
    {
        try
        {
            ddlGender.DataSource = objddl.GenderFill();
            ddlGender.DataTextField = "Gender";
            ddlGender.DataValueField = "Gend_id";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    private void GetCasteCategory()
    {
        try
        {
            ddlCategory.DataSource = objddl.CasteCategoryFill();
            ddlCategory.DataTextField = "CasteCategory";
            ddlCategory.DataValueField = "CasteCat_id";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
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
            if (ds != null)
            {
                ds.Clear();
            }
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

    private void ClearAll()
    {
        txtAadhaarNo.Text = "";
        txtAddress.Text = "";
        txtBankAccountNo.Text = "";
        txtDOB.Text = "";
        txtIFSCCode.Text = "";
        txtPinCode.Text = "";
        txtProducerName.Text = "";
        txtRationCardNo.Text = "";
        ddlBank.ClearSelection();
        ddlBranchName.ClearSelection();
        ddlBranchName.DataSource = string.Empty;
        ddlBranchName.DataBind();
        ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
        ddlCategory.ClearSelection();
        ddlDistrict.ClearSelection();
        ddlDistrict.DataSource = string.Empty;
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        ddlDivision_Name.ClearSelection();
        ddlGender.ClearSelection();
        ddlMilkCard.ClearSelection();
        GridView1.SelectedIndex = -1;
        txtEmail.Text = string.Empty;
        txtMoibleNo.Text = string.Empty;
        btnSubmit.Text = "Save";
    }
    #endregion====================================end of user defined function

    #region=====================Init event for controls===========================

    protected void ddlGender_Init(object sender, EventArgs e)
    {
        GetGender();
    }
    protected void ddlCategory_Init(object sender, EventArgs e)
    {
        GetCasteCategory();
    }
    protected void ddlBank_Init(object sender, EventArgs e)
    {
        GetBank();
    }
    protected void ddlDS_Init(object sender, EventArgs e)
    {
        GetDSOfficeDetails();
    }
    protected void ddlDCS_Init(object sender, EventArgs e)
    {
        GetDCSOfficeDetails();
    }
    protected void ddlDivision_Name_Init(object sender, EventArgs e)
    {
        FillDivision();
    }

    #endregion=====================end of control======================

    #region=============== changed event for controls =================

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDistrict();
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

    #region============ button click event or gridview event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if (btnSubmit.Text == "Save")
                {
                    if (ds != null) { ds.Clear(); }
                    //Add insert parameter here
                    ds = objdb.ByProcedure("SptblPUProducerReg",
                             new string[] { "flag", "UserTypeId","Name","Mobile","Email", "RationCardNo", "AadharNo"
                                 , "Gend_id","CasteCat_id","DOB","MilkCardStatus"
                                 ,"Branch_id","Bank_AccountNo" ,"Division_ID","District_ID"
                                 ,"PAddress","PPincode","OfficeType_ID","Office_ID"
                                 ,"CreatedBy","CreatedBy_IP"},
                             new string[] { "2",objddl.GetProducerType_id(),txtProducerName.Text.Trim(),txtMoibleNo.Text.Trim(),
                             txtEmail.Text.Trim(),txtRationCardNo.Text.Trim(),
                             txtAadhaarNo.Text.Trim(),ddlGender.SelectedValue,ddlCategory.SelectedValue
                             ,txtDOB.Text.Trim(),ddlMilkCard.SelectedValue,ddlBranchName.SelectedValue
                             ,txtBankAccountNo.Text.Trim(),ddlDivision_Name.SelectedValue,
                             ddlDistrict.SelectedValue,txtAddress.Text.Trim(),txtPinCode.Text.Trim()
                             ,objdb.OfficeType_ID(),objdb.Office_ID(),objdb.createdBy()
                             ,objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()}, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        ClearAll();
                        GetProdcuerDetails();
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }

                    if (ds != null) { ds.Clear(); }

                }
                if (btnSubmit.Text == "Update")
                {
                    if (ds != null) { ds.Clear(); }
                    //Add Update parameter here
                    ds = objdb.ByProcedure("SptblPUProducerReg",
                              new string[] { "flag","ProducerId", "Name","Mobile","Email", "RationCardNo", "AadharNo"
                                 , "Gend_id","CasteCat_id","DOB","MilkCardStatus"
                                 ,"Branch_id","Bank_AccountNo" ,"Division_ID","District_ID"
                                 ,"PAddress","PPincode","OfficeType_ID","Office_ID"
                                 ,"CreatedBy","CreatedBy_IP", "PageName", "Remark"},
                              new string[] { "3",ViewState["rowid"].ToString(),txtProducerName.Text.Trim(),txtMoibleNo.Text.Trim(),
                             txtEmail.Text.Trim(),txtRationCardNo.Text.Trim(),
                             txtAadhaarNo.Text.Trim(),ddlGender.SelectedValue,ddlCategory.SelectedValue
                             ,txtDOB.Text.Trim(),ddlMilkCard.SelectedValue,ddlBranchName.SelectedValue
                             ,txtBankAccountNo.Text.Trim(),ddlDivision_Name.SelectedValue,
                             ddlDistrict.SelectedValue,txtAddress.Text.Trim(),txtPinCode.Text.Trim()
                             ,objdb.UserTypeID(),objdb.Office_ID(),objdb.createdBy()
                             ,objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                              , Path.GetFileName(Request.Url.AbsolutePath), "Producer Record Updated"}, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        ClearAll();
                        GetProdcuerDetails();
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }

                    if (ds != null) { ds.Clear(); }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
    }
    protected void txtMoibleNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (ds != null) { ds.Clear(); }

            ds = objdb.ByProcedure("SptblPUProducerReg",
                        new string[] { "flag", "Mobile" },
                        new string[] { "5", txtMoibleNo.Text }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Mobile No:  " + txtMoibleNo.Text + " has already been taken.");
                txtMoibleNo.Text = string.Empty;
                txtMoibleNo.Focus();
            }
            else
            {
                txtEmail.Focus();
            }

            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void txtRationCardNo_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

        if (ds != null) { ds.Clear(); }

        ds = objdb.ByProcedure("SptblPUProducerReg",
                    new string[] { "flag", "RationCardNo" },
                    new string[] { "7", txtRationCardNo.Text.Trim() }, "dataset");

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Ration CardNo :  " + txtRationCardNo.Text + " has already been taken.");
            txtRationCardNo.Text = string.Empty;
            txtRationCardNo.Focus();
        }
        else
        {
            txtAadhaarNo.Focus();
        }
    }
    protected void txtAadhaarNo_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

        if (ds != null) { ds.Clear(); }

        ds = objdb.ByProcedure("SptblPUProducerReg",
                    new string[] { "flag", "AadharNo" },
                    new string[] { "6", txtAadhaarNo.Text }, "dataset");

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Aadhar No :  " + txtAadhaarNo.Text + " has already been taken.");
            txtAadhaarNo.Text = string.Empty;
            txtAadhaarNo.Focus();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ClearAll();
    }
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

                    ViewState["rowid"] = e.CommandArgument;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblAddress = (Label)row.FindControl("lblAddress");
                    Label lblPincode = (Label)row.FindControl("lblPincode");
                    Label lblBank_AccountNo = (Label)row.FindControl("lblBank_AccountNo");
                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                    Label lblBranch_id = (Label)row.FindControl("lblBranch_id");
                    Label lblIFSC = (Label)row.FindControl("IFSC");
                    Label lblName = (Label)row.FindControl("lblName");
                    Label lblMobile = (Label)row.FindControl("lblMobile");
                    Label lblEmail = (Label)row.FindControl("lblEmail");
                    Label lblRationCardNo = (Label)row.FindControl("lblRationCardNo");
                    Label lblAadharNo = (Label)row.FindControl("lblAadharNo");
                    Label lblGend_id = (Label)row.FindControl("lblGend_id");
                    Label lblCasteCat_id = (Label)row.FindControl("lblCasteCat_id");
                    Label lblMilkCardStatus = (Label)row.FindControl("lblMilkCardStatus");

                    txtProducerName.Text = lblName.Text;
                    txtMoibleNo.Text = lblMobile.Text;
                    txtEmail.Text = lblEmail.Text;
                    txtAadhaarNo.Text = lblAadharNo.Text;
                    txtRationCardNo.Text = lblRationCardNo.Text;
                    ddlCategory.SelectedValue = lblCasteCat_id.Text;
                    ddlGender.SelectedValue = lblGend_id.Text;
                    ddlMilkCard.SelectedValue = lblMilkCardStatus.Text;
                    ddlDivision_Name.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    txtAddress.Text = lblAddress.Text;
                    txtPinCode.Text = lblPincode.Text;
                    if (lblBranch_id.Text != "")
                    {
                        ddlBank.SelectedValue = lblBank_id.Text;
                        GetBranchByBank();
                        ddlBranchName.SelectedValue = lblBranch_id.Text;
                        txtIFSCCode.Text = lblIFSC.Text;
                    }
                   

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
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("SptblPUProducerReg",
                                new string[] { "flag", "ProducerId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Producer Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ClearAll();
                        GetProdcuerDetails();
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
    #endregion=============end of button click funciton==================  
    
}