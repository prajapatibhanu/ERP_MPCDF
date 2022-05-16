using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


public partial class mis_dailyplan_EditMilkProductionEntry_frm : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    GetSectionView();
                    FillShift();
                    btnNext.Visible = false;
                    btnPopupSave.Visible = false;
                    if (Session["ItemName"].ToString() != "" && Session["ItemName"].ToString() != null && Session["ItemTypeId"].ToString() != "" && Session["ItemTypeId"].ToString() != null && Session["Date"].ToString() != "" && Session["Date"].ToString() != null && Session["Shift"].ToString() != "" && Session["Shift"].ToString() != null)
                    {

                        string ItemTypeId = Session["ItemTypeId"].ToString();
                        string Date = Session["Date"].ToString();
                        string Shift = Session["Shift"].ToString();
                        string ItemName = Session["ItemName"].ToString();

                        lblvname.Text = "[ " + ItemName + " ]";
                        txtDate.Text = Date;
                        ddlShift.SelectedValue = Shift;
                        txtDate.Enabled = false;
                        ddlShift.Enabled = false;

                        DDSHEET_Info(ItemTypeId);
                        FillContainer();
                        CreateDataTable();
                        

                    }
                    else
                    {
                        Response.Redirect("~/MilkProductionEntry.aspx");
                    }

                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillContainer()
    {

        ds = objdb.ByProcedure("SpProductionProduct_Milk_InOutEdit", new string[] { "flag", "Office_ID" }, new string[] {"3",objdb.Office_ID() }, "dataset");
        if(ds != null && ds.Tables.Count > 0)
        {
            if(ds.Tables[0].Rows.Count > 0)
            {
                ddlContainer.DataSource = ds.Tables[0];
                ddlContainer.DataTextField = "V_MCName";
                ddlContainer.DataValueField = "I_MCID";
                ddlContainer.DataBind();
                ddlContainer.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlContainer.Items.Insert(0, new ListItem("Select", "0"));
            }
            if (ViewState["TypeId"].ToString() == objdb.SkimmedMilkItemTypeId_ID())
            {
                // For Tanker 
                if (ds.Tables[1].Rows.Count > 0)
                {
                    //gv_SMTDetails1.DataSource = dsVD_Child.Tables[2];
                    //gv_SMTDetails1.DataBind();



                    ddlTankerNo.DataSource = ds.Tables[1];
                    ddlTankerNo.DataTextField = "V_VehicleNo";
                    ddlTankerNo.DataValueField = "I_TankerID";
                    ddlTankerNo.DataBind();
                    ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));


                }
                else
                {
                    
                    ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                }

            }
            else
            {
               
                ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void DDSHEET_Info(string ItemtypeID)
    {
        try
        {
            ViewState["InsertRecord2"] = null;
            btnPopupSave.Enabled = false;

            ViewState["TypeId"] = ItemtypeID;
			if (ItemtypeID == "87")
            {
                spnPreparedtext.InnerHtml = "Prepared for SMP";
                trPrepared.Visible = true;
            }
            else
            {
                spnPreparedtext.InnerHtml = "Prepared";
                trPrepared.Visible = false;
            }
            if (ItemtypeID == "59")
            {
                txtIssuedforProductions.Text = "0";
                txtIssuedforProductions.Enabled = true;
                txtIssuedforProductions.ToolTip = "";
            }
            else
            {
                txtIssuedforProductions.Text = "0";
                txtIssuedforProductions.Enabled = false;
                txtIssuedforProductions.ToolTip = "This Field Enable Only For STD Milk!";
            }


            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblMsg.Text = "";

            DataSet dsVD = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                  new string[] { "10", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, ItemtypeID }, "dataset");

            if (dsVD != null && dsVD.Tables[0].Rows.Count > 0)
            {
                if (dsVD.Tables[1].Rows.Count > 0)
                {
                    ViewState["MilkProductionProcess_Id"] = dsVD.Tables[1].Rows[0]["MilkProductionProcess_Id"].ToString();
                    ViewState["Mult_MilkProductionProcess_Id"] += "," + dsVD.Tables[1].Rows[0]["MilkProductionProcess_Id"].ToString();
                }
                else
                {
                    Session["NoRecordFound"] = true;
                    Response.Redirect("EditMilkProductionEntry.aspx", false);
                }
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
				if (txtPreparedforSeparation.Text == "")
                {
                    txtPreparedforSeparation.Text = "0";
                }
                if (lblReturn.Text == "")
                {
                    lblReturn.Text = "0";
                }


                if (txtIssuedforstes.Text == "")
                {
                    txtIssuedforstes.Text = "0";
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

                if (txtIssuedforstesQtyinPkt1.Text == "")
                {
                    txtIssuedforstesQtyinPkt1.Text = "0";
                }
                if (txtIssuedforstesQtyinPkt2.Text == "")
                {
                    txtIssuedforstesQtyinPkt2.Text = "0";
                }
                if (txtIssuedforstesQtyinPkt3.Text == "")
                {
                    txtIssuedforstesQtyinPkt3.Text = "0";
                }
                if (txtIssuedforstesQtyinPkt4.Text == "")
                {
                    txtIssuedforstesQtyinPkt4.Text = "0";
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


                // For Variant

                DataSet dsVD_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                new string[] { "11", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, ItemtypeID }, "dataset");

                if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
                {
                    
                    GVVariantDetail.DataSource = dsVD_Child.Tables[0];
                    GVVariantDetail.DataBind();

                }

                else
                {
                    GVVariantDetail.DataSource = string.Empty;
                    GVVariantDetail.DataBind();
                }

                // For Container 
                if (dsVD_Child != null && dsVD_Child.Tables[1].Rows.Count > 0)
                {

                    //gvMilkinContainer1.DataSource = dsVD_Child.Tables[1];
                    //gvMilkinContainer1.DataBind();


                  //ddlContainer.DataSource = dsVD_Child.Tables[1];
                  //ddlContainer.DataTextField = "V_MCName";
                  //ddlContainer.DataValueField = "I_MCID";
                  //ddlContainer.DataBind();
                  //ddlContainer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    gvMilkinContainer.DataSource = string.Empty;
                    gvMilkinContainer.DataBind();
                    //ddlContainer.Items.Insert(0, new ListItem("Select", "0"));
                }

                if (ItemtypeID == objdb.SkimmedMilkItemTypeId_ID())
                {
                    // For Tanker 
                    if (dsVD_Child != null && dsVD_Child.Tables[2].Rows.Count > 0)
                    {
                        //gv_SMTDetails1.DataSource = dsVD_Child.Tables[2];
                        //gv_SMTDetails1.DataBind();



                        //ddlTankerNo.DataSource = dsVD_Child.Tables[2];
                        //ddlTankerNo.DataTextField = "V_VehicleNo";
                        //ddlTankerNo.DataValueField = "I_TankerID";
                        //ddlTankerNo.DataBind();
                        //ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));


                    }
                    else
                    {
                        gv_SMTDetails.DataSource = string.Empty;
                        gv_SMTDetails.DataBind();
                        //ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                    }

                }
                else
                {
                    txtIssuedforstes2.Enabled = true;
                    txtIssuedforstes3.Enabled = true;
                    txtIssuedforstes4.Enabled = true;


                    gv_SMTDetails.DataSource = string.Empty;
                    gv_SMTDetails.DataBind();
                    //ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                }



                DataSet dsVDUpdated = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID", "ItemType_id" },
                  new string[] { "13", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue, ItemtypeID }, "dataset");

                if (dsVDUpdated != null && dsVDUpdated.Tables[0].Rows.Count > 0)
                {
                    btnPopupSave.Enabled = false;

                    lblopeningBalance.Text = dsVDUpdated.Tables[0].Rows[0]["OpeningMilk"].ToString();
                    lblReceived.Text = dsVDUpdated.Tables[0].Rows[0]["ReceivedMilk"].ToString();
                    txtPrepared.Text = dsVDUpdated.Tables[0].Rows[0]["PreparedMilk"].ToString();
					if (dsVDUpdated.Tables[0].Rows[0]["PreparedMilkforSeparation"].ToString() == "")
                    {
                        txtPreparedforSeparation.Text = "0";
                    }
                    else
                    {
                        txtPreparedforSeparation.Text = dsVDUpdated.Tables[0].Rows[0]["PreparedMilkforSeparation"].ToString();
                    }
                    lblReturn.Text = dsVDUpdated.Tables[0].Rows[0]["ReturnMilk"].ToString();

                    txtPrepared.Enabled = true;
                    txtPreparedforSeparation.Enabled = true;
                    lblReturn.Enabled = true;

                    txtIssuedforstes2.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSale2"].ToString();
                    txtIssuedforstes3.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSale3"].ToString();
                    txtIssuedforstes4.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSale4"].ToString();


                    txtIssuedforstes.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForSates"].ToString();
                    txtIssuedforWH.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForWholeMilk"].ToString();
                    txtIssuedforProductions.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedForProductSection"].ToString();
                    txtLosses.Text = dsVDUpdated.Tables[0].Rows[0]["Losses"].ToString();
                    txtClosingBalance.Text = dsVDUpdated.Tables[0].Rows[0]["ClosingBalance"].ToString();

                    txtIssuedforstes2.Enabled = true;
                    txtIssuedforstes3.Enabled = true;
                    txtIssuedforstes4.Enabled = true;

                    txtIssuedforstes.Enabled = true;
                    txtIssuedforWH.Enabled = true;
                    //txtIssuedforProductions.Enabled = false;
                    txtLosses.Enabled = true;
                    txtClosingBalance.Enabled = true;

                    lblReceiptTotal.Text = dsVDUpdated.Tables[0].Rows[0]["ReceiptMilkNetQty"].ToString();
                    lblIssuedtotal.Text = dsVDUpdated.Tables[0].Rows[0]["IssuedMilkNetQty"].ToString();

                    btngettotalvarient.Enabled = true;

                    FSContainer.Visible = true;
                    FSTanker.Visible = true;

                    if (dsVD_Child != null && dsVD_Child.Tables[1].Rows.Count > 0)
                    {

                        gvMilkinContainer.DataSource = dsVD_Child.Tables[1];
                        gvMilkinContainer.DataBind();
                        DataTable dt1 = new DataTable();
                        DataRow dr1;
                        dt1.Columns.Add(new DataColumn("I_MCID", typeof(int)));
                        dt1.Columns.Add(new DataColumn("V_MCName", typeof(string)));
                        dt1.Columns.Add(new DataColumn("QtyInKg", typeof(string)));
                        
                        int count = dsVD_Child.Tables[1].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string  I_MCID = dsVD_Child.Tables[1].Rows[i]["I_MCID"].ToString();
                            string  V_MCName = dsVD_Child.Tables[1].Rows[i]["V_MCName"].ToString();
                            string QtyInKg = dsVD_Child.Tables[1].Rows[i]["QtyInKg"].ToString();
                            dt1.Rows.Add(I_MCID, V_MCName, QtyInKg);
                           
                            
                        }

                        
                       
                       
                        ViewState["InsertRecord2"] = dt1;
                       


                    }
                    else
                    {
                        gvMilkinContainer.DataSource = string.Empty;
                        gvMilkinContainer.DataBind();
                    }

                    if (ItemtypeID == objdb.SkimmedMilkItemTypeId_ID())
                    {
                        if (dsVD_Child != null && dsVD_Child.Tables[2].Rows.Count > 0)
                        {
                            gv_SMTDetails.DataSource = dsVD_Child.Tables[2];
                            gv_SMTDetails.DataBind();
                            DataTable dt1 = new DataTable();
                            DataRow dr1;
                            dt1.Columns.Add(new DataColumn("I_TankerID", typeof(int)));
                            dt1.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
                            dt1.Columns.Add(new DataColumn("QtyInLtr", typeof(string)));

                            int count = dsVD_Child.Tables[1].Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                string I_TankerID = dsVD_Child.Tables[2].Rows[i]["I_TankerID"].ToString();
                                string V_VehicleNo = dsVD_Child.Tables[2].Rows[i]["V_VehicleNo"].ToString();
                                string QtyInLtr = dsVD_Child.Tables[2].Rows[i]["QtyInLtr"].ToString();
                                dt1.Rows.Add(I_TankerID, V_VehicleNo, QtyInLtr);


                            }




                            ViewState["InsertRecord1"] = dt1;
                        }
                        else
                        {
                            gv_SMTDetails.DataSource = string.Empty;
                            gv_SMTDetails.DataBind();
                        }
                    }


                }
                else
                {
                    FSContainer.Visible = true;

                    if (ItemtypeID == objdb.SkimmedMilkItemTypeId_ID())
                    {
                        FSTanker.Visible = true;
                    }
                    else
                    {
                        FSTanker.Visible = false;
                    }



                    //gvMilkinContainer1.DataSource = string.Empty;
                    //gvMilkinContainer1.DataBind();
                    //gv_SMTDetails1.DataSource = string.Empty;
                    //gv_SMTDetails1.DataBind();


                    btngettotalvarient.Enabled = true;
                    txtIssuedforstes2.Enabled = true;
                    txtIssuedforstes3.Enabled = true;
                    txtIssuedforstes4.Enabled = true;

                    //btnPopupSave.Enabled = true;
                    txtIssuedforstes.Enabled = true;
                    txtIssuedforWH.Enabled = true;
                    //txtIssuedforProductions.Enabled = true;
                    txtLosses.Enabled = true;
                    txtClosingBalance.Enabled = true;
                    txtPrepared.Enabled = true;
                    txtPreparedforSeparation.Enabled = true;
                    lblReturn.Enabled = true;

                }

                if (dsVDUpdated != null && dsVDUpdated.Tables[1].Rows.Count > 0)
                {
                    lblopeningBalance.Text = dsVDUpdated.Tables[1].Rows[0]["OpeningMilkQty"].ToString();

                }

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

                if (ItemtypeID == objdb.SkimmedMilkItemTypeId_ID())
                {
                    if (dsVDUpdated != null && dsVDUpdated.Tables[4].Rows.Count > 0)
                    {
                        gv_SMTDetails_Opening.DataSource = dsVDUpdated.Tables[4];
                        gv_SMTDetails_Opening.DataBind();
                    }
                    else
                    {
                        gv_SMTDetails_Opening.DataSource = string.Empty;
                        gv_SMTDetails_Opening.DataBind();
                    }

                }
                else
                {
                    gv_SMTDetails_Opening.DataSource = string.Empty;
                    gv_SMTDetails_Opening.DataBind();
                }
                //decimal MCQ_Opening = 0;

                //if (dsVDUpdated != null && dsVDUpdated.Tables[3].Rows.Count > 0)
                //{
                //    MCQ_Opening = Convert.ToDecimal(dsVDUpdated.Tables[3].Rows[0]["SumQty"].ToString());
                //}


                lblReceiptTotal.Text = (Convert.ToDecimal(lblopeningBalance.Text) + Convert.ToDecimal(lblReceived.Text) +
                   Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(txtPreparedforSeparation.Text) + Convert.ToDecimal(lblReturn.Text)).ToString();

                decimal VariantMCQ = 0;
                decimal VariantMCQ_Total = 0;

                foreach (GridViewRow row in GVVariantDetail.Rows)
                {
                    Label lblPackagingSize = (Label)row.FindControl("lblPackagingSize");
                    Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
                    TextBox txtQtyInPkt = (TextBox)row.FindControl("txtQtyInPkt");
                    Label lblQtyInLtr = (Label)row.FindControl("lblQtyInLtr");

                    if (txtQtyInPkt.Text != "0" && txtQtyInPkt.Text != "0.00" && txtQtyInPkt.Text != "0.0" && txtQtyInPkt.Text != "")
                    {
                        if (lblPackagingSize.Text != "0" && lblPackagingSize.Text != "0.00" && lblPackagingSize.Text != "0.0" && lblPackagingSize.Text != "")
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

                    VariantMCQ_Total += VariantMCQ;

                }


                decimal MCQ = 0;

                foreach (GridViewRow row in gvMilkinContainer.Rows)
                {
                    TextBox txtQtyInKg = (TextBox)row.FindControl("txtQtyInKg");
                    //Label txtQtyInKg = (Label)row.FindControl("txtQtyInKg");

                    if (txtQtyInKg.Text != "0" && txtQtyInKg.Text != "0.00" && txtQtyInKg.Text != "0.0" && txtQtyInKg.Text != "")
                    {
                        MCQ += Convert.ToDecimal(txtQtyInKg.Text);
                    }
                }

                decimal SMPD = 0;

                if (ItemtypeID == objdb.SkimmedMilkItemTypeId_ID())
                {
                    //FSTanker.Visible = true;
                    //FSContainer.Visible = true;
                    txtIssuedforstes.Enabled = false;
                    txtIssuedforstes2.Enabled = false;
                    txtIssuedforstes3.Enabled = false;
                    txtIssuedforstes4.Enabled = false;

                    ddlVariant2.Enabled = false;
                    ddlVariant3.Enabled = false;
                    ddlVariant4.Enabled = false;
                    txtIssuedforstesQtyinPkt2.Enabled = false;
                    txtIssuedforstesQtyinPkt3.Enabled = false;
                    txtIssuedforstesQtyinPkt4.Enabled = false;

                    foreach (GridViewRow row_SM in gv_SMTDetails.Rows)
                    {
                        TextBox txtQtyInLtr = (TextBox)row_SM.FindControl("txtQtyInLtr");

                        if (txtQtyInLtr.Text != "")
                        {
                            SMPD += Convert.ToDecimal(txtQtyInLtr.Text);
                        }
                    }

                    txtClosingBalance.Text = (MCQ + VariantMCQ_Total).ToString();
                    txtClosingBalance.Enabled = false;

                }
                else
                {
                    txtClosingBalance.Text = (MCQ + VariantMCQ_Total).ToString();
                    txtClosingBalance.Enabled = false;

                    //FSTanker.Visible = false;
                    //FSContainer.Visible = true;
                    txtIssuedforstes.Enabled = false;
                    txtIssuedforstes2.Enabled = false;
                    txtIssuedforstes3.Enabled = false;
                    txtIssuedforstes4.Enabled = false;

                    ddlVariant2.Enabled = true;
                    ddlVariant3.Enabled = true;
                    ddlVariant4.Enabled = true;
                    txtIssuedforstesQtyinPkt2.Enabled = true;
                    txtIssuedforstesQtyinPkt3.Enabled = true;
                    txtIssuedforstesQtyinPkt4.Enabled = true;

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
                   + Convert.ToDecimal(txtIssuedforstes4.Text) + SMPD).ToString();


            }
            DataSet dsNextData = objdb.ByProcedure("SpProductionProduct_Milk_InOutEdit", new string[] { "flag", "Date", "Office_ID", "ProductSection_ID", "Shift_Id", "ItemType_id" }, new string[] { "1", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID(), ddlPSection.SelectedValue, ddlShift.SelectedValue, ViewState["TypeId"].ToString() }, "dataset");
            if (dsNextData != null && dsNextData.Tables.Count > 0)
            {
                if (dsNextData.Tables[0].Rows.Count > 0)
                {
                    if (dsNextData.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        btnNext.Visible = true;
                        btnPopupSave.Visible = false;
                        ViewState["NextDate"] = dsNextData.Tables[0].Rows[0]["NextDate"].ToString();
                        ViewState["NextShift_Id"] = dsNextData.Tables[0].Rows[0]["NextShift_Id"].ToString();
                    }
                    else
                    {
                        btnNext.Visible = false;
                        btnPopupSave.Visible = true;
                    }
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlShift_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btngettotalvarient_Click(object sender, EventArgs e)
    {
        try
        {
            
            CalculationMilkP();
            btnPopupSave.Enabled = true;
            btnNext.Enabled = true;

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
            lblPopupMsg.Text = "";
            if (ViewState["TypeId"] == null)
            {
                Response.Redirect("EditMilkProductionEntry.aspx", true);
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
				if (txtPreparedforSeparation.Text == "")
                {
                    txtPreparedforSeparation.Text = "0";
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
                    Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(txtPreparedforSeparation.Text) + Convert.ToDecimal(lblReturn.Text)).ToString();


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

                decimal SMPD = 0;

                if (ViewState["TypeId"].ToString() == objdb.SkimmedMilkItemTypeId_ID())
                {
                    FSTanker.Visible = true;
                    FSContainer.Visible = true;
                    txtIssuedforstes.Enabled = false;
                    txtIssuedforstes2.Enabled = false;
                    txtIssuedforstes3.Enabled = false;
                    txtIssuedforstes4.Enabled = false;

                    ddlVariant2.Enabled = false;
                    ddlVariant3.Enabled = false;
                    ddlVariant4.Enabled = false;
                    txtIssuedforstesQtyinPkt2.Enabled = false;
                    txtIssuedforstesQtyinPkt3.Enabled = false;
                    txtIssuedforstesQtyinPkt4.Enabled = false;

                    foreach (GridViewRow row_SM in gv_SMTDetails.Rows)
                    {
                        TextBox txtQtyInLtr = (TextBox)row_SM.FindControl("txtQtyInLtr");

                        if (txtQtyInLtr.Text != "")
                        {
                            SMPD += Convert.ToDecimal(txtQtyInLtr.Text);
                        }

                    }

                    txtClosingBalance.Text = (MCQ + VariantMCQ_Total).ToString();
                    txtClosingBalance.Enabled = false;

                }
                else
                {
                    FSTanker.Visible = false;
                    FSContainer.Visible = true;
                    txtIssuedforstes.Enabled = false;
                    txtIssuedforstes2.Enabled = false;
                    txtIssuedforstes3.Enabled = false;
                    txtIssuedforstes4.Enabled = false;

                    ddlVariant2.Enabled = true;
                    ddlVariant3.Enabled = true;
                    ddlVariant4.Enabled = true;
                    txtIssuedforstesQtyinPkt2.Enabled = true;
                    txtIssuedforstesQtyinPkt3.Enabled = true;
                    txtIssuedforstesQtyinPkt4.Enabled = true;

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
                   + Convert.ToDecimal(txtIssuedforstes4.Text) + SMPD).ToString();
                ViewState["ClosingBalance"] = txtClosingBalance.Text;
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
    }
   
    //private DataTable GetMilkVarientInfo()
    //{

    //    decimal MQinpkt = 0;

    //    DataTable dt = new DataTable();
    //    DataRow dr;

    //    dt.Columns.Add(new DataColumn("QtyInPkt", typeof(decimal)));
    //    dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
    //    dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));


    //    foreach (GridViewRow row in GVVariantDetail.Rows)
    //    {
    //        Label lblItem_id = (Label)row.FindControl("lblItem_id");
    //        TextBox txtQtyInPkt = (TextBox)row.FindControl("txtQtyInPkt");

    //        if (txtQtyInPkt.Text != "0" && txtQtyInPkt.Text != "0.00" && txtQtyInPkt.Text != "0.0" && txtQtyInPkt.Text != "")
    //        {
    //            MQinpkt = Convert.ToDecimal(txtQtyInPkt.Text);
    //        }
    //        else
    //        {
    //            MQinpkt = 0;
    //        }

    //        if (MQinpkt != 0 && MQinpkt != 0 && MQinpkt != 0)
    //        {
    //            dr = dt.NewRow();
    //            dr[0] = MQinpkt;
    //            dr[1] = lblItem_id.Text;
    //            dr[2] = ViewState["TypeId"].ToString();
    //            dt.Rows.Add(dr);
    //        }


    //    }

    //    return dt;

    //}

    //private DataTable GetMilkinContainerInfo()
    //{

    //    decimal QtyInKg = 0;

    //    DataTable dt = new DataTable();
    //    DataRow dr;

    //    dt.Columns.Add(new DataColumn("QtyInKg", typeof(decimal)));
    //    dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
    //    dt.Columns.Add(new DataColumn("I_MCID", typeof(int)));

    //    foreach (GridViewRow row in gvMilkinContainer.Rows)
    //    {
    //        Label lblI_MCID = (Label)row.FindControl("lblI_MCID");
    //        TextBox txtQtyInKg = (TextBox)row.FindControl("txtQtyInKg");

    //        if (txtQtyInKg.Text != "0" && txtQtyInKg.Text != "0.00" && txtQtyInKg.Text != "0.0" && txtQtyInKg.Text != "")
    //        {
    //            QtyInKg = Convert.ToDecimal(txtQtyInKg.Text);
    //        }
    //        else
    //        {
    //            QtyInKg = 0;
    //        }

    //        if (QtyInKg != 0)
    //        {
    //            dr = dt.NewRow();
    //            dr[0] = QtyInKg;
    //            dr[1] = ViewState["TypeId"].ToString();
    //            dr[2] = lblI_MCID.Text;
    //            dt.Rows.Add(dr);
    //        }


    //    }

    //    return dt;

    //}

    //private DataTable GetMilkinTankerInfo()
    //{

    //    decimal QtyInLtr = 0;

    //    DataTable dt = new DataTable();
    //    DataRow dr;

    //    dt.Columns.Add(new DataColumn("QtyInLtr", typeof(decimal)));
    //    dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
    //    dt.Columns.Add(new DataColumn("I_TankerID", typeof(int)));

    //    foreach (GridViewRow row in gv_SMTDetails.Rows)
    //    {
    //        Label lblI_TankerID = (Label)row.FindControl("lblI_TankerID");
    //        TextBox txtQtyInLtr = (TextBox)row.FindControl("txtQtyInLtr");

    //        if (txtQtyInLtr.Text != "")
    //        {
    //            QtyInLtr = Convert.ToDecimal(txtQtyInLtr.Text);
    //        }
    //        else
    //        {
    //            QtyInLtr = 0;
    //        }

    //        if (QtyInLtr != 0)
    //        {
    //            dr = dt.NewRow();
    //            dr[0] = txtQtyInLtr.Text;
    //            dr[1] = ViewState["TypeId"].ToString();
    //            dr[2] = lblI_TankerID.Text;
    //            dt.Rows.Add(dr);
    //        }


    //    }

    //    return dt;

    //}

    protected void btnYesT_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

               

                CalculationMilkP();
                if (Convert.ToDecimal(lblReceiptTotal.Text) == Convert.ToDecimal(lblIssuedtotal.Text))
                {
                    GetMilkInfo();
                    GetMilkVarientInfo();
                    GetMilkinContainerInfo();
                    GetMilkinTankerInfo();

                    DataTable dtMainInfo = new DataTable();
                    dtMainInfo = (DataTable)ViewState["dt_MilkInfo"];

                    DataTable dtIDF = new DataTable();
                    dtIDF = (DataTable)ViewState["dt_MilkVarientInfo"];

                    DataTable dtMCD = new DataTable();
                    dtMCD = (DataTable)ViewState["dt_MilkinContainerInfo"];

                    DataTable dtMIT = new DataTable();
                    dtMIT = (DataTable)ViewState["dt_MilkinTankerInfo"];
                   ds = null;

                   ds = objdb.ByProcedure("SpProductionProduct_Milk_InOutEdit",
                                             new string[] { "flag"
                                                           ,"Office_ID"
                                                           ,"ProductSection_ID"
                                                           ,"Mult_MilkProductionProcess_Id"
                                                           
                                   },
                                             new string[] { "2"
                                                          ,objdb.Office_ID()
                                                          ,ddlPSection.SelectedValue
                                                          ,ViewState["Mult_MilkProductionProcess_Id"].ToString()
                                   },
                                            new string[] { "type_UpdateProduction_Milk_InOutProcess", "type_UpdateProduction_Milk_InOutProcessChild", "type_UpdateProduction_Milk_InOutProcessChild2MIC", "type_UpdateProduction_Milk_InOutProcessChild3_SM_Tanker" },
                                            new DataTable[] { dtMainInfo, dtIDF, dtMCD, dtMIT }, "TableSave");


                   if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                   {
                       Session["IsSuccessMilkEntry"] = true;
                       Response.Redirect("EditMilkProductionEntry.aspx", false);
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

    // For Container

    protected void btnaddContainer_Click(object sender, EventArgs e)
    {
        lblmsgContainer.Text = "";
        AddContainerDetails();
    }

    private void AddContainerDetails()
    {
        try
        {
            int CompartmentType = 0;
           
            if (Convert.ToString(ViewState["InsertRecord2"]) == null || Convert.ToString(ViewState["InsertRecord2"]) == "")
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
                ViewState["InsertRecord2"] = dt1;
                gvMilkinContainer.DataSource = dt1;
                gvMilkinContainer.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                DataTable DT1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("I_MCID", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_MCName", typeof(string)));
                dt1.Columns.Add(new DataColumn("QtyInKg", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord2"];

                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    if (ddlContainer.SelectedValue == DT1.Rows[i]["I_MCID"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblmsgContainer.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Seal of \"" + ddlContainer.SelectedItem.Text + "\" already exist.");

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
                ViewState["InsertRecord2"] = dt1;
                gvMilkinContainer.DataSource = dt1;
                gvMilkinContainer.DataBind();
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
            DataTable dt2 = ViewState["InsertRecord2"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord2"] = dt2;
            gvMilkinContainer.DataSource = dt2;
            gvMilkinContainer.DataBind();
            ddlContainer.ClearSelection();
            txtQtyInLtr.Text = "";
        }
        catch (Exception ex)
        {
            lblmsgContainer.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    // For Tanker 

    protected void btnLtrTanker_Click(object sender, EventArgs e)
    {
        lblmsgTanker.Text = "";
        AddTankerDetails();
    }

    private void AddTankerDetails()
    {
        try
        {
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
                dr1[1] = ddlTankerNo.SelectedItem.Text;
                dr1[2] = txtQtyInLtrTanker.Text;
                dt1.Rows.Add(dr1);
                ViewState["InsertRecord1"] = dt1;
                gv_SMTDetails.DataSource = dt1;
                gv_SMTDetails.DataBind();
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
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblmsgTanker.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Seal of \"" + ddlTankerNo.SelectedItem.Text + "\" already exist.");

                }
                else
                {
                    dr1 = dt1.NewRow();
                    dr1[0] = ddlTankerNo.SelectedValue;
                    dr1[1] = ddlTankerNo.SelectedItem.Text;
                    dr1[2] = txtQtyInLtrTanker.Text;
                    dt1.Rows.Add(dr1);
                }

                foreach (DataRow tr in DT1.Rows)
                {
                    dt1.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord1"] = dt1;
                gv_SMTDetails.DataSource = dt1;
                gv_SMTDetails.DataBind();
            }

            ddlTankerNo.ClearSelection();
            txtQtyInLtrTanker.Text = "";

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
            gv_SMTDetails.DataSource = dt2;
            gv_SMTDetails.DataBind();
            ddlTankerNo.ClearSelection();
            txtQtyInLtrTanker.Text = "";
        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }



    protected void txtIssuedforstesQtyinPkt1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt1.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant1.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt1.Text) * Convert.ToDecimal(ddlVariant1.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt1.Text) * Convert.ToDecimal(ddlVariant1.SelectedValue)) / 1000;

                }

                txtIssuedforstes.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void ddlVariant1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt1.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant1.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt1.Text) * Convert.ToDecimal(ddlVariant1.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt1.Text) * Convert.ToDecimal(ddlVariant1.SelectedValue)) / 1000;

                }

                txtIssuedforstes.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void ddlVariant2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt2.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant2.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt2.Text) * Convert.ToDecimal(ddlVariant2.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt2.Text) * Convert.ToDecimal(ddlVariant2.SelectedValue)) / 1000;

                }

                txtIssuedforstes2.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }


    }
    protected void txtIssuedforstesQtyinPkt2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt2.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant2.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt2.Text) * Convert.ToDecimal(ddlVariant2.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt2.Text) * Convert.ToDecimal(ddlVariant2.SelectedValue)) / 1000;

                }

                txtIssuedforstes2.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void ddlVariant3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt3.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant3.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt3.Text) * Convert.ToDecimal(ddlVariant3.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt3.Text) * Convert.ToDecimal(ddlVariant3.SelectedValue)) / 1000;

                }

                txtIssuedforstes3.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void txtIssuedforstesQtyinPkt3_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt3.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant3.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt3.Text) * Convert.ToDecimal(ddlVariant3.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt3.Text) * Convert.ToDecimal(ddlVariant3.SelectedValue)) / 1000;

                }

                txtIssuedforstes3.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void ddlVariant4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt4.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant4.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt4.Text) * Convert.ToDecimal(ddlVariant4.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt4.Text) * Convert.ToDecimal(ddlVariant4.SelectedValue)) / 1000;

                }

                txtIssuedforstes4.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void txtIssuedforstesQtyinPkt4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal VariantQtyInLtr = 0;

            if (txtIssuedforstesQtyinPkt4.Text != "")
            {
                int PackagingSize = Convert.ToInt32(ddlVariant4.SelectedValue);

                if (PackagingSize == 1)
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt4.Text) * Convert.ToDecimal(ddlVariant4.SelectedValue));
                }

                else
                {
                    VariantQtyInLtr = (Convert.ToDecimal(txtIssuedforstesQtyinPkt4.Text) * Convert.ToDecimal(ddlVariant4.SelectedValue)) / 1000;

                }

                txtIssuedforstes4.Text = VariantQtyInLtr.ToString("0.0");

            }


        }
        catch (Exception ex)
        {
            lblmsgTanker.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(lblReceiptTotal.Text) == Convert.ToDecimal(lblIssuedtotal.Text))
        {
            
            GetMilkInfo();
            GetMilkVarientInfo();
            GetMilkinContainerInfo();
            GetMilkinTankerInfo();
            ddlShift.SelectedValue = ViewState["NextShift_Id"].ToString();
            txtDate.Text = ViewState["NextDate"].ToString();
            DDSHEET_Info(Session["ItemTypeId"].ToString());
            lblopeningBalance.Text = ViewState["ClosingBalance"].ToString();
            btngettotalvarient_Click(sender, e);
        }
        else
        {
            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Receipt Milk Qty And Issued Milk Qty Can't Equal");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
        }

    }
    
    protected void CreateDataTable()
    {
        DataTable dt_MilkInfo = new DataTable();
        dt_MilkInfo.Columns.Add(new DataColumn("MilkProductionProcess_Id", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("OpeningMilk", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("ReceivedMilk", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("PreparedMilk", typeof(string)));
		dt_MilkInfo.Columns.Add(new DataColumn("PreparedMilkforSeparation", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("ReturnMilk", typeof(decimal)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedForSates", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedForWholeMilk", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedForProductSection", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("Losses", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("ClosingBalance", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("ReceiptMilkNetQty", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedMilkNetQty", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("ItemType_id", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedForSale2", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedForSale3", typeof(string)));
        dt_MilkInfo.Columns.Add(new DataColumn("IssuedForSale4", typeof(string)));


        ViewState["dt_MilkInfo"] = dt_MilkInfo;


        DataTable dt_MilkVarientInfo = new DataTable();

        dt_MilkVarientInfo.Columns.Add(new DataColumn("MilkProductionProcess_Id", typeof(decimal)));       
        dt_MilkVarientInfo.Columns.Add(new DataColumn("QtyInPkt", typeof(string)));
        dt_MilkVarientInfo.Columns.Add(new DataColumn("ItemType_id", typeof(string)));
        dt_MilkVarientInfo.Columns.Add(new DataColumn("Item_id", typeof(string)));

        ViewState["dt_MilkVarientInfo"] = dt_MilkVarientInfo;

        DataTable dt_MilkinContainerInfo = new DataTable();

        dt_MilkinContainerInfo.Columns.Add(new DataColumn("MilkProductionProcess_Id", typeof(int)));
        dt_MilkinContainerInfo.Columns.Add(new DataColumn("Date", typeof(string)));
        dt_MilkinContainerInfo.Columns.Add(new DataColumn("Shift_Id", typeof(int)));
        dt_MilkinContainerInfo.Columns.Add(new DataColumn("QtyInKg", typeof(decimal)));
        dt_MilkinContainerInfo.Columns.Add(new DataColumn("I_MCID", typeof(string)));
        dt_MilkinContainerInfo.Columns.Add(new DataColumn("ItemType_id", typeof(string)));
        
        ViewState["dt_MilkinContainerInfo"] = dt_MilkinContainerInfo;

        DataTable dt_MilkinTankerInfo = new DataTable();

        dt_MilkinTankerInfo.Columns.Add(new DataColumn("MilkProductionProcess_Id", typeof(int)));
        dt_MilkinTankerInfo.Columns.Add(new DataColumn("Date", typeof(string)));
        dt_MilkinTankerInfo.Columns.Add(new DataColumn("Shift_Id", typeof(int)));
        dt_MilkinTankerInfo.Columns.Add(new DataColumn("QtyInLtr", typeof(decimal)));
        dt_MilkinTankerInfo.Columns.Add(new DataColumn("I_TankerID", typeof(string)));
        dt_MilkinTankerInfo.Columns.Add(new DataColumn("ItemType_id", typeof(string)));
        
        ViewState["dt_MilkinTankerInfo"] = dt_MilkinTankerInfo;
       
    }
    
    protected void GetMilkInfo()
    {

        DataTable dt_MilkInfo = (DataTable)ViewState["dt_MilkInfo"];
        dt_MilkInfo.Rows.Add(ViewState["MilkProductionProcess_Id"].ToString()
                                         , lblopeningBalance.Text
                                         , lblReceived.Text
                                         , txtPrepared.Text
										 ,txtPreparedforSeparation.Text
                                         , lblReturn.Text
                                         , txtIssuedforstes.Text
                                         , txtIssuedforWH.Text
                                         , txtIssuedforProductions.Text
                                         , txtLosses.Text
                                         , txtClosingBalance.Text
                                         , lblReceiptTotal.Text
                                         , lblIssuedtotal.Text
                                         , ViewState["TypeId"].ToString(),
                                         txtIssuedforstes2.Text,
                                         txtIssuedforstes3.Text,
                                         txtIssuedforstes4.Text);
        ViewState["dt_MilkInfo"] = dt_MilkInfo;

        
    }
    protected void GetMilkVarientInfo()
    {

        DataTable dt_MilkVarientInfo = (DataTable)ViewState["dt_MilkVarientInfo"];
        DataRow dr;
        decimal MQinpkt = 0;
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
                dr = dt_MilkVarientInfo.NewRow();
                dr[0] = ViewState["MilkProductionProcess_Id"].ToString();
                dr[1] = MQinpkt;
                dr[2] = lblItem_id.Text;
                dr[3] = ViewState["TypeId"].ToString();
                dt_MilkVarientInfo.Rows.Add(dr);
            }


        }
        ViewState["dt_MilkVarientInfo"] = dt_MilkVarientInfo;
    }

    protected void GetMilkinContainerInfo()
    {
        decimal QtyInKg = 0;

        DataTable dt_MilkinContainerInfo = (DataTable)ViewState["dt_MilkinContainerInfo"];
        DataRow dr;

        

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
                dr = dt_MilkinContainerInfo.NewRow();
                dr[0] = ViewState["MilkProductionProcess_Id"].ToString();
                dr[1] = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
                dr[2] = ddlShift.SelectedValue;     
                dr[3] = QtyInKg;               
                dr[4] = lblI_MCID.Text;
                dr[5] = ViewState["TypeId"].ToString();
                dt_MilkinContainerInfo.Rows.Add(dr);
            }


        }
        ViewState["dt_MilkinContainerInfo"] = dt_MilkinContainerInfo;
    }
    protected void GetMilkinTankerInfo()
    {
        decimal QtyInLtr = 0;

        DataTable dt_MilkinTankerInfo = (DataTable)ViewState["dt_MilkinTankerInfo"];
        DataRow dr;

        
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
                dr = dt_MilkinTankerInfo.NewRow();
                dr[0] = ViewState["MilkProductionProcess_Id"].ToString();
                dr[1] = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
                dr[2] = ddlShift.SelectedValue;  
                dr[3] = QtyInLtr;                
                dr[4] = lblI_TankerID.Text;
                dr[5] = ViewState["TypeId"].ToString();
                dt_MilkinTankerInfo.Rows.Add(dr);
            }


        }
        ViewState["dt_MilkinTankerInfo"] = dt_MilkinTankerInfo;
    }

   
}