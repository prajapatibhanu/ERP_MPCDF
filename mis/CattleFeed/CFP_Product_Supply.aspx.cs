using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

public partial class mis_CattelFeed_CFP_Product_Supply : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    decimal dPageTotal = 0;
    Int32 totalbags = 0;
    decimal GrandTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        txtGrandTotal.Attributes.Add("readonly", "readonly");
        fillSupplyto();
        fillRbtn();
        fillProdUnit();
        fillSupplier();
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
    private void fillSupplyto()
    {
        ddlOfficeTypeTo.Items.Clear();
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_CFP_OfficeType_PS_List", new string[] { "flag" }, new string[] { "0" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlOfficeTypeTo.DataSource = ds;
            ddlOfficeTypeTo.DataTextField = "OfficeTypeName";
            ddlOfficeTypeTo.DataValueField = "OfficeType_ID";
            ddlOfficeTypeTo.DataBind();

        }
        ddlOfficeTypeTo.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objdb);

    }
    private void fillOffice()
    {
        ddlOffice.Items.Clear();
        ds = new DataSet();
        string supplierto = string.Empty;
        if (ddlOfficeTypeTo.SelectedValue == "20") { supplierto = "0"; } else { supplierto = ddlOfficeTypeTo.SelectedValue; }
        ds = objdb.ByProcedure("SP_CFP_AdminOffice_ByType", new string[] { "flag", "OfficeTypeID" }, new string[] { "0", supplierto }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlOffice.DataSource = ds;
            ddlOffice.DataValueField = "Office_ID";
            ddlOffice.DataTextField = "Office_Name";
            ddlOffice.DataBind();

        }
        ddlOffice.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objdb);

    }
    private void fillRbtn()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_CFP_Rate_Types", new string[] { "flag" }, new string[] { "0" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            rbtnRateType.DataSource = ds;
            rbtnRateType.DataTextField = "RateType";
            rbtnRateType.DataValueField = "RateTypeID";
            rbtnRateType.DataBind();

        }
        GC.SuppressFinalize(objdb);

    }
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                decimal Amount = 0;
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlProduct");
                    TextBox txtBags = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtBags");
                    TextBox txtQuantity = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                    TextBox txtrate = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtrate");
                    TextBox txtAmt = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtAmt");
                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["Column1"] = ddlProduct.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column2"] = txtBags.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = txtQuantity.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = txtrate.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = txtAmt.Text;
                    rowIndex++;

                    //if (txtGrandTotal.Text != "")
                    //{
                    //    Amount = Convert.ToDecimal(txtGrandTotal.Text);
                    //}
                    //else
                    //{
                    //    Amount = 0;
                    //}
                    GrandTotal += Convert.ToDecimal(txtAmt.Text) + Amount;
                    txtGrandTotal.Text = GrandTotal.ToString();
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
            }
        }
        else
        {
            SetInitialRow();
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlProduct = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddlProduct");
                    TextBox txtBags = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtBags");
                    TextBox txtQuantity = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                    TextBox txtrate = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtrate");
                    TextBox txtAmt = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtAmt");
                    ddlProduct.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    txtBags.Text = dt.Rows[i]["Column2"].ToString();
                    txtQuantity.Text = dt.Rows[i]["Column3"].ToString();
                    txtrate.Text = dt.Rows[i]["Column4"].ToString();
                    txtAmt.Text = dt.Rows[i]["Column5"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {

        lblMsg.Text = string.Empty;
        if (VaildateRecord())
        {
            try
            {
                AddNewRowToGrid();
                ddlOfficeTypeTo.Enabled = false;
                ddlOffice.Enabled = false;
                txtOffice.Enabled = false;
                ddlcfp.Enabled = false;

                btnClear.Visible = true;

            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Production Entry for Products are not completed.");
                btnClear.Visible = true;
            }

        }
        else
        {
            btnsave.Visible = false;
            btnClear.Visible = false;
        }

    }

    private bool VaildateRecord()
    {
        bool val = true;
        StringBuilder sb = new StringBuilder();
        if (ddlOfficeTypeTo.SelectedValue != "0")
        {
            if (ddlOfficeTypeTo.SelectedValue == "99")
            {
                if (txtOffice.Text == string.Empty) { sb.Append("Enter Customer Name<br>"); val = false; }
            }
            else
            {
                if (ddlOffice.SelectedValue == "0")
                {
                    sb.Append("Select office<br>");
                    val = false;
                }
            }
        }
        else
        {
            sb.Append("Select Sale Type<br>");
            val = false;
        }
        if (ddlcfp.SelectedValue == "0")
        {
            sb.Append("Select Cattel Feed Plant.<br>");
            val = false;

        }
        if (val == false)
            lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Alert", "Select Mandatory feild :" + sb.ToString());
        return val;
    }
    private void fillSupplier()
    {
        ddlSupplier.Items.Clear();
        ds = new DataSet();
        ds = objdb.ByProcedure("CFP_Supplier_RegistrationByOfficeList", new string[] { "flag", "OfficeID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.DataSource = ds;
            ddlSupplier.DataTextField = "Supplier_Name";
            ddlSupplier.DataValueField = "I_Supplier_Registration_ID";
            ddlSupplier.DataBind();
        }
        ddlSupplier.Items.Insert(0, new ListItem("-- Select --", "0"));
        GC.SuppressFinalize(objdb);

    }
    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        Gridview1.DataSource = dt;
        Gridview1.DataBind();
    }
    //private void fillProductforInvoice()
    //{
    //    ddlProduct.Items.Clear();
    //    ds = new DataSet();
    //    ds = objdb.ByProcedure("SP_CFP_Product_Production_ForInvoice", new string[] { "flag", "CFPID", "OfficeID" }, new string[] { "0", ddlcfp.SelectedValue, objdb.Office_ID() }, "dataset");
    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlProduct.DataSource = ds;
    //        ddlProduct.DataTextField = "Product";
    //        ddlProduct.DataValueField = "CFPProductSizeID";
    //        ddlProduct.DataBind();
    //        hdnCFPProductid.Value = Convert.ToString(ds.Tables[0].Rows[0]["CFPProduct_id"]);
    //    }
    //    ddlProduct.Items.Insert(0, new ListItem("-- Select --", "0"));
    //    GC.SuppressFinalize(objdb);

    //}
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlProduct = (e.Row.FindControl("ddlProduct") as DropDownList);
            HiddenField hdnCFPProductid = (e.Row.FindControl("hdnCFPProductid") as HiddenField);
            DataSet ds1 = new DataSet();
            ds1 = objdb.ByProcedure("SP_CFP_Product_Production_ForInvoice", new string[] { "flag", "CFPID", "OfficeID" }, new string[] { "0", ddlcfp.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlProduct.DataSource = ds1;
            ddlProduct.DataTextField = "Product";
            ddlProduct.DataValueField = "CFPProductSizeID";
            ddlProduct.DataBind();
            hdnCFPProductid.Value = Convert.ToString(ds1.Tables[0].Rows[0]["CFPProduct_id"]);
            //Add Default Item in the DropDownList
            ddlProduct.Items.Insert(0, new ListItem("Please select"));
            //

        }
    }
    protected void ddlSupplierTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillOffice();
        if (ddlOfficeTypeTo.SelectedValue == "99")
        {
            tsupplier.Visible = true;
            ddsupply.Visible = false;
            ddlOffice.SelectedValue = "0";
            notDC.Visible = false;
            rbtnRateType.ClearSelection();
            txtsupplydate.Text = string.Empty;
            ddlSupplier.SelectedValue = "0";
            txtDistance.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
        }
        else if (ddlOfficeTypeTo.SelectedValue == "0")
        {
            tsupplier.Visible = false;
            ddsupply.Visible = false;
            ddlOffice.SelectedValue = "0";
            txtOffice.Text = string.Empty;
            notDC.Visible = false;
            rbtnRateType.ClearSelection();
            txtsupplydate.Text = string.Empty;
            ddlSupplier.SelectedValue = "0";
            txtDistance.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
        }
        else
        {
            tsupplier.Visible = false;
            ddsupply.Visible = true;
            notDC.Visible = true;
            txtOffice.Text = string.Empty;
            rbtnRateType.ClearSelection();
            txtsupplydate.Text = string.Empty;
            ddlSupplier.SelectedValue = "0";
            txtDistance.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;

        }
    }
    //private void fillProductQuantity()
    //{
    //    ds = new DataSet();
    //    ds = objdb.ByProcedure("SP_Trn_CFP_Product_ProductionBY_ID", new string[] { "flag", "CFPProductSizeID", "CFPProductid" }, new string[] { "0", ddlProduct.SelectedValue, hdnCFPProductid.Value }, "dataset");
    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        hdnproductavilableQuantity.Value = Convert.ToString(ds.Tables[0].Rows[0]["AvailableProductionQuantity"]);
    //        hdnproductsize.Value = Convert.ToString(ds.Tables[0].Rows[0]["Packaging_Size"]);
    //    }
    //    GC.SuppressFinalize(objdb);

    //}
    protected void btnsave_Click(object sender, EventArgs e)
    {
        InsertProductRate();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillProductforInvoice();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gridview1.Rows)
        {

            DropDownList ddlProduct = (DropDownList)row.FindControl("ddlProduct");
            TextBox txtBags = (TextBox)row.FindControl("txtBags");
            TextBox txtrate = (TextBox)row.FindControl("txtrate");

            HiddenField hdnCFPProductid = (HiddenField)row.FindControl("hdnCFPProductid");
            HiddenField hdnpackagingsize = (HiddenField)row.FindControl("hdnproductavilableQuantity");
            HiddenField hdnproductavilableQuantity = (HiddenField)row.FindControl("hdnproductavilableQuantity");
            ds = new DataSet();
            DataSet ds1 = new DataSet();
            ds = objdb.ByProcedure("SP_Trn_CFP_Product_ProductionBY_ID", new string[] { "flag", "CFPProductSizeID", "CFPProductid" }, new string[] { "0", ddlProduct.SelectedValue, hdnCFPProductid.Value }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                hdnproductavilableQuantity.Value = Convert.ToString(ds.Tables[0].Rows[0]["AvailableProductionQuantity"]);
                hdnproductsize.Value = Convert.ToString(ds.Tables[0].Rows[0]["Packaging_Size"]);

            }
            ds1 = objdb.ByProcedure("SP_CFP_Product_Sale_Rate_List", new string[] { "flag", "SaleOfficeID", "ProductID", "CFPProductSizeID", "SaleOfficeTypeID" },
                new string[] { "0", ddlOffice.SelectedValue, hdnCFPProductid.Value, ddlProduct.SelectedValue, ddlOfficeTypeTo.SelectedValue }, "dataset");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtrate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);
                txtBags.Enabled = true;
            }
            else
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :Please provide the rate list for product for selecte office");


            //txtrate.Enabled = true;
            GC.SuppressFinalize(objdb);
            GC.SuppressFinalize(ds);
        }
    }
    private void InsertProductRate()
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ds = new DataSet();
                string flag = "0";
                if (Convert.ToInt32(hdnvalue.Value) > 0)
                {
                    flag = "1";
                }
                //"CFPProductSizeID",ddlProduct.SelectedValue,"TotalBagsPurchase", "QuantityInMT", "Rate",  "TotalAmt", txtBags.Text, txtQuantity.Text.Trim(), txtrate.Text,  txtAmt.Text, 
                ds = objdb.ByProcedure("SP_Trn_CFP_Product_Invoice_Insert",
                    new string[] { "flag", "OfficeTypeID", "OfficeID", "OfficePurchaserName", "CFPID", "RateTypeID", "SupplyDate", "SupplierID", "TotalDistance", "VehicleNo", "EmptyBag", "GrandTotal", "ReplaceItem", "CreatedOfficeID", "InsertedBy", "IPAddress", "Str" },
                    new string[] { flag, ddlOfficeTypeTo.SelectedValue, ddlOffice.SelectedValue, txtOffice.Text, ddlcfp.SelectedValue, rbtnRateType.SelectedValue, txtsupplydate.Text, ddlSupplier.SelectedValue, txtDistance.Text, txtVehicleNo.Text, txtEmptyBag.Text, txtGrandTotal.Text, txtReplaceItem.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, GetXML() }, "TableSave");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
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
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void Clear()
    {
        // hdnCFPProductid.Value = "0";
        ddlOfficeTypeTo.SelectedValue = "0";
        ddlOffice.SelectedValue = "0";
        txtOffice.Text = string.Empty;
        ddlcfp.SelectedValue = "0";
        Gridview1.DataSource = null;
        Gridview1.DataBind();
        //txtBags.Text = string.Empty;
        //txtQuantity.Text = string.Empty;
        //txtrate.Text = string.Empty;
        // ddlProduct.SelectedValue = "0";
        //txtAmt.Text = string.Empty;
        rbtnRateType.ClearSelection();
        txtsupplydate.Text = string.Empty;
        ddlSupplier.SelectedValue = "0";
        txtDistance.Text = string.Empty;
        txtVehicleNo.Text = string.Empty;
        hdnvalue.Value = "0";
        tsupplier.Visible = false;
        ddsupply.Visible = false;
        notDC.Visible = false;
        ddlOfficeTypeTo.Enabled = true;
        ddlcfp.Enabled = true;
        bttonpnl.Visible = false;
        //ddlProduct.Enabled = true;
        btnsave.Text = "Save";

    }
    private void fillGrd()
    {

        ds = new DataSet();
        ds = objdb.ByProcedure("SP_Trn_CFP_Product_Invoice_By_Office_List", new string[] { "flag", "CreatedOfficeID" }, new string[] { "0", objdb.Office_ID() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();
        }
        GC.SuppressFinalize(ds);
        GC.SuppressFinalize(objdb);
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("SP_CFP_Product_Invoice_Child_BY_Invoice_ID", new string[] { "flag", "CFPProductInvoiceID" }, new string[] { "0", hdnvalue.Value }, "dataset");
                GridView2.DataSource = ds1;
                GridView2.DataBind();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objdb.ByProcedure("SP_Trn_CFP_Product_Invoice_InActive_InvoiceNo",
                    new string[] { "flag", "CFPProductInvoiceID", "InsertedBy", "IPAddress" },
                    new string[] { "0", hdnvalue.Value, objdb.createdBy(), Request.UserHostAddress }, "TableSave");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
                {
                    fillGrd();
                    //  Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success : Operation Completed.");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                }
                break;
            case "Report":
                ds = new DataSet();
                DataSet ds2 = new DataSet();
                ds = objdb.ByProcedure("SP_CFP_Product_Invoice_Child_By_InvoiceID_List",
                    new string[] { "flag", "InvoiceID" },
                    new string[] { "0", hdnvalue.Value }, "TableSave");
                // lblTerms.Text = objdb.changeToWords("123456");
                ds2 = objdb.ByProcedure("SP_Trn_CFP_Product_Invoice_DETAIL_BY_ID",
                    new string[] { "flag", "InvoiceID" },
                    new string[] { "0", hdnvalue.Value }, "TableSave");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblcfpname.Text = Convert.ToString(ds2.Tables[0].Rows[0]["CFP_Name"]);
                    lblcfp.Text = Convert.ToString(ds2.Tables[0].Rows[0]["CFP_Name"]);
                    lblAddress.Text = Convert.ToString(ds2.Tables[0].Rows[0]["CFP_Address"]);
                    lblphone.Text = Convert.ToString(ds2.Tables[0].Rows[0]["Contact_No"]);
                    lblemail.Text = Convert.ToString(ds2.Tables[0].Rows[0]["Email_Address"]);
                    lblpan.Text = Convert.ToString(ds2.Tables[0].Rows[0]["CFP_Pan_No"]);
                    lblGSTN.Text = Convert.ToString(ds2.Tables[0].Rows[0]["CFP_GSTN_No"]);
                    lblTAN.Text = Convert.ToString(ds2.Tables[0].Rows[0]["CFP_TAN_NO"]);
                    lblConsignee.Text = Convert.ToString(ds2.Tables[0].Rows[0]["OfficeNAme"]);
                    lblinvoiceno.Text = Convert.ToString(ds2.Tables[0].Rows[0]["InvoiceNO"]);
                    lblinvoicedate.Text = Convert.ToString(ds2.Tables[0].Rows[0]["TransactionDate"]);
                    lbldispatch.Text = Convert.ToString(ds2.Tables[0].Rows[0]["DispatchThrough"]);
                    lblTerms.Text = Convert.ToString(ds2.Tables[0].Rows[0]["SupplierName"]);
                    lblword.Text = objdb.changeToWords(Convert.ToString(ds2.Tables[0].Rows[0]["Amount"]));
                    lblEmptyBag.Text = Convert.ToString(ds2.Tables[0].Rows[0]["EmptyBag"]);
                    lblEmptyBagCharge.Text = (Convert.ToInt32(ds2.Tables[0].Rows[0]["EmptyBag"]) * 25).ToString();
                    lblReplaceItem.Text = Convert.ToString(ds2.Tables[0].Rows[0]["ReplaceItem"]);
                    lblTPC.Text = Convert.ToString(ds2.Tables[0].Rows[0]["TPC"]);
                    lblgrand.Text = (Convert.ToInt32(ds2.Tables[0].Rows[0]["TPC"].ToString()) + Convert.ToDecimal(ds2.Tables[0].Rows[0]["GrandTotal"].ToString())).ToString();
                    string IsTdsApplicable = Convert.ToString(ds2.Tables[0].Rows[0]["IsTdsApplicable"]);
                    if (IsTdsApplicable == "1")
                    {
                        decimal TDSAMT = (Convert.ToDecimal(Convert.ToString(ds2.Tables[0].Rows[0]["Amount"])) * Convert.ToDecimal("0.75")) / 100;
                        lblTDSAmount.Text = TDSAMT.ToString("0.00");
                        lblgrand.Text = (Convert.ToDecimal(lblgrand.Text) - TDSAMT).ToString("0.00");
                    }
                }
                grdinvoice.DataSource = ds;
                grdinvoice.DataBind();
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(ds2);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
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
    protected void txtBags_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        foreach (GridViewRow row in Gridview1.Rows)
        {
            TextBox txtrate = (TextBox)row.FindControl("txtrate");
            TextBox txtBags = (TextBox)row.FindControl("txtBags");
            TextBox txtAmt = (TextBox)row.FindControl("txtAmt");
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
            HiddenField hdnCFPProductid = (HiddenField)row.FindControl("hdnCFPProductid");
            HiddenField hdnpackagingsize = (HiddenField)row.FindControl("hdnpackagingsize");
            HiddenField hdnproductavilableQuantity = (HiddenField)row.FindControl("hdnproductavilableQuantity");
            double packets = 0, Weight = 0, MTWt = 0, rate = 0;
            if (Convert.ToInt32(txtBags.Text) <= Convert.ToInt32(hdnproductavilableQuantity.Value))
            {
                if (txtBags.Text != "")
                {
                    packets = Convert.ToDouble(txtBags.Text.ToString());
                    Weight = Convert.ToDouble(hdnproductsize.Value);
                    MTWt = (packets * Weight) / 1000;
                    txtQuantity.Text = Math.Round(MTWt, 3).ToString();
                    if (txtrate.Text != "")
                    {
                        rate = Convert.ToDouble(txtrate.Text.ToString());

                    }
                    else
                        rate = 0;

                    txtAmt.Text = Convert.ToString(rate * packets);
                    btnsave.Visible = true;
                }
                else
                {
                    txtQuantity.Text = string.Empty;
                }
                GrandTotal += Convert.ToDecimal(txtAmt.Text);
                txtGrandTotal.Text = GrandTotal.ToString();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Total bag should be less than availablity. Available bag is " + hdnproductavilableQuantity.Value);
            }
        }

    }
    private string GetXML()
    {
        string result = string.Empty;
        if (Gridview1.Rows.Count > 0)
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

            foreach (GridViewRow row in Gridview1.Rows)
            {
                TextBox txtrate = (TextBox)row.FindControl("txtrate");
                TextBox txtBags = (TextBox)row.FindControl("txtBags");
                TextBox txtAmt = (TextBox)row.FindControl("txtAmt");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                HiddenField hdnCFPProductid = (HiddenField)row.FindControl("hdnCFPProductid");
                DropDownList ddlProduct = (DropDownList)row.FindControl("ddlProduct");

                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("Product_ID", hdnCFPProductid.Value);
                xw.WriteAttributeString("CFP_Product_Size_ID", ddlProduct.SelectedValue);
                xw.WriteAttributeString("ProductQuantity", txtQuantity.Text);
                xw.WriteAttributeString("Rate", txtrate.Text);
                xw.WriteAttributeString("Amount", txtAmt.Text);
                xw.WriteAttributeString("TotalBagPurchased", txtBags.Text);
                xw.WriteEndElement();
            }
            xw.WriteEndDocument();
            xw.Flush();
            xw.Close();
            result = sb.ToString();
        }
        return result;

    }
    protected void txtrate_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Gridview1.Rows)
        {
            TextBox txtrate = (TextBox)row.FindControl("txtrate");
            TextBox txtBags = (TextBox)row.FindControl("txtBags");
            TextBox txtAmt = (TextBox)row.FindControl("txtAmt");
            if (txtBags.Text != "")
            {
                double packets = 0, rate = 0;
                packets = Convert.ToDouble(txtBags.Text.ToString());
                if (txtrate.Text != "")
                {
                    rate = Convert.ToDouble(txtrate.Text.ToString());

                }
                else
                    rate = 0;
                txtAmt.Text = Convert.ToString(rate * packets);
            }
        }
    }
    protected void grdinvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalbags += Int32.Parse(e.Row.Cells[2].Text);
            dPageTotal += Decimal.Parse(e.Row.Cells[6].Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total QTY";
            e.Row.Cells[5].Text = "SUB Total";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Text = totalbags.ToString();
            e.Row.Cells[6].Text = dPageTotal.ToString();
        }
    }
    protected void txtEmptyBag_TextChanged(object sender, EventArgs e)
    {
        decimal FinalAmount = Convert.ToDecimal(txtGrandTotal.Text);
        decimal BagAmount = Convert.ToInt32(txtEmptyBag.Text) * 25;
        GrandTotal += FinalAmount + BagAmount;
        txtGrandTotal.Text = GrandTotal.ToString();
    }
}