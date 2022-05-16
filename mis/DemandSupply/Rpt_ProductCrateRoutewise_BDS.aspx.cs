using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_Rpt_ProductCrateRoutewise_BDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    int sum11 = 0, sum = 0, sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetRoute();
                GetOfficeDetails();
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
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {
                ddlRoute.Items.Clear();
                ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                         new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                         new string[] { "12", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
                ddlRoute.DataTextField = "DRName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("All", "0"));
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
        }
    }
    public static DateTime LastDayOfMonth(DateTime dt)
    {
        DateTime ss = new DateTime(dt.Year, dt.Month, 1);
        return ss.AddMonths(1).AddDays(-1);
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
            //  Code for current month date
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            // End of Current month date

          // previous monthdate for opeining balalce
             string prevmonthid = fmonth.AddMonths(-1).ToString("MM");
             int YearID = 0;
            if(prevmonthid=="12" )
            {
                 YearID = fmonth.Year-1;
            }
            else
            {
                YearID = fmonth.Year;
            }
           
            //string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string Previuosdate = "01/" + Convert.ToString(prevmonthid) + "/" + Convert.ToString(YearID);

            DateTime pmonth = DateTime.ParseExact(Previuosdate, "dd/MM/yyyy", culture);
            DateTime pmonth1 = LastDayOfMonth(pmonth);
            string PreMonthFdate = pmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string PreMonthTdate = pmonth1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            // end of  mprevious monthdate for opeining balalce

            ds2 = objdb.ByProcedure("USP_Trn_ProductCrateMgmt"
                , new string[] { "flag", "FromDate", "ToDate", "PreFDate", "PreTDate", "ItemCat_id",
                                 "AreaID","RouteId","Office_ID" }
                , new string[] { "5", fromnonth, tomonth, PreMonthFdate, PreMonthTdate, objdb.GetProductCatId()
                                   ,ddlLocation.SelectedValue,ddlRoute.SelectedValue, objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    //string[] disandroutename = ddlRoute.SelectedItem.Text.Split('-');

                    int Total_Issue = 0, Total_Return = 0, Total_SE = 0;
                   

                    int Count = ds2.Tables[0].Rows.Count;
                    sb.Append("<table class='table'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='5'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='1'><b>" + fmonth.ToString("MMM-yyyy") + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='4'><b>" + ddlRoute.SelectedItem.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Date</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Issue</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Return</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Short/Excess</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>ChallanNo</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    string Date = "";
                    for (int i = 0; i < Count; i++)
                    {

                        sb.Append("<tr>");
                        sb.Append("<td rowspan='1' style='border:1px solid black;text-align: center;padding-top:20px;'>" + ds2.Tables[0].Rows[i]["Date"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[1].Rows[i]["IssueCrate"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReturnCrate"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + (int.Parse(ds2.Tables[1].Rows[i]["IssueCrate"].ToString()) - int.Parse(ds2.Tables[0].Rows[i]["ReturnCrate"].ToString())) + "</td>");
                        //sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ShortExcessCrate"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ChallanNo"] + "</td>");
                        sb.Append("</tr>");
                        //urban
                        Total_Issue += Convert.ToInt32(ds2.Tables[1].Rows[i]["IssueCrate"]);
                        Total_Return += Convert.ToInt32(ds2.Tables[0].Rows[i]["ReturnCrate"]);
                       // Total_SE += Convert.ToInt32(ds2.Tables[0].Rows[i]["ShortExcessCrate"]);
                        Total_SE += Convert.ToInt32(int.Parse(ds2.Tables[1].Rows[i]["IssueCrate"].ToString()) - int.Parse(ds2.Tables[0].Rows[i]["ReturnCrate"].ToString()));


                        Date = ds2.Tables[0].Rows[i]["Date"].ToString();
                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='1'><b>Total</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Issue + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_Return + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SE + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: right;' colspan='3'><b>Opening Balance</b></td>");
                    //sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + ds2.Tables[2].Rows[0]["ShortExcessCrate_OB"].ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + (int.Parse(ds2.Tables[3].Rows[0]["IssueCrate_OB"].ToString()) - int.Parse(ds2.Tables[2].Rows[0]["ReturnCrate_OB"].ToString())) + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: right;' colspan='3'><b>Total Short Excess</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SE + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    div_page_content.InnerHtml = sb.ToString();
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "ProductCrate" + ddlRoute.SelectedItem.Text + DateTime.Now + ".xls");
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