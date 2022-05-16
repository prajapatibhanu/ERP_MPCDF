using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;


public partial class mis_MilkCollection_CCwiseMilkCollectionInvoice_Adiwasi : System.Web.UI.Page
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
                //txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                //txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
				txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                //CreateDataSet();
                CreateDataTable();
                GetCCDetails();
				if (objdb.Office_ID() == "2" || objdb.Office_ID() == "5")
                {
                    ddlBillingCycle.SelectedValue = "5 days";
                }
                else
                {
                    ddlBillingCycle.SelectedValue = "10 days";
                }
                txtDate_TextChanged(sender, e);


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


    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
			btnPrintInvoice.Visible = false;
            btnGenerateInvoice.Visible = false;
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            CreateDataTable();
            ViewState["GetTable"] = "";
            //GetData();
            //DataSet dsMainDataDetail = (DataSet)ViewState["dsMainDataDetail"];
            //string MilkValue;
            //string Commission;
            decimal TotalNetAmount = 0;
			decimal TotalMilkQty = 0;
            decimal TotalMilkValue = 0;
            decimal TotalCommission = 0;
            decimal TotalGrossEarning = 0;
            decimal TotalDeductionAdditionValue = 0;
            //decimal TotalProducerPayment = 0;
            //decimal TotalProducerAdjPayment = 0;
            //decimal TotalSocietyAdjPayment = 0;
            decimal TotalHeadLoadCharges = 0;
            decimal TotalChillingCost = 0;
            //string GrossEarning;
            //string DeductionAdditionValue;
            //string ProducerPayment;
            //string ProducerAdjPayment;
            //string SocietyAdjPayment;
            //string HeadLoadCharges;
            //string ChillingCost;
            //string I_OfficeID;
            //string Office_Name;
            //string MilkCollectionInvoice_ID;
            //string NetAmount;
            // string Quantity;
            DataSet dsFillGrid = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
                       new string[] { "flag", "Office_ID", "FDT", "TDT", "AdiwasiSociety" },
                       new string[] { "6", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"),"1" }, "dataset");
           
            
            if (dsFillGrid != null && dsFillGrid.Tables.Count > 0)
            {
                if (dsFillGrid.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = dsFillGrid.Tables[0];
                    GridView1.DataBind();
                     
                   
                    foreach (GridViewRow rows in GridView1.Rows)
                    {
                        Label lblNetAmount = (Label)rows.FindControl("lblNetAmount");
                        Label lblMilkCollectionInvoice_ID = (Label)rows.FindControl("lblMilkCollectionInvoice_ID");

                        Label lblFinalSubmitStatus = (Label)rows.FindControl("lblFinalSubmitStatus");
                        Label lblI_OfficeID = (Label)rows.FindControl("lblI_OfficeID");
                        Label lblQuantity = (Label)rows.FindControl("lblQuantity");
                        Label lblMilkValue = (Label)rows.FindControl("lblMilkValue");
                        Label lblCommission = (Label)rows.FindControl("lblCommission");
                        Label lblGrossEarning = (Label)rows.FindControl("lblGrossEarning");
                        Label lblDeductionAdditionValue = (Label)rows.FindControl("lblDeductionAdditionValue");
                        //Label lblProducerPayment = (Label)rows.FindControl("lblProducerPayment");
                        //Label lblProducerAdjPayment = (Label)rows.FindControl("lblProducerAdjPayment");
                        //Label lblSocietyAdjPayment = (Label)rows.FindControl("lblSocietyAdjPayment");
                        Label lblHeadLoadCharges = (Label)rows.FindControl("lblHeadLoadCharges");
                        Label lblChillingCost = (Label)rows.FindControl("lblChillingCost");
                        
                        if (lblMilkCollectionInvoice_ID.Text == "0")
                        {
                            if(ViewState["GetTable"].ToString() == "")
                            {
                                GetData();
                            }
                            btnGenerateInvoice.Visible = true;
                            btnReprocess.Visible = false;
                            btnFinalSubmit.Visible = false;
                            DataTable dtMainTable = (DataTable)ViewState["dtMainDataDetail"];
                            foreach (DataRow row in dtMainTable.Rows)
                            {

                                if (row["OfficeId"].ToString() == lblI_OfficeID.Text)
                                {
                                    lblMilkValue.Text = row["MilkValue"].ToString();
                                    lblCommission.Text = row["Commission"].ToString();
                                    lblGrossEarning.Text = row["GrossEarning"].ToString();
                                    lblDeductionAdditionValue.Text = Math.Abs(decimal.Parse(row["DeductionAdditionValue"].ToString())).ToString();
                                    
                                    lblHeadLoadCharges.Text = row["HeadLoadCharges"].ToString();
                                    lblChillingCost.Text = row["ChillingCost"].ToString();

                                    lblNetAmount.Text = row["NetAmount"].ToString();
                                    

                                }
                            }


                        }
                        else if (lblFinalSubmitStatus.Text == "0")
                        {
                            btnPrintInvoice.Visible = true;
                            btnGenerateInvoice.Visible = false;
                            btnReprocess.Visible = true;
                            btnFinalSubmit.Visible = true;
                           
                        }
                        else
                        {
                            btnPrintInvoice.Visible = true;
                            btnGenerateInvoice.Visible = false;
                            btnReprocess.Visible = false;
                            btnFinalSubmit.Visible = false;
                            
                        }
                        TotalNetAmount += decimal.Parse(lblNetAmount.Text);
                        TotalMilkValue += decimal.Parse(lblMilkValue.Text); 
                        TotalMilkQty += decimal.Parse(lblQuantity.Text);
                        TotalCommission += decimal.Parse(lblCommission.Text);
                        TotalGrossEarning += decimal.Parse(lblGrossEarning.Text);
                        TotalDeductionAdditionValue += decimal.Parse(lblDeductionAdditionValue.Text);
                       
                        TotalHeadLoadCharges += decimal.Parse(lblHeadLoadCharges.Text);
                        TotalChillingCost += decimal.Parse(lblChillingCost.Text);
                    }
                    GridView1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GridView1.FooterRow.Cells[3].Text = "<b>" + TotalMilkQty.ToString() + "</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + TotalMilkValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + TotalCommission.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + TotalGrossEarning.ToString() + "</b>";
                    GridView1.FooterRow.Cells[7].Text = "<b>" + TotalDeductionAdditionValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalProducerPayment.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalProducerAdjPayment.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalSocietyAdjPayment.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalHeadLoadCharges.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalChillingCost.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalNetAmount.ToString() + "</b>";
                }
            }


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
    protected void btnGenerateInvoice_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblMsg.Text = "";
                int Status = 0;

                string FDate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                string TDate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                    
                DataTable dtMainDataDetail = (DataTable)ViewState["dtMainDataDetail"];

                DataTable dtDispatch = (DataTable)ViewState["dtMilkDispatch"];

                DataTable dt2Buf = (DataTable)ViewState["dtMilkBuff"];

                DataTable dt3Cow = (DataTable)ViewState["dtMilkCowh"];

                DataTable dt4AD = (DataTable)ViewState["dtGetMilkADDetail"];
                
                DataTable dtMC = new DataTable();

                dtMC.Columns.Add(new DataColumn("OfficeId", typeof(string)));
                dtMC.Columns.Add(new DataColumn("Head_ID", typeof(string)));
                dtMC.Columns.Add(new DataColumn("HeadType", typeof(string)));
                dtMC.Columns.Add(new DataColumn("HeadName", typeof(string)));
                dtMC.Columns.Add(new DataColumn("HeadAmount", typeof(decimal)));
                dtMC.Columns.Add(new DataColumn("HeadRemark", typeof(string)));
				dtMC.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(string)));
                foreach (DataRow drow in dt4AD.Rows)
                {
                    string OfficeId = drow["OfficeId"].ToString();
                    string Head_ID = drow["ItemBillingHead_ID"].ToString();
                    string HeadType = drow["ItemBillingHead_Type"].ToString();
                    string HeadName = drow["ItemBillingHead_Name"].ToString();
                    string HeadAmount = drow["TotalAmount"].ToString();
                    string HeadRemark = drow["Remark"].ToString();
					string AddtionsDeducEntry_ID = drow["AddtionsDeducEntry_ID"].ToString();
                    dtMC.Rows.Add(OfficeId,Head_ID, HeadType, HeadName, HeadAmount, HeadRemark, AddtionsDeducEntry_ID);
                }

                DataTable dt4ADChild = (DataTable)ViewState["dtadheadChild"];
                DataTable dtADID = new DataTable();
                DataRow drADID;
                dtADID.Columns.Add("OfficeId", typeof(int));
                dtADID.Columns.Add("AddtionsDeducEntry_ID", typeof(int));
                dtADID.Columns.Add("ItemBillingHead_ID", typeof(int));
               
                if (dt4ADChild != null)
                {
                    int Count = dt4ADChild.Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        DataRow dr = dt4ADChild.Rows[i];
                        drADID = dtADID.NewRow();
                        drADID[0] = dr["OfficeId"];
                        drADID[1] = dr["AddtionsDeducEntry_ID"];
                        drADID[2] = dr["ItemBillingHead_ID"];
               
                     
                        dtADID.Rows.Add(drADID);
                    }
                }
                           

               ds = null;
               ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                                    new string[] { "flag", 
                                                   "GenerateFrom_Office_ID", 
                                                   "BillingCycleFromDate",
                                                   "BillingCycleToDate",
                                                   "RouteNo",                       
                                                   "I_CreatedBy",
                                                   "V_IPAddress",
                                                   "V_MacAddress",
                                                   "V_EntryFrom" ,
						                           "FinalSubmitStatus",
						                           "CC_Id",
						                           "OfficeType_ID"
                                                 },
                                                 new string[] { "22",  
                                                 objdb.Office_ID(),
                                                 FDate,
                                                 TDate,
                                                 "0",                       
                                                 ViewState["Emp_ID"].ToString(),
                                                 objdb.GetLocalIPAddress(),
                                                 objdb.GetMACAddress(),
                                                 "Web",
						                         "0",
						                         ddlccbmcdetail.SelectedValue,
                                                 objdb.OfficeType_ID()

                                                 },
                                                 new string[] {
                                                 "type_SocietywiseMilkCollectionInvoiceMaster",
                                                 "type_SocietywiseMilkCollectionInvoiceChild1Dispatch",
                                                 "type_SocietywiseMilkCollectionInvoiceChild2Buf",
                                                 "type_SocietywiseMilkCollectionInvoiceChild3Cow", 
                                                 "type_SocietywiseMilkCollectionInvoiceChild4AD",
                                                 "type_MilkCollectionAdditionDeductionEntry_Child",
                                                 "Update_type_MilkCollectionAdditionDeductionEntry_Child"                      },
                                                 new DataTable[] {
                                                 dtMainDataDetail,
                                                 dtDispatch, 
                                                 dt2Buf, 
                                                 dt3Cow, 
                                                 dtMC, 
                                                 dt4ADChild,
                                                 dtADID }, "TableSave");



               objdb.ByProcedure("Sp_NPDWMShifting", new string[] { "flag", "CC_Id", "BillingPeriodFromDate", "BillingPeriodToDate", "CreatedAt", "CreatedBy", "CreatedByIP" }, new string[] { "2", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");    
                    Session["IsSuccess"] = true;
                    btnSubmit_Click(sender, e);
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
               

            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }

    protected void FillInvoice_New(string SocietyID)
    {
        try
        {
            lblMsg.Text = "";
            gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
            gv_SocietyMilkDispatchDetail.DataBind();
            gv_SocietyBufData.DataSource = string.Empty;
            gv_SocietyBufData.DataBind();
            gv_SocietyCowData.DataSource = string.Empty;
            gv_SocietyCowData.DataBind();
            grhradsdetails.DataSource = string.Empty;
            grhradsdetails.DataBind();
            lblMilkValue.Text = "";
            lblCommission.Text = "";
            lblGrossEarning.Text = "";
            lblProducerPayment.Text = "";
            lblSocAdjustAmount.Text = "";
            lblProcAdjustAmount.Text = "";
            lblCC.Text = "";
            lblHeadLoadCharges.Text = "";
            lblnetamount.Text = "";
            decimal MilkQty_InKG = 0;
            decimal FAT_IN_KG = 0;
            decimal SNF_IN_KG = 0;
            decimal Value = 0;
            decimal MilkValue = 0;
            decimal BufFatKg = 0;
            decimal CowFatKg = 0;
            decimal CowSnfKg = 0;
            decimal Prodvalue = 0;
            FS_DailyReport.Visible = true;
            FS_DailyReport_Shift.Visible = true;

            DataTable dtMainDataDetail = (DataTable)ViewState["dtMainDataDetail"];
            DataTable dtMilkDispatch = (DataTable)ViewState["dtMilkDispatch"];
            DataTable dtMilkBuff = (DataTable)ViewState["dtMilkBuff"];
            DataTable dtMilkCowh = (DataTable)ViewState["dtMilkCowh"];
            DataTable dtGetMilkADDetail = (DataTable)ViewState["dtGetMilkADDetail"];
            //ds = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
            //          new string[] { "flag", "OfficeId", "FDT", "TDT" },
            //          new string[] { "4", SocietyID, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    lblSociety.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
            //    lblbankInfo.Text = ds.Tables[0].Rows[0]["IFSC"].ToString() + " / " + ds.Tables[0].Rows[0]["BankName"].ToString() + " / " + ds.Tables[0].Rows[0]["BankAccountNo"].ToString();
            //    lblOfficename.Text = ds.Tables[0].Rows[0]["AttachUnitName"].ToString() + " / " + ds.Tables[0].Rows[0]["AttachUnitCode"].ToString();
            //    lblBillingPeriod.Text = ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString();
            //}
            if (dtMainDataDetail != null)
            {

                DataView dv = new DataView(dtMainDataDetail);
                dv.RowFilter = "OfficeId =" + SocietyID + "";
                DataTable dtMain = new DataTable();
                dtMain = dv.ToTable();

                lblMilkValue.Text = dtMain.Rows[0]["MilkValue"].ToString();
                lblCommission.Text = dtMain.Rows[0]["Commission"].ToString();
                lblGrossEarning.Text = dtMain.Rows[0]["GrossEarning"].ToString();
                lbldeductionadditionValue.Text = dtMain.Rows[0]["DeductionAdditionValue"].ToString();
                lblProducerPayment.Text = dtMain.Rows[0]["ProducerPayment"].ToString();
                lblProcAdjustAmount.Text = dtMain.Rows[0]["ProducerAdjPayment"].ToString();
                lblSocAdjustAmount.Text = dtMain.Rows[0]["SocietyAdjPayment"].ToString();
                lblCC.Text = dtMain.Rows[0]["ChillingCost"].ToString();
                lblHeadLoadCharges.Text = dtMain.Rows[0]["HeadLoadCharges"].ToString();
                lblnetamount.Text = dtMain.Rows[0]["NetAmount"].ToString();
                lblSociety.Text = dtMain.Rows[0]["Office_Name_E"].ToString() + " / " + dtMain.Rows[0]["SocietyCode"].ToString();
                lblbankInfo.Text = dtMain.Rows[0]["IFSC"].ToString() + " / " + dtMain.Rows[0]["BankName"].ToString() + " / " + dtMain.Rows[0]["AccountNo"].ToString();
                //lblOfficename.Text = dtMain.Rows[0]["AttachUnitName"].ToString() + " / " + dtMain.Rows[0]["AttachUnitCode"].ToString();
                lblBillingPeriod.Text = txtFdt.Text + " To " + txtTdt.Text;
                if(objdb.Office_ID() == "4")
                {

                    lblNarration.Text = dtMain.Rows[0]["CommisionNarration"].ToString();
                
                }
                else
                {
                    lblNarration.Text = "";
                }
            }
            if (dtMilkDispatch != null)
            {


                DataView dv = new DataView(dtMilkDispatch);
                dv.RowFilter = "OfficeId =" + SocietyID + "";
                //dt = dv.ToTable;
                gv_SocietyMilkDispatchDetail.DataSource = dv;
                gv_SocietyMilkDispatchDetail.DataBind();


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
            }
            if (dtMilkBuff != null)
            {
                
                DataView dv = new DataView(dtMilkBuff);
                dv.RowFilter = "OfficeId =" + SocietyID + "";
                gv_SocietyBufData.DataSource = dv;
                gv_SocietyBufData.DataBind();
                foreach (GridViewRow rows in gv_SocietyBufData.Rows)
                {

                    Label lblFAT_IN_KG = (Label)rows.FindControl("lblFAT_IN_KG");

                    if (lblFAT_IN_KG.Text != "")
                    {
                        BufFatKg = Convert.ToDecimal(lblFAT_IN_KG.Text);

                    }
                }
            }

            if (dtMilkCowh != null)
            {
                
                DataView dv = new DataView(dtMilkCowh);
                dv.RowFilter = "OfficeId =" + SocietyID + "";
                gv_SocietyCowData.DataSource = dv;
                gv_SocietyCowData.DataBind();
                foreach (GridViewRow rows in gv_SocietyCowData.Rows)
                {
                    Label lblSNF_IN_KG = (Label)rows.FindControl("lblSNF_IN_KG");
                    Label lblFAT_IN_KG = (Label)rows.FindControl("lblFAT_IN_KG");
                    if (lblSNF_IN_KG.Text != "")
                    {
                        CowSnfKg = Convert.ToDecimal(lblSNF_IN_KG.Text);

                    }
                    if (lblFAT_IN_KG.Text != "")
                    {
                        CowFatKg = Convert.ToDecimal(lblFAT_IN_KG.Text);

                    }
                }
            }

            if (dtGetMilkADDetail != null)
            {
                
                DataView dv = new DataView(dtGetMilkADDetail);
                dv.RowFilter = "OfficeId =" + SocietyID + "";
                grhradsdetails.DataSource = dv;
                grhradsdetails.DataBind();
                Label lblGrandTotal = (grhradsdetails.FooterRow.FindControl("lblGrandTotal") as Label);
                foreach (GridViewRow rowcc in grhradsdetails.Rows)
                {
                    Label lblTotalPrice = (Label)rowcc.FindControl("lblTotalPrice");
                    Label lblItemBillingHead_Type = (Label)rowcc.FindControl("lblItemBillingHead_Type");


                    if (lblTotalPrice.Text != "")
                    {
                        if (lblItemBillingHead_Type.Text == "ADDITION")
                        {
                            //Prodvalue += Decimal.Parse(lblTotalPrice.Text);
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
                lblGrandTotal.Text = Prodvalue.ToString("0.00");
            }


            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void CreateDataTable()
    {
       
        DataTable dtMainDataDetail = new DataTable();

        dtMainDataDetail.Columns.Add("OfficeId", typeof(int));
        dtMainDataDetail.Columns.Add("MilkValue", typeof(string));
        dtMainDataDetail.Columns.Add("Commission", typeof(string));
        dtMainDataDetail.Columns.Add("GrossEarning", typeof(string));
        dtMainDataDetail.Columns.Add("DeductionAdditionValue", typeof(string));
        dtMainDataDetail.Columns.Add("ProducerPayment", typeof(string));
        dtMainDataDetail.Columns.Add("ProducerAdjPayment", typeof(string));
        dtMainDataDetail.Columns.Add("SocietyAdjPayment", typeof(string));
        dtMainDataDetail.Columns.Add("HeadLoadCharges", typeof(string));
        dtMainDataDetail.Columns.Add("ChillingCost", typeof(string));
        dtMainDataDetail.Columns.Add("NetAmount", typeof(string));
        dtMainDataDetail.Columns.Add("CommisionNarration", typeof(string));
        dtMainDataDetail.Columns.Add("Office_Name_E", typeof(string));
        dtMainDataDetail.Columns.Add("SocietyCode", typeof(string));
        dtMainDataDetail.Columns.Add("AccountNo", typeof(string));
        dtMainDataDetail.Columns.Add("IFSC", typeof(string));
        dtMainDataDetail.Columns.Add("BankName", typeof(string));
        ViewState["dtMainDataDetail"] = dtMainDataDetail;
        DataTable dtMilkDispatch = new DataTable();

        dtMilkDispatch.Columns.Add(new DataColumn("OfficeId", typeof(int)));
        dtMilkDispatch.Columns.Add(new DataColumn("Date", typeof(DateTime)));
        dtMilkDispatch.Columns.Add(new DataColumn("Shift", typeof(string)));
        dtMilkDispatch.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMilkDispatch.Columns.Add(new DataColumn("FatPer", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("SnfPer", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("MilkValue", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dtMilkDispatch.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMilkDispatch.Columns.Add(new DataColumn("RatePerLtr", typeof(decimal)));

        ViewState["dtMilkDispatch"] = dtMilkDispatch;
        DataTable dtMilkCowh = new DataTable();

        dtMilkCowh.Columns.Add(new DataColumn("OfficeId", typeof(int)));
        dtMilkCowh.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMilkCowh.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMilkCowh.Columns.Add(new DataColumn("FatPer", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("SnfPer", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("MilkValue", typeof(decimal)));
        dtMilkCowh.Columns.Add(new DataColumn("Rate", typeof(decimal)));

        ViewState["dtMilkCowh"] = dtMilkCowh;

        DataTable dtMilkBuff = new DataTable();

        dtMilkBuff.Columns.Add(new DataColumn("OfficeId", typeof(string)));
        dtMilkBuff.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMilkBuff.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMilkBuff.Columns.Add(new DataColumn("FatPer", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("SnfPer", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("MilkValue", typeof(decimal)));
        dtMilkBuff.Columns.Add(new DataColumn("Rate", typeof(decimal)));

        ViewState["dtMilkBuff"] = dtMilkBuff;

        DataTable dtGetMilkADDetail = new DataTable();
        dtGetMilkADDetail.Columns.Add(new DataColumn("OfficeId", typeof(int)));
        dtGetMilkADDetail.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
        dtGetMilkADDetail.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
        dtGetMilkADDetail.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
        dtGetMilkADDetail.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));
        dtGetMilkADDetail.Columns.Add(new DataColumn("Remark", typeof(string)));
        dtGetMilkADDetail.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(decimal)));

        ViewState["dtGetMilkADDetail"] = dtGetMilkADDetail;

        DataTable dtadheadChild = new DataTable();

        dtadheadChild.Columns.Add(new DataColumn("OfficeId", typeof(int)));
        dtadheadChild.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(int)));
        dtadheadChild.Columns.Add(new DataColumn("CycleAmount", typeof(decimal)));
        dtadheadChild.Columns.Add(new DataColumn("PrevoiusAmount", typeof(decimal)));
        dtadheadChild.Columns.Add(new DataColumn("DeductedAmount", typeof(decimal)));

        dtadheadChild.Columns.Add(new DataColumn("BalanceAmount", typeof(decimal)));
        dtadheadChild.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
        dtadheadChild.Columns.Add(new DataColumn("BillingFromDate", typeof(string)));
        dtadheadChild.Columns.Add(new DataColumn("BillingToDate", typeof(string)));
        dtadheadChild.Columns.Add(new DataColumn("Status", typeof(int)));

        ViewState["dtadheadChild"] = dtadheadChild;


    }
    protected void GetData()
    {



        //DataSet ds1 = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
        //     new string[] { "flag", "Office_ID", "FDT", "TDT" },
        //     new string[] { "5", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        //int ds1Count = ds1.Tables[0].Rows.Count;

        foreach (GridViewRow rows in GridView1.Rows)
        {
            Label lblI_OfficeID = (Label)rows.FindControl("lblI_OfficeID");
            string DisDate = "";
            string Shift = "";
            decimal MShiftCount = 0;
            decimal TShiftCount = 0;
            decimal EShiftCount = 0;
            decimal TotalDays = 0;
            decimal TotalFatinKg = 0;
            decimal CowRate = 0;
            decimal Rate = 0;
            decimal BuffRate = 0;
            decimal CowSnfKg = 0;
            decimal TMilkQty_InKG = 0;
            decimal TFAT_IN_KG = 0;
            decimal pwf = 0;
            decimal CowFatKg = 0;
            decimal BufFatKg = 0;
            decimal MilkValue = 0;
            decimal Commission = 0;
            decimal GrossEarning = 0;
            decimal GrossEarning_T = 0;
            decimal DeductionAdditionValue = 0;
            decimal ProducerPayment = 0;
            decimal ProducerAdjPayment = 0;
            decimal SocietyAdjPayment = 0;
            decimal NetAmount = 0;
            decimal ChillingCost = 0;
            decimal HeadLoadCharges = 0;
            decimal AutoDeduction = 0;
            decimal TotalEarning = 0;
            string CommisionNarration = "";
           // string OfficeId = ds1.Tables[0].Rows[j]["Office_ID"].ToString();
            string OfficeId =lblI_OfficeID.Text;
            ds = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
                        new string[] { "flag", "FDT", "TDT", "OfficeId", "Office_ID","CCID" },
                        new string[] { "1", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), OfficeId, objdb.Office_ID(),ddlccbmcdetail.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TotalFatinKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                    MilkValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Value"));
                    DataTable dtMilkDispatch = (DataTable)ViewState["dtMilkDispatch"];
                    
                    
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        //string DT_DispatchDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["DT_DispatchDate"].ToString(),cult).ToString("yyyy/MM/dd");
						string DT_DispatchDate = ds.Tables[0].Rows[i]["DT_DispatchDate"].ToString();
                        string V_Shift = ds.Tables[0].Rows[i]["V_Shift"].ToString();
                        string V_MilkType = ds.Tables[0].Rows[i]["V_MilkType"].ToString();
                        decimal FAT_Per = decimal.Parse(ds.Tables[0].Rows[i]["FAT_Per"].ToString());
                        decimal SNF_Per = decimal.Parse(ds.Tables[0].Rows[i]["SNF_Per"].ToString());
                        decimal MilkQty_InKG = decimal.Parse(ds.Tables[0].Rows[i]["MilkQty_InKG"].ToString());
                        decimal FAT_IN_KG = decimal.Parse(ds.Tables[0].Rows[i]["FAT_IN_KG"].ToString());
                        decimal SNF_IN_KG = decimal.Parse(ds.Tables[0].Rows[i]["SNF_IN_KG"].ToString());
                        decimal Value = decimal.Parse(ds.Tables[0].Rows[i]["Value"].ToString());
                        decimal CLR = decimal.Parse(ds.Tables[0].Rows[i]["CLR"].ToString());
                        string D_MilkQuality = ds.Tables[0].Rows[i]["D_MilkQuality"].ToString();
                        decimal Rate_Per_Ltr = decimal.Parse(ds.Tables[0].Rows[i]["Rate_Per_Ltr"].ToString());
                        dtMilkDispatch.Rows.Add(OfficeId, ds.Tables[0].Rows[i]["DT_DispatchDate"].ToString(), V_Shift, V_MilkType, FAT_Per, SNF_Per, MilkQty_InKG, FAT_IN_KG, SNF_IN_KG, Value, CLR, D_MilkQuality, Rate_Per_Ltr);
                        if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6")
                        {
                            if (D_MilkQuality == "Good")
                            {
                                pwf += FAT_IN_KG;
                            }
                        }
                        else
                        {
                            pwf += FAT_IN_KG;
                        }
                        if (DisDate == DT_DispatchDate)
                        {
                            if (Shift != V_Shift)
                            {
                                if (V_Shift == "M")
                                {
                                    MShiftCount += 1;
                                }
                                if (V_Shift == "E")
                                {
                                    EShiftCount += 1;
                                }
                            }
                        }
                        else
                        {
                            TotalDays += 1;
                            if (V_Shift == "M")
                            {
                                MShiftCount += 1;
                            }
                            if (V_Shift == "E")
                            {
                                EShiftCount += 1;
                            }
                        }

                        DisDate = DT_DispatchDate;
                        Shift = V_Shift;
                    }
                    TShiftCount = MShiftCount + EShiftCount;
                    TFAT_IN_KG = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                 
                    TMilkQty_InKG = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQty_InKG"));
                    ViewState["dtMilkDispatch"] = dtMilkDispatch;
                    
                }
                DataTable dtMainDataDetail = (DataTable)ViewState["dtMainDataDetail"];
                
                
                if (ds.Tables[7].Rows.Count > 0)
                {
                    //ProducerPayment = Math.Round(decimal.Parse(ds.Tables[7].Rows[0]["Final_ProducerPayableAmount"].ToString()), 2);
                }
                if (ds.Tables[8].Rows.Count > 0)
                {
                    //ProducerAdjPayment = Math.Round(decimal.Parse(ds.Tables[8].Rows[0]["ProcAdjustAmount"].ToString()), 2);
                }
                if (ds.Tables[9].Rows.Count > 0)
                {
                   // SocietyAdjPayment = Math.Round(decimal.Parse(ds.Tables[9].Rows[0]["SocAdjustAmount"].ToString()), 2);
                }

                //Calculate Chilling Cost
                String FDate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                DateTime Mtrl_datevalue = (Convert.ToDateTime(FDate.ToString()));

                int Mtrl_dy = int.Parse(Mtrl_datevalue.Day.ToString());
                if (Mtrl_dy >= 21)
                {
                    DataSet dschillingcost = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "FDT" }, new string[] { "22", OfficeId, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                    if (dschillingcost != null && dschillingcost.Tables.Count > 0)
                    {
                        if (dschillingcost.Tables[0].Rows.Count > 0)
                        {
                            ChillingCost = Math.Round(decimal.Parse(dschillingcost.Tables[0].Rows[0]["CC"].ToString()), 2);
                        }
                    }
                }
                //Calculate HeadLoadCharges

               
                    string flag = "23";
                    if (objdb.Office_ID() == "5")
                    {
                        flag = "24";
                        lblHeadLoadCharges.Visible = true;
                        DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { flag, OfficeId, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
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
                        DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { flag, OfficeId, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                        if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                        {
                            if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                            {
                                HeadLoadCharges = Math.Round(((TMilkQty_InKG / 1.030M) * decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString())), 2);
                            }
                        }
                    }
                    else if (objdb.Office_ID() == "6")
                    {

                        flag = "26";
                        if (TMilkQty_InKG != 0)
                        {
                            decimal LitreperDay = (TMilkQty_InKG / (TotalDays));
                            if (LitreperDay >= 21)
                            {
                                // LitreperDay = Math.Round((TMilkQty_InKG / (TotalDays)),0);
                                decimal Distance = 0;
                                lblHeadLoadCharges.Visible = true;
                                DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT", "LitreperDay" }, new string[] { flag, OfficeId, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), LitreperDay.ToString() }, "dataset");
                                if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                                {
                                    if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                                    {
                                        if (dsHeadLoadCharges.Tables[0].Rows[0]["Distance"].ToString() != "0" && dsHeadLoadCharges.Tables[0].Rows[0]["Rate"].ToString() != "0")
                                        {
                                            Distance = decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["Distance"].ToString());
                                            HeadLoadCharges = Math.Round((((Distance * 2) * decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["Rate"].ToString()) * TShiftCount)), 2);
                                        }

                                    }
                                }
                            }
                        }



                    }
                    else
                    {
                        if (Mtrl_dy >= 21)
                        {
                            lblHeadLoadCharges.Visible = true;
                            DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { flag, OfficeId, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                            if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                            {
                                if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                                {
                                    HeadLoadCharges = decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString());
                                }
                            }
                        }
                    }


                if (ds.Tables[2].Rows.Count > 0)
                {
                    //CowFatKg = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                    //CowSnfKg = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("SNF_IN_KG"));
                    DataTable dtMilkCowh = (DataTable)ViewState["dtMilkCowh"];
                    
                    int Count1 = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < Count1; i++)
                    {
                        string V_MilkType = ds.Tables[2].Rows[i]["V_MilkType"].ToString();
                        string D_MilkQuality = ds.Tables[2].Rows[i]["D_MilkQuality"].ToString();
                        decimal FAT_Per = decimal.Parse(ds.Tables[2].Rows[i]["FAT_Per"].ToString());
                        decimal SNF_Per = decimal.Parse(ds.Tables[2].Rows[i]["SNF_Per"].ToString());

                        decimal MilkQty_InKGC = decimal.Parse(ds.Tables[2].Rows[i]["MilkQty_InKG"].ToString());
                        decimal FAT_IN_KGC = decimal.Parse(ds.Tables[2].Rows[i]["FAT_IN_KG"].ToString());
                        decimal SNF_IN_KG = decimal.Parse(ds.Tables[2].Rows[i]["SNF_IN_KG"].ToString());
                        decimal Value = decimal.Parse(ds.Tables[2].Rows[i]["Value"].ToString());
                        decimal CLR = decimal.Parse(ds.Tables[2].Rows[i]["CLR"].ToString());

                        dtMilkCowh.Rows.Add(OfficeId ,V_MilkType, D_MilkQuality, FAT_Per, SNF_Per, MilkQty_InKGC, FAT_IN_KGC, SNF_IN_KG, CLR, Value, 0);
                        if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6")
                        {
                            if (D_MilkQuality == "Good")
                            {
                                CowFatKg += FAT_IN_KGC;
                                CowSnfKg += SNF_IN_KG;
                            }
                        }
                        else
                        {
                            CowFatKg += FAT_IN_KGC;
                            CowSnfKg += SNF_IN_KG;
                        }
                    }

                    ViewState["dtMilkCowh"] = dtMilkCowh;
                    

                }
                if (ds.Tables[1].Rows.Count > 0)
                {

                    //BufFatKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                    DataTable dtMilkBuff = (DataTable)ViewState["dtMilkBuff"];
                   

                   
                    int Count2 = ds.Tables[1].Rows.Count;
                    for (int i = 0; i < Count2; i++)
                    {
                        string V_MilkType = ds.Tables[1].Rows[i]["V_MilkType"].ToString();
                        string D_MilkQuality = ds.Tables[1].Rows[i]["D_MilkQuality"].ToString();
                        decimal FAT_Per = decimal.Parse(ds.Tables[1].Rows[i]["FAT_Per"].ToString());
                        decimal SNF_Per = decimal.Parse(ds.Tables[1].Rows[i]["SNF_Per"].ToString());

                        decimal MilkQty_InKGB = decimal.Parse(ds.Tables[1].Rows[i]["MilkQty_InKG"].ToString());
                        decimal FAT_IN_KGB = decimal.Parse(ds.Tables[1].Rows[i]["FAT_IN_KG"].ToString());
                        decimal SNF_IN_KG = decimal.Parse(ds.Tables[1].Rows[i]["SNF_IN_KG"].ToString());
                        decimal Value = decimal.Parse(ds.Tables[1].Rows[i]["Value"].ToString());
                        decimal CLR = decimal.Parse(ds.Tables[1].Rows[i]["CLR"].ToString());

                        dtMilkBuff.Rows.Add(OfficeId,V_MilkType, D_MilkQuality, FAT_Per, SNF_Per, MilkQty_InKGB, FAT_IN_KGB, SNF_IN_KG, CLR, Value, 0);
                        if (objdb.Office_ID() == "4" || objdb.Office_ID() == "6")
                        {
                            if (D_MilkQuality == "Good")
                            {
                                BufFatKg += FAT_IN_KGB;

                            }
                        }
                        else
                        {
                            BufFatKg += FAT_IN_KGB;
                        }
                    }
                    ViewState["dtMilkBuff"] = dtMilkBuff;
                    
                }



                if (ds.Tables[3].Rows.Count > 0)
                {

                    BuffRate = decimal.Parse(ds.Tables[3].Rows[0]["Rate"].ToString());

                }
                if (ds.Tables[4].Rows.Count > 0)
                {

                    CowRate = decimal.Parse(ds.Tables[4].Rows[0]["Rate"].ToString());

                }
                if (ds.Tables[5].Rows.Count > 0)
                {

                    Rate = decimal.Parse(ds.Tables[5].Rows[0]["Rate"].ToString());

                }
                if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4" || objdb.Office_ID() == "5")
                {
                    if (ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "1" || ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "3")
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
                            lblCommission.Text = (pwf * Rate).ToString("0.000");

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
                    if (ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "1" || ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "3")
                    {
                        lblCommission.Text = (pwf * Rate).ToString("0.000");
                    }
                    else
                    {
                        lblCommission.Text = "0";
                    }
                }

                lblMilkValue.Text = MilkValue.ToString("0.00");
                lblCommission.Text = Math.Round(Convert.ToDecimal(lblCommission.Text), 2).ToString();
                Commission = decimal.Parse(lblCommission.Text);
                CommisionNarration = "DCS.Comm.  Buf @Rs  " + BuffRate + "/KgFat  &  Cow @Rs.  " + CowRate + "/KgTS:- " + Commission;
                lblGrossEarning.Text = (Math.Round(MilkValue + Convert.ToDecimal(lblCommission.Text), 2)).ToString();
                GrossEarning_T = decimal.Parse(lblGrossEarning.Text);
                //DataSet dsGetMilkADDetail = (DataSet)ViewState["dsGetMilkADDetail"];
                //DataSet dsGetMilkADChildDetail = (DataSet)ViewState["dsGetMilkADChildDetail"];
                DataTable dtGetMilkADDetail = (DataTable)ViewState["dtGetMilkADDetail"];
                
                if (ds.Tables[6].Rows.Count > 0)
                {
                    DataTable dtadheadChild = (DataTable)ViewState["dtadheadChild"];
                    //dtadheadChild.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(int)));
                    //dtadheadChild.Columns.Add(new DataColumn("CycleAmount", typeof(decimal)));
                    //dtadheadChild.Columns.Add(new DataColumn("PrevoiusAmount", typeof(decimal)));
                    //dtadheadChild.Columns.Add(new DataColumn("DeductedAmount", typeof(decimal)));

                    //dtadheadChild.Columns.Add(new DataColumn("BalanceAmount", typeof(decimal)));
                    //dtadheadChild.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                    //dtadheadChild.Columns.Add(new DataColumn("BillingFromDate", typeof(string)));
                    //dtadheadChild.Columns.Add(new DataColumn("BillingToDate", typeof(string)));
                    //dtadheadChild.Columns.Add(new DataColumn("Status", typeof(int)));
                    int Count = ds.Tables[6].Rows.Count;
                    //DataTable dtadhead = new DataTable();
                    //dtadhead.Columns.Add(new DataColumn("S.No", typeof(int)));
                    //dtadhead.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                    //dtadhead.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                    //dtadhead.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                    //dtadhead.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));
                    //dtadhead.Columns.Add(new DataColumn("Remark", typeof(string)));


                    

                    DataRow dr, dr1;
                    if(objdb.Office_ID() == "3")
                    {
                        if(ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "1")
                        {
                            dr = dtGetMilkADDetail.NewRow();
                            dr[0] = OfficeId;
                            dr[1] = "DEDUCTION";
                            dr[2] = "0";
                            dr[3] = "N.R.D.";
                            if (objdb.Office_ID() == "4")
                            {
                                dr[4] = Math.Round((pwf * Convert.ToDecimal(2.25)), 2).ToString();
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((pwf * Convert.ToDecimal(2.25)), 2).ToString());
                                AutoDeduction += Math.Round((pwf * Convert.ToDecimal(2.25)), 2);

                            }
                            else
                            {
                                dr[4] = Math.Round((TFAT_IN_KG * 1), 2).ToString();
                                AutoDeduction = Math.Round((TFAT_IN_KG * 1), 2);
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((TFAT_IN_KG * 1), 2).ToString());

                            }
                            dr[5] = "";
                            dr[6] = "0";
                            dtGetMilkADDetail.Rows.Add(dr);
                            if (objdb.Office_ID() == "3")
                            {
                                dr = dtGetMilkADDetail.NewRow();
                                dr[0] = OfficeId;
                                dr[1] = "DEDUCTION";
                                dr[2] = "0";
                                dr[3] = "Audit Fees";
                                dr[4] = Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0).ToString();

                                dr[5] = "";
                                dr[6] = "0";
                                DeductionAdditionValue -= Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0);
                                AutoDeduction += Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0);
                                dtGetMilkADDetail.Rows.Add(dr);
                            }
                        }
                    }
                    else
                    {
                        if (ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "1" || ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "3")
                        {
                            dr = dtGetMilkADDetail.NewRow();
                            dr[0] = OfficeId;
                            dr[1] = "DEDUCTION";
                            dr[2] = "0";
                            dr[3] = "N.R.D.";
                            if (objdb.Office_ID() == "4")
                            {
                                dr[4] = Math.Round((pwf * Convert.ToDecimal(2.25)), 2).ToString();
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((pwf * Convert.ToDecimal(2.25)), 2).ToString());
                                AutoDeduction += Math.Round((pwf * Convert.ToDecimal(2.25)), 2);

                            }
                            else
                            {
                                dr[4] = Math.Round((TFAT_IN_KG * 1), 2).ToString();
                                AutoDeduction = Math.Round((TFAT_IN_KG * 1), 2);
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((TFAT_IN_KG * 1), 2).ToString());

                            }
                            dr[5] = "";
                            dr[6] = "0";
                            dtGetMilkADDetail.Rows.Add(dr);
                            //if (objdb.Office_ID() == "3")
                            //{
                            //    dr = dtGetMilkADDetail.NewRow();
                            //    dr[0] = dtGetMilkADDetail.Rows.Count + 1;
                            //    dr[1] = "DEDUCTION";
                            //    dr[2] = "0";
                            //    dr[3] = "Audit Fees";
                            //    dr[4] = Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0).ToString();

                            //    dr[5] = "";
                            //    dr[6] = "0";
                            //    DeductionAdditionValue -= Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0);
                            //    AutoDeduction += Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0);
                            //    dtGetMilkADDetail.Rows.Add(dr);
                            //}
                            //if (objdb.Office_ID() == "3")
                            //{
                            //dr = dtadhead.NewRow();
                            //dr[0] = dtadhead.Rows.Count + 1;
                            //dr[1] = "DEDUCTION";
                            //dr[2] = "0";
                            //dr[3] = "Development Charges";
                            //dr[4] = Math.Round((MilkValue / 1000) * (1),2).ToString();

                            //dr[5] = "";
                            // dtadhead.Rows.Add(dr);
                            //}
                        }
                    }
                    if (objdb.Office_ID() == "4")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "ADDITION";
                        dr[2] = "0";
                        dr[3] = "Incentive for P.W.F.(SAY)(@Rs 1.0 per KgFat)";
                        dr[4] = Math.Round((pwf * 1), 2).ToString();
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        TotalEarning = Math.Round((pwf * 1), 2);
                        //AutoDeduction += Math.Round((pwf * 1), 2);

                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Contrb.for PWF";
                        dr[4] = Math.Round((pwf * 1), 2).ToString();
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= Math.Round((pwf * 1), 2);
                        //AutoDeduction += Math.Round((pwf * 1), 2);
                    }
                    if (objdb.Office_ID() == "3" || objdb.Office_ID() == "4" || objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Computer Charges";
                        dr[4] = 5;
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= 5;
                        AutoDeduction = AutoDeduction + 5;

                    }
                    if (objdb.Office_ID() == "5")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "R.B.P.F";
                        dr[4] = Math.Round(((TMilkQty_InKG) * 2) / 100, 2).ToString();
                        //dr[4] = "0";
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= Math.Round(((TMilkQty_InKG) * 2) / 100, 2);
                        AutoDeduction += Math.Round(((TMilkQty_InKG) * 2) / 100, 2);
                    }
                    if (objdb.Office_ID() == "3")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Development Charges";
                        dr[4] = Math.Round((MilkValue / 1000) * (1), 2).ToString();

                        dr[5] = "";
						dr[6] = "0";
                        DeductionAdditionValue -= Math.Round((MilkValue / 1000) * (1), 2);
                        AutoDeduction += Math.Round((MilkValue / 1000) * (1), 2);
                        dtGetMilkADDetail.Rows.Add(dr);
                    }
                    if (objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Development Charges";
                        dr[4] = Math.Round((TMilkQty_InKG) * (0.01M), 2).ToString();
                        //dr[4] = "0";
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= Math.Round((TMilkQty_InKG) * (0.01M), 2);
                        AutoDeduction += Math.Round((TMilkQty_InKG) * (0.01M), 2);
                    }
                    GrossEarning = decimal.Parse(lblGrossEarning.Text);
                    TFAT_IN_KG = Math.Round(TFAT_IN_KG, 2);
                    GrossEarning = GrossEarning - AutoDeduction - ProducerPayment +SocietyAdjPayment + HeadLoadCharges + ChillingCost ;
                    GrossEarning = Math.Round(GrossEarning, 2);
                    for (int i = 0; i < Count; i++)
                    {


                        decimal Amount = (decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString()));
                        if (ds.Tables[6].Rows[i]["ItemBillingHead_Type"].ToString() == "DEDUCTION")
                        {
                            if (GrossEarning >= 0)
                            {
                                if ((GrossEarning - Amount) >= 0)
                                {
                                    dr = dtGetMilkADDetail.NewRow();
                                    dr[0] = OfficeId;
                                    dr[1] = ds.Tables[6].Rows[i]["ItemBillingHead_Type"].ToString();
                                    dr[2] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr[3] = ds.Tables[6].Rows[i]["ItemBillingHead_Name"].ToString();
                                    dr[4] = Amount;
                                    dr[5] = ds.Tables[6].Rows[i]["Remark"].ToString();
									dr[6] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                    dtGetMilkADDetail.Rows.Add(dr);

                                    dr1 = dtadheadChild.NewRow();
                                    dr1[0] = OfficeId;
                                    dr1[1] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                    dr1[2] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                                    dr1[3] = ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString();
                                    dr1[4] = Amount;
                                    dr1[5] = decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString()) - Amount;
                                    dr1[6] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr1[7] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[8] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[9] = "1";


                                    dtadheadChild.Rows.Add(dr1);


                                    GrossEarning = GrossEarning - Amount;
                                    DeductionAdditionValue -= Amount;
                                }
                                else if (GrossEarning != 0)
                                {
                                    //decimal BalanceAmnt = GrossEarning - decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString());
                                    Amount = Math.Abs(GrossEarning);
                                    dr = dtGetMilkADDetail.NewRow();
                                    dr[0] = OfficeId;
                                    dr[1] = ds.Tables[6].Rows[i]["ItemBillingHead_Type"].ToString();
                                    dr[2] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr[3] = ds.Tables[6].Rows[i]["ItemBillingHead_Name"].ToString();
                                    dr[4] = Amount;
                                    dr[5] = ds.Tables[6].Rows[i]["Remark"].ToString();
									dr[6] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                    dtGetMilkADDetail.Rows.Add(dr);

                                    dr1 = dtadheadChild.NewRow();
                                    dr1[0] = OfficeId;
                                    dr1[1] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"];
                                    dr1[2] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                                    dr1[3] = ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString();
                                    dr1[4] = Amount;
                                    dr1[5] = decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString()) - Amount;
                                    dr1[6] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr1[7] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[8] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[9] = "0";

                                    dtadheadChild.Rows.Add(dr1);
                                    GrossEarning = GrossEarning - Amount;
                                    DeductionAdditionValue -= Amount;
                                }
                                else
                                {
                                    if (ds.Tables[6].Rows[i]["CycleAmount"].ToString() != "0.00")
                                    {

                                    }
                                    dr1 = dtadheadChild.NewRow();
                                    dr1[0] = OfficeId;
                                    dr1[1] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"];
                                    dr1[2] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                                    dr1[3] = ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString();
                                    dr1[4] = "0";
                                    dr1[5] = decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString());
                                    dr1[6] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr1[7] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[8] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[9] = "0";

                                    dtadheadChild.Rows.Add(dr1);


                                }
                            }
                           

                        }
                        else
                        {
                            dr = dtGetMilkADDetail.NewRow();
                            dr[0] = OfficeId;
                            dr[1] = ds.Tables[6].Rows[i]["ItemBillingHead_Type"].ToString();
                            dr[2] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                            dr[3] = ds.Tables[6].Rows[i]["ItemBillingHead_Name"].ToString();
                            dr[4] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                            dr[5] = ds.Tables[6].Rows[i]["Remark"].ToString();
							dr[6] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                            dtGetMilkADDetail.Rows.Add(dr);
                            TotalEarning += Amount;
                            
                            GrossEarning = GrossEarning + Amount;
                            //DeductionAdditionValue += Amount;
                        }

                    }

                    ViewState["dtadheadChild"] = dtadheadChild;
                    //dsGetMilkADChildDetail.Merge((DataTable)ViewState["dtadheadChild"]);
                    //ViewState["dsGetMilkADChildDetail"] = dsGetMilkADChildDetail;





                }
                else
                {

                    //DataTable dt = new DataTable();
                    DataRow dr;
                    //dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                    //dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                    //dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                    //dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                    //dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));

                    //dt.Columns.Add(new DataColumn("Remark", typeof(string)));
                   if(objdb.Office_ID() == "3")
                    {
                        if (ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "1")
                        {
                            dr = dtGetMilkADDetail.NewRow();
                            dr[0] = OfficeId;
                            dr[1] = "DEDUCTION";
                            dr[2] = "0";
                            dr[3] = "N.R.D.";
                            if (objdb.Office_ID() == "4")
                            {
                                dr[4] = Math.Round((TFAT_IN_KG * Convert.ToDecimal(2.25)), 2).ToString();
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((TFAT_IN_KG * Convert.ToDecimal(2.25)), 2).ToString());


                            }
                            else
                            {
                                dr[4] = Math.Round((TFAT_IN_KG * 1), 2).ToString();
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((TFAT_IN_KG * 1), 2).ToString());


                            }

                            dr[5] = "";
                            dr[6] = "0";
                            dtGetMilkADDetail.Rows.Add(dr);

                            if (objdb.Office_ID() == "3")
                            {
                                dr = dtGetMilkADDetail.NewRow();
                                dr[0] = OfficeId;
                                dr[1] = "DEDUCTION";
                                dr[2] = "0";
                                dr[3] = "Audit Fees";
                                dr[4] = Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0).ToString();

                                dr[5] = "";
                                DeductionAdditionValue -= Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0);
                                dtGetMilkADDetail.Rows.Add(dr);
                            }
                            //if (objdb.Office_ID() == "3")
                            //{
                            // dr = dt.NewRow();
                            // dr[0] = dt.Rows.Count + 1;
                            // dr[1] = "DEDUCTION";
                            // dr[2] = "0";
                            // dr[3] = "Development Charges";
                            // dr[4] = Math.Round((MilkValue / 1000) * (1),2).ToString();

                            // dr[5] = "";
                            // dt.Rows.Add(dr);
                            //}


                        }
                    }
                    else
                    {
                        if (ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "1" || ds.Tables[10].Rows[0]["SocietyCategory"].ToString() == "3")
                        {
                            dr = dtGetMilkADDetail.NewRow();
                            dr[0] = OfficeId;
                            dr[1] = "DEDUCTION";
                            dr[2] = "0";
                            dr[3] = "N.R.D.";
                            if (objdb.Office_ID() == "4")
                            {
                                dr[4] = Math.Round((TFAT_IN_KG * Convert.ToDecimal(2.25)), 2).ToString();
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((TFAT_IN_KG * Convert.ToDecimal(2.25)), 2).ToString());


                            }
                            else
                            {
                                dr[4] = Math.Round((TFAT_IN_KG * 1), 2).ToString();
                                DeductionAdditionValue += decimal.Parse("-" + Math.Round((TFAT_IN_KG * 1), 2).ToString());


                            }

                            dr[5] = "";
                            dr[6] = "0";
                            dtGetMilkADDetail.Rows.Add(dr);

                            //if (objdb.Office_ID() == "3")
                            //{
                            //    dr = dtGetMilkADDetail.NewRow();
                            //    dr[0] = dtGetMilkADDetail.Rows.Count + 1;
                            //    dr[1] = "DEDUCTION";
                            //    dr[2] = "0";
                            //    dr[3] = "Audit Fees";
                            //    dr[4] = Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0).ToString();

                            //    dr[5] = "";
                            //    DeductionAdditionValue -= Math.Round(((MilkValue + Convert.ToDecimal(lblCommission.Text) + Convert.ToDecimal(HeadLoadCharges) + Convert.ToDecimal(ChillingCost)) / 1000) * (2), 0);
                            //    dtGetMilkADDetail.Rows.Add(dr);
                            //}
                            //if (objdb.Office_ID() == "3")
                            //{
                            // dr = dt.NewRow();
                            // dr[0] = dt.Rows.Count + 1;
                            // dr[1] = "DEDUCTION";
                            // dr[2] = "0";
                            // dr[3] = "Development Charges";
                            // dr[4] = Math.Round((MilkValue / 1000) * (1),2).ToString();

                            // dr[5] = "";
                            // dt.Rows.Add(dr);
                            //}


                        }
                    }
                    if (objdb.Office_ID() == "4")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "ADDITION";
                        dr[2] = "0";
                        dr[3] = "Incentive for P.W.F.(SAY)(@Rs 1.0 per KgFat)";
                        dr[4] = Math.Round((pwf * 1), 2).ToString();
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        TotalEarning += Math.Round((pwf * 1), 2);

                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Contrb.for PWF";
                        dr[4] = Math.Round((pwf * 1), 2).ToString();
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= Math.Round((pwf * 1), 2);
                        
                    }
                    if (objdb.Office_ID() == "3" || objdb.Office_ID() == "4" || objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Computer Charges";
                        dr[4] = 5;
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= 5;

                    }
                    if (objdb.Office_ID() == "5")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "R.B.P.F";
                        dr[4] = Math.Round(((TMilkQty_InKG) * 2) / 100, 2).ToString();
                        //dr[4] = "0";
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= Math.Round(((TMilkQty_InKG) * 2) / 100, 2);
                    }
                    if (objdb.Office_ID() == "5" || objdb.Office_ID() == "6")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Development Charges";
                        dr[4] = Math.Round((TMilkQty_InKG) * (0.01M), 2).ToString();
                        //dr[4] = "0";
                        dr[5] = "";
						dr[6] = "0";
                        dtGetMilkADDetail.Rows.Add(dr);
                        DeductionAdditionValue -= Math.Round((TMilkQty_InKG) * (0.01M), 2);

                    }
                    if (objdb.Office_ID() == "3")
                    {
                        dr = dtGetMilkADDetail.NewRow();
                        dr[0] = OfficeId;
                        dr[1] = "DEDUCTION";
                        dr[2] = "0";
                        dr[3] = "Development Charges";
                        dr[4] = Math.Round((MilkValue / 1000) * (1), 2).ToString();

                        dr[5] = "";
						dr[6] = "0";
                        DeductionAdditionValue -= Math.Round((MilkValue / 1000) * (1), 2);
                        dtGetMilkADDetail.Rows.Add(dr);
                    }


                }
          
                DeductionAdditionValue = Math.Round(DeductionAdditionValue, 2);
                ViewState["dtGetMilkADDetail"] = dtGetMilkADDetail;
                //dsGetMilkADDetail.Merge((DataTable)ViewState["dtGetMilkADDetail"]);
                //ViewState["dsGetMilkADDetail"] = dsGetMilkADDetail;

                lblGrossEarning.Text = (Math.Round(MilkValue + Convert.ToDecimal(lblCommission.Text), 2) + TotalEarning + HeadLoadCharges + ChillingCost).ToString();
                GrossEarning_T = decimal.Parse(lblGrossEarning.Text);




              
                if (objdb.Office_ID() == "4" || objdb.Office_ID() == "3" || objdb.Office_ID() == "5" || objdb.Office_ID() == "7")
                {
                    NetAmount = Math.Round((GrossEarning_T + (DeductionAdditionValue) - ProducerPayment + SocietyAdjPayment ), 0);
                }
                else
                {
                    NetAmount = Math.Round((GrossEarning_T + (DeductionAdditionValue) - ProducerPayment + SocietyAdjPayment ), 2);
                }
                string Office_Name_E = ds.Tables[10].Rows[0]["Office_Name_E"].ToString();
                string SocietyCode = ds.Tables[10].Rows[0]["SocietyCode"].ToString();
                string AccountNo = ds.Tables[10].Rows[0]["AccountNo"].ToString();
                string IFSC = ds.Tables[10].Rows[0]["IFSC"].ToString();
                string BankName = ds.Tables[10].Rows[0]["BankName"].ToString();
                //dtMainDataDetail.Rows.Add(OfficeId, MilkValue, Commission, GrossEarning_T, DeductionAdditionValue, ProducerPayment, ProducerAdjPayment, SocietyAdjPayment, HeadLoadCharges, ChillingCost, NetAmount, CommisionNarration);
                dtMainDataDetail.Rows.Add(OfficeId, MilkValue, Commission, GrossEarning_T, DeductionAdditionValue, ProducerPayment, ProducerAdjPayment, SocietyAdjPayment, HeadLoadCharges, ChillingCost, NetAmount, CommisionNarration
                                          , Office_Name_E, SocietyCode, AccountNo, IFSC, BankName);
                ViewState["dtMainDataDetail"] = dtMainDataDetail;
                ViewState["GetTable"] = "1";
                //dsMainDataDetail.Merge((DataTable)ViewState["dtMainDataDetail"]);
                //ViewState["dsMainDataDetail"] = dsMainDataDetail;
            }

        }
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewInvoice")
        {
            string SocietyID = e.CommandArgument.ToString();
            FillInvoice_New(SocietyID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowInvoice();", true);
        }
    }
    protected void lNKpRINT_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkPrint = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkPrint");
            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                    new string[] { "flag", "MilkCollectionInvoice_ID" },
                     new string[] { "5", lnkPrint.CommandArgument }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<div class='content' style='border:1px solid black; width:100%'>");
                        sb.Append("<table style='width:100%; margin:5px;'>");

                        sb.Append("<tr>");
                        sb.Append("<td style='text-align:center' colspan='2'>" + Session["Office_Name"].ToString() + "");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>CC: " + ddlccbmcdetail.SelectedItem.Text + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table    style='width:100%; margin-top:20px;'>");
                        sb.Append("<tr style='border-bottom:dashed; border-top:dashed'>");
                        sb.Append("<td>Date</td>");
                        sb.Append("<td>M / E</td>");
                        sb.Append("<td>Buf / Cow</td>");
                        sb.Append("<td>Kg.Wt</td>");
                        sb.Append("<td>Fat</td>");
                        sb.Append("<td>Clr</td>");
                        sb.Append("<td>S.N.F</td>");
                        sb.Append("<td>Kg.Fat</td>");
                        sb.Append("<td>Kg.Snf</td>");
                        sb.Append("<td>Value</td>");

                        sb.Append("<td>Quality</td>");
                        sb.Append("<td>Rate Per Ltr</td>");
                        sb.Append("</tr>");
                        int Count = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < Count; i++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["DispDate"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfPer"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["RatePerLtr"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }
                        decimal FatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatPer"));
                        decimal SnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfPer"));
                        decimal MilkQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                        decimal FatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                        decimal SnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                        decimal MilkValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        decimal RatePerLtr = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RatePerLtr"));
                        sb.Append("<tr style='border-top:dashed; padding-top:20px;'>");
                        sb.Append("<td>Total</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + MilkQuantity.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        //sb.Append("<td>" + SnfPer.ToString() + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + FatInKg.ToString() + "</td>");
                        sb.Append("<td>" + SnfInKg.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        sb.Append("<td>" + MilkValue.ToString() + "</td>");


                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td>" + RatePerLtr.ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Particulars:</td>");
                        sb.Append("<td colspan='8'></td>");
                        sb.Append("<td>Rate:</td>");
                        sb.Append("</tr>");
                        decimal RateBuff = 0;
                        decimal RateCow = 0;
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            RateBuff = decimal.Parse(ds.Tables[6].Rows[0]["BuffRate"].ToString());
                            RateCow = decimal.Parse(ds.Tables[6].Rows[0]["CowRate"].ToString());
                        }
                        int BufCount = ds.Tables[2].Rows.Count;

                        for (int j = 0; j < BufCount; j++)
                        {
                            if (j == 0)
                            {
                                sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkType"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateBuff + "</td>");

                            }
                            else if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateBuff) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateBuff) * 30 / 100) + "</td>");
                            }
                            sb.Append("</tr>");

                        }
                        int CowCount = ds.Tables[3].Rows.Count;

                        for (int j = 0; j < CowCount; j++)
                        {
                            if (j == 0)
                            {
                                sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkType"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateCow + "</td>");

                            }
                            else if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateCow) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateCow) * 30 / 100) + "</td>");
                            }

                            sb.Append("</tr>");



                        }
                        if (objdb.Office_ID() == "4")
                        {
                            sb.Append("<tr>");
                            //sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");

                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }


                        sb.Append("</table>");
                        sb.Append("<table >");
                        int HeadTypeCount = ds.Tables[4].Rows.Count;
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td  >Earn.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        string HeadType = "";

                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "ADDITION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td >Deduction.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");


                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("<table >");
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");


                        sb.Append("<td style='text-align:left'>GROSS EARNING:&nbsp;&nbsp;&nbsp;&nbsp" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        sb.Append("<td >Deductions:&nbsp;&nbsp;&nbsp;&nbsp;" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString())).ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
                        //sb.Append("<td style='width:20%'>Producer Payment:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        ////sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td style='width:40%'>Producer Adj Amount:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        ////sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        //sb.Append("</tr>");

                        //sb.Append("<tr>");

                        //sb.Append("<td style='width:40%'>Society Adjust Amount:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        // sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
                        sb.Append("<td >Chilling Cost:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
                        //  sb.Append("<td>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");



                        sb.Append("<td >Head Load Charges:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

                        sb.Append("<td >Net:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
                        //sb.Append("<td >" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                        sb.Append("</table>");
                        if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4")
                        {
                            if (ds.Tables[7].Rows.Count > 0)
                            {
                                sb.Append("<table>");
                                sb.Append("<tr>");
                                sb.Append("<td>* CURRENT LEDGER POSITION , TODAY *   </td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td>*************************************************</td>");
                                sb.Append("<td>* A M O U N T *</td>");
                                sb.Append("</tr>");
                                int LedgerCount = ds.Tables[7].Rows.Count;
                                for (int i = 0; i < LedgerCount; i++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString() + "</td>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["BalanceAmount"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
								sb.Append("<tr>");
								sb.Append("<td>Total</td>");
								sb.Append("<td>" + ds.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount")).ToString() + "</td>");
								sb.Append("</tr>");

                                sb.Append("</table>");
                            }
                        }
                        sb.Append("</div>");

                        dvReport.InnerHtml = sb.ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
                    }
                    else
                    {

                    }
                }

                else
                {

                }


            }
            else
            {


            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnPrintInvoice_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        dvReport.InnerHtml = "";
        string Status = "0";
        foreach (GridViewRow rows in GridView1.Rows)
        {
            LinkButton MilkCollectionInvoice_ID = (LinkButton)rows.FindControl("lnkPrint");


            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
               new string[] { "flag", "MilkCollectionInvoice_ID" },
                new string[] { "5", MilkCollectionInvoice_ID.CommandArgument.ToString() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<div class='content pagebreak' style='border:1px solid black; width:100%;'>");
                        sb.Append("<table  style='width:100%;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='text-align:center' colspan='2'>" + Session["Office_Name"].ToString() + "");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>CC: " + ddlccbmcdetail.SelectedItem.Text + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table style='width:100%;'>");
                        sb.Append("<tr style='border-bottom:dashed; border-top:dashed'>");
                        sb.Append("<td>Date</td>");
                        sb.Append("<td>M / E</td>");
                        sb.Append("<td>Buf / Cow</td>");
                        sb.Append("<td>Kg.Wt</td>");
                        sb.Append("<td>Fat</td>");
                        sb.Append("<td>Clr</td>");
                        sb.Append("<td>S.N.F</td>");
                        sb.Append("<td>Kg.Fat</td>");
                        sb.Append("<td>Kg.Snf</td>");
                        sb.Append("<td>Value</td>");

                        sb.Append("<td>Quality</td>");
                        sb.Append("<td>Rate Per Ltr</td>");
                        sb.Append("</tr>");
                        int Count = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < Count; i++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["DispDate"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfPer"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["RatePerLtr"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }
                        decimal FatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatPer"));
                        decimal SnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfPer"));
                        decimal MilkQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                        decimal FatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                        decimal SnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                        decimal MilkValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        decimal RatePerLtr = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RatePerLtr"));
                        sb.Append("<tr style='border-top:dashed; padding-top:20px;'>");
                        sb.Append("<td>Total</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + MilkQuantity.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        //sb.Append("<td>" + SnfPer.ToString() + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + FatInKg.ToString() + "</td>");
                        sb.Append("<td>" + SnfInKg.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        sb.Append("<td>" + MilkValue.ToString() + "</td>");


                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td>" + RatePerLtr.ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Particulars:</td>");
                        sb.Append("<td colspan='8'></td>");
                        sb.Append("<td>Rate:</td>");
                        sb.Append("</tr>");
                        decimal RateBuff = 0;
                        decimal RateCow = 0;
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            RateBuff = decimal.Parse(ds.Tables[6].Rows[0]["BuffRate"].ToString());
                            RateCow = decimal.Parse(ds.Tables[6].Rows[0]["CowRate"].ToString());
                        }
                        int BufCount = ds.Tables[2].Rows.Count;

                        for (int j = 0; j < BufCount; j++)
                        {
                            if (j == 0)
                            {
                                sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkType"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateBuff + "</td>");

                            }
                            else if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateBuff) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateBuff) * 30 / 100) + "</td>");
                            }
                            sb.Append("</tr>");

                        }
                        int CowCount = ds.Tables[3].Rows.Count;

                        for (int j = 0; j < CowCount; j++)
                        {
                            if (j == 0)
                            {
                                sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkType"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateCow + "</td>");

                            }
                            else if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateCow) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateCow) * 30 / 100) + "</td>");
                            }

                            sb.Append("</tr>");



                        }
                        if (objdb.Office_ID() == "4")
                        {
                            sb.Append("<tr>");
                            //sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");

                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }


                        sb.Append("</table>");
                        sb.Append("<table>");
                        int HeadTypeCount = ds.Tables[4].Rows.Count;
                        sb.Append("<tr style='border-top:dashed;'>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td  >Earn.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        string HeadType = "";

                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "ADDITION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td >Deduction.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");


                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("<table>");
                        sb.Append("<tr style='border-top:dashed;'>");


                        sb.Append("<td style='text-align:left' >GROSS EARNING:&nbsp;&nbsp;&nbsp;&nbsp" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        sb.Append("<td >Deductions:&nbsp;&nbsp;&nbsp;&nbsp;" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString())).ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
                        //sb.Append("<td style='width:20%'>Producer Payment:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        ////sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td style='width:40%'>Producer Adj Amount:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        ////sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        //sb.Append("</tr>");

                        //sb.Append("<tr>");

                        //sb.Append("<td style='width:40%'>Society Adjust Amount:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        // sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
                        sb.Append("<td >Chilling Cost:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
                        //  sb.Append("<td>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");



                        sb.Append("<td >Head Load Charges:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

                        sb.Append("<td >Net:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
                        //sb.Append("<td >" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                        sb.Append("</table>");
                        if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4")
                        {
                            if (ds.Tables[7].Rows.Count > 0)
                            {
                                sb.Append("<table>");
                                sb.Append("<tr>");
                                sb.Append("<td>* CURRENT LEDGER POSITION , TODAY *   </td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td>*************************************************</td>");
                                sb.Append("<td>* A M O U N T *</td>");
                                sb.Append("</tr>");
                                int LedgerCount = ds.Tables[7].Rows.Count;
                                for (int i = 0; i < LedgerCount; i++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString() + "</td>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["BalanceAmount"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
								sb.Append("<tr>");
								sb.Append("<td>Total</td>");
								sb.Append("<td>" + ds.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount")).ToString() + "</td>");
								sb.Append("</tr>");

                                sb.Append("</table>");
                            }
                        }
                        sb.Append("</div>");
                        dvReport.InnerHtml += sb.ToString();

                    }
                    else
                    {

                    }
                }

                else
                {

                }


            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
        }




    }
    protected void btnReprocess_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MilkCollectionInvoice_ID", typeof(int));
            foreach (GridViewRow rows in GridView1.Rows)
            {

                LinkButton lnkPrint = (LinkButton)rows.FindControl("lnkPrint");
                dt.Rows.Add(lnkPrint.CommandArgument.ToString());
               
                
            }
            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                                   new string[] { "flag" },
                                   new string[] { "20" },
                                   new string[] { "type_UpdateSocietyInvocieFinalSubmitStatus" },
                                   new DataTable[] { dt }, "TableSave");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }

                }
            }
            //CreateDataSet();
            //GetData();
            btnSubmit_Click(sender, e);
            btnGenerateInvoice_Click(sender, e);
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GetMilkCollectionInvoice_ID()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MilkCollectionInvoice_ID", typeof(int));
        foreach (GridViewRow rows in GridView1.Rows)
        {

            LinkButton lnkPrint = (LinkButton)rows.FindControl("lnkPrint");
            dt.Rows.Add(lnkPrint.CommandArgument.ToString());
            

        }
        return dt;
    }
    protected void btnFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetMilkCollectionInvoice_ID();
            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice", 
                                    new string[] {"flag" }, 
                                    new string[] {"21" },
                                    new string[] { "type_UpdateSocietyInvocieFinalSubmitStatus" },
                                    new DataTable[] {dt }, "TableSave");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    btnSubmit_Click(sender, e);
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
	 protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtDate.Text != "")
            {
                string BillingCycle = ddlBillingCycle.SelectedItem.Text;
                string[] DatePart = txtDate.Text.Split('/');
                int Day = int.Parse(DatePart[0]);
                int Month = int.Parse(DatePart[1]);
                int Year = int.Parse(DatePart[2]);
                string SelectedFromDate = "";
                string SelectedToDate = "";
                if (BillingCycle == "5 days")
                {
                    if (Day >= 1 && Day < 6)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "05";
                    }
                    else if (Day > 5 && Day < 11)
                    {
                        SelectedFromDate = "6";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 16)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "15";
                    }
                    else if (Day > 15 && Day < 21)
                    {
                        SelectedFromDate = "16";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day < 26)
                    {
                        SelectedFromDate = "21";
                        SelectedToDate = "25";
                    }
                    else if (Day > 25 && Day <= 31)
                    {
                        SelectedFromDate = "26";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }

                    }
                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

                }
                else
                {
                    if (Day >= 1 && Day < 11)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 21)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day <= 31)
                    {
                        SelectedFromDate = "21";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }
                    }

                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
                }
                txtFdt.Text = Convert.ToDateTime(SelectedFromDate, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(SelectedToDate, cult).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlBillingCycle_TextChanged(object sender, EventArgs e)
    {
        txtDate_TextChanged(sender, e);
    }
}