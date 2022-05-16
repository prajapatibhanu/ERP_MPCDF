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

public partial class mis_DemandSupply_PartywiseDMwiseDMReport : System.Web.UI.Page
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
                GetLocation();
                GetShift();
                GetDisOrSS();
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
    private void GetDisOrSS()
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
        lblMsg.Text = string.Empty;
        GetDisOrSS();
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
                GetPartywiseDMwiseGatePassRpt();
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
    protected void GetShift()
    {
        try
        {
            ddlShift.DataTextField = "ShiftName";
            ddlShift.DataValueField = "Shift_id";
            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void GetPartywiseDMwiseGatePassRpt()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "AreaId", "DistributorId", "Office_ID" },
                       new string[] { "26", fromdat, todat, ddlShift.SelectedValue, objdb.GetProductCatId(), ddlLocation.SelectedValue, ddlDitributor.SelectedValue, objdb.Office_ID() }, "dataset");

            DataTable ds2 = ds5.Tables[1];
            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                // print for partywise D.M. - Gatepass Report
                ViewState["GPandProduct"] = "";
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                lblMsg.Text = string.Empty;
                StringBuilder sb = new StringBuilder();


                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb.Append("<td style='text-align: center;'><b>Partywise D.M. wise -Gatepass Report</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");

                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table'>");

                int Count = ds5.Tables[0].Rows.Count;

                sb.Append("<thead>");
                sb.Append("<tr>");
                //sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>S.No.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>D.M. No.</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>D.M. Date</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Shift</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Party Code</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Party Name</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Quantity</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Amount</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Gst Amt</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Net Amt</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TcsTax Amt</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>Net Grand Total</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                string Date = "", DCode = "", DName = "", Amt = "";
                decimal GrandTotalAmt = 0;
                //int i=0;
                for (int i = 0; i < Count; i++)
                {

                    if (i == 0)
                    {
                        sb.Append("<tr>");
                        //sb.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</b></td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMDate"] + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ShiftName"] + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DCode"] + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DName"] + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["TotalQty"] + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["Amount"]).ToString("0") + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["GST"]).ToString("0") + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");

                    }
                    else
                    {
                        if (ds5.Tables[0].Rows[i]["DName"].ToString() == DName)
                        {
                            if (GrandTotalAmt > 0)
                            {
                                sb.Append("<td style='border-top:1px dashed black;'></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                //sb.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</b></td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMDate"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ShiftName"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DCode"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DName"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["TotalQty"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["Amount"]).ToString("0") + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["GST"]).ToString("0") + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");
                                //GrandTotalAmt += Convert.ToDecimal(ds5.Tables[0].Rows[i]["Amount"]);
                                GrandTotalAmt += Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]);
                            }
                            else
                            {

                                sb.Append("<td style='border-top:1px dashed black;'>" + (GrandTotalAmt.ToString() == "0" ? "" : GrandTotalAmt.ToString("0")) + "</td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");

                                //sb.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</b></td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMDate"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ShiftName"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DCode"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DName"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["TotalQty"] + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["Amount"]).ToString("0") + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["GST"]).ToString("0") + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");
                                GrandTotalAmt += (Convert.ToDecimal(Amt) + Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]));
                                //GrandTotalAmt += (Convert.ToDecimal(Amt) + Convert.ToDecimal(ds5.Tables[0].Rows[i]["Amount"]));
                            }



                        }
                        else
                        {
                            sb.Append("<td style='border-top:1px dashed black;'>" + (GrandTotalAmt.ToString() == "0" ? "" : GrandTotalAmt.ToString("0")) + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            //sb.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</b></td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DMDate"] + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["ShiftName"] + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DCode"] + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["DName"] + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[0].Rows[i]["TotalQty"] + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["Amount"]).ToString("0") + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["GST"]).ToString("0") + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                            sb.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");

                            GrandTotalAmt = 0;

                        }
                        if (i == (Count - 1))
                        {
                            sb.Append("<td style='border-top:1px dashed black;'>" + (GrandTotalAmt.ToString() == "0" ? "" : GrandTotalAmt.ToString("0")) + "</td>");
                            sb.Append("</tr>");
                        }

                    }


                    Amt = Convert.ToDecimal(ds5.Tables[0].Rows[i]["NetAmt"]).ToString("0");
                    Date = ds5.Tables[0].Rows[i]["DMDate"].ToString();
                    DCode = ds5.Tables[0].Rows[i]["DCode"].ToString();
                    DName = ds5.Tables[0].Rows[i]["DName"].ToString();
                }
                sb.Append("<tr>");
                //int ColCount = ds5.Tables[0].Columns.Count;
                //for (int i = 0; i <= ColCount; i++)
                //{
                //    if (i == 4)
                //    {
                sb.Append("<td colspan='5' style='border-top:1px dashed black; border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
                //  }
                //else if (i == 5)
                //{
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToInt32(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<int?>("TotalQty") ?? 0)) + "</b></td>");
                //}
                //else if (i == 6)
                //{
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)).ToString("0") + "</b></td>");
                //}
                //else if (i == 7)
                //{
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("GST") ?? 0)).ToString("0") + "</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("NetAmt") ?? 0)).ToString("0") + "</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("TCSTaxAmt") ?? 0)).ToString("0") + "</b></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
                //}
                //else
                //{

                //}
                // }
                sb.Append("</tr>");
                sb.Append("</table>");
                ViewState["Sb"] = sb.ToString();
                div_page_content.InnerHtml = sb.ToString();
                ViewState["GPandProduct"] = sb.ToString();
                //Print.InnerHtml= sb.ToString();
                // End of partywise D.M. - Gatepass Report

                // print for Product wise DM Sales Report

                StringBuilder sb1 = new StringBuilder();
                sb1.Append("<div class='pagebreak'></div>");
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb1.Append("</tr>");
                //sb1.Append("<tr>");
                //sb1.Append("<td style='text-align: left;'><b>Partywise D.M. wise -Gatepass Report</b></td>");
                //sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: left;'><b>Productwise Sales Report</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                //sb1.Append("<td style='padding: 2px 5px;'><b>Productwise Sales Report</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                int Count1 = ds5.Tables[1].Rows.Count;
                sb1.Append("<thead>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>S.No.</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>PRODUCT NAME</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>QUANTITY</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>AMOUNT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>GST AMT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>NET AMT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCSTAX AMT</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>GHEE (IN KG)</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>IN KG/LTR</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>WEIGHT TOTAL</b></td>");
                sb1.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>AMOUNT GRAND TOTAL</b></td>");
                sb1.Append("</thead>");
                decimal GrandTotalNetAmt = 0;
                decimal GrandTotalWeight = 0;
                string subsubsectionid = "";
                string SubNetAmount = "";
                string SubTotalWeight = "";
                for (int i = 0; i < Count1; i++)
                {
                    if (i == 0)
                    {
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + (i + 1).ToString() + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["ProductName"] + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["TotalQty"] + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["Amount"]).ToString("0") + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["GST"]).ToString("0") + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + (ds5.Tables[1].Rows[i]["GheeInKG"].ToString() == "0.00" ? "" : ds5.Tables[1].Rows[i]["GheeInKG"].ToString() + " KG") + "</td>");
                        sb1.Append("<td style='border-top:1px dashed black;'>" + ds5.Tables[1].Rows[i]["QtyInKGLtR"] + "</td>");
                        //sb1.Append("</tr>");
                    }
                    else
                    {
                        if (ds5.Tables[1].Rows[i]["MOrPSubSection_id"].ToString() == subsubsectionid)
                        {
                            if (GrandTotalNetAmt > 0 && GrandTotalWeight > 0)
                            {
                                sb1.Append("<td></td>");
                                sb1.Append("</tr>");
                                sb1.Append("<tr>");
                                sb1.Append("<td>" + (i + 1).ToString() + "</td>");
                                sb1.Append("<td>" + ds5.Tables[1].Rows[i]["ProductName"] + "</td>");
                                sb1.Append("<td>" + ds5.Tables[1].Rows[i]["TotalQty"] + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["Amount"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["GST"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + (ds5.Tables[1].Rows[i]["GheeInKG"].ToString() == "0.00" ? "" : ds5.Tables[1].Rows[i]["GheeInKG"].ToString() + " KG") + "</td>");
                                sb1.Append("<td>" + ds5.Tables[1].Rows[i]["QtyInKGLtR"] + "</td>");
                                GrandTotalWeight += Convert.ToDecimal(ds5.Tables[1].Rows[i]["QtyInKGLtR_value"]);
                                GrandTotalNetAmt += Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]);

                            }
                            else
                            {
                                if (i == 1)
                                {
                                    sb1.Append("<td style='border-top:1px dashed black;'>" + (GrandTotalWeight.ToString() == "0" ? "" : GrandTotalWeight.ToString()) + "</td>");
                                    sb1.Append("<td style='border-top:1px dashed black;'>" + (GrandTotalNetAmt.ToString() == "0" ? "" : GrandTotalNetAmt.ToString("0")) + "</td>");
                                }
                                else
                                {
                                    sb1.Append("<td>" + (GrandTotalWeight.ToString() == "0" ? "" : GrandTotalWeight.ToString()) + "</td>");
                                    sb1.Append("<td>" + (GrandTotalNetAmt.ToString() == "0" ? "" : GrandTotalNetAmt.ToString("0")) + "</td>");
                                }
                                sb1.Append("</tr>");
                                sb1.Append("<tr>");
                                sb1.Append("<td>" + (i + 1).ToString() + "</td>");
                                sb1.Append("<td>" + ds5.Tables[1].Rows[i]["ProductName"] + "</td>");
                                sb1.Append("<td>" + ds5.Tables[1].Rows[i]["TotalQty"] + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["Amount"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["GST"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");
                                sb1.Append("<td>" + (ds5.Tables[1].Rows[i]["GheeInKG"].ToString() == "0.00" ? "" : ds5.Tables[1].Rows[i]["GheeInKG"].ToString() + " KG") + "</td>");
                                sb1.Append("<td>" + ds5.Tables[1].Rows[i]["QtyInKGLtR"] + "</td>");
                                GrandTotalWeight += (Convert.ToDecimal(SubTotalWeight) + Convert.ToDecimal(ds5.Tables[1].Rows[i]["QtyInKGLtR_value"]));
                                GrandTotalNetAmt += (Convert.ToDecimal(SubNetAmount) + Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]));
                            }
                        }
                        else
                        {
                            sb1.Append("<td>" + (GrandTotalWeight.ToString() == "0" ? "" : GrandTotalWeight.ToString()) + "</td>");
                            sb1.Append("<td>" + (GrandTotalNetAmt.ToString() == "0" ? "" : GrandTotalNetAmt.ToString("0")) + "</td>");
                            sb1.Append("</tr>");
                            sb1.Append("<tr>");
                            sb1.Append("<td>" + (i + 1).ToString() + "</td>");
                            sb1.Append("<td>" + ds5.Tables[1].Rows[i]["ProductName"] + "</td>");
                            sb1.Append("<td>" + ds5.Tables[1].Rows[i]["TotalQty"] + "</td>");
                            sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["Amount"]).ToString("0") + "</td>");
                            sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["GST"]).ToString("0") + "</td>");
                            sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]).ToString("0") + "</td>");
                            sb1.Append("<td>" + Convert.ToDecimal(ds5.Tables[1].Rows[i]["TCSTaxAmt"]).ToString("0") + "</td>");
                            sb1.Append("<td>" + (ds5.Tables[1].Rows[i]["GheeInKG"].ToString() == "0.00" ? "" : ds5.Tables[1].Rows[i]["GheeInKG"].ToString() + " KG") + "</td>");
                            sb1.Append("<td>" + ds5.Tables[1].Rows[i]["QtyInKGLtR"] + "</td>");
                            //if (ds5.Tables[1].Rows[i]["idcount"].ToString() == "1")
                            //{
                            //sb1.Append("<td>" + ds5.Tables[1].Rows[i]["QtyInKGLtR_value"] + "</td>");
                            //}


                            GrandTotalWeight = 0;
                            GrandTotalNetAmt = 0;
                        }
                        if (i == (Count1 - 1))
                        {
                            sb1.Append("<td>" + (GrandTotalWeight.ToString() == "0" ? "" : GrandTotalWeight.ToString()) + "</td>");
                            sb1.Append("<td>" + (GrandTotalNetAmt.ToString() == "0" ? "" : GrandTotalNetAmt.ToString("0")) + "</td>");
                            sb1.Append("</tr>");
                        }

                    }

                    SubTotalWeight = Convert.ToDecimal(ds5.Tables[1].Rows[i]["QtyInKGLtR_value"]).ToString();
                    SubNetAmount = Convert.ToDecimal(ds5.Tables[1].Rows[i]["NetAmt"]).ToString("0");
                    subsubsectionid = ds5.Tables[1].Rows[i]["MOrPSubSection_id"].ToString();


                }
                sb1.Append("<tr>");
                int ColCount1 = ds5.Tables[1].Columns.Count;
                for (int i = 0; i <= ColCount1; i++)
                {
                    if (i == 1)
                    {
                        sb1.Append("<td colspan='2' style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Grand Total</b></td>");
                    }
                    else if (i == 2)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToInt32(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<int?>("TotalQty") ?? 0)) + "</b></td>");
                    }
                    else if (i == 3)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("Amount") ?? 0)).ToString("0") + "</b></td>");
                    }
                    else if (i == 4)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("GST") ?? 0)).ToString("0") + "</b></td>");
                    }
                    else if (i == 5)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("NetAmt") ?? 0)).ToString("0") + "</b></td>");
                    }
                    else if (i == 6)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("TCSTaxAmt") ?? 0)).ToString("0") + "</b></td>");
                    }
                    else if (i == 7)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + Convert.ToDecimal(ds5.Tables[1].AsEnumerable().Sum(r => r.Field<decimal?>("GheeInKG") ?? 0)).ToString("0.00") + "</b></td>");
                    }
                    else if (i == 8)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 9)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 10)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else
                    {

                    }
                }
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                //  int ColCount1 = ds5.Tables[1].Columns.Count;
                for (int i = 0; i <= ColCount1; i++)
                {
                    if (i == 1)
                    {
                        sb1.Append("<td colspan='2' style='border-top:1px dashed black;border-bottom:1px dashed black;text-align:right;'><b>Ghee Grand Total</b></td>");
                    }
                    else if (i == 2)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 3)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 4)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 5)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 6)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 7)
                    {
                        if (ds5.Tables.Count > 1)
                        {
                            if (ds5.Tables[2].Rows.Count > 0)
                            {
                                sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'><b>" + ds5.Tables[2].Rows[0]["Gheetotal"].ToString() + "</b></td>");
                            }
                        }
                    }
                    else if (i == 8)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 9)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else if (i == 10)
                    {
                        sb1.Append("<td style='border-top:1px dashed black;border-bottom:1px dashed black;'></td>");
                    }
                    else
                    {

                    }
                }

                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_contnt2.InnerHtml = sb1.ToString();
                ViewState["GPandProduct"] += sb1.ToString();
                Print.InnerHtml = ViewState["GPandProduct"].ToString();
                // End of Product wise DM Sales Report
            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["GPandProduct"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
                div_page_contnt2.InnerHtml = "";
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
        ViewState["GPandProduct"] = "";
        div_page_content.InnerHtml = "";
        div_page_contnt2.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        Print.InnerHtml = ViewState["GPandProduct"].ToString();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "Location-" + ddlLocation.SelectedItem.Text + DateTime.Now + ".xls");
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