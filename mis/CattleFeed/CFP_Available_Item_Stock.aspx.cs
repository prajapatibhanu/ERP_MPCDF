using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_CFP_Available_Item_Stock : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void Category()
    {
        try
        {
            ddlItemCategory.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemCategoryList",
                                  new string[] { "flag" },
                                  new string[] { "0" }, "dataset");
            ddlItemCategory.DataSource = ds;
            ddlItemCategory.DataTextField = "CFP_ItemCatName";
            ddlItemCategory.DataValueField = "CFPItemCat_id";
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {


        }
        finally { ds.Dispose(); }

    }
    private void ItemType()
    {
        try
        {
            ddlItemType.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemTypeList",
                                  new string[] { "flag", "CategoryID" },
                                  new string[] { "0", ddlItemCategory.SelectedValue }, "dataset");
            ddlItemType.DataSource = ds;
            ddlItemType.DataTextField = "ItemTypeName";
            ddlItemType.DataValueField = "ItemType_id";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            ddlItem.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFPItems_By_ItemTypeID_List", new string[] { "flag", "ItemTypeID" }, new string[] { "0", ddlItemType.SelectedValue }, "dataset");
            ddlItem.DataSource = ds;
            ddlItem.DataValueField = "Item_id";
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemType();
    }
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();

    }
    private void FillGRD()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFPItemsAvailableStock", new string[] { "flag", "CFPID", "ItemCatID", "ItemTypeID", "ItemID", "UptoDate" }, new string[] { "0", ddlcfp.SelectedValue, ddlItemCategory.SelectedValue, ddlItemType.SelectedValue, ddlItem.SelectedValue, txtdate.Text }, "dataset");
            grdlist.DataSource = ds;
            grdlist.DataBind();
        }
        catch (Exception)
        {
           
        }
        finally
        {

            GC.SuppressFinalize(ds);
            GC.SuppressFinalize(objdb);
        }
       
    }
    protected void grdlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlist.PageIndex = e.NewPageIndex;
        FillGRD();

    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        FillGRD();
    }
}