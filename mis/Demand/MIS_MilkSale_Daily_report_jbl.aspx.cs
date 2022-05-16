using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_MIS_MilkSale_Daily_report_jbl : System.Web.UI.Page
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
                //GetLocation();
                //GetShift();
                //GetCategory();

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

    //protected void GetLocation()
    //{
    //    try
    //    {
    //        ddlLocation.DataTextField = "AreaName";
    //        ddlLocation.DataValueField = "AreaId";
    //        ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
    //             new string[] { "flag" },
    //               new string[] { "1" }, "dataset");
    //        ddlLocation.DataBind();
    //        ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //}
    protected void GetOfficeDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string Officeid = objdb.Office_ID();
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
    //protected void GetShift()
    //{
    //    try
    //    {


    //        ddlShift.DataTextField = "ShiftName";
    //        ddlShift.DataValueField = "Shift_id";
    //        ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
    //             new string[] { "flag" },
    //               new string[] { "1" }, "dataset");
    //        ddlShift.DataBind();
    //        ddlShift.Items.Insert(0, new ListItem("All", "0"));


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
    //    }
    //}


    //private void GetDisOrSS()
    //{
    //    try
    //    {
    //        ddlDitributor.DataTextField = "DTName";
    //        ddlDitributor.DataValueField = "DistributorId";
    //        ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //             new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
    //               new string[] { "8", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
    //        ddlDitributor.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
    //    }
    //}
    //private void GetDisOrSS()
    //{
    //    try
    //    {
    //        ddlDitributor.DataTextField = "DTName";
    //        ddlDitributor.DataValueField = "DistributorId";
    //        ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorReg",
    //             new string[] { "flag", "Office_ID" },
    //               new string[] { "1", objdb.Office_ID() }, "dataset");
    //        ddlDitributor.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
    //    }
    //}
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



    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";
            ViewState["DistData"] = "";
            GetInvoiceDetails();
            //  FillDistributorDetails();
        }
    }

    //private void FillDistributorDetails()
    //{
    //    try
    //    {
    //        lblMsg.Text = string.Empty;
    //        div_page_content.InnerHtml = "";
    //        string distid = "";
    //        ViewState["PStatus"] = "";
    //        foreach (ListItem item in ddlDitributor.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                distid = item.Value;
    //                GetTcsTax(distid);
    //                GetInvoiceDetails(Convert.ToInt32(distid));
    //            }
    //        }
    //        if (ViewState["DistData"].ToString() != "" && ViewState["DistData"].ToString() != null && ViewState["DistData"].ToString() != "0")
    //        {
    //            btnPrint.Visible = true;
    //            div_page_content.InnerHtml = ViewState["DistData"].ToString();
    //            ViewState["DistData"] = "";
    //        }
    //        else
    //        {
    //            btnPrint.Visible = false;
    //            div_page_content.InnerHtml = "";
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
    //            return;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
    //    }
    //}
    private void GetInvoiceDetails()
    {
        try
        {
            DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string daydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);



            ds6 = objdb.ByProcedure("USP_Trn_Milksale_report_DAYWISE_jbl",
                         new string[] { "flag", "Daydate", "Office_ID" },
                           new string[] { "0", daydate.ToString(), objdb.Office_ID() }, "dataset");
            string pstatus = "";
            if (ds6.Tables.Count > 1 && ds6.Tables[1].Rows[0]["Msg"].ToString() == "Found")
            {

                ////////////////Start Of Route Wise Print Code   ///////////////////////
                
                StringBuilder sball = new StringBuilder();
                decimal Tot_CashAmount = 0,  Total_QTYInLtr = 0, total_TotalAmt = 0, Total_Transport= 0;
                int Total_ReturnQty = 0, Total_cashqty = 0, Total_AdvCardQty = 0, Total_InstQty = 0, Total_QTY = 0, total_qty_withleakqty = 0;

                //if (ViewState["PStatus"].ToString() != "")
                //{
                //    sball.Append("<p style='page-break-after: always'>");
                //}

                //morning
                sball.Append("<div class='table-responsive'>");
                sball.Append("<div  style='border: 0px solid black'>");
                sball.Append("<table class='table1' style='width:100%; height:100%;'>");
                sball.Append("<tr>");
                //sball.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sball.Append("<td style='text-align:Center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sball.Append("</tr>");
                 sball.Append("<tr>");
                sball.Append("<td></td>");
                
                sball.Append("</tr>");

                sball.Append("<tr>");
                sball.Append("<td style='text-align:left'><b>Daily Sales Report Of Main Dairy Plant, Jabalpur Of " + txtDate.Text + "</b></td>");

               

                sball.Append("<tr>");
                sball.Append("<td></td>");
                
                sball.Append("</tr>");
                sball.Append("<tr>");
                sball.Append("<td></td>");

                sball.Append("</tr>");
                sball.Append("<tr>");
                sball.Append("<td></td>");

                sball.Append("</tr>");

                sball.Append("</table>");
                sball.Append("<table class='table table1-bordered' > ");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sball.Append("<thead style='padding-left:0px;'>");
                sball.Append("<td style='width:120px'>Particulars</td>");
                sball.Append("<td>Leakage Pkt's</td>");
                sball.Append("<td>Pkt's With Leakage</td>");
               
                sball.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                //sball.Append("<td>Adv. Card Amount</td>");
                sball.Append("<td>Inst. Qty</td>");
                sball.Append("<td>Transport Chages</td>");
                sball.Append("<td>Cash Qty (In Pkt.)</td>");
                sball.Append("<td>Cash Amount</td>");
                sball.Append("<td>Total Sale Qty(In Ltr)</td>");
                sball.Append("<td>Total Sale in Ltr.</td>");
                sball.Append("<td>Total Payble Amt.</td>");
               


                sball.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sball.Append("<tr>");
                    sball.Append("<td style='text-align:left'>" + ds6.Tables[0].Rows[i]["ItemName"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                    sball.Append("<td>" + Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalReturnQty"]) + Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalQTY"]) + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sball.Append("<td >" + ds6.Tables[0].Rows[i]["TotalInstQty"] + "</td>");
                    //sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalQTY"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["Transport"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["cashqty"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["CashAmount"] + "</td>");
                    //sball.Append("<td >" + ds6.Tables[0].Rows[i]["TotalInstAmt"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalQTY"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalQTYInLtr"] + "</td>");
                    //sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAmt"] + "</td>");
                    sball.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                    sball.Append("</tr>");


                    Total_ReturnQty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalReturnQty"]);
                    total_qty_withleakqty += (Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalReturnQty"]) + Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalQTY"]));
                   
                    Total_AdvCardQty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalAdvCardQty"]);
                    Total_InstQty += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalInstQty"]);
                    Total_Transport += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Transport"]);
                    Total_cashqty += Convert.ToInt32(ds6.Tables[0].Rows[i]["cashqty"]);
                    Tot_CashAmount += Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]);
                    Total_QTY += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalQTY"]);
                    Total_QTYInLtr += Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalQTYInLtr"]);
                    total_TotalAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);
                    
                    //Tot_AdvCardAmt_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    //Total_InstAmt_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstAmt"]);
                    
                    
                    

                }
                sball.Append("<tr>");
                // int ColumnCount = ds6.Tables[0].Columns.Count - 13;


                for (int i = 0; i < ColCount; i++)
                {
                    if (i == 0)
                    {
                        sball.Append("<td style='text-align:left'><b>Total</b></td>");
                    }
                    else if (i == 1)
                    {
                        sball.Append("<td><b>" + Total_ReturnQty.ToString() + "</b></td>");
                    }
                    else if (i == 2)
                    {
                        sball.Append("<td><b>" + total_qty_withleakqty.ToString() + "</b></td>");
                    }
                    else if (i == 3)
                    {
                        sball.Append("<td><b>" + Total_AdvCardQty.ToString() + "</b></td>");
                    }
                    else if (i == 4)
                    {
                        sball.Append("<td><b>" + Total_InstQty.ToString() + "</b></td>");
                    }
                    else if (i == 5)
                    {
                        sball.Append("<td><b>" + Total_Transport.ToString("0.00") + "</b></td>");
                    }
                    else if (i == 6)
                    {
                        sball.Append("<td><b>" + Total_cashqty.ToString("0.00") + "</b></td>");
                    }
                    else if (i == 7)
                    {
                        sball.Append("<td><b>" + Tot_CashAmount.ToString() + "</b></td>");
                    }
                    else if (i == 8)
                    {
                        sball.Append("<td><b>" + Total_QTY.ToString("0.00") + "</b></td>");
                    }
                    else if (i == 9)
                    {
                        sball.Append("<td><b>" + Total_QTYInLtr.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 10)
                    {
                        sball.Append("<td><b>" + total_TotalAmt.ToString("0.000") + "</b></td>");
                    }
                    //else if (i == 11)
                    //{
                    //    sball.Append("<td><b>" + Total_Transport.ToString() + "</b></td>");
                    //}
                    //else if (i == 12)
                    //{
                    //    sball.Append("<td><b>" + total_Gatepassqty.ToString() + "</b></td>");
                    //}



                }
                sball.Append("</tr>");

                sball.Append("</table>");

                sball.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
                sball.Append("<tr>");
                //sball.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sball.Append("<td style='text-align:Center'><b>Prepared By<b></td>");
                sball.Append("<td style='text-align:Center'><b>Mgr.(MARKETING)<b></td>");
                sball.Append("<td style='text-align:Center'><b>Mgr (Fin)<b></td>");
                sball.Append("<td style='text-align:Center'><b>Mgr/I/c (PO)<b></td>");
                sball.Append("</tr>");

                sball.Append("</table>");
                sball.Append("</div>");
                sball.Append("</div>");




                
                
                div_page_content_All.InnerHtml = sball.ToString();
                btnPrint.Visible = true;
                //ViewState["DistData"] += sball.ToString();
                //ViewState["PStatus"] = "1";
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
    //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLocation.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
    //    {
    //        lblMsg.Text = string.Empty;
    //        GetDisOrSS();
    //    }
    //}

    private void GetTcsTax(string distid)
    {
        try
        {
            if (distid != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
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