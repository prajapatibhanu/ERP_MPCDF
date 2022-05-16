using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Rpt_MilkOrProductDuesReport_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5, ds7 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0;
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
                GetCategory();
                GetLocation();
                GetSS();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void GetSS()
    {
        try
        {
            ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlDitributor.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlDitributor.DataTextField = "SSName";
                ddlDitributor.DataValueField = "SSRDId";
                ddlDitributor.DataSource = ds7.Tables[0];
                ddlDitributor.DataBind();
                ddlDitributor.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlDitributor.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds7 != null) { ds7.Dispose(); }
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSS();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSS();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                {
                    MilkPayment_Report();
                }
                else
                {
                    ProductDM_Report();
                }

            }
        }
        catch
        {

        }

    }
    protected void GetCategory()
    {
        try
        {
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
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
            string routeid = "";
            if (ddlDitributor.SelectedValue == "0")
            {
                routeid = "0";
            }
            else
            {
                string[] SSRDId = ddlDitributor.SelectedValue.Split('-');
                routeid = SSRDId[1];
            }
            ds2 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet",
                     new string[] { "flag", "Fromdate1", "Todate1", "AreaId", "RouteId", "Office_ID", "ItemCat_id" },
                       new string[] { "9", txtFromDate.Text, txtToDate.Text, ddlLocation.SelectedValue, routeid.ToString(), objdb.Office_ID(), objdb.GetMilkCatId() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;
                btnExportAll.Visible = true;
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];


                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("<td style='text-align: center;'><b>Milk Payment Report</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");

                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table1'>");

                int Count = ds2.Tables[0].Rows.Count;

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;'><b>S.No.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Route</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Distributor Name</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Opening Balance</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Milk Value</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Total Value</b></td>");

                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount1_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Amount(A)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Payment Mode (A)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (A)</b></td>");

                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount2_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Amount(B)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Payment Mode (B)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (B)</b></td>");

                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount3_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Amount(C)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Payment Mode (C)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (C)</b></td>");
                    
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount4_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Amount(D)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Payment Mode (D)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>U.T.R No. (D)</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["TotalPayment"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Paid Amount</b></td>");
                }
                sb.Append("<td style='border:1px solid black;'><b>Closing</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");


                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["RName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Delivary_Date"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Opening"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])) + "</td>");
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount1_total"].ToString()) > 0)
                    {
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount1"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount2_total"].ToString()) > 0)
                    {
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount2"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount3_total"].ToString()) > 0)
                    {
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount3"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount4_total"].ToString()) > 0)
                    {
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount4"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo4"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["TotalPayment"].ToString()) > 0)
                    {
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaidAmt"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])) - Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"])) + "</td>");
                    }
                  

                    sb.Append("</tr>");
                    Totalvalue += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]));
                    ClosingBalance += ((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])) - Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"]));

                }
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:right;border:1px solid black;'><b>Grand Total</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Opening") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Totalvalue + "</b></td>");
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount1_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='3' style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount1") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount2_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='3' style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount2") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount3_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='3' style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount3") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount4_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='3' style='text-align:left;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount4") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["TotalPayment"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaidAmt") ?? 0)) + "</b></td>");
                }
                sb.Append("<td style='border:1px solid black;'><b>" + ClosingBalance + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
               // ViewState["sb"] = sb.ToString();
               // ViewState["MilkOrProductPaymentRpt"] = sb.ToString();
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();
                

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


    private void ProductDM_Report()
    {
        try
        {
            lblMsg.Text = "";
            string routeid = "";
            if (ddlDitributor.SelectedValue == "0")
            {
                routeid = "0";
            }
            else
            {
                string[] SSRDId = ddlDitributor.SelectedValue.Split('-');
                routeid = SSRDId[1];
            }
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);

            ds2 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet",
                     new string[] { "flag", "Fromdate1", "Todate1", "AreaId", "RouteId", "Office_ID", "ItemCat_id", "DelivaryShift_id" },
                       new string[] { "5", txtFromDate.Text, txtToDate.Text, ddlLocation.SelectedValue, routeid, objdb.Office_ID(), objdb.GetProductCatId(), objdb.GetShiftMorId() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;
                btnExportAll.Visible = true;


                DataTable dt = new DataTable();
                dt = ds2.Tables[0];



                StringBuilder sb = new StringBuilder();


                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("<td style='text-align: center;'><b>Product Payment Report</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");

                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table1'>");

                int Count = ds2.Tables[0].Rows.Count;

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;'><b>S.No.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Party Name</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Party Code</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Opening</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Bill Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>BillNo</b></td>");

                sb.Append("<td style='border:1px solid black; width:200px !important;'><b>DM NO.</b></td>");


                sb.Append("<td style='border:1px solid black;'><b>Total Bill Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>GST Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>TCS Tax</b></td>");

                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount1_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Pay Amount 1</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Date 1</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Mode 1</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay No. 1</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount2_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Pay Amount 2</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Date 2</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Mode 2</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay No. 2</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount3_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Pay Amount 3</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Date 3</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Mode 3</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay No. 3</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount4_total"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Pay Amount 4</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Date 4</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay Mode 4</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Pay No. 4</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["TotalPayment"].ToString()) > 0)
                {
                    sb.Append("<td style='border:1px solid black;'><b>Total Payment</b></td>");
                }

                sb.Append("<td style='border:1px solid black;'><b>Closing</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                //  string Date = "", DCode = "", DName = "";
                for (int i = 0; i < Count; i++)
                {
                    //if (i == 0)
                    //{
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DCode"] + "</td>");

                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Opening"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Payment_Date"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["BillNo"] + "</td>");

                    sb.Append("<td style='border:1px solid black; width:200px !important;    word-break: break-all;'>" + ds2.Tables[0].Rows[i]["DMChallanNo"] + "</td>");

                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalBillAmount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalGSTAmount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TcsTaxAmt"] + "</td>");
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount1_total"].ToString()) > 0)
                    {

                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount1"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate1"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount2_total"].ToString()) > 0)
                    {

                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount2"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate2"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount3_total"].ToString()) > 0)
                    {

                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount3"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate3"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount4_total"].ToString()) > 0)
                    {

                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount4"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate4"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo4"] + "</td>");
                    }
                    if (double.Parse(ds2.Tables[1].Rows[0]["TotalPayment"].ToString()) > 0)
                    {

                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalPayment"] + "</td>");
                    }


                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["closing"] + "</td>");

                    sb.Append("</tr>");
                    // }

                }
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='border:1px solid black;'><b>Grand Total</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Opening") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='3' style='border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalBillAmount") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalGSTAmount") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TcsTaxAmt") ?? 0)) + "</b></td>");
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount1_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount1") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount2_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount2") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount3_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount3") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["PaymentAmount4_total"].ToString()) > 0)
                {
                    sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount4") ?? 0)) + "</b></td>");
                }
                if (double.Parse(ds2.Tables[1].Rows[0]["TotalPayment"].ToString()) > 0)
                {
                    sb.Append("<td colspan='1' style='text-align:right;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalPayment") ?? 0)) + "</b></td>");
                }
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("closing") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
               // ViewState["sb"] = sb.ToString();
                // div_page_content.InnerHtml = sb.ToString();
               // ViewState["partybillreport"] = sb.ToString();
                //sb.Append("<b><span style='padding-top:20px;'>Total DM:  " + lblTotalSupplyValue.Text + "</span><br></b>");
                //sb.Append("<b><span style='padding-top:20px;'>Total Cost Of Product :  " + lblCostOfProduct.Text + "</span></b>");
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

        Response.Redirect("Rpt_MilkOrProductDuesReport_IDS.aspx");
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + ddlItemCategory.SelectedItem.Text + "-" + "DuesPaymentRpt.xls");
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
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}