using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Demand_MilkRetailerPaymentSheetEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-in", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                PaymentMode();
                GetRouteIDByDistributor();
                GetRetailer();
                GetFilterRetailer();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDateFilter.Text = Date;
                txtDeliveryDate.Text = Date;
                txtDeliveryDate.Attributes.Add("readonly", "readonly");
                txtDateFilter.Attributes.Add("readonly", "readonly");
                txtPaymentDate.Attributes.Add("readonly", "readonly");
                txtPaymentDate.Text = Date;
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtMilkAmount.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    #region=========== User Defined function======================

    private void GetRouteIDByDistributor()
    {
        try
        {
            ds5 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "DistributorId" },
                       new string[] { "4", objdb.Office_ID(), objdb.createdBy() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0)
            {
                ViewState["AreaId"] = ds5.Tables[0].Rows[0]["AreaId"].ToString();
                ViewState["RouteId"] = ds5.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["AreaId"] = "0";
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    private void GetRetailer()
    {
        try
        {
            ddlRetailer.Items.Clear();
            ds1 = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "RouteId", "ItemCat_id" },
                      new string[] { "12", ViewState["RouteId"].ToString(), objdb.GetMilkCatId() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BoothName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds1.Tables[0];
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    private void GetFilterRetailer()
    {
        try
        {
            ddlFilterRetailer.Items.Clear();
            ds4 = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "RouteId", "ItemCat_id" },
                      new string[] { "12", ViewState["RouteId"].ToString(), objdb.GetMilkCatId() }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {
                ddlFilterRetailer.DataTextField = "BoothName";
                ddlFilterRetailer.DataValueField = "BoothId";
                ddlFilterRetailer.DataSource = ds4.Tables[0];
                ddlFilterRetailer.DataBind();
                ddlFilterRetailer.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlFilterRetailer.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }

    }
    private void FillGrid()
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };


            string Delivary_Date = Convert.ToDateTime(txtDateFilter.Text, culture).ToString("yyyy/MM/dd");

            ds4 = objdb.ByProcedure("USP_Trn_RetailerPaymentSheetForMilk",
                     new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id", "Delivary_Date", "DistributorId", "BoothId" },
                       new string[] { "1", objdb.Office_ID(),ViewState["RouteId"].ToString(),objdb.GetMilkCatId()
                           , Delivary_Date.ToString(),objdb.createdBy(),
                           ddlFilterRetailer.SelectedValue,  }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds4.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
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
    private void GetMilkAmount()
    {
        try
        {
            lblMsg.Text = "";
            txtMilkAmount.Text = "";
            decimal curdata = 0, predata = 0, totalmilamt = 0;
            DateTime CurrenDeliverydate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string Currentdeliverydat = CurrenDeliverydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            CurrenDeliverydate = CurrenDeliverydate.AddDays(-1);
            string predeliverydat = CurrenDeliverydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_RetailerPaymentSheetForMilk",
                     new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id", "BoothId", "DistributorId",
                                     "OrganizationId", "MShift_Id", "EShift_Id","Delivary_Date","PreDelivary_Date" },
                       new string[] { "0", objdb.Office_ID(), ViewState["RouteId"].ToString(), objdb.GetMilkCatId(),
                                    ddlRetailer.SelectedValue, objdb.createdBy(), "0"
                                    , objdb.GetShiftMorId(),objdb.GetShiftEveId(),
                                    Currentdeliverydat.ToString(), predeliverydat.ToString()}, "dataset");

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds1.Tables[0];
                    Decimal MilkAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    Decimal AdvCardAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
                    curdata = MilkAmount - AdvCardAmount;

                    dt.Dispose();
                }
                else
                {
                    curdata = 0;
                }
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds1.Tables[1];
                    Decimal MilkAmount = dt1.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    Decimal AdvCardAmount = dt1.AsEnumerable().Sum(row => row.Field<decimal>("AdvCardAmt"));
                    predata = MilkAmount - AdvCardAmount;

                    dt1.Dispose();

                }
                else
                {
                    predata = 0;
                }


                totalmilamt = curdata + predata;

                if (totalmilamt != 0)
                {
                    txtMilkAmount.Text = Convert.ToString(totalmilamt);
                }
                else
                {
                    txtMilkAmount.Text = "";
                }

              
            }
            else
            {
                txtMilkAmount.Text = "";
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
    private void PaymentMode()
    {
        try
        {
            lblMsg.Text = "";
            ds1 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlPaymentMode.DataSource = ds1.Tables[0];
                ddlPaymentMode.DataTextField = "PaymentModeName";
                ddlPaymentMode.DataValueField = "PaymentModeId";
                ddlPaymentMode.DataBind();
                ddlPaymentMode.Items.Insert(0, new ListItem("Select", "0"));
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
    private void ClearText()
    {

        ddlRetailer.SelectedIndex = 0;
        ddlPaymentMode.SelectedIndex = 0;
        txtMilkAmount.Text = "";
        txtPaymentNo.Text = "";
        txtPaymentAmt.Text = "";
        txtPaymentDate.Text = "";
        txtRemark.Text = "";
    }
    #endregion
    #region=========== changed even===========================


    #endregion===========================

    #region=========== Button Event===========================
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = string.Empty;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                Decimal Balance = (decimal.Parse(txtPaymentAmt.Text) - decimal.Parse(txtMilkAmount.Text));
                DateTime deldat = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = deldat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime paymentdate = DateTime.ParseExact(txtPaymentDate.Text, "dd/MM/yyyy", culture);
                string paymentdat = paymentdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSave.Text == "Save")
                    {


                        ds2 = objdb.ByProcedure("USP_Trn_RetailerPaymentSheetForMilk"
                        , new string[] {"flag","Office_ID","AreaId", "RouteId","DistributorId","BoothId","ItemCat_id" ,
                        "Delivary_Date","MilkAmount", "PaymentModeId","PaymentNo","PaymentAmount", "PaymentDate","Balance"
                        ,"Remark", "CreatedBy","CreatedByIP",
                         }
                       , new string[] {"2",objdb.Office_ID(),ViewState["AreaId"].ToString(),ViewState["RouteId"].ToString()
                           ,objdb.createdBy(),ddlRetailer.SelectedValue,objdb.GetMilkCatId(),deliverydate.ToString()
                           ,txtMilkAmount.Text
                           ,ddlPaymentMode.SelectedValue,txtPaymentNo.Text.Trim(),txtPaymentAmt.Text.Trim(),
                           paymentdat.ToString(),Balance.ToString(),txtRemark.Text.Trim() ,objdb.createdBy() ,IPAddress
                      }, "dataset");
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string Msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Msg.ToString());
                                    ClearText();
                                }
                                else if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                {
                                    string Msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString() + " Exits for " + ddlRetailer.SelectedItem.Text);
                                }
                                else
                                {
                                    string Msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                                }
                            }
                        }
                    }

                    if (btnSave.Text == "Update")
                    {


                        ds2 = objdb.ByProcedure("USP_Trn_RetailerPaymentSheetForMilk"
                        , new string[] {"flag","RetailerPaymentSheet_ID","Office_ID","AreaId", "RouteId","DistributorId","BoothId","ItemCat_id" ,
                        "Delivary_Date","MilkAmount", "PaymentModeId","PaymentNo","PaymentAmount", "PaymentDate","Balance"
                        ,"Remark", "CreatedBy","CreatedByIP",
                         }
                       , new string[] {"3",ViewState["paymentrowid"].ToString(),objdb.Office_ID(),ViewState["AreaId"].ToString(),ViewState["RouteId"].ToString()
                           ,objdb.createdBy(),ddlRetailer.SelectedValue,objdb.GetMilkCatId(),deliverydate.ToString()
                           ,txtMilkAmount.Text
                           ,ddlPaymentMode.SelectedValue,txtPaymentNo.Text.Trim(),txtPaymentAmt.Text.Trim(),
                           paymentdat.ToString(),Balance.ToString(),txtRemark.Text.Trim() ,objdb.createdBy() ,IPAddress
                      }, "dataset");
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string Msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Msg.ToString());
                                    ClearText();
                                    FillGrid();
                                }
                                else if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                {
                                    string Msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString() + " Exits for " + ddlRetailer.SelectedItem.Text);
                                }
                                else
                                {
                                    string Msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                                }
                            }
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

            }
            finally
            {
                if (ds2 != null) { ds2.Dispose(); }
            }
        }


    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtMilkAmount.Text = "";
        ddlRetailer.SelectedIndex=0;
        txtPaymentNo.Text = "";
        txtPaymentAmt.Text = "";
        txtRemark.Text = "";
        //txtPaymentDate.Text = "";
        btnSave.Text = "Save";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion===========================

    #region Rowcommand Event
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditRecord")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    ViewState["paymentrowid"] = e.CommandArgument.ToString();
                    Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
                    Label lblBoothId = (Label)row.FindControl("lblBoothId");
                    Label lblMilkAmount = (Label)row.FindControl("lblMilkAmount");
                    Label lblPaymentModeId = (Label)row.FindControl("lblPaymentModeId");
                    Label lblPaymentNo = (Label)row.FindControl("lblPaymentNo");
                    Label lblPaymentAmount = (Label)row.FindControl("lblPaymentAmount");
                    Label lblPaymentDate = (Label)row.FindControl("lblPaymentDate");
                    Label lblRemark = (Label)row.FindControl("lblRemark");

                    btnSave.Text = "Update";

                    txtDeliveryDate.Text = lblDelivary_Date.Text;
                    txtMilkAmount.Text = lblMilkAmount.Text;
                    ddlRetailer.SelectedValue = lblBoothId.Text;                   
                    ddlPaymentMode.SelectedValue = lblPaymentModeId.Text;
                    txtPaymentNo.Text = lblPaymentNo.Text;
                    txtPaymentAmt.Text = lblPaymentAmount.Text;
                    txtPaymentDate.Text = lblPaymentDate.Text;
                    txtRemark.Text = lblRemark.Text;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    #endregion
    protected void ddlRetailer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRetailer.SelectedValue != "0")
        {
            GetMilkAmount();
        }
    }
}