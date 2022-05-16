using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Rpt_InvoiceDistOrInst_GDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds4, ds5, ds6, ds7 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0, totalsupply = 0.00, finalsupply = 0.00, totalAdvAmt = 0.00, instmarginamt = 0.000, tcstax = 0.000, tcstaxAmt = 0.000, PaybleAmtWithTcsTax = 0.000;

    int cellIndex = 2;
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
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = Date;
                txtDeliveryDate.Attributes.Add("readonly", "readonly");
               // GetCategory();
                // GetDisOrSS();
                GetInstitution();
                GetOfficeDetails();

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

    private void GetInstitution()
    {
        try
        {

            ddlInstitution.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                 new string[] { "flag", "Office_ID", "RetailerTypeID" },
                 new string[] { "15", objdb.Office_ID(), objdb.GetInstRetailerTypeId() }, "dataset");
            ddlInstitution.DataTextField = "BName";
            ddlInstitution.DataValueField = "BoothId";
            ddlInstitution.DataBind();
            ddlInstitution.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
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
            ddlDitributor.Items.Insert(0, new ListItem("All", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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

    private void GetDistOrIstDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;

            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            if (ddlInvoiceFor.SelectedValue == "1")
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "DistributorId", "AreaId" },
                       new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), ddlDitributor.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            }
            else
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "OrganizationId", "AreaId" },
                       new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), ddlInstitution.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                if (ddlInvoiceFor.SelectedValue == "1")
                {

                    ViewState["GridData"] = "1";
                    GridView1.DataSource = ds1.Tables[0];
                    GridView1.DataBind();

                    GridView1.Visible = true;
                    GridView2.Visible = false;
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
                else
                {
                    ViewState["GridData"] = "2";
                    GridView2.DataSource = ds1.Tables[0];
                    GridView2.DataBind();

                    GridView1.Visible = false;
                    GridView2.Visible = true;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            else
            {
                ViewState["GridData"] = "0";
                pnlData.Visible = false;
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :", " Record not found");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    #endregion========================================================
    #region=========== init or changed even===========================


    protected void ddlInvoiceFor_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblMsg.Text = string.Empty;
        if (ddlInvoiceFor.SelectedValue == "1")
        {
            GetDisOrSS();
            ddlDitributor.SelectedIndex = 0;
            pnldistorss.Visible = true;
            pnlInstitution.Visible = false;
            ddlInstitution.SelectedIndex = 0;

        }
        else if (ddlInvoiceFor.SelectedValue == "2")
        {
            pnlInstitution.Visible = true;
            pnldistorss.Visible = false;
            // ddlDitributor.SelectedIndex = 0;
        }
        else
        {
            pnldistorss.Visible = false;
            pnlInstitution.Visible = false;
            ddlInstitution.SelectedIndex = 0;
            // ddlDitributor.SelectedIndex = 0;
        }

    }
    #endregion===========================
    #region=========== click event for grdiview row bound event===========================
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridView3.Visible = true;
                    pnltcs.Visible = true;
                    GridView4.Visible = false;
                    GridView4.DataSource = null;
                    GridView4.DataBind();
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    // Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblDistName = (Label)row.FindControl("lblDistName");
                    Label lblDelivaryDate = (Label)row.FindControl("lblDelivaryDate");
                    Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");

                    modalBoothName.InnerHtml = lblDistName.Text;
                    modaldate.InnerHtml = lblDelivaryDate.Text;
                    modelShift.InnerHtml = lblShiftName.Text;

                    ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");

                    if (ds6.Tables[0].Rows.Count > 0)
                    {
                        lblMsName.Text = lblDistName.Text;
                        lblRouteName.Text = ds6.Tables[0].Rows[0]["RName"].ToString();
                        if (ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString() == "")
                        {
                            ViewState["Tval"] = "0.000";
                        }
                        else
                        {
                            ViewState["Tval"] = ds6.Tables[0].Rows[0]["TcsTaxPer"].ToString();
                        }
                        lblOName1.Text = ViewState["Office_Name"].ToString();
                        lblOName2.Text = ViewState["Office_Name"].ToString();
                        lblOName3.Text = ViewState["Office_Name"].ToString();
                        lblGST.Text = ViewState["Office_GST"].ToString();
                        lblDelishift.Text = lblShiftName.Text;
                        lbldelidate.Text = lblDelivaryDate.Text;
                        lblDelivarydate.Text = lblDelivaryDate.Text;

                        GridView3.DataSource = ds6.Tables[0];
                        GridView3.DataBind();


                        ////////////////Start Of Route Wise Print Code   ///////////////////////
                        StringBuilder sb = new StringBuilder();
                        decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, tcstamt = 0, fpaybleamt = 0, totaladvAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0;
                        sb.Append("<div class='content' style='border: 1px solid black'>");
                        sb.Append("<table class='table1' style='width:100%; height:100%'>");
                        sb.Append("<tr>");
                        sb.Append("<td rowspan='4' class='text-left'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                        sb.Append("<td colspan='2' class='text-center' style='font-size:16px;'><b>" + lblOName1.Text + "</td><td class='blank_td' style='width: 250px;'></td>");

                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' class='text-center' style='font-size:12px;'><b>Office : </b>" + ViewState["Office_Address"].ToString() + " - " + ViewState["Office_Pincode"].ToString() + "</br><b>Plant : </b>" + ViewState["Office_Address1"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");                     
                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' class='text-center' style='font-size:13px;'><b>Bill Book</b></td><td class='blank_td' style='width: 250px;'></td>");

                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan='2' class='text-center' style='font-size:13px;'><b>G.S.T/U.I.N NO:-" + lblGST.Text + "<b></td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>Invoice No. :-" + ds6.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>M/s  :-" + lblMsName.Text + "<td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>Add:" + ds6.Tables[0].Rows[0]["DAddress"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>GST No.: " + ds6.Tables[0].Rows[0]["GSTNo"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>" + lblDelishift.Text + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left'>" + lbldelidate.Text + "</td>");
                        sb.Append("<td class='text-right'>" + lblRouteName.Text + "</td>");
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
                        sb.Append("<td>Inst. Qty</td>");
                        sb.Append("<td>Inst. Margin</td>");
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
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransComm"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["InstTransCommAmt"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingAmount"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                            sb.Append("</tr>");



                            totaladvAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["RatePerLtrAdCard"]));
                            //totalInstAmt += (Convert.ToDecimal(ds6.Tables[0].Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstRatePerLtr"]));
                            totalInstTranCommAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["InstTransCommAmt"]);
                            totalAdvCrdAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["AdvCardAmt"]);
                            totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["BillingAmount"]);
                            paybleAmt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);

                        }
                        sb.Append("<tr>");
                        int ColumnCount = GridView3.FooterRow.Cells.Count;
                        for (int i = 0; i < ColumnCount; i++)
                        {
                            if (i == 0)
                            {
                                sb.Append("<td><b>" + GridView3.FooterRow.Cells[i].Text + "</b></td>");
                            }
                            else if (i == ColumnCount - 10)
                            {
                                sb.Append("<td><b>" + totalAdvCrdAmt + "</b></td>");
                            }
                            else if (i == ColumnCount - 6)
                            {
                                sb.Append("<td><b>" + totalInstTranCommAmt + "</b></td>");
                            }
                            else if (i == ColumnCount - 2)
                            {
                                sb.Append("<td><b>" + totalamt + "</b></td>");
                            }
                            else if (i == ColumnCount - 1)
                            {
                                sb.Append("<td><b>" + paybleAmt + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td>" + GridView3.FooterRow.Cells[i].Text + "</td>");
                            }



                        }
                        
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString()) / 100));
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
                        sb.Append("<td colspan='2'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>Note:</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");

                        sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='1'>2 . Please quote our Bill No. while remiting the amount.</td>");
                        sb.Append("<td class='text-right'>For :-" + lblOName2.Text + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='1'>3 . All Payment to be made by Bank Draft payable to  :-" + lblOName3.Text + "</td>");
                        sb.Append("<td class='text-right'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                        sb.Append("</div>");

                        Print.InnerHtml = sb.ToString();
                        ////////////////End Of Route Wise Print Code   ///////////////////////
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                }
            }
            if (e.CommandName == "RecordReject")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                    new string[] { "flag", "MilkOrProductInvoiceId", "CreatedBy", "CreatedByIP" },
                                    new string[] { "8", e.CommandArgument.ToString(), objdb.createdBy(), IPAddress }, "TableSave");

                    if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetDistOrIstDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    }
                    else
                    {
                        string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }


                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }

        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    pnltcs.Visible = false;
                    GridView4.Visible = true;
                    GridView3.Visible = false;
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    // Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblIName = (Label)row.FindControl("lblIName");
                    Label lblDelivaryDate = (Label)row.FindControl("lblDelivaryDate");
                    Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");

                    modalBoothName.InnerHtml = lblIName.Text;
                    modaldate.InnerHtml = lblDelivaryDate.Text;
                    modelShift.InnerHtml = lblShiftName.Text;

                    ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                                           new string[] { "Flag", "MilkOrProductInvoiceId", "Office_ID" },
                                           new string[] { "3", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");

                    if (ds6.Tables[0].Rows.Count > 0)
                    {
                        lblMsName.Text = lblIName.Text;
                        lblRouteName.Text = ds6.Tables[0].Rows[0]["RName"].ToString();
                        lblOName1.Text = ViewState["Office_Name"].ToString();
                        lblOName2.Text = ViewState["Office_Name"].ToString();
                        lblOName3.Text = ViewState["Office_Name"].ToString();
                        lblGST.Text = ViewState["Office_GST"].ToString();
                        lblDelishift.Text = lblShiftName.Text;
                        lbldelidate.Text = lblDelivaryDate.Text;
                        lblDelivarydate.Text = lblDelivaryDate.Text;

                        GridView4.DataSource = ds6.Tables[0];
                        GridView4.DataBind();

                        ////////////////Start Of Route Wise Print Code   ///////////////////////
                        StringBuilder sb = new StringBuilder();
                        decimal totalamt = 0;
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
                        sb.Append("<td class='text-left' colspan='2'>No" + lblDelivarydate.Text + "</td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>M/s  :-" + lblMsName.Text + "<td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>Add:" + ds6.Tables[0].Rows[0]["BAddress"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left' colspan='2'>" + lblDelishift.Text + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class='text-left'>" + lbldelidate.Text + "</td>");
                        sb.Append("<td class='text-right'>" + lblRouteName.Text + "</td>");
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
                        sb.Append("<td>Billing Qty(In Pkt.)</td>");
                        sb.Append("<td>Billing Qty(In Ltr.)</td>");
                        sb.Append("<td>Rate (Per Ltr)</td>");
                        sb.Append("<td>Net Payble Amount</td>");
                        sb.Append("</thead>");

                        for (int i = 0; i < Count; i++)
                        {

                            sb.Append("<tr>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["IName"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["SupplyTotalQty"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["TotalReturnQty"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQty"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["BillingQtyInLtr"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["RatePerLtr"] + "</td>");
                            sb.Append("<td>" + ds6.Tables[0].Rows[i]["PaybleAmount"] + "</td>");
                            sb.Append("</tr>");

                            totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[i]["PaybleAmount"]);
                        }
                        sb.Append("<tr>");
                        int ColumnCount = GridView4.FooterRow.Cells.Count;
                        for (int i = 0; i < ColumnCount; i++)
                        {
                            if (i == 0)
                            {
                                sb.Append("<td><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
                            }
                            else if (i == ColumnCount - 1)
                            {
                                sb.Append("<td><b>" + totalamt + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td>" + GridView4.FooterRow.Cells[i].Text + "</td>");
                            }



                        }
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

                        Print.InnerHtml = sb.ToString();
                        ////////////////End Of Route Wise Print Code   ///////////////////////
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }



                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }

        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotalPaybleAmount = (e.Row.FindControl("lblTotalPaybleAmount") as Label);
                totalsupply += Convert.ToDouble(lblTotalPaybleAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblPAmount = (e.Row.FindControl("lblPAmount") as Label);
                lblPAmount.Text = totalsupply.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotalPaybleAmount = (e.Row.FindControl("lblTotalPaybleAmount") as Label);
                totalsupply += Convert.ToDouble(lblTotalPaybleAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblPAmount = (e.Row.FindControl("lblPAmount") as Label);
                lblPAmount.Text = totalsupply.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAdvCardAmt = (e.Row.FindControl("lblAdvCardAmt") as Label);
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                Label lblPaybleAmount = (e.Row.FindControl("lblPaybleAmount") as Label);
               
                Label lblInstTransCommAmt = (e.Row.FindControl("lblInstTransCommAmt") as Label);
               
                
                totalsupply += Convert.ToDouble(lblAmount.Text);
                finalsupply += Convert.ToDouble(lblPaybleAmount.Text);
                totalAdvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
                instmarginamt += Convert.ToDouble(lblInstTransCommAmt.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblTAmount = (e.Row.FindControl("lblTAmount") as Label);
                Label lblTotalPAmount = (e.Row.FindControl("lblTotalPAmount") as Label);
                Label lblTAdvCAmt = (e.Row.FindControl("lblTAdvCAmt") as Label);
                Label lblFInstMarAmt = (e.Row.FindControl("lblFInstMarAmt") as Label);
             
                lblTAmount.Text = totalsupply.ToString("0.000");
                lblFInstMarAmt.Text = instmarginamt.ToString("0.000");
                lblTotalPAmount.Text = finalsupply.ToString("0.000");
                lblTotalAdvCardAmt.Text = totalAdvAmt.ToString("0.000");
                if (ViewState["Tval"].ToString() == "")
                {
                    tcstax = 0.000;
                }
                else
                {
                    tcstax = Convert.ToDouble(ViewState["Tval"].ToString());
                }

                tcstaxAmt = ((tcstax * finalsupply) / 100);
                PaybleAmtWithTcsTax = tcstaxAmt + finalsupply;
                // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
                lblTcsTax.Text = ViewState["Tval"].ToString();
                lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.000");
                lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPaybleAmount = (e.Row.FindControl("lblPaybleAmount") as Label);
                finalsupply += Convert.ToDouble(lblPaybleAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPAmount = (e.Row.FindControl("lblTotalPAmount") as Label);
                lblTotalPAmount.Text = finalsupply.ToString("0.000");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetDistOrIstDetails();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtDeliveryDate.Text = string.Empty;
        ddlInvoiceFor.SelectedIndex = 0;
        pnldistorss.Visible = false;
        pnlInstitution.Visible = false;
        ddlDitributor.SelectedIndex = 0;
        ddlInstitution.SelectedIndex = 0;
       // ddlItemCategory.SelectedIndex = 0;
        pnlData.Visible = false;
        ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "Invoice" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            if (!string.IsNullOrEmpty(ViewState["GridData"].ToString()))
            {
                if (ViewState["GridData"].ToString() == "1")
                {
                    GridView1.Columns[7].Visible = false;
                    GridView1.RenderControl(htmlWrite);
                }
                else if (ViewState["GridData"].ToString() == "2")
                {
                    GridView2.Columns[7].Visible = false;
                    GridView2.RenderControl(htmlWrite);
                }

            }


            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }

    #endregion===========================
}