using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class mis_DemandSupply_BulkMilksale_PaymentEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds6, ds4, ds5, ds7, ds8, dsInst = new DataSet();
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
               
                GetFilterCategory();
               
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
                HideshowUnionOrThirdParty();
                HideshowUnionOrThirdPartyFilter();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void HideshowUnionOrThirdParty()
    {
        try
        {
            ddlThirdparty.ClearSelection();
            ddlUnion.ClearSelection();
            ddlMDP.ClearSelection();
            lblMsg.Text = "";
            if (rbtnTransferType.SelectedIndex == 0)
            {
                union.Visible = true;
                thirdparty.Visible = false;
                MDP.Visible = false;
                FillUnion();

            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                MDP.Visible = false;
                union.Visible = false;
                thirdparty.Visible = true;
                FillThirdparty();
            }
            else
            {
                MDP.Visible = true;
                union.Visible = false;
                thirdparty.Visible = false;
                FillMDP();
            }
            // FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3 : ", ex.Message.ToString());
        }
    }
    protected void rbtnsaleto_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideshowUnionOrThirdParty();
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
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            pnlMilk.Visible = true;
            pnlProduct.Visible = false;
          
        }
        else
        {

            pnlMilk.Visible = false;
            pnlProduct.Visible = true;
            
        }
    }
   
    protected void FillUnion()
    {
        try
        {
            lblMsg.Text = "";
            //ds = objdb.ByProcedure("SpAdminOffice",
            //     new string[] { "flag" },
            //     new string[] { "12" }, "dataset");

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlDS.DataSource = ds.Tables[0];
            //    ddlDS.DataTextField = "Office_Name";
            //    ddlDS.DataValueField = "Office_ID";
            //    ddlDS.DataBind();
            //    ddlDS.Items.Insert(0, new ListItem("Select", "0"));
            //    ddlDS.SelectedValue = objdb.Office_ID();
            //    ddlDS.Enabled = false;


            //}
            //else
            //{
            //    ddlDS.Items.Clear();
            //    ddlDS.Items.Insert(0, new ListItem("Select", "0"));
            //    ddlDS.DataBind();
            //}
            //if (ds != null) { ds.Dispose(); }


            DataSet ds1 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "3", objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlUnion.DataSource = ds1.Tables[0];
                ddlUnion.DataTextField = "Office_Name";
                ddlUnion.DataValueField = "Office_ID";
                ddlUnion.DataBind();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlUnion.Items.Clear();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnion.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error U : ", ex.Message.ToString());
        }
    }
    protected void FillThirdparty()
    {
        try
        {
            lblMsg.Text = "";
            if (ds1 != null) { ds1.Dispose(); }
            DataSet ds2 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "4", objdb.Office_ID() }, "dataset");

            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlThirdparty.DataSource = ds2.Tables[0];
                ddlThirdparty.DataTextField = "ThirdPartyUnion_Name";
                ddlThirdparty.DataValueField = "ThirdPartyUnion_Id";
                ddlThirdparty.DataBind();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlThirdparty.Items.Clear();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));
                ddlThirdparty.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error TP : ", ex.Message.ToString());
        }

    }
    protected void FillMDP()
    {
        try
        {
            lblMsg.Text = "";
            if (ds2 != null) { ds2.Dispose(); }
            DataSet ds3 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "5", objdb.Office_ID() }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlMDP.DataSource = ds3.Tables[0];
                ddlMDP.DataTextField = "Office_Name";
                ddlMDP.DataValueField = "Office_ID";
                ddlMDP.DataBind();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlMDP.Items.Clear();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));
                ddlMDP.DataBind();
            }
            if (ds3 != null) { ds3.Dispose(); }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error MDP : ", ex.Message.ToString());
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
            ds5 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet_Insert", new string[] { "flag" }, new string[] { "6" }, "dataset");
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
    private void FillGridMilkProduct()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime dateSearch = DateTime.ParseExact(txtDateFilter.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string SaleToOffice_Id = "0";

            if (rbtnsaletofilter.SelectedValue == "1")
            {
                SaleToOffice_Id = ddlUnionFilter.SelectedValue;
            }
            else if (rbtnsaletofilter.SelectedValue == "2")
            {
                SaleToOffice_Id = ddlThirdpartyFilter.SelectedValue;
            }
            else if (rbtnsaletofilter.SelectedValue == "3")
            {
                SaleToOffice_Id = ddlMDPFilter.SelectedValue;
            }
            //ds4 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet_Insert",
            //         new string[] { "flag", "Office_ID", "Delivary_Date", "AreaId", "RouteId" },
            //           new string[] { "2", objdb.Office_ID(), dateSearch.ToString(), ddlLocationfilter.SelectedValue, disid.ToString() }, "dataset");
            ds4 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet",
                     new string[] { "flag", "Office_ID", "Delivary_Date", "SaleToOffice_Id", "MilkTrasferType", "ItemCat_id" },
                       new string[] { "2", objdb.Office_ID(), dateSearch.ToString(), SaleToOffice_Id.ToString(), rbtnTransferType.SelectedValue,ddlFilterItemCategory.SelectedValue }, "dataset");
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
            string SaleToOffice_Id = "0";

            if (rbtnTransferType.SelectedValue == "1")
            {
                SaleToOffice_Id = ddlUnion.SelectedValue;
            }
            else if (rbtnTransferType.SelectedValue == "2")
            {
                SaleToOffice_Id = ddlThirdparty.SelectedValue;
            }
            else if (rbtnTransferType.SelectedValue == "3")
            {
                SaleToOffice_Id = ddlMDP.SelectedValue;
            }

            ds1 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet",
                     new string[] { "Flag", "Delivary_Date", "Office_ID", "SaleToOffice_Id", "MilkTrasferType", "ItemCat_id" },
                     new string[] { "2", datSearch, objdb.Office_ID(), SaleToOffice_Id.ToString(), rbtnTransferType.SelectedValue,ddlFilterItemCategory.SelectedValue  }, "dataset");

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
   
    private void GetMilkProductAmountUnion()
    {
        try
        {
            lblMsg.Text = "";
            txtMilkAmount.Text = "";
            DateTime deldate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string delidate = deldate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string OI = objdb.Office_ID();
            ds1 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
           new string[] { "Flag", "Delivary_Date", "ItemCat_id", "SaleToOffice_Id", "MilkTrasferType", "Office_ID" },
           new string[] { "2", delidate, ddlItemCategory.SelectedValue, ddlUnion.SelectedValue, rbtnTransferType.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ddlItemCategory.SelectedValue == "3")
                {
                    txtMilkAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
                }
                else if (ddlItemCategory.SelectedValue == "2")
                {
                    txtTotalBillAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
                    txtTotalGSTAmount.Text = (Convert.ToDecimal(ds1.Tables[0].Rows[0]["GST"])).ToString("0.00");
                    txtTotalTcsTaxAmount.Text = (Convert.ToDecimal(ds1.Tables[0].Rows[0]["TCSTaxAmt"])).ToString("0.00");
                }
            }
            else
            {
                if (ddlItemCategory.SelectedValue == "3")
                {
                    txtMilkAmount.Text = "0";
                }
                else if (ddlItemCategory.SelectedValue == "2")
                {
                    txtTotalBillAmount.Text = "0";
                    txtTotalGSTAmount.Text = "0";
                    txtTotalTcsTaxAmount.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    private void GetMilkProductAmountThirdParty()
    {
        try
        {
            lblMsg.Text = "";
            txtMilkAmount.Text = "";
            DateTime deldate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string delidate = deldate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
           

            ds1 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
           new string[] { "Flag", "Delivary_Date", "ItemCat_id", "SaleToOffice_Id", "MilkTrasferType", "Office_ID" },
           new string[] { "2", delidate, ddlItemCategory.SelectedValue, ddlThirdparty.SelectedValue, rbtnTransferType.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ddlItemCategory.SelectedValue == "3")
                {
                    txtMilkAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
                }
                else if (ddlItemCategory.SelectedValue == "2")
                {
                    txtTotalBillAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
                    txtTotalGSTAmount.Text = (Convert.ToDecimal(ds1.Tables[0].Rows[0]["GST"])).ToString("0.00");
                    txtTotalTcsTaxAmount.Text = (Convert.ToDecimal(ds1.Tables[0].Rows[0]["TCSTaxAmt"])).ToString("0.00");
                }
            }
            else
            {
                if (ddlItemCategory.SelectedValue == "3")
                {
                    txtMilkAmount.Text = "0";
                }
                else if (ddlItemCategory.SelectedValue == "2")
                {
                    txtTotalBillAmount.Text = "0";
                    txtTotalGSTAmount.Text = "0";
                    txtTotalTcsTaxAmount.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
    private void GetMilkProductAmountMDP()
    {
        try
        {
            lblMsg.Text = "";
            txtMilkAmount.Text = "";
            DateTime deldate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string delidate = deldate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            

            ds1 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
           new string[] { "Flag", "Delivary_Date", "ItemCat_id", "SaleToOffice_Id", "MilkTrasferType", "Office_ID" },
           new string[] { "2", delidate, ddlItemCategory.SelectedValue, ddlMDP.SelectedValue, rbtnTransferType.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ddlItemCategory.SelectedValue == "3")
                {
                    txtMilkAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
                }
                else if (ddlItemCategory.SelectedValue == "2")
                {
                    txtTotalBillAmount.Text = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TotalPaybleAmount"]).ToString();
                    txtTotalGSTAmount.Text = (Convert.ToDecimal(ds1.Tables[0].Rows[0]["GST"])).ToString("0.00");
                    txtTotalTcsTaxAmount.Text = (Convert.ToDecimal(ds1.Tables[0].Rows[0]["TCSTaxAmt"])).ToString("0.00");
                }
            }
            else
            {
                if (ddlItemCategory.SelectedValue == "3")
                {
                    txtMilkAmount.Text = "0";
                }
                else if (ddlItemCategory.SelectedValue == "2")
                {
                    txtTotalBillAmount.Text = "0";
                    txtTotalGSTAmount.Text = "0";
                    txtTotalTcsTaxAmount.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
    }
   
    private void ClearText()
    {

        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            pnlMilk.Visible = true;
            txtMilkAmount.Text = "";
            txtRemark.Text = "";
            GridView1.SelectedIndex = -1;
        }
        else
        {
            pnlProduct.Visible = true;
            //txtDMNo.Text = string.Empty;
            txtTotalBillAmount.Text = string.Empty;
            txtTotalGSTAmount.Text = string.Empty;
            txtTotalTcsTaxAmount.Text = string.Empty;
            GridView1.SelectedIndex = -1;
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

                    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
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
            GridView1.SelectedIndex = -1;
            // txtDMNo.Text = string.Empty;
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
        ddlThirdparty.SelectedIndex = 0;
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
                  
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
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
                    
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblSaleToOffice_Id = (Label)row.FindControl("lblSaleToOffice_Id");
                    Label lblMilkTrasferType = (Label)row.FindControl("lblMilkTrasferType");
                    Label lblSaletoOfficeName = (Label)row.FindControl("lblSaletoOfficeName");
                    Label lblTotalGSTAmount = (Label)row.FindControl("lblTotalGSTAmount");
                    Label lblTotalTcsTaxAmt = (Label)row.FindControl("lblTotalTcsTaxAmt");
                    rbtnTransferType.SelectedValue = lblMilkTrasferType.Text;
                    HideshowUnionOrThirdParty();
                    if (rbtnTransferType.SelectedValue=="1")
                    {
                        ddlUnion.SelectedValue = lblSaleToOffice_Id.Text;
                    }
                    else if (rbtnTransferType.SelectedValue == "2")
                    {
                        ddlThirdparty.SelectedValue = lblSaleToOffice_Id.Text;
                    }
                    else if (rbtnTransferType.SelectedValue == "3")
                    {
                        ddlMDP.SelectedValue = lblSaleToOffice_Id.Text;
                    }

                    
                    txtDeliveryDate.Text = lblDelivary_Date.Text;
                    ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    DisplayMilkOrProductPanel();
                    if (ddlItemCategory.SelectedValue == "3")
                    {
                        pnlMilk.Visible = true;
                        txtMilkAmount.Text = lblAmount.Text;
                       
                    }
                    else
                    {
                       
                        txtTotalBillAmount.Text = lblAmount.Text;
                        txtTotalGSTAmount.Text = lblTotalGSTAmount.Text;
                        txtTotalTcsTaxAmount.Text = lblTotalTcsTaxAmt.Text;
                    }
                 
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

   
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
      
            GridView1.Visible = true;
            GridView1.DataSource = null;
            GridView1.DataBind();

          

            FillGridMilkProduct();

      
    }
    protected void txtDeliveryDate_TextChanged(object sender, EventArgs e)
    {
        

        if (rbtnTransferType.SelectedValue == "1")
        {
            GetMilkProductAmountUnion();
        }
        else if (rbtnTransferType.SelectedValue == "2")
        {
            GetMilkProductAmountThirdParty();
        }
        else if (rbtnTransferType.SelectedValue == "3")
        {
            GetMilkProductAmountMDP();
        }
        
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayMilkOrProductPanel();
    }
   
    protected void ddlUnion_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        if (rbtnTransferType.SelectedValue == "1")
        {
            GetMilkProductAmountUnion();
        }
        else if (rbtnTransferType.SelectedValue == "2")
        {
            GetMilkProductAmountThirdParty();
        }
        else if (rbtnTransferType.SelectedValue == "3")
        {
            GetMilkProductAmountMDP();
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
            //Response.AddHeader("content-disposition", "attachment; filename=" + ddlFilterItemCategory.SelectedItem.Text + "-" + ddlLocationfilter.SelectedItem.Text + "-" + ddlFilterInstitute.SelectedItem.Text + "-" + txtDateFilter.Text + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
            {
                GridView1.Columns[10].Visible = false;
                GridView1.RenderControl(htmlWrite);
            }
            //else
            //{
            //    GridView1.Columns[12].Visible = false;
            //    GridView1.RenderControl(htmlWrite);
            //}


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
      
        string SaleToOffice_Id = "0";
        if (rbtnTransferType.SelectedValue == "1")
        {
            SaleToOffice_Id = ddlUnion.SelectedValue;
        }
        else if (rbtnTransferType.SelectedValue == "2")
        {
            SaleToOffice_Id = ddlThirdparty.SelectedValue;
        }
        else if (rbtnTransferType.SelectedValue == "3")
        {
            SaleToOffice_Id = ddlMDP.SelectedValue;
        }
        if (btnSave.Text == "Save")
        {
            ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet_Insert"
                , new string[] {"flag","MilkTrasferType" ,"SaleToOffice_Id","ItemCat_id" ,
                        "Delivary_Date","Amount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                    "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4"}
               , new string[] {"1",rbtnTransferType.SelectedValue,SaleToOffice_Id.ToString()
                       ,objdb.GetMilkCatId(),deliverydate,
                       txtMilkAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                   ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44}, "dataset");

            //ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet_Insert"
            //    , new string[] {"flag","AreaId", "RouteId","OrganizationId","ItemCat_id" ,
            //            "Delivary_Date","Amount","Balance","Remark" ,"Office_ID"
            //            , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
            //            , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
            //            "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
            //        "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","SuperStockistId","DistributorId"}
            //   , new string[] {"1" ,ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[0].ToString()
            //           ,objdb.GetMilkCatId(),deliverydate,
            //           txtMilkAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
            //           ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
            //           ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
            //           ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
            //          ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
            //       ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,"0","0"}, "dataset");
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
            ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet_Insert"
                , new string[] {"Flag","BulkMilkPaymentSheet_ID","MilkTrasferType" ,"SaleToOffice_Id","ItemCat_id" ,
                        "Delivary_Date","Amount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3" ,
                     "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4"}
               , new string[] {"4" ,ViewState["rowid"].ToString(),rbtnTransferType.SelectedValue,SaleToOffice_Id.ToString()
                       ,objdb.GetMilkCatId(),deliverydate,
                       txtMilkAmount.Text.Trim(),Balance.ToString(),txtRemark.Text.Trim()
                       ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
                       ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
                       ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
                      ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
                    ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44}, "dataset");

            if (ds7 != null && ds7.Tables.Count > 0)
            {
                if (ds7.Tables[0].Rows.Count > 0)
                {
                    if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        FillGridMilkProduct();
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
      
        string SaleToOffice_Id = "0";
        if (rbtnTransferType.SelectedValue == "1")
        {
            SaleToOffice_Id = ddlUnion.SelectedValue;
        }
        else if (rbtnTransferType.SelectedValue == "2")
        {
            SaleToOffice_Id = ddlThirdparty.SelectedValue;
        }
        else if (rbtnTransferType.SelectedValue == "3")
        {
            SaleToOffice_Id = ddlMDP.SelectedValue;
        }

        if (btnSave.Text == "Save")
        {

            // ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet"
            // , new string[] {"Flag","DMChallanNo","AreaId", "RouteId","SuperStockistId" ,"DistributorId","ItemCat_id" ,
            //             "Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
            //             , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
            //             , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
            //             "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
            //         "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","TotalTcsTaxAmt"}
            //, new string[] {"3" ,txtDMNo.Text.Trim(),ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[0].ToString()
            //    ,FSSRDId[2].ToString() ,ddlItemCategory.SelectedValue,deliverydate,
            //            txtTotalBillAmount.Text.Trim(),txtTotalGSTAmount.Text.Trim(),Balance.ToString(),txtRemarkProd.Text.Trim()
            //            ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
            //            ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
            //            ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
            //           ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
            //          ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,
            //          txtTotalTcsTaxAmount.Text.Trim()}, "dataset");
            ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet"
            , new string[] {"Flag","MilkTrasferType" ,"SaleToOffice_Id","ItemCat_id" ,
                        "Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                    "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","TotalTcsTaxAmt"}
           , new string[] {"3" ,rbtnTransferType.SelectedValue
               ,SaleToOffice_Id.ToString() ,ddlItemCategory.SelectedValue,deliverydate,
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
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString() + " Exits for " + ddlInstitute.SelectedItem.Text);
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
            // ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet"
            // , new string[] {"Flag","ProductPaymentSheet_ID","DMChallanNo","AreaId", "RouteId","SuperStockistId" ,"DistributorId"
            //     ,"ItemCat_id","Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
            //             , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
            //             , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
            //             "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
            //          "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","TotalTcsTaxAmt"}
            //, new string[] {"4" ,ViewState["rowid"].ToString(),txtDMNo.Text.Trim(),ddlLocation.SelectedValue,FSSRDId[1].ToString(),FSSRDId[0].ToString()
            //    ,FSSRDId[2].ToString() ,ddlItemCategory.SelectedValue,deliverydate,
            //            txtTotalBillAmount.Text.Trim(),txtTotalGSTAmount.Text.Trim(),Balance.ToString(),txtRemarkProd.Text.Trim()
            //            ,objdb.Office_ID(),objdb.createdBy() ,IPAddress
            //            ,ddlPaymentMode1.SelectedValue,txtPaymentNo1.Text.Trim(),P1.ToString(),pd11,
            //            ddlPaymentMode2.SelectedValue,txtPaymentNo2.Text.Trim(),P2.ToString(),pd22,
            //           ddlPaymentMode3.SelectedValue,txtPaymentNo3.Text.Trim(),P3.ToString(),pd33,
            //           ddlPaymentMode4.SelectedValue,txtPaymentNo4.Text.Trim(),P4.ToString(),pd44,
            //        txtTotalTcsTaxAmount.Text.Trim()}, "dataset");
            ds7 = objdb.ByProcedure("USP_Trn_BulkMilkPaymentSheet"
           , new string[] {"Flag","BulkMilkPaymentSheet_ID","MilkTrasferType" ,"SaleToOffice_Id"
                ,"ItemCat_id","Delivary_Date","TotalBillAmount","TotalGSTAmount","Balance","Remark" ,"Office_ID"
                        , "CreatedBy","CreatedByIP","PaymentModeId1","PaymentNo1","PaymentAmount1", "PaymentDate1"
                        , "PaymentModeId2",  "PaymentNo2",  "PaymentAmount2","PaymentDate2",
                        "PaymentModeId3","PaymentNo3", "PaymentAmount3","PaymentDate3",
                     "PaymentModeId4","PaymentNo4", "PaymentAmount4","PaymentDate4","TotalTcsTaxAmt"}
          , new string[] {"4" ,ViewState["rowid"].ToString(),rbtnTransferType.SelectedValue
               ,SaleToOffice_Id.ToString() ,ddlItemCategory.SelectedValue,deliverydate,
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
                        FillGridMilkProduct();
                        ClearText();
                    }
                    else if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string Msg = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString() + " Exits for " + ddlInstitute.SelectedItem.Text);
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
    protected void HideshowUnionOrThirdPartyFilter()
    {
        try
        {
            ddlThirdparty.ClearSelection();
            ddlUnion.ClearSelection();
            ddlMDP.ClearSelection();
            lblMsg.Text = "";
            if (rbtnsaletofilter.SelectedIndex == 0)
            {
                DivUnion.Visible = true;
                DivThirdParty.Visible = false;
                DivMDP.Visible = false;
                FillUnionFilter();

            }
            else if (rbtnsaletofilter.SelectedIndex == 1)
            {
                DivMDP.Visible = false;
                DivUnion.Visible = false;
                DivThirdParty.Visible = true;
                FillThirdpartyFilter();
            }
            else
            {
                DivMDP.Visible = true;
                DivUnion.Visible = false;
                DivThirdParty.Visible = false;
                FillMDPFilter();
            }
            // FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3 : ", ex.Message.ToString());
        }
    }
    protected void rbtnsaletofilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideshowUnionOrThirdPartyFilter();
    }
    protected void FillUnionFilter()
    {
        try
        {
            lblMsg.Text = "";

            DataSet ds1 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "3", objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlUnionFilter.DataSource = ds1.Tables[0];
                ddlUnionFilter.DataTextField = "Office_Name";
                ddlUnionFilter.DataValueField = "Office_ID";
                ddlUnionFilter.DataBind();
                ddlUnionFilter.Items.Insert(0, new ListItem("All", "0"));



            }
            else
            {
                ddlUnionFilter.Items.Clear();
                ddlUnionFilter.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnionFilter.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error U : ", ex.Message.ToString());
        }
    }
    protected void FillThirdpartyFilter()
    {
        try
        {
            lblMsg.Text = "";
            if (ds1 != null) { ds1.Dispose(); }
            DataSet ds2 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "4", objdb.Office_ID() }, "dataset");

            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlThirdpartyFilter.DataSource = ds2.Tables[0];
                ddlThirdpartyFilter.DataTextField = "ThirdPartyUnion_Name";
                ddlThirdpartyFilter.DataValueField = "ThirdPartyUnion_Id";
                ddlThirdpartyFilter.DataBind();
                ddlThirdpartyFilter.Items.Insert(0, new ListItem("All", "0"));



            }
            else
            {
                ddlThirdpartyFilter.Items.Clear();
                ddlThirdpartyFilter.Items.Insert(0, new ListItem("Select", "0"));
                ddlThirdpartyFilter.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error TP : ", ex.Message.ToString());
        }

    }
    protected void FillMDPFilter()
    {
        try
        {
            lblMsg.Text = "";
            if (ds2 != null) { ds2.Dispose(); }
            DataSet ds3 = objdb.ByProcedure("USP_BulkMilkSaleInvoiceDetail",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "5", objdb.Office_ID() }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlMDPFilter.DataSource = ds3.Tables[0];
                ddlMDPFilter.DataTextField = "Office_Name";
                ddlMDPFilter.DataValueField = "Office_ID";
                ddlMDPFilter.DataBind();
                ddlMDPFilter.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlMDPFilter.Items.Clear();
                ddlMDPFilter.Items.Insert(0, new ListItem("Select", "0"));
                ddlMDPFilter.DataBind();
            }
            if (ds3 != null) { ds3.Dispose(); }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error MDP : ", ex.Message.ToString());
        }

    }

}