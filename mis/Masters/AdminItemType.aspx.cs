using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Admin_AdminItemType : System.Web.UI.Page
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
                GetProductMasterDetails();
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

    private void Clear()
    {
        txtItemTypeName.Text = string.Empty;
        ddlItemCategory.SelectedIndex = 0;
        txtAbbreviation.Text = string.Empty;
       // txtDescription.Text = string.Empty;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void GetProductMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("SpItemType",
                            new string[] { "flag", "Office_ID",  },
                            new string[] { "5", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertOrUpdateProduct()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("SpItemType",
                            new string[] { "flag", "ItemCat_id", "ItemTypeName", "Abbreviation", "Fat_percent", "Snf_percent", "CreatedBy", "Office_ID" },
                            new string[] { "2", ddlItemCategory.SelectedValue, txtItemTypeName.Text.Trim(), txtAbbreviation.Text.Trim()
                               , txtFat.Text.Trim(),txtSNF.Text.Trim(), objdb.createdBy(), objdb.Office_ID() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetProductMasterDetails();
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :");
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", txtItemTypeName.Text + " or " + txtAbbreviation.Text.Trim() + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("SpItemType",
                            new string[] { "flag", "ItemType_id", "ItemCat_id", "ItemTypeName", "Abbreviation", "Fat_percent", "Snf_percent", "CreatedBy", "Office_ID" },
                            new string[] { "3", ViewState["rowid"].ToString(), ddlItemCategory.SelectedValue
                                , txtItemTypeName.Text.Trim(),txtAbbreviation.Text.Trim()
                               , txtFat.Text.Trim(),txtSNF.Text.Trim() , objdb.createdBy(), objdb.Office_ID() }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetProductMasterDetails();
                            lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", txtItemTypeName.Text + " or " + txtAbbreviation.Text.Trim() + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!", " Enter Bank Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }

    }
    protected void GetProductCategory()
    {
        try
        {
            ddlItemCategory.DataSource = objdb.ByProcedure("SpItemType",
                   new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion====================================end of user defined function==========

    #region=============== init or changed event for controls =================

    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetProductCategory();
    }
    #endregion============ end of changed event for controls===========

    #region============ button click and gridview events event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateProduct();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
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
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblCat_id = (Label)row.FindControl("lblCat_id");
                    Label lblProductId = (Label)row.FindControl("lblProductId");
                    Label lblItemTypeName = (Label)row.FindControl("lblItemTypeName");
                    Label lblAbbreviation = (Label)row.FindControl("lblAbbreviation");
                    Label lblDescription = (Label)row.FindControl("lblDescription");
                    Label lblFat = (Label)row.FindControl("lblFat");
                    Label lblSnf = (Label)row.FindControl("lblSnf");
                    ViewState["rowid"] = e.CommandArgument;
                    ddlItemCategory.SelectedValue = lblCat_id.Text;
                    txtItemTypeName.Text = lblItemTypeName.Text;
                    txtAbbreviation.Text = lblAbbreviation.Text;
                    txtFat.Text = lblFat.Text;
                    txtSNF.Text = lblSnf.Text;
                   // txtDescription.Text = lblDescription.Text;
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
                    ds = objdb.ByProcedure("SpItemType",
                                new string[] { "flag", "ItemType_id", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "ItemType Master Record Deleted" }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        GetProductMasterDetails();
                        lblMsg.Text = objdb.Alert("fa fa-thumbs-up", "alert-success", "Thank You!", "Success :" + success);
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
    #endregion=============end of button click funciton==================

}