using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class mis_EngneeringDepartment_HeadMaster : System.Web.UI.Page
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

                GetRouteHeadMasterDetails();
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
    

    private void Clear()
    {

        txtHeadName.Text = string.Empty;



        btnSubmit.Text = "SUBMIT";
        GridView1.SelectedIndex = -1;
    }
    private void GetRouteHeadMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_ENGHead",
                    new string[] { "flag", "Office_ID", "CreatedBy" },
                    new string[] { "1", objdb.Office_ID(), objdb.createdBy() }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateHeadMaster()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (btnSubmit.Text == "SUBMIT")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_ENGHead",
                            new string[] { "flag", "ENGHeadName", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "2", txtHeadName.Text.Trim(), objdb.Office_ID(), objdb.createdBy(),
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
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Engineering Head " + txtHeadName.Text + " " + error + " Exist.");
                                GetDatatableHeaderDesign();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSubmit.Text == "UPDATE")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_ENGHead",
                            new string[] { "flag", "ENGHeadId", "ENGHeadName", "Office_ID", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                            new string[] { "3", ViewState["rowid"].ToString(),txtHeadName.Text.Trim(), objdb.Office_ID()
                                    , objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress() , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "ENG Head Master Details Updated" }, "dataset");

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
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Engineering Head Name " + txtHeadName.Text + " " + error + " Exist.");
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
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Head Name");
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
                    Label lblHeadName = (Label)row.FindControl("lblHeadName");
                    



                    txtHeadName.Text = lblHeadName.Text;

                   
                    ViewState["rowid"] = e.CommandArgument;
                    btnSubmit.Text = "UPDATE";

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
                     Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblHeadName = (Label)row.FindControl("lblHeadName");
                    LinkButton  lnkDelete=(LinkButton)row.FindControl("lnkDelete");
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    string Status=lnkDelete.Text;
                    if (Status == "Active")
                    {
                        ds = objdb.ByProcedure("USP_Mst_ENGHead",
                                    new string[] { "flag", "ENGHeadId", "CreatedBy", "CreatedByIP", "PageName", "Remark", "IsActive" },
                                    new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , " Head Master Details Deactivate","0" }, "TableSave");
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
                    }
                    else if (Status == "Deactive")
                    {
                        ds = objdb.ByProcedure("USP_Mst_ENGHead",
                                    new string[] { "flag", "ENGHeadId", "CreatedBy", "CreatedByIP", "PageName", "Remark", "IsActive" },
                                    new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , " Head Master Details Activate","1" }, "TableSave");
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
                    }

                    
                    ds.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
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
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    InsertorUpdateRouteMaster();
    //}
    #endregion=============end of button click funciton==================

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        GetDatatableHeaderDesign();
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        GetRouteHeadMasterDetails();
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        { 
        InsertorUpdateHeadMaster();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}