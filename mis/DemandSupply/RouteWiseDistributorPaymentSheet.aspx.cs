using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_RouteWiseDistributorPaymentSheet : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0;
   
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetRoute();
                GetOfficeDetails();

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
            ddlRoute.Items.Clear();
            ds5 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds5.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRoute();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                MilkPayment_Report();
            }
        }
        catch
        {

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
    private void MilkPayment_Report()
    {
        try
        {
            lblMsg.Text = "";
            ds2 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet",
                     new string[] { "flag", "Fromdate1", "Todate1", "AreaId", "RouteId", "Office_ID", "ItemCat_id" },
                       new string[] { "9", txtFromDate.Text, txttodate.Text, ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID(), objdb.GetMilkCatId() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];
               
                btnExportAll.Visible = true;

                StringBuilder sb = new StringBuilder();


                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txttodate.Text + "</b></td>");
                sb.Append("<td style='text-align: center;'><b>Milk Payment Report</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");

                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table'>");

                int Count = ds2.Tables[0].Rows.Count;

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;'><b>S.No.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Route</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Distributor Name</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Opening Balance</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Milk Value</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Total Value</b></td>");               
                sb.Append("<td style='border:1px solid black;'><b>Amount(A)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Payment Mode (A)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (A)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Amount(B)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Payment Mode (B)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (B)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Amount(C)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Payment Mode (C)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (C)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Amount(D)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Payment Mode (D)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (D)</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Paid Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Closing</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");

               
                for (int i = 0; i < Count; i++)
                {
                  
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["RName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Opening"] + "</td>");                   
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])) + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount1"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount2"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount3"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount4"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo4"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaidAmt"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])) - Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"])) + "</td>");

                    sb.Append("</tr>");
                    Totalvalue += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]));
                    ClosingBalance += ((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])) - Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"]));

                }
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:right;border:1px solid black;'><b>Grand Total</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Opening") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Totalvalue + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount1") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount2") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount3") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:left;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount4") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaidAmt") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + ClosingBalance + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                ViewState["sb"] = sb.ToString();
                ViewState["MilkPaymentRpt"] = sb.ToString();
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();
                div_page_content.Visible = true;

                ////////////////End Of Distributor Wise Print Code   ///////////////////////
            }
            else
            {
                pnlData.Visible = false;
                btnExportAll.Visible = false;
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "No Record Found.");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Product DM ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }



    protected void btnClear_Click(object sender, EventArgs e)
    {

         Response.Redirect("RouteWiseDistributorPaymentSheet.aspx");
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txttodate.Text + "Route -" + ddlRoute.SelectedItem.Text + "-" + "MilkPaymentRpt.xls");
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
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Print.InnerHtml = ViewState["MilkPaymentRpt"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}