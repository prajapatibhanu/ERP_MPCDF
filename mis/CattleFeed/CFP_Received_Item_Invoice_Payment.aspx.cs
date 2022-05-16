using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_CattleFeed_CFP_Received_Item_Invoice_Payment : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Category();
        fillProdUnit();
        recv.Visible = false;
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
     private void fillStatus()
    {
        try
        {
            ddlStatus.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Mst_CFP_Payment_Status", new string[] { "flag" }, new string[] { "0" }, "dataset");
            ddlStatus.DataSource = ds;
            ddlStatus.DataValueField = "CFPPaymentStatusID";
            ddlStatus.DataTextField = "CFPPaymentStatus";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-- Select --", "0"));

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
    private void fillPaymentMode()
    {
        try
        {
            ddlpaymentmode.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_Payment_Mode", new string[] { "flag" }, new string[] { "0" }, "dataset");
            ddlpaymentmode.DataSource = ds;
            ddlpaymentmode.DataValueField = "PaymentModeID";
            ddlpaymentmode.DataTextField = "PaymentMode";
            ddlpaymentmode.DataBind();
            ddlpaymentmode.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void fillIteminvoiceDetailByItemrecid()
    {
        try
        {

            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Detail_By_ID", new string[] { "flag", "ItemReceivedID" }, new string[] { "0", hdnitemrecid.Value }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblInvoiceDT.Text = Convert.ToString(ds.Tables[0].Rows[0]["CFPInvoiceDate"]);
                lblQuantity.Text = Convert.ToString(ds.Tables[0].Rows[0]["Quantity"]);
                lblrate.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                lblTotalAmt.Text = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                lblpayableAmt.Text = Convert.ToString(ds.Tables[0].Rows[0]["PaymentAmount"]);
                hdnstatus.Value = Convert.ToString(ds.Tables[0].Rows[0]["Payable_Status"]);
            }

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
    protected void ddlitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIteminvoiceByItem();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Payment_Insert",
                          new string[] { "flag", "ItemReceivedID", "PayablePercentage", "TotalAmount", "PayableAmount", "RemainingBalance", "PaymentStatus", "PaymentModeID", "PaymentNO", "PaymentDate", "PaymentAmount", "Remark", "CFPID", "OfficeID", "InsertedBY", "IPAddress" },
                          new string[] { "0", hdnitemrecid.Value, txtPayablepercentage.Text, lblTotalAmt.Text, lblPaidAmt.Text, lblrembalance.Text.Trim(), ddlStatus.SelectedValue, ddlpaymentmode.SelectedValue, txtneftno.Text,txtneftDate.Text,txtneftamt.Text, txtRemark.Text,ddlcfp.SelectedValue,objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress }, "TableSave");
            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
            {
                fillGRD();
                clearsection();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
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
        try
        {

            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Payment_Detail_By_ReceiveID", new string[] { "flag", "ItemReceivedID" }, new string[] { "0", hdnitemrecid.Value }, "dataset");
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        recv.Visible = true;
        btnSave.Enabled = false;
        paypnl.Visible = true;
        ddlitemcategory.Enabled = false;
        ddlitemtype.Enabled = false;
        ddlcfp.Enabled = false;
        ddlitems.Enabled = false;
        ddlItemChallan.Enabled = false;
        hdnitemrecid.Value = ddlItemChallan.SelectedValue;
        fillIteminvoiceDetailByItemrecid();
        fillPaymentMode();
        fillStatus();
        txtPayablepercentage.Text=string.Empty;
        fillGRD();
        lblPaidAmt.Text = string.Empty;
       // lblTotalAmt.Text=string.Empty;
        lblrembalance.Text=string.Empty;
        ddlpaymentmode.SelectedValue = "0";
        txtneftno.Text=string.Empty;
        txtneftDate.Text=string.Empty;
        txtneftamt.Text=string.Empty;
        txtRemark.Text=string.Empty;
        if (hdnstatus.Value == "2") { btnSubmit.Visible = false; }
    }
    private void clearsection()
    {
        lblpayableAmt.Text=string.Empty;
        txtPayablepercentage.Text=string.Empty;
        lblPaidAmt.Text=string.Empty;
        lblrembalance.Text=string.Empty;
        ddlStatus.SelectedValue="0";
        paymntsection.Visible = false;
        ddlpaymentmode.SelectedValue = "0";
        txtneftno.Text=string.Empty;
        txtneftDate.Text=string.Empty;
        txtneftamt.Text=string.Empty;
        txtRemark.Text=string.Empty;
        fillIteminvoiceDetailByItemrecid();
        if (hdnstatus.Value == "2") { btnSubmit.Visible = false; }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        hdnstatus.Value="0";
        hdnitemrecid.Value="0";
        txtRemark.Text=string.Empty;
        txtneftamt.Text=string.Empty;
        txtneftDate.Text=string.Empty;
        txtneftno.Text=string.Empty;
        ddlpaymentmode.SelectedValue = "0";
        paymntsection.Visible=false;
        ddlStatus.SelectedValue = "0";
        lblrembalance.Text=string.Empty;
        lblPaidAmt.Text=string.Empty;
        txtPayablepercentage.Text=string.Empty;
        lblpayableAmt.Text=string.Empty;
        lblTotalAmt.Text=string.Empty;
        lblrate.Text=string.Empty;
        lblQuantity.Text=string.Empty;
        lblInvoiceDT.Text=string.Empty;
        paypnl.Visible = false;
        ddlitemcategory.SelectedValue="0";
        ddlitemcategory.Enabled = true;
        ItemType();
        ddlitemtype.SelectedValue="0";
        ddlitemtype.Enabled = true;
        ddlcfp.SelectedValue="0";
        ddlcfp.Enabled = true;
        fillItemByType();
        ddlitems.SelectedValue="0";
        ddlitems.Enabled = true;
        fillIteminvoiceByItem();
        ddlItemChallan.SelectedValue="0";
        ddlItemChallan.Enabled = true;
        btnSave.Enabled = true;
        grdCatlist.DataSource = null;
        grdCatlist.DataBind();
        recv.Visible = false;
    }
    protected void txtPayablepercentage_TextChanged(object sender, EventArgs e)
    {
        int payperc = Convert.ToInt32(txtPayablepercentage.Text);
        double payamt = 0;
        if (Convert.ToDouble(lblpayableAmt.Text)>0)
            payamt = Convert.ToDouble(lblpayableAmt.Text);
        else
            payamt = Convert.ToDouble(lblTotalAmt.Text);
        double Amountrem = (payperc * payamt) / 100;
        lblPaidAmt.Text = Convert.ToString(Amountrem);
        if (Convert.ToDouble(lblpayableAmt.Text) > 0)
            lblrembalance.Text = Convert.ToString(Convert.ToDouble(lblpayableAmt.Text) - Convert.ToDouble(lblPaidAmt.Text));
        else
            lblrembalance.Text = Convert.ToString(Convert.ToDouble(lblTotalAmt.Text) - Convert.ToDouble(lblPaidAmt.Text));

      //  txtneftamt.Text = Convert.ToString(lblTotalAmt.Text);
    }
    protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpaymentmode.SelectedValue == "1")
        {
            payno.InnerText = "NEFT No";
            paydate.InnerText = "NEFT Date";
            payAmt.InnerText = "NEFT Amount";
        }
        else if (ddlpaymentmode.SelectedValue == "2")
        {
            payno.InnerText = "RTGS No";
            paydate.InnerText = "RTGS Date";
            payAmt.InnerText = "RTGS Amount";
        }
        else if (ddlpaymentmode.SelectedValue == "3")
        {
            payno.InnerText = "CHEQUE No";
            paydate.InnerText = "CHEQUE Date";
            payAmt.InnerText = "CHEQUE Amount";
        }
        else
        {
            payno.InnerText = "Payment No";
            paydate.InnerText = "Payment Date";
            payAmt.InnerText = "Payment Amount";

        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "1")
        {
            paymntsection.Visible = true;
            txtneftamt.Text = lblPaidAmt.Text;
        }
        else
        {
            paymntsection.Visible = false;
            txtneftno.Text = string.Empty;
            txtneftDate.Text = string.Empty;
            txtneftamt.Text = string.Empty;
        }
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}