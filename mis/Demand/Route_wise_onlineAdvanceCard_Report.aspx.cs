using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Route_wise_onlineAdvanceCard_Report : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds3, ds4 = new DataSet();
    DataTable dt = new DataTable();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetOfficeDetails();
            Fillroute();
            string Date = DateTime.Now.ToString("dd/MM/yyyy");
            txtMonth.Attributes.Add("readonly", "readonly");
            divdatavisible.Visible = false;
        }
    }

    private void Fillroute()
    {
        ds1 = objdb.ByProcedure("USP_Adv_Adv_Mst_Area",
                               new string[] { "flag", "Office_ID" },
                               new string[] { "1", objdb.Office_ID() }, "dataset");

        if (ds1 != null)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds1.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        else
        {
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null)
            {
                ds3.Dispose();
            }
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if(Page.IsValid)
            { 
            divdatavisible.Visible = false;

            lblMsg.Text = "";
          
                string mor = "";
                string eve = "";
                string fm = "01/" + txtMonth.Text;
                DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
                string FromDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime origDT = Convert.ToDateTime(FromDate);
                string MonthYear = Monthdate.ToString("MMMM-yyyy", CultureInfo.InvariantCulture);
                DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);
                string ToDate = lastDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds = objdb.ByProcedure("usp_Route_wise_onlineAdvanceCard_Report", new string[] { "flag", "RouteId", "FromDate", "ToDate", "Office_ID" }, new string[] { "1", ddlRoute.SelectedValue, FromDate, ToDate, objdb.Office_ID() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    divdatavisible.Visible = true;
                    dt = ds.Tables[0];
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table border='1' cellpadding='0' cellspacing='0' width='100%' style='text-align:center'>");
                    //sb.append("<tr>");
                    //sb.append("<td style='text-align:center' colspan='5'><b> " + viewstate["office_name"].tostring() + "</b></td>");
                    //sb.append("</tr>");
                    int colcount = 1;
                    for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                    {

                        if (dt.Columns[k].ToString() != "DEPO_AGENCY_PARLAR" && dt.Columns[k].ToString() != "tmp_shift" && dt.Columns[k].ToString() != "BoothId")
                        {
                            colcount++;
                        }



                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center' colspan='" + colcount + "'><b> " + ViewState["Office_Name"].ToString() + "</b> </td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center' colspan='" + colcount + "'><b>Route Wise Advance Card Report</b> </td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center' colspan='" + colcount + "'><b>Bill Month :</b> " + MonthYear + " </td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center' colspan='" + colcount + "'><b>Route :</b> " + ddlRoute.SelectedItem.ToString() + " </td>");

                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td><b>DEPO/AGENCY/PARLAR</b></td>");
                    string Col_Name = "";
                    for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                    {

                        if (dt.Columns[k].ToString() != "DEPO_AGENCY_PARLAR" && dt.Columns[k].ToString() != "tmp_shift" && dt.Columns[k].ToString() != "BoothId")
                        {
                            string ColName = dt.Columns[k].ToString();
                            string[] col = ColName.Split('_');
                            if (Col_Name != col[0])
                            {
                                sb.Append("<td colspan='2'><b>" + col[0] + "</b></td>");
                            }
                            Col_Name = col[0];
                        }



                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td></td>");

                    for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                    {

                        if (dt.Columns[k].ToString() != "DEPO_AGENCY_PARLAR" && dt.Columns[k].ToString() != "tmp_shift" && dt.Columns[k].ToString() != "BoothId")
                        {
                            string ColName = dt.Columns[k].ToString();
                            string[] col = ColName.Split('_');


                            sb.Append("<td ><b>" + col[1] + "</b></td>");


                        }



                    }
                    sb.Append("</tr>");
                    //int arrLength = Convert.ToInt32(ds.Tables[0].Columns.Count);
                    //int[] arrTotal = new int[arrLength];
                    //int arrCount = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToString(ds.Tables[0].Rows[i]["DEPO_AGENCY_PARLAR"]) + "</td>");
                        //arrCount = 0;
                        for (int s = 0; s < ds.Tables[0].Columns.Count; s++)
                        {

                            if (dt.Columns[s].ToString() != "DEPO_AGENCY_PARLAR" && dt.Columns[s].ToString() != "tmp_shift" && dt.Columns[s].ToString() != "ItemName" && dt.Columns[s].ToString() != "BoothId")
                            {

                                string ColName = dt.Columns[s].ToString();
                                mor = ds.Tables[0].Rows[i][ColName].ToString();
                                if (mor.ToString() == string.Empty)
                                {
                                    sb.Append("<td>0</td>");
                                    // mor = "0";
                                }
                                else
                                {
                                    sb.Append("<td>" + mor + "</td>");
                                }
                                //arrTotal[arrCount] = (arrTotal[s] + Convert.ToInt32(mor));
                                //arrCount++;
                            }
                        }
                        sb.Append("</tr>");

                    }

                    sb.Append("<tr>");
                    sb.Append("<td><b>Total</b></td>");
                    //for (int i = 0; i < arrCount; i++)
                    //{
                    //    sb.Append("<td><b>" + arrTotal[i].ToString() + "</b></td>");
                    //}
                    for (int m = 0; m < ds.Tables[0].Columns.Count; m++)
                    {

                        string ColName = ds.Tables[0].Columns[m].ToString();
                        if (ColName != "DEPO_AGENCY_PARLAR" && ColName != "tmp_shift" && ColName != "ItemName" && ColName != "BoothId")
                        {
                            string Grandtotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int?>(ColName)).ToString();
                            sb.Append("<td><b>" + Grandtotal + "</b></td>");
                        }

                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    div1.InnerHtml = sb.ToString();
                    printdiv.InnerHtml = sb.ToString();
                    grid();

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry! : ", "No Record Found.");

                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }

    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseDWMReport" + DateTime.Now + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        div1.RenderControl(htmlWrite);
        divtab2.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());

        Response.End();

    }
    private void grid()
    {
        try
        {
            string fm = "01/" + txtMonth.Text;
            DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string FromDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime origDT = Convert.ToDateTime(FromDate);
            DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);
            string ToDate = lastDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("usp_Route_wise_onlineAdvanceCard_Report", new string[] { "flag", "RouteId", "FromDate", "ToDate", "Office_ID" }, new string[] { "2", ddlRoute.SelectedValue, FromDate, ToDate, objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds.Tables[0].Rows.Count > 0)
            {

                DataTable dt2 = new DataTable();
                dt2 = ds1.Tables[0];

                divtab2.InnerHtml = summryDtl(dt2, txtMonth.Text, ddlRoute.SelectedItem.ToString());
                Sumprintdiv.InnerHtml = summryDtl(dt2, txtMonth.Text, ddlRoute.SelectedItem.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }

    }
    public string summryDtl(DataTable dts, string month_year, string strRoute)
    {
        StringBuilder ssb = new StringBuilder();
        ssb.Append("");
        ssb.Append("<table border=1 class='table1' style='width:50%; height:100%'>");
        //ssb.append("<tr>");
        //ssb.append("<td style='text-align:center' colspan='5'><b> " + viewstate["office_name"].tostring() + "</b></td>");
        //ssb.append("</tr>");
        //ssb.append("<tr>");
        //ssb.append("<td style='text-align:center' colspan='3'><b>route sales summary</b> </td>");
        //ssb.append("</tr>");
        //ssb.append("<tr>");
        //ssb.append("<td style='text-align:center' colspan='3'><b>bill month : " + month_year + "</b> </td>");
        //ssb.append("</tr>");
        //ssb.append("<tr>");
        //ssb.append("<td style='text-align:center' colspan='3'><b>route : " + strroute + "</b> </td>");
        //ssb.append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center'><b>Product</b></td>");
        //ssb.Append("<td style='text-align:center'><b>Packet Size</b></td>");
        //ssb.Append("<td style='text-align:center'><b>Shift</b></td>");
        //ssb.Append("<td style='text-align:center'><b>Qty</b></td>");
        ssb.Append("<td style='text-align:center'><b>Amount</b></td>");
        ssb.Append("</tr>");


        for (int i = 0; i < dts.Rows.Count; i++)
        {
            ssb.Append("<tr>");

            for (int j = 0; j < dts.Columns.Count; j++)
            {

                ssb.Append("<td style='text-align:center'>" + dts.Rows[i][j].ToString() + "</td>");
            }

            ssb.Append("</tr>");
        }
        Decimal TotalPrice = Convert.ToDecimal(dts.Compute("SUM(Amount)", string.Empty));
        //   Decimal Totalqty = Convert.ToDecimal(dts.Compute("SUM(ItemQty)", string.Empty));
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center'  ><b>Total</b></td>");
        //ssb.Append("<td style='text-align:center'><b>" + Totalqty + "</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + TotalPrice + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center'  ><b>Net Amount</b></td>");
        //ssb.Append("<td style='text-align:center'><b>" + Totalqty + "</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + TotalPrice + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center'  ><b>Total Deposit Amount</b></td>");
        //ssb.Append("<td style='text-align:center'><b>" + Totalqty + "</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + TotalPrice + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("</table>");
        return ssb.ToString();
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtMonth.Text = string.Empty;
        ddlRoute.ClearSelection();
        divdatavisible.Visible = false;
    }
}