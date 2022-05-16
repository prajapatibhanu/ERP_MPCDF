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

public partial class mis_Demand_DistributorProductPaymentReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds,ds1, ds2, ds3, ds4, ds9, ds5, dsInvo = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetRouteIDByDistributor();
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }
    private void GetRouteIDByDistributor()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "DistributorId" },
                       new string[] { "4", objdb.Office_ID(), objdb.createdBy() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["RouteId"] = ds.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistributorProductPaymentReport.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ProductDM_Report();
        }
    }
    private void ProductDM_Report()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
            //DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            //string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds2 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet",
                     new string[] { "flag", "Fromdate1", "Todate1", "RouteId", "Office_ID", "ItemCat_id", "DelivaryShift_id" },
                       new string[] { "6", txtFromDate.Text, txttodate.Text, ViewState["RouteId"].ToString(), objdb.Office_ID(), objdb.GetProductCatId(), objdb.GetShiftMorId() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;

                // pnltotalPDM.Visible = true;
                //  pnltotalcost.Visible = true;
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];

                //foreach (DataRow drow in dt.Rows)
                //{
                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        if (column.ToString() != "DCode" && column.ToString() != "DistributorId" && column.ToString() != "DName" && column.ToString() != "Total DM" && column.ToString() != "Cost  of Product")
                //        {

                //            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                //            {
                //                drow[column] = 0;
                //            }

                //        }

                //    }
                //}
                GridView1.DataSource = ds2;
                GridView1.DataBind();
                GridView2.DataSource = ds2;
                GridView2.DataBind();
                //btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                StringBuilder sb = new StringBuilder();


                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txttodate.Text + "</b></td>");
                sb.Append("<td style='text-align: center;'><b>Distributor Product Payment Report</b></td>");
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
                sb.Append("<td style='border:1px solid black;'><b>Opening</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>BillNo</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Bill Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>DM NO.</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Party Code</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Party Name</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Total Bill Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>GST Amount</b></td>");

                sb.Append("<td style='border:1px solid black;'><b>Pay Mode 1</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay No. 1</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay Amount 1</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay Mode 2</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay No. 2</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay Amount 2</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay Mode 3</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay No. 3</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay Amount 3</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Closing</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                //  string Date = "", DCode = "", DName = "";
                for (int i = 0; i < Count; i++)
                {
                    //if (i == 0)
                    //{
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Opening"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["BillNo"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Payment_Date"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DCode"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalBillAmount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalGSTAmount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount1"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount2"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount3"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["closing"] + "</td>");

                    sb.Append("</tr>");
                    // }

                }
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;'><b>Grand Total</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Opening") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='5' style='border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalBillAmount") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalGSTAmount") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='3' style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount1") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='3' style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount2") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='3' style='text-align:right;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount3") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("closing") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                ViewState["sb"] = sb.ToString();
                // div_page_content.InnerHtml = sb.ToString();
                ViewState["DistribuotrProdPayreport"] = sb.ToString();
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
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();

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
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=DistributorProductPaymentreport.xls");
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
        Print.InnerHtml = ViewState["DistribuotrProdPayreport"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
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
}