using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_BMC_ReceiveTankerChallanQAOld : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (apiprocedure.createdBy() != null)
        {
            if (!IsPostBack)
            {
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                // SET Datetime
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtArrivalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                GetReferenceInfo();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    private void GetReferenceInfo()
    {

        DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                                  new string[] { "flag", "I_OfficeID", "V_EntryType" },
                                  new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {

            ddlReferenceNo.DataSource = ds1;
            ddlReferenceNo.DataTextField = "C_ReferenceNo";
            ddlReferenceNo.DataValueField = "BI_MilkInOutRefID";
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

    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txttemperature.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtnetsnf.Text = "";
            divmqd.Visible = false;
            divadt.Visible = false;
            divaction.Visible = false;

            if (ddlReferenceNo.SelectedValue != "0")
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                               new string[] { "9", ddlReferenceNo.SelectedValue, "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {

                    ddlchallanno.DataSource = ds1;
                    ddlchallanno.DataTextField = "Challanno";
                    ddlchallanno.DataValueField = "I_EntryID";
                    ddlchallanno.DataBind();
                    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));


                }
                else
                {
                    ddlchallanno.DataSource = string.Empty;
                    ddlchallanno.DataBind();
                    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlchallanno.DataSource = string.Empty;
                ddlchallanno.DataBind();
                ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));

            }

            //ddlchallanno_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void ddlchallanno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();


            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txttemperature.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtnetsnf.Text = "";
            divmqd.Visible = false;
            divadt.Visible = false;
            divaction.Visible = false;

            if (ddlReferenceNo.SelectedValue != "0" && ddlchallanno.SelectedValue != "0")
            {

                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_EntryID" },
                               new string[] { "10", ddlReferenceNo.SelectedValue, ddlchallanno.SelectedValue }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {

                    ddlUnitName.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Office_Name"].ToString(), ds1.Tables[0].Rows[0]["I_OfficeID"].ToString()));
                    ddlUnitName.SelectedValue = ds1.Tables[0].Rows[0]["I_OfficeID"].ToString();
                    ddlUnitName.Enabled = false;

                    txtDriverName.Text = ds1.Tables[0].Rows[0]["V_DriverName"].ToString();
                    txtDriverMobileNo.Text = ds1.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                    txtVehicleNo.Text = ds1.Tables[0].Rows[0]["V_VehicleNo"].ToString();

                    dv_TankerType.Visible = true;
                    divmqd.Visible = true;
                    divadt.Visible = true;
                    divaction.Visible = true;

                    string ttpte = ds1.Tables[0].Rows[0]["V_MilkDispatchType"].ToString();

                    if (ttpte == "Tanker" || ttpte == "S")
                    {
                        ddlTankerType.SelectedValue = "S";
                    }

                    ddlCompartmentType.Items.Clear();
                    ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                    ddlCompartmentType.Enabled = false;
                    BindAdulterationTestGrid();
                    ddlReferenceNo.Enabled = false;

                }
                else
                {
                    ddlUnitName.ClearSelection();
                    txtDriverName.Text = "";
                    txtDriverMobileNo.Text = "";
                    txtVehicleNo.Text = "";
                    divmqd.Visible = false;
                    divadt.Visible = false;
                    divaction.Visible = false;
                    dv_TankerType.Visible = true;
                    ddlTankerType.ClearSelection();

                }
            }
            else
            {
                ddlUnitName.ClearSelection();
                txtDriverName.Text = "";
                txtDriverMobileNo.Text = "";
                txtVehicleNo.Text = "";
                divmqd.Visible = false;
                divadt.Visible = false;
                divaction.Visible = false;
                dv_TankerType.Visible = true;
                ddlTankerType.ClearSelection();
                ddlReferenceNo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            //btnAdd.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }

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

            gvReceivedEntry.DataSource = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "I_OfficeID", "V_EntryType", "Filter_Date" },
                               new string[] { "14", apiprocedure.Office_ID(), "In", date }, "dataset");
            gvReceivedEntry.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }

    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    //private void GetOffice()
    //{
    //    try
    //    {
    //        ddlUnitName.DataSource = apiprocedure.ByProcedure("Sp_CommonTables",
    //                             new string[] { "flag", "Office_ID", "V_ChallanNo" },
    //                             new string[] { "12", apiprocedure.Office_ID(), ddlchallanno.SelectedItem.Text }, "dataset");
    //        ddlUnitName.DataTextField = "Office_Name";
    //        ddlUnitName.DataValueField = "Office_ID";
    //        ddlUnitName.DataBind(); 
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
    //    }

    //}

    private void BindAdulterationTestGrid()
    {
        try
        {

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

                for (int i = 0; i < ddlCompartmentType.Items.Count; i++)
                {
                    for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                    {
                        drTL = dtTL.NewRow();
                        drTL[0] = dtTL.Rows.Count + 1;
                        drTL[1] = ddlCompartmentType.SelectedValue;
                        drTL[2] = ddlCompartmentType.SelectedItem.Text.ToString();
                        drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                        dtTL.Rows.Add(drTL);
                    }
                }
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

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

    private int GetMQualityEntry()
    {
        int MQDCOUNT = 0;

        try
        {
            DataSet GetQualityEntry = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                      new string[] { "flag", "V_ReferenceCode" },
                                      new string[] { "34", ddlchallanno.SelectedItem.Text }, "dataset");

            if (GetQualityEntry != null)
            {
                if (GetQualityEntry.Tables.Count > 0)
                {
                    if (GetQualityEntry.Tables[0].Rows.Count > 0)
                    {
                        MQDCOUNT = GetQualityEntry.Tables[0].Rows.Count;
                    }
                    else
                    {
                        return MQDCOUNT;
                    }
                }
                else
                {
                    return MQDCOUNT;
                }

            }

            return MQDCOUNT;

        }
        catch (Exception)
        {

            return MQDCOUNT;
        }

    }

    private DataTable GetAdulterationTestDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

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
        return dt;
    }

    private DataTable GetMilkQualityDetail()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("EntryShift", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));

        if (txtNetFat.Text != null && txtNetCLR.Text != null && txtnetsnf.Text != null)
        {
            dr = dt.NewRow();
            dr[0] = Convert.ToDecimal("0.00"); ;
            dr[1] = Convert.ToDecimal(txtNetFat.Text);
            dr[2] = Convert.ToDecimal(txtnetsnf.Text);
            dr[3] = Convert.ToDecimal(txtNetCLR.Text);
            dr[4] = GetShift();
            dr[5] = apiprocedure.Office_ID();
            dt.Rows.Add(dr);
        }

        return dt;
    }

    private string GetShift()
    {

        try
        {
            DataSet dsct = apiprocedure.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            string[] s = currrentime.Split(':');

            if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            {
                return "Morning";
            }
            else
            {
                return "Evening";
            }

        }
        catch (Exception ex)
        {
            return "";
        }

    }

    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtNetCLR.Text != "")
            { clr = Convert.ToDecimal(txtNetCLR.Text); }
            if (txtNetFat.Text != "")
            { fat = Convert.ToDecimal(txtNetFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }

    protected void txtNetFat_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }

    }

    protected void txtNetCLR_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }

    }



    // Runtime Validation

    public string BMC_GetChallanStatus()
    {
        string strtankerlivestatus = "0";

        try
        {
            DataSet dscheckTanker = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_ChallanNo", "V_EntryType" },
                               new string[] { "21", ddlReferenceNo.SelectedValue, ddlchallanno.SelectedItem.Text, "In" }, "dataset");

            if (dscheckTanker != null)
            {
                if (dscheckTanker.Tables.Count > 0)
                {
                    if (dscheckTanker.Tables[0].Rows.Count > 0)
                    {
                        strtankerlivestatus = dscheckTanker.Tables[0].Rows[0]["Status"].ToString();

                        return strtankerlivestatus;
                    }
                    else
                    {
                        return strtankerlivestatus;
                    }
                }
                else
                {
                    return strtankerlivestatus;
                }
            }
            else
            {
                return strtankerlivestatus;
            }

        }
        catch (Exception ex)
        {

            return strtankerlivestatus;
        }


    }

    public string BMC_ChallanRunTime_GrossWeight_Validation()
    {
        string ReferenceGrossWeight = "0";

        try
        {

            DataSet dsReferenceGrossWeight = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "20", ddlReferenceNo.SelectedValue }, "dataset");

            if (dsReferenceGrossWeight != null)
            {
                if (dsReferenceGrossWeight.Tables.Count > 0)
                {
                    if (dsReferenceGrossWeight.Tables[0].Rows.Count > 0)
                    {
                        ReferenceGrossWeight = dsReferenceGrossWeight.Tables[0].Rows[0]["Status"].ToString();

                        return ReferenceGrossWeight;
                    }
                    else
                    {
                        return ReferenceGrossWeight;
                    }
                }
                else
                {
                    return ReferenceGrossWeight;
                }
            }
            else
            {
                return ReferenceGrossWeight;
            }

        }
        catch (Exception)
        {

            return ReferenceGrossWeight;
        }
    }

    public string BMC_RuntimeChallan_In_QC_NetWeight_Validation()
    {
        string ChallanNetWeight = "0";

        try
        {

            DataSet dschallanNetWeight = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "V_ChallanNo", },
                               new string[] { "22", ddlchallanno.SelectedItem.Text }, "dataset");

            if (dschallanNetWeight != null)
            {
                if (dschallanNetWeight.Tables.Count > 0)
                {
                    if (dschallanNetWeight.Tables[0].Rows.Count > 0)
                    {
                        ChallanNetWeight = dschallanNetWeight.Tables[0].Rows[0]["Status"].ToString();

                        return ChallanNetWeight;
                    }
                    else
                    {
                        return ChallanNetWeight;
                    }
                }
                else
                {
                    return ChallanNetWeight;
                }
            }
            else
            {
                return ChallanNetWeight;
            }

        }
        catch (Exception)
        {

            return ChallanNetWeight;
        }
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                // Check Runtime Challan Info

                if (ddlchallanno.SelectedIndex > 0)
                {
                    string strtS = BMC_GetChallanStatus();

                    if (strtS == "0")
                    {

                    }
                    else
                    {
                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Challan No - " + ddlchallanno.SelectedItem.Text + " QC Entry already exists!");
                        FillGrid();
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Please select Challan Number.!");
                    return;
                }

                // Check Gross Weight Enter Or Not

                if (ddlReferenceNo.SelectedIndex > 0)
                {
                    string strtR = BMC_ChallanRunTime_GrossWeight_Validation();

                    if (strtR == "1")
                    {

                    }
                    else
                    {

                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Reference No. " + ddlReferenceNo.SelectedItem.Text + " Gross Weight Not Updated!");
                        FillGrid();
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                    return;
                }

                // Check Net Weight Enter Or Not

                if (ddlReferenceNo.SelectedIndex > 0)
                {
                    string strtR = BMC_RuntimeChallan_In_QC_NetWeight_Validation();

                    if (strtR == "0")
                    {

                    }
                    else
                    {

                        GetReferenceInfo();
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Challan No. " + ddlchallanno.SelectedItem.Text + " Net Weight Updated SO QC Entry Not a Process!");
                        FillGrid();
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                    return;
                }


                lblMsg.Text = "";
                DataTable dtMquality = new DataTable();

                dtMquality = GetMilkQualityDetail();

                //Check Milk Quality

                if (dtMquality.Rows.Count > 0)
                {
                    dtMquality = GetMilkQualityDetail();
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : Required All Quality Details!");
                    Session["IsSuccess"] = false;
                }

                //Check adulteration details filled all row

                DataTable dtAdultration = new DataTable();
                dtAdultration = GetAdulterationTestDetails();

                DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtArrivalDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtArrivalTime.Text, cult).ToString("hh:mm:ss tt")), cult);


                if (dtAdultration.Rows.Count > 0)
                {

                    ds = null;
                    ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                            new string[] { "flag", 
                                                 "V_ChallanNo",
				                                "I_OfficeID",
				                                "I_OfficeTypeID",
				                                "AttachedToCC",
				                                "V_MilkDispatchType",
                                                "I_TotalCans",
				                                "V_VehicleNo",
				                                "V_DriverName",
				                                "V_DriverMobileNo",
				                                "V_EntryType",
				                                "D_MilkQuality",
				                                "V_Shift",
				                                "D_MilkQuantity",
				                                "FAT",
				                                "CLR",
				                                "SNF",  
                                                "DT_ArrivalDateTime",
				                                "I_CreatedByEmpID",
				                                "V_Remark",
				                                "V_EntryFrom",
                                                "V_Temp",
                                                "MilkSaleQty",
                                                "MilkSaleRatePerLtr",
                                                "MilkSaleAmount",
                                                "NetMilkQtyInKG",
                                                "NetFATInKG",
                                                "NetSNFInKG",
                                                "BI_MilkInOutRefID",
                                                "SampalNo",
                                                "ScaleReading",
                                                "DT_Date"
                                                 },

                                                new string[] { "15",
  
                                                ddlchallanno.SelectedItem.Text,
                                                apiprocedure.Office_ID(),
                                                apiprocedure.OfficeType_ID(), 
                                                ddlUnitName.SelectedValue,
                                                ddlTankerType.SelectedValue,
                                                "0",
                                                txtVehicleNo.Text,
                                                txtDriverName.Text,
                                                txtDriverMobileNo.Text,
                                                "In",
                                                ddlMilkQuality.SelectedItem.Text,
                                                GetShift(),
                                                "0.00",
                                                txtNetFat.Text,
                                                txtNetCLR.Text,
                                                txtnetsnf.Text,
                                                ADate.ToString(),
                                                apiprocedure.createdBy(),
                                                "",
                                                "Web",
                                                txttemperature.Text,
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                ddlReferenceNo.SelectedValue,
                                                txtSampalNo.Text,
                                                "0.00",
                                                Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                                },
                                             new string[] { "type_Trn_tblAdulterationTest_MCU", "type_Trn_MilkQualityDetails_MCU" },
                                             new DataTable[] { dtAdultration, dtMquality }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("BMC_ReceiveTankerChallanQAOld.aspx", false);

                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        Session["IsSuccess"] = false;
                    }

                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : Required All Adulteration Test!");
                    Session["IsSuccess"] = false;
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
    }



}