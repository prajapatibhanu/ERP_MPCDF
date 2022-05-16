using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Warehouse_OpeningStock : System.Web.UI.Page
{
    DataSet ds, ds1;
    AbstApiDBApi objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    CultureInfo cult = new CultureInfo("gu-IN", true);

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                 ViewState["Department_ID"] = Session["Department_ID"].ToString();
               // BindOpeningStockItem();
                 FillDropdown();
                 BindOpeningStockItem();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    //protected void PageLoad_Init(object sender, EventArgs e)
    //{
       
    //}
    protected void FillDropdown()
    {
        try
        {
            ddlOffice.DataSource = objdb.ByProcedure("SpAdminOffice",
                                       new string[] { "flag" },
                                       new string[] { "54", }, "Dataset");
            ddlOffice.DataTextField = "Office_Name";
            ddlOffice.DataValueField = "Office_ID";
            ddlOffice.DataBind();
            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();


            ddlitemcategory.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                      new string[] { "flag", "Office_Id" },
                                      new string[] { "1", ViewState["Office_ID"].ToString() }, "Dataset");
            ddlitemcategory.DataTextField = "ItemCatName";
            ddlitemcategory.DataValueField = "ItemCat_id";
            ddlitemcategory.DataBind();
            ddlitemcategory.Items.Insert(0, new ListItem("Select", "0"));
         

            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));

            ddlitems.Items.Insert(0, new ListItem("Select", "0"));

            ddlWarehouse.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                       new string[] { "flag", "Office_Id" },
                                       new string[] { "0", ViewState["Office_ID"].ToString() }, "Dataset");
            ddlWarehouse.DataTextField = "WarehouseName";
            ddlWarehouse.DataValueField = "Warehouse_id";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("Select", "0"));


            ddlSearchWarehouse.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                      new string[] { "flag", "Office_Id" },
                                      new string[] { "0", ViewState["Office_ID"].ToString() }, "Dataset");
            ddlSearchWarehouse.DataTextField = "WarehouseName";
            ddlSearchWarehouse.DataValueField = "Warehouse_id";
            ddlSearchWarehouse.DataBind();
            ddlSearchWarehouse.Items.Insert(0, new ListItem("All", "0"));
            ddlSearchWarehouse.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
  
   
    private void ItemCategory()
    {
        if (ddlitemcategory.SelectedItem.Value != "0")
        {
            BindSubgroupByGroup();
            GetDatatableHeaderDesign();
        }

        else
        {
            ddlitemtype.Items.Clear();
            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            ddlitems.Items.Clear();
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
            ddlitemtype.Enabled = false;
            ddlitems.Enabled = false;
            ddlUnit.Enabled = false;
        }
    }

    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlitemcategory.SelectedItem.Value != "0")
        {
            BindSubgroupByGroup();
            GetDatatableHeaderDesign();
        }

        else
        {
            ddlitemtype.Items.Clear();
            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            ddlitems.Items.Clear();
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
            ddlitemtype.Enabled = false;
            ddlitems.Enabled = false;
            ddlUnit.Enabled = false;
        }
    }
    private void BindSubgroupByGroup()
    {
        try
        {

            lblError.Text = "";
            ddlitemtype.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                        new string[] { "flag", "Office_Id", "ItemCat_id" },
                                        new string[] { "2",ViewState["Office_ID"].ToString(), ddlitemcategory.SelectedItem.Value }, "Dataset");
            ddlitemtype.DataTextField = "ItemTypeName";
            ddlitemtype.DataValueField = "ItemType_id";
            ddlitemtype.DataBind();
            ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
            ddlitemtype.Enabled = true;

            ddlitems.Items.Clear();
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));
            ddlitems.Enabled = false;
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }

    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlitemcategory.SelectedItem.Value != "0" && ddlitemtype.SelectedItem.Value != "0")
        {
            BindItemByGroupOrSubgroup();
            GetDatatableHeaderDesign();
        }
        else
        {
            ddlitems.Items.Clear();
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));
            ddlitems.Enabled = false;
        }
    }
    private void BindItemByGroupOrSubgroup()
    {
        try
        {
            lblError.Text = "";
            ddlitems.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                        new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id" },
                                        new string[] { "3", ViewState["Office_ID"].ToString(), ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value }, "Dataset");
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataBind();

            //if (ViewState["Office_ID"].ToString() == objdb.GetMAFId() && ddlitemcategory.SelectedValue == objdb.GetProductId())
            //{
            //    for (int i = ddlitems.Items.Count - 1; i >= 0; i--)
            //    {
            //        if (ddlitems.Items[i].Value != objdb.GetWheat().ToString() && ddlitems.Items[i].Value != objdb.GetPaddy().ToString())
            //        {
            //            ddlitems.Items.RemoveAt(i);
            //        }
            //    }
            //}
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));
            ddlitems.Enabled = true;
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    protected void ddlitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlitems.SelectedItem.Value != "0")
        {
            BindUnitByItemOrSubgroupOrGroup();
            GetDatatableHeaderDesign();
        }
    }
    private void BindUnitByItemOrSubgroupOrGroup()
    {
        try
        {
            lblError.Text = "";
            ddlUnit.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                     new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id" },
                                     new string[] { "4", ViewState["Office_ID"].ToString(), ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value }, "Dataset");
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "Unit_id";
            ddlUnit.DataBind();
            ddlUnit.Enabled = false;
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gvOpeningStock.Rows.Count > 0)
            {
                gvOpeningStock.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvOpeningStock.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void Clear()
    {
        lblError.Text = string.Empty;
        ddlitemtype.Items.Clear();
        ddlitemtype.Items.Insert(0, new ListItem("Select", "0"));
        ddlitemtype.SelectedIndex = 0;
        //ddlitemtype.Enabled = false;
        ddlWarehouse.SelectedIndex = 0;
        ddlitemcategory.SelectedIndex = 0;
        ddlitems.Items.Clear();
        ddlitems.Items.Insert(0, new ListItem("Select", "0"));
        //ddlitems.Enabled = false;
        ddlUnit.Items.Clear();
        ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        ddlUnit.Enabled = false;
        txtQty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtAmount.Text = string.Empty;
        btnSubmit.Text = "Save";
        txtTransactionDt.Text = string.Empty;
        gvOpeningStock.SelectedIndex = -1;
        GetDatatableHeaderDesign();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    DateTime date3 = DateTime.ParseExact(txtTransactionDt.Text, "dd/MM/yyyy", culture);
                    if (btnSubmit.Text == "Save")
                    {
                        lblError.Text = "";
                        ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                                new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Cr", "Dr", "Rate", "Amount", "Warehouse_id", "Depart_Id", "TranDt", "Office_Id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                                new string[] { "5", ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtQty.Text, "0", txtRate.Text, txtAmount.Text, ddlWarehouse.SelectedValue, ViewState["Department_ID"].ToString(), Convert.ToDateTime(txtTransactionDt.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Inserted", "" }, "dataset");
                                                //new string[] { "5", ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtQty.Text, "0", txtRate.Text,txtAmount.Text, ddlWarehouse.SelectedValue, ViewState["Department_ID"].ToString(), date3.ToString(), ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Inserted", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();

                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblError.Text = "";

                        ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                                new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Cr", "Dr", "Rate","Amount","Warehouse_id", "Depart_Id", "TranDt", "Office_Id", "ItmStock_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                                 new string[] { "7", ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtQty.Text, "0", txtRate.Text, txtAmount.Text, ddlWarehouse.SelectedValue, ViewState["Department_ID"].ToString(), Convert.ToDateTime(txtTransactionDt.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ViewState["rid"].ToString(), ViewState["Emp_ID"].ToString(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Updated", "" }, "dataset");
                                                //new string[] { "7", ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtQty.Text, "0",txtRate.Text,txtAmount.Text, ddlWarehouse.SelectedValue, ViewState["Department_ID"].ToString(), date3.ToString(), ViewState["Office_ID"].ToString(), ViewState["rid"].ToString(), ViewState["Emp_ID"].ToString(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Updated", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            BindOpeningStockItem();
                            Clear();
                            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        ds.Clear();
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! .", "Please Enter Date");
                }

            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
    protected void gvOpeningStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblWarehouse_id = (Label)row.FindControl("lblWarehouse_id");
                    Label lblTranDt = (Label)row.FindControl("lblTranDt");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblProducUnit_id = (Label)row.FindControl("lblProducUnit_id");
                    Label lblCr = (Label)row.FindControl("lblCr");
                    Label lblRate = (Label)row.FindControl("lblRate");
                    Label lblAmount = (Label)row.FindControl("lblAmount");

                    btnSubmit.Text = "Update";
                    txtQty.Text = lblCr.Text;
                    txtTransactionDt.Text = Convert.ToDateTime(lblTranDt.Text).ToString("dd/MM/yyyy");
                    ViewState["rid"] = e.CommandArgument.ToString();
                    ddlWarehouse.SelectedValue = lblWarehouse_id.Text;
                    ddlitemcategory.SelectedValue = lblItemCat_id.Text;
                    BindSubgroupByGroup();
                    ddlitemtype.SelectedValue = lblItemType_id.Text;
                    BindItemByGroupOrSubgroup(); ;
                    ddlitems.SelectedValue = lblItem_id.Text;
                    txtRate.Text = lblRate.Text;
                    txtAmount.Text = lblAmount.Text;
                    BindUnitByItemOrSubgroupOrGroup();

                    foreach (GridViewRow gvRow in gvOpeningStock.Rows)
                    {
                        if (gvOpeningStock.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gvOpeningStock.SelectedIndex = gvRow.DataItemIndex;
                            gvOpeningStock.SelectedRowStyle.BackColor = System.Drawing.Color.LemonChiffon;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    ViewState["rid"] = e.CommandArgument.ToString();
                    ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                                new string[] { "flag", "ItmStock_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                                //new string[] { "8", ViewState["rid"].ToString(), ViewState["Emp_ID"].ToString(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Deleted", objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");
                                                 new string[] { "8", ViewState["rid"].ToString(), ViewState["Emp_ID"].ToString(), Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Deleted", "" }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        BindOpeningStockItem();
                        lblError.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        ds.Clear();
                        BindOpeningStockItem();
                        lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void BindOpeningStockItem()
    {
        try
        {
            lblError.Text = "";
            ds = objdb.ByProcedure("Sp_tblSpItemStock",
                                        new string[] { "flag", "Office_Id", "Warehouse_id" },
                                        new string[] { "27", ViewState["Office_ID"].ToString(), ddlSearchWarehouse.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                gvOpeningStock.DataSource = ds;
                gvOpeningStock.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                gvOpeningStock.DataSource = null;
                gvOpeningStock.DataBind();
            }
            ds.Clear();
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
   
    protected void ddlSearchWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOpeningStockItem();
    }
  
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void gvOpeningStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTransactionFrom = e.Row.FindControl("lblTransactionFrom") as Label;
                LinkButton lnkUpdate = e.Row.FindControl("lnkUpdate") as LinkButton;
                LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;

                if (lblTransactionFrom.Text == "Purchase Order Received")
                {
                    lnkDelete.Visible = false;
                    lnkUpdate.Visible = false;
                }
                else
                {
                    lnkDelete.Visible = true;
                    lnkUpdate.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            decimal qty = 0;
            if (ddlWarehouse.SelectedIndex != 0)
            {
                if (txtQty.Text != "")
                {
                    qty = Convert.ToDecimal(txtQty.Text);
                    ds1 = objdb.ByProcedure("Sp_tblSpItemStock",
                      new string[] { "flag", "Office_Id", "Warehouse_id", "Item_id", "ItemQty" },
                      new string[] { "23", ViewState["Office_ID"].ToString(), ddlWarehouse.SelectedValue, ddlitems.SelectedValue, qty.ToString() }, "Dataset");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(ds1.Tables[0].Rows[0]["LeftCapacityInTon"].ToString()) >= 0)
                        {
                            lblError.Text = "";
                        }
                        else
                        {
                            lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warehouse Capacity is full!", "Your Warehouse Capacity is : " +
                            Convert.ToDecimal(ds1.Tables[0].Rows[0]["WarehouseCapacity"].ToString()).ToString("0.000") +
                            " Ton and Capacity Consumed : " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["StockInTon"].ToString()).ToString("0.000") +
                            " Ton and you want to Inward Quantity : " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["AddStock"].ToString()).ToString("0.000") +
                            " Ton, Kindly reduce quantity and Try again!");

                            txtQty.Text = "";
                            ddlWarehouse.ClearSelection();
                        }
                    }
                    else
                    {
                        if (ds1 != null) { ds1.Clear(); }
                        ds1 = objdb.ByProcedure("SpWarehouseMaster",
                                  new string[] { "flag", "WrId" },
                                  new string[] { "2", ddlWarehouse.SelectedValue }, "Dataset");
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            qty = Convert.ToDecimal(txtQty.Text);
                            qty = qty / 1000; //Conveted KG to Ton
                            if (Convert.ToDecimal(ds1.Tables[0].Rows[0]["WarehouseCapacity"].ToString()) >= qty)
                            {
                                lblError.Text = "";
                            }
                            else
                            {
                                lblError.Text = objdb.Alert("fa-warning", "alert-warning", "Warehouse Capacity is full!", "Your Warehouse Capacity is : " +
                                Convert.ToDecimal(ds1.Tables[0].Rows[0]["WarehouseCapacity"].ToString()).ToString("0.000") +
                                " Ton and you want to Inward Quantity : " + Convert.ToDecimal(qty).ToString("0.000") +
                                " Ton, Kindly reduce quantity and Try again!");

                                txtQty.Text = "";
                                ddlWarehouse.ClearSelection();
                            }
                        }
                        else
                        {
                            lblError.Text = objdb.Alert("fa-warning", "alert-warning", "","Something Wents wrong.");
                        }
                    }
                }
                else
                {
                    lblError.Text = objdb.Alert("fa-info", "alert-info", "","Please first to fill Quantity.");
                    SetFocus(txtQty);
                    ddlWarehouse.SelectedIndex = 0;
                }
            }
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblError.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
}