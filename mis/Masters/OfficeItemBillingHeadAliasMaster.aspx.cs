using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Masters_OfficeItemBillingHeadAliasMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdb1 = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["ItemBillingHeadAlias_ID"] = "0";
                    FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillHeadName()
    {
        try
        {
            DataSet dsH = objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                new string[] { "flag", "ItemBillingHead_Type" },
                new string[] { "9", ddlItemBillingHead_Type.SelectedValue }, "dataset");
            if (dsH != null && dsH.Tables[0].Rows.Count > 0)
            {
                ddlHeadNameID.DataTextField = "ItemBillingHead_Name";
                ddlHeadNameID.DataValueField = "ItemBillingHead_ID";
                ddlHeadNameID.DataSource = dsH;
                ddlHeadNameID.DataBind();
                ddlHeadNameID.Items.Insert(0, new ListItem("Select", "0"));
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
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
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
            if (ddlHeadNameID.SelectedIndex == 0)
            {
                msg += "Select Item Head ID";
            }
            if (txtItemBillingHeadAliasName.Text.Trim() == "")
            {
                msg += "Enter Item Billing Head Alias Name";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                       new string[] { "flag", "ItemBillingHeadAlias_ID", "ItemBillingHead_ID", "ItemBillingHeadAlias_Name", "ItemBillingHead_Type", "OfficeID" },
                       new string[] { "4", ViewState["ItemBillingHeadAlias_ID"].ToString(), ddlHeadNameID.SelectedValue, txtItemBillingHeadAliasName.Text.Trim(), ddlItemBillingHead_Type.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");


                if (btnSave.Text == "Save" && ViewState["ItemBillingHeadAlias_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                {
                    ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                    new string[] { "flag", "ItemBillingHead_ID", "ItemBillingHeadAlias_Name", "ItemBillingHead_Type", "OfficeID", "IsActive", "CreatedBy", "CreatedByIP" },
                    new string[] { "0", ddlHeadNameID.SelectedValue, txtItemBillingHeadAliasName.Text.Trim(), ddlItemBillingHead_Type.SelectedValue, ViewState["Office_ID"].ToString(), IsActive, ViewState["Emp_ID"].ToString(), objdb1.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thank You!", "Billing Head Alias Name is already exists for this Head type");
                        }
                    }
                }


                else if (btnSave.Text == "Edit" && ViewState["ItemBillingHeadAlias_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                    new string[] { "flag", "ItemBillingHeadAlias_ID", "ItemBillingHead_ID", "ItemBillingHeadAlias_Name", "ItemBillingHead_Type", "CreatedBy", "CreatedByIP" },
                    new string[] { "5", ViewState["ItemBillingHeadAlias_ID"].ToString(), ddlHeadNameID.SelectedValue, txtItemBillingHeadAliasName.Text.Trim(), ddlItemBillingHead_Type.SelectedValue, ViewState["Emp_ID"].ToString(), objdb1.GetLocalIPAddress() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This ItemBillingHead Is Already Exist.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Item Billing Head Alias Name  Is Already Exist');", true);
                }

                ddlHeadNameID.ClearSelection();
                ddlItemBillingHead_Type.ClearSelection();
                txtItemBillingHeadAliasName.Text = "";
                btnSave.Text = "Save";
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string ItemBillingHeadAlias_ID = chk.ToolTip.ToString();
            string IsActive = "0";
            if (chk != null & chk.Checked)
            {
                IsActive = "1";
            }
            objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                       new string[] { "flag", "IsActive", "ItemBillingHeadAlias_ID", "CreatedBy" },
                       new string[] { "6", IsActive, ItemBillingHeadAlias_ID, ViewState["Emp_ID"].ToString() }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["ItemBillingHeadAlias_ID"] = GridView1.SelectedValue.ToString();
            ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadAliasMaster",
                       new string[] { "flag", "ItemBillingHeadAlias_ID" },
                       new string[] { "3", ViewState["ItemBillingHeadAlias_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHeadNameID.ClearSelection();
                ddlItemBillingHead_Type.SelectedValue = ds.Tables[0].Rows[0]["ItemBillingHead_Type"].ToString();
                if (ddlItemBillingHead_Type.SelectedIndex > 0)
                {
                    FillHeadName();
                    ddlHeadNameID.ClearSelection();
                    ddlHeadNameID.Items.FindByValue(ds.Tables[0].Rows[0]["ItemBillingHead_ID"].ToString()).Selected = true;
                }
                txtItemBillingHeadAliasName.Text = ds.Tables[0].Rows[0]["ItemBillingHeadAlias_Name"].ToString();

                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItemBillingHead_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillHeadName();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}