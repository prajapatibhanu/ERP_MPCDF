using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Web.Services;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;


public partial class mis_Dashboard_AnalyticalDashboard : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
				 lblCDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               // Curr.Text = "09/09/2021";
                DateTime previousdate = DateTime.Today.AddDays(-1);
                Curr.Text = previousdate.ToString("dd/MM/yyyy");
				//Curr.Text = "16/02/2022";
				//Curr.Visible = false;
               // DateTime currentda = DateTime.Now;
                //Curr.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetInVentoryPOCount();
                GetAnalyticalCount();
               // GetMilkCollectionDetailsForPieChart();
                //GetMilkSaleDetailsForPieChart();
             
                
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }
    private void GetInVentoryPOCount()
    {
        string currentdate = Curr.Text;//"01/06/2021";
        string[] monthyear = currentdate.Split('/');
        //string[] monthyear = Curr.Text.Split('/');
        int cmonth=Convert.ToInt32(monthyear[1]);
        int cyear=Convert.ToInt32(monthyear[2]);

        string fm = "01" + "/" + monthyear[1] + "/" + monthyear[2];
        int lastdayofmonth = DateTime.DaysInMonth(cyear, cmonth);

        string tm = lastdayofmonth + "/" + monthyear[1] + "/" + monthyear[2];

        string URL = "http://45.114.143.126:8222/WebService.asmx/DSWise_POCount";
      // string URL = "http://182.70.253.156:8084/WebService.asmx/DSWise_POCount";
        URL = URL + "?key=SFA_MPCDFINV" + "&FromDate=" + fm + "&ToDate=" + tm;
        var request = (HttpWebRequest)WebRequest.Create(URL);

        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        string jsonString = string.Empty;

        using (var stream = response.GetResponseStream())
        {

            using (var sr = new StreamReader(stream))
            {

                jsonString = sr.ReadToEnd();

            }

        }

        DataTable DT = JsonStringToDataTable(jsonString);
        DataTable dtGrd = new DataTable();
        dtGrd.Columns.Add("POCount", typeof(string));
        int Count = DT.Rows.Count;
        for (int i = 0; i < Count; i++)
        {
            DataRow dr = DT.Rows[i];
            dtGrd.Rows.Add(dr["POCount"].ToString());
        }

        ViewState["GetPOCount"] = dtGrd;
        if (dtGrd != null) { dtGrd.Dispose(); }
    }

    private void GetInVentoryPOData()
    {
        string currentdate = Curr.Text; //"01/06/2021";
        string[] monthyear = currentdate.Split('/');
        //string[] monthyear = Curr.Text.Split('/');
        int cmonth = Convert.ToInt32(monthyear[1]);
        int cyear = Convert.ToInt32(monthyear[2]);

        string fm = "01" + "/" + monthyear[1] + "/" + monthyear[2];
        int lastdayofmonth = DateTime.DaysInMonth(cyear, cmonth);

        string tm = lastdayofmonth + "/" + monthyear[1] + "/" + monthyear[2];

     //   string URL = "http://182.70.253.156:8084/WebService.asmx/DSWise_POData";
        string URL = "http://45.114.143.126:8222/WebService.asmx/DSWise_POData";
        URL = URL + "?key=SFA_MPCDFINV" + "&FromDate=" + fm + "&ToDate=" + tm;
        var request = (HttpWebRequest)WebRequest.Create(URL);

        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        string jsonString = string.Empty;

        using (var stream = response.GetResponseStream())
        {

            using (var sr = new StreamReader(stream))
            {

                jsonString = sr.ReadToEnd();

            }
        }

        DataTable DT1 = JsonStringToDataTable(jsonString);
        DataTable dtGrd1 = new DataTable();
        dtGrd1.Columns.Add("Office_Name", typeof(string));
        dtGrd1.Columns.Add("POCount", typeof(int));
        int Count = DT1.Rows.Count;
        for (int i = 0; i < Count; i++)
        {
            DataRow dr = DT1.Rows[i];
            dtGrd1.Rows.Add(dr["Office_Name"].ToString(), dr["POCount"].ToString());
        }

        ViewState["GetPOData"] = dtGrd1;
        if (dtGrd1 != null) { dtGrd1.Dispose(); }
    }

    private void GetAnalyticalCount()
    {
        DataSet ds3 = new DataSet();
        try
        {
            DateTime currdat = DateTime.ParseExact(Curr.Text, "dd/MM/yyyy", culture);
            string cdate = currdat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds3 = objdb.ByProcedure("USP_AnalyticalDashboard",
                new string[] { "Flag", "CurrenDate" },
                new string[] { "1", cdate.ToString() },
                "dataset");
            if (ds3 != null)
            {
                lblPlantMilkSale.Text = ds3.Tables[0].Rows[0]["PlantMilkSale"].ToString();
                ViewState["PlantMilkSale"] = lblPlantMilkSale.Text;
                lblMilkCollectionInLtr.Text = ds3.Tables[1].Rows[0]["QtyInLtr"].ToString();
                ViewState["QtyInLtr"] = lblMilkCollectionInLtr.Text;
                lblSocietyBilling.Text = ds3.Tables[2].Rows[0]["NetAmount"].ToString();

                DataTable dt11 = new DataTable();
                dt11 = ds3.Tables[3];
                BindPieChartMilkCollection(dt11);
                dt11.Dispose();

                DataTable dt12 = new DataTable();
                dt12 = ds3.Tables[4];
                BindPieChartMilkSale(dt12);
                dt12.Dispose();


                DataTable dt13 = new DataTable();
                dt13 = ds3.Tables[5];
                BindPieChartSocietyBilling(dt13);
                dt13.Dispose();

             

                DataTable dt15 = new DataTable();
                dt15 = ds3.Tables[6];
                GetProfitData(dt15);
                dt15.Dispose();

                DataTable dt16 = new DataTable();
                dt16 = ds3.Tables[7];
                GetCFPData(dt16);
                dt16.Dispose();

                DataTable dt14 = new DataTable();
                dt14 = ds3.Tables[8];
                GetInflowOutflowDataForChart(dt14);
                dt14.Dispose();
               
            }

            DataTable dt1 = (DataTable)ViewState["GetPOCount"];
            if(dt1.Rows.Count>0)
            {
                lblInventoryPOCount.Text = dt1.Rows[0]["POCount"].ToString();
            }
            if (dt1 != null) { dt1.Dispose(); }
            ViewState["GetPOCount"] = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    protected void lnkMilkCollection_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet d1 = new DataSet();
            d1 = null;
            DateTime currdat = DateTime.ParseExact(Curr.Text, "dd/MM/yyyy", culture);
            string cdate = currdat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            d1 = objdb.ByProcedure("USP_AnalyticalDashboard",
               new string[] { "Flag", "CurrenDate" },
               new string[] { "3", cdate.ToString() },
               "dataset");

            if (d1.Tables.Count > 0 && d1.Tables[0].Rows.Count > 0)
            {
                GVMilkCollection.DataSource = d1;
                GVMilkCollection.DataBind();
                Label lbltotalcount = (GVMilkCollection.FooterRow.FindControl("lbltotalcount") as Label);
                decimal total = d1.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("MC_QtyInLtr") ?? 0);
                lbltotalcount.Text = total.ToString("0.00");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MilkCollection()", true);

                if (d1 != null) { d1.Dispose(); }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
    }
   
    protected void lnkPlantMilkSale_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet d1 = new DataSet();
            d1 = null;
            DateTime currdat = DateTime.ParseExact(Curr.Text, "dd/MM/yyyy", culture);
            string cdate = currdat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            d1 = objdb.ByProcedure("USP_AnalyticalDashboard",
               new string[] { "Flag", "CurrenDate" },
               new string[] { "2", cdate.ToString() },
               "dataset");

            if (d1.Tables.Count > 0 && d1.Tables[0].Rows.Count > 0)
            {
                GvPlantMilkSale.DataSource = d1;
                GvPlantMilkSale.DataBind();
                Label lbltotalcount = (GvPlantMilkSale.FooterRow.FindControl("lbltotalcount") as Label);
                decimal total = d1.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PlantMilkSale") ?? 0);
                lbltotalcount.Text = total.ToString("0.00");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "PlantMilkSale()", true);

                if (d1 != null) { d1.Dispose(); }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
    }
    protected void lnkSocietyBilling_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet d1 = new DataSet();
            d1 = null;          
            d1 = objdb.ByProcedure("USP_AnalyticalDashboard",
               new string[] { "Flag" },
               new string[] { "4" },
               "dataset");

            if (d1.Tables.Count > 0 && d1.Tables[0].Rows.Count > 0)
            {
                GVSocietyBilling.DataSource = d1;
                GVSocietyBilling.DataBind();
                Label lblSocietyCount = (GVSocietyBilling.FooterRow.FindControl("lblSocietyCount") as Label);
                int scount = d1.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("SocietyCount") ?? 0);
                lblSocietyCount.Text = Convert.ToString(scount);
                Label lblTotalNetAmount = (GVSocietyBilling.FooterRow.FindControl("lblTotalNetAmount") as Label);
                decimal NetAmount = d1.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("NetAmount") ?? 0);
                lblTotalNetAmount.Text = NetAmount.ToString("0.00");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SocietyBillingData()", true);

                if (d1 != null) { d1.Dispose(); }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
    }
    protected void lnkPOOfficeWise_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            GetInVentoryPOData();
            DataTable dt1 = (DataTable)ViewState["GetPOData"];
            if (dt1.Rows.Count > 0)
            {
                GVInventoryPOData.DataSource = dt1;
                GVInventoryPOData.DataBind();
                Label lbltotalcount = (GVInventoryPOData.FooterRow.FindControl("lbltotalcount") as Label);
                int total = dt1.AsEnumerable().Sum(r => r.Field<int?>("POCount") ?? 0);  
                lbltotalcount.Text = Convert.ToString(total);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "InventoryPOData()", true);

                if (dt1 != null) { dt1.Dispose(); }
                ViewState["GetPOData"] = "";
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
    }

    //protected void lnkProducerPayment_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataSet d1 = new DataSet();
    //        d1 = null;

    //        d1 = objdb.ByProcedure("USP_AnalyticalDashboard",
    //           new string[] { "Flag" },
    //           new string[] { "5" },
    //           "dataset");

    //        if (d1.Tables.Count > 0 && d1.Tables[0].Rows.Count > 0)
    //        {
    //            GVProducerPayment.DataSource = d1;
    //            GVProducerPayment.DataBind();
    //            Label lblProducerCount = (GVProducerPayment.FooterRow.FindControl("lblProducerCount") as Label);
    //            int pcount = d1.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("ProducerCount") ?? 0);
    //            lblProducerCount.Text = Convert.ToString(pcount);
    //            Label lblTotalNetAmount = (GVProducerPayment.FooterRow.FindControl("lblTotalNetAmount") as Label);
    //            decimal NetAmount = d1.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("NetAmount") ?? 0);
    //            lblTotalNetAmount.Text = NetAmount.ToString("0.00");
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ProducerPaymentData()", true);

    //            if (d1 != null) { d1.Dispose(); }
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Producer Payment: ", ex.Message.ToString());
    //    }
    //}

    //private void GetMilkCollectionDetailsForPieChart()
    //{
    //    DataTable dt1 = new DataTable();
    //    DataSet d1 = new DataSet();
    //    d1 = null;
    //    DateTime currdat = DateTime.ParseExact(Curr.Text, "dd/MM/yyyy", culture);
    //    string cdate = currdat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //    d1 = objdb.ByProcedure("USP_AnalyticalDashboard",
    //       new string[] { "Flag", "CurrenDate" },
    //       new string[] { "3", cdate.ToString() },
    //       "dataset");

        
    //}
    //private void GetMilkSaleDetailsForPieChart()
    //{
    //    DataTable dt1 = new DataTable();
    //    DataSet d1 = new DataSet();
    //    d1 = null;
    //    DateTime currdat = DateTime.ParseExact(Curr.Text, "dd/MM/yyyy", culture);
    //    string cdate = currdat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //    d1 = objdb.ByProcedure("USP_AnalyticalDashboard",
    //           new string[] { "Flag", "CurrenDate" },
    //           new string[] { "2", cdate.ToString() },
    //           "dataset");

    //    dt1 = d1.Tables[0];
    //    BindPieChartMilkSale(dt1);
    //}

    protected void BindPieChartMilkCollection(DataTable dt1)
    {
        try
        {

            if (dt1.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.Append("<script>");
                html.Append("Highcharts.chart('divTruckDetentionData', {");
                html.Append("chart: {");
                html.Append("plotBackgroundColor: null,");
                html.Append("plotBorderWidth: null,");
                html.Append("plotShadow: false,");
                html.Append("type: 'pie'");
                html.Append("},");
                html.Append("title: {");
                html.Append("text: '<b>Milk Collection (In KG)</b> -  " + ViewState["QtyInLtr"].ToString() + "'");
                html.Append("},");
                html.Append("tooltip: {");
                html.Append("pointFormat: '{series.name}: <br><b>{point.percentage:.1f}%</b><br>Ltr : {point.y}'");
                html.Append("},");
                html.Append("credits: {");
                html.Append("enabled: false");
                html.Append("},");
                html.Append("accessibility: {");
                html.Append("point: {");
                html.Append("valueSuffix: '%'");
                html.Append("}");
                html.Append("},");
                html.Append("plotOptions: {");
                html.Append("pie: {");
                html.Append("allowPointSelect: true,");
                html.Append("cursor: 'pointer',");
                html.Append("dataLabels: {");
                html.Append("enabled: true,");
                html.Append("format: '<b>{point.name}</b>: <br>{point.percentage:.1f} %<br>KG : {point.y}',");
                html.Append("},");
                html.Append("showInLegend: true");
                html.Append("}");
                html.Append("},");
                html.Append("series: [{");
                html.Append("name: 'Per',");
                html.Append("colorByPoint: true,");
                html.Append("data: [");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    html.Append("{name: '" + dt1.Rows[i]["Office_Code"].ToString() + "',");
                    if (i != dt1.Rows.Count - 1)
                    {
                        html.Append("y: " + dt1.Rows[i]["MC_QtyInLtr"].ToString() + "},");
                    }
                    else
                    {
                        html.Append("y: " + dt1.Rows[i]["MC_QtyInLtr"].ToString() + "}");
                    }

                }
                html.Append("]");
                html.Append("}]");
                html.Append("});");
                html.Append("</script>");

                divTruckDetentionData1.InnerHtml = html.ToString();
                div_table2.Visible = true;
            }
            else
            {
                divTruckDetentionData1.InnerHtml = "";
                div_table2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1 : ", ex.Message.ToString());
        }
    }

    protected void BindPieChartMilkSale(DataTable dt1)
    {
        try
        {

            if (dt1.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.Append("<script>");
                html.Append("Highcharts.chart('divMilkSale', {");
                html.Append("chart: {");
                html.Append("plotBackgroundColor: null,");
                html.Append("plotBorderWidth: null,");
                html.Append("plotShadow: false,");
                html.Append("type: 'pie'");
                html.Append("},");
                html.Append("title: {");
                html.Append("text: '<b>Milk Sale (In Ltr)</b>  -  " + ViewState["PlantMilkSale"].ToString() + "'");
                html.Append("},");
                html.Append("tooltip: {");
                html.Append("pointFormat: '{series.name}: <br><b>{point.percentage:.1f}%</b><br>Ltr : {point.y}'");
                html.Append("},");
                html.Append("credits: {");
                html.Append("enabled: false");
                html.Append("},");
                html.Append("accessibility: {");
                html.Append("point: {");
                html.Append("valueSuffix: '%'");
                html.Append("}");
                html.Append("},");
                html.Append("plotOptions: {");
                html.Append("pie: {");
                html.Append("allowPointSelect: true,");
                html.Append("cursor: 'pointer',");
                html.Append("dataLabels: {");
                html.Append("enabled: true,");
                html.Append("format: '<b>{point.name}</b>: <br>{point.percentage:.1f} %<br>Ltr : {point.y}',");
                html.Append("},");
                html.Append("showInLegend: true");
                html.Append("}");
                html.Append("},");
                html.Append("series: [{");
                html.Append("name: 'Per',");
                html.Append("colorByPoint: true,");
                html.Append("data: [");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    html.Append("{name: '" + dt1.Rows[i]["Office_Code"].ToString() + "',");
                    if (i != dt1.Rows.Count - 1)
                    {
                        html.Append("y: " + dt1.Rows[i]["PlantMilkSale"].ToString() + "},");
                    }
                    else
                    {
                        html.Append("y: " + dt1.Rows[i]["PlantMilkSale"].ToString() + "}");
                    }

                }
                html.Append("]");
                html.Append("}]");
                html.Append("});");
                html.Append("</script>");

                divMilkSale1.InnerHtml = html.ToString();
                div_table3.Visible = true;
            }
            else
            {
                divMilkSale1.InnerHtml = "";
                div_table3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1 : ", ex.Message.ToString());
        }
    }

    protected void BindPieChartSocietyBilling(DataTable dt1)
    {
        try
        {

            if (dt1.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.Append("<script>");
                html.Append("Highcharts.chart('divSC', {");
                html.Append("chart: {");
                html.Append("plotBackgroundColor: null,");
                html.Append("plotBorderWidth: null,");
                html.Append("plotShadow: false,");
                html.Append("type: 'pie'");
                html.Append("},");
                html.Append("title: {");
                html.Append("text: '<b>Society Billing (In Rs)</b>'");
                html.Append("},");
                html.Append("tooltip: {");
                html.Append("pointFormat: '{series.name}: <br><b>{point.percentage:.1f}%</b><br>Rs : {point.y}'");
                html.Append("},");
                html.Append("credits: {");
                html.Append("enabled: false");
                html.Append("},");
                html.Append("accessibility: {");
                html.Append("point: {");
                html.Append("valueSuffix: '%'");
                html.Append("}");
                html.Append("},");
                html.Append("plotOptions: {");
                html.Append("pie: {");
                html.Append("allowPointSelect: true,");
                html.Append("cursor: 'pointer',");
                html.Append("dataLabels: {");
                html.Append("enabled: true,");
                html.Append("format: '<b>{point.name}</b>: <br>{point.percentage:.1f} %<br>Rs : {point.y}',");
                html.Append("},");
                html.Append("showInLegend: true");
                html.Append("}");
                html.Append("},");
                html.Append("series: [{");
                html.Append("name: 'Per',");
                html.Append("colorByPoint: true,");
                html.Append("data: [");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    html.Append("{name: '" + dt1.Rows[i]["Office_Code"].ToString() + "',");
                    if (i != dt1.Rows.Count - 1)
                    {
                        html.Append("y: " + dt1.Rows[i]["NetAmount"].ToString() + "},");
                    }
                    else
                    {
                        html.Append("y: " + dt1.Rows[i]["NetAmount"].ToString() + "}");
                    }

                }
                html.Append("]");
                html.Append("}]");
                html.Append("});");
                html.Append("</script>");

                divSC1.InnerHtml = html.ToString();
                div_table4.Visible = true;
            }
            else
            {
                divSC1.InnerHtml = "";
                div_table4.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1 : ", ex.Message.ToString());
        }
    }

    //private void GetFatSNFData()
    //{
    //            StringBuilder html = new StringBuilder();
    //            html.Append("<script>");
    //            html.Append("<script>");
    //            html.Append("</script>");
    //}

    private void GetInflowOutflowDataForChart(DataTable dt1)
    {
        if (dt1.Rows.Count > 0)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<script>");
            html.Append("Highcharts.chart('divie', {");
            html.Append("chart: {");
            html.Append("type: 'bar'");
            html.Append("},");
            html.Append("title: {");
            html.Append("text: '<b>Milk Processing (In KG)</b>'");
            html.Append("},");
            html.Append("xAxis: {");
            html.Append("categories: [");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != dt1.Rows.Count - 1)
                {
                    html.Append("'" + dt1.Rows[i]["Office_Code"].ToString() + "',");
                }
                else
                {
                    html.Append("'" + dt1.Rows[i]["Office_Code"].ToString() + "'");
                }
            }
            html.Append("],");
            html.Append("title: {text: null }  },");
            html.Append("yAxis: { min: 0, title: {  text: '',align: 'high'}, labels: { overflow: 'justify'} },");
            html.Append("tooltip: {valueSuffix: ' KG' }, plotOptions: { bar: { dataLabels: { enabled: true  } } },");
            html.Append("legend: { layout: 'vertical', align: 'right', verticalAlign: 'top',  x: -40, y: 80, floating: true, borderWidth: 1, backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF', shadow: true },");
            html.Append("credits: { enabled: false},");
            html.Append("series: [");
            //html.Append("{name: 'Inflow FAT (In KG)', data: [50201.24,7691.38,28430.14,3091.14,0,9688.82]},");
            //html.Append("{name: 'Inflow SNF (In KG)', data: [88083.82,31263.38,55054.04,5840.57,0,15488.99]},");
            //html.Append("{name: 'OutFlow FAT (In KG)', data: [45366.27,6607.01,28382.84,3102.26,0,9675.27]},");
            //html.Append("{name: 'OutFlow SNF (In KG)', data: [86524.69,7026.35,54737.1,5804.87,0,15468.01]");
            html.Append("{name: 'Inflow FAT (In KG)', data: [");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != dt1.Rows.Count - 1)
                {
                    html.Append("" + dt1.Rows[i]["InFlowKgFat"].ToString() + ",");
                }
                else
                {
                    html.Append("" + dt1.Rows[i]["InFlowKgFat"].ToString() + "]},");
                }


            }
            html.Append("{name: 'Inflow SNF (In KG)', data: [");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != dt1.Rows.Count - 1)
                {
                    html.Append("" + dt1.Rows[i]["InFlowKgSnf"].ToString() + ",");
                }
                else
                {
                    html.Append("" + dt1.Rows[i]["InFlowKgSnf"].ToString() + "]},");
                }


            }
            html.Append("{name: 'OutFlow FAT (In KG)', data: [");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != dt1.Rows.Count - 1)
                {
                    html.Append("" + dt1.Rows[i]["OutFlowKgFat"].ToString() + ",");
                }
                else
                {
                    html.Append("" + dt1.Rows[i]["OutFlowKgFat"].ToString() + "]},");
                }


            }
            html.Append("{name: 'OutFlow SNF (In KG)', data: [");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != dt1.Rows.Count - 1)
                {
                    html.Append("" + dt1.Rows[i]["OutFlowKgSnf"].ToString() + ",");
                }
                else
                {
                    html.Append("" + dt1.Rows[i]["OutFlowKgSnf"].ToString() + "]");
                }


            }
            //html.Append("] },");
            //html.Append("{ name: 'Expenses', data: [");
            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    if (i != dt1.Rows.Count - 1)
            //    {
            //        html.Append("" + dt1.Rows[i]["Expenses"].ToString() + ",");
            //    }
            //    else
            //    {
            //        html.Append("" + dt1.Rows[i]["Expenses"].ToString() + "");
            //    }


            //}
            //html.Append("]");
            html.Append("}]");
            html.Append("});");
            html.Append("</script>");
            div2.InnerHtml = html.ToString();
            div_table5.Visible = true;
        }
        else
        {
            div_table5.Visible = false;
            div2.InnerHtml = "";

        }
    }

    private void GetProfitData(DataTable dt1)
    {
        StringBuilder html = new StringBuilder();
        html.Append("<script>");
        html.Append("Highcharts.chart('divprofit', {");
        html.Append("title: { text: 'Sales Turnover (Rs. Crore)'  },");
        html.Append("subtitle: {text: '' },");
        html.Append("yAxis: { title: { text: 'Sales Turnover' } },");
       html.Append("xAxis: { accessibility: { rangeDescription: 'Range: 2017 to 2021'");
       html.Append("} },");
       html.Append(" legend: { layout: 'vertical',  align: 'right', verticalAlign: 'middle'  },");
      html.Append("plotOptions: { series: { label: { connectorAllowed: false  }, pointStart: 2017   }  },");
      html.Append("credits: { enabled: false},");
        html.Append("series: [{");
      for (int i = 0; i < dt1.Rows.Count; i++)
      {
          if (i==0)
          {
              html.Append("name: '" + dt1.Rows[i]["Office_Code"].ToString() + "',");
          }
          else
          {
              html.Append("{name: '" + dt1.Rows[i]["Office_Code"].ToString() + "',");
          }
          
          if (i != dt1.Rows.Count - 1)
          {
              html.Append("data: [" + dt1.Rows[i]["DSWiseData"].ToString() + "] },");
          }
          else
          {
              html.Append("data: [" + dt1.Rows[i]["DSWiseData"].ToString() + "]");
          }

      }
      //html.Append("name: 'Installation', data: [43934, 52503, 57177, 69658, 97031, 119931, 137133, 154175] },");
      //html.Append("{ name: 'Manufacturing', data: [24916, 24064, 29742, 29851, 32490, 30282, 38121, 40434] },");
      //html.Append("{ name: 'Sales & Distribution', data: [11744, 17722, 16005, 19771, 20185, 24377, 32147, 39387] },");
      // html.Append("{   name: 'Project Development', data: [null, null, 7988, 12169, 15112, 22452, 34400, 34227] },");
      // html.Append("{ name: 'Other',  data: [12908, 5948, 8105, 11248, 8989, 11816, 18274, 18111]");
     html.Append("}],");
    html.Append("responsive: { rules: [{ condition: {  maxWidth: 500  }, chartOptions: {");
      html.Append(" legend: {layout: 'horizontal', align: 'center', verticalAlign: 'bottom'  } }");
       html.Append("}] }});");
        html.Append("</script>");
        div3.InnerHtml = html.ToString();
        div_table6.Visible = true;
    }

    private void GetCFPData(DataTable dt1)
    {
        StringBuilder html = new StringBuilder();

        if (dt1.Rows.Count>0)
        {
            html.Append("<div id='columnchart_material' style='width: 500px; height: 400px;'></div>");
            html.Append("<script type='text/javascript'>");
            html.Append("google.charts.load('current', { 'packages': ['bar'] });");
            html.Append("google.charts.setOnLoadCallback(drawChart);");
            html.Append("function drawChart() {");
            html.Append("var data = google.visualization.arrayToDataTable([");
            html.Append(" ['', 'Production', 'Sales'],");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != dt1.Rows.Count - 1)
                {
                    html.Append("['" + dt1.Rows[i]["Office_Name"].ToString() + "'," + dt1.Rows[i]["ProductionCumulativeQuantity"].ToString() + "," + dt1.Rows[i]["SaleCumulativeQuantity"].ToString() + "],");
                }
                else
                {
                    html.Append("['" + dt1.Rows[i]["Office_Name"].ToString() + "'," + dt1.Rows[i]["ProductionCumulativeQuantity"].ToString() + "," + dt1.Rows[i]["SaleCumulativeQuantity"].ToString() + "]");
                }
            }
            html.Append("]);");
            html.Append("var options = {");
            html.Append("chart: {");
            html.Append("title: 'Cattle Feed Plant (In MT)',");
            html.Append(" }  };");
            html.Append("var chart = new google.charts.Bar(document.getElementById('columnchart_material'));");
            html.Append("chart.draw(data, google.charts.Bar.convertOptions(options));");
            html.Append("} </script>");
            divCFP1.InnerHtml = html.ToString();
            
        }
        else
        {
            divCFP1.InnerHtml = "";
        }
       
    }
}