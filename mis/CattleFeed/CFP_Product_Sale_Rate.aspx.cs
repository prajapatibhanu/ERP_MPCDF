using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
public partial class mis_CattleFeed_CFP_Product_Sale_Rate : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        fillproduct();
        fillGrd();
        fillLocType();
        office.Visible = false;
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
    private void fillproduct()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPItems_By_ItemTypeID_List",
                            new string[] { "flag", "ItemTypeID" },
                            new string[] { "0", "4" }, "dataset");
            ddlProd.DataSource = ds;
            ddlProd.DataTextField = "ItemName";
            ddlProd.DataValueField = "Item_id";
            ddlProd.DataBind();
            ddlProd.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            GC.SuppressFinalize(ds);
            GC.SuppressFinalize(objapi);
        }
    }
    private void fillGrd()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Product_Sale_By_Office_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();
            grdCatlist.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdCatlist.UseAccessibleHeader = true;

        }
        catch (Exception)
        {


        }
        finally
        {
            GC.SuppressFinalize(ds);
            GC.SuppressFinalize(objapi);
        }

    }
    private void Clear()
    {
        ddlProd.SelectedValue = "0";
        FillProductSizeByProductID();
        ddlpackaging.SelectedValue = "0";
        ddlcfp.SelectedValue = "0";
        txtRate.Text = string.Empty;
        txtfromDt.Text = string.Empty;
        ddlpackaging.SelectedValue = "0";
        ddlOfficeType.SelectedValue = "0";
        fillLoc();
        ddlOffice.SelectedValue = "0";
        hdnvalue.Value = "0";
        ddlProd.Enabled = true;
        ddlpackaging.Enabled = true;
        ddlOfficeType.Enabled = true;
        ddlOffice.Enabled = true;
        btnsave.Text = "Save/सुरक्षित";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    private bool FillCheckProductrecord()
    {
        bool result = true;
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_ProductExist_CFP_Product_Sale",
                                         new string[] { "flag", "ProductID", "OfficeID", "CFPID" },
                                         new string[] { "0", ddlProd.SelectedValue, ddlOffice.SelectedValue, ddlcfp.SelectedValue }, "dataset");


            result = Convert.ToBoolean(ds.Tables[0].Rows[0]["IExist"]);


        }
        catch (Exception)
        {

        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }
        return result;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (hdnvalue.Value == "0")
        {
            if (!FillCheckProductrecord()) { InsertProductRate(); }
            else { lblMsg.Text = objapi.Alert("fa-check", "alert-error", "Thank You!", "Fail : Rate for Product and selected office already exist."); }
        }
        else InsertProductRate();


    }

    private void InsertProductRate()
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                StringBuilder sb = new StringBuilder();
                ds = new DataSet();
                if (ddlOfficeType.SelectedValue != "0")
                {
                    if (ddlOfficeType.SelectedValue != "99")
                    {
                        if (ddlOffice.SelectedValue == "0")
                        {
                            sb.Append("Select office\\n");
                        }
                    }
                    
                }
                else
                {
                    sb.Append("Select office type\\n");
                }
                if (sb.ToString() == string.Empty)
                {
                    string flag = "0";
                    if (Convert.ToInt32(hdnvalue.Value) > 0)
                    {
                        flag = "1";
                    }
                    ds = objapi.ByProcedure("SP_Mst_CFP_Product_Sale_Insert_Update_Delete",
                        new string[] { "flag", "ProductID", "Rate", "CFPProductSizeID", "EffectiveDate", "SaleOfficeTypeID", "SaleOfficeID", "Office_ID", "InsertedID", "ProductSaleID", "CFPID" },
                        new string[] { flag, ddlProd.SelectedValue, txtRate.Text.Trim(), ddlpackaging.SelectedValue, txtfromDt.Text.Trim(), ddlOfficeType.SelectedValue, ddlOffice.SelectedValue, objapi.Office_ID(), objapi.createdBy(), hdnvalue.Value, ddlcfp.SelectedValue }, "TableSave");
                    if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                    {
                        fillGrd();
                        Clear();
                        lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                    }
                    else
                    {
                        lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                    }
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Sorry!", sb.ToString());
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objapi.ByProcedure("SP_CFP_Product_Sale_By_ID_List", new string[] { "flag", "ProductSaleID" }, new string[] { "0", hdnvalue.Value }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlcfp.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ID"]);
                    ddlProd.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ProductID"]);
                    FillProductSizeByProductID();
                    ddlpackaging.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ProductSize_ID"]);
                    txtRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);
                    txtfromDt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["EffectiveDate"]);
                    ddlOfficeType.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["SaleOfficeTypeID"]);
                    fillLoc();
                    ddlOffice.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["SaleOfficeID"]);
                    ddlProd.Enabled = false;
                    ddlpackaging.Enabled = false;
                    ddlOfficeType.Enabled = false;
                    ddlOffice.Enabled = false;
                    btnsave.Text = "Edit";
                }
                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds1);
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_Mst_CFP_Product_Sale_Insert_Update_Delete",
                  new string[] { "flag", "ProductID", "Rate", "EffectiveDate", "Office_ID", "InsertedID", "ProductSaleID", "CFPID" },
                  new string[] { "2", ddlProd.SelectedValue, txtRate.Text.Trim(), txtfromDt.Text.Trim(), objapi.Office_ID(), objapi.createdBy(), hdnvalue.Value, ddlcfp.SelectedValue }, "TableSave");

                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    fillGrd();
                    Clear();
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                }
                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds);
                break;
            default:
                break;
        }
    }
    protected void grdCatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCatlist.PageIndex = e.NewPageIndex;
        fillGrd();
    }
    private void FillProductSizeByProductID()
    {

        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Product_Size_By_ProductID_List",
                                         new string[] { "flag", "ProductID", "CFPID" },
                                         new string[] { "0", ddlProd.SelectedValue, ddlcfp.SelectedValue }, "dataset");

            ddlpackaging.DataSource = ds;
            ddlpackaging.DataTextField = "PackagingSize";
            ddlpackaging.DataValueField = "CFPProductSizeID";
            ddlpackaging.DataBind();
            ddlpackaging.Items.Insert(0, new ListItem("-- Select --", "0"));
            //hdnproductsize.Value = Convert.ToString(ds.Tables[0].Rows[0]["CFPProductSizeID"]);
            //txtproductsize.Text = Convert.ToString(ds.Tables[0].Rows[0]["PackagingSize"]);

        }
        catch (Exception)
        {

        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }

    }
    private void fillLocType()
    {
        ddlOfficeType.Items.Clear();
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_CFP_OfficeType_PS_List", new string[] { "flag" }, new string[] { "0" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlOfficeType.DataSource = ds;
            ddlOfficeType.DataValueField = "OfficeType_ID";
            ddlOfficeType.DataTextField = "OfficeTypeName";
            ddlOfficeType.DataBind();

        }
        ddlOfficeType.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objapi);

    }
    private void fillLoc()
    {
        if (ddlOfficeType.SelectedValue != "99")
        {
            office.Visible = true;
            ddlOffice.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_AdminOffice_ByType", new string[] { "flag", "OfficeTypeID" }, new string[] { "0", ddlOfficeType.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataBind();

            }
            ddlOffice.Items.Insert(0, new ListItem("-- Select --", "0"));
            GC.SuppressFinalize(objapi);
        }
        else
        { office.Visible = false; ddlOffice.SelectedValue = "0"; }
        if (ddlOfficeType.SelectedValue == "0")
        { office.Visible = false; ddlOffice.SelectedValue = "0"; }

    }
    protected void ddlProd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillProductSizeByProductID();
    }
    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillLoc();
    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproduct();
    }
}