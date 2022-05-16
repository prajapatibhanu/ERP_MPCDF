using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_RMRD_ReceiveTankerAtQC_RMRD : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (apiprocedure.createdBy() != null)
        {
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

                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                FillGrid();
                //txtDispatchTime.Text = DateTime.Now.ToString("hh:mm tt");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtArrivalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetReferenceInfo();
                Timer1_Tick(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }
    #region User Defined Function
    private void GetReferenceInfo()
    {

        DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                                  new string[] { "flag", "I_OfficeID"},
                                  new string[] { "3", apiprocedure.Office_ID()}, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {

            ddlReferenceNo.DataSource = ds1;
            ddlReferenceNo.DataTextField = "C_ReferenceNo";
            ddlReferenceNo.DataValueField = "BI_MilkInOutRMRDRefID";
            ddlReferenceNo.DataBind();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlReferenceNo.DataSource = string.Empty;
            ddlReferenceNo.DataBind();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtCLR.Text != "")
            { clr = Convert.ToDecimal(txtCLR.Text); }
            if (txtFat.Text != "")
            { fat = Convert.ToDecimal(txtFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }
    private decimal GetGridSNF(string Fat,string CLR)
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (Fat != "")
            { clr = Convert.ToDecimal(Fat); }
            if (CLR != "")
            { fat = Convert.ToDecimal(CLR); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }
    private DataTable GetAdulterationTestDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));
        if (ddlAdulterationTest.SelectedValue == "1")
        {
            foreach (GridViewRow row in gvmilkAdulterationtestdetail.Rows)
            {
                Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
                Label lblAdulterationType = (Label)row.FindControl("lblAdulterationType");
                DropDownList ddlAdelterationTestValue = (DropDownList)row.FindControl("ddlAdelterationTestValue");

                string SealLocation = "";
                switch (lblSealLocation.Text)
                {
                    case "Single":
                        SealLocation = "S";
                        break;
                    case "Rear":
                        SealLocation = "R";
                        break;
                    case "Front":
                        SealLocation = "F";
                        break;
                    default:
                        break;
                }

                dr = dt.NewRow();
                dr[0] = SealLocation;
                dr[1] = lblAdulterationType.Text;
                dr[2] = ddlAdelterationTestValue.SelectedValue;
                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
    private DataTable GetSampleMilkQualityDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("BI_MilkInOutRMRDRefID", typeof(int)));
        dt.Columns.Add(new DataColumn("I_EntryID", typeof(int)));
        dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
        dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(string)));     
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
         
        dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_Temp", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_Acidity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
        dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
        dt.Columns.Add(new DataColumn("V_MBRT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));
        
        
            foreach (GridViewRow row in gvSampleMilkQualityDetails.Rows)
            {
                Label lblBI_MilkCollectionID = (Label)row.FindControl("lblBI_MilkCollectionID");
                Label lblI_OfficeID = (Label)row.FindControl("lblI_OfficeID");
                Label lblLocation = (Label)row.FindControl("lblLocation");
                DropDownList ddlgvMilkQuality = (DropDownList)row.FindControl("ddlgvMilkQuality");
                TextBox txtgvFat = (TextBox)row.FindControl("txtgvFat");
                TextBox txtgvCLR = (TextBox)row.FindControl("txtgvCLR");
                TextBox txtgvSNF = (TextBox)row.FindControl("txtgvSNF");
                TextBox txtgvTemp = (TextBox)row.FindControl("txtgvTemp");

                
                dr = dt.NewRow();
                dr[0] = ddlReferenceNo.SelectedValue;
                dr[1] = lblBI_MilkCollectionID.Text;
                dr[2] = lblI_OfficeID.Text;
                dr[3] = "0";
                dr[4] = lblLocation.Text;
                
                dr[5] = txtgvFat.Text;
                dr[6] = txtgvSNF.Text;
                dr[7] = txtgvCLR.Text;
                dr[8] = txtgvTemp.Text;
                dr[9] = 0;
                dr[10] = "";
                dr[11] = "NA";
                dr[12] = 0;
                dr[13] = ddlgvMilkQuality.SelectedValue;
                dt.Rows.Add(dr);
            }
            string alcoholePerS = "";

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                alcoholePerS = ddlAlcohol.SelectedValue + "(" + txtAlcoholperc.Text + "%)";
            }
            else if (ddlAlcohol.SelectedValue == "Positive")
            {
                alcoholePerS = "Positive";
            }
            else
            {
                alcoholePerS = "0";
            }
            dr = dt.NewRow();
            dr[0] = ddlReferenceNo.SelectedValue;
            dr[1] = 0;
            dr[2] = apiprocedure.Office_ID();
           
            dr[3] = "0";
            dr[4] = "NA";
            dr[5] = txtFat.Text;
            dr[6] = txtSNF.Text;
            dr[7] = txtCLR.Text;
            dr[8] = txtTemprature.Text;
            dr[9] = txtAcidity.Text;
            dr[10] = ddlCOB.SelectedValue;
            dr[11] = alcoholePerS;

            dr[12] = txtMBRT.Text;
            dr[13] = ddlMilkQuality.SelectedItem.Text;
            dt.Rows.Add(dr);
            
        

        return dt;
    }
    private void FillGrid()
    {
        try
        {
            string date = "";

            if (txtfilterdate.Text != "")
            {
                date = Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd");
            }

            gvReceivedEntry.DataSource = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                               new string[] { "flag", "I_OfficeID", "Filter_Date" },
                               new string[] { "14", apiprocedure.Office_ID(), date }, "dataset");
            gvReceivedEntry.DataBind();
        }
       catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        } 
    }
    private void BindAdulterationTestGrid()
    {
        try
        {
            //Start Logic Here
            DataTable dtTL = new DataTable();
            DataRow drTL;

            DataSet DSAT = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
                                   new string[] { "flag" },
                                   new string[] { "1" }, "dataset");

            if (DSAT.Tables[0].Rows.Count != 0)
            {
                dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
                dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                if (ddlTankerType.SelectedValue == "D")
                {
                    
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = "Front";
                            drTL[2] = "F";
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = "Rear";
                            drTL[2] = "R";
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                    
                }
                else
                {
                    
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = ddlTankerType.SelectedValue;
                            drTL[2] = ddlTankerType.SelectedItem.Text.ToString();
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                   
                }
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();

            milktestdetail.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }
    #endregion

    #region Init Function
    protected void ddlMilkQuality_Init(object sender, EventArgs e)
    {
        ddlMilkQuality.DataSource = apiprocedure.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
        ddlMilkQuality.DataValueField = "V_MilkQualityList";
        ddlMilkQuality.DataTextField = "V_MilkQualityList";
        ddlMilkQuality.DataBind();
        ddlMilkQuality.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion

    #region change event
    private string MCMS_RuntimeChallan_In_QC_ChamberLocation_Validation(string Location)
    {
        string GetChamberLocationStatus = "0";

        try
        {

            DataSet dsChamberLocation = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                               new string[] { "flag", "I_EntryID", "V_SealLocation" },
                               new string[] { "6", ddlSampleNo.SelectedValue, Location }, "dataset");

            if (dsChamberLocation != null)
            {
                if (dsChamberLocation.Tables.Count > 0)
                {
                    if (dsChamberLocation.Tables[0].Rows.Count > 0)
                    {
                        GetChamberLocationStatus = dsChamberLocation.Tables[0].Rows[0]["Status"].ToString();

                        return GetChamberLocationStatus;
                    }
                    else
                    {
                        return GetChamberLocationStatus;
                    }
                }
                else
                {
                    return GetChamberLocationStatus;
                }
            }
            else
            {
                return GetChamberLocationStatus;
            }

        }
        catch (Exception)
        {

            return GetChamberLocationStatus;
        }

    }
    protected void ddlAlcohol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAlcoholperc.Text = "";

            if (ddlAlcohol.SelectedValue == "0")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Positive")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                DivAlcoholper.Visible = true;
            }
        }
        catch (Exception ex)
        {
            DivAlcoholper.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        txtSNF.Text = GetSNF().ToString();

        if (txtCLR.Text == "" || txtFat.Text == "")
        { txtSNF.Text = ""; }

        if (txtCLR.Text == "")
        {
            txtCLR.Focus();
        }

        if (txtFat.Text == "")
        {
            txtFat.Focus();
        }

        if (txtCLR.Text != "" && txtFat.Text != "")
        {
            txtMBRT.Focus();
        }
    }
    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            ViewState["InsertRecord"] = null;
            ViewState["InsertRecord2"] = null;


            txtDriverMobileNo.Text = "";
            txtDriverName.Text = "";
            txtVehicleNo.Text = "";
            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
            
            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtFat.Text = "";
            txtCLR.Text = "";
            txtSNF.Text = "";
            txtMBRT.Text = "";
            ddlAlcohol.ClearSelection();          
            txtAlcoholperc.Text = "";
            ddlAlcohol.ClearSelection();
            DivAlcoholper.Visible = false;
            if (ddlReferenceNo.SelectedValue != "0")
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                               new string[] { "flag", "BI_MilkInOutRMRDRefID" },
                               new string[] { "4", ddlReferenceNo.SelectedValue}, "dataset");
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    txtDriverMobileNo.Text = ds1.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                    txtDriverName.Text = ds1.Tables[0].Rows[0]["V_DriverName"].ToString();
                    txtVehicleNo.Text = ds1.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                    ddlTankerType.SelectedValue = ds1.Tables[0].Rows[0]["V_VehicleType"].ToString();
                  
                }
                if (ds1.Tables[1].Rows.Count != 0)
                {
                    panel.Visible = true;
                    BindAdulterationTestGrid();
                    gvSampleMilkQualityDetails.DataSource = ds1.Tables[1];
                    gvSampleMilkQualityDetails.DataBind();
                    foreach(GridViewRow rows in gvSampleMilkQualityDetails.Rows)
                    {
                        DropDownList ddlgvMilkQuality = (DropDownList)rows.FindControl("ddlgvMilkQuality");
                        TextBox txtgvSNF = (TextBox)rows.FindControl("txtgvSNF");
                        txtgvSNF.Attributes.Add("readonly", "readonly");
                        ddlgvMilkQuality.DataSource = apiprocedure.ByProcedure("USP_Mst_MilkQualityList",
                               new string[] { "flag" },
                               new string[] { "1" }, "dataset");
                        ddlgvMilkQuality.DataValueField = "V_MilkQualityList";
                        ddlgvMilkQuality.DataTextField = "V_MilkQualityList";
                        ddlgvMilkQuality.DataBind();
                        ddlgvMilkQuality.Items.Insert(0, new ListItem("Select", "0"));

                    }
                    //ddlSampleNo.DataSource = ds1.Tables[1];
                    //ddlSampleNo.DataTextField = "V_SampleNo";
                    //ddlSampleNo.DataValueField = "BI_MilkCollectionID";
                    //ddlSampleNo.DataBind();
                    //ddlSampleNo.Items.Insert(0, new ListItem("Select", "0"));


                }
                else
                {
                    panel.Visible = false;
                    gvSampleMilkQualityDetails.DataSource = string.Empty;
                    gvSampleMilkQualityDetails.DataBind();

                    //ddlSampleNo.DataSource = string.Empty;
                    //ddlSampleNo.DataBind();
                    //ddlSampleNo.Items.Insert(0, new ListItem("Select", "0"));
                }
               
            }
            else
            {
                panel.Visible = false;
                gvSampleMilkQualityDetails.DataSource = string.Empty;
                gvSampleMilkQualityDetails.DataBind();
                //ddlSampleNo.DataSource = string.Empty;
                //ddlSampleNo.DataBind();
                //ddlSampleNo.Items.Insert(0, new ListItem("Select", "0"));

            }

            //ddlSampleNo_SelectedIndexChanged(sender, e);


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    protected void ddlSampleNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          
            ddlUnitName.Items.Clear();
            if(ddlSampleNo.SelectedIndex > 0)
            {
                ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                               new string[] { "flag", "BI_MilkCollectionID" },
                               new string[] { "5", ddlSampleNo.SelectedValue }, "dataset");
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlUnitName.DataSource = ds;
                    ddlUnitName.DataTextField = "Office_Name";
                    ddlUnitName.DataValueField = "I_OfficeID";
                    ddlUnitName.DataBind();
                    ddlUnitName.SelectedValue = ds.Tables[0].Rows[0]["I_OfficeID"].ToString();

                    ddlCompartmentType.DataSource = ds;
                    ddlCompartmentType.DataTextField = "V_SealLocation";
                    ddlCompartmentType.DataValueField = "V_SealLocation";
                    ddlCompartmentType.DataBind();
                    ddlCompartmentType.SelectedValue = ds.Tables[0].Rows[0]["V_SealLocation"].ToString();
                   
                    BindAdulterationTestGrid();
                    

                }
            }
            ddlUnitName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlAdulterationTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdulterationTest.SelectedValue == "1")
        {
            milktestdetail.Visible = true;

        }
        else
        {
            milktestdetail.Visible = false;
        }
    }
    #endregion

    #region Button Event
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('New Notification');", true);

            //DataSet NotificationDS = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
            //                          new string[] { "flag", "I_OfficeID", "V_EntryType" },
            //                          new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");

            //if (NotificationDS.Tables[0].Rows.Count != 0)
            //{

            //    if (NotificationDS != null)
            //    {

            //        lblNotificationCount_Top.Text = NotificationDS.Tables[0].Rows.Count.ToString();
            //        lblNotificationCount.Text = NotificationDS.Tables[0].Rows.Count.ToString();
            //        Repeater1.DataSource = NotificationDS;
            //        Repeater1.DataBind();
            //    }
            //    else
            //    {
            //        lblNotificationCount_Top.Text = "0";
            //        lblNotificationCount.Text = "0";
            //        Repeater1.DataSource = string.Empty;
            //        Repeater1.DataBind();
            //    }

            //}
            //else
            //{
            //    lblNotificationCount_Top.Text = "0";
            //    lblNotificationCount.Text = "0";
            //    Repeater1.DataSource = string.Empty;
            //    Repeater1.DataBind();
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //mv_FormWizard.ActiveViewIndex += 1;

        //if (mv_FormWizard.ActiveViewIndex > 0)
        //{
        //    btnPrevious.Visible = true;
        //    if(ddlSampleNo.SelectedIndex > 0)
        //    {
        //        btnSubmit.Visible = true;
        //    }
            
        //}
        //else
        //{
        //    btnPrevious.Visible = false;
        //    btnSubmit.Visible = false;
        //}

        //if (mv_FormWizard.ActiveViewIndex < 1)
        //{
        //    btnNext.Visible = true;
        //    btnSubmit.Visible = false;
        //}
        //else
        //{
        //    btnNext.Visible = false;
        //    if (ddlSampleNo.SelectedIndex > 0)
        //    {
        //        btnSubmit.Visible = true;
        //    }
        //}
    }  
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        //mv_FormWizard.ActiveViewIndex -= 1;

        //if (mv_FormWizard.ActiveViewIndex > 0)
        //{
        //    btnPrevious.Visible = true;
        //    btnSubmit.Visible = true;
        //}
        //else
        //{
        //    btnPrevious.Visible = false;
        //    btnSubmit.Visible = false;
        //}

        //if (mv_FormWizard.ActiveViewIndex < 1)
        //{
        //    btnNext.Visible = true;
        //    btnSubmit.Visible = false;
        //}
        //else
        //{
        //    btnNext.Visible = false;
        //    btnSubmit.Visible = true;
        //}
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            
                lblMsg.Text = "";
                DataTable dt1 = new DataTable();
                dt1 = GetSampleMilkQualityDetails();

                DataTable dt2 = new DataTable();
                dt2 = GetAdulterationTestDetails();               

                if (dt1.Rows.Count > 0) //Check quality details filled atleast 1 row
                {
                    
                       

                        DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtArrivalDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtArrivalTime.Text, cult).ToString("hh:mm:ss tt")), cult);


                        ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                                    new string[] { "flag"
                                                ,"BI_MilkInOutRMRDRefID"                                               
                                    },
                                    new string[] { "7",
                                               ddlReferenceNo.SelectedValue,                                               
                                    },
                                   new string[] { "type_Trn_MilkQualityDetails_RMRD", "type_Trn_tblAdulterationTest_RMRD"},
                                   new DataTable[] { dt1, dt2}, "TableSave");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                Session["IsSuccess"] = true;


                                Response.Redirect("ReceiveTankerAtQC_RMRD.aspx", false);
                                
                            }
                            else
                            {
                                lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                Session["IsSuccess"] = false;
                            }
                            
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to fill all Sample milk quality detail!");
                    Session["IsSuccess"] = false;
                }
                //}
                //else
                //{
                //    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "Kindly enter total no. of seals first.");
                //    Session["IsSuccess"] = false;
                //}

                //Get After run code token for denied page refresh activity
                
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
    }
    #endregion
    protected void txtgvFat_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox Fat = (TextBox)row.FindControl("txtgvFat");
            TextBox CLR = (TextBox)row.FindControl("txtgvCLR");
            TextBox SNF = (TextBox)row.FindControl("txtgvSNF");
            SNF.Text = GetGridSNF(Fat.Text, CLR.Text).ToString();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
    protected void txtgvCLR_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox Fat = (TextBox)row.FindControl("txtgvFat");
            TextBox CLR = (TextBox)row.FindControl("txtgvCLR");
            TextBox SNF = (TextBox)row.FindControl("txtgvSNF");
            SNF.Text = GetGridSNF(Fat.Text, CLR.Text).ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
}