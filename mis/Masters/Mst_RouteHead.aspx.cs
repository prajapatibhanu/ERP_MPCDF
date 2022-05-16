using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class mis_Masters_Mst_RouteHead : System.Web.UI.Page
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
                GetRouteHead();
              
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
    protected void GetRouteHead()
    {
        try
        {
            ddlRouteHead.DataTextField = "RouteHeadName";
            ddlRouteHead.DataValueField = "RouteHeadId";
            ddlRouteHead.DataSource = objdb.ByProcedure("USP_Mst_RouteHead",
                 new string[] { "flag", "Office_ID" },
                   new string[] { "1",objdb.Office_ID() }, "dataset");
            ddlRouteHead.DataBind();
            ddlRouteHead.Items.Insert(0, new ListItem("All", "0"));
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

        txtRouteHeadName.Text = string.Empty;
      
        txtSequenctNo.Text = string.Empty;

        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void GetRouteHeadMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_RouteHead",
                    new string[] { "flag", "Office_ID", "RouteHeadId" },
                    new string[] { "1", objdb.Office_ID(), ddlRouteHead.SelectedValue }, "dataset");
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
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (btnSave.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_RouteHead",
                            new string[] { "flag", "RouteHeadName","SequenceNo", "Office_ID", "CreatedBy","CreatedByIP" },
                            new string[] { "2", txtRouteHeadName.Text.Trim(),txtSequenctNo.Text.Trim(), objdb.Office_ID(), objdb.createdBy(),
                                            IPAddress + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetRouteHeadMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Route Head " + txtRouteHeadName.Text + " " + error + " Exist.");
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
                        ds = objdb.ByProcedure("USP_Mst_RouteHead",
                            new string[] { "flag", "RouteHeadId", "RouteHeadName", "SequenceNo", "Office_ID", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                            new string[] { "3", ViewState["rowid"].ToString(),txtRouteHeadName.Text.Trim(),txtSequenctNo.Text.Trim(), objdb.Office_ID(),objdb.OfficeType_ID()
                                    , objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress() , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Route Head Master Details Updated" }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetRouteHeadMasterDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Route Number " + txtRouteHeadName.Text + " " + error + " Exist.");
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
                    Label lblRouteHeadName = (Label)row.FindControl("lblRouteHeadName");                  
                    Label lblSequenceNo = (Label)row.FindControl("lblSequenceNo");



                    txtRouteHeadName.Text = lblRouteHeadName.Text;
                  
                    txtSequenctNo.Text = lblSequenceNo.Text;
                    ViewState["rowid"] = e.CommandArgument;
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
                    ds = objdb.ByProcedure("USP_Mst_RouteHead",
                                new string[] { "flag", "RouteHeadId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Route Head Master Details Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetRouteHeadMasterDetails();
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
            GetRouteHeadMasterDetails();
        }
    }
}