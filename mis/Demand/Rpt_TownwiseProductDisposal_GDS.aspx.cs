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


public partial class mis_Demand_Rpt_TownwiseProductDisposal_GDS : System.Web.UI.Page
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
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Text = Date;
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

                string itemid = "";
                int iddata = 0;
               
               ProductDisposalReport(itemid);

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
   
    private void ProductDisposalReport(string itemid)
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_ProductDisposalRpt_townwise",
                     new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "Office_ID" },
                       new string[] { "1", fromdat, todat, objdb.GetProductCatId(), objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                int Count = ds5.Tables[0].Rows.Count;
                int ColCount = ds5.Tables[0].Columns.Count;
                StringBuilder sb1 = new StringBuilder();

                //string s = date3.DayOfWeek.ToString();
                sb1.Append("<div class='table-responsive'>");
                sb1.Append("<table class='table'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount + 1) + "'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount + 1) + "'><b>Party Wise Product Sale Report</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount + 1) + "'><b>Date :" + txtFromDate.Text+ "-" + txtToDate.Text +"</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.No</b></td>");
                for (int j = 0; j < ColCount; j++)
                {
                        string ColName = ds5.Tables[0].Columns[j].ToString();
                        if (ColName.ToString() == "RouteHeadName")
                        {
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>City Name</b></td>");
                        }
                         else
                        {
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                        }
                        
                }
                sb1.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    { 
                            string ColName = ds5.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");

                    }
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='2'><b>Total (In Unit)</b></td>");
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];
                int sum11 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "RouteHeadName")
                    {
                      sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                      sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");
                    }
                }
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='2'><b>Total (In Ltr/KG)</b></td>");
                DataTable dt1 = new DataTable();
                dt1 = ds5.Tables[1];
                decimal dsum11 = 0;
                foreach (DataColumn column in dt1.Columns)
                {
                    if (column.ToString() != "RouteHeadName")
                    {
                        dsum11 = dt1.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + dsum11.ToString() + "</b></td>");
                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("</div>");
                div_page_content.InnerHtml = sb1.ToString();
                ViewState["SectionWiseProdDetails"] = sb1.ToString();
                if (dt != null) { dt.Dispose(); }
                if (dt1 != null) { dt1.Dispose(); }

            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["SectionWiseProdDetails"] = "";
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
        ViewState["SectionWiseProdDetails"] = "";
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
       
            Print.InnerHtml = ViewState["SectionWiseProdDetails"].ToString();

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
            Response.AddHeader("content-disposition", "attachment; filename=" + "TownWiseProductDisposalReport"+ DateTime.Now + ".xls");
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