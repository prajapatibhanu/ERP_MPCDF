using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Finance_ItemMaster : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            try
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = objdb.Office_ID();

                    FillOffice();
                    //FillSalesLedger();
                    //FillPurchaseLedger();

                    ViewState["ItemId"] = "0";
                    ddlItemCategory.Enabled = false;

                    ds = objdb.ByProcedure("SpItemCategory",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

                    ItemGroup.DataSource = ds;
                    ItemGroup.DataTextField = "ItemCatName";
                    ItemGroup.DataValueField = "ItemCat_id";
                    ItemGroup.DataBind();
                    ItemGroup.Items.Insert(0, new ListItem("Select", "0"));

                    ds1 = objdb.ByProcedure("SpUnit",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
                    FillItemCategory();
                    ddlUnit.DataSource = ds1;
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "Unit_Id";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, new ListItem("Select", "0"));

                    ds3 = objdb.ByProcedure("SpFinHSNMaster",
                                new string[] { "flag" },
                                new string[] { "2" }, "dataset");

                    ddlHsnCode.DataSource = ds3;
                    ddlHsnCode.DataTextField = "HSN_Code";
                    ddlHsnCode.DataValueField = "HSN_ID";
                    ddlHsnCode.DataBind();
                    ddlHsnCode.Items.Insert(0, new ListItem("Select", "0"));

                    //ds5 = objdb.ByProcedure("SpFinHSNMaster",
                    //            new string[] { "flag" },
                    //            new string[] { "2" }, "dataset");

                    //ddlHsnCode.DataSource = ds5;
                    //ddlHsnCode.DataTextField = "HSN_Code";
                    //ddlHsnCode.DataValueField = "HSN_ID";
                    //ddlHsnCode.DataBind();
                    //ddlHsnCode.Items.Insert(0, "Select");
                    FillGrid();
                    ViewState["Emp_ID"] = objdb.createdBy();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
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
    //protected void FillPurchaseLedger()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "8", ViewState["Office_ID"].ToString() }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlpurchaseledger.DataSource = ds;
    //            ddlpurchaseledger.DataTextField = "Ledger_Name";
    //            ddlpurchaseledger.DataValueField = "Ledger_ID";
    //            ddlpurchaseledger.DataBind();
    //            ddlpurchaseledger.Items.Insert(0, "Select");
    //        }
    //        else
    //        {
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void FillSalesLedger()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "9", ViewState["Office_ID"].ToString() }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlsalesledger.DataSource = ds;
    //            ddlsalesledger.DataTextField = "Ledger_Name";
    //            ddlsalesledger.DataValueField = "Ledger_ID";
    //            ddlsalesledger.DataBind();
    //            ddlsalesledger.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //        else
    //        {
    //            ds.Clear();
    //            ddlsalesledger.DataSource = ds;
    //            ddlsalesledger.DataTextField = "Ledger_Name";
    //            ddlsalesledger.DataValueField = "Ledger_ID";
    //            ddlsalesledger.DataBind();
    //            ddlsalesledger.Items.Insert(0, "Select");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    protected void ItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillItemCategory();
            showAndHideEvent();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtItemName.Text == "")
            {
                msg = "Enter Item Name";
            }
            if (ItemGroup.SelectedIndex <= 0)
            {
                msg += "Select Item Group. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
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
                                new string[] { "flag", "GroupId", "CategoryId", "UnitId", "ItemName", "PacketSize", "ItemAliasCode", "CreatedBy", "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass" },
                                new string[] { "1", ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtPacketSize.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(), ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, lClass }, "dataset");

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
                                        new string[] { "0", ItemId, item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
                                }
                            }
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
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
                            new string[] { "flag", "ItemId", "GroupId", "CategoryId", "UnitId", "ItemName", "PacketSize", "ItemAliasCode", "CreatedBy", "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass" },
                            new string[] { "17", ViewState["ItemId"].ToString(), ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtPacketSize.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(), ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, lClass }, "dataset");
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
                                         new string[] { "0", ViewState["ItemId"].ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
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
                    ClearData();
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
    private void ClearData()
    {
        txtItemName.Text = "";
        ItemGroup.ClearSelection();
        txtItemSpecification.Text = "";
        txtItemSize.Text = "";
        txtItemBrand.Text = "";
        ddlDimensionClass.ClearSelection();
        ddlItemCategory.ClearSelection();
        ddlUnit.ClearSelection();
        txtPacketSize.Text = "";
        //ddlpurchaseledger.ClearSelection();
        //ddlsalesledger.ClearSelection();
        ddlHsnCode.ClearSelection();
        ddlItemCategory.Enabled = false;
        txtitemaliscode.Text = "";
        SetFocus(txtItemName);
        chkOffice.ClearSelection();
        chkOfficeAll.Checked = false;
    }
    private void FillGrid()
    {
        try
        {
            ds4 = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "GroupId", "CategoryId" },
                        new string[] { "19", ItemGroup.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString() }, "dataset");
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
    protected void GVItemDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string id = GVItemDetail.DataKeys[e.RowIndex].Value.ToString();
            ds4 = objdb.ByProcedure("SpItemMaster",
                      new string[] { "flag", "ItemId" },
                      new string[] { "2", id.ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void GVItemDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GVItemDetail.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GVItemDetail.DataSource = ds;
                GVItemDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVItemDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillGrid();
            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            string ItemId = GVItemDetail.SelectedDataKey.Value.ToString();
            ViewState["ItemId"] = ItemId;
            ds = objdb.ByProcedure("SpItemMaster",
                     new string[] { "flag", "ItemId" },
                     new string[] { "16", ViewState["ItemId"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                ItemGroup.ClearSelection();
                ItemGroup.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCat_id"].ToString()).Selected = true;
                FillItemCategory();
                ddlItemCategory.ClearSelection();
                ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;
                ddlUnit.ClearSelection();
                showAndHideEvent();
                txtPacketSize.Text = ds.Tables[0].Rows[0]["PacketSize"].ToString();
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
    protected void ViewOffice_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkid = (LinkButton)GVItemDetail.Rows[selRowIndex].FindControl("ViewOffice");
            string Item_ID = lnkid.ToolTip.ToString();

            ds = objdb.ByProcedure("SpItemMaster",
                new string[] { "flag", "ItemId" },
                new string[] { "9", Item_ID }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
            }
            else
            {
                GridView1.DataSource = new string[] { };
            }
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillItemCategory()
    {
        lblMsg.Text = "";
        try
        {
            if (ItemGroup.SelectedIndex != 0)
            {
                ddlItemCategory.Enabled = true;
                ds2 = objdb.ByProcedure("SpItemType",
                          new string[] { "flag", "ItemCat_id" },
                          new string[] { "6", ItemGroup.SelectedValue }, "dataset");
                 
                ddlItemCategory.DataSource = ds2;
                ddlItemCategory.DataTextField = "Abbreviation";
                ddlItemCategory.DataValueField = "ItemType_id";
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemCategory.Items.Clear();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void showAndHideEvent()
    {
        if (ItemGroup.SelectedValue != "2" && ItemGroup.SelectedValue != "0")
        {
            packetsize.Visible = true;
        }
        else
        {
            packetsize.Visible = false;
        }
    }
    protected void txtPacketSize_TextChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue != "0" && txtPacketSize.Text != string.Empty)
        {
            txtItemName.Text = ddlItemCategory.SelectedItem.Text + "-" + txtPacketSize.Text;
        }
    }
    protected void GVItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            try
            {
                FillGrid();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
                //int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
                //LinkButton lnkid = (LinkButton)GVItemDetail.Rows[selRowIndex].FindControl("ViewOffice");
                //string Item_ID = lnkid.ToolTip.ToString();

                ds = objdb.ByProcedure("SpItemMaster",
                    new string[] { "flag", "ItemId" },
                    new string[] { "9", e.CommandArgument.ToString() }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                }
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
}