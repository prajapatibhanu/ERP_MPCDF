using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Admin_DeptDesigMapping : System.Web.UI.Page
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
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillGrid();
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
    private void GetDepartment()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminDepartment",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDepartment.DataTextField = "Department_Name";
                ddlDepartment.DataValueField = "Department_ID";
                ddlDepartment.DataSource = ds;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetDesignation()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminDesignation",
                        new string[] { "flag" },
                        new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "Designation_ID";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
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
            GridView1.DataSource = objdb.ByProcedure("sp_tblDeptDesgMapping",
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
        ddlDepartment.ClearSelection();
        ddlDesignation.ClearSelection();
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void InsertOrUpdateMapping()
    {
       string IsActive = "1";
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("sp_tblDeptDesgMapping",
                            new string[] { "flag", "Department_ID", "Designation_ID", "IsActive", "Office_ID", "CreatedBy", "CreatedBy_IP" },
                            new string[] { "2", ddlDepartment.SelectedValue, ddlDesignation.SelectedValue, IsActive, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Clear();
                            FillGrid();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            FillGrid();
                        }
                        ds.Clear();
                    }
                    else if (btnSubmit.Text == "Update" && ViewState["DeptDesgMappping_ID"].ToString() != "0")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("sp_tblDeptDesgMapping",
                            new string[] { "flag", "DeptDesgMappping_ID", "Department_ID", "Designation_ID", "Office_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                            new string[] { "3", ViewState["rowid"].ToString(), ddlDepartment.SelectedValue, ddlDesignation.SelectedValue, objdb.Office_ID(), objdb.createdBy() , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Dept. Designation Mapping Record Updated" }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Clear();
                            FillGrid();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            FillGrid();
                        }
                    }
                     else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Something wents wrong!");
                    }
                        ds.Clear();
                    }
                
               Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }

    }
   
    #endregion====================================end of user defined function==========



    #region=============== init or changed event for controls =================

    protected void ddlDepartment_Init(object sender, EventArgs e)
    {
        GetDepartment();
    }
    protected void ddlDesignation_Init(object sender, EventArgs e)
    {
        GetDesignation();
    }
    #endregion============ end of changed event for controls===========

    #region============ button click and gridview events event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateMapping();
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
                    Label lblDeptDesgMapppingID = (Label)row.FindControl("lblDeptDesgMapppingID");
                    Label lblDepartmentName = (Label)row.FindControl("lblDepartmentName");
                    Label lblDepartmentID = (Label)row.FindControl("lblDepartmentID");
                    Label lblDesignationName = (Label)row.FindControl("lblDesignationName");
                    Label lblDesignationID = (Label)row.FindControl("lblDesignationID");

                    ddlDepartment.SelectedValue = lblDepartmentID.Text;
                    ddlDesignation.SelectedValue = lblDesignationID.Text;
                    ViewState["DeptDesgMappping_ID"] = lblDeptDesgMapppingID.Text;
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
                    ds = objdb.ByProcedure("sp_tblDeptDesgMapping",
                                new string[] { "flag", "DeptDesgMappping_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Dept. Designation Mapping Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        FillGrid();
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