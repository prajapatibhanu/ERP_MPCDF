﻿using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_UMRoleFormMap : System.Web.UI.Page
{

    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divGrid.Visible = false;
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            ds = objdb.ByProcedure("SpUMRoleMaster",
                        new string[] { "flag" },
                        new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRole_Name.DataTextField = "Role_Name";
                ddlRole_Name.DataValueField = "Role_ID";
                ddlRole_Name.DataSource = ds;
                ddlRole_Name.DataBind();
                ddlRole_Name.Items.Insert(0, "Select");
            }
            ds = objdb.ByProcedure("SpUMModuleMaster",
                        new string[] { "flag" },
                        new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlModule_Name.DataTextField = "Module_Name";
                ddlModule_Name.DataValueField = "Module_ID";
                ddlModule_Name.DataSource = ds;
                ddlModule_Name.DataBind();
                ddlModule_Name.Items.Insert(0, "Select");
            }

        }
    }
    protected void ddlModule_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ddlRole_Name.ClearSelection();
        divGrid.Visible = false;
        if (ddlModule_Name.SelectedIndex > 0)
        {
            divGrid.Visible = true;
            string ModuleID = ddlModule_Name.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpUMRoleFormMap",
                            new string[] { "flag", "Module_ID" },
                            new string[] { "3", ModuleID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = true;
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool minoneselect = false;
            int RowNo = 0;

            ds = objdb.ByProcedure("SpUMRoleFormMap",
                new string[] { "flag", "Role_ID", "Module_ID" },
                new string[] { "2", ddlRole_Name.SelectedValue.ToString(), ddlModule_Name.SelectedValue }, "dataset");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {

                CheckBox chk = (CheckBox)gvrow.Cells[0].FindControl("chkSelect") as CheckBox;
                if (chk.Checked)
                {
                    minoneselect = true;
                    HiddenField FormID = (HiddenField)gvrow.FindControl("hdnForm_ID");
                    string RoleID = ddlRole_Name.SelectedValue.ToString();


                    objdb.ByProcedure("SpUMRoleFormMap",
                        new string[] { "flag", "Form_ID", "Role_ID", "RoleFormMap_UpdatedBy" },
                        new string[] { "0", FormID.Value, RoleID, ViewState["Emp_ID"].ToString() }, "dataset");
                    RowNo++;
                    divGrid.Visible = true;
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    minoneselect = true;
                    RowNo++;
                    divGrid.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlRole_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                lblMsg.Text = "";
                HiddenField hdnForm_ID = (HiddenField)row.FindControl("hdnForm_ID");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                ds = objdb.ByProcedure("SpUMRoleFormMap",
                   new string[] { "flag", "Role_ID", "Form_ID" },
                   new string[] { "4", ddlRole_Name.SelectedValue.ToString(), hdnForm_ID.Value }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Checked = false;
                }
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}