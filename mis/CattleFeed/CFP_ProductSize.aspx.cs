using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_CattleFeed_CFP_ProductSize : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        Unit();
        fillGrd();
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


        }
        finally { ds.Dispose(); }
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
    private void fillProdunderCFP()
    {
        try
        {
            ddlProd.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Items_Implementation_CFPofDSList", new string[] { "flag", "CFPID" }, new string[] { "0", ddlcfp.SelectedValue }, "dataset");
            ddlProd.DataSource = ds;
            ddlProd.DataValueField = "Itemid";
            ddlProd.DataTextField = "ItemName";
            ddlProd.DataBind();
            ddlProd.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private Int32 fillUnitunderItem()
    {
        lblMsg.Text = "";
        Int32 UnitID = 0;
        try
        {

            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPItemsUnitByItemID", new string[] { "flag", "ItemID" }, new string[] { "0", ddlProd.SelectedValue }, "dataset");
            UnitID = Convert.ToInt32(ds.Tables[0].Rows[0]["Unitid"]);
        }
        catch (Exception ex)
        {
            UnitID = 0;
            //lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
        return UnitID;
    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillProdunderCFP();
    }

    protected void ddlProd_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlUnit.SelectedValue = Convert.ToString(fillUnitunderItem());
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateDelete();
    }
    private void fillGrd()
    {

        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Product_Size_By_Office_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            gvOpeningStock.DataSource = ds;
            gvOpeningStock.DataBind();
            gvOpeningStock.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvOpeningStock.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void gvOpeningStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                fillitembyProductSize();
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_Product_Size_Insert_Update_Delete",
                            new string[] { "flag", "CFP_ID", "Product_ID", "UnitID", "Packaging_Size", "OfficeID", "InsertBy", "IpAddress", "CFPProductSizeID", "EffectiveDate" },
                            new string[] { "2", ddlcfp.SelectedValue, ddlProd.SelectedItem.Value, ddlUnit.SelectedItem.Value, txtPackagSize.Text, objapi.Office_ID(), objapi.createdBy(), Request.UserHostAddress, hdnvalue.Value ,txtfromDt.Text}, "dataset");
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
    private void fillitembyProductSize()
    {
        DataSet ds1 = new DataSet();
        try
        {

            ds1 = objapi.ByProcedure("SP_CFP_Product_Size_By_ID_List",
                            new string[] { "flag", "CFPProductSizeID" },
                            new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlcfp.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ID"]);
                fillProdunderCFP();
                ddlProd.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Product_ID"]);
                ddlUnit.SelectedValue = Convert.ToString(fillUnitunderItem());
                txtPackagSize.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Packaging_Size"]);
                txtfromDt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["EffectiveDate"]);
                ddlProd.Enabled = false;
                ddlcfp.Enabled = false;
                //txtRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);


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
            ds = objapi.ByProcedure("SP_CFP_Product_Size_Insert_Update_Delete",
                        new string[] { "flag", "CFP_ID", "Product_ID", "UnitID", "Packaging_Size",  "OfficeID", "InsertBy", "IpAddress", "CFPProductSizeID","EffectiveDate" },
                        new string[] { flag, ddlcfp.SelectedValue, ddlProd.SelectedItem.Value, ddlUnit.SelectedItem.Value, txtPackagSize.Text,  objapi.Office_ID(), objapi.createdBy(), Request.UserHostAddress, hdnvalue.Value ,txtfromDt.Text}, "dataset");
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

    private void Clear()
    {
        ddlcfp.SelectedValue = "0";
        fillProdunderCFP();
        ddlProd.SelectedValue = "0";
        ddlUnit.SelectedValue = "0";
        txtPackagSize.Text = string.Empty;
       // txtRate.Text = string.Empty;
        hdnvalue.Value = "0";
        ddlProd.Enabled = true;
        ddlcfp.Enabled = true;
        btnSubmit.Text = "Save";
    }

    protected void gvOpeningStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOpeningStock.PageIndex = e.NewPageIndex;
        fillGrd();
    }
}