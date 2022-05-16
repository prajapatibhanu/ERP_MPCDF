using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

public partial class mis_Common_MilkRateMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetMilkRateMasterDetails();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateMilkRateMaster();
    }

    private void InsertorUpdateMilkRateMaster()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSave.Text == "Save")
                    {
                        lblMsg.Text = "";
                        DateTime date3 = DateTime.ParseExact(txtEffective_Date.Text, "dd/MM/yyyy", culture);
                        ds = objdb.ByProcedure("SpMilkCollectionRateMaster",
                            new string[] { "flag", "MilkType_id", "Rate", "EffectiveDate" , "Office_ID", "OfficeType_ID", "CreatedBy"
                                , "CreatedBy_IP" },
                            new string[] { "2", ddlMilk_category.SelectedValue ,txtRate.Text.Trim() ,date3.ToString(), objdb.Office_ID(), objdb.UserTypeID(), objdb.createdBy(),
                                            objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            //GetRouteMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetMilkRateMasterDetails();
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
                        ds.Clear();
                    }
                    //if (btnSave.Text == "Update")
                    //{
                    //    lblMsg.Text = "";
                    //    DateTime date3 = DateTime.ParseExact(txtEffective_Date.Text, "dd/MM/yyyy", culture);
                    //    ds = objdb.ByProcedure("SpMilkCollectionRateMaster",
                    //        new string[] { "flag", "MilkRateId", "MilkType_id", "Rate", "EffectiveDate", "Office_ID", "OfficeType_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                    //        new string[] { "5", ViewState["rowid"].ToString(),ddlMilk_category.SelectedValue ,txtRate.Text.Trim(), date3.ToString(), objdb.Office_ID(), objdb.OfficeType_ID()
                    //                , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() , Path.GetFileName(Request.Url.AbsolutePath)
                    //                , "Milk Rate Master Details Updated" }, "dataset");

                    //    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    //    {
                    //        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    //        Clear();
                    //        GetMilkRateMasterDetails();
                    //        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    //    }
                    //    else
                    //    {
                    //        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    //        if (error == "Already Exists.")
                    //        {
                    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                    //        }
                    //        else
                    //        {
                    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    //        }
                    //    }
                    //    ds.Clear();
                    //}

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Route Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }

    #region=======================user defined function========================

    private void GetMilk_category()
    {
        try
        {
            ddlMilk_category.DataSource = objdb.ByProcedure("SpMilkCollectionRateMaster",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            ddlMilk_category.DataTextField = "MilkTypeName";
            ddlMilk_category.DataValueField = "MilkType_id";
            ddlMilk_category.DataBind();
            ddlMilk_category.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        txtRate.Text = string.Empty;
        txtEffective_Date.Text = string.Empty;
        ddlMilk_category.ClearSelection();
        btnSave.Text = "Save";
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
            //ddlDS.SelectedValue = objddl.GetOfficeParant_ID().Tables[0].Rows[0]["Office_Parant_ID"].ToString();
            ddlDS.SelectedValue = objdb.Office_ID();
            ddlDS.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //private void GetDCSOfficeDetails()
    //{
    //    try
    //    {
    //        ddlDCS.DataSource = objdb.ByProcedure("SpAdminOffice",
    //                new string[] { "flag" },
    //                new string[] { "1" }, "dataset");
    //        ddlDCS.DataTextField = "Office_Name";
    //        ddlDCS.DataValueField = "Office_ID";
    //        ddlDCS.DataBind();
    //        //ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
    //        ddlDCS.SelectedValue = objdb.Office_ID();
    //        ddlDCS.Enabled = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    private void GetMilkRateMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("SpMilkCollectionRateMaster",
                    new string[] { "flag", "Office_ID" },
                    new string[] { "3", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion=====================end of control======================

    #region=====================Init event for controls===========================

    protected void ddlMilk_category_Init(object sender, EventArgs e)
    {
        GetMilk_category();
    }
    protected void ddlDS_Init(object sender, EventArgs e)
    {
        GetDSOfficeDetails();
    }
    //protected void ddlDCS_Init(object sender, EventArgs e)
    //{
    //    GetDCSOfficeDetails();
    //}

    #endregion=====================end of control======================

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "ViewRow")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    gvPopup_ViewMilkRateDetails.DataSource = objdb.ByProcedure("SpMilkCollectionRateMaster",
                                                                 new string[] { "flag", "MilkRateId" },
                                                                 new string[] { "5", e.CommandArgument.ToString() }, "dataset");
                    gvPopup_ViewMilkRateDetails.DataBind();

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewDetailModal()", true);
                }
            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("SpMilkCollectionRateMaster",
                                new string[] { "flag", "MilkRateId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Milk Rate Master Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetMilkRateMasterDetails();
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
        lblMsg.Text = "";
        Clear();
        gvPopup_ViewMilkRateDetails.SelectedIndex = -1;
        GridView1.SelectedIndex = -1;
    }
}