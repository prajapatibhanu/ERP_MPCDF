using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Masters_ItemVariant : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Emp_ID"] = "1";
        Session["Office_ID"] = "2";
        if (objdb.createdBy() != null)
        {
            try
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = objdb.Office_ID();
                    FillOffice();
                    ItemCategory();
                    FillUnit();
                    HSNCode();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    protected void HSNCode()
    {
        ds3 = objdb.ByProcedure("SpFinHSNMaster",
                                   new string[] { "flag" },
                                   new string[] { "2" }, "dataset");
        ddlHsnCode.DataSource = ds3;
        ddlHsnCode.DataTextField = "HSN_Code";
        ddlHsnCode.DataValueField = "HSN_ID";
        ddlHsnCode.DataBind();
        ddlHsnCode.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void ItemCategory()
    {
        ViewState["ItemId"] = "0";
        ddlItem.Enabled = false;

        ds = objdb.ByProcedure("SpItemCategory",
                 new string[] { "flag" },
                 new string[] { "1" }, "dataset");
        ddlItemCategory.DataSource = ds;
        ddlItemCategory.DataTextField = "ItemCatName";
        ddlItemCategory.DataValueField = "ItemCat_id";
        ddlItemCategory.DataBind();
        ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void ItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillItem();
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void txtPacketSize_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GVItemDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVItemDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GVItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            // string ItemId = GVItemDetail.SelectedDataKey.Value.ToString();
            string ItemId = Convert.ToString(e.CommandArgument.ToString());
            ViewState["ItemId"] = ItemId;
            ds = objdb.ByProcedure("SpItemMaster",
                     new string[] { "flag", "ItemId" },
                     new string[] { "16", ViewState["ItemId"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtItemVariantName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                ddlItemCategory.ClearSelection();
                ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCat_id"].ToString()).Selected = true;
                FillItem();
                ddlItem.ClearSelection();
                ddlItem.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;
                ddlUnit.ClearSelection();
                //showAndHideEvent();
                //txtPacketSize.Text = ds.Tables[0].Rows[0]["PacketSize"].ToString();
                ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_Id"].ToString()).Selected = true;
                ddlHsnCode.ClearSelection();
                ddlHsnCode.Items.FindByText(ds.Tables[0].Rows[0]["HSNCode"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["LengthClass"].ToString() != "")
                {
                    ddlDimensionClass.ClearSelection();
                    ddlDimensionClass.Items.FindByText(ds.Tables[0].Rows[0]["LengthClass"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["ItemAliasCode"].ToString() != "")
                {
                    txtitemaliscode.Text = ds.Tables[0].Rows[0]["ItemAliasCode"].ToString();
                }
                //if (ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString() != "")
                //{
                //    ddlpurchaseledger.ClearSelection();
                //    ddlpurchaseledger.Items.FindByValue(ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString()).Selected = true;
                //}
                //if (ds.Tables[0].Rows[0]["SalesLedger_id"].ToString() != "")
                //{
                //    ddlsalesledger.ClearSelection();
                //    ddlsalesledger.Items.FindByValue(ds.Tables[0].Rows[0]["SalesLedger_id"].ToString()).Selected = true;
                //}
                if (ds.Tables[0].Rows[0]["ItemBrand"].ToString() != "")
                {
                    txtItemBrand.Text = ds.Tables[0].Rows[0]["ItemBrand"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemSize"].ToString() != "")
                {
                    txtItemSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemSpecification"].ToString() != "")
                {
                    txtItemSpecification.Text = ds.Tables[0].Rows[0]["ItemSpecification"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    chkOffice.ClearSelection();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string Value = ds.Tables[1].Rows[i]["Office_ID"].ToString();
                        foreach (ListItem item in chkOffice.Items)
                        {
                            if (item.Value == Value)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    btnSubmit.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                string msg = "";
                if (txtItemVariantName.Text == "")
                {
                    msg = "Enter Item Name";
                }
                if (ddlItemCategory.SelectedIndex <= 0)
                {
                    msg += "Select Item Group. \\n";
                }
                if (ddlItem.SelectedIndex <= 0)
                {
                    msg += "Select Item Sub-Group. \\n";
                }
                if (ddlUnit.SelectedIndex <= 0)
                {
                    msg += "Select Unit. \\n";
                }
                if (ddlHsnCode.SelectedIndex <= 0)
                {
                    msg += "Select HSN Code. \\n";
                }
                if (msg.Trim() == "")
                {
                    string lClass = "";
                    if (ddlDimensionClass.SelectedValue != "0")
                    {
                        lClass = ddlDimensionClass.SelectedItem.Text;
                    }
                    else
                    {
                        lClass = "";
                    }
                    if (btnSubmit.Text == "Submit")
                    {
                        ds = objdb.ByProcedure("SpItemMaster",
                               new string[] { "flag", "GroupId", "CategoryId", "UnitId", "ItemName", "ItemAliasCode", "CreatedBy", "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass" },
                               new string[] { "1", ddlItemCategory.SelectedValue, ddlItem.SelectedValue, ddlUnit.SelectedValue, txtItemVariantName.Text, txtitemaliscode.Text, Session["Emp_ID"].ToString(), ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, lClass }, "dataset");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string ItemId = ds.Tables[0].Rows[0]["ItemId"].ToString();
                            if (ItemId != "")
                            {
                                foreach (ListItem item in chkOffice.Items)
                                {
                                    if (item.Selected == true)
                                    {
                                        objdb.ByProcedure("SpItemMasterChild",
                                            new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
                                            new string[] { "0", ItemId, item.Value, Session["Emp_ID"].ToString() }, "dataset");
                                    }
                                }
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Saved Successfully.");
                                ClearData();
                                FillGrid();
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Exist")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        ds = objdb.ByProcedure("SpItemMaster",
                          new string[] { "flag", "ItemId", "GroupId", "CategoryId", "UnitId", "ItemName", "ItemAliasCode", "CreatedBy", "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass" },
                          new string[] { "17", ViewState["ItemId"].ToString(), ddlItemCategory.SelectedValue, ddlItem.SelectedValue, ddlUnit.SelectedValue, txtItemVariantName.Text, txtitemaliscode.Text, Session["Emp_ID"].ToString(), ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, lClass }, "dataset");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            objdb.ByProcedure("SpItemMasterChild",
                                     new string[] { "flag", "Item_id" },
                                     new string[] { "2", ViewState["ItemId"].ToString() }, "dataset");

                            foreach (ListItem item in chkOffice.Items)
                            {
                                if (item.Selected == true)
                                {
                                    objdb.ByProcedure("SpItemMasterChild",
                                             new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
                                             new string[] { "0", ViewState["ItemId"].ToString(), item.Value, Session["Emp_ID"].ToString() }, "dataset");
                                }
                            }
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            ClearData();
                            FillGrid();
                            btnSubmit.Text = "Save";
                        }
                        else if (ds.Tables[1].Rows[0]["Msg"].ToString() == "Exist")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Name is already exist.');", true);
                        //ClearData();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    protected void FillItem()
    {
        lblMsg.Text = "";
        try
        {
            if (ddlItemCategory.SelectedIndex != 0)
            {
                ddlItem.Enabled = true;
                ds2 = objdb.ByProcedure("SpItemType",
                          new string[] { "flag", "ItemCat_id" },
                          new string[] { "6", ddlItemCategory.SelectedValue }, "dataset");

                ddlItem.DataSource = ds2;
                ddlItem.DataTextField = "Abbreviation";
                ddlItem.DataValueField = "ItemType_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItem.Items.Clear();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
                ddlItem.Enabled = false;
            }
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
                 new string[] { "9" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkOffice.DataSource = ds;
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();
            }
            else
            {
                ds.Clear();
                chkOffice.DataSource = ds;
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void FillGrid()
    {
        try
        {
            ds4 = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "GroupId", "CategoryId" },
                        new string[] { "19", ddlItemCategory.SelectedValue.ToString(), ddlItem.SelectedValue.ToString() }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GVItemDetail.DataSource = ds4;
            }
            else
            {
                GVItemDetail.DataSource = new string[] { };
            }
            GVItemDetail.DataBind();
            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void FillUnit()
    {
        ds1 = objdb.ByProcedure("SpUnit",
                                   new string[] { "flag" },
                                   new string[] { "1" }, "dataset");
        // FillItem();
        ddlUnit.DataSource = ds1;
        ddlUnit.DataTextField = "UnitName";
        ddlUnit.DataValueField = "Unit_Id";
        ddlUnit.DataBind();
        ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
    }
    private void ClearData()
    {
        txtItemVariantName.Text = "";
        ddlItemCategory.ClearSelection();
        txtItemSpecification.Text = "";
        //txtItemSize.Text = "";
        //txtItemBrand.Text = "";
        //ddlDimensionClass.ClearSelection();
        ddlItem.ClearSelection();
        ddlUnit.ClearSelection();
        //txtPacketSize.Text = "";
        //ddlpurchaseledger.ClearSelection();
        //ddlsalesledger.ClearSelection();
        ddlHsnCode.ClearSelection();
        //ddlItemCategory.Enabled = false;
        //txtitemaliscode.Text = "";
        SetFocus(txtItemVariantName);
        chkOffice.ClearSelection();
        chkOfficeAll.Checked = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
}