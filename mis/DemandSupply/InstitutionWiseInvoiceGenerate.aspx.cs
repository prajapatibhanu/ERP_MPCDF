using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_InstitutionWiseInvoiceGenerate : System.Web.UI.Page
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
               // GetCategory();
                GetRoute();
               // GetInstitution();
                GetOfficeDetails();
                ViewState["InstData"] = "";
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    private void GetRoute()
    {
        
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag","AreaId", "Office_ID", "ItemCat_id" },
                 new string[] { "9","0", objdb.Office_ID(), "3" }, "dataset");
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
               ddlRoute.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
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

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
    }
    //protected void GetCategory()
    //{
    //    try
    //    {
    //        ddlItemCategory.DataTextField = "ItemCatName";
    //        ddlItemCategory.DataValueField = "ItemCat_id";
    //        ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
    //             new string[] { "flag" },
    //               new string[] { "1" }, "dataset");
    //        ddlItemCategory.DataBind();
    //        ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
    //        ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //}

    #endregion========================================================
    #region=========== init or changed even===========================
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInstitution();
    }


    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }

    private void FillInstitutionDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            div_page_content.InnerHtml = "";
            string InstId = "";
            ViewState["PStatus"] = "";
            foreach (ListItem item in ddlInstitution.Items)
            {
                if (item.Selected)
                {
                    InstId = item.Value;
                    GetInvoiceDetails(Convert.ToInt32(InstId));
                }
            }
            if (ViewState["InstData"].ToString() != "" && ViewState["InstData"].ToString() != null && ViewState["InstData"].ToString() != "0")
            {
                btnPrint.Visible = true;
                div_page_content.InnerHtml = ViewState["InstData"].ToString();
                ViewState["InstData"] = "";
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
    private void GetInvoiceDetails(int InstId)
    {
        try
        {
           // string fm = "01/" + txtMonth.Text;

            //DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            //string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            //DateTime lastDayOfMonth = fmonth.AddMonths(1).AddDays(-1);


            //string LastDayofMnth = lastDayOfMonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tmonth = DateTime.ParseExact(LastDayofMnth, "dd/MM/yyyy", culture);
            //string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            //string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                         new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "Office_ID", "OrganizationId", "RouteId" },
                           new string[] { "4", fromdat, todat, objdb.GetMilkCatId(), objdb.Office_ID(), InstId.ToString(), ddlRoute.SelectedValue }, "dataset");
            string pstatus = "";
            if (ds6.Tables[0].Rows.Count > 0)
            {
               
                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                decimal totalamt = 0;

                if (ViewState["PStatus"].ToString() != "")
                {
                    sb.Append("<p style='page-break-after: always'>");
                }

                sb.Append("<div class='content' style='border: 1px solid black'>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td class='text-left'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding-left:50px;'><b>Bill Book</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>M/s  :-" + ds6.Tables[0].Rows[0]["BName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left'>" + txtFromDate.Text + "-" + txtToDate.Text + "</td>");
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
                sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sb.Append("<td>Return Qty (In Pkt.)</td>");
                sb.Append("<td>Adv. Card Qty (In Ltr.)</td>");
                sb.Append("<td>Billing Qty(In Pkt.)</td>");
                sb.Append("<td>Billing Qty(In Ltr.)</td>");
                sb.Append("<td>Rate (Per Ltr.)</td>");
                sb.Append("<td>Payble Amount</td>");
                sb.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                    sb.Append("<td>" + ds6.Tables[0].Rows[i]["Amount"] + "</td>");
                    sb.Append("</tr>");

                  
                    totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Amount"]);
                }
                sb.Append("<tr>");
                int ColumnCount = ds6.Tables[0].Columns.Count;

                for (int i = 0; i < ColumnCount - 8; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td><b>Total</b></td>");
                    }
                    else if (i == ColumnCount - 9)
                    {
                        sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='text-left'>Prepared & Checked by</td>");
                sb.Append("<td style='padding-left:270px;'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");

                ViewState["InstData"] += sb.ToString();
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
                FillInstitutionDetails();
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
    #endregion===========================
}