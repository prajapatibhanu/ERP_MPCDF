using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class mis_CattleFeed_CFP_Received_Item_For_Payment_In_CattleFeed : System.Web.UI.Page
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
    private void fillInvoiceDetail()
    {
        lblMsg.Text = string.Empty;
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_TRrn_CFP_Items_Receipts_Note_Detail_BY_INVOICENo_For_Payment", new string[] { "flag", "CFPID", "BigGRNo" }, new string[] { "0", ddlCFP.SelectedValue, ddlInvoice.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    paypnl.Visible = true;

                    lblSmallGRNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["SMALLGRNo"]);
                    lblcfpname.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Name"]);
                    hdnCFPItemNoteID.Value = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Items_Receipts_Note_ID"]);
                    lblItemName.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                    lblItemReceiveddate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Received_Date"]);
                    lbSupplier.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Supplier_Name"]);
                    lblGRNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Item_GR_No"]);

                    lblInvoiceNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                    lblTruckNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_No"]);
                    lblItemBagsNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NoofBags"]);
                    lbllrrno.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["BigGRNo"]);
                    lblTruckSupplyDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Truck_Loaded_Date"]);
                    lblTotalGrosswt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Total_Gross_Wt"]);
                    lbltarewt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Less_Tara_Wt"]);
                    lblNetWt.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NET_Wt"]);
                    lblWtMT.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["QuantityMT"]);
                    lblReatAmount.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                    lbltransportcharge.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["TransportCharge"]);
                    tg.InnerText = "Total Gross Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    ltw.InnerText = "Less Tare Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    NW.InnerText = "Net Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    WT.InnerText = "Weight(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                    lblGSTIncluded.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Item_Rate_Type"]);
                    lblGSTpercentage.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["GSTPercentage"]);
                    lblAmount.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["TotalAmount"]);
                    lblpayableAmt.Text = Convert.ToString(ds.Tables[0].Rows[0]["Payment_Amount"]);

                    hdnUnitID.Value = Convert.ToString(ds.Tables[0].Rows[0]["Unit_id"]);
                    hdnvalue.Value = Convert.ToString(ds.Tables[0].Rows[0]["ItemReceivedID"]);
                    hdnItemID.Value = Convert.ToString(ds.Tables[0].Rows[0]["Item_ID"]);
                    if (lblpayableAmt.Text != "0") { btnSave.Visible = true; ddlpaypercentage.Enabled = true; } else { btnSave.Visible = false; ddlpaypercentage.Enabled = false; lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Payment Invoice for selected Invoice No has been Generated!", ""); }
                }
                else
                {
                    paypnl.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Please Receive this Item before payment!", "");
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
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
        fillInvoiceDetail();
        fillGRD();
        ddlpaypercentage.SelectedValue = "0";
        txtDeduction.Text = "0";
        txtRebateAmt.Text = "0";
        txtRebate.Text = "0";
        lblPaidAmt.Text = "0";
        txtpaneltyAmt.Text = "0";
        grdlist.DataSource = null;
        grdlist.DataBind();
        iteminkgfirst.Visible = false;
        //fillGST();
        //rbtnGSTType.SelectedValue = "2";
        //fillInvoiceDetail();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lblMsg.Text = string.Empty;
        paypnl.Visible = false;
        hdnvalue.Value = "0";
        if (hdnvalue.Value != "0") { btnSave.Visible = false; }
        ddlCFP.Enabled = true;
        ddlCFP.SelectedValue = "0";
        fillInvoice();
        ddlInvoice.SelectedValue = "0";
        ddlInvoice.Enabled = true;
        hdntobepaidamount.Value = "0";
        hdnUnitID.Value = "0";
        hdnItemID.Value = "0";
        hdnCFPItemNoteID.Value = "0";
        hdnCFPItemRecieveID.Value = "0";
        ddlpaypercentage.SelectedValue = "0";
        //hdntobepaidamount,hdnUnitID,hdnItemID,hdnCFPItemNoteID,hdnCFPItemRecieveID,hdnvalue
        hdntobepaidamount.Value = "0";
        lblPaidAmt.Text = string.Empty;
        lblrembalance.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRebate.Text = "0";
        txtRebateAmt.Text = "0";
        txtDeduction.Text = "0";
        txtpaneltyAmt.Text = "0";
        grdlist.DataSource = null;
        grdlist.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Item_Received_Payment_Invoice_Insert",
                          new string[] { "flag", "ItemReceivedID", "CFPItemsReceiptsNoteID", "PayablePercentage", "TotalAmount", "TobePaid", "PayableAmount", "RemainingBalance", "RebateAmt", "RebatePercentage", "OtherDeductonAmt", "Remark", "CFPID", "OfficeID", "InsertedBY", "IPAddress", "PaneltyAmt", "Str" },
                          new string[] { "0", hdnvalue.Value, hdnCFPItemNoteID.Value, ddlpaypercentage.SelectedValue, lblAmount.InnerText, hdntobepaidamount.Value, lblPaidAmt.Text, lblrembalance.Text.Trim(), txtRebateAmt.Text, txtRebate.Text, txtDeduction.Text, txtRemark.Text, ddlCFP.SelectedValue, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress,txtpaneltyAmt.Text, GetXML() }, "TableSave");
            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
            {
                fillGRD();
                fillInvoiceDetail();
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
    private string GetXML()
    {
        XmlWriterSettings xws = new XmlWriterSettings();
        xws.Indent = true;
        xws.NewLineOnAttributes = true;
        xws.OmitXmlDeclaration = true;
        xws.CheckCharacters = true;
        xws.CloseOutput = false;
        xws.Encoding = Encoding.UTF8;
        StringBuilder sb = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(sb, xws);
        xw.WriteStartDocument();
        xw.WriteStartElement("ROOT");

        foreach (GridViewRow li in grdlist.Rows)
        {
            
            HiddenField hdntype = (HiddenField)li.FindControl("hdntype");
            TextBox txtvalue = (TextBox)li.FindControl("txtvalue");
                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("Test_Variable_ID", hdntype.Value);
                xw.WriteAttributeString("Test_VALUE", txtvalue.Text);
                xw.WriteEndElement();
           
        }
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        return sb.ToString();
    }
    private void clearsection()
    {
        ddlpaypercentage.SelectedValue = "0";

        hdntobepaidamount.Value = "0";
        lblPaidAmt.Text = string.Empty;
        lblrembalance.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRebate.Text = "0";
        txtRebateAmt.Text = "0";
        txtDeduction.Text = "0";
        txtpaneltyAmt.Text = "0";
        iteminkgfirst.Visible = false;
        grdlist.DataSource = null;
        grdlist.DataBind();
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
    protected void ddlpaypercentage_SelectedIndexChanged(object sender, EventArgs e)
    {

        ds = new DataSet();
        ds = objdb.ByProcedure("SP_Check_TRrn_CFP_Items_Receipts_Note_Detail_BY_INVOICENo_For_Payment_For_Percentage", new string[] { "flag", "CFPID", "BigGRNo", "Percentage" }, new string[] { "0", ddlCFP.SelectedValue, ddlInvoice.SelectedValue, ddlpaypercentage.SelectedValue }, "dataset");
        if (ds.Tables[0].Rows[0]["IsExist"].ToString() == "0")
        {
            lblMsg.Text = string.Empty;

            lblPaidAmt.Text = Convert.ToString(Convert.ToDouble((Convert.ToInt32(ddlpaypercentage.SelectedValue) * Convert.ToDouble(lblAmount.InnerText)) / 100));
            hdntobepaidamount.Value = lblPaidAmt.Text;
            lblrembalance.Text = Convert.ToString(Convert.ToDouble(lblpayableAmt.Text) - Convert.ToDouble(hdntobepaidamount.Value));

            if (ddlpaypercentage.SelectedValue == "20")
            {
                iteminkgfirst.Visible = true;
                DataSet ds1 = new DataSet();
                string variant = string.Empty;
                if (hdnUnitID.Value == "10")
                    variant = "2";
                else
                    variant = "1";
                ds1 = objdb.ByProcedure("SP_Mst_CFP_Item_Test_variant_By_Type", new string[] { "flag", "VariantTypeID" }, new string[] { "0", variant }, "dataset");
                grdlist.DataSource = ds1;
                grdlist.DataBind();
            }
            else
            {
                iteminkgfirst.Visible = false;
                txtRebate.Text = "0";
                txtRebateAmt.Text = "0";
                txtDeduction.Text = "0";
                grdlist.DataSource = null;
                grdlist.DataBind();
            }
            btnSave.Visible = true;
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry!", "You have already entered payment detail for selected percentage.");
            btnSave.Visible = false;
            iteminkgfirst.Visible = false;
            grdlist.DataSource = null;
            grdlist.DataBind();
        }
    }
    public static bool isFloatValue(string text)
    {
        Regex regex = new Regex(@"^\d*\.?\d?$");
        return regex.IsMatch(text);
    }
    public static bool isFloatpointValue(string text)
    {
        Regex regex = new Regex(@"^[0-9]*[.][0-9]*$");
        return regex.IsMatch(text);
    }
   
    protected void txtPercentage_TextChanged(object sender, EventArgs e)
    {
        //if (hdnUnitID.Value == "10")
        //{
        try
        {
            hdnDamagebag.Value = "0";
            hdnDoublebag.Value = "0";
            grdlist.ShowFooter = true;
            decimal total = 0;
            decimal total1 = 0;
            lblMsg.Text = string.Empty;
            foreach (GridViewRow row in grdlist.Rows)
            {

                TextBox txtPercentage = (TextBox)row.FindControl("txtvalue");
                HiddenField hdntype = (HiddenField)row.FindControl("hdntype");
                if (hdntype.Value == "8")
                {
                    if (txtPercentage.Text == "")
                    {
                        txtPercentage.Text = "0";
                        txtPercentage.Focus();
                    }
                    if (txtPercentage.Text != "")
                    {
                        if (isFloatValue(txtPercentage.Text))
                        {
                            total += (decimal.Parse(txtPercentage.Text) * decimal.Parse("11.05"));
                        }
                        else
                        {
                            txtPercentage.Text = "0";
                            txtPercentage.Focus();
                        }
                       
                    }
                    
                }
                if (hdntype.Value == "9")
                {
                    if (txtPercentage.Text == "")
                    {
                        txtPercentage.Text = "0";
                        txtPercentage.Focus();
                    }
                    if (isFloatValue(txtPercentage.Text))
                    {
                        total1 += (decimal.Parse(txtPercentage.Text) * decimal.Parse("10.95"));
                    }
                    else
                    {
                        txtPercentage.Text = "0";
                        txtPercentage.Focus();
                    }
                }
            }

            Label lblFooterTotal = grdlist.FooterRow.FindControl("lblFooterTotal") as Label;
            Label lblFooterTotal1 = grdlist.FooterRow.FindControl("lblFooterTotal1") as Label;
            if (total != 0)
            {
                lblFooterTotal.Text = "Total Damage Bag 11.05 (Rate) " + total.ToString();

            }
            else
            {
                lblFooterTotal.Text = total.ToString();

            }
            if (total1 != 0)
            {
                lblFooterTotal1.Text = "Total Double Bag 10.95 (Rate) " + total1.ToString();
            }
            else
            {
                lblFooterTotal1.Text = total1.ToString();

            }
            //hdnDamagebag.Value=Convert.ToString((Convert.ToDecimal(total)));
            //hdnDoublebag.Value = Convert.ToString((Convert.ToDecimal(total1)));
            txtDeduction.Text = Convert.ToString((Convert.ToDecimal(total) + Convert.ToDecimal(total1)));
            lblPaidAmt.Text = Convert.ToString((Convert.ToDouble(hdntobepaidamount.Value) - Convert.ToDouble(txtRebateAmt.Text)) - (Convert.ToDouble(txtDeduction.Text)) - (Convert.ToDouble(txtpaneltyAmt.Text)));

            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
        //}
        //else { grdlist.ShowFooter = false; }

    }
    
    protected void txtRebate_TextChanged(object sender, EventArgs e)
    {
        if (isFloatpointValue(txtRebate.Text))
        {
            txtRebateAmt.Text = Convert.ToString(Convert.ToDouble((Convert.ToDouble(txtRebate.Text) * Convert.ToDouble(lblpayableAmt.Text)) / 100));
            lblPaidAmt.Text = Convert.ToString((Convert.ToDouble(hdntobepaidamount.Value) - Convert.ToDouble(txtRebateAmt.Text)) - Convert.ToDouble(txtDeduction.Text) - (Convert.ToDouble(txtpaneltyAmt.Text)));
        }
        else
        {
            txtRebate.Text = "0";
            txtRebate.Focus();
        }
        
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
    protected void txtpaneltyAmt_TextChanged(object sender, EventArgs e)
    {
        if (isFloatValue(txtpaneltyAmt.Text) || isFloatpointValue(txtpaneltyAmt.Text))
        {
            lblPaidAmt.Text = Convert.ToString((Convert.ToDouble(hdntobepaidamount.Value) - Convert.ToDouble(txtRebateAmt.Text)) - Convert.ToDouble(txtDeduction.Text) - (Convert.ToDouble(txtpaneltyAmt.Text)));
        }
        else
        {
            txtpaneltyAmt.Text = "0";
            txtpaneltyAmt.Focus();
        }
    }
}