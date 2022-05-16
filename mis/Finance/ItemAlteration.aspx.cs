﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_Finance_ItemAlteration : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    FillOffice();
                    FillSalesLedger();
                    FillPurchaseLedger();
                    FillPackagingMode();
                    ViewState["ItemId"] = "0";
                    ddlItemCategory.Enabled = false;

                    ds = objdb.ByProcedure("SpItemCategory",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");

                    ItemGroup.DataSource = ds;
                    ItemGroup.DataTextField = "ItemCatName";
                    ItemGroup.DataValueField = "ItemCat_id";
                    ItemGroup.DataBind();
                    ItemGroup.Items.Insert(0, new ListItem("Select", "0"));
                    ItemGroup.SelectedValue = "1";
					ItemGroup.Enabled = false;
                    ds1 = objdb.ByProcedure("SpUnit",
                                new string[] { "flag" },
                                new string[] { "7" }, "dataset");
                    FillItemCategory();
                    ddlUnit.DataSource = ds1;
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "Unit_Id";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, "Select");

                    ds3 = objdb.ByProcedure("SpFinHSNMaster",
                                new string[] { "flag" },
                                new string[] { "2" }, "dataset");

                    ddlHsnCode.DataSource = ds3;
                    ddlHsnCode.DataTextField = "HSN_Code";
                    ddlHsnCode.DataValueField = "HSN_ID";
                    ddlHsnCode.DataBind();
                    ddlHsnCode.Items.Insert(0, "Select");
                    btnSubmit.Enabled = false;
                    //ds5 = objdb.ByProcedure("SpFinHSNMaster",
                    //            new string[] { "flag" },
                    //            new string[] { "2" }, "dataset");

                    //ddlHsnCode.DataSource = ds5;
                    //ddlHsnCode.DataTextField = "HSN_Code";
                    //ddlHsnCode.DataValueField = "HSN_ID";
                    //ddlHsnCode.DataBind();
                    //ddlHsnCode.Items.Insert(0, "Select");
                    FillGrid();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkOffice.DataSource = ds.Tables[0];
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();

                //chkAllProductionUnit.DataSource = ds.Tables[1];
                //chkAllProductionUnit.DataTextField = "Office_Name";
                //chkAllProductionUnit.DataValueField = "Office_ID";
                //chkAllProductionUnit.DataBind();


                //chkOtherOffice.DataSource = ds.Tables[2];
                //chkOtherOffice.DataTextField = "Office_Name";
                //chkOtherOffice.DataValueField = "Office_ID";
                //chkOtherOffice.DataBind();

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPurchaseLedger()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "8", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlpurchaseledger.DataSource = ds;
                ddlpurchaseledger.DataTextField = "Ledger_Name";
                ddlpurchaseledger.DataValueField = "Ledger_ID";
                ddlpurchaseledger.DataBind();
                ddlpurchaseledger.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalesLedger()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "9", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlsalesledger.DataSource = ds;
                ddlsalesledger.DataTextField = "Ledger_Name";
                ddlsalesledger.DataValueField = "Ledger_ID";
                ddlsalesledger.DataBind();
                ddlsalesledger.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ds.Clear();
                ddlsalesledger.DataSource = ds;
                ddlsalesledger.DataTextField = "Ledger_Name";
                ddlsalesledger.DataValueField = "Ledger_ID";
                ddlsalesledger.DataBind();
                ddlsalesledger.Items.Insert(0, "Select");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPackagingMode()
    {
        try
        {

            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpItemMaster",
                 new string[] { "flag" },
                 new string[] { "25" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPackgngMode.DataSource = ds;
                ddlPackgngMode.DataTextField = "PackagingModeName";
                ddlPackgngMode.DataValueField = "PackagingModeId";
                ddlPackgngMode.DataBind();
                ddlPackgngMode.Items.Insert(0, "Select");

            }
            else
            {
                ddlPackgngMode.Items.Clear();
                ddlPackgngMode.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillItemCategory();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        string msg = "";
    //        if (txtItemName.Text == "")
    //        {
    //            msg = "Enter Item Name";
    //        }
    //        if (ItemGroup.SelectedIndex <= 0)
    //        {
    //            msg += "Select Nature. \\n";
    //        }
    //        if (ddlItemCategory.SelectedIndex <= 0)
    //        {
    //            msg += "Select Item Group. \\n";
    //        }
    //        if (chkpurchaseledger.Checked == true)
    //        {
    //            if (ddlpurchaseledger.SelectedIndex <= 0)
    //            {
    //                msg += "Select Purchase Ledger. \\n";
    //            }
    //        }
    //        if (chksalesledger.Checked == true)
    //        {
    //            if (ddlsalesledger.SelectedIndex <= 0)
    //            {
    //                msg += "Select Sales Ledger . \\n";
    //            }
    //        }
    //        if (ddlUnit.SelectedIndex <= 0)
    //        {
    //            msg += "Select Unit. \\n";
    //        }
    //        if (ddlHsnCode.SelectedIndex <= 0)
    //        {
    //            msg += "Select HSN Code. \\n";
    //        }
    //        if (msg.Trim() == "")
    //        {
    //            int Status = 0;

    //            txtItemName.Text = FirstLetterToUpper(txtItemName.Text);
    //            DataSet ds11 = objdb.ByProcedure("SpItemMaster",
    //                new string[] { "flag", "ItemName", "GroupId", "ItemId" },
    //                new string[] { "18", txtItemName.Text, ItemGroup.SelectedValue, ViewState["ItemId"].ToString() }, "dataset");
    //            if (ds11.Tables[0].Rows.Count > 0)
    //            {
    //                Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

    //            }
    //            if (btnSubmit.Text == "Submit" && ViewState["ItemId"].ToString() == "0" && Status == 0)
    //            {
    //                ds = objdb.ByProcedure("SpItemMaster",
    //                            new string[] { "flag", "GroupId", "CategoryId", "UnitId", "PurchaseLedger_id", "SalesLedger_id", "ItemName", "ItemName_Hindi", "ItemAliasCode", "CreatedBy", "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass", "PackagingSize", "Packaging_Mode", "ItemOrderNo" },
    //                            new string[] { "1", ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, ddlpurchaseledger.SelectedValue.ToString(), ddlsalesledger.SelectedValue.ToString(), txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(), ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, ddlDimensionClass.SelectedItem.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(),txtItemOrderNo.Text }, "dataset");
    //                if (ds != null && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    string ItemId = ds.Tables[0].Rows[0]["ItemId"].ToString();
    //                    if (ItemId != "")
    //                    {
    //                        //foreach (ListItem item in chkOffice.Items)
    //                        //{
    //                        //    if (item.Selected == true)
    //                        //    {
    //                        //        objdb.ByProcedure("SpItemMasterChild",
    //                        //            new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                        //            new string[] { "0", ItemId, item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        //        objdb.ByProcedure("SpFinLedgerChild",
    //                        //           new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                        //           new string[] { "6", ddlsalesledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        //        objdb.ByProcedure("SpFinLedgerChild",
    //                        //           new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                        //           new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");

    //                        //    }
    //                        //}
    //                        if (chkHeadOffice.Checked == true)
    //                        {
    //                            string Value = "1";
    //                            objdb.ByProcedure("SpItemMasterChild",
    //                                    new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                                    new string[] { "0", ItemId, Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                            objdb.ByProcedure("SpFinLedgerChild",
    //                               new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                               new string[] { "6", ddlsalesledger.SelectedValue.ToString(), Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                            objdb.ByProcedure("SpFinLedgerChild",
    //                               new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                               new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), Value, ViewState["Emp_ID"].ToString() }, "dataset");

    //                        }
    //                        foreach (ListItem item in chkOffice.Items)
    //                        {
    //                            if (item.Selected == true)
    //                            {
    //                                objdb.ByProcedure("SpItemMasterChild",
    //                                    new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                                    new string[] { "0", ItemId, item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                                objdb.ByProcedure("SpFinLedgerChild",
    //                                   new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                                   new string[] { "6", ddlsalesledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                                objdb.ByProcedure("SpFinLedgerChild",
    //                                   new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                                   new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");

    //                            }
    //                        }
                         

    //                        //foreach (ListItem item in chkOtherOffice.Items)
    //                        //{
    //                        //    if (item.Selected == true)
    //                        //    {
    //                        //        objdb.ByProcedure("SpItemMasterChild",
    //                        //             new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                        //             new string[] { "0", ItemId, item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        //        objdb.ByProcedure("SpFinLedgerChild",
    //                        //           new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                        //           new string[] { "6", ddlsalesledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        //        objdb.ByProcedure("SpFinLedgerChild",
    //                        //           new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                        //           new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        //    }
    //                        //}
    //                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //                        ClearData();
    //                        FillGrid();
    //                    }
    //                }
    //            }
    //            else if (btnSubmit.Text == "Update" && ViewState["ItemId"].ToString() != "0" && Status == 0)
    //            {

    //                objdb.ByProcedure("SpItemMaster",
    //                       new string[] { "flag", "ItemId", "GroupId", "CategoryId", "UnitId", "PurchaseLedger_id", "SalesLedger_id", "ItemName", "ItemName_Hindi", "ItemAliasCode", "CreatedBy", "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass", "PackagingSize", "Packaging_Mode", "ItemOrderNo" },
    //                       new string[] { "17", ViewState["ItemId"].ToString(), ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, ddlpurchaseledger.SelectedValue.ToString(), ddlsalesledger.SelectedValue.ToString(), txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(), ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, ddlDimensionClass.SelectedItem.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(),txtItemOrderNo.Text }, "dataset");
    //                objdb.ByProcedure("SpItemMasterChild", new string[] { "flag", "Item_id" }, new string[] { "2", ViewState["ItemId"].ToString() }, "dataset");
    //                if (chkHeadOffice.Checked == true)
    //                {
    //                    string Value = "1";
    //                    objdb.ByProcedure("SpItemMasterChild",
    //                        new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                        new string[] { "0", ViewState["ItemId"].ToString(), Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                    objdb.ByProcedure("SpFinLedgerChild",
    //                               new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                               new string[] { "6", ddlsalesledger.SelectedValue.ToString(), Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                    objdb.ByProcedure("SpFinLedgerChild",
    //                       new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                       new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                }
    //                foreach (ListItem item in chkOffice.Items)
    //                {
    //                    if (item.Selected == true)
    //                    {

    //                        objdb.ByProcedure("SpItemMasterChild",
    //                            new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                            new string[] { "0", ViewState["ItemId"].ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        objdb.ByProcedure("SpFinLedgerChild",
    //                                   new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                                   new string[] { "6", ddlsalesledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                        objdb.ByProcedure("SpFinLedgerChild",
    //                           new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                           new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                    }
    //                }
                   

    //                //foreach (ListItem item in chkOtherOffice.Items)
    //                //{
    //                //    if (item.Selected == true)
    //                //    {
    //                //        objdb.ByProcedure("SpItemMasterChild",
    //                //            new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                //            new string[] { "0", ViewState["ItemId"].ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                //        objdb.ByProcedure("SpFinLedgerChild",
    //                //                   new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                //                   new string[] { "6", ddlsalesledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                //        objdb.ByProcedure("SpFinLedgerChild",
    //                //           new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_UpdatedBy" },
    //                //           new string[] { "6", ddlpurchaseledger.SelectedValue.ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");
    //                //    }
    //                //}
    //                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //                ClearData();
    //                FillGrid();
    //                btnSubmit.Enabled = false;
    //            }

    //            else
    //            {
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Name is already exist.');", true);
    //                ClearData();
    //                FillGrid();
    //            }
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtItemName.Text == "")
            {
                msg = "Enter Item Name";
            }
            if (ItemGroup.SelectedIndex <= 0)
            {
                msg += "Select Nature. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg += "Select Item Group. \\n";
            }
            if (chkpurchaseledger.Checked == true)
            {
                if (ddlpurchaseledger.SelectedIndex <= 0)
                {
                    msg += "Select Purchase Ledger. \\n";
                }
            }
            if (chksalesledger.Checked == true)
            {
                if (ddlsalesledger.SelectedIndex <= 0)
                {
                    msg += "Select Sales Ledger . \\n";
                }
            }
            if (ddlUnit.SelectedIndex <= 0)
            {
                msg += "Select Unit. \\n";
            }
            if (ddlHsnCode.SelectedIndex <= 0)
            {
                msg += "Select HSN Code. \\n";
            }
            if (msg.Trim() == "")
            {
                

                txtItemName.Text = FirstLetterToUpper(txtItemName.Text);

                if (btnSubmit.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpItemMaster",
                                new string[] { "flag", "ItemId","GroupId", "CategoryId", "UnitId", "PurchaseLedger_id", "SalesLedger_id", "ItemName", "ItemName_Hindi", "ItemAliasCode",  "HSNCode", "ItemBrand", "ItemSize", "ItemSpecification", "LengthClass", "PackagingSize", "Packaging_Mode", "ItemOrderNo","Office_ID","CreatedBy", "CreatedAt", "CreatedByIP" },
                                new string[] { "17",ViewState["ItemId"].ToString(),ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, ddlpurchaseledger.SelectedValue.ToString(), ddlsalesledger.SelectedValue.ToString(), txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text,  ddlHsnCode.SelectedItem.Text, txtItemBrand.Text, txtItemSize.Text, txtItemSpecification.Text, ddlDimensionClass.SelectedItem.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text,objdb.Office_ID(),objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");


                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");


                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");


                        }

                    }

                    ClearData();
                }


               
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void ClearData()
    {
        txtItemName.Text = "";
        //ItemGroup.ClearSelection();
        txtItemSpecification.Text = "";
        txtItemSize.Text = "";
        txtItemBrand.Text = "";
        ddlDimensionClass.ClearSelection();
        ddlItemCategory.ClearSelection();
        ddlUnit.ClearSelection();
        ddlpurchaseledger.ClearSelection();
        ddlsalesledger.ClearSelection();
        ddlHsnCode.ClearSelection();
        //ddlItemCategory.Enabled = false;
        txtitemaliscode.Text = "";
        SetFocus(txtItemName);
        chkOffice.ClearSelection();
        chkOfficeAll.Checked = false;

        chkHeadOffice.Checked = false;
        chkOfficeAll.Checked = false;
        chkDistrict.Checked = false;
        chkOffice.ClearSelection();     
        chkAllOtherOffice.Checked = false;
        chkOtherOffice.ClearSelection();
        chksalesledger.Checked = true;
        chkpurchaseledger.Checked = true;
        txtItemOrderNo.Text = "";
		btnSubmit.Enabled = false;
    }


    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        //  System.Web.Globalization.cult.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }
    private void FillGrid()
    {
        try
        {
            ds4 = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "GroupId", "CategoryId", "Office_ID" },
                        new string[] { "19", ItemGroup.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString(),objdb.Office_ID() }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GVItemDetail.DataSource = ds4.Tables[0];


                ds5 = ds4;
                //  ds5.Tables.Add(dt);
            }
            else
            {
                GVItemDetail.DataSource = new string[] { };

            }
            GVItemDetail.DataBind();
            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            foreach (GridViewRow rows in GVItemDetail.Rows)
            {
                Label lblID = (Label)rows.FindControl("lblID");
                LinkButton lnkdelete = (LinkButton)rows.FindControl("Delete");
                string id = lblID.Text;
                ds = objdb.ByProcedure("SpFinItemTx",
                             new string[] { "flag", "Item_id" },
                             new string[] { "2", id.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        lnkdelete.Visible = false;
                    }
                    else
                    {
                        lnkdelete.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void GVItemDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string id = GVItemDetail.DataKeys[e.RowIndex].Value.ToString();
            
           objdb.ByProcedure("SpItemMaster",
                             new string[] { "flag", "ItemId" },
                             new string[] { "2", id.ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                
            
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void GVItemDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GVItemDetail.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GVItemDetail.DataSource = ds;
                GVItemDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVItemDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillGrid();
            lblMsg.Text = "";
            chkHeadOffice.Checked = false;
            chkOfficeAll.Checked = false;
            chkDistrict.Checked = false;
            chkOffice.ClearSelection();         
            chkAllOtherOffice.Checked = false;
            chkOtherOffice.ClearSelection();
            ddlPackgngMode.ClearSelection();
            txtPackgngSz.Text = "";

            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            string ItemId = GVItemDetail.SelectedDataKey.Value.ToString();
            ViewState["ItemId"] = ItemId;
            ds = objdb.ByProcedure("SpItemMaster",
                new string[] { "flag", "ItemId" },
                new string[] { "16", ViewState["ItemId"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtItemNameHindi.Text = ds.Tables[0].Rows[0]["ItemName_Hindi"].ToString();
                txtItemOrderNo.Text = ds.Tables[0].Rows[0]["ItemOrderNo"].ToString();
                ItemGroup.ClearSelection();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                ItemGroup.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCat_id"].ToString()).Selected = true;
                FillItemCategory();
                ddlItemCategory.ClearSelection();
                ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;
                ddlUnit.ClearSelection();
                ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_Id"].ToString()).Selected = true;
                ddlHsnCode.ClearSelection();
                ddlHsnCode.Items.FindByText(ds.Tables[0].Rows[0]["HSNCode"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["LengthClass"].ToString() != "")
                {
                    ddlDimensionClass.ClearSelection();
                    ddlDimensionClass.Items.FindByText(ds.Tables[0].Rows[0]["LengthClass"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["ItemAliasCode"].ToString() != "")
                {
                    txtitemaliscode.Text = ds.Tables[0].Rows[0]["ItemAliasCode"].ToString();
                }
                //if (ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString() != "")
                //{
                //    ddlpurchaseledger.ClearSelection();
                //    ddlpurchaseledger.Items.FindByValue(ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString()).Selected = true;
                //}
                //if (ds.Tables[0].Rows[0]["SalesLedger_id"].ToString() != "")
                //{
                //    ddlsalesledger.ClearSelection();
                //    ddlsalesledger.Items.FindByValue(ds.Tables[0].Rows[0]["SalesLedger_id"].ToString()).Selected = true;
                //}
                if (ds.Tables[0].Rows[0]["ItemBrand"].ToString() != "")
                {
                    txtItemBrand.Text = ds.Tables[0].Rows[0]["ItemBrand"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemSize"].ToString() != "")
                {
                    txtItemSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemSpecification"].ToString() != "")
                {
                    txtItemSpecification.Text = ds.Tables[0].Rows[0]["ItemSpecification"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PackagingSize"].ToString() != "")
                {
                    txtPackgngSz.Text = ds.Tables[0].Rows[0]["PackagingSize"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Packaging_Mode"].ToString() != "")
                {
                    ddlPackgngMode.ClearSelection();
                    ddlPackgngMode.Items.FindByValue(ds.Tables[0].Rows[0]["Packaging_Mode"].ToString()).Selected = true;
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    chkOffice.ClearSelection();                    
                    chkOtherOffice.ClearSelection();
                    chkHeadOffice.Checked = false;
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string Value = ds.Tables[1].Rows[i]["Office_ID"].ToString();
                        if (Value == "1")
                        {
                            chkHeadOffice.Checked = true;
                            DataSet ds11 = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Item_id", "Office_ID" }, new string[] { "6", ViewState["ItemId"].ToString(), Value }, "dataset");
                            if (ds11 != null)
                            {
                                if (ds11.Tables[0].Rows[0]["Status"].ToString() == "True")
                                {
                                    chkHeadOffice.Enabled = false;
                                }
                                else
                                {
                                    chkHeadOffice.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (ListItem item in chkOffice.Items)
                            {
                                if (item.Value == Value)
                                {
                                    item.Selected = true;
                                    DataSet ds11 = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Item_id", "Office_ID" }, new string[] { "6", ViewState["ItemId"].ToString(), Value }, "dataset");
                                    if (ds11 != null)
                                    {
                                        if (ds11.Tables[0].Rows[0]["Status"].ToString() == "True")
                                        {
                                            item.Enabled = false;
                                        }
                                        else
                                        {
                                            item.Enabled = true;
                                        }
                                    }
                                }
                            }
                            
                            //foreach (ListItem item in chkOtherOffice.Items)
                            //{
                            //    if (item.Value == Value)
                            //    {
                            //        item.Selected = true;
                            //        DataSet ds11 = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Item_id", "Office_ID" }, new string[] { "6", ViewState["ItemId"].ToString(), Value }, "dataset");
                            //        if (ds11 != null)
                            //        {
                            //            if (ds11.Tables[0].Rows[0]["Status"].ToString() == "True")
                            //            {
                            //                item.Enabled = false;
                            //            }
                            //            else
                            //            {
                            //                item.Enabled = true;
                            //            }
                            //        }
                            //    }
                            //}
                        }


                    }
                    btnSubmit.Enabled = true;
                    if (ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString() != "0")
                    {
                        ddlpurchaseledger.ClearSelection();
                        ddlpurchaseledger.Items.FindByValue(ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString()).Selected = true;
                        chkpurchaseledger.Checked = true;
                    }
                    else
                    {
                        ddlpurchaseledger.ClearSelection();
                        chkpurchaseledger.Checked = false;
                    }
                    if (ds.Tables[0].Rows[0]["SalesLedger_id"].ToString() != "0")
                    {
                        ddlsalesledger.ClearSelection();
                        ddlsalesledger.Items.FindByValue(ds.Tables[0].Rows[0]["SalesLedger_id"].ToString()).Selected = true;
                        chksalesledger.Checked = true;
                    }
                    else
                    {
                        ddlsalesledger.ClearSelection();
                        chksalesledger.Checked = false;
                    }


                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ViewOffice_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkid = (LinkButton)GVItemDetail.Rows[selRowIndex].FindControl("ViewOffice");
            string Item_ID = lnkid.ToolTip.ToString();
            ds = objdb.ByProcedure("SpItemMaster", new string[] { "flag", "ItemId" }, new string[] { "9", Item_ID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;

            }
            else
            {
                GridView1.DataSource = new string[] { };

            }
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillItemCategory()
    {
        lblMsg.Text = "";
        try
        {
            if (ItemGroup.SelectedIndex != 0)
            {
                ddlItemCategory.Enabled = true;
                ds2 = objdb.ByProcedure("SpItemType",
                                    new string[] { "flag", "ItemCat_id" },
                                    new string[] { "6", ItemGroup.SelectedValue }, "dataset");

                ddlItemCategory.DataSource = ds2;
                ddlItemCategory.DataTextField = "ItemTypeName";
                ddlItemCategory.DataValueField = "ItemType_id";
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemCategory.Items.Clear();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchCustomers(string ItemName)
    {
        List<string> customers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds5.Tables[1].DefaultView;
            dv.RowFilter = "ItemName like '%" + ItemName + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    customers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return customers;
    }


    [WebMethod]

    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public string[] GetOfficedetail(string prefix, string selectedValue)
    public string[] GetOfficedetail(string prefix)
    {
        List<string> Officedetail = new List<string>();
        //using (SqlDataReader sdr = cmd.ExecuteReader())
        //{
        //    while (sdr.Read())
        //    {
        //        Officedetail.Add(string.Format("{0}-{1}", sdr["OfficerName"], sdr["ExecutiveID"]));
        //    }
        //}
        return Officedetail.ToArray();

    }
}