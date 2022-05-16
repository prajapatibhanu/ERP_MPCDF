using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_DistInvoice_GDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();



    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = Date;
                txtDeliveryDate.Attributes.Add("readonly", "readonly");
                GetLocation();
                GetShift();
                GetDisOrSS();
              //  GetCategory();

                GetOfficeDetails();
                ViewState["DistData"] = "";
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

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
    protected void GetOfficeDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply"
                                   , new string[] { "flag", "Office_ID" }
                                   , new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_GST"] = ds2.Tables[0].Rows[0]["Office_Gst"].ToString();
                ViewState["Office_Address"] = ds2.Tables[0].Rows[0]["Office_Address"].ToString();
                ViewState["Office_Address1"] = ds2.Tables[0].Rows[0]["Office_Address1"].ToString();
                ViewState["Office_Pincode"] = ds2.Tables[0].Rows[0]["Office_Pincode"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
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


    private void GetDisOrSS()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "8", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlDitributor.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }

    #endregion========================================================
    #region=========== init or changed even===========================



    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ViewState["DistData"] = "";
            FillDistributorDetails();

        }
    }

    private void FillDistributorDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            div_page_content.InnerHtml = "";
            string distid = "";
            ViewState["PStatus"] = "";
            foreach (ListItem item in ddlDitributor.Items)
            {
                if (item.Selected)
                {
                    distid = item.Value;
                    GetTcsTax(distid);
                    GetInvoiceDetails(Convert.ToInt32(distid));
                }
            }
            if (ViewState["DistData"].ToString() != "" && ViewState["DistData"].ToString() != null && ViewState["DistData"].ToString() != "0")
            {
                btnPrint.Visible = true;
                div_page_content.InnerHtml = ViewState["DistData"].ToString();
                ViewState["DistData"] = "";
            }
            else
            {
                btnPrint.Visible = false;
                div_page_content.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                return;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    private void GetInvoiceDetails(int distid)
    {
        try
        {
            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                         new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "DistributorId", "OrganizationId", "AreaId" },
                           new string[] { "9", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), distid.ToString(), "0", ddlLocation.SelectedValue }, "dataset");
            string pstatus = "";
            if (ds6.Tables[0].Rows.Count > 0 && ds6.Tables[0].Rows[0]["Msg"].ToString() == "Found")
            {

                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0, paybleAmt = 0, totaladvAmt = 0, totalAdvCrdCmmAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0, tcstamt = 0, fpaybleamt = 0;

                if (ViewState["PStatus"].ToString() != "")
                {
                    sb.Append("<p style='page-break-after: always'>");
                }

                sb.Append("<div class='content' style='border: 1px solid black'>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='4' class='text-left'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:16px;'><b>" + ViewState["Office_Name"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td colspan='2' class='text-center' style='font-size:12px;'><b>Office : </b>" + ViewState["Office_Address"].ToString() + " - " + ViewState["Office_Pincode"].ToString() + "</br><b>Plant : </b>" + ViewState["Office_Address1"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:13px;'><b>Bill Book</b></td><td class='blank_td' style='width: 250px;'></td>");
                
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' class='text-center' style='font-size:13px;'><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>No" + txtDeliveryDate.Text + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>M/s:" + ds6.Tables[0].Rows[0]["DName"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Add:" + ds6.Tables[0].Rows[0]["DAddress"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>GST No.: " + ds6.Tables[0].Rows[0]["GSTNo"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Shift :" + ddlShift.SelectedItem.Text + "</td><td class='blank_td' style='width: 250px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
                sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>Particulars</td>");
                sb.Append("<td>Qty(In Pkt)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty(In Ltr.)</td>");
                sb.Append("<td>Adv. Card Margin</td>");
                sb.Append("<td>Adv. Card Margin Amt</td>");
                sb.Append("<td>Inst. Qty(In Pkt)</td>");
                sb.Append("<td>Inst. Qty(In Ltr.)</td>");
                sb.Append("<td>Inst. Margin(Per Ltr.)</td>");
                sb.Append("<td>Inst. Tran Margin Amt</td>");
                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr.)</td>");
                sb.Append("<td>Amount</td>");
                sb.Append("<td>Net Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardComm"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("<td>" + (Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"])) + "</td>");
                    sb.Append("</tr>");

                    totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
                    totalAdvCrdCmmAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
                    paybleAmt += ((Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"])));
                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count;

              //  for (int i = 0; i < ColumnCount - 9; i++) // privious
                    for (int i = 0; i < ColumnCount - 15; i++)
                    {
                        if (i == 0)
                        {
                            sb.Append("<td><b>Total</b></td>");
                        }
                        else if (i == ColumnCount - 25)
                        {
                            sb.Append("<td><b>" + totalAdvCrdCmmAmt.ToString("0.000") + "</b></td>");
                        }
                        else if (i == ColumnCount - 21)
                        {
                            sb.Append("<td><b>" + totalInstTranCommAmt.ToString("0.000") + "</b></td>");
                        }
                        else if (i == ColumnCount - 17)
                        {
                            sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                        }

                        else if (i == ColumnCount - 16)
                        {
                            sb.Append("<td><b>" + paybleAmt.ToString("0.000") + "</b></td>");
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }



                    }
                sb.Append("</tr>");
                sb.Append("<tr>");
                tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()))/100);
                fpaybleamt = paybleAmt + tcstamt;
                sb.Append("<td><b>Tcs on Sales @</b>" + (ViewState["Tval"].ToString() != "0.000" ? ViewState["Tval"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + (ViewState["Tval"].ToString() != "0.000" ? tcstamt.ToString("0.000") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + fpaybleamt.ToString("0.000") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-right' colspan='2'</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='1'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("<td class='text-right'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='1'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("<td class='text-right'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");

                ViewState["DistData"] += sb.ToString();
                ViewState["PStatus"] = "1";
                ////////////////End Of Route Wise Print Code   ///////////////////////
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }

    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetDisOrSS();
        }
    }

    private void GetTcsTax(string distid)
    {
        try
        {
            if (distid != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds5 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deliverydate, distid }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds5.Tables[0].Rows[0]["Tval"].ToString();
                }
                else
                {
                    ViewState["Tval"] = "0.000";
                }
            }
            else
            {
                ViewState["Tval"] = "0.000";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    #endregion===========================
}