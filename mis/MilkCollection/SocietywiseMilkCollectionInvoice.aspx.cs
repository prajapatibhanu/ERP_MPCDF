using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;


public partial class mis_MilkCollection_SocietywiseMilkCollectionInvoice : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Society Milk Invoice Successfully Generated');", true);
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");


                ddlItemBillingHead_Type.ClearSelection();
                ddlHeaddetails.Items.Clear();
                ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
                gv_HeadDetails.DataSource = string.Empty;
                gv_HeadDetails.DataBind();
                FillBMCRoot();
                ViewState["Entry_ID"] = "0";


            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }


    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }

    protected void FillSociety()
    {
        try
        {
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[2];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("SocietywiseMilkCollectionInvoice.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "OfficeType_ID", "Office_ID" },
                      new string[] { "6", objdb.OfficeType_ID(), objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            decimal ActualAmount = 0;
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblmsgshow.Visible = false;
                string Fdate = "";
                string Tdate = "";

                if (txtFdt.Text != "")
                {
                    Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                }

                if (txtTdt.Text != "")
                {
                    Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                }


                DataSet dsPP = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                       new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                       new string[] { "7", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");


                if (dsPP != null)
                {
                    if (dsPP.Tables.Count > 0)
                    {
                        if (dsPP.Tables[3].Rows.Count > 0)
                        {
                            lblProducerPayment.Text = dsPP.Tables[3].Rows[0]["Final_ProducerPayableAmount"].ToString();
                        }
                    }
                }
                DataSet dsAdjustAmnt = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                      new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                      new string[] { "9", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");


                if (dsAdjustAmnt != null)
                {
                    if (dsAdjustAmnt.Tables.Count > 0)
                    {
                        if (dsAdjustAmnt.Tables[0].Rows.Count > 0)
                        {
                            lblProcAdjustAmount.Text = dsAdjustAmnt.Tables[0].Rows[0]["AdjustAmount"].ToString();
                        }
                        else
                        {
                            lblSocAdjustAmount.Text = "0.00";
                        }
                        if (dsAdjustAmnt.Tables[1].Rows.Count > 0)
                        {
                            lblSocAdjustAmount.Text = dsAdjustAmnt.Tables[1].Rows[0]["NetAmount"].ToString();
                        }
                        else
                        {
                            lblSocAdjustAmount.Text = "0.00";
                        }
                    }
                }


                ds = null;

                ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                         new string[] { "flag", "FDT", "TDT", "OfficeId", "Office_ID" },
                         new string[] { "21", Fdate, Tdate, ddlSociety.SelectedValue, objdb.Office_ID() }, "dataset");



                if (ds != null)
                {
                    decimal RateBuff = 0;
                    decimal RateCow = 0;
                    decimal CowRate = 0;
                    decimal Rate = 0;
                    decimal BuffRate = 0;
                    decimal CowSnfKg = 0;
                    decimal CowFatKg = 0;
                    decimal BufFatKg = 0;
                    decimal PPD = 0;
                    decimal ProdAdjustAmount = 0;
                    decimal SocAdjustAmount = 0;
                    decimal ChillingCost = 0;
                    decimal HeadLoadCharges = 0;
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[4].Rows.Count > 0)
                        {

                            BuffRate = decimal.Parse(ds.Tables[4].Rows[0]["Rate"].ToString());

                        }
                        if (ds.Tables[5].Rows.Count > 0)
                        {

                            CowRate = decimal.Parse(ds.Tables[5].Rows[0]["Rate"].ToString());

                        }
                        if (ds.Tables[6].Rows.Count > 0)
                        {

                            Rate = decimal.Parse(ds.Tables[6].Rows[0]["Rate"].ToString());

                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //Bind Invoice Header Data
                            lblSociety.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
                            lblbankInfo.Text = ds.Tables[0].Rows[0]["IFSC"].ToString() + " / " + ds.Tables[0].Rows[0]["BankName"].ToString() + " / " + ds.Tables[0].Rows[0]["BankAccountNo"].ToString();
                            lblOfficename.Text = ds.Tables[0].Rows[0]["AttachUnitName"].ToString() + " / " + ds.Tables[0].Rows[0]["AttachUnitCode"].ToString();
                            lblBillingPeriod.Text = ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString();


                            // Calculate  MilkValue 
                            if (ds.Tables[1].Rows.Count != 0)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    btnGenerateInvoice.Visible = true;
                                    FS_DailyReport.Visible = true;
                                    gv_SocietyMilkDispatchDetail.DataSource = ds.Tables[1];
                                    gv_SocietyMilkDispatchDetail.DataBind();

                                    decimal MilkQty_InKG = 0;
                                    decimal FAT_IN_KG = 0;
                                    decimal SNF_IN_KG = 0;
                                    decimal Value = 0;
                                    decimal MilkValue = 0;

                                    foreach (GridViewRow row in gv_SocietyMilkDispatchDetail.Rows)
                                    {
                                        Label lblI_MilkSupplyQty = (Label)row.FindControl("lblI_MilkSupplyQty");

                                        if (lblI_MilkSupplyQty.Text != "")
                                        {
                                            MilkQty_InKG += Convert.ToDecimal(lblI_MilkSupplyQty.Text);
                                            Label lblI_MilkSupplyQtyTotal = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblI_MilkSupplyQtyTotal") as Label);
                                            lblI_MilkSupplyQtyTotal.Text = MilkQty_InKG.ToString("0.00");
                                        }

                                        Label lblFAT_IN_KG = (Label)row.FindControl("lblFAT_IN_KG");

                                        if (lblFAT_IN_KG.Text != "")
                                        {
                                            FAT_IN_KG += Convert.ToDecimal(lblFAT_IN_KG.Text);
                                            Label lblTotal_FAT_IN_KG = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblTotal_FAT_IN_KG") as Label);
                                            lblTotal_FAT_IN_KG.Text = FAT_IN_KG.ToString("0.000");
                                        }



                                        Label lblSNF_IN_KG = (Label)row.FindControl("lblSNF_IN_KG");

                                        if (lblSNF_IN_KG.Text != "")
                                        {
                                            SNF_IN_KG += Convert.ToDecimal(lblSNF_IN_KG.Text);
                                            Label lblTotal_SNF_IN_KG = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblTotal_SNF_IN_KG") as Label);
                                            lblTotal_SNF_IN_KG.Text = SNF_IN_KG.ToString("0.00");
                                        }

                                        Label lblValue = (Label)row.FindControl("lblValue");

                                        if (lblValue.Text != "")
                                        {
                                            Value += Convert.ToDecimal(lblValue.Text);
                                            MilkValue += Convert.ToDecimal(lblValue.Text);
                                            Label lblNetValue = (gv_SocietyMilkDispatchDetail.FooterRow.FindControl("lblNetValue") as Label);
                                            lblNetValue.Text = Value.ToString("0.00");
                                        }

                                    }
                                    if (ds.Tables[9].Rows.Count > 0)
                                    {
                                        RateBuff = decimal.Parse(ds.Tables[9].Rows[0]["BuffRate"].ToString());
                                        RateCow = decimal.Parse(ds.Tables[9].Rows[0]["CowRate"].ToString());
                                    }
                                    //Calculate Buf Data
                                    if (ds.Tables[2].Rows.Count != 0)
                                    {
                                        if (ds.Tables[2].Rows.Count > 0)
                                        {

                                            FS_DailyReport_Shift.Visible = true;
                                            gv_SocietyBufData.DataSource = ds.Tables[2];
                                            gv_SocietyBufData.DataBind();
                                            foreach (GridViewRow rows in gv_SocietyBufData.Rows)
                                            {
                                                
                                                Label lblFAT_IN_KG = (Label)rows.FindControl("lblFAT_IN_KG");
												Label lblmsgQuality = (Label)rows.FindControl("lblmsgQuality");
                                                Label lblBufRate = (Label)rows.FindControl("lblBufRate");
                                                if (lblFAT_IN_KG.Text != "")
                                                {
													if(objdb.Office_ID() == "6"  || objdb.Office_ID() == "4" )
                                                    {
                                                        if(lblmsgQuality.Text == "Good")
                                                        {
                                                            BufFatKg += Convert.ToDecimal(lblFAT_IN_KG.Text);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BufFatKg += Convert.ToDecimal(lblFAT_IN_KG.Text);
                                                    }
                                                    //BufFatKg += Convert.ToDecimal(lblFAT_IN_KG.Text);

                                                }
                                                if (lblmsgQuality.Text == "Good")
                                                {

                                                    lblBufRate.Text = RateBuff.ToString();
                                                }
                                                else if (lblmsgQuality.Text == "Sour")
                                                {

                                                    lblBufRate.Text = ((RateBuff) * 50 / 100).ToString();
                                                }
                                                else
                                                {
                                                    lblBufRate.Text = ((RateBuff) * 30 / 100).ToString();
                                                }
                                            }

                                        }

                                        else
                                        {
                                            // FS_DailyReport_Shift.Visible = false;
                                            gv_SocietyBufData.DataSource = string.Empty;
                                            gv_SocietyBufData.DataBind();
                                        }
                                    }

                                    else
                                    {
                                        //FS_DailyReport_Shift.Visible = false;
                                        gv_SocietyBufData.DataSource = string.Empty;
                                        gv_SocietyBufData.DataBind();
                                    }

                                    

                                    //Calculate Cow Data
                                    if (ds.Tables[3].Rows.Count != 0)
                                    {
                                        if (ds.Tables[3].Rows.Count > 0)
                                        {
                                            FS_DailyReport_Shift.Visible = true;
                                            
                                            gv_SocietyCowData.DataSource = ds.Tables[3];
                                            gv_SocietyCowData.DataBind();
                                            foreach (GridViewRow rows in gv_SocietyCowData.Rows)
                                            {
                                                Label lblSNF_IN_KG = (Label)rows.FindControl("lblSNF_IN_KG");
                                                Label lblFAT_IN_KG = (Label)rows.FindControl("lblFAT_IN_KG");
												Label lblmsgQuality = (Label)rows.FindControl("lblmsgQuality");
                                                Label lblCowRate = (Label)rows.FindControl("lblCowRate");
                                                if (lblSNF_IN_KG.Text != "")
                                                {
													if (objdb.Office_ID() == "6"  || objdb.Office_ID() == "4")
                                                    {
                                                        if (lblmsgQuality.Text == "Good")
                                                        {
                                                            CowSnfKg += Convert.ToDecimal(lblSNF_IN_KG.Text);
                                                            
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CowSnfKg += Convert.ToDecimal(lblSNF_IN_KG.Text);
                                                    }
                                                    //CowSnfKg += Convert.ToDecimal(lblSNF_IN_KG.Text);
                                                   
                                                }
                                                if (lblFAT_IN_KG.Text != "")
                                                {
													 if (objdb.Office_ID() == "6"  || objdb.Office_ID() == "4")
                                                    {
                                                        if (lblmsgQuality.Text == "Good")
                                                        {
                                                            CowFatKg += Convert.ToDecimal(lblFAT_IN_KG.Text);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CowFatKg += Convert.ToDecimal(lblFAT_IN_KG.Text);
                                                    }
                                                    //CowFatKg += Convert.ToDecimal(lblFAT_IN_KG.Text);

                                                }
                                                if (lblmsgQuality.Text == "Good")
                                                {
                                                    
                                                    lblCowRate.Text = RateCow.ToString();
                                                }
                                                else if (lblmsgQuality.Text == "Sour")
                                                {

                                                    lblCowRate.Text = ((RateCow) * 50/100).ToString();
                                                }
                                                else
                                                {
                                                    lblCowRate.Text = ((RateCow) * 30 / 100).ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //FS_DailyReport_Shift.Visible = false;
                                            gv_SocietyCowData.DataSource = string.Empty;
                                            gv_SocietyCowData.DataBind();
                                        }
                                    }

                                    else
                                    {
                                        // FS_DailyReport_Shift.Visible = false;
                                        gv_SocietyCowData.DataSource = string.Empty;
                                        gv_SocietyCowData.DataBind();
                                    }


                                    //Calculate Commission
                                    if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4" || objdb.Office_ID() == "5")
                                    {
                                        if (ds.Tables[0].Rows[0]["SocietyCategory"].ToString() == "1" || ds.Tables[0].Rows[0]["SocietyCategory"].ToString() == "3")
                                        {
                                            //lblCommission.Text = (FAT_IN_KG * Rate).ToString("0.000");
                                            string myStringfromdat = txtFdt.Text; // From Database
                                            DateTime dt1 = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                            string myStringtodate = "11/11/2020";// From Database
                                            DateTime dt2 = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            //DateTime dt1 = Convert.ToDateTime(txtFdt.Text).Date;
                                            //DateTime dt2 = Convert.ToDateTime("11/11/2020").Date;

                                            if (dt1 < dt2)
                                            {
                                                lblCommission.Text = ((BufFatKg + CowFatKg) * Rate).ToString("0.000");

                                            }
                                            else
                                            {
                                                lblCommission.Text = (((CowFatKg + CowSnfKg) * CowRate) + (BufFatKg * BuffRate)).ToString("0.000");
                                            }
                                        }
                                        else
                                        {
                                            lblCommission.Text = "0";
                                        }
                                        
                                    }
                                    else
                                    {
                                        if (ds.Tables[0].Rows[0]["SocietyCategory"].ToString() == "1" || ds.Tables[0].Rows[0]["SocietyCategory"].ToString() == "3")
                                        {
                                            lblCommission.Text = ((BufFatKg + CowFatKg) * Rate).ToString("0.000");
                                        }
                                        else
                                        {
                                            lblCommission.Text = "0";
                                        }
                                    }
                                    // lblCommission.Text = (FAT_IN_KG * 7).ToString();
                                    // lblCommission.Text = (FAT_IN_KG * Rate).ToString("0.000");
                                    lblMilkValue.Text = MilkValue.ToString("0.00");
                                    lblCommission.Text = Math.Round(Convert.ToDecimal(lblCommission.Text), 2).ToString();
									if(objdb.Office_ID() == "4")
                                    {
                                        lblNarration.Text = "DCS.Comm.  Buf @Rs  " + BuffRate + "/KgFat  &  Cow @Rs.  " + CowRate + "/KgTS:- " + lblCommission.Text;
                                    }
                                    else
                                    {
                                        lblNarration.Text = "";
                                    }
                                    lblGrossEarning.Text = (Math.Round(MilkValue + Convert.ToDecimal(lblCommission.Text),2)).ToString();

                                    //Calculate Producer Amount
                                    

                                    if (lblProducerPayment.Text != "")
                                    {
                                        PPD = Convert.ToDecimal(lblProducerPayment.Text);
                                        
                                    }
                                    else
                                    {
                                        //PPD = 0;
                                    }
                                   

                                    //Calculate Producer Adjust Amount
                                    if (lblProcAdjustAmount.Text != "")
                                    {
                                        ProdAdjustAmount = Convert.ToDecimal(lblProcAdjustAmount.Text);
                                    }
                                    else
                                    {
                                        ProdAdjustAmount = 0;
                                    }

                                    //Calculate Society Adjust Amount
                                    if (lblSocAdjustAmount.Text != "")
                                    {
                                        SocAdjustAmount = Convert.ToDecimal(lblSocAdjustAmount.Text);
                                    }
                                    else
                                    {
                                        SocAdjustAmount = 0;
                                    }



                                    //Calculate Chilling Cost
                                    
                                    String FDate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    DateTime Mtrl_datevalue = (Convert.ToDateTime(FDate.ToString()));
                                    
                                    int Mtrl_dy = int.Parse(Mtrl_datevalue.Day.ToString());
                                    if (Mtrl_dy >= 21)
                                    {
                                        DataSet dschillingcost = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "FDT" }, new string[] { "22", ddlSociety.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                        if (dschillingcost != null && dschillingcost.Tables.Count > 0)
                                        {
                                            if (dschillingcost.Tables[0].Rows.Count > 0)
                                            {
                                                ChillingCost = decimal.Parse(dschillingcost.Tables[0].Rows[0]["CC"].ToString());
                                            }
                                        }
                                    }

                                    lblCC.Text = ChillingCost.ToString();



                                    //Calculate HeadLoadCharges
                                    
                                    //if (ddlMilkCollectionUnit.SelectedValue == "6")
                                    //{
                                        string flag = "23";
                                        if (objdb.Office_ID() == "5")
                                        {
                                            flag = "24";
                                            lblHeadLoadCharges.Visible = true;
                                            DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { flag, ddlSociety.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                            if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                                            {
                                                if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                                                {
                                                    HeadLoadCharges = decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString());
                                                }
                                            }
                                        }
                                        else if (objdb.Office_ID() == "3")
                                        {
                                            flag = "25";
                                            lblHeadLoadCharges.Visible = true;
                                            DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { flag, ddlSociety.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                            if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                                            {
                                                if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                                                {
                                                    HeadLoadCharges = Math.Round(((MilkQty_InKG / 1.030M) * decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString())),2);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Mtrl_dy >= 21)
                                            {
                                                lblHeadLoadCharges.Visible = true;
                                                DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { flag, ddlSociety.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                                if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                                                {
                                                    if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                                                    {
                                                        HeadLoadCharges = decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                       
                                       

                                    //}



                                    lblHeadLoadCharges.Text = HeadLoadCharges.ToString();


                                    // Calculate Addition/Deduction Data
                                    if (ds.Tables[7].Rows.Count > 0)
                                    {
                                        int Count = ds.Tables[7].Rows.Count;
                                        DataTable dtadhead = new DataTable();
                                        dtadhead.Columns.Add(new DataColumn("S.No", typeof(int)));
                                        dtadhead.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                                        dtadhead.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                                        dtadhead.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                                        dtadhead.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));
                                        dtadhead.Columns.Add(new DataColumn("Remark", typeof(string)));
                                        dtadhead.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(decimal)));

                                        DataTable dtadheadChild = new DataTable();
                                        dtadheadChild.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(int)));
                                        dtadheadChild.Columns.Add(new DataColumn("CycleAmount", typeof(decimal)));
                                        dtadheadChild.Columns.Add(new DataColumn("PrevoiusAmount", typeof(decimal)));
                                        dtadheadChild.Columns.Add(new DataColumn("DeductedAmount", typeof(decimal)));

                                        dtadheadChild.Columns.Add(new DataColumn("BalanceAmount", typeof(decimal)));
                                        dtadheadChild.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                                        dtadheadChild.Columns.Add(new DataColumn("BillingFromDate", typeof(string)));
                                        dtadheadChild.Columns.Add(new DataColumn("BillingToDate", typeof(string)));
                                        dtadheadChild.Columns.Add(new DataColumn("Status", typeof(int)));

                                        DataRow dr, dr1;
                                        if (ds.Tables[0].Rows[0]["SocietyCategory"].ToString() == "1")
                                        {
                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "N.R.D.";
                                            if (objdb.Office_ID() == "4")
                                            {
                                                dr[4] = Math.Round((FAT_IN_KG * Convert.ToDecimal(2.25)), 2).ToString();
                                                ActualAmount = Math.Round((FAT_IN_KG * Convert.ToDecimal(2.25)), 2);

                                            }
                                            else
                                            {
                                                dr[4] = Math.Round((FAT_IN_KG * 1),2).ToString();
                                                ActualAmount = Math.Round((FAT_IN_KG * 1), 2);

                                            }
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dtadhead.Rows.Add(dr);
                                            if (objdb.Office_ID() == "3")
                                            {
                                               dr = dtadhead.NewRow();
                                               dr[0] = dtadhead.Rows.Count + 1;
                                               dr[1] = "DEDUCTION";
                                               dr[2] = "0";
                                               dr[3] = "Audit Fees";
                                               dr[4] = Math.Round(((MilkValue+Convert.ToDecimal(lblCommission.Text)) / 1000) * (2),0).ToString();
                                               
                                               dr[5] = "";
                                               dr[6] = "0";
                                               ActualAmount += Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text)) / 1000) * (2), 0);
                                               dtadhead.Rows.Add(dr);
                                            }
                                        }
										if (objdb.Office_ID() == "4")
                                        {
                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "ADDITION";
                                            dr[2] = "0";
                                            dr[3] = "Incentive for P.W.F.(SAY)(@Rs 1.0 per KgFat)";
                                            dr[4] = Math.Round((FAT_IN_KG * 1), 2).ToString();
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dtadhead.Rows.Add(dr);
                                            

                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Contrb.for PWF";
                                            dr[4] = Math.Round((FAT_IN_KG * 1), 2).ToString();
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dtadhead.Rows.Add(dr);
                                        }
                                        if (objdb.Office_ID() == "3" || objdb.Office_ID() == "4" || objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                                        {
                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Computer Charges";
                                            dr[4] = 5;
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dtadhead.Rows.Add(dr);
                                            ActualAmount = ActualAmount + 5;

                                        }
                                        if (objdb.Office_ID() == "5")
                                        {
                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "R.B.P.F";
                                            dr[4] = Math.Round(((MilkQty_InKG) * 2)/100, 2).ToString();
                                            //dr[4] = "0";
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dtadhead.Rows.Add(dr);
                                            ActualAmount += Math.Round(((MilkQty_InKG) * 2) / 100, 2);
                                        }
                                        if (objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                                        {
                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Development Charges";
                                            dr[4] = Math.Round((MilkQty_InKG) * (0.01M),2).ToString();
                                            //dr[4] = "0";
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dtadhead.Rows.Add(dr);
                                            ActualAmount += Math.Round((MilkQty_InKG) * (0.01M), 2);
                                        }
                                        if (objdb.Office_ID() == "3")
                                        {
                                            dr = dtadhead.NewRow();
                                            dr[0] = dtadhead.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Development Charges";
                                            dr[4] = Math.Round((MilkValue / 1000) * (1), 2).ToString();

                                            dr[5] = "";
                                            dr[6] = "0";
                                            ActualAmount += Math.Round((MilkValue / 1000) * (1), 2);
                                            dtadhead.Rows.Add(dr);
                                        }
                                       if(ds.Tables[8].Rows.Count > 0)
                                       {
                                           dr = dtadhead.NewRow();
                                           dr[0] = dtadhead.Rows.Count + 1;
                                           dr[1] = "DEDUCTION";
                                           dr[2] = ds.Tables[8].Rows[0]["ItemBillingHead_ID"].ToString();
                                           dr[3] = ds.Tables[8].Rows[0]["ItemBillingHead_Name"].ToString();
                                           dr[4] = ds.Tables[8].Rows[0]["Amount"].ToString() ;
                                           dr[5] = ds.Tables[8].Rows[0]["Remark"].ToString();
                                           dr[6] = "0";
                                           dtadhead.Rows.Add(dr);
                                           ViewState["Entry_ID"] = ds.Tables[8].Rows[0]["Entry_ID"].ToString(); ;
                                           ActualAmount = ActualAmount + decimal.Parse(ds.Tables[8].Rows[0]["Amount"].ToString());
                                       }
                                        decimal GrossEarning = decimal.Parse(lblGrossEarning.Text);
                                        FAT_IN_KG = Math.Round(FAT_IN_KG, 2);
                                        

                                        GrossEarning = GrossEarning - ActualAmount - PPD + SocAdjustAmount + ChillingCost + HeadLoadCharges;
                                       
                                        GrossEarning = Math.Round(GrossEarning, 2);
                                        for (int i = 0; i < Count; i++)
                                        {


                                            decimal Amount = (decimal.Parse(ds.Tables[7].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString()));
                                            if (ds.Tables[7].Rows[i]["ItemBillingHead_Type"].ToString() == "DEDUCTION")
                                            {
                                                if ((GrossEarning - Amount) >= 0)
                                                {
                                                    dr = dtadhead.NewRow();
                                                    dr[0] = (i + 1) + 1;
                                                    dr[1] = ds.Tables[7].Rows[i]["ItemBillingHead_Type"].ToString();
                                                    dr[2] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                                    dr[3] = ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString();
                                                    dr[4] = Amount;
                                                    dr[5] = ds.Tables[7].Rows[i]["Remark"].ToString();
                                                    dr[6] = ds.Tables[7].Rows[0]["AddtionsDeducEntry_ID"].ToString();
                                                    dtadhead.Rows.Add(dr);

                                                    dr1 = dtadheadChild.NewRow();
                                                    dr1[0] = ds.Tables[7].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                                    dr1[1] = ds.Tables[7].Rows[i]["CycleAmount"].ToString();
                                                    dr1[2] = ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString();
                                                    dr1[3] = Amount;
                                                    dr1[4] = decimal.Parse(ds.Tables[7].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString()) - Amount;
                                                    dr1[5] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                                    dr1[6] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                                    dr1[7] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                                    dr1[8] = "1";


                                                    dtadheadChild.Rows.Add(dr1);


                                                    GrossEarning = GrossEarning - Amount;
                                                }
                                                else if (GrossEarning != 0)
                                                {
                                                    //decimal BalanceAmnt = GrossEarning - decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString());
                                                    Amount = Math.Abs(GrossEarning);
                                                    dr = dtadhead.NewRow();
                                                    dr[0] = (i + 1) + 1;
                                                    dr[1] = ds.Tables[7].Rows[i]["ItemBillingHead_Type"].ToString();
                                                    dr[2] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                                    dr[3] = ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString();
                                                    dr[4] = Amount;
                                                    dr[5] = ds.Tables[7].Rows[i]["Remark"].ToString();
                                                    dr[6] = ds.Tables[7].Rows[0]["AddtionsDeducEntry_ID"].ToString();
                                                    dtadhead.Rows.Add(dr);

                                                    dr1 = dtadheadChild.NewRow();
                                                    dr1[0] = ds.Tables[7].Rows[i]["AddtionsDeducEntry_ID"];
                                                    dr1[1] = ds.Tables[7].Rows[i]["CycleAmount"].ToString();
                                                    dr1[2] = ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString();
                                                    dr1[3] = Amount;
                                                    dr1[4] = decimal.Parse(ds.Tables[7].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString()) - Amount;
                                                    dr1[5] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                                    dr1[6] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                                    dr1[7] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                                    dr1[8] = "0";

                                                    dtadheadChild.Rows.Add(dr1);
                                                    GrossEarning = GrossEarning - Amount;
                                                }
                                                else
                                                {
                                                    if (ds.Tables[7].Rows[i]["CycleAmount"].ToString() != "0.00")
                                                    {

                                                    }
                                                    dr1 = dtadheadChild.NewRow();
                                                    dr1[0] = ds.Tables[7].Rows[i]["AddtionsDeducEntry_ID"];
                                                    dr1[1] = ds.Tables[7].Rows[i]["CycleAmount"].ToString();
                                                    dr1[2] = ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString();
                                                    dr1[3] = "0";
                                                    dr1[4] = decimal.Parse(ds.Tables[7].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString());
                                                    dr1[5] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                                    dr1[6] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                                    dr1[7] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                                    dr1[8] = "0";

                                                    dtadheadChild.Rows.Add(dr1);
                                                    
                                                }
                                               
                                            }
                                            else
                                            {
                                                dr = dtadhead.NewRow();
                                                dr[0] = (i + 1) + 1;
                                                dr[1] = ds.Tables[7].Rows[i]["ItemBillingHead_Type"].ToString();
                                                dr[2] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                                dr[3] = ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString();
                                                dr[4] = ds.Tables[7].Rows[i]["CycleAmount"].ToString();
                                                dr[5] = ds.Tables[7].Rows[i]["Remark"].ToString();
                                                dr[6] = ds.Tables[7].Rows[0]["AddtionsDeducEntry_ID"].ToString();
                                                dtadhead.Rows.Add(dr);
                                                GrossEarning = GrossEarning + Amount;
                                            }
                                           
                                        }
                                        ViewState["dtadheadChild"] = dtadheadChild;

                                        grhradsdetails.DataSource = dtadhead;
                                        grhradsdetails.DataBind();
                                        decimal Prodvalue = 0;
                                        Label lblGrandTotals = (grhradsdetails.FooterRow.FindControl("lblGrandTotal") as Label);

                                        foreach (GridViewRow rowcc in grhradsdetails.Rows)
                                        {
                                            Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                                            Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");


                                            if (lblTotalPrice.Text != "")
                                            {
                                                if (lblItemBillingHead_Type.Text == "ADDITION")
                                                {
                                                    Prodvalue += Decimal.Parse(lblTotalPrice.Text);
                                                }
                                                if (lblItemBillingHead_Type.Text == "DEDUCTION")
                                                {
                                                    Prodvalue -= Decimal.Parse(lblTotalPrice.Text);
                                                }
                                            }

                                        }
                                        //lblGrandTotals.Text = Prodvalue.ToString("N2");


                                        //lbldeductionadditionValue.Text = Prodvalue.ToString("N2");
                                        //CODE CHANGES BY MOHINI ON 16SEP2020 //FOR DECIMAL CONVERSION
                                        lblGrandTotals.Text = Prodvalue.ToString("0.00");


                                        lbldeductionadditionValue.Text = Prodvalue.ToString("0.00");
                                        //CODE CHANGES BY MOHINI ON 16SEP2020 //FOR DECIMAL CONVERSION



                                        lblnetamount.Text = (Convert.ToDecimal(lblGrossEarning.Text) + (Prodvalue) - PPD + SocAdjustAmount + ChillingCost + HeadLoadCharges).ToString();
                                        lblnetamount.Text = Math.Round(Convert.ToDecimal(lblnetamount.Text), 2).ToString();

                                    }
                                    else
                                    {
                                        decimal Prodvalue = 0;
                                        DataTable dt = new DataTable();
                                        DataRow dr;
                                        dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                                        dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));

                                        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
                                        dt.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(string)));
                                        if (ds.Tables[0].Rows[0]["SocietyCategory"].ToString() == "1")
                                        {
                                            dr = dt.NewRow();
                                            dr[0] = 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "N.R.D.";
											if (objdb.Office_ID() == "4")
                                            {
                                                dr[4] = Math.Round((FAT_IN_KG * Convert.ToDecimal(2.25)), 2).ToString();
                                               

                                            }
                                            else
                                            {
                                                dr[4] = Math.Round((FAT_IN_KG * 1),2).ToString();
                                                

                                            }
                                            
                                            dr[5] = "";
                                            dt.Rows.Add(dr);
                                            if (objdb.Office_ID() == "3")
                                            {
                                             dr = dt.NewRow();
                                             dr[0] = dt.Rows.Count + 1;
                                             dr[1] = "DEDUCTION";
                                             dr[2] = "0";
                                             dr[3] = "Audit Fees";
                                             dr[4] = Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text)) / 1000) * (2), 0).ToString();
                                            
                                             dr[5] = "";
                                             dr[6] = "0";
                                             dt.Rows.Add(dr);
                                            }
                                           

                                       
                                        }
										if (objdb.Office_ID() == "4")
                                        {
                                            dr = dt.NewRow();
                                            dr[0] = dt.Rows.Count + 1;
                                            dr[1] = "ADDITION";
                                            dr[2] = "0";
                                            dr[3] = "Incentive for P.W.F.(SAY)(@Rs 1.0 per KgFat)";
                                            dr[4] = Math.Round((FAT_IN_KG * 1), 2).ToString();
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dt.Rows.Add(dr);


                                            dr = dt.NewRow();
                                            dr[0] = dt.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Contrb.for PWF";
                                            dr[4] = Math.Round((FAT_IN_KG * 1), 2).ToString();
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dt.Rows.Add(dr);
                                        }
                                        if (objdb.Office_ID() == "3" || objdb.Office_ID() == "4" || objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                                        {
                                            dr = dt.NewRow();
                                            dr[0] = dt.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Computer Charges";
                                            dr[4] = 5;
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dt.Rows.Add(dr);
											
                                        }
                                        if (objdb.Office_ID() == "5")
                                        {
                                            dr = dt.NewRow();
                                            dr[0] = dt.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "R.B.P.F";
                                            dr[4] = Math.Round(((MilkQty_InKG) * 2) / 100, 2).ToString();
                                            //dr[4] = "0";
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dt.Rows.Add(dr);
                                            
                                        }
                                        if (objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                                        {
                                            dr = dt.NewRow();
                                            dr[0] = dt.Rows.Count + 1;
                                            dr[1] = "DEDUCTION";
                                            dr[2] = "0";
                                            dr[3] = "Development Charges";
                                            dr[4] = Math.Round((MilkQty_InKG) * (0.01M),2).ToString();
                                            //dr[4] = "0";
                                            dr[5] = "";
                                            dr[6] = "0";
                                            dt.Rows.Add(dr);
											
                                        }
                                        if (objdb.Office_ID() == "3")
                                        {
                                         dr = dt.NewRow();
                                         dr[0] = dt.Rows.Count + 1;
                                         dr[1] = "DEDUCTION";
                                         dr[2] = "0";
                                         dr[3] = "Development Charges";
                                         dr[4] = Math.Round((MilkValue / 1000) * (1),2).ToString();
                                        
                                         dr[5] = "";
                                         dr[6] = "0";
                                         dt.Rows.Add(dr);
                                        }
                                       if (ds.Tables[8].Rows.Count > 0)
                                       {
                                           dr = dt.NewRow();
                                           dr[0] = dt.Rows.Count + 1;
                                           dr[1] = "DEDUCTION";
                                           dr[2] = ds.Tables[8].Rows[0]["ItemBillingHead_ID"].ToString();
                                           dr[3] = ds.Tables[8].Rows[0]["ItemBillingHead_Name"].ToString();
                                           dr[4] = ds.Tables[8].Rows[0]["Amount"].ToString();
                                           dr[5] = ds.Tables[8].Rows[0]["Remark"].ToString();
                                           dr[6] = ds.Tables[8].Rows[0]["AddtionsDeducEntry_ID"].ToString();
                                           dt.Rows.Add(dr);
                                           ViewState["Entry_ID"] = ds.Tables[8].Rows[0]["Entry_ID"].ToString();

                                        
                                       }
										grhradsdetails.DataSource = dt;
                                        grhradsdetails.DataBind();
                                       
                                        Label lblGrandTotals = (grhradsdetails.FooterRow.FindControl("lblGrandTotal") as Label);

                                        foreach (GridViewRow rowcc in grhradsdetails.Rows)
                                        {
                                            Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                                            Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");

                                            if (lblTotalPrice.Text != "")
                                            {
                                                if (lblItemBillingHead_Type.Text == "ADDITION")
                                                {
                                                    Prodvalue += Decimal.Parse(lblTotalPrice.Text);
                                                }
                                                if (lblItemBillingHead_Type.Text == "DEDUCTION")
                                                {
                                                    Prodvalue -= Decimal.Parse(lblTotalPrice.Text);
                                                }
                                            }

                                        }
                                        
                                        //lblGrandTotals.Text = Prodvalue.ToString("N2");


                                        //lbldeductionadditionValue.Text = Prodvalue.ToString("N2");
                                        //CODE CHANGES BY MOHINI ON 16SEP2020 //FOR DECIMAL CONVERSION
                                        lblGrandTotals.Text = Prodvalue.ToString("0.00");


                                        lbldeductionadditionValue.Text = Prodvalue.ToString("0.00");
                                        lblnetamount.Text = (Convert.ToDecimal(lblGrossEarning.Text) + (Prodvalue) - PPD + SocAdjustAmount + ChillingCost + HeadLoadCharges).ToString();
                                        lblnetamount.Text = Math.Round(Convert.ToDecimal(lblnetamount.Text), 2).ToString();
                                        
									

                                    }



                                }

                                else
                                {
                                    btnGenerateInvoice.Visible = false;
                                    lblmsgshow.Visible = true;
                                    lblmsgshow.Text = "No Record Found";
                                    FS_DailyReport_Shift.Visible = false;
                                    FS_DailyReport.Visible = false;
                                    gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                                    gv_SocietyMilkDispatchDetail.DataBind();
                                }

                            }

                            else
                            {
                                btnGenerateInvoice.Visible = false;
                                lblmsgshow.Visible = true;
                                lblmsgshow.Text = "No Record Found";
                                FS_DailyReport_Shift.Visible = false;
                                FS_DailyReport.Visible = false;
                                gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                                gv_SocietyMilkDispatchDetail.DataBind();
                            }

                        }
                        else
                        {
                            btnGenerateInvoice.Visible = false;
                            lblmsgshow.Visible = true;
                            lblmsgshow.Text = "No Record Found";
                            FS_DailyReport_Shift.Visible = false;
                            FS_DailyReport.Visible = false;
                            gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                            gv_SocietyMilkDispatchDetail.DataBind();
                        }
                    }

                    else
                    {
                        btnGenerateInvoice.Visible = false;
                        lblmsgshow.Visible = true;
                        lblmsgshow.Text = "No Record Found";
                        FS_DailyReport_Shift.Visible = false;
                        FS_DailyReport.Visible = false;
                        gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                        gv_SocietyMilkDispatchDetail.DataBind();
                    }


                   
                }
                    
                else
                {
                    btnGenerateInvoice.Visible = false;
                    lblmsgshow.Visible = true;
                    lblmsgshow.Text = "No Record Found";
                    lblSociety.Text = "";
                    lblbankInfo.Text = "";
                    lblOfficename.Text = "";
                    lblBillingPeriod.Text = "";
                    FS_DailyReport_Shift.Visible = false;
                    FS_DailyReport.Visible = false;

                }

            }

            gv_HeadDetails.DataSource = string.Empty;
            gv_HeadDetails.DataBind();
            ViewState["InsertRecord"] = null;
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void gv_SocietyMorningData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 3;
        for (int i = gv_SocietyBufData.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_SocietyBufData.Rows[i];
            GridViewRow prevRow = gv_SocietyBufData.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }

    protected void gv_SocietyEveningData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 3;
        for (int i = gv_SocietyCowData.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_SocietyCowData.Rows[i];
            GridViewRow prevRow = gv_SocietyCowData.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }

    protected void btnaddhead_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SocietywiseMilkCollectionInvoice.aspx", false);
    }

    protected void btnAddHeadsDetails_Click(object sender, EventArgs e)
    {
        
        //lblPopupMsg.Text = "";
        //AddMCUDetails();
    }

    private void AddMCUDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Remark", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlItemBillingHead_Type.SelectedValue;
                dr[2] = ddlHeaddetails.SelectedValue;
                dr[3] = ddlHeaddetails.SelectedItem.Text;
                dr[4] = txtHeadAmount.Text;
                dr[5] = txtRemark.Text;
                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();

            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));
                dt.Columns.Add(new DataColumn("ItemBillingHead_Remark", typeof(string)));

                DT = (DataTable)ViewState["InsertRecord"];

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlHeaddetails.SelectedValue == DT.Rows[i]["ItemBillingHead_ID"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }

                if (CompartmentType == 1)
                {
                    lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Head \"" + ddlHeaddetails.SelectedItem.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlItemBillingHead_Type.SelectedValue;
                    dr[2] = ddlHeaddetails.SelectedValue;
                    dr[3] = ddlHeaddetails.SelectedItem.Text;
                    dr[4] = txtHeadAmount.Text;
                    dr[5] = txtRemark.Text;

                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();
            }

            ddlItemBillingHead_Type.ClearSelection();
            ddlHeaddetails.Items.Clear();
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            txtHeadAmount.Text = "";
            txtRemark.Text = "";
            GetAmountTotal();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }

    }

    protected void lnkDeleteHead_Click(object sender, EventArgs e)
    {
        try
        {
            lblPopupMsg.Text = "";
            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecord"] as DataTable;
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecord"] = dt3;
            gv_HeadDetails.DataSource = dt3;
            gv_HeadDetails.DataBind();
            ddlItemBillingHead_Type.ClearSelection();
            ddlHeaddetails.Items.Clear();
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            txtHeadAmount.Text = "";
            GetAmountTotal();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    private void GetAmountTotal()
    {

        try
        {
            decimal dPageTotal = 0;

            Label lblGrandTotal = (gv_HeadDetails.FooterRow.FindControl("lblGrandTotal") as Label);


            foreach (GridViewRow rowcc in gv_HeadDetails.Rows)
            {
                Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");

                if (lblTotalPrice.Text != "")
                {
                    if (lblItemBillingHead_Type.Text == "ADDITION")
                    {
                        dPageTotal += Decimal.Parse(lblTotalPrice.Text);
                    }
                    if (lblItemBillingHead_Type.Text == "DEDUCTION")
                    {
                        dPageTotal -= Decimal.Parse(lblTotalPrice.Text);
                    }
                }

            }

            //lblGrandTotal.Text = dPageTotal.ToString("N2");
            //CODE CHANGES BY MOHINI ON 19Sep2020 for decimal conversion
            lblGrandTotal.Text = dPageTotal.ToString("0.00");

        }
        catch (Exception)
        {

            throw;
        }


    }

    protected void ddlHeaddetails_Init(object sender, EventArgs e)
    {
        try
        {
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    protected void ddlItemBillingHead_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblPopupMsg.Text = "";

            if (ddlItemBillingHead_Type.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
               new string[] { "flag", "ItemBillingHead_Type" },
               new string[] { "8", ddlItemBillingHead_Type.SelectedValue }, "dataset");

                ddlHeaddetails.DataTextField = "ItemBillingHead_Name";
                ddlHeaddetails.DataValueField = "ItemBillingHead_ID";
                ddlHeaddetails.DataSource = ds;
                ddlHeaddetails.DataBind();
                ddlHeaddetails.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlItemBillingHead_Type.ClearSelection();
                ddlHeaddetails.Items.Clear();
                ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }


    }

    protected void btnSearchHeadDetails_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }

    private DataTable GetMilkDispatchDetail()
    {

        DataTable dtMC = new DataTable();
        DataRow drMC;

        dtMC.Columns.Add(new DataColumn("Date", typeof(string)));
        dtMC.Columns.Add(new DataColumn("Shift", typeof(string)));
        dtMC.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMC.Columns.Add(new DataColumn("FatPer", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("SnfPer", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkValue", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMC.Columns.Add(new DataColumn("RatePerLtr", typeof(decimal)));

        foreach (GridViewRow rowseal in gv_SocietyMilkDispatchDetail.Rows)
        {
            Label lbllblDt_Date_F = (Label)rowseal.FindControl("lbllblDt_Date_F");
            Label lblV_Shift = (Label)rowseal.FindControl("lblV_Shift");
            Label lblV_MilkType = (Label)rowseal.FindControl("lblV_MilkType");
            Label lblFat = (Label)rowseal.FindControl("lblFat");
            Label lblSNF = (Label)rowseal.FindControl("lblSNF");
            Label lblI_MilkSupplyQty = (Label)rowseal.FindControl("lblI_MilkSupplyQty");
            Label lblFAT_IN_KG = (Label)rowseal.FindControl("lblFAT_IN_KG");
            Label lblSNF_IN_KG = (Label)rowseal.FindControl("lblSNF_IN_KG");
            Label lblValue = (Label)rowseal.FindControl("lblValue");
            Label lblCLR = (Label)rowseal.FindControl("lblCLR");
            Label lblQuality = (Label)rowseal.FindControl("lblQuality");
            Label lblRate_Per_Ltr = (Label)rowseal.FindControl("lblRate_Per_Ltr");

            drMC = dtMC.NewRow();
            drMC[0] = lbllblDt_Date_F.Text;
            drMC[1] = lblV_Shift.Text;
            drMC[2] = lblV_MilkType.Text;
            drMC[3] = lblFat.Text;
            drMC[4] = lblSNF.Text;
            drMC[5] = lblI_MilkSupplyQty.Text;
            drMC[6] = lblFAT_IN_KG.Text;
            drMC[7] = lblSNF_IN_KG.Text;
            drMC[8] = lblValue.Text;
            drMC[9] = lblCLR.Text;
            drMC[10] = lblQuality.Text;
            drMC[11] = lblRate_Per_Ltr.Text;

            dtMC.Rows.Add(drMC);
        }
        return dtMC;
    }

    private DataTable GetMilkBuffDetail()
    {

        DataTable dtMC = new DataTable();
        DataRow drMC;

        dtMC.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMC.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMC.Columns.Add(new DataColumn("FatPer", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("SnfPer", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkValue", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("Rate", typeof(decimal)));

        foreach (GridViewRow rowseal in gv_SocietyBufData.Rows)
        {

            Label lblV_Shift = (Label)rowseal.FindControl("lblV_Shift");
            Label lblmsgQuality = (Label)rowseal.FindControl("lblmsgQuality");
            Label lblFat = (Label)rowseal.FindControl("lblFat");
            Label lblSNF = (Label)rowseal.FindControl("lblSNF");
            Label lblI_MilkSupplyQty = (Label)rowseal.FindControl("lblI_MilkSupplyQty");
            Label lblFAT_IN_KG = (Label)rowseal.FindControl("lblFAT_IN_KG");
            Label lblSNF_IN_KG = (Label)rowseal.FindControl("lblSNF_IN_KG");
            Label lblCLR = (Label)rowseal.FindControl("lblCLR");
            Label lblValue = (Label)rowseal.FindControl("lblValue");


            Label lblRate_Per_Ltr = (Label)rowseal.FindControl("lblRate_Per_Ltr");

            drMC = dtMC.NewRow();
            drMC[0] = lblV_Shift.Text;
            drMC[1] = lblmsgQuality.Text;
            drMC[2] = lblFat.Text;
            drMC[3] = lblSNF.Text;
            drMC[4] = lblI_MilkSupplyQty.Text;
            drMC[5] = lblFAT_IN_KG.Text;
            drMC[6] = lblSNF_IN_KG.Text;
            drMC[7] = lblCLR.Text;
            drMC[8] = lblValue.Text;
            drMC[9] = 0;

            dtMC.Rows.Add(drMC);
        }
        return dtMC;
    }

    private DataTable GetMilkCowhDetail()
    {

        DataTable dtMC = new DataTable();
        DataRow drMC;

        dtMC.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMC.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMC.Columns.Add(new DataColumn("FatPer", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("SnfPer", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("MilkValue", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("Rate", typeof(decimal)));

        foreach (GridViewRow rowseal in gv_SocietyCowData.Rows)
        {

            Label lblV_Shift = (Label)rowseal.FindControl("lblV_Shift");
            Label lblmsgQuality = (Label)rowseal.FindControl("lblmsgQuality");
            Label lblFat = (Label)rowseal.FindControl("lblFat");
            Label lblSNF = (Label)rowseal.FindControl("lblSNF");
            Label lblI_MilkSupplyQty = (Label)rowseal.FindControl("lblI_MilkSupplyQty");
            Label lblFAT_IN_KG = (Label)rowseal.FindControl("lblFAT_IN_KG");
            Label lblSNF_IN_KG = (Label)rowseal.FindControl("lblSNF_IN_KG");
            Label lblCLR = (Label)rowseal.FindControl("lblCLR");
            Label lblValue = (Label)rowseal.FindControl("lblValue");


            Label lblRate_Per_Ltr = (Label)rowseal.FindControl("lblRate_Per_Ltr");

            drMC = dtMC.NewRow();
            drMC[0] = lblV_Shift.Text;
            drMC[1] = lblmsgQuality.Text;
            drMC[2] = lblFat.Text;
            drMC[3] = lblSNF.Text;
            drMC[4] = lblI_MilkSupplyQty.Text;
            drMC[5] = lblFAT_IN_KG.Text;
            drMC[6] = lblSNF_IN_KG.Text;
            drMC[7] = lblCLR.Text;
            drMC[8] = lblValue.Text;
            drMC[9] = 0;

            dtMC.Rows.Add(drMC);
        }
        return dtMC;
    }

    private DataTable GetMilkADDetail()
    {

        DataTable dtMC = new DataTable();
        DataRow drMC;

        dtMC.Columns.Add(new DataColumn("Head_ID", typeof(string)));
        dtMC.Columns.Add(new DataColumn("HeadType", typeof(string)));
        dtMC.Columns.Add(new DataColumn("HeadName", typeof(string)));
        dtMC.Columns.Add(new DataColumn("HeadAmount", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("HeadRemark", typeof(string)));
        dtMC.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(string)));

        foreach (GridViewRow rowseal in grhradsdetails.Rows)
        {
            Label lblItemBillingHead_ID = (Label)rowseal.FindControl("lblItemBillingHead_ID");
            Label lblItemBillingHead_Type = (Label)rowseal.FindControl("lblItemBillingHead_Type");
            Label lblItemBillingHead_Name = (Label)rowseal.FindControl("lblItemBillingHead_Name");
			Label lblRemark = (Label)rowseal.FindControl("lblRemark");
            Label lblAddtionsDeducEntry_ID = (Label)rowseal.FindControl("lblAddtionsDeducEntry_ID");
            Label lblTotalPrice = (Label)rowseal.FindControl("lblTotalPrice");

            drMC = dtMC.NewRow();
            drMC[0] = lblItemBillingHead_Type.Text;
            drMC[1] = lblItemBillingHead_ID.Text;
            drMC[2] = lblItemBillingHead_Name.Text;
            drMC[3] = lblTotalPrice.Text;
            drMC[4] = lblRemark.Text;
            drMC[5] = lblAddtionsDeducEntry_ID.Text;
            dtMC.Rows.Add(drMC);
           
            

        }
        return dtMC;
    }
    private DataTable GetMilkADChildDetail()
    {

        DataTable dtMC = (DataTable)ViewState["dtadheadChild"];

        return dtMC;
    }
    private DataTable GetMilkADID()
    {
        DataTable dtADID = new DataTable();
        DataRow drADID;
        dtADID.Columns.Add("AddtionsDeducEntry_ID", typeof(int));
        dtADID.Columns.Add("ItemBillingHead_ID", typeof(int));
        DataTable dtMC = (DataTable)ViewState["dtadheadChild"];
        if(dtMC != null)
        {
            int Count = dtMC.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = dtMC.Rows[i];
                drADID = dtADID.NewRow();
                drADID[0] = dr["AddtionsDeducEntry_ID"];
                drADID[1] = dr["ItemBillingHead_ID"];

                //drMC[3] ="NA";
                dtADID.Rows.Add(drADID);
            }
        }
        return dtADID;
    }
    protected void btnGenerateInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblMsg.Text = "";
                string ProducerAdjustment = "0";
                string SocietyAdjustment = "0";
                string ChillingCost = "0";
                string HeadLoadCharges = "0";
				string ProducerAdjPayment = "0";
                string SocietyAdjPayment = "0";
				string ProducerPayment = "0";
				 if (lblProducerPayment.Text != "")
                {
                    ProducerPayment = lblProducerPayment.Text;
                }
                if (lblProcAdjustAmount.Text != "")
                {
                    ProducerAdjPayment = lblProcAdjustAmount.Text;
                }
                if (lblSocAdjustAmount.Text != "")
                {
                    SocietyAdjPayment = lblSocAdjustAmount.Text;
                }
                if (lblProcAdjustAmount.Text != "" || lblProcAdjustAmount.Text != "0.00")
                {
                    ProducerAdjustment = "1";
                }
                if (lblSocAdjustAmount.Text != "" || lblSocAdjustAmount.Text != "0.00")
                {
                    SocietyAdjustment = "1";
                }
                if (lblCC.Text == "")
                {
                    ChillingCost = "0";
                    
                }
                else
                {
                    ChillingCost = lblCC.Text;
                }
                if (lblHeadLoadCharges.Text == "")
                {
                    HeadLoadCharges = "0";
                    
                }
                else
                {
                    HeadLoadCharges = lblHeadLoadCharges.Text;
                }
                DataTable dtDispatch = new DataTable();
                dtDispatch = GetMilkDispatchDetail();

                DataTable dt2Buf = new DataTable();
                dt2Buf = GetMilkBuffDetail();

                DataTable dt3Cow = new DataTable();
                dt3Cow = GetMilkCowhDetail();

                DataTable dt4AD = new DataTable();
                dt4AD = GetMilkADDetail();

                DataTable dt4ADChild = new DataTable();
                dt4ADChild = GetMilkADChildDetail();

                DataTable dt4ADID = new DataTable();
                dt4ADID = GetMilkADID();
                // Runtime Validation

                string FDate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                string TDate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");


                DataSet dsValidationRuntime = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                    new string[] { "flag", "BillingCycleFromDate", "BillingCycleToDate", "GenerateTo_Office_ID" },
                     new string[] { "3", FDate, TDate, ddlSociety.SelectedValue }, "dataset");

                if (dsValidationRuntime.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice Already Generated!");
                    return;

                }
                else
                {

                    ds = null;
                    ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                            new string[] { "flag", 
			                                    "GenerateFrom_Office_ID",
			                                    "GenerateTo_Office_ID",
			                                    "BillingCycleFromDate",
			                                    "BillingCycleToDate",
			                                    "RouteNo",
			                                    "MilkValue",
			                                    "Commission",
			                                    "GrossEarning",
			                                    "DeductionAdditionValue",
                                                "ProducerPayment",
                                                "ProducerAdjPayment",
                                                "SocietyAdjPayment",
                                                "HeadLoadCharges",
                                                "ChillingCost",
                                                "NetAmount", 
			                                    "I_CreatedBy",
			                                    "V_IPAddress",
			                                    "V_MacAddress",
			                                    "V_EntryFrom",
                                                "NP_ProducerPayment",
                                                "ProducerAdjustment",
                                                "SocietyAdjustment",
                                                "Entry_ID",
												"FinalSubmitStatus"
                                                 },

                                            new string[] { "2",  
                                                objdb.Office_ID(),
                                                ddlSociety.SelectedValue,
                                                FDate,
                                                TDate,
                                                ddlBMCTankerRootName.SelectedValue,
                                                lblMilkValue.Text,
                                                lblCommission.Text,
                                                lblGrossEarning.Text,
                                                lbldeductionadditionValue.Text,
                                                ProducerPayment,
                                                ProducerAdjPayment,
                                                SocietyAdjPayment,
                                                HeadLoadCharges,
                                                ChillingCost,
                                                lblnetamount.Text,
                                                ViewState["Emp_ID"].ToString(),
                                                objdb.GetLocalIPAddress(),
                                                objdb.GetMACAddress(),
                                                "Web",
                                                ProducerPayment,
                                                ProducerAdjustment,
                                                SocietyAdjustment,
                                                ViewState["Entry_ID"].ToString(),
												"1"

                                                },
                                             new string[] { "type_SocietywiseMilkCollectionInvoiceChild1Dispatch",
                                                     "type_SocietywiseMilkCollectionInvoiceChild2Buf",
                                                     "type_SocietywiseMilkCollectionInvoiceChild3Cow", 
                                                     "type_SocietywiseMilkCollectionInvoiceChild4AD",
                                                     "type_MilkCollectionAdditionDeductionEntry_Child",
                                                     "Update_type_MilkCollectionAdditionDeductionEntry_Child"},
                                             new DataTable[] { dtDispatch, dt2Buf, dt3Cow, dt4AD, dt4ADChild, dt4ADID }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("SocietywiseMilkCollectionInvoice.aspx", false);

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        Session["IsSuccess"] = false;
                    }

                }


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
	protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBMCTankerRootName.ClearSelection();
            ddlBMCTankerRootName.Enabled = false;
            if (ddlSociety.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "Office_ID" }, new string[] { "2", ddlSociety.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBMCTankerRootName.SelectedValue = ds.Tables[0].Rows[0]["BMCTankerRoot_Id"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    //private void GetCompareDate()
    //{
    //    try
    //    {
    //        string myStringfromdat = txtFromDate.Text; // From Database
    //        DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //        string myStringtodate = txtToDate.Text; // From Database
    //        DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //        if (fdate <= tdate)
    //        {
    //            lblMsg.Text = string.Empty;
    //            GetItemDetails();
    //        }
    //        else
    //        {
    //            txtToDate.Text = string.Empty;
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6: ", ex.Message.ToString());
    //    }
    //}
}