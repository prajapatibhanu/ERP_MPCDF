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


public partial class mis_Demand_Rpt_CrateTransporterwise_UDS : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5, ds7 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0;
    decimal TotalMvalue = 0, TotalPvalue = 0, TotalMPay = 0, TotalPPay = 0, transpotttotal = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
                GetOfficeDetails();
                GetTransporter();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetTransporter()
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Mst_TransporterandRouteHeadMapping",
                 new string[] { "Flag", "Office_ID" },
                   new string[] { "5", objdb.Office_ID() }, "dataset");

            ddlTransporter.Items.Clear();
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlTransporter.DataTextField = "RHCPName";
                ddlTransporter.DataValueField = "RouteHeadId";
                ddlTransporter.DataSource = ds2.Tables[0];
                ddlTransporter.DataBind();
                ddlTransporter.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlTransporter.Items.Insert(0, new ListItem("No Record Found", "0"));
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
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                GetTransporterWiseReport();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GetCompareDate();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Search: ", ex.Message.ToString());
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
    private void GetTransporterWiseReport()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_MilkCrateReport_UDS",
                     new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "AreaId", "ItemCat_id", "DeliveryShift_id", "RouteHeadId" },
                       new string[] { "1", fromdat, todat, objdb.Office_ID(), "2", objdb.GetMilkCatId(), "0", ddlTransporter.SelectedValue }, "dataset");

            if (ds5.Tables[0].Rows.Count > 0)
            {

                pnlData.Visible = true;
                btnExportAll.Visible = true;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: center;border:1px solid black;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: center;border:1px solid black;'><b>Name  :-" + ddlTransporter.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6'style='text-align: center;border:1px solid black;'><b>Transporter wise Crate Report</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Date</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Shift</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Issued Crate</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Return Crate</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Short/Excess</b></td>");
                sb.Append("</tr>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["Date"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["ShiftName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["IssueCrate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["ReturnCrate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["ShortOrExcess"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='border:1px solid black;border:1px solid black;text-align:center;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;border-bottom:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("IssueCrate") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border-bottom:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("ReturnCrate") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border-bottom:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("ShortOrExcess") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + ddlTransporter.SelectedItem.Text + "-" + "CrateRpt.xls");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnArchive_Click(object sender, EventArgs e)
    {
        try
        {
           // Response.Redirect("Rpt_CrateTransporterwise_BDS_Rural_Archive.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
}