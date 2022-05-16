using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_CFP_AddSpecifications : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        fillCategory();
        fillType();
        ddlitemtype.SelectedValue = "7";
        ddlitemCat.Enabled = false;
        ddlitemtype.Enabled = false;
        fillIteRatio();
        fillGrd();
    }
    private void fillCategory()
    {
        try
        {
            ddlitemCat.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_ItemCategoryList", new string[] { "flag" }, new string[] { "0" }, "dataset");
            ddlitemCat.DataSource = ds;
            ddlitemCat.DataValueField = "CFPItemCat_id";
            ddlitemCat.DataTextField = "CFP_ItemCatName";
            ddlitemCat.DataBind();
            ddlitemCat.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlitemCat.SelectedValue = "3";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillType()
    {
        try
        {
            ddlitemtype.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPItemType_ItemCat_List", new string[] { "flag", "ItemCatID" }, new string[] { "0", ddlitemCat.SelectedValue }, "dataset");
            ddlitemtype.DataSource = ds;
            ddlitemtype.DataValueField = "ItemTypeid";
            ddlitemtype.DataTextField = "ItemTypeName";
            ddlitemtype.DataBind();
            ddlitemtype.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillItemByType()
    {
        try
        {
            ddlitems.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPItems_By_ItemTypeID_CFP_List", new string[] { "flag", "ItemTypeID", "CFPID" }, new string[] { "0", ddlitemtype.SelectedValue, ddlProdUnit.SelectedValue }, "dataset");
            ddlitems.DataSource = ds;
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataBind();
            ddlitems.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillProdUnit()
    {
        try
        {
            ddlProdUnit.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0",objapi.Office_ID() }, "dataset");
            ddlProdUnit.DataSource = ds;
            ddlProdUnit.DataValueField = "CFPOfficeID";
            ddlProdUnit.DataTextField = "CFPName";
            ddlProdUnit.DataBind();
            ddlProdUnit.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillIteRatio()
    {
        try
        {
            ddlItemRatio.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_ItemRatioList", new string[] { "flag" }, new string[] { "0" }, "dataset");
            ddlItemRatio.DataSource = ds;
            ddlItemRatio.DataValueField = "ItemRationID";
            ddlItemRatio.DataTextField = "ItemRation";
            ddlItemRatio.DataBind();
            ddlItemRatio.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillProdunderCFP()
    {
        try
        {
            ddlProdName.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Items_Implementation_CFPofDSList", new string[] { "flag", "CFPID" }, new string[] { "0", ddlProdUnit.SelectedValue }, "dataset");
            ddlProdName.DataSource = ds;
            ddlProdName.DataValueField = "Itemid";
            ddlProdName.DataTextField = "ItemName";
            ddlProdName.DataBind();
            ddlProdName.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateDelete();
    }
    private void InsertUpdateDelete()
    {
        try
        {
            //if (!Checktrnfortrndtitmexist())
            //{
            string flag = "0";
            if (Convert.ToInt32(hdnvalue.Value) > 0)
            {
                flag = "1";
            }
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Product_Specifcation_Insert_Update_Delete",
                        new string[] { "flag", "ProductID", "OfficeID", "ItemRatioID", "ItemCatID", "ItemTypeID", "ItemID", "InsertedBy", "InsertedIP", "CFPProductSpecifcationID", "CFPID" },
                        new string[] { flag, ddlProdName.SelectedValue, objapi.Office_ID(), ddlItemRatio.SelectedValue, ddlitemCat.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, objapi.createdBy(), Request.UserHostAddress, hdnvalue.Value, ddlProdUnit.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
            {
                lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                fillGrd();
                Clear();
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
            }
            //}
            //else
            //{
            //    lblMsg.Text = objapi.Alert("fa-ban", "alert-warning", "Failed ! Stock for Transaction date and item is already.", " ");
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }
    }

    
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
    }
    private void Clear()
    {
        ddlProdUnit.SelectedValue="0" ;
        fillProdunderCFP();
        ddlProdName.SelectedValue="0" ;
        ddlitemCat.SelectedValue="3" ;
        fillType();
        ddlitemtype.SelectedValue="7" ;
        
        ddlitemCat.Enabled = false;
        ddlitemtype.Enabled = false;
        fillItemByType();
        ddlitems.SelectedValue="0" ;
        ddlItemRatio.SelectedValue = "0";
        hdnvalue.Value = "0";
        btnSubmit.Text = "Save";
    }
    protected void ddlProdUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillProdunderCFP();
        fillItemByType();
    }
    protected void ddlitemCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillType();
    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    protected void gvOpeningStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "RecordUpdate":
                fillitembySpecification();
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_Product_Specifcation_Insert_Update_Delete",
                        new string[] { "flag", "ProductID", "OfficeID", "ItemRatioID", "ItemCatID", "ItemTypeID", "ItemID", "InsertedBy", "InsertedIP", "CFPProductSpecifcationID", "CFPID" },
                        new string[] { "2", ddlProdName.SelectedValue, objapi.Office_ID(), ddlItemRatio.SelectedValue, ddlitemCat.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, objapi.createdBy(), Request.UserHostAddress, hdnvalue.Value, ddlProdUnit.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
            {
                lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                fillGrd();
                Clear();
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
            }
                break;
            default:
                break;
        }
    }
    private void fillitembySpecification()
    {
        DataSet ds1 = new DataSet();
        try
        {

            ds1 = objapi.ByProcedure("SP_CFP_Product_Specifcation_By_SpecificationID_List",
                            new string[] { "flag", "CFPProductSpecifcationID" },
                            new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlProdUnit.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFPID"]);
                fillProdunderCFP();
                ddlProdName.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ProductID"]);
                ddlitemCat.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCatID"]);
                fillType();
                ddlitemtype.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemTypeid"]);
                fillItemByType();
                ddlitems.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Itemid"]);
                ddlItemRatio.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemRatioID"]);

                btnSubmit.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally
        {
            ds1.Dispose();
            GC.SuppressFinalize(objapi);
        }
    }
    private void fillGrd()
    {

        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Product_Specifcation_BY_Office_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            gvOpeningStock.DataSource = ds;
            gvOpeningStock.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void gvOpeningStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOpeningStock.PageIndex = e.NewPageIndex;
        fillGrd();
    }
}