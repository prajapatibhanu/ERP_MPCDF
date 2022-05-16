using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;


public partial class mis_CattleFeed_ItemReceiving : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        Category();
        Unit();
        fillGrd();
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
    private void Unit()
    {
        try
        {
            ddlUnit.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SpUnit",
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

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); }
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
            ds = objdb.ByProcedure("SP_CFPItems_By_ItemTypeID_CFP_List", new string[] { "flag", "ItemTypeID", "CFPID" }, new string[] { "0", ddlitemtype.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            ddlitems.DataSource = ds;
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataBind();
            ddlitems.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }

    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillItemByType();

    }
    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ItemType();
    }
    private void fillGrd()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_Local_Item_Received_By_OfficeID_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
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
            GC.SuppressFinalize(objdb);
        }

    }
    private bool filecheck()
    {
        bool res = true;
        if (Fileinvoice.HasFile)
        {
            string Extension = Path.GetExtension(Fileinvoice.FileName);
            if (Extension.ToLower().Contains("jpg") || Extension.ToLower().Contains("png") || Extension.ToLower().Contains("pdf") || Extension.ToLower().Contains("jpeg") || Extension.ToLower().Contains("gif"))
            {
                res = true;
            }
            else
                res = false;
        }
        return res;
    }
    private bool CheckInvoiceNo()
    {
        bool res = false;
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_BYInvoiceNO_CFPID", new string[] { "flag", "CFPID", "InvoiceNo" }, new string[] { "0", ddlcfp.SelectedValue, txtinvoiceno.Text }, "dataset");
            res = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsExist"]);
        }
        catch (Exception)
        {

            res = Convert.ToBoolean(0);
        }
        finally
        {
            GC.SuppressFinalize(ds);
            GC.SuppressFinalize(objdb);
        }
        return res;
    }
    private bool GetCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtSupplyDate.Text; // From Database
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
            if (hdnamt.Value != "0") { txtAmount.Text = hdnamt.Value; }

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetinvoiceCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtInvoiceDate.Text; // From Database
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
            if (hdnamt.Value != "0") { txtAmount.Text = hdnamt.Value; }

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private void InsertProductRate()
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ds = new DataSet();
                if (GetCompareDate())
                {
                    if (GetinvoiceCompareDate())
                    {
                        if (filecheck())
                        {

                            string attachment = string.Empty;
                            string flag = "0";
                            if (Convert.ToInt32(hdnvalue.Value) > 0)
                            {
                                flag = "1";
                            }
                            if (Fileinvoice.HasFile)
                            {
                                attachment = Guid.NewGuid() + "-" + "IR-" + Fileinvoice.FileName;
                                Fileinvoice.PostedFile.SaveAs(Server.MapPath("~/mis/CattleFeed/Upload/" + attachment));
                            }
                            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Insert_Update_Delete",
                                new string[] { "flag", "Item_ID", "Quantity", "Rate", "Supplier_Name", "Supply_Date", "Remark", "OfficeID", "InsertedBY", "IPAddress", "ItemReceivedID", "Amount", "ItemInvoicePath", "CFPID", "InvoiceNo", "InvoiceDate", "DepartmentID" },
                                new string[] { flag, ddlitems.SelectedValue, txtQuantity.Text, txtRate.Text.Trim(), txtSupplier.Text.Trim(), txtSupplyDate.Text.Trim(), txtRemark.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, hdnvalue.Value, hdnamt.Value, attachment, ddlcfp.SelectedValue, txtinvoiceno.Text, txtInvoiceDate.Text, "1" }, "TableSave");
                            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                            {
                                fillGrd();
                                Clear();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : file extension should be PDF,JPG OR PNG.");
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry!", "Error : Invoice date should not be greater than current date");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry!", "Error : Receive date should not be greater than current date");
                }

            }
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }

    private void Clear()
    {
        //txtTransactionDt.Text=string.Empty;
        //txtPONo.Text = string.Empty;
        //txtpodate.Text = string.Empty;
        ddlcfp.Enabled = true;
        ddlitemcategory.SelectedValue = "0";
        ddlitemcategory.Enabled = true;
        ddlcfp.SelectedValue = "0";
        ItemType();
        ddlitemtype.SelectedValue = "0";
        ddlitemtype.Enabled = true;
        fillItemByType();
        ddlitems.SelectedValue = "0";
        ddlitems.Enabled = true;
        ddlUnit.SelectedValue = "0";
        txtAmount.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtSupplier.Text = string.Empty;
        txtSupplyDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtinvoiceno.Text = string.Empty;
        txtinvoiceno.Enabled = true;
        txtInvoiceDate.Enabled = true;
        txtInvoiceDate.Text = string.Empty;
        hdnvalue.Value = "0";
        btnSubmit.Text = "Save";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hdnvalue.Value == "0")
        {
            if (CheckInvoiceNo() == false)
            {
                InsertProductRate();
            }
            else { lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Invoice No is duplcate."); }
        }
        else
            InsertProductRate();


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
        lblMsg.Text = string.Empty;
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("SP_CFP_Item_Received_By_ReceivedID_List", new string[] { "flag", "ItemReceivedID" }, new string[] { "0", hdnvalue.Value }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //txtTransactionDt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ReceivedDate"]);
                    ddlcfp.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFPID"]);
                    ddlcfp.Enabled = false;
                    //txtPONo.Text = Convert.ToString(ds1.Tables[0].Rows[0]["PONo"]);
                    //txtpodate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["PODate"]);
                    ddlitemcategory.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCatid"]);
                    ddlitemcategory.Enabled = false;
                    ItemType();
                    ddlitemtype.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemTypeid"]);
                    ddlitemtype.Enabled = false;
                    fillItemByType();

                    ddlitems.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemID"]);
                    ddlitems.Enabled = false;
                    fillItemUnit();
                    txtQuantity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Quantity"]);
                    txtRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);
                    txtSupplier.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SupplierName"]);
                    txtSupplyDate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SupplyDate"]);
                    txtRemark.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Remark"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["InvoiceNo"]))) { txtinvoiceno.Enabled = false; }
                    txtinvoiceno.Text = Convert.ToString(ds1.Tables[0].Rows[0]["InvoiceNo"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CFPInvoiceDate"]))) { txtInvoiceDate.Enabled = false; }

                    txtInvoiceDate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["CFPInvoiceDate"]);
                    txtAmount.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]) == "0" ? Convert.ToString(Convert.ToDouble(txtQuantity.Text) * Convert.ToDouble(txtRate.Text)) : Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]);
                    btnSubmit.Text = "Edit";
                }
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds1);
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Insert_Update_Delete",
                   new string[] { "flag", "Item_ID", "Quantity", "Rate", "Supplier_Name", "Supply_Date", "Remark", "OfficeID", "InsertedBY", "IPAddress", "ItemReceivedID", "Amount", "ItemInvoicePath", "CFPID", "InvoiceNo", "InvoiceDate", "DepartmentID" },
                   new string[] { "2", ddlitems.SelectedValue, txtQuantity.Text, txtRate.Text.Trim(), txtSupplier.Text.Trim(), txtSupplyDate.Text.Trim(), txtRemark.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, hdnvalue.Value, "0", "", ddlcfp.SelectedValue, txtinvoiceno.Text, txtInvoiceDate.Text, "1" }, "TableSave");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    fillGrd();
                    Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                }
                GC.SuppressFinalize(objdb);
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
    protected void ddlitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillItemUnit();
    }
    private void fillItemUnit()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_ItemUnit_Under_Item", new string[] { "flag", "ItemID" }, new string[] { "0", ddlitems.SelectedValue }, "dataset");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }

    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillItemByType();
    }
}