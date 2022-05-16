using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_Monthly_distributor_rural_invoice : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6, ds7, ds8 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0, totalsupply = 0.00, finalsupply = 0.00, totalAdvAmt = 0.00, tcstax = 0.000, tcstaxAmt = 0.000, PaybleAmtWithTcsTax = 0.000;

    int cellIndex = 2;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                GetLocation();
               // GetShift();
                
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
            ddlLocation.SelectedValue = "2";
            ddlLocation.Enabled = false;

            ddlLocation_SelectedIndexChanged(sender,  e);
            //ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
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


            //ddlShift.DataTextField = "ShiftName";
            //ddlShift.DataValueField = "Shift_id";
            //ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
            //     new string[] { "flag" },
            //       new string[] { "1" }, "dataset");
            //ddlShift.DataBind();
            //ddlShift.Items.Insert(0, new ListItem("Select", "0"));


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
    //private void GetRoute()
    //{
    //    try
    //    {

    //        ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
    //             new string[] { "flag", "Office_ID" },
    //             new string[] { "1", objdb.Office_ID() }, "dataset");
    //        ddlRoute.DataTextField = "RNameOrNo";
    //        ddlRoute.DataValueField = "RouteId";
    //        ddlRoute.DataBind();
    //        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
    //    }
    //}
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
                new string[] { "flag", "Office_ID", "RouteId" },
                  new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue }, "dataset");

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
            // ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
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
            DateTime odate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
            DateTime cdate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
            string fromdate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string Todate = cdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (ddlInvoiceFor.SelectedValue == "1")
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductRuraldistributer_MonthlyInvoice",
                     new string[] { "flag", "From_Date","To_date",  "ItemCat_id", "Office_ID", "RouteId", "DistributorId", "OrganizationId", "AreaId" },
                       new string[] { "1", fromdate.ToString(), Todate.ToString(), ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue, "0", ddlLocation.SelectedValue }, "dataset");
                // GetVehicleNo(ddlRoute.SelectedValue, ddlDitributor.SelectedValue, "0");
            }
            //else
            //{
            //    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
            //         new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "OrganizationId", "AreaId" },
            //           new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlRoute.SelectedValue, ddlInstitution.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            //    // GetVehicleNo(ddlRoute.SelectedValue, "0", ddlInstitution.SelectedValue);
            //}

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["PrintDistOrInt"] = ds1.Tables[0];
                lblFinalPaybleAmount.Text = string.Empty;
                lblTcsTax.Text = string.Empty;
                lblTcsTaxAmt.Text = string.Empty;
                if (ddlInvoiceFor.SelectedValue == "1")
                {
                    GetTcsTax();
                    //lblMsName.Text = ds1.Tables[0].Rows[0]["DName"].ToString();
                    lblMsName.Text = ddlDitributor.SelectedItem.Text;
                   // lblRouteName.Text = ds1.Tables[0].Rows[0]["RName"].ToString();
                    lblRouteName.Text = ddlLocation.SelectedItem.Text;


                    GridView1.DataSource = ds1.Tables[0];
                    GridView1.DataBind();



                    DataTable dtdist = new DataTable();
                    DataRow drdist;

                    dtdist.Columns.Add("Item_id", typeof(int));
                    //dtdist.Columns.Add("SupplyTotalQty", typeof(int));
                    //dtdist.Columns.Add("TotalAdvCardQty", typeof(int));
                    //dtdist.Columns.Add("TotalReturnQty", typeof(int));
                    dtdist.Columns.Add("SupplyTotalQtyInLtr", typeof(decimal));
                    //dtdist.Columns.Add("TotalAdvCardQtyInLtr", typeof(decimal));
                    //dtdist.Columns.Add("TotalReturnQtyInLtr", typeof(decimal));
                    //dtdist.Columns.Add("AdvCardComm", typeof(decimal));
                    //dtdist.Columns.Add("AdvCardAmt", typeof(decimal));
                    //dtdist.Columns.Add("DistOrInstRate", typeof(decimal));
                    //dtdist.Columns.Add("BillingQty", typeof(int));
                    //dtdist.Columns.Add("BillingQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("RatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("Amount", typeof(decimal));

                    //dtdist.Columns.Add("PaybleAmount", typeof(decimal));
                    //dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    //dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    //dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    //dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    drdist = dtdist.NewRow();
                    int Count = ds1.Tables[0].Rows.Count;
                    double totalAmt=0;

                    for (int j = 0; j < Count; j++)
                        {

                        drdist[0] = ds1.Tables[0].Rows[j]["Item_id"].ToString();
                        //drdist[1] = ds1.Tables[0].Rows[j]["SupplyTotalQty"].ToString();
                        //drdist[2] = ds1.Tables[0].Rows[j]["TotalAdvCardQty"].ToString();
                        //drdist[3] = ds1.Tables[0].Rows[j]["TotalReturnQty"].ToString();
                        drdist[1] = ds1.Tables[0].Rows[j]["SupplyTotalQtyInLtr"].ToString();
                        //drdist[5] = ds1.Tables[0].Rows[j]["TotalAdvCardQtyInLtr"].ToString();
                        //drdist[6] = ds1.Tables[0].Rows[j]["TotalReturnQtyInLtr"].ToString();
                        //drdist[7] = ds1.Tables[0].Rows[j]["AdvCardComm"].ToString();
                        //drdist[8] = ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString();
                        //drdist[9] = ds1.Tables[0].Rows[j]["DistOrSSRate"].ToString();
                        //drdist[10] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                        //drdist[11] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                        drdist[2] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                        drdist[3] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        //drdist[14] = Convert.ToDecimal(ds1.Tables[0].Rows[j]["Amount"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString());
                        //drdist[15] = ds1.Tables[0].Rows[j]["TotalInstSupplyQty"].ToString();
                        //drdist[16] = ds1.Tables[0].Rows[j]["TotalInstSupplyQtyInLtr"].ToString();
                        //drdist[17] = ds1.Tables[0].Rows[j]["InstTransComm"].ToString();
                        //drdist[18] = ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString();

                        dtdist.Rows.Add(drdist.ItemArray);
                      //  totalAmt = totalAmt + double.Parse(ds1.Tables[0].Rows[j]["Amount"].ToString());

                    }
                  //dtdist.Rows.Add
                    ViewState["ItemDetails"] = dtdist;
                     

                    if (dtdist != null) { dtdist.Dispose(); }
                    GridView1.Visible = true;
                    GridView2.Visible = false;

                    Label lbltotal = (Label)GridView1.FooterRow.FindControl("lblTAmount");
                    Label lblSupplyallTotalQtyInLtr = (Label)GridView1.FooterRow.FindControl("lblSupplyallTotalQtyInLtr");
                    lblSupplyallTotalQtyInLtr.Text = ds1.Tables[1].Rows[0]["SupplyallTotalQtyInLtr"].ToString();
                    lbltotal.Text = ds1.Tables[1].Rows[0]["Totalamount"].ToString();


                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
                //else
                //{

                //    GridView2.DataSource = ds1.Tables[0];
                //    GridView2.DataBind();

                //    DataTable dtdist = new DataTable();
                //    DataRow drdist;

                //    dtdist.Columns.Add("Item_id", typeof(int));
                //    dtdist.Columns.Add("SupplyTotalQty", typeof(int));
                //    dtdist.Columns.Add("TotalAdvCardQty", typeof(int));
                //    dtdist.Columns.Add("TotalReturnQty", typeof(int));
                //    dtdist.Columns.Add("SupplyTotalQtyInLtr", typeof(decimal));
                //    dtdist.Columns.Add("TotalAdvCardQtyInLtr", typeof(decimal));
                //    dtdist.Columns.Add("TotalReturnQtyInLtr", typeof(decimal));
                //    dtdist.Columns.Add("AdvCardComm", typeof(decimal));
                //    dtdist.Columns.Add("AdvCardAmt", typeof(decimal));
                //    dtdist.Columns.Add("DistOrInstRate", typeof(decimal));
                //    dtdist.Columns.Add("BillingQty", typeof(int));
                //    dtdist.Columns.Add("BillingQtyInLtr", typeof(decimal));
                //    dtdist.Columns.Add("RatePerLtr", typeof(decimal));
                //    dtdist.Columns.Add("Amount", typeof(decimal));
                //    dtdist.Columns.Add("PaybleAmount", typeof(decimal));
                //    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                //    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                //    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                //    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                //    drdist = dtdist.NewRow();
                //    int Count = ds1.Tables[0].Rows.Count;

                //    for (int j = 0; j < Count; j++)
                //    {

                //        drdist[0] = ds1.Tables[0].Rows[j]["Item_id"].ToString();
                //        drdist[1] = ds1.Tables[0].Rows[j]["SupplyTotalQty"].ToString();
                //        drdist[2] = ds1.Tables[0].Rows[j]["TotalAdvCardQty"].ToString();
                //        drdist[3] = ds1.Tables[0].Rows[j]["TotalReturnQty"].ToString();
                //        drdist[4] = ds1.Tables[0].Rows[j]["SupplyTotalQtyInLtr"].ToString();
                //        drdist[5] = ds1.Tables[0].Rows[j]["TotalAdvCardQtyInLtr"].ToString();
                //        drdist[6] = ds1.Tables[0].Rows[j]["TotalReturnQtyInLtr"].ToString();
                //        drdist[7] = "0";
                //        drdist[8] = "0";
                //        drdist[9] = ds1.Tables[0].Rows[j]["DistOrSSRate"].ToString();
                //        drdist[10] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                //        drdist[11] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                //        drdist[12] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                //        drdist[13] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                //        drdist[14] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                //        drdist[15] = "0";
                //        drdist[16] = "0";
                //        drdist[17] = "0";
                //        drdist[18] = "0";

                //        dtdist.Rows.Add(drdist.ItemArray);

                //    }
                //    ViewState["ItemDetails"] = dtdist;


                //    if (dtdist != null) { dtdist.Dispose(); }

                //    GridView1.Visible = false;
                //    GridView2.Visible = true;

                //    GridView1.DataSource = null;
                //    GridView1.DataBind();


                //    lblMsName.Text = ds1.Tables[0].Rows[0]["BName"].ToString();
                //    lblRouteName.Text = ds1.Tables[0].Rows[0]["RName"].ToString();
                //}
                lblOName1.Text = ViewState["Office_Name"].ToString();
                lblOName2.Text = ViewState["Office_Name"].ToString();
                lblOName3.Text = ViewState["Office_Name"].ToString();
                lblGST.Text = ViewState["Office_GST"].ToString();
                //lblDelishift.Text = ddlShift.SelectedItem.Text;
                //lbldelidate.Text = txtDeliveryDate.Text;
                //lblDelivarydate.Text = txtDeliveryDate.Text;
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
       // GetInstitution();
        ddlInvoiceFor_SelectedIndexChanged(sender, e);
    }
    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //string did = "", oid = "";
            //if(ddlInvoiceFor.SelectedValue=="1")
            //{
            //    did = ddlDitributor.SelectedValue;
            //    oid = "";
            //}
            //else
            //{
            //    did = "";
            //    oid = ddlInstitution.SelectedValue;
            //}
          //  GenerateInvoice();
            lblMsg.Text = "";
             DateTime odate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
            DateTime cdate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
            string fromdate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string Todate = cdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (ddlInvoiceFor.SelectedValue == "1")
            {
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductRuraldistributer_MonthlyInvoice",
                     new string[] { "flag", "From_Date","To_date",  "ItemCat_id", "Office_ID", "RouteId", "DistributorId", "OrganizationId", "AreaId" },
                       new string[] { "1", fromdate.ToString(), Todate.ToString(), ddlItemCategory.SelectedValue, objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue, "0", ddlLocation.SelectedValue }, "dataset");
               
            }
           

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["PrintDistOrInt"] = ds1.Tables[0];
                PrintDistData1();
            }
            else
            {
                Button1.Visible = false;
                div_page_contentpage.InnerHtml = null;
                div_page_content.InnerHtml = null;
                ViewState["PrintDistOrInt"] = null;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :", " Record not found");
            }
           
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        //txtDeliveryDate.Text = string.Empty;
        ddlRoute.SelectedIndex = 0;
        ddlInvoiceFor.SelectedIndex = 0;
        pnldistorss.Visible = false;
        pnlInstitution.Visible = false;
        ddlDitributor.SelectedIndex = 0;
        ddlInstitution.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        pnlData.Visible = false;
        lblVehicleNo.Text = string.Empty;
        //ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        lblFinalPaybleAmount.Text = string.Empty;
        lblTcsTax.Text = string.Empty;
        lblTcsTaxAmt.Text = string.Empty;
        txtfromDate.Text = "";
        txttodate.Text = "";

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
                totalsupply += Convert.ToDouble(lblAmount.Text);

                finalsupply += Convert.ToDouble(lblFAmount.Text);

                totalAdvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblTAmount = (e.Row.FindControl("lblTAmount") as Label);
                Label lblFinalAmount = (e.Row.FindControl("lblFinalAmount") as Label);
                lblTAmount.Text = totalsupply.ToString("0.000");
                lblFinalAmount.Text = finalsupply.ToString("0.000");
                lblTotalAdvCardAmt.Text = totalAdvAmt.ToString("0.000");
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
    //private void GetVehicleNo(string routid, string distit, string instid)
    //{
    //    try
    //    {
    //        DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
    //        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
    //                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date",
    //                                 "RouteId", "DistributorId", "OrganizationId" },
    //                       new string[] { "15", objdb.Office_ID(),ddlShift.SelectedValue,ddlItemCategory.SelectedValue, deliverydate
    //                       , routid ,distit,instid}, "dataset");

    //        if (ds5.Tables[0].Rows.Count > 0)
    //        {
    //            DataTable dt5 = new DataTable();
    //            dt5 = ds5.Tables[0];


    //            string output = "";
    //            for (int i = 0; i < dt5.Rows.Count; i++)
    //            {
    //                output = output + dt5.Rows[i]["VehicleNo"].ToString();
    //                output += (i != (dt5.Rows.Count - 1)) ? "," : string.Empty;
    //            }
    //            lblVehicleNo.Text = output;

    //            if (dt5 != null) { dt5.Dispose(); }
    //        }
    //        else
    //        {
    //            lblVehicleNo.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds5 != null) { ds5.Dispose(); }
    //    }

    //}
    #endregion===========================
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                //DataTable dtInsert = new DataTable();
                //dtInsert = null;
                //dtInsert = (DataTable)ViewState["ItemDetails"];
                //if (dtInsert.Rows.Count > 0)
                //{
                //    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                //   // Decimal PaybleAmount = dtInsert.AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                //    if (ddlInvoiceFor.SelectedValue == "1")
                //    {
                        //if (PaybleAmount > 0)
                        //{
                        //Decimal FPaybleAmount = Convert.ToDecimal(lblFinalPaybleAmount.Text);
                        //DateTime odate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
                        //DateTime cdate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
                        //string fromdate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        //string Todate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                        //ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert",
                        //     new string[] { "flag", "From_Date","To_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                        //             "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId","TcsTaxPer" },
                        //       new string[] { "1", fromdate.ToString(),Todate.ToString(), 
                        //               ddlItemCategory.SelectedValue, ddlRoute.SelectedValue, ddlDitributor.SelectedValue
                        //               , "0", FPaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress,ddlLocation.SelectedValue,ViewState["Tval"].ToString() }
                        //           , "type_Trn_MilkOrProductInvoiceDetailChild", dtInsert, "dataset");

                        //if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        //{

                        //    string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        //    PrintDistData();
                        //    pnlData.Visible = false;
                        //    btnPrint.Visible = false;
                        //    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);
                        //}
                        //else
                        //{
                            //string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            //if (error == "Already")
                            //{
                            //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice already Saved. ");
                                //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);
                                PrintDistData();
                               
                            //}
                            //else
                            //{
                            //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Save Data :" + error);
                            //}
                       // }
                        //}
                        //else
                        //{
                        //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Invoice Not Saved and Print");
                        //    return;
                        //}

                  //  }
                    //else
                    //{
                    //    DateTime odate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
                    //    DateTime cdate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
                    //    string fromdate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    //    string Todate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                    //    ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert",
                    //         new string[] { "flag", "From_Date","To_Date",  "ItemCat_id", "RouteId", "DistributorId",
                    //                 "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId" },
                    //           new string[] { "1", fromdate.ToString(),Todate.ToString(), 
                    //                   ddlItemCategory.SelectedValue, ddlRoute.SelectedValue, "0"
                    //                   , ddlInstitution.SelectedValue, PaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress,ddlLocation.SelectedValue }
                    //               , "type_Trn_MilkOrProductInvoiceDetailChild", dtInsert, "dataset");

                    //    if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    //    {

                    //        string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    //        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                    //        // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);
                    //        PrintInstData();
                    //        pnlData.Visible = false;
                    //        btnPrint.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    //        if (error == "Already")
                    //        {
                    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice already Exist. ");
                    //            PrintInstData();
                    //            //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);
                    //        }
                    //        else
                    //        {
                    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Save Data :" + error);
                    //        }
                    //    }

                    //}

            //    }

            //    if (dtInsert != null) { dtInsert.Dispose(); }

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

    private void PrintDistData()
    {
        StringBuilder sb = new StringBuilder();
        decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, totalqtyinltr = 0, totrebateamount = 0, grandtotal = 0;
        if (!string.IsNullOrEmpty(ViewState["PrintDistOrInt"].ToString()))
        {
            DataTable dsDist = (DataTable)ViewState["PrintDistOrInt"];
            div_page_content.InnerHtml = "";
           // div_page_contentpage.InnerHtml = "";
            GetTcsTax();
            GetRebate();
            DateTime Ddate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
            // string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string Month = Ddate.ToString("MMMdd", CultureInfo.InvariantCulture);
            string MonthYear = Ddate.ToString("MMM-yy", CultureInfo.InvariantCulture);
            string printDate = Ddate.AddMonths(1).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            //fortax
            tcstax = Convert.ToDouble(ViewState["Tval"].ToString());
            tcstaxAmt = ((tcstax * finalsupply) / 100);

            //fortax
            sb.Append("<div class='content' style='border: 1px solid black'>");


            sb.Append("<table class='table1' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
            sb.Append("<td class='text-left' style='font-size:17px'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  class='text-left' style='padding-left:10px;'><b>" + ViewState["Office_Address"].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='padding-left:90px;'><b>Bill Book</b></td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>No" + txtDeliveryDate.Text + "</td>");
            //sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table class='table table1-bordered'>");

            sb.Append("<tr>");
            sb.Append("<td><b>No." + Month.ToString() + "</b></td>");
            sb.Append("<td class='text-center'><b>GSTIN-" + ViewState["Office_GST"].ToString() + "</b></td>");
            sb.Append("<td><b>" + printDate.ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-center' colspan='5'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-center' colspan='5'><b>M/s  :-" + ddlDitributor.SelectedItem.Text + "</b></td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
            //sb.Append("</tr>");
            sb.Append("<tr>");
            //sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
            sb.Append("<td class='text-center' colspan='5'><b>" + ddlRoute.SelectedItem.Text + "</b></td>");
            sb.Append("</tr>");

            //sb.Append("<tr>");
            //sb.Append("<td class='text-center' colspan='5'></td>");
            //sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("<table class='table table1-bordered'>");
            int Count = dsDist.Rows.Count;
            int ColCount = dsDist.Columns.Count;
            sb.Append("<thead >");
            sb.Append("<td><b>Month & Date</b></td>");
            sb.Append("<td><b>Particulars</b></td>");
            //sb.Append("<td>Qty(In Pkt)</td>");
            //sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
            //sb.Append("<td>Return Qty (In Pkt.)</td>");
            //sb.Append("<td>Inst Qty (In Pkt.)</td>");
            //sb.Append("<td>Adv. Card Qty (In Ltr.)</td>");
            //sb.Append("<td>Adv. Card Margin.</td>");
            //sb.Append("<td>Adv. Card Amount</td>");
            //sb.Append("<td>Billing Qty(In Pkt.)</td>");
            sb.Append("<td><b>Billing Qty(In Ltr.)</b></td>");
            sb.Append("<td><b>Rate (Per Ltr.)</b></td>");
            sb.Append("<td><b>Amount</b></td>");
            //sb.Append("<td>Payble Amount</td>");
            sb.Append("</thead>");

            for (int i = 0; i < Count; i++)
            {
                int rowcount = Count + 1;
                sb.Append("<tr>");
                if (i == 0)
                {
                    sb.Append("<td v-align='middle' rowspan='" + rowcount + "' ><b>" + MonthYear.ToString() + "</b></td>");
                }
                sb.Append("<td>" + dsDist.Rows[i]["IName"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalReturnQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalInstSupplyQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["AdvCardComm"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["AdvCardAmt"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["BillingQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["BillingQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["RatePerLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["Amount"] + "</td>");
                //sb.Append("<td>" + (Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"])) + "</td>");
                sb.Append("</tr>");

                // totalAdvCrdAmt += Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]);
                totalamt += Convert.ToDecimal(dsDist.Rows[i]["Amount"]);
                totalqtyinltr += Convert.ToDecimal(dsDist.Rows[i]["SupplyTotalQtyInLtr"]);
                // paybleAmt += ((Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"])));


            }
            sb.Append("<tr>");

            PaybleAmtWithTcsTax = tcstaxAmt + double.Parse(totalamt.ToString());
            // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
            lblTcsTax.Text = ViewState["Tval"].ToString();
            lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.00");
            lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.00");
            totrebateamount = Math.Round(totalqtyinltr * Convert.ToDecimal(lblrebateper.Text),2);
            lblrebateamount.Text = Convert.ToString(totrebateamount);
            grandtotal = Math.Round( Convert.ToDecimal(PaybleAmtWithTcsTax) - totrebateamount,2);
            lblgrandtotal.Text = Convert.ToString(grandtotal);
            int ColumnCount = dsDist.Columns.Count;

            //for (int i =0; i < ColumnCount-9; i++) // privious
            //for (int i = 0; i < ColumnCount - 15; i++)
            //{
            //if (i == 0)
            //{
            //    sb.Append("<td><b>Total</b></td>");
            //}
            ////else if (i == ColumnCount - 21)
            ////{
            ////    sb.Append("<td><b>" + totalAdvCrdAmt.ToString("0.000") + "</b></td>");
            ////}
            ////else if (i == ColumnCount-10)
            //else if (i == ColumnCount - 17)
            //{
            //    sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
            //}
            //// else if (i == ColumnCount-10)
            //else if (i == ColumnCount - 16)
            //{
            //    sb.Append("<td><b>" + paybleAmt.ToString("0.000") + "</b></td>");
            //}
            //else
            //{
            //    sb.Append("<td></td>");
            //}



            //}
            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td><b>Total</b></td>");
            sb.Append("<td><b>" + totalqtyinltr.ToString() + "</b></td>");
            sb.Append("<td></td>");

            sb.Append("<td><b>" + totalamt.ToString("0.00") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>TCS @</b>" + (lblTcsTax.Text != "0.000" ? lblTcsTax.Text : "NA") + "</td>");


            sb.Append("<td><b>" + (lblTcsTax.Text != "0.00" ? lblTcsTaxAmt.Text : "NA") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>TOTAL VALUE</b></td>");


            sb.Append("<td><b>" + lblFinalPaybleAmount.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>Less Insentive (" + lblrebateper.Text + ") </b></td>");


            sb.Append("<td><b>" + lblrebateamount.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>ACTUAL VALUE</b></td>");


            sb.Append("<td><b>" + lblgrandtotal.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table class='table1' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td style='text-left' ></td>");
            sb.Append("<td  class='text-right' >For :-" + ViewState["Office_Name"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:50px'>");
            sb.Append("<td></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='text-left' >Prepared & Checked by</td>");
            sb.Append("<td class='text-right' style='font-size:20px' ><b>ASST.GENERAL MANAGER <b></td>");
            sb.Append("</tr>");
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
            sb.Append("</table>");
            sb.Append("</div>");

            div_page_content.InnerHtml = sb.ToString();
           // div_page_contentpage.InnerHtml = sb.ToString();
            paneldata1.Visible = true;
            if (dsDist != null) { dsDist.Dispose(); }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);

            ////////////////End Of Route Wise Print Code   ///////////////////////
        }

    }
    private void PrintDistData1()
    {
        StringBuilder sb = new StringBuilder();
        decimal totalamt = 0, paybleAmt = 0, totalAdvCrdAmt = 0, totalqtyinltr = 0,totrebateamount=0,grandtotal=0;
        if (!string.IsNullOrEmpty(ViewState["PrintDistOrInt"].ToString()))
        {
            DataTable dsDist = (DataTable)ViewState["PrintDistOrInt"];
            div_page_content.InnerHtml = "";
            div_page_contentpage.InnerHtml = "";
            GetTcsTax();
            GetRebate();
            DateTime Ddate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
           // string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string Month = Ddate.ToString("MMMdd", CultureInfo.InvariantCulture);
            string MonthYear = Ddate.ToString("MMM-yy", CultureInfo.InvariantCulture);
            string printDate = Ddate.AddMonths(1).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            //fortax
            tcstax = Convert.ToDouble(ViewState["Tval"].ToString());
            tcstaxAmt = ((tcstax * finalsupply) / 100);
            
            //fortax
            sb.Append("<div class='content' style='border: 1px solid black'>");


            sb.Append("<table class='table1' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
            sb.Append("<td class='text-left' style='font-size:17px'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  class='text-left' style='padding-left:10px;'><b>" + ViewState["Office_Address"].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='padding-left:90px;'><b>Bill Book</b></td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
            //sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>No" + txtDeliveryDate.Text + "</td>");
            //sb.Append("</tr>");
             sb.Append("</table>");
            sb.Append("<table class='table table1-bordered'>");
            
            sb.Append("<tr>");
            sb.Append("<td><b>No." + Month.ToString() + "</b></td>");
            sb.Append("<td class='text-center'><b>GSTIN-" + ViewState["Office_GST"].ToString() + "</b></td>");
            sb.Append("<td><b>" + printDate.ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-center' colspan='5'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-center' colspan='5'><b>M/s  :-" + ddlDitributor.SelectedItem.Text + "</b></td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
            //sb.Append("</tr>");
            sb.Append("<tr>");
            //sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
            sb.Append("<td class='text-center' colspan='5'><b>" + ddlRoute.SelectedItem.Text + "</b></td>");
            sb.Append("</tr>");

            //sb.Append("<tr>");
            //sb.Append("<td class='text-center' colspan='5'></td>");
            //sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("<table class='table table1-bordered'>");
            int Count = dsDist.Rows.Count;
            int ColCount = dsDist.Columns.Count;
            sb.Append("<thead >");
            sb.Append("<td><b>Month & Date</b></td>");
            sb.Append("<td><b>Particulars</b></td>");
            //sb.Append("<td>Qty(In Pkt)</td>");
            //sb.Append("<td>Adv. Card Qty(In Pkt.)</td>");
            //sb.Append("<td>Return Qty (In Pkt.)</td>");
            //sb.Append("<td>Inst Qty (In Pkt.)</td>");
            //sb.Append("<td>Adv. Card Qty (In Ltr.)</td>");
            //sb.Append("<td>Adv. Card Margin.</td>");
            //sb.Append("<td>Adv. Card Amount</td>");
            //sb.Append("<td>Billing Qty(In Pkt.)</td>");
            sb.Append("<td><b>Billing Qty(In Ltr.)</b></td>");
            sb.Append("<td><b>Rate (Per Ltr.)</b></td>");
            sb.Append("<td><b>Amount</b></td>");
            //sb.Append("<td>Payble Amount</td>");
            sb.Append("</thead>");

            for (int i = 0; i < Count; i++)
            {
                int rowcount = Count + 1;
                sb.Append("<tr>");
                if (i == 0)
                {
                    sb.Append("<td v-align='middle' rowspan='" + rowcount + "' ><b>" + MonthYear.ToString() + "</b></td>");
                }
                sb.Append("<td>" + dsDist.Rows[i]["IName"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalReturnQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalInstSupplyQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["TotalAdvCardQtyInLtr"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["AdvCardComm"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["AdvCardAmt"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["BillingQty"] + "</td>");
                //sb.Append("<td>" + dsDist.Rows[i]["BillingQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["SupplyTotalQtyInLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["RatePerLtr"] + "</td>");
                sb.Append("<td>" + dsDist.Rows[i]["Amount"] + "</td>");
                //sb.Append("<td>" + (Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"])) + "</td>");
                sb.Append("</tr>");

                // totalAdvCrdAmt += Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"]);
                totalamt += Convert.ToDecimal(dsDist.Rows[i]["Amount"]);
                totalqtyinltr += Convert.ToDecimal(dsDist.Rows[i]["SupplyTotalQtyInLtr"]);
                // paybleAmt += ((Convert.ToDecimal(dsDist.Rows[i]["Amount"]) - Convert.ToDecimal(dsDist.Rows[i]["AdvCardAmt"])));


            }
            sb.Append("<tr>");

            PaybleAmtWithTcsTax = tcstaxAmt + double.Parse(totalamt.ToString());
            // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
            lblTcsTax.Text = ViewState["Tval"].ToString();
            lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.00");
            lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.00");
            totrebateamount = Math.Round(totalqtyinltr*Convert.ToDecimal(lblrebateper.Text),2);
            lblrebateamount.Text = Convert.ToString(totrebateamount);
            grandtotal = Math.Round(Convert.ToDecimal(PaybleAmtWithTcsTax) - totrebateamount,2);
            lblgrandtotal.Text = Convert.ToString(grandtotal);
            int ColumnCount = dsDist.Columns.Count;

            //for (int i =0; i < ColumnCount-9; i++) // privious
            //for (int i = 0; i < ColumnCount - 15; i++)
            //{
            //if (i == 0)
            //{
            //    sb.Append("<td><b>Total</b></td>");
            //}
            ////else if (i == ColumnCount - 21)
            ////{
            ////    sb.Append("<td><b>" + totalAdvCrdAmt.ToString("0.000") + "</b></td>");
            ////}
            ////else if (i == ColumnCount-10)
            //else if (i == ColumnCount - 17)
            //{
            //    sb.Append("<td><b>" + totalamt.ToString("0.000") + "</b></td>");
            //}
            //// else if (i == ColumnCount-10)
            //else if (i == ColumnCount - 16)
            //{
            //    sb.Append("<td><b>" + paybleAmt.ToString("0.000") + "</b></td>");
            //}
            //else
            //{
            //    sb.Append("<td></td>");
            //}



            //}
            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td><b>Total</b></td>");
            sb.Append("<td><b>" + totalqtyinltr.ToString("0.00") + "</b></td>");
            sb.Append("<td></td>");

            sb.Append("<td><b>" + totalamt.ToString("0.00") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>TCS @</b>" + (lblTcsTax.Text != "0.000" ? lblTcsTax.Text : "NA") + "</td>");


            sb.Append("<td><b>" + (lblTcsTax.Text != "0.000" ? lblTcsTaxAmt.Text : "NA") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>TOTAL VALUE</b></td>");


            sb.Append("<td><b>" + lblFinalPaybleAmount.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>Less Insentive (" + lblrebateper.Text + ") </b></td>");


            sb.Append("<td><b>" + lblrebateamount.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='4' class='text-right'><b>ACTUAL VALUE</b></td>");


            sb.Append("<td><b>" + lblgrandtotal.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table class='table1' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td style='text-left' ></td>");
            sb.Append("<td  class='text-right' >For :-" + ViewState["Office_Name"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='height:50px'>");
            sb.Append("<td></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='text-left' >Prepared & Checked by</td>");
            sb.Append("<td class='text-right' style='font-size:20px' ><b>ASST.GENERAL MANAGER <b></td>");
            sb.Append("</tr>");
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
            sb.Append("</table>");
            sb.Append("</div>");

            div_page_content.InnerHtml = sb.ToString();
            div_page_contentpage.InnerHtml = sb.ToString();
            paneldata1.Visible = true;
            Button1.Visible = true;
            //if (dsDist != null) { dsDist.Dispose(); }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);

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
            sb.Append("<td rowspan='3' class='text-right' style='padding-left:70px;'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
            sb.Append("<td class='text-left'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='padding-left:50px;'><b>Bill Book</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td><b>G.S.T/U.I.N NO:-" + ViewState["Office_GST"].ToString() + "<b></td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>No" + txtDeliveryDate.Text + "</td>");
            //sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='text-left' colspan='2'>M/s  :-" + dsInst.Rows[0]["BName"].ToString() + "</td>");
            sb.Append("</tr>");
            //sb.Append("<tr>");
            //sb.Append("<td class='text-left' colspan='2'>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
            //sb.Append("</tr>");
            sb.Append("<tr>");
            //sb.Append("<td class='text-left'>" + txtDeliveryDate.Text + "</td>");
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
            sb.Append("<td>Payble Amount</td>");
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
                    sb.Append("<td><b>" + totalamt.ToString("0.00") + "</b></td>");
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
                DateTime Ddate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
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
    private void GetRebate()
    {
        try
        {
            lblrebateper.Text = "0";
            if (ddlDitributor.SelectedValue != "0")
            {

                DateTime Ddate = DateTime.ParseExact(txtfromDate.Text, "dd/MM/yyyy", culture);
                string EffectiveDate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string Month = Ddate.ToString("MMMdd", CultureInfo.InvariantCulture);
                string MonthYear = Ddate.ToString("MMM-yy", CultureInfo.InvariantCulture);
                string Date = Ddate.AddMonths(1).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                DataSet dsrebate = objdb.ByProcedure("USP_Trn_MilkOrProductRuraldistributer_MonthlyInvoice",
                      new string[] { "Flag", "Office_ID", "ItemCat_id", "EffectiveDate" },
                        new string[] { "0", objdb.Office_ID(), ddlItemCategory.SelectedValue, EffectiveDate.ToString() }, "dataset");

                if (dsrebate.Tables[0].Rows.Count > 0)
                {
                    lblrebateper.Text = dsrebate.Tables[0].Rows[0]["RebatePerLiter_Amt"].ToString();
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

    public object sender { get; set; }

    public EventArgs e { get; set; }
}