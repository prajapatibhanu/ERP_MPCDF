﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_DCSInformation_UnionDistrictMapping : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!Page.IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    lblMsg.Text = "";
                    FillUnion();
                    FillDistrict();
                    FillGrid();

                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillUnion()
    {
        try
        {
            ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_ID.DataSource = ds;
                ddlOffice_ID.DataTextField = "Office_Name";
                ddlOffice_ID.DataValueField = "Office_ID";
                ddlOffice_ID.DataBind();
                ddlOffice_ID.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDistrict()
    {
        try
        {
            ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
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
            string msg = "";
            string IsActive = "1";
            if (ddlOffice_ID.SelectedIndex == 0)
            {
                msg += "Select Milk Union . \\n";
            }
            if (ddlDistrict.SelectedIndex == 0)
            {
                msg += "Select District. \\n";
            }
            if (msg == "")
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag", "Office_ID", "District_ID", "IsActive", "UpdatedBy" }, new string[] { "0", ddlOffice_ID.SelectedValue.ToString(), ddlDistrict.SelectedValue.ToString(), IsActive, objdb.createdBy() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        FillGrid();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                    }
                }
                if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag", "UnionDistrictMapping_ID", "Office_ID", "District_ID", "IsActive", "UpdatedBy" }, new string[] { "4",ViewState["UnionDistrictMapping_ID"].ToString(), ddlOffice_ID.SelectedValue.ToString(), ddlDistrict.SelectedValue.ToString(), IsActive, objdb.createdBy() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        FillGrid();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        btnSave.Text = "Save";

                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Clear()
    {
        try
        {
            ddlOffice_ID.ClearSelection();
            ddlDistrict.ClearSelection();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;

            }
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string UnionDistrictMapping_ID = GridView1.SelectedDataKey.Value.ToString();
            ViewState["UnionDistrictMapping_ID"] = UnionDistrictMapping_ID.ToString();
            ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag", "UnionDistrictMapping_ID" }, new string[] { "2", UnionDistrictMapping_ID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_ID.ClearSelection();
                ddlOffice_ID.Items.FindByValue(ds.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                ddlDistrict.ClearSelection();
                ddlDistrict.Items.FindByValue(ds.Tables[0].Rows[0]["District_ID"].ToString()).Selected = true;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}