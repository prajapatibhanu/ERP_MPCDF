using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;

public partial class mis_Common_VehicleRouteMapping : System.Web.UI.Page
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
                VehicleRouteMappingDetails();
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
    protected void GetRoute()
    {
        try
        {
            ds = objdb.ByProcedure("SpRouteMaster",
                       new string[] { "flag" },
                       new string[] { "5" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRoute.DataTextField = "RouteNumber";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds;
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetVehicle()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_tblVehicleMaster",
                       new string[] { "flag" },
                       new string[] { "5" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlVehicle.DataTextField = "Vehicle_RC_Number";
                ddlVehicle.DataValueField = "VehicleTypeMaster_ID";
                ddlVehicle.DataSource = ds;
                ddlVehicle.DataBind();
                ddlVehicle.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void VehicleRouteMappingDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("Sp_VehicleRouteMapping",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        ddlRoute.ClearSelection();
        ddlRoute.ClearSelection();
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void InsertorUpdateVehicleRouteMapping()
    {
        lblMsg.Text = "";
        string Is_Active = "1";
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        ds = objdb.ByProcedure("Sp_VehicleRouteMapping",
                            new string[] { "flag", "RouteId", "VehicleType_ID", "Is_Active", "CreatedBy", "CreatedBy_IP" },
                            new string[] { "2", ddlRoute.SelectedValue, ddlVehicle.SelectedValue, Is_Active, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            VehicleRouteMappingDetails();
                            Clear();
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
                    if (btnSubmit.Text == "Update")
                    {
                        ds = objdb.ByProcedure("Sp_VehicleRouteMapping",
                             new string[] { "flag", "VehicleRouteMapping_ID", "RouteId", "VehicleType_ID", "Is_Active", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                             new string[] { "3",ViewState["rowid"].ToString(), ddlRoute.SelectedValue, ddlVehicle.SelectedValue, Is_Active,  objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Office Registration Details Updated"}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            VehicleRouteMappingDetails();
                            Clear();
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

    #region=============== init or changed event for controls =================
    protected void ddlRoute_Init(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void ddlVehicle_Init(object sender, EventArgs e)
    {
        GetVehicle();
    }
    #endregion============ end of changed event for controls===========

    #region============ button click and gridview events event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateVehicleRouteMapping();
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
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblVehicleRouteMappingID = (Label)row.FindControl("lblVehicleRouteMappingID");
                    Label lblRouteNumber = (Label)row.FindControl("lblRouteNumber");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblVehicleRCNumber = (Label)row.FindControl("lblVehicleRCNumber");
                    Label lblVehicleTypeID = (Label)row.FindControl("lblVehicleTypeID");

                    ddlRoute.SelectedValue = lblRouteId.Text;
                    GetVehicle();
                    ddlVehicle.SelectedValue = lblVehicleTypeID.Text;
                    ViewState["VehicleRouteMapping_ID"] = lblVehicleRouteMappingID.Text;
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
                    ds = objdb.ByProcedure("Sp_VehicleRouteMapping",
                                new string[] { "flag", "VehicleRouteMapping_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Dept. Designation Mapping Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        VehicleRouteMappingDetails();
                        Clear();
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
    #endregion=============end of button click function==================

}