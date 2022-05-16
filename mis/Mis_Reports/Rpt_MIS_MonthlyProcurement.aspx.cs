using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Mis_Reports_Rpt_MIS_MonthlyProcurement : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {

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



    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtMonth.Text = string.Empty;
        lblMsg.Text = "";
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateReport();
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

            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_Manage_MIS_Data"
                , new string[] { "flag", "FromDate", "ToDate" }
                , new string[] { "4", fromnonth, tomonth }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                btnPrint.Visible = true;
                btnExcel.Visible = true;
                DataTable dt3 = new DataTable();
                dt3 = ds2.Tables[0];
                int Count = dt3.Rows.Count;
                int ColCount = dt3.Columns.Count;
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td colspan='" + (ColCount) + "' style='text-align: center;border:1px solid black;'><b>PROCUREMENT (" + fmonth.ToString("MMM-yyyy") + ") </b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                for (int j = 0; j < ColCount; j++)
                {

                    string ColName = dt3.Columns[j].ToString();
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                }
                sb1.Append("</tr>");
                for (int i = 0; i < Count; i++)
                {
                    sb1.Append("<tr>");
                    for (int K = 0; K < ColCount; K++)
                    {

                        string ColName = dt3.Columns[K].ToString();

                        sb1.Append("<td style='border: 1px solid #000000 !important;'>" + dt3.Rows[i][ColName].ToString() + "</td>");


                    }
                    sb1.Append("</tr>");
                }

                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>SWA</b></td>");
                decimal sum13 = 0, tmpbdsdata=0,tmpgdsdata=0,tmpidsdata=0,tmpjdsdata=0,tmpudsdata=0,tmpbkdsdata=0,tmptotaldata=0;
                int dd = Count - 7;
                if (Count>7)
                {
                    for (int i = dd; i < Count; i++)
                    {
                        for (int K = 0; K < ColCount; K++)
                        {
                            if (dt3.Columns[K].ToString() == "BDS")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmpbdsdata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                            if (dt3.Columns[K].ToString() == "GDS")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmpgdsdata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                            if (dt3.Columns[K].ToString() == "IDS")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmpidsdata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                            if (dt3.Columns[K].ToString() == "JDS")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmpjdsdata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                            if (dt3.Columns[K].ToString() == "UDS")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmpudsdata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                            if (dt3.Columns[K].ToString() == "BKDS")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmpbkdsdata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                            if (dt3.Columns[K].ToString() == "Total")
                            {
                                string ColName = dt3.Columns[K].ToString();
                                tmptotaldata += Convert.ToDecimal(dt3.Rows[i][ColName]);
                            }
                        }
                    }
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmpbdsdata/7).ToString("0.00") + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmpgdsdata/7).ToString("0.00") + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmpidsdata/7).ToString("0.00") + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmpjdsdata/7).ToString("0.00") + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmpudsdata/7).ToString("0.00") + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmpbkdsdata/7).ToString("0.00") + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (tmptotaldata/7).ToString("0.00") + "</b></td>");

                   
                }
                else if(Count==7)
                {
                    decimal sum77 = 0;
                    foreach (DataColumn column2 in dt3.Columns)
                    {
                        if (column2.ToString() != "DayOfMonth")
                        {
                            sum77 = dt3.AsEnumerable().Sum(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                            sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (sum77 / 7).ToString("0.00") + "</b></td>");
                        }
                    }
                }
                else
                {
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>0.00</b></td>");
                }
                sb1.Append("</tr>");
                
             

                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>TOT</b></td>");
                decimal sum14 = 0;
                foreach (DataColumn column2 in dt3.Columns)
                {
                    if (column2.ToString() != "DayOfMonth")
                    {
                        sum14 = dt3.AsEnumerable().Sum(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + sum14.ToString("0.00") + "</b></td>");
                    }
                }
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>AVG</b></td>");
                decimal sum15 = 0;
                foreach (DataColumn column2 in dt3.Columns)
                {
                    if (column2.ToString() != "DayOfMonth")
                    {
                        sum15 = dt3.AsEnumerable().Average(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + sum15.ToString("0.00") + "</b></td>");
                    }
                }
                sb1.Append("</tr>");

                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>MAX</b></td>");
                decimal sum16 = 0;
                foreach (DataColumn column2 in dt3.Columns)
                {
                    if (column2.ToString() != "DayOfMonth")
                    {
                        sum16 = dt3.AsEnumerable().Max(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + sum16.ToString("0.00") + "</b></td>");
                    }
                }
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;'><b>MIN</b></td>");
                decimal sum17 = 0;
                foreach (DataColumn column2 in dt3.Columns)
                {
                    if (column2.ToString() != "DayOfMonth")
                    {
                        sum17 = dt3.AsEnumerable().Min(r1 => r1.Field<decimal?>("" + column2 + "") ?? 0);
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + sum17.ToString("0.00") + "</b></td>");
                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
            }
            else
            {
                ViewState["Sb"] = "";
                btnPrint.Visible = false;
                btnExcel.Visible = false;
                div_page_content.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! ", "No Record Found.");
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "MontylyMilkProcurementRpt_" + txtMonth.Text + "_" + DateTime.Now + ".xls");
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}