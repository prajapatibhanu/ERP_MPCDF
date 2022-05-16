using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_mcms_reports_mcms_dashboard : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.SessionID != null)
        {
            if (!IsPostBack)
            {
                ViewState["Dt"] = "";
                txtDSWiseQCDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCCWiseQCDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDateTankerWiseQCReport.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCCWiseTankerSealReport.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtOverallReportDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                GetOverallReportbydate();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void txtOverallReportDate_TextChanged(object sender, EventArgs e)
    {
        GetOverallReportbydate();
    }

    private void GetOverallReportbydate()
    {
        DataSet dsDSList = apiprocedure.ByProcedure("SpAdminOffice",
                        new string[] { "flag", "OfficeType_ID", "Office_ID" },
                        new string[] { "7", "2", apiprocedure.Office_ID() }, "dataset"); //2 is Office Type (Dugdh Sangh)

        DataTable dtOverallReport = new DataTable();

        dtOverallReport.Columns.Add("OfficeID", typeof(Int32));
        dtOverallReport.Columns.Add("Office_Name", typeof(String));
        dtOverallReport.Columns.Add("DT_ReportDate", typeof(String));
        dtOverallReport.Columns.Add("I_MilkQuantityCC", typeof(Decimal));
        dtOverallReport.Columns.Add("D_FATCC", typeof(Decimal));
        dtOverallReport.Columns.Add("D_SNFCC", typeof(Decimal));
        dtOverallReport.Columns.Add("DispatchCCCount", typeof(Decimal));
        dtOverallReport.Columns.Add("I_MilkQuantityDS", typeof(Decimal));
        dtOverallReport.Columns.Add("D_FATDS", typeof(Decimal));
        dtOverallReport.Columns.Add("D_SNFDS", typeof(Decimal));

        foreach (DataRow row in dsDSList.Tables[0].Rows)
        {
            DataSet dsOverallReport = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                                 new string[] { "flag", "OfficeID", "DT_Date" },
                                                 new string[] { "6", row["Office_ID"].ToString(), Convert.ToDateTime(txtOverallReportDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            DataRow dtRow = dtOverallReport.NewRow();

            if (dsOverallReport != null)
            {
                if (dsOverallReport.Tables.Count > 0)
                {
                    if (dsOverallReport.Tables[0].Rows.Count > 0)
                    {
                        dtRow["OfficeID"] = row["Office_ID"].ToString();
                        dtRow["Office_Name"] = dsOverallReport.Tables[0].Rows[0]["Office_Name"].ToString();
                        dtRow["DT_ReportDate"] = dsOverallReport.Tables[0].Rows[0]["DT_ReportDate"].ToString();
                        dtRow["I_MilkQuantityCC"] = dsOverallReport.Tables[0].Rows[0]["I_MilkQuantityCC"].ToString();
                        dtRow["D_FATCC"] = dsOverallReport.Tables[0].Rows[0]["D_FAT"].ToString();
                        dtRow["D_SNFCC"] = dsOverallReport.Tables[0].Rows[0]["D_SNF"].ToString();
                        dtRow["DispatchCCCount"] = dsOverallReport.Tables[0].Rows[0]["DispatchCCCount"].ToString();
                    }

                    if (dsOverallReport.Tables[1].Rows.Count > 0)
                    {
                        dtRow["I_MilkQuantityDS"] = dsOverallReport.Tables[1].Rows[0]["I_MilkQuantityDS"].ToString();
                        dtRow["D_FATDS"] = dsOverallReport.Tables[1].Rows[0]["D_FAT"].ToString();
                        dtRow["D_SNFDS"] = dsOverallReport.Tables[1].Rows[0]["D_SNF"].ToString();
                    }
                    dtOverallReport.Rows.Add(dtRow);
                }
            }
        }

        for (int i = dtOverallReport.Rows.Count - 1; i >= 0; i--)
        {
            int emptyColumnCount = 0;
            DataRow row = dtOverallReport.Rows[i];

            foreach (var rowItem in row.ItemArray)
            {
                if (rowItem == null || rowItem == DBNull.Value || rowItem.Equals(""))
                {
                    emptyColumnCount += 1;
                }
            }

            if (emptyColumnCount == dtOverallReport.Columns.Count)
            {
                dtOverallReport.Rows.Remove(row);
            }
        }

        gvDailyMilkCollection.DataSource = dtOverallReport;
        gvDailyMilkCollection.DataBind();
    }

    protected void ddlDSName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDSName.SelectedIndex != 0)
        {
            ddlCCName.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                              new string[] { "flag", "Office_Parant_ID" },
                              new string[] { "22", ddlDSName.SelectedValue }, "dataset");
            ddlCCName.DataTextField = "Office_Name";
            ddlCCName.DataValueField = "Office_ID";
            ddlCCName.DataBind();
            ddlCCName.Items.Insert(0, new ListItem("Select", "0"));

        }
        else
        {
            ddlCCName.DataSource = string.Empty;
            ddlCCName.DataBind();
            ddlCCName.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void ddlDSName_Init(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "OfficeType_ID", "Office_ID" },
                                  new string[] { "5", "2", apiprocedure.Office_ID() }, "dataset");
            ddlDSName.DataSource = ds1;
            ddlDSName.DataTextField = "Office_Name";
            ddlDSName.DataValueField = "Office_ID";
            ddlDSName.DataBind();
            ddlDSName.Items.Insert(0, new ListItem("Select", "0"));

            ddlDSName1.DataSource = ds1;
            ddlDSName1.DataTextField = "Office_Name";
            ddlDSName1.DataValueField = "Office_ID";
            ddlDSName1.DataBind();
            ddlDSName1.Items.Insert(0, new ListItem("Select", "0"));

            ddlDSName2.DataSource = ds1;
            ddlDSName2.DataTextField = "Office_Name";
            ddlDSName2.DataValueField = "Office_ID";
            ddlDSName2.DataBind();
            ddlDSName2.Items.Insert(0, new ListItem("Select", "0"));

            ddlDSName3.DataSource = ds1;
            ddlDSName3.DataTextField = "Office_Name";
            ddlDSName3.DataValueField = "Office_ID";
            ddlDSName3.DataBind();
            ddlDSName3.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ex.Message.ToString());
        }
    }

    protected void ddlCCName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCCName.SelectedIndex > 0)
            {
                ddlTankerNo.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                  new string[] { "flag", "Office_ID ", "DT_Date" },
                                  new string[] { "12", ddlCCName.SelectedValue, Convert.ToDateTime(txtDateTankerWiseQCReport.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                ddlTankerNo.DataTextField = "V_VehicleNoText";
                ddlTankerNo.DataValueField = "V_ReferenceCode";
                ddlTankerNo.DataBind();
                ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlTankerNo.DataSource = string.Empty;
                ddlTankerNo.DataBind();
                ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
        }
    }

    protected void btnSearchTankerWiseReport_Click(object sender, EventArgs e)
    {
        try
        {
            ClearTankerQCReport();

            //Usp_ComparisionReports
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "OfficeID ", "DT_Date", "V_ReferenceCode" },
                                  new string[] { "1", ddlCCName.SelectedValue, Convert.ToDateTime(txtDateTankerWiseQCReport.Text, cult).ToString("yyyy/MM/dd"), ddlTankerNo.SelectedValue }, "dataset");

            if (ds.Tables.Count > 0)
            {
                #region -- Milk Quantity Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQuantityTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQuantityTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQuantityTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQuantityTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQuantityTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQuantityTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString());
                    }

                    lblMilkQuantityTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkQuantityTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkQuantityTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkQuantityTanker_CC_S.Text = "0";
                    lblMilkQuantityTanker_CC_F.Text = "0";
                    lblMilkQuantityTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkQuantityTanker_CCDS_F.Text) == 0)
                {
                    lblMilkQuantityTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkQuantityTanker_CCDS_R.Text) == 0)
                {
                    lblMilkQuantityTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkQuantityTanker_CCDS_S.Text) == 0)
                {
                    lblMilkQuantityTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityTanker_CCDS_S.Font.Bold = true;
                }


                #endregion

                #region -- Milk Quality Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQualityTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MilkQuality"].ToString();
                    }
                    else
                    {
                        lblMilkQualityTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQualityTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MilkQuality"].ToString();
                    }
                    else
                    {
                        lblMilkQualityTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQualityTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MilkQuality"].ToString();
                    }
                    else
                    {
                        lblMilkQualityTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQualityTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MilkQuality"].ToString();
                    }
                    else
                    {
                        lblMilkQualityTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQualityTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MilkQuality"].ToString();
                    }
                    else
                    {
                        lblMilkQualityTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQualityTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MilkQuality"].ToString();
                    }
                    else
                    {
                        lblMilkQualityTanker_DS_R.Text = "0";
                    }

                    string cc_f = "", cc_r = "", cc_s = "", ds_f = "", ds_r = "", ds_s = "";

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MilkQuality"].ToString();
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MilkQuality"].ToString();
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MilkQuality"].ToString();
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MilkQuality"].ToString();
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MilkQuality"].ToString();
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MilkQuality"].ToString();
                    }

                    if (ds_f != "" && cc_f != "")
                    {
                        if (ds_f.Equals(cc_f))
                        {
                            lblMilkQualityTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                            lblMilkQualityTanker_CCDS_F.Text = "Match";
                            lblMilkQualityTanker_CCDS_F.Font.Bold = true;
                        }
                        else
                        {
                            lblMilkQualityTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                            lblMilkQualityTanker_CCDS_F.Text = "Mismatch";
                            lblMilkQualityTanker_CCDS_F.Font.Bold = true;
                        }
                    }
                    else
                    {
                        lblMilkQualityTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                        lblMilkQualityTanker_CCDS_F.Text = "0";
                        lblMilkQualityTanker_CCDS_F.Font.Bold = true;
                    }

                    if (ds_r != "" && cc_r != "")
                    {
                        if (ds_r.Equals(cc_r))
                        {
                            lblMilkQualityTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                            lblMilkQualityTanker_CCDS_R.Text = "Match";
                            lblMilkQualityTanker_CCDS_R.Font.Bold = true;
                        }
                        else
                        {
                            lblMilkQualityTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                            lblMilkQualityTanker_CCDS_R.Text = "Mismatch";
                            lblMilkQualityTanker_CCDS_R.Font.Bold = true;
                        }
                    }
                    else
                    {
                        lblMilkQualityTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                        lblMilkQualityTanker_CCDS_R.Text = "0";
                        lblMilkQualityTanker_CCDS_R.Font.Bold = true;
                    }

                    if (ds_s != "" && cc_s != "")
                    {
                        if (ds_s.Equals(cc_s))
                        {
                            lblMilkQualityTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                            lblMilkQualityTanker_CCDS_S.Text = "Match";
                            lblMilkQualityTanker_CCDS_S.Font.Bold = true;
                        }
                        else
                        {
                            lblMilkQualityTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                            lblMilkQualityTanker_CCDS_S.Text = "Mismatch";
                            lblMilkQualityTanker_CCDS_S.Font.Bold = true;
                        }
                    }
                    else
                    {
                        lblMilkQualityTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                        lblMilkQualityTanker_CCDS_S.Text = "0";
                        lblMilkQualityTanker_CCDS_S.Font.Bold = true;
                    }
                }
                else
                {
                    lblMilkQualityTanker_CC_S.Text = "0";
                    lblMilkQualityTanker_CC_F.Text = "0";
                    lblMilkQualityTanker_CC_R.Text = "0";

                    lblMilkQualityTanker_DS_S.Text = "0";
                    lblMilkQualityTanker_DS_F.Text = "0";
                    lblMilkQualityTanker_DS_R.Text = "0";

                    lblMilkQualityTanker_CCDS_S.Text = "0";
                    lblMilkQualityTanker_CCDS_F.Text = "0";
                    lblMilkQualityTanker_CCDS_R.Text = "0";
                }

                #endregion

                #region -- FAT % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkFATTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkFATTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkFATTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkFATTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkFATTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkFATTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_FAT"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_FAT"].ToString());
                    }

                    lblMilkFATTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkFATTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkFATTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkFATTanker_CC_S.Text = "0";
                    lblMilkFATTanker_CC_F.Text = "0";
                    lblMilkFATTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkFATTanker_CCDS_F.Text) == 0)
                {
                    lblMilkFATTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkFATTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkFATTanker_CCDS_R.Text) == 0)
                {
                    lblMilkFATTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkFATTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkFATTanker_CCDS_S.Text) == 0)
                {
                    lblMilkFATTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkFATTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATTanker_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- SNF % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkSNFTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkSNFTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkSNFTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkSNFTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkSNFTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkSNFTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_SNF"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_SNF"].ToString());
                    }

                    lblMilkSNFTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkSNFTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkSNFTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkSNFTanker_CC_S.Text = "0";
                    lblMilkSNFTanker_CC_F.Text = "0";
                    lblMilkSNFTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkSNFTanker_CCDS_F.Text) == 0)
                {
                    lblMilkSNFTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkSNFTanker_CCDS_R.Text) == 0)
                {
                    lblMilkSNFTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkSNFTanker_CCDS_S.Text) == 0)
                {
                    lblMilkSNFTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFTanker_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- CLR Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCLRTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCLRTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCLRTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCLRTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCLRTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCLRTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_CLR"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_CLR"].ToString());
                    }

                    lblMilkCLRTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkCLRTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkCLRTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkCLRTanker_CC_S.Text = "0";
                    lblMilkCLRTanker_CC_F.Text = "0";
                    lblMilkCLRTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkCLRTanker_CCDS_F.Text) == 0)
                {
                    lblMilkCLRTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkCLRTanker_CCDS_R.Text) == 0)
                {
                    lblMilkCLRTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkCLRTanker_CCDS_S.Text) == 0)
                {
                    lblMilkCLRTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRTanker_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Temp % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkTempTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkTempTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkTempTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkTempTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkTempTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkTempTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Temp"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Temp"].ToString());
                    }

                    lblMilkTempTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkTempTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkTempTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkTempTanker_CC_S.Text = "0";
                    lblMilkTempTanker_CC_F.Text = "0";
                    lblMilkTempTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkTempTanker_CCDS_F.Text) == 0)
                {
                    lblMilkTempTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkTempTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkTempTanker_CCDS_R.Text) == 0)
                {
                    lblMilkTempTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkTempTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkTempTanker_CCDS_S.Text) == 0)
                {
                    lblMilkTempTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkTempTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempTanker_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Acidity % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkAcidityTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkAcidityTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkAcidityTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkAcidityTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkAcidityTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkAcidityTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString());
                    }

                    lblMilkAcidityTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkAcidityTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkAcidityTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkAcidityTanker_CC_S.Text = "0";
                    lblMilkAcidityTanker_CC_F.Text = "0";
                    lblMilkAcidityTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkAcidityTanker_CCDS_F.Text) == 0)
                {
                    lblMilkAcidityTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkAcidityTanker_CCDS_R.Text) == 0)
                {
                    lblMilkAcidityTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkAcidityTanker_CCDS_S.Text) == 0)
                {
                    lblMilkAcidityTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityTanker_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Milk COB Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCOBTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_COB"].ToString();
                    }
                    else
                    {
                        lblMilkCOBTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCOBTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_COB"].ToString();
                    }
                    else
                    {
                        lblMilkCOBTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCOBTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_COB"].ToString();
                    }
                    else
                    {
                        lblMilkCOBTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCOBTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_COB"].ToString();
                    }
                    else
                    {
                        lblMilkCOBTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCOBTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_COB"].ToString();
                    }
                    else
                    {
                        lblMilkCOBTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCOBTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_COB"].ToString();
                    }
                    else
                    {
                        lblMilkCOBTanker_DS_R.Text = "0";
                    }

                    string cc_f = "", cc_r = "", cc_s = "", ds_f = "", ds_r = "", ds_s = "";

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_COB"].ToString();
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_COB"].ToString();
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_COB"].ToString();
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_COB"].ToString();
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_COB"].ToString();
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_COB"].ToString();
                    }

                    if (ds_f != "" && cc_f != "")
                    {
                        if (ds_f.Equals(cc_f))
                        {
                            lblMilkCOBTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                            lblMilkCOBTanker_CCDS_F.Text = "Match";
                            lblMilkCOBTanker_CCDS_F.Font.Bold = true;
                        }
                        else
                        {
                            lblMilkCOBTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                            lblMilkCOBTanker_CCDS_F.Text = "Mismatch";
                            lblMilkCOBTanker_CCDS_F.Font.Bold = true;
                        }
                    }
                    else
                    {
                        lblMilkCOBTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                        lblMilkCOBTanker_CCDS_F.Text = "0";
                        lblMilkCOBTanker_CCDS_F.Font.Bold = true;
                    }

                    if (ds_r != "" && cc_r != "")
                    {
                        if (ds_r.Equals(cc_r))
                        {
                            lblMilkCOBTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                            lblMilkCOBTanker_CCDS_R.Text = "Match";
                            lblMilkCOBTanker_CCDS_R.Font.Bold = true;
                        }
                        else
                        {
                            lblMilkCOBTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                            lblMilkCOBTanker_CCDS_R.Text = "Mismatch";
                            lblMilkCOBTanker_CCDS_R.Font.Bold = true;
                        }
                    }
                    else
                    {
                        lblMilkCOBTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                        lblMilkCOBTanker_CCDS_R.Text = "0";
                        lblMilkCOBTanker_CCDS_R.Font.Bold = true;
                    }

                    if (ds_s != "" && cc_s != "")
                    {
                        if (ds_s.Equals(cc_s))
                        {
                            lblMilkCOBTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                            lblMilkCOBTanker_CCDS_S.Text = "Match";
                            lblMilkCOBTanker_CCDS_S.Font.Bold = true;
                        }
                        else
                        {
                            lblMilkCOBTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                            lblMilkCOBTanker_CCDS_S.Text = "Mismatch";
                            lblMilkCOBTanker_CCDS_S.Font.Bold = true;
                        }
                    }
                    else
                    {
                        lblMilkCOBTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                        lblMilkCOBTanker_CCDS_S.Text = "0";
                        lblMilkCOBTanker_CCDS_S.Font.Bold = true;
                    }
                }
                else
                {
                    lblMilkCOBTanker_CC_S.Text = "0";
                    lblMilkCOBTanker_CC_F.Text = "0";
                    lblMilkCOBTanker_CC_R.Text = "0";

                    lblMilkCOBTanker_DS_S.Text = "0";
                    lblMilkCOBTanker_DS_F.Text = "0";
                    lblMilkCOBTanker_DS_R.Text = "0";

                    lblMilkCOBTanker_CCDS_S.Text = "0";
                    lblMilkCOBTanker_CCDS_F.Text = "0";
                    lblMilkCOBTanker_CCDS_R.Text = "0";
                }

                if (lblMilkCOBTanker_CCDS_F.Text == "0")
                {
                    lblMilkCOBTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkCOBTanker_CCDS_F.Font.Bold = true;
                }

                if (lblMilkCOBTanker_CCDS_R.Text == "0")
                {
                    lblMilkCOBTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkCOBTanker_CCDS_R.Font.Bold = true;
                }

                if (lblMilkCOBTanker_CCDS_S.Text == "0")
                {
                    lblMilkCOBTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkCOBTanker_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- MBRT % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkMBRTTanker_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTTanker_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkMBRTTanker_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTTanker_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkMBRTTanker_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTTanker_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkMBRTTanker_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTTanker_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkMBRTTanker_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTTanker_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkMBRTTanker_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTTanker_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString());
                    }

                    lblMilkMBRTTanker_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkMBRTTanker_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkMBRTTanker_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkMBRTTanker_CC_S.Text = "0";
                    lblMilkMBRTTanker_CC_F.Text = "0";
                    lblMilkMBRTTanker_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkMBRTTanker_CCDS_F.Text) == 0)
                {
                    lblMilkMBRTTanker_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTTanker_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTTanker_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTTanker_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkMBRTTanker_CCDS_R.Text) == 0)
                {
                    lblMilkMBRTTanker_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTTanker_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTTanker_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTTanker_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkMBRTTanker_CCDS_S.Text) == 0)
                {
                    lblMilkMBRTTanker_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTTanker_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTTanker_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTTanker_CCDS_S.Font.Bold = true;
                }

                #endregion
            }
            else
            {
                ClearTankerQCReport();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 3:" + ex.Message.ToString());
        }
    }

    private void ClearTankerQCReport()
    {
        lblMilkQuantityTanker_CC_F.Text = "0";
        lblMilkQuantityTanker_CC_R.Text = "0";
        lblMilkQuantityTanker_CC_S.Text = "0";
        lblMilkQuantityTanker_DS_F.Text = "0";
        lblMilkQuantityTanker_DS_R.Text = "0";
        lblMilkQuantityTanker_DS_S.Text = "0";
        lblMilkQuantityTanker_CCDS_F.Text = "0";
        lblMilkQuantityTanker_CCDS_R.Text = "0";
        lblMilkQuantityTanker_CCDS_S.Text = "0";
        lblMilkQualityTanker_CC_F.Text = "0";
        lblMilkQualityTanker_CC_R.Text = "0";
        lblMilkQualityTanker_CC_S.Text = "0";
        lblMilkQualityTanker_DS_F.Text = "0";
        lblMilkQualityTanker_DS_R.Text = "0";
        lblMilkQualityTanker_DS_S.Text = "0";
        lblMilkQualityTanker_CCDS_F.Text = "0";
        lblMilkQualityTanker_CCDS_R.Text = "0";
        lblMilkQualityTanker_CCDS_S.Text = "0";
        lblMilkFATTanker_CC_F.Text = "0";
        lblMilkFATTanker_CC_R.Text = "0";
        lblMilkFATTanker_CC_S.Text = "0";
        lblMilkFATTanker_DS_F.Text = "0";
        lblMilkFATTanker_DS_R.Text = "0";
        lblMilkFATTanker_DS_S.Text = "0";
        lblMilkFATTanker_CCDS_F.Text = "0";
        lblMilkFATTanker_CCDS_R.Text = "0";
        lblMilkFATTanker_CCDS_S.Text = "0";
        lblMilkSNFTanker_CC_F.Text = "0";
        lblMilkSNFTanker_CC_R.Text = "0";
        lblMilkSNFTanker_CC_S.Text = "0";
        lblMilkSNFTanker_DS_F.Text = "0";
        lblMilkSNFTanker_DS_R.Text = "0";
        lblMilkSNFTanker_DS_S.Text = "0";
        lblMilkSNFTanker_CCDS_F.Text = "0";
        lblMilkSNFTanker_CCDS_R.Text = "0";
        lblMilkSNFTanker_CCDS_S.Text = "0";
        lblMilkCLRTanker_CC_F.Text = "0";
        lblMilkCLRTanker_CC_R.Text = "0";
        lblMilkCLRTanker_CC_S.Text = "0";
        lblMilkCLRTanker_DS_F.Text = "0";
        lblMilkCLRTanker_DS_R.Text = "0";
        lblMilkCLRTanker_DS_S.Text = "0";
        lblMilkCLRTanker_CCDS_F.Text = "0";
        lblMilkCLRTanker_CCDS_R.Text = "0";
        lblMilkCLRTanker_CCDS_S.Text = "0";
        lblMilkTempTanker_CC_F.Text = "0";
        lblMilkTempTanker_CC_R.Text = "0";
        lblMilkTempTanker_CC_S.Text = "0";
        lblMilkTempTanker_DS_F.Text = "0";
        lblMilkTempTanker_DS_R.Text = "0";
        lblMilkTempTanker_DS_S.Text = "0";
        lblMilkTempTanker_CCDS_F.Text = "0";
        lblMilkTempTanker_CCDS_R.Text = "0";
        lblMilkTempTanker_CCDS_S.Text = "0";
        lblMilkAcidityTanker_CC_F.Text = "0";
        lblMilkAcidityTanker_CC_R.Text = "0";
        lblMilkAcidityTanker_CC_S.Text = "0";
        lblMilkAcidityTanker_DS_F.Text = "0";
        lblMilkAcidityTanker_DS_R.Text = "0";
        lblMilkAcidityTanker_DS_S.Text = "0";
        lblMilkAcidityTanker_CCDS_F.Text = "0";
        lblMilkAcidityTanker_CCDS_R.Text = "0";
        lblMilkAcidityTanker_CCDS_S.Text = "0";
        lblMilkCOBTanker_CC_F.Text = "0";
        lblMilkCOBTanker_CC_R.Text = "0";
        lblMilkCOBTanker_CC_S.Text = "0";
        lblMilkCOBTanker_DS_F.Text = "0";
        lblMilkCOBTanker_DS_R.Text = "0";
        lblMilkCOBTanker_DS_S.Text = "0";
        lblMilkCOBTanker_CCDS_F.Text = "0";
        lblMilkCOBTanker_CCDS_R.Text = "0";
        lblMilkCOBTanker_CCDS_S.Text = "0";
        lblMilkMBRTTanker_CC_F.Text = "0";
        lblMilkMBRTTanker_CC_R.Text = "0";
        lblMilkMBRTTanker_CC_S.Text = "0";
        lblMilkMBRTTanker_DS_F.Text = "0";
        lblMilkMBRTTanker_DS_R.Text = "0";
        lblMilkMBRTTanker_DS_S.Text = "0";
        lblMilkMBRTTanker_CCDS_F.Text = "0";
        lblMilkMBRTTanker_CCDS_R.Text = "0";
        lblMilkMBRTTanker_CCDS_S.Text = "0";
    }

    protected void btnCCWiseQCReport_Click(object sender, EventArgs e)
    {
        try
        {
            ClearCCQCReport();
            //Usp_ComparisionReports
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "OfficeID ", "DT_Date" },
                                  new string[] { "2", ddlCCName1.SelectedValue, Convert.ToDateTime(txtCCWiseQCDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables.Count > 0)
            {
                #region -- Milk Quantity Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQuantityChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQuantityChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQuantityChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQuantityChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQuantityChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQuantityChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString());
                    }

                    lblMilkQuantityChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkQuantityChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkQuantityChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkQuantityChillingCentre_CC_S.Text = "0";
                    lblMilkQuantityChillingCentre_CC_F.Text = "0";
                    lblMilkQuantityChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkQuantityChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkQuantityChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkQuantityChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkQuantityChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkQuantityChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkQuantityChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityChillingCentre_CCDS_S.Font.Bold = true;
                }


                #endregion

                #region -- FAT % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkFATChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkFATChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkFATChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkFATChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkFATChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkFATChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_FAT"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_FAT"].ToString());
                    }

                    lblMilkFATChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkFATChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkFATChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkFATChillingCentre_CC_S.Text = "0";
                    lblMilkFATChillingCentre_CC_F.Text = "0";
                    lblMilkFATChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkFATChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkFATChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkFATChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkFATChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkFATChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkFATChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkFATChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkFATChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkFATChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATChillingCentre_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- SNF % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkSNFChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkSNFChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkSNFChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkSNFChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkSNFChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkSNFChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_SNF"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_SNF"].ToString());
                    }

                    lblMilkSNFChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkSNFChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkSNFChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkSNFChillingCentre_CC_S.Text = "0";
                    lblMilkSNFChillingCentre_CC_F.Text = "0";
                    lblMilkSNFChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkSNFChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkSNFChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkSNFChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkSNFChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkSNFChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkSNFChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFChillingCentre_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- CLR Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCLRChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCLRChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCLRChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCLRChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCLRChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCLRChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_CLR"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_CLR"].ToString());
                    }

                    lblMilkCLRChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkCLRChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkCLRChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkCLRChillingCentre_CC_S.Text = "0";
                    lblMilkCLRChillingCentre_CC_F.Text = "0";
                    lblMilkCLRChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkCLRChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkCLRChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkCLRChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkCLRChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkCLRChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkCLRChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRChillingCentre_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Temp % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkTempChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkTempChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkTempChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkTempChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkTempChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkTempChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Temp"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Temp"].ToString());
                    }

                    lblMilkTempChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkTempChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkTempChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkTempChillingCentre_CC_S.Text = "0";
                    lblMilkTempChillingCentre_CC_F.Text = "0";
                    lblMilkTempChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkTempChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkTempChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkTempChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkTempChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkTempChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkTempChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkTempChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkTempChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkTempChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempChillingCentre_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Acidity % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkAcidityChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkAcidityChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkAcidityChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkAcidityChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkAcidityChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkAcidityChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString());
                    }

                    lblMilkAcidityChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkAcidityChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkAcidityChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkAcidityChillingCentre_CC_S.Text = "0";
                    lblMilkAcidityChillingCentre_CC_F.Text = "0";
                    lblMilkAcidityChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkAcidityChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkAcidityChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkAcidityChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkAcidityChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkAcidityChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkAcidityChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityChillingCentre_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- MBRT % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkMBRTChillingCentre_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTChillingCentre_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkMBRTChillingCentre_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTChillingCentre_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkMBRTChillingCentre_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTChillingCentre_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkMBRTChillingCentre_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTChillingCentre_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkMBRTChillingCentre_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTChillingCentre_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkMBRTChillingCentre_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString();
                    }
                    else
                    {
                        lblMilkMBRTChillingCentre_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString());
                    }

                    lblMilkMBRTChillingCentre_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkMBRTChillingCentre_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkMBRTChillingCentre_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkMBRTChillingCentre_CC_S.Text = "0";
                    lblMilkMBRTChillingCentre_CC_F.Text = "0";
                    lblMilkMBRTChillingCentre_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkMBRTChillingCentre_CCDS_F.Text) == 0)
                {
                    lblMilkMBRTChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTChillingCentre_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTChillingCentre_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTChillingCentre_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkMBRTChillingCentre_CCDS_R.Text) == 0)
                {
                    lblMilkMBRTChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTChillingCentre_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTChillingCentre_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTChillingCentre_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkMBRTChillingCentre_CCDS_S.Text) == 0)
                {
                    lblMilkMBRTChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTChillingCentre_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTChillingCentre_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTChillingCentre_CCDS_S.Font.Bold = true;
                }

                #endregion

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 4:" + ex.Message.ToString());
        }
    }

    private void ClearCCQCReport()
    {
        lblMilkQuantityChillingCentre_CC_F.Text = "0";
        lblMilkQuantityChillingCentre_CC_R.Text = "0";
        lblMilkQuantityChillingCentre_CC_S.Text = "0";
        lblMilkQuantityChillingCentre_DS_F.Text = "0";
        lblMilkQuantityChillingCentre_DS_R.Text = "0";
        lblMilkQuantityChillingCentre_DS_S.Text = "0";
        lblMilkQuantityChillingCentre_CCDS_F.Text = "0";
        lblMilkQuantityChillingCentre_CCDS_R.Text = "0";
        lblMilkQuantityChillingCentre_CCDS_S.Text = "0";
        lblMilkFATChillingCentre_CC_F.Text = "0";
        lblMilkFATChillingCentre_CC_R.Text = "0";
        lblMilkFATChillingCentre_CC_S.Text = "0";
        lblMilkFATChillingCentre_DS_F.Text = "0";
        lblMilkFATChillingCentre_DS_R.Text = "0";
        lblMilkFATChillingCentre_DS_S.Text = "0";
        lblMilkFATChillingCentre_CCDS_F.Text = "0";
        lblMilkFATChillingCentre_CCDS_R.Text = "0";
        lblMilkFATChillingCentre_CCDS_S.Text = "0";
        lblMilkSNFChillingCentre_CC_F.Text = "0";
        lblMilkSNFChillingCentre_CC_R.Text = "0";
        lblMilkSNFChillingCentre_CC_S.Text = "0";
        lblMilkSNFChillingCentre_DS_F.Text = "0";
        lblMilkSNFChillingCentre_DS_R.Text = "0";
        lblMilkSNFChillingCentre_DS_S.Text = "0";
        lblMilkSNFChillingCentre_CCDS_F.Text = "0";
        lblMilkSNFChillingCentre_CCDS_R.Text = "0";
        lblMilkSNFChillingCentre_CCDS_S.Text = "0";
        lblMilkCLRChillingCentre_CC_F.Text = "0";
        lblMilkCLRChillingCentre_CC_R.Text = "0";
        lblMilkCLRChillingCentre_CC_S.Text = "0";
        lblMilkCLRChillingCentre_DS_F.Text = "0";
        lblMilkCLRChillingCentre_DS_R.Text = "0";
        lblMilkCLRChillingCentre_DS_S.Text = "0";
        lblMilkCLRChillingCentre_CCDS_F.Text = "0";
        lblMilkCLRChillingCentre_CCDS_R.Text = "0";
        lblMilkCLRChillingCentre_CCDS_S.Text = "0";
        lblMilkTempChillingCentre_CC_F.Text = "0";
        lblMilkTempChillingCentre_CC_R.Text = "0";
        lblMilkTempChillingCentre_CC_S.Text = "0";
        lblMilkTempChillingCentre_DS_F.Text = "0";
        lblMilkTempChillingCentre_DS_R.Text = "0";
        lblMilkTempChillingCentre_DS_S.Text = "0";
        lblMilkTempChillingCentre_CCDS_F.Text = "0";
        lblMilkTempChillingCentre_CCDS_R.Text = "0";
        lblMilkTempChillingCentre_CCDS_S.Text = "0";
        lblMilkAcidityChillingCentre_CC_F.Text = "0";
        lblMilkAcidityChillingCentre_CC_R.Text = "0";
        lblMilkAcidityChillingCentre_CC_S.Text = "0";
        lblMilkAcidityChillingCentre_DS_F.Text = "0";
        lblMilkAcidityChillingCentre_DS_R.Text = "0";
        lblMilkAcidityChillingCentre_DS_S.Text = "0";
        lblMilkAcidityChillingCentre_CCDS_F.Text = "0";
        lblMilkAcidityChillingCentre_CCDS_R.Text = "0";
        lblMilkAcidityChillingCentre_CCDS_S.Text = "0";
        lblMilkMBRTChillingCentre_CC_F.Text = "0";
        lblMilkMBRTChillingCentre_CC_R.Text = "0";
        lblMilkMBRTChillingCentre_CC_S.Text = "0";
        lblMilkMBRTChillingCentre_DS_F.Text = "0";
        lblMilkMBRTChillingCentre_DS_R.Text = "0";
        lblMilkMBRTChillingCentre_DS_S.Text = "0";
        lblMilkMBRTChillingCentre_CCDS_F.Text = "0";
        lblMilkMBRTChillingCentre_CCDS_R.Text = "0";
        lblMilkMBRTChillingCentre_CCDS_S.Text = "0";
    }

    protected void ddlDSName1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDSName1.SelectedIndex != 0)
        {
            ddlCCName1.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                              new string[] { "flag", "Office_Parant_ID" },
                              new string[] { "22", ddlDSName1.SelectedValue }, "dataset");
            ddlCCName1.DataTextField = "Office_Name";
            ddlCCName1.DataValueField = "Office_ID";
            ddlCCName1.DataBind();
            ddlCCName1.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlCCName1.DataSource = string.Empty;
            ddlCCName1.DataBind();
            ddlCCName1.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnDSWiseQCReport_Click(object sender, EventArgs e)
    {
        try
        {
            ClearDSQCReport();
            //Usp_ComparisionReports
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "OfficeID ", "DT_Date" },
                                  new string[] { "3", ddlDSName2.SelectedValue, Convert.ToDateTime(txtDSWiseQCDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
            {
                #region -- Milk Quantity Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQuantityDugdhSangh_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQuantityDugdhSangh_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQuantityDugdhSangh_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkQuantityDugdhSangh_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkQuantityDugdhSangh_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkQuantityDugdhSangh_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString();
                    }
                    else
                    {
                        lblMilkQuantityDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["I_MilkQuantity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["I_MilkQuantity"].ToString());
                    }

                    lblMilkQuantityDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkQuantityDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkQuantityDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkQuantityDugdhSangh_CC_S.Text = "0";
                    lblMilkQuantityDugdhSangh_CC_F.Text = "0";
                    lblMilkQuantityDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkQuantityDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkQuantityDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkQuantityDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkQuantityDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkQuantityDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkQuantityDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkQuantityDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkQuantityDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkQuantityDugdhSangh_CCDS_S.Font.Bold = true;
                }


                #endregion

                #region -- FAT % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkFATDugdhSangh_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkFATDugdhSangh_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkFATDugdhSangh_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkFATDugdhSangh_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkFATDugdhSangh_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkFATDugdhSangh_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_FAT"].ToString();
                    }
                    else
                    {
                        lblMilkFATDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_FAT"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_FAT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_FAT"].ToString());
                    }

                    lblMilkFATDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkFATDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkFATDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkFATDugdhSangh_CC_S.Text = "0";
                    lblMilkFATDugdhSangh_CC_F.Text = "0";
                    lblMilkFATDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkFATDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkFATDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkFATDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkFATDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkFATDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkFATDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkFATDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkFATDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkFATDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkFATDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkFATDugdhSangh_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- SNF % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkSNFDugdhSangh_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkSNFDugdhSangh_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkSNFDugdhSangh_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkSNFDugdhSangh_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkSNFDugdhSangh_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkSNFDugdhSangh_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_SNF"].ToString();
                    }
                    else
                    {
                        lblMilkSNFDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_SNF"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_SNF"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_SNF"].ToString());
                    }

                    lblMilkSNFDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkSNFDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkSNFDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkSNFDugdhSangh_CC_S.Text = "0";
                    lblMilkSNFDugdhSangh_CC_F.Text = "0";
                    lblMilkSNFDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkSNFDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkSNFDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkSNFDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkSNFDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkSNFDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkSNFDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkSNFDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkSNFDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkSNFDugdhSangh_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- CLR % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCLRDugdhSangh_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCLRDugdhSangh_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCLRDugdhSangh_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkCLRDugdhSangh_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkCLRDugdhSangh_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkCLRDugdhSangh_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["D_CLR"].ToString();
                    }
                    else
                    {
                        lblMilkCLRDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["D_CLR"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["D_CLR"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["D_CLR"].ToString());
                    }

                    lblMilkCLRDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkCLRDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkCLRDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkCLRDugdhSangh_CC_S.Text = "0";
                    lblMilkCLRDugdhSangh_CC_F.Text = "0";
                    lblMilkCLRDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkCLRDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkCLRDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkCLRDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkCLRDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkCLRDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkCLRDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkCLRDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkCLRDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkCLRDugdhSangh_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Temp % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkTempDugdhSangh_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkTempDugdhSangh_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkTempDugdhSangh_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkTempDugdhSangh_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkTempDugdhSangh_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkTempDugdhSangh_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Temp"].ToString();
                    }
                    else
                    {
                        lblMilkTempDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Temp"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Temp"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Temp"].ToString());
                    }

                    lblMilkTempDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkTempDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkTempDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkTempDugdhSangh_CC_S.Text = "0";
                    lblMilkTempDugdhSangh_CC_F.Text = "0";
                    lblMilkTempDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkTempDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkTempDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkTempDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkTempDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkTempDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkTempDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkTempDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkTempDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkTempDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkTempDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkTempDugdhSangh_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- Acidity % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkAcidityDugdhSangh_CC_S.Text = ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkAcidityDugdhSangh_CC_F.Text = ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkAcidityDugdhSangh_CC_R.Text = ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkAcidityDugdhSangh_DS_S.Text = ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkAcidityDugdhSangh_DS_F.Text = ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkAcidityDugdhSangh_DS_R.Text = ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString();
                    }
                    else
                    {
                        lblMilkAcidityDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_Acidity"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_Acidity"].ToString());
                    }

                    lblMilkAcidityDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString();
                    lblMilkAcidityDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString();
                    lblMilkAcidityDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString();
                }
                else
                {
                    lblMilkAcidityDugdhSangh_CC_S.Text = "0";
                    lblMilkAcidityDugdhSangh_CC_F.Text = "0";
                    lblMilkAcidityDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkAcidityDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkAcidityDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkAcidityDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkAcidityDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkAcidityDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkAcidityDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkAcidityDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkAcidityDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkAcidityDugdhSangh_CCDS_S.Font.Bold = true;
                }

                #endregion

                #region -- MBRT % Row --

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkMBRTDugdhSangh_CC_S.Text = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        lblMilkMBRTDugdhSangh_CC_S.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkMBRTDugdhSangh_CC_F.Text = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        lblMilkMBRTDugdhSangh_CC_F.Text = "0";
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkMBRTDugdhSangh_CC_R.Text = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        lblMilkMBRTDugdhSangh_CC_R.Text = "0";
                    }

                    //At DS Fill

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        lblMilkMBRTDugdhSangh_DS_S.Text = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        lblMilkMBRTDugdhSangh_DS_S.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        lblMilkMBRTDugdhSangh_DS_F.Text = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        lblMilkMBRTDugdhSangh_DS_F.Text = "0";
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        lblMilkMBRTDugdhSangh_DS_R.Text = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString()).ToString("0.00");
                    }
                    else
                    {
                        lblMilkMBRTDugdhSangh_DS_R.Text = "0";
                    }

                    decimal cc_f = 0, cc_r = 0, cc_s = 0, ds_f = 0, ds_r = 0, ds_s = 0;

                    //Table 0 -- CC
                    if (ds.Tables[0].Select("V_SealLocation='F'").Length > 0)
                    {
                        cc_f = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='R'").Length > 0)
                    {
                        cc_r = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[0].Select("V_SealLocation='S'").Length > 0)
                    {
                        cc_s = Convert.ToDecimal(ds.Tables[0].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString());
                    }

                    //Table 1 -- DS
                    if (ds.Tables[1].Select("V_SealLocation='F'").Length > 0)
                    {
                        ds_f = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='F'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='R'").Length > 0)
                    {
                        ds_r = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='R'")[0]["V_MBRT"].ToString());
                    }

                    if (ds.Tables[1].Select("V_SealLocation='S'").Length > 0)
                    {
                        ds_s = Convert.ToDecimal(ds.Tables[1].Select("V_SealLocation='S'")[0]["V_MBRT"].ToString());
                    }

                    lblMilkMBRTDugdhSangh_CCDS_F.Text = (ds_f - cc_f).ToString("0.00");
                    lblMilkMBRTDugdhSangh_CCDS_R.Text = (ds_r - cc_r).ToString("0.00");
                    lblMilkMBRTDugdhSangh_CCDS_S.Text = (ds_s - cc_s).ToString("0.00");
                }
                else
                {
                    lblMilkMBRTDugdhSangh_CC_S.Text = "0";
                    lblMilkMBRTDugdhSangh_CC_F.Text = "0";
                    lblMilkMBRTDugdhSangh_CC_R.Text = "0";
                }

                if (Convert.ToDecimal(lblMilkMBRTDugdhSangh_CCDS_F.Text) == 0)
                {
                    lblMilkMBRTDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTDugdhSangh_CCDS_F.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTDugdhSangh_CCDS_F.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTDugdhSangh_CCDS_F.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkMBRTDugdhSangh_CCDS_R.Text) == 0)
                {
                    lblMilkMBRTDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTDugdhSangh_CCDS_R.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTDugdhSangh_CCDS_R.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTDugdhSangh_CCDS_R.Font.Bold = true;
                }

                if (Convert.ToDecimal(lblMilkMBRTDugdhSangh_CCDS_S.Text) == 0)
                {
                    lblMilkMBRTDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Green;
                    lblMilkMBRTDugdhSangh_CCDS_S.Font.Bold = true;
                }
                else
                {
                    lblMilkMBRTDugdhSangh_CCDS_S.ForeColor = System.Drawing.Color.Red;
                    lblMilkMBRTDugdhSangh_CCDS_S.Font.Bold = true;
                }

                #endregion

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }

    private void ClearDSQCReport()
    {
        lblMilkQuantityDugdhSangh_CC_F.Text = "0";
        lblMilkQuantityDugdhSangh_CC_R.Text = "0";
        lblMilkQuantityDugdhSangh_CC_S.Text = "0";
        lblMilkQuantityDugdhSangh_DS_F.Text = "0";
        lblMilkQuantityDugdhSangh_DS_R.Text = "0";
        lblMilkQuantityDugdhSangh_DS_S.Text = "0";
        lblMilkQuantityDugdhSangh_CCDS_F.Text = "0";
        lblMilkQuantityDugdhSangh_CCDS_R.Text = "0";
        lblMilkQuantityDugdhSangh_CCDS_S.Text = "0";
        lblMilkFATDugdhSangh_CC_F.Text = "0";
        lblMilkFATDugdhSangh_CC_R.Text = "0";
        lblMilkFATDugdhSangh_CC_S.Text = "0";
        lblMilkFATDugdhSangh_DS_F.Text = "0";
        lblMilkFATDugdhSangh_DS_R.Text = "0";
        lblMilkFATDugdhSangh_DS_S.Text = "0";
        lblMilkFATDugdhSangh_CCDS_F.Text = "0";
        lblMilkFATDugdhSangh_CCDS_R.Text = "0";
        lblMilkFATDugdhSangh_CCDS_S.Text = "0";
        lblMilkSNFDugdhSangh_CC_F.Text = "0";
        lblMilkSNFDugdhSangh_CC_R.Text = "0";
        lblMilkSNFDugdhSangh_CC_S.Text = "0";
        lblMilkSNFDugdhSangh_DS_F.Text = "0";
        lblMilkSNFDugdhSangh_DS_R.Text = "0";
        lblMilkSNFDugdhSangh_DS_S.Text = "0";
        lblMilkSNFDugdhSangh_CCDS_F.Text = "0";
        lblMilkSNFDugdhSangh_CCDS_R.Text = "0";
        lblMilkSNFDugdhSangh_CCDS_S.Text = "0";
        lblMilkCLRDugdhSangh_CC_F.Text = "0";
        lblMilkCLRDugdhSangh_CC_R.Text = "0";
        lblMilkCLRDugdhSangh_CC_S.Text = "0";
        lblMilkCLRDugdhSangh_DS_F.Text = "0";
        lblMilkCLRDugdhSangh_DS_R.Text = "0";
        lblMilkCLRDugdhSangh_DS_S.Text = "0";
        lblMilkCLRDugdhSangh_CCDS_F.Text = "0";
        lblMilkCLRDugdhSangh_CCDS_R.Text = "0";
        lblMilkCLRDugdhSangh_CCDS_S.Text = "0";
        lblMilkTempDugdhSangh_CC_F.Text = "0";
        lblMilkTempDugdhSangh_CC_R.Text = "0";
        lblMilkTempDugdhSangh_CC_S.Text = "0";
        lblMilkTempDugdhSangh_DS_F.Text = "0";
        lblMilkTempDugdhSangh_DS_R.Text = "0";
        lblMilkTempDugdhSangh_DS_S.Text = "0";
        lblMilkTempDugdhSangh_CCDS_F.Text = "0";
        lblMilkTempDugdhSangh_CCDS_R.Text = "0";
        lblMilkTempDugdhSangh_CCDS_S.Text = "0";
        lblMilkAcidityDugdhSangh_CC_F.Text = "0";
        lblMilkAcidityDugdhSangh_CC_R.Text = "0";
        lblMilkAcidityDugdhSangh_CC_S.Text = "0";
        lblMilkAcidityDugdhSangh_DS_F.Text = "0";
        lblMilkAcidityDugdhSangh_DS_R.Text = "0";
        lblMilkAcidityDugdhSangh_DS_S.Text = "0";
        lblMilkAcidityDugdhSangh_CCDS_F.Text = "0";
        lblMilkAcidityDugdhSangh_CCDS_R.Text = "0";
        lblMilkAcidityDugdhSangh_CCDS_S.Text = "0";
        lblMilkMBRTDugdhSangh_CC_F.Text = "0";
        lblMilkMBRTDugdhSangh_CC_R.Text = "0";
        lblMilkMBRTDugdhSangh_CC_S.Text = "0";
        lblMilkMBRTDugdhSangh_DS_F.Text = "0";
        lblMilkMBRTDugdhSangh_DS_R.Text = "0";
        lblMilkMBRTDugdhSangh_DS_S.Text = "0";
        lblMilkMBRTDugdhSangh_CCDS_F.Text = "0";
        lblMilkMBRTDugdhSangh_CCDS_R.Text = "0";
        lblMilkMBRTDugdhSangh_CCDS_S.Text = "0";
    }

    protected void btnCCWiseTankerQCReport_Click(object sender, EventArgs e)
    {
        try
        {
            //Usp_ComparisionReports
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "OfficeID ", "DT_Date" },
                                  new string[] { "4", ddlCCName3.SelectedValue, Convert.ToDateTime(txtCCWiseTankerSealReport.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_CCWiseTankerSealReport.DataSource = ds;
                gv_CCWiseTankerSealReport.DataBind();
            }
            else
            {
                gv_CCWiseTankerSealReport.DataSource = null;
                gv_CCWiseTankerSealReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6:" + ex.Message.ToString());
        }
    }

    protected void ddlDSName3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDSName3.SelectedIndex != 0)
            {
                ddlCCName3.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                              new string[] { "flag", "Office_Parant_ID" },
                              new string[] { "22", ddlDSName3.SelectedValue }, "dataset");
                ddlCCName3.DataTextField = "Office_Name";
                ddlCCName3.DataValueField = "Office_ID";
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

                ddlCCName3.DataSource = string.Empty;
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7:" + ex.Message.ToString());
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ShowTankerSealDetails")
            {
                //Usp_ComparisionReports
                ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                      new string[] { "flag", "OfficeID", "DT_Date", "V_ReferenceCode" },
                                      new string[] { "5", ddlCCName3.SelectedValue, Convert.ToDateTime(txtCCWiseTankerSealReport.Text, cult).ToString("yyyy/MM/dd"), e.CommandArgument.ToString() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    lblVehicleNo.Text = e.CommandArgument.ToString();

                    gvTankerSealDetailsForCC.DataSource = ds.Tables[0]; //For CC
                    gvTankerSealDetailsForCC.DataBind();

                    gvTankerSealDetailsForDS.DataSource = ds.Tables[1]; //For DS
                    gvTankerSealDetailsForDS.DataBind();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTankerSealDetails();", true);
                }
                else
                {
                    gvTankerSealDetailsForCC.DataSource = null;
                    gvTankerSealDetailsForCC.DataBind();

                    gvTankerSealDetailsForDS.DataSource = null;
                    gvTankerSealDetailsForDS.DataBind();

                    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 8:" + ex.Message.ToString());
        }
    }

    protected void gvDailyMilkCollection_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewData")
        {
           
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                     new string[] { "flag", "OfficeID", "DT_Date" },
                                     new string[] { "7", e.CommandArgument.ToString(), Convert.ToDateTime(txtOverallReportDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblUnitName.Text = ds.Tables[0].Rows[0]["UnitName"].ToString() + " [" + Convert.ToDateTime(txtOverallReportDate.Text, cult).ToString("dd-MMM-yyyy") + " ]";
                //dv_headerDetails.InnerHtml = "<center><h5>" + ds.Tables[0].Rows[0]["UnitName"].ToString() + "</h5></center>";

                gv_ReportedCC.DataSource = null;
                gv_ReportedCC.DataBind();

                gv_ReportedCC.DataSource = ds.Tables[0];
                gv_ReportedCC.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReportedCC()", true);
            }
            else
            {
                lblUnitName.Text = "-";
                gv_ReportedCC.DataSource = null;
                gv_ReportedCC.DataBind();
            }
        }
    }
}