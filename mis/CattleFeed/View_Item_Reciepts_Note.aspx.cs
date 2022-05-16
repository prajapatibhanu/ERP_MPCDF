using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_View_Item_Reciepts_Note : System.Web.UI.Page
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
    protected void ddlCFP_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillInvoice();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillInvoiceDetail();
    }
    private void FillInvoiceDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_Items_Receipts_Note_BY_InvoiceNo_List", new string[] { "flag", "BigGRNO" }, new string[] { "0", ddlInvoice.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    paypnl.Visible = true;
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
                    lblNetWt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NET_Wt"]);
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
}