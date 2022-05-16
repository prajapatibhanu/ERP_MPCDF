using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_MaterialIssue : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        lblMsg.Text = string.Empty;
        fillProdUnit();
        fillCategory();
        Unit();
        fillGrd();
    }
    private void fillProdUnit()
    {
        try
        {
            ddlcfp.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            ddlcfp.DataSource = ds;
            ddlcfp.DataValueField = "CFPOfficeID";
            ddlcfp.DataTextField = "CFPName";
            ddlcfp.DataBind();
            ddlcfp.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillType();
    }
    protected void ddlitemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillItemByType();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hdnvalue.Value == "0")
        {
            if (!Checktrnfortrndtitmexist())
            {
                InsertUpdateDelete();
            }

            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Failed ! As Record already exist please update.", " ");
            }

        }
        else { InsertUpdateDelete(); }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    private bool GetCompareDate()
    {
        bool res = true;
        try
        {
            string myStringfromdat = txtTransactionDt.Text; // From Database
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
    private void InsertUpdateDelete()
    {
        try
        {
            if (GetCompareDate())
            {
                string flag = "0";
                if (Convert.ToInt32(hdnvalue.Value) > 0)
                {
                    flag = "1";
                }
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_ItemMaterialIssue_Insert_Update_Delete",
                            new string[] { "flag", "TranDt", "ItemCatid", "ItemTypeid", "Itemid", "IssueQuantity", "RequestedPage", "Office_Id", "CFP_ID", "IPAddress", "InsertedBy", "CFPItemMaterialID", "IssueTo", "Name_Of_Sender", "Driver_Name", "Driver_Contact_No", "NoOFBag", "Rate", "Amount", "VehicleNO", "DepartmentID", "MaterialDocumentNo" },
                            new string[] { flag, txtTransactionDt.Text, ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtIssue.Text, Path.GetFileName(Request.Url.AbsolutePath), objapi.Office_ID(), ddlcfp.SelectedValue, Request.UserHostAddress, objapi.createdBy(), hdnvalue.Value, ddlIssue.SelectedValue, txtIssuerName.Text, txtDriver.Text, txtDriverContactNo.Text, txtnoofbags.Text, txtRate.Text, hdnamt.Value, txtVehicle.Text, "1", txtMaterialDocumentno.Text }, "dataset");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                    fillGrd();
                    clear();
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
                }
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Failed ! Issued date Should be less than or equal to current date.", " ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally
        {
            ds.Dispose();
            GC.SuppressFinalize(objapi);
        }
    }

    private void fillGrd()
    {

        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_Trn_CFP_MaterialIssue_By_OfficeID_List", new string[] { "flag", "OfficeID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            gvOpeningStock.DataSource = ds;
            gvOpeningStock.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void Unit()
    {
        try
        {
            ddlUnit.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SpUnit",
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

            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); }
    }
    private void fillCategory()
    {
        try
        {
            ddlitemcategory.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_ItemCategoryList_For_Purchase", new string[] { "flag" }, new string[] { "0" }, "dataset");
            ddlitemcategory.DataSource = ds;
            ddlitemcategory.DataValueField = "CFPItemCat_id";
            ddlitemcategory.DataTextField = "CFP_ItemCatName";
            ddlitemcategory.DataBind();
            ddlitemcategory.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillType()
    {
        try
        {
            ddlitemtype.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPItemType_ItemCat_List", new string[] { "flag", "ItemCatID" }, new string[] { "0", ddlitemcategory.SelectedValue }, "dataset");
            ddlitemtype.DataSource = ds;
            ddlitemtype.DataValueField = "ItemTypeid";
            ddlitemtype.DataTextField = "ItemTypeName";
            ddlitemtype.DataBind();
            ddlitemtype.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillItemByType()
    {
        try
        {
            ddlitems.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPItems_By_ItemTypeID_CFP_List", new string[] { "flag", "ItemTypeID", "CFPID" }, new string[] { "0", ddlitemtype.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            ddlitems.DataSource = ds;
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataBind();
            ddlitems.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private bool Checktrnfortrndtitmexist()
    {
        bool res = false;
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("CheckTrnExist_CFP_ItemMaterialIssueBYTrnDT_Item", new string[] { "flag", "TrnDT", "ItemID", "CFPID" }, new string[] { "0", txtTransactionDt.Text, ddlitems.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    res = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsItemExist"]);
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
        return res;
    }
    private bool ChckItemToCattleFeed()
    {
        bool res = false;
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CheckItemTOCFP", new string[] { "flag", "ItemID", "CFPID" }, new string[] { "0", ddlitems.SelectedValue, ddlcfp.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    res = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsItemExist"]);
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
        return res;
    }
    private void fillAvailableStocks()
    {
        lblMsg.Text = string.Empty;
        if (ChckItemToCattleFeed())
        {
            try
            {
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_Trn_CFP_AvailableQuantity_By_ItemID_List", new string[] { "flag", "ItemID", "CFPID" }, new string[] { "0", ddlitems.SelectedValue, ddlcfp.SelectedValue }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtQty.Text = Convert.ToString(ds.Tables[0].Rows[0]["Available"]);
                        unitname.InnerText = "Issue Quantity (जारी मात्रा)(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                        availunitname.InnerText = "Available Stock (मौजूदा भंडार)(In " + Convert.ToString(ds.Tables[0].Rows[0]["UQCCode"]) + ")";
                        if (txtQty.Text == "0")
                        {
                            lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Item Quantity is not available in Cattle feed to issue.!", "");

                        }
                    }
                    else
                    {
                        txtQty.Text = "0";
                        unitname.InnerText = "Issue Quantity (जारी मात्रा)(In MT)";
                        availunitname.InnerText = "Available Stock (मौजूदा भंडार)(In MT)";

                        lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Item is not available in Cattle feed to issue.!", "");

                    }
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
            }
            finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
        }
        else
        {
            txtQty.Text = "0";
            unitname.InnerText = "Issue Quantity (जारी मात्रा)(In MT)";
            availunitname.InnerText = "Available Stock (मौजूदा भंडार)(In MT)";

            lblMsg.Text = objapi.Alert("fa-ban", "alert-info", "Please  implement Item to cattle feed. You can implement item to cattle feed from Item Registration List!", "");

        }
    }
    private void fillItemUnit()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_ItemUnit_Under_Item", new string[] { "flag", "ItemID" }, new string[] { "0", ddlitems.SelectedValue }, "dataset");
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
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void clear()
    {
        txtTransactionDt.Text = string.Empty;
        ddlcfp.SelectedValue = "0";
        txtQty.Text = string.Empty;
        txtIssue.Text = string.Empty;
        ddlUnit.SelectedValue = "0";
        ddlitemcategory.SelectedValue = "0";
        fillType();
        ddlitemtype.SelectedValue = "0";
        fillItemByType();
        ddlitems.SelectedValue = "0";
        ddlcfp.Enabled = true;
        txtTransactionDt.Enabled = true;
        ddlitemcategory.Enabled = true;
        ddlitemtype.Enabled = true;
        ddlitems.Enabled = true;
        ddlIssue.SelectedValue = "0";
        txtIssuerName.Text = string.Empty;
        txtDriver.Text = string.Empty;
        txtDriverContactNo.Text = string.Empty;
        txtnoofbags.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtVehicle.Text = string.Empty;
        txtIssue.Enabled = true;
        txtMaterialDocumentno.Text = string.Empty;
        if (ddlIssue.SelectedValue == "0" || ddlIssue.SelectedValue == "1") { issuerdetail.Visible = false; issuerPur.Visible = false; }
        if (ddlIssue.SelectedValue == "2") { issuerdetail.Visible = true; issuerPur.Visible = true; }
        hdnvalue.Value = "0";
        hdnamt.Value = "0";
        btnSubmit.Text = "Save";
    }
    protected void ddlitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillAvailableStocks();
        fillItemUnit();
        if (txtQty.Text != string.Empty)
        {
            if (Convert.ToDouble(txtQty.Text) > 0)
            {
                btnSubmit.Visible = true;
            }
            else
                btnSubmit.Visible = false;

        }
    }
    private void fillitembyMaterialIssue()
    {
        DataSet ds1 = new DataSet();
        try
        {

            ds1 = objapi.ByProcedure("SP_Trn_CFP_MaterialIssue_By_MaterialIssueID_List",
                            new string[] { "flag", "MaterialIssueID" },
                            new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlcfp.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CFP_ID"]);
                ddlcfp.Enabled = false;
                txtTransactionDt.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TranDt"]);
                txtTransactionDt.Enabled = false;
                ddlitemcategory.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCatid"]);
                ddlitemcategory.Enabled = false;
                fillType();
                ddlitemtype.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemTypeid"]);
                ddlitemtype.Enabled = false;
                fillItemByType();
                ddlitems.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Itemid"]);
                ddlitems.Enabled = false;
                fillAvailableStocks();
                txtIssue.Text = Convert.ToString(ds1.Tables[0].Rows[0]["IssuedQuantity"]);
                txtIssue.Enabled = false;
                txtQty.Text = (Convert.ToDouble(txtQty.Text) + Convert.ToDouble(txtIssue.Text)).ToString();
                ddlUnit.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Unit_id"]);
                ddlIssue.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["IssueTo"]);
                if (ddlIssue.SelectedValue == "0" || ddlIssue.SelectedValue == "1") { issuerdetail.Visible = false; issuerPur.Visible = false; }
                if (ddlIssue.SelectedValue == "2") { issuerdetail.Visible = true; issuerPur.Visible = true; }
                txtIssuerName.Text = Convert.ToString(ds1.Tables[0].Rows[0]["NameOfSender"]);
                txtDriver.Text = Convert.ToString(ds1.Tables[0].Rows[0]["DriverName"]);
                txtDriverContactNo.Text = Convert.ToString(ds1.Tables[0].Rows[0]["DriverContactNo"]);
                txtnoofbags.Text = Convert.ToString(ds1.Tables[0].Rows[0]["NoOFBag"]);
                txtRate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Rate"]);
                txtAmount.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]) == "0" ? Convert.ToString(Convert.ToDecimal(txtIssue.Text) * Convert.ToDecimal(txtRate.Text)) : Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]);
                hdnamt.Value = Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]) == "0" ? Convert.ToString(Convert.ToDecimal(txtIssue.Text) * Convert.ToDecimal(txtRate.Text)) : Convert.ToString(ds1.Tables[0].Rows[0]["Amount"]);
                txtVehicle.Text = Convert.ToString(ds1.Tables[0].Rows[0]["VehicleNO"]);
                txtMaterialDocumentno.Text = Convert.ToString(ds1.Tables[0].Rows[0]["MaterialDocumentNO"]);

                btnSubmit.Visible = true;
                btnSubmit.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally
        {
            ds1.Dispose();
            GC.SuppressFinalize(objapi);
        }
    }
    private void FillMaterialDetail()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_Trn_CFP_MaterialIssue_DETAIL_BY_ID", new string[] { "flag", "MaterialIssueID" }, new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblcfpname.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Name"]);
                    lblcfp.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["CFP_Address"]);
                    lbSupplier.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Issuer"]);
                    lblReceivedqty.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["IssuedQuantity"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
                    lblItemIssuedDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["IssueDate"]);
                    lblItem.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["IssueTovalue"]) == 0 || Convert.ToInt32(ds.Tables[0].Rows[0]["IssueTovalue"]) == 1)
                    {
                        otherissuedetail.Visible = false;
                        otherissueAmt.Visible = false;
                        otherissueDriver.Visible = false;
                    }
                    else
                    {
                        otherissuedetail.Visible = true;
                        otherissueAmt.Visible = true;
                        otherissueDriver.Visible = true;
                    }

                    lblVehicleNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["VehicleNO"]);
                    lblNoofBags.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NoOFBag"]);
                    lblRate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                    lblAmount.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                    lblDocumentNO.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["MaterialDocumentNO"]);
                    lblDriver.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["DriverName"]);
                    lbDriverContact.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["DriverContactNo"]);
                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void gvOpeningStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "RecordUpdate":
                fillitembyMaterialIssue();
                break;
            case "RecordDelete":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_ItemMaterialIssue_Insert_Update_Delete",
                            new string[] { "flag", "TranDt", "ItemCatid", "ItemTypeid", "Itemid", "IssueQuantity", "RequestedPage", "Office_Id", "CFP_ID", "IPAddress", "InsertedBy", "CFPItemMaterialID", "IssueTo", "Name_Of_Sender", "Driver_Name", "Driver_Contact_No", "NoOFBag", "Rate", "Amount", "VehicleNO", "DepartmentID", "MaterialDocumentNo" },
                            new string[] { "2", txtTransactionDt.Text, ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtIssue.Text, Path.GetFileName(Request.Url.AbsolutePath), objapi.Office_ID(), ddlcfp.SelectedValue, Request.UserHostAddress, objapi.createdBy(), hdnvalue.Value, ddlIssue.SelectedValue, txtIssuerName.Text, txtDriver.Text, txtDriverContactNo.Text, txtnoofbags.Text, txtRate.Text, txtAmount.Text, txtVehicle.Text, "1", txtMaterialDocumentno.Text }, "dataset");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                    fillGrd();
                    clear();
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
                }
                break;
            case "Recordlock":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFP_ItemMaterialIssue_Insert_Update_Delete",
                            new string[] { "flag", "TranDt", "ItemCatid", "ItemTypeid", "Itemid", "IssueQuantity", "RequestedPage", "Office_Id", "CFP_ID", "IPAddress", "InsertedBy", "CFPItemMaterialID", "IssueTo", "Name_Of_Sender", "Driver_Name", "Driver_Contact_No", "NoOFBag", "Rate", "Amount", "VehicleNO", "DepartmentID", "MaterialDocumentNo" },
                            new string[] { "3", txtTransactionDt.Text, ddlitemcategory.SelectedItem.Value, ddlitemtype.SelectedItem.Value, ddlitems.SelectedItem.Value, txtIssue.Text, Path.GetFileName(Request.Url.AbsolutePath), objapi.Office_ID(), ddlcfp.SelectedValue, Request.UserHostAddress, objapi.createdBy(), hdnvalue.Value, ddlIssue.SelectedValue, txtIssuerName.Text, txtDriver.Text, txtDriverContactNo.Text, txtnoofbags.Text, txtRate.Text, txtAmount.Text, txtVehicle.Text, "1", txtMaterialDocumentno.Text }, "dataset");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "SUCCESS")
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                    fillGrd();
                    clear();
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
                }
                break;
            case "RecordReport":
                FillMaterialDetail();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowReport();", true);
                break;

            default:
                break;
        }
    }
    protected void gvOpeningStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOpeningStock.PageIndex = e.NewPageIndex;
        fillGrd();

    }
    protected void ddlIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddlIssue.SelectedValue == "0" || ddlIssue.SelectedValue == "1")
        {
            issuerdetail.Visible = false;
            issuerPur.Visible = false;
            txtIssuerName.Text = string.Empty;
            txtDriver.Text = string.Empty;
            txtDriverContactNo.Text = string.Empty;
            txtnoofbags.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtVehicle.Text = string.Empty;
        }
        if (ddlIssue.SelectedValue == "2")
        {
            issuerdetail.Visible = true;
            issuerPur.Visible = true;
            txtIssuerName.Text = string.Empty;
            txtDriver.Text = string.Empty;
            txtDriverContactNo.Text = string.Empty;
            txtnoofbags.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtVehicle.Text = string.Empty;

        }
    }
    protected void ddlcfp_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        fillItemByType();
    }
}