using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Masters_OrganizationReg : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds,ds1=new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetRetailerType();
                GetOrganizationDetails();
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
        txtOrganizationName.Text = string.Empty;
        ddlDeliveryType.SelectedIndex = 0;
        txtPincode.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContactPersonMobileNo.Text = string.Empty;
        ddlRoute.SelectedIndex = -1;
        txtContactPerson.Text = string.Empty;
        txtTownOrvillage.Text = string.Empty;
       ddlOrganizationType.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        ddlDistrict.SelectedIndex = 0;
        ddlBlock_Name.ClearSelection();
        GetBlock();
        GetDatatableHeaderDesign();
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        txtICode.Text = string.Empty;
    }
    private void GetOrganizationDetails()
    {
        try
        {
          

            ds = objdb.ByProcedure("USP_Mst_Organization",
                    new string[] { "flag", "Office_ID" },
                    new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error1!", ex.Message.ToString());
        }
         finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
                     new string[] { "flag", "Office_ID" },
                     new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlRoute.DataTextField = "RNameOrNo";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error2 !", ex.Message.ToString());
        }
    }
    private void GetRetailerType()
    {
        try
        {

           ds = objdb.ByProcedure("USP_Mst_RetailerType",
                     new string[] { "flag" },
                     new string[] { "2" }, "dataset");
           if (ds.Tables[0].Rows.Count > 0)
           {
               ViewState["RetailerTypeId"] = ds.Tables[0].Rows[0]["RetailerTypeID"];
           
           }
      

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error3 !", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
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
                    new string[] { "10"}, "dataset");
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error4!", ex.Message.ToString());
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error5!", ex.Message.ToString());
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error6!", ex.Message.ToString());
        }
    }
    //private void GetDSOfficeDetails()
   
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
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds1 = objdb.ByProcedure("USP_Mst_Organization",
                            new string[] { "flag","RouteId", "Organization_Type","Organization_Name", "CPersonName"
                                , "CPersonMobileNo", "Division_ID", "District_ID","Block_ID","TownOrVillage", "OAddress",
                                "OPincode","OfficeType_ID","Office_ID","CreatedBy", "CreatedBy_IP" ,"RetailerTypeID","Delivery_Type","ICode"},
                            new string[] { "2",ddlRoute.SelectedValue, ddlOrganizationType.SelectedValue
                                , txtOrganizationName.Text.Trim(),txtContactPerson.Text.Trim()
                                ,txtContactPersonMobileNo.Text.Trim()
                                ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue,
                                ddlBlock_Name.SelectedValue,txtTownOrvillage.Text,txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim()
                              , objdb.OfficeType_ID(),objdb.Office_ID()
                               , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                               ,ViewState["RetailerTypeId"].ToString(),ddlDeliveryType.SelectedValue,txtICode.Text.Trim().ToUpper() }, "TableSave");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetOrganizationDetails();
                            GetDatatableHeaderDesign();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Instituion code already exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error7!", "Error :" + error);
                            }
                        }
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds1 = objdb.ByProcedure("USP_Mst_Organization",
                            new string[] { "flag","OrganizationID","RouteId","Organization_Type","Organization_Name","CPersonName"
                                , "CPersonMobileNo", "Delivery_Type","Division_ID", "District_ID","Block_ID","TownOrVillage", "OAddress",
                                "OPincode","Office_ID","CreatedBy", "CreatedBy_IP" ,"PageName","Remark","ICode" },
                            new string[] { "3", ViewState["rowid"].ToString(),ddlRoute.SelectedValue
                                ,ddlOrganizationType.SelectedValue
                                ,txtOrganizationName.Text.Trim()
                                 ,txtContactPerson.Text.Trim(),txtContactPersonMobileNo.Text
                                 ,ddlDeliveryType.SelectedValue
                                ,ddlDivision.SelectedValue,ddlDistrict.SelectedValue,ddlBlock_Name.SelectedValue
                                ,txtTownOrvillage.Text.Trim(),txtAddress.Text.Trim()
                               ,txtPincode.Text.Trim(),
                               objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                , Path.GetFileName(Request.Url.AbsolutePath), "Organization Registration Details Updated",txtICode.Text.Trim().ToUpper()}, "dataset");

                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetOrganizationDetails();
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Instituion code already exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error8 :" + error);
                            }
                        }
                       
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error10!", ex.Message.ToString());
            }
            finally
            {
                if (ds1 != null) ds1.Dispose();
            }
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
   
  
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
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

                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblOrganization_Type = (Label)row.FindControl("lblOrganization_Type");
                    Label lblRetailerTypeID = (Label)row.FindControl("lblRetailerTypeID");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblOrganization_Name = (Label)row.FindControl("lblOrganization_Name");
                    Label lblBlock_ID = (Label)row.FindControl("lblBlock_ID");
                    Label lblTownOrVillage = (Label)row.FindControl("lblTownOrVillage");
                    Label lblCPersonName = (Label)row.FindControl("lblCPersonName");
                    Label lblCPersonMobileNo = (Label)row.FindControl("lblCPersonMobileNo");
                    
                    Label lblBAddress = (Label)row.FindControl("lblBAddress");
                    Label lblBPincode = (Label)row.FindControl("lblBPincode");
                    Label lblDeliveryType = (Label)row.FindControl("lblDeliveryType");
                    Label lblICode = (Label)row.FindControl("lblICode");

                 

                    ddlRoute.SelectedValue = lblRouteId.Text;
                    ddlOrganizationType.SelectedValue = lblOrganization_Type.Text;
                    txtOrganizationName.Text = lblOrganization_Name.Text;
                  
                    txtContactPerson.Text = lblCPersonName.Text;
                    txtContactPersonMobileNo.Text = lblCPersonMobileNo.Text;
                   ddlDeliveryType.SelectedValue=lblDeliveryType.Text;
                    txtAddress.Text = lblBAddress.Text;
                    txtPincode.Text = lblBPincode.Text;
                   
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    GetDistrict();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlock_ID.Text;
                    txtTownOrvillage.Text = lblTownOrVillage.Text;
                    txtICode.Text = lblICode.Text;

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
                    ds = objdb.ByProcedure("USP_Mst_Organization",
                                new string[] { "flag", "OrganizationId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Organization Registration Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetOrganizationDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error11 :" + error);
                    }
                    ds.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry Error12!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDelliveryTypeName = e.Row.FindControl("lblDelliveryTypeName") as Label;

                if (lblDelliveryTypeName.Text == "1")
                {
                    lblDelliveryTypeName.Text = "Self";
                }

                else if (lblDelliveryTypeName.Text == "8")
                {
                    lblDelliveryTypeName.Text = "Distributor/Super Stockist";
                }
                else if (lblDelliveryTypeName.Text == "9")
                {
                    lblDelliveryTypeName.Text = "Sub Distributor";
                }
                else
                {
                    lblDelliveryTypeName.Text = "";

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    #endregion=============end of button click funciton==================



   
}