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

public partial class mis_DemandSupply_InvoiceGenerationSSOrDistwise_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6, ds7, ds8, dsInvo, dsInst, ds3 = new DataSet();
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    double totalsupply = 0.00, finalsupply = 0.00, totalAdvAmt = 0.00, tcstax = 0.000, tcstaxAmt = 0.000, PaybleAmtWithTcsTax = 0.000,AdvTotalAmt=0.00;

    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetOfficeDetails();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = Date;
                txtDeliveryDate.Attributes.Add("readonly", "readonly");
                GetLocation();
                GetPartyListByInvoicefor();
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
    private void GetPartyListByInvoicefor()
    {
        if (ddlInvoiceFor.SelectedValue == "1")
        {
            pnlSSorDist.Visible = true;
            GetSS();
        }
        else
        {
            pnlInstitution.Visible = true;
            GetInstitution();
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

            //ddlInstitution.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
            //     new string[] { "flag", "Office_ID", "AreaId" },
            //     new string[] { "20", objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");
            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            dsInst = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id"
                    , "Delivary_Date", "DelivaryShift_id" },
                  new string[] { "10", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() ,
                      deliverydate.ToString(), ddlShift.SelectedValue }, "dataset");

            ddlInstitution.Items.Clear();
            if (dsInst.Tables[0].Rows.Count > 0)
            {
                ddlInstitution.DataTextField = "BName";
                ddlInstitution.DataValueField = "InstNRId";
                ddlInstitution.DataSource = dsInst.Tables[0];
                ddlInstitution.DataBind();
                ddlInstitution.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlInstitution.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
        finally
        {
            if (dsInst != null) { dsInst.Dispose(); }
        }
    }
    private void GetSS()
    {
        try
        {
            //ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
            //    new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
            //      new string[] { "7", objdb.Office_ID(),ddlLocation.SelectedValue,objdb.GetMilkCatId()}, "dataset");

            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            if(objdb.Office_ID()=="6")
            {
                ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                               new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id"
                    , "Delivary_Date", "DelivaryShift_id", },
                                 new string[] { "11", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() ,
                      deliverydate.ToString(), ddlShift.SelectedValue }, "dataset");
            }
            else
            {
                ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
               new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id"
                    , "Delivary_Date", "DelivaryShift_id", },
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() ,
                      deliverydate.ToString(), ddlShift.SelectedValue }, "dataset");
            }
           
            ddlSuperStockist.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlSuperStockist.DataTextField = "SSName";
                ddlSuperStockist.DataValueField = "SSRDId";
                ddlSuperStockist.DataSource = ds7.Tables[0];
                ddlSuperStockist.DataBind();
                ddlSuperStockist.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSuperStockist.Items.Insert(0, new ListItem("No Record Found.", "0"));
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
    private void GenerateInvoice()
    {
        try
        {
            DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (ddlInvoiceFor.SelectedValue == "1")
            {
                string[] SSRId = ddlSuperStockist.SelectedValue.Split('-');
                ViewState["SSId"] = SSRId[0];
                ViewState["SSRId"] = SSRId[1];
                ViewState["SSDId"] = SSRId[2];
                
                if(objdb.Office_ID()=="6")
                {
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "SuperStockistId", "RouteId", "OrganizationId", "AreaId" },
                       new string[] { "12", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), SSRId[0].ToString(), SSRId[1].ToString(), "0", ddlLocation.SelectedValue }, "dataset");
                }
                else
                {
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "SuperStockistId", "RouteId", "OrganizationId", "AreaId" },
                       new string[] { "11", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), SSRId[0].ToString(), SSRId[1].ToString(), "0", ddlLocation.SelectedValue }, "dataset");
                }
                
            }
            else
            {
                string[] InstBRAId = ddlInstitution.SelectedValue.Split('-');
                ViewState["InstId"] = InstBRAId[0];
                ViewState["InstRId"] = InstBRAId[1];
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "OrganizationId", "AreaId" },
                       new string[] { "2", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), InstBRAId[1], InstBRAId[0], ddlLocation.SelectedValue }, "dataset");
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblFinalPaybleAmount.Text = string.Empty;
                lblTcsTax.Text = string.Empty;
                lblTcsTaxAmt.Text = string.Empty;
                pnltcxdata.Visible = true;
                ViewState["MultiDemandId"] = ds1.Tables[0].Rows[0]["MultiDemandId"];
                if (ddlInvoiceFor.SelectedValue == "1")
                {
                    GetTcsTax();

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
                    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    dtdist.Columns.Add("SSTransMargin", typeof(decimal));
                    dtdist.Columns.Add("SSMargin", typeof(decimal));
                    dtdist.Columns.Add("ConsumerRate", typeof(decimal));
                    dtdist.Columns.Add("AdvCardRatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("AdvCardTotalAmount", typeof(decimal));
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
                        drdist[9] = ds1.Tables[0].Rows[j]["FinalSSRate"].ToString();
                        drdist[10] = ds1.Tables[0].Rows[j]["BillingQty"].ToString();
                        drdist[11] = ds1.Tables[0].Rows[j]["BillingQtyInLtr"].ToString();
                        drdist[12] = ds1.Tables[0].Rows[j]["RatePerLtr"].ToString();
                        drdist[13] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        if(objdb.Office_ID()=="6")
                        {
                            drdist[14] = ds1.Tables[0].Rows[j]["Amount"].ToString();
                        }
                        else
                        {
                            drdist[14] = Convert.ToDecimal(ds1.Tables[0].Rows[j]["Amount"].ToString()) - Convert.ToDecimal(ds1.Tables[0].Rows[j]["AdvCardAmt"].ToString());
                        }
                        drdist[15] = ds1.Tables[0].Rows[j]["TotalInstSupplyQty"].ToString();
                        drdist[16] = ds1.Tables[0].Rows[j]["TotalInstSupplyQtyInLtr"].ToString();
                        drdist[17] = ds1.Tables[0].Rows[j]["InstTransComm"].ToString();
                        drdist[18] = ds1.Tables[0].Rows[j]["InstTransCommAmt"].ToString();
                        drdist[19] = ds1.Tables[0].Rows[j]["FinalSSTransMargin"].ToString();
                        drdist[20] = ds1.Tables[0].Rows[j]["FinalSSMargin"].ToString();
                        drdist[21] = ds1.Tables[0].Rows[j]["ConsumerRate"].ToString();
                        // drdist[22] = ds1.Tables[0].Rows[j]["AdvCardRatePerLtr"].ToString();
                        // drdist[23] = ds1.Tables[0].Rows[j]["AdvCardTotalAmount"].ToString();
						if (objdb.Office_ID() == "6")
                        {
                            drdist[22] = ds1.Tables[0].Rows[j]["RatePerLtr_UDS"].ToString();
                        }
                        else
                        {
                            drdist[22] = ds1.Tables[0].Rows[j]["AdvCardRatePerLtr"].ToString();
                        }
                        if (objdb.Office_ID() == "6")
                        {
                            drdist[23] = ds1.Tables[0].Rows[j]["AdVCardTotalAmount_UDS"].ToString();
                        }
                        else
                        {
                            drdist[23] = ds1.Tables[0].Rows[j]["AdvCardTotalAmount"].ToString();
                        }


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
                    dtdist.Columns.Add("TotalInstSupplyQty", typeof(int));
                    dtdist.Columns.Add("TotalInstSupplyQtyInLtr", typeof(decimal));
                    dtdist.Columns.Add("InstTransComm", typeof(decimal));
                    dtdist.Columns.Add("InstTransCommAmt", typeof(decimal));
                    dtdist.Columns.Add("SSTransMargin", typeof(decimal));
                    dtdist.Columns.Add("SSMargin", typeof(decimal));
                    dtdist.Columns.Add("ConsumerRate", typeof(decimal));
                    dtdist.Columns.Add("AdvCardRatePerLtr", typeof(decimal));
                    dtdist.Columns.Add("AdvCardTotalAmount", typeof(decimal));
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
                        drdist[21] = "0";
                        drdist[22] = "0";
                        drdist[23] = "0";

                        dtdist.Rows.Add(drdist.ItemArray);

                    }
                    ViewState["ItemDetails"] = dtdist;


                    if (dtdist != null) { dtdist.Dispose(); }

                    GridView1.Visible = false;
                    GridView2.Visible = true;

                    GridView1.DataSource = null;
                    GridView1.DataBind();


                 
                }
               
                lblMsg.Text = string.Empty;
                pnlData.Visible = true;
                btnPrint.Visible = true;
                pnlremark.Visible = true;


            }
            else
            {
                lblFinalPaybleAmount.Text = string.Empty;
                lblTcsTax.Text = string.Empty;
                lblTcsTaxAmt.Text = string.Empty;
                pnltcxdata.Visible = false;
                pnlData.Visible = false;
                pnlremark.Visible = false;
                txtRemark.Visible = false;
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
        if(ddlInvoiceFor.SelectedValue=="1")
        {
            GetSS();
        }
        else
        {
            GetInstitution();
        }
    }
    protected void ddlInvoiceFor_SelectedIndexChanged(object sender, EventArgs e)
    {
            lblMsg.Text = string.Empty;
            if (ddlInvoiceFor.SelectedValue == "1")
            {
                pnlSSorDist.Visible = true;
                pnlInstitution.Visible = false;
                ddlInstitution.SelectedIndex = 0;
                GetSS();

            }
            else
            {
                pnlInstitution.Visible = true;
                pnlSSorDist.Visible = false;
                ddlSuperStockist.SelectedIndex = 0;
                GetInstitution();
            }
       
    }
    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateInvoice();
            //get_distributor_mobileno();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlInvoiceFor.SelectedIndex = 0;
        pnlInstitution.Visible = false;
        ddlSuperStockist.SelectedIndex = 0;
        ddlInstitution.SelectedIndex = 0;
        pnlData.Visible = false;
        ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        lblFinalPaybleAmount.Text = string.Empty;
        lblTcsTax.Text = string.Empty;
        lblTcsTaxAmt.Text = string.Empty;
        pnltcxdata.Visible = false;

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
                Label lblAdvCardTotalAmount = (e.Row.FindControl("lblAdvCardTotalAmount") as Label);
                totalsupply += Convert.ToDouble(lblAmount.Text);

                finalsupply += Convert.ToDouble(lblFAmount.Text);

                totalAdvAmt += Convert.ToDouble(lblAdvCardAmt.Text);
                AdvTotalAmt += Convert.ToDouble(lblAdvCardTotalAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalAdvCardAmt = (e.Row.FindControl("lblTotalAdvCardAmt") as Label);
                Label lblTAmount = (e.Row.FindControl("lblTAmount") as Label);
                Label lblFinalAmount = (e.Row.FindControl("lblFinalAmount") as Label);
                Label lblFAdvCardTotalAmount = (e.Row.FindControl("lblFAdvCardTotalAmount") as Label);
                lblTAmount.Text = totalsupply.ToString("0.000");
                lblFinalAmount.Text = finalsupply.ToString("0.000");
                lblTotalAdvCardAmt.Text = totalAdvAmt.ToString("0.000");
                tcstax = Convert.ToDouble(ViewState["Tval"].ToString());
                tcstaxAmt = ((tcstax * finalsupply) / 100);
                PaybleAmtWithTcsTax = tcstaxAmt + finalsupply;
                lblTcsTax.Text = ViewState["Tval"].ToString();
                lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.000");
                lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
                lblFAdvCardTotalAmount.Text = AdvTotalAmt.ToString("0.000");
              
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

                PaybleAmtWithTcsTax = totalsupply;
                lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
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
                   
                    if (ddlInvoiceFor.SelectedValue == "1" && ViewState["SSId"].ToString() != "" && ViewState["SSRId"].ToString() != "" && ViewState["SSDId"].ToString()!="" && ViewState["MultiDemandId"].ToString()!="")
                    {
                       // if (PaybleAmount > 0)
                      //  {
                            Decimal FPaybleAmount = Convert.ToDecimal(lblFinalPaybleAmount.Text);
                        DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                        ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert_IDS",
                             new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                                     "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId"
                                     ,"TcsTaxPer","SuperStockistId","MultiDemandId","CreatedRemark" },
                               new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, 
                                       objdb.GetMilkCatId(), ViewState["SSRId"].ToString(), ViewState["SSDId"].ToString()
                                       , "0", FPaybleAmount.ToString(), objdb.Office_ID(), objdb.createdBy(),
                                       IPAddress,ddlLocation.SelectedValue,ViewState["Tval"].ToString(),ViewState["SSId"].ToString()
                              ,ViewState["MultiDemandId"].ToString(),txtRemark.Text.Trim()}
                                   , "type_Trn_MilkOrProductInvoiceDetailChild_IDS", dtInsert, "dataset");

                        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            //if (objdb.Office_ID() == "2")
                            //{
                                get_distributor_mobileno();
                                if (lbldistributerMONO.Text != "0000000000" && lbldistributerMONO.Text != "9999999999" && lbldistributerMONO.Text != null && lbldistributerMONO.Text != "")
                                {
                                    string Supmessage = "";
                                    string link = "";
                                    string Invoice_No = ds6.Tables[0].Rows[0]["Invoice_No"].ToString();
                                    ServicePointManager.Expect100Continue = true;
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    //lbldistributerMONO.Text = "8962389494";

                                    Supmessage = "Your bill for order " + Invoice_No.ToString() + " date " + deliverydate.ToString() + " shift " + ddlShift.SelectedItem.Text + " has been generated for amount " + FPaybleAmount.ToString() + " rs. Kindly make payment on time.";
                                    link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + lbldistributerMONO.Text + "&text=" + Server.UrlEncode(Supmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162650801393194&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";


                                    HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                    Stream stream = response.GetResponseStream();
                                }
                            //}
                            string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            PrintInvoiceCumBillDetails(ds6.Tables[0].Rows[0]["MilkOrProductInvoiceId"].ToString());
                            GetSS();
                            pnlData.Visible = false;
                            btnPrint.Visible = false;
                            pnlremark.Visible = false;
                            txtRemark.Text = string.Empty;
                            pnltcxdata.Visible = false;
                        }
                        else
                        {
                            string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice already Saved. ");
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Save Data :" + error);
                            }
                        }
                        //}
                        //else
                        //{
                        //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Invoice Not Saved and Print");
                        //    return;
                        //}

                    }
                    else
                    {
                        DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                        ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail_Insert_IDS",
                             new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "DistributorId",
                                     "OrganizationId", "TotalPaybleAmount", "Office_ID", "CreatedBy", "CreatedByIP","AreaId"
                             ,"TcsTaxPer","SuperStockistId","MultiDemandId","CreatedRemark"},
                               new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, 
                                       objdb.GetMilkCatId(), ViewState["InstRId"].ToString(), "0"
                                       , ViewState["InstId"].ToString() , PaybleAmount.ToString(), objdb.Office_ID()
                                       , objdb.createdBy(), IPAddress,ddlLocation.SelectedValue,"0","0",
                               ViewState["MultiDemandId"].ToString(),txtRemark.Text.Trim()}
                                   , "type_Trn_MilkOrProductInvoiceDetailChild_IDS", dtInsert, "dataset");

                        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            PrintInvoiceCumBillDetails_Inst(ds6.Tables[0].Rows[0]["MilkOrProductInvoiceId"].ToString());
                            pnlData.Visible = false;
                            btnPrint.Visible = false;
                            pnlremark.Visible = false;
                            txtRemark.Text = string.Empty;
                            pnltcxdata.Visible = false;
                            GetInstitution();
                        }
                        else
                        {
                            string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Invoice already Exist. ");
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
        string[] BRR_Id1 = ddlSuperStockist.SelectedValue.Split('-');
        ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                    new string[] { "Flag", "Office_ID", "superstockist_ID" },
                      new string[] { "10", objdb.Office_ID(), BRR_Id1[0] }, "dataset");
        if (ds3.Tables.Count > 0)
        {
            if (ds3.Tables[0].Rows.Count > 0)
            {
                lbldistributerMONO.Text = ds3.Tables[0].Rows[0]["SSCPersonMobileNo"].ToString();
            }
        }

    }

    private void GetTcsTax()
    {
        try
        {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string[] tmpSSRId = ddlSuperStockist.SelectedValue.Split('-');
             ds8 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "SuperStockistId" },
                       new string[] { "1", objdb.Office_ID(), deliverydate, tmpSSRId [0]}, "dataset");

                if (ds8.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds8.Tables[0].Rows[0]["Tval"].ToString();
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

    //private void PrintInvoiceCumBillDetails(string dmid)
    //{
    //    try
    //    {
    //        dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
    //                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
    //                 new string[] { "6", objdb.Office_ID(), dmid }, "dataset");

    //        if (dsInvo.Tables[0].Rows.Count > 0)
    //        {

    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("<div>");
    //            sb.Append("<h2 style='text-align:center'>Invoice-cum-Bill of Supply</h2>");
    //            sb.Append("<table class='table1' style='width:100%'>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='6'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Delivery Note</td>");
    //            sb.Append("<td colspan='6'>Mode/Terms of Payment</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Supplier's Ref</td>");
    //            sb.Append("<td colspan='6'>Other Reference(s)</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Buyer's Order No.</td>");
    //            sb.Append("<td colspan='6'>Dated</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Dispatch Document No.</td>");
    //            sb.Append("<td colspan='6'>Delivery Note Date</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Dispatched through</br></td>");
    //            sb.Append("<td colspan='6'>Destination</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='11'>Terms of Delivery</td>");

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>S.No</td>");
    //            sb.Append("<td style='text-align:center'>Description of Goods</td>");
    //            sb.Append("<td style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Qty(In Pkt.).</td>");
    //            sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Inst Qty (In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Qty(In Ltr).</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Margin(In Ltr).</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Amount(D)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
    //            sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Amount(A)</td>");
    //            sb.Append("<td style='text-align:center'>Payble Amount(A-D)</td>");
               
    //            sb.Append("</tr>");

    //            int TCount = dsInvo.Tables[0].Rows.Count;
    //            for (int i = 0; i < TCount; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>" + (i + 1).ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" +"</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["AdvCardComm"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["AdvCardAmt"].ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["BillingAmount"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'><b>" + dsInvo.Tables[0].Rows[i]["PaybleAmount"].ToString() + "</b></td>");
    //                sb.Append("</tr>");
    //            }
    //            decimal TAdvCardAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
    //            decimal TAamt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingAmount"));
    //            decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

    //            decimal tcstaxamt = ((TPaybleAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
    //            decimal FTPaybleAmt = TPaybleAmt + tcstaxamt;
    //            string Amount = GenerateWordsinRs(FTPaybleAmt.ToString());
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2'></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TAdvCardAmt.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TAamt.ToString() + "</b></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TPaybleAmt.ToString() + "</b></td>");
    //            sb.Append("</tr>");              
    //            sb.Append("<tr>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
    //            sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
    //            sb.Append("<td colspan='12' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt.ToString() : "NA") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
    //            sb.Append("<td colspan='12' style='text-align:right'><b>" + FTPaybleAmt.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='15'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2' style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td colspan='7' style='text-align:center'>Taxable Value</td>");
    //            sb.Append("<td colspan='7' style='text-align:center'>Total Tax Amount</td>");
    //            sb.Append("</tr>");
    //            int TCount1 = dsInvo.Tables[1].Rows.Count;
    //            for (int i = 0; i < TCount1; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td colspan='2' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
    //                sb.Append("<td colspan='7' style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TaxableValue"].ToString() + "</td>");
    //                sb.Append("<td colspan='7' style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TotalTaxAmount"].ToString() + "</td>");

    //                sb.Append("</tr>");
    //            }
    //            decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
    //            decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
    //            string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString());
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
    //            sb.Append("<td colspan='7' style='text-align:right'><b>" + TTaxableValue.ToString() + "</b></td>");

    //            sb.Append("<td colspan='7' style='text-align:right'><b>" + TotalTaxAmount.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td  rowspan ='2' colspan='9' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br></br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

    //            //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            sb.Append("</tr>");

    //            sb.Append("</table>");
    //            Print.InnerHtml = sb.ToString();
    //            ClientScriptManager CSM = Page.ClientScript;
    //            string strScript = "<script>";
    //            strScript += "window.print();";

    //            strScript += "</script>";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (dsInvo != null) { dsInvo.Dispose(); }
    //    }

    //}

    //private void PrintInvoiceCumBillDetails(string dmid)
    //{
    //    try
    //    {
    //        if (objdb.Office_ID() == "6")
    //        {
    //            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
    //                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
    //                 new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
    //        }
    //        else
    //        {
    //            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
    //                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
    //                 new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
    //        }

    //        if (dsInvo.Tables[0].Rows.Count > 0)
    //        {

    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("<div>");
    //            sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
    //            sb.Append("<table class='table1' style='width:100%'>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='6'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='5'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
    //                sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='5'>Delivery Note</td>");
    //                sb.Append("<td colspan='6'>Mode/Terms of Payment</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                //sb.Append("<td colspan='5'></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='5'>Supplier's Ref</td>");
    //            }
    //            if (objdb.Office_ID() == "6")
    //            {
    //                //sb.Append("<td colspan='6'</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='6'>Other Reference(s)</td>");
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='5'>DM No. <b>" + dsInvo.Tables[0].Rows[0]["OrderId"].ToString() + "</b></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='5'>Buyer's Order No.</td>");
    //            }
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='6'>Vehicle Out Time :<b>" + dsInvo.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b> </td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='6'>Dated</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Dispatch Document No.</td>");
    //            sb.Append("<td colspan='6'>Delivery Note Date</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='5'>Vehicle No : <b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></br></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='5'>Dispatched through</br></td>");
    //            }
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='6'></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='6'>Destination</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='11'>Terms of Delivery</td>");

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>SN</td>");
    //            sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
    //            sb.Append("<td style='text-align:center'>HSN / SAC</td>");
    //            sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Qty(In Pkt.).</td>");
    //            sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Inst Qty (In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Qty(In Ltr).</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Margin(In Ltr).</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Margin Amount(D)</td>");
    //            sb.Append("<td style='text-align:center'>Adv. Card Amount</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
    //            sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Amount(A)</td>");
    //            sb.Append("<td style='text-align:center'>Payble Amount(A-D)</td>");

    //            sb.Append("</tr>");

    //            int TCount = dsInvo.Tables[0].Rows.Count;
    //            for (int i = 0; i < TCount; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>" + (i + 1).ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardComm"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardAmt"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardTotalAmount"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
    //                sb.Append("<td style='text-align:center'>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["BillingAmount"]).ToString("0.00") + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
    //                sb.Append("</tr>");
    //            }
    //            decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
    //            decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
    //            decimal TAdvCardQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalAdvCardQty"));
    //            decimal TInstQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalInstSupplyQty"));
    //            decimal TAdvCardQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalAdvCardQtyInLtr"));
    //            decimal TAdvCardAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
    //            //decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardTotalAmount"));
    //            decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("AdvCardTotalAmount") ?? 0);
    //            decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
    //            decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
    //            decimal TAamt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingAmount"));
    //            decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

    //            decimal tcstaxamt = ((TPaybleAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
    //            decimal FTPaybleAmt = TPaybleAmt + tcstaxamt;
    //            string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));

    //            decimal tcstaxamt_AdvCard = ((TAdvCardTotalAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
    //            decimal FAdvCardwithtcstax = TAdvCardTotalAmt + tcstaxamt_AdvCard;
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2'></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TAdvCardQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TInstQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TAdvCardQtyInLtr.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TAdvCardAmt.ToString() + "</b></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TAdvCardTotalAmt.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TAamt.ToString("0.00") + "</b></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
    //            sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
    //            sb.Append("<td colspan='7' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt_AdvCard.ToString("0.00") : "NA") + "</b></td>");
    //            sb.Append("<td colspan='12' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt.ToString("0.00") : "NA") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
    //            sb.Append("<td colspan='8' style='text-align:right'><b>" + FAdvCardwithtcstax.ToString("0.00") + "</b></td>");
    //            sb.Append("<td colspan='13' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='16'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2' style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td colspan='7' style='text-align:center'>Taxable Value</td>");
    //            sb.Append("<td colspan='7' style='text-align:center'>Total Tax Amount</td>");
    //            sb.Append("</tr>");
    //            int TCount1 = dsInvo.Tables[1].Rows.Count;
    //            for (int i = 0; i < TCount1; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td colspan='2' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
    //                sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
    //                sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

    //                sb.Append("</tr>");
    //            }
    //            decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
    //            decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
    //            string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
    //            sb.Append("<td colspan='7' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

    //            sb.Append("<td colspan='7' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td  rowspan ='2' colspan='9' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

    //            //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No : <b> " + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }

    //            sb.Append("</tr>");

    //            sb.Append("</table>");
    //            Print.InnerHtml = sb.ToString();

    //            ClientScriptManager CSM = Page.ClientScript;
    //            string strScript = "<script>";
    //            strScript += "window.print();";

    //            strScript += "</script>";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (dsInvo != null) { dsInvo.Dispose(); }
    //    }

    //}
    //private void PrintInvoiceCumBillDetails_Inst(string dmid)
    //{
    //    try
    //    {
    //        if (objdb.Office_ID() == "6")
    //        {
    //            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
    //                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
    //                 new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
    //        }
    //        else
    //        {
    //            dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
    //                 new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
    //                 new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
    //        }

    //        if (dsInvo.Tables[0].Rows.Count > 0)
    //        {

    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("<div>");
    //            sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
    //            sb.Append("<table class='table1' style='width:100%'>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='4' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='4' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='4'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='3'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
    //                sb.Append("<td colspan='4'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='3'>Delivery Note</td>");
    //                sb.Append("<td colspan='4'>Mode/Terms of Payment</td>");
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                //sb.Append("<td colspan='5'></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='3'>Supplier's Ref</td>");
    //            }
    //            if (objdb.Office_ID() == "6")
    //            {
    //                //sb.Append("<td colspan='6'</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='4'>Other Reference(s)</td>");
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='4' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["BName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["BAddress"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='3'>DM No. <b>" + dsInvo.Tables[0].Rows[0]["OrderId"].ToString() + "</b></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='3'>Buyer's Order No.</td>");
    //            }
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='4'>Vehicle Out Time :<b>" + dsInvo.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b> </td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='4'>Dated</td>");
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3'>Dispatch Document No.</td>");
    //            sb.Append("<td colspan='4'>Delivery Note Date</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='3'>Vehicle No : <b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></br></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='3'>Dispatched through</br></td>");
    //            }
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='4'></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='4'>Destination</td>");
    //            }
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='5'>Terms of Delivery</td>");

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>SN</td>");
    //            sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
    //            sb.Append("<td style='text-align:center'>HSN / SAC</td>");
    //            sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
    //            sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
    //            sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
    //            sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
    //            sb.Append("<td style='text-align:center'>Payble Amount</td>");

    //            sb.Append("</tr>");

    //            int TCount = dsInvo.Tables[0].Rows.Count;
    //            for (int i = 0; i < TCount; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>" + (i + 1).ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
    //                sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
    //                sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
    //                sb.Append("</tr>");
    //            }
    //            decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
    //            decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
    //            decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
    //            decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
    //            decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

    //            decimal FTPaybleAmt = TPaybleAmt;
    //            string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));


    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2'></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
    //            sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
    //            sb.Append("<td colspan='2' style='text-align:right'><b></b></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
    //            sb.Append("</tr>"); ;
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='9'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3' style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td colspan='3' style='text-align:center'>Taxable Value</td>");
    //            sb.Append("<td colspan='3' style='text-align:center'>Total Tax Amount</td>");
    //            sb.Append("</tr>");
    //            int TCount1 = dsInvo.Tables[1].Rows.Count;
    //            for (int i = 0; i < TCount1; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td colspan='3' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
    //                sb.Append("<td colspan='3' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
    //                sb.Append("<td colspan='3' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

    //                sb.Append("</tr>");
    //            }
    //            decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
    //            decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
    //            string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3' style='text-align:right'><b>Total</b></td>");
    //            sb.Append("<td colspan='3' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

    //            sb.Append("<td colspan='3' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td  rowspan ='2' colspan='6' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

    //            //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            if (objdb.Office_ID() == "6")
    //            {
    //                sb.Append("<td colspan='3' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No :  <b>" + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='3' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            sb.Append("</tr>");

    //            sb.Append("</table>");
    //            Print.InnerHtml = sb.ToString();

    //            ClientScriptManager CSM = Page.ClientScript;
    //            string strScript = "<script>";
    //            strScript += "window.print();";

    //            strScript += "</script>";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (dsInvo != null) { dsInvo.Dispose(); }
    //    }

    //}






    private void PrintInvoiceCumBillDetails_Inst(string dmid)
    {
        try
        {
            if (objdb.Office_ID() == "6")
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
            }
            else
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
            }

            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='4' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='4' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='4'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='3'>Gate Pass No <b>" + dsInvo.Tables[0].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
                    //sb.Append("<td colspan='4'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Delivery Note</td>");
                    sb.Append("<td colspan='4'>Mode/Terms of Payment</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Supplier's Ref</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'</td>");
                }
                else
                {
                    sb.Append("<td colspan='4'>Other Reference(s)</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["BName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["BAddress"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='3'></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Buyer's Order No.</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='4'></td>");
                }
                else
                {
                    sb.Append("<td colspan='4'>Dated</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Delivery Note</td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dispatch Document No.</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='4'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");

                }
                else
                {
                    sb.Append("<td colspan='4'>Delivery Note Date</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //   sb.Append("<td colspan='3'></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dispatched through</br></td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    // sb.Append("<td colspan='4'></td>");
                }
                else
                {
                    sb.Append("<td colspan='4'>Destination</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='5'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Terms of Delivery</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>SN</td>");
                sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
                sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Payble Amount</td>");

                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
                decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
                decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
                decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
                decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                decimal FTPaybleAmt = TPaybleAmt;
                string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));


                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b></b></td>");
                sb.Append("<td colspan='4' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>"); ;
                sb.Append("<tr>");
                sb.Append("<td colspan='9'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td colspan='3' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='3' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td colspan='3' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
                    sb.Append("<td colspan='3' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td colspan='3' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

                sb.Append("<td colspan='3' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='6' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

                //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No :  <b>" + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");
                Print.InnerHtml = sb.ToString();

                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (dsInvo != null) { dsInvo.Dispose(); }
        }

    }

    private void PrintInvoiceCumBillDetails(string dmid)
    {
        try
        {
            if (objdb.Office_ID() == "6")
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "11", objdb.Office_ID(), dmid }, "dataset");
            }
            else
            {
                dsInvo = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
                     new string[] { "flag", "Office_ID", "MilkOrProductInvoiceId" },
                     new string[] { "6", objdb.Office_ID(), dmid }, "dataset");
            }


            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='5' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["InvoiceNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='6'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                    //sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Delivery Note</td>");
                    sb.Append("<td colspan='6'>Mode/Terms of Payment</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Supplier's Ref</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'</td>");
                }
                else
                {
                    sb.Append("<td colspan='6'>Other Reference(s)</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Buyer's Order No.</td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'></td>");
                }
                else
                {
                    sb.Append("<td colspan='6'>Dated</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='5'>Delivery Note</td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Dispatch Document No.</td>");
                }

                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='6'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");

                }
                else
                {
                    sb.Append("<td colspan='6'>Delivery Note Date</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='5'></br></td>");
                }
                else
                {
                    sb.Append("<td colspan='5'>Dispatched through</br></td>");
                }
                if (objdb.Office_ID() == "6")
                {
                    //sb.Append("<td colspan='6'></td>");
                }
                else
                {
                    sb.Append("<td colspan='6'>Destination</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='11'>Gate Pass No <b>" + dsInvo.Tables[2].Rows[0]["VDChallanNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='11'>Terms of Delivery</td>");
                }


                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>SN</td>");
                sb.Append("<td style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Qty(In Pkt)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Qty(In Pkt.).</td>");
                sb.Append("<td style='text-align:center'>Return Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Inst Qty (In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Qty(In Ltr).</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Margin(In Ltr).</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Margin Amount(D)</td>");
                sb.Append("<td style='text-align:center'>Adv. Card Amount</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Pkt.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Qty(In Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Rate (Per Ltr.)</td>");
                sb.Append("<td style='text-align:center'>Cash Sale Amount(A)</td>");
                sb.Append("<td style='text-align:center'>Payble Amount(A-D)</td>");

                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["IName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalReturnQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalInstSupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["TotalAdvCardQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardComm"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardAmt"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["AdvCardTotalAmount"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["BillingQty"].ToString() + " Pkt" + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + dsInvo.Tables[0].Rows[i]["BillingQtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + dsInvo.Tables[0].Rows[i]["RatePerLtr"].ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["BillingAmount"]).ToString("0.00") + "</td>");
                    sb.Append("<td style='text-align:center'><b>" + Convert.ToDecimal(dsInvo.Tables[0].Rows[i]["PaybleAmount"]).ToString("0.00") + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TSupplyTotalQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("SupplyTotalQty"));
                decimal TReturnQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalReturnQty"));
                decimal TAdvCardQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalAdvCardQty"));
                decimal TInstQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalInstSupplyQty"));
                decimal TAdvCardQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalAdvCardQtyInLtr"));
                decimal TAdvCardAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
                //decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdvCardTotalAmount"));
                decimal TAdvCardTotalAmt = dsInvo.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("AdvCardTotalAmount") ?? 0);
                decimal TCashQty = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<int>("BillingQty"));
                decimal TCashQtyInLtr = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingQtyInLtr"));
                decimal TAamt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BillingAmount"));
                decimal TPaybleAmt = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaybleAmount"));

                decimal tcstaxamt = ((TPaybleAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
                decimal FTPaybleAmt = TPaybleAmt + tcstaxamt;
                string Amount = GenerateWordsinRs(FTPaybleAmt.ToString("0.00"));

                decimal tcstaxamt_AdvCard = ((TAdvCardTotalAmt * Convert.ToDecimal(dsInvo.Tables[0].Rows[0]["TcsTaxPer"]) / 100));
                decimal FAdvCardwithtcstax = TAdvCardTotalAmt + tcstaxamt_AdvCard;
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td style='text-align:center'><b>" + TSupplyTotalQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TAdvCardQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TReturnQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TInstQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TAdvCardQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TAdvCardAmt.ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TAdvCardTotalAmt.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQty.ToString() + "</b></td>");
                sb.Append("<td  style='text-align:center'><b>" + TCashQtyInLtr.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TAamt.ToString("0.00") + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
                sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
                sb.Append("<td colspan='7' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt_AdvCard.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("<td colspan='12' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? tcstaxamt.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td colspan='8' style='text-align:right'><b>" + FAdvCardwithtcstax.ToString("0.00") + "</b></td>");
                sb.Append("<td colspan='13' style='text-align:right'><b>" + FTPaybleAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='16'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td colspan='7' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='7' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='text-align:center'>" + dsInvo.Tables[1].Rows[i]["HSN_Code"].ToString() + "</td>");
                    sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TaxableValue"]).ToString("0.00") + "</td>");
                    sb.Append("<td colspan='7' style='text-align:right'>" + Convert.ToDecimal(dsInvo.Tables[1].Rows[i]["TotalTaxAmount"]).ToString("0.00") + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td colspan='7' style='text-align:right'><b>" + TTaxableValue.ToString("0.00") + "</b></td>");

                sb.Append("<td colspan='7' style='text-align:right'><b>" + TotalTaxAmount.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='9' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");

                //sb.Append("<td colspan='6' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No : <b> " + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else
                {
                    sb.Append("<td colspan='7' rowspan ='2' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }

                sb.Append("</tr>");

                sb.Append("</table>");
                Print.InnerHtml = sb.ToString();

                ClientScriptManager CSM = Page.ClientScript;
                string strScript = "<script>";
                strScript += "window.print();";

                strScript += "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (dsInvo != null) { dsInvo.Dispose(); }
        }

    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
    protected void txtDeliveryDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlInvoiceFor.SelectedValue == "1")
        {
            GetSS();
        }
        else
        {
            GetInstitution();
        }
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInvoiceFor.SelectedValue == "1")
        {
            GetSS();
        }
        else
        {
            GetInstitution();
        }
    }
}