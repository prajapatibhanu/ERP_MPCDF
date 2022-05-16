using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Admin_AdminVillage : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Village_ID"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    #region=======================user defined function========================
    private void GetState()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminState",
                        new string[] { "flag" },
                        new string[] { "2" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlState_Name.DataTextField = "State_Name";
                ddlState_Name.DataValueField = "State_ID";
                ddlState_Name.DataSource = ds;
                ddlState_Name.DataBind();
                ddlState_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetDivision()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminDivision", new string[] { "flag", "State_ID" }, new string[] { "7", ddlState_Name.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDivision_Name.DataTextField = "Division_Name";
                ddlDivision_Name.DataValueField = "Division_ID";
                ddlDivision_Name.DataSource = ds;
                ddlDivision_Name.DataBind();
                ddlDivision_Name.Items.Insert(0, "Select");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetDistrict()
    {
        try
        {
            lblMsg.Text = "";
            ddlDistrict_Name.ClearSelection();
            string DivisionID = ddlDivision_Name.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpAdminDistrict",
                           new string[] { "flag", "Division_ID" },
                           new string[] { "2", DivisionID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDistrict_Name.DataTextField = "District_Name";
                ddlDistrict_Name.DataValueField = "District_ID";
                ddlDistrict_Name.DataSource = ds;
                ddlDistrict_Name.DataBind();
                ddlDistrict_Name.Items.Insert(0, "Select");
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
            string District_ID = ddlDistrict_Name.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpAdminBlock",
                           new string[] { "flag", "District_ID" },
                           new string[] { "2", District_ID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBlock_Name.DataTextField = "Block_Name";
                ddlBlock_Name.DataValueField = "Block_ID";
                ddlBlock_Name.DataSource = ds;
                ddlBlock_Name.DataBind();
                ddlBlock_Name.Items.Insert(0, "Select");
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
            GridView1.DataSource = objdb.ByProcedure("SpAdminVillage",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddlState_Name.ClearSelection();
        ddlDivision_Name.ClearSelection();
        ddlDistrict_Name.ClearSelection();
        ddlBlock_Name.ClearSelection();
        txtVillage_Name.Text = "";
        ViewState["Village_ID"] = "0";
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void InsertorUpdateVillageReg()
    {
        lblMsg.Text = "";
        string msg = "";
        string Village_IsActive = "1";
        if (Page.IsValid)
        {
            try
            {
                if (msg.Trim() == "")
                {
                    if (btnSubmit.Text == "Save" && ViewState["Village_ID"].ToString() == "0")
                    {
                        ds = objdb.ByProcedure("SpAdminVillage",
                    new string[] { "flag", "Village_IsActive", "Village_Name", "State_ID", "Division_ID", "District_ID", "Block_ID", "Village_UpdatedBy" },
                    new string[] { "0", Village_IsActive, txtVillage_Name.Text, ddlState_Name.SelectedValue.ToString(), ddlDivision_Name.SelectedValue.ToString(), ddlDistrict_Name.SelectedValue.ToString(), ddlBlock_Name.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            FillGrid();
                            ClearText();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            FillGrid();
                        }
                    }
                    else if (btnSubmit.Text == "Edit" && ViewState["Village_ID"].ToString() != "0")
                    {
                        ds = objdb.ByProcedure("SpAdminVillage",
                      new string[] { "flag", "Village_ID", "Village_Name", "State_ID", "Division_ID", "District_ID", "Block_ID", "Village_UpdatedBy" },
                      new string[] { "2", ViewState["rowid"].ToString(), txtVillage_Name.Text, ddlState_Name.SelectedValue.ToString(), ddlDivision_Name.SelectedValue.ToString(), ddlDistrict_Name.SelectedValue.ToString(), ddlBlock_Name.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            FillGrid();
                            ClearText();
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
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    #endregion====================================end of user defined function

    #region=============== changed event for controls =================
    protected void ddlState_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDivision();
    }
    protected void ddlDivision_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrict();
    }
    protected void ddlDistrict_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBlock();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
                    Label lblVillageID = (Label)row.FindControl("lblVillageID");
                    Label lblStateID = (Label)row.FindControl("lblStateID");
                    Label lblDivisionID = (Label)row.FindControl("lblDivisionID");
                    Label lblDistrictID = (Label)row.FindControl("lblDistrictID");
                    Label lblBlockID = (Label)row.FindControl("lblBlockID");

                    Label lblStateName = (Label)row.FindControl("lblStateName");
                    Label lblDivisionName = (Label)row.FindControl("lblDivisionName");
                    Label lblDistrictName = (Label)row.FindControl("lblDistrictName");
                    Label lblBlockName = (Label)row.FindControl("lblBlockName");
                    Label lblVillageName = (Label)row.FindControl("lblVillageName");
                    ddlState_Name.SelectedValue = lblStateID.Text;
                    GetDivision();
                    ddlDivision_Name.SelectedValue = lblDivisionID.Text;
                    GetDistrict();
                    ddlDistrict_Name.SelectedValue = lblDistrictID.Text;
                    GetBlock();
                    ddlBlock_Name.SelectedValue = lblBlockID.Text;
                    txtVillage_Name.Text = lblVillageName.Text;

                    ViewState["rowid"] = e.CommandArgument;
                    ViewState["Village_ID"] = lblVillageID.Text;
                    btnSubmit.Text = "Edit";

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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
        string Village_ID = chk.ToolTip.ToString();
        string Village_IsActive = "0";
        if (chk != null & chk.Checked)
        {
            Village_IsActive = "1";
        }
        objdb.ByProcedure("SpAdminVillage",
                   new string[] { "flag", "Village_IsActive", "Village_ID", "Village_UpdatedBy" },
                   new string[] { "5", Village_IsActive, Village_ID, ViewState["Emp_ID"].ToString() }, "dataset");
    }
    #endregion============ end of changed event for controls===========

    #region============ button click event ============================
    protected void ddlState_Name_Init(object sender, EventArgs e)
    {
        GetState();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateVillageReg();
    }
    #endregion=============end of button click funciton==================
}