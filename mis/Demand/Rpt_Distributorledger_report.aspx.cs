
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Demand_Rpt_Distributorledger_report : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    double sum1 = 0;
    int sum11, sum22 = 0, sum33 = 0, sum44 = 0;
    int cellIndex = 2;
    int cellIndexboothOrg = 3;
    int i_Qty = 0, i_NaQty = 0;
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
                txttodate.Text = Date;
                txttodate.Attributes.Add("readonly", "readonly");
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
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds5.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("--Select--", "0"));
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
    private void GetDistByRouteID()
    {
        try
        {
            if (ddlRoute.SelectedValue != "0")
            {
                ddlDitributor.DataTextField = "DTName";
                ddlDitributor.DataValueField = "DistributorId";
                ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                 new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue,objdb.GetProductCatId() }, "dataset");
                ddlDitributor.DataBind();

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist ", ex.Message.ToString());
        }

    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (txtFromDate.Text != "" && txttodate.Text != "")
        {
            GetDistByRouteID();
          //  string fm = "01/" + txtMonth.Text;
           // GetTcsTax(fm);
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning! : ", " Select From Date & To Date");
        }

    }
    //private void GetTcsTax(string date)
    //{
    //    try
    //    {
    //        if (ddlDitributor.SelectedValue != "0")
    //        {
    //            ViewState["Tval"] = "";
    //            DateTime Ddate = DateTime.ParseExact(date, "dd/MM/yyyy", culture);
    //            string deld = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //            ds3 = objdb.ByProcedure("USP_Mst_TcsTax",
    //                 new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
    //                   new string[] { "0", objdb.Office_ID(), deld, ddlDitributor.SelectedValue }, "dataset");

    //            if (ds3.Tables[0].Rows.Count > 0)
    //            {
    //                ViewState["Tval"] = ds3.Tables[0].Rows[0]["Tval"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds3 != null) { ds3.Dispose(); }
    //    }
    //}
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
                if (ddlRoute.SelectedIndex > 0)
                {
                    //if (ddlDitributor.SelectedIndex > 0)
                    //{
                        GetCompareDate();
                    //}
                    //else
                    //{
                    //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select Distributor");
                    //}
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select Route");
                }
                
            }
        }
        catch
        {

        }

    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txttodate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                ProductDM_Report();
            }
            else
            {
                txttodate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6: ", ex.Message.ToString());
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
    private void ProductDM_Report()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
            //DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            //string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds2 = objdb.ByProcedure("USP_Trn_Distributor_ledger_report",
                     new string[] { "flag", "Fromdate1", "Todate1", "AreaId", "RouteId", "Office_ID", "ItemCat_id", "DistributorId" },
                       new string[] { "1", txtFromDate.Text, txttodate.Text, ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID(), objdb.GetProductCatId(), ddlDitributor.SelectedValue }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
              //  pnlData.Visible = true;

                // pnltotalPDM.Visible = true;
                //  pnltotalcost.Visible = true;
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];

               
                
                //btnPrintRoutWise.Visible = true;
                btnExcel.Visible = true;

                StringBuilder sb = new StringBuilder();

                int Count = ds2.Tables[0].Rows.Count;
                sb.Append("<table style='width:100%; height:100%'>");
                //sb.Append("<tr>");
                //sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txttodate.Text + "</b></td>");
                //sb.Append("<td style='text-align: center;'><b>Partywise D.M. wise -Bill Report</b></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td style='padding: 2px 5px;'></td>");
                //sb.Append("<td style='padding: 2px 5px;'></td>");

                //sb.Append("<td style='padding: 2px 5px;'></td>");
                //sb.Append("</tr>");

                //sb.Append("</table>");
                //sb.Append("<table class='tmptd'>");

                //int Count = ds2.Tables[0].Rows.Count;

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;text-align: center;' colspan='12'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;text-align: Left;' colspan='12'><b> Supplier Name :- " + ddlDitributor.SelectedItem.Text + "</br> Route :- " + ddlRoute.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;text-align: Left;' colspan='12'><b>Time Period :- " + txtFromDate.Text + " To " + txttodate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;'><b>S.No.</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Party Name</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Party Code</b></td>");
                
                sb.Append("<td style='border:1px solid black;'><b>BillNo</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Bill Date</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Opening</b></td>");
                

                //sb.Append("<td style='border:1px solid black; width:200px !important;'><b>DM NO.</b></td>");


                sb.Append("<td style='border:1px solid black;'><b>Total Bill Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>GST Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>TCS Tax Amount</b></td>");

                //sb.Append("<td style='border:1px solid black;'><b>Pay Amount 1</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Date 1</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Mode 1</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay No. 1</b></td>");

                //sb.Append("<td style='border:1px solid black;'><b>Pay Amount 2</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Date 2</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Mode 2</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay No. 2</b></td>");

                //sb.Append("<td style='border:1px solid black;'><b>Pay Amount 3</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Date 3</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Mode 3</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay No. 3</b></td>");

                //sb.Append("<td style='border:1px solid black;'><b>Pay Amount 4</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Date 4</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay Mode 4</b></td>");
                //sb.Append("<td style='border:1px solid black;'><b>Pay No. 4</b></td>");
   
                sb.Append("<td style='border:1px solid black;'><b>Pay Mode </b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Pay Date </b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Paid Amount </b></td>");
                
                //sb.Append("<td style='border:1px solid black;'><b>Pay No. </b></td>");

                sb.Append("<td style='border:1px solid black;'><b>Total Paid Amount</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>Closing</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                //  string Date = "", DCode = "", DName = "";
                for (int i = 0; i < Count; i++)
                {
                    //if (i == 0)

                    //{

                    //amount
                    string paidamount="",Paydate="";
                    if(double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount1"].ToString())>0.00)
                    {
                        paidamount = paidamount+ds2.Tables[0].Rows[i]["PaymentAmount1"].ToString() + "</br>";
                    }
                    if(double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount2"].ToString()) > 0.00)
                    {
                        paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount2"].ToString() + "</br>";
                    }
                   if(double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount3"].ToString()) > 0.00)
                    {
                        paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount3"].ToString() + "</br>";
                    }
                     if(double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount4"].ToString()) > 0.00)
                    {
                        paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount4"].ToString();
                    }


                    //date
                    if (ds2.Tables[0].Rows[i]["PaymentDate1"].ToString() !="")
                    {
                        Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate1"] + "</br>";
                    }
                    if (ds2.Tables[0].Rows[i]["PaymentDate2"].ToString() != "")
                    {
                        Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate2"] + "</br>";
                    }
                     if(ds2.Tables[0].Rows[i]["PaymentDate3"].ToString()!="")
                    {
                        Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate3"] + "</br>";
                    }
                     if(ds2.Tables[0].Rows[i]["PaymentDate4"].ToString()!="")
                    {
                        Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate4"];
                    }



                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DCode"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["BillNo"] + "</td>");
                   
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Payment_Date"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Opening"] + "</td>");
                    

                   // sb.Append("<td style='border:1px solid black; width:200px !important;    word-break: break-all;'>" + ds2.Tables[0].Rows[i]["DMChallanNo"] + "</td>");

                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalBillAmount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalGSTAmount"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TcsTaxAmt"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount1"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate1"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount2"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate2"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount3"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate3"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentAmount4"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentDate4"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo4"] + "</td>");
                   
                    sb.Append("<td style='border:1px solid black;' >" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + Paydate + "</td>");
                    sb.Append("<td style='border:1px solid black;'>" + paidamount + "</td>");
                    //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentNo4"] + "</td>");

                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["TotalPayment"] + "</td>");

                    sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["closing"] + "</td>");

                    sb.Append("</tr>");
                    // }

                }
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='border:1px solid black;'><b>Grand Total</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Opening") ?? 0)) + "</b></td>");
                //sb.Append("<td colspan='3' style='border:1px solid black;'></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalBillAmount") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalGSTAmount") ?? 0)) + "</b></td>");
                sb.Append("<td style='text-align:right;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TcsTaxAmt") ?? 0)) + "</b></td>");

                //sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount1") ?? 0)) + "</b></td>");
                //sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount2") ?? 0)) + "</b></td>");
                //sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount3") ?? 0)) + "</b></td>");
                //sb.Append("<td colspan='4' style='text-align:left;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("PaymentAmount4") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='3' style='text-align:right;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalPayment") ?? 0)) + "</b></td>");
                sb.Append("<td colspan='1' style='text-align:right;border:1px solid black;''><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TotalPayment") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;'><b>" + Convert.ToDecimal(ds2.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("closing") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                ViewState["sb"] = sb.ToString();
                // div_page_content.InnerHtml = sb.ToString();
                ViewState["partybillreport"] = sb.ToString();
                //sb.Append("<b><span style='padding-top:20px;'>Total DM:  " + lblTotalSupplyValue.Text + "</span><br></b>");
                //sb.Append("<b><span style='padding-top:20px;'>Total Cost Of Product :  " + lblCostOfProduct.Text + "</span></b>");
                div_page_content.InnerHtml = sb.ToString();
               // Print.InnerHtml = sb.ToString();
                div_page_content.Visible = true;

                ////////////////End Of Distributor Wise Print Code   ///////////////////////
            }
            else
            {
                //pnlData.Visible = false;
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                //GridView2.DataSource = null;
                //GridView2.DataBind();

                btnExcel.Visible = false;
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

        Response.Redirect("Rpt_Distributorledger_report.aspx");
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=Partywisebillreport.xls");
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
       // Print.InnerHtml = ViewState["partybillreport"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

    }
}