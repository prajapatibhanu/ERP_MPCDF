using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
public partial class mis_Demand_MIS_MilkSale_report_datewise_jbl : System.Web.UI.Page
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
                GetLocation();
                GetShift();
                GetCategory();
                GetDisOrSS();
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
                   new string[] { "8", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlDitributor.DataBind();
           // ddlDitributor.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    //private void GetDisOrSS()
    //{
    //    try
    //    {
    //        ddlDitributor.DataTextField = "DTName";
    //        ddlDitributor.DataValueField = "DistributorId";
    //        ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //             new string[] { "flag", "Office_ID", "ItemCat_id" },
    //               new string[] { "1", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
    //        ddlDitributor.DataBind();
    //        ddlDitributor.Items.Insert(0, new ListItem("All", "0"));


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
    //    }
    //}
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
            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
            DateTime odate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string From_date = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime Todate = DateTime.ParseExact(txttoDate.Text, "dd/MM/yyyy", culture);
            string To_date = Todate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string Ditributorid = "";
            int iddata = 0;
            foreach (ListItem Ditributor in ddlDitributor.Items)
            {
                if (Ditributor.Selected)
                {
                    ++iddata;
                    if (iddata == 1)
                    {
                        Ditributorid = Ditributor.Value;
                    }
                    else
                    {
                        Ditributorid += "," + Ditributor.Value;
                    }


                }
            }


            //ds6 = objdb.ByProcedure("USP_Trn_Milksale_report_datewise_jbl_new",
            //             new string[] { "flag", "From_Date", "To_Date", "Office_ID", "OrganizationId", "DelivaryShift_id", "ItemCat_id", "DistributorId", "AreaId" },
            //               new string[] { "0", From_date.ToString(), To_date.ToString(), objdb.Office_ID(), "0",ddlShift.SelectedValue,ddlItemCategory.SelectedValue,ddlDitributor.SelectedValue,ddlLocation.SelectedValue }, "dataset");
            ds6 = objdb.ByProcedure("USP_Trn_Milksale_report_datewise_jbl_new",
                         new string[] { "flag", "From_Date", "To_Date", "Office_ID", "OrganizationId", "DelivaryShift_id", "ItemCat_id", "MultiDistributorId", "AreaId" },
                           new string[] { "0", From_date.ToString(), To_date.ToString(), objdb.Office_ID(), "0", ddlShift.SelectedValue, ddlItemCategory.SelectedValue, Ditributorid, ddlLocation.SelectedValue }, "dataset");
            string pstatus = "";
          //  if (ds6.Tables.Count>3 && ds6.Tables[3].Rows[0]["Msg"].ToString() == "Found")
            if (ds6.Tables.Count > 1 && ds6.Tables[0].Rows.Count>0 && ds6.Tables[1].Rows[0]["Msg"].ToString() == "Found")
            {

                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sbm = new StringBuilder();
                //StringBuilder sbe = new StringBuilder();
                //StringBuilder sball = new StringBuilder();
                decimal Tot_CashAmount_mor = 0, Tot_AdvCardAmt_mor = 0, Total_InstAmt_mor = 0, Total_QTYInLtr_mor = 0, total_TotalAmt_mor = 0, Total_Transport_mor = 0;
                int Total_ReturnQty_mor = 0, Total_cashqty_mor = 0, Total_AdvCardQty_mor = 0, Total_InstQty_mor = 0, Total_QTY_mor = 0, total_Gatepassqty_mor = 0;

                //if (ViewState["PStatus"].ToString() != "")
                //{
                //    sball.Append("<p style='page-break-after: always'>");
                //}

                //morning
                sbm.Append("<div class='table-responsive'>");
                sbm.Append("<div  style='border: 0px solid black'>");
                sbm.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
                sbm.Append("<tr>");
                //sball.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sbm.Append("<td style='text-align:Center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sbm.Append("</tr>");
               
                sbm.Append("<tr>");
                if (ddlShift.SelectedValue == "0")
                {
                    sbm.Append("<td class='text-left'><b>Shift :- Day </b></td>");
                }
                if (ddlShift.SelectedValue == "1")
                {
                    sbm.Append("<td class='text-left'><b>Shift :- Morning</b></td>");
                }
                if (ddlShift.SelectedValue == "2")
                {
                    sbm.Append("<td class='text-left'><b>Shift :- Evening</b></td>");
                }
                sbm.Append("</tr>");
                sbm.Append("<tr>");
                sbm.Append("<td class='text-left'><b> Time Period :- " + txtFromDate.Text + " - " + txttoDate.Text + "</b></td>");

                sbm.Append("</tr>");

                sbm.Append("<tr>");
                sbm.Append("<td > </td>");

                sbm.Append("</tr>");
                //sbm.Append("<tr>");
                //sbm.Append("<td class='text-left'><b>Shift :- Morning </b></td>");
                //sbm.Append("<td class='text-right'></td>");
                //sbm.Append("</tr>");

                sbm.Append("<tr>");
                sbm.Append("<td></td>");
                sbm.Append("<td></td>");
                sbm.Append("<td></td>");
                sbm.Append("</tr>");

                sbm.Append("</table>");
                sbm.Append("<table class='table table1-bordered' > ");
                int Count = ds6.Tables[0].Rows.Count;
                int ColCount = ds6.Tables[0].Columns.Count;
                sbm.Append("<thead style='padding-left:0px;'>");
                sbm.Append("<td style='width:140px'>Particulars</td>");
                sbm.Append("<td>LeakQty</td>");
                sbm.Append("<td>Cash Qty</td>");
                sbm.Append("<td>Cash Amount (In Pkt.)</td>");
                sbm.Append("<td>Adv. Card Qty(In Pkt.)</td>");
                sbm.Append("<td>Adv. Card Amount</td>");
                sbm.Append("<td>Inst. Qty</td>");
                sbm.Append("<td>Inst. Amount</td>");
                sbm.Append("<td>Total Qty</td>");
                sbm.Append("<td>Total Qty(In Ltr)</td>");
                sbm.Append("<td>Total Amt.</td>");
                sbm.Append("<td>Transport</td>");
                sbm.Append("<td>Gatepass</td>");


                sbm.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sbm.Append("<tr>");
                    sbm.Append("<td style='text-align:left'>" + ds6.Tables[0].Rows[i]["ItemName"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["TotalLeakageQty"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["cashqty"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["CashAmount"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQty"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["AdvCardAmt"] + "</td>");
                    //sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                    sbm.Append("<td >" + ds6.Tables[0].Rows[i]["TotalInstQty"] + "</td>");
                    sbm.Append("<td >" + ds6.Tables[0].Rows[i]["TotalInstAmt"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["TotalQTY"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["TotalQTYInLtr"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAmt"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["Transport"] + "</td>");
                    sbm.Append("<td>" + ds6.Tables[0].Rows[i]["Gatepassqty"] + "</td>");

                    sbm.Append("</tr>");


                    Total_ReturnQty_mor += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalLeakageQty"]);
                    Total_cashqty_mor += Convert.ToInt32(ds6.Tables[0].Rows[i]["cashqty"]);
                    Total_AdvCardQty_mor += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalAdvCardQty"]);
                    Total_InstQty_mor += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalInstQty"]);
                    Total_QTY_mor += Convert.ToInt32(ds6.Tables[0].Rows[i]["TotalQTY"]);
                    total_Gatepassqty_mor += Convert.ToInt32(ds6.Tables[0].Rows[i]["Gatepassqty"]);

                    Tot_CashAmount_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["CashAmount"]);
                    Tot_AdvCardAmt_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                    Total_InstAmt_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstAmt"]);
                    Total_QTYInLtr_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalQTYInLtr"]);
                    total_TotalAmt_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAmt"]);
                    Total_Transport_mor += Convert.ToDecimal(ds6.Tables[0].Rows[i]["Transport"]);
                   
                }
                sbm.Append("<tr>");
               // int ColumnCount = ds6.Tables[0].Columns.Count - 13;


                for (int i = 0; i < ColCount; i++)
                {
                    if (i == 0)
                    {
                        sbm.Append("<td style='text-align:left'><b>Total</b></td>");
                    }
                    else if (i == 1)
                    {
                        sbm.Append("<td><b>" + Total_ReturnQty_mor.ToString() + "</b></td>");
                    }
                    else if (i == 2)
                    {
                        sbm.Append("<td><b>" + Total_cashqty_mor.ToString() + "</b></td>");
                    }
                    else if (i == 3)
                    {
                        sbm.Append("<td><b>" + Tot_CashAmount_mor.ToString() + "</b></td>");
                    }
                    else if (i == 4)
                    {
                        sbm.Append("<td><b>" + Total_AdvCardQty_mor.ToString() + "</b></td>");
                    }
                    else if (i == 5)
                    {
                        sbm.Append("<td><b>" + Tot_AdvCardAmt_mor.ToString("0.00") + "</b></td>");
                    }
                    else if (i == 6)
                    {
                        sbm.Append("<td><b>" + Total_InstQty_mor.ToString("0.00") + "</b></td>");
                    }
                    else if (i == 7)
                    {
                        sbm.Append("<td><b>" + Total_InstAmt_mor.ToString() + "</b></td>");
                    }
                    else if (i == 8)
                    {
                        sbm.Append("<td><b>" + Total_QTY_mor.ToString("0.00") + "</b></td>");
                    }
                    else if (i == 9)
                    {
                        sbm.Append("<td><b>" + Total_QTYInLtr_mor.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 10)
                    {
                        sbm.Append("<td><b>" + total_TotalAmt_mor.ToString("0.000") + "</b></td>");
                    }
                    else if (i == 11)
                    {
                        sbm.Append("<td><b>" + Total_Transport_mor.ToString() + "</b></td>");
                    }
                    else if (i == 12)
                    {
                        sbm.Append("<td><b>" + total_Gatepassqty_mor.ToString() + "</b></td>");
                    }
                   


                }
                sbm.Append("</tr>");

                sbm.Append("</table>");
                sbm.Append("</div>");
                sbm.Append("</div>");




               // //evening
               // decimal Tot_CashAmount_eve = 0, Tot_AdvCardAmt_eve = 0, Total_InstAmt_eve = 0, Total_QTYInLtr_eve = 0, total_TotalAmt_eve = 0, Total_Transport_eve = 0;
               // int Total_ReturnQty_eve = 0, Total_cashqty_eve = 0, Total_AdvCardQty_eve = 0, Total_InstQty_eve = 0, Total_QTY_eve = 0, total_Gatepassqty_eve = 0;

               // sbe.Append("<div class='table-responsive'>");
               // sbe.Append("<div  style='border: 0px solid black'>");
               // sbe.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
               // //sbe.Append("<tr>");
               // ////sball.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
               // //sbe.Append("<td style='text-align:Center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
               // //sbe.Append("</tr>");

               // //sbe.Append("<tr>");
               // //sbe.Append("<td style='text-align:Center'><b> Time Period :- " + txtFromDate.Text + " - " + txttoDate.Text + "</b></td>");

               // //sbe.Append("</tr>");

               // //sbe.Append("<tr>");
               // //sbe.Append("<td > </td>");

               // //sbe.Append("</tr>");
               // sbe.Append("<tr>");
               // sbe.Append("<td class='text-left'><b>Shift :- Evening </b></td>");
               
               // sbe.Append("</tr>");

               // sbe.Append("<tr>");
               // sbe.Append("<td></td>");
               // sbe.Append("<td></td>");
               // sbe.Append("<td></td>");
               // sbe.Append("</tr>");

               // sbe.Append("</table>");
               // sbe.Append("<table class='table table1-bordered'>");
               // int Count1 = ds6.Tables[1].Rows.Count;
               // int ColCount1 = ds6.Tables[1].Columns.Count;
               // sbe.Append("<thead style='padding-left:0px;'>");
               // sbe.Append("<td style='width:120px'>Particulars</td>");
               // sbe.Append("<td>LeakQty</td>");
               // sbe.Append("<td>Cash Qty</td>");
               // sbe.Append("<td>Cash Amount (In Pkt.)</td>");
               // sbe.Append("<td>Adv. Card Qty(In Pkt.)</td>");
               // sbe.Append("<td>Adv. Card Amount</td>");
               // sbe.Append("<td>Inst. Qty</td>");
               // sbe.Append("<td>Inst. Amount</td>");
               // sbe.Append("<td>Total Qty</td>");
               // sbe.Append("<td>Total Qty(In Ltr)</td>");
               // sbe.Append("<td>Total Amt.</td>");
               // sbe.Append("<td>Transport</td>");
               // sbe.Append("<td>Gatepass</td>");


               // sbe.Append("</thead>");

               // for (int i = 0; i < Count1; i++)
               // {

               //     sbe.Append("<tr>");
               //     sbe.Append("<td style='text-align:left'>" + ds6.Tables[1].Rows[i]["ItemName"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["TotalReturnQty"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["cashqty"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["CashAmount"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["TotalAdvCardQty"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["AdvCardAmt"] + "</td>");
               //     //sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
               //     sbe.Append("<td >" + ds6.Tables[1].Rows[i]["TotalInstQty"] + "</td>");
               //     sbe.Append("<td >" + ds6.Tables[1].Rows[i]["TotalInstAmt"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["TotalQTY"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["TotalQTYInLtr"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["TotalAmt"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["Transport"] + "</td>");
               //     sbe.Append("<td>" + ds6.Tables[1].Rows[i]["Gatepassqty"] + "</td>");

               //     sbe.Append("</tr>");


               //     Total_ReturnQty_eve += Convert.ToInt32(ds6.Tables[1].Rows[i]["TotalReturnQty"]);
               //     Total_cashqty_eve += Convert.ToInt32(ds6.Tables[1].Rows[i]["cashqty"]);
               //     Total_AdvCardQty_eve += Convert.ToInt32(ds6.Tables[1].Rows[i]["TotalAdvCardQty"]);
               //     Total_InstQty_eve += Convert.ToInt32(ds6.Tables[1].Rows[i]["TotalInstQty"]);
               //     Total_QTY_eve += Convert.ToInt32(ds6.Tables[1].Rows[i]["TotalQTY"]);
               //     total_Gatepassqty_eve += Convert.ToInt32(ds6.Tables[1].Rows[i]["Gatepassqty"]);

               //     Tot_CashAmount_eve += Convert.ToDecimal(ds6.Tables[1].Rows[i]["CashAmount"]);
               //     Tot_AdvCardAmt_eve += Convert.ToDecimal(ds6.Tables[1].Rows[i]["AdvCardAmt"]);
               //     Total_InstAmt_eve += Convert.ToDecimal(ds6.Tables[1].Rows[i]["TotalInstAmt"]);
               //     Total_QTYInLtr_eve += Convert.ToDecimal(ds6.Tables[1].Rows[i]["TotalQTYInLtr"]);
               //     total_TotalAmt_eve += Convert.ToDecimal(ds6.Tables[1].Rows[i]["TotalAmt"]);
               //     Total_Transport_eve += Convert.ToDecimal(ds6.Tables[1].Rows[i]["Transport"]);

               // }
               // sbe.Append("<tr>");
               //// int ColumnCount = ds6.Tables[0].Columns.Count - 13;


               // for (int i = 0; i < ColCount1; i++)
               // {
               //     if (i == 0)
               //     {
               //         sbe.Append("<td style='text-align:left'><b>Total</b></td>");
               //     }
               //     else if (i == 1)
               //     {
               //         sbe.Append("<td><b>" + Total_ReturnQty_eve.ToString() + "</b></td>");
               //     }
               //     else if (i == 2)
               //     {
               //         sbe.Append("<td><b>" + Total_cashqty_eve.ToString() + "</b></td>");
               //     }
               //     else if (i == 3)
               //     {
               //         sbe.Append("<td><b>" + Tot_CashAmount_eve.ToString() + "</b></td>");
               //     }
               //     else if (i == 4)
               //     {
               //         sbe.Append("<td><b>" + Total_AdvCardQty_eve.ToString() + "</b></td>");
               //     }
               //     else if (i == 5)
               //     {
               //         sbe.Append("<td><b>" + Tot_AdvCardAmt_eve.ToString("0.00") + "</b></td>");
               //     }
               //     else if (i == 6)
               //     {
               //         sbe.Append("<td><b>" + Total_InstQty_eve.ToString("0.00") + "</b></td>");
               //     }
               //     else if (i == 7)
               //     {
               //         sbe.Append("<td><b>" + Total_InstAmt_eve.ToString() + "</b></td>");
               //     }
               //     else if (i == 8)
               //     {
               //         sbe.Append("<td><b>" + Total_QTY_eve.ToString("0.00") + "</b></td>");
               //     }
               //     else if (i == 9)
               //     {
               //         sbe.Append("<td><b>" + Total_QTYInLtr_eve.ToString("0.000") + "</b></td>");
               //     }
               //     else if (i == 10)
               //     {
               //         sbe.Append("<td><b>" + total_TotalAmt_eve.ToString("0.000") + "</b></td>");
               //     }
               //     else if (i == 11)
               //     {
               //         sbe.Append("<td><b>" + Total_Transport_eve.ToString() + "</b></td>");
               //     }
               //     else if (i == 12)
               //     {
               //         sbe.Append("<td><b>" + total_Gatepassqty_eve.ToString() + "</b></td>");
               //     }



               // }
               // sbe.Append("</tr>");

               // sbe.Append("</table>");
               // sbe.Append("</div>");
               // sbe.Append("</div>");



               // //All
               // decimal Tot_CashAmount = 0, Tot_AdvCardAmt = 0, Total_InstAmt = 0, Total_QTYInLtr = 0, total_TotalAmt = 0, Total_Transport = 0;
               // int Total_ReturnQty = 0, Total_cashqty = 0, Total_AdvCardQty = 0, Total_InstQty = 0, Total_QTY = 0, total_Gatepassqty = 0;

               // sball.Append("<div class='table-responsive' >");
               // sball.Append("<div  style='border: 0px solid black'>");
               // sball.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
               // //sball.Append("<tr>");
               // ////sball.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
               // //sball.Append("<td style='text-align:Center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
               // //sball.Append("</tr>");

               // //sball.Append("<tr>");
               // //sball.Append("<td style='text-align:Center'><b> Time Period :- " + txtFromDate.Text + " - " + txttoDate.Text + "</b></td>");

               // //sball.Append("</tr>");

               // //sball.Append("<tr>");
               // //sball.Append("<td > </td>");

               // //sball.Append("</tr>");
               // sball.Append("<tr>");
               // sball.Append("<td class='text-left'><b>Shift :- Day </b></td>");
               // sball.Append("<td class='text-right'></td>");
               // sball.Append("</tr>");

               // sball.Append("<tr>");
               // sball.Append("<td></td>");
               // sball.Append("<td></td>");
               // sball.Append("<td></td>");
               // sball.Append("</tr>");

               // sball.Append("</table>");
               // sball.Append("<table class='table table1-bordered' > ");
               // int Count2 = ds6.Tables[2].Rows.Count;
               // int ColCount2 = ds6.Tables[2].Columns.Count;
               // sball.Append("<thead style='padding-left:0px;'>");
               // sball.Append("<td style='width:120px'>Particulars</td>");
               // sball.Append("<td>LeakQty</td>");
               // sball.Append("<td>Cash Qty</td>");
               // sball.Append("<td>Cash Amount (In Pkt.)</td>");
               // sball.Append("<td>Adv. Card Qty(In Pkt.)</td>");
               // sball.Append("<td>Adv. Card Amount</td>");
               // sball.Append("<td>Inst. Qty</td>");
               // sball.Append("<td>Inst. Amount</td>");
               // sball.Append("<td>Total Qty</td>");
               // sball.Append("<td>Total Qty(In Ltr)</td>");
               // sball.Append("<td>Total Amt.</td>");
               // sball.Append("<td>Transport</td>");
               // sball.Append("<td>Gatepass</td>");


               // sball.Append("</thead>");

               // for (int i = 0; i < Count2; i++)
               // {

               //     sball.Append("<tr>");
               //     sball.Append("<td style='text-align:left'>" + ds6.Tables[2].Rows[i]["ItemName"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["TotalReturnQty"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["cashqty"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["CashAmount"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["TotalAdvCardQty"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["AdvCardAmt"] + "</td>");
               //     //sball.Append("<td>" + ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
               //     sball.Append("<td >" + ds6.Tables[2].Rows[i]["TotalInstQty"] + "</td>");
               //     sball.Append("<td >" + ds6.Tables[2].Rows[i]["TotalInstAmt"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["TotalQTY"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["TotalQTYInLtr"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["TotalAmt"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["Transport"] + "</td>");
               //     sball.Append("<td>" + ds6.Tables[2].Rows[i]["Gatepassqty"] + "</td>");

               //     sball.Append("</tr>");


               //     Total_ReturnQty += Convert.ToInt32(ds6.Tables[2].Rows[i]["TotalReturnQty"]);
               //     Total_cashqty += Convert.ToInt32(ds6.Tables[2].Rows[i]["cashqty"]);
               //     Total_AdvCardQty += Convert.ToInt32(ds6.Tables[2].Rows[i]["TotalAdvCardQty"]);
               //     Total_InstQty += Convert.ToInt32(ds6.Tables[2].Rows[i]["TotalInstQty"]);
               //     Total_QTY += Convert.ToInt32(ds6.Tables[2].Rows[i]["TotalQTY"]);
               //     total_Gatepassqty += Convert.ToInt32(ds6.Tables[2].Rows[i]["Gatepassqty"]);

               //     Tot_CashAmount += Convert.ToDecimal(ds6.Tables[2].Rows[i]["CashAmount"]);
               //     Tot_AdvCardAmt += Convert.ToDecimal(ds6.Tables[2].Rows[i]["AdvCardAmt"]);
               //     Total_InstAmt += Convert.ToDecimal(ds6.Tables[2].Rows[i]["TotalInstAmt"]);
               //     Total_QTYInLtr += Convert.ToDecimal(ds6.Tables[2].Rows[i]["TotalQTYInLtr"]);
               //     total_TotalAmt += Convert.ToDecimal(ds6.Tables[2].Rows[i]["TotalAmt"]);
               //     Total_Transport += Convert.ToDecimal(ds6.Tables[2].Rows[i]["Transport"]);

               // }
               // sball.Append("<tr>");
               // //int ColumnCount = ds6.Tables[0].Columns.Count - 13;


               // for (int i = 0; i < ColCount2; i++)
               // {
               //     if (i == 0)
               //     {
               //         sball.Append("<td style='text-align:left'><b>Total</b></td>");
               //     }
               //     else if (i == 1)
               //     {
               //         sball.Append("<td><b>" + Total_ReturnQty.ToString() + "</b></td>");
               //     }
               //     else if (i == 2)
               //     {
               //         sball.Append("<td><b>" + Total_cashqty.ToString() + "</b></td>");
               //     }
               //     else if (i == 3)
               //     {
               //         sball.Append("<td><b>" + Tot_CashAmount.ToString() + "</b></td>");
               //     }
               //     else if (i == 4)
               //     {
               //         sball.Append("<td><b>" + Total_AdvCardQty.ToString() + "</b></td>");
               //     }
               //     else if (i == 5)
               //     {
               //         sball.Append("<td><b>" + Tot_AdvCardAmt.ToString("0.00") + "</b></td>");
               //     }
               //     else if (i == 6)
               //     {
               //         sball.Append("<td><b>" + Total_InstQty.ToString("0.00") + "</b></td>");
               //     }
               //     else if (i == 7)
               //     {
               //         sball.Append("<td><b>" + Total_InstAmt.ToString() + "</b></td>");
               //     }
               //     else if (i == 8)
               //     {
               //         sball.Append("<td><b>" + Total_QTY.ToString("0.00") + "</b></td>");
               //     }
               //     else if (i == 9)
               //     {
               //         sball.Append("<td><b>" + Total_QTYInLtr.ToString("0.000") + "</b></td>");
               //     }
               //     else if (i == 10)
               //     {
               //         sball.Append("<td><b>" + total_TotalAmt.ToString("0.000") + "</b></td>");
               //     }
               //     else if (i == 11)
               //     {
               //         sball.Append("<td><b>" + Total_Transport.ToString() + "</b></td>");
               //     }
               //     else if (i == 12)
               //     {
               //         sball.Append("<td><b>" + total_Gatepassqty.ToString() + "</b></td>");
               //     }



               // }
               // sball.Append("</tr>");

               // sball.Append("</table>");
               // sball.Append("</div>");
               // sball.Append("</div>");

                div_page_content_morning.InnerHtml = sbm.ToString();
                //div_page_content_Evening.InnerHtml = sbe.ToString();
               // div_page_content_All.InnerHtml = sball.ToString();
                btnPrint.Visible = true;
                //ViewState["DistData"] += sball.ToString();
                //ViewState["PStatus"] = "1";
                ////////////////End Of Route Wise Print Code   ///////////////////////
            }
            else
            {
                btnPrint.Visible = false;
                div_page_content_morning.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry! ", "No record Found");
                btnPrint.Visible = false;
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
        if (ddlLocation.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
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
                DateTime Ddate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
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