using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_CattleFeed_Items_Receipts_Note : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        fillProdUnit();
       
       // fillGrd();
    }
    //private void fillMAXID()
    //{
    //    try
    //    {

    //        ds = new DataSet();
    //        ds = objdb.ByProcedure("SP_Max_TRrn_CFP_Items_Receipts_NoteID", new string[] { "flag","CFP_ID" }, new string[] { "0",ddlCFP.SelectedValue }, "dataset");
    //        lblInvoiceno.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Items_Receipts_Note_ID"]);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
    //    }
    //    finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    //}
    private void fillProdUnit()
    {
        try
        {
            ddlCFP.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
            ddlCFP.DataSource = ds;
            ddlCFP.DataValueField = "CFPOfficeID";
            ddlCFP.DataTextField = "CFPName";
            ddlCFP.DataBind();
            ddlCFP.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void fillPO()
    {
        try
        {
            ddlPO.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_By_CFPID", new string[] { "flag", "CFPID" }, new string[] { "0", ddlCFP.SelectedValue }, "dataset");
            ddlPO.DataSource = ds;
            ddlPO.DataValueField = "CFP_ITEM_Purchase_Order_ID";
            ddlPO.DataTextField = "Item_PO_NO";
            ddlPO.DataBind();
            ddlPO.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
   
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertProductRate();
        fillGrd();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ClearText();
    }
    private bool GetTenderCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txttruckRecdate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtSupplierDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate >= tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnAmt.Value != "0") { lblNetWt.Text = hdnAmt.Value; }
            if (hdnconverted.Value != "0") { txtconverted.Text = hdnconverted.Value; }
           
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
            ds = new DataSet();
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (GetTenderCompareDate())
                {

                    StringBuilder sb = new StringBuilder();
                    if (txtTotalGrossWeight.Text != string.Empty && txtTareWt.Text != string.Empty) {
                        if (Convert.ToDouble(txtTotalGrossWeight.Text) < Convert.ToDouble(txtTareWt.Text))
                        {
                            sb.Append("Tare Wt should be less than Gross Wt .\\n");
                        }
                    }
                    if (lblitemquantity.Text != string.Empty && txtconverted.Text != string.Empty)
                    {
                        if (Convert.ToDouble(lblitemquantity.Text) < Convert.ToDouble(txtconverted.Text))
                        {
                            sb.Append("Net WT should be less than Item Quantity WT .\\n");
                        }
                    }
                    if (sb.ToString() == string.Empty)
                    {
                        string flag = "0";
                        if (Convert.ToInt32(hdnvalue.Value) > 0)
                        {
                            flag = "1";
                        }
                        ds = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_Insert",
                            new string[] { "flag", "CFP_ID", "Supplier_Name", "Supplier_Tin_No", "Item_Cat_ID", "Item_Type_ID", "Item_ID", "Received_Quantity", "Truck_No", "Item_Recieved_On_Date", "LRBRBillNo", "Truck_Loaded_Date", "Total_Gross_Wt", "Less_Tara_Wt", "NET_Wt", "SmallGRNo", "Driver_Name", "Driver_Contact_No", "Remark", "OfficeID", "InsertedBy", "InsertedIP", "CFPItemsReceiptsNoteID", "SmallGRDocumentNO", "CFP_ITEM_Purchase_Order_ID" },
                            new string[] { flag, ddlCFP.SelectedValue, txtSupplier.Text, txtTinno.Text, hdnItemCatID.Value, hdnItemTypeID.Value, hdnItemID.Value, txtRecQuantity.Text.Trim(), txttruckno.Text.Trim(), txttruckRecdate.Text.Trim(), txtlrbr.Text, txtSupplierDate.Text, txtTotalGrossWeight.Text, txtTareWt.Text, hdnAmt.Value, txtsmallGRNO.Text, txtDriver.Text, txtDriverContactNo.Text, txtRemark.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, hdnvalue.Value, txtsmallgrdocument.Text, ddlPO.SelectedValue }, "TableSave");
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
                        lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", sb.ToString());

                    }
                  
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", "Truck loaded date should be less than Item Received date.");

                }

            }
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void ClearText()
    {
        ddlCFP.SelectedValue = "0";
        ddlCFP.Enabled = true;
        fillPO();
        ddlPO.SelectedValue = "0";
        ddlPO.Enabled = true;
        POReceiptDetail.Visible = false;
        txtSupplier.Text = string.Empty;
        txtTinno.Text = string.Empty;
        hdnItemCatID.Value = "0";
        hdnItemTypeID.Value = "0";
        hdnItemID.Value = "0";
        lblitemquantity.Text = string.Empty;
        txtRecQuantity.Text = string.Empty;
        txtRecQuantity.Enabled = true;
        txttruckno.Text = string.Empty;
        txttruckRecdate.Text = string.Empty;
        txtlrbr.Text = string.Empty;
        txtlrbr.Enabled = true;
        txtSupplierDate.Text = string.Empty;
        txtTotalGrossWeight.Text = string.Empty;
        txtTotalGrossWeight.Enabled = true;
        txtTareWt.Text = string.Empty;
        txtTareWt.Enabled = true;
        lblNetWt.Text = string.Empty;
        txtsmallGRNO.Text = string.Empty;
        txtsmallGRNO.Enabled = true;
        txtsmallgrdocument.Text = string.Empty;
        //lblInvoiceno.Text = string.Empty;
        txtDriver.Text = string.Empty;
        txtDriverContactNo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        hdnvalue.Value = "0";
        btnSubmit.Text = "Save";
        grdCatlist.DataSource = null;
        grdCatlist.DataBind();
    }
    private void Clear()
    {
        //ddlCFP.SelectedValue = "0";
        //ddlCFP.Enabled = true;
        //txtSupplier.Text = string.Empty;
        //txtTinno.Text = string.Empty;
        //ddlitemcategory.SelectedValue = "0";
        //ddlitemcategory.Enabled = true;
        //ItemType();
        //ddlitemtype.SelectedValue = "0";
        //ddlitemtype.Enabled = true;
        //fillItemByType();
        //ddlitems.SelectedValue = "0";
        //ddlitems.Enabled = true;
        txtRecQuantity.Text = string.Empty;
        txtRecQuantity.Enabled = true;
        txttruckno.Text = string.Empty;
        txttruckRecdate.Text = string.Empty;
        txtlrbr.Text = string.Empty;
        txtlrbr.Enabled = true;
        txtSupplierDate.Text = string.Empty;
        txtTotalGrossWeight.Text = string.Empty;
        txtTotalGrossWeight.Enabled = true;
        txtTareWt.Text = string.Empty;
        txtTareWt.Enabled = true;
        lblNetWt.Text = string.Empty;
        txtconverted.Text = string.Empty;
        txtsmallGRNO.Text = string.Empty;
        txtsmallGRNO.Enabled = true;
        txtsmallgrdocument.Text = string.Empty;
        //lblInvoiceno.Text = string.Empty;
        txtDriver.Text = string.Empty;
        txtDriverContactNo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        hdnvalue.Value = "0";
        btnSubmit.Text = "Save";
    }

    private void fillGrd()
    {
        try
        {

            ds = new DataSet();
            ds = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_Detail_ByID", new string[] { "flag", "OfficeID", "CFPID", "CFPITEMPurchaseOrderID" }, new string[] { "0", objdb.Office_ID(),ddlCFP.SelectedValue,ddlPO.SelectedValue }, "dataset");
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();
        }
        catch (Exception ex) { }
        finally { }
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        llbMsg2.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_Detail_ByNoteID", new string[] { "flag", "CFPItemsReceiptsNoteID" }, new string[] { "0", hdnvalue.Value }, "dataset");
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlCFP.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ID"]);
                        ddlCFP.Enabled = false;
                       // GetTinNO();
                        txtSupplier.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Supplier_Name"]);
                        txtTinno.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Supplier_Tin_No"]);
                        //ddlitemcategory.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Item_Cat_ID"]);
                        //ddlitemcategory.Enabled = false;
                        //ItemType();
                        //ddlitemtype.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Item_Type_ID"]);
                        //ddlitemtype.Enabled = false;
                        //fillItemByType();
                        //ddlitems.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Item_ID"]);
                        //ddlitems.Enabled = false;
                        txtRecQuantity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Received_Quantity"]);
                        txtRecQuantity.Enabled = false;
                        txttruckno.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Truck_No"]);
                        txttruckRecdate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Item_Recieved_On_Date"]);
                        txtlrbr.Text = Convert.ToString(ds1.Tables[0].Rows[0]["LRBRBillNo"]);
                        txtlrbr.Enabled = false;
                        txtSupplierDate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Truck_Loaded_Date"]);
                        txtTotalGrossWeight.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Total_Gross_Wt"]);
                        txtTotalGrossWeight.Enabled = false;
                        txtTareWt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Less_Tara_Wt"]);
                        txtTareWt.Enabled = false;
                        lblNetWt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["NET_Wt"]);
                        hdnAmt.Value = Convert.ToString(ds1.Tables[0].Rows[0]["NET_Wt"]);
                        txtsmallGRNO.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SMALLGRNo"]);
                        txtsmallGRNO.Enabled = false;
                        txtsmallgrdocument.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SmallGRDocumentNO"]);
                        //lblInvoiceno.Text = Convert.ToString(ds1.Tables[0].Rows[0]["InvoiceNo"]);
                        txtDriver.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Driver_Name"]);
                        txtDriverContactNo.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Driver_Contact_No"]);
                        txtRemark.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Remark"]);
                        btnSubmit.Text = "Edit";
                    }
                }
                break;
            case "RecordDetail":
                DataSet ds2 = new DataSet();

                ds2 = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_GRINVOICE_ByID", new string[] { "flag", "CFPItemsReceiptsNoteID" }, new string[] { "0", Convert.ToString(hdnvalue.Value) }, "dataset");
                if (ds2 != null)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        txtGRNO.Text = Convert.ToString(ds2.Tables[0].Rows[0]["BigGRNo"]);
                        txtMaterialNo.Text = Convert.ToString(ds2.Tables[0].Rows[0]["InvoiceNo"]);
                    }
                }

                GC.SuppressFinalize(ds2);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            case "RecordReport":
                FillInvoiceDetail();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
                break;
            default:
                break;
        }
    }
    private void FillInvoiceDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_Items_Receipts_Note_BY_ItemReceptNotID_List", new string[] { "flag", "CFPItemsReceiptsNoteID" }, new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                   /// paypnl.Visible = true;
                    lblSmallGRNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["SMALLGRNo"]);
                    lblcfpname.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFPName"]);
                    lblcfp.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP"]);
                    lbSupplier.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Supplier_Name"]);
                    lblReceivedqty.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Received_Quantity"]);
                    lblItemReceivedDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Item_Recieved_On_Date"]);
                    lblItem.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                    lblTruckNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_No"]);
                    lbllrrno.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["LRBRBillNo"]);
                    lblTruckloadingDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_Loaded_Date"]);
                    lblTotalGrosswt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Total_Gross_Wt"]);
                    lbltarewt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Less_Tara_Wt"]);
                    lblNetWt1.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NET_Wt"]);
                    lblRemark.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Remark"]);
                    lblDriver.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Driver_Name"]);
                    lbDriverContact.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Driver_Contact_No"]);
                    lblInvoiceNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                    lblGrNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["BigGRNo"]);
                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void grdCatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCatlist.PageIndex = e.NewPageIndex;
        fillGrd();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataSet ds2 = new DataSet();

        ds2 = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_UpdateGRINVOICE_ByID", new string[] { "flag", "BigGRNo", "InvoiceNo", "CFPItemsReceiptsNoteID" }, new string[] { "0", txtGRNO.Text, txtMaterialNo.Text, Convert.ToString(hdnvalue.Value) }, "dataset");
        if (ds2 != null)
        {

            if (ds2.Tables[0].Rows[0]["ERRORMsg"].ToString() == "SUCCESS")
            {
               
                //llbMsg2.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
               fillGrd();
            }
            else
            {
                llbMsg2.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
            }
        }

    }
    protected void ddlCFP_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPO();
        //GetTinNO();
    }

    private void GetTinNO()
    {
        DataSet ds2 = new DataSet();
        try
        {
            ds2 = objdb.ByProcedure("SP_Mst_CFPOfficeRegistration_TANNOBYOffceID", new string[] { "flag", "CFPOfficeID" }, new string[] { "0", ddlCFP.SelectedValue }, "dataset");
            if (ds2 != null)
            {

                if (ds2.Tables[0].Rows.Count > 0)
                {

                    txtTinno.Text = ds2.Tables[0].Rows[0]["CFPTANNO"].ToString();
                }

            }
        }
        catch (Exception ex)
        {
            
            //throw;
        }
        finally { ds2.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void FillPOeDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_Purchase_Order_ID_List", new string[] { "flag", "CFPITEMPurchaseOrderID", "CFPID" }, new string[] { "0", ddlPO.SelectedValue, ddlCFP.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    POReceiptDetail.Visible = true;
                    /// paypnl.Visible = true;
                    txtSupplier.Text = Convert.ToString(ds.Tables[0].Rows[0]["Vendor_Name"]);
                    txtTinno.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFP_TAN_NO"]);
                    lblItemCat.Text = Convert.ToString(ds.Tables[0].Rows[0]["ItemCatName"]);
                    lblItemType.Text = Convert.ToString(ds.Tables[0].Rows[0]["ItemTypeName"]);
                    lblItemname.Text = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                    lblRef.Text = Convert.ToString(ds.Tables[0].Rows[0]["Reference_NO"]);
                    lblitemquantity.Text = Convert.ToString(ds.Tables[0].Rows[0]["Item_Quantity"]);
                    hdnItemCatID.Value=Convert.ToString(ds.Tables[0].Rows[0]["Item_Cat_ID"]);
                    hdnItemTypeID.Value=Convert.ToString(ds.Tables[0].Rows[0]["Item_Type_ID"]);
                    hdnItemID.Value = Convert.ToString(ds.Tables[0].Rows[0]["Item_ID"]);
                    lblunit.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]);
                    lblunit1.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]);
                    //lblcfpname.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFPName"]);
                    //lblcfp.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP"]);
                    //lbSupplier.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Supplier_Name"]);
                    //lblReceivedqty.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Received_Quantity"]);
                    //lblItemReceivedDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Item_Recieved_On_Date"]);
                    //lblItem.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                    //lblTruckNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_No"]);
                    //lbllrrno.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["LRBRBillNo"]);
                    //lblTruckloadingDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_Loaded_Date"]);
                    //lblTotalGrosswt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Total_Gross_Wt"]);
                    //lbltarewt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Less_Tara_Wt"]);
                    //lblNetWt1.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NET_Wt"]);
                    //lblRemark.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Remark"]);
                    //lblDriver.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Driver_Name"]);
                    //lbDriverContact.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Driver_Contact_No"]);
                    //lblInvoiceNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                    //lblGrNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["BigGRNo"]);
                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        FillPOeDetail();
        ddlCFP.Enabled = false;
        ddlPO.Enabled = false;

        Clear();
        fillGrd();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
}