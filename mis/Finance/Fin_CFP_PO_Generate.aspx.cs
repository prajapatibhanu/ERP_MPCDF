using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
using QRCoder;
using System.Drawing;
using System.IO;

public partial class mis_CattleFeed_Fin_CFP_PO_Generate : System.Web.UI.Page
{
    DataSet ds, ds1 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                txtPODt.Attributes.Add("readonly", "readonly");
                txtPOendDt.Attributes.Add("readonly", "readonly");
                txtTender.Attributes.Add("readonly", "readonly");
                txtAmount.Attributes.Add("readonly", "readonly");
                fillProdUnit();
                Unit();
                Category();
                FillGrid();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //SP_tblFin_CFP_Purchase_Order

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }


    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void FillGrid()
    {
        try
        {
            Gridview3.DataSource = null;
            Gridview3.DataBind();
            ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
                                  new string[] { "Flag", "Office_ID" },
                                  new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                Gridview3.DataSource = ds1.Tables[0];
                Gridview3.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    private void Unit()
    {
        try
        {
            ddlItemUnit.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SpUnit",
                                  new string[] { "flag" },
                                  new string[] { "1" }, "dataset");
            ddlItemUnit.DataSource = ds;
            ddlItemUnit.DataTextField = "UnitName";
            ddlItemUnit.DataValueField = "Unit_id";
            ddlItemUnit.DataBind();
            ddlItemUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); }
    }
    private void fillProdUnit()
    {
        try
        {

            ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag" },
                 new string[] { "26" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlcfp.DataSource = ds;
                ddlcfp.DataTextField = "Office_Name";
                ddlcfp.DataValueField = "Office_ID";
                ddlcfp.DataBind();
                ddlcfp.SelectedValue = ViewState["Office_ID"].ToString();

                ddlcfp.Enabled = false;
            }


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




            ds = objdb.ByProcedure("SpItemCategory",
                                  new string[] { "flag" },
                                  new string[] { "1" }, "dataset");


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = ds;
                ddlCategory.DataTextField = "ItemCatName";
                ddlCategory.DataValueField = "ItemCat_id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlCategory.SelectedValue = "1";
                ddlCategory.Enabled = false;
                ItemType();
            }
            else
            {
                ddlCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
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
            ds = objdb.ByProcedure("SpItemType",
                                 new string[] { "flag", "ItemCat_id" },
                                 new string[] { "6", ddlCategory.SelectedValue }, "dataset");


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = ds;
                ddlType.DataTextField = "ItemTypeName";
                ddlType.DataValueField = "ItemType_id";
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                ddlType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

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
            ds = objdb.ByProcedure("SpItemMaster", new string[] { "flag", "CategoryId", "ItemType_id", "Office_Id" },
                new string[] { "31", ddlCategory.SelectedValue.ToString(), ddlType.SelectedValue.ToString(), ddlcfp.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItems.DataSource = ds;
                ddlItems.DataValueField = "Item_id";
                ddlItems.DataTextField = "ItemName";
                ddlItems.DataBind();
                ddlItems.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                ddlItems.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ItemType();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillItemByType();
    }
    private void Clear()
    {
        txtPONo.Text = string.Empty;
        txtPODt.Text = string.Empty;
        txtPOendDt.Text = string.Empty;
        txtReferenceNo.Text = string.Empty;
        txtSupplyScheduling.Text = string.Empty;
        txtTender.Text = string.Empty;
        txtVendor.Text = string.Empty;
        txtvendorAddress.Text = string.Empty;
        txtVendorcontact.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtGSTN.Text = string.Empty;
        txtRemark.Text = string.Empty;

        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
        ViewState["ItemDetails"] = "";
        Gridview3.SelectedIndex = -1;
        btnSave.Text = "Save";

    }
    private void InsertRecord()
    {
        try
        {
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
        {
            if(GridView1.Rows.Count>0)
            {

            

            DataTable dtInsertChild = (DataTable)ViewState["ItemDetails"];
                DataColumnCollection columns = dtInsertChild.Columns;
                if (columns.Contains("ItemCat"))
                {
                    dtInsertChild.Columns.Remove("ItemCat");
                }
                if (columns.Contains("ItemType"))
                {
                    dtInsertChild.Columns.Remove("ItemType");
                }
                if (columns.Contains("Item_Name"))
                {
                    dtInsertChild.Columns.Remove("Item_Name");
                }
                if (columns.Contains("ItemUnit_Name"))
                {
                    dtInsertChild.Columns.Remove("ItemUnit_Name");
                }
           
            if (dtInsertChild.Rows.Count > 0)
            {
                DateTime date5 = DateTime.ParseExact(txtPODt.Text, "dd/MM/yyyy", culture);
                string podate = date5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime date6 = DateTime.ParseExact(txtPOendDt.Text, "dd/MM/yyyy", culture);
                string poenddate = date6.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime date7 = DateTime.ParseExact(txtTender.Text, "dd/MM/yyyy", culture);
                string tenderdate = date7.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
             new string[] { "Flag","PO_NO","PO_Date","PO_End_Date"
                         ,"Reference_NO","TenderDate","Vendor_Name"
                         ,"Vendor_Address", "Vendor_Contact_No"
                         , "GSTN_NO", "Email_Address","Remark","SupplyScheduling","Office_ID"
                         , "Inserted_BY", "Inserted_IP"},
             new string[] { "1",txtPONo.Text.Trim(),podate.ToString(),poenddate.ToString(),txtReferenceNo.Text.Trim()
                         ,tenderdate.ToString(),txtVendor.Text.Trim(),txtvendorAddress.Text.Trim()
                         ,txtVendorcontact.Text.Trim(),txtGSTN.Text.Trim(),txtEmail.Text.Trim(),txtRemark.Text.Trim()
                         ,txtSupplyScheduling.Text.Trim(),  ViewState["Office_ID"].ToString(),
                         ViewState["Emp_ID"].ToString() ,IPAddress 
                         // }, "dataset");
                         }, "type_tblFin_CFP_Purchase_OrderChild", dtInsertChild, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        FillGrid();
                        Clear();
                        
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", "Select atleast one Invoice Details.");
            }

            if (dtInsertChild != null) { dtInsertChild.Dispose(); }
            

            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", "Select atleast one Invoice Details.");
            }
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2 : ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void UpdateRecord()
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime date5 = DateTime.ParseExact(txtPODt.Text, "dd/MM/yyyy", culture);
                string podate = date5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime date6 = DateTime.ParseExact(txtPOendDt.Text, "dd/MM/yyyy", culture);
                string poenddate = date6.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime date7 = DateTime.ParseExact(txtTender.Text, "dd/MM/yyyy", culture);
                string tenderdate = date7.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
             new string[] { "Flag","CFP_Purchase_Order_ID","PO_NO","PO_Date","PO_End_Date"
                         ,"Reference_NO","TenderDate","Vendor_Name"
                         ,"Vendor_Address", "Vendor_Contact_No"
                         , "GSTN_NO", "Email_Address","Remark","SupplyScheduling","Office_ID"
                         , "Inserted_BY", "Inserted_IP"},
             new string[] { "2",ViewState["rowid"].ToString(),txtPONo.Text.Trim(),podate.ToString(),poenddate.ToString(),txtReferenceNo.Text.Trim()
                         ,tenderdate.ToString(),txtVendor.Text.Trim(),txtvendorAddress.Text.Trim()
                         ,txtVendorcontact.Text.Trim(),txtGSTN.Text.Trim(),txtEmail.Text.Trim(),txtRemark.Text.Trim()
                         ,txtSupplyScheduling.Text.Trim(),  ViewState["Office_ID"].ToString(),
                         ViewState["Emp_ID"].ToString() ,IPAddress 
                          }, "dataset");
                       
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        FillGrid();
                        Clear();
                        
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2 : ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void Gridview3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridview3.PageIndex = e.NewPageIndex;
        lblMsg.Text = "";
        FillGrid();
    }

    protected void Gridview3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordReport")
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    ViewState["rowid"] = e.CommandArgument;
                    FillReport();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
                }
            }

            if (e.CommandName == "RecordLock")
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
                 new string[] { "Flag", "CFP_Purchase_Order_ID", "Inserted_BY", "Inserted_IP" },
                new string[] { "3", e.CommandArgument.ToString(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            FillGrid();
                            
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }

                }
            }
            else if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblPO_NO = (Label)row.FindControl("lblPO_NO");
                    Label lblPO_Date = (Label)row.FindControl("lblPO_Date");
                    Label lblPO_End_Date = (Label)row.FindControl("lblPO_End_Date");
                    Label lblTenderDate = (Label)row.FindControl("lblTenderDate");
                    Label lblSupplyScheduling = (Label)row.FindControl("lblSupplyScheduling");
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    Label lblVendorName = (Label)row.FindControl("lblVendorName");
                    Label lblVendorAddress = (Label)row.FindControl("lblVendorAddress");
                    Label lblVendorContactNo = (Label)row.FindControl("lblVendorContactNo");
                    Label lblGSTNNO = (Label)row.FindControl("lblGSTNNO");
                    Label lblEmailAddress = (Label)row.FindControl("lblEmailAddress");
                    Label lblReference_NO = (Label)row.FindControl("lblReference_NO");

                    txtPONo.Text = lblPO_NO.Text;
                    txtPODt.Text = lblPO_Date.Text;
                    txtPOendDt.Text = lblPO_End_Date.Text;
                    txtTender.Text = lblTenderDate.Text;
                    txtReferenceNo.Text = lblReference_NO.Text;
                    txtVendor.Text = lblVendorName.Text;
                    txtvendorAddress.Text = lblVendorAddress.Text;
                    txtVendorcontact.Text = lblVendorContactNo.Text;
                    txtEmail.Text = lblEmailAddress.Text;
                    txtGSTN.Text = lblGSTNNO.Text;
                    txtRemark.Text = lblRemark.Text;
                    txtSupplyScheduling.Text = lblSupplyScheduling.Text;

                    btnSave.Text = "Update";

                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    GridView2.Visible = true;
                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_OrderChild",
                   new string[] { "Flag", "CFP_Purchase_Order_ID" },
                  new string[] { "0", e.CommandArgument.ToString() }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                            GridView2.DataSource = ds1.Tables[0];
                            GridView2.DataBind();
                       
                    }

                    foreach (GridViewRow gvRow in Gridview3.Rows)
                    {
                        if (Gridview3.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            Gridview3.SelectedIndex = gvRow.DataItemIndex;
                            Gridview3.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }
            }
            else if (e.CommandName == "RecordDelete")
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
                 new string[] { "Flag", "CFP_Purchase_Order_ID", "Inserted_BY", "Inserted_IP" },
                new string[] { "4", e.CommandArgument.ToString(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            FillGrid();
                           
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds1.Dispose(); GC.SuppressFinalize(objdb); }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtPODt.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string fdat = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string myStringtodate = txtPOendDt.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string tdat = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                if (btnSave.Text == "Save")
                {
                    InsertRecord();
                }
                else
                {
                    UpdateRecord();

                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "PO End Date must be greater than or equal to the PO Date ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        ChildClear();
        GridView2.Visible = false;
        GridView2.SelectedIndex = -1;
        GridView2.DataSource = null;
        GridView2.DataBind();
    }
    private void FillReport()
    {
        try
        {
            DataSet ds1 = new DataSet();
            ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_Order",
                            new string[] { "flag", "CFP_Purchase_Order_ID" },
                            new string[] { "5", ViewState["rowid"].ToString() }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lblCFP.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Name"]);
                    lblCFPAddress.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Address"]);
                    lblCFP1.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Name_E"]);
                    lblCFP2.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Name"]);
                    lblCFP3.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Name"]);
                    lblCFPAddress.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Address"]);
                    lblCFPAddress1.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Address"]);
                    lblCFPincode.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Pincode"]);
                    lblCFPEmail.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Email"]);
                    lblCFPGSTN.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Office_Gst"]);
                    lblCFPName_HO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["HO_OfficeName"]);
                    lblCFPAddress_HO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["HO_OfficeAddress"]);
                    lblCFPPincode_HO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["HO_OfficePincode"]);
                    lblCFPPincode_HO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["HO_OfficePincode"]);
                    lblCFPEmailHO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["HO_Office_Email"]);
                    lblPurchaseorder.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["PO_NO"]);
                    lblPurchaseorderdate.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPODate"]);
                    lblRefNo.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Reference_NO"]);
                   // lblDate.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["ItemPODate"]);
                    lblVendor.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["VendorName"]);
                    lblVendorAddress.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["VendorAddress"]);
                    lblEmail.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["VendorEmailAddress"]);
                    lblGSTNNO.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["GSTNNO"]);                
                    lblTender.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["TenderDate"]);
                    remark.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["Remark"]);
                    supplyscheduleing.InnerText = Convert.ToString(ds1.Tables[0].Rows[0]["SupplyScheduling"]);

                   // string Code = "http://45.114.143.126:8222/PO/POPrintNew.aspx?POID=" + Request.QueryString["PO_NO"].ToString() + "&&DSID=" + Request.QueryString["DSID"].ToString();
                    string Code = lblCFP1.InnerText + " ," + lblCFPAddress.InnerText + " " + lblPurchaseorder.InnerText;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(Code, QRCodeGenerator.ECCLevel.Q);
                    System.Web.UI.WebControls.Image imgQRCode = new System.Web.UI.WebControls.Image();
                    imgQRCode.Height = 125;
                    imgQRCode.Width = 125;
                    using (Bitmap bitmap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();
                            imgQRCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        }
                        PlaceHolderQRCode.Controls.Add(imgQRCode);
                    }

                    StringBuilder sb1 = new StringBuilder();
                    int Count1 = ds1.Tables[1].Rows.Count;
                    int ColCount1 = ds1.Tables[1].Columns.Count;

                    sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>S.No.</b></td>");
                sb1.Append("<td style='border:1px solid black;'><b>Name Of Items</b></td>");
                sb1.Append("<td style='border:1px solid black;'><b>Commodity</b></td>");
                sb1.Append("<td style='border:1px solid black;'><b>Rate</b></td>");
                sb1.Append("<td style='border:1px solid black;'><b>Qty.</b></td>");
                sb1.Append("<td style='border:1px solid black;'><b>Amount</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</thead>");
                decimal amtount = 0;
                for (int i = 0; i < Count1; i++)
                {
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + (i + 1) + "</b></td>");
                    sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["ItemName"] + "</td>");
                    sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["Item_Commodity"] + "</td>");
                    sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["Item_Rate"] + "</td>");
                    sb1.Append("<td style='border:1px solid black;'>" + ds1.Tables[1].Rows[i]["Item_Quantity"] + "</td>");
                    sb1.Append("<td style='border:1px solid black;text-align:right;'>" + ds1.Tables[1].Rows[i]["Amount"] + "</td>");
                    sb1.Append("</tr>");
                    amtount += Convert.ToDecimal(ds1.Tables[1].Rows[i]["Amount"]);
                }
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>Total</b></td>");
                sb1.Append("<td style='border:1px solid black;'></td>");
                sb1.Append("<td style='border:1px solid black;'></td>");
                sb1.Append("<td style='border:1px solid black;'></td>");
                sb1.Append("<td style='border:1px solid black;'></td>");
                sb1.Append("<td style='border:1px solid black;text-align:right;'><b>" + amtount + "</b></td>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();

                   
                    
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #region============================code for Multiple Item Insert==============================
    private void ChildClear()
    {
        ddlItemUnit.SelectedIndex = -1;
        ddlItems.SelectedIndex = -1;
        ddlType.SelectedIndex = -1;
        txtAmount.Text = string.Empty;
        txtitemCommodity.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        lnkAdd.Text = "Add";
        lnkClear.Visible = false;
        GridView2.SelectedIndex = -1;
        
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (btnSave.Text == "Save")
            {

                if (GridView1.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)ViewState["ItemDetails"];


                    dt.Rows.Add(ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue
                               , ddlItemUnit.SelectedValue, txtitemCommodity.Text.Trim(), txtItemQuantity.Text.Trim()
                               , txtItemRate.Text.Trim(), txtAmount.Text.Trim()
                               , ddlCategory.SelectedItem.Text, ddlType.SelectedItem.Text, ddlItems.SelectedItem.Text
                               , ddlItemUnit.SelectedItem.Text

                           );
                    ViewState["ItemDetails"] = dt;
                    this.BindGrid();
                    ChildClear();
                    if (dt != null) { dt.Dispose(); }
                }
                else
                {

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[12] { new DataColumn("ItemCat_id"), new DataColumn("ItemType_id") 
                , new DataColumn("Item_id") , new DataColumn("ItemUnit_id") , new DataColumn("Item_Commodity") 
                , new DataColumn("Item_Quantity") , new DataColumn("Item_Rate") 
                , new DataColumn("Amount")
                 , new DataColumn("ItemCat") , new DataColumn("ItemType") , new DataColumn("Item_Name")
                  , new DataColumn("ItemUnit_Name")
                });

                    dt.Rows.Add(ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue
                               , ddlItemUnit.SelectedValue, txtitemCommodity.Text.Trim(), txtItemQuantity.Text.Trim()
                               , txtItemRate.Text.Trim(), txtAmount.Text.Trim()
                               , ddlCategory.SelectedItem.Text, ddlType.SelectedItem.Text, ddlItems.SelectedItem.Text
                               , ddlItemUnit.SelectedItem.Text

                           );
                    ViewState["ItemDetails"] = dt;
                    this.BindGrid();
                    ChildClear();
                    if (dt != null) { dt.Dispose(); }
                }

            }

            else
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (lnkAdd.Text == "Add")
                {


                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_OrderChild",
                        new string[] { "Flag", "CFP_Purchase_Order_ID", "ItemCat_id", "ItemType_id","Item_id"
                    ,"ItemUnit_id","Item_Commodity","Item_Quantity","Item_Rate","Amount"
                    , "Inserted_BY", "Inserted_IP" },
                       new string[] { "1", ViewState["rowid"].ToString()
                                  ,ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue
                               , ddlItemUnit.SelectedValue, txtitemCommodity.Text.Trim(), txtItemQuantity.Text.Trim()
                               , txtItemRate.Text.Trim(), txtAmount.Text.Trim()
                   , ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GridView2.DataSource = ds1.Tables[1];
                            GridView2.DataBind();
                            ChildClear();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }

                        ds1.Dispose();
                        GC.SuppressFinalize(objdb);
                    }
                }
                else
                {
                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_OrderChild",
                        new string[] { "Flag","CFP_Purchase_Order_ChildID", "CFP_Purchase_Order_ID", "ItemCat_id", "ItemType_id","Item_id"
                    ,"ItemUnit_id","Item_Commodity","Item_Quantity","Item_Rate","Amount"
                    , "Inserted_BY", "Inserted_IP" },
                       new string[] { "2",ViewState["rowidchild"].ToString(), ViewState["rowid"].ToString()
                                  ,ddlCategory.SelectedValue, ddlType.SelectedValue, ddlItems.SelectedValue
                               , ddlItemUnit.SelectedValue, txtitemCommodity.Text.Trim(), txtItemQuantity.Text.Trim()
                               , txtItemRate.Text.Trim(), txtAmount.Text.Trim()
                   , ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GridView2.DataSource = ds1.Tables[1];
                            GridView2.DataBind();
                            ChildClear();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Warning!", ds1.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }

                        ds1.Dispose();
                        GC.SuppressFinalize(objdb);
                    }
                }

            }
        }
        
    }
    protected void lnkClear_Click(object sender, EventArgs e)
    {
        ChildClear();
        GridView2.SelectedIndex = -1;
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordDelete")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    ds1 = objdb.ByProcedure("SP_tblFin_CFP_Purchase_OrderChild",
                   new string[] { "Flag", "CFP_Purchase_Order_ID", "CFP_Purchase_Order_ChildID" },
                  new string[] { "3", ViewState["rowid"].ToString(), e.CommandArgument.ToString() }, "dataset");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                            GridView2.DataSource = ds1.Tables[0];
                            GridView2.DataBind();
                       
                    }

                }
            }
            else if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                   
                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    Label lblItemUnit_id = (Label)row.FindControl("lblItemUnit_id");
                    Label lblItem_Commodity = (Label)row.FindControl("lblItem_Commodity");
                    Label lblItem_Quantity = (Label)row.FindControl("lblItem_Quantity");
                    Label lblItem_Rate = (Label)row.FindControl("lblItem_Rate");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    

                   
                    ddlType.SelectedValue = lblItemType_id.Text;
                    fillItemByType();
                    ddlItems.SelectedValue = lblItem_id.Text;
                    ddlItemUnit.SelectedValue = lblItemUnit_id.Text;                   
                    txtitemCommodity.Text = lblItem_Commodity.Text;
                    txtItemQuantity.Text = lblItem_Quantity.Text;
                    txtItemRate.Text = lblItem_Rate.Text;
                    txtAmount.Text = lblAmount.Text;

                    lblMsg.Text = string.Empty;
                    lnkAdd.Text = "Update";
                    lnkClear.Visible = true;
                    ViewState["rowidchild"] = e.CommandArgument;


                    foreach (GridViewRow gvRow in Gridview3.Rows)
                    {
                        if (GridView2.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView2.SelectedIndex = gvRow.DataItemIndex;
                            GridView2.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5 : ", ex.Message.ToString());
        }
        finally { ds1.Dispose(); GC.SuppressFinalize(objdb); }
    }
    protected void BindGrid()
    {
        GridView1.DataSource = ViewState["ItemDetails"] as DataTable;
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RowDelete")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    int RowIndex = row.RowIndex;
                    int index = Convert.ToInt32(RowIndex);
                    DataTable dt = ViewState["ItemDetails"] as DataTable;
                    dt.Rows[index].Delete();
                    ViewState["ItemDetails"] = dt;
                    BindGrid();
                    if (dt != null) { dt.Dispose(); }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5 : ", ex.Message.ToString());
        }
    }
    #endregion================================End ode for Multiple GCAS No Insert====================


}