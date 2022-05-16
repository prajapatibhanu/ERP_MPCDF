using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;

public partial class mis_DemandSupply_MilkDistributionVehicleChallan : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds9, ds5, ds6, ds8 = new DataSet();
    int totalmilkqty = 0, totalcrate = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetShift();
                GetVendorType();
                // GetVehicleNo();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtDate.Attributes.Add("readonly", "true");
                if (Request.QueryString["DSA"] != null && Request.QueryString["DSR"] != null && Request.QueryString["DSD"] != null && Request.QueryString["DSS"] != null && Request.QueryString["DSDMS"] != null && Request.QueryString["DSMDID"] != null)
                {
                    GetSupplyDetailsByViewChallanList();
                }
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

    private void CClear()
    {

        pnldata.Visible = false;
        ddlVehicleNo.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    private void InvisibeDistOrSS()
    {
        if (ddlLocation.SelectedValue != "0")
        {
            pnldistss.Visible = true;
            ddlDitributor.SelectedIndex = 0;
        }
        else
        {
            ddlDitributor.SelectedIndex = 0;
            pnldistss.Visible = false;
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

    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, "3" }, "dataset");
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
    private void GetDisOrSSByRouteID()
    {
        try
        {
            if (ddlRoute.SelectedValue != "0")
            {

                ds6 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                       new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue, objdb.GetMilkCatId() }, "dataset");

                if (ds6 != null && ds6.Tables[0].Rows.Count > 0)
                {
                    ddlDitributor.DataTextField = "DTName";
                    ddlDitributor.DataValueField = "DistributorId";
                    ddlDitributor.DataSource = ds6.Tables[0];
                    ddlDitributor.DataBind();
                }
                else
                {
                    ddlDitributor.Items.Insert(0, new ListItem("No Record Found", "0"));
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist./SS ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }

    private void GetDisOrSSByRouteIDRedirected()
    {
        try
        {
            if (ddlRoute.SelectedValue != "0")
            {
                ds5 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                       new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue, objdb.GetMilkCatId() }, "dataset");
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    ddlDitributor.DataTextField = "DTName";
                    ddlDitributor.DataValueField = "DistributorId";
                    ddlDitributor.DataSource = ds5.Tables[0];
                    ddlDitributor.DataBind();

                }
                else
                {
                    ddlDitributor.Items.Insert(0, new ListItem("No Record Found", "0"));
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist./SS ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    private void GetLimitOrBalanceOfDistOrSS()
    {
        try
        {
            ds4 = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet",
                   new string[] { "flag", "Office_ID", "RouteId", "DistributorId" },
                     new string[] { "7", objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue }, "dataset");


            if (ds4.Tables[0].Rows.Count > 0)
            {
                decimal maxlimit = Convert.ToDecimal(ds4.Tables[0].Rows[0]["Limit"]);
                decimal totalBalance = Convert.ToDecimal(ds4.Tables[1].Rows[0]["Balance"]);

                if (totalBalance.ToString().Contains("-"))
                {
                    decimal tb = Math.Abs(totalBalance);
                    if (tb <= maxlimit)
                    {
                        InsertVehicleChallan();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Max Supply Limit exceed of " + ddlDitributor.SelectedItem.Text);
                        return;
                        tb = 0;
                    }
                }
                else if (!string.IsNullOrEmpty(totalBalance.ToString()))
                {
                    InsertVehicleChallan();
                }


            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "First Set Max Supply Limit of Distributor/Superstockist.");
                return;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist./SS ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }

    }

    private void GetSupplyDetailsByViewChallanList()
    {
        try
        {

            lblMsg.Text = "";
            string redirectedAreaID = objdb.Decrypt(Request.QueryString["DSA"].ToString());
            string redirectedRouteId = objdb.Decrypt(Request.QueryString["DSR"].ToString());
            string redirectedDeliverDate = objdb.Decrypt(Request.QueryString["DSD"].ToString());
            string redirectedDelivaryShift_id = objdb.Decrypt(Request.QueryString["DSS"].ToString());
            string redirectedprodstatus = objdb.Decrypt(Request.QueryString["DSDMS"].ToString());
            string redirectedDMId = objdb.Decrypt(Request.QueryString["DSMDID"].ToString());

            txtDate.Text = redirectedDeliverDate;
            ddlShift.SelectedValue = redirectedDelivaryShift_id;
            ddlLocation.SelectedValue = redirectedAreaID;
            InvisibeDistOrSS();
            GetRoute();
            ddlRoute.SelectedValue = redirectedRouteId;
            GetDisOrSSByRouteID();
            //GetDisOrSSByRouteIDRedirected();
            if (redirectedprodstatus == "0")
            {
                ddlDMType.SelectedValue = "0";
                GetSupplyDetails();
            }
            else
            {
                ddlDMType.SelectedValue = "1";
                ddlDMType_SelectedIndexChanged(this, EventArgs.Empty);
                ddlOrderNo.SelectedValue = redirectedDMId;
                GetSupplyDetailsByDemandId();
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
    private void GetOrderNo()
    {
        try
        {
            if (txtDate.Text != "" && ddlShift.SelectedValue != "0" && ddlRoute.SelectedValue != "0")
            {
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                      new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "Office_ID", "ProductDMStatus" },
                        new string[] { "2", odat, ddlShift.SelectedValue, objdb.GetMilkCatId(), ddlRoute.SelectedValue, objdb.Office_ID(), ddlDMType.SelectedValue }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    ddlOrderNo.DataTextField = "OrderId";
                    ddlOrderNo.DataValueField = "MilkOrProductDemandId";
                    ddlOrderNo.DataSource = ds5.Tables[0];
                    ddlOrderNo.DataBind();
                    ddlOrderNo.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlOrderNo.Items.Clear();
                    ddlOrderNo.Items.Insert(0, new ListItem("No Record Found", "0"));
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist./SS ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    private void GetSupplyDetails()
    {
        try
        {
            lblMsg.Text = "";

            DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
                     new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "Office_ID" },
                     new string[] { "1", deliverydate, ddlShift.SelectedValue, objdb.GetMilkCatId(), ddlRoute.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                pnldata.Visible = true;
                pnlvehicledetail.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                GetVehicleNo();
            }
            else
            {
                pnldata.Visible = false;
                pnlvehicledetail.Visible = false;
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
    private void GetSupplyDetailsByDemandId()
    {
        try
        {
            lblMsg.Text = "";

            DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
                     new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "Office_ID", "MilkOrProductDemandId" },
                     new string[] { "5", deliverydate, ddlShift.SelectedValue, objdb.GetMilkCatId(), ddlRoute.SelectedValue, objdb.Office_ID(), ddlOrderNo.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                pnldata.Visible = true;
                pnlvehicledetail.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                GetVehicleNo();
            }
            else
            {
                pnldata.Visible = false;
                pnlvehicledetail.Visible = false;
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

    private void InsertVehicleChallan()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                DateTime intime = Convert.ToDateTime(txtInTime.Text.Trim());
                string it = intime.ToString("hh:mm tt");
                DateTime outtime = Convert.ToDateTime(txtOutTime.Text.Trim());
                string ot = outtime.ToString("hh:mm tt");
                string dat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (btnSubmit.Text == "Save")
                {
                    lblMsg.Text = "";
                    DataTable dtInsertChild = new DataTable();
                    DataRow drIC;

                    dtInsertChild.Columns.Add("Item_id", typeof(int));
                    dtInsertChild.Columns.Add("TotalSupplyQty", typeof(int));
                    dtInsertChild.Columns.Add("IssueCrate", typeof(int));
                    dtInsertChild.Columns.Add("ExtraPacket", typeof(int));
                    drIC = dtInsertChild.NewRow();
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        Label lblItem_id = (Label)row.FindControl("lblItem_id");
                        Label lblMilkQty = (Label)row.FindControl("lblMilkQty");
                        Label lblCrate = (Label)row.FindControl("lblCrate");
                        Label lblExtrapacket = (Label)row.FindControl("lblExtrapacket");
                        drIC[0] = lblItem_id.Text;
                        drIC[1] = lblMilkQty.Text;
                        drIC[2] = lblCrate.Text;
                        drIC[3] = lblExtrapacket.Text;

                        dtInsertChild.Rows.Add(drIC.ItemArray);
                    }
                    if (dtInsertChild.Rows.Count > 0 && ddlVehicleNo.SelectedValue != "0")
                    {
                        Int64 milkdemandid = 0;
                        if (ddlDMType.SelectedValue == "0")
                        {
                            milkdemandid = 0;
                        }
                        else
                        {
                            milkdemandid = Convert.ToInt64(ddlOrderNo.SelectedValue);
                        }
                        ds2 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan_Insert",
                                             new string[] { "Flag","AreaId", "RouteId", "ItemCat_id", "DeliveryShift_id", "Delivary_Date"
                                                          , "VehicleIn_Time","VehicleOut_Time","VehicleMilkOrProduct_ID","SupervisorName",
                                                          "TotalIssueCrate","CreatedBy", "CreatedByIP","Office_ID","DistributorId","MilkCurDMCrateIsueStatus","MilkOrProductDemandId","ProductDMStatus" },
                                             new string[] { "1",ddlLocation.SelectedValue, ddlRoute.SelectedValue, "3", ddlShift.SelectedValue, dat, 
                                                 it,ot,ddlVehicleNo.SelectedValue,txtSupervisorName.Text.Trim(),
                                                 ViewState["TotalIssueCrate"].ToString(),objdb.createdBy(), IPAddress,objdb.Office_ID(),ddlDitributor.SelectedValue,ddlCrateStatus.SelectedValue,milkdemandid.ToString(),ddlDMType.SelectedValue }, "type_VehicleDispDelivChallanChild", dtInsertChild, "TableSave");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            CClear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            dtInsertChild.Dispose();
                            GetChallanDetails(ds2.Tables[0].Rows[0]["VehicleDispId"].ToString());
                        }
                        else
                        {
                            string msg = ds2.Tables[0].Rows[0]["Msg"].ToString();
                            if (msg == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Record " + msg + " exists for Date : " + txtDate.Text + ", Shift : " + ddlShift.SelectedItem.Text + " and Route : " + ddlRoute.SelectedItem.Text);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  5:" + msg);
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Please enter valid MilkQty and select Vehicle. ");
                        return;
                    }



                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Date");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6:", ex.Message.ToString());
        }

        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }

    }
    #endregion========================================================
    #region=============== changed event for controls =================
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetRoute();
            InvisibeDistOrSS();
        }
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDisOrSSByRouteID();
        pnldata.Visible = false;
        pnldata.Visible = false;
        pnlvehicledetail.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    #endregion============ end of changed event for controls===========
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ddlDMType.SelectedValue == "0")
            {
                decimal finalamount = 0, tcstax = 0, PaybleAmtWithTcsTax = 0, tcstaxAmt = 0,Totalpaybleamount=0;
                DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "RouteId", "DistributorId", "OrganizationId", "AreaId" },
                  new string[] { "1", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue, "0", ddlLocation.SelectedValue }, "dataset");
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            finalamount = finalamount + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds2.Tables[0].Rows[i]["AdvCardAmt"]));
                        }
                        lblFinalAmount.Text = finalamount.ToString();
                        GetTcsTax();
                        tcstax = Convert.ToDecimal(ViewState["Tval"].ToString());
                        tcstaxAmt = ((tcstax * finalamount) / 100);
                        PaybleAmtWithTcsTax = tcstaxAmt + finalamount;
                        // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
                        lblTcsTax.Text = ViewState["Tval"].ToString();
                        lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.000");
                        lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
                    }
                }
                ds1 = objdb.ByProcedure("USP_Trn_Paymentandsecuritycompare",
                     new string[] { "Flag", "RouteId", "Office_ID", "DistributorId", "Delivary_Date" },
                     new string[] { "2", ddlRoute.SelectedValue, objdb.Office_ID(), ddlDitributor.SelectedValue, deliverydate.ToString() }, "dataset");
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)

                    {
                        Totalpaybleamount = PaybleAmtWithTcsTax + decimal.Parse(ds1.Tables[0].Rows[0]["Opening"].ToString());
                        //if (Totalpaybleamount <= decimal.Parse(ds1.Tables[0].Rows[0]["SecurityDeposit"].ToString()))
                        //{
                            GetSupplyDetails();
                        //}
                        //else
                        //{
                        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Warning :" + "Security Amount Should be greater than or equal to Total Payable Amount");
                        //}
                    }
                }
            }
            else
            {
                GetSupplyDetailsByDemandId();
            }

        }
    }
    private void GetTcsTax()
    {
        try
        {
            if (ddlDitributor.SelectedValue != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        CClear();
        pnlvehicledetail.Visible = false;
        txtDate.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        // ddlRoute.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        txtSupervisorName.Text.Trim();
        ddlDitributor.SelectedIndex = 0;
        pnldistss.Visible = false;
        string url = "MilkDistributionVehicleChallan.aspx";
        Response.Redirect(url);

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //if (ViewState["TotalIssueQty"].ToString() != "" && ViewState["TotalIssueQty"].ToString() != "0" && ViewState["TotalIssueCrate"].ToString() != "" && ViewState["TotalIssueCrate"].ToString() != "0")
            if (ViewState["TotalIssueQty"].ToString() != "" && ViewState["TotalIssueQty"].ToString() != "0" && ViewState["TotalIssueCrate"].ToString() != "")
            {
                //if (ddlLocation.SelectedValue == "1")
                //{
                //    GetLimitOrBalanceOfDistOrSS();
                //}
                //else
                //{
                InsertVehicleChallan();
                // }

            }

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMilkQty = (e.Row.FindControl("lblMilkQty") as Label);
                Label lblCrate = (e.Row.FindControl("lblCrate") as Label);
                totalmilkqty += Convert.ToInt32(lblMilkQty.Text);
                totalcrate += Convert.ToInt32(lblCrate.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFTotalMilkQty = (e.Row.FindControl("lblFTotalMilkQty") as Label);
                Label lblFTotalCrate = (e.Row.FindControl("lblFTotalCrate") as Label);
                lblFTotalMilkQty.Text = totalmilkqty.ToString();
                lblFTotalCrate.Text = totalcrate.ToString();
                ViewState["TotalIssueQty"] = lblFTotalMilkQty.Text;
                ViewState["TotalIssueCrate"] = lblFTotalCrate.Text;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
    }
    private void GetChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
                     new string[] { "Flag", "VehicleDispId", "Office_ID" },
                     new string[] { "2", cid, objdb.Office_ID() }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                StringBuilder sb = new StringBuilder();
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');
                sb.Append("<div class='invoice'>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                if (objdb.Office_ID() != "3")
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3' style='text-align:center:'>MKTG/F/04</td>");
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td>दिनांक&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='font-size:17px;'><b>" + Dairyplant[0] + " डेरी प्लांट</b></td>");
                sb.Append("<td>" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr></tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'><span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VDChallanNo"].ToString() + "</span></td>");
                sb.Append("<td style='font-size:17px;'><b>वाहन वितरण चालान</b></td>");
                sb.Append("<td>वाहन क्रं&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:left'>सुपरवाइजर का नाम&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["SupervisorName"].ToString() + "</span></td>");
                sb.Append("<td style='text-align:right'>" + (ds3.Tables[0].Rows[0]["MilkCurDMCrateIsueStatus"].ToString() == "1" ? "Crate Issued" : "Crate Not Issued") + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'>आने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleIn_Time"].ToString() + "</span></td>");
                sb.Append("<td>मार्ग क्रं&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["RName"].ToString() + "</span></td>");
                sb.Append("<td>जाने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:left'>प्रदाय वितरण</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='table table1-bordered' style='width:100%;'>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center'>क्र.म.</th>");
                sb.Append("<th style='text-align:center'>दूध का नाम</th>");
                if (objdb.Office_ID() != "3")
                {
                    sb.Append("<th style='text-align:center'>दूध की मात्रा</th>");
                }
                else
                {
                    sb.Append("<th style='text-align:center'>पैकेट की संख्या</th>");
                }
                sb.Append("<th style='text-align:center'>क्रैट की संख्या</th>");
                sb.Append("<th style='text-align:center'>अन्य</th>");
                sb.Append("</tr>");
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;

                for (int i = 0; i < Count; i++)
                {
                    TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                    TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1) + "</td>");
                    sb.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueCrate"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["ExtraPacket"].ToString() + "</td>");
                    sb.Append("</tr>");

                }
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td><b>टोटल</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["MilkCurDMCrateIsueStatus"].ToString() == "1" ? TotalissueCrate.ToString() : "0") + "</b></td>");

                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वाहन सुपरवाइजर</span></td>");
				if (objdb.Office_ID() == "5")
                {
                    sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>I/C GMPO</span></td>");
                }
                sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर सुरक्षा प्रभारी</br>&nbsp;&nbsp;जाते समय&nbsp;&nbsp;</span></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                Print.InnerHtml = sb.ToString();
                Print1.InnerHtml = sb.ToString();
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
            if (ds3 != null) { ds3.Dispose(); }
        }

    }
    #region============ Code for Vehicle Details=========================================
    protected void GetVendorType()
    {
        try
        {
            ddlVendorType.DataTextField = "VendorTypeName";
            ddlVendorType.DataValueField = "VendorTypeId";
            ddlVendorType.DataSource = objdb.ByProcedure("USP_Mst_VendorType",
                           new string[] { "flag" },
                           new string[] { "1" }, "dataset");
            ddlVendorType.DataBind();
            ddlVendorType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }

    protected void GetName()
    {
        try
        {
            if (ddlVendorType.SelectedValue != "0")
            {
                ddlVendorName.DataTextField = "Contact_Person";
                ddlVendorName.DataValueField = "TransporterId";
                ddlVendorName.DataSource = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                               new string[] { "flag", "Office_ID", "VendorTypeId" },
                               new string[] { "7", objdb.Office_ID(), ddlVendorType.SelectedValue }, "dataset");
                ddlVendorName.DataBind();
                ddlVendorName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    private void GetVehicleNo()
    {
        try
        {
            ddlVehicleNo.DataTextField = "VehicleNo";
            ddlVehicleNo.DataValueField = "VehicleMilkOrProduct_ID";
            ddlVehicleNo.DataSource = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "5", objdb.Office_ID() }, "dataset");
            ddlVehicleNo.DataBind();
            ddlVehicleNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Transportation ", ex.Message.ToString());
        }
    }
    private void Clear()
    {

        txtVehicleNo.Text = string.Empty;
        txtVehicleType.Text = string.Empty;
        ddlVendorType.SelectedIndex = 0;
        ddlVendorName.SelectedValue = "0";
    }
    protected void ddlVendorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetName();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myVehicleDetailsModal()", true);
    }
    protected void lnkVehicle_Click(object sender, EventArgs e)
    {
        Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myVehicleDetailsModal()", true);
    }
    protected void btnSaveVehicleDetails_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {

                    lblModalMsg1.Text = "";
                    string isactive = "";
                    if (chkIsActive.Checked == true)
                    {
                        isactive = "1";
                    }
                    else
                    {
                        isactive = "0";
                    }

                    if (btnSaveVehicleDetails.Text == "Submit")
                    {
                        lblModalMsg1.Text = "";
                        ds9 = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                            new string[] { "flag", "VendorTypeId", "TransporterId", "VehicleType", "VehicleNo", "IsActive", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "2",ddlVendorType.SelectedValue,ddlVendorName.SelectedValue,txtVehicleType.Text.Trim(), txtVehicleNo.Text.Trim(),isactive, objdb.Office_ID(), objdb.createdBy(),
                                            objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds9.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetVehicleNo();
                            string success = ds9.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblModalMsg1.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds9.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
                            {
                                lblModalMsg1.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle Number " + txtVehicleNo.Text + " " + error);
                            }
                            else
                            {
                                lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }
            }

            catch (Exception ex)
            {
                lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

            }
            finally
            {
                if (ds9 != null) { ds9.Dispose(); }
            }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myVehicleDetailsModal()", true);
    }
    #endregion========================================================================


    protected void ddlDMType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnldata.Visible = false;
        pnlvehicledetail.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        if (ddlDMType.SelectedValue == "0")
        {
            pnlorderno.Visible = false;
        }
        else
        {
            pnlorderno.Visible = true;
            GetOrderNo();
        }
    }
}