using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_OfficeEntryDashboard : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    CultureInfo cult = new CultureInfo("en-IN", true);
    decimal Totalmilkqty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtEffectiveDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

            GetOfficeCount();
            GetOverallReportbydate();
            GetDeoDetail();
            
        }

    }

    private void GetDeoDetail()
    {
        try
        {
            string date = "";

            if (txtEffectiveDate.Text != "")
            {
                date = Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy-MM-dd");
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Invalid Filter Date");
            }

           
            DataSet ds1Deo = objdb.ByProcedure("Admin_Office_Dashboard",
                              new string[] { "flag", "FilterDate" },
                              new string[] { "2", date }, "dataset");

            if (ds1Deo != null)
            {
                if (ds1Deo.Tables.Count > 0)
                {
                    if (ds1Deo.Tables[0].Rows.Count > 0)
                    {
                        gvDeoInfo.DataSource = ds1Deo;
                        gvDeoInfo.DataBind();

                        
                    }
                    else
                    {
                        gvDeoInfo.DataSource = string.Empty;
                        gvDeoInfo.DataBind();
                    }
                }
                else
                {
                    gvDeoInfo.DataSource = string.Empty;
                    gvDeoInfo.DataBind();
                }
            }
            else
            {
                gvDeoInfo.DataSource = string.Empty;
                gvDeoInfo.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }

    }

    private void GetOverallReportbydate()
    {
        DataSet dsDSList = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag", "OfficeType_ID", "Office_ID" },
                        new string[] { "7", "2", "2" }, "dataset"); //2 is Office Type (Dugdh Sangh)

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
            DataSet dsOverallReport = objdb.ByProcedure("Usp_ComparisionReports",
                                                 new string[] { "flag", "OfficeID", "DT_Date" },
                                                 new string[] { "6", row["Office_ID"].ToString(), Convert.ToDateTime(System.DateTime.Now, cult).ToString("yyyy/MM/dd") }, "dataset");

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


        if (dtOverallReport.Rows.Count > 0)
        {
            lblDispatchCCCount.Text = dtOverallReport.Rows[0]["DispatchCCCount"].ToString();
        }
        else
        {
            lblDispatchCCCount.Text = "0";
        }


    }
     
    private void GetOfficeCount()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("Admin_Office_Dashboard",
                              new string[] { "flag" },
                              new string[] { "1" }, "dataset");

            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {

                        lbltotalCC.Text = ds1.Tables[0].Rows[0]["CC_Count"].ToString();
                        lbllbltotalBMC.Text = ds1.Tables[1].Rows[0]["BMC_Count"].ToString() + "(" + ds1.Tables[3].Rows[0]["BMC_Producer_Count"].ToString() + ")";
                        lbltotalDCS.Text = ds1.Tables[2].Rows[0]["DCS_Count"].ToString() + "(" + ds1.Tables[4].Rows[0]["DCS_Producer_Count"].ToString() + ")";

                        lbllbltotalTotalProducer.Text = ds1.Tables[5].Rows[0]["Total_Producer_Count"].ToString();

                    }
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }

    }
     
    protected void txtEffectiveDate_TextChanged(object sender, EventArgs e)
    {
        GetDeoDetail();
    }
}