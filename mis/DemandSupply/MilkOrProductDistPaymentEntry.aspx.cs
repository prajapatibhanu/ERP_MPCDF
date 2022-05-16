using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DemandSupply_MilkOrProductDistPaymentEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds6, ds4, ds5, ds7, ds8 = new DataSet();
    double MA = 0, PA1 = 0, PA2 = 0, PA3 = 0, PA4 = 0, PTA = 0;
    double TBA = 0, TGSTA = 0, TTCSA = 0, PPA1 = 0, PPA2 = 0, PPA3 = 0, PPA4 = 0, PPTA = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                DisplayMilkOrProductPanel();
                PaymentMode();
                GetLocation();
                GetFilterCategory();
                GetSSByLocationOrCategory();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDateFilter.Text = Date;
                txtDeliveryDate.Text = Date;
                txtDeliveryDate.Attributes.Add("readonly", "readonly");
                txtDateFilter.Attributes.Add("readonly", "readonly");
                txtPaymentDate1.Attributes.Add("readonly", "readonly");
                txtPaymentDate2.Attributes.Add("readonly", "readonly");
                txtPaymentDate3.Attributes.Add("readonly", "readonly");
                txtPaymentDate4.Attributes.Add("readonly", "readonly");
                txtPaymentDate1.Text = Date;
                txtPaymentDate2.Text = Date;
                txtPaymentDate3.Text = Date;
                txtPaymentDate4.Text = Date;
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                DisplayMilkOrProductPanel();
            }
        }
        else
        {
            objdb.redirectToHome();
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

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetFilterCategory()
    {
        try
        {

            ddlFilterItemCategory.DataTextField = "ItemCatName";
            ddlFilterItemCategory.DataValueField = "ItemCat_id";
            ddlFilterItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlFilterItemCategory.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void DisplayMilkOrProductPanel()
    {
        if(ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
        {
            pnlMilk.Visible = true;
            pnlProduct.Visible = false;
            GetSS();
        }
        else
        {
            
            pnlMilk.Visible = false;
            pnlProduct.Visible = true;
            GetSS();
        }
    }
    private void GetSS()
    {
        try
        {
            ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlDitributor.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlDitributor.DataTextField = "SSName";
                ddlDitributor.DataValueField = "SSRDId";
                ddlDitributor.DataSource = ds7.Tables[0];
                ddlDitributor.DataBind();
                ddlDitributor.Items.Insert(0, new ListItem("Select", "0"));
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
    private void GetSSByLocationOrCategory()
    {
        try
        {
            ds7 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "7", objdb.Office_ID(), ddlLocationfilter.SelectedValue, ddlFilterItemCategory.SelectedValue }, "dataset");
            ddlFilterDistributor.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlFilterDistributor.DataTextField = "SSName";
                ddlFilterDistributor.DataValueField = "SSRDId";
                ddlFilterDistributor.DataSource = ds7.Tables[0];
                ddlFilterDistributor.DataBind();
                ddlFilterDistributor.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlFilterDistributor.Items.Insert(0, new ListItem("No Record Found.", "0"));
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
    #region=========== User Defined function======================
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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
            }
            else
            {
                ddlPaymentMode1.Items.Insert(0, new ListItem("No Record Found", "0"));
                ddlPaymentMode2.Items.Insert(0, new ListItem("No Record Found", "0"));
                ddlPaymentMode3.Items.Insert(0, new ListItem("No Record Found", "0"));
                ddlPaymentMode4.Items.Insert(0, new ListItem("No Record Found", "0"));
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
    private void FillGridMilk()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime dateSearch = DateTime.ParseExact(txtDateFilter.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string disid = "";
            if (ddlFilterDistributor.SelectedValue == "0")
            {
                disid = "0";
            }
            else
            {
                string[] SSRDId = ddlFilterDistributor.SelectedValue.Split('-');
                disid = SSRDId[1];
            }
            ds4 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet",
                     new string[] { "flag", "Office_ID", "Delivary_Date", "AreaId", "RouteId" },
                       new string[] { "2", objdb.Office_ID(), dateSearch.ToString(), ddlLocationfilter.SelectedValue, disid.ToString() }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds4.Tables[0];
                GridView1.DataBind();
                btnExport.Visible = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnExport.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    private void GetProductPaymentDetails()
    {
        try
        {
            DateTime dateSearch = DateTime.ParseExact(txtDateFilter.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string routeid = "";
            if (ddlFilterDistributor.SelectedValue == "0")
            {
                routeid = "0";
            }
            else
            {
                string[] SSRDId = ddlFilterDistributor.SelectedValue.Split('-');
                routeid = SSRDId[1];
            }
            ds1 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet",
                     new string[] { "Flag", "Delivary_Date", "AreaId", "RouteId", "Office_ID" },
                     new string[] { "2", datSearch, ddlLocationfilter.SelectedValue, routeid, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                GridView2.DataSource = ds1.Tables[0];
                GridView2.DataBind();
            }
            else
            {
                btnExport.Visible = false;
                GridView2.DataSource = null;
                GridView2.DataBind();
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

            ddlLocationfilter.DataTextField = "AreaName";
            ddlLocationfilter.DataValueField = "AreaId";
            ddlLocationfilter.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocationfilter.DataBind();
            ddlLocationfilter.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    private void GetMilkAmount()
    {
        try
        {
            lblMsg.Text = "";
            txtMilkAmount.Text = "";
            DateTime deldate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string delidate = deldate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string disid="";
            if(ddlDitributor.SelectedValue=="0")
            {
                disid="0";
            }
            else
            {
                string[] SSRDId = ddlDitributor.SelectedValue.Split('-');
                disid = SSRDId[2];
            }
           
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductInvoiceDetail",
           new string[] { "Flag", "Delivary_Date", "ItemCat_id", "DistributorId", "Office_ID" },
           new string[] { "5", delidate, objdb.GetMilkCatId(), disid, objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                txtMilkAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
            }
            else
            {
                txtMilkAmount.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    private void GetProductAmount()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime deldate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string delidate = deldate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string disid = "",routeid="";
            if (ddlDitributor.SelectedValue == "0")
            {
                disid = "0";
                routeid = "0";
            }
            else
            {
                string[] SSRDId = ddlDitributor.SelectedValue.Split('-');
                disid = SSRDId[2];
                routeid = SSRDId[1];
            }
            ds6 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet",
           new string[] { "Flag", "Delivary_Date", "AreaId", "RouteId", "ItemCat_id", "DistributorId", "SuperStockistId", "Office_ID" },
           new string[] { "1",delidate,ddlLocation.SelectedValue,routeid ,ddlItemCategory.SelectedValue
               ,disid.ToString(),"0",objdb.Office_ID() }, "dataset");
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
    private void ClearText()
    {

        if(ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
        {
            pnlMilk.Visible = true;
            txtMilkAmount.Text = "";
            txtRemark.Text = "";
            GridView1.SelectedIndex = -1;
        }
        else
        {
            pnlProduct.Visible = true;
            txtDMNo.Text = string.Empty;
            txtTotalBillAmount.Text = string.Empty;
            txtTotalGSTAmount.Text = string.Empty;
            txtTotalTcsTaxAmount.Text = string.Empty;
            GridView2.SelectedIndex = -1;
            pnlMilk.Visible = false;
        }

        btnSave.Text = "Save";
    }
    #endregion

    #region=========== Button Event===========================
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {

                    if(ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
                    {
                        InserMilkPayment();
                    }
                    else
                    {
                        InsertProductPayment();
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            txtMilkAmount.Text = "";
            txtRemark.Text = "";
            pnlMilk.Visible = true; 
            GridView1.SelectedIndex = -1;
        }
        else
        {
            GridView2.SelectedIndex = -1;
            txtDMNo.Text = string.Empty;
            txtTotalBillAmount.Text = string.Empty;
            txtTotalGSTAmount.Text = string.Empty;
            txtTotalTcsTaxAmount.Text = string.Empty;
        }
       
        ddlPaymentMode1.SelectedIndex = 0;
        ddlPaymentMode2.SelectedIndex = 0;
        ddlPaymentMode3.SelectedIndex = 0;
        ddlPaymentMode4.SelectedIndex = 0;
        string Date1 = DateTime.Now.ToString("dd/MM/yyyy");
        txtPaymentDate1.Text = Date1;
        txtPaymentDate2.Text = Date1;
        txtPaymentDate3.Text = Date1;
        txtPaymentDate4.Text = Date1;
        txtPaymentAmt1.Text = "";
        txtPaymentAmt2.Text = "";
        txtPaymentAmt3.Text = "";
        txtPaymentAmt4.Text = "";
        txtPaymentNo1.Text = "";
        txtPaymentNo2.Text = "";
        txtPaymentNo3.Text = "";
        txtPaymentNo4.Text = "";
        ddlDitributor.SelectedIndex = 0;
        btnSave.Text = "Save";

    }
    #endregion===========================

    #region Rowcommand Event
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "EditRecord")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblAreaID = (Label)row.FindControl("lblAreaID");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");

                    Label lblAmount = (Label)row.FindControl("lblAmount");


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
                    Label lblSSRDId = (Label)row.FindControl("lblSSRDId");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");

                    pnlMilk.Visible = true;
                    txtMilkAmount.Text = lblAmount.Text;
                    txtDeliveryDate.Text = lblDelivary_Date.Text;
                    ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    ddlLocation.SelectedValue = lblAreaID.Text;
                    DisplayMilkOrProductPanel();
                    ddlDitributor.SelectedValue = lblSSRDId.Text;
                    txtRemark.Text = lblRemark.Text;
                    ddlPaymentMode1.SelectedValue = lblPaymentModeId1.Text;
                    ddlPaymentMode2.SelectedValue = lblPaymentModeId2.Text;
                    ddlPaymentMode3.SelectedValue = lblPaymentModeId3.Text;
                    ddlPaymentMode4.SelectedValue = lblPaymentModeId4.Text;
                    txtPaymentNo1.Text = lblPaymentNo1.Text;
                    txtPaymentNo2.Text = lblPaymentNo2.Text;
                    txtPaymentNo3.Text = lblPaymentNo3.Text;
                    txtPaymentNo4.Text = lblPaymentNo4.Text;
                    txtPaymentAmt1.Text = lblPaymentAmount1.Text;
                    txtPaymentAmt2.Text = lblPaymentAmount2.Text;
                    txtPaymentAmt3.Text = lblPaymentAmount3.Text;
                    txtPaymentAmt4.Text = lblPaymentAmount4.Text;
                    if (lblPaymentDate1.Text == "01/01/1900")
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                Label lblPaymentAmount1 = (e.Row.FindControl("lblPaymentAmount1") as Label);
                Label lblPaymentAmount2 = (e.Row.FindControl("lblPaymentAmount2") as Label);
                Label lblPaymentAmount3 = (e.Row.FindControl("lblPaymentAmount3") as Label);
                Label lblPaymentAmount4 = (e.Row.FindControl("lblPaymentAmount4") as Label);
                Label lblTotalPayment = (e.Row.FindControl("lblTotalPayment") as Label);
                MA += Convert.ToDouble(lblAmount.Text);
                PA1 += Convert.ToDouble(lblPaymentAmount1.Text);
                PA2 += Convert.ToDouble(lblPaymentAmount2.Text);
                PA3 += Convert.ToDouble(lblPaymentAmount3.Text);
                PA4 += Convert.ToDouble(lblPaymentAmount4.Text);
                PTA += Convert.ToDouble(lblTotalPayment.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFAmount = (e.Row.FindControl("lblFAmount") as Label);
                Label lblFPA1 = (e.Row.FindControl("lblFPA1") as Label);
                Label lblFPA2 = (e.Row.FindControl("lblFPA2") as Label);
                Label lblFPA3 = (e.Row.FindControl("lblFPA3") as Label);
                Label lblFPA4 = (e.Row.FindControl("lblFPA4") as Label);
                Label lblFTPA = (e.Row.FindControl("lblFTPA") as Label);
                lblFAmount.Text = MA.ToString("0.00");
                lblFPA1.Text = PA1.ToString("0.00");
                lblFPA2.Text = PA2.ToString("0.00");
                lblFPA3.Text = PA3.ToString("0.00");
                lblFPA4.Text = PA4.ToString("0.00");
                lblFTPA.Text = PTA.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label PlblAreaID = (Label)row.FindControl("PlblAreaID");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblDelivaryShift_id = (Label)row.FindControl("lblDelivaryShift_id");
                    Label PlblDelivary_Date = (Label)row.FindControl("PlblDelivary_Date");
                    Label lblIsActive = (Label)row.FindControl("IsActive");
                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblSuperStockistId = (Label)row.FindControl("lblSuperStockistId");
                    Label lblCatId = (Label)row.FindControl("lblCatId");

                    Label lblTotalBillAmount = (Label)row.FindControl("lblTotalBillAmount");
                    Label lblTotalGSTAmount = (Label)row.FindControl("lblTotalGSTAmount");
                    Label lblDMChallanNo = (Label)row.FindControl("lblDMChallanNo");
                    Label PlblPaymentModeId1 = (Label)row.FindControl("PlblPaymentModeId1");
                    Label PlblPaymentNo1 = (Label)row.FindControl("PlblPaymentNo1");
                    Label PlblPaymentAmount1 = (Label)row.FindControl("PlblPaymentAmount1");
                    Label PlblPaymentDate1 = (Label)row.FindControl("PlblPaymentDate1");
                    Label PlblPaymentModeId2 = (Label)row.FindControl("PlblPaymentModeId2");
                    Label PlblPaymentNo2 = (Label)row.FindControl("PlblPaymentNo2");
                    Label PlblPaymentAmount2 = (Label)row.FindControl("PlblPaymentAmount2");
                    Label PlblPaymentDate2 = (Label)row.FindControl("PlblPaymentDate2");
                    Label PlblPaymentModeId3 = (Label)row.FindControl("PlblPaymentModeId3");
                    Label PlblPaymentNo3 = (Label)row.FindControl("PlblPaymentNo3");
                    Label PlblPaymentAmount3 = (Label)row.FindControl("PlblPaymentAmount3");
                    Label PlblPaymentDate3 = (Label)row.FindControl("PlblPaymentDate3");
                    Label PlblPaymentModeId4 = (Label)row.FindControl("PlblPaymentModeId4");
                    Label PlblPaymentNo4 = (Label)row.FindControl("PlblPaymentNo4");
                    Label PlblPaymentAmount4 = (Label)row.FindControl("PlblPaymentAmount4");
                    Label PlblPaymentDate4 = (Label)row.FindControl("PlblPaymentDate4");
                    Label lblTotalTcsTaxAmt = (Label)row.FindControl("lblTotalTcsTaxAmt");
                    Label PlblSSRDId = (Label)row.FindControl("PlblSSRDId");
                    Label PlblRemark = (Label)row.FindControl("PlblRemark");


                    txtDeliveryDate.Text = PlblDelivary_Date.Text;
                    ddlItemCategory.SelectedValue = lblCatId.Text;
                    ddlLocation.SelectedValue = PlblAreaID.Text;
                    DisplayMilkOrProductPanel();
                    ddlDitributor.SelectedValue = PlblSSRDId.Text;
                    txtTotalBillAmount.Text = lblTotalBillAmount.Text;
                    txtTotalGSTAmount.Text = lblTotalGSTAmount.Text;
                    txtTotalTcsTaxAmount.Text = lblTotalTcsTaxAmt.Text;
                    txtDMNo.Text = lblDMChallanNo.Text;
                    txtRemarkProd.Text = PlblRemark.Text;

                    ddlPaymentMode1.SelectedValue = PlblPaymentModeId1.Text;
                    ddlPaymentMode2.SelectedValue = PlblPaymentModeId2.Text;
                    ddlPaymentMode3.SelectedValue = PlblPaymentModeId3.Text;
                    ddlPaymentMode4.SelectedValue = PlblPaymentModeId4.Text;
                    txtPaymentNo1.Text = PlblPaymentNo1.Text;
                    txtPaymentNo2.Text = PlblPaymentNo2.Text;
                    txtPaymentNo3.Text = PlblPaymentNo3.Text;
                    txtPaymentAmt1.Text = PlblPaymentAmount1.Text;
                    txtPaymentAmt2.Text = PlblPaymentAmount2.Text;
                    txtPaymentAmt3.Text = PlblPaymentAmount3.Text;
                    txtPaymentAmt4.Text = PlblPaymentAmount4.Text;
                    if (PlblPaymentDate1.Text == "01/01/1900")
                    {
                        txtPaymentDate1.Text = "";
                    }
                    else
                    {
                        txtPaymentDate1.Text = PlblPaymentDate1.Text;
                    }
                    if (PlblPaymentDate2.Text == "01/01/1900")
                    {
                        txtPaymentDate2.Text = "";
                    }
                    else
                    {
                        txtPaymentDate2.Text = PlblPaymentDate2.Text;
                    }
                    if (PlblPaymentDate3.Text == "01/01/1900")
                    {
                        PlblPaymentDate3.Text = "";
                    }
                    else
                    {
                        txtPaymentDate3.Text = PlblPaymentDate3.Text;
                    }
                    if (PlblPaymentDate4.Text == "01/01/1900")
                    {
                        PlblPaymentDate4.Text = "";
                    }
                    else
                    {
                        txtPaymentDate4.Text = PlblPaymentDate4.Text;
                    }

                    ViewState["rowid"] = e.CommandArgument;
                    btnSave.Text = "Update";

                    foreach (GridViewRow gvRow in GridView2.Rows)
                    {
                        if (GridView2.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView2.SelectedIndex = gvRow.DataItemIndex;
                            GridView2.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotalBillAmount = (e.Row.FindControl("lblTotalBillAmount") as Label);
                Label lblTotalGSTAmount = (e.Row.FindControl("lblTotalGSTAmount") as Label);
                Label lblTotalTcsTaxAmt = (e.Row.FindControl("lblTotalTcsTaxAmt") as Label);
                Label PlblPaymentAmount1 = (e.Row.FindControl("PlblPaymentAmount1") as Label);
                Label PlblPaymentAmount2 = (e.Row.FindControl("PlblPaymentAmount2") as Label);
                Label PlblPaymentAmount3 = (e.Row.FindControl("PlblPaymentAmount3") as Label);
                Label PlblPaymentAmount4 = (e.Row.FindControl("PlblPaymentAmount4") as Label);
                Label lblTotalPaidPayment = (e.Row.FindControl("lblTotalPaidPayment") as Label);
                TBA += Convert.ToDouble(lblTotalBillAmount.Text);
                TGSTA += Convert.ToDouble(lblTotalGSTAmount.Text);
                TTCSA += Convert.ToDouble(lblTotalTcsTaxAmt.Text);
               PPA1 += Convert.ToDouble(PlblPaymentAmount1.Text);
               PPA2 += Convert.ToDouble(PlblPaymentAmount2.Text);
               PPA3 += Convert.ToDouble(PlblPaymentAmount3.Text);
               PPA4 += Convert.ToDouble(PlblPaymentAmount4.Text);
               PPTA += Convert.ToDouble(lblTotalPaidPayment.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTBillAmt = (e.Row.FindControl("lblTBillAmt") as Label);
                Label lblFGSTAmt = (e.Row.FindControl("lblFGSTAmt") as Label);
                Label lblFTcsTaxAmt = (e.Row.FindControl("lblFTcsTaxAmt") as Label);
                Label PlblFPA1 = (e.Row.FindControl("PlblFPA1") as Label);
                Label PlblFPA2 = (e.Row.FindControl("PlblFPA2") as Label);
                Label PlblFPA3 = (e.Row.FindControl("PlblFPA3") as Label);
                Label PlblFPA4 = (e.Row.FindControl("PlblFPA4") as Label);
                Label PlblFTPA = (e.Row.FindControl("FlblFTPA") as Label);
                lblTBillAmt.Text = TBA.ToString("0.00");
                lblFGSTAmt.Text = TGSTA.ToString("0.00");
                lblFTcsTaxAmt.Text = TTCSA.ToString("0.00");
                PlblFPA1.Text = PPA1.ToString("0.00");
                PlblFPA2.Text = PPA2.ToString("0.00");
                PlblFPA3.Text = PPA3.ToString("0.00");
                PlblFPA4.Text = PPA4.ToString("0.00");
                PlblFTPA.Text = PPTA.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if(ddlFilterItemCategory.SelectedValue==objdb.GetMilkCatId())
        {
            GridView1.Visible = true;
            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();

            FillGridMilk();
           
        }
        else
        {
            GridView2.Visible = true;
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView1.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();

            GetProductPaymentDetails();
        }
      
    }
    protected void txtDeliveryDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            GetMilkAmount();
        }
        else
        {
            GetProductAmount();
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayMilkOrProductPanel();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSS();
    }
    protected void ddlDitributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            GetMilkAmount();
        }
        else
        {
            GetProductAmount();
        }
    }
    protected void ddlFilterItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSSByLocationOrCategory();
    }
    protected void ddlLocationfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSSByLocationOrCategory();
    } 
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlFilterItemCategory.SelectedItem.Text + "-"+ ddlLocationfilter.SelectedItem.Text + "-" + ddlFilterDistributor.SelectedItem.Text + "-" + txtDateFilter.Text + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            if(ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
            {
                GridView1.Columns[10].Visible = false;
                GridView1.RenderControl(htmlWrite);
            }
            else
            {
                GridView2.Columns[12].Visible = false;
                GridView2.RenderControl(htmlWrite);
            }
            

            Response.Write(stringWrite.ToString());
            Response.End();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-Consolidated " + ex.Message.ToString());
        }
    }
    private void InserMilkPayment()
    {
        lblMsg.Text = string.Empty;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        decimal Balance = 0, P1 = 0, P2 = 0, P3 = 0, P4 = 0;
        DateTime deldat = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = deldat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        if (txtPaymentDate1.Text == "") { txtPaymentDate1.Text = "01/01/1900"; }
        if (txtPaymentDate2.Text == "") { txtPaymentDate2.Text = "01/01/1900"; }
        if (txtPaymentDate3.Text == "") { txtPaymentDate3.Text = "01/01/1900"; }
        if (txtPaymentDate4.Text == "") { txtPaymentDate4.Text = "01/01/1900"; }
        DateTime pd1 = DateTime.ParseExact(txtPaymentDate1.Text, "dd/MM/yyyy", culture);
        string pd11 = pd1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime pd2 = DateTime.ParseExact(txtPaymentDate2.Text, "dd/MM/yyyy", culture);
        string pd22 = pd2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime pd3 = DateTime.ParseExact(txtPaymentDate3.Text, "dd/MM/yyyy", culture);
        string pd33 = pd3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime pd4 = DateTime.ParseExact(txtPaymentDate4.Text, "dd/MM/yyyy", culture);
        string pd44 = pd4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


        if (txtPaymentAmt1.Text == "") { P1 = 0; } else { P1 = Convert.ToDecimal(txtPaymentAmt1.Text); }
        if (txtPaymentAmt2.Text == "") { P2 = 0; } else { P2 = Convert.ToDecimal(txtPaymentAmt2.Text); }
        if (txtPaymentAmt3.Text == "") { P3 = 0; } else { P3 = Convert.ToDecimal(txtPaymentAmt3.Text); }
        if (txtPaymentAmt4.Text == "") { P4 = 0; } else { P4 = Convert.ToDecimal(txtPaymentAmt4.Text); }


        Balance = (Convert.ToDecimal(txtMilkAmount.Text) - (P1 + P2 + P3 + P4));
        string[] FSSRDId = ddlDitributor.SelectedValue.Split('-');
        if (btnSave.Text == "Save")
        {

            ds7 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet"
                , new string[] {"flag","AreaId", "RouteId","DistributorId","ItemCat_id" ,
                        "Delivary_Date","Amount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                    "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","SuperStockistId"}
               , new string[] {"1" ,ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[2].ToString()
                       ,objdb.GetMilkCatId(),deliverydate,
                       txtMilkAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                   ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,FSSRDId[0].ToString()}, "dataset");
            if (ds7 != null && ds7.Tables.Count > 0)
            {
                if (ds7.Tables[0].Rows.Count > 0)
                {
                    if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());
                        ClearText();
                    }
                    else if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString());
                    }
                    else
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                    }
                }
            }
        }
        else if (btnSave.Text == "Update")
        {
            ds7 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet"
                , new string[] {"Flag","DistPaymentSheet_ID","AreaId", "RouteId","DistributorId","ItemCat_id" ,
                        "Delivary_Date","Amount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3" ,
                     "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","SuperStockistId"}
               , new string[] {"4" ,ViewState["rowid"].ToString(),ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[2].ToString()
                       ,objdb.GetMilkCatId(),deliverydate,
                       txtMilkAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                    ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,FSSRDId[0].ToString()}, "dataset");

            if (ds7 != null && ds7.Tables.Count > 0)
            {
                if (ds7.Tables[0].Rows.Count > 0)
                {
                    if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        
                        FillGridMilk();
                        ClearText();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());
                    }
                    else if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString());
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

    private void InsertProductPayment()
    {
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        decimal Balance = 0, P1 = 0, P2 = 0, P3 = 0, P4 = 0;
        DateTime deldat = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = deldat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        if (txtPaymentDate1.Text == "") { txtPaymentDate1.Text = "01/01/1900"; }
        if (txtPaymentDate2.Text == "") { txtPaymentDate2.Text = "01/01/1900"; }
        if (txtPaymentDate3.Text == "") { txtPaymentDate3.Text = "01/01/1900"; }
        if (txtPaymentDate4.Text == "") { txtPaymentDate4.Text = "01/01/1900"; }
        DateTime pd1 = DateTime.ParseExact(txtPaymentDate1.Text, "dd/MM/yyyy", culture);
        string pd11 = pd1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime pd2 = DateTime.ParseExact(txtPaymentDate2.Text, "dd/MM/yyyy", culture);
        string pd22 = pd2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime pd3 = DateTime.ParseExact(txtPaymentDate3.Text, "dd/MM/yyyy", culture);
        string pd33 = pd3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        DateTime pd4 = DateTime.ParseExact(txtPaymentDate4.Text, "dd/MM/yyyy", culture);
        string pd44 = pd4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


        if (txtPaymentAmt1.Text == "") { P1 = 0; } else { P1 = Convert.ToDecimal(txtPaymentAmt1.Text); }
        if (txtPaymentAmt2.Text == "") { P2 = 0; } else { P2 = Convert.ToDecimal(txtPaymentAmt2.Text); }
        if (txtPaymentAmt3.Text == "") { P3 = 0; } else { P3 = Convert.ToDecimal(txtPaymentAmt3.Text); }
        if (txtPaymentAmt4.Text == "") { P4 = 0; } else { P4 = Convert.ToDecimal(txtPaymentAmt4.Text); }



        Balance = (Convert.ToDecimal(txtTotalBillAmount.Text) - (P1 + P2 + P3 + P4));
        string[] FSSRDId = ddlDitributor.SelectedValue.Split('-');
        if (btnSave.Text == "Save")
        {

            ds7 = objdb.ByProcedure("USP_Trn_ProductPaymentSheet"
            , new string[] {"Flag","DMChallanNo","AreaId", "RouteId","SuperStockistId" ,"DistributorId","ItemCat_id" ,
                        "Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                    "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","TotalTcsTaxAmt"}
           , new string[] {"3" ,txtDMNo.Text.Trim(),ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[0].ToString()
               ,FSSRDId[2].ToString() ,ddlItemCategory.SelectedValue,deliverydate,
                       txtTotalBillAmount.Text.Trim(),txtTotalGSTAmount.Text.Trim(),Balance.ToString(),txtRemarkProd.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                     ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,
                     txtTotalTcsTaxAmount.Text.Trim()}, "dataset");
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
            , new string[] {"Flag","ProductPaymentSheet_ID","DMChallanNo","AreaId", "RouteId","SuperStockistId" ,"DistributorId"
                ,"ItemCat_id","Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                     "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","TotalTcsTaxAmt"}
           , new string[] {"4" ,ViewState["rowid"].ToString(),txtDMNo.Text.Trim(),ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[0].ToString()
               ,FSSRDId[2].ToString() ,ddlItemCategory.SelectedValue,deliverydate,
                       txtTotalBillAmount.Text.Trim(),txtTotalGSTAmount.Text.Trim(),Balance.ToString(),txtRemarkProd.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                      ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,
                   txtTotalTcsTaxAmount.Text.Trim()}, "dataset");
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