using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;

public partial class mis_CattleFeed_OpeningStock : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        lblMsg.Text = string.Empty;
        fillCategory();
        fillGrd();
        Unit();
    }
    private void fillProdUnit()
    {
        try
        {
            ddlcfp.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            ddlcfp.DataSource = ds;
            ddlcfp.DataValueField = "CFPOfficeID";
            ddlcfp.DataTextField = "CFPName";
            ddlcfp.DataBind();
            ddlcfp.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void gvOpeningStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "RecordUpdate":
                fillitembystockid();
              
                break;
            case "RecordDelete":
                 ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_ItemStock_Insert_Update_Delete",
                            new string[] { "flag", "TranDt", "ItemCatid", "ItemTypeid", "Itemid", "Quantity", "RequestedPage", "Remark", "Office_Id", "CFP_ID", "IPAddress",  "InsertedBy", "CFPItemStockID" },
                            new string[] { "2", txtTransactionDt.Text, ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtQty.Text, Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Inserted", objapi.Office_ID(), ddlcfp.SelectedValue, Request.UserHostAddress, objapi.createdBy(), hdnvalue.Value }, "dataset");
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
    private void Unit()
    {
        try
        {
            ddlUnit.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SpUnit",
                                  new string[] { "flag" },
                                  new string[] { "7" }, "dataset");
            ddlUnit.DataSource = ds;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); }
    }
    private bool Checktrnfortrndtitmexist()
    {

        ds = new DataSet();
        ds = objapi.ByProcedure("CheckTrnExist_CFP_ItemStockBYTrnDT_Item",
                    new string[] { "flag", "TrnDT", "Itemid", "CFPID" },
                    new string[] { "0", txtTransactionDt.Text, ddlitems.SelectedItem.Value,ddlcfp.SelectedValue }, "dataset");
        return Convert.ToBoolean(ds.Tables[0].Rows[0]["IsItemExist"]);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdnvalue.Value) == 0)
        {
            if (!Checktrnfortrndtitmexist())
            {
                InsertUpdateDelete();
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-warning", "Failed ! Stock for Transaction date and item is already.", " ");
            }
        }
        else
        {
            InsertUpdateDelete();
        }

    }
    private void fillitembystockid()
    {
        DataSet ds1 = new DataSet();
        try
        {
           
            ds1 = objapi.ByProcedure("CFP_ItemStock_By_StockID_List",
                            new string[] { "flag", "ItemStockID" },
                            new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlcfp.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ID"]);
                txtTransactionDt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TranDt"]);
                txtTransactionDt.Enabled = false;
                ddlitemcategory.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCatid"]);
                fillType();
                ddlitemtype.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemTypeid"]);
                fillItemByType();
                ddlitems.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Itemid"]);
                txtQty.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Quantity"]);
                fillItemUnit();
                //txtRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);
                //txtAmount.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]);
                btnSubmit.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " "); 
        }
        finally{
            ds1.Dispose();
            GC.SuppressFinalize(objapi);
        }
    }
    private void Clear()
    {
        txtTransactionDt.Text = string.Empty;
        txtTransactionDt.Enabled = true;
        ddlitemcategory.SelectedValue = "0";
        fillType();
        ddlitemtype.SelectedValue = "0";
        fillItemByType();
        ddlitems.SelectedValue = "0";
        txtQty.Text = string.Empty;
        ddlUnit.SelectedValue = "0";
        //txtRate.Text = string.Empty;
        //txtAmount.Text = string.Empty;
        btnSubmit.Text = "Save";
        //lblMsg.Text = string.Empty;
    }
    private bool GetCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtTransactionDt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = DateTime.Now.ToString("dd/MM/yyyy"); // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
           

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private void InsertUpdateDelete()
    {
        try
        {
            if (GetCompareDate())
            {
                string flag = "0";
                if (Convert.ToInt32(hdnvalue.Value) > 0)
                {
                    flag = "1";
                }
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_ItemStock_Insert_Update_Delete",
                            new string[] { "flag", "TranDt", "ItemCatid", "ItemTypeid", "Itemid", "Quantity", "RequestedPage", "Remark", "Office_Id", "CFP_ID", "IPAddress",  "InsertedBy", "CFPItemStockID" },
                            new string[] { flag, txtTransactionDt.Text, ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtQty.Text, Path.GetFileName(Request.Url.AbsolutePath), "Opening Stock Record Inserted", objapi.Office_ID(), ddlcfp.SelectedValue, Request.UserHostAddress,  objapi.createdBy(), hdnvalue.Value }, "dataset");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Details Successfully Completed ");
                    fillGrd();
                    Clear();
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Failed ! Due to Some technical problem. Please try it again.", " ");
                }
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Failed ! Stock for Transaction date should not greater then current date.", " ");
            }
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
    private void fillCategory()
    {
        try
        {
            ddlitemcategory.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_ItemCategoryList", new string[] { "flag" }, new string[] { "0" }, "dataset");
            ddlitemcategory.DataSource = ds;
            ddlitemcategory.DataValueField = "CFPItemCat_id";
            ddlitemcategory.DataTextField = "CFP_ItemCatName";
            ddlitemcategory.DataBind();
            ddlitemcategory.Items.Insert(0, new ListItem("-- Select --", "0"));

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
            ds = objapi.ByProcedure("SP_CFPItemType_ItemCat_List", new string[] { "flag", "ItemCatID" }, new string[] { "0", ddlitemcategory.SelectedValue }, "dataset");
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
            ds = objapi.ByProcedure("SP_CFPItems_By_ItemTypeID_CFP_List", new string[] { "flag", "ItemTypeID", "CFPID" }, new string[] { "0", ddlitemtype.SelectedValue,ddlcfp.SelectedValue }, "dataset");
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
    private void fillGrd()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("CFP_ItemStock_By_OfficeID_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            gvOpeningStock.DataSource = ds;
            gvOpeningStock.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillType();
        fillItemByType();   
    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
    }
    private void fillItemUnit()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_ItemUnit_Under_Item", new string[] { "flag", "ItemID" }, new string[] { "0", ddlitems.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlUnit.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Unit_id"]);
                }
                else
                    ddlUnit.SelectedValue = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void ddlitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemUnit();

    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
}