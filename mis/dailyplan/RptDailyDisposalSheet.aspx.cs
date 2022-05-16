using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


public partial class mis_dailyplan_RptDailyDisposalSheet : System.Web.UI.Page
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
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    GetSectionView(sender, e);
                    FillShift();
					spnofcname.InnerHtml = Session["Office_Name"].ToString();

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
                //ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;
                ddlPSection_SelectedIndexChanged(sender, e);

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
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

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPSection.SelectedValue != "0")
        //{
        //    divfinal.Visible = true;
        //    GetSectionDetail();
        //    ViewFinalDisposalSheet();
        //}
        //else
        //{
        //    gvDDSheet.DataSource = string.Empty;
        //    gvDDSheet.DataBind();
        //    divfinal.Visible = false;
        //}
    }

    private void ViewFinalDisposalSheet()
    {

        try
        {
            decimal FinalReceiptTotalQty = 0;
            decimal FinalIssuedTotalQty = 0;

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID" },
                new string[] { "16", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lblReceiptqty.Text = ds.Tables[0].Rows[0]["ReceiptMilkQty"].ToString();

            }
            else
            {

                lblReceiptqty.Text = "0";
            }

            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                lblReceiptOpeningBalance.Text = ds.Tables[3].Rows[0]["WH_IssuedClosingBalance"].ToString();
                lblReceiptqty.Text = ds.Tables[3].Rows[0]["WM_ReceiptMilk"].ToString();
                lblReceiptgood.Text = ds.Tables[3].Rows[0]["WM_ReceiptGood"].ToString();
                lblReceiptSour.Text = ds.Tables[3].Rows[0]["WM_ReceiptSour"].ToString();
                lblReceiptCurdled.Text = ds.Tables[3].Rows[0]["WH_ReceiptCurdled"].ToString();
                lblReceiptCR.Text = ds.Tables[3].Rows[0]["WH_ReceiptCR"].ToString();
                lblReceiptWB.Text = ds.Tables[3].Rows[0]["WH_ReceiptWB"].ToString();
                lblReceiptFlushing.Text = ds.Tables[3].Rows[0]["WH_ReceiptFlushing"].ToString();

                lblIssuedsaletoproduct.Text = ds.Tables[3].Rows[0]["WM_IssuedForSaleToProduct"].ToString();
                lblIssuedGood.Text = ds.Tables[3].Rows[0]["WM_IssuedGood"].ToString();
                lblIssuedSour.Text = ds.Tables[3].Rows[0]["WM_IssuedSour"].ToString();
                lblIssuedCurdled.Text = ds.Tables[3].Rows[0]["WH_IssuedCurdled"].ToString();
                lblIssuedToProductSection.Text = ds.Tables[3].Rows[0]["WH_IssuedToProductSection"].ToString();
                lblIssuedLosses.Text = ds.Tables[3].Rows[0]["WH_IssuedLosses"].ToString();
                lblIssuedClosingBalance.Text = ds.Tables[3].Rows[0]["WH_IssuedClosingBalance"].ToString();


            }

            else
            {

                if (lblReceiptOpeningBalance.Text == "")
                {
                    lblReceiptOpeningBalance.Text = "0";
                }
                if (lblIssuedsaletoproduct.Text == "")
                {
                    lblIssuedsaletoproduct.Text = "0";
                }
                if (lblReceiptqty.Text == "")
                {
                    lblReceiptqty.Text = "0";
                }
                if (lblReceiptgood.Text == "")
                {
                    lblReceiptgood.Text = "0";
                }
                if (lblIssuedGood.Text == "")
                {
                    lblIssuedGood.Text = "0";
                }
                if (lblReceiptSour.Text == "")
                {
                    lblReceiptSour.Text = "0";
                }
                if (lblIssuedSour.Text == "")
                {
                    lblIssuedSour.Text = "0";
                }
                if (lblReceiptCurdled.Text == "")
                {
                    lblReceiptCurdled.Text = "0";
                }
                if (lblIssuedCurdled.Text == "")
                {
                    lblIssuedCurdled.Text = "0";
                }
                if (lblReceiptCR.Text == "")
                {
                    lblReceiptCR.Text = "0";
                }
                if (lblIssuedToProductSection.Text == "")
                {
                    lblIssuedToProductSection.Text = "0";
                }
                if (lblReceiptWB.Text == "")
                {
                    lblReceiptWB.Text = "0";
                }
                if (lblIssuedLosses.Text == "")
                {
                    lblIssuedLosses.Text = "0";
                }
                if (lblReceiptFlushing.Text == "")
                {
                    lblReceiptFlushing.Text = "0";
                }
                if (lblIssuedClosingBalance.Text == "")
                {
                    lblIssuedClosingBalance.Text = "0";
                }
            }


            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {

                GVIssueMilkFromVarient.DataSource = ds.Tables[2];
                GVIssueMilkFromVarient.DataBind();

                foreach (GridViewRow row in GVIssueMilkFromVarient.Rows)
                {
                    // Milk QTY  
                    Label txtQtyInPkt = (Label)row.FindControl("txtQtyInPkt");
                    if (txtQtyInPkt.Text != "")
                    {
                        FinalReceiptTotalQty += Convert.ToDecimal(txtQtyInPkt.Text);
                    }

                }

            }
            else
            {

                GVIssueMilkFromVarient.DataSource = string.Empty;
                GVIssueMilkFromVarient.DataBind();
            }

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {

                GVIssuedFromWholeMilk.DataSource = ds.Tables[1];
                GVIssuedFromWholeMilk.DataBind();

                foreach (GridViewRow row in GVIssuedFromWholeMilk.Rows)
                {
                    // Milk QTY  
                    Label txtQtyInPkt = (Label)row.FindControl("txtQtyInPkt");
                    if (txtQtyInPkt.Text != "")
                    {
                        FinalIssuedTotalQty += Convert.ToDecimal(txtQtyInPkt.Text);
                    }

                }


            }
            else
            {

                GVIssuedFromWholeMilk.DataSource = string.Empty;
                GVIssuedFromWholeMilk.DataBind();
            }


            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                lblReceiptOpeningBalance.Text = ds.Tables[4].Rows[0]["OpeningMilkQtyHW_Prev"].ToString();
            }


            if (ds != null && ds.Tables[5].Rows.Count > 0)
            {
                lblIssuedGood.Text = ds.Tables[5].Rows[0]["WM_GoodMilk"].ToString();
                lblIssuedGood.Enabled = false;
            }


            if (ds != null && ds.Tables[6].Rows.Count > 0)
            {
                GV_CD_WM.DataSource = ds.Tables[6];
                GV_CD_WM.DataBind();
            }
            else
            {
                GV_CD_WM.DataSource = string.Empty;
                GV_CD_WM.DataBind();
            }

            if (ds != null && ds.Tables[7].Rows.Count > 0)
            {
                GV_TDetail_WM_Receipt.DataSource = ds.Tables[7];
                GV_TDetail_WM_Receipt.DataBind();

            }
            else
            {
                GV_TDetail_WM_Receipt.DataSource = string.Empty;
                GV_TDetail_WM_Receipt.DataBind();

            }


            if (ds != null && ds.Tables[8].Rows.Count > 0)
            {

                GV_TDetail_WM_Issued.DataSource = ds.Tables[8];
                GV_TDetail_WM_Issued.DataBind();

            }
            else
            {

                GV_TDetail_WM_Issued.DataSource = string.Empty;
                GV_TDetail_WM_Issued.DataBind();
            }



            if (lblReceiptOpeningBalance.Text == "")
            {
                lblReceiptOpeningBalance.Text = "0";
            }
            if (lblIssuedsaletoproduct.Text == "")
            {
                lblIssuedsaletoproduct.Text = "0";
            }
            if (lblReceiptqty.Text == "")
            {
                lblReceiptqty.Text = "0";
            }
            if (lblReceiptgood.Text == "")
            {
                lblReceiptgood.Text = "0";
            }
            if (lblIssuedGood.Text == "")
            {
                lblIssuedGood.Text = "0";
            }
            if (lblReceiptSour.Text == "")
            {
                lblReceiptSour.Text = "0";
            }
            if (lblIssuedSour.Text == "")
            {
                lblIssuedSour.Text = "0";
            }
            if (lblReceiptCurdled.Text == "")
            {
                lblReceiptCurdled.Text = "0";
            }
            if (lblIssuedCurdled.Text == "")
            {
                lblIssuedCurdled.Text = "0";
            }
            if (lblReceiptCR.Text == "")
            {
                lblReceiptCR.Text = "0";
            }
            if (lblIssuedToProductSection.Text == "")
            {
                lblIssuedToProductSection.Text = "0";
            }
            if (lblReceiptWB.Text == "")
            {
                lblReceiptWB.Text = "0";
            }
            if (lblIssuedLosses.Text == "")
            {
                lblIssuedLosses.Text = "0";
            }
            if (lblReceiptFlushing.Text == "")
            {
                lblReceiptFlushing.Text = "0";
            }
            if (lblIssuedClosingBalance.Text == "")
            {
                lblIssuedClosingBalance.Text = "0";
            }

            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {

            }
            else
            {
                GV_TDetail_WM_Receipt.DataSource = string.Empty;
                GV_TDetail_WM_Receipt.DataBind();

                GV_TDetail_WM_Issued.DataSource = string.Empty;
                GV_TDetail_WM_Issued.DataBind();

                GV_CD_WM.DataSource = string.Empty;
                GV_CD_WM.DataBind();

            }


            //lblReceiptFinalTotal.Text = (Convert.ToDecimal(lblReceiptOpeningBalance.Text) +
            //    Convert.ToDecimal(lblReceiptqty.Text) + FinalReceiptTotalQty
            //    + Convert.ToDecimal(lblReceiptgood.Text) + Convert.ToDecimal(lblReceiptSour.Text) +
            //    Convert.ToDecimal(lblReceiptCurdled.Text) + Convert.ToDecimal(lblReceiptCR.Text) +
            //    Convert.ToDecimal(lblReceiptWB.Text) + Convert.ToDecimal(lblReceiptFlushing.Text)).ToString();

            //lblIssuedFinalTotal.Text = (Convert.ToDecimal(lblIssuedsaletoproduct.Text) + FinalIssuedTotalQty
            //   + Convert.ToDecimal(lblIssuedGood.Text) + Convert.ToDecimal(lblIssuedSour.Text) +
            //   Convert.ToDecimal(lblIssuedCurdled.Text) + Convert.ToDecimal(lblIssuedToProductSection.Text) +
            //   Convert.ToDecimal(lblIssuedLosses.Text) + Convert.ToDecimal(lblIssuedClosingBalance.Text)).ToString();

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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

            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID" },
                new string[] { "14", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvDDSheet.DataSource = ds;
                gvDDSheet.DataBind();


                foreach (GridViewRow row in gvDDSheet.Rows)
                {

                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

                    GridView GVVariantDetail = (GridView)row.FindControl("GVVariantDetail");

                    DataSet dsVD_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                    new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                    new string[] { "11", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, lblItemType_id.Text }, "dataset");

                    if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
                    {
                        GVVariantDetail.DataSource = dsVD_Child;
                        GVVariantDetail.DataBind();
                    }

                    else
                    {
                        GVVariantDetail.DataSource = string.Empty;
                        GVVariantDetail.DataBind();
                    }

                    //decimal VariantMCQ = 0;
                    //decimal VariantMCQ_Total = 0;

                    //foreach (GridViewRow row11 in GVVariantDetail.Rows)
                    //{
                    //    Label lblPackagingSize = (Label)row11.FindControl("lblPackagingSize");
                    //    Label lblUnit_id = (Label)row11.FindControl("lblUnit_id");
                    //    Label txtQtyInPkt = (Label)row11.FindControl("txtQtyInPkt");
                    //    Label lblQtyInLtr = (Label)row11.FindControl("lblQtyInLtr");

                    //    if (txtQtyInPkt.Text != "0" && txtQtyInPkt.Text != "0.00" && txtQtyInPkt.Text != "0.0" && txtQtyInPkt.Text != "")
                    //    {
                    //        if (lblPackagingSize.Text != "0" && lblPackagingSize.Text != "0.00" && lblPackagingSize.Text != "0.0" && lblPackagingSize.Text != "")
                    //        {
                    //            if (lblUnit_id.Text != "")
                    //            {
                    //                int Unit_id = Convert.ToInt32(lblUnit_id.Text);

                    //                if (Unit_id == 20)
                    //                {
                    //                    VariantMCQ = (Convert.ToDecimal(txtQtyInPkt.Text) * Convert.ToDecimal(lblPackagingSize.Text)) / 1000;
                    //                }

                    //                if (Unit_id == 6)
                    //                {
                    //                    VariantMCQ = (Convert.ToDecimal(txtQtyInPkt.Text) * Convert.ToDecimal(lblPackagingSize.Text));
                    //                }

                    //                lblQtyInLtr.Text = VariantMCQ.ToString("0.0");


                    //            }
                    //        }

                    //    }

                    //    VariantMCQ_Total += VariantMCQ;

                    //}


                    GridView gvMilkinContainer = (GridView)row.FindControl("gvMilkinContainer");
                    // For Variant 
                    if (dsVD_Child != null && dsVD_Child.Tables[1].Rows.Count > 0)
                    {
                        gvMilkinContainer.DataSource = dsVD_Child.Tables[1];
                        gvMilkinContainer.DataBind();
                    }
                    else
                    {
                        gvMilkinContainer.DataSource = string.Empty;
                        gvMilkinContainer.DataBind();
                    }


                    GridView gvMilkinContainer_opening = (GridView)row.FindControl("gvMilkinContainer_opening");
                    DataSet dsVDUpdated = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                      new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                      new string[] { "13", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, lblItemType_id.Text }, "dataset");

                    if (dsVDUpdated != null && dsVDUpdated.Tables[2].Rows.Count > 0)
                    {
                        gvMilkinContainer_opening.DataSource = dsVDUpdated.Tables[2];
                        gvMilkinContainer_opening.DataBind();
                    }
                    else
                    {
                        gvMilkinContainer_opening.DataSource = string.Empty;
                        gvMilkinContainer_opening.DataBind();
                    }








                }


            }
            else
            {
                gvDDSheet.DataSource = string.Empty;
                gvDDSheet.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            divfinal.Visible = true;
            GetSectionDetail();
            ViewFinalDisposalSheet();
            ViewFinalRRSheet();
            ViewFinalCOwMilkSheet();
        }
        else
        {
            gvDDSheet.DataSource = string.Empty;
            gvDDSheet.DataBind();
            divfinal.Visible = false;
        }
    }

    private void ViewFinalRRSheet()
    {
        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            DataSet dsRR_ChildF = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheet",
            new string[] { "flag", "Office_ID", "Date", "Shift_Id", "ProductSection_ID", "Ghee_RR", "Cream_RR", "Butter_RR", "SMP_RR" },
            new string[] { "1", objdb.Office_ID(), Fdate, ddlShift.SelectedValue, "2", objdb.LooseGheeItemTypeId_ID(), objdb.LooseCreamItemTypeId_ID(), objdb.LooseButterItemTypeId_ID(), objdb.LooseSMPItemTypeId_ID() }, "dataset");

            if (dsRR_ChildF != null && dsRR_ChildF.Tables[1].Rows.Count > 0)
            {
                GV_RRFinalSheet.DataSource = dsRR_ChildF.Tables[1];
                GV_RRFinalSheet.DataBind();
            }

            else
            {
                GV_RRFinalSheet.DataSource = string.Empty;
                GV_RRFinalSheet.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }

    private void ViewFinalCOwMilkSheet()
    {
        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            DataSet dsCW_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheet",
            new string[] { "flag", "Office_ID", "Date", "Shift_Id", "ProductSection_ID", "ItemType_id" },
            new string[] { "3", objdb.Office_ID(), Fdate, ddlShift.SelectedValue, "2", objdb.CowMilkItemTypeId_ID() }, "dataset");

            if (dsCW_Child != null && dsCW_Child.Tables[1].Rows.Count > 0)
            {
                GV_CowMilkSheetFinal.DataSource = dsCW_Child.Tables[1];
                GV_CowMilkSheetFinal.DataBind();
            }

            else
            {
                GV_CowMilkSheetFinal.DataSource = string.Empty;
                GV_CowMilkSheetFinal.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }



}