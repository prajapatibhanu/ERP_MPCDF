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

public partial class mis_DemandSupply_Rpt_ProductReturnOrReplace_JBL : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
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
                GetLocation();
                GetDist();

            }
        }
        else
        {
            objdb.redirectToHome();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetDist()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "11", ddlLocation.SelectedValue, objdb.GetProductCatId(), objdb.Office_ID() }, "dataset");
            ddlDitributor.DataBind();
            ddlDitributor.Items.Insert(0, new ListItem("All", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDist();
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
                GetRRReport();
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
    private void GetRRReport()
    {
        try
        {
            DataTable dt1 = new DataTable();
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_RR",
                     new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "ItemCat_id", "AreaId", "DistributorId" },
                       new string[] { "3", fromdat, todat, objdb.Office_ID(), objdb.GetProductCatId(), ddlLocation.SelectedValue, ddlDitributor.SelectedValue }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                StringBuilder sb = new StringBuilder();
                int Count1 = ds2.Tables[0].Rows.Count;
                int ColCount1 = ds2.Tables[0].Columns.Count;
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='" + (ColCount1) + "' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='" + (ColCount1) + "' style='text-align: left;border:1px solid black;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='" + (ColCount1) + "' style='text-align: left;border:1px solid black;'><b>Party  :-" + ddlDitributor.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td ccolspan='" + (ColCount1) + "' style='text-align: left;border:1px solid black;'><b>Product Return or Replace Report</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
               
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Party Name</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>D.M. No.</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>D.M. Date</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Item Name</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Supply Qty(In Pkt)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Return Qty(In Pkt)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Return Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Replace Qty(In Pkt)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Replace Date</b></td>");
                sb.Append("</tr>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["DMDate"] + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ItemName"] + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["SupplyQty"] + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + (ds2.Tables[0].Rows[i]["ReturnQty"].ToString() == "0" ? "" : ds2.Tables[0].Rows[i]["ReturnQty"].ToString()) + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReturnDate"] + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + (ds2.Tables[0].Rows[i]["ReplaceQty"].ToString() == "0" ? "" : ds2.Tables[0].Rows[i]["ReplaceQty"].ToString()) + "</td>");
                    sb.Append("<td style='border-top:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["ReplaceDate"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='5' style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Total</b></td>");
                sb.Append("<td style='border-top:1px solid black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("SupplyQty") ?? 0)) + "</b></td>");
                sb.Append("<td style='border-top:1px solid black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("ReturnQty") ?? 0)) + "</b></td>");
                sb.Append("<td style='border-top:1px solid black;border-bottom:1px dashed black;text-align:right;'></td>");
                sb.Append("<td style='border-top:1px solid black;border-bottom:1px dashed black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("ReplaceQty") ?? 0)) + "</b></td>");
                sb.Append("<td style='border-top:1px solid black;border-bottom:1px dashed black;text-align:right;'></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();

            }
            else
            {
                pnlData.Visible = false;
                div_page_content.Visible = false;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Recore Found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }


    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + ddlDitributor.SelectedItem.Text + "-" + "ReturnReplaceReport.xls");
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
}