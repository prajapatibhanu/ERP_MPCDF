using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web.UI;
using System.Web;
public partial class mis_dailyplan_BulkMileSaletoUnionandThirdParty : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    double TQtyInLtr = 0, TQtyInKG = 0, TFatInKG = 0, TsnfInKG = 0,GSTAmt,TCSTAXAmt, Amt = 0, TAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    GetCategory();
                    FillItemName();
                    FillOffice();
                    GetShift();
                    string date = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = date.ToString();
                    txtSearchDate.Text = date.ToString();
                    txttodate.Text = date.ToString();
                    HideshowUnionOrThirdParty();
                    txtDate.Attributes.Add("readonly", "readonly");
                    txtSearchDate.Attributes.Add("readonly", "readonly");
                    txttodate.Attributes.Add("readonly", "readonly");
                    txtFATInKG.Attributes.Add("readonly", "readonly");
                    txtSNFInKG.Attributes.Add("readonly", "readonly");
                    txtAmount.Attributes.Add("readonly", "readonly");
                    txtTCSTAXAmt.Attributes.Add("readonly", "readonly");
                    txtGST_Amt.Attributes.Add("readonly", "readonly");
                    txtTotalAmount.Attributes.Add("readonly", "readonly");
                   
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1 : ", ex.Message.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetShift()
    {
        try
        {
            ddlshift.DataTextField = "ShiftName";
            ddlshift.DataValueField = "Shift_id";
            ddlshift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlshift.DataBind();
            //ddlshift.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    protected void GetCategory()
    {
        try
        {
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void FillItemName()
    {
        try
        {
            ddItemName.DataTextField = "ItemTypeName";
            ddItemName.DataValueField = "ItemType_id";
            ddItemName.DataSource = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag", "ItemCat_id" },
                   new string[] { "8",ddlItemCategory.SelectedValue }, "dataset");
            ddItemName.DataBind();
            ddItemName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        FillItemName();
        if(ddlItemCategory.SelectedValue=="3")
        {
            PanelMilk.Visible = true;
            Panelproduct.Visible = false;
           
            txtQuantityInKG.Text = "";
            txtAmount.Text = "";
            txtFATPer.Text = "";
            txtRate.Text = "";

        }
        else
        {
            PanelMilk.Visible = false;
            Panelproduct.Visible = true;

            txtQuantityInKG.Text = "";
            txtFATPer.Text = "";
            txtSNFPer.Text = "";
            txtFATInKG.Text = "";
            txtSNFInKG.Text = "";
            txtFATRate.Text = "";
            txtSNFRate.Text = "";
            txtAmount.Text = "";

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
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = objdb.Office_ID();
                ddlDS.Enabled = false;


            }
            else
            {
                ddlDS.Items.Clear();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.DataBind();
            }
            if (ds != null) { ds.Dispose(); }

            DataSet ds1 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "1",objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlUnion.DataSource = ds1.Tables[0];
                ddlUnion.DataTextField = "Office_Name";
                ddlUnion.DataValueField = "Office_ID";
                ddlUnion.DataBind();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlUnion.Items.Clear();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnion.DataBind();
            }
            if (ds1 != null) { ds1.Dispose(); }
            DataSet ds2 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag","Office_ID"},
                 new string[] { "2", objdb.Office_ID() }, "dataset");

            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlThirdparty.DataSource = ds2.Tables[0];
                ddlThirdparty.DataTextField = "ThirdPartyUnion_Name";
                ddlThirdparty.DataValueField = "ThirdPartyUnion_Id";
                ddlThirdparty.DataBind();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlThirdparty.Items.Clear();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));
                ddlThirdparty.DataBind();
            }
            if (ds2 != null) { ds2.Dispose(); }
            DataSet ds3 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "4",objdb.Office_ID() }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlMDP.DataSource = ds3.Tables[0];
                ddlMDP.DataTextField = "Office_Name";
                ddlMDP.DataValueField = "Office_ID";
                ddlMDP.DataBind();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlMDP.Items.Clear();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));
                ddlMDP.DataBind();
            }
            if (ds3 != null) { ds3.Dispose(); }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2 : ", ex.Message.ToString());
        }
        
    }
    protected void HideshowUnionOrThirdParty()
    {
        try
        {
            ddlThirdparty.ClearSelection();
            ddlUnion.ClearSelection();
            ddlMDP.ClearSelection();
            lblMsg.Text = "";
            if(rbtnTransferType.SelectedIndex == 0)
            {
                union.Visible = true;
                thirdparty.Visible = false;
                MDP.Visible = false;

            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                MDP.Visible = false;
                union.Visible = false;
                thirdparty.Visible = true;
            }
            else
            {
                MDP.Visible = true;
                union.Visible = false;
                thirdparty.Visible = false;
            }
           // FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3 : ", ex.Message.ToString());
        }
    }
    protected void rbtnsaleto_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideshowUnionOrThirdParty();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            lblMsg.Text = "";
            string gstper = "0";
            string gstamt = "0";
            string tcstaxper = "0";
            string tcstaxamt = "0";
            string IsActive = "1";
            if(Page.IsValid)
            { 
           
            string SaleToOffice_Id =  "";
            if (rbtnTransferType.SelectedIndex == 0)
            {
                SaleToOffice_Id = ddlUnion.SelectedValue;
            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                SaleToOffice_Id = ddlThirdparty.SelectedValue;
            }
            else
            {
                SaleToOffice_Id = ddlMDP.SelectedValue;
            }
                // gst
                if(txtGST_Per.Text=="")
                {
                    gstper="0";

                }
                else
                {
                    gstper = txtGST_Per.Text;
                }
                if(txtGST_Amt.Text=="")
                {
                    gstamt = "0";

                }
                else
                {
                    gstamt = txtGST_Amt.Text;
                }

                // tcs tax
                if (txtTCSTAX.Text == "")
                {
                    tcstaxper = "0";

                }
                else
                {
                    tcstaxper = txtTCSTAX.Text;
                }
                if (txtTCSTAXAmt.Text == "")
                {
                    tcstaxamt = "0";

                }
                else
                {
                    tcstaxamt = txtTCSTAXAmt.Text;
                }
                if (btnSave.Text == "Save")
                {
                    if(ddlItemCategory.SelectedValue=="3")
                    {
                        ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                        new string[] { "flag", "Date", "SaleFromOffice_Id", "SaleToOffice_Id", "MilkTrasferType"
                            ,"ItemCat_id","ItemType_id","SupplyTypeInLtrOrKG","QuantityInKG"
                            ,"FAT_Per","SNF_Per", "FAT_InKG", "SNF_InKG","FATRate_KG","SNFRate_KG","Amount"
                            ,"GST_Per","GST_Amt","TCSTAX_Per","TCSTAX_Amt"
                           ,"TotalAmount" , "Remark", "IsActive", "CreatedBy", "CreatedByIP","shift_id","Gate_passno","DM_No","GST_TYpe" },
                         new string[] { "0", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                             , ddlDS.SelectedValue, SaleToOffice_Id, rbtnTransferType.SelectedValue
                             ,ddlItemCategory.SelectedValue,ddItemName.SelectedValue
                             ,ddlSupplyUnitType.SelectedValue, txtQuantityInKG.Text,
                             txtFATPer.Text,txtSNFPer.Text, txtFATInKG.Text, txtSNFInKG.Text
                             ,txtFATRate.Text,txtSNFRate.Text, txtAmount.Text,gstper,gstamt,tcstaxper,tcstaxamt,txtTotalAmount.Text,txtRemark.Text.Trim()
                             , IsActive, objdb.createdBy(), IPAddress,ddlshift.SelectedValue,txtGatepassNo.Text,txtDMNo.Text,dddlgsttype.SelectedValue }, "dataset");
                    }
                    else
                    {
                        ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                        new string[] { "flag", "Date", "SaleFromOffice_Id", "SaleToOffice_Id", "MilkTrasferType"
                            ,"ItemCat_id","ItemType_id","SupplyTypeInLtrOrKG", "QuantityInKG"
                            ,"FAT_Per","Rate","Amount"
                            ,"GST_Per","GST_Amt","TCSTAX_Per","TCSTAX_Amt"
                           ,"TotalAmount" , "Remark", "IsActive", "CreatedBy", "CreatedByIP","shift_id","Gate_passno","DM_No","GST_TYpe" },
                         new string[] { "0", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                             , ddlDS.SelectedValue, SaleToOffice_Id, rbtnTransferType.SelectedValue
                             ,ddlItemCategory.SelectedValue,ddItemName.SelectedValue
                             ,ddlSupplyUnitType.SelectedValue, txtQuantityInKG.Text,
                             txtFATPer.Text
                             ,txtRate.Text, txtAmount.Text,gstper,gstamt,tcstaxper,tcstaxamt,txtTotalAmount.Text,txtRemark.Text.Trim()
                             , IsActive, objdb.createdBy(), IPAddress,ddlshift.SelectedValue,txtGatepassNo.Text,txtDMNo.Text,dddlgsttype.SelectedValue }, "dataset");
                    }
                    //ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty", 
                    //    new string[] { "flag", "Date", "SaleFromOffice_Id", "SaleToOffice_Id", "MilkTrasferType"
                    //        ,"ItemCat_id","ItemType_id","SupplyTypeInLtrOrKG", "QuantityInLtr","QuantityInKG"
                    //        ,"FAT_Per","SNF_Per", "FAT_InKG", "SNF_InKG","Rate","Amount"
                    //        ,"GST_Per","GST_Amt","TCSTAX_Per","TCSTAX_Amt"
                    //       ,"TotalAmount" , "Remark", "IsActive", "CreatedBy", "CreatedByIP","shift_id","Gate_passno","DM_No" },
                    //     new string[] { "0", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                    //         , ddlDS.SelectedValue, SaleToOffice_Id, rbtnTransferType.SelectedValue
                    //         ,ddlItemCategory.SelectedValue,ddItemName.SelectedValue
                    //         ,ddlSupplyUnitType.SelectedValue, txtQuantityInLtr.Text,txtQuantityInKG.Text,
                    //         txtFATPer.Text,txtSNFPer.Text, txtFATInKG.Text, txtSNFInKG.Text
                    //         ,txtRate.Text, txtAmount.Text,gstper,gstamt,tcstaxper,tcstaxamt,txtTotalAmount.Text,txtRemark.Text.Trim()
                    //         , IsActive, objdb.createdBy(), IPAddress,ddlshift.SelectedValue,txtGatepassNo.Text,txtDMNo.Text }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                PrintInvoice(ds.Tables[0].Rows[0]["BilkMilkSale_Id"].ToString());
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print();", true);
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                ClearText();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error 4 :" + error);
                            }
                        }
                    }
                }
                else if(btnSave.Text == "Update")
                {
                    //ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                    //    new string[] { "flag", "BilkMilkSale_Id", "Date"
                    //        ,"ItemCat_id","ItemType_id","SupplyTypeInLtrOrKG", "QuantityInLtr","QuantityInKG"
                    //        ,"FAT_Per","SNF_Per", "FAT_InKG", "SNF_InKG","Rate","Amount","GST_Per","GST_Amt","TCSTAX_Per","TCSTAX_Amt"
                    //       ,"TotalAmount" , "Remark", "CreatedBy", "CreatedByIP" ,"shift_id","Gate_passno","DM_No"},
                    //     new string[] { "7",ViewState["BilkMilkSale_Id"].ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                    //         ,ddlItemCategory.SelectedValue,ddItemName.SelectedValue ,ddlSupplyUnitType.SelectedValue, txtQuantityInLtr.Text,txtQuantityInKG.Text,
                    //         txtFATPer.Text,txtSNFPer.Text, txtFATInKG.Text, txtSNFInKG.Text
                    //         ,txtRate.Text, txtAmount.Text,gstper,gstamt,tcstaxper,tcstaxamt,txtTotalAmount.Text,txtRemark.Text.Trim(),
                    //         objdb.createdBy(), objdb.GetLocalIPAddress() ,ddlshift.SelectedValue,txtGatepassNo.Text,txtDMNo.Text}, "dataset");
                    if (ddlItemCategory.SelectedValue == "3")
                    {
                        ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                        new string[] { "flag", "BilkMilkSale_Id", "Date"
                            ,"ItemCat_id","ItemType_id","SupplyTypeInLtrOrKG","QuantityInKG"
                            ,"FAT_Per","SNF_Per", "FAT_InKG", "SNF_InKG","FATRate_KG","SNFRate_KG","Amount","GST_Per","GST_Amt","TCSTAX_Per","TCSTAX_Amt"
                           ,"TotalAmount" , "Remark", "CreatedBy", "CreatedByIP" ,"shift_id","Gate_passno","DM_No","GST_TYpe"},
                         new string[] { "7",ViewState["BilkMilkSale_Id"].ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                             ,ddlItemCategory.SelectedValue,ddItemName.SelectedValue ,ddlSupplyUnitType.SelectedValue,txtQuantityInKG.Text,
                             txtFATPer.Text,txtSNFPer.Text, txtFATInKG.Text, txtSNFInKG.Text
                             ,txtFATRate.Text,txtSNFRate.Text, txtAmount.Text,gstper,gstamt,tcstaxper,tcstaxamt,txtTotalAmount.Text,txtRemark.Text.Trim(),
                             objdb.createdBy(), objdb.GetLocalIPAddress() ,ddlshift.SelectedValue,txtGatepassNo.Text,txtDMNo.Text,dddlgsttype.SelectedValue}, "dataset");
                    }
                    else
                    {
                        ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                        new string[] { "flag", "BilkMilkSale_Id", "Date"
                            ,"ItemCat_id","ItemType_id","SupplyTypeInLtrOrKG","QuantityInKG"
                            ,"FAT_Per"
                            ,"Rate","Amount","GST_Per","GST_Amt","TCSTAX_Per","TCSTAX_Amt"
                           ,"TotalAmount" , "Remark", "CreatedBy", "CreatedByIP" ,"shift_id","Gate_passno","DM_No","GST_TYpe"},
                         new string[] { "7",ViewState["BilkMilkSale_Id"].ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                             ,ddlItemCategory.SelectedValue,ddItemName.SelectedValue ,ddlSupplyUnitType.SelectedValue,txtQuantityInKG.Text,
                             txtFATPer.Text
                             ,txtRate.Text, txtAmount.Text,gstper,gstamt,tcstaxper,tcstaxamt,txtTotalAmount.Text,txtRemark.Text.Trim(),
                             objdb.createdBy(), objdb.GetLocalIPAddress() ,ddlshift.SelectedValue,txtGatepassNo.Text,txtDMNo.Text,dddlgsttype.SelectedValue}, "dataset");

                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();                                
                                ClearText();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error 5 :" + error);
                            }
                        }
                    }
                   
                    btnSave.Text = "Save";
                    ddlUnion.Enabled = true;
                    ddlMDP.Enabled = true;
                    ddlThirdparty.Enabled = true;
                    rbtnTransferType.Enabled = true;
                    FillGrid();
                }
       }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6 :", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddItemName.SelectedIndex = 0;
        txtFATPer.Text=string.Empty;
        txtSNFPer.Text=string.Empty;
        txtTotalAmount.Text=string.Empty;
        txtAmount.Text=string.Empty;
        txtRate.Text=string.Empty;
        txtGST_Amt.Text = string.Empty; 
        txtGST_Per.Text = string.Empty;  
        txtTCSTAX.Text=string.Empty;
        txtTCSTAXAmt.Text = string.Empty;
        txtFATInKG.Text = "";
        txtSNFInKG.Text = "";
        ddlUnion.ClearSelection();
        ddlThirdparty.ClearSelection();
        ddlMDP.ClearSelection();
        txtQuantityInLtr.Text = "";
        txtQuantityInKG.Text = string.Empty;
        txtRemark.Text = "";
        GridView1.SelectedIndex = -1;
       // ddlSupplyUnitType.SelectedIndex = 0;
        GetShift();
        //ddlshift.SelectedValue = "1";
        txtGatepassNo.Text = "";
        txtDMNo.Text = "";
         
    }
    protected void FillGrid()
    {
        try
        {
            lblMsg.Text=string.Empty;
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
               new string[] { "flag", "SaleFromOffice_Id", "FromDate","ToDate" },
               new string[] { "5", objdb.Office_ID(), Convert.ToDateTime(txtSearchDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txttodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }

            }
            else
            {
                btnExport.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 7 :", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditRecord")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    ViewState["BilkMilkSale_Id"] = e.CommandArgument.ToString();

                    Label lblDate = (Label)row.FindControl("lblDate");
                    Label lblSaleFromOffice_Id = (Label)row.FindControl("lblSaleFromOffice_Id");
                    Label lblSaleToOffice_Id = (Label)row.FindControl("lblSaleToOffice_Id");
                    Label lblMilkTrasferType = (Label)row.FindControl("lblMilkTrasferType");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblSupplyTypeInLtrOrKG = (Label)row.FindControl("lblSupplyTypeInLtrOrKG");
                    Label lblQuantityInLtr = (Label)row.FindControl("lblQuantityInLtr");
                    Label lblQuantityInKG = (Label)row.FindControl("lblQuantityInKG");
                    Label lblFAT_Per = (Label)row.FindControl("lblFAT_Per");
                    Label lblSNF_Per = (Label)row.FindControl("lblSNF_Per");
                    Label lblFAT_InKG = (Label)row.FindControl("lblFAT_InKG");
                    Label lblSNF_InKG = (Label)row.FindControl("lblSNF_InKG");
                    Label lblRate = (Label)row.FindControl("lblRate");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblGST_Per = (Label)row.FindControl("lblGST_Per");
                    Label lblGST_Amt = (Label)row.FindControl("lblGST_Amt");
                    Label lblTCSTAX_Per = (Label)row.FindControl("lblTCSTAX_Per");
                    Label lblTCSTAX_Amt = (Label)row.FindControl("lblTCSTAX_Amt");
                    Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    Label lblshift_id = (Label)row.FindControl("lblshift_id");
                    Label lblGate_passno = (Label)row.FindControl("lblGate_passno");
                    Label lblDM_No = (Label)row.FindControl("lblDM_No");
                    Label lblGST_TYpe = (Label)row.FindControl("lblGST_TYpe");
                    Label lblFATRate_KG = (Label)row.FindControl("lblFATRate_KG");
                    Label lblSNFRate_KG = (Label)row.FindControl("lblSNFRate_KG");

                    rbtnTransferType.Enabled = false;
                    txtDate.Text = lblDate.Text;

                    if (lblMilkTrasferType.Text == "1")
                    {
                        ddlUnion.ClearSelection();
                        ddlUnion.Items.FindByValue(lblSaleToOffice_Id.Text).Selected = true;
                        ddlUnion.Enabled = false;
                    }
                    else if (lblMilkTrasferType.Text == "2")
                    {
                        ddlThirdparty.ClearSelection();
                        ddlThirdparty.Items.FindByValue(lblSaleToOffice_Id.Text).Selected = true;
                        ddlThirdparty.Enabled = false;
                    }
                    else
                    {
                        ddlMDP.ClearSelection();
                        ddlMDP.Items.FindByValue(lblSaleToOffice_Id.Text).Selected = true;
                        ddlMDP.Enabled = false;
                    }
                    ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    ddlItemCategory_SelectedIndexChanged(sender, e);
                    dddlgsttype.SelectedValue = lblGST_TYpe.Text;
                    FillItemName();
                    GetShift();
                    ddlshift.SelectedValue = lblshift_id.Text;
                    ddItemName.SelectedValue = lblItemType_id.Text;
                    txtQuantityInLtr.Text = lblQuantityInLtr.Text;
                    txtQuantityInKG.Text = lblQuantityInKG.Text;
                    txtFATPer.Text = lblFAT_Per.Text;
                    txtSNFPer.Text = lblSNF_Per.Text;
                    txtFATInKG.Text = lblFAT_InKG.Text;
                    txtSNFInKG.Text = lblSNF_InKG.Text;
                    txtRate.Text = lblRate.Text;
                    txtAmount.Text = lblAmount.Text;
                    txtGST_Per.Text = lblGST_Per.Text;
                    txtGST_Amt.Text = lblGST_Amt.Text;
                    txtTCSTAX.Text = lblTCSTAX_Per.Text;
                    txtTCSTAXAmt.Text = lblTCSTAX_Amt.Text;
                    txtTotalAmount.Text = lblTotalAmount.Text;
                    txtRemark.Text = lblRemark.Text;
                    txtGatepassNo.Text = lblGate_passno.Text;
                    txtDMNo.Text = lblDM_No.Text;
                    txtSNFRate.Text = lblSNFRate_KG.Text;
                    txtFATRate.Text = lblFATRate_KG.Text;

                    btnSave.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }
            }
                else if (e.CommandName == "RecordPrint")
                {

                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        lblMsg.Text = string.Empty;
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        PrintInvoice(e.CommandArgument.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print();", true);

                    }

                }
            else if (e.CommandName == "DMPrint")
            {

                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    PrintDM(e.CommandArgument.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print();", true);

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 8 :", ex.Message.ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQuantityInLtr = (e.Row.FindControl("lblQuantityInLtr") as Label);
                Label lblQuantityInKG = (e.Row.FindControl("lblQuantityInKG") as Label);
                Label lblFAT_InKG = (e.Row.FindControl("lblFAT_InKG") as Label);
                Label lblSNF_InKG = (e.Row.FindControl("lblSNF_InKG") as Label);
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                Label lblGST_Amt = (e.Row.FindControl("lblGST_Amt") as Label);
                Label lblTCSTAX_Amt = (e.Row.FindControl("lblTCSTAX_Amt") as Label);
                Label lblTotalAmount = (e.Row.FindControl("lblTotalAmount") as Label);
                LinkButton lnkDMPrint = (e.Row.FindControl("lnkDMPrint") as LinkButton);
                if (objdb.Office_ID()=="4")
                {
                    lnkDMPrint.Visible = true;
                }
                else
                {
                    lnkDMPrint.Visible = false;
                }

                //TQtyInLtr += Convert.ToDouble(lblQuantityInLtr.Text);
                TQtyInKG += Convert.ToDouble(lblQuantityInKG.Text);
                TFatInKG += Convert.ToDouble(lblFAT_InKG.Text);
                TsnfInKG += Convert.ToDouble(lblSNF_InKG.Text);

                Amt += Convert.ToDouble(lblAmount.Text);
                GSTAmt += Convert.ToDouble(lblGST_Amt.Text);
                TCSTAXAmt += Convert.ToDouble(lblTCSTAX_Amt.Text);
                TAmt += Convert.ToDouble(lblTotalAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label FlblQtyInLtr = (e.Row.FindControl("FlblQtyInLtr") as Label);
                Label FlblQtyInKG = (e.Row.FindControl("FlblQtyInKG") as Label);
                Label FlblFATInKG = (e.Row.FindControl("FlblFATInKG") as Label);
                Label FlblSNFInKG = (e.Row.FindControl("FlblSNFInKG") as Label);
                Label FlblAmount = (e.Row.FindControl("FlblAmount") as Label);
                Label FlblGST_Amt = (e.Row.FindControl("FlblGST_Amt") as Label);
                Label FlblTCSTAX_Amt = (e.Row.FindControl("FlblTCSTAX_Amt") as Label);
                Label FlblTAmount = (e.Row.FindControl("FlblTAmount") as Label);
               // FlblQtyInLtr.Text = TQtyInLtr.ToString("0.00");
                FlblQtyInKG.Text = TQtyInKG.ToString("0.00");
                FlblFATInKG.Text = TFatInKG.ToString("0.00");
                FlblSNFInKG.Text = TsnfInKG.ToString("0.00");
                FlblAmount.Text = Amt.ToString("0.00");
                FlblTCSTAX_Amt.Text = GSTAmt.ToString("0.00");
                FlblTCSTAX_Amt.Text = TCSTAXAmt.ToString("0.00");
                FlblTAmount.Text = TAmt.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        btnSave.Text = "Save";
        ClearText();
        ddlUnion.Enabled = true;
        ddlMDP.Enabled = true;
        ddlThirdparty.Enabled = true;
        rbtnTransferType.Enabled = true;
    }

    private void PrintInvoice(string billid)
    {
        DataSet dsInvo = new DataSet();
        try
        {

            dsInvo = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty"
                            , new string[] { "flag", "BilkMilkSale_Id" }
                            , new string[] { "6", billid.ToString() }, "dataset");
            if (dsInvo != null && dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                string Amountinwords = GenerateWordsinRs(dsInvo.Tables[0].Rows[0]["TotalAmount"].ToString());
                sb.Append("<table class='table'>");
                //sb.Append("<tr>");
                //sb.Append("<td></td>");
                //sb.Append("<td style='text-align:center' colspan='7'><b><u>SUBJECT TO JURIISDICTION</u></b></td>");

                //sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:right' colspan='2'>Invoice No : " + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='5'></td>");
                sb.Append("<td style='text-align:right'>Dated:<b>" + dsInvo.Tables[0].Rows[0]["Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'>" + dsInvo.Tables[0].Rows[0]["Office_FinAddress"].ToString() + "<br/>GSTIN/UIN: " + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "<br/>State Name: Madhya Pradesh,Code : 23<br/>E-Mail: " + dsInvo.Tables[0].Rows[0]["Office_Email"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'>TAX INVOICE<br/>" + dsInvo.Tables[0].Rows[0]["OfficeName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'>Party:<b> " + dsInvo.Tables[0].Rows[0]["TransferTo"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'>GSTIN/UIN: " + dsInvo.Tables[0].Rows[0]["TransferToOffice_Gst"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("</table>");

                sb.Append("<table class='table table-bordered1'>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center'>S.No</th>");
                sb.Append("<th style='text-align:center'>Description of Goods</th>");
                //sb.Append("<th style='text-align:center'>HSN/SAC</th>");
                sb.Append("<th style='text-align:center'>Quantity</th>");
                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<th style='text-align:center'>Rate</th>");
                }
                else
                {
                    sb.Append("<th style='text-align:center'>FAT Qty KG</th>");
                    sb.Append("<th style='text-align:center'>FAT Rate/KG</th>");
                    sb.Append("<th style='text-align:center'>SNF Qty KG</th>");
                    sb.Append("<th style='text-align:center'>SNF Rate/KG</th>");
                }
                sb.Append("<th style='text-align:center'>per</th>");
                //sb.Append("<th style='text-align:center'>Disc %</th>");
                sb.Append("<th style='text-align:center'>Amount</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>1</td>");
                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["ItemTypeName"].ToString() + "</td>");

                //sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["ItemTypeName"].ToString() + "<br/>Bill Details:</b><br/><br/>On Account<span style='padding-left:100px;'>" + dsInvo.Tables[0].Rows[0]["Amount"].ToString() + "</span></td>");
                //sb.Append("<td></td>");
                if (dsInvo.Tables[0].Rows[0]["SupplyTypeInLtrOrKG"].ToString() == "1")
                {
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["QuantityInLtr"].ToString() + "   LTR</b></td>");
                }
                else
                {
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["QuantityInKG"].ToString() + "  KG</b></td>");
                }

                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["Rate"].ToString() + "</td>");
                }
                else
                {
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["FAT_InKG"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["FATRate_KG"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["SNF_InKG"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["SNFRate_KG"].ToString() + "</td>");
                }
                if (dsInvo.Tables[0].Rows[0]["SupplyTypeInLtrOrKG"].ToString() == "1")
                {
                    sb.Append("<td>LTR</td>");
                }
                else
                {
                    sb.Append("<td>KG</td>");
                }


                //sb.Append("<td></td>");
                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["Amount"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>Total</td>");
                //sb.Append("<td></td>");
                if (dsInvo.Tables[0].Rows[0]["SupplyTypeInLtrOrKG"].ToString() == "1")
                {
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["QuantityInLtr"].ToString() + "   LTR</b></td>");
                }
                else
                {
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["QuantityInKG"].ToString() + "  KG</b></td>");
                }

                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                else
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                   // sb.Append("<td></td>");
                }
                sb.Append("<td><b>Rs." + dsInvo.Tables[0].Rows[0]["Amount"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>GST <b>" + dsInvo.Tables[0].Rows[0]["GST_Per"].ToString() + " %</b></td>");
                //sb.Append("<td></td>");
                sb.Append("<td></td>");
                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                else
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                sb.Append("<td><b>Rs." + dsInvo.Tables[0].Rows[0]["GST_Amt"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>TCS TAX <b>" + dsInvo.Tables[0].Rows[0]["TCSTAX_Per"].ToString() + " %</b></td>");
                //sb.Append("<td></td>");
                sb.Append("<td></td>");
                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                else
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                sb.Append("<td><b>Rs." + dsInvo.Tables[0].Rows[0]["TCSTAX_Amt"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>Grand Total</td>");
                //sb.Append("<td></td>");
                sb.Append("<td></td>");
                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                else
                {
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                }
                sb.Append("<td><b>Rs." + dsInvo.Tables[0].Rows[0]["TotalAmount"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table'>");
                sb.Append("<tr><td>Amount Changeable(In Words)</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8'>INR " + Amountinwords + "</td>");
                sb.Append("</tr>");
                if (dsInvo.Tables[0].Rows[0]["ItemCat_id"].ToString() == "2")
                {
                    sb.Append("<tr><td>Remarks:<br/>fat%  " + dsInvo.Tables[0].Rows[0]["FAT_Per"].ToString() +  "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[0]["Remark"].ToString() + "</td>");

                }
                else
                {
                    sb.Append("<tr><td>Remarks:<br/>fat%  " + dsInvo.Tables[0].Rows[0]["FAT_Per"].ToString() + "&nbsp;&nbsp;&nbsp;snf%" + dsInvo.Tables[0].Rows[0]["SNF_Per"].ToString() + "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[0]["Remark"].ToString() + "</td>");

                } 
                sb.Append("</tr>");
                sb.Append("<tr><td><span style='text-decoration:underline'>Declaration</span><br/>We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<td style='text-align:left' colspan='4'>Customer's Seal and Signature</td>");
                sb.Append("<td style='text-align:left' colspan='4'>for " + dsInvo.Tables[0].Rows[0]["OfficeName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:right' colspan='8'>Authorised Signatory</td>");

                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-align:center; text-decoration:underline' colspan='8'>This is a Computer Generated Invoice</td>");

                sb.Append("</tr>");
                sb.Append("</table>");
                divprint.InnerHtml = sb.ToString();

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 9 :", ex.Message.ToString());
        }
        finally
        {
            if (dsInvo != null) { dsInvo.Dispose(); }
        }
    }
    private void PrintDM(string billid)
    {
        DataSet dsInvo = new DataSet();
        try
        {

            dsInvo = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty"
                            , new string[] { "flag", "BilkMilkSale_Id" }
                            , new string[] { "6", billid.ToString() }, "dataset");
            if (dsInvo != null && dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                string Amountinwords = GenerateWordsinRs(dsInvo.Tables[0].Rows[0]["TotalAmount"].ToString());
                sb.Append("<table class='table'>");
                //sb.Append("<tr>");
                //sb.Append("<td></td>");
                //sb.Append("<td style='text-align:center' colspan='7'><b><u>SUBJECT TO JURIISDICTION</u></b></td>");

                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td style='text-align:right' colspan='2'>Invoice No : " + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                //sb.Append("<td colspan='5'></td>");
                //sb.Append("<td style='text-align:right'>Dated:<b>" + dsInvo.Tables[0].Rows[0]["Date"].ToString() + "</b></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td></td>");
                //sb.Append("<td style='text-align:center' colspan='7'>" + dsInvo.Tables[0].Rows[0]["Office_FinAddress"].ToString() + "<br/>GSTIN/UIN: " + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "<br/>State Name: Madhya Pradesh,Code : 23<br/>E-Mail: " + dsInvo.Tables[0].Rows[0]["Office_Email"].ToString() + "</td>");
                //sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'><b style='font-size:25px'>" + dsInvo.Tables[0].Rows[0]["OfficeName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'><span style='font-size:18px'>चांदा  तलावली  मांगलिया  इंदौर  म. प्र. फोन : 2802535</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'><b style='font-size:18px'>दूध एवं दुग्ध पदार्थ गेट पास डी. एम.</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center' colspan='7'><span style='font-size:18px'>निम्नांकित  गेट पास के द्वारा जारी की गई सामग्री संयंत्र गेट से बहार ले जाने की स्वीकृति दी जाती है |</span></td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='table table-bordered1'>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center'></th>");
                sb.Append("<th style='text-align:center'>क्रेट</th>");
                sb.Append("<th style='text-align:center'>केन / बॉक्स</th>");
                sb.Append("<th style='text-align:left' rowspan=2><b>गेट पास नं. :" + dsInvo.Tables[0].Rows[0]["Gate_passno"].ToString() + "</b><br/><b>डी. एम. नं. :" + dsInvo.Tables[0].Rows[0]["DM_No"].ToString() + "</b><br/>दिनांक :" + dsInvo.Tables[0].Rows[0]["Date"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;समय :" + dsInvo.Tables[0].Rows[0]["Time"].ToString() + "<br/>शिफ्ट :" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</th>");
                

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:left'>" + dsInvo.Tables[0].Rows[0]["TransferTo"].ToString() + "</th>");
                sb.Append("<th style='text-align:center'></th>");
                sb.Append("<th style='text-align:center'></th>");
                //sb.Append("<th style='text-align:center'></th>");


                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center' colspan=4></th>");
                
                //sb.Append("<th style='text-align:center'></th>");


                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</table>");

                sb.Append("<table class='table table-bordered1'>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center;width:50px'>क्रमांक</th>");
                sb.Append("<th style='text-align:center'>विवरण</th>");
                sb.Append("<th style='text-align:center;width:150px'>मात्रा</th>");
                sb.Append("<th style='text-align:center;width:100px'>क्रेट</th>");
                sb.Append("<th style='text-align:center;width:100px'>केन / बॉक्स</th>");
                sb.Append("<th style='text-align:center;width:100px'>थप्पी</th>");
                
                sb.Append("</tr>");
                sb.Append("<tr style='Height:200px'>");
                sb.Append("<td>1</td>");
                sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["ItemTypeName"].ToString() + "</td>");
                //sb.Append("<td></td>");
                if (dsInvo.Tables[0].Rows[0]["SupplyTypeInLtrOrKG"].ToString() == "1")
                {
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["QuantityInLtr"].ToString() + "   LTR</td>");
                }
                else
                {
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[0]["QuantityInKG"].ToString() + "  KG</td>");
                }

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>योग</b></td>");
                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[0]["TotalAmount"].ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table'>");
               
                sb.Append("<tr><td>सुरक्षा गेट पास से बाहर जाने का दिनांक : <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</u>समय : <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></td>");
                sb.Append("</tr>");
                
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<td style='text-align:center;padding-right:300px' >हस्ताक्षर सामग्री प्राप्तकर्ता</br>(वाहन चालक) </td>");
                sb.Append("<td style='text-align:center'>हस्ताक्षर सामग्री प्राप्तकर्ता </br> (उत्पादन /भंडार शाखा )</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                divprint.InnerHtml = sb.ToString();

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 9 :", ex.Message.ToString());
        }
        finally
        {
            if (dsInvo != null) { dsInvo.Dispose(); }
        }
    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            //FillGrid();
            GetCompareDate();
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtSearchDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txttodate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                FillGrid();
            }
            else
            {
                txttodate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5: ", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "Bulk" + ddlItemCategory.SelectedItem.Text + "Sale " + "-" + txtSearchDate.Text + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            GridView1.Columns[17].Visible = false;
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-Consolidated " + ex.Message.ToString());
        }
    }
}