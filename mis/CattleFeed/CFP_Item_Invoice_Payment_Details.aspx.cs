using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_CFP_Item_Invoice_Payment_Details : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        Category();
    }
    private void fillProdUnit()
    {
        try
        {
            ddlcfp.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
            ddlcfp.DataSource = ds;
            ddlcfp.DataValueField = "CFPOfficeID";
            ddlcfp.DataTextField = "CFPName";
            ddlcfp.DataBind();
            ddlcfp.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            //  lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void Category()
    {
        try
        {
            ddlitemcategory.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemCategoryList",
                                  new string[] { "flag" },
                                  new string[] { "0" }, "dataset");
            ddlitemcategory.DataSource = ds;
            ddlitemcategory.DataTextField = "CFP_ItemCatName";
            ddlitemcategory.DataValueField = "CFPItemCat_id";
            ddlitemcategory.DataBind();
            ddlitemcategory.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {


        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }

    }
    private void ItemType()
    {
        try
        {
            ddlitemtype.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemTypeList",
                                  new string[] { "flag", "CategoryID" },
                                  new string[] { "0", ddlitemcategory.SelectedValue }, "dataset");
            ddlitemtype.DataSource = ds;
            ddlitemtype.DataTextField = "ItemTypeName";
            ddlitemtype.DataValueField = "ItemType_id";
            ddlitemtype.DataBind();
            ddlitemtype.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void fillItemByType()
    {
        try
        {
            ddlitems.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFPItems_By_ItemTypeID_CFPID_List", new string[] { "flag", "ItemTypeID", "CFPID" }, new string[] { "0", ddlitemtype.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            ddlitems.DataSource = ds;
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataBind();
            ddlitems.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void fillIteminvoiceByItem()
    {
        try
        {
            ddlItemChallan.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_Item_Received_Invoice_By_ItemID", new string[] { "flag", "itemID", "CFPID" }, new string[] { "0", ddlitems.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            ddlItemChallan.DataSource = ds;
            ddlItemChallan.DataValueField = "ItemReceivedID";
            ddlItemChallan.DataTextField = "CFPInvoiceNo";
            ddlItemChallan.DataBind();
            ddlItemChallan.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemType();
    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    private void fillIteminvoiceDetailByItemrecid()
    {
        try
        {

            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_By_ID_Detail", new string[] { "flag", "ItemReceivedID" }, new string[] { "0", ddlItemChallan.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                paypnl.Visible = true;
                lblItem.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                lblpurchaseorder.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Purchase_Order_No"]);
                lblpurchaseorderdate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Purchase_Order_Date"]);
                lblInvoiceNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Invoice_No"]);
                lblInvoiceDT.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFPInvoiceDate"]);
                lblQuantity.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Quantity"]);
                lblrate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Rate"])+" /-";
                lblTotalAmt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                lblPaidAmt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["PaymentAmount"]);
                lblPayablepercentage.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["paymentPercentage"]);
                lblunit.InnerText = "Per " + Convert.ToString(ds.Tables[0].Rows[0]["UnitName"])+" .";
                lblunit1.InnerText = " "+Convert.ToString(ds.Tables[0].Rows[0]["UnitName"]);
                lblrembalance.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["RemainingAmount"]);
                fillGRD();
                //hdnstatus.Value = Convert.ToString(ds.Tables[0].Rows[0]["Payable_Status"]);
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void fillGRD()
    {
        DataSet ds1 = new DataSet();
        try
        {

           
            ds1 = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Payment_Detail_by_ID_List", new string[] { "flag", "ItemReceivedID" }, new string[] { "0", ddlItemChallan.SelectedValue }, "dataset");
            grdCatlist.DataSource = ds1;
            grdCatlist.DataBind();

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds1.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void ddlitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIteminvoiceByItem();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        fillIteminvoiceDetailByItemrecid();
    }
}