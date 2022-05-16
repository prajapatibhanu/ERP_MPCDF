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

public partial class mis_Demand_Rpt_InstituteMilkInvoice_JDS : System.Web.UI.Page
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
                GetRoute();
                GetInstitution();

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
    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("All", "0"));



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

        GetInstitution();
    }
    private void GetInstitution()
    {
        try
        {

            ddlInstitution.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                 new string[] { "flag", "Office_ID", "RouteId" },
                 new string[] { "16", objdb.Office_ID(), ddlRoute.SelectedValue }, "dataset");
            ddlInstitution.DataTextField = "BName";
            ddlInstitution.DataValueField = "BoothId";
            ddlInstitution.DataBind();
            ddlInstitution.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
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
                GetInstituteReport();
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
    private void GetInstituteReport()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            //ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
            //         new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "OrganizationId" },
            //           new string[] { "10", fromdat, todat, objdb.Office_ID(), ddlInstitution.SelectedValue }, "dataset");
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                    new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "OrganizationId", "Item_id" },
                      new string[] { "14", fromdat, todat, objdb.Office_ID(), ddlInstitution.SelectedValue,ddlItems.SelectedValue }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {


                pnlData.Visible = true;
                btnExportAll.Visible = true;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8' style='text-align: center;border:1px solid black;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8' style='text-align: left;border:1px solid black;'>Date <b>  :" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8'style='text-align: left;border:1px solid black;'>Party Name : <b>" + ddlInstitution.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds2.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Invoice No</b></td>");
                sb.Append("<td style='border:1px solid black;width:50px'><b>Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Product Name</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Qty (In Pkt).</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Qty (In Ltr).</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Rate/Ltr.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Total Amount</b></td>");
                sb.Append("</tr>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["InvoiceNo"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["SupplyDate"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='border:1px solid black;border:1px solid black;text-align:center;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToInt32(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("BillingQty") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("BillingQtyInLtr") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'></td>");
                sb.Append("<td style='border:1px solid black;border:1px solid black;text-align: center;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaybleAmount") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();

            }
            else
            {
                pnlData.Visible = false;
                div_page_content.Visible = false;
                btnExportAll.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
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
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + ddlInstitution.SelectedItem.Text + "-" + "Invoice_Report.xls");
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
    protected void ddlInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstitution.SelectedIndex > 0)
        {
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "OrganizationId" },
                       new string[] { "13", fromdat, todat, objdb.Office_ID(), ddlInstitution.SelectedValue }, "dataset");
            if (ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ddlItems.DataTextField = "IName";
                    ddlItems.DataValueField = "Item_id";
                    ddlItems.DataSource = ds2.Tables[0];
                        //objdb.ByProcedure("USP_Mst_Area",
                        // new string[] { "flag" },
                        //   new string[] { "1" }, "dataset");
                    ddlItems.DataBind();
                    ddlItems.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    ddlItems.DataSource = null;
                    ddlItems.DataBind();
                }
            }
            else
            {
                ddlItems.DataSource = null;
                ddlItems.DataBind();
            }


        }
        else
        {
            ddlItems.DataSource = null;
            ddlItems.DataBind();
        }
    }
}