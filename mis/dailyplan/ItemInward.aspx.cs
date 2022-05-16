using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_ItemInward : System.Web.UI.Page
{
    DataSet ds, ds2, ds4;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    ViewState["ItmStock_id"] = "0";
                    ddlItemCategory.Enabled = false;

                    ds = objdb.ByProcedure("SpItemCategory",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");

                    ItemGroup.DataSource = ds;
                    ItemGroup.DataTextField = "ItemCatName";
                    ItemGroup.DataValueField = "ItemCat_id";
                    ItemGroup.DataBind();
                    ItemGroup.Items.Insert(0, new ListItem("Select", "0"));
                    ItemGroup.SelectedValue = "1";


                    FillItemCategory();

                    //FillGrid();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void ItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillItemCategory();
            //FillGrid();
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

            if (ItemGroup.SelectedIndex <= 0)
            {
                msg += "Select Nature. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg += "Select Item Group. \\n";
            }

            if (ddlItem.SelectedIndex <= 0)
            {
                msg += "Select Item. \\n";
            }

            if (msg.Trim() == "")
            {
                //int Status = 0;
                ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
                if (btnSubmit.Text == "Submit" && ViewState["ItmStock_id"].ToString() == "0")
                {


                    ds = objdb.ByProcedure("spProductionItemStock",
                       new string[] { "flag", "ReceiverOffice_ID", "ItemCat_id", "ItemType_id", "Item_id", "Inward", "ReceiverID", "TranDt", "BatchNo", "LotNo", "TransactionType", "ReceivingRemark" },
                       new string[] { "12", ViewState["Office_ID"].ToString(), ItemGroup.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), txtQuantity.Text.ToString(), ViewState["Emp_ID"].ToString(), ViewState["TranDt"].ToString(), txtBatchNo.Text.ToString(), txtLotNo.Text.ToString(), "OfficeOpeningStock", txtReceivingRemark.Text.ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    //FillGrid();

                }
                else if (btnSubmit.Text == "Update" && ViewState["ItmStock_id"].ToString() != "0")
                {
                    ItemGroup.Enabled = false;
                    ddlItemCategory.Enabled = false;
                    ddlItem.Enabled = false;
                    ddlUnit.Enabled = false;
                    txtOrderDate.Enabled = false;

                    ds = objdb.ByProcedure("spProductionItemStock",
                      new string[] { "flag", "Inward", "ItmStock_id", "BatchNo", "LotNo", "ReceivingRemark" },
                      new string[] { "14", txtQuantity.Text.ToString(), ViewState["ItmStock_id"].ToString(), txtBatchNo.Text.ToString(), txtLotNo.Text.ToString(), txtReceivingRemark.Text.ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Name is already exist.');", true);

                }
                ClearData();
                FillGrid();
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
        txtQuantity.Text = "";
        //ddlItemCategory.ClearSelection();
        ddlItem.ClearSelection();
        ddlUnit.ClearSelection();
        txtBatchNo.Text = "";
        txtLotNo.Text = "";
        txtReceivingRemark.Text = "";
    }

    private void FillGrid()
    {
        try
        {
            ds4 = objdb.ByProcedure("spProductionItemStock",
                        new string[] { "flag", "ItemCat_id", "ItemType_id", "ReceiverOffice_ID", "TransactionType" },
                        new string[] { "11", ItemGroup.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), "OfficeOpeningStock" }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GVItemDetail.DataSource = ds4.Tables[0];

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

            string ItmStock_id = GVItemDetail.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spProductionItemStock",
                   new string[] { "flag", "ItmStock_id" },
                   new string[] { "6", ItmStock_id }, "dataset");
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
            lblMsg.Text = "";
            //txtPackgngSz.Text = "";

            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            string ItemId = GVItemDetail.SelectedDataKey.Value.ToString();
            ViewState["ItmStock_id"] = ItemId;
            ds = objdb.ByProcedure("spProductionItemStock",
                new string[] { "flag", "ItmStock_id" },
                new string[] { "13", ViewState["ItmStock_id"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ItemGroup.ClearSelection();
                ItemGroup.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCat_id"].ToString()).Selected = true;

                FillItemCategory();
                ddlItemCategory.ClearSelection();
                ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;

                FillItem();
                ddlItem.ClearSelection();
                ddlItem.Items.FindByValue(ds.Tables[0].Rows[0]["Item_id"].ToString()).Selected = true;

                FillUnit();
                //ddlUnit.ClearSelection();
                //ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_Id"].ToString()).Selected = true;


                if (ds.Tables[0].Rows[0]["BatchNo"].ToString() != "")
                {
                    txtBatchNo.Text = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                }

                if (ds.Tables[0].Rows[0]["Inward"].ToString() != "")
                {
                    txtQuantity.Text = ds.Tables[0].Rows[0]["Inward"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LotNo"].ToString() != "")
                {
                    txtLotNo.Text = ds.Tables[0].Rows[0]["LotNo"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ReceivingRemark"].ToString() != "")
                {
                    txtReceivingRemark.Text = ds.Tables[0].Rows[0]["ReceivingRemark"].ToString();
                }
                btnSubmit.Text = "Update";
            }

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
                ddlItemCategory.DataTextField = "ItemTypeName";
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

    protected void FillItem()
    {
        lblMsg.Text = "";
        try
        {
            if (ddlItemCategory.SelectedIndex != 0)
            {

                ddlItem.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                        new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id" },
                                        new string[] { "3", ViewState["Office_ID"].ToString(), ItemGroup.SelectedItem.Value, ddlItemCategory.SelectedItem.Value }, "Dataset");
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
                ddlItem.Enabled = true;
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

    protected void FillUnit()
    {
        lblMsg.Text = "";
        try
        {
            if (ddlItem.SelectedIndex != 0)
            {

                ddlUnit.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                     new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id" },
                                     new string[] { "4", ViewState["Office_ID"].ToString(), ItemGroup.SelectedItem.Value, ddlItemCategory.SelectedItem.Value, ddlItem.SelectedItem.Value }, "Dataset");
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "Unit_id";
                ddlUnit.DataBind();
                ddlUnit.Enabled = false;
            }
            else
            {
                ddlUnit.Items.Clear();
                ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnit.Enabled = false;

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
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillUnit();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            FillItem();
            FillUnit();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}