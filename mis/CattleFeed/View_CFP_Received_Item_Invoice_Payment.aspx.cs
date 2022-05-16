using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_View_CFP_Received_Item_Invoice_Payment : System.Web.UI.Page
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
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnpaymentid.Value = Convert.ToString(e.CommandArgument);

        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("SP_Trn_CFP_Item_Test_Variable_For_Received_Payment_BY_PAYMENTID", new string[] { "flag", "ItemReceivedPaymentID" }, new string[] { "0", hdnpaymentid.Value }, "dataset");
                GridView2.DataSource = ds1;
                GridView2.DataBind();
                GC.SuppressFinalize(ds1);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            default:
                break;
        }
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
        fillGRD();
    }
    private void fillGRD()
    {
        try
        {

            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Payment_Detail_By_GRNO", new string[] { "flag", "CFPID", "GRNO" }, new string[] { "0", ddlCFP.SelectedValue, ddlInvoice.SelectedValue }, "dataset");
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        paypnl.Visible = false;
        ddlCFP.Enabled = true;
        ddlCFP.SelectedValue = "0";
        fillInvoice();
        ddlInvoice.SelectedValue = "0";
        ddlInvoice.Enabled = true;
        hdnpaymentid.Value = "0";
    }
}