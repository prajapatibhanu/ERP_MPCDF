using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_MilkProductionEntry : System.Web.UI.Page
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
                    GetSectionView();
                    FillShift();

                    if (Session["ItemName"] != null && Session["ItemTypeId"] != null && Session["Date"] != null && Session["Shift"] != null)
                    { 
                        string ItemTypeId = Session["ItemTypeId"].ToString();
                        string Date = Session["Date"].ToString();
                        string Shift = Session["Shift"].ToString();
                        string ItemName = Session["ItemName"].ToString();
                        txtDate.Text = Date;
                        ddlShift.SelectedValue = Shift;
                        Session["ItemName"] = null;
                        Session["ItemTypeId"] = null;
                        Session["Date"] = null;
                        Session["Shift"] = null; 
                    }
                    txtDate_TextChanged(sender, e);

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

    private void GetSectionView()
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
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;

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
                //ddlShift.Enabled = false;
                //txtDate.Enabled = false;
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
            GetDisposalSheet();
            ViewFinalDisposalSheet();
            ViewFinalRRSheet();
            ViewFinalCOwMilkSheet();

        }
        else
        {

            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();
        }
    }

    protected void ddlShift_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
            GetDisposalSheet();
            ViewFinalDisposalSheet();
            ViewFinalRRSheet();
            ViewFinalCOwMilkSheet();
        }
        else
        {
            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
            GetDisposalSheet();
            ViewFinalDisposalSheet();
            ViewFinalRRSheet();
            ViewFinalCOwMilkSheet();
        }
        else
        {
            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();
        }
    }

    private void GetSectionDetail()
    {

        try
        {

            ds = null;

            int strcat_id = 0;

            if (ddlPSection.SelectedValue == "1")
            {
                strcat_id = 3;
            }
            if (ddlPSection.SelectedValue == "2")
            {
                strcat_id = 2;
            }

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }


            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id" },
                  new string[] { "8", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString() }, "dataset");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvmttos.DataSource = ds;
                gvmttos.DataBind();

                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;

                decimal PrvD_500InPkt = 0;
                decimal PrvD_200InPkt = 0;
                decimal PrvD_1InPkt = 0;
                decimal PrvD_500InLtr = 0;
                decimal PrvD_200InLtr = 0;
                decimal PrvD_1InLtr = 0;
                decimal CurD_500InPkt = 0;
                decimal CurD_200InPkt = 0;
                decimal CurD_1InPkt = 0;
                decimal CurD_500InLtr = 0;
                decimal CurD_200InLtr = 0;
                decimal CurD_1InLtr = 0;

                foreach (GridViewRow row in gvmttos.Rows)
                {


                    Label lblPrev_Demand_500InPkt = (Label)row.FindControl("lblPrev_Demand_500InPkt");
                    Label Prev_Demand_500InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_500InPkt_F") as Label);

                    if (lblPrev_Demand_500InPkt.Text != "")
                    {
                        PrvD_500InPkt += Convert.ToDecimal(lblPrev_Demand_500InPkt.Text);
                        Prev_Demand_500InPkt_F.Text = PrvD_500InPkt.ToString("0.00");
                    }


                    Label lblPrev_Demand_200InPkt = (Label)row.FindControl("lblPrev_Demand_200InPkt");
                    Label Prev_Demand_200InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_200InPkt_F") as Label);


                    if (lblPrev_Demand_200InPkt.Text != "")
                    {
                        PrvD_200InPkt += Convert.ToDecimal(lblPrev_Demand_200InPkt.Text);
                        Prev_Demand_200InPkt_F.Text = PrvD_200InPkt.ToString("0.00");
                    }

                    Label lblPrev_Demand_1InPkt = (Label)row.FindControl("lblPrev_Demand_1InPkt");
                    Label Prev_Demand_1InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_1InPkt_F") as Label);


                    if (lblPrev_Demand_1InPkt.Text != "")
                    {
                        PrvD_1InPkt += Convert.ToDecimal(lblPrev_Demand_1InPkt.Text);
                        Prev_Demand_1InPkt_F.Text = PrvD_1InPkt.ToString("0.00");
                    }

                    //Label lblPrev_Demand500InLtr = (Label)row.FindControl("lblPrev_Demand500InLtr");
                    //Label lblPrev_Demand500InLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_Demand500InLtr_F") as Label);


                    //if (lblPrev_Demand500InLtr.Text != "")
                    //{
                    //    PrvD_500InLtr += Convert.ToDecimal(lblPrev_Demand500InLtr.Text);
                    //    lblPrev_Demand500InLtr_F.Text = PrvD_500InLtr.ToString("0.00");
                    //}
                    //Label lblPrev_Demand200InLtr = (Label)row.FindControl("lblPrev_Demand200InLtr");
                    //Label lblPrev_Demand200InLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_Demand200InLtr_F") as Label);


                    //if (lblPrev_Demand200InLtr.Text != "")
                    //{
                    //    PrvD_200InLtr += Convert.ToDecimal(lblPrev_Demand200InLtr.Text);
                    //    lblPrev_Demand200InLtr_F.Text = PrvD_200InLtr.ToString("0.00");
                    //}
                    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                    Label lblPrev_Demand1InLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_Demand1InLtr_F") as Label);


                    if (lblPrev_DemandInLtr.Text != "")
                    {
                        PrvD_1InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                        lblPrev_Demand1InLtr_F.Text = PrvD_1InLtr.ToString("0.00");
                    }

                    Label lblCurrent_Demand_500InPkt = (Label)row.FindControl("lblCurrent_Demand_500InPkt");
                    Label lblCurrent_Demand_500InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_500InPkt_F") as Label);


                    if (lblCurrent_Demand_500InPkt.Text != "")
                    {
                        CurD_500InPkt += Convert.ToDecimal(lblCurrent_Demand_500InPkt.Text);
                        lblCurrent_Demand_500InPkt_F.Text = CurD_500InPkt.ToString("0.00");
                    }
                    Label lblCurrent_Demand_200InPkt = (Label)row.FindControl("lblCurrent_Demand_200InPkt");
                    Label lblCurrent_Demand_200InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_200InPkt_F") as Label);


                    if (lblCurrent_Demand_200InPkt.Text != "")
                    {
                        CurD_200InPkt += Convert.ToDecimal(lblCurrent_Demand_200InPkt.Text);
                        lblCurrent_Demand_200InPkt_F.Text = CurD_200InPkt.ToString("0.00");
                    }
                    Label lblCurrent_Demand_1InPkt = (Label)row.FindControl("lblCurrent_Demand_1InPkt");
                    Label lblCurrent_Demand_1InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_1InPkt_F") as Label);


                    if (lblCurrent_Demand_1InPkt.Text != "")
                    {
                        CurD_1InPkt += Convert.ToDecimal(lblCurrent_Demand_1InPkt.Text);
                        lblCurrent_Demand_1InPkt_F.Text = CurD_1InPkt.ToString("0.00");
                    }
                    //Label lblCurrent_Demand_500InLtr = (Label)row.FindControl("lblCurrent_Demand_500InLtr");
                    //Label lblCurrent_Demand_500InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_500InLtr_F") as Label);


                    //if (lblCurrent_Demand_500InLtr.Text != "")
                    //{
                    //    CurD_500InLtr += Convert.ToDecimal(lblCurrent_Demand_500InLtr.Text);
                    //    lblCurrent_Demand_500InLtr_F.Text = CurD_500InLtr.ToString("0.00");
                    //}
                    //Label lblCurrent_Demand_200InLtr = (Label)row.FindControl("lblCurrent_Demand_200InLtr");
                    //Label lblCurrent_Demand_200InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_200InLtr_F") as Label);


                    //if (lblCurrent_Demand_200InLtr.Text != "")
                    //{
                    //    CurD_200InLtr += Convert.ToDecimal(lblCurrent_Demand_200InLtr.Text);
                    //    lblCurrent_Demand_200InLtr_F.Text = CurD_200InLtr.ToString("0.00");
                    //}
                    Label lblCurrent_Demand_1InLtr = (Label)row.FindControl("lblCurrent_Demand_1InLtr");
                    Label lblCurrent_Demand_1InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_1InLtr_F") as Label);


                    if (lblCurrent_Demand_1InLtr.Text != "")
                    {
                        CurD_1InLtr += Convert.ToDecimal(lblCurrent_Demand_1InLtr.Text);
                        lblCurrent_Demand_1InLtr_F.Text = CurD_1InLtr.ToString("0.00");
                    }

                }


            }
            else
            {
                gvmttos.DataSource = string.Empty;
                gvmttos.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void lnkbtnVN_Click(object sender, EventArgs e)
    {

        try
        {

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lnkbtnVN");

            Session["ItemTypeId"] = lnkbtnVN.CommandArgument;
            Session["Date"] = txtDate.Text;
            Session["Shift"] = ddlShift.SelectedValue;
            Session["ItemName"] = lnkbtnVN.Text;

            Response.Redirect("MilkProductionEntry_frm.aspx", true);

            //btnPopupSave.Enabled = false;

            //int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            //LinkButton lnkbtnVN = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lnkbtnVN");
            //lblVarientname.Text = lnkbtnVN.Text;
            //lbldate.Text = txtDate.Text;
            //lblshift.Text = ddlShift.SelectedItem.Text;
            //lblsection.Text = ddlPSection.SelectedItem.Text;
            //ViewState["TypeId"] = lnkbtnVN.CommandArgument;

            //if (lnkbtnVN.CommandArgument == "59")
            //{
            //    txtIssuedforProductions.Text = "0";
            //    txtIssuedforProductions.Enabled = true;
            //    txtIssuedforProductions.ToolTip = "";
            //}
            //else
            //{
            //    txtIssuedforProductions.Text = "0";
            //    txtIssuedforProductions.Enabled = false;
            //    txtIssuedforProductions.ToolTip = "This Field Enable Only For STD Milk!";
            //}


            //if (txtDate.Text != "")
            //{
            //    Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            //}

            //lblMsg.Text = "";

            //DataSet dsVD = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
            //      new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
            //      new string[] { "10", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument }, "dataset");

            //if (dsVD != null && dsVD.Tables[0].Rows.Count > 0)
            //{

            //    lblReceived.Text = dsVD.Tables[0].Rows[0]["TMQty"].ToString();

            //    if (lblReceived.Text == "")
            //    {
            //        lblReceived.Text = "0";
            //    }

            //    if (lblopeningBalance.Text == "")
            //    {
            //        lblopeningBalance.Text = "0";
            //    }

            //    if (txtPrepared.Text == "")
            //    {
            //        txtPrepared.Text = "0";
            //    }

            //    if (lblReturn.Text == "")
            //    {
            //        lblReturn.Text = "0";
            //    }


            //    if (txtIssuedforstes.Text == "")
            //    {
            //        txtIssuedforstes.Text = "0";
            //    }

            //    if (txtIssuedforstes2.Text == "")
            //    {
            //        txtIssuedforstes2.Text = "0";
            //    }

            //    if (txtIssuedforstes3.Text == "")
            //    {
            //        txtIssuedforstes3.Text = "0";
            //    }

            //    if (txtIssuedforstes4.Text == "")
            //    {
            //        txtIssuedforstes4.Text = "0";
            //    }



            //    if (txtIssuedforWH.Text == "")
            //    {
            //        txtIssuedforWH.Text = "0";
            //    }

            //    if (txtIssuedforProductions.Text == "")
            //    {
            //        txtIssuedforProductions.Text = "0";
            //    }

            //    if (txtLosses.Text == "")
            //    {
            //        txtLosses.Text = "0";
            //    }

            //    if (txtClosingBalance.Text == "")
            //    {
            //        txtClosingBalance.Text = "0";
            //    }


            //    // For Variant

            //    DataSet dsVD_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
            //    new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
            //    new string[] { "11", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument }, "dataset");

            //    if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            //    {
            //        GVVariantDetail.DataSource = dsVD_Child.Tables[0];
            //        GVVariantDetail.DataBind();
            //    }

            //    else
            //    {
            //        GVVariantDetail.DataSource = string.Empty;
            //        GVVariantDetail.DataBind();
            //    }

            //    // For Container 
            //    if (dsVD_Child != null && dsVD_Child.Tables[1].Rows.Count > 0)
            //    {
            //        gvMilkinContainer.DataSource = dsVD_Child.Tables[1];
            //        gvMilkinContainer.DataBind();
            //    }
            //    else
            //    {
            //        gvMilkinContainer.DataSource = string.Empty;
            //        gvMilkinContainer.DataBind();
            //    }

            //    if (lnkbtnVN.CommandArgument == objdb.SkimmedMilkItemTypeId_ID())
            //    {
            //        // For Tanker 
            //        if (dsVD_Child != null && dsVD_Child.Tables[2].Rows.Count > 0)
            //        {
            //            gv_SMTDetails.DataSource = dsVD_Child.Tables[2];
            //            gv_SMTDetails.DataBind();
            //        }
            //        else
            //        {
            //            gv_SMTDetails.DataSource = string.Empty;
            //            gv_SMTDetails.DataBind();
            //        }

            //    }
            //    else
            //    {
            //        gv_SMTDetails.DataSource = string.Empty;
            //        gv_SMTDetails.DataBind();
            //    }



            //    DataSet dsVDUpdated = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
            //      new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
            //      new string[] { "13", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument }, "dataset");

            //    if (dsVDUpdated != null && dsVDUpdated.Tables[0].Rows.Count > 0)
            //    {
            //        btnPopupSave.Enabled = false;

            //        lblopeningBalance.Text = dsVDUpdated.Tables[0].Rows[0]["OpeningMilk"].ToString();
            //        lblReceived.Text = dsVDUpdated.Tables[0].Rows[0]["ReceivedMilk"].ToString();
            //        txtPrepared.Text = dsVDUpdated.Tables[0].Rows[0]["PreparedMilk"].ToString();
            //        lblReturn.Text = dsVDUpdated.Tables[0].Rows[0]["ReturnMilk"].ToString();

            //        txtPrepared.Enabled = false;
            //        lblReturn.Enabled = false;

            //        txtIssuedforstes2.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSale2"].ToString();
            //        txtIssuedforstes3.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSale3"].ToString();
            //        txtIssuedforstes4.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSale4"].ToString();


            //        txtIssuedforstes.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSates"].ToString();
            //        txtIssuedforWH.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForWholeMilk"].ToString();
            //        txtIssuedforProductions.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForProductSection"].ToString();
            //        txtLosses.Text = dsVDUpdated.Tables[0].Rows[0]["Losses"].ToString();
            //        txtClosingBalance.Text = dsVDUpdated.Tables[0].Rows[0]["ClosingBalance"].ToString();

            //        txtIssuedforstes2.Enabled = false;
            //        txtIssuedforstes3.Enabled = false;
            //        txtIssuedforstes4.Enabled = false;

            //        txtIssuedforstes.Enabled = false;
            //        txtIssuedforWH.Enabled = false;
            //        //txtIssuedforProductions.Enabled = false;
            //        txtLosses.Enabled = false;
            //        txtClosingBalance.Enabled = false;

            //        lblReceiptTotal.Text = dsVDUpdated.Tables[0].Rows[0]["ReceiptMilkNetQty"].ToString();
            //        lblIssuedtotal.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedMilkNetQty"].ToString();

            //        btngettotalvarient.Enabled = false;

            //    }
            //    else
            //    {
            //        btngettotalvarient.Enabled = true;
            //        txtIssuedforstes2.Enabled = true;
            //        txtIssuedforstes3.Enabled = true;
            //        txtIssuedforstes4.Enabled = true;

            //        //btnPopupSave.Enabled = true;
            //        txtIssuedforstes.Enabled = true;
            //        txtIssuedforWH.Enabled = true;
            //        //txtIssuedforProductions.Enabled = true;
            //        txtLosses.Enabled = true;
            //        txtClosingBalance.Enabled = true;
            //        txtPrepared.Enabled = true;
            //        lblReturn.Enabled = true;

            //    }

            //    if (dsVDUpdated != null && dsVDUpdated.Tables[1].Rows.Count > 0)
            //    {
            //        lblopeningBalance.Text = dsVDUpdated.Tables[1].Rows[0]["OpeningMilkQty"].ToString();

            //    }

            //    if (dsVDUpdated != null && dsVDUpdated.Tables[2].Rows.Count > 0)
            //    {
            //        gvMilkinContainer_opening.DataSource = dsVDUpdated.Tables[2];
            //        gvMilkinContainer_opening.DataBind();
            //    }
            //    else
            //    {
            //        gvMilkinContainer_opening.DataSource = string.Empty;
            //        gvMilkinContainer_opening.DataBind();
            //    }

            //    if (lnkbtnVN.CommandArgument == objdb.SkimmedMilkItemTypeId_ID())
            //    {
            //        if (dsVDUpdated != null && dsVDUpdated.Tables[4].Rows.Count > 0)
            //        {
            //            gv_SMTDetails_Opening.DataSource = dsVDUpdated.Tables[4];
            //            gv_SMTDetails_Opening.DataBind();
            //        }
            //        else
            //        {
            //            gv_SMTDetails_Opening.DataSource = string.Empty;
            //            gv_SMTDetails_Opening.DataBind();
            //        }

            //    }
            //    else
            //    {
            //        gv_SMTDetails_Opening.DataSource = string.Empty;
            //        gv_SMTDetails_Opening.DataBind();
            //    }
            //    //decimal MCQ_Opening = 0;

            //    //if (dsVDUpdated != null && dsVDUpdated.Tables[3].Rows.Count > 0)
            //    //{
            //    //    MCQ_Opening = Convert.ToDecimal(dsVDUpdated.Tables[3].Rows[0]["SumQty"].ToString());
            //    //}


            //    lblReceiptTotal.Text = (Convert.ToDecimal(lblopeningBalance.Text) + Convert.ToDecimal(lblReceived.Text) +
            //       Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(lblReturn.Text)).ToString();

            //    decimal VariantMCQ = 0;
            //    decimal VariantMCQ_Total = 0;

            //    foreach (GridViewRow row in GVVariantDetail.Rows)
            //    {
            //        Label lblPackagingSize = (Label)row.FindControl("lblPackagingSize");
            //        Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
            //        TextBox txtQtyInPkt = (TextBox)row.FindControl("txtQtyInPkt");
            //        Label lblQtyInLtr = (Label)row.FindControl("lblQtyInLtr");

            //        if (txtQtyInPkt.Text != "0" && txtQtyInPkt.Text != "0.00" && txtQtyInPkt.Text != "0.0" && txtQtyInPkt.Text != "")
            //        {
            //            if (lblPackagingSize.Text != "0" && lblPackagingSize.Text != "0.00" && lblPackagingSize.Text != "0.0" && lblPackagingSize.Text != "")
            //            {
            //                if (lblUnit_id.Text != "")
            //                {
            //                    int Unit_id = Convert.ToInt32(lblUnit_id.Text);

            //                    if (Unit_id == 20)
            //                    {
            //                        VariantMCQ = (Convert.ToDecimal(txtQtyInPkt.Text) * Convert.ToDecimal(lblPackagingSize.Text)) / 1000;
            //                    }

            //                    if (Unit_id == 6)
            //                    {
            //                        VariantMCQ = (Convert.ToDecimal(txtQtyInPkt.Text) * Convert.ToDecimal(lblPackagingSize.Text));
            //                    }

            //                    lblQtyInLtr.Text = VariantMCQ.ToString("0.0");


            //                }
            //            }

            //        }

            //        VariantMCQ_Total += VariantMCQ;

            //    }


            //    decimal MCQ = 0;

            //    foreach (GridViewRow row in gvMilkinContainer.Rows)
            //    {
            //        TextBox txtQtyInKg = (TextBox)row.FindControl("txtQtyInKg");

            //        if (txtQtyInKg.Text != "0" && txtQtyInKg.Text != "0.00" && txtQtyInKg.Text != "0.0" && txtQtyInKg.Text != "")
            //        {
            //            MCQ += Convert.ToDecimal(txtQtyInKg.Text);
            //        }
            //    }


            //    if (lnkbtnVN.CommandArgument == objdb.SkimmedMilkItemTypeId_ID())
            //    {

            //        decimal SMPD = 0;

            //        foreach (GridViewRow row_SM in gv_SMTDetails.Rows)
            //        {
            //            TextBox txtQtyInLtr = (TextBox)row_SM.FindControl("txtQtyInLtr");

            //            if (txtQtyInLtr.Text != "")
            //            {
            //                SMPD += Convert.ToDecimal(txtQtyInLtr.Text);
            //            }
            //        }

            //        txtClosingBalance.Text = (MCQ + VariantMCQ_Total + SMPD).ToString();
            //        txtClosingBalance.Enabled = false;

            //    }
            //    else
            //    {
            //        txtClosingBalance.Text = (MCQ + VariantMCQ_Total).ToString();
            //        txtClosingBalance.Enabled = false;
            //    }


            //    if (txtIssuedforstes2.Text == "")
            //    {
            //        txtIssuedforstes2.Text = "0";
            //    }

            //    if (txtIssuedforstes3.Text == "")
            //    {
            //        txtIssuedforstes3.Text = "0";
            //    }

            //    if (txtIssuedforstes4.Text == "")
            //    {
            //        txtIssuedforstes4.Text = "0";
            //    }


            //    lblIssuedtotal.Text = (Convert.ToDecimal(txtIssuedforstes.Text) + Convert.ToDecimal(txtIssuedforWH.Text) +
            //       Convert.ToDecimal(txtIssuedforProductions.Text) + Convert.ToDecimal(txtLosses.Text) + Convert.ToDecimal(txtClosingBalance.Text)
            //       + Convert.ToDecimal(txtIssuedforstes2.Text)
            //       + Convert.ToDecimal(txtIssuedforstes3.Text)
            //       + Convert.ToDecimal(txtIssuedforstes4.Text)).ToString();

            //}


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btngettotalvarient_Click(object sender, EventArgs e)
    {
        try
        {
            CalculationMilkP();
            btnPopupSave.Enabled = true;
            // if (txtClosingBalance.Text != "" && txtClosingBalance.Text != "0.0" && txtClosingBalance.Text != "0.00")
            // {
            // btnPopupSave.Enabled = true;
            // }
            // else
            // {
            // btnPopupSave.Enabled = false;
            // }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        lblPopupMsg.Text = "";
        txtIssuedforstes.Text = "";
        txtPrepared.Text = "";
        txtIssuedforWH.Text = "";
        lblReturn.Text = "";
        txtIssuedforProductions.Text = "";
        txtLosses.Text = "";
        txtClosingBalance.Text = "";
        GVVariantDetail.DataSource = string.Empty;
        GVVariantDetail.DataBind();
        lblReceiptTotal.Text = "";
        lblIssuedtotal.Text = "";

        ddlPSection_SelectedIndexChanged(sender, e);
    }

    public void CalculationMilkP()
    {
        try
        {

            if (ViewState["TypeId"] == null)
            {
                Response.Redirect("MilkProductionEntry.aspx", true);
            }


            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblMsg.Text = "";

            DataSet dsVD = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                  new string[] { "10", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, ViewState["TypeId"].ToString() }, "dataset");

            if (dsVD != null && dsVD.Tables[0].Rows.Count > 0)
            {

                lblReceived.Text = dsVD.Tables[0].Rows[0]["TMQty"].ToString();

                if (lblReceived.Text == "")
                {
                    lblReceived.Text = "0";
                }


                if (lblopeningBalance.Text == "")
                {
                    lblopeningBalance.Text = "0";
                }


                if (txtPrepared.Text == "")
                {
                    txtPrepared.Text = "0";
                }

                if (lblReturn.Text == "")
                {
                    lblReturn.Text = "0";
                }

                decimal MCQ_Opening = 0;

                foreach (GridViewRow row1 in gvMilkinContainer_opening.Rows)
                {
                    Label txtQtyInKg = (Label)row1.FindControl("txtQtyInKg");

                    if (txtQtyInKg.Text != "0" && txtQtyInKg.Text != "0.00" && txtQtyInKg.Text != "0.0" && txtQtyInKg.Text != "")
                    {
                        MCQ_Opening += Convert.ToDecimal(txtQtyInKg.Text);
                    }

                }



                lblReceiptTotal.Text = (Convert.ToDecimal(lblopeningBalance.Text) + Convert.ToDecimal(lblReceived.Text) +
                    Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(lblReturn.Text)).ToString();


                if (txtIssuedforstes.Text == "")
                {
                    txtIssuedforstes.Text = "0";
                }

                if (txtIssuedforWH.Text == "")
                {
                    txtIssuedforWH.Text = "0";
                }

                if (txtIssuedforProductions.Text == "")
                {
                    txtIssuedforProductions.Text = "0";
                }

                if (txtLosses.Text == "")
                {
                    txtLosses.Text = "0";
                }

                if (txtClosingBalance.Text == "")
                {
                    txtClosingBalance.Text = "0";
                }

                decimal MCQ = 0;

                foreach (GridViewRow row in gvMilkinContainer.Rows)
                {
                    TextBox txtQtyInKg = (TextBox)row.FindControl("txtQtyInKg");

                    if (txtQtyInKg.Text != "0" && txtQtyInKg.Text != "0.00" && txtQtyInKg.Text != "0.0" && txtQtyInKg.Text != "")
                    {
                        MCQ += Convert.ToDecimal(txtQtyInKg.Text);
                    }

                }



                decimal VariantMCQ = 0;
                decimal VariantMCQ_Total = 0;

                foreach (GridViewRow row in GVVariantDetail.Rows)
                {
                    Label lblPackagingSize = (Label)row.FindControl("lblPackagingSize");
                    Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
                    TextBox txtQtyInPkt = (TextBox)row.FindControl("txtQtyInPkt");
                    Label lblQtyInLtr = (Label)row.FindControl("lblQtyInLtr");

                    if (txtQtyInPkt.Text == "") { txtQtyInPkt.Text = "0"; }

                    if (txtQtyInPkt.Text != "")
                    {
                        if (lblPackagingSize.Text != "")
                        {
                            if (lblUnit_id.Text != "")
                            {
                                int Unit_id = Convert.ToInt32(lblUnit_id.Text);

                                if (Unit_id == 20)
                                {
                                    VariantMCQ = (Convert.ToDecimal(txtQtyInPkt.Text) * Convert.ToDecimal(lblPackagingSize.Text)) / 1000;
                                }

                                if (Unit_id == 6)
                                {
                                    VariantMCQ = (Convert.ToDecimal(txtQtyInPkt.Text) * Convert.ToDecimal(lblPackagingSize.Text));
                                }

                                lblQtyInLtr.Text = VariantMCQ.ToString("0.0");


                            }
                        }

                    }

                    VariantMCQ_Total += Convert.ToDecimal(lblQtyInLtr.Text);

                }


                if (ViewState["TypeId"].ToString() == objdb.SkimmedMilkItemTypeId_ID())
                {

                    decimal SMPD = 0;

                    foreach (GridViewRow row_SM in gv_SMTDetails.Rows)
                    {
                        TextBox txtQtyInLtr = (TextBox)row_SM.FindControl("txtQtyInLtr");

                        if (txtQtyInLtr.Text != "")
                        {
                            SMPD += Convert.ToDecimal(txtQtyInLtr.Text);
                        }

                    }

                    txtClosingBalance.Text = (MCQ + VariantMCQ_Total + SMPD).ToString();
                    txtClosingBalance.Enabled = false;

                }
                else
                {
                    txtClosingBalance.Text = (MCQ + VariantMCQ_Total).ToString();
                    txtClosingBalance.Enabled = false;

                }


                if (txtIssuedforstes2.Text == "")
                {
                    txtIssuedforstes2.Text = "0";
                }

                if (txtIssuedforstes3.Text == "")
                {
                    txtIssuedforstes3.Text = "0";
                }

                if (txtIssuedforstes4.Text == "")
                {
                    txtIssuedforstes4.Text = "0";
                }

                lblIssuedtotal.Text = (Convert.ToDecimal(txtIssuedforstes.Text) + Convert.ToDecimal(txtIssuedforWH.Text) +
                    Convert.ToDecimal(txtIssuedforProductions.Text) + Convert.ToDecimal(txtLosses.Text) + Convert.ToDecimal(txtClosingBalance.Text)
                    + Convert.ToDecimal(txtIssuedforstes2.Text)
                   + Convert.ToDecimal(txtIssuedforstes3.Text)
                   + Convert.ToDecimal(txtIssuedforstes4.Text)).ToString();

            }
            else
            {

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtQtyInLtr_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();

    }

    protected void txtQtyInPkt_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtIssuedforstes_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtPrepared_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtIssuedforWH_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void lblReturn_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtIssuedforProductions_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtLosses_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtQtyInKg_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
    }

    protected void txtClosingBalance_TextChanged(object sender, EventArgs e)
    {
        CalculationMilkP();
        GetDisposalSheet();
    }

    private DataTable GetMilkVarientInfo()
    {

        decimal MQinpkt = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("QtyInPkt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));


        foreach (GridViewRow row in GVVariantDetail.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");
            TextBox txtQtyInPkt = (TextBox)row.FindControl("txtQtyInPkt");

            if (txtQtyInPkt.Text != "0" && txtQtyInPkt.Text != "0.00" && txtQtyInPkt.Text != "0.0" && txtQtyInPkt.Text != "")
            {
                MQinpkt = Convert.ToDecimal(txtQtyInPkt.Text);
            }
            else
            {
                MQinpkt = 0;
            }

            if (MQinpkt != 0 && MQinpkt != 0 && MQinpkt != 0)
            {
                dr = dt.NewRow();
                dr[0] = MQinpkt;
                dr[1] = lblItem_id.Text;
                dr[2] = ViewState["TypeId"].ToString();
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    private DataTable GetMilkinContainerInfo()
    {

        decimal QtyInKg = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("QtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("I_MCID", typeof(int)));

        foreach (GridViewRow row in gvMilkinContainer.Rows)
        {
            Label lblI_MCID = (Label)row.FindControl("lblI_MCID");
            TextBox txtQtyInKg = (TextBox)row.FindControl("txtQtyInKg");

            if (txtQtyInKg.Text != "0" && txtQtyInKg.Text != "0.00" && txtQtyInKg.Text != "0.0" && txtQtyInKg.Text != "")
            {
                QtyInKg = Convert.ToDecimal(txtQtyInKg.Text);
            }
            else
            {
                QtyInKg = 0;
            }

            if (QtyInKg != 0)
            {
                dr = dt.NewRow();
                dr[0] = QtyInKg;
                dr[1] = ViewState["TypeId"].ToString();
                dr[2] = lblI_MCID.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    private DataTable GetMilkinTankerInfo()
    {

        decimal QtyInLtr = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(int)));

        foreach (GridViewRow row in gv_SMTDetails.Rows)
        {
            Label lblI_TankerID = (Label)row.FindControl("lblI_TankerID");
            TextBox txtQtyInLtr = (TextBox)row.FindControl("txtQtyInLtr");

            if (txtQtyInLtr.Text != "")
            {
                QtyInLtr = Convert.ToDecimal(txtQtyInLtr.Text);
            }
            else
            {
                QtyInLtr = 0;
            }

            if (QtyInLtr != 0)
            {
                dr = dt.NewRow();
                dr[0] = txtQtyInLtr.Text;
                dr[1] = ViewState["TypeId"].ToString();
                dr[2] = lblI_TankerID.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    protected void btnYesT_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtIDF = new DataTable();
                dtIDF = GetMilkVarientInfo();

                DataTable dtMCD = new DataTable();
                dtMCD = GetMilkinContainerInfo();

                DataTable dtMIT = new DataTable();
                dtMIT = GetMilkinTankerInfo();


                if (Convert.ToDecimal(lblReceiptTotal.Text) == Convert.ToDecimal(lblIssuedtotal.Text))
                {
                    ds = null;

                    ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                                              new string[] { "flag" 
				                                ,"Office_ID"
				                                ,"Date" 
				                                ,"Shift_Id" 
				                                ,"CreatedBy" 
                                                ,"ProductSection_ID"
                                                ,"OpeningMilk"
                                                ,"ReceivedMilk"
                                                ,"PreparedMilk"
                                                ,"ReturnMilk"
                                                ,"IssuedForSates"
                                                ,"IssuedForWholeMilk"
                                                ,"IssuedForProductSection"
                                                ,"Losses"
                                                ,"ClosingBalance"
                                                ,"ReceiptMilkNetQty"
                                                ,"IssuedMilkNetQty" 
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id"
                                                ,"IssuedForSale2"
                                                ,"IssuedForSale3"
                                                ,"IssuedForSale4" 

                                    },
                                              new string[] { "12"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                              , ddlShift.SelectedValue.ToString()
                                              , ViewState["Emp_ID"].ToString()
                                              ,ddlPSection.SelectedValue
                                              ,lblopeningBalance.Text
                                              ,lblReceived.Text
                                              ,txtPrepared.Text
                                              ,lblReturn.Text
                                              ,txtIssuedforstes.Text
                                              ,txtIssuedforWH.Text
                                              ,txtIssuedforProductions.Text
                                              ,txtLosses.Text
                                              ,txtClosingBalance.Text
                                              ,lblReceiptTotal.Text
                                              ,lblIssuedtotal.Text
                                              ,objdb.GetLocalIPAddress()
                                              ,ViewState["TypeId"].ToString(),
                                              txtIssuedforstes2.Text,
                                              txtIssuedforstes3.Text,
                                              txtIssuedforstes4.Text

                                    },
                                             new string[] { "type_Production_Milk_InOutProcessChild", "type_Production_Milk_InOutProcessChild2MC", "type_Production_Milk_InOutProcessChild3MIT" },
                                             new DataTable[] { dtIDF, dtMCD, dtMIT }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblopeningBalance.Text = "0";
                        lblReceived.Text = "0";
                        txtPrepared.Text = "0";
                        lblReturn.Text = "0";
                        txtIssuedforstes.Text = "0";
                        txtIssuedforWH.Text = "0";
                        txtIssuedforProductions.Text = "0";
                        txtLosses.Text = "0";
                        txtClosingBalance.Text = "0";
                        lblReceiptTotal.Text = "0";
                        lblIssuedtotal.Text = "0";
                        txtIssuedforstes2.Text = "0";
                        txtIssuedforstes3.Text = "0";
                        txtIssuedforstes4.Text = "0";

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "AlreadySaved")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
                    }

                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);

                    }

                }
                else
                {
                    lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Receipt Milk Qty And Issued Milk Qty Can't Equal");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
                }




                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);


            }
        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
        }


    }

    private void GetDisposalSheet()
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
                FTitle.Visible = true;
                gvDDSheet.DataSource = ds;
                gvDDSheet.DataBind();


                foreach (GridViewRow row in gvDDSheet.Rows)
                {

                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

                    GridView GVVariantDetail = (GridView)row.FindControl("GVVariantDetail");

                    DataSet dsVD_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                    new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                    new string[] { "11", objdb.Office_ID(), "1", "2020-06-03", ddlPSection.SelectedValue, lblItemType_id.Text }, "dataset");

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



                }


            }
            else
            {
                FTitle.Visible = false;
                gvDDSheet.DataSource = string.Empty;
                gvDDSheet.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    //For Whole Milk Sheet

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

                lblWMSReceipt.Text = ds.Tables[0].Rows[0]["ReceiptMilkQty"].ToString();

            }
            else
            {

                lblReceiptqty.Text = "0";
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

            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                lblReceiptOpeningBalance.Text = ds.Tables[4].Rows[0]["OpeningMilkQtyHW_Prev"].ToString();
            }

            lblReceiptFinalTotal.Text = "0";
            lblIssuedFinalTotal.Text = "0";

            lblWMSR_Total.Text = "0";
            lblWMSI_Total.Text = "0";

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


    protected void lbFInalSheet_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg_issue.Text = "";
            lblmsgTanker.Text = "";
            divTankerName.Visible = false;
            ViewState["InsertRecord1"] = null;
            ViewState["InsertRecord2"] = null;
            ViewState["InsertRecord3"] = null;
            GV_CD_WM1.DataSource = string.Empty;
            GV_CD_WM1.DataBind();
            GV_TDetail_WM_Receipt1.DataSource = string.Empty;
            GV_TDetail_WM_Receipt1.DataBind();
            GV_TDetail_WM_Issued1.DataSource = string.Empty;
            GV_TDetail_WM_Issued1.DataBind();
            GV_TDetail_WM_Issued.DataSource = string.Empty;
            GV_TDetail_WM_Issued.DataBind();
            GV_TDetail_WM_Receipt.DataSource = string.Empty;
            GV_TDetail_WM_Receipt.DataBind();


            Label4.Text = "";
            txtWMSClosingBalance.Text = "";
            lblName_WM.Text = "WHOLE MILK";
            lblDate_WM.Text = txtDate.Text;
            lblShift_WM.Text = ddlShift.SelectedItem.Text;
            lblSection_WM.Text = ddlPSection.SelectedItem.Text;

            decimal FinalReceiptTotalQty_EU = 0;
            decimal FinalIssuedTotalQty_EU = 0;

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


            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {

                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();

                foreach (GridViewRow row in GridView2.Rows)
                {
                    // Milk QTY  
                    Label txtQtyInPkt = (Label)row.FindControl("txtQtyInPkt");
                    if (txtQtyInPkt.Text != "")
                    {
                        FinalIssuedTotalQty_EU += Convert.ToDecimal(txtQtyInPkt.Text);
                    }

                }


            }
            else
            {

                GridView2.DataSource = string.Empty;
                GridView2.DataBind();
            }

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {

                GridView1.DataSource = ds.Tables[2];
                GridView1.DataBind();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Milk QTY  
                    Label txtQtyInPkt = (Label)row.FindControl("txtQtyInPkt");
                    if (txtQtyInPkt.Text != "")
                    {
                        FinalReceiptTotalQty_EU += Convert.ToDecimal(txtQtyInPkt.Text);
                    }

                }

            }
            else
            {

                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
            }


            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                lblWMSOpeningBalance.Text = ds.Tables[3].Rows[0]["WH_IssuedClosingBalance"].ToString();
                lblWMSReceipt.Text = ds.Tables[3].Rows[0]["WM_ReceiptMilk"].ToString();
                txtWMSR_Good.Text = ds.Tables[3].Rows[0]["WM_ReceiptGood"].ToString();
                txtWMSR_Sour.Text = ds.Tables[3].Rows[0]["WM_ReceiptSour"].ToString();
                txtWMSR_Curdled.Text = ds.Tables[3].Rows[0]["WH_ReceiptCurdled"].ToString();
                txtWMSCR.Text = ds.Tables[3].Rows[0]["WH_ReceiptCR"].ToString();
                txtWMSWB.Text = ds.Tables[3].Rows[0]["WH_ReceiptWB"].ToString();
                txtWMSFlushing.Text = ds.Tables[3].Rows[0]["WH_ReceiptFlushing"].ToString();

                txtWMSIssuedForSaleToProduct.Text = ds.Tables[3].Rows[0]["WM_IssuedForSaleToProduct"].ToString();
                txtWMSI_Good.Text = ds.Tables[3].Rows[0]["WM_IssuedGood"].ToString();
                txtWMSI_Sour.Text = ds.Tables[3].Rows[0]["WM_IssuedSour"].ToString();
                txtWMSI_Curdled.Text = ds.Tables[3].Rows[0]["WH_IssuedCurdled"].ToString();
                txtWMSIssuedToProductSection.Text = ds.Tables[3].Rows[0]["WH_IssuedToProductSection"].ToString();
                txtWMSLosses.Text = ds.Tables[3].Rows[0]["WH_IssuedLosses"].ToString();
                txtWMSClosingBalance.Text = ds.Tables[3].Rows[0]["WH_IssuedClosingBalance"].ToString();

                txtWMSR_Good.Enabled = false;
                txtWMSR_Sour.Enabled = false;
                txtWMSR_Curdled.Enabled = false;
                txtWMSCR.Enabled = false;
                txtWMSWB.Enabled = false;
                txtWMSFlushing.Enabled = false;
                txtWMSIssuedForSaleToProduct.Enabled = false;
                txtWMSI_Good.Enabled = false;
                txtWMSI_Sour.Enabled = false;
                txtWMSI_Curdled.Enabled = false;
                txtWMSIssuedToProductSection.Enabled = false;
                txtWMSLosses.Enabled = false;
                txtWMSClosingBalance.Enabled = false;
                btnFinalPopup.Enabled = false;


            }
            else
            {
                txtWMSR_Good.Enabled = true;
                txtWMSR_Sour.Enabled = true;
                txtWMSR_Curdled.Enabled = true;
                txtWMSCR.Enabled = true;
                txtWMSWB.Enabled = true;
                txtWMSFlushing.Enabled = true;
                txtWMSIssuedForSaleToProduct.Enabled = true;
                txtWMSI_Good.Enabled = true;
                txtWMSI_Sour.Enabled = true;
                txtWMSI_Curdled.Enabled = true;
                txtWMSIssuedToProductSection.Enabled = true;
                txtWMSLosses.Enabled = true;
                txtWMSClosingBalance.Enabled = true;

                btnFinalPopup.Enabled = true;

                if (lblWMSOpeningBalance.Text == "")
                {
                    lblWMSOpeningBalance.Text = "0";
                }
                if (txtWMSIssuedForSaleToProduct.Text == "")
                {
                    txtWMSIssuedForSaleToProduct.Text = "0";
                }
                if (lblWMSReceipt.Text == "")
                {
                    lblWMSReceipt.Text = "0";
                }

                if (txtWMSR_Good.Text == "")
                {
                    txtWMSR_Good.Text = "0";
                }
                if (txtWMSI_Good.Text == "")
                {
                    txtWMSI_Good.Text = "0";
                }

                if (txtWMSR_Sour.Text == "")
                {
                    txtWMSR_Sour.Text = "0";
                }
                if (txtWMSI_Sour.Text == "")
                {
                    txtWMSI_Sour.Text = "0";
                }

                if (txtWMSR_Curdled.Text == "")
                {
                    txtWMSR_Curdled.Text = "0";
                }
                if (txtWMSI_Curdled.Text == "")
                {
                    txtWMSI_Curdled.Text = "0";
                }

                if (txtWMSCR.Text == "")
                {
                    txtWMSCR.Text = "0";
                }
                if (txtWMSIssuedToProductSection.Text == "")
                {
                    txtWMSIssuedToProductSection.Text = "0";
                }
                if (txtWMSWB.Text == "")
                {
                    txtWMSWB.Text = "0";
                }
                if (txtWMSLosses.Text == "")
                {
                    txtWMSLosses.Text = "0";
                }
                if (txtWMSFlushing.Text == "")
                {
                    txtWMSFlushing.Text = "0";
                }
                if (txtWMSClosingBalance.Text == "")
                {
                    txtWMSClosingBalance.Text = "0";
                }


            }


            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {
                lblWMSOpeningBalance.Text = ds.Tables[4].Rows[0]["OpeningMilkQtyHW_Prev"].ToString();
            }


            if (ds != null && ds.Tables[5].Rows.Count > 0)
            {
                txtWMSI_Good.Text = ds.Tables[5].Rows[0]["WM_GoodMilk"].ToString();
                txtWMSI_Good.Enabled = false;
            }


            if (ds != null && ds.Tables[6].Rows.Count > 0)
            {
                if (ds != null && ds.Tables[3].Rows.Count > 0)
                {
                    FSTanker_R.Visible = false;
                    GV_CD_WM1.DataSource = ds.Tables[6];
                    GV_CD_WM1.DataBind();
                }
                else
                {
                    FSTanker_R.Visible = true;
                    ddlContainer.DataSource = ds.Tables[6];
                    ddlContainer.DataTextField = "V_MCName";
                    ddlContainer.DataValueField = "I_MCID";
                    ddlContainer.DataBind();
                    ddlContainer.Items.Insert(0, new ListItem("Select", "0"));
                }



            }
            else
            {
                FSTanker_R.Visible = false;
                ddlContainer.Items.Insert(0, new ListItem("Select", "0"));
                GV_CD_WM1.DataSource = string.Empty;
                GV_CD_WM1.DataBind();
            }

            if (ds != null && ds.Tables[7].Rows.Count > 0)
            {

                if (ds != null && ds.Tables[3].Rows.Count > 0)
                {
                    FSTanker_I.Visible = false;
                    GV_TDetail_WM_Receipt1.DataSource = ds.Tables[7];
                    GV_TDetail_WM_Receipt1.DataBind();
                }
                else
                {
                    FSTanker_I.Visible = true;
                    ddlTankerNo.DataSource = ds.Tables[7];
                    ddlTankerNo.DataTextField = "V_VehicleNo";
                    ddlTankerNo.DataValueField = "I_TankerID";
                    ddlTankerNo.DataBind();
                    ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                    ddlTankerNo.Items.Insert(ddlTankerNo.Items.Count - 1, new ListItem("Add Temporary Tanker", "-1"));
                }




            }
            else
            {
                FSTanker_I.Visible = false;
                GV_TDetail_WM_Receipt1.DataSource = string.Empty;
                GV_TDetail_WM_Receipt1.DataBind();
                ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));

            }


            if (ds != null && ds.Tables[8].Rows.Count > 0)
            {

                if (ds != null && ds.Tables[3].Rows.Count > 0)
                {
                    FSContainer.Visible = false;
                    GV_TDetail_WM_Issued1.DataSource = ds.Tables[8];
                    GV_TDetail_WM_Issued1.DataBind();
                }
                else
                {
                    FSContainer.Visible = true;
                    ddlTankerNo_Issue.DataSource = ds.Tables[8];
                    ddlTankerNo_Issue.DataTextField = "V_VehicleNo";
                    ddlTankerNo_Issue.DataValueField = "I_TankerID";
                    ddlTankerNo_Issue.DataBind();
                    ddlTankerNo_Issue.Items.Insert(0, new ListItem("Select", "0"));
                    ddlTankerNo_Issue.Items.Insert(ddlTankerNo_Issue.Items.Count - 1, new ListItem("Add Temporary Tanker", "-1"));
                    //ddlTankerNo_Issue.Items.Insert(-1, new ListItem("Temporary Tanker", "-1"));
                }




            }
            else
            {
                FSContainer.Visible = false;
                ddlTankerNo_Issue.Items.Insert(0, new ListItem("Select", "0"));
                GV_TDetail_WM_Issued1.DataSource = string.Empty;
                GV_TDetail_WM_Issued1.DataBind();
            }


            lblWMSR_Total.Text = "0";
            lblWMSI_Total.Text = "0";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);

        }

        catch (Exception ex)
        {
            lblhmMSG.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    private DataTable WM_GetMilkinTankerInfo_Receipt()
    {

        decimal QtyInLtr = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
        dt.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));

        foreach (GridViewRow row in GV_TDetail_WM_Receipt.Rows)
        {
            Label lblI_TankerID = (Label)row.FindControl("lblI_TankerID");
            Label lblV_VehicleNo = (Label)row.FindControl("lblV_VehicleNo");
            TextBox txtQtyInLtr = (TextBox)row.FindControl("txtQtyInLtr");

            if (txtQtyInLtr.Text != "0" && txtQtyInLtr.Text != "0.00" && txtQtyInLtr.Text != "0.0" && txtQtyInLtr.Text != "")
            {
                QtyInLtr = Convert.ToDecimal(txtQtyInLtr.Text);
            }
            else
            {
                QtyInLtr = 0;
            }

            if (QtyInLtr != 0)
            {
                dr = dt.NewRow();
                dr[0] = txtQtyInLtr.Text;
                dr[1] = 0;
                dr[2] = lblI_TankerID.Text;
                dr[3] = lblV_VehicleNo.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    private DataTable WM_GetMilkinTankerInfo_Issued()
    {

        decimal QtyInLtr = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("QtyInLtr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
        dt.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));

        foreach (GridViewRow row in GV_TDetail_WM_Issued.Rows)
        {
            Label lblI_TankerID = (Label)row.FindControl("lblI_TankerID");
            Label lblV_VehicleNo = (Label)row.FindControl("lblV_VehicleNo");
            TextBox txtQtyInLtr = (TextBox)row.FindControl("txtQtyInLtr");

            if (txtQtyInLtr.Text != "0" && txtQtyInLtr.Text != "0.00" && txtQtyInLtr.Text != "0.0" && txtQtyInLtr.Text != "")
            {
                QtyInLtr = Convert.ToDecimal(txtQtyInLtr.Text);
            }
            else
            {
                QtyInLtr = 0;
            }

            if (QtyInLtr != 0)
            {
                dr = dt.NewRow();
                dr[0] = txtQtyInLtr.Text;
                dr[1] = 0;
                dr[2] = lblI_TankerID.Text;
                dr[3] = lblV_VehicleNo.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    private DataTable WM_GetMilkinContainerInfo()
    {

        decimal QtyInKg = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("QtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("I_MCID", typeof(int)));

        foreach (GridViewRow row in GV_CD_WM.Rows)
        {
            Label lblI_MCID = (Label)row.FindControl("lblI_MCID");
            TextBox txtQtyInKgCM = (TextBox)row.FindControl("txtQtyInKg");

            if (txtQtyInKgCM.Text != "0" && txtQtyInKgCM.Text != "0.00" && txtQtyInKgCM.Text != "0.0" && txtQtyInKgCM.Text != "")
            {
                QtyInKg = Convert.ToDecimal(txtQtyInKgCM.Text);
            }
            else
            {
                QtyInKg = 0;
            }

            if (QtyInKg != 0)
            {
                dr = dt.NewRow();
                dr[0] = QtyInKg;
                dr[1] = 0;
                dr[2] = lblI_MCID.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    protected void btnYes_Final_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dt1_WMReceipt = new DataTable();
                dt1_WMReceipt = WM_GetMilkinTankerInfo_Receipt();

                DataTable dt2_WMIssued = new DataTable();
                dt2_WMIssued = WM_GetMilkinTankerInfo_Issued();

                DataTable dt3_WMContainerInfo = new DataTable();
                dt3_WMContainerInfo = WM_GetMilkinContainerInfo();

                ds = null;
                ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                                          new string[] { "flag" 
				                                ,"Office_ID"
				                                ,"Date" 
				                                ,"Shift_Id" 
				                                ,"CreatedBy" 
                                                ,"ProductSection_ID",
                                                 "WM_ReceiptOpeningBalance",
                                                 "WM_ReceiptMilk",
                                                 "WM_ReceiptGood",
                                                 "WM_ReceiptSour",
                                                 "WH_ReceiptCurdled",
                                                 "WH_ReceiptCR",
                                                 "WH_ReceiptWB",
                                                 "WH_ReceiptFlushing",
                                                 "WH_ReceiptTotal",
                                                 "WM_IssuedForSaleToProduct",
                                                 "WM_IssuedGood",
                                                 "WM_IssuedSour",
                                                 "WH_IssuedCurdled",
                                                 "WH_IssuedToProductSection",
                                                 "WH_IssuedLosses",
                                                 "WH_IssuedClosingBalance",
                                                 "WH_IssuedTotal",
                                                 "CreatedBy_IP"
                                              },
                                          new string[] { "17"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                              , ddlShift.SelectedValue.ToString()
                                              , ViewState["Emp_ID"].ToString()
                                              ,ddlPSection.SelectedValue
                                              ,lblWMSOpeningBalance.Text
                                              ,lblWMSReceipt.Text
                                              ,txtWMSR_Good.Text
                                              ,txtWMSR_Sour.Text
                                              ,txtWMSR_Curdled.Text
                                              ,txtWMSCR.Text
                                              ,txtWMSWB.Text
                                              ,txtWMSFlushing.Text
                                              ,lblWMSR_Total.Text
                                              ,txtWMSIssuedForSaleToProduct.Text
                                              ,txtWMSI_Good.Text
                                              ,txtWMSI_Sour.Text
                                              ,txtWMSI_Curdled.Text
                                              ,txtWMSIssuedToProductSection.Text
                                              ,txtWMSLosses.Text
                                              ,txtWMSClosingBalance.Text
                                              ,lblWMSI_Total.Text
                                              ,objdb.GetLocalIPAddress()

                                     },
                                             new string[] { "type_tbl_Production_Milk_InOutProcessWholeMilkSheet_ChildTDR1", "type_tbl_Production_Milk_InOutProcessWholeMilkSheet_ChildTDI1", "type_ttbl_Production_Milk_InOutProcessWholeMilkSheet_ChildCMI1" },
                                             new DataTable[] { dt1_WMReceipt, dt2_WMIssued, dt3_WMContainerInfo }, "TableSave");


                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Label4.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                }

                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "AlreadyUpdated")
                {
                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Label4.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", success);

                }

                else
                {
                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Label4.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());

                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                lbFInalSheet_Click(sender, e);

            }
        }

        catch (Exception ex)
        {
            Label4.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        }
    }

    protected void txtQtyInKgCM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtWMSClosingBalance.Text = "";

            decimal CL_HMS = 0;

            foreach (GridViewRow row in GV_CD_WM.Rows)
            {

                TextBox txtQtyInKgCM = (TextBox)row.FindControl("txtQtyInKgCM");

                if (txtQtyInKgCM.Text != "" && txtQtyInKgCM.Text != "0" && txtQtyInKgCM.Text != "0.0" && txtQtyInKgCM.Text != "0.00")
                {
                    CL_HMS += Convert.ToDecimal(txtQtyInKgCM.Text);
                }

            }

            txtWMSClosingBalance.Text = CL_HMS.ToString("0.00");
            txtWMSClosingBalance.Enabled = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        }
        catch (Exception ex)
        {
            lblhmMSG.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnRecombinationRecombination_Click(object sender, EventArgs e)
    {
        try
        {
            lblerrorPopR.Text = "";
            Label4.Text = "";
            lblerrorPopR.Text = "";

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblName_RAR.Text = "Recombination & Reconstitution";
            lblDate_RAR.Text = txtDate.Text;
            lblShift_RAR.Text = ddlShift.SelectedItem.Text;
            lblsection_RAR.Text = ddlPSection.SelectedItem.Text;

            DataSet dsRR_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheet",
            new string[] { "flag", "Office_ID", "Date", "Shift_Id", "ProductSection_ID", "Ghee_RR", "Cream_RR", "Butter_RR", "SMP_RR" },
            new string[] { "1", objdb.Office_ID(), Fdate, ddlShift.SelectedValue, "2", objdb.LooseGheeItemTypeId_ID(), objdb.LooseCreamItemTypeId_ID(), objdb.LooseButterItemTypeId_ID(), objdb.LooseSMPItemTypeId_ID() }, "dataset");

            if (dsRR_Child != null && dsRR_Child.Tables[0].Rows.Count > 0)
            {
                GVRRSheet.DataSource = dsRR_Child.Tables[1];
                GVRRSheet.DataBind();

                if (dsRR_Child.Tables[1].Rows[0]["RR_Total"].ToString() == "0.00")
                {
                    btnGetTotal.Enabled = true;
                    btnsaveRR.Enabled = true;
                }
                else
                {
                    btnGetTotal.Enabled = false;
                    btnsaveRR.Enabled = false;
                }


            }

            else
            {
                GVRRSheet.DataSource = string.Empty;
                GVRRSheet.DataBind();
                btnGetTotal.Enabled = true;
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);

        }

        catch (Exception ex)
        {
            lblerrorPopR.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
        }

    }

    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        try
        {
            lblerrorPopR.Text = "";
            Label4.Text = "";
            lblerrorPopR.Text = "";

            foreach (GridViewRow row in GVRRSheet.Rows)
            {
                Label lblItemTypeName = (Label)row.FindControl("lblItemTypeName");
                Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

                TextBox txtBalance_BFRR = (TextBox)row.FindControl("txtBalance_BFRR");
                TextBox txtRRObtained = (TextBox)row.FindControl("txtRRObtained");
                TextBox txtRRTotal = (TextBox)row.FindControl("txtRRTotal");

                TextBox txtRRToning = (TextBox)row.FindControl("txtRRToning");
                TextBox txtRRMaintainingSNF = (TextBox)row.FindControl("txtRRMaintainingSNF");
                TextBox txtRRIssueforproductionsection = (TextBox)row.FindControl("txtRRIssueforproductionsection");
                TextBox txtRRTotalIssued = (TextBox)row.FindControl("txtRRTotalIssued");

                TextBox txtRRClosingBalance = (TextBox)row.FindControl("txtRRClosingBalance");

                if (txtBalance_BFRR.Text == "")
                {
                    txtBalance_BFRR.Text = "0";
                }
                if (txtRRObtained.Text == "")
                {
                    txtRRObtained.Text = "0";
                }

                txtRRTotal.Text = (Convert.ToDecimal(txtBalance_BFRR.Text) + Convert.ToDecimal(txtRRObtained.Text)).ToString();


                if (txtRRToning.Text == "")
                {
                    txtRRToning.Text = "0";
                }
                if (txtRRMaintainingSNF.Text == "")
                {
                    txtRRMaintainingSNF.Text = "0";
                }
                if (txtRRIssueforproductionsection.Text == "")
                {
                    txtRRIssueforproductionsection.Text = "0";
                }

                txtRRTotalIssued.Text = (Convert.ToDecimal(txtRRToning.Text) + Convert.ToDecimal(txtRRMaintainingSNF.Text) + Convert.ToDecimal(txtRRIssueforproductionsection.Text)).ToString();


                if (txtRRClosingBalance.Text == "")
                {
                    txtRRClosingBalance.Text = "0";
                }

                txtRRClosingBalance.Text = (Convert.ToDecimal(txtRRTotal.Text) - Convert.ToDecimal(txtRRTotalIssued.Text)).ToString();
                txtRRClosingBalance.Enabled = false;

                if (txtRRTotalIssued.Text != "" || txtRRTotal.Text != "")
                {
                    btnrorrsheet.Enabled = true;
                }



            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
        }
        catch (Exception ex)
        {
            lblerrorPopR.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
        }
    }

    private DataTable GetRRSheetData()
    {

        decimal RR_OpeningBalance = 0;
        decimal RR_Obtained = 0;
        decimal RR_Total = 0;
        decimal RR_Toning = 0;
        decimal RR_MaintainingSNF = 0;
        decimal RR_IssuedForProductSection = 0;
        decimal RR_TotalIssued = 0;
        decimal RR_ClosingBalance = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("RR_OpeningBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_Obtained", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_Total", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_Toning", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_MaintainingSNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_IssuedForProductSection", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_TotalIssued", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RR_ClosingBalance", typeof(decimal)));

        foreach (GridViewRow row in GVRRSheet.Rows)
        {

            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
            Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

            TextBox txtBalance_BFRR = (TextBox)row.FindControl("txtBalance_BFRR");
            TextBox txtRRObtained = (TextBox)row.FindControl("txtRRObtained");
            TextBox txtRRTotal = (TextBox)row.FindControl("txtRRTotal");

            TextBox txtRRToning = (TextBox)row.FindControl("txtRRToning");
            TextBox txtRRMaintainingSNF = (TextBox)row.FindControl("txtRRMaintainingSNF");
            TextBox txtRRIssueforproductionsection = (TextBox)row.FindControl("txtRRIssueforproductionsection");
            TextBox txtRRTotalIssued = (TextBox)row.FindControl("txtRRTotalIssued");
            TextBox txtRRClosingBalance = (TextBox)row.FindControl("txtRRClosingBalance");


            if (txtBalance_BFRR.Text != "0" && txtBalance_BFRR.Text != "0.00")
            {
                RR_OpeningBalance = Convert.ToDecimal(txtBalance_BFRR.Text);
            }
            else
            {
                RR_OpeningBalance = 0;
            }

            if (txtRRObtained.Text != "0" && txtRRObtained.Text != "0.00")
            {
                RR_Obtained = Convert.ToDecimal(txtRRObtained.Text);
            }
            else
            {
                RR_Obtained = 0;
            }

            if (txtRRTotal.Text != "0" && txtRRTotal.Text != "0.00")
            {
                RR_Total = Convert.ToDecimal(txtRRTotal.Text);
            }
            else
            {
                RR_Total = 0;
            }

            if (txtRRToning.Text != "0" && txtRRToning.Text != "0.00")
            {
                RR_Toning = Convert.ToDecimal(txtRRToning.Text);
            }
            else
            {
                RR_Toning = 0;
            }

            if (txtRRMaintainingSNF.Text != "0" && txtRRMaintainingSNF.Text != "0.00")
            {
                RR_MaintainingSNF = Convert.ToDecimal(txtRRMaintainingSNF.Text);
            }
            else
            {
                RR_MaintainingSNF = 0;
            }


            if (txtRRIssueforproductionsection.Text != "0" && txtRRIssueforproductionsection.Text != "0.00")
            {
                RR_IssuedForProductSection = Convert.ToDecimal(txtRRIssueforproductionsection.Text);
            }
            else
            {
                RR_IssuedForProductSection = 0;
            }


            if (txtRRTotalIssued.Text != "0" && txtRRTotalIssued.Text != "0.00")
            {
                RR_TotalIssued = Convert.ToDecimal(txtRRTotalIssued.Text);
            }
            else
            {
                RR_TotalIssued = 0;
            }

            if (txtRRClosingBalance.Text != "0" && txtRRClosingBalance.Text != "0.00")
            {
                RR_ClosingBalance = Convert.ToDecimal(txtRRClosingBalance.Text);
            }
            else
            {
                RR_ClosingBalance = 0;
            }

            dr = dt.NewRow();
            dr[0] = lblItemCat_id.Text;
            dr[1] = lblItemType_id.Text;
            dr[2] = RR_OpeningBalance;
            dr[3] = RR_Obtained;
            dr[4] = RR_Total;
            dr[5] = RR_Toning;
            dr[6] = RR_MaintainingSNF;
            dr[7] = RR_IssuedForProductSection;
            dr[8] = RR_TotalIssued;
            dr[9] = RR_ClosingBalance;
            dt.Rows.Add(dr);

        }

        return dt;
    }

    protected void btnsaveRR_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblerrorPopR.Text = "";
                DataTable dtIDF = new DataTable();
                dtIDF = GetRRSheetData();

                if (dtIDF.Rows.Count > 0)
                {
                    ds = null;
                    ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheet",
                                              new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"Shift_Id" 
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP" 
                                    },
                                              new string[] { "2"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,"2" 
                                              ,ddlShift.SelectedValue 
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress() 
                                    },
                                             new string[] { "type_Production_Milk_InOutProcessRRSheet" },
                                             new DataTable[] { dtIDF }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblerrorPopR.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);

                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblerrorPopR.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
                    }

                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblerrorPopR.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
                    }

                }
                else
                {
                    lblerrorPopR.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
                }


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);

            }
        }

        catch (Exception ex)
        {
            lblerrorPopR.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RecombinationReconstitution_Final()", true);
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
            lblerrorPopCW.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }


    protected void btnLtrTanker_Click(object sender, EventArgs e)
    {
        lblmsgTanker.Text = "";

        if (txtQtyInLtrTanker.Text != "")
        {
            AddTankerDetails();
        }
        else
        {
            lblmsgTanker.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Qty Can't Blank");
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
    }

    private void AddTankerDetails()
    {
        try
        {
            lblmsgTanker.Text = "";
            lblmsg_issue.Text = "";

            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));

                dr1 = dt1.NewRow();
                dr1[0] = ddlTankerNo.SelectedValue;
                if (ddlTankerNo.SelectedValue == "-1")
                {
                    dr1[1] = txtTankerNo_Receipt.Text;
                }
                else
                {
                    dr1[1] = ddlTankerNo.SelectedItem.Text;

                }
                dr1[2] = txtQtyInLtrTanker.Text;
                dt1.Rows.Add(dr1);
                ViewState["InsertRecord1"] = dt1;
                GV_TDetail_WM_Receipt.DataSource = dt1;
                GV_TDetail_WM_Receipt.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                DataTable DT1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord1"];

                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    if (ddlTankerNo.SelectedValue == DT1.Rows[i]["I_TankerID"].ToString())
                    {
                        if (ddlTankerNo.SelectedValue == "-1")
                        {
                            
                        }
                        else
                        {
                            CompartmentType = 1;
                        }
                        
                    }
                }
                if (CompartmentType == 1)
                {
                    lblmsgTanker.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ddlTankerNo.SelectedItem.Text + "\" already exist.");

                }
                else
                {
                    dr1 = dt1.NewRow();
                    dr1[0] = ddlTankerNo.SelectedValue;
                    if (ddlTankerNo.SelectedValue == "-1")
                    {
                        dr1[1] = txtTankerNo_Receipt.Text;
                    }
                    else
                    {
                        dr1[1] = ddlTankerNo.SelectedItem.Text;

                    }
                    dr1[2] = txtQtyInLtrTanker.Text;
                    dt1.Rows.Add(dr1);
                }

                foreach (DataRow tr in DT1.Rows)
                {
                    dt1.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord1"] = dt1;
                GV_TDetail_WM_Receipt.DataSource = dt1;
                GV_TDetail_WM_Receipt.DataBind();
            }

            ddlTankerNo.ClearSelection();
            txtQtyInLtrTanker.Text = "";
            txtTankerNo_Receipt.Text = "";
            divTankerNameReceipt.Visible = false;

        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsgTanker.Text = "";
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord1"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord1"] = dt2;
            GV_TDetail_WM_Receipt.DataSource = dt2;
            GV_TDetail_WM_Receipt.DataBind();
            ddlTankerNo.ClearSelection();
            txtQtyInLtrTanker.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }



    protected void btn_Add_Issue_Click(object sender, EventArgs e)
    {

        lblmsg_issue.Text = "";

        if (txtQtyInLtrTanker_Issue.Text != "")
        {
            AddTankerDetails_Issue();
        }
        else
        {
            lblmsg_issue.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Qty Can't Blank");
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);

    }

    private void AddTankerDetails_Issue()
    {
        try
        {
            lblmsgTanker.Text = "";
            lblmsg_issue.Text = "";

            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord2"]) == null || Convert.ToString(ViewState["InsertRecord2"]) == "")
            {
                DataTable dt11 = new DataTable();
                DataRow dr11;
                dt11.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
                dt11.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
                dt11.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));

                dr11 = dt11.NewRow();
                dr11[0] = ddlTankerNo_Issue.SelectedValue;
                if(ddlTankerNo_Issue.SelectedValue == "-1")
                {
                    dr11[1] = txtTankerNo_Issue.Text;
                }
                else
                {
                    dr11[1] = ddlTankerNo_Issue.SelectedItem.Text;

                }
                
                dr11[2] = txtQtyInLtrTanker_Issue.Text;
                dt11.Rows.Add(dr11);
                ViewState["InsertRecord2"] = dt11;
                GV_TDetail_WM_Issued.DataSource = dt11;
                GV_TDetail_WM_Issued.DataBind();
            }
            else
            {
                DataTable dt11 = new DataTable();
                DataTable DT11 = new DataTable();
                DataRow dr11;
                dt11.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
                dt11.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
                dt11.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));

                DT11 = (DataTable)ViewState["InsertRecord2"];

                for (int i = 0; i < DT11.Rows.Count; i++)
                {
                    if (ddlTankerNo_Issue.SelectedValue == DT11.Rows[i]["I_TankerID"].ToString())
                    {
                        if (ddlTankerNo_Issue.SelectedValue == "-1")
                        {

                        }
                        else
                        {
                            CompartmentType = 1;
                        }
                    }
                }
                if (CompartmentType == 1)
                {
                    lblmsg_issue.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ddlTankerNo_Issue.SelectedItem.Text + "\" already exist.");

                }
                else
                {
                    dr11 = dt11.NewRow();
                    dr11[0] = ddlTankerNo_Issue.SelectedValue;
                    if (ddlTankerNo_Issue.SelectedValue == "-1")
                    {
                        dr11[1] = txtTankerNo_Issue.Text;
                    }
                    else
                    {
                        dr11[1] = ddlTankerNo_Issue.SelectedItem.Text;

                    }
                    dr11[2] = txtQtyInLtrTanker_Issue.Text;
                    dt11.Rows.Add(dr11);
                }

                foreach (DataRow tr in DT11.Rows)
                {
                    dt11.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord2"] = dt11;
                GV_TDetail_WM_Issued.DataSource = dt11;
                GV_TDetail_WM_Issued.DataBind();
            }

            ddlTankerNo_Issue.ClearSelection();
            txtQtyInLtrTanker_Issue.Text = "";
            divTankerName.Visible = false;
            txtTankerNo_Issue.Text = "";

        }
        catch (Exception ex)
        {
            lblmsg_issue.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDelete_Issue_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg_issue.Text = "";
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord2"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord2"] = dt2;
            GV_TDetail_WM_Issued.DataSource = dt2;
            GV_TDetail_WM_Issued.DataBind();
            ddlTankerNo_Issue.ClearSelection();
            txtQtyInLtrTanker_Issue.Text = "";
             Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        }
        catch (Exception ex)
        {
            lblmsg_issue.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }


    // For Container

    protected void btnaddContainer_Click(object sender, EventArgs e)
    {

        lblmsgContainer.Text = "";

        if (txtQtyInLtr.Text != "")
        {
            AddContainerDetails();
            ClosingBalanceTotal();
        }
        else
        {
            lblmsgContainer.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Qty Can't Blank");
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);

    }

    private void AddContainerDetails()
    {
        try
        {
            lblmsgContainer.Text = "";

            int CompartmentType = 0;


            if (Convert.ToString(ViewState["InsertRecord3"]) == null || Convert.ToString(ViewState["InsertRecord3"]) == "")
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("I_MCID", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_MCName", typeof(string)));
                dt1.Columns.Add(new DataColumn("QtyInKg", typeof(string)));

                dr1 = dt1.NewRow();
                dr1[0] = ddlContainer.SelectedValue;
                dr1[1] = ddlContainer.SelectedItem.Text;
                dr1[2] = txtQtyInLtr.Text;
                dt1.Rows.Add(dr1);
                ViewState["InsertRecord3"] = dt1;
                GV_CD_WM.DataSource = dt1;
                GV_CD_WM.DataBind();

            }
            else
            {
                DataTable dt1 = new DataTable();
                DataTable DT1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("I_MCID", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_MCName", typeof(string)));
                dt1.Columns.Add(new DataColumn("QtyInKg", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord3"];

                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    if (ddlContainer.SelectedValue == DT1.Rows[i]["I_MCID"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblmsgContainer.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ddlContainer.SelectedItem.Text + "\" already exist.");

                }
                else
                {
                    dr1 = dt1.NewRow();
                    dr1[0] = ddlContainer.SelectedValue;
                    dr1[1] = ddlContainer.SelectedItem.Text;
                    dr1[2] = txtQtyInLtr.Text;
                    dt1.Rows.Add(dr1);
                }

                foreach (DataRow tr in DT1.Rows)
                {
                    dt1.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord3"] = dt1;
                GV_CD_WM.DataSource = dt1;
                GV_CD_WM.DataBind();
            }

            ddlContainer.ClearSelection();
            txtQtyInLtr.Text = "";

        }
        catch (Exception ex)
        {
            lblmsgContainer.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDelete_Click1(object sender, EventArgs e)
    {
        try
        {
            lblmsgContainer.Text = "";
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord3"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord3"] = dt2;
            GV_CD_WM.DataSource = dt2;
            GV_CD_WM.DataBind();
            ddlContainer.ClearSelection();
            txtQtyInLtr.Text = "";

            ClosingBalanceTotal();
        }
        catch (Exception ex)
        {
            lblmsgContainer.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private void ClosingBalanceTotal()
    {

        try
        {
            txtWMSClosingBalance.Text = "";

            decimal CL_HMS = 0;

            foreach (GridViewRow row in GV_CD_WM.Rows)
            {

                TextBox txtQtyInKgCM = (TextBox)row.FindControl("txtQtyInKg");

                if (txtQtyInKgCM.Text != "" && txtQtyInKgCM.Text != "0" && txtQtyInKgCM.Text != "0.0" && txtQtyInKgCM.Text != "0.00")
                {
                    CL_HMS += Convert.ToDecimal(txtQtyInKgCM.Text);
                }

            }

            txtWMSClosingBalance.Text = CL_HMS.ToString("0.00");
            txtWMSClosingBalance.Enabled = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        }
        catch (Exception ex)
        {
            lblhmMSG.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    // New Cow Milk Sheet

    protected void lblCowMilkSheet_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lbltitle_cwm.Text = "Cow Milk";
            lbldate_cwm.Text = txtDate.Text;
            lblshift_cwm.Text = ddlShift.SelectedItem.Text;
            lblsection_cwm.Text = ddlPSection.SelectedItem.Text;

            DataSet dsCW_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheet",
            new string[] { "flag", "Office_ID", "Date", "Shift_Id", "ProductSection_ID", "ItemType_id" },
            new string[] { "3", objdb.Office_ID(), Fdate, ddlShift.SelectedValue, "2", objdb.CowMilkItemTypeId_ID() }, "dataset");

            if (dsCW_Child != null && dsCW_Child.Tables[1].Rows.Count > 0)
            {
                GVCW_Sheet.DataSource = dsCW_Child.Tables[1];
                GVCW_Sheet.DataBind();

                if (dsCW_Child.Tables[1].Rows[0]["CM_TotalIssued"].ToString() != "0.00" && dsCW_Child.Tables[1].Rows[0]["CM_TotalIssued"].ToString() != "")
                {
                    btnCWGetTotal.Enabled = false;
                }
                else
                {
                    btnCWGetTotal.Enabled = true;
                }

            }

            else
            {

                GVCW_Sheet.DataSource = string.Empty;
                GVCW_Sheet.DataBind();
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnCWGetTotal_Click(object sender, EventArgs e)
    {
        try
        {
            lblerrorPopR.Text = "";

            foreach (GridViewRow row in GVCW_Sheet.Rows)
            {
                Label lblItemTypeName = (Label)row.FindControl("lblItemTypeName");
                Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

                TextBox txtBalance_BFCw = (TextBox)row.FindControl("txtBalance_BFCw");
                TextBox txtCWPrepard = (TextBox)row.FindControl("txtCWPrepard");
                TextBox txtCWTotal = (TextBox)row.FindControl("txtCWTotal");

                TextBox txtCWSale = (TextBox)row.FindControl("txtCWSale");
                TextBox txtCWIssuedTowm = (TextBox)row.FindControl("txtCWIssuedTowm");
                TextBox txtCWLoss = (TextBox)row.FindControl("txtCWLoss");
                TextBox txtCWClosingBalance = (TextBox)row.FindControl("txtCWClosingBalance");
                TextBox txtCWTotalIssued = (TextBox)row.FindControl("txtCWTotalIssued");

                if (txtBalance_BFCw.Text == "")
                {
                    txtBalance_BFCw.Text = "0";
                }
                if (txtCWPrepard.Text == "")
                {
                    txtCWPrepard.Text = "0";
                }

                txtCWTotal.Text = (Convert.ToDecimal(txtBalance_BFCw.Text) + Convert.ToDecimal(txtCWPrepard.Text)).ToString();


                if (txtCWSale.Text == "")
                {
                    txtCWSale.Text = "0";
                }
                if (txtCWIssuedTowm.Text == "")
                {
                    txtCWIssuedTowm.Text = "0";
                }
                if (txtCWLoss.Text == "")
                {
                    txtCWLoss.Text = "0";
                }

                if (txtCWClosingBalance.Text == "")
                {
                    txtCWClosingBalance.Text = "0";
                }

                txtCWTotalIssued.Text = (Convert.ToDecimal(txtCWSale.Text) + Convert.ToDecimal(txtCWIssuedTowm.Text) + Convert.ToDecimal(txtCWLoss.Text) + Convert.ToDecimal(txtCWClosingBalance.Text)).ToString();

                if (txtCWTotalIssued.Text != "" || txtCWTotalIssued.Text != "")
                {
                    btnrocwsheet.Enabled = true;
                }



            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);
        }
        catch (Exception ex)
        {
            lblerrorPopR.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);
        }
    }

    private DataTable GetCWSheetData()
    {

        decimal CM_OpeningBalance = 0;
        decimal CM_Prepard = 0;
        decimal CM_Total = 0;
        decimal CM_Sale = 0;
        decimal CM_IssuedTowm = 0;
        decimal CM_Loss = 0;
        decimal CM_ClosingBalance = 0;
        decimal CM_TotalIssued = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("CM_OpeningBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_Prepard", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_Total", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_Sale", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_IssuedTowm", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_Loss", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_ClosingBalance", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CM_TotalIssued", typeof(decimal)));

        foreach (GridViewRow row in GVCW_Sheet.Rows)
        {

            Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
            Label lblItemType_id = (Label)row.FindControl("lblItemType_id");

            TextBox txtBalance_BFCw = (TextBox)row.FindControl("txtBalance_BFCw");
            TextBox txtCWPrepard = (TextBox)row.FindControl("txtCWPrepard");
            TextBox txtCWTotal = (TextBox)row.FindControl("txtCWTotal");

            TextBox txtCWSale = (TextBox)row.FindControl("txtCWSale");
            TextBox txtCWIssuedTowm = (TextBox)row.FindControl("txtCWIssuedTowm");
            TextBox txtCWLoss = (TextBox)row.FindControl("txtCWLoss");
            TextBox txtCWClosingBalance = (TextBox)row.FindControl("txtCWClosingBalance");
            TextBox txtCWTotalIssued = (TextBox)row.FindControl("txtCWTotalIssued");



            if (txtBalance_BFCw.Text != "0" && txtBalance_BFCw.Text != "0.00")
            {
                CM_OpeningBalance = Convert.ToDecimal(txtBalance_BFCw.Text);
            }
            else
            {
                CM_OpeningBalance = 0;
            }

            if (txtCWPrepard.Text != "0" && txtCWPrepard.Text != "0.00")
            {
                CM_Prepard = Convert.ToDecimal(txtCWPrepard.Text);
            }
            else
            {
                CM_Prepard = 0;
            }

            if (txtCWTotal.Text != "0" && txtCWTotal.Text != "0.00")
            {
                CM_Total = Convert.ToDecimal(txtCWTotal.Text);
            }
            else
            {
                CM_Total = 0;
            }

            if (txtCWSale.Text != "0" && txtCWSale.Text != "0.00")
            {
                CM_Sale = Convert.ToDecimal(txtCWSale.Text);
            }
            else
            {
                CM_Sale = 0;
            }

            if (txtCWIssuedTowm.Text != "0" && txtCWIssuedTowm.Text != "0.00")
            {
                CM_IssuedTowm = Convert.ToDecimal(txtCWIssuedTowm.Text);
            }
            else
            {
                CM_IssuedTowm = 0;
            }


            if (txtCWLoss.Text != "0" && txtCWLoss.Text != "0.00")
            {
                CM_Loss = Convert.ToDecimal(txtCWLoss.Text);
            }
            else
            {
                CM_Loss = 0;
            }


            if (txtCWClosingBalance.Text != "0" && txtCWClosingBalance.Text != "0.00")
            {
                CM_ClosingBalance = Convert.ToDecimal(txtCWClosingBalance.Text);
            }
            else
            {
                CM_ClosingBalance = 0;
            }

            if (txtCWTotalIssued.Text != "0" && txtCWTotalIssued.Text != "0.00")
            {
                CM_TotalIssued = Convert.ToDecimal(txtCWTotalIssued.Text);
            }
            else
            {
                CM_TotalIssued = 0;
            }



            dr = dt.NewRow();
            dr[0] = lblItemCat_id.Text;
            dr[1] = lblItemType_id.Text;
            dr[2] = CM_OpeningBalance;
            dr[3] = CM_Prepard;
            dr[4] = CM_Total;
            dr[5] = CM_Sale;
            dr[6] = CM_IssuedTowm;
            dr[7] = CM_Loss;
            dr[8] = CM_ClosingBalance;
            dr[9] = CM_TotalIssued;
            dt.Rows.Add(dr);


        }

        return dt;
    }

    protected void btnCwS_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblerrorPopCW.Text = "";
                DataTable dtICW = new DataTable();
                dtICW = GetCWSheetData();

                if (dtICW.Rows.Count > 0)
                {
                    ds = null;
                    ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheet",
                                              new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"Shift_Id" 
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP" 
                                    },
                                              new string[] { "4"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,"2" 
                                              ,ddlShift.SelectedValue 
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress() 
                                    },
                                             new string[] { "type_Production_Milk_InOutProcessCWSheet" },
                                             new DataTable[] { dtICW }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblerrorPopCW.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);

                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblerrorPopCW.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);
                    }

                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblerrorPopCW.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);
                    }

                }
                else
                {
                    lblerrorPopCW.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);
                }


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);

            }
        }

        catch (Exception ex)
        {
            lblerrorPopCW.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CowMilk_Final()", true);
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
            lblerrorPopCW.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void ddlTankerNo_Issue_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg_issue.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        divTankerName.Visible = false;
        if(ddlTankerNo_Issue.SelectedIndex > 0)
        {
            if(ddlTankerNo_Issue.SelectedValue == "-1")
            {
                divTankerName.Visible = true;
            }
            else
            {
                divTankerName.Visible = false;
            }
        }
    }
    protected void ddlTankerNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsgTanker.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Final()", true);
        divTankerNameReceipt.Visible = false;
        if (ddlTankerNo.SelectedIndex > 0)
        {
            if (ddlTankerNo.SelectedValue == "-1")
            {
                divTankerNameReceipt.Visible = true;
            }
            else
            {
                divTankerNameReceipt.Visible = false;
            }
        }
    }
}