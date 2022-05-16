using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;


public partial class mis_MilkCollection_RoutewiseMilkCollectionInvoice_New1 : System.Web.UI.Page
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
                CreateDataSet();
                GetCCDetails();
				if (objdb.Office_ID() == "2")
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
            CreateDataSet();
            GetData();
            DataSet dsMainDataDetail = (DataSet)ViewState["dsMainDataDetail"];
            string MilkValue;
            string Commission;
            decimal TotalNetAmount = 0;
			decimal TotalMilkQty = 0;
            decimal TotalMilkValue = 0;
            decimal TotalCommission = 0;
            decimal TotalGrossEarning = 0;
            decimal TotalDeductionAdditionValue = 0;
            decimal TotalProducerPayment = 0;
            decimal TotalProducerAdjPayment = 0;
            decimal TotalSocietyAdjPayment = 0;
            decimal TotalHeadLoadCharges = 0;
            decimal TotalChillingCost = 0;
            string GrossEarning;
            string DeductionAdditionValue;
            string ProducerPayment;
            string ProducerAdjPayment;
            string SocietyAdjPayment;
            string HeadLoadCharges;
            string ChillingCost;
            string I_OfficeID;
            string Office_Name;
			string Office_Code;
            string MilkCollectionInvoice_ID;
            string NetAmount;
			 string Quantity;
            DataSet dsFillGrid = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
                       new string[] { "flag", "Office_ID", "FDT", "TDT" },
                       new string[] { "6", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("MilkCollectionInvoice_ID", typeof(string));
     
            dtGrid.Columns.Add("I_OfficeID", typeof(string));
            dtGrid.Columns.Add("Office_Name", typeof(string));
			 dtGrid.Columns.Add("Office_Code", typeof(string));
			dtGrid.Columns.Add("Quantity", typeof(string));
            dtGrid.Columns.Add("MilkValue", typeof(string));
            dtGrid.Columns.Add("Commission", typeof(string));
            dtGrid.Columns.Add("GrossEarning", typeof(string));
            dtGrid.Columns.Add("DeductionAdditionValue", typeof(string));
            dtGrid.Columns.Add("ProducerPayment", typeof(string));
            dtGrid.Columns.Add("ProducerAdjPayment", typeof(string));
            dtGrid.Columns.Add("SocietyAdjPayment", typeof(string));
            dtGrid.Columns.Add("HeadLoadCharges", typeof(string));
            dtGrid.Columns.Add("ChillingCost", typeof(string));
            dtGrid.Columns.Add("NetAmount", typeof(string));

            if (dsFillGrid != null && dsFillGrid.Tables.Count > 0)
            {
                if (dsFillGrid.Tables[0].Rows.Count > 0)
                {
                   
                    
                    
                    int Count = dsFillGrid.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        if (dsFillGrid.Tables[0].Rows[i]["MilkCollectionInvoice_ID"].ToString() == "")
                        {
                            btnGenerateInvoice.Visible = true;
                            btnReprocess.Visible = false;
                            btnFinalSubmit.Visible = false;
                            string SocietyID = dsFillGrid.Tables[0].Rows[i]["I_OfficeID"].ToString();
                            MilkCollectionInvoice_ID = "0";
                            MilkValue = dsMainDataDetail.Tables[SocietyID].Rows[0]["MilkValue"].ToString();
                            Commission = dsMainDataDetail.Tables[SocietyID].Rows[0]["Commission"].ToString();
                            GrossEarning = dsMainDataDetail.Tables[SocietyID].Rows[0]["GrossEarning"].ToString();
                            DeductionAdditionValue = Math.Abs(decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["DeductionAdditionValue"].ToString())).ToString();
                            ProducerPayment = dsMainDataDetail.Tables[SocietyID].Rows[0]["ProducerPayment"].ToString();
                            ProducerAdjPayment = dsMainDataDetail.Tables[SocietyID].Rows[0]["ProducerAdjPayment"].ToString();
                            SocietyAdjPayment = dsMainDataDetail.Tables[SocietyID].Rows[0]["SocietyAdjPayment"].ToString();
                            HeadLoadCharges = dsMainDataDetail.Tables[SocietyID].Rows[0]["HeadLoadCharges"].ToString();
                            ChillingCost = dsMainDataDetail.Tables[SocietyID].Rows[0]["ChillingCost"].ToString();
                            I_OfficeID = dsFillGrid.Tables[0].Rows[i]["I_OfficeID"].ToString();
                            Office_Name = dsFillGrid.Tables[0].Rows[i]["Office_Name"].ToString();
							Office_Code = dsFillGrid.Tables[0].Rows[i]["Office_Code"].ToString();
                            NetAmount = dsMainDataDetail.Tables[SocietyID].Rows[0]["NetAmount"].ToString();
							 Quantity = dsFillGrid.Tables[0].Rows[i]["Quantity"].ToString();
                             dtGrid.Rows.Add(MilkCollectionInvoice_ID,I_OfficeID, Office_Name, Office_Code, Quantity, MilkValue, Commission, GrossEarning, DeductionAdditionValue, ProducerPayment, ProducerAdjPayment, SocietyAdjPayment, HeadLoadCharges, ChillingCost, NetAmount);

                        }
                        else if (dsFillGrid.Tables[0].Rows[i]["FinalSubmitStatus"].ToString() == "0")
                        {
							btnPrintInvoice.Visible = true;
                            btnGenerateInvoice.Visible = false;
                            btnReprocess.Visible = true;
                            btnFinalSubmit.Visible = true;
                            MilkCollectionInvoice_ID = dsFillGrid.Tables[0].Rows[i]["MilkCollectionInvoice_ID"].ToString();
                            MilkValue = dsFillGrid.Tables[0].Rows[i]["MilkValue"].ToString();
                            Commission = dsFillGrid.Tables[0].Rows[i]["Commission"].ToString();
                            GrossEarning = dsFillGrid.Tables[0].Rows[i]["GrossEarning"].ToString();
                            DeductionAdditionValue = Math.Abs(decimal.Parse(dsFillGrid.Tables[0].Rows[i]["DeductionAdditionValue"].ToString())).ToString();
                            ProducerPayment = dsFillGrid.Tables[0].Rows[i]["ProducerPayment"].ToString();
                            ProducerAdjPayment = dsFillGrid.Tables[0].Rows[i]["ProducerAdjPayment"].ToString();
                            SocietyAdjPayment = dsFillGrid.Tables[0].Rows[i]["SocietyAdjPayment"].ToString();
                            HeadLoadCharges = dsFillGrid.Tables[0].Rows[i]["HeadLoadCharges"].ToString();
                            ChillingCost = dsFillGrid.Tables[0].Rows[i]["ChillingCost"].ToString();
                            NetAmount = dsFillGrid.Tables[0].Rows[i]["NetAmount"].ToString();
                            I_OfficeID = dsFillGrid.Tables[0].Rows[i]["I_OfficeID"].ToString();
                            Office_Name = dsFillGrid.Tables[0].Rows[i]["Office_Name"].ToString();
							Office_Code = dsFillGrid.Tables[0].Rows[i]["Office_Code"].ToString();
							Quantity = dsFillGrid.Tables[0].Rows[i]["Quantity"].ToString();
                            dtGrid.Rows.Add(MilkCollectionInvoice_ID,I_OfficeID, Office_Name,Office_Code,Quantity, MilkValue, Commission, GrossEarning, DeductionAdditionValue, ProducerPayment, ProducerAdjPayment, SocietyAdjPayment, HeadLoadCharges, ChillingCost, NetAmount);
                        }
                        else
                        {
                            btnPrintInvoice.Visible = true;
                            btnGenerateInvoice.Visible = false;
                            btnReprocess.Visible = false;
                            btnFinalSubmit.Visible = false;
                            MilkCollectionInvoice_ID = dsFillGrid.Tables[0].Rows[i]["MilkCollectionInvoice_ID"].ToString();
                            MilkValue = dsFillGrid.Tables[0].Rows[i]["MilkValue"].ToString();
                            Commission = dsFillGrid.Tables[0].Rows[i]["Commission"].ToString();
                            GrossEarning = dsFillGrid.Tables[0].Rows[i]["GrossEarning"].ToString();
                            DeductionAdditionValue = Math.Abs(decimal.Parse(dsFillGrid.Tables[0].Rows[i]["DeductionAdditionValue"].ToString())).ToString();
                            ProducerPayment = dsFillGrid.Tables[0].Rows[i]["ProducerPayment"].ToString();
                            ProducerAdjPayment = dsFillGrid.Tables[0].Rows[i]["ProducerAdjPayment"].ToString();
                            SocietyAdjPayment = dsFillGrid.Tables[0].Rows[i]["SocietyAdjPayment"].ToString();
                            HeadLoadCharges = dsFillGrid.Tables[0].Rows[i]["HeadLoadCharges"].ToString();
                            ChillingCost = dsFillGrid.Tables[0].Rows[i]["ChillingCost"].ToString();
                            NetAmount = dsFillGrid.Tables[0].Rows[i]["NetAmount"].ToString();
                            I_OfficeID = dsFillGrid.Tables[0].Rows[i]["I_OfficeID"].ToString();
                            Office_Name = dsFillGrid.Tables[0].Rows[i]["Office_Name"].ToString();
							Office_Code = dsFillGrid.Tables[0].Rows[i]["Office_Code"].ToString();
                            Quantity = dsFillGrid.Tables[0].Rows[i]["Quantity"].ToString();
                            dtGrid.Rows.Add(MilkCollectionInvoice_ID, I_OfficeID, Office_Name,Office_Code, Quantity, MilkValue, Commission, GrossEarning, DeductionAdditionValue, ProducerPayment, ProducerAdjPayment, SocietyAdjPayment, HeadLoadCharges, ChillingCost, NetAmount);
                        }
                        
                    }
                    GridView1.DataSource = dtGrid;
                    GridView1.DataBind();
                    foreach (GridViewRow rows in GridView1.Rows)
                    {
                        Label lblNetAmount = (Label)rows.FindControl("lblNetAmount");
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
                       
                        TotalNetAmount += decimal.Parse(lblNetAmount.Text);
                        TotalMilkValue += decimal.Parse(lblMilkValue.Text); 
                        TotalMilkQty += decimal.Parse(lblQuantity.Text);
                        TotalCommission += decimal.Parse(lblCommission.Text);
                        TotalGrossEarning += decimal.Parse(lblGrossEarning.Text);
                        TotalDeductionAdditionValue += decimal.Parse(lblDeductionAdditionValue.Text);
                        //TotalProducerPayment += decimal.Parse(lblProducerPayment.Text);
                        //TotalProducerAdjPayment += decimal.Parse(lblProducerAdjPayment.Text);
                        //TotalSocietyAdjPayment += decimal.Parse(lblSocietyAdjPayment.Text);
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


    private DataTable GetMilkDispatchDetail(string SocietyID)
    {
        DataSet dsGetMilkDispatchDetail = (DataSet)ViewState["dsGetMilkDispatchDetail"];
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


        //foreach (GridViewRow rowseal in gv_SocietyMilkDispatchDetail.Rows)
        //{
        //    Label lbllblDt_Date_F = (Label)rowseal.FindControl("lbllblDt_Date_F");
        //    Label lblV_Shift = (Label)rowseal.FindControl("lblV_Shift");
        //    Label lblV_MilkType = (Label)rowseal.FindControl("lblV_MilkType");
        //    Label lblFat = (Label)rowseal.FindControl("lblFat");
        //    Label lblSNF = (Label)rowseal.FindControl("lblSNF");
        //    Label lblI_MilkSupplyQty = (Label)rowseal.FindControl("lblI_MilkSupplyQty");
        //    Label lblFAT_IN_KG = (Label)rowseal.FindControl("lblFAT_IN_KG");
        //    Label lblSNF_IN_KG = (Label)rowseal.FindControl("lblSNF_IN_KG");
        //    Label lblValue = (Label)rowseal.FindControl("lblValue");
        //    Label lblCLR = (Label)rowseal.FindControl("lblCLR");
        //    Label lblQuality = (Label)rowseal.FindControl("lblQuality");
        //    Label lblRate_Per_Ltr = (Label)rowseal.FindControl("lblRate_Per_Ltr");

        //    drMC = dtMC.NewRow();
        //    drMC[0] = lbllblDt_Date_F.Text;
        //    drMC[1] = lblV_Shift.Text;
        //    drMC[2] = lblV_MilkType.Text;
        //    drMC[3] = lblFat.Text;
        //    drMC[4] = lblSNF.Text;
        //    drMC[5] = lblI_MilkSupplyQty.Text;
        //    drMC[6] = lblFAT_IN_KG.Text;
        //    drMC[7] = lblSNF_IN_KG.Text;
        //    drMC[8] = lblValue.Text;
        //    drMC[9] = lblCLR.Text;
        //    drMC[10] = lblQuality.Text;
        //    drMC[11] = lblRate_Per_Ltr.Text;

        //    dtMC.Rows.Add(drMC);
        //}
        return dtMC;
    }

    private DataTable GetMilkBuffDetail(string SocietyID)
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

    private DataTable GetMilkCowhDetail(string SocietyID)
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

    private DataTable GetMilkADDetail(string SocietyID)
    {

        DataTable dtMC = new DataTable();
        DataRow drMC;
        dtMC.Columns.Add(new DataColumn("Head_ID", typeof(int)));
        dtMC.Columns.Add(new DataColumn("HeadType", typeof(string)));
        dtMC.Columns.Add(new DataColumn("HeadName", typeof(string)));
        dtMC.Columns.Add(new DataColumn("HeadAmount", typeof(decimal)));
        dtMC.Columns.Add(new DataColumn("HeadRemark", typeof(string)));
        dtMC.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(string)));
        foreach (GridViewRow rowseal in grhradsdetails.Rows)
        {
            Label lblItemBillingHead_Type = (Label)rowseal.FindControl("lblItemBillingHead_Type");
            Label lblItemBillingHead_ID = (Label)rowseal.FindControl("lblItemBillingHead_ID");
            Label lblItemBillingHead_Name = (Label)rowseal.FindControl("lblItemBillingHead_Name");
			Label lblAddtionsDeducEntry_ID = (Label)rowseal.FindControl("lblAddtionsDeducEntry_ID");
            Label lblItemBillingHead_Remark = (Label)rowseal.FindControl("lblItemBillingHead_Remark");
            Label lblTotalPrice = (Label)rowseal.FindControl("lblTotalPrice");

            drMC = dtMC.NewRow();
            drMC[0] = lblItemBillingHead_Type.Text;
            drMC[1] = lblItemBillingHead_ID.Text;
            drMC[2] = lblItemBillingHead_Name.Text;
            drMC[3] = lblTotalPrice.Text;
            drMC[4] = lblItemBillingHead_Remark.Text;
			drMC[5] = lblAddtionsDeducEntry_ID.Text;
            dtMC.Rows.Add(drMC);

        }
        return dtMC;
    }

    protected void btnGenerateInvoice_Click(object sender, EventArgs e)
    {
        try
        {


            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblMsg.Text = "";
                int Status = 0;
                decimal ChillingCost = 0;
                decimal HeadLoadCharges = 0;
                decimal ProducerAdjPayment = 0;
                decimal SocietyAdjPayment = 0;
                decimal ProducerPayment = 0;
                decimal NetAmount = 0;
                decimal MilkValue = 0;
                decimal Commission = 0;
                decimal GrossEarning = 0;
                decimal DeductionAdditionValue = 0;
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                //    if (chk.Checked == true)
                //    {
                //        Status = 1;
                //        break;
                //    }

                //}
                DataSet dsGetMilkDispatchDetail = (DataSet)ViewState["dsGetMilkDispatchDetail"];
                DataSet dsGetMilkCowhDetail = (DataSet)ViewState["dsGetMilkCowhDetail"];
                DataSet dsGetMilkBuffDetail = (DataSet)ViewState["dsGetMilkBuffDetail"];
                DataSet dsGetMilkADDetail = (DataSet)ViewState["dsGetMilkADDetail"];
                DataSet dsMainDataDetail = (DataSet)ViewState["dsMainDataDetail"];
                DataSet dsGetMilkADChildDetail = (DataSet)ViewState["dsGetMilkADChildDetail"];
                //if (Status == 1)
                //{
                    foreach (GridViewRow row in GridView1.Rows)
                    {
						
                        //CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                        Label lblI_OfficeID = (Label)row.FindControl("lblI_OfficeID");
						Label lblStatus = (Label)row.FindControl("lblStatus");
                        if (lblStatus.Text == "Not generated")
                        {
                            string SocietyID = lblI_OfficeID.Text;
                            DataTable dtDispatch = new DataTable();
                            dtDispatch = dsGetMilkDispatchDetail.Tables[SocietyID];

                            DataTable dt2Buf = new DataTable();
                            dt2Buf = dsGetMilkBuffDetail.Tables[SocietyID];

                            DataTable dt3Cow = new DataTable();
                            dt3Cow = dsGetMilkCowhDetail.Tables[SocietyID];

                            DataTable dt4AD = new DataTable();
                            dt4AD = dsGetMilkADDetail.Tables[SocietyID];


                            DataTable dtMC = new DataTable();

                            dtMC.Columns.Add(new DataColumn("Head_ID", typeof(string)));
                            dtMC.Columns.Add(new DataColumn("HeadType", typeof(string)));
                            dtMC.Columns.Add(new DataColumn("HeadName", typeof(string)));
                            dtMC.Columns.Add(new DataColumn("HeadAmount", typeof(decimal)));
                            dtMC.Columns.Add(new DataColumn("HeadRemark", typeof(string)));
                            dtMC.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(string)));
                            foreach (DataRow drow in dt4AD.Rows)
                            {
                                string Head_ID = drow["ItemBillingHead_ID"].ToString();
                                string HeadType = drow["ItemBillingHead_Type"].ToString();
                                string HeadName = drow["ItemBillingHead_Name"].ToString();
                                string HeadAmount = drow["TotalAmount"].ToString();
                                string HeadRemark = drow["Remark"].ToString();
                                string AddtionsDeducEntry_ID = drow["AddtionsDeducEntry_ID"].ToString();
                                dtMC.Rows.Add(Head_ID, HeadType, HeadName, HeadAmount, HeadRemark, AddtionsDeducEntry_ID);
                            }

                            DataTable dt4ADChild = new DataTable();
                            dt4ADChild = dsGetMilkADChildDetail.Tables[SocietyID];
                            DataTable dtADID = new DataTable();
                            DataRow drADID;
                            dtADID.Columns.Add("AddtionsDeducEntry_ID", typeof(int));
                            dtADID.Columns.Add("ItemBillingHead_ID", typeof(int));

                            if (dt4ADChild != null)
                            {
                                int Count = dt4ADChild.Rows.Count;
                                for (int i = 0; i < Count; i++)
                                {
                                    DataRow dr = dt4ADChild.Rows[i];
                                    drADID = dtADID.NewRow();
                                    drADID[0] = dr["AddtionsDeducEntry_ID"];
                                    drADID[1] = dr["ItemBillingHead_ID"];

                                    //drMC[3] ="NA";
                                    dtADID.Rows.Add(drADID);
                                }
                            }
                            ProducerPayment = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["ProducerPayment"].ToString());
                            ProducerAdjPayment = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["ProducerAdjPayment"].ToString());
                            SocietyAdjPayment = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["SocietyAdjPayment"].ToString());
                            HeadLoadCharges = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["HeadLoadCharges"].ToString());
                            ChillingCost = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["ChillingCost"].ToString());


                            MilkValue = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["MilkValue"].ToString());
                            Commission = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["Commission"].ToString());
                            GrossEarning = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["GrossEarning"].ToString());
                            DeductionAdditionValue = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["DeductionAdditionValue"].ToString());
                            NetAmount = decimal.Parse(dsMainDataDetail.Tables[SocietyID].Rows[0]["NetAmount"].ToString());
                            // Runtime Validation

                            string FDate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                            string TDate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");

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
                         "V_EntryFrom" ,
						 "FinalSubmitStatus",
						 "CC_Id",
						 "OfficeType_ID"
                          },

                                                    new string[] { "2",  
                         objdb.Office_ID(),
                         SocietyID,
                         FDate,
                         TDate,
                         "0",
                         MilkValue.ToString(),
                         Commission.ToString(),
                         GrossEarning.ToString(),
                         DeductionAdditionValue.ToString(),
                         ProducerPayment.ToString(),
                         ProducerAdjPayment.ToString(),
                         SocietyAdjPayment.ToString(),
                         HeadLoadCharges.ToString(),
                         ChillingCost.ToString(),
                         NetAmount.ToString(),
                         ViewState["Emp_ID"].ToString(),
                         objdb.GetLocalIPAddress(),
                         objdb.GetMACAddress(),
                         "Web",
						 "0",
						 ddlccbmcdetail.SelectedValue,
                         objdb.OfficeType_ID()

                         },
                                                     new string[] {
                                                         "type_SocietywiseMilkCollectionInvoiceChild1Dispatch",
                              "type_SocietywiseMilkCollectionInvoiceChild2Buf",
                              "type_SocietywiseMilkCollectionInvoiceChild3Cow", 
                              "type_SocietywiseMilkCollectionInvoiceChild4AD",
                              "type_MilkCollectionAdditionDeductionEntry_Child",
                              "Update_type_MilkCollectionAdditionDeductionEntry_Child"                      },
                                                     new DataTable[] {
                                                         dtDispatch, 
                                                         dt2Buf, dt3Cow, dtMC
                                                         , dt4ADChild,dtADID }, "TableSave");
                        }
                        //if (chk.Checked == true)
                        //{
                            



                        //}

                    
					}
                    Session["IsSuccess"] = true;
                    Response.Redirect("RoutewiseMilkCollectionInvoice_New.aspx", false);
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Checked at Least one checkbox.')", true);
                //}

            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }



    //protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    ds = null;
    //    if (ddlBMCTankerRootName.SelectedIndex > 0)
    //    {

    //        //ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
    //        //           new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
    //        //           new string[] { "5", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

    //        //if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        //{

    //        //    ddlSociety.DataSource = ds;
    //        //    ddlSociety.DataValueField = "I_OfficeID";
    //        //    ddlSociety.DataTextField = "Office_Name";
    //        //    ddlSociety.DataBind();


    //        //}
    //        //else
    //        //{
    //        //    ddlSociety.DataSource = null;
    //        //    ddlSociety.DataBind();

    //        //}

    //    }
    //    else
    //    {
    //        //ddlSociety.DataSource = null;
    //        //ddlSociety.DataBind();

    //    }
    //}
    protected void FillInvoice(string SocietyID)
    {
        try
        {

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
                       new string[] { "7", Fdate, Tdate, SocietyID }, "dataset");


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
                      new string[] { "9", Fdate, Tdate, SocietyID }, "dataset");


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
                         new string[] { "21", Fdate, Tdate, SocietyID, objdb.Office_ID() }, "dataset");



                if (ds != null)
                {
                    decimal CowRate = 0;
                    decimal Rate = 0;
                    decimal BuffRate = 0;
                    decimal CowSnfKg = 0;
                    decimal CowFatKg = 0;
                    decimal BufFatKg = 0;
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

                            lblSociety.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
                            lblbankInfo.Text = ds.Tables[0].Rows[0]["IFSC"].ToString() + " / " + ds.Tables[0].Rows[0]["BankName"].ToString() + " / " + ds.Tables[0].Rows[0]["BankAccountNo"].ToString();
                            lblOfficename.Text = ds.Tables[0].Rows[0]["AttachUnitName"].ToString() + " / " + ds.Tables[0].Rows[0]["AttachUnitCode"].ToString();
                            lblBillingPeriod.Text = ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString();

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

                                                if (lblFAT_IN_KG.Text != "")
                                                {
                                                    BufFatKg = Convert.ToDecimal(lblFAT_IN_KG.Text);

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
                                    if (ds.Tables[7].Rows.Count != 0)
                                    {
                                        if (ds.Tables[7].Rows.Count > 0)
                                        {

                                        }
                                    }
                                    if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4")
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
                                            lblCommission.Text = (FAT_IN_KG * Rate).ToString("0.000");

                                        }
                                        else
                                        {
                                            lblCommission.Text = (((CowFatKg + CowSnfKg) * CowRate) + (BufFatKg * BuffRate)).ToString("0.000");
                                        }

                                    }
                                    else
                                    {
                                        lblCommission.Text = (FAT_IN_KG * Rate).ToString("0.000");
                                    }
                                    // lblCommission.Text = (FAT_IN_KG * 7).ToString();
                                    // lblCommission.Text = (FAT_IN_KG * Rate).ToString("0.000");
                                    lblMilkValue.Text = MilkValue.ToString("0.00");
                                    lblGrossEarning.Text = (MilkValue + Convert.ToDecimal(lblCommission.Text)).ToString();

                                    if (ds.Tables[7].Rows.Count > 0)
                                    {
                                        int Count = ds.Tables[7].Rows.Count;
                                        DataTable dtadhead = new DataTable();
                                        dtadhead.Columns.Add(new DataColumn("S.No", typeof(int)));
                                        dtadhead.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                                        dtadhead.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                                        dtadhead.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                                        dtadhead.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));

                                        DataRow dr;
                                        dr = dtadhead.NewRow();
                                        dr[0] = dtadhead.Rows.Count + 1;
                                        dr[1] = "DEDUCTION";
                                        dr[2] = "0";
                                        dr[3] = "N.R.D.";
                                        dr[4] = (FAT_IN_KG * 1).ToString();

                                        dtadhead.Rows.Add(dr);

                                        for (int i = 0; i < Count; i++)
                                        {

                                            dr = dtadhead.NewRow();
                                            dr[0] = (i + 1) + 1;
                                            dr[1] = ds.Tables[7].Rows[i]["ItemBillingHead_Type"].ToString();
                                            dr[2] = ds.Tables[7].Rows[i]["ItemBillingHead_ID"].ToString();
                                            dr[3] = ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString();
                                            dr[4] = ds.Tables[7].Rows[i]["TotalAmount"].ToString();

                                            dtadhead.Rows.Add(dr);
                                        }


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

                                        decimal PPD = 0;

                                        if (lblProducerPayment.Text != "")
                                        {
                                            PPD = Convert.ToDecimal(lblProducerPayment.Text);
                                        }
                                        else
                                        {
                                            PPD = 0;
                                        }
                                        decimal ProdAdjustAmount = 0;
                                        decimal SocAdjustAmount = 0;
                                        if (lblProcAdjustAmount.Text != "")
                                        {
                                            ProdAdjustAmount = Convert.ToDecimal(lblProcAdjustAmount.Text);
                                        }
                                        else
                                        {
                                            ProdAdjustAmount = 0;
                                        }
                                        if (lblSocAdjustAmount.Text != "")
                                        {
                                            SocAdjustAmount = Convert.ToDecimal(lblSocAdjustAmount.Text);
                                        }
                                        else
                                        {
                                            SocAdjustAmount = 0;
                                        }
                                        lblnetamount.Text = (Convert.ToDecimal(lblGrossEarning.Text) + (Prodvalue) - PPD + SocAdjustAmount).ToString();


                                    }
                                    else
                                    {

                                        DataTable dt = new DataTable();
                                        DataRow dr;
                                        dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                                        dt.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                                        dt.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));



                                        dr = dt.NewRow();
                                        dr[0] = 1;
                                        dr[1] = "DEDUCTION";
                                        dr[2] = "0";
                                        dr[3] = "N.R.D.";
                                        dr[4] = (FAT_IN_KG * 1).ToString();

                                        dt.Rows.Add(dr);

                                        grhradsdetails.DataSource = dt;
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

                                        decimal PPD = 0;

                                        if (lblProducerPayment.Text != "")
                                        {
                                            PPD = Convert.ToDecimal(lblProducerPayment.Text);
                                        }
                                        else
                                        {
                                            PPD = 0;
                                        }
                                        decimal ProdAdjustAmount = 0;
                                        decimal SocAdjustAmount = 0;
                                        if (lblProcAdjustAmount.Text != "")
                                        {
                                            ProdAdjustAmount = Convert.ToDecimal(lblProcAdjustAmount.Text);
                                        }
                                        else
                                        {
                                            ProdAdjustAmount = 0;
                                        }
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
                                        decimal ChillingCost = 0;
                                        int Mtrl_dy = int.Parse(Mtrl_datevalue.Day.ToString());
                                        if (Mtrl_dy >= 21)
                                        {
                                            DataSet dschillingcost = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "FDT" }, new string[] { "22", SocietyID, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
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
                                        decimal HeadLoadCharges = 0;
                                        //if (ddlMilkCollectionUnit.SelectedValue == "6")
                                        //{

                                        //    if (Mtrl_dy >= 21)
                                        //    {
                                        //        lblHeadLoadCharges.Visible = true;
                                        //        DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { "23", ddlSociety.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                        //        if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                                        //        {
                                        //            if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                                        //            {
                                        //                HeadLoadCharges = decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString());
                                        //            }
                                        //        }
                                        //    }


                                        //}



                                        lblHeadLoadCharges.Text = HeadLoadCharges.ToString();
                                        lblnetamount.Text = (Convert.ToDecimal(lblGrossEarning.Text) + (Prodvalue) - PPD + SocAdjustAmount + ChillingCost + HeadLoadCharges).ToString();


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


            ViewState["InsertRecord"] = null;
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

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

            DataSet dsGetMilkDispatchDetail = (DataSet)ViewState["dsGetMilkDispatchDetail"];
            DataSet dsGetMilkBuffDetail = (DataSet)ViewState["dsGetMilkBuffDetail"];
            DataSet dsGetMilkCowhDetail = (DataSet)ViewState["dsGetMilkCowhDetail"];
            DataSet dsGetMilkADDetail = (DataSet)ViewState["dsGetMilkADDetail"];
            DataSet dsMainDataDetail = (DataSet)ViewState["dsMainDataDetail"];
            ds = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
                      new string[] { "flag", "OfficeId", "FDT", "TDT" },
                      new string[] { "4", SocietyID, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblSociety.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
                lblbankInfo.Text = ds.Tables[0].Rows[0]["IFSC"].ToString() + " / " + ds.Tables[0].Rows[0]["BankName"].ToString() + " / " + ds.Tables[0].Rows[0]["BankAccountNo"].ToString();
                lblOfficename.Text = ds.Tables[0].Rows[0]["AttachUnitName"].ToString() + " / " + ds.Tables[0].Rows[0]["AttachUnitCode"].ToString();
                lblBillingPeriod.Text = ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString();
            }
            if (dsMainDataDetail != null)
            {

                lblMilkValue.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["MilkValue"].ToString();
                lblCommission.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["Commission"].ToString();
                lblGrossEarning.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["GrossEarning"].ToString();
                lbldeductionadditionValue.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["DeductionAdditionValue"].ToString();
                lblProducerPayment.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["ProducerPayment"].ToString();
                lblProcAdjustAmount.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["ProducerAdjPayment"].ToString();
                lblSocAdjustAmount.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["SocietyAdjPayment"].ToString();
                lblCC.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["ChillingCost"].ToString();
                lblHeadLoadCharges.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["HeadLoadCharges"].ToString();
                lblnetamount.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["NetAmount"].ToString();
                if(objdb.Office_ID() == "4")
                {
                    lblNarration.Text = dsMainDataDetail.Tables[SocietyID].Rows[0]["CommissionNarration"].ToString();

                }
                else
                {
                    lblNarration.Text = "";
                }
            }
            if (dsGetMilkDispatchDetail != null)
            {
                gv_SocietyMilkDispatchDetail.DataSource = dsGetMilkDispatchDetail.Tables[SocietyID];
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
            if (dsGetMilkBuffDetail != null)
            {
                gv_SocietyBufData.DataSource = dsGetMilkBuffDetail.Tables[SocietyID];
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

            if (dsGetMilkCowhDetail != null)
            {
                gv_SocietyCowData.DataSource = dsGetMilkCowhDetail.Tables[SocietyID];
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

            if (dsGetMilkADDetail != null)
            {

                grhradsdetails.DataSource = dsGetMilkADDetail.Tables[SocietyID];
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
    protected void CreateDataSet()
    {
        DataSet dsGetMilkDispatchDetail = new DataSet();
        DataSet dsGetMilkBuffDetail = new DataSet();
        DataSet dsGetMilkCowhDetail = new DataSet();
        DataSet dsGetMilkADDetail = new DataSet();
        DataSet dsGetMilkADChildDetail = new DataSet();
        DataSet dsMainDataDetail = new DataSet();

        ViewState["dsGetMilkDispatchDetail"] = dsGetMilkDispatchDetail;
        ViewState["dsGetMilkBuffDetail"] = dsGetMilkBuffDetail;
        ViewState["dsGetMilkCowhDetail"] = dsGetMilkCowhDetail;
        ViewState["dsGetMilkADDetail"] = dsGetMilkADDetail;
        ViewState["dsGetMilkADChildDetail"] = dsGetMilkADChildDetail;
        ViewState["dsMainDataDetail"] = dsMainDataDetail;

    }
    protected void GetData()
    {



        DataSet ds1 = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
             new string[] { "flag", "Office_ID", "FDT", "TDT" },
             new string[] { "5", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        int ds1Count = ds1.Tables[0].Rows.Count;

        for (int j = 0; j < ds1Count; j++)
        {
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
            string OfficeId = ds1.Tables[0].Rows[j]["Office_ID"].ToString();
            ds = objdb.ByProcedure("Usp_RouteWiseMilkColllectionInvoice",
                        new string[] { "flag", "FDT", "TDT", "OfficeId", "Office_ID","CCID" },
                        new string[] { "1", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), OfficeId, objdb.Office_ID(),ddlccbmcdetail.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TotalFatinKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                    MilkValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Value"));
                    DataSet dsGetMilkDispatchDetail = (DataSet)ViewState["dsGetMilkDispatchDetail"];
                    DataTable dtMilkDispatch = new DataTable(OfficeId);

                    dtMilkDispatch.Columns.Add(new DataColumn("Date", typeof(string)));
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
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
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
                        dtMilkDispatch.Rows.Add(DT_DispatchDate, V_Shift, V_MilkType, FAT_Per, SNF_Per, MilkQty_InKG, FAT_IN_KG, SNF_IN_KG, Value, CLR, D_MilkQuality, Rate_Per_Ltr);
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

                    }
                    TFAT_IN_KG = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                 
                    TMilkQty_InKG = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQty_InKG"));
                    ViewState["dtMilkDispatch"] = dtMilkDispatch;
                    dsGetMilkDispatchDetail.Merge((DataTable)ViewState["dtMilkDispatch"]);
                    ViewState["dsGetMilkDispatchDetail"] = dsGetMilkDispatchDetail;
                }
                DataSet dsMainDataDetail = (DataSet)ViewState["dsMainDataDetail"];
                DataTable dtMainDataDetail = new DataTable(OfficeId);
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
                dtMainDataDetail.Columns.Add("CommissionNarration", typeof(string));
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
                    DataSet dsGetMilkCowhDetail = (DataSet)ViewState["dsGetMilkCowhDetail"];
                    DataTable dtMilkCowh = new DataTable(OfficeId);

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

                        dtMilkCowh.Rows.Add(V_MilkType, D_MilkQuality, FAT_Per, SNF_Per, MilkQty_InKGC, FAT_IN_KGC, SNF_IN_KG, CLR, Value, 0);
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
                    dsGetMilkCowhDetail.Merge((DataTable)ViewState["dtMilkCowh"]);
                    ViewState["dsGetMilkCowhDetail"] = dsGetMilkCowhDetail;

                }
                if (ds.Tables[1].Rows.Count > 0)
                {

                    //BufFatKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FAT_IN_KG"));
                    DataSet dsGetMilkBuffDetail = (DataSet)ViewState["dsGetMilkBuffDetail"];
                    DataTable dtMilkBuff = new DataTable(OfficeId);

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

                        dtMilkBuff.Rows.Add(V_MilkType, D_MilkQuality, FAT_Per, SNF_Per, MilkQty_InKGB, FAT_IN_KGB, SNF_IN_KG, CLR, Value, 0);
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
                    dsGetMilkBuffDetail.Merge((DataTable)ViewState["dtMilkBuff"]);
                    ViewState["dsGetMilkBuffDetail"] = dsGetMilkBuffDetail;
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
                DataSet dsGetMilkADDetail = (DataSet)ViewState["dsGetMilkADDetail"];
                DataSet dsGetMilkADChildDetail = (DataSet)ViewState["dsGetMilkADChildDetail"];
                DataTable dtGetMilkADDetail = new DataTable(OfficeId);
                dtGetMilkADDetail.Columns.Add(new DataColumn("S.No", typeof(int)));
                dtGetMilkADDetail.Columns.Add(new DataColumn("ItemBillingHead_Type", typeof(string)));
                dtGetMilkADDetail.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                dtGetMilkADDetail.Columns.Add(new DataColumn("ItemBillingHead_Name", typeof(string)));
                dtGetMilkADDetail.Columns.Add(new DataColumn("TotalAmount", typeof(decimal)));
                dtGetMilkADDetail.Columns.Add(new DataColumn("Remark", typeof(string)));
                dtGetMilkADDetail.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(decimal)));
                if (ds.Tables[6].Rows.Count > 0)
                {
                    DataTable dtadheadChild = new DataTable(OfficeId);
                    dtadheadChild.Columns.Add(new DataColumn("AddtionsDeducEntry_ID", typeof(int)));
                    dtadheadChild.Columns.Add(new DataColumn("CycleAmount", typeof(decimal)));
                    dtadheadChild.Columns.Add(new DataColumn("PrevoiusAmount", typeof(decimal)));
                    dtadheadChild.Columns.Add(new DataColumn("DeductedAmount", typeof(decimal)));

                    dtadheadChild.Columns.Add(new DataColumn("BalanceAmount", typeof(decimal)));
                    dtadheadChild.Columns.Add(new DataColumn("ItemBillingHead_ID", typeof(int)));
                    dtadheadChild.Columns.Add(new DataColumn("BillingFromDate", typeof(string)));
                    dtadheadChild.Columns.Add(new DataColumn("BillingToDate", typeof(string)));
                    dtadheadChild.Columns.Add(new DataColumn("Status", typeof(int)));
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
                            dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                                dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                            dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                                    dr[0] = (i + 1) + 1;
                                    dr[1] = ds.Tables[6].Rows[i]["ItemBillingHead_Type"].ToString();
                                    dr[2] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr[3] = ds.Tables[6].Rows[i]["ItemBillingHead_Name"].ToString();
                                    dr[4] = Amount;
                                    dr[5] = ds.Tables[6].Rows[i]["Remark"].ToString();
									dr[6] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                    dtGetMilkADDetail.Rows.Add(dr);

                                    dr1 = dtadheadChild.NewRow();
                                    dr1[0] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                    dr1[1] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                                    dr1[2] = ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString();
                                    dr1[3] = Amount;
                                    dr1[4] = decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString()) - Amount;
                                    dr1[5] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr1[6] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[7] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[8] = "1";


                                    dtadheadChild.Rows.Add(dr1);


                                    GrossEarning = GrossEarning - Amount;
                                    DeductionAdditionValue -= Amount;
                                }
                                else if (GrossEarning != 0)
                                {
                                    //decimal BalanceAmnt = GrossEarning - decimal.Parse(ds.Tables[7].Rows[i]["PrevoiusAmount"].ToString());
                                    Amount = Math.Abs(GrossEarning);
                                    dr = dtGetMilkADDetail.NewRow();
                                    dr[0] = (i + 1) + 1;
                                    dr[1] = ds.Tables[6].Rows[i]["ItemBillingHead_Type"].ToString();
                                    dr[2] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr[3] = ds.Tables[6].Rows[i]["ItemBillingHead_Name"].ToString();
                                    dr[4] = Amount;
                                    dr[5] = ds.Tables[6].Rows[i]["Remark"].ToString();
									dr[6] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"].ToString();
                                    dtGetMilkADDetail.Rows.Add(dr);

                                    dr1 = dtadheadChild.NewRow();
                                    dr1[0] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"];
                                    dr1[1] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                                    dr1[2] = ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString();
                                    dr1[3] = Amount;
                                    dr1[4] = decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString()) - Amount;
                                    dr1[5] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr1[6] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[7] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[8] = "0";

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
                                    dr1[0] = ds.Tables[6].Rows[i]["AddtionsDeducEntry_ID"];
                                    dr1[1] = ds.Tables[6].Rows[i]["CycleAmount"].ToString();
                                    dr1[2] = ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString();
                                    dr1[3] = "0";
                                    dr1[4] = decimal.Parse(ds.Tables[6].Rows[i]["CycleAmount"].ToString()) + decimal.Parse(ds.Tables[6].Rows[i]["PrevoiusAmount"].ToString());
                                    dr1[5] = ds.Tables[6].Rows[i]["ItemBillingHead_ID"].ToString();
                                    dr1[6] = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[7] = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                                    dr1[8] = "0";

                                    dtadheadChild.Rows.Add(dr1);


                                }
                            }
                           

                        }
                        else
                        {
                            dr = dtGetMilkADDetail.NewRow();
                            dr[0] = (i + 1) + 1;
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
                    dsGetMilkADChildDetail.Merge((DataTable)ViewState["dtadheadChild"]);
                    ViewState["dsGetMilkADChildDetail"] = dsGetMilkADChildDetail;





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
                            dr[0] = 1;
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
                                dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                            dr[0] = 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                        dr[0] = dtGetMilkADDetail.Rows.Count + 1;
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
                dsGetMilkADDetail.Merge((DataTable)ViewState["dtGetMilkADDetail"]);
                ViewState["dsGetMilkADDetail"] = dsGetMilkADDetail;

                lblGrossEarning.Text = (Math.Round(MilkValue + Convert.ToDecimal(lblCommission.Text), 2) + TotalEarning + HeadLoadCharges + ChillingCost).ToString();
                GrossEarning_T = decimal.Parse(lblGrossEarning.Text);




                //Calculate HeadLoadCharges

                //if (ddlMilkCollectionUnit.SelectedValue == "6")
                //{

                //    if (Mtrl_dy >= 21)
                //    {
                //        lblHeadLoadCharges.Visible = true;
                //        DataSet dsHeadLoadCharges = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess", new string[] { "flag", "OfficeId", "Office_ID", "FDT", "TDT" }, new string[] { "23", ddlSociety.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                //        if (dsHeadLoadCharges != null && dsHeadLoadCharges.Tables.Count > 0)
                //        {
                //            if (dsHeadLoadCharges.Tables[0].Rows.Count > 0)
                //            {
                //                HeadLoadCharges = decimal.Parse(dsHeadLoadCharges.Tables[0].Rows[0]["HeadLoadCharges"].ToString());
                //            }
                //        }
                //    }


                //}
                if (objdb.Office_ID() == "4" || objdb.Office_ID() == "3" || objdb.Office_ID() == "5" || objdb.Office_ID() == "7")
                {
                    NetAmount = Math.Round((GrossEarning_T + (DeductionAdditionValue) - ProducerPayment + SocietyAdjPayment ), 0);
                }
                else
                {
                    NetAmount = Math.Round((GrossEarning_T + (DeductionAdditionValue) - ProducerPayment + SocietyAdjPayment ), 2);
                }
                dtMainDataDetail.Rows.Add(MilkValue, Commission, GrossEarning_T, DeductionAdditionValue, ProducerPayment, ProducerAdjPayment, SocietyAdjPayment, HeadLoadCharges, ChillingCost, NetAmount, CommisionNarration);
                ViewState["dtMainDataDetail"] = dtMainDataDetail;
                dsMainDataDetail.Merge((DataTable)ViewState["dtMainDataDetail"]);
                ViewState["dsMainDataDetail"] = dsMainDataDetail;
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
                        sb.Append("<table class='table1'  style='width:100%; margin:5px;'>");
						 sb.Append("<thead class='header'>");
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
						sb.Append("</thead>");
                        sb.Append("</table>");

                        sb.Append("<table  class='table1'  style='width:100%; margin-top:20px;'>");
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
                        sb.Append("<table class='table'>");
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
                        sb.Append("<table class='table'>");
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
                        if (objdb.Office_ID() == "2")
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
        foreach(GridViewRow rows in GridView1.Rows)
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
                        sb.Append("<table class='table1'  style='width:100%;'>");
						 sb.Append("<thead class='header'>");
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
						sb.Append("</thead>");
                        sb.Append("</table>");

                        sb.Append("<table  class='table1'  style='width:100%;'>");
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
                        sb.Append("<table class='table'>");
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
                        sb.Append("<table class='table'>");
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
                        if (objdb.Office_ID() == "2")
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
            foreach (GridViewRow rows in GridView1.Rows)
            {

                LinkButton lnkPrint = (LinkButton)rows.FindControl("lnkPrint");
				Label lblStatus = (Label)rows.FindControl("lblStatus");
                if (lblStatus.Text == "Not generated")
                {
                    ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice", new string[] { "flag", "MilkCollectionInvoice_ID" }, new string[] { "20", lnkPrint.CommandArgument.ToString() }, "dataset");
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
                }
                
			  
            }
            CreateDataSet();
            GetData();
            btnGenerateInvoice_Click(sender, e);
            btnSubmit_Click(sender, e);
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