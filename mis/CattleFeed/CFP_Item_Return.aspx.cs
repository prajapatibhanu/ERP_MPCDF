using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_CFP_Item_Return : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();
        Unit();
        Category();
        Fillgrd();
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
    private void Category()
    {
        try
        {
            ddlCategory.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemCategoryList",
                                  new string[] { "flag" },
                                  new string[] { "0" }, "dataset");
            ddlCategory.DataSource = ds;
            ddlCategory.DataTextField = "CFP_ItemCatName";
            ddlCategory.DataValueField = "CFPItemCat_id";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            ddlType.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemTypeList",
                                  new string[] { "flag", "CategoryID" },
                                  new string[] { "0", ddlCategory.SelectedValue }, "dataset");
            ddlType.DataSource = ds;
            ddlType.DataTextField = "ItemTypeName";
            ddlType.DataValueField = "ItemType_id";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            ddlItems.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFPItems_By_ItemTypeID_CFP_List", new string[] { "flag", "ItemTypeID", "CFPID" }, new string[] { "0", ddlType.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            ddlItems.DataSource = ds;
            ddlItems.DataValueField = "Item_id";
            ddlItems.DataTextField = "ItemName";
            ddlItems.DataBind();
            ddlItems.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemType();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    protected void rblpartial_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblpartial.SelectedValue == "1")
        {
            Firstinstallment.Visible = true;
            Secinstallment.Visible = true;
            ThirdInstallment.Visible = true;
            txtfirstfromdt.Text = string.Empty;
            txtfirstTOdt.Text = string.Empty;
            txtfirstitemQuantity.Text = string.Empty;
            txtSecfromdt.Text = string.Empty;
            txtSecTodt.Text = string.Empty;
            txtSecitemQuantity.Text = string.Empty;
            if (hdnamt.Value == "0")
            {
                if (txtItemQuantity.Text != string.Empty && txtItemRate.Text != string.Empty)
                {
                    hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value;

                }
            }
            else
                lblAmount.Text = hdnamt.Value;
        }
        else
        {
            Firstinstallment.Visible = false;
            Secinstallment.Visible = false;
            ThirdInstallment.Visible = false;
            txtfirstfromdt.Text = string.Empty;
            txtfirstTOdt.Text = string.Empty;
            txtfirstitemQuantity.Text = string.Empty;
            txtSecfromdt.Text = string.Empty;
            txtSecTodt.Text = string.Empty;
            txtSecitemQuantity.Text = string.Empty;
            if (hdnamt.Value == "0")
            {
                if (txtItemQuantity.Text != string.Empty && txtItemRate.Text != string.Empty)
                {
                    hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value;

                }
            }
            else
                lblAmount.Text = hdnamt.Value;
        }
    }
    private void Fillgrd()
    {
        try
        {
            grdCatlist.DataSource = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_By_OfficeID_List",
                            new string[] { "flag", "OfficeID" },
                            new string[] { "0", objdb.Office_ID() }, "dataset");
            grdCatlist.DataBind();

            GC.SuppressFinalize(objdb);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private bool GetTenderCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtTender.Text; // From Database
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
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtPODt.Text; // From Database
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
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOEndCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtPODt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtPOendDt.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOFirstCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtfirstfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = DateTime.Now.ToString("dd/MM/yyyy"); ; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOSecCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtSecfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = DateTime.Now.ToString("dd/MM/yyyy"); ; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOdateFirstdateCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtfirstfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtPODt.Text; ; // From Database
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

            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOdateSecdateCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtSecfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtPODt.Text; ; // From Database
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
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOenddateFirstdateCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtfirstfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtPOendDt.Text; ; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetPOenddateSecdateCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtSecfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtPOendDt.Text; ; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetFirstdateSecDateCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtfirstfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtfirstTOdt.Text; ; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    private bool GetSecdateToDateCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtSecfromdt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtSecTodt.Text; ; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate < tdate)
            {
                res = true;

            }
            else
            {
                res = false;
                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than FromDate ");
            }
            if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
            else
                lblAmount.Text = hdnamt.Value;

        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (GetTenderCompareDate())
        {
            //if (GetPOCompareDate())
            //{
            if (GetPOEndCompareDate())
            {
                StringBuilder sb = new StringBuilder();
                if (txtGSTN.Text != string.Empty)
                {
                    if (txtGSTN.Text.Length < 15)
                    {
                        sb.Append("Enter valid GST No.\\n");
                    }
                }
                if (txtVendorcontact.Text != string.Empty)
                {
                    if (txtVendorcontact.Text.Length < 10)
                    {
                        sb.Append("Enter valid Mobile No.\\n");
                    }
                }
                if (rblpartial.SelectedValue == "1")
                {
                    if (txtfirstfromdt.Text == string.Empty)
                    {
                        sb.Append("Enter First from date.\\n");
                    }
                    if (txtfirstTOdt.Text == string.Empty)
                    {
                        sb.Append("Enter First To date.\\n");
                    }
                    if (txtSecfromdt.Text == string.Empty)
                    {
                        sb.Append("Enter Second from date.\\n");
                    }
                    if (txtSecTodt.Text == string.Empty)
                    {
                        sb.Append("Enter Second To date.\\n");
                    }
                    if (txtThirdFromdt.Text == string.Empty)
                    {
                        sb.Append("Enter Third from date.\\n");
                    }
                    if (txtThirdTodt.Text == string.Empty)
                    {
                        sb.Append("Enter Third To date.\\n");
                    }
                    //if (txtfirstfromdt.Text != string.Empty)
                    //{
                    //    if (!GetPOFirstCompareDate())
                    //    {
                    //        sb.Append("Enter First from date should be less than current date.\\n");
                    //    }
                    //    if (!GetPOdateFirstdateCompareDate())
                    //    {
                    //        sb.Append("Enter First from date greated than or equal to PO date.\\n");
                    //    }
                    //    if (!GetPOenddateFirstdateCompareDate())
                    //    {
                    //        sb.Append("Enter First from date less than PO end date.\\n");
                    //    }
                    //    if (!GetFirstdateSecDateCompareDate())
                    //    {
                    //        sb.Append("Enter First from date less than First To date.\\n");
                    //    }
                    //}
                    //if (txtSecfromdt.Text != string.Empty)
                    //{
                    //    //if (!GetPOSecCompareDate())
                    //    //{
                    //    //    sb.Append("Enter Second from date should be less than current date.\\n");
                    //    //}
                    //    if (!GetPOdateSecdateCompareDate())
                    //    {
                    //        sb.Append("Enter Second from date greated than or equal to PO date.\\n");
                    //    }
                    //    if (!GetPOenddateSecdateCompareDate())
                    //    {
                    //        sb.Append("Enter Second from less than PO end date.\\n");
                    //    }
                    //    if (!GetSecdateToDateCompareDate())
                    //    {
                    //        sb.Append("Enter Second from less than First To date.\\n");
                    //    }
                    //}
                    if (txtfirstitemQuantity.Text == string.Empty)
                    {
                        sb.Append("Enter First Quantity.\\n");
                    }
                    if (txtSecitemQuantity.Text == string.Empty)
                    {
                        sb.Append("Enter Second Quantity.\\n");
                    }
                    if (txtThirdItemQuantity.Text == string.Empty)
                    {
                        sb.Append("Enter Third Quantity.\\n");
                    }
                    if (txtThirdItemQuantity.Text != string.Empty && txtSecitemQuantity.Text != string.Empty && txtfirstitemQuantity.Text != string.Empty)
                    {
                        Double qty = Convert.ToDouble(txtfirstitemQuantity.Text) + Convert.ToDouble(txtSecitemQuantity.Text) + Convert.ToDouble(txtThirdItemQuantity.Text);
                        Double ItemQuantity = Convert.ToDouble(txtItemQuantity.Text);
                        if (qty != ItemQuantity)
                        { sb.Append("First and  second Quantity shoud be equal to Item quantity.\\n"); }

                    }
                }
                if (sb.ToString() == string.Empty)
                {
                    ds = new DataSet();
                    if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
                    else
                        lblAmount.Text = hdnamt.Value;
                    string flagvalue = "0";
                    if (Convert.ToInt32(hdnvalue.Value) > 0) { flagvalue = "1"; }
                    ds = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_Insert_Update_Delete_Lock",
                       new string[] { "flag", "CFP_ID", "Item_Cat_ID", "Item_Type_ID", "Item_ID", "Vendor_Name", "Vendor_Address", "Vendor_Contact_No", "GSTN_NO", "Email_Address", "Item_Commodity", "Item_Quantity", "Item_Rate", "Is_GST_Included", "Amount", "Item_PO_Date", "Item_PO_End_Date", "Remark", "Is_Scheduled", "First_From_Date", "First_To_Date", "First_Item_Quantity", "SEC_From_Date", "SEC_To_Date", "Sec_Item_Quantity", "THIRDFromDate", "THIRDToDate", "THIRDItemQuantity", "Office_ID", "InsertedBY", "InsertedIP", "ReferenceNO", "CFPITEMPurchaseID", "Unit_ID", "TenderDate", "FirstLineMessage" },
                       new string[] { flagvalue, ddlcfp.SelectedValue, ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue, txtVendor.Text, txtvendorAddress.Text, txtVendorcontact.Text, txtGSTN.Text, txtEmail.Text, txtitemCommodity.Text, txtItemQuantity.Text, txtItemRate.Text, rbtnGST.SelectedValue, lblAmount.Text, txtPODt.Text, txtPOendDt.Text, txtRemark.Text, rblpartial.SelectedValue, txtfirstfromdt.Text, txtfirstTOdt.Text, txtfirstitemQuantity.Text, txtSecfromdt.Text, txtSecTodt.Text, txtSecitemQuantity.Text, txtThirdFromdt.Text, txtThirdTodt.Text, txtThirdItemQuantity.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, txtReferenceNo.Text, hdnvalue.Value, ddlUnit.SelectedValue, txtTender.Text, txtfirstlinemssage.Text }, "dataset");

                    if (ds != null)
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ClearText();
                        Fillgrd();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", sb.ToString());

                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", "PO date should be less than PO End date.");

            }
            //}
            //else
            //{
            //    lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", "PO date should be less than Current date.");

            //}
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Fail!", "Tender date should be less than Current date.");

        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ClearText();

    }

    private void ClearText()
    {
        ddlcfp.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        ItemType();
        ddlType.SelectedValue = "0";
        fillItemByType();
        ddlItems.SelectedValue = "0";
        ddlcfp.Enabled = true;
        ddlCategory.Enabled = true;
        ddlType.Enabled = true;
        ddlItems.Enabled = true;
        txtVendor.Text = string.Empty;
        txtVendorcontact.Text = string.Empty;
        txtvendorAddress.Text = string.Empty;
        txtGSTN.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtitemCommodity.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        rbtnGST.ClearSelection();
        lblAmount.Text = string.Empty;
        txtPODt.Text = string.Empty;
        txtPOendDt.Text = string.Empty;
        txtRemark.Text = string.Empty;
        rblpartial.ClearSelection();
        btnSave.Text = "Save";
        Firstinstallment.Visible = false;
        Secinstallment.Visible = false;
        ThirdInstallment.Visible = false;
        txtGSTN.Enabled = true;
        txtReferenceNo.Text = string.Empty;
        txtPODt.Enabled = true;
        txtPOendDt.Enabled = true;
        txtReferenceNo.Enabled = true;
        ddlUnit.SelectedValue = "0";
        ddlUnit.Enabled = true;
        txtTender.Text = string.Empty;
        txtfirstlinemssage.Text = string.Empty;
    }
    private void Unit()
    {
        try
        {
            ddlUnit.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("CFP_ItemtblSpUnit",
                                  new string[] { "flag" },
                                  new string[] { "0" }, "dataset");
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
    private void FillUpdate()
    {
        try
        {
            DataSet ds1 = new DataSet();
            ds1 = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_By_PurchaseOrderID",
                            new string[] { "flag", "CFPITEMPurchaseOrderID" },
                            new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlcfp.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ID"]);
                    ddlUnit.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Unit_ID"]);
                    ddlUnit.Enabled = false;
                    ddlCategory.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCatID"]);
                    ItemType();
                    ddlType.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemTypeID"]);
                    fillItemByType();
                    ddlItems.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemID"]);
                    ddlcfp.Enabled = false;
                    ddlCategory.Enabled = false;
                    ddlType.Enabled = false;
                    ddlItems.Enabled = false;
                    txtVendor.Text = Convert.ToString(ds1.Tables[0].Rows[0]["VendorName"]);
                    txtVendorcontact.Text = Convert.ToString(ds1.Tables[0].Rows[0]["VendorContactNo"]);
                    txtvendorAddress.Text = Convert.ToString(ds1.Tables[0].Rows[0]["VendorAddress"]);
                    txtGSTN.Text = Convert.ToString(ds1.Tables[0].Rows[0]["GSTNNO"]);
                    txtGSTN.Enabled = false;
                    txtEmail.Text = Convert.ToString(ds1.Tables[0].Rows[0]["EmailAddress"]);
                    txtitemCommodity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCommodity"]);
                    txtItemQuantity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemQuantity"]);
                    txtItemRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemRate"]);
                    txtReferenceNo.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Reference_NO"]);
                    txtReferenceNo.Enabled = false;
                    rbtnGST.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["IsGSTIncluded"]);
                    lblAmount.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]);
                    txtPODt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPODate"]);
                    txtPODt.Enabled = false;
                    txtPOendDt.Enabled = false;
                    txtPOendDt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPOEndDate"]);
                    txtRemark.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Remark"]);
                    txtTender.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TenderDate"]);
                    txtfirstlinemssage.Text = Convert.ToString(ds1.Tables[0].Rows[0]["FirstLineMessage"]);
                    rblpartial.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Is_Scheduled"]);
                    if (rblpartial.SelectedValue == "1")
                    {
                        Firstinstallment.Visible = true;
                        Secinstallment.Visible = true;
                        ThirdInstallment.Visible = true;
                        txtfirstfromdt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["FirstFromDate"]);
                        txtfirstTOdt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["FirstTODate"]);
                        txtfirstitemQuantity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["FirstItemQuantity"]);
                        txtSecfromdt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SECFromDate"]);
                        txtSecTodt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SECToDate"]);
                        txtSecitemQuantity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["SecItemQuantity"]);
                        txtThirdFromdt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["THIRDFromDate"]);
                        txtThirdTodt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["THIRDToDate"]);
                        txtThirdItemQuantity.Text = Convert.ToString(ds1.Tables[0].Rows[0]["THIRDItemQuantity"]);
                    }
                    else
                    {
                        Firstinstallment.Visible = false;
                        Secinstallment.Visible = false;
                        ThirdInstallment.Visible = false;
                    }
                    btnSave.Text = "Edit";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void FillReport()
    {
        try
        {
            DataSet ds1 = new DataSet();
            ds1 = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_By_PurchaseOrderID_Report",
                            new string[] { "flag", "CFPITEMPurchaseOrderID" },
                            new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lblCFP.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_Name"]);
                    lblCFP1.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_Name"]);
                    lblCFPAddress.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_Address"]);
                    lblCFPEmail.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["CFPEmailAddress"]);
                    lblCFPGSTN.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_GSTN_No"]);
                    lblPurchaseorder.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPONO"]);
                    lblPurchaseorderdate.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPODate"]);
                    lblRefNo.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Reference_NO"]);
                    lblDate.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPODate"]);
                    lblVendor.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["VendorName"]);
                    lblVendorAddress.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["VendorAddress"]);
                    lblEmail.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["VendorEmailAddress"]);
                    lblGSTNNO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["GSTNNO"]);
                    lblfirstmessage.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["FirstLineMessage"]);
                    lblTender.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["TenderDate"]);
                    lblItemName.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemName"]);
                    lblCommodity.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCommodity"]);
                    lblRate.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemRate"]);
                    lblQty.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemQuantity"]);
                    lblfirstsp1dt.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["FirstFromDate"]);
                    lblfirstsp1dt1.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["FirstTODate"]);
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["FirstFromDate"]) == "" && Convert.ToString(ds1.Tables[0].Rows[0]["FirstTODate"]) == "")
                    {
                        Span2.Visible = false;
                    }
                    lblSecsp1dt1.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["SECFromDate"]);
                    lblSecsp1dt2.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["SECToDate"]);
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["SECFromDate"]) == "" && Convert.ToString(ds1.Tables[0].Rows[0]["SECToDate"]) == "")
                    {
                        Span1.Visible = false;
                    }
                    lblThirdsp1dt1.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["THIRDFromDate"]);
                    lblThirdsp1dt2.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["THIRDToDate"]);
                    if (Convert.ToString(ds1.Tables[0].Rows[0]["THIRDFromDate"]) == "" && Convert.ToString(ds1.Tables[0].Rows[0]["THIRDToDate"]) == "")
                    {
                        Span3.Visible = false;
                    }
                    lblfirstQty.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["FirstItemQuantity"]);
                    lblSecQty.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["SecItemQuantity"]);
                    lblThirdQty.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["THIRDItemQuantity"]);
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "RecordUpdate":
                FillUpdate();
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_Insert_Update_Delete_Lock",
            new string[] { "flag", "CFP_ID", "Item_Cat_ID", "Item_Type_ID", "Item_ID", "Vendor_Name", "Vendor_Address", "Vendor_Contact_No", "GSTN_NO", "Email_Address", "Item_Commodity", "Item_Quantity", "Item_Rate", "Is_GST_Included", "Amount", "Item_PO_Date", "Item_PO_End_Date", "Remark", "Is_Scheduled", "First_From_Date", "First_To_Date", "First_Item_Quantity", "SEC_From_Date", "SEC_To_Date", "Sec_Item_Quantity", "THIRDFromDate", "THIRDToDate", "THIRDItemQuantity", "Office_ID", "InsertedBY", "InsertedIP", "ReferenceNO", "CFPITEMPurchaseID", "Unit_ID", "TenderDate", "FirstLineMessage" },
            new string[] { "2", ddlcfp.SelectedValue, ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue, txtVendor.Text, txtvendorAddress.Text, txtVendorcontact.Text, txtGSTN.Text, txtEmail.Text, txtitemCommodity.Text, txtItemQuantity.Text, txtItemRate.Text, rbtnGST.SelectedValue, lblAmount.Text, txtPODt.Text, txtPOendDt.Text, txtRemark.Text, rblpartial.SelectedValue, txtfirstfromdt.Text, txtfirstTOdt.Text, txtfirstitemQuantity.Text, txtSecfromdt.Text, txtSecTodt.Text, txtSecitemQuantity.Text, txtThirdFromdt.Text, txtThirdTodt.Text, txtThirdItemQuantity.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, txtReferenceNo.Text, hdnvalue.Value, ddlUnit.SelectedValue, txtTender.Text, txtfirstlinemssage.Text }, "dataset");

                if (ds != null)
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    Fillgrd();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                }
                break;
            case "RecordLock":
                ds = new DataSet();
                ds = objdb.ByProcedure("SP_TRN_CFP_ITEM_Purchase_Order_Insert_Update_Delete_Lock",
            new string[] { "flag", "CFP_ID", "Item_Cat_ID", "Item_Type_ID", "Item_ID", "Vendor_Name", "Vendor_Address", "Vendor_Contact_No", "GSTN_NO", "Email_Address", "Item_Commodity", "Item_Quantity", "Item_Rate", "Is_GST_Included", "Amount", "Item_PO_Date", "Item_PO_End_Date", "Remark", "Is_Scheduled", "First_From_Date", "First_To_Date", "First_Item_Quantity", "SEC_From_Date", "SEC_To_Date", "Sec_Item_Quantity", "THIRDFromDate", "THIRDToDate", "THIRDItemQuantity", "Office_ID", "InsertedBY", "InsertedIP", "ReferenceNO", "CFPITEMPurchaseID", "Unit_ID", "TenderDate", "FirstLineMessage" },
            new string[] { "3", ddlcfp.SelectedValue, ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue, txtVendor.Text, txtvendorAddress.Text, txtVendorcontact.Text, txtGSTN.Text, txtEmail.Text, txtitemCommodity.Text, txtItemQuantity.Text, txtItemRate.Text, rbtnGST.SelectedValue, lblAmount.Text, txtPODt.Text, txtPOendDt.Text, txtRemark.Text, rblpartial.SelectedValue, txtfirstfromdt.Text, txtfirstTOdt.Text, txtfirstitemQuantity.Text, txtSecfromdt.Text, txtSecTodt.Text, txtSecitemQuantity.Text, txtThirdFromdt.Text, txtThirdTodt.Text, txtThirdItemQuantity.Text, objdb.Office_ID(), objdb.createdBy(), Request.UserHostAddress, txtReferenceNo.Text, hdnvalue.Value, ddlUnit.SelectedValue, txtTender.Text, txtfirstlinemssage.Text }, "dataset");

                if (ds != null)
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    Fillgrd();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                }
                break;
            case "RecordReport":
                FillReport();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
                break;
            default:
                break;
        }
    }
    protected void grdCatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    private void fillItemUnit()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_ItemUnit_Under_Item", new string[] { "flag", "ItemID" }, new string[] { "0", ddlItems.SelectedValue }, "dataset");
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
    protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemUnit();
    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    protected void txtfirstitemQuantity_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (txtfirstitemQuantity.Text == string.Empty) { txtfirstitemQuantity.Text = "0"; }
        if (txtSecitemQuantity.Text == string.Empty) { txtSecitemQuantity.Text = "0"; }
        if (txtThirdItemQuantity.Text == string.Empty) { txtThirdItemQuantity.Text = "0"; }
        Double qty = Convert.ToDouble(txtfirstitemQuantity.Text) + Convert.ToDouble(txtSecitemQuantity.Text) + Convert.ToDouble(txtThirdItemQuantity.Text);
        Double ItemQuantity = Convert.ToDouble(txtItemQuantity.Text);
        if (qty != ItemQuantity)
        { lblMsg.Text = objdb.Alert("fa-check", "alert-info", "alert!", "First ,second and Third Quantity shoud be equal to Item quantity."); }
        if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
        else
            lblAmount.Text = hdnamt.Value;
    }
    protected void txtSecitemQuantity_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (txtfirstitemQuantity.Text == string.Empty) { txtfirstitemQuantity.Text = "0"; }
        if (txtSecitemQuantity.Text == string.Empty) { txtSecitemQuantity.Text = "0"; }
        if (txtThirdItemQuantity.Text == string.Empty) { txtThirdItemQuantity.Text = "0"; }
        Double qty = Convert.ToDouble(txtfirstitemQuantity.Text) + Convert.ToDouble(txtSecitemQuantity.Text) + Convert.ToDouble(txtThirdItemQuantity.Text);
        Double ItemQuantity = Convert.ToDouble(txtItemQuantity.Text);
        if (qty != ItemQuantity)
        { lblMsg.Text = objdb.Alert("fa-check", "alert-info", "alert!", "First ,second and Third Quantity shoud be equal to Item quantity."); }
        if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
        else
            lblAmount.Text = hdnamt.Value;
    }
    protected void TxtThirdItemQuan_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (txtfirstitemQuantity.Text == string.Empty) { txtfirstitemQuantity.Text = "0"; }
        if (txtSecitemQuantity.Text == string.Empty) { txtSecitemQuantity.Text = "0"; }
        if (txtThirdItemQuantity.Text == string.Empty) { txtThirdItemQuantity.Text = "0"; }
        Double qty = Convert.ToDouble(txtfirstitemQuantity.Text) + Convert.ToDouble(txtSecitemQuantity.Text) + Convert.ToDouble(txtThirdItemQuantity.Text);
        Double ItemQuantity = Convert.ToDouble(txtItemQuantity.Text);
        if (qty != ItemQuantity)
        { lblMsg.Text = objdb.Alert("fa-check", "alert-info", "alert!", "First ,second and Third Quantity shoud be equal to Item quantity."); }
        if (hdnamt.Value == "0") { hdnamt.Value = Convert.ToString(Convert.ToDouble(txtItemQuantity.Text) * Convert.ToDouble(txtItemRate.Text)); lblAmount.Text = hdnamt.Value; }
        else
            lblAmount.Text = hdnamt.Value;
    }
}