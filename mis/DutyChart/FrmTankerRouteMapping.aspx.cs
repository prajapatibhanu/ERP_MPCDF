using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class mis_DutyChart_FrmTankerRouteMapping : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillDropDown();
                FillGrid();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "OfficeID" }, new string[] { "6", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlTankerNo.DataTextField = "V_VehicleNo";
                ddlTankerNo.DataValueField = "I_TankerID";
                ddlTankerNo.DataSource = ds;
                ddlTankerNo.DataBind();
                ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds.Clear();
            ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "OfficeID" }, new string[] { "7", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRouteNo.DataTextField = "BMCTankerRootName";
                ddlRouteNo.DataValueField = "BMCTankerRoot_Id";
                ddlRouteNo.DataSource = ds;
                ddlRouteNo.DataBind();
                ddlRouteNo.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds.Clear();
            ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "OfficeID" }, new string[] { "8", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDriver.DataTextField = "Driver_Name";
                ddlDriver.DataValueField = "Driver_ID";
                ddlDriver.DataSource = ds;
                ddlDriver.DataBind();
                ddlDriver.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GvMappingDetail.DataSource = null;
            GvMappingDetail.DataBind();
            ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "OfficeID" },
                new string[] { "4", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvMappingDetail.DataSource = ds;
                    GvMappingDetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void GvMappingDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["MappingID"] = e.CommandArgument.ToString();
            ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "MappingID" },
                        new string[] { "5", ViewState["MappingID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlTankerNo.ClearSelection();
                ddlTankerNo.Items.FindByValue(ds.Tables[0].Rows[0]["TankerID"].ToString()).Selected = true;
                ddlRouteNo.ClearSelection();
                ddlRouteNo.Items.FindByValue(ds.Tables[0].Rows[0]["RouteID"].ToString()).Selected = true;
                ddlDriver.ClearSelection();
                ddlDriver.Items.FindByValue(ds.Tables[0].Rows[0]["DriverID"].ToString()).Selected = true;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "OfficeID", "TankerID", "RouteID", "DriverID", "CreatedBy", "CreatedIP" },
                        new string[] { "1", objdb.Office_ID(), ddlTankerNo.SelectedValue.ToString(), ddlRouteNo.SelectedValue.ToString(), ddlDriver.SelectedValue.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Danger.ToString());
                            }
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("Sp_tblDutyChart_TankerRouteMapping", new string[] { "flag", "MappingID", "TankerID", "RouteID", "DriverID", "CreatedBy", "CreatedIP" },
                        new string[] { "2", ViewState["MappingID"].ToString(), ddlTankerNo.SelectedValue.ToString(), ddlRouteNo.SelectedValue.ToString(), ddlDriver.SelectedValue.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Danger.ToString());
                            }
                        }
                    }
                }
                FillGrid();
                ddlTankerNo.ClearSelection();
                ddlRouteNo.ClearSelection();
                ddlDriver.ClearSelection();
                btnSave.Text = "Save";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}