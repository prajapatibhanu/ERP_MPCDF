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

public partial class mis_DemandSupply_InvoiceGenerateDistOrInst_GDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6, ds7, ds8, ds3 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0, totalsupply = 0.00, finalsupply = 0.00, totaladvAmt = 0.00, tcstax = 0.000, instmarginamt = 0.000, tcstaxAmt = 0.000, PaybleAmtWithTcsTax = 0.000;

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
               // GetCategory();
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
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
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
                  new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue, objdb.GetMilkCatId() }, "dataset");

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
    //        ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //}
    private void GenerateInvoice()
    {
        try
        {
            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (ddlInvoiceFor.SelectedValue == "1")
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "DistributorId", "OrganizationId", "AreaId" },
                       new string[] { "9", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue, "0", ddlLocation.SelectedValue }, "dataset");
            }
            else
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "OrganizationId", "AreaId" },
                       new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), ddlRoute.SelectedValue, ddlInstitution.SelectedValue, ddlLocation.SelectedValue }, "dataset");
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
                    dtdist.Columns.Add("TotalAdvCardQty", typeof(int));
                    dtdist.Columns.Add("TotalReturnQty", typeof(int));
                    dtdist.Columns.Add("SupplyTotalQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalAdvCardQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("TotalReturnQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("AdvCardComm", typeof(decimal));
                    dtdist.Columns.Add("AdvCardAmt", typeof(decimal));
                    dtdist.Columns.Add("DistOrInstRate", typeof(decimal));
                    dtdist.Columns.Add("BillingQty", typeof(int));
                    dtdist.Columns.Add("BillingQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("Amount", typeof(decimal));
                    dtdist.Columns.Add("PaybleAmount", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtrAdCard", typeof(decimal));
                    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    dtdist.Columns.Add("InstRatePerLtr", typeof(decimal));
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
                        drdist[7] = ds1.Tables[0].Rows[j]["AdvCardComm"].ToString();
                        drdist[8] = ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString();
                        drdist[9] = ds1.Tables[0].Rows[j]["DistOrSSRate"].ToString();
                        drdist[10] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                        drdist[11] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                        drdist[12] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                        drdist[13] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        drdist[14] = Convert.ToDecimal(ds1.Tables[0].Rows[j]["Amount"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString());
                        drdist[15] = ds1.Tables[0].Rows[j]["RatePerLtrAdCard"].ToString();
                        drdist[16] = ds1.Tables[0].Rows[j]["TotalInstSupplyQty"].ToString();
                        drdist[17] = ds1.Tables[0].Rows[j]["TotalInstSupplyQtyInLtr"].ToString();
                        drdist[18] = ds1.Tables[0].Rows[j]["InstTransComm"].ToString();
                        drdist[19] = ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString();
                        drdist[20] = ds1.Tables[0].Rows[j]["InstRatePerLtr"].ToString();
                       

                        dtdist.Rows.Add(drdist.ItemArray);

                    }
                    ViewState["ItemDetails"] = dtdist;


                    if (dtdist != null) { dtdist.Dispose(); }
                    GridView1.Visible = true;
                    GridView2.Visible = false;


                    GridView2.DataSource = null;
                    GridView2.DataBind();
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
                    dtdist.Columns.Add("BillingQty", typeof(int));
                    dtdist.Columns.Add("BillingQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("Amount", typeof(decimal));
                    dtdist.Columns.Add("PaybleAmount", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtrAdCard", typeof(decimal));
                    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    dtdist.Columns.Add("InstRatePerLtr", typeof(decimal));
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
                        drdist[10] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                        drdist[11] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                        drdist[12] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                        drdist[13] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        drdist[14] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        drdist[15] = "0";
                        drdist[16] = "0";
                        drdist[17] = "0";
                        drdist[18] = "0";
                        drdist[19] = "0";
                        drdist[20] = "0";

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
        if (ddlLocation.SelectedValue != "0")
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
      //  ddlItemCategory.SelectedIndex = 0;
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
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblInstTransCommAmt = (e.Row.FindControl("lblInstTransCommAmt") as Label);

                totaladvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
                instmarginamt += Convert.ToDouble(lblInstTransCommAmt.Text);
                totalsupply += Convert.ToDouble(lblAmount.Text);

                finalsupply += Convert.ToDouble(lblFAmount.Text);

               // totaladvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblTAmount = (e.Row.FindControl("lblTAmount") as Label);
                Label lblFinalAmount = (e.Row.FindControl("lblFinalAmount") as Label);
                Label lblFInstMarAmt = (e.Row.FindControl("lblFInstMarAmt") as Label);
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

                            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert_GDS",
                                 new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                                     "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId","TcsTaxPer" },
                                   new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, 
                                      objdb.GetMilkCatId(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue
                                       , "0", FPaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress,ddlLocation.SelectedValue,ViewState["Tval"].ToString() }
                                       , "type_Trn_MilkOrProductInvoiceDetailChild_GDS", dtInsert, "dataset");

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

                        ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert_GDS",
                             new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                                     "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId" },
                               new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, 
                                       objdb.GetMilkCatId(), ddlRoute.SelectedValue, "0"
                                       , ddlInstitution.SelectedValue, PaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress,ddlLocation.SelectedValue }
                                   , "type_Trn_MilkOrProductInvoiceDetailChild_GDS", dtInsert, "dataset");

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
        StringBuilder sb = new StringBuilder();
        decimal totalamt = 0, paybleAmt = 0, totaladvAmt = 0, totalAdvCrdCmmAmt = 0, totalInstAmt = 0, totalInstTranCommAmt = 0, tcstamt = 0, fpaybleamt = 0;
        if (!string.IsNullOrEmpty(ViewState["PrintDistOrInt"].ToString()))
        {
            DataTable dsDist = (DataTable)ViewState["PrintDistOrInt"];
            div_page_content.InnerHtml = "";

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
            sb.Append("<td class='text-left' colspan='2'>M/s  :-" + dsDist.Rows[0]["DName"].ToString() + "<td class='blank_td' style='width: 250px;'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>Add:" + dsDist.Rows[0]["DAddress"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>GST No.: " + dsDist.Rows[0]["GSTNo"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
            sb.Append("<td class='text-right'>" + dsDist.Rows[0]["RName"].ToString() + "</td>");
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
                sb.Append("<td>" + dsDist.Rows[i]["IName"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalReturnQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["AdvCardComm"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["AdvCardAmt"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalInstSupplyQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["TotalInstSupplyQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["InstTransComm"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["InstTransCommAmt"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["BillingQty"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["BillingQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["RatePerLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["Amount"] + "</td>");
                sb.Append("<td>" + (Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(dsDist.Rows[i]["InstTransCommAmt"])) + "</td>");
                sb.Append("</tr>");

                totalInstTranCommAmt += Convert.ToDecimal(dsDist.Rows[i]["InstTransCommAmt"]);
                totalAdvCrdCmmAmt += Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]);
                totalamt += Convert.ToDecimal(dsDist.Rows[i]["Amount"]);
                paybleAmt += ((Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]) - Convert.ToDecimal(dsDist.Rows[i]["InstTransCommAmt"])));


            }
            sb.Append("<tr>");
            int ColumnCount = dsDist.Columns.Count;

            //for (int i =0; i < ColumnCount-9; i++) // privious
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
            sb.Append("<td><b>Tcs on Sales @</b>" + (lblTcsTax.Text != "0.000" ? lblTcsTax.Text : "NA") + "</td>");
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
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td><b>" + lblFinalPaybleAmount.Text + "</b></td>");
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

            div_page_content.InnerHtml = sb.ToString();
            if (dsDist != null) { dsDist.Dispose(); }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);

            ////////////////End Of Route Wise Print Code   ///////////////////////
        }

    }

    private void PrintInstData()
    {

        StringBuilder sb = new StringBuilder();
        decimal totalamt = 0;
        if (!string.IsNullOrEmpty(ViewState["PrintDistOrInt"].ToString()))
        {
            DataTable dsInst = (DataTable)ViewState["PrintDistOrInt"];
            div_page_content.InnerHtml = "";

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
            sb.Append("<td class='text-left' colspan='2'>M/s  :-" + dsInst.Rows[0]["BName"].ToString() + "<td class='blank_td' style='width: 250px;'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>Add:" + dsInst.Rows[0]["BAddress"].ToString() + "</td><td class='blank_td' style='width: 250px;'></td>");
            sb.Append("</tr>");
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
            sb.Append("<thead>");
            sb.Append("<td>Particulars</td>");
            sb.Append("<td>Qty(In Pkt)</td>");
            sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
            sb.Append("<td>Return Qty (In Pkt.)</td>");
            sb.Append("<td>Adv. Card Qty (In Ltr.)</td>");
            sb.Append("<td>Billing Qty(In Pkt.)</td>");
            sb.Append("<td>Billing Qty(In Ltr.)</td>");
            sb.Append("<td>Rate (Per Ltr.)</td>");
            sb.Append("<td>Net Payble Amount</td>");
            sb.Append("</thead>");

            for (int i = 0; i < Count; i++)
            {

                sb.Append("<tr>");
                sb.Append("<td>" + dsInst.Rows[i]["IName"] + "</td>");
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
                else if (i == ColumnCount - 10)
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