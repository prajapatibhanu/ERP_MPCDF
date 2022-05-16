using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;

public partial class mis_Common_RouteMaster : System.Web.UI.Page
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
               
                GetLocation();
                GetLocationSearch();
               // GetRouteMasterDetails();
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
    #region=======================user defined function========================
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
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetLocationSearch()
    {
        try
        {
            ddlLocationSearch.DataTextField = "AreaName";
            ddlLocationSearch.DataValueField = "AreaId";
            ddlLocationSearch.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocationSearch.DataBind();
            ddlLocationSearch.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void Clear()
    {

        txtRouteName.Text = string.Empty;
        txtRouteNo.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        txtSequenctNo.Text = string.Empty;

        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void GetRouteMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_Route",
                    new string[] { "flag", "Office_ID","AreaId" },
                    new string[] { "1", objdb.Office_ID(),ddlLocationSearch.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateRouteMaster()
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
                        ds = objdb.ByProcedure("USP_Mst_Route",
                            new string[] { "flag", "RouteName", "RouteNumber", "Office_ID", "OfficeType_ID", "CreatedBy"
                                , "CreatedBy_IP","AreaId","SequenceNo" },
                            new string[] { "2", txtRouteName.Text.Trim() ,txtRouteNo.Text.Trim(), objdb.Office_ID(), objdb.OfficeType_ID(), objdb.createdBy(),
                                            objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),ddlLocation.SelectedValue,txtSequenctNo.Text.Trim() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetRouteMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Route Number " + txtRouteNo.Text + " " + error + " Exist.");
                                GetDatatableHeaderDesign();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSave.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_Route",
                            new string[] { "flag", "RouteId", "RouteName", "RouteNumber", "Office_ID", "OfficeType_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark", "AreaId", "SequenceNo" },
                            new string[] { "3", ViewState["rowid"].ToString(),txtRouteName.Text.Trim() ,txtRouteNo.Text.Trim(), objdb.Office_ID(),objdb.OfficeType_ID()
                                    , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Route Master Details Updated",ddlLocation.SelectedValue ,txtSequenctNo.Text.Trim()}, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetRouteMasterDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Route Number " + txtRouteNo.Text +" "+ error + " Exist.");
                                GetDatatableHeaderDesign();
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
    #endregion====================================end of user defined function

    #region=============== changed event for controls =================
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
                    Label lblRoute_Name = (Label)row.FindControl("lblRoute_Name");
                    Label lblRoute_Number = (Label)row.FindControl("lblRoute_Number");
                    Label lblAreaId = (Label)row.FindControl("lblAreaId");
                    Label lblSequenceNo = (Label)row.FindControl("lblSequenceNo");



                    txtRouteName.Text = lblRoute_Name.Text;
                    txtRouteNo.Text = lblRoute_Number.Text;
                    txtSequenctNo.Text = lblSequenceNo.Text;
                    ViewState["rowid"] = e.CommandArgument;
                    if (lblAreaId.Text == "" || lblAreaId.Text==null)
                    {
                        ddlLocation.SelectedValue = "0";
                    }
                    ddlLocation.SelectedValue = lblAreaId.Text;
                    btnSave.Text = "Update";

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
                    ds = objdb.ByProcedure("USP_Mst_Route",
                                new string[] { "flag", "RouteId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Route Master Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetRouteMasterDetails();
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
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateRouteMaster();
    }
    #endregion=============end of button click funciton==================

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        GetDatatableHeaderDesign();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetRouteMasterDetails();
        }
    }
}