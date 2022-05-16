using System;
using System.Collections.Generic;
using System.Data;  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Masters_SuperstockistReg : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1=new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetDivision();
                GetSuperStockist_Type();
                GetBank();
                GetSuperStockist();
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
   // #region=======================user defined function========================
    protected void GetSuperStockist()
    {
        try
        {

            ddlSuperStockistName.DataTextField = "SSName";
            ddlSuperStockistName.DataValueField = "SuperStockistId";
            ddlSuperStockistName.DataSource = objdb.ByProcedure("USP_Mst_SuperStockistReg",
                 new string[] { "flag", "Office_ID" },
                   new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlSuperStockistName.DataBind();
            ddlSuperStockistName.Items.Insert(0, new ListItem("All", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void Clear()
    {
       

        pnlBankName.Visible = false;
        pnlNewIFSC.Visible = false;
        pnlAccntNo.Visible = false;
        txtGSTIN.Text = string.Empty;
        txtPanCard.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtBankAccountNo.Text = string.Empty;
        txtSuperStockistName.Text = string.Empty;
        txtSSCode.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtSSContactNo.Text = string.Empty;
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
    private void GetSupersStockistDetails()
    {
        try
        {

            ds1 = objdb.ByProcedure("USP_Mst_SuperStockistReg",
                    new string[] { "flag", "Office_ID", "SuperStockistId" },
                    new string[] { "1", objdb.Office_ID(),ddlSuperStockistName.SelectedValue }, "dataset");
            GridView1.DataSource = ds1.Tables[0];
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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

    private void GetSuperStockist_Type()
    {
        try
        {


            ddlSuperStockistType.DataSource = objdb.ByProcedure("USP_Mst_VendorType",
                       new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            ddlSuperStockistType.DataTextField = "VendorTypeName";
            ddlSuperStockistType.DataValueField = "VendorTypeId";

            ddlSuperStockistType.DataBind();
            ddlSuperStockistType.Items.Insert(0, new ListItem("Select", "0"));



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
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

    private void InsertorUpdateSSReg()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";

                        ds1 = objdb.ByProcedure("USP_Mst_SuperStockistReg",
                            new string[] { "flag","UserTypeId", "SSName","SSCode","SSContactNo", "SSCPersonName"
                                , "SSCPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "SSAddress",
                                "SSPincode","BankAccountNo","Bank_ID","BranchName","IFSC_Code", "VendorTypeId", "PANNo","GSTNo" 
                               , "OfficeType_ID","Office_ID","CreatedBy", "CreatedBy_IP" },
                            new string[] { "2",objddl.GetSuperStockistType_id() , txtSuperStockistName.Text.Trim(),txtSSCode.Text.Trim().ToUpper()
                                ,txtSSContactNo.Text.Trim(),txtContactPerson.Text.Trim() ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim()
                                ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue ,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text
                                ,txtAddress.Text.Trim(),txtPincode.Text.Trim(),txtBankAccountNo.Text.Trim() ,
                                ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim()
                              ,ddlSuperStockistType.SelectedValue ,txtPanCard.Text.Trim(), txtGSTIN.Text.Trim() 
                              , Session["OfficeType_ID"].ToString(),objdb.Office_ID() , objdb.createdBy(), IPAddress }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSupersStockistDetails();
                            GetDatatableHeaderDesign();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            }
                        }
                        ds1.Clear();
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";

                        ds1 = objdb.ByProcedure("USP_Mst_SuperStockistReg",
                            new string[] { "flag","SuperStockistId", "SSName","SSCode","SSContactNo", "SSCPersonName"
                                , "SSCPersonMobileNo","Email", "Division_ID", "District_ID","Block_ID","TownOrVillage", "SSAddress",
                                "SSPincode","BankAccountNo","Bank_ID","BranchName","IFSC_Code", "VendorTypeId", "PANNo","GSTNo" 
                               , "OfficeType_ID","Office_ID","CreatedBy", "CreatedBy_IP","PageName","Remark" },
                            new string[] { "3", ViewState["rowid"].ToString() , txtSuperStockistName.Text.Trim(),txtSSCode.Text.Trim().ToUpper()
                                ,txtSSContactNo.Text.Trim(),txtContactPerson.Text.Trim() ,txtContactPersonMobileNo.Text.Trim(),txtEmail.Text.Trim()
                                ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue ,ddlBlock_Name.SelectedValue,txtTownOrvillage.Text
                                ,txtAddress.Text.Trim(),txtPincode.Text.Trim() ,txtBankAccountNo.Text.Trim() ,ddlBank.SelectedValue,txtBranchName.Text.Trim(),txtIFSCCode.Text.Trim()
                              ,ddlSuperStockistType.SelectedValue ,txtPanCard.Text.Trim(), txtGSTIN.Text.Trim()
                              , Session["OfficeType_ID"].ToString(),objdb.Office_ID()
                               , objdb.createdBy(), IPAddress, Path.GetFileName(Request.Url.AbsolutePath), "SuperStockist Registration Details Updated" }, "TableSave");
                      

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetSupersStockistDetails();
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
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
                if (ds1 != null) { ds1.Dispose(); }
            }
        }
    }  

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
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator9.Enabled = false;

        }
    }



    #region============ button click event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            InsertorUpdateSSReg();
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
                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblBlock_ID = (Label)row.FindControl("lblBlock_ID");
                    Label lblTownOrVillage = (Label)row.FindControl("lblTownOrVillage");
                    Label lblSSName = (Label)row.FindControl("lblSSName");
                    Label lblSSCode = (Label)row.FindControl("lblSSCode");
                    Label lblSSContactNo = (Label)row.FindControl("lblSSContactNo");
                    Label lblSSCPersonName = (Label)row.FindControl("lblSSCPersonName");
                    Label lblSSCPersonMobileNo = (Label)row.FindControl("lblSSCPersonMobileNo");
                    Label lblEmail = (Label)row.FindControl("lblEmail");
                    Label lblSSAddress = (Label)row.FindControl("lblSSAddress");
                    Label lblSSPincode = (Label)row.FindControl("lblSSPincode");
                    Label lblVendorTypeId = (Label)row.FindControl("lblVendorTypeId");
                    Label lblPANNo = (Label)row.FindControl("lblPANNo");
                    Label lblGSTNo = (Label)row.FindControl("lblGSTNo");

                 

                    Label lblBank_id = (Label)row.FindControl("lblBank_id");
                  
                    Label lblBranchName = (Label)row.FindControl("lblBranchName");
                    Label lblIFSCCode = (Label)row.FindControl("lblIFSCCode");
                    Label lblBankAccountNo = (Label)row.FindControl("lblBankAccountNo");


                    txtSuperStockistName.Text = lblSSName.Text;
                    txtSSCode.Text = lblSSCode.Text;
                    txtSSContactNo.Text = lblSSContactNo.Text;
                    txtContactPerson.Text = lblSSCPersonName.Text;
                    txtContactPersonMobileNo.Text = lblSSCPersonMobileNo.Text;
                    txtEmail.Text = lblEmail.Text;
                    txtAddress.Text = lblSSAddress.Text;
                    txtPincode.Text = lblSSPincode.Text;
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlock_ID.Text;
                    txtTownOrvillage.Text = lblTownOrVillage.Text;
                    ddlSuperStockistType.SelectedValue = lblVendorTypeId.Text;
                    txtPanCard.Text = lblPANNo.Text;
                    txtGSTIN.Text = lblGSTNo.Text;
                  
                    GetDatatableHeaderDesign();
                    if (lblBank_id.Text != "" && lblBank_id.Text != "0")
                    {
                        pnlBankName.Visible = true;
                        pnlNewIFSC.Visible = true;
                        pnlAccntNo.Visible = true;
                        ddlBank.SelectedValue = lblBank_id.Text;
                        txtBranchName.Text = lblBranchName.Text;
                        txtIFSCCode.Text = lblIFSCCode.Text;
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
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds1 = objdb.ByProcedure("USP_Mst_SuperStockistReg",
                                new string[] { "flag", "SuperStockistId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , IPAddress
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Super Stockist Registration Details Deleted" }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetSupersStockistDetails();
                        GetDatatableHeaderDesign();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds1.Clear();
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
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetSupersStockistDetails();
        }
    }
    #endregion=============end of button click funciton==================



   
}