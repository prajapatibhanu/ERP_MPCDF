using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_Distributor_dues_report_itemcategorywise : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5, ds7 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0, TotalPay = 0, TotalOpening = 0, Totalclosing = 0;
    //decimal TotalMvalue = 0, TotalPvalue = 0, TotalMPay = 0, TotalPPay = 0, transpotttotal = 0;
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
                //GetRoute();
                GetSS();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
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
    private void GetSS()
    {
        try
        {
            //ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
            //    new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
            //      new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ds7 = objdb.ByProcedure("USP_Trn_merge_dues_report_categorywise_New",
               new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "2", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlDitributor.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlDitributor.DataTextField = "DName";
                ddlDitributor.DataValueField = "DistributorId";
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
                duesPayment_Report();


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
    private void duesPayment_Report()
    {
        try
        {
            DateTime date1 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string fromdate = date1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime date2 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string todate = date2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            lblMsg.Text = "";
            string routeid = "";
            if (ddlDitributor.SelectedValue == "0")
            {
                routeid = "0";
            }
            else
            {

                routeid = "0";
            }

            if (ddlDitributor.SelectedValue != "0")
            {
                if (objdb.Office_ID() == "2")
                {
                    ds2 = objdb.ByProcedure("USP_Trn_merge_dues_report_categorywise_New_Bhopal",
                             new string[] { "flag", "FromDate1", "ToDate1 ", "AreaID", "RouteId", "Office_ID", "DistributorId", "ItemCat_id" },
                               new string[] { "1", fromdate.ToString(), todate.ToString(), ddlLocation.SelectedValue, routeid.ToString(), objdb.Office_ID(), ddlDitributor.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
                }
                else
                {
                    ds2 = objdb.ByProcedure("USP_Trn_merge_dues_report_categorywise_New",
                             new string[] { "flag", "FromDate1", "ToDate1 ", "AreaID", "RouteId", "Office_ID", "DistributorId", "ItemCat_id" },
                               new string[] { "1", fromdate.ToString(), todate.ToString(), ddlLocation.SelectedValue, routeid.ToString(), objdb.Office_ID(), ddlDitributor.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
                }
            }
            else if (ddlDitributor.SelectedValue == "0")
            {
                if (objdb.Office_ID() == "2" || objdb.Office_ID() == "5")
                {
                    ds2 = objdb.ByProcedure("USP_Trn_merge_dues_report_categorywise_New_Bhopal",
                             new string[] { "flag", "FromDate1", "ToDate1 ", "AreaID", "RouteId", "Office_ID", "DistributorId", "ItemCat_id" },
                               new string[] { "3", fromdate.ToString(), todate.ToString(), ddlLocation.SelectedValue, routeid.ToString(), objdb.Office_ID(), ddlDitributor.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
                }
                else
                {
                    ds2 = objdb.ByProcedure("USP_Trn_merge_dues_report_categorywise_New",
                             new string[] { "flag", "FromDate1", "ToDate1 ", "AreaID", "RouteId", "Office_ID", "DistributorId", "ItemCat_id" },
                               new string[] { "3", fromdate.ToString(), todate.ToString(), ddlLocation.SelectedValue, routeid.ToString(), objdb.Office_ID(), ddlDitributor.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
                }
            }
            if (ds2.Tables.Count != 0)
            {
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
                    sb.Append("<td style='text-align: center;'><b>" + ddlItemCategory.SelectedItem.Text + " Dues Report</b></td>");
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
                    if (ddlDitributor.SelectedValue != "0")
                    {
                        sb.Append("<tr>");

                        sb.Append("<td style='border:1px solid black;width:5px;'><b>S.No.</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Route</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Party Name</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Date</b></td>");
                        if ((objdb.Office_ID() == "2" || objdb.Office_ID() == "5") && ddlItemCategory.SelectedValue == "3" && ddlDitributor.SelectedValue!="0")
                        {
                            sb.Append("<td style='border:1px solid black;'><b>Invoice No.</b></td>");
                        }
                        else if ((objdb.Office_ID() == "2" || objdb.Office_ID() == "5") && ddlItemCategory.SelectedValue == "2" && ddlDitributor.SelectedValue != "0")
                        {
                            sb.Append("<td style='border:1px solid black;'><b>DM No.</b></td>");
                        }
                        sb.Append("<td style='border:1px solid black;'><b>Opening </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>" + ddlItemCategory.SelectedItem.Text + " Value</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Pay Mode </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Pay Date </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Paid Amount </b></td>");

                        sb.Append("<td style='border:1px solid black;'><b>Pay No. </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Total Payment</b></td>");


                        sb.Append("<td style='border:1px solid black;'><b>Closing  </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Securiy Deposit </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Bank Guarantee </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Contact No.</b></td>");


                        sb.Append("</tr>");


                        for (int i = 0; i < Count; i++)
                        {
                            //amount
                            string paidamount = "", Paydate = "", PayNo = "";
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount1"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount1"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount2"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount2"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount3"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount3"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount4"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount4"].ToString();
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount5"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount5"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount6"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount6"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount7"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount7"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount8"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount8"].ToString();
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount9"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount9"].ToString() + "</br>";
                            }
                            if (double.Parse(ds2.Tables[0].Rows[i]["PaymentAmount10"].ToString()) > 0.00)
                            {
                                paidamount = paidamount + ds2.Tables[0].Rows[i]["PaymentAmount10"].ToString();
                            }


                            //date
                            if (ds2.Tables[0].Rows[i]["PaymentDate1"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate1"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate2"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate2"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate3"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate3"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate4"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate4"];
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate5"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate5"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate6"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate6"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate7"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate7"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate8"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate8"];
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate9"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate9"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentDate10"].ToString() != "")
                            {
                                Paydate = Paydate + ds2.Tables[0].Rows[i]["PaymentDate10"];
                            }


                            //no
                            if (ds2.Tables[0].Rows[i]["PaymentNo1"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo1"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo2"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo2"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo3"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo3"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo4"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo4"];
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo5"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo5"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo6"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo6"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo7"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo7"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo8"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo8"];
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo9"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo9"] + "</br>";
                            }
                            if (ds2.Tables[0].Rows[i]["PaymentNo10"].ToString() != "")
                            {
                                PayNo = PayNo + ds2.Tables[0].Rows[i]["PaymentNo10"];
                            }
                            decimal closing = Math.Round((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]) - (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"]))), 2);

                            sb.Append("<tr>");
                            sb.Append("<td style='border:1px solid black;width:5px;'>" + (i + 1).ToString() + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["RName"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ddate"] + "</td>");
                            if ((objdb.Office_ID() == "2" || objdb.Office_ID() == "5") && ddlItemCategory.SelectedValue == "3" && ddlDitributor.SelectedValue != "0")
                            {
                                sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["InvoiceNoALL"] + "</td>");
                            }
                            else if ((objdb.Office_ID() == "2" || objdb.Office_ID() == "5") && ddlItemCategory.SelectedValue == "2" && ddlDitributor.SelectedValue != "0")
                            {
                                sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DMNoALL"] + "</td>");
                            }
                            sb.Append("<td style='border:1px solid black;'>" + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + (objdb.Office_ID() == "2" ? Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 0) : Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 2)) + "</td>");
                            sb.Append("<td style='border:1px solid black;' >" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + Paydate + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + paidamount + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + PayNo + "</td>");

                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaidAmt"] + "</td>");





                            sb.Append("<td style='border:1px solid black;'>" + closing.ToString("0.00") + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["SecurityDeposit"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["BankGuarantee"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DCPersonMobileNo"] + "</td>");




                            sb.Append("</tr>");
                            Totalclosing += closing;
                            Totalvalue += (objdb.Office_ID() == "2" ? Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 0) : Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 2));

                            TotalPay += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"]));
                            TotalOpening += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]));
                        }

                        //Totalvalue += Math.Round((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"])), 2);


                    }
                    else if (ddlDitributor.SelectedValue == "0")
                    {
                        sb.Append("<tr>");

                        sb.Append("<td style='border:1px solid black;width:5px;'><b>S.No.</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Route</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Party Name</b></td>");
                        // sb.Append("<td style='border:1px solid black;'><b>Date</b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Opening </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>" + ddlItemCategory.SelectedItem.Text + " Value</b></td>");
                        //sb.Append("<td style='border:1px solid black;'><b>Pay Mode </b></td>");
                        //sb.Append("<td style='border:1px solid black;'><b>Pay Date </b></td>");
                        //sb.Append("<td style='border:1px solid black;'><b>Paid Amount </b></td>");

                        //sb.Append("<td style='border:1px solid black;'><b>Pay No. </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Total Payment</b></td>");


                        sb.Append("<td style='border:1px solid black;'><b>Closing  </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Securiy Deposit </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Bank Guarantee </b></td>");
                        sb.Append("<td style='border:1px solid black;'><b>Contact No.</b></td>");


                        sb.Append("</tr>");


                        for (int i = 0; i < Count; i++)
                        {

                            decimal closing = Math.Round((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]) - (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"]))), 2);

                            sb.Append("<tr>");
                            sb.Append("<td style='border:1px solid black;width:5px;'>" + (i + 1).ToString() + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["RName"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                            //sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ddate"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]) + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + (objdb.Office_ID() == "2" ? Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 0) : Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 2)) + "</td>");
                            //sb.Append("<td style='border:1px solid black;' >" + ds2.Tables[0].Rows[i]["PaymentModeId1"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId2"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId3"] + "</br>" + ds2.Tables[0].Rows[i]["PaymentModeId4"] + "</td>");
                            //sb.Append("<td style='border:1px solid black;'>" + Paydate + "</td>");
                            //sb.Append("<td style='border:1px solid black;'>" + paidamount + "</td>");
                            //sb.Append("<td style='border:1px solid black;'>" + PayNo + "</td>");

                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaidAmt"] + "</td>");





                            sb.Append("<td style='border:1px solid black;'>" + closing.ToString("0.00") + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["SecurityDeposit"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["BankGuarantee"] + "</td>");
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DCPersonMobileNo"] + "</td>");




                            sb.Append("</tr>");
                            Totalclosing += closing;
                            Totalvalue += (objdb.Office_ID() == "2" ? Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 0) : Math.Round(Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]), 2));

                            TotalPay += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt"]));
                            TotalOpening += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening"]));
                        }

                    }




                    sb.Append("<tr>");
                    if (ddlDitributor.SelectedValue == "0")
                    {
                        sb.Append("<td colspan='3'><b>Total</b></td>");
                    }
                    else
                    {
                        sb.Append("<td colspan='5'><b>Total</b></td>");
                    }
                    sb.Append("<td style='border:1px solid black;'><b>" + TotalOpening + "</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>" + Totalvalue + "</b></td>");
                    if (ddlDitributor.SelectedValue == "0")
                    {
                    }
                    else
                    {
                        sb.Append("<td ></td>");
                        sb.Append("<td ></td>");
                        sb.Append("<td ></td>");
                        sb.Append("<td ></td>");
                    }
                    sb.Append("<td style='border:1px solid black;'><b>" + TotalPay + "</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>" + Totalclosing + "</b></td>");


                    sb.Append("<td colspan='3' ></td>");



                    sb.Append("</tr>");
                    sb.Append("</table>");

                    div_page_content.Visible = true;
                    div_page_content.InnerHtml = sb.ToString();
                    Print.InnerHtml = sb.ToString();



                }
                else
                {
                    pnlData.Visible = false;
                    btnExportAll.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "No Record Found.");

                }
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

        Response.Redirect("Distributor_dues_report_itemcategorywise.aspx");
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