using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class mis_DemandSupply_ProductPaymentSheet : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4,ds5,ds6,ds7 = new DataSet();
    double TBA = 0, TGSTA = 0, TTCSA = 0, PA1 = 0, PA2 = 0, PA3 = 0, PA4 = 0, PA5 = 0, PA6 = 0, PA7 = 0, PA8 = 0, PA9 = 0, PA10 = 0, PTA = 0;
    IFormatProvider culture = new CultureInfo("gu-in", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                PaymentMode();
                GetLocation();
                GetLocationSearch();
                GetRouteSerch();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = Date;
               
                txtDeliveryDate.Attributes.Add("readonly", "readonly");
                txtPaymentDate1.Attributes.Add("readonly", "readonly");
                txtPaymentDate2.Attributes.Add("readonly", "readonly");
                txtPaymentDate3.Attributes.Add("readonly", "readonly");
                txtPaymentDate4.Attributes.Add("readonly", "readonly");
                txtPaymentDate5.Attributes.Add("readonly", "readonly");
                txtPaymentDate6.Attributes.Add("readonly", "readonly");
                txtPaymentDate7.Attributes.Add("readonly", "readonly");
                txtPaymentDate8.Attributes.Add("readonly", "readonly");
                txtPaymentDate9.Attributes.Add("readonly", "readonly");
                txtPaymentDate10.Attributes.Add("readonly", "readonly");

                txtPaymentDate1.Text = Date;
                txtPaymentDate2.Text = Date;
                txtPaymentDate3.Text = Date;
                txtPaymentDate4.Text = Date;
                txtPaymentDate5.Text = Date;
                txtPaymentDate6.Text = Date;
                txtPaymentDate7.Text = Date;
                txtPaymentDate8.Text = Date;
                txtPaymentDate9.Text = Date;
                txtPaymentDate10.Text = Date;

                txtSearchDate.Text = Date;
                txtSearchDate.Attributes.Add("readonly", "readonly");
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
            ddlRoute.Items.Clear();

            ds2 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataSource = ds2.Tables[0];
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }
            


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null)
            {
                ds2.Dispose();
            }
        }
    }
    private void GetRouteSerch()
    {
        try
        {
            ddlRouteSearch.Items.Clear();

            ds2 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "9", objdb.Office_ID(), ddlLocationSearch.SelectedValue, objdb.GetProductCatId() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlRouteSearch.DataSource = ds2.Tables[0];
                ddlRouteSearch.DataTextField = "RName";
                ddlRouteSearch.DataValueField = "RouteId";
                ddlRouteSearch.DataBind();
                ddlRouteSearch.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlRouteSearch.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null)
            {
                ds2.Dispose();
            }
        }
    }
    private void GetDisOrSSByRouteID()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id"},
                   new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue,objdb.GetProductCatId() }, "dataset");
            ddlDitributor.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));

          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    protected void GetLocationSearch()
    {
        try
        {
            ddlLocationSearch.DataTextField = "AreaName";
            ddlLocationSearch.DataValueField = "AreaId";
            ddlLocationSearch.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocationSearch.DataBind();
            ddlLocationSearch.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Search Location:  ", ex.Message.ToString());
        }
    }
    private void PaymentMode()
    {
        try
        {
            lblMsg.Text = "";
            ds5 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds5 != null && ds5.Tables[0].Rows.Count > 0)
            {
                ddlPaymentMode1.DataSource = ds5.Tables[0];
                ddlPaymentMode1.DataTextField = "PaymentModeName";
                ddlPaymentMode1.DataValueField = "PaymentModeId";
                ddlPaymentMode1.DataBind();
                ddlPaymentMode1.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode2.DataSource = ds5.Tables[0];
                ddlPaymentMode2.DataTextField = "PaymentModeName";
                ddlPaymentMode2.DataValueField = "PaymentModeId";
                ddlPaymentMode2.DataBind();
                ddlPaymentMode2.Items.Insert(0, new ListItem("Select", "0"));


                ddlPaymentMode3.DataSource = ds5.Tables[0];
                ddlPaymentMode3.DataTextField = "PaymentModeName";
                ddlPaymentMode3.DataValueField = "PaymentModeId";
                ddlPaymentMode3.DataBind();
                ddlPaymentMode3.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode4.DataSource = ds5.Tables[0];
                ddlPaymentMode4.DataTextField = "PaymentModeName";
                ddlPaymentMode4.DataValueField = "PaymentModeId";
                ddlPaymentMode4.DataBind();
                ddlPaymentMode4.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode5.DataSource = ds5.Tables[0];
                ddlPaymentMode5.DataTextField = "PaymentModeName";
                ddlPaymentMode5.DataValueField = "PaymentModeId";
                ddlPaymentMode5.DataBind();
                ddlPaymentMode5.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode6.DataSource = ds5.Tables[0];
                ddlPaymentMode6.DataTextField = "PaymentModeName";
                ddlPaymentMode6.DataValueField = "PaymentModeId";
                ddlPaymentMode6.DataBind();
                ddlPaymentMode6.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode7.DataSource = ds5.Tables[0];
                ddlPaymentMode7.DataTextField = "PaymentModeName";
                ddlPaymentMode7.DataValueField = "PaymentModeId";
                ddlPaymentMode7.DataBind();
                ddlPaymentMode7.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode8.DataSource = ds5.Tables[0];
                ddlPaymentMode8.DataTextField = "PaymentModeName";
                ddlPaymentMode8.DataValueField = "PaymentModeId";
                ddlPaymentMode8.DataBind();
                ddlPaymentMode8.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode9.DataSource = ds5.Tables[0];
                ddlPaymentMode9.DataTextField = "PaymentModeName";
                ddlPaymentMode9.DataValueField = "PaymentModeId";
                ddlPaymentMode9.DataBind();
                ddlPaymentMode9.Items.Insert(0, new ListItem("Select", "0"));

                ddlPaymentMode10.DataSource = ds5.Tables[0];
                ddlPaymentMode10.DataTextField = "PaymentModeName";
                ddlPaymentMode10.DataValueField = "PaymentModeId";
                ddlPaymentMode10.DataBind();
                ddlPaymentMode10.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                 ddlPaymentMode1.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode2.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode3.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode4.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode5.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode6.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode7.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode8.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode9.Items.Insert(0, new ListItem("No Record Found", "0"));
                 ddlPaymentMode10.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }

    private void GetProductPaymentDetails()
    {
        try
        {
            DateTime dateSearch = DateTime.ParseExact(txtSearchDate.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet",
                     new string[] { "Flag", "Delivary_Date", "AreaId", "RouteId", "Office_ID" },
                     new string[] { "2", datSearch, ddlLocationSearch.SelectedValue, ddlRouteSearch.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                btnExport.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void ClearText()
    {
      
        ddlPaymentMode1.SelectedIndex = 0;
        txtDMNo.Text = string.Empty;
        txtTotalBillAmount.Text = string.Empty;
        txtTotalGSTAmount.Text = string.Empty;
        txtRemark.Text = "";
        ddlPaymentMode1.SelectedIndex = 0;
        ddlPaymentMode2.SelectedIndex = 0;
        ddlPaymentMode3.SelectedIndex = 0;
        ddlPaymentMode4.SelectedIndex = 0;
        ddlPaymentMode5.SelectedIndex = 0;
        ddlPaymentMode6.SelectedIndex = 0;
        ddlPaymentMode7.SelectedIndex = 0;
        ddlPaymentMode8.SelectedIndex = 0;
        ddlPaymentMode9.SelectedIndex = 0;
        ddlPaymentMode10.SelectedIndex = 0;
        

        txtPaymentNo1.Text = string.Empty;
        txtPaymentNo2.Text = string.Empty;
        txtPaymentNo3.Text = string.Empty;
        txtPaymentNo4.Text = string.Empty;
        txtPaymentNo5.Text = string.Empty;
        txtPaymentNo6.Text = string.Empty;
        txtPaymentNo7.Text = string.Empty;
        txtPaymentNo8.Text = string.Empty;
        txtPaymentNo9.Text = string.Empty;
        txtPaymentNo10.Text = string.Empty;

        txtPaymentAmt1.Text = string.Empty;
        txtPaymentAmt2.Text = string.Empty;
        txtPaymentAmt3.Text = string.Empty;
        txtPaymentAmt4.Text = string.Empty;
        txtPaymentAmt5.Text = string.Empty;
        txtPaymentAmt6.Text = string.Empty;
        txtPaymentAmt7.Text = string.Empty;
        txtPaymentAmt8.Text = string.Empty;
        txtPaymentAmt9.Text = string.Empty;
        txtPaymentAmt10.Text = string.Empty;
       
        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
        txtTotalTcsTaxAmount.Text = string.Empty;
    }

    private void GetProductAmount()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime deldate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string delidate = deldate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds6 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet",
           new string[] { "Flag", "Delivary_Date", "AreaId", "RouteId", "ItemCat_id", "DelivaryShift_id", "DistributorId", "SuperStockistId", "Office_ID" },
           new string[] { "1",delidate,ddlLocation.SelectedValue,ddlRoute.SelectedValue ,objdb.GetProductCatId(),objdb.GetShiftMorId()
               ,ddlDitributor.SelectedValue,"0",objdb.Office_ID() }, "dataset");
            if (ds6 != null && ds6.Tables[0].Rows.Count > 0)
            {
                txtDMNo.Text = ds6.Tables[0].Rows[0]["DMChallanNo"].ToString();
                txtTotalBillAmount.Text = (Convert.ToDecimal(ds6.Tables[0].Rows[0]["Amount"]) + Convert.ToDecimal(ds6.Tables[0].Rows[0]["TCSTaxAmt"])).ToString("0.00");
                txtTotalGSTAmount.Text = (Convert.ToDecimal(ds6.Tables[0].Rows[0]["GST"])).ToString("0.00");
                txtTotalTcsTaxAmount.Text = (Convert.ToDecimal(ds6.Tables[0].Rows[0]["TCSTaxAmt"])).ToString("0.00");
            }
            else
            {
                txtDMNo.Text = "";
                txtTotalBillAmount.Text = "0";
                txtTotalGSTAmount.Text = "0";
                txtTotalTcsTaxAmount.Text = "0";
                txtDMNo.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ProductAmt ", ex.Message.ToString());
        }
       finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    #endregion
    #region=========== changed even===========================
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if(ddlRoute.SelectedValue!="0")
        {
            GetDisOrSSByRouteID();
            GetProductAmount();
        }
      

    }
    #endregion===========================

    #region=========== Button Event===========================
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (Page.IsValid)
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                decimal Balance = 0, P1 = 0, P2 = 0, P3 = 0, P4 = 0, P5 = 0, P6 = 0, P7 = 0, P8 = 0, P9 = 0, P10 = 0;
                DateTime deldat = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = deldat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                if (txtPaymentDate1.Text == "") { txtPaymentDate1.Text = "01/01/1900"; }
                if (txtPaymentDate2.Text == "") { txtPaymentDate2.Text = "01/01/1900"; }
                if (txtPaymentDate3.Text == "") { txtPaymentDate3.Text = "01/01/1900"; }
                if (txtPaymentDate4.Text == "") { txtPaymentDate4.Text = "01/01/1900"; }
                if (txtPaymentDate5.Text == "") { txtPaymentDate5.Text = "01/01/1900"; }
                if (txtPaymentDate6.Text == "") { txtPaymentDate6.Text = "01/01/1900"; }
                if (txtPaymentDate7.Text == "") { txtPaymentDate7.Text = "01/01/1900"; }
                if (txtPaymentDate8.Text == "") { txtPaymentDate8.Text = "01/01/1900"; }
                if (txtPaymentDate9.Text == "") { txtPaymentDate9.Text = "01/01/1900"; }
                if (txtPaymentDate10.Text == "") { txtPaymentDate10.Text = "01/01/1900"; }

                DateTime pd1 = DateTime.ParseExact(txtPaymentDate1.Text, "dd/MM/yyyy", culture);
                string pd11 = pd1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd2 = DateTime.ParseExact(txtPaymentDate2.Text, "dd/MM/yyyy", culture);
                string pd22 = pd2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd3 = DateTime.ParseExact(txtPaymentDate3.Text, "dd/MM/yyyy", culture);
                string pd33 = pd3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd4 = DateTime.ParseExact(txtPaymentDate4.Text, "dd/MM/yyyy", culture);
                string pd44 = pd4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd5 = DateTime.ParseExact(txtPaymentDate5.Text, "dd/MM/yyyy", culture);
                string pd55 = pd5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd6 = DateTime.ParseExact(txtPaymentDate6.Text, "dd/MM/yyyy", culture);
                string pd66 = pd6.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd7 = DateTime.ParseExact(txtPaymentDate7.Text, "dd/MM/yyyy", culture);
                string pd77 = pd7.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd8 = DateTime.ParseExact(txtPaymentDate8.Text, "dd/MM/yyyy", culture);
                string pd88 = pd8.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd9 = DateTime.ParseExact(txtPaymentDate9.Text, "dd/MM/yyyy", culture);
                string pd99 = pd9.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime pd10 = DateTime.ParseExact(txtPaymentDate10.Text, "dd/MM/yyyy", culture);
                string pd101 = pd10.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                if (txtPaymentAmt1.Text == "") { P1 = 0; } else { P1 = Convert.ToDecimal(txtPaymentAmt1.Text); }
                if (txtPaymentAmt2.Text == "") { P2 = 0; } else { P2 = Convert.ToDecimal(txtPaymentAmt2.Text); }
                if (txtPaymentAmt3.Text == "") { P3 = 0; } else { P3 = Convert.ToDecimal(txtPaymentAmt3.Text); }
                if (txtPaymentAmt4.Text == "") { P4 = 0; } else { P4 = Convert.ToDecimal(txtPaymentAmt4.Text); }
                if (txtPaymentAmt5.Text == "") { P5 = 0; } else { P5 = Convert.ToDecimal(txtPaymentAmt5.Text); }
                if (txtPaymentAmt6.Text == "") { P6 = 0; } else { P6 = Convert.ToDecimal(txtPaymentAmt6.Text); }
                if (txtPaymentAmt7.Text == "") { P7 = 0; } else { P7 = Convert.ToDecimal(txtPaymentAmt7.Text); }
                if (txtPaymentAmt8.Text == "") { P8 = 0; } else { P8 = Convert.ToDecimal(txtPaymentAmt8.Text); }
                if (txtPaymentAmt9.Text == "") { P9 = 0; } else { P9 = Convert.ToDecimal(txtPaymentAmt9.Text); }
                if (txtPaymentAmt10.Text == "") { P10 = 0; } else { P10 = Convert.ToDecimal(txtPaymentAmt10.Text); }




                Balance = (Convert.ToDecimal(txtTotalBillAmount.Text) - (P1 + P2 + P3 + P4 + P5 + P6 + P7 + P8 + P9 + P10));
                if (btnSave.Text == "Save")
                {
                   
                    ds7 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet"
                    , new string[] {"Flag","DMChallanNo","AreaId", "RouteId","SuperStockistId" ,"DistributorId","ItemCat_id" ,
                        "DelivaryShift_id","Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                    "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4"
                    ,"PaymentModeId5","PaymentNo5","PaymentAmount5", "PaymentDate5"
                    ,"PaymentModeId6","PaymentNo6","PaymentAmount6", "PaymentDate6"
                    ,"PaymentModeId7","PaymentNo7","PaymentAmount7", "PaymentDate7"
                    ,"PaymentModeId8","PaymentNo8","PaymentAmount8", "PaymentDate8"
                    ,"PaymentModeId9","PaymentNo9","PaymentAmount9", "PaymentDate9"
                    ,"PaymentModeId10","PaymentNo10","PaymentAmount10", "PaymentDate10"
                    ,"TotalTcsTaxAmt"}
                   , new string[] {"3" ,txtDMNo.Text.Trim(),ddlLocation.SelectedValue,ddlRoute.SelectedValue
                       ,"0",ddlDitributor.SelectedValue,objdb.GetProductCatId(),objdb.GetShiftMorId(),deliverydate,
                       txtTotalBillAmount.Text.Trim(),txtTotalGSTAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                     ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44
                     ,ddlPaymentMode5.SelectedValue,txtPaymentNo5.Text.Trim(),P5.ToString(),pd55
                        ,ddlPaymentMode6.SelectedValue,txtPaymentNo6.Text.Trim(),P6.ToString(),pd66
                        ,ddlPaymentMode7.SelectedValue,txtPaymentNo7.Text.Trim(),P7.ToString(),pd77
                        ,ddlPaymentMode8.SelectedValue,txtPaymentNo8.Text.Trim(),P8.ToString(),pd88
                        ,ddlPaymentMode9.SelectedValue,txtPaymentNo9.Text.Trim(),P9.ToString(),pd99
                        ,ddlPaymentMode10.SelectedValue,txtPaymentNo10.Text.Trim(),P10.ToString(),pd101
                     , txtTotalTcsTaxAmount.Text.Trim()}, "dataset");
                    if (ds7 != null && ds7.Tables.Count > 0)
                    {
                        if (ds7.Tables[0].Rows.Count > 0)
                        {
                            if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Msg.ToString());                               
                                ClearText();
                            }
                            else if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                            {
                                string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString() + " Exits for " + ddlDitributor.SelectedItem.Text);
                            }
                            else
                            {
                                string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                            }
                        }
                    }
                }

                if (btnSave.Text == "Update")
                {
                    ds7 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet"
                    , new string[] {"Flag","ProductPaymentSheet_ID","DMChallanNo","AreaId", "RouteId","SuperStockistId" ,"DistributorId","ItemCat_id" ,
                        "DelivaryShift_id","Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                     "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4"
                      ,"PaymentModeId5","PaymentNo5","PaymentAmount5", "PaymentDate5"
                    ,"PaymentModeId6","PaymentNo6","PaymentAmount6", "PaymentDate6"
                    ,"PaymentModeId7","PaymentNo7","PaymentAmount7", "PaymentDate7"
                    ,"PaymentModeId8","PaymentNo8","PaymentAmount8", "PaymentDate8"
                    ,"PaymentModeId9","PaymentNo9","PaymentAmount9", "PaymentDate9"
                    ,"PaymentModeId10","PaymentNo10","PaymentAmount10", "PaymentDate10"
                     ,"TotalTcsTaxAmt"}
                   , new string[] {"4" ,ViewState["rowid"].ToString(),txtDMNo.Text.Trim(),ddlLocation.SelectedValue,ddlRoute.SelectedValue
                       ,"0",ddlDitributor.SelectedValue,objdb.GetProductCatId(),objdb.GetShiftMorId(),deliverydate,
                       txtTotalBillAmount.Text.Trim(),txtTotalGSTAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                      ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44
                       ,ddlPaymentMode5.SelectedValue,txtPaymentNo5.Text.Trim(),P5.ToString(),pd55
                        ,ddlPaymentMode6.SelectedValue,txtPaymentNo6.Text.Trim(),P6.ToString(),pd66
                        ,ddlPaymentMode7.SelectedValue,txtPaymentNo7.Text.Trim(),P7.ToString(),pd77
                        ,ddlPaymentMode8.SelectedValue,txtPaymentNo8.Text.Trim(),P8.ToString(),pd88
                        ,ddlPaymentMode9.SelectedValue,txtPaymentNo9.Text.Trim(),P9.ToString(),pd99
                        ,ddlPaymentMode10.SelectedValue,txtPaymentNo10.Text.Trim(),P10.ToString(),pd101
                      ,txtTotalTcsTaxAmount.Text.Trim()}, "dataset");
                    if (ds7 != null && ds7.Tables.Count > 0)
                    {
                        if (ds7.Tables[0].Rows.Count > 0)
                        {
                            if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Msg.ToString());
                                GetProductPaymentDetails();
                                ClearText();
                            }
                            else if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                            {
                                string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString() + " Exits for " + ddlDitributor.SelectedItem.Text);
                            }
                            else
                            {
                                string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                            }
                        }
                    }
                }
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblAreaID = (Label)row.FindControl("lblAreaID");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblDelivaryShift_id = (Label)row.FindControl("lblDelivaryShift_id");
                    Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
                    Label lblIsActive = (Label)row.FindControl("IsActive");
                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblSuperStockistId = (Label)row.FindControl("lblSuperStockistId");
                    Label lblCatId = (Label)row.FindControl("lblCatId");
                   
                    Label lblTotalBillAmount = (Label)row.FindControl("lblTotalBillAmount");
                    Label lblTotalGSTAmount = (Label)row.FindControl("lblTotalGSTAmount");
                    Label lblDMChallanNo = (Label)row.FindControl("lblDMChallanNo");
                    Label lblPaymentModeId1 = (Label)row.FindControl("lblPaymentModeId1");
                    Label lblPaymentNo1 = (Label)row.FindControl("lblPaymentNo1");
                    Label lblPaymentAmount1 = (Label)row.FindControl("lblPaymentAmount1");
                    Label lblPaymentDate1 = (Label)row.FindControl("lblPaymentDate1");

                    Label lblPaymentModeId2 = (Label)row.FindControl("lblPaymentModeId2");
                    Label lblPaymentNo2 = (Label)row.FindControl("lblPaymentNo2");
                    Label lblPaymentAmount2 = (Label)row.FindControl("lblPaymentAmount2");
                    Label lblPaymentDate2 = (Label)row.FindControl("lblPaymentDate2");

                    Label lblPaymentModeId3 = (Label)row.FindControl("lblPaymentModeId3");
                    Label lblPaymentNo3 = (Label)row.FindControl("lblPaymentNo3");
                    Label lblPaymentAmount3 = (Label)row.FindControl("lblPaymentAmount3");
                    Label lblPaymentDate3 = (Label)row.FindControl("lblPaymentDate3");

                    Label lblPaymentModeId4 = (Label)row.FindControl("lblPaymentModeId4");
                    Label lblPaymentNo4 = (Label)row.FindControl("lblPaymentNo4");
                    Label lblPaymentAmount4 = (Label)row.FindControl("lblPaymentAmount4");
                    Label lblPaymentDate4 = (Label)row.FindControl("lblPaymentDate4");

                    Label lblPaymentModeId5 = (Label)row.FindControl("lblPaymentModeId5");
                    Label lblPaymentNo5 = (Label)row.FindControl("lblPaymentNo5");
                    Label lblPaymentAmount5 = (Label)row.FindControl("lblPaymentAmount5");
                    Label lblPaymentDate5 = (Label)row.FindControl("lblPaymentDate5");

                    Label lblPaymentModeId6 = (Label)row.FindControl("lblPaymentModeId6");
                    Label lblPaymentNo6 = (Label)row.FindControl("lblPaymentNo6");
                    Label lblPaymentAmount6 = (Label)row.FindControl("lblPaymentAmount6");
                    Label lblPaymentDate6 = (Label)row.FindControl("lblPaymentDate6");

                    Label lblPaymentModeId7 = (Label)row.FindControl("lblPaymentModeId7");
                    Label lblPaymentNo7 = (Label)row.FindControl("lblPaymentNo7");
                    Label lblPaymentAmount7 = (Label)row.FindControl("lblPaymentAmount7");
                    Label lblPaymentDate7 = (Label)row.FindControl("lblPaymentDate7");

                    Label lblPaymentModeId8 = (Label)row.FindControl("lblPaymentModeId8");
                    Label lblPaymentNo8 = (Label)row.FindControl("lblPaymentNo8");
                    Label lblPaymentAmount8 = (Label)row.FindControl("lblPaymentAmount8");
                    Label lblPaymentDate8 = (Label)row.FindControl("lblPaymentDate8");

                    Label lblPaymentModeId9 = (Label)row.FindControl("lblPaymentModeId9");
                    Label lblPaymentNo9 = (Label)row.FindControl("lblPaymentNo9");
                    Label lblPaymentAmount9 = (Label)row.FindControl("lblPaymentAmount9");
                    Label lblPaymentDate9 = (Label)row.FindControl("lblPaymentDate9");

                    Label lblPaymentModeId10 = (Label)row.FindControl("lblPaymentModeId10");
                    Label lblPaymentNo10 = (Label)row.FindControl("lblPaymentNo10");
                    Label lblPaymentAmount10 = (Label)row.FindControl("lblPaymentAmount10");
                    Label lblPaymentDate10 = (Label)row.FindControl("lblPaymentDate10");

                    Label lblTotalTcsTaxAmt = (Label)row.FindControl("lblTotalTcsTaxAmt");
                    

                    txtDeliveryDate.Text = lblDelivary_Date.Text;
                    ddlLocation.SelectedValue = lblAreaID.Text;
                    GetRoute();
                    ddlRoute.SelectedValue = lblRouteId.Text;
                    if (lblDistributorId.Text != "")
                    {
                        GetDisOrSSByRouteID();
                        ddlDitributor.SelectedValue = lblDistributorId.Text;
                    }
                    txtTotalBillAmount.Text = lblTotalBillAmount.Text;
                    txtTotalGSTAmount.Text = lblTotalGSTAmount.Text;
                    txtTotalTcsTaxAmount.Text = lblTotalTcsTaxAmt.Text;
                    txtDMNo.Text = lblDMChallanNo.Text;
                    txtRemark.Text = lblRouteId.Text;
                    if (lblPaymentModeId1.Text != "")
                    {
                        ddlPaymentMode1.SelectedValue = lblPaymentModeId1.Text;
                    }
                    if (lblPaymentModeId2.Text != "")
                    {
                        ddlPaymentMode2.SelectedValue = lblPaymentModeId2.Text;
                    }
                    if (lblPaymentModeId3.Text != "")
                    {
                        ddlPaymentMode3.SelectedValue = lblPaymentModeId3.Text;
                    }
                    if (lblPaymentModeId4.Text!="")
                    {
                        ddlPaymentMode4.SelectedValue = lblPaymentModeId4.Text;
                    }
                    if (lblPaymentModeId5.Text != "")
                    {
                        ddlPaymentMode5.SelectedValue = lblPaymentModeId5.Text;
                    }
                    if (lblPaymentModeId6.Text != "")
                    {
                        ddlPaymentMode6.SelectedValue = lblPaymentModeId6.Text;
                    }
                    if (lblPaymentModeId7.Text != "")
                    {
                        ddlPaymentMode7.SelectedValue = lblPaymentModeId7.Text;
                    }
                    if (lblPaymentModeId8.Text != "")
                    {
                        ddlPaymentMode8.SelectedValue = lblPaymentModeId8.Text;
                    }
                    if (lblPaymentModeId9.Text != "")
                    {
                        ddlPaymentMode9.SelectedValue = lblPaymentModeId9.Text;
                    }
                    if (lblPaymentModeId10.Text != "")
                    {
                        ddlPaymentMode10.SelectedValue = lblPaymentModeId10.Text;
                    }
                    
                   
                    txtPaymentNo1.Text = lblPaymentNo1.Text;
                    txtPaymentNo2.Text = lblPaymentNo2.Text;
                    txtPaymentNo3.Text = lblPaymentNo3.Text;
                    txtPaymentNo4.Text = lblPaymentNo4.Text;
                    txtPaymentNo5.Text = lblPaymentNo5.Text;
                    txtPaymentNo6.Text = lblPaymentNo6.Text;
                    txtPaymentNo7.Text = lblPaymentNo7.Text;
                    txtPaymentNo8.Text = lblPaymentNo8.Text;
                    txtPaymentNo9.Text = lblPaymentNo9.Text;
                    txtPaymentNo10.Text = lblPaymentNo10.Text;

                    txtPaymentAmt1.Text = lblPaymentAmount1.Text;
                    txtPaymentAmt2.Text = lblPaymentAmount2.Text;
                    txtPaymentAmt3.Text = lblPaymentAmount3.Text;
                    txtPaymentAmt4.Text = lblPaymentAmount4.Text;
                    txtPaymentAmt5.Text = lblPaymentAmount5.Text;
                    txtPaymentAmt6.Text = lblPaymentAmount6.Text;
                    txtPaymentAmt7.Text = lblPaymentAmount7.Text;
                    txtPaymentAmt8.Text = lblPaymentAmount8.Text;
                    txtPaymentAmt9.Text = lblPaymentAmount9.Text;
                    txtPaymentAmt10.Text = lblPaymentAmount10.Text;
                   

                    if(lblPaymentDate1.Text=="01/01/1900")
                    {
                        txtPaymentDate1.Text = "";
                    }
                    else
                    {
                        txtPaymentDate1.Text = lblPaymentDate1.Text;
                    }
                    if (lblPaymentDate2.Text == "01/01/1900")
                    {
                        lblPaymentDate2.Text = "";
                    }
                    else
                    {
                        txtPaymentDate2.Text = lblPaymentDate2.Text;
                    }
                    if (lblPaymentDate3.Text == "01/01/1900")
                    {
                        lblPaymentDate3.Text = "";
                    }
                    else
                    {
                        txtPaymentDate3.Text = lblPaymentDate3.Text;
                    }
                    if (lblPaymentDate4.Text == "01/01/1900")
                    {
                        lblPaymentDate4.Text = "";
                    }
                    else
                    {
                        txtPaymentDate4.Text = lblPaymentDate4.Text;
                    }
                    if (lblPaymentDate5.Text == "01/01/1900")
                    {
                        txtPaymentDate5.Text = "";
                    }
                    else
                    {
                        txtPaymentDate5.Text = lblPaymentDate5.Text;
                    }

                    if (lblPaymentDate6.Text == "01/01/1900")
                    {
                        txtPaymentDate6.Text = "";
                    }
                    else
                    {
                        txtPaymentDate6.Text = lblPaymentDate6.Text;
                    }

                    if (lblPaymentDate7.Text == "01/01/1900")
                    {
                        txtPaymentDate7.Text = "";
                    }
                    else
                    {
                        txtPaymentDate7.Text = lblPaymentDate7.Text;
                    }

                    if (lblPaymentDate8.Text == "01/01/1900")
                    {
                        txtPaymentDate8.Text = "";
                    }
                    else
                    {
                        txtPaymentDate8.Text = lblPaymentDate8.Text;
                    }

                    if (lblPaymentDate9.Text == "01/01/1900")
                    {
                        txtPaymentDate9.Text = "";
                    }
                    else
                    {
                        txtPaymentDate9.Text = lblPaymentDate9.Text;
                    }

                    if (lblPaymentDate10.Text == "01/01/1900")
                    {
                        txtPaymentDate10.Text = "";
                    }
                    else
                    {
                        txtPaymentDate10.Text = lblPaymentDate10.Text;
                    }
                   
                    ViewState["rowid"] = e.CommandArgument;
                    btnSave.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtDMNo.Text = string.Empty;
        txtTotalBillAmount.Text = string.Empty;
        txtTotalGSTAmount.Text = string.Empty;
        GridView1.SelectedIndex=-1;
        btnSave.Text = "Save";
        txtDeliveryDate.Text = string.Empty;
        ddlLocation.SelectedValue = "0";
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        ddlDitributor.Items.Clear();
        ddlDitributor.Items.Insert(0, new ListItem("Select", "0"));
        txtRemark.Text = "";
        ddlPaymentMode1.SelectedIndex = 0;
        ddlPaymentMode2.SelectedIndex = 0;
        ddlPaymentMode3.SelectedIndex = 0;
        ddlPaymentMode4.SelectedIndex = 0;
        ddlPaymentMode5.SelectedIndex = 0;
        ddlPaymentMode6.SelectedIndex = 0;
        ddlPaymentMode7.SelectedIndex = 0;
        ddlPaymentMode8.SelectedIndex = 0;
        ddlPaymentMode9.SelectedIndex = 0;
        ddlPaymentMode10.SelectedIndex = 0;

        string Date1 = DateTime.Now.ToString("dd/MM/yyyy");
        txtPaymentDate1.Text = Date1;
        txtPaymentDate2.Text = Date1;
        txtPaymentDate3.Text = Date1;
        txtPaymentDate4.Text = Date1;
        txtPaymentDate5.Text = Date1;
        txtPaymentDate6.Text = Date1;
        txtPaymentDate7.Text = Date1;
        txtPaymentDate8.Text = Date1;
        txtPaymentDate9.Text = Date1;
        txtPaymentDate10.Text = Date1;

        txtPaymentAmt1.Text = "";
        txtPaymentAmt2.Text = "";
        txtPaymentAmt3.Text = "";
        txtPaymentAmt4.Text = "";
        txtPaymentAmt5.Text = "";
        txtPaymentAmt6.Text = "";
        txtPaymentAmt7.Text = "";
        txtPaymentAmt8.Text = "";
        txtPaymentAmt9.Text = "";
        txtPaymentAmt10.Text = "";

        txtPaymentNo1.Text = "";
        txtPaymentNo2.Text = "";
        txtPaymentNo3.Text = "";
        txtPaymentNo4.Text = "";
        txtPaymentNo5.Text = "";
        txtPaymentNo6.Text = "";
        txtPaymentNo7.Text = "";
        txtPaymentNo8.Text = "";
        txtPaymentNo9.Text = "";
        txtPaymentNo10.Text = "";
    }
    #endregion===========================
   
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void ddlLocationSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocationSearch.SelectedValue != "0")
        {
           
            GetRouteSerch();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
             lblMsg.Text=string.Empty;
            GetProductPaymentDetails();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlRouteSearch.SelectedItem.Text + "-" + txtSearchDate.Text + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            GridView1.Columns[12].Visible = false;
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-Consolidated " + ex.Message.ToString());
        }
    }
    protected void txtDeliveryDate_TextChanged(object sender, EventArgs e)
    {
        GetProductAmount();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotalBillAmount = (e.Row.FindControl("lblTotalBillAmount") as Label);
                Label lblTotalGSTAmount = (e.Row.FindControl("lblTotalGSTAmount") as Label);
                Label lblTotalTcsTaxAmt = (e.Row.FindControl("lblTotalTcsTaxAmt") as Label);
                Label lblPaymentAmount1 = (e.Row.FindControl("lblPaymentAmount1") as Label);
                Label lblPaymentAmount2 = (e.Row.FindControl("lblPaymentAmount2") as Label);
                Label lblPaymentAmount3 = (e.Row.FindControl("lblPaymentAmount3") as Label);
                Label lblPaymentAmount4 = (e.Row.FindControl("lblPaymentAmount4") as Label);
                Label lblPaymentAmount5 = (e.Row.FindControl("lblPaymentAmount5") as Label);
                Label lblPaymentAmount6 = (e.Row.FindControl("lblPaymentAmount6") as Label);
                Label lblPaymentAmount7 = (e.Row.FindControl("lblPaymentAmount7") as Label);
                Label lblPaymentAmount8 = (e.Row.FindControl("lblPaymentAmount8") as Label);
                Label lblPaymentAmount9 = (e.Row.FindControl("lblPaymentAmount9") as Label);
                Label lblPaymentAmount10 = (e.Row.FindControl("lblPaymentAmount10") as Label);

                Label lblTotalPaidPayment = (e.Row.FindControl("lblTotalPaidPayment") as Label);
                TBA += Convert.ToDouble(lblTotalBillAmount.Text);
                TGSTA += Convert.ToDouble(lblTotalGSTAmount.Text);
                TTCSA += Convert.ToDouble(lblTotalTcsTaxAmt.Text);
                PA1 += Convert.ToDouble(lblPaymentAmount1.Text);
                PA2 += Convert.ToDouble(lblPaymentAmount2.Text);
                PA3 += Convert.ToDouble(lblPaymentAmount3.Text);
                PA4 += Convert.ToDouble(lblPaymentAmount4.Text);
                PA5 += Convert.ToDouble(lblPaymentAmount5.Text);
                PA6 += Convert.ToDouble(lblPaymentAmount6.Text);
                PA7 += Convert.ToDouble(lblPaymentAmount7.Text);
                PA8 += Convert.ToDouble(lblPaymentAmount8.Text);
                PA9 += Convert.ToDouble(lblPaymentAmount9.Text);
                PA10 += Convert.ToDouble(lblPaymentAmount10.Text);

                PTA += Convert.ToDouble(lblTotalPaidPayment.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTBillAmt = (e.Row.FindControl("lblTBillAmt") as Label);
                Label lblFGSTAmt = (e.Row.FindControl("lblFGSTAmt") as Label);
                Label lblFTcsTaxAmt = (e.Row.FindControl("lblFTcsTaxAmt") as Label);
                Label lblFPA1 = (e.Row.FindControl("lblFPA1") as Label);
                Label lblFPA2 = (e.Row.FindControl("lblFPA2") as Label);
                Label lblFPA3 = (e.Row.FindControl("lblFPA3") as Label);
                Label lblFPA4 = (e.Row.FindControl("lblFPA4") as Label);
                Label lblFPA5 = (e.Row.FindControl("lblFPA5") as Label);
                Label lblFPA6 = (e.Row.FindControl("lblFPA6") as Label);
                Label lblFPA7 = (e.Row.FindControl("lblFPA7") as Label);
                Label lblFPA8 = (e.Row.FindControl("lblFPA8") as Label);
                Label lblFPA9 = (e.Row.FindControl("lblFPA9") as Label);
                Label lblFPA10 = (e.Row.FindControl("lblFPA10") as Label);

                Label lblFTPA = (e.Row.FindControl("lblFTPA") as Label);
                lblTBillAmt.Text = TBA.ToString("0.00");
                lblFGSTAmt.Text = TGSTA.ToString("0.00");
                lblFTcsTaxAmt.Text = TTCSA.ToString("0.00");               
                lblFPA1.Text = PA1.ToString("0.00");
                lblFPA2.Text = PA2.ToString("0.00");
                lblFPA3.Text = PA3.ToString("0.00");
                lblFPA4.Text = PA4.ToString("0.00");
                lblFPA5.Text = PA5.ToString("0.00");
                lblFPA6.Text = PA6.ToString("0.00");
                lblFPA7.Text = PA7.ToString("0.00");
                lblFPA8.Text = PA8.ToString("0.00");
                lblFPA9.Text = PA9.ToString("0.00");
                lblFPA10.Text = PA10.ToString("0.00");

                lblFTPA.Text = PTA.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
}