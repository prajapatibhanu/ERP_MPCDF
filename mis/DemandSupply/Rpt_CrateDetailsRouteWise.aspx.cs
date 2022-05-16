using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_Rpt_CrateDetailsRouteWise : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    int sum11=0,sum = 0, sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetOfficeDetails();
               // GetItemCategory();
                txtMonth.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
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
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    //protected void GetItemCategory()
    //{
    //    try
    //    {
    //        ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
    //              new string[] { "flag" },
    //             new string[] { "1" }, "dataset");

    //        if (ds1.Tables[0].Rows.Count != 0)
    //        {
    //            ddlItemCategory.DataTextField = "ItemCatName";
    //            ddlItemCategory.DataValueField = "ItemCat_id";
    //            ddlItemCategory.DataSource = ds1.Tables[0];
    //            ddlItemCategory.DataBind();
    //            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    private void GetRoute()
    {
        try
        {
            if (ddlLocation.SelectedValue != "0")
            {
                ddlRoute.Items.Clear();
                ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                         new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                         new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtMonth.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        lblMsg.Text = "";
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        ddlRoute.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateReport();
           // GenerateReportFooter();
        }
    }

    protected void GenerateReport()
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            div_page_content.InnerHtml = "";
            string fm = "01/" + txtMonth.Text;
            DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string MDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            // ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt"
                // , new string[] { "flag", "FromDate", "RouteId", "Office_ID" }
                // , new string[] { "5", MDate, ddlRoute.SelectedValue, objdb.Office_ID(), }, "dataset");
				ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt_BDS_urben"
                , new string[] { "flag", "FromDate", "RouteId", "Office_ID" }
                , new string[] { "5", MDate, ddlRoute.SelectedValue, objdb.Office_ID(), }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table class='table1' style='width:100%'>");
                    int Count = ds2.Tables[0].Rows.Count;
                    int ColCount = ds2.Tables[0].Columns.Count;
                    int Count1 = ds2.Tables[0].Rows.Count;
                    int ColCount1 = ds2.Tables[0].Columns.Count;
                    sb.Append("<td style='padding: 2px 5px;text-align: left;'><b>" + ddlRoute.SelectedItem.Text + "</b></td>");
                    sb.Append("<td colspan='" + (ColCount + ColCount1 - 1) + "' style='text-align:center;'><b>[ " + ViewState["Office_Name"].ToString() + " ] " + "</b> </td>");
                    sb.Append("<tr >");


                    sb.Append("<td rowspan='2' style='text-align:center'>Month</td>");
                    sb.Append("<td colspan='2' style='text-align:center'>Mor</td>");
                    sb.Append("<td colspan='2' style='text-align:center'>Eve</td>");
                    sb.Append("<td style='text-align:center'>Mor</td>");
                    sb.Append("<td style='text-align:center'>Eve</td>");
                    sb.Append("<td style='text-align:center'>M+E</td>");
                    sb.Append("<td style='text-align:center'>Mor</td>");
                    sb.Append("<td style='text-align:center'>Eve</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td style='text-align:center'>Issue</td>");
                    sb.Append("<td style='text-align:center'>Return</td>");
                    sb.Append("<td style='text-align:center'>Issue</td>");
                    sb.Append("<td style='text-align:center'>Return</td>");
                    sb.Append("<td style='text-align:center' colspan='3'>Short/Excess</td>");

                    sb.Append("<td style='text-align:center'>CH.NO.</td>");
                    sb.Append("<td style='text-align:center'>CH.NO.</td>");
                    sb.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {

                        sb.Append("<tr>");
                        if (i == 0)
                        {
                            sb.Append("<td style='border-top:1px dashed black !important; text-align:center'>" + ds2.Tables[0].Rows[i]["DATE"].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='text-align:center'>" + ds2.Tables[0].Rows[i]["DATE"].ToString() + "</td>");
                        }
                        for (int K = 0; K < ColCount; K++)
                        {


                            if (ds2.Tables[0].Columns[K].ToString() != "DATE" && ds2.Tables[0].Columns[K].ToString() != "ShiftId" && ds2.Tables[0].Columns[K].ToString() != "Shift_id")
                            {
                                string ColName = ds2.Tables[0].Columns[K].ToString();
                                if (i == 0)
                                {

                                    sb.Append("<td style='border-top:1px dashed black !important;text-align:center'>" + ds2.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td style='text-align:center'>" + ds2.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                }

                            }

                        }
                        sb.Append("</tr>");

                    }

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center'><b>Total</b></td>");

                    DataTable dt = new DataTable();
                    dt = ds2.Tables[0];
                    int sum11 = 0;
                        if (dt.ToString() != "DATE" && dt.ToString() != "ShiftId" && dt.ToString() != "Shift_id")
                        {

                           // sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                            sum11 = dt.AsEnumerable().Sum(row => row.Field<int>("Mor_MIssue"));
                            sum = dt.AsEnumerable().Sum(row => row.Field<int>("Mor_MReturn"));
                            sum1 = dt.AsEnumerable().Sum(row => row.Field<int>("Eve_EIssue"));
                            sum2 = dt.AsEnumerable().Sum(row => row.Field<int>("Eve_EReturn"));
                            sum3 = dt.AsEnumerable().Sum(row => row.Field<int>("MShortExcess"));
                            sum4 = dt.AsEnumerable().Sum(row => row.Field<int>("EShortExcess"));
                            sum5 = dt.AsEnumerable().Sum(row => row.Field<int>("ME"));
                            int sum6 = 0;
                            int sum7 = 0;
                            ViewState["ShortExcessCrate"] = sum5;
                           sb.Append("<td style='text-align:center;'><b>" + sum11.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum1.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum2.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum3.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum4.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum5.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum6.ToString() + "</b></td>");
                           sb.Append("<td style='text-align:center;'><b>" + sum7.ToString() + "</b></td>");

                        }
                    sb.Append("</tr>");
                   // string fm = "01/" + txtMonth.Text;
                    // DateTime current = txtMonth.Text
                    //  moment.Month;
                    DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
                    string monthid = fmonth.AddMonths(-1).ToString("MM");
                    int YearID = fmonth.Year;
                    //string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string Previuosdate = "01/" + Convert.ToString(monthid) + "/" + Convert.ToString(YearID);

                    DateTime pmonth = DateTime.ParseExact(Previuosdate, "dd/MM/yyyy", culture);
                    DateTime pmonth1 = LastDayOfMonth(pmonth);
                    string fromdate = pmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string todate = pmonth1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                    // ds3 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt"
                        // , new string[] { "Flag", "Fromdate", "Todate", "RouteId", "Office_ID" }
                        // , new string[] { "6", fromdate, todate, ddlRoute.SelectedValue, objdb.Office_ID(), }, "dataset");
						ds3 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt_BDS_urben"
                        , new string[] { "Flag", "Fromdate", "Todate", "RouteId", "Office_ID" }
                        , new string[] { "6", fromdate, todate, ddlRoute.SelectedValue, objdb.Office_ID(), }, "dataset");
                    if (ds3 != null && ds3.Tables.Count > 0)
                    {
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                           // StringBuilder sb = new StringBuilder();
                            string total = "";
                            string primonth = ds3.Tables[0].Rows[0]["ShortExcessCrate"].ToString();
                            string currnt = ViewState["ShortExcessCrate"].ToString();
                            total = Convert.ToString(Convert.ToInt32(primonth) + Convert.ToInt32(currnt));
                            // total = primonth + currnt;

                            sb.Append("<tr>");
                            sb.Append("<td style='text-align:center;'><b>Grand Total</b></td>");
                            sb.Append("<td style='text-align:center;' colspan='2'><b>Issue : " + (sum11 + sum1) + "</b></td>");
                            sb.Append("<td style='text-align:center;' colspan='2'><b>Return : " + (sum + sum2) + "</b></td>");
                            sb.Append("<td style='text-align:center;' colspan='3'><b>Short/Excess : " + (sum5) + "</b></td>");
                            sb.Append("<td style='text-align:center;'></td>");
                            sb.Append("<td style='text-align:center;'></td>");
                            //sb.Append("<td style='text-align:center;'></td>");
                            //sb.Append("<td style='text-align:center;'></td>");
                            //sb.Append("<td style='text-align:center;'></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr><td style='text-align:center;' colspan='7'><b>Opening Balance</b></td><td style='text-align:left;' colspan='3'><b>" + primonth + "</b></td></tr>");
                            sb.Append("<tr><td style='text-align:center;' colspan='7'><b>Total Short/Excess Crate</b></td><td style='text-align:left;' colspan='3'><b>" + total + "</b></td></tr>");
                           
                        }
                    }
                    sb.Append("</table>");


                    ////////////////End Of Route Wise Print Code   ///////////////////////
                  
                    div_page_content.InnerHtml = sb.ToString();
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                }
				else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Info! : ", "No Record Found");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }

    //protected void GenerateReportFooter()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        div_Footer.InnerHtml = "";


    //        lblMsg.Text = "";
    //        string fm = "01/" + txtMonth.Text;
    //        // DateTime current = txtMonth.Text
    //        //  moment.Month;
    //        DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
    //        string monthid = fmonth.AddMonths(-1).ToString("MM");
    //        int YearID = fmonth.Year;
    //        //string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        string Previuosdate = "01/" + Convert.ToString(monthid) + "/" + Convert.ToString(YearID);

    //        DateTime pmonth = DateTime.ParseExact(Previuosdate, "dd/MM/yyyy", culture);
    //        DateTime pmonth1 = LastDayOfMonth(pmonth);
    //        string fromdate = pmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        string todate = pmonth1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


    //        ds3 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt"
    //            , new string[] { "Flag", "Fromdate", "Todate", "RouteId", "Office_ID" }
    //            , new string[] { "6", fromdate, todate, ddlRoute.SelectedValue, objdb.Office_ID(), }, "dataset");
    //        if (ds3 != null && ds3.Tables.Count > 0)
    //        {
    //            if (ds3.Tables[0].Rows.Count > 0)
    //            {
    //                StringBuilder sb = new StringBuilder();
    //                string total = "";
    //                string primonth = ds3.Tables[0].Rows[0]["ShortExcessCrate"].ToString();
    //                string currnt = ViewState["ShortExcessCrate"].ToString();
    //                total = Convert.ToString(Convert.ToInt32(primonth) + Convert.ToInt32(currnt));
    //               // total = primonth + currnt;
    //                sb.Append("<table class='table1' style='width:100%'>");

    //                sb.Append("<tr><td style='text-align:center;'>Opening Balance</td><td colspan='9' >" + primonth + "</td></tr>");
    //                sb.Append("<tr><td style='text-align:center;'>Total Short/Excess Crate</td><td colspan='9'>" + total + "</td></tr>");
    //                sb.Append("</table>");
    //                div_Footer.InnerHtml = sb.ToString();
    //            }
    //        }
            
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds3 != null) { ds3.Dispose(); }
    //    }
    //}
    public static DateTime LastDayOfMonth(DateTime dt)
    {
        DateTime ss = new DateTime(dt.Year, dt.Month, 1);
        return ss.AddMonths(1).AddDays(-1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "MonthlyCrateReport" + ddlRoute.SelectedItem.Text +DateTime.Now + ".xls");
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

}