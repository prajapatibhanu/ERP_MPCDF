using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_Trn_MilkInvoiceDetailsRuntime_JDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtMonth.Attributes.Add("readonly", "readonly");
                GetOfficeDetails();
                GetLocation();
                GetDisOrSS();
                ViewState["DistData"] = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_GST"] = ds2.Tables[0].Rows[0]["Office_Gst"].ToString();
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDisOrSS();
    }
    private void GetDisOrSS()
    {
        try
        {

            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "8", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataBind();
            ddlDitributor.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }

    private void GetDisdetails()
    {
        try
        {
            objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "8", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }

    #endregion========================================================

    #region=========== click event for grdiview item command event===========================
    private void GetMilkInvoiceDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            GridView1.DataSource = null;
            GridView1.DataBind();

            string fm = txtMonth.Text;
            string[] my = fm.Split('/');
            ds1 = objdb.ByProcedure("USP_Trn_DateRangeWiseRunTimeMilkInvoice_JDS",
                 new string[] { "flag", "DistributorId", "FromMonth", "FromYear", },
                   new string[] { "2", ddlDitributor.SelectedValue, my[0], my[1] }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetMilkInvoiceDetails();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordPrint")
            {
                ds1 = objdb.ByProcedure("USP_Trn_DateRangeWiseRunTimeMilkInvoice_JDS",
                                new string[] { "Flag", "Invoice_Rid", "Office_ID" },
                                new string[] { "3", e.CommandArgument.ToString(),objdb.Office_ID() }, "TableSave");

                if (ds1.Tables[0].Rows.Count>0)
                {
                    PrintNew(ds1.Tables[0]);
                    
                }
                GetDatatableHeaderDesign();
            }
            else if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_Trn_DateRangeWiseRunTimeMilkInvoice_JDS",
                                new string[] { "flag", "Invoice_Rid", "CreatedBy", "CreatedByIP" },
                                new string[] { "4", e.CommandArgument.ToString(),objdb.createdBy(), IPAddress }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();                        
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetMilkInvoiceDetails();
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds1.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void PrintNew(DataTable dt6)
    {
        DataSet ds6 = new DataSet();
        ds6.Merge(dt6);
        StringBuilder sb = new StringBuilder();

        Print.InnerHtml = "";
        sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
        sb.Append("<tr>");
        sb.Append("<td rowspan='4' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
        sb.Append("<td class='text-left'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='padding-left:50px;'><b>Bill Book</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td><b>&nbsp;&nbsp;&nbsp;&nbsp;FSSAI Lic No.: 10013026000522<b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>Invoice No -" + ds6.Tables[0].Rows[0]["InvoiceNo_R"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>M/s  :-" + ds6.Tables[0].Rows[0]["DName"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='2'>Shift :-" + ds6.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left'> Time Period" + ds6.Tables[0].Rows[0]["FromDate"].ToString() + " - " + ds6.Tables[0].Rows[0]["ToDate"].ToString() + "</td>");
        sb.Append("<td class='text-right'>" + ds6.Tables[0].Rows[0]["RName"].ToString() + "</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("</tr>");

        sb.Append("</table>");
        sb.Append("<table class='table table1-bordered' > ");
        int Count = ds6.Tables[0].Rows.Count;
        int ColCount = ds6.Tables[0].Columns.Count;
        sb.Append("<thead style='padding-left:0px;'>");
        sb.Append("<td style='width:120px'>Particulars</td>");
        sb.Append("<td>Qty(In Pkt)</td>");
        sb.Append("<td>Qty(In Ltr.)</td>");
        sb.Append("<td>Return Qty (In Pkt.)</td>");
        sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
        sb.Append("<td>Adv. Card Price</td>");
        sb.Append("<td>Total Adv. Card Amt</td>");
        sb.Append("<td>Inst. Qty (In Pkt.)</td>");
        sb.Append("<td>Inst. Total Amt</td>");
        sb.Append("<td>Billing Qty(In Pkt.)</td>");
        sb.Append("<td>Billing Qty(In Ltr.)</td>");
        sb.Append("<td>Rate (Per Ltr.)</td>");
        sb.Append("<td>Amount</td>");
        sb.Append("<td>Payble Amount</td>");
        sb.Append("</thead>");
        decimal paybleAmt = 0, totaladvAmt = 0, totalcashsale = 0;
        for (int i = 0; i < Count; i++)
        {
            sb.Append("<tr>");
            sb.Append("<td style='text-align:left'>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardPrice"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardAmt"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalInstSupplyQty"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTotalAmt"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["Amount"] + "</td>");
            sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
            sb.Append("</tr>");

            paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);
            totaladvAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardAmt"]);
            totalcashsale += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
        }
        sb.Append("<tr>");
        sb.Append("<td style='text-align:left'><b>Tcs on Sales @</b>" + (ds6.Tables[0].Rows[0]["TcsPer"] != "0.000" ? ds6.Tables[0].Rows[0]["TcsPer"].ToString() : "NA") + "</td>");
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
        sb.Append("<td><b>" + (ds6.Tables[0].Rows[0]["TcsPer"].ToString() != "0.000" ? ds6.Tables[0].Rows[0]["TcsAmount"] : "NA") + "</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='text-align:left'><b>Grand Total</b></td>");
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
        sb.Append("<td>Amount For Cheque Preparation</td>");
        sb.Append("<td><b>" + (Convert.ToDecimal(ds6.Tables[0].Rows[0]["TcsAmount"]) + paybleAmt).ToString("0.000") + "</b></td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");

        sb.Append("<tr>");
        sb.Append("<td style='text-left;width:250px'>Prepared by</td>");
        sb.Append("<td style='text-right'><b>Advance Card Total</b></td>");
        sb.Append("<td style='text-right'><b>" + totaladvAmt.ToString("0.00") + "</b></td>");
        sb.Append("<td style='display:none' class='text-right'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td></td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td style='text-left'></td>");
        sb.Append("<td style='text-right'><b>Cash Milk Sale Total</b></td>");
        sb.Append("<td style='text-right'><b>" + totalcashsale.ToString("0.00") + "</b></td>");
        sb.Append("<td  style='display:none' class='text-right' ><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td style='text-left'></td>");
        sb.Append("<td style='text-right'><b>Total Milk Sale Amount</b></td>");
        sb.Append("<td style='text-right'><b>" + (totaladvAmt + totalcashsale).ToString("0.00") + "</b></td>");
        sb.Append("<td class='text-center' ></td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td style='text-left;width:250px'>Checked By</td>");
        sb.Append("<td style='text-right'><b>Transportation Charges(-)</b></td>");
        sb.Append("<td style='text-right'><b>" + ds6.Tables[0].Rows[0]["TransportationChargeAmt"].ToString() + "</b></td>");
        sb.Append("<td class='text-center' ></td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td style='text-left;width:250px'></td>");
        sb.Append("<td style='text-right'><b>Net Milk Sale Amount</b></td>");
        sb.Append("<td style='text-right'><b>" + ds6.Tables[0].Rows[0]["NetMilkSaleAmt"].ToString() + "</b></td>");
        sb.Append("<td ></td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td></td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td style='text-left'></td>");
        sb.Append("<td style='text-right'><b>Total Product Sale Amount(+)</b></td>");
        sb.Append("<td style='text-right'><b>" + 0.00 + "</b></td>");
        sb.Append("<td class='text-right' ><b></b></td>");
        sb.Append("</tr>");

        //Net Receivable Amount=Net Milk Sale Amount+Total Product Sale Amount
        sb.Append("<tr>");
        sb.Append("<td style='text-left'></td>");
        sb.Append("<td style='text-right'><b>Net Receivable Amount</b></td>");
        sb.Append("<td style='text-right'><b>" + ds6.Tables[0].Rows[0]["NetMilkSaleAmt"].ToString() + "</b></td>");
        sb.Append("<td class='text-center' ></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='4'>Note:</td>");
        sb.Append("<td class='text-right' colspan='4'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='4'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
        sb.Append("<td class='text-right' colspan='4'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='4'>2 . Please quote our Bill No. while remiting the amount.</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td class='text-left' colspan='4'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Print.InnerHtml = sb.ToString();


        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
    #endregion===========================
}