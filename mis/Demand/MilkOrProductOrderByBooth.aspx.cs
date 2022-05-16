using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
//using System.Text.RegularExpressions;

public partial class mis_MilkOrProductOrderByBooth : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1,ds2 = new DataSet();

    string orderdate = "", demanddate = "", currentdate = "", currrentime = "", deliverydat = "", predemanddate="";
    Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    //Regex regaxfdnotzero = new Regex(@"^[1-9][0-9]*$");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                DisplayMilkTime();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
    #region=======================user defined function========================
    private void DisplayMilkTime()
    {
        if (objdb.Office_ID() == "2")
        {
            pnlmilkOrProducttimeline.Visible = true;
        }
        else
        {
            pnlmilkOrProducttimeline.Visible = false;
        }

    }
    protected void GetShift()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void CheckDemandOrderTime() // this function will check morning and evening shift order of product or milk
    {
        try
        {
            ds = objdb.ByProcedure("USP_GetServerDatetime",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string myStringfromdat = txtOrderDate.Text; // From Database
                DateTime demanddate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime demanddateplus = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string myStringcurrentdate = ds.Tables[0].Rows[0]["currentDate"].ToString();
                DateTime currentdate = DateTime.ParseExact(myStringcurrentdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                currrentime = ds.Tables[0].Rows[0]["currentTime"].ToString();
                string[] s = currrentime.Split(':');
                if (demanddate == currentdate)
                {
                    if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Milk")
                    {
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "2";

                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
                        {

                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                            ddlShift.SelectedIndex = 0;
                            pnlProduct.Visible = false;
                            btnSubmit.Visible = false;
                            return;
                        }
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                            ddlShift.SelectedIndex = 0;
                            pnlProduct.Visible = false;
                            btnSubmit.Visible = false;
                            return;
                        }
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                    }

                    else if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Milk")
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";

                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
                        {
                            if ((Convert.ToInt32(s[0]) >= 8 && Convert.ToInt32(s[0]) <= 11 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 11 && Convert.ToInt32(s[1]) < 31) || (Convert.ToInt32(s[0]) < 11 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                ddlShift.SelectedIndex = 0;
                                pnlProduct.Visible = false;
                                btnSubmit.Visible = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
                        ddlItemCategory.SelectedIndex = 0;
                        pnlProduct.Visible = false;
                        btnSubmit.Visible = false;
                        return;
                    }
                    //if ((Convert.ToInt32(s[0]) >= 6 && Convert.ToInt32(s[0]) <= 11 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 11 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 11 && Convert.ToInt32(s[1]) <= 59)))
                    //{
                    if ((Convert.ToInt32(s[0]) >= 1 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59)))
                    {
                        //if ((Convert.ToInt32(s[0]) >= 8 && Convert.ToInt32(s[0]) <= 10 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 10 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 10 && Convert.ToInt32(s[1]) <= 59)))
                        //{
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;

                        GetItem();
                    }
                    //else if ((Convert.ToInt32(s[0]) >= 11 && Convert.ToInt32(s[0]) <= 17 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 17 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 17 && Convert.ToInt32(s[1]) <= 59)))
                    //{
                    else if ((Convert.ToInt32(s[0]) >= 1 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59)))
                    {
                        //else if ((Convert.ToInt32(s[0]) >= 15 && Convert.ToInt32(s[0]) <= 17 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 17 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 17 && Convert.ToInt32(s[1]) <= 59)))
                        //{

                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Date :" + txtOrderDate.Text + " . Morning Shift order allowed between 6:00 am to 11:00 am and Evening shift order allowed between 11:00 am to 5:00 pm .");
                        txtOrderDate.Text = string.Empty;
                        ddlShift.SelectedIndex = 0;
                        ddlItemCategory.SelectedIndex = 0;
                        pnlProduct.Visible = false;
                        btnSubmit.Visible = false;
                        return;
                    }
                }
                else if (demanddate >= currentdate)
                {
                    if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
                        ddlItemCategory.SelectedIndex = 0;
                        return;
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        demanddateplus = demanddateplus.AddDays(-1);
                        if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
                        {
                            if ((Convert.ToInt32(s[0]) >= 7 && Convert.ToInt32(s[0]) <= 10 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 10 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 10 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                ddlShift.SelectedIndex = 0;
                                pnlProduct.Visible = false;
                                btnSubmit.Visible = false;
                                return;
                            }
                        }
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Milk")
                    {
                        demanddateplus = demanddateplus.AddDays(-1);
                        if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
                        {
                            if ((Convert.ToInt32(s[0]) >= 12 && Convert.ToInt32(s[0]) <= 16 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 16 && Convert.ToInt32(s[1]) < 31) || (Convert.ToInt32(s[0]) < 16 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                ddlShift.SelectedIndex = 0;
                                pnlProduct.Visible = false;
                                btnSubmit.Visible = false;
                                return;
                            }
                        }
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "2";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();
                    }
                    else
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();
                    }
                }
                else
                {
                    if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
                        ddlItemCategory.SelectedIndex = 0;
                        return;
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Milk")
                    {
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "2";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();
                    }
                    else
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlClear.Visible = false;
                        btnSubmit.Visible = true;
                        GetItem();
                    }
                    //lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Date :" + txtOrderDate.Text + " , Previous Date order not allowed;");
                    //pnlProduct.Visible = false;
                    //return;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetItem()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "CreatedBy", "Office_ID" },
                       new string[] { "3", orderdate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.createdBy(), objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                lblCartMsg.Text = "<i class='fa fa-cart-plus fa-2x'></i> Cart details for " + ddlItemCategory.SelectedItem.Text;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                pnlProduct.Visible = true;
                pnlSubmit.Visible = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlProduct.Visible = false;
                pnlSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetPreviousDemand()
    {
        try
        {
            
            DateTime ddate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            predemanddate = ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag", "Shift_id", "ItemCat_id", "BoothId", "Office_ID", "Demand_Date" },
                       new string[] { "15", ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.createdBy(), objdb.Office_ID(), predemanddate }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    GridViewRow selectedrow = row;
                    TextBox gv_txtQty = (TextBox)selectedrow.FindControl("gv_txtQty");
                    Label lblItemid = (Label)row.FindControl("lblItemid");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (lblItemid.Text == ds.Tables[0].Rows[i]["Item_id"].ToString())
                        {

                            gv_txtQty.Text = ds.Tables[0].Rows[i]["ItemQty"].ToString();
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Previous Demand Found. Add New Demand.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 : ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetDemandDetails()
    {
        DateTime date5 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        demanddate = date5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "Office_ID", "BoothId" },
                   new string[] { "10", demanddate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), objdb.createdBy() }, "dataset");

        if (ds1.Tables[0].Rows.Count != 0)
        {
            pnlsearchdata.Visible = true;
            GridView3.DataSource = ds1.Tables[0];
            GridView3.DataBind();

        }
        else
        {
            pnlsearchdata.Visible = true;
            GridView3.DataSource = null;
            GridView3.DataBind();
        }
    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {

        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = string.Empty;
                GetDemandDetails();
            }
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error  6 :", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    protected void lnkAddDemand_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {

            lblMsg.Text = string.Empty;
            pnlmsg.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            CheckDemandOrderTime();
        }

    }

    protected void lnkPreviousOrder_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetPreviousDemand();
        }
    }


    private void InsertOrder()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                DataTable dtStatus = new DataTable();
                DataRow dr;

                dtStatus.Columns.Add("ItemName", typeof(string));
                dtStatus.Columns.Add("ItemQty", typeof(string));
                dtStatus.Columns.Add("AdvCard", typeof(int));
                dtStatus.Columns.Add("TotalItemQty", typeof(int));
                dtStatus.Columns.Add("Status", typeof(int));
                dtStatus.Columns.Add("Msg", typeof(string));

                dr = dtStatus.NewRow();
                foreach (GridViewRow row in GridView1.Rows)
                {

                    Label lblItemName = (Label)row.FindControl("ItemName");
                    Label lblItemid = (Label)row.FindControl("lblItemid");
                    TextBox gvtxtQty = (TextBox)row.FindControl("gv_txtQty");
                    Label lblAdvanceCard = (Label)row.FindControl("lblAdvanceCard");
                    dr[0] = lblItemName.Text;
                    dr[1] = gvtxtQty.Text;
                    dr[2] = lblAdvanceCard.Text;

                    if (gvtxtQty.Text == "0")
                    {
                        gvtxtQty.Text = "0";
                        totalqty = Convert.ToInt32(gvtxtQty.Text); //+ Convert.ToInt32(lblAdvanceCard.Text);
                        dr[3] = totalqty;
                    }
                    else if(!string.IsNullOrEmpty(gvtxtQty.Text))
                    {

                        totalqty = Convert.ToInt32(gvtxtQty.Text);// +Convert.ToInt32(lblAdvanceCard.Text);
                        dr[3] = totalqty;
                    }
                   else
                    {
                        dr[3] = "0";
                    }
                    if (!string.IsNullOrEmpty(gvtxtQty.Text) && gvtxtQty.Text != "")
                    //if (gvtxtQty.Text != "" && gvtxtQty.Text != "0")
                    {
                        //ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                        //   new string[] { "flag", "BoothId", "ItemCat_id", "Item_id", "ItemQty", "AdvCard", "TotalQty", "Demand_Date", "Shift_id", "Demand_Status", "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType", "Delivary_Date", "DelivaryShift_id", "RetailerTypeID","RouteId" },
                        //  new string[] { "4", objdb.createdBy(), ddlItemCategory.SelectedValue, lblItemid.Text, gvtxtQty.Text, lblAdvanceCard.Text, totalqty.ToString(), odat.ToString(), ddlShift.SelectedValue, "1", objdb.UserTypeID(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), "1", ViewState["DelivaryDate"].ToString(), ViewState["DelivaryShift"].ToString(), objdb.RetailerType_ID(),objdb.Route_ID() }, "dataset");
                        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                          new string[] { "flag", "BoothId", "ItemCat_id", "Item_id", "ItemQty", "AdvCard", "TotalQty", "Demand_Date", "Shift_id", "Demand_Status", "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType", "Delivary_Date", "DelivaryShift_id", "RetailerTypeID", "RouteId" },
                         new string[] { "4", objdb.createdBy(), ddlItemCategory.SelectedValue, lblItemid.Text, gvtxtQty.Text, lblAdvanceCard.Text, totalqty.ToString(), odat.ToString(), ddlShift.SelectedValue, "1", objdb.UserTypeID(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), "1", odat.ToString(), ddlShift.SelectedValue, objdb.RetailerType_ID(), objdb.Route_ID() }, "dataset");

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                dr[4] = "1";
                                dr[5] = "Record Saved Successfully";
                                ds.Clear();
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                            {
                                dr[4] = "2";
                                dr[5] = "Record Already Exist";
                                ds.Clear();
                            }
                            else
                            {
                                dr[4] = "0";
                                dr[5] = "Record Not Saved";
                                ds.Clear();

                            }
                        }
                    }
                    else
                    {
                        dr[4] = "0";
                        dr[5] = "Empty entry is not allowed.";
                    }
                    dtStatus.Rows.Add(dr.ItemArray);
                }
                if (dtStatus.Rows.Count > 0)
                {
                    pnlProduct.Visible = false;
                    pnlSubmit.Visible = false;
                    pnlmsg.Visible = true;
                    pnlClear.Visible = true;
                    GridView2.DataSource = dtStatus;
                    GridView2.DataBind();
                    GetDemandDetails();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Proceed.");
                    ViewState["DelivaryDate"] = null;
                    ViewState["DelivaryShift"] = null;
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================

    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    //protected void txtOrderDate_TextChanged(object sender, EventArgs e)
    //{

    //    ddlShift.SelectedIndex = 0;
    //    ddlItemCategory.SelectedIndex = 0;
    //    pnlProduct.Visible = false;
    //    pnlSubmit.Visible = false;
    //    pnlmsg.Visible = false;
    //    pnlsearchdata.Visible = false;
    //    GridView3.DataSource = null;
    //    GridView3.DataBind();
    //    lblMsg.Text = string.Empty;
    //    pnlClear.Visible = false;
    //}
    //protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtOrderDate.Text != "")
    //    {

    //        ddlItemCategory.SelectedIndex = 0;
    //        pnlProduct.Visible = false;
    //        pnlSubmit.Visible = false;
    //        pnlmsg.Visible = false;
    //        lblMsg.Text = string.Empty;
    //        pnlsearchdata.Visible = false;
    //        GridView3.DataSource = null;
    //        GridView3.DataBind();
    //        pnlClear.Visible = false;

    //    }
    //    else
    //    {
    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Please Select Date");
    //    }

    //}
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlItemCategory.SelectedValue != "0" && txtOrderDate.Text != "" && ddlShift.SelectedValue != "0")
    //    {
    //        lblMsg.Text = string.Empty;
    //        btnSubmit.Visible = false;
    //        pnlProduct.Visible = false;
    //        pnlmsg.Visible = false;
    //        pnlClear.Visible = false;

    //    }
    //    else
    //    {
    //        txtOrderDate.Text = string.Empty;
    //        ddlShift.SelectedIndex = 0;
    //        ddlItemCategory.SelectedIndex = 0;
    //        pnlProduct.Visible = false;
    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Please Select Date & Shift");
    //    }
    //}

    #endregion============ end of changed event for controls===========




    #region============ button click event ============================
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DateTime checkdate3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
                string chekdate = checkdate3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "BoothId"},
                     new string[] { "18", chekdate,ddlShift.SelectedValue,ddlItemCategory.SelectedValue, objdb.createdBy()}, "dataset");

                if (ds2.Tables[0].Rows.Count>0)
                {
                       lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Once Order has been approved,you can't order it again.");
                       pnlProduct.Visible = false;
                       pnlSubmit.Visible = false;
                       pnlmsg.Visible = false;
                       pnlClear.Visible = false;
                       GridView2.DataSource = null;
                       GridView2.DataBind();
                       ViewState["DelivaryDate"] = null;
                       ViewState["DelivaryShift"] = null;
                       ddlItemCategory.SelectedIndex = 0;
                   
                }
                else
                {
                    InsertOrder();
                }
               
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error  8 : ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlSubmit.Visible = false;
        pnlProduct.Visible = false;
        pnlmsg.Visible = false;
        pnlClear.Visible = false;
        pnlmsg.Visible = false;
    }



    #endregion=============end of button click funciton==================


    private void GetItemDetailByDemandID()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["rowid"].ToString(), ViewState["rowitemcatid"].ToString(),objdb.Office_ID() }, "dataset");
            GridView4.DataSource = ds1.Tables[0];
            GridView4.DataBind();
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 9 :", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ItemOrdered")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                pnlmsg.Visible = false;
                pnlClear.Visible = false;
                lblSearchMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                Label lblDeliveryDate = (Label)row.FindControl("lblDeliveryDate");
                Label lblDeliveryShift = (Label)row.FindControl("lblDeliveryShift");
                Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                ViewState["rowid"] = e.CommandArgument.ToString();
                ViewState["rowitemcatid"] = lblItemCatid.Text; ;
                GetItemDetailByDemandID();


                modaldate.InnerHtml = lblDemandDate.Text;
                modelShift.InnerHtml = ddlShift.SelectedItem.Text;
                modelcategory.InnerHtml = ddlItemCategory.SelectedItem.Text;
                modalDevliveryDate.InnerHtml = lblDeliveryDate.Text;
                modalDelivaryShift.InnerHtml = lblDeliveryShift.Text;
                modalstatus.InnerHtml = lblDemandStatus.Text;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

            }
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;
                Label lblDemandStatus = e.Row.FindControl("lblDemandStatus") as Label;

                if (lblDemandStatus.Text == "2")
                {
                    e.Row.Cells[6].Visible = false;
                    GridView4.HeaderRow.Cells[6].Visible = false;

                }
                else if (lblDemandStatus.Text == "3")
                {
                    e.Row.Cells[6].Visible = false;
                    GridView4.HeaderRow.Cells[6].Visible = false;

                }
                else
                {
                    GridView4.HeaderRow.Cells[6].Visible = true;
                    e.Row.Cells[6].Visible = true;
                }

                if (lblItemCatName.Text == "Milk")
                {
                    e.Row.CssClass = "columnmilk";
                }
                else
                {
                    e.Row.CssClass = "columnproduct";
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10 : " + ex.Message.ToString());
        }
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    pnlmsg.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblItemQty = (Label)gvRow.FindControl("lblItemQty");
                        TextBox HtxtItemQty = (TextBox)gvRow.FindControl("txtItemQty");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                        HlblItemQty.Visible = true;
                        HtxtItemQty.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrev1.Enabled = false;

                    }
                    txtItemQty.Text = "";
                    txtItemQty.Text = lblItemQty.Text;
                    lblItemQty.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    txtItemQty.Visible = true;
                }

            }
            if (e.CommandName == "RecordReset")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    Label lblAdvCard = (Label)row.FindControl("lblAdvCard");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    lblItemQty.Visible = true;
                    lblAdvCard.Visible = true;
                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    txtItemQty.Visible = false;
                }

            }
            if (e.CommandName == "RecordUpdate")
            {
                if (Page.IsValid)
                {


                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        lblMsg.Text = string.Empty;
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblItemQty = (Label)row.FindControl("lblItemQty");
                        Label lblAdvCard = (Label)row.FindControl("lblAdvCard");
                        TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                        int totalqty = 0;
                        if (txtItemQty.Text == "0")
                        {
                            totalqty = 0;
                        }
                        else
                        {
                            totalqty = Convert.ToInt32(txtItemQty.Text);// +Convert.ToInt32(lblAdvCard.Text);
                        }

                        ds = objdb.ByProcedure("USP_GetServerDatetime",
                         new string[] { "flag" },
                           new string[] { "1" }, "dataset");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string myStringfromdat = txtOrderDate.Text; // From Database
                            DateTime demanddate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            string myStringcurrentdate = ds.Tables[0].Rows[0]["currentDate"].ToString();
                            DateTime currentdate = DateTime.ParseExact(myStringcurrentdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            currrentime = ds.Tables[0].Rows[0]["currentTime"].ToString();
                            string[] s = currrentime.Split(':');
                            if (demanddate == currentdate)
                            {
                                //if ((Convert.ToInt32(s[0]) >= 6 && Convert.ToInt32(s[0]) <= 11 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 11 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 11 && Convert.ToInt32(s[1]) <= 59)))
                                //{
                                if ((Convert.ToInt32(s[0]) >= 1 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59)))
                                {
                                    //if ((Convert.ToInt32(s[0]) >= 8 && Convert.ToInt32(s[0]) <= 10 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 10 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 10 && Convert.ToInt32(s[1]) <= 59)))
                                    //{
                                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                     new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                     new string[] { "5", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour) " }, "TableSave");

                                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                        lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                        GetItemDetailByDemandID();
                                        GridView1.SelectedIndex = -1;
                                    }
                                    else
                                    {
                                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                    }
                                }
                                //else if ((Convert.ToInt32(s[0]) >= 11 && Convert.ToInt32(s[0]) <= 17 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 17 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 17 && Convert.ToInt32(s[1]) <= 59)))
                                //{
                                else if ((Convert.ToInt32(s[0]) >= 1 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59)))
                                {
                                    //else if ((Convert.ToInt32(s[0]) >= 15 && Convert.ToInt32(s[0]) <= 17 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 17 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 17 && Convert.ToInt32(s[1]) <= 59)))
                                    //{

                                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                 new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                 new string[] { "5", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour) " }, "TableSave");

                                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                        lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                        GetItemDetailByDemandID();
                                        GridView1.SelectedIndex = -1;
                                    }
                                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Approved")
                                    {
                                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                        lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + success);
                                        GetItemDetailByDemandID();
                                        GridView1.SelectedIndex = -1;
                                    }
                                    else
                                    {
                                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                    }
                                }
                                else
                                {
                                    foreach (GridViewRow gvRow in GridView4.Rows)
                                    {
                                        Label HlblItemQty = (Label)gvRow.FindControl("lblItemQty");
                                        TextBox HtxtItemQty = (TextBox)gvRow.FindControl("txtItemQty");
                                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                                        HlblItemQty.Visible = true;
                                        HtxtItemQty.Visible = false;
                                        HlnkEdit.Visible = true;
                                        HlnkUpdate.Visible = false;
                                        HlnkReset.Visible = false;
                                        Hrfv.Enabled = false;
                                        Hrev1.Enabled = false;

                                    }
                                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Date :" + txtOrderDate.Text + " . You can edit Order between 6:00am to 11:00am in Morning and 11:00am to 5:00pm in evening.");
                                    return;
                                }
                            }
                            if (demanddate >= currentdate)
                            {
                                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                     new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                     new string[] { "5", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour) " }, "TableSave");

                                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                    GetItemDetailByDemandID();
                                    GridView1.SelectedIndex = -1;
                                }
                                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Approved")
                                {
                                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + success);
                                    GetItemDetailByDemandID();
                                    GridView1.SelectedIndex = -1;
                                }
                                else
                                {
                                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 11:" + error);
                                }
                            }
                            else
                            {
                                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                     new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                     new string[] { "5", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour) " }, "TableSave");

                                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                    GetItemDetailByDemandID();
                                    GridView1.SelectedIndex = -1;
                                }
                                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Approved")
                                {
                                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + success);
                                    GetItemDetailByDemandID();
                                    GridView1.SelectedIndex = -1;
                                }
                                else
                                {
                                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 11:" + error);
                                }
                            }
                        }
                    }

                }
            }
            if (e.CommandName == "RecordDelete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                lblMsg.Text = string.Empty;
                ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                            new string[] { "flag", "MilkOrProductDemandChildId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                            new string[] { "7", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "ItemQty Deleted from MilkorProduct Page(Parlour)" }, "TableSave");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    GetItemDetailByDemandID();
                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  12:" + error);
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 13: " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
            if (ds != null) { ds.Dispose(); }
        }

    }

}