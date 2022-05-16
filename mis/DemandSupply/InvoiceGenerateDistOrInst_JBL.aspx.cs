using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Net;
using System.IO;

public partial class mis_DemandSupply_InvoiceGenerateDistOrInst_JBL : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6, ds7, ds8, ds3 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0, totalsupply = 0.00, finalsupply = 0.00, totaladvAmt = 0.00, tcstax = 0.000, advcardtotalamt = 0.000, instamtotalt = 0.00, instmarginamt = 0.000, tcstaxAmt = 0.000, PaybleAmtWithTcsTax = 0.000, totldisttransmarginamt = 0.000, totSupplyTotalQtyinltr = 0.000, totBillingQtyInLtr=0.000;
    int totSupplyTotalQty = 0, totTotalReturnQty = 0, totTotalAdvCardQty = 0, totInstQty = 0, totBillingQty = 0;
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
                GetCategory();
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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
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
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            ddlInstitution.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
    }
    private void GetDisOrSSByRouteID()
    {
        try
        {
            ds7 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                  new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue,objdb.GetMilkCatId() }, "dataset");

            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlDitributor.DataTextField = "DTName";
                ddlDitributor.DataValueField = "DistributorId";
                ddlDitributor.DataSource = ds7.Tables[0];
                ddlDitributor.DataBind();
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
            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GenerateInvoice()
    {
        try
        {
            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            TxtRemark.Enabled = true;

            if (ddlInvoiceFor.SelectedValue == "1")
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "DistributorId", "OrganizationId", "AreaId" },
                       new string[] { "6", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue, "0", ddlLocation.SelectedValue }, "dataset");

                
            }
            else
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "OrganizationId", "AreaId" },
                       new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlRoute.SelectedValue, ddlInstitution.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["PrintDistOrInt"] = ds1.Tables[0];
                lblFinalPaybleAmount.Text = string.Empty;
                lblTcsTax.Text = string.Empty;
                lblTcsTaxAmt.Text = string.Empty;
                if (ddlInvoiceFor.SelectedValue == "1")
                {
                    GetTcsTax();
                    lblMsName.Text = ds1.Tables[0].Rows[0]["DName"].ToString();
                    lblRouteName.Text = ds1.Tables[0].Rows[0]["RName"].ToString();


                    GridView1.DataSource = ds1.Tables[0];
                    GridView1.DataBind();



                    DataTable dtdist = new DataTable();
                    DataRow drdist;

                    dtdist.Columns.Add("Item_id", typeof(int));
                    dtdist.Columns.Add("SupplyTotalQty", typeof(int));
                    //dtdist.Columns.Add("SupplyTotalQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalAdvCardQty", typeof(int));
                    dtdist.Columns.Add("TotalReturnQty", typeof(int));
                    dtdist.Columns.Add("SupplyTotalQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalAdvCardQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalReturnQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("AdvCardComm", typeof(decimal));
                    dtdist.Columns.Add("AdvCardAmt", typeof(decimal));
                    dtdist.Columns.Add("DistOrInstRate", typeof(decimal));
                    //sadhana
                    dtdist.Columns.Add("DisttransportRate_oncashsale", typeof(decimal));
                    dtdist.Columns.Add("Disttransportmargin_oncashsale", typeof(decimal));
                    //sadhana
                    dtdist.Columns.Add("BillingQty", typeof(int));
                    dtdist.Columns.Add("BillingQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("BillingAmount", typeof(decimal));
                    dtdist.Columns.Add("PaybleAmount", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtrAdCard", typeof(decimal));
                    dtdist.Columns.Add("AdvCardRebateComm", typeof(decimal));
                    dtdist.Columns.Add("SpecialRebateMargin", typeof(decimal));
                    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    dtdist.Columns.Add("InstRatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("ConsumerRate", typeof(decimal));
                    dtdist.Columns.Add("Retailer_Rate", typeof(decimal));
                    dtdist.Columns.Add("DistOrSSComm", typeof(decimal));
                    dtdist.Columns.Add("TransComm", typeof(decimal));
                    dtdist.Columns.Add("RetailerComm", typeof(decimal));
                    dtdist.Columns.Add("TotalInstAmount", typeof(decimal));
               
                    drdist = dtdist.NewRow();
                    int Count = ds1.Tables[0].Rows.Count;

                    for (int j = 0; j < Count; j++)
                    {

                        drdist[0] = ds1.Tables[0].Rows[j]["Item_id"].ToString();
                        drdist[1] = ds1.Tables[0].Rows[j]["SupplyTotalQty"].ToString();
                       // drdist[2] = ds1.Tables[0].Rows[j]["SupplyTotalQtyInLtr"].ToString();
                        drdist[2] = ds1.Tables[0].Rows[j]["TotalAdvCardQty"].ToString();
                        drdist[3] = ds1.Tables[0].Rows[j]["TotalReturnQty"].ToString();
                        drdist[4] = ds1.Tables[0].Rows[j]["SupplyTotalQtyInLtr"].ToString();
                        drdist[5] = ds1.Tables[0].Rows[j]["TotalAdvCardQtyInLtr"].ToString();
                        drdist[6] = ds1.Tables[0].Rows[j]["TotalReturnQtyInLtr"].ToString();
                        drdist[7] = ds1.Tables[0].Rows[j]["AdvCardComm"].ToString();
                        drdist[8] = ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString();
                        drdist[9] = ds1.Tables[0].Rows[j]["DistOrSSRate"].ToString();
                        drdist[10] = (Convert.ToDecimal(ds1.Tables[0].Rows[j]["DistOrSSComm"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["TransComm"].ToString())).ToString();
                        drdist[11] =((Convert.ToDecimal(ds1.Tables[0].Rows[j]["DistOrSSComm"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["TransComm"].ToString()))* Convert.ToDecimal(ds1.Tables[0].Rows[j]["BillingQty"].ToString())).ToString();
                        drdist[12] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                        drdist[13] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                        drdist[14] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                        drdist[15] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        //drdist[16] = Convert.ToDecimal(ds1.Tables[0].Rows[j]["Amount"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString());
                        drdist[16] = Convert.ToDecimal(ds1.Tables[0].Rows[j]["Amount"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString()) - (Convert.ToDecimal(ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString()) - Convert.ToDecimal((double.Parse(ds1.Tables[0].Rows[j]["DistOrSSComm"].ToString()) + double.Parse(ds1.Tables[0].Rows[j]["TransComm"].ToString())) * double.Parse(ds1.Tables[0].Rows[j]["BillingQty"].ToString())));

                        drdist[17] = ds1.Tables[0].Rows[j]["RatePerLtrAdCard"].ToString();
                        drdist[18] = ds1.Tables[0].Rows[j]["AdvCardRebateComm"].ToString();
                        drdist[19] = ds1.Tables[0].Rows[j]["SpecialRebateMargin"].ToString();
                        drdist[20] = ds1.Tables[0].Rows[j]["TotalInstSupplyQty"].ToString();
                        drdist[21] = ds1.Tables[0].Rows[j]["TotalInstSupplyQtyInLtr"].ToString();
                        drdist[22] = ds1.Tables[0].Rows[j]["InstTransComm"].ToString();
                        drdist[23] = ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString();
                        drdist[24] = "0";
                        drdist[25] = ds1.Tables[0].Rows[j]["Finalconsumerrate"].ToString();
                        drdist[26] = ds1.Tables[0].Rows[j]["FinalRetailerrate"].ToString();
                        drdist[27] = ds1.Tables[0].Rows[j]["DistOrSSComm"].ToString();
                        drdist[28] = ds1.Tables[0].Rows[j]["TransComm"].ToString();
                        drdist[29] = ds1.Tables[0].Rows[j]["RetailerComm"].ToString();
                        drdist[30] = ds1.Tables[0].Rows[j]["TotalInstAmount"].ToString();


                        dtdist.Rows.Add(drdist.ItemArray);

                    }
                    ViewState["ItemDetails"] = dtdist;


                    if (dtdist != null) { dtdist.Dispose(); }
                    GridView1.Visible = true;
                    GridView2.Visible = false;


                    GridView2.DataSource = null;
                    GridView2.DataBind();

                    //for remark if saved
                    ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "DistributorId", "AreaId" },
                       new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlDitributor.SelectedValue, ddlLocation.SelectedValue }, "dataset");
                    if (ds2.Tables.Count > 0)
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            // TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                           // lblinvoiceno.Text = ds2.Tables[0].Rows[0]["InvoiceNo"].ToString();
                            if (ds2.Tables[0].Rows[0]["Remark"].ToString() == "")
                            {
                                TxtRemark.Text = "";
                            }
                            else
                            {
                                TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                                TxtRemark.Enabled = false;
                            }
                        }
                        else
                        {
                            TxtRemark.Text = "";
                        }
                    }


                    
                }
                else
                {

                    GridView2.DataSource = ds1.Tables[0];
                    GridView2.DataBind();

                    DataTable dtdist = new DataTable();
                    DataRow drdist;

                    dtdist.Columns.Add("Item_id", typeof(int));
                    dtdist.Columns.Add("SupplyTotalQty", typeof(int));
                    dtdist.Columns.Add("TotalAdvCardQty", typeof(int));
                    dtdist.Columns.Add("TotalReturnQty", typeof(int));
                    dtdist.Columns.Add("SupplyTotalQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalAdvCardQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalReturnQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("AdvCardComm", typeof(decimal));
                    dtdist.Columns.Add("AdvCardAmt", typeof(decimal));
                    dtdist.Columns.Add("DistOrInstRate", typeof(decimal));
                    dtdist.Columns.Add("DisttransportRate_oncashsale", typeof(decimal));
                    dtdist.Columns.Add("Disttransportmargin_oncashsale", typeof(decimal));
                    dtdist.Columns.Add("BillingQty", typeof(int));
                    dtdist.Columns.Add("BillingQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("Amount", typeof(decimal));
                    dtdist.Columns.Add("PaybleAmount", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtrAdCard", typeof(decimal));
                    dtdist.Columns.Add("AdvCardRebateComm", typeof(decimal));
                    dtdist.Columns.Add("SpecialRebateMargin", typeof(decimal));
                    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    dtdist.Columns.Add("InstRatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("ConsumerRate", typeof(decimal));
                    dtdist.Columns.Add("Retailer_Rate", typeof(decimal));
                    dtdist.Columns.Add("DistOrSSComm", typeof(decimal));
                    dtdist.Columns.Add("TransComm", typeof(decimal));
                    dtdist.Columns.Add("RetailerComm", typeof(decimal));
                    dtdist.Columns.Add("TotalInstAmount", typeof(decimal));
                    drdist = dtdist.NewRow();
                    int Count = ds1.Tables[0].Rows.Count;

                    for (int j = 0; j < Count; j++)
                    {

                        drdist[0] = ds1.Tables[0].Rows[j]["Item_id"].ToString();
                        drdist[1] = ds1.Tables[0].Rows[j]["SupplyTotalQty"].ToString();
                        drdist[2] = ds1.Tables[0].Rows[j]["TotalAdvCardQty"].ToString();
                        drdist[3] = ds1.Tables[0].Rows[j]["TotalReturnQty"].ToString();
                        drdist[4] = ds1.Tables[0].Rows[j]["SupplyTotalQtyInLtr"].ToString();
                        drdist[5] = ds1.Tables[0].Rows[j]["TotalAdvCardQtyInLtr"].ToString();
                        drdist[6] = ds1.Tables[0].Rows[j]["TotalReturnQtyInLtr"].ToString();
                        drdist[7] = "0";
                        drdist[8] = "0";
                        drdist[9] = ds1.Tables[0].Rows[j]["DistOrSSRate"].ToString();
                        //drdist[10] = (Convert.ToDecimal(ds1.Tables[0].Rows[j]["DistOrSSComm"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["TransComm"].ToString())).ToString();
                        //drdist[11] = ((Convert.ToDecimal(ds1.Tables[0].Rows[j]["DistOrSSComm"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["TransComm"].ToString())) * Convert.ToDecimal(ds1.Tables[0].Rows[j]["BillingQty"].ToString())).ToString();
                        drdist[10] = "0";
                        drdist[11] = "0";
                        drdist[12] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                        drdist[13] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                        drdist[14] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                        drdist[15] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        drdist[16] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        drdist[17] = "0";
                        drdist[18] = "0";
                        drdist[19] = "0";
                        drdist[20] = "0";
                        drdist[21] = "0";
                        drdist[22] = "0";
                        drdist[23] = "0";
                        drdist[24] = "0";
                        drdist[25] = "0";
                        drdist[26] = "0";
                        drdist[27] = "0";
                        drdist[28] = "0";
                        drdist[29] = "0";
                        drdist[30] = "0";

                        dtdist.Rows.Add(drdist.ItemArray);

                    }
                    ViewState["ItemDetails"] = dtdist;


                    if (dtdist != null) { dtdist.Dispose(); }

                    GridView1.Visible = false;
                    GridView2.Visible = true;

                    GridView1.DataSource = null;
                    GridView1.DataBind();


                    lblMsName.Text = ds1.Tables[0].Rows[0]["BName"].ToString();
                    lblRouteName.Text = ds1.Tables[0].Rows[0]["RName"].ToString();

                    //for remark if saved
                    ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "OrganizationId", "AreaId" },
                       new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlInstitution.Text, ddlLocation.SelectedValue }, "dataset");
                    if (ds2.Tables.Count > 0)
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            // TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                           // lblinvoiceno.Text = ds2.Tables[0].Rows[0]["InvoiceNo"].ToString();
                            if (ds2.Tables[0].Rows[0]["Remark"].ToString() == "")
                            {
                                TxtRemark.Text = "";
                            }
                            else
                            {
                                TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                                TxtRemark.Enabled = false;
                            }
                        }
                        else
                        {
                            TxtRemark.Text = "";
                        }
                    }


                    
                }
                lblOName1.Text = ViewState["Office_Name"].ToString();
                lblOName2.Text = ViewState["Office_Name"].ToString();
                lblOName3.Text = ViewState["Office_Name"].ToString();
                lblGST.Text = ViewState["Office_GST"].ToString();
                lblDelishift.Text = ddlShift.SelectedItem.Text;
                lbldelidate.Text = txtDeliveryDate.Text;
                lblDelivarydate.Text = txtDeliveryDate.Text;
                lblMsg.Text = string.Empty;
                pnlData.Visible = true;
                btnPrint.Visible = true;




            }
            else
            {
                lblFinalPaybleAmount.Text = string.Empty;
                lblTcsTax.Text = string.Empty;
                lblTcsTaxAmt.Text = string.Empty;
                pnlData.Visible = false;
                btnPrint.Visible = false;
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                ViewState["PrintDistOrInt"] = null;
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

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetRoute();
        }
    }
    protected void ddlInvoiceFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoute.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            if (ddlInvoiceFor.SelectedValue == "1")
            {
                pnldistorss.Visible = true;
                pnlInstitution.Visible = false;
                ddlInstitution.SelectedIndex = 0;

            }
            else if (ddlInvoiceFor.SelectedValue == "2")
            {
                pnlInstitution.Visible = true;
                pnldistorss.Visible = false;
                ddlDitributor.SelectedIndex = 0;
            }
            else
            {
                pnldistorss.Visible = false;
                pnlInstitution.Visible = false;
                ddlInstitution.SelectedIndex = 0;
                ddlDitributor.SelectedIndex = 0;
            }
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Select Route First");
            ddlInvoiceFor.SelectedIndex = 0;
        }
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDisOrSSByRouteID();
        GetInstitution();
    }
    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateInvoice();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtDeliveryDate.Text = string.Empty;
        ddlRoute.SelectedIndex = 0;
        ddlInvoiceFor.SelectedIndex = 0;
        pnldistorss.Visible = false;
        pnlInstitution.Visible = false;
        ddlDitributor.SelectedIndex = 0;
        ddlInstitution.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        pnlData.Visible = false;
        lblVehicleNo.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        lblFinalPaybleAmount.Text = string.Empty;
        lblTcsTax.Text = string.Empty;
        lblTcsTaxAmt.Text = string.Empty;

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAdvCardAmt = (e.Row.FindControl("lblAdvCardAmt") as Label);
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                Label lblFAmount = (e.Row.FindControl("lblFAmount") as Label);
                Label lblTotalAdvCardAmount = (e.Row.FindControl("lblTotalAdvCardAmount") as Label);
                Label lblInstTotalAmt = (e.Row.FindControl("lblInstTotalAmt") as Label);
                Label lblInstTransCommAmt = (e.Row.FindControl("lblInstTransCommAmt") as Label);
                //sadhana
                Label lblSupplyTotalQty = (e.Row.FindControl("lblSupplyTotalQty") as Label);
                Label lblSupplyTotalQtyInLtr = (e.Row.FindControl("lblSupplyTotalQtyInLtr") as Label);
                Label lblTotalReturnQty = (e.Row.FindControl("lblTotalReturnQty") as Label);
                Label lblTotalAdvCardQty = (e.Row.FindControl("lblTotalAdvCardQty") as Label);
                Label lblInstQty = (e.Row.FindControl("lblInstQty") as Label);
                Label lbldisttransmarginamt = (e.Row.FindControl("lbldisttransmarginamt") as Label);
                Label lblBillingQty = (e.Row.FindControl("lblBillingQty") as Label);
                Label lblBillingQtyInLtr = (e.Row.FindControl("lblBillingQtyInLtr") as Label);
                
                //sadhana
               
                advcardtotalamt += Convert.ToDouble(lblTotalAdvCardAmount.Text);
                instamtotalt += Convert.ToDouble(lblInstTotalAmt.Text);
                instmarginamt += Convert.ToDouble(lblInstTransCommAmt.Text);
                totalsupply += Convert.ToDouble(lblAmount.Text);

                finalsupply += Convert.ToDouble(lblFAmount.Text);

                totaladvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
                //sadhana
                totSupplyTotalQty +=Convert.ToInt32(lblSupplyTotalQty.Text);
                totSupplyTotalQtyinltr += Convert.ToDouble(lblSupplyTotalQtyInLtr.Text);
                totTotalReturnQty +=Convert.ToInt32(lblTotalReturnQty.Text);
                totTotalAdvCardQty +=Convert.ToInt32(lblTotalAdvCardQty.Text);
                totInstQty +=Convert.ToInt32(lblInstQty.Text);
                totBillingQty +=Convert.ToInt32(lblBillingQty.Text);
                totldisttransmarginamt +=Convert.ToDouble(lbldisttransmarginamt.Text);
                totBillingQtyInLtr += Convert.ToDouble(lblBillingQtyInLtr.Text);
                //sadhana
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblTAmount = (e.Row.FindControl("lblTAmount") as Label);
                Label lblFinalAmount = (e.Row.FindControl("lblFinalAmount") as Label);
                Label lblTAdvCAmt = (e.Row.FindControl("lblTAdvCAmt") as Label);
                Label lblFITAmt = (e.Row.FindControl("lblFITAmt") as Label);
                Label lblFInstMarAmt = (e.Row.FindControl("lblFInstMarAmt") as Label);

                //sadhana
                Label lblSupplyTotalQty_tot = (e.Row.FindControl("lblSupplyTotalQty_tot") as Label);
                Label lblTotalReturnQty_tot = (e.Row.FindControl("lblTotalReturnQty_tot") as Label);
                Label lblTotalAdvCardQty_tot = (e.Row.FindControl("lblTotalAdvCardQty_tot") as Label);
                Label lblInstQty_tot = (e.Row.FindControl("lblInstQty_tot") as Label);
                Label lbldisttransmarginamt_tot = (e.Row.FindControl("lbldisttransmarginamt_tot") as Label);
                Label lblBillingQty_tot = (e.Row.FindControl("lblBillingQty_tot") as Label);
                Label lblSupplyTotalQtyInLtr_tot = (e.Row.FindControl("lblSupplyTotalQtyInLtr_tot") as Label);
                Label lblBillingQtyInLtr_tot = (e.Row.FindControl("lblBillingQtyInLtr_tot") as Label);
                //sadhana

                lblTAdvCAmt.Text=advcardtotalamt.ToString("0.000");
                lblFITAmt.Text=instamtotalt.ToString("0.000");
                lblTAmount.Text = totalsupply.ToString("0.000");
                lblFInstMarAmt.Text = instmarginamt.ToString("0.000");
                lblFinalAmount.Text = finalsupply.ToString("0.000");
                lblTotalAdvCardAmt.Text = totaladvAmt.ToString("0.000");
                tcstax = Convert.ToDouble(ViewState["Tval"].ToString());
                tcstaxAmt = ((tcstax * finalsupply) / 100);
                PaybleAmtWithTcsTax = tcstaxAmt + finalsupply;
                // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
                lblTcsTax.Text = ViewState["Tval"].ToString();
                lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.000");
                lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
                lblSupplyTotalQtyInLtr_tot.Text = totSupplyTotalQtyinltr.ToString("0.00");

                //sadhana
                lblSupplyTotalQty_tot.Text = totSupplyTotalQty.ToString();
                lblTotalReturnQty_tot.Text = totTotalReturnQty.ToString();
                lblTotalAdvCardQty_tot.Text = totTotalAdvCardQty.ToString();
                lblInstQty_tot.Text = totInstQty.ToString();
                lblBillingQty_tot.Text = totBillingQty.ToString();
                lbldisttransmarginamt_tot.Text =totldisttransmarginamt.ToString("0.000");
                lblBillingQtyInLtr_tot.Text = totBillingQtyInLtr.ToString("0.000");
                
                //sadhana
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
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                totalsupply += Convert.ToDouble(lblAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFinalAmount = (e.Row.FindControl("lblFinalAmount") as Label);
                lblFinalAmount.Text = totalsupply.ToString("0.000");
                lblFinalPaybleAmount.Text = totalsupply.ToString("0.000");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    #endregion===========================
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                DataTable dtInsert = new DataTable();
                dtInsert = null;
                dtInsert = (DataTable)ViewState["ItemDetails"];
                if (dtInsert.Rows.Count > 0)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    Decimal PaybleAmount = dtInsert.AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                    if (ddlInvoiceFor.SelectedValue == "1")
                    {
                        if (PaybleAmount > 0)
                        {
                            Decimal FPaybleAmount = Convert.ToDecimal(lblFinalPaybleAmount.Text);
                            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert_JBL",
                                 new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                                     "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId","TcsTaxPer","Remark" },
                                   new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, 
                                       ddlItemCategory.SelectedValue, ddlRoute.SelectedValue, ddlDitributor.SelectedValue
                                       , "0", FPaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress,ddlLocation.SelectedValue,ViewState["Tval"].ToString(),TxtRemark.Text }
                                       , "type_Trn_MilkOrProductInvoiceDetailChild_JBL", dtInsert, "dataset");

                            if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                //if (objdb.Office_ID() == "2")
                                //{
                                    get_distributor_mobileno();
                                    if (lbldistributerMONO.Text != "0000000000" && lbldistributerMONO.Text != "9999999999" && lbldistributerMONO.Text != null && lbldistributerMONO.Text != "")
                                    {
                                        if (decimal.Parse(FPaybleAmount.ToString()) > 0)
                                        {
                                            string Supmessage = "";
                                            string link = "";
                                            //string Invoice_No = ds6.Tables[0].Rows[0]["Invoice_No"].ToString();
											string Invoice_No = ds6.Tables[0].Rows[0]["invoiceSnoNew"].ToString();
                                            ServicePointManager.Expect100Continue = true;
                                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                            //lbldistributerMONO.Text = "8962389494";

                                            Supmessage = "Your bill for order " + Invoice_No.ToString() + " date " + deliverydate.ToString() + " shift " + ddlShift.SelectedItem.Text + " has been generated for amount " + FPaybleAmount.ToString() + " rs. Kindly make payment on time.";
                                            link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + lbldistributerMONO.Text + "&text=" + Server.UrlEncode(Supmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162650801393194&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";


                                            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                                            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                            Stream stream = response.GetResponseStream();
                                        }
                                    }
                               // }
                                string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                PrintDistData();
                                pnlData.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                if (error == "Already")
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice already Saved. ");
                                    PrintDistData();
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Save Data :" + error);
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Invoice Not Saved and Print");
                            return;
                        }

                    }
                    else
                    {
                        DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                        ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert_JBL",
                             new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                                     "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId","Remark" },
                               new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, 
                                       ddlItemCategory.SelectedValue, ddlRoute.SelectedValue, "0"
                                       , ddlInstitution.SelectedValue, PaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress,ddlLocation.SelectedValue,TxtRemark.Text }
                                   , "type_Trn_MilkOrProductInvoiceDetailChild_JBL", dtInsert, "dataset");

                        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                            PrintInstData();
                            pnlData.Visible = false;
                            btnPrint.Visible = false;
                        }
                        else
                        {
                            string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice already Exist. ");
                                PrintInstData();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Save Data :" + error);
                            }
                        }

                    }

                }

                if (dtInsert != null) { dtInsert.Dispose(); }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error printing ", ex.Message.ToString());
        }

        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    private void get_distributor_mobileno()
    {
        ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                                     new string[] { "Flag", "Office_ID", "DistributorId" },
                                     new string[] { "11", objdb.Office_ID(), ddlDitributor.SelectedValue }, "dataset");
        if (ds3.Tables.Count > 0)
        {
            if (ds3.Tables[0].Rows.Count > 0)
            {
                lbldistributerMONO.Text = ds3.Tables[0].Rows[0]["DCPersonMobileNo"].ToString();
            }
        }

    }

    private void PrintDistData()
    {

        DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        //for remark if saved
        ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
         new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "DistributorId", "AreaId" },
           new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlDitributor.SelectedValue, ddlLocation.SelectedValue }, "dataset");
        if (ds2.Tables.Count > 0)
        {
            if (ds2.Tables[0].Rows.Count > 0)
            {
                // TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                //lblinvoiceno.Text = ds2.Tables[0].Rows[0]["InvoiceNo"].ToString();
				lblinvoiceno.Text = ds2.Tables[0].Rows[0]["NewInvoiceNo"].ToString();
                if (ds2.Tables[0].Rows[0]["Remark"].ToString() == "")
                {
                    TxtRemark.Text = "";
                }
                else
                {
                    TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                    TxtRemark.Enabled = false;
                }
            }
            else
            {
                TxtRemark.Text = "";
            }
        }

        StringBuilder sb = new StringBuilder();
        decimal totalamt = 0, paybleAmt = 0, totaladvAmt = 0, totalAdvCrdCmmAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0, tcstamt = 0, fpaybleamt = 0;
        decimal totaldiscomamt = 0, totaltranscomamt = 0, totalcashsale = 0, totalbillingqtyinltr = 0, totalsupplyqtyinltr=0;
        int totalbillingqty = 0, totalsupplyqty = 0, totaladvancecardqty = 0, totalinstituteqty = 0, totalreturnqty = 0;
        if (!string.IsNullOrEmpty(ViewState["PrintDistOrInt"].ToString()))
        {
            DataTable dsDist = (DataTable)ViewState["PrintDistOrInt"];
            div_page_content.InnerHtml = "";
            sb.Append("<div class='table-responsive'");
            sb.Append("<div class='content' style='border: 0px solid black'>");


            sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
            sb.Append("<tr>");
            sb.Append("<td rowspan='4' class='text-right' style='padding-left:70px;width:40%;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
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
            sb.Append("<td class='text-left' colspan='2'>Invoice No. :" + lblinvoiceno.Text + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>M/s  :-" + dsDist.Rows[0]["DName"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' style='width:33%' >GSTIN  :-" + ds2.Tables[0].Rows[0]["GSTNo"].ToString() + "</td>");
            sb.Append("<td class='text-left' style='width:33%'>City  :-" + ds2.Tables[0].Rows[0]["TownOrVillage"].ToString() + " / " + ddlLocation.SelectedItem.Text + "</td>");
            sb.Append("<td class='text-Right' style='width:33%'>Bill Generate Date  :-" + ds2.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left'  style='width:33%'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
            sb.Append("<td class='text-left' style='width:33%'>PanNo.  :-" + ds2.Tables[0].Rows[0]["PANNo"].ToString() + "</td>");
            sb.Append("<td class='text-Right' style='width:33%'>Address  :-" + ds2.Tables[0].Rows[0]["DAddress"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' style='width:33%'>" + txtDeliveryDate.Text + "</td>");
            sb.Append("<td class='text-left' style='width:33%'>Security  :-" + ds2.Tables[0].Rows[0]["SecurityDeposit"].ToString() + "</td>");
            sb.Append("<td class='text-right' style='width:33%'>" + dsDist.Rows[0]["RName"].ToString() + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("<table class='table table1-bordered'>");
            int Count = dsDist.Rows.Count;
            int ColCount = dsDist.Columns.Count;
            sb.Append("<thead style='padding-left:0px;'>");
            sb.Append("<td style='width:120px'>Particulars</td>");
            sb.Append("<td>Qty(In Pkt)</td>");
            sb.Append("<td>Qty(In Ltr.)</td>");
            sb.Append("<td>Return Qty (In Pkt.)</td>");
            sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
            sb.Append("<td>Adv. Card Price</td>");
            sb.Append("<td>Total Adv. Card Amt</td>");
            sb.Append("<td style='display:none'>Adv. Card Margin</td>");
            sb.Append("<td style='display:none'>Adv. Card Margin Amt</td>");
            sb.Append("<td>Inst. Qty</td>");
            sb.Append("<td>Inst. Total Amt</td>");
            sb.Append("<td style='display:none'>Inst. Margin</td>");
            sb.Append("<td style='display:none'>Inst. Tran Margin Amt</td>");

            sb.Append("<td style='display:none'>(Distri. + trans) Margin on cash supply</td>");
            sb.Append("<td style='display:none'>(Distri. + trans) Margin Amt on cash supply</td>");

            sb.Append("<td>Billing Qty(In Pkt.)</td>");
            sb.Append("<td>Billing Qty(In Ltr.)</td>");
            sb.Append("<td>Rate (Per Ltr.)</td>");
            sb.Append("<td>Amount</td>");
            sb.Append("<td>Payble Amount</td>");
            sb.Append("</thead>");

            for (int i = 0; i < Count; i++)
            {

                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'>" + dsDist.Rows[i]["IName"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalReturnQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQty"] + "</td>");
                sb.Append("<td>" + (dsDist.Rows[i]["TotalAdvCardQty"].ToString() == "0" ? "0.000" : dsDist.Rows[i]["RatePerLtrAdCard"]) + "</td>");
                sb.Append("<td>" + (Convert.ToDecimal(dsDist.Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(dsDist.Rows[i]["RatePerLtrAdCard"])).ToString("0.000") + "</td>");
                sb.Append("<td style='display:none'>" + dsDist.Rows[i]["AdvCardComm"] + "</td>");
                sb.Append("<td style='display:none'>" + dsDist.Rows[i]["AdvCardAmt"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalInstSupplyQty"] + "</td>");
               // sb.Append("<td>" + (Convert.ToDecimal(dsDist.Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(dsDist.Rows[i]["InstRatePerLtr"])).ToString("0.000") + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalInstAmount"] + "</td>");
                sb.Append("<td style='display:none'>" + dsDist.Rows[i]["InstTransComm"] + "</td>");
                sb.Append("<td style='display:none'>" + dsDist.Rows[i]["InstTransCommAmt"] + "</td>");

                sb.Append("<td style='display:none'>" + (Convert.ToDecimal(dsDist.Rows[i]["DistOrSSComm"]) + Convert.ToDecimal(dsDist.Rows[i]["TransComm"])).ToString() + "</td>");
                sb.Append("<td style='display:none'>" + ((Convert.ToDecimal(dsDist.Rows[i]["DistOrSSComm"]) + Convert.ToDecimal(dsDist.Rows[i]["TransComm"])) * Convert.ToDecimal(dsDist.Rows[i]["BillingQty"])).ToString("0.000") + "</td>");

                sb.Append("<td>" + dsDist.Rows[i]["BillingQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["BillingQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["RatePerLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["Amount"] + "</td>");
                sb.Append("<td>" + (Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(dsDist.Rows[i]["InstTransCommAmt"]) - ((Convert.ToDecimal(dsDist.Rows[i]["DistOrSSComm"]) + Convert.ToDecimal(dsDist.Rows[i]["TransComm"])) * Convert.ToDecimal(dsDist.Rows[i]["BillingQty"]))) + "</td>");
                sb.Append("</tr>");

                totaladvAmt += (Convert.ToDecimal(dsDist.Rows[i]["TotalAdvCardQtyInLtr"]) * Convert.ToDecimal(dsDist.Rows[i]["RatePerLtrAdCard"]));
                //totalInstAmt += (Convert.ToDecimal(dsDist.Rows[i]["TotalInstSupplyQtyInLtr"]) * Convert.ToDecimal(dsDist.Rows[i]["InstRatePerLtr"]));
                totalInstAmt += (Convert.ToDecimal(dsDist.Rows[i]["TotalInstAmount"]));
                totalInstTranCommAmt += Convert.ToDecimal(dsDist.Rows[i]["InstTransCommAmt"]);
                totalAdvCrdCmmAmt += Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]);
                totalamt += Convert.ToDecimal(dsDist.Rows[i]["Amount"]);
                paybleAmt += ((Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(dsDist.Rows[i]["InstTransCommAmt"])) - ((Convert.ToDecimal(dsDist.Rows[i]["DistOrSSComm"]) + Convert.ToDecimal(dsDist.Rows[i]["TransComm"])) * Convert.ToDecimal(dsDist.Rows[i]["BillingQty"])));

                totaldiscomamt += Convert.ToDecimal((double.Parse(dsDist.Rows[i]["DistOrSSComm"].ToString()) + double.Parse(dsDist.Rows[i]["TransComm"].ToString())) * double.Parse(dsDist.Rows[i]["BillingQty"].ToString()));
                totalbillingqty += Convert.ToInt32(dsDist.Rows[i]["BillingQty"]);
                totalsupplyqty += Convert.ToInt32(dsDist.Rows[i]["SupplyTotalQty"]);
                totalsupplyqtyinltr += Convert.ToDecimal(dsDist.Rows[i]["SupplyTotalQtyInLtr"]);
                totalbillingqtyinltr += Convert.ToInt32(dsDist.Rows[i]["BillingQtyInLtr"]);
                totaladvancecardqty += Convert.ToInt32(dsDist.Rows[i]["TotalAdvCardQty"]);
                totalinstituteqty += Convert.ToInt32(dsDist.Rows[i]["TotalInstSupplyQty"]);
                totalreturnqty += Convert.ToInt32(dsDist.Rows[i]["TotalReturnQty"]);
                totalcashsale += (Convert.ToDecimal(dsDist.Rows[i]["BillingQty"]) * Convert.ToDecimal(dsDist.Rows[i]["FinalRetailerrate"]));

            }
            sb.Append("<tr>");
            int ColumnCount = dsDist.Columns.Count-3;

            //for (int i =0; i < ColumnCount-9; i++) // privious
            for (int i = 0; i < ColumnCount - 14; i++)
            {
                if (i == 0)
                {
                    sb.Append("<td style='text-align:left'><b>Total</b></td>");
                }
                //else if (i == ColumnCount - 30)
                else if (i == ColumnCount - 30)
                {
                    sb.Append("<td><b>" + totalsupplyqty.ToString() + "</b></td>");
                }
                else if (i == ColumnCount - 29)
                {
                    sb.Append("<td><b>" + totalsupplyqtyinltr.ToString() + "</b></td>");
                }
                else if (i == ColumnCount - 28)
                {
                    sb.Append("<td><b>" + totalreturnqty.ToString() + "</b></td>");
                }
                else if (i == ColumnCount - 27)
                {
                    sb.Append("<td><b>" + totaladvancecardqty.ToString() + "</b></td>");
                }
                else if (i == ColumnCount - 25)
                {
                    sb.Append("<td><b>" + totaladvAmt.ToString("0.000") + "</b></td>");
                }
                else if (i == ColumnCount - 24)
                {
                    sb.Append("<td style='display:none'><b>" + totalAdvCrdCmmAmt.ToString("0.000") + "</b></td>");
                }
                else if (i == ColumnCount - 24)
                {
                    sb.Append("<td><b>" + totalinstituteqty.ToString() + "</b></td>");
                }
                else if (i == ColumnCount - 22)
                {
                    sb.Append("<td><b>" + totalInstAmt.ToString("0.000") + "</b></td>");
                }
                else if (i == ColumnCount - 21)
                {
                    sb.Append("<td style='display:none'><b>" + totalInstTranCommAmt.ToString("0.000") + "</b></td>");
                }
                else if (i == ColumnCount - 20)
                {
                    sb.Append("<td style='display:none'><b>" + totaldiscomamt.ToString("0.000") + "</b></td>");
                }
                else if (i == ColumnCount - 19)
                {
                    sb.Append("<td><b>" + totalbillingqty.ToString() + "</b></td>");
                }
                else if (i == ColumnCount - 18)
                {
                    sb.Append("<td><b>" + totalbillingqtyinltr.ToString() + "</b></td>");
                }
                //else if (i == ColumnCount - 17)
                //{
                //    sb.Append("<td><b>" + totaltranscomamt.ToString("0.000") + "</b></td>");
                //}
                //else if (i == ColumnCount - 17)
                //{
                //    sb.Append("<td></td>");
                //}

                //else if (i == ColumnCount - 16)
                //{
                //    sb.Append("<td></td>");
                //}
                else if (i == ColumnCount - 16)
                {
                    sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                }

                else if (i == ColumnCount - 15)
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
            sb.Append("<td><b>Tcs on Sales @</b>" + (lblTcsTax.Text != "0.000" ? lblTcsTax.Text : "NA") + "</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td><b>" + (lblTcsTax.Text != "0.000" ? lblTcsTaxAmt.Text : "NA") + "</b></td>");
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
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<td style='display:none'></td>");
            sb.Append("<tdstyle='display:none'></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td><b>" + lblFinalPaybleAmount.Text + "</b></td>");
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
            sb.Append("<td style='display:none' class='text-right' ><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</b></td>");
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
            sb.Append("<td style='text-right'><b>" + (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt).ToString("0.00") + "</b></td>");
            sb.Append("<td class='text-center' ></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td style='text-left;width:250px'></td>");
            sb.Append("<td style='text-right'><b>Net Milk Sale Amount</b></td>");
            sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
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
            sb.Append("<td style='text-right'><b>" + ((totaladvAmt + totalcashsale) - (totaldiscomamt + totalAdvCrdCmmAmt + totalInstTranCommAmt)).ToString("0.00") + "</b></td>");
            sb.Append("<td class='text-center' ></td>");
            sb.Append("</tr>");

            //sb.Append("<tr>");
            //sb.Append("<td style='text-left'>Prepared & Checked by</td>");
            //sb.Append("<td style='padding-left:550px;'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td></td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>Note:</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");

            //sb.Append("<td class='text-left' colspan='2'>1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>2 . Please quote our Bill No. while remiting the amount.</td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>3 . All Payment to be made by Bank Draft payable to  :-" + ViewState["Office_Name"].ToString() + "</td>");
            //sb.Append("</tr>");
            sb.Append("</br><tr>");
            sb.Append("<td class='text-left' >Remark :" + TxtRemark.Text + "</td>");
          

            sb.Append("</tr></br>");

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
            sb.Append("</div>");
            sb.Append("</div>");

            div_page_content.InnerHtml = sb.ToString();
            if (dsDist != null) { dsDist.Dispose(); }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);

            ////////////////End Of Route Wise Print Code   ///////////////////////
        }

    }

    private void PrintInstData()
    {

        DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        //for remark if saved
        ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
         new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "OrganizationId", "AreaId" },
           new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlInstitution.Text, ddlLocation.SelectedValue }, "dataset");
        if (ds2.Tables.Count > 0)
        {
            if (ds2.Tables[0].Rows.Count > 0)
            {
                // TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                //lblinvoiceno.Text = ds2.Tables[0].Rows[0]["InvoiceNo"].ToString();
				lblinvoiceno.Text = ds2.Tables[0].Rows[0]["NewInvoiceNo"].ToString();
                if (ds2.Tables[0].Rows[0]["Remark"].ToString() == "")
                {
                    TxtRemark.Text = "";
                }
                else
                {
                    TxtRemark.Text = ds2.Tables[0].Rows[0]["Remark"].ToString();
                    TxtRemark.Enabled = false;
                }
            }
            else
            {
                TxtRemark.Text = "";
            }
        }

        StringBuilder sb = new StringBuilder();
        decimal totalamt = 0;
        if (!string.IsNullOrEmpty(ViewState["PrintDistOrInt"].ToString()))
        {
            DataTable dsInst = (DataTable)ViewState["PrintDistOrInt"];
            div_page_content.InnerHtml = "";
            sb.Append("<div class='table-responsive'");
            sb.Append("<div class='content' style='border: 0px solid black'>");


            sb.Append("<table class='table1' style='width:100%; height:100%'>");
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
            sb.Append("<td class='text-left' colspan='2'>invoice No. :" + lblinvoiceno.Text + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>M/s  :-" + dsInst.Rows[0]["BName"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
            sb.Append("<td class='text-right'>" + dsInst.Rows[0]["RName"].ToString() + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("<div class='table table1-bordered'>");
            sb.Append("<table class='table table1-bordered'>");
            int Count = dsInst.Rows.Count;
            int ColCount = dsInst.Columns.Count;
            sb.Append("<thead style='padding-left:0px;'>");
            sb.Append("<td style='width:120px'>Particulars</td>");
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
                sb.Append("<td style='text-align:left'>" + dsInst.Rows[i]["IName"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["SupplyTotalQty"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["TotalAdvCardQty"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["TotalReturnQty"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["BillingQty"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["BillingQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["RatePerLtr"] + "</td>");
                sb.Append("<td>" + dsInst.Rows[i]["Amount"] + "</td>");
                sb.Append("</tr>");

                totalamt += Convert.ToDecimal(dsInst.Rows[i]["Amount"]);
            }
            sb.Append("<tr>");
            int ColumnCount = dsInst.Columns.Count;

            //for (int i =0; i < ColumnCount-9; i++) // privious
            for (int i = 0; i < ColumnCount - 9; i++)
            {
                if (i == 0)
                {
                    sb.Append("<td><b>Total</b></td>");
                }
                // else if (i == ColumnCount-10)
                else if (i == 8)
                {
                    sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                }
                //else if (i == ColumnCount - 5)
                //{
                //    sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
                //}
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
            sb.Append("<td style='padding-left:550px;'>For :-" + ViewState["Office_Name"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-center' colspan='2'><b>General Manager (Marketing)/Asst.Gen.Manager (Marketing)<b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("</br><tr>");
            sb.Append("<td class='text-left' >Remark :" + TxtRemark.Text + "</td>");


            sb.Append("</tr></br>");
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

            div_page_content.InnerHtml = sb.ToString();
            if (dsInst != null) { dsInst.Dispose(); }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);

            ////////////////End Of Route Wise Print Code   ///////////////////////
        }

    }

    private void GetTcsTax()
    {
        try
        {
            if (ddlDitributor.SelectedValue != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds8 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deliverydate, ddlDitributor.SelectedValue }, "dataset");

                if (ds8.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds8.Tables[0].Rows[0]["Tval"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
        }
        finally
        {
            if (ds8 != null) { ds8.Dispose(); }
        }
    }
}