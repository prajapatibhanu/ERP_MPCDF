using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class mis_MisDailyRpt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    DataSet ds, ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillOffice();
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("sp_Mis_ReportDaily",
                            new string[] { "flag" },
                            new string[] { "4" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
                //ddlOfficeName.Items.Insert(0, new ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();

    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            string msg = "";
            string rptDate = "";
            string OfficeIDList = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            div_page_content.InnerHtml = "";

            if (txtReportDate.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            else
            {
                rptDate = Convert.ToDateTime(txtReportDate.Text, cult).ToString("yyyy/MM/dd");
            }
            foreach (ListItem item in ddlOfficeName.Items)
            {
                if (item.Selected)
                {
                    OfficeIDList += item.Value + ",";
                }
            }

            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("sp_Mis_ReportDaily",
                            new string[] { "flag", "Tmc_FromDate", "Office_ID_List" },
                            new string[] { "5", rptDate, OfficeIDList }, "dataset");

             DataSet   ds2 = objdb.ByProcedure("sp_Mis_ReportDaily",
                            new string[] { "flag", "Tmc_FromDate", "Office_ID_List" },
                            new string[] { "6", rptDate, OfficeIDList }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {

                    int Count = ds.Tables[0].Rows.Count;
                    StringBuilder sb1 = new StringBuilder();

                    sb1.Append("<table class='table table-bordered' >");
                    sb1.Append("<tr>");
                    sb1.Append("<th>Particulars</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<th>" + ds.Tables[0].Rows[i]["OfficeName"].ToString() + "</th>");
                       
                    }
                    sb1.Append("<th>Total</td>");

                    sb1.Append("</tr>");
                    sb1.Append("<td>Current TGT</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_CurrentTGT"].ToString() + "</td>");

                        
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_CurrentTGT"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>On Reporting Date</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_OnReportingDate"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_OnReportingDate"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Fat %</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_Fat"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>SNF %</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_SNF"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Monthly Average Till Date</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_MonthlyAverageTillDate"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_MonthlyAverageTillDate"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Last Year Same Month</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_LastYearSameMonth"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_LastYearSameMonth"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Last Month (Provisional)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_LastMonth_Provisional"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_LastMonth_Provisional"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cummulative Till Date (Current Year)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_CummulativeTillDate_CurrentYear"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_CummulativeTillDate_CurrentYear"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cummulative Till Date (Last Year)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_CummulativeTillDate_LastYear"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_CummulativeTillDate_LastYear"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Growth %</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tmc_GrowthPercentage"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tmc_GrowthPercentage"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<th>Local Milk Sale (TLPD)</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<th>(TLPD)</th>");
                    }
                    sb1.Append("<th>(TLPD)</th>");
                    sb1.Append("</tr>");

                    sb1.Append("<td>Current Month Target</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_CurrentMonthTarget"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_CurrentMonthTarget"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>On Reporting Date</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_OnReportingDate"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_OnReportingDate"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Monthly Average Till Date</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_MonthlyAverageTillDate"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_MonthlyAverageTillDate"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Last Year Same Month</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_LastYearSameMonth"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_LastYearSameMonth"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Last Month (Provisional)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_LastMonth_Provisional"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_LastMonth_Provisional"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cummulative Till Date (Current Year)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_CummulativeTillDate_CurrentYear"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_CummulativeTillDate_CurrentYear"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cummulative Till Date (Last Year)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_CummulativeTillDate_LastYear"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_CummulativeTillDate_LastYear"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Growth %</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Tms_GrowthPercentage"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Tms_GrowthPercentage"].ToString() + "</td>");


                    //-------------------------------------------------------------------------------------------------------------------------------------
                    sb1.Append("</tr>");
                    sb1.Append("<th>Milk Procurement Price(Rs.)</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<th></th>");
                    }
                    sb1.Append("<th></th>");
                    sb1.Append("</tr>");

                    sb1.Append("<td>Buffalo Milk (Rs. /KG Fat) CM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Mcp_BuffaloMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Mcp_LastYearSameMonthBuffalo"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cow Milk (Rs.KG) CM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Mcp_CowMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Mcp_LastYearSameMonthCow"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    //-----------------------------------------------------------------------------------------------------
                    sb1.Append("<th>Sale Price (Rs/Lit)</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<th></th>");
                    }
                    sb1.Append("<th></th>");
//-------------------------------------------------------------------------------------------------------------------------------------
                    sb1.Append("</tr>");
                    sb1.Append("<td>Full Cream Milk CM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_FullCreamMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_LastYearSameMonthFullCreamMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Standard Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_StandardMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_LastYearSameMonthStandardMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Tonned Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_TonnedMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_LastYearSameMonthTonnedMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Double Tonned Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_DoubleTonnedMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_LastYearSameMonthTonnedDoubleTonnedMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Skim Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_SkimMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_LastYearSameMonthSkimMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Chah Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_ChahMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>LYSM</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_LastYearSameMonthChahMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Tea Special</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_TeaSpecail"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cow Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Sp_CowMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>-</td>");


//----------------------------------------------------------------------------------------------------------------------------------------------------------------------






                    sb1.Append("</tr>");
                    sb1.Append("<th>Product Sale (cummulative During the Month)</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<th></th>");
                    }
                    sb1.Append("<th></th>");
                    sb1.Append("</tr>");

                    sb1.Append("<td>Ghee</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Ghee"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Ghee"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>SMP+WMP</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_SMP_WMP"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_SMP_WMP"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>White Butter</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_WhiteButter"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_WhiteButter"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Table Butter</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_TableButter"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_TableButter"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Sweet SMP</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_SweetSMP"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_SweetSMP"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Shrikhand</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_ShriKhand"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_ShriKhand"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Paneer</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Paneer"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Paneer"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Flavoured Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_FlavouredMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_FlavouredMilk"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Butter Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_ButterMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_ButterMilk"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Lassi</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Lassi"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Lassi"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Peda</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Peda"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Peda"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Sweet Curd</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_SweetCurd"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_SweetCurd"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Plain Curd</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_PlainCurd"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_PlainCurd"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Probiotic Curd</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_ProbioticCurd"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_ProbioticCurd"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Chhena Kheer</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_ChhenaKheer_Rabadi"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_ChhenaKheer_Rabadi"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Khowa (Mawa)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Khowa_Mawa"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Khowa_Mawa"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Rasgulla</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Rasgulla"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Rasgulla"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Gulab Jamun</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_GulabJamun"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_GulabJamun"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Milk Cake</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_MilkCake"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_MilkCake"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Chakka</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Chakka"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Chakka"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Thandai Pet Bottle</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_Thandai"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_Thandai"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>F/ Milk Bottle</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_F_MilkBottle"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_F_MilkBottle"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Lassi Lite</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_LassiLite"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_LassiLite"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Nariyal Barfi</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_NariyalBarfi"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_NariyalBarfi"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Gulab Jamun Mix</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_GulabJamunMix"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_GulabJamunMix"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Paneer Achaar</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_PaneerAchaar"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_PaneerAchaar"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Coffee Mix Powder</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_CoffeeMixPowder"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_CoffeeMixPowder"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Cooking Butter</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_CookingButter"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_CookingButter"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Low Fat Paneer</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_LowFatPaneer"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_LowFatPaneer"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Peda Prasad</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_PedaPrasad"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_PedaPrasad"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Sanchi Ice Cream</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_SanchiIceCream"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_SanchiIceCream"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<td>Sanchi Golden Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Ps_SanchiGoldenMilk"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Ps_SanchiGoldenMilk"].ToString() + "</td>");
                    sb1.Append("</tr>");
                   // sb1.Append("<tr >");
                    sb1.Append("<td rowspan='3' >Closing Stock (Kgs)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cs_ClosingSMP"].ToString() + "</td>");
                       
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cs_ClosingSMP"].ToString() + "</td>");
                    sb1.Append("</tr>");

                    sb1.Append("<tr >");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cs_ClosingWhiteButter"].ToString() + "</td>");

                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cs_ClosingWhiteButter"].ToString() + "</td>");
                    sb1.Append("</tr>");

                    sb1.Append("<tr>");
                   
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cs_ClosingGhee"].ToString() + "</td>");

                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cs_ClosingGhee"].ToString() + "</td>");
                    sb1.Append("</tr>");

                    sb1.Append("<tr>");

                    sb1.Append("<td rowspan='4' >Commodity Used (Kgs)</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cu_CommoditySMPToday"].ToString() + "</td>");
                       
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cu_CommoditySMPToday"].ToString() + "</td>");
                    sb1.Append("</tr>");

                   // sb1.Append("<tr>");

                  
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cu_CommoditySMPCummulative"].ToString() + "</td>");

                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cu_CommoditySMPCummulative"].ToString() + "</td>");
                    sb1.Append("</tr>");
                  //  sb1.Append("<tr>");

                 
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cu_CommodityWhiteButterTo"].ToString() + "</td>");

                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cu_CommodityWhiteButterTo"].ToString() + "</td>");
                    sb1.Append("</tr>");
                   // sb1.Append("<tr>");

                  
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["Cu_CommodityWhiteButterCU"].ToString() + "</td>");

                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["Cu_CommodityWhiteButterCU"].ToString() + "</td>");
                    sb1.Append("</tr>");
                    
                    //sb1.Append("<tr>");
                    sb1.Append("<td>Milk Used for Indigenous Product</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        sb1.Append("<td>" + ds.Tables[0].Rows[i]["MilkUsedForIndigenousProduct"].ToString() + "</td>");
                    }
                    sb1.Append("<td>" + ds2.Tables[0].Rows[0]["MilkUsedForIndigenousProduct"].ToString() + "</td>");

                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    gridShowdata.InnerHtml = "";
                    gridShowdata.InnerHtml = sb1.ToString();


                    div_page_content.InnerHtml = sb1.ToString();
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;

                }
                else
                {
                    gridShowdata.InnerHtml = "";
                    gridShowdata.InnerHtml = "No Record Found";
                    
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}