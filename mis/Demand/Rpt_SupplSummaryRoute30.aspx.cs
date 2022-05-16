using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Data;

public partial class mis_Demand_Rpt_SupplSummaryRoute30 : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
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
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                SummaryReportForRoute30();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    private void SummaryReportForRoute30()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_MilkSupplyReportForRouteThirty",
                     new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "Office_ID" },
                       new string[] { "1", fromdat, todat,objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                int Count = ds5.Tables[0].Rows.Count;
                int ColCount = ds5.Tables[0].Columns.Count;
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align:center' colspan='2'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: left;'><b>Supply Summary Report For  :-" + " Route  30" + "</b></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Period  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                sb1.Append("<thead>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Date</b></td>");
                for (int j = 0; j < ColCount; j++)
                {

                    if (ds5.Tables[0].Columns[j].ToString() != "DDate" && ds5.Tables[0].Columns[j].ToString() != "RouteId" && ds5.Tables[0].Columns[j].ToString() != "FinalDate")
                    {
                        string ColName = ds5.Tables[0].Columns[j].ToString();
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb1.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["FinalDate"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds5.Tables[0].Columns[K].ToString() != "DDate" && ds5.Tables[0].Columns[K].ToString() != "RouteId" && ds5.Tables[0].Columns[K].ToString() != "FinalDate")
                        {
                            string ColName = ds5.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");

                        }

                    }
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='1'><b>Total</b></td>");
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];
                int sum11 = 0;
                decimal dsum11 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "DDate" && column.ToString() != "RouteId" && column.ToString() != "FinalDate")
                    {
                        if (column.ToString() == "In Litre" || column.ToString() == "Milk Amount")
                        {
                            dsum11 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + dsum11.ToString() + "</b></td>");
                        }
                        else
                        {
                            sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");
                        }


                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrintRoutWise.Visible = false;
        btnExportAll.Visible = false;
        ViewState["SummaryProduct"] = "";
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
       // Print.InnerHtml = ViewState["SummaryProduct"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "Route30-" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Print.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
}