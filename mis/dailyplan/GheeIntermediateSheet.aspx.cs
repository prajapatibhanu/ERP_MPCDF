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


public partial class mis_dailyplan_GheeIntermediateSheet : System.Web.UI.Page
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
            btnGetTotal_Click(sender, e);
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

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }
            btnPopupSave_Product.Text = "Save";
            DataSet dsVD_Child = null;
            // For Variant
            //txtGainLossQtyInKg.Text = "";
            //txtGainLossFatInKg.Text = "";
            //txtGainLossSnfInKg.Text = "";
            //Label1.Text = "";
            //Label3.Text = "";
            //Label4.Text = "";
            //Label5.Text = "";
            //Label6.Text = "";
            //Label7.Text = "";
            dsVD_Child = objdb.ByProcedure("Sp_Production_GheeIntermediateSheetChild",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "2", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, objdb.LooseGheeItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables.Count > 0)
            {
                if(dsVD_Child.Tables[0].Rows.Count > 0)
                {
                    GVVariantDetail_In.DataSource = dsVD_Child;
                    GVVariantDetail_In.DataBind();
                    GVVariantDetail_Out.DataSource = dsVD_Child;
                    GVVariantDetail_Out.DataBind();
                    if (dsVD_Child.Tables[1].Rows[0]["Status"].ToString() == "Update")
                    {
                        btnPopupSave_Product.Text = "Update";
                    }
                    else
                    {
                        btnPopupSave_Product.Text = "Save";
                    }
                }
                
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
            decimal ButterRcvdfromButterSecQty= 0;
            decimal ButterRcvdfromButterSecFat = 0;
            decimal ButterRcvdfromButterSecSnf = 0;
            decimal GheeMfgQty = 0;
            decimal GheeMfgFat = 0;
            decimal GheeMfgSnf = 0;
            foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
            {
                TextBox lblPckt = (TextBox)row1.FindControl("lblPckt");
                TextBox lblLoose = (TextBox)row1.FindControl("lblLoose");
                TextBox lblButterRcvdfromButterSec = (TextBox)row1.FindControl("lblButterRcvdfromButterSec");
                TextBox lblSourMilkRcvd = (TextBox)row1.FindControl("lblSourMilkRcvd");
                TextBox lblCurdMilkRcvd = (TextBox)row1.FindControl("lblCurdMilkRcvd");
                TextBox lblCreamRcvd = (TextBox)row1.FindControl("lblCreamRcvd");
                TextBox txtInFlowOther = (TextBox)row1.FindControl("txtInFlowOther");
                TextBox lblGheeRetrunFromFP = (TextBox)row1.FindControl("lblGheeRetrunFromFP");
                Label lblItemName = (Label)row1.FindControl("lblItemName");
                if (lblPckt.Text == "")
                {
                    lblPckt.Text = "0";
                }
                if (lblLoose.Text == "")
                {
                    lblLoose.Text = "0";
                }
                if (lblButterRcvdfromButterSec.Text == "")
                {
                    
                    lblButterRcvdfromButterSec.Text = "0";
                }
                if (lblSourMilkRcvd.Text == "")
                {

                    lblSourMilkRcvd.Text = "0";
                }
                if (lblCurdMilkRcvd.Text == "")
                {

                    lblCurdMilkRcvd.Text = "0";
                }
                if (lblCreamRcvd.Text == "")
                {

                    lblCreamRcvd.Text = "0";
                }
                if (lblGheeRetrunFromFP.Text == "")
                {
                    lblGheeRetrunFromFP.Text = "0";
                }
                if (txtInFlowOther.Text == "")
                {
                    txtInFlowOther.Text = "0";
                }
                if (lblItemName.Text == " Quantity In Kg")
                {
                    ButterRcvdfromButterSecQty = decimal.Parse(lblButterRcvdfromButterSec.Text);
                }
                if (lblItemName.Text == " Fat In Kg")
                {
                    ButterRcvdfromButterSecFat = decimal.Parse(lblButterRcvdfromButterSec.Text);
                }
                if (lblItemName.Text == " Snf In Kg")
                {
                    ButterRcvdfromButterSecSnf = decimal.Parse(lblButterRcvdfromButterSec.Text);
                }
                
                

                TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

                lblIntotal.Text = (Convert.ToDecimal(lblPckt.Text) + Convert.ToDecimal(lblLoose.Text) + Convert.ToDecimal(lblButterRcvdfromButterSec.Text) + Convert.ToDecimal(lblSourMilkRcvd.Text) + Convert.ToDecimal(lblCurdMilkRcvd.Text) + Convert.ToDecimal(lblCreamRcvd.Text) + Convert.ToDecimal(lblGheeRetrunFromFP.Text) + Convert.ToDecimal(txtInFlowOther.Text)).ToString();
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
                TextBox txtGheeMfg = (TextBox)row1.FindControl("txtGheeMfg");
                TextBox txtGheeMfgfromSour = (TextBox)row1.FindControl("txtGheeMfgfromSour");
                TextBox txtGheeMfgfromCurdle = (TextBox)row1.FindControl("txtGheeMfgfromCurdle");
                TextBox txtGheeMfgfromCream = (TextBox)row1.FindControl("txtGheeMfgfromCream");
                TextBox txtOutFlowOther = (TextBox)row1.FindControl("txtOutFlowOther");
                TextBox txtGheeIssuetoFP = (TextBox)row1.FindControl("txtGheeIssuetoFP");
                TextBox txtGheeIssueForPacking = (TextBox)row1.FindControl("txtGheeIssueForPacking");
                TextBox txtSample = (TextBox)row1.FindControl("txtSample");
                TextBox txtClosingBalancePacket = (TextBox)row1.FindControl("txtClosingBalancePacket");
                TextBox txtClosingBalanceLoose = (TextBox)row1.FindControl("txtClosingBalanceLoose");

                if (txtOutFlowOther.Text == "")
                {
                    txtOutFlowOther.Text = "0";
                }
                if (txtGheeIssuetoFP.Text == "")
                {
                    txtGheeIssuetoFP.Text = "0";
                }
                if (txtClosingBalancePacket.Text == "")
                {
                    txtClosingBalancePacket.Text = "0";
                }
                if (txtClosingBalanceLoose.Text == "")
                {
                    txtClosingBalanceLoose.Text = "0";
                }
                if (txtGheeMfg.Text == "")
                {
                    txtGheeMfg.Text = "0";
                }
                if (txtGheeMfgfromSour.Text == "")
                {
                    txtGheeMfgfromSour.Text = "0";
                }
                if (txtGheeMfgfromCurdle.Text == "")
                {
                    txtGheeMfgfromCurdle.Text = "0";
                }
                if (txtGheeMfgfromCream.Text == "")
                {
                    txtGheeMfgfromCream.Text = "0";
                }
                if (txtGheeIssueForPacking.Text == "")
                {
                    txtGheeIssueForPacking.Text = "0";
                }
                if (txtSample.Text == "")
                {
                    txtSample.Text = "0";
                }
                TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

                lblouttotal.Text = (Convert.ToDecimal(txtOutFlowOther.Text) + Convert.ToDecimal(txtGheeIssuetoFP.Text) + Convert.ToDecimal(txtClosingBalancePacket.Text) + Convert.ToDecimal(txtClosingBalanceLoose.Text) + Convert.ToDecimal(txtSample.Text)).ToString();
                if (lblouttotal.Text == "")
                {
                    lblouttotal.Text = "0";
                }
                if (Label2.Text == " Quantity In Kg")
                {
                    TotalQtyOutFlow = decimal.Parse(lblouttotal.Text);
                    GheeMfgQty = decimal.Parse(txtGheeMfg.Text);
                }
                if (Label2.Text == " Fat In Kg")
                {
                    TotalFatOutFlow = decimal.Parse(lblouttotal.Text);
                    GheeMfgFat = decimal.Parse(txtGheeMfg.Text);
                }
                if (Label2.Text == " Snf In Kg")
                {
                    TotalSnfOutFlow = decimal.Parse(lblouttotal.Text);
                    GheeMfgSnf = decimal.Parse(txtGheeMfg.Text);
                }
                if (lblouttotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }
            decimal FatLoss = 0;
            txtGainLossQtyInKg.Text = (TotalQtyOutFlow - TotalQtyInflow).ToString();
            txtGainLossFatInKg.Text = (TotalFatOutFlow - TotalFatInflow).ToString();
            //txtGainLossSnfInKg.Text = (TotalSnfOutFlow - TotalSnfInflow).ToString();
            
            //Label1.Text = (Math.Round((decimal.Parse(txtGainLossQtyInKg.Text) / ButterRcvdfromButterSecQty) * 100,2)).ToString();
			FatLoss = Math.Round((decimal.Parse(txtGainLossFatInKg.Text) / ButterRcvdfromButterSecFat) * 100, 2);
            Label3.Text = (Math.Round((decimal.Parse(txtGainLossFatInKg.Text) / ButterRcvdfromButterSecFat) * 100, 2)).ToString();
            //Label4.Text = (Math.Round((decimal.Parse(txtGainLossSnfInKg.Text) / ButterRcvdfromButterSecSnf) * 100, 2)).ToString();

            //Label5.Text = (Math.Round((GheeMfgQty / ButterRcvdfromButterSecQty) * 100, 2)).ToString();
            Label6.Text = (Math.Round((GheeMfgFat / ButterRcvdfromButterSecFat) * 100, 2)).ToString();
            //Label7.Text = (Math.Round((GheeMfgSnf / ButterRcvdfromButterSecSnf) * 100, 2)).ToString();
           
            //decimal TotalGainLoss = Convert.ToDecimal(txtGainLossQtyInKg.Text) + Convert.ToDecimal(txtGainLossFatInKg.Text) + Convert.ToDecimal(txtGainLossSnfInKg.Text);
            //if (decimal.Parse(Label3.Text) <= 1.50M)
            //{
                
            //    lblGainLoss.Text = "";
            //    btnPopupSave_Product.Enabled = true;
            //}
            //else
            //{
            //    btnPopupSave_Product.Enabled = false;
            //    lblGainLoss.Text = "Gain/Loss should be allow to only 1.5%";
            //}
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

        decimal Pckt = 0;
        decimal Loose = 0;


        decimal ButterRcvdfromButterSec = 0;
        decimal SourMilkRcvd = 0;
        decimal CurdleMilkRcvd = 0;
        decimal CreamMilkRcvd = 0;
        decimal GheeRetrunFromFP = 0;
        
        decimal InFlowOther = 0;
        decimal InTotal = 0;
        decimal OutTotal = 0;
        decimal GheeMfgfromButter = 0;
        decimal GheeMfgfromSour = 0;
        decimal GheeMfgfromCurdle = 0;
        decimal GheeMfgfromCream = 0;
        decimal GheeIssuetoFP = 0;
        decimal OutFlowOther = 0;
        decimal LooseClosingBalance = 0;
        decimal PacketClosingBalance = 0;
        decimal GheeIssueForPacking = 0;
        decimal Sample = 0;
       

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Pckt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Loose", typeof(decimal)));

        dt.Columns.Add(new DataColumn("ButterRcvdfromButterSec", typeof(decimal)));
        dt.Columns.Add(new DataColumn("SourMilkRcvd", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CurdleMilkRcvd", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamMilkRcvd", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeRetrunFromFP", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InFlowOther", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromButter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromSour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromCurdle", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromCream", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeIssuetoFP", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutFlowOther", typeof(decimal)));
        dt.Columns.Add(new DataColumn("LooseClosingBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("PacketClosingBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeIssueForPacking", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Sample", typeof(decimal)));
       

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");

            TextBox lblPckt = (TextBox)row.FindControl("lblPckt");
            TextBox lblLoose = (TextBox)row.FindControl("lblLoose");
            TextBox lblButterRcvdfromButterSec = (TextBox)row.FindControl("lblButterRcvdfromButterSec");
            TextBox lblSourMilkRcvd = (TextBox)row.FindControl("lblSourMilkRcvd");
            TextBox lblCurdMilkRcvd = (TextBox)row.FindControl("lblCurdMilkRcvd");
            TextBox lblCreamRcvd = (TextBox)row.FindControl("lblCreamRcvd");


            TextBox lblGheeRetrunFromFP = (TextBox)row.FindControl("lblGheeRetrunFromFP");
            TextBox txtInFlowOther = (TextBox)row.FindControl("txtInFlowOther");
            TextBox lblIntotal = (TextBox)row.FindControl("lblIntotal");

            if (lblPckt.Text != "" && lblPckt.Text != "0" && lblPckt.Text != "0.00")
            {
                Pckt = Convert.ToDecimal(lblPckt.Text);
            }
            else
            {
                Pckt = 0;
            }


            if (lblLoose.Text != "" && lblLoose.Text != "0" && lblLoose.Text != "0.00")
            {
                Loose = Convert.ToDecimal(lblLoose.Text);
            }
            else
            {
                Loose = 0;
            }

            if (lblGheeRetrunFromFP.Text != "" && lblGheeRetrunFromFP.Text != "0" && lblGheeRetrunFromFP.Text != "0.00")
            {
                GheeRetrunFromFP = Convert.ToDecimal(lblGheeRetrunFromFP.Text);
            }
            else
            {
                GheeRetrunFromFP = 0;
            }
            if (lblButterRcvdfromButterSec.Text != "" && lblButterRcvdfromButterSec.Text != "0" && lblButterRcvdfromButterSec.Text != "0.00")
            {
                ButterRcvdfromButterSec = Convert.ToDecimal(lblButterRcvdfromButterSec.Text);
            }
            else
            {
                ButterRcvdfromButterSec = 0;
            }
            if (lblSourMilkRcvd.Text != "" && lblSourMilkRcvd.Text != "0" && lblSourMilkRcvd.Text != "0.00")
            {
                SourMilkRcvd = Convert.ToDecimal(lblSourMilkRcvd.Text);
            }
            else
            {
                SourMilkRcvd = 0;
            }
            if (lblCurdMilkRcvd.Text != "" && lblCurdMilkRcvd.Text != "0" && lblCurdMilkRcvd.Text != "0.00")
            {
                CurdleMilkRcvd = Convert.ToDecimal(lblCurdMilkRcvd.Text);
            }
            else
            {
                CurdleMilkRcvd = 0;
            }
            if (lblCreamRcvd.Text != "" && lblCreamRcvd.Text != "0" && lblCreamRcvd.Text != "0.00")
            {
                CreamMilkRcvd = Convert.ToDecimal(lblCreamRcvd.Text);
            }
            else
            {
                CreamMilkRcvd = 0;
            }
            if (txtInFlowOther.Text != "" && txtInFlowOther.Text != "0" && txtInFlowOther.Text != "0.00")
            {
                InFlowOther = Convert.ToDecimal(txtInFlowOther.Text);
            }
            else
            {
                InFlowOther = 0;
            }

            InTotal = Pckt + Loose + ButterRcvdfromButterSec + GheeRetrunFromFP + SourMilkRcvd + CurdleMilkRcvd + CreamMilkRcvd;

            dr = dt.NewRow();
            dr[0] = objdb.LooseGheeItemCategoryId_ID();
            dr[1] = objdb.LooseGheeItemTypeId_ID();
            dr[2] = lblItem_id.Text;
            dr[3] = Pckt;
            dr[4] = Loose;
            dr[5] = ButterRcvdfromButterSec;
            dr[6] = SourMilkRcvd;
            dr[7] = CurdleMilkRcvd;
            dr[8] = CreamMilkRcvd;
            dr[9] = GheeRetrunFromFP;
            dr[10] = InFlowOther;
            dr[11] = InTotal;
            dr[12] = OutTotal;
            dr[13] = GheeMfgfromButter;
            dr[14] = GheeMfgfromSour;
            dr[15] = GheeMfgfromCurdle;
            dr[16] = GheeMfgfromCream;
            dr[17] = GheeIssuetoFP;
            dr[18] = OutFlowOther;
            dr[19] = LooseClosingBalance;
            dr[20] = PacketClosingBalance;
            dr[21] = GheeIssueForPacking;
            dr[22] = Sample;
            dt.Rows.Add(dr);


        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
            TextBox txtGheeMfg = (TextBox)row1.FindControl("txtGheeMfg");
            TextBox txtGheeMfgfromSour = (TextBox)row1.FindControl("txtGheeMfgfromSour");
            TextBox txtGheeMfgfromCurdle = (TextBox)row1.FindControl("txtGheeMfgfromCurdle");
            TextBox txtGheeMfgfromCream = (TextBox)row1.FindControl("txtGheeMfgfromCream");
            TextBox txtGheeIssuetoFP = (TextBox)row1.FindControl("txtGheeIssuetoFP");
            TextBox txtOutFlowOther = (TextBox)row1.FindControl("txtOutFlowOther");
            TextBox txtClosingBalanceLoose = (TextBox)row1.FindControl("txtClosingBalanceLoose");
            TextBox txtClosingBalancePacket = (TextBox)row1.FindControl("txtClosingBalancePacket");
            TextBox txtGheeIssueForPacking = (TextBox)row1.FindControl("txtGheeIssueForPacking");
            TextBox txtSample = (TextBox)row1.FindControl("txtSample");
            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");
           

            if (txtGheeMfg.Text != "0" && txtGheeMfg.Text != "0.00")
            {
                GheeMfgfromButter = Convert.ToDecimal(txtGheeMfg.Text);
            }
            else
            {
                GheeMfgfromButter = 0;
            }
            if (txtGheeMfgfromSour.Text != "0" && txtGheeMfgfromSour.Text != "0.00")
            {
                GheeMfgfromSour = Convert.ToDecimal(txtGheeMfgfromSour.Text);
            }
            else
            {
                GheeMfgfromSour = 0;
            }
            if (txtGheeMfgfromCurdle.Text != "0" && txtGheeMfgfromCurdle.Text != "0.00")
            {
                GheeMfgfromCurdle = Convert.ToDecimal(txtGheeMfgfromCurdle.Text);
            }
            else
            {
                GheeMfgfromCurdle = 0;
            }
            if (txtGheeMfgfromCream.Text != "0" && txtGheeMfgfromCream.Text != "0.00")
            {
                GheeMfgfromCream = Convert.ToDecimal(txtGheeMfgfromCream.Text);
            }
            else
            {
                GheeMfgfromCream = 0;
            }
            if (txtGheeIssuetoFP.Text != "0" && txtGheeIssuetoFP.Text != "0.00")
            {
                GheeIssuetoFP = Convert.ToDecimal(txtGheeIssuetoFP.Text);
            }
            else
            {
                GheeIssuetoFP = 0;
            }
            if (txtOutFlowOther.Text != "0" && txtOutFlowOther.Text != "0.00")
            {
                OutFlowOther = Convert.ToDecimal(txtOutFlowOther.Text);
            }
            else
            {
                OutFlowOther = 0;
            }

            if (txtClosingBalanceLoose.Text != "0" && txtClosingBalanceLoose.Text != "0.00")
            {
                LooseClosingBalance = Convert.ToDecimal(txtClosingBalanceLoose.Text);
            }
            else
            {
                LooseClosingBalance = 0;
            }
            if (txtClosingBalancePacket.Text != "0" && txtClosingBalancePacket.Text != "0.00")
            {
                PacketClosingBalance = Convert.ToDecimal(txtClosingBalancePacket.Text);
            }
            else
            {
                PacketClosingBalance = 0;
            }
            if (txtGheeIssueForPacking.Text != "0" && txtGheeIssueForPacking.Text != "0.00")
            {
                GheeIssueForPacking = Convert.ToDecimal(txtGheeIssueForPacking.Text);
            }
            else
            {
                GheeIssueForPacking = 0;
            }
            if (txtSample.Text != "0" && txtSample.Text != "0.00")
            {
                Sample = Convert.ToDecimal(txtSample.Text);
            }
            else
            {
                Sample = 0;
            }

            OutTotal = GheeIssuetoFP + OutFlowOther + LooseClosingBalance + LooseClosingBalance + Sample;

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

            dr1["GheeMfgfromButter"] = GheeMfgfromButter;
            dr1["GheeMfgfromSour"] = GheeMfgfromSour;
            dr1["GheeMfgfromCurdle"] = GheeMfgfromCurdle;
            dr1["GheeMfgfromCream"] = GheeMfgfromCream;
            dr1["GheeIssuetoFP"] = GheeIssuetoFP;
            dr1["OutFlowOther"] = OutFlowOther;
            dr1["LooseClosingBalance"] = LooseClosingBalance;
            dr1["PacketClosingBalance"] = PacketClosingBalance;
            dr1["GheeIssueForPacking"] = GheeIssueForPacking;
            dr1["Sample"] = Sample;
            dr1["OutTotal"] = OutTotal;
          

        }
        return dt;
    }
    private DataTable GetUpdateProductVarientInfo()
    {

        decimal Pckt = 0;
        decimal Loose = 0;


        decimal ButterRcvdfromButterSec = 0;
        decimal SourMilkRcvd = 0;
        decimal CurdleMilkRcvd = 0;
        decimal CreamMilkRcvd = 0;
        decimal GheeRetrunFromFP = 0;

        decimal InFlowOther = 0;
        decimal InTotal = 0;
        decimal OutTotal = 0;
        decimal GheeMfgfromButter = 0;
        decimal GheeMfgfromSour = 0;
        decimal GheeMfgfromCurdle = 0;
        decimal GheeMfgfromCream = 0;
        decimal GheeIssuetoFP = 0;
        decimal OutFlowOther = 0;
        decimal LooseClosingBalance = 0;
        decimal PacketClosingBalance = 0;
        decimal GheeIssueForPacking = 0;

        decimal Sample = 0;
        DataTable dt = new DataTable();
        DataRow dr;


        dt.Columns.Add(new DataColumn("GIS_Child", typeof(int)));
      

        dt.Columns.Add(new DataColumn("Pckt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Loose", typeof(decimal)));

        dt.Columns.Add(new DataColumn("ButterRcvdfromButterSec", typeof(decimal)));
        dt.Columns.Add(new DataColumn("SourMilkRcvd", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CurdleMilkRcvd", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamMilkRcvd", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeRetrunFromFP", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InFlowOther", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromButter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromSour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromCurdle", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeMfgfromCream", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeIssuetoFP", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutFlowOther", typeof(decimal)));
        dt.Columns.Add(new DataColumn("LooseClosingBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("PacketClosingBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheeIssueForPacking", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Sample", typeof(decimal)));


        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");
            Label lblInflowGIS_Child = (Label)row.FindControl("lblInflowGIS_Child");
            TextBox lblPckt = (TextBox)row.FindControl("lblPckt");
            TextBox lblLoose = (TextBox)row.FindControl("lblLoose");
            TextBox lblButterRcvdfromButterSec = (TextBox)row.FindControl("lblButterRcvdfromButterSec");
            TextBox lblSourMilkRcvd = (TextBox)row.FindControl("lblSourMilkRcvd");
            TextBox lblCurdMilkRcvd = (TextBox)row.FindControl("lblCurdMilkRcvd");
            TextBox lblCreamRcvd = (TextBox)row.FindControl("lblCreamRcvd");
            TextBox lblGheeRetrunFromFP = (TextBox)row.FindControl("lblGheeRetrunFromFP");
            TextBox txtInFlowOther = (TextBox)row.FindControl("txtInFlowOther");
            TextBox lblIntotal = (TextBox)row.FindControl("lblIntotal");

            if (lblPckt.Text != "" && lblPckt.Text != "0" && lblPckt.Text != "0.00")
            {
                Pckt = Convert.ToDecimal(lblPckt.Text);
            }
            else
            {
                Pckt = 0;
            }


            if (lblLoose.Text != "" && lblLoose.Text != "0" && lblLoose.Text != "0.00")
            {
                Loose = Convert.ToDecimal(lblLoose.Text);
            }
            else
            {
                Loose = 0;
            }

            if (lblGheeRetrunFromFP.Text != "" && lblGheeRetrunFromFP.Text != "0" && lblGheeRetrunFromFP.Text != "0.00")
            {
                GheeRetrunFromFP = Convert.ToDecimal(lblGheeRetrunFromFP.Text);
            }
            else
            {
                GheeRetrunFromFP = 0;
            }
            if (lblButterRcvdfromButterSec.Text != "" && lblButterRcvdfromButterSec.Text != "0" && lblButterRcvdfromButterSec.Text != "0.00")
            {
                ButterRcvdfromButterSec = Convert.ToDecimal(lblButterRcvdfromButterSec.Text);
            }
            else
            {
                ButterRcvdfromButterSec = 0;
            }
            if (lblSourMilkRcvd.Text != "" && lblSourMilkRcvd.Text != "0" && lblSourMilkRcvd.Text != "0.00")
            {
                SourMilkRcvd = Convert.ToDecimal(lblSourMilkRcvd.Text);
            }
            else
            {
                SourMilkRcvd = 0;
            }
            if (lblCurdMilkRcvd.Text != "" && lblCurdMilkRcvd.Text != "0" && lblCurdMilkRcvd.Text != "0.00")
            {
                CurdleMilkRcvd = Convert.ToDecimal(lblCurdMilkRcvd.Text);
            }
            else
            {
                CurdleMilkRcvd = 0;
            }
            if (lblCreamRcvd.Text != "" && lblCreamRcvd.Text != "0" && lblCreamRcvd.Text != "0.00")
            {
                CreamMilkRcvd = Convert.ToDecimal(lblCreamRcvd.Text);
            }
            else
            {
                CreamMilkRcvd = 0;
            }
            if (txtInFlowOther.Text != "" && txtInFlowOther.Text != "0" && txtInFlowOther.Text != "0.00")
            {
                InFlowOther = Convert.ToDecimal(txtInFlowOther.Text);
            }
            else
            {
                InFlowOther = 0;
            }

            InTotal = Pckt + Loose + ButterRcvdfromButterSec + GheeRetrunFromFP + InFlowOther+ SourMilkRcvd + CurdleMilkRcvd + CreamMilkRcvd;

            dr = dt.NewRow();
            
            dr[0] = lblInflowGIS_Child.Text;
            
            dr[1] = Pckt;
            dr[2] = Loose;
            dr[3] = ButterRcvdfromButterSec;
            dr[4] = SourMilkRcvd;
            dr[5] = CurdleMilkRcvd;
            dr[6] = CreamMilkRcvd;
            dr[7] = GheeRetrunFromFP;
            dr[8] = InFlowOther;
            dr[9] = InTotal;
            dr[10] = OutTotal;
            dr[11] = GheeMfgfromButter;
            dr[12] = GheeMfgfromSour;
            dr[13] = GheeMfgfromCurdle;
            dr[14] = GheeMfgfromCream;
            dr[15] = GheeIssuetoFP;
            dr[16] = OutFlowOther;
            dr[17] = LooseClosingBalance;
            dr[18] = PacketClosingBalance;
            dr[19] = GheeIssueForPacking;
            dr[20] = Sample;
            dt.Rows.Add(dr);


        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblOutFlowflowGIS_Child = (Label)row1.FindControl("lblOutFlowflowGIS_Child");
            Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
            TextBox txtGheeMfg = (TextBox)row1.FindControl("txtGheeMfg");
            TextBox txtGheeMfgfromSour = (TextBox)row1.FindControl("txtGheeMfgfromSour");
            TextBox txtGheeMfgfromCurdle = (TextBox)row1.FindControl("txtGheeMfgfromCurdle");
            TextBox txtGheeMfgfromCream = (TextBox)row1.FindControl("txtGheeMfgfromCream");
            TextBox txtGheeIssuetoFP = (TextBox)row1.FindControl("txtGheeIssuetoFP");
            TextBox txtOutFlowOther = (TextBox)row1.FindControl("txtOutFlowOther");
            TextBox txtClosingBalancePacket = (TextBox)row1.FindControl("txtClosingBalancePacket");
            TextBox txtClosingBalanceLoose = (TextBox)row1.FindControl("txtClosingBalanceLoose");
            TextBox txtGheeIssueForPacking = (TextBox)row1.FindControl("txtGheeIssueForPacking");
            TextBox txtSample = (TextBox)row1.FindControl("txtSample");
            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");


            if (txtGheeMfg.Text != "0" && txtGheeMfg.Text != "0.00")
            {
                GheeMfgfromButter = Convert.ToDecimal(txtGheeMfg.Text);
            }
            else
            {
                GheeMfgfromButter = 0;
            }
            if (txtGheeMfgfromSour.Text != "0" && txtGheeMfgfromSour.Text != "0.00")
            {
                GheeMfgfromSour = Convert.ToDecimal(txtGheeMfgfromSour.Text);
            }
            else
            {
                GheeMfgfromSour = 0;
            }
            if (txtGheeMfgfromCurdle.Text != "0" && txtGheeMfgfromCurdle.Text != "0.00")
            {
                GheeMfgfromCurdle = Convert.ToDecimal(txtGheeMfgfromCurdle.Text);
            }
            else
            {
                GheeMfgfromCurdle = 0;
            }
            if (txtGheeMfgfromCream.Text != "0" && txtGheeMfgfromCream.Text != "0.00")
            {
                GheeMfgfromCream = Convert.ToDecimal(txtGheeMfgfromCream.Text);
            }
            else
            {
                GheeMfgfromCream = 0;
            }

            if (txtGheeIssuetoFP.Text != "0" && txtGheeIssuetoFP.Text != "0.00")
            {
                GheeIssuetoFP = Convert.ToDecimal(txtGheeIssuetoFP.Text);
            }
            else
            {
                GheeIssuetoFP = 0;
            }
            if (txtOutFlowOther.Text != "0" && txtOutFlowOther.Text != "0.00")
            {
                OutFlowOther = Convert.ToDecimal(txtOutFlowOther.Text);
            }
            else
            {
                OutFlowOther = 0;
            }

            if (txtClosingBalanceLoose.Text != "0" && txtClosingBalanceLoose.Text != "0.00")
            {
                LooseClosingBalance = Convert.ToDecimal(txtClosingBalanceLoose.Text);
            }
            else
            {
                LooseClosingBalance = 0;
            }
            if (txtClosingBalancePacket.Text != "0" && txtClosingBalancePacket.Text != "0.00")
            {
                PacketClosingBalance = Convert.ToDecimal(txtClosingBalancePacket.Text);
            }
            else
            {
                PacketClosingBalance = 0;
            }
            if (txtGheeIssueForPacking.Text != "0" && txtGheeIssueForPacking.Text != "0.00")
            {
                GheeIssueForPacking = Convert.ToDecimal(txtGheeIssueForPacking.Text);
            }
            else
            {
                GheeIssueForPacking = 0;
            }
            if (txtSample.Text != "0" && txtSample.Text != "0.00")
            {
                Sample = Convert.ToDecimal(txtSample.Text);
            }
            else
            {
                Sample = 0;
            }


            OutTotal = GheeIssueForPacking + OutFlowOther + LooseClosingBalance + PacketClosingBalance + Sample;

            Int32 IID = Convert.ToInt32(lblOutFlowflowGIS_Child.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["GIS_Child"]).Equals(IID)).First();

            dr1["GheeMfgfromButter"] = GheeMfgfromButter;
            dr1["GheeMfgfromSour"] = GheeMfgfromSour;
            dr1["GheeMfgfromCurdle"] = GheeMfgfromCurdle;
            dr1["GheeMfgfromCream"] = GheeMfgfromCream;
            dr1["GheeIssuetoFP"] = GheeIssuetoFP;
            dr1["OutFlowOther"] = OutFlowOther;
            dr1["LooseClosingBalance"] = LooseClosingBalance;
            dr1["PacketClosingBalance"] = PacketClosingBalance;
            dr1["GheeIssueForPacking"] = GheeIssueForPacking;          
            dr1["OutTotal"] = OutTotal;
            dr1["Sample"] = Sample;


        }
        return dt;
    }
    protected void btnYesT_P_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                btnGetTotal_Click(sender, e);
                
                if(btnPopupSave_Product.Text == "Save")
                {
                    DataTable dtIDF = new DataTable();
                    dtIDF = GetProductVarientInfo();
                    if (dtIDF.Rows.Count > 0)
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Sp_Production_GheeIntermediateSheetChild",
                                                  new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"TotalIn"
                                                ,"TotalOut"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id"
                                                
                                    },
                                                  new string[] { "1"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseGheeItemTypeId_ID()
                                              
                                    },
                                                 new string[] { "type_Production_GheeIntermediateSheet" },
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
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");

                    }
                }
                else
                {
                    DataTable dtUpdateIDF = new DataTable();
                    dtUpdateIDF = GetUpdateProductVarientInfo();
                    if (dtUpdateIDF.Rows.Count > 0)
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Sp_Production_GheeIntermediateSheetChild",
                                                  new string[] { "flag" 
                                               
                                                
                                    },
                                                  new string[] { "5"
                                              
                                              
                                    },
                                                 new string[] { "type_Production_UpdateGheeIntermediateSheet" },
                                                 new DataTable[] { dtUpdateIDF }, "TableSave");


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
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");

                    }
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
            HeaderCell.ColumnSpan = 12;
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
            HeaderCell.ColumnSpan = 16;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVVariantDetail_Out.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}