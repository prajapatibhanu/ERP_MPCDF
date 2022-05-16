using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_CattelFeed_CFP_ItemTypes : System.Web.UI.Page
{
    APIProcedure objapi = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Category();
        Fillgrd();
    }
    protected void Category()
    {
        try
        {
            ddlItemCategory.DataSource = objapi.ByProcedure("SP_CFPCItemCategory_List",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCatid";
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
          GC.SuppressFinalize(objapi);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertOrUpdateProduct()
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";
            ds = new DataSet();
            string flag = "0";
            if (Convert.ToInt32(hdnItemtype.Value) > 0)
            {
                flag = "1";
            }
            ds = objapi.ByProcedure("SP_CFPItemType_Insert_Update_Delete",
                new string[] { "flag", "ItemTypeName", "Abbreviation", "ItemCatID", "ItemTypeID", "OfficeID", "InsertedBy" },
                new string[] { flag, txtItemTypeName.Text.Trim(), txtAbbreviation.Text.Trim(), ddlItemCategory.SelectedValue, hdnItemtype.Value, objapi.Office_ID(), objapi.createdBy() }, "TableSave");
            if (ds.Tables[0].Rows[0]["ErrorMSG"].ToString() == "OK")
            {
                Fillgrd();
                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                Clear();
                lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
            }
        }
    }
    private void Clear()
    {
        txtItemTypeName.Text = string.Empty;
        ddlItemCategory.SelectedIndex = 0;
        txtAbbreviation.Text = string.Empty;
        hdnItemtype.Value = "0";
        btnSubmit.Text = "Save";

    }
    private void Fillgrd()
    {
        try
        {
            grdlist.DataSource = objapi.ByProcedure("SP_CFPItemType_List",
                            new string[] { "flag" },
                            new string[] { "0" }, "dataset");
            grdlist.DataBind();
            GC.SuppressFinalize(objapi);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnItemtype.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "RecordDelete":
                   ds = objapi.ByProcedure("SP_CFPItemType_Insert_Update_Delete",
                new string[] { "flag", "ItemTypeName", "Abbreviation", "ItemCatID", "ItemTypeID", "OfficeID", "InsertedBy" },
                new string[] { "3", txtItemTypeName.Text.Trim(), txtAbbreviation.Text.Trim(), ddlItemCategory.SelectedValue, hdnItemtype.Value, objapi.Office_ID(), objapi.createdBy() }, "TableSave");
            if (ds.Tables[0].Rows[0]["ErrorMSG"].ToString() == "OK")
            {
                Fillgrd();
                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                Clear();
                lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
            }
                break;
            case "RecordUpdate":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFPItemType_By_ItemTypeID_List",
                    new string[] { "flag", "ItemTypeID" },
                    new string[] { "0", Convert.ToString(e.CommandArgument) }, "TableSave");
                ddlItemCategory.SelectedValue = ds.Tables[0].Rows[0][1].ToString(); 
                txtItemTypeName.Text = ds.Tables[0].Rows[0][2].ToString();
                txtAbbreviation.Text = ds.Tables[0].Rows[0][3].ToString();
                hdnItemtype.Value = Convert.ToString(e.CommandArgument);
                btnSubmit.Text = "Edit";
                break;
            default:
                break;
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateProduct();
    }
}