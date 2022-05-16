﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_RptSheetProduct : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillProductSection();
                FillDropdown();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillDropdown()
    {
        try
        {
            //ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            //ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlShift.DataSource = ds;
            //    ddlShift.DataTextField = "ShiftName";
            //    ddlShift.DataValueField = "Shift_id";
            //    ddlShift.DataBind();
            //    ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProductSection()
    {
        try
        {

            //lblMsg.Text = "";
            //ddlProductSection.Items.Clear();
            //ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
            //     new string[] { "flag", "Office_ID" },
            //     new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlProductSection.DataSource = ds.Tables[0];
            //    ddlProductSection.DataTextField = "ProductSection_Name";
            //    ddlProductSection.DataValueField = "ProductSection_ID";
            //    ddlProductSection.DataBind();

            //}
            //ddlProductSection.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillProductSection();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

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
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
            //lblSeletedInfo.Text = "";
            //ViewState["SelectedOffice"] = ddlDS.SelectedValue.ToString();
            //ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            //ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            //ViewState["Shift_ID"] = ddlShift.SelectedValue.ToString();

            //ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "ReceiverOffice_ID", "ReceiverSection_ID", "TranDt", "Shift_ID" }, new string[] { "10", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString() }, "dataset");
            //if (ds != null && ds.Tables[0].Rows.Count != 0)
            //{
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    GridView1.UseAccessibleHeader = true;

            //}
            //if (ds != null)
            //{
            //    //btnSave.Visible = true;
            //}
            ///********************************/

            //lblSeletedInfo.Text = "<b>Dugdh Sangh :</b> " + ddlDS.SelectedItem.ToString() + " </p> <p><b>Product Section :</b> " + ddlProductSection.SelectedItem.ToString() + " </p> <p><b>Date :</b> " + txtOrderDate.Text.ToString() + " </p><p> <b>Shift :</b>   " + ddlShift.SelectedItem.ToString() + "</p> ";

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
            //lblMsg.Text = "";
            ////ViewState["SelectedOffice"] = ddlProductSection.SelectedValue.ToString();
            ////ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            //    Label ItemName = (Label)row.FindControl("ItemName");
            //    string Item_id = ItemName.ToolTip.ToString();
            //    TextBox LastTarget = (TextBox)row.FindControl("txtTarget");
            //    Label lblGenStatus = (Label)row.FindControl("lblGenStatus");

            //    if (chkSelect.Checked == true && lblGenStatus.Text != "Received")
            //    {
            //        //ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            //        ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "SenderOffice_ID", "ReceiverSection_ID", "TranDt", "Shift_ID", "ReceiverID", "ReceiverOffice_ID", "Item_id" }, new string[] { "4", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), Item_id }, "dataset");
            //    }

            //}


            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            //FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    
}