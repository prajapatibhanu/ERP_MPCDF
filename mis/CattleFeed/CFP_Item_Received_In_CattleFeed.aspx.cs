using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_CFP_Item_Received_In_CattleFeed : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
    }
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
    private void fillInvoice()
    {
        try
        {
            ddlInvoice.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_InvoiceNo_By_CFP", new string[] { "flag", "CFPID" }, new string[] { "0", ddlCFP.SelectedValue }, "dataset");
            ddlInvoice.DataSource = ds;
            ddlInvoice.DataValueField = "BigGRNo";
            ddlInvoice.DataTextField = "InvoiceNo";
            ddlInvoice.DataBind();
            ddlInvoice.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void fillGST()
    {
        try
        {
            rbtnGSTType.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Mst_CFP_Item_GST_Type_List", new string[] { "flag" }, new string[] { "0" }, "dataset");
            rbtnGSTType.DataSource = ds;
            rbtnGSTType.DataValueField = "CFP_Item_Rate_Type_ID";
            rbtnGSTType.DataTextField = "CFP_Item_Rate_Type";
            rbtnGSTType.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void ddlCFP_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillInvoice();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ddlCFP.Enabled = false;
        ddlInvoice.Enabled = false;
        paypnl.Visible = true;
        fillGST();
        rbtnGSTType.SelectedValue = "2";
        fillInvoiceDetail();
    }
    protected void rbtnGSTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnGSTType.SelectedValue == "1")
        { gstpnl.Visible = true; }
        else { gstpnl.Visible = false; txtGSTPercentage.Text = "0"; }

        decimal reatamt = 0, transportcharge = 0, GSTPERC = 0, Amt = 0;
        if (txtReatAmount.Text == string.Empty)
        {
            reatamt = 0;
        }
        else { reatamt = Convert.ToDecimal(txtReatAmount.Text); }
        if (txttransportcharge.Text == string.Empty)
        {

            transportcharge = 0;
        }
        else { transportcharge = Convert.ToDecimal(txttransportcharge.Text); }
        if (txtGSTPercentage.Text == string.Empty)
        {

            GSTPERC = 0;
        }
        else { GSTPERC = Convert.ToDecimal(txtGSTPercentage.Text); }
        Amt = reatamt + transportcharge + ((GSTPERC / 100) * reatamt);
        txtAmount.Text = Convert.ToString(Amt);
    }
    private void fillInvoiceDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_Detail_BY_INVOICENo", new string[] { "flag", "CFPID", "BigGRNo" }, new string[] { "0", ddlCFP.SelectedValue, ddlInvoice.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblBigGRGRNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["BigGRNo"]);
                    lblsmallGR.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["SMALLGRNo"]);
                    lblcfpname.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Name"]);
                    hdnCFPItemNoteID.Value = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Items_Receipts_Note_ID"]);
                    lblItemName.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                    lblOnReceivedDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["OnReceivedDate"]);
                    lbSupplier.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Supplier_Name"]);
                    //txtGRNO.Text = Convert.ToString(ds.Tables[0].Rows[0]["Item_GR_No"]);
                    txtReceiveddate.Text = Convert.ToString(ds.Tables[0].Rows[0]["Received_Date"]);

                    lblInvoiceNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                    lblTruckNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_No"]);
                    lblItemBagsNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NoofBags"]);
                    lbllrrno.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["BigGRNo"]);
                    lblTruckSupplyDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_Loaded_Date"]);
                    lblTotalGrosswt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Total_Gross_Wt"]);
                    lbltarewt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Less_Tara_Wt"]);
                    lblNetWt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NET_Wt"]);
                    lblWtMT.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["QuantityMT"]);
                    txtRate.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                    txtReatAmount.Text = Convert.ToString(Convert.ToDecimal(ds.Tables[0].Rows[0]["QuantityMT"]) * Convert.ToDecimal(ds.Tables[0].Rows[0]["Rate"]));
                    // txtReatAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["ReatAmount"]);
                    txttransportcharge.Text = Convert.ToString(ds.Tables[0].Rows[0]["TransportCharge"]);
                    txtPOno.Text = Convert.ToString(ds.Tables[0].Rows[0]["Purchase_Order_No"]);
                    txtpurchaseDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["Purchase_Order_Date"]);
                    hdnCFPPurchase_Order_ID.Value = Convert.ToString(ds.Tables[0].Rows[0]["CFP_ITEM_Purchase_Order_ID"]);

                    //tg.InnerText = "Total Gross Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    //ltw.InnerText = "Less Tare Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    //NW.InnerText = "Net Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    WT.InnerText = "Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    // rbtnGSTType
                    if (Convert.ToString(ds.Tables[0].Rows[0]["GST_Type_ID"]) == "0")
                    {
                        gstpnl.Visible = false;
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["GST_Type_ID"]) == "1")
                    {
                        rbtnGSTType.SelectedValue = "1";
                        gstpnl.Visible = true;
                        txtGSTPercentage.Text = Convert.ToString(ds.Tables[0].Rows[0]["GSTPercentage"]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["GST_Type_ID"]) == "2")
                    {
                        rbtnGSTType.SelectedValue = "2";
                        gstpnl.Visible = false;
                        txtGSTPercentage.Text = "0";
                    }
                    if (Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalAmount"]) != 0)
                        txtAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalAmount"]);
                    else
                        txtAmount.Text = Convert.ToString((Convert.ToDecimal(lblWtMT.InnerText) * Convert.ToDecimal(txtRate.Text))+Convert.ToDecimal(txttransportcharge.Text ));

                    txtRemark.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remark"]);
                    hdnvalue.Value = Convert.ToString(ds.Tables[0].Rows[0]["ItemReceivedID"]);
                    hdnItemID.Value = Convert.ToString(ds.Tables[0].Rows[0]["Item_ID"]);
                    if (hdnvalue.Value != "0") { btnSave.Visible = false; } else { btnSave.Visible = true; }
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private bool GetCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtpurchaseDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtReceiveddate.Text; // From Database
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ds = new DataSet();
                if (GetCompareDate())
                {
                   


                    string flag = "0";
                    if (Convert.ToInt32(hdnvalue.Value) > 0)
                    {
                        flag = "1";
                    }

                    ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Insert",
                        new string[] { "flag", "Received_Date", "Item_ID", "Quantity", "Remark", "Office_ID", "Inserted_By", "Inserted_IP", "Amount", "CFPID", "Item_GR_No", "QuantityMT", "GST_Type_ID", "ReatAmount", "TransportCharge", "GSTPercentage", "CFP_Items_Receipts_Note_ID", "DepartmentID", "Rate", "PurchaseOrder", "PurchaseOrderDate", "CFP_ITEM_Purchase_Order_ID" },
                        new string[] { flag, txtReceiveddate.Text, hdnItemID.Value, lblNetWt.InnerText, txtRemark.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, txtAmount.Text, ddlCFP.SelectedValue, lblsmallGR.InnerText, lblWtMT.InnerText, rbtnGSTType.SelectedValue, txtReatAmount.Text, txttransportcharge.Text, txtGSTPercentage.Text, hdnCFPItemNoteID.Value, "1", txtRate.Text, txtPOno.Text, txtpurchaseDate.Text, hdnCFPPurchase_Order_ID.Value }, "TableSave");
                    if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                    {
                        fillInvoiceDetail();
                        //fillGrd();
                        //Clear();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", "Received date should be less than Purchase date.");

                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lblMsg.Text = string.Empty;
        paypnl.Visible = false;
        lblcfpname.InnerText = string.Empty;
        hdnCFPItemNoteID.Value = "0";
        lblItemName.InnerText = string.Empty;
        lblOnReceivedDate.InnerText = string.Empty;
        lbSupplier.InnerText = string.Empty;
        lblsmallGR.InnerText = string.Empty;
        txtReceiveddate.Text = string.Empty;
        lblInvoiceNo.InnerText = string.Empty;
        lblTruckNO.InnerText = string.Empty;
        lblItemBagsNo.InnerText = string.Empty;
        lbllrrno.InnerText = string.Empty;
        lblTruckSupplyDate.InnerText = string.Empty;
        lblTotalGrosswt.InnerText = string.Empty;
        lbltarewt.InnerText = string.Empty;
        lblNetWt.InnerText = string.Empty;
        lblWtMT.InnerText = string.Empty;
        txtRate.Text = string.Empty;
        txtReatAmount.Text = string.Empty;
        txttransportcharge.Text = string.Empty;

        rbtnGSTType.ClearSelection();
        txtGSTPercentage.Text = string.Empty;
        gstpnl.Visible = false;

        txtAmount.Text = string.Empty;
        txtRemark.Text = string.Empty;
        hdnvalue.Value = "0";
        hdnItemID.Value = "0";
        if (hdnvalue.Value != "0") { btnSave.Visible = false; }
        ddlCFP.Enabled = true;
        ddlCFP.SelectedValue = "0";
        fillInvoice();
        ddlInvoice.SelectedValue = "0";
        ddlInvoice.Enabled = true;
    }
    public static bool isFloatValue(string text)
    {
        Regex regex = new Regex(@"^\d*\.?\d?$");
        return regex.IsMatch(text);
    }
    protected void txttransportcharge_TextChanged(object sender, EventArgs e)
    {
        decimal reatamt = 0, transportcharge = 0, GSTPERC = 0, Amt = 0;
        if (txtReatAmount.Text == string.Empty)
        {
            reatamt = 0;
        }
        else {
            reatamt = Convert.ToDecimal(txtReatAmount.Text); }
        if (txttransportcharge.Text == string.Empty)
        {

            transportcharge = 0;
        }
        else {
            if (isFloatValue(txttransportcharge.Text))
            {
                transportcharge = Convert.ToDecimal(txttransportcharge.Text);
            }
            else
            {
                txttransportcharge.Text = "0";
                txttransportcharge.Focus();
            }
        }
        if (txtGSTPercentage.Text == string.Empty)
        {

            GSTPERC = 0;
        }
        else { GSTPERC = Convert.ToDecimal(txtGSTPercentage.Text); }
        Amt = reatamt + transportcharge + ((GSTPERC / 100) * reatamt);
        txtAmount.Text = Convert.ToString(Amt);
    }
    protected void txtGSTPercentage_TextChanged(object sender, EventArgs e)
    {
        decimal reatamt = 0, transportcharge = 0, GSTPERC = 0, Amt = 0;
        if (txtReatAmount.Text == string.Empty)
        {
            reatamt = 0;
        }
        else { reatamt = Convert.ToDecimal(txtReatAmount.Text); }
        if (txttransportcharge.Text == string.Empty)
        {

            transportcharge = 0;
        }
        else { transportcharge = Convert.ToDecimal(txttransportcharge.Text); }
        if (txtGSTPercentage.Text == string.Empty)
        {

            GSTPERC = 0;
        }
        else { GSTPERC = Convert.ToDecimal(txtGSTPercentage.Text); }
        Amt = reatamt + transportcharge + ((GSTPERC / 100) * reatamt);
        txtAmount.Text = Convert.ToString(Amt);
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        decimal rateamt = 0, transportcharge = 0, GSTPERC = 0, Amt = 0, calAMT = 0;

        if (txtRate.Text == string.Empty)
        {
            rateamt = 0;
        }
        else { rateamt = Convert.ToDecimal(txtRate.Text); }
        calAMT = (rateamt * Convert.ToDecimal(lblWtMT.InnerText));
        txtReatAmount.Text = Convert.ToString(calAMT);
        //if (txttransportcharge.Text == string.Empty)
        //{

        //    transportcharge = 0;
        //}
        //else { transportcharge = Convert.ToDecimal(txttransportcharge.Text); }
        //if (txtGSTPercentage.Text == string.Empty)
        //{

        //    GSTPERC = 0;
        //}
        //else { GSTPERC = Convert.ToDecimal(txtGSTPercentage.Text); }
        //Amt = calAMT + transportcharge + ((GSTPERC / 100) * calAMT);
        //txtAmount.Text = Convert.ToString(Amt);
    }
}