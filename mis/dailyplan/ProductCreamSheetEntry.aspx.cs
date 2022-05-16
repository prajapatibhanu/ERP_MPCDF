using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Text;


public partial class mis_dailyplan_ProductCreamSheetEntry : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    FillShift();
                    GetSectionView(sender, e);

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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;
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
    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[1].Rows[0]["Shift_Id"].ToString();
                txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                ddlShift.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
        }

    }
    private void GetSectionView(object sender, EventArgs e)
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.SelectedValue = "2";
                ddlPSection.Enabled = false;

                ddlPSection_SelectedIndexChanged(sender, e);

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
                ddlPSection.Enabled = false;
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {

            GetSectionDetail();

        }

    }
    private void GetSectionDetail()
    {

        try
        {
			btnPopupSave_Product.Text = "Save";
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            // For Variant
            txtGainLossQtyInKg.Text = "";
            txtGainLossFatInKg.Text = "";
            txtGainLossSnfInKg.Text = "";
            DataSet dsVD_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "9", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, objdb.LooseCreamItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
				if (dsVD_Child.Tables[0].Rows[0]["PPSM"].ToString() != "0")
                {
                    btnPopupSave_Product.Text = "Update";
                    ViewState["PPSM"] = dsVD_Child.Tables[0].Rows[0]["PPSM"].ToString();
                }
                GVVariantDetail_In.DataSource = dsVD_Child;
                GVVariantDetail_In.DataBind();
                GVVariantDetail_Out.DataSource = dsVD_Child;
                GVVariantDetail_Out.DataBind();
            }

            else
            {
                GVVariantDetail_In.DataSource = string.Empty;
                GVVariantDetail_In.DataBind();
                GVVariantDetail_Out.DataSource = string.Empty;
                GVVariantDetail_Out.DataBind();
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            decimal TotalQtyInflow = 0;
            decimal TotalQtyOutFlow = 0;
            decimal TotalFatInflow = 0;
            decimal TotalFatOutFlow = 0;
            decimal TotalSnfInflow = 0;
            decimal TotalSnfOutFlow = 0;
            foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
            {
                Label lblCST1 = (Label)row1.FindControl("lblCST1");
                Label lblCST2 = (Label)row1.FindControl("lblCST2");
                Label lblClosingBalanceGood = (Label)row1.FindControl("lblClosingBalanceGood");
                Label lblItemName = (Label)row1.FindControl("lblItemName");
                Label lblClosingBalanceSour = (Label)row1.FindControl("lblClosingBalanceSour");

                TextBox txtGoodMilk = (TextBox)row1.FindControl("txtGoodMilk");
                TextBox txtSourMilk = (TextBox)row1.FindControl("txtSourMilk");
                TextBox txtRcvdfromProcSctn = (TextBox)row1.FindControl("txtRcvdfromProcSctn");
                if (lblCST1.Text == "")
                {
                    lblCST1.Text = "0";
                }
                if (lblCST2.Text == "")
                {
                    lblCST2.Text = "0";
                }
                if (lblClosingBalanceGood.Text == "")
                {
                    lblClosingBalanceGood.Text = "0";
                }
                if (lblClosingBalanceSour.Text == "")
                {
                    lblClosingBalanceSour.Text = "0";
                }

                if (txtGoodMilk.Text == "")
                {
                    txtGoodMilk.Text = "0";
                }

                if (txtSourMilk.Text == "")
                {
                    txtSourMilk.Text = "0";
                }
                if (txtRcvdfromProcSctn.Text == "")
                {
                    txtRcvdfromProcSctn.Text = "0";
                }

                TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

                lblIntotal.Text = (Convert.ToDecimal(lblCST1.Text) + Convert.ToDecimal(lblCST2.Text) + Convert.ToDecimal(lblClosingBalanceGood.Text) + Convert.ToDecimal(lblClosingBalanceSour.Text) + Convert.ToDecimal(txtGoodMilk.Text) + Convert.ToDecimal(txtSourMilk.Text) + Convert.ToDecimal(txtRcvdfromProcSctn.Text)).ToString();
                if (lblIntotal.Text == "")
                {
                    lblIntotal.Text = "0";
                }
                if (lblItemName.Text == " Quantity In Kg")
                {
                    TotalQtyInflow = decimal.Parse(lblIntotal.Text);
                }
                if (lblItemName.Text == " Fat In Kg")
                {
                    TotalFatInflow = decimal.Parse(lblIntotal.Text);
                }
                if (lblItemName.Text == " Snf In Kg")
                {
                    TotalSnfInflow = decimal.Parse(lblIntotal.Text);
                }
                if (lblIntotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }


            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {
                Label Label2 = (Label)row1.FindControl("Label2");
                TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
                TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
                TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
                TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
                TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
                TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");
                TextBox txtIssueFor_TableButter = (TextBox)row1.FindControl("txtIssueFor_TableButter");
                TextBox txtIssueFor_CookingButter = (TextBox)row1.FindControl("txtIssueFor_CookingButter");
                TextBox txtReturntoProcessingSection = (TextBox)row1.FindControl("txtReturntoProcessingSection");
                TextBox txtCreamIssueforSale = (TextBox)row1.FindControl("txtCreamIssueforSale");
                TextBox txtCreamIssueforothers = (TextBox)row1.FindControl("txtCreamIssueforothers");
                if (txtIssueFor_WhiteButterGood.Text == "")
                {
                    txtIssueFor_WhiteButterGood.Text = "0";
                }
                if (txtIssueFor_WhiteButterSour.Text == "")
                {
                    txtIssueFor_WhiteButterSour.Text = "0";
                }
                if (txtIssueFor_TableButter.Text == "")
                {
                    txtIssueFor_TableButter.Text = "0";
                }

                if (txtIssueFor_CookingButter.Text == "")
                {
                    txtIssueFor_CookingButter.Text = "0";
                }

                if (txtCreamIssueforSale.Text == "")
                {
                    txtCreamIssueforSale.Text = "0";
                }

                if (txtCreamIssueforothers.Text == "")
                {
                    txtCreamIssueforothers.Text = "0";
                }

                if (txtReturntoProcessingSection.Text == "")
                {
                    txtReturntoProcessingSection.Text = "0";
                }

                if (txtCST1.Text == "")
                {
                    txtCST1.Text = "0";
                }

                if (txtCST2.Text == "")
                {
                    txtCST2.Text = "0";
                }

                if (txtSourQty.Text == "")
                {
                    txtSourQty.Text = "0";
                }

                if (txtCLClosing.Text == "")
                {
                    txtCLClosing.Text = "0";
                }

                TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

                lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text) + Convert.ToDecimal(txtIssueFor_TableButter.Text) + Convert.ToDecimal(txtIssueFor_CookingButter.Text) + Convert.ToDecimal(txtReturntoProcessingSection.Text) + Convert.ToDecimal(txtCreamIssueforSale.Text) + Convert.ToDecimal(txtCreamIssueforothers.Text)).ToString();
                if (lblouttotal.Text == "")
                {
                    lblouttotal.Text = "0";
                }
                if (Label2.Text == " Quantity In Kg")
                {
                    TotalQtyOutFlow = decimal.Parse(lblouttotal.Text);
                }
                if (Label2.Text == " Fat In Kg")
                {
                    TotalFatOutFlow = decimal.Parse(lblouttotal.Text);
                }
                if (Label2.Text == " Snf In Kg")
                {
                    TotalSnfOutFlow = decimal.Parse(lblouttotal.Text);
                }
                if (lblouttotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }
            txtGainLossQtyInKg.Text = (TotalQtyOutFlow - TotalQtyInflow).ToString();
            txtGainLossFatInKg.Text = (TotalFatOutFlow - TotalFatInflow).ToString();
            txtGainLossSnfInKg.Text = (TotalSnfOutFlow - TotalSnfInflow).ToString();

            decimal TotalGainLoss = Convert.ToDecimal(txtGainLossQtyInKg.Text) + Convert.ToDecimal(txtGainLossFatInKg.Text) + Convert.ToDecimal(txtGainLossSnfInKg.Text);
            if (TotalGainLoss == 0)
            {
                lblGainLoss.Text = "";
                btnPopupSave_Product.Enabled = true;
            }
            else
            {
                //btnPopupSave_Product.Enabled = false;
                lblGainLoss.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Gain/Loss should be 0");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    //protected void txtGoodMilk_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
    //        {

    //            Label lblClosingBalanceGood = (Label)row1.FindControl("lblClosingBalanceGood");
    //            Label lblClosingBalanceSour = (Label)row1.FindControl("lblClosingBalanceSour");

    //            TextBox txtGoodMilk = (TextBox)row1.FindControl("txtGoodMilk");
    //            TextBox txtSourMilk = (TextBox)row1.FindControl("txtSourMilk");

    //            if (lblClosingBalanceGood.Text == "")
    //            {
    //                lblClosingBalanceGood.Text = "0";
    //            }

    //            if (lblClosingBalanceSour.Text == "")
    //            {
    //                lblClosingBalanceSour.Text = "0";
    //            }

    //            if (txtGoodMilk.Text == "")
    //            {
    //                txtGoodMilk.Text = "0";
    //            }

    //            if (txtSourMilk.Text == "")
    //            {
    //                txtSourMilk.Text = "0";
    //            }


    //            TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

    //            lblIntotal.Text = (Convert.ToDecimal(lblClosingBalanceGood.Text)+ Convert.ToDecimal(lblClosingBalanceSour.Text) + Convert.ToDecimal(txtGoodMilk.Text) + Convert.ToDecimal(txtSourMilk.Text)).ToString();

    //            if (lblIntotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtSourMilk_TextChanged(object sender, EventArgs e)
    //{
    //     try
    //    {
    //        lblMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
    //        {

    //            Label lblClosingBalanceGood = (Label)row1.FindControl("lblClosingBalanceGood");
    //            Label lblClosingBalanceSour = (Label)row1.FindControl("lblClosingBalanceSour");

    //            TextBox txtGoodMilk = (TextBox)row1.FindControl("txtGoodMilk");
    //            TextBox txtSourMilk = (TextBox)row1.FindControl("txtSourMilk");

    //            if (lblClosingBalanceGood.Text == "")
    //            {
    //                lblClosingBalanceGood.Text = "0";
    //            }

    //            if (lblClosingBalanceSour.Text == "")
    //            {
    //                lblClosingBalanceSour.Text = "0";
    //            }

    //            if (txtGoodMilk.Text == "")
    //            {
    //                txtGoodMilk.Text = "0";
    //            }

    //            if (txtSourMilk.Text == "")
    //            {
    //                txtSourMilk.Text = "0";
    //            }


    //            TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");
    //            lblIntotal.Text = (Convert.ToDecimal(lblClosingBalanceGood.Text)+ Convert.ToDecimal(lblClosingBalanceSour.Text) + Convert.ToDecimal(txtGoodMilk.Text) + Convert.ToDecimal(txtSourMilk.Text)).ToString();

    //            if (lblIntotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtIssueFor_WhiteButterGood_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
    //            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
    //            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
    //            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
    //            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueFor_WhiteButterGood.Text == "")
    //            {
    //                txtIssueFor_WhiteButterGood.Text = "0";
    //            }
    //            if (txtIssueFor_WhiteButterSour.Text == "")
    //            {
    //                txtIssueFor_WhiteButterSour.Text = "0";
    //            }

    //            if (txtCST1.Text == "")
    //            {
    //                txtCST1.Text = "0";
    //            }

    //            if (txtCST2.Text == "")
    //            {
    //                txtCST2.Text = "0";
    //            }

    //            if (txtSourQty.Text == "")
    //            {
    //                txtSourQty.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtIssueFor_WhiteButterSour_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
    //            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
    //            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
    //            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
    //            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueFor_WhiteButterGood.Text == "")
    //            {
    //                txtIssueFor_WhiteButterGood.Text = "0";
    //            }
    //            if (txtIssueFor_WhiteButterSour.Text == "")
    //            {
    //                txtIssueFor_WhiteButterSour.Text = "0";
    //            }

    //            if (txtCST1.Text == "")
    //            {
    //                txtCST1.Text = "0";
    //            }

    //            if (txtCST2.Text == "")
    //            {
    //                txtCST2.Text = "0";
    //            }

    //            if (txtSourQty.Text == "")
    //            {
    //                txtSourQty.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtCLClosing_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
    //            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
    //            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
    //            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
    //            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueFor_WhiteButterGood.Text == "")
    //            {
    //                txtIssueFor_WhiteButterGood.Text = "0";
    //            }
    //            if (txtIssueFor_WhiteButterSour.Text == "")
    //            {
    //                txtIssueFor_WhiteButterSour.Text = "0";
    //            }

    //            if (txtCST1.Text == "")
    //            {
    //                txtCST1.Text = "0";
    //            }

    //            if (txtCST2.Text == "")
    //            {
    //                txtCST2.Text = "0";
    //            }

    //            if (txtSourQty.Text == "")
    //            {
    //                txtSourQty.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtCST1_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
    //            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
    //            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
    //            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
    //            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueFor_WhiteButterGood.Text == "")
    //            {
    //                txtIssueFor_WhiteButterGood.Text = "0";
    //            }
    //            if (txtIssueFor_WhiteButterSour.Text == "")
    //            {
    //                txtIssueFor_WhiteButterSour.Text = "0";
    //            }

    //            if (txtCST1.Text == "")
    //            {
    //                txtCST1.Text = "0";
    //            }

    //            if (txtCST2.Text == "")
    //            {
    //                txtCST2.Text = "0";
    //            }

    //            if (txtSourQty.Text == "")
    //            {
    //                txtSourQty.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtCST2_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
    //            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
    //            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
    //            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
    //            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueFor_WhiteButterGood.Text == "")
    //            {
    //                txtIssueFor_WhiteButterGood.Text = "0";
    //            }
    //            if (txtIssueFor_WhiteButterSour.Text == "")
    //            {
    //                txtIssueFor_WhiteButterSour.Text = "0";
    //            }

    //            if (txtCST1.Text == "")
    //            {
    //                txtCST1.Text = "0";
    //            }

    //            if (txtCST2.Text == "")
    //            {
    //                txtCST2.Text = "0";
    //            }

    //            if (txtSourQty.Text == "")
    //            {
    //                txtSourQty.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtSourQty_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
    //            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
    //            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
    //            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
    //            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueFor_WhiteButterGood.Text == "")
    //            {
    //                txtIssueFor_WhiteButterGood.Text = "0";
    //            }
    //            if (txtIssueFor_WhiteButterSour.Text == "")
    //            {
    //                txtIssueFor_WhiteButterSour.Text = "0";
    //            }

    //            if (txtCST1.Text == "")
    //            {
    //                txtCST1.Text = "0";
    //            }

    //            if (txtCST2.Text == "")
    //            {
    //                txtCST2.Text = "0";
    //            }

    //            if (txtSourQty.Text == "")
    //            {
    //                txtSourQty.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text) + Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text) + Convert.ToDecimal(txtCST1.Text) + Convert.ToDecimal(txtCST2.Text) + Convert.ToDecimal(txtSourQty.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    private DataTable GetProductVarientInfo()
    {

        decimal CreamGoodQtyOpening = 0;
        decimal CreamSourQtyOpening = 0;


        decimal CreamObtFrom_GoodMilk = 0;
        decimal CreamObtFrom_SourMilk = 0;
        decimal CreamReceivedfromProcessSection = 0;
        decimal IssueFor_WhiteButterGood = 0;
        decimal IssueFor_WhiteButterSour = 0;
        decimal IssueFor_TableButter = 0;
        decimal IssueFor_CookingButter = 0;
        decimal ReturntoProcessingSection = 0;
        decimal IssueFor_Sale = 0;
        decimal IssueFor_Others = 0;
        decimal Closing = 0;
        decimal CST1 = 0;
        decimal CST2 = 0;
        decimal SourQty = 0;

        decimal InTotal = 0;
        decimal OutTotal = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("CreamGoodQtyOpening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamSourQtyOpening", typeof(decimal)));

        dt.Columns.Add(new DataColumn("CreamObtFrom_GoodMilk", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamObtFrom_SourMilk", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamReceivedfromProcessSection", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueFor_WhiteButterGood", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueFor_WhiteButterSour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueFor_TableButter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueFor_CookingButter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ReturntoProcessingSection", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueFor_Sale", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueFor_Others", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Closing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CST1", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CST2", typeof(decimal)));
        dt.Columns.Add(new DataColumn("SourQty", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutTotal", typeof(decimal)));

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");

            Label lblClosingBalanceGood = (Label)row.FindControl("lblClosingBalanceGood");
            Label lblClosingBalanceSour = (Label)row.FindControl("lblClosingBalanceSour");

            TextBox txtGoodMilk = (TextBox)row.FindControl("txtGoodMilk");
            TextBox txtSourMilk = (TextBox)row.FindControl("txtSourMilk");
            TextBox txtRcvdfromProcSctn = (TextBox)row.FindControl("txtRcvdfromProcSctn");

            if (lblClosingBalanceGood.Text != "" && lblClosingBalanceGood.Text != "0" && lblClosingBalanceGood.Text != "0.00")
            {
                CreamGoodQtyOpening = Convert.ToDecimal(lblClosingBalanceGood.Text);
            }
            else
            {
                CreamGoodQtyOpening = 0;
            }


            if (lblClosingBalanceSour.Text != "" && lblClosingBalanceSour.Text != "0" && lblClosingBalanceSour.Text != "0.00")
            {
                CreamSourQtyOpening = Convert.ToDecimal(lblClosingBalanceSour.Text);
            }
            else
            {
                CreamSourQtyOpening = 0;
            }

            if (txtGoodMilk.Text != "" && txtGoodMilk.Text != "0" && txtGoodMilk.Text != "0.00")
            {
                CreamObtFrom_GoodMilk = Convert.ToDecimal(txtGoodMilk.Text);
            }
            else
            {
                CreamObtFrom_GoodMilk = 0;
            }


            if (txtSourMilk.Text != "" && txtSourMilk.Text != "0" && txtSourMilk.Text != "0.00")
            {
                CreamObtFrom_SourMilk = Convert.ToDecimal(txtSourMilk.Text);
            }
            else
            {
                CreamObtFrom_SourMilk = 0;
            }
            if (txtRcvdfromProcSctn.Text != "" && txtRcvdfromProcSctn.Text != "0" && txtRcvdfromProcSctn.Text != "0.00")
            {
                CreamReceivedfromProcessSection = Convert.ToDecimal(txtRcvdfromProcSctn.Text);
            }
            else
            {
                CreamReceivedfromProcessSection = 0;
            }
            InTotal = CreamGoodQtyOpening + CreamSourQtyOpening + CreamObtFrom_GoodMilk + CreamObtFrom_SourMilk + CreamReceivedfromProcessSection;

            dr = dt.NewRow();
            dr[0] = objdb.LooseCreamItemCategoryId_ID();
            dr[1] = objdb.LooseCreamItemTypeId_ID();
            dr[2] = lblItem_id.Text;
            dr[3] = CreamGoodQtyOpening;
            dr[4] = CreamSourQtyOpening;
            dr[5] = CreamObtFrom_GoodMilk;
            dr[6] = CreamObtFrom_SourMilk;
            dr[7] = CreamReceivedfromProcessSection;
            dr[8] = IssueFor_WhiteButterGood;
            dr[9] = IssueFor_WhiteButterSour;
            dr[10] = IssueFor_TableButter;
            dr[11] = IssueFor_CookingButter;
            dr[12] = ReturntoProcessingSection;
            dr[13] = IssueFor_Sale;
            dr[14] = IssueFor_Others;
            dr[15] = Closing;
            dr[16] = CST1;
            dr[17] = CST2;
            dr[18] = SourQty;
            dr[19] = InTotal;
            dr[20] = OutTotal;
            dt.Rows.Add(dr);


        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
            TextBox txtIssueFor_WhiteButterGood = (TextBox)row1.FindControl("txtIssueFor_WhiteButterGood");
            TextBox txtIssueFor_WhiteButterSour = (TextBox)row1.FindControl("txtIssueFor_WhiteButterSour");
            TextBox txtIssueFor_TableButter = (TextBox)row1.FindControl("txtIssueFor_TableButter");
            TextBox txtIssueFor_CookingButter = (TextBox)row1.FindControl("txtIssueFor_CookingButter");
            TextBox txtReturntoProcessingSection = (TextBox)row1.FindControl("txtReturntoProcessingSection");
            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");
            TextBox txtCST1 = (TextBox)row1.FindControl("txtCST1");
            TextBox txtCST2 = (TextBox)row1.FindControl("txtCST2");
            TextBox txtSourQty = (TextBox)row1.FindControl("txtSourQty");
            TextBox txtCreamIssueforSale = (TextBox)row1.FindControl("txtSourQty");
            TextBox txtCreamIssueforothers = (TextBox)row1.FindControl("txtSourQty");

            if (txtIssueFor_WhiteButterGood.Text != "0" && txtIssueFor_WhiteButterGood.Text != "0.00")
            {
                IssueFor_WhiteButterGood = Convert.ToDecimal(txtIssueFor_WhiteButterGood.Text);
            }
            else
            {
                IssueFor_WhiteButterGood = 0;
            }

            if (txtIssueFor_WhiteButterSour.Text != "0" && txtIssueFor_WhiteButterSour.Text != "0.00")
            {
                IssueFor_WhiteButterSour = Convert.ToDecimal(txtIssueFor_WhiteButterSour.Text);
            }
            else
            {
                IssueFor_WhiteButterSour = 0;
            }
            if (txtIssueFor_TableButter.Text != "0" && txtIssueFor_TableButter.Text != "0.00")
            {
                IssueFor_TableButter = Convert.ToDecimal(txtIssueFor_TableButter.Text);
            }
            else
            {
                IssueFor_TableButter = 0;
            }

            if (txtIssueFor_CookingButter.Text != "0" && txtIssueFor_CookingButter.Text != "0.00")
            {
                IssueFor_CookingButter = Convert.ToDecimal(txtIssueFor_CookingButter.Text);
            }
            else
            {
                IssueFor_CookingButter = 0;
            }
            if (txtReturntoProcessingSection.Text != "0" && txtReturntoProcessingSection.Text != "0.00")
            {
                ReturntoProcessingSection = Convert.ToDecimal(txtReturntoProcessingSection.Text);
            }
            else
            {
                ReturntoProcessingSection = 0;
            }
            if (txtCreamIssueforSale.Text != "0" && txtCreamIssueforSale.Text != "0.00")
            {
                IssueFor_Sale = Convert.ToDecimal(txtCreamIssueforSale.Text);
            }
            else
            {
                IssueFor_Sale = 0;
            }
            if (txtCreamIssueforothers.Text != "0" && txtCreamIssueforothers.Text != "0.00")
            {
                IssueFor_Others = Convert.ToDecimal(txtCreamIssueforothers.Text);
            }
            else
            {
                IssueFor_Others = 0;
            }
            if (txtCLClosing.Text != "0" && txtCLClosing.Text != "0.00")
            {
                Closing = Convert.ToDecimal(txtCLClosing.Text);
            }
            else
            {
                Closing = 0;
            }



            if (txtCST1.Text != "0" && txtCST1.Text != "0.00")
            {
                CST1 = Convert.ToDecimal(txtCST1.Text);
            }
            else
            {
                CST1 = 0;
            }

            if (txtCST2.Text != "0" && txtCST2.Text != "0.00")
            {
                CST2 = Convert.ToDecimal(txtCST2.Text);
            }
            else
            {
                CST2 = 0;
            }

            if (txtSourQty.Text != "0" && txtSourQty.Text != "0.00")
            {
                SourQty = Convert.ToDecimal(txtSourQty.Text);
            }
            else
            {
                SourQty = 0;
            }

            OutTotal = IssueFor_WhiteButterGood + IssueFor_WhiteButterSour + Closing + CST1 + CST2 + SourQty + IssueFor_TableButter + IssueFor_CookingButter + ReturntoProcessingSection + IssueFor_Others + IssueFor_Sale;

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

            dr1["IssueFor_WhiteButterGood"] = IssueFor_WhiteButterGood;
            dr1["IssueFor_WhiteButterSour"] = IssueFor_WhiteButterSour;
            dr1["IssueFor_TableButter"] = IssueFor_TableButter;
            dr1["IssueFor_CookingButter"] = IssueFor_CookingButter;
            dr1["ReturntoProcessingSection"] = ReturntoProcessingSection;
            dr1["IssueFor_Sale"] = IssueFor_Sale;
            dr1["IssueFor_Others"] = IssueFor_Others;
            dr1["Closing"] = Closing;
            dr1["CST1"] = CST1;
            dr1["CST2"] = CST2;
            dr1["SourQty"] = SourQty;
            dr1["OutTotal"] = OutTotal;

        }
        return dt;
    }
    protected void btnYesT_P_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtIDF = new DataTable();
                dtIDF = GetProductVarientInfo();

                if (dtIDF.Rows.Count > 0)
                {
                      if(btnPopupSave_Product.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                                                  new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"TotalIn"
                                                ,"TotalOut"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id"
                                                ,"Received_From" 
                                    },
                                                  new string[] { "10"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseCreamItemTypeId_ID()
                                              ,txtReceived_From.Text
                                    },
                                                 new string[] { "type_Production_ProductSheetMasterChild_Cream" },
                                                 new DataTable[] { dtIDF }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSectionDetail();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                        }

                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);

                        }

                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());

                        }
                    }
                    else if(btnPopupSave_Product.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                                                  new string[] { "flag" 
                                                ,"PPSM"
												,"Received_From"
                                                
                                    },
                                                  new string[] { "20"
                                              ,ViewState["PPSM"].ToString()                                              
                                              ,txtReceived_From.Text
                                    },
                                                 new string[] { "type_Production_ProductSheetMasterChild_Cream" },
                                                 new DataTable[] { dtIDF }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSectionDetail();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                        }

                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);

                        }

                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());

                        }
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");

                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());


            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }

    }
    protected void GVVariantDetail_In_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "In Flow";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVVariantDetail_In.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void GVVariantDetail_Out_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Out Flow";
            HeaderCell.ColumnSpan = 13;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVVariantDetail_Out.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}