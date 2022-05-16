using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Net;


public partial class mis_Demand_MilkOrProductOrderBySuperStockist : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds5, ds6, ds7 = new DataSet();
    string orderdate = "", demanddate = "", currentdate = "", currrentime = "", prevcurrrentime = "", deliverydat = "", predemanddate = "";
    Int32 totalqty = 0;
    int recordyn = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "readonly");
                GetCategory();
                GetShift();
                DisplayMilkTime();
                GetVehicleNo();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ds = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                    new string[] { "Flag", "Office_ID", "superstockist_ID" },
                      new string[] { "10", objdb.Office_ID(), objdb.createdBy() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbldistributerMONO.Text = ds.Tables[0].Rows[0]["SSCPersonMobileNo"].ToString();
                    }
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
    #region=======================user defined function========================
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
        //ddlItemCategory.SelectedIndex = 0;
        ddlRetailer.SelectedIndex = 0;
        btnSubmit.Text = "Save";

    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayMilkTime();
    }
    private void DisplayMilkTime()
    {
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
        {
            pnlmilktimeline.Visible = true;
            pnlmilkMD.InnerHtml = "Note : Morning Demand -(12:00 pm to 04:30 pm).";
            pnlmilkED.InnerHtml = "Note : Evening Demand -(08:00 am to 11:30 am).";
            pnlproducttimeline.Visible = false;
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = true;
            pnlproductMD.InnerHtml = "Note : Product Morning Demand -(07:00 am to 10:00 am)";
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4")
        {
            pnlmilktimeline.Visible = true;
            pnlmilkMD.InnerHtml = "Note : Morning Demand -(03:00 pm to 10:00 pm).";
            pnlmilkED.InnerHtml = "Note : Evening Demand -(09:00 am to 02:00 pm).";
            pnlproducttimeline.Visible = false;
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "4")
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = true;
            pnlproductMD.InnerHtml = "Note : Product Morning Demand -(09:00 am to 02 :30 pm)";
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjain Milk
        {
            pnlmilktimeline.Visible = true;
            pnlmilkMD.InnerHtml = "Note : Morning Demand -(06:00 pm to 9:00 pm).";
            pnlmilkED.InnerHtml = "Note : Evening Demand -(09:00 am to 01:00 pm).";
            pnlproducttimeline.Visible = false;
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "6") // Ujjain Product
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = true;
            pnlproductMD.InnerHtml = "Note : Product Morning Demand -(09:00 am to 01 :00 pm)";
        }
        else
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = false;
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
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void CheckDemandOrderTime()
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
                            pnlProduct.Visible = false;
                            GetDatatableHeaderDesign();
                            ddlRetailer.SelectedIndex = 0;
                            return;
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4") // Indore
                        {

                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                            pnlProduct.Visible = false;
                            GetDatatableHeaderDesign();
                            ddlRetailer.SelectedIndex = 0;
                            return;
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjain
                        {

                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                            pnlProduct.Visible = false;
                            GetDatatableHeaderDesign();
                            ddlRetailer.SelectedIndex = 0;
                            return;
                        }
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can not place on Date: " + txtOrderDate.Text + "");
                            ddlRetailer.SelectedIndex = 0;
                            return;
                        }
                        if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "4") // Indore
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can not place on Date: " + txtOrderDate.Text + "");
                            ddlRetailer.SelectedIndex = 0;
                            return;
                        }
                        if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "6")//Ujjain
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can not place on Date: " + txtOrderDate.Text + "");
                            ddlRetailer.SelectedIndex = 0;
                            return;
                        }
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
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4") // indore 9 am to 2 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 14 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 14 && Convert.ToInt32(s[1]) <1) || (Convert.ToInt32(s[0]) < 14 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjain 9 am to 1 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 13 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 13 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 13 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
                        ddlRetailer.SelectedIndex = 0;
                        return;
                    }
                    if ((Convert.ToInt32(s[0]) >= 1 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59)))
                    {
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        GetItem();
                        GetDatatableHeaderDesign();
                    }
                    
                    else if ((Convert.ToInt32(s[0]) >= 1 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59)))
                    {

                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        GetItem();
                        GetDatatableHeaderDesign();


                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Date :" + txtOrderDate.Text + " . Morning Shift order allow between (6:00 am to 11:00 am) and Evening shift order allow betwee  (11:00 am to 5:00 pm) .");
                        txtOrderDate.Text = string.Empty;
                        if (ddlItemCategory.SelectedValue == objdb.GetProductCatId())
                        {
                            ddlShift.SelectedValue = objdb.GetShiftMorId();
                            ddlShift.Enabled = false;
                        }
                        else
                        {
                            ddlShift.SelectedIndex = 0;
                            ddlShift.Enabled = true;
                        }
                        ddlRetailer.SelectedIndex = 0;
                        GetDatatableHeaderDesign();
                        pnlProduct.Visible = false;
                        return;
                    }
                }
                else if (demanddate >= currentdate)
                {
                    if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
                        ddlRetailer.SelectedIndex = 0;
                        return;
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        demanddateplus = demanddateplus.AddDays(-1);
                        if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
                        {
                            if ((Convert.ToInt32(s[0]) >= 7 && Convert.ToInt32(s[0]) <= 10 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 10 && Convert.ToInt32(s[1]) <= 1) || (Convert.ToInt32(s[0]) < 10 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                        else if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "4") // Indore 9 am to 2:30 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 14 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 14 && Convert.ToInt32(s[1]) < 31) || (Convert.ToInt32(s[0]) < 14 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                        else if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "6") // Ujjain 9 am to 1:00 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 13 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 13 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 13 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
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
                        GetItem();
                        GetDatatableHeaderDesign();
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
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                        else if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4") // indore morning 3 pm to 10 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 15 && Convert.ToInt32(s[0]) <= 22 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 22 && Convert.ToInt32(s[1]) <1) || (Convert.ToInt32(s[0]) < 22 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
                                return;
                            }
                        }
                        else if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjain morning 6 pm to 9 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 18 && Convert.ToInt32(s[0]) <= 21 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 21 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 21 && Convert.ToInt32(s[1]) <= 59)))
                            {
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                pnlProduct.Visible = false;
                                GetDatatableHeaderDesign();
                                ddlRetailer.SelectedIndex = 0;
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
                        GetItem();
                        GetDatatableHeaderDesign();
                    }
                    else
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        GetItem();
                        GetDatatableHeaderDesign();
                    }
                }
                else // temporary open for previous data
                {
                    if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Product")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
                        pnlProduct.Visible = false;
                        GetDatatableHeaderDesign();
                        ddlRetailer.SelectedIndex = 0;
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
                        GetItem();
                        GetDatatableHeaderDesign();
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
                        GetItem();
                        GetDatatableHeaderDesign();
                    }
                    else
                    {
                        demanddate = demanddate.AddDays(1);
                        deliverydat = demanddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        ViewState["DelivaryDate"] = deliverydat;
                        ViewState["DelivaryShift"] = "1";
                        lblMsg.Text = string.Empty;
                        pnlProduct.Visible = true;
                        pnlmsg.Visible = false;
                        pnlClear.Visible = false;
                        GetItem();
                        GetDatatableHeaderDesign();
                    }
                    //lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Date :" + txtOrderDate.Text + " , Previous Date order not allowed;");
                    //pnlProduct.Visible = false;
                    //GetDatatableHeaderDesign();
                    //return;

                }
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
                       new string[] { "3", orderdate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlRetailer.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                lblCartMsg.Text = "<i class='fa fa-cart-plus'></i> Cart details for " + ddlItemCategory.SelectedItem.Text;
                GridView1.DataSource = ds;
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



    private void GetRetailer()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                     new string[] { "Flag", "Office_ID", "ItemCat_id", "SuperStockistId" },
                       new string[] { "5", objdb.Office_ID(), ddlItemCategory.SelectedValue,objdb.createdBy() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds.Tables[0];
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRetailerTypeID()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_BoothReg",
                     new string[] { "flag", "BoothId" },
                       new string[] { "8", ddlRetailer.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["RetailerTypeID"] = ds.Tables[0].Rows[0]["RetailerTypeID"];
                ViewState["RouteId"] = ds.Tables[0].Rows[0]["RouteId"];
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void InsertOrder()
    {
        try
        {

            GetDatatableHeaderDesign();
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DataTable dtStatus = new DataTable();
            DataRow dr;

            dtStatus.Columns.Add("Itemid", typeof(string));
            dtStatus.Columns.Add("ItemQty", typeof(string));
            dtStatus.Columns.Add("AdvCard", typeof(int));
            dtStatus.Columns.Add("TotalItemQty", typeof(int));

            dr = dtStatus.NewRow();
            foreach (GridViewRow row in GridView1.Rows)
            {

                Label lblItemid = (Label)row.FindControl("lblItemid");
                TextBox gvtxtQty = (TextBox)row.FindControl("gv_txtQty");
                Label lblAdvanceCard = (Label)row.FindControl("lblAdvanceCard");

                if (!string.IsNullOrEmpty(gvtxtQty.Text) && gvtxtQty.Text != "0" && ViewState["RetailerTypeID"].ToString() != "")
                {
                    dr[0] = lblItemid.Text;
                    dr[1] = gvtxtQty.Text;
                    dr[2] = lblAdvanceCard.Text;
                    dr[3] = gvtxtQty.Text;

                    dtStatus.Rows.Add(dr.ItemArray);
                }
            }
            if (dtStatus.Rows.Count > 0)
            {

                ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                          new string[] { "flag", "BoothId", "ItemCat_id", "Demand_Date", "Shift_id", "Demand_Status"
                                    , "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType"
                                    , "Delivary_Date", "DelivaryShift_id", "RetailerTypeID", "RouteId", "DistributorId","ProductDMStatus","SuperStockistId","VehicleMilkOrProduct_ID" },
                         new string[] { "3", ddlRetailer.SelectedValue, ddlItemCategory.SelectedValue, odat.ToString(), 
                             ddlShift.SelectedValue, "1", objdb.UserTypeID(), objdb.Office_ID(), 
                             objdb.createdBy(), IPAddress, "1", odat.ToString(), ddlShift.SelectedValue, ViewState["RetailerTypeID"].ToString(), 
                              ViewState["RouteId"].ToString(),"",ddlDMType.SelectedValue,objdb.createdBy(),ddlVehicleNo.SelectedValue },
                           new string[] { "type_Trn_MilkOrProductDemandChild" },
                           new DataTable[] { dtStatus }, "dataset");

                if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    //if (objdb.Office_ID() == "2")
                    //{
                        if (lbldistributerMONO.Text != "0000000000" && lbldistributerMONO.Text != "9999999999" && lbldistributerMONO.Text != null && lbldistributerMONO.Text != "")
                        {
                            string Supmessage = "";
                            string link = "";
                            string Order_ID = ds5.Tables[0].Rows[0]["Order_ID"].ToString();
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            //lbldistributerMONO.Text = "8962389494";

                            Supmessage = "Your order " + Order_ID.ToString() + " for date " + odat.ToString() + " shift " + ddlShift.SelectedItem.Text + " has been placed successfully.";
                            link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + lbldistributerMONO.Text + "&text=" + Server.UrlEncode(Supmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162650796309365&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";


                            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                            Stream stream = response.GetResponseStream();
                        }
                   // }
                    string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    ddlRetailer.SelectedIndex = 0;
                    pnlProduct.Visible = false;
                    pnlSubmit.Visible = false;
                }
                else if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                {
                    string already = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + already);
                    ddlRetailer.SelectedIndex = 0;
                }
                else
                {
                    string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
               
                if (GridView3.Rows.Count > 0)
                {
                    GetDemandDetailsOfParlour();
                }

            }
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
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView3.Rows.Count > 0)
            {
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView3.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 9: " + ex.Message.ToString());
        }
    }
    private void GetDemandDetailsOfParlour()
    {

        DateTime date5 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        demanddate = date5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "Office_ID", "SuperStockistId" },
                  new string[] { "1", demanddate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), objdb.createdBy() }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {
            lblMsg.Text = string.Empty;
            lblSearchMsg.Text = string.Empty;
            pnlsearchdata.Visible = true;
            GridView3.DataSource = ds1.Tables[0];
            GridView3.DataBind();
            GetDatatableHeaderDesign();
        }
        else
        {
            pnlsearchdata.Visible = true;
            GridView3.DataSource = null;
            GridView3.DataBind();
        }
    }
    #endregion============ end of changed event for controls===========
    #region============ button click event ============================
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = string.Empty;
                GetDemandDetailsOfParlour();
            }
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 10:", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void lnkAddDemand_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            pnladddemand.Visible = true;
            GetRetailer();
            GetDatatableHeaderDesign();
        }
    }
    //protected void lnkPreviousOrder_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        lblMsg.Text = string.Empty;
    //        ds7 = objdb.ByProcedure("USP_GetServerDatetime",
    //                 new string[] { "flag" },
    //                   new string[] { "1" }, "dataset");
    //        if (ds7.Tables[0].Rows.Count > 0)
    //        {
    //            string myStringfromdat = txtOrderDate.Text; // From Database
    //            DateTime Prevdemanddate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //            DateTime Prevdemanddateplus = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //            string myStringcurrentdate = ds7.Tables[0].Rows[0]["currentDate"].ToString();
    //            DateTime Prevcurrentdate = DateTime.ParseExact(myStringcurrentdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //            prevcurrrentime = ds7.Tables[0].Rows[0]["currentTime"].ToString();
    //            string[] s1 = prevcurrrentime.Split(':');
    //            if (Prevdemanddate == Prevcurrentdate)
    //            {

    //                if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Milk")
    //                {

    //                    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
    //                    {

    //                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
    //                        ddlShift.SelectedIndex = 0;
    //                        return;
    //                    }
    //                }
    //                else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
    //                {
    //                    if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
    //                    {
    //                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can not place on Date: " + txtOrderDate.Text + "");
    //                        txtOrderDate.Text = "";
    //                        return;
    //                    }
    //                }

    //                else if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Milk")
    //                {

    //                    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
    //                    {
    //                        if ((Convert.ToInt32(s1[0]) >= 8 && Convert.ToInt32(s1[0]) <= 11 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s1[0]) == 11 && Convert.ToInt32(s1[1]) < 31) || (Convert.ToInt32(s1[0]) < 11 && Convert.ToInt32(s1[1]) <= 59)))
    //                        {
    //                        }
    //                        else
    //                        {
    //                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
    //                            ddlShift.SelectedIndex = 0;
    //                            return;
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
    //                    ddlShift.SelectedIndex = 0;
    //                    return;
    //                }
    //            }
    //            else if (Prevdemanddate >= Prevcurrentdate)
    //            {
    //                if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Product")
    //                {
    //                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " can be done in Morning shift.");
    //                    return;
    //                }
    //                else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Product")
    //                {
    //                    Prevdemanddateplus = Prevdemanddateplus.AddDays(-1);
    //                    if (Prevcurrentdate == Prevdemanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
    //                    {
    //                        if ((Convert.ToInt32(s1[0]) >= 7 && Convert.ToInt32(s1[0]) <= 10 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s1[0]) == 10 && Convert.ToInt32(s1[1]) <= 1) || (Convert.ToInt32(s1[0]) < 10 && Convert.ToInt32(s1[1]) <= 59)))
    //                        {
    //                        }
    //                        else
    //                        {
    //                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
    //                            return;
    //                        }
    //                    }
    //                }
    //                else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedItem.Text == "Milk")
    //                {
    //                    Prevdemanddateplus = Prevdemanddateplus.AddDays(-1);
    //                    if (Prevcurrentdate == Prevdemanddateplus && ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
    //                    {
    //                        if ((Convert.ToInt32(s1[0]) >= 12 && Convert.ToInt32(s1[0]) <= 16 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s1[0]) == 16 && Convert.ToInt32(s1[1]) < 31) || (Convert.ToInt32(s1[0]) < 16 && Convert.ToInt32(s1[1]) <= 59)))
    //                        {
    //                        }
    //                        else
    //                        {
    //                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
    //                            ddlShift.SelectedIndex = 0;
    //                            return;
    //                        }
    //                    }
    //                }
    //                else
    //                {

    //                }
    //            }
    //        }




    //        //  GetPreviousDemandAllRetailer();
    //        this.GetPreviousDemandAllRetailer();
    //        GetDatatableHeaderDesign();

    //        modalpreviousDate.InnerHtml = txtOrderDate.Text;
    //        modalpreviousShift.InnerHtml = ddlShift.SelectedItem.Text;
    //        modalPreviousCategory.InnerHtml = ddlItemCategory.SelectedItem.Text;
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myRetailerListModal()", true);
    //        if (ds7 != null) { ds7.Dispose(); }
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (Page.IsValid)
            {   
                InsertOrder();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 11 : ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlSubmit.Visible = false;
        pnlProduct.Visible = false;
        pnlmsg.Visible = false;
        pnlClear.Visible = false;
        pnladddemand.Visible = false;
        GetDatatableHeaderDesign();
    }
    #endregion=============end of button click funciton==================

    //protected void ddlRetailer_Init(object sender, EventArgs e)
    //{
    //    GetRetailer();
    //}
    protected void ddlRetailer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue != "0" && ddlShift.SelectedValue != "0" && txtOrderDate.Text != "" && ddlRetailer.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            CheckDemandOrderTime();


            GetRetailerTypeID();
            GetDatatableHeaderDesign();

            

        }
        else
        {
            txtOrderDate.Text = string.Empty;
            if (ddlItemCategory.SelectedValue == objdb.GetProductCatId())
            {
                ddlShift.SelectedValue = objdb.GetShiftMorId();
                ddlShift.Enabled = false;
            }
            else
            {
                ddlShift.SelectedIndex = 0;
                ddlShift.Enabled = true;
            }
            ddlRetailer.SelectedIndex = 0;
            // ddlItemCategory.SelectedIndex = 0;
            pnlProduct.Visible = false;
            pnlSubmit.Visible = false;
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "Please Select Date, Shift,Category & Retailer");
        }
    }

    #region=====================code for search parlour wise==========================
    private void GetItemDetailByDemandID()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["rowid"].ToString(), ViewState["rowitemcatid"].ToString(), objdb.Office_ID() }, "dataset");
            GridView4.DataSource = ds1.Tables[0];
            GridView4.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 12: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDStatus = e.Row.FindControl("lblDStatus") as Label;

                if (lblDStatus.Text == "Yes")
                {
                    e.Row.CssClass = "columngreen";
                }
                else
                {
                    e.Row.CssClass = "columnred";
                }

            }
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 13: " + ex.Message.ToString());
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ItemOrdered")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Visible = false;
                    pnlmsg.Visible = false;
                    lblSearchMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblBoothName = (Label)row.FindControl("lblBoothName");
                    Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                    Label lblOrderId = (Label)row.FindControl("lblOrderId");
                    Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                    Label lblVehicleNo = (Label)row.FindControl("lblVehicleNo");
                    ViewState["rowid"] = e.CommandArgument.ToString();
                    ViewState["rowitemcatid"] = lblItemCatid.Text;
                    GetItemDetailByDemandID();

                    modalBoothName.InnerHtml = lblBoothName.Text;
                    modalorderid.InnerHtml = lblOrderId.Text;
                    modaldate.InnerHtml = lblDemandDate.Text;
                    modelShift.InnerHtml = ddlShift.SelectedItem.Text;
                    modalorderstatus.InnerHtml = lblDemandStatus.Text;
                    modalvehicle.InnerHtml = lblVehicleNo.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14: " + ex.Message.ToString());
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
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 15: " + ex.Message.ToString());
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
                        rfv.Enabled = false;
                        rev1.Enabled = false;

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

                    // GridView4.SelectedIndex = -1;

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
                                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 16:" + error);
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
                                    else
                                    {
                                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + error);
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
                                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Date :" + txtOrderDate.Text + " . You can edit Order between in morning (6:00 am to 11:00 am) and in evening(11:00 am to 5:00 pm) .");
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
                                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 18:" + error);
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
                                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 18:" + error);
                                }
                            }
                        }
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 19:" + error);
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 20: " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion=======================================================================

    #region======================code for previous Demand All Retailer Superstockist Wise=========

    //private void GetPreviousDemandAllRetailer()
    //{
    //    try
    //    {

    //        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
    //        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        ViewState["Preevious"] = null;
    //        ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductPreviousDemand",
    //                     new string[] { "Flag", "Demand_Date", "Shift_id", "ItemCat_id"
    //                                   , "SuperStockistId", "Office_ID" },
    //                       new string[] { "3", orderedate, ddlShift.SelectedValue, ddlItemCategory.SelectedValue
    //                           ,objdb.createdBy(), objdb.Office_ID() }, "dataset");
    //        if (ViewState["DynamicGridBind"] == null)
    //        {
    //            foreach (DataColumn column in ds5.Tables[0].Columns)
    //            {
    //                TemplateField tfield = new TemplateField();
    //                tfield.HeaderText = column.ColumnName;
    //                GridViewPreviousDemand.Columns.Add(tfield);
    //            }
    //        }

    //        if (ds5.Tables[0].Rows.Count > 0)
    //        {
    //            lbldModalMsgPreDemand.Text = string.Empty;

    //            ViewState["Preevious"] = ds5.Tables[0];
    //            ViewState["DynamicGridBind"] = "1";
    //            GridViewPreviousDemand.DataSource = ds5.Tables[0];
    //            GridViewPreviousDemand.DataBind();

    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lbldModalMsgPreDemand.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Previous Demand : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds5 != null) { ds5.Dispose(); }
    //    }
    //}
    #endregion==================================================================================
    //protected void GridViewPreviousDemand_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        DataTable dt1 = (DataTable)ViewState["Preevious"];
    //        for (int i = 0; i < dt1.Columns.Count + 1; i++)
    //        {
    //            if (i == 0)
    //            {


    //                CheckBox chkSelect = new CheckBox();

    //                chkSelect.ID = "chkSelect" + i;
    //                e.Row.Cells[i].Controls.Add(chkSelect);
    //            }
    //            if (i > 3)
    //            {

    //                string strcolval = dt1.Columns[i - 1].ColumnName;
    //                TextBox txtboxid = new TextBox();
    //                txtboxid.MaxLength = 5;
    //                txtboxid.Width = 50;
    //                txtboxid.ID = "txtboxid" + i;
    //                txtboxid.Text = (e.Row.DataItem as DataRowView).Row[strcolval].ToString();
    //                if (txtboxid.Text == "")
    //                {
    //                    txtboxid.Text = "0";
    //                }
    //                txtboxid.Attributes.Add("onkeypress", "return validateNum(event);");
    //                e.Row.Cells[i + 1].Controls.Add(txtboxid);
    //            }


    //        }

    //    }

    //    e.Row.Cells[2].Visible = false;
    //    e.Row.Cells[3].Visible = false;
    //    e.Row.Cells[4].Visible = false;
    //    e.Row.Cells[5].Visible = false;

    //}
    //protected void btnsave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Page.IsValid)
    //        {
    //            DataTable dt1 = (DataTable)ViewState["Preevious"];

    //            if (dt1.Rows.Count > 0)
    //            {
    //                DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
    //                string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //                for (int i = 0; i <= GridViewPreviousDemand.Rows.Count - 1; i++)
    //                {
    //                    GridViewRow row1 = GridViewPreviousDemand.Rows[i];


    //                    Label lblBoothId = (Label)row1.FindControl("lblBoothId");
    //                    Label lblRetailerTypeID = (Label)row1.FindControl("lblRetailerTypeID");
    //                    Label lblRouteId = (Label)row1.FindControl("lblRouteId");

    //                    int j = 0;
    //                    int k = 3;


    //                    if ((row1.FindControl("chkSelect0") as CheckBox).Checked)
    //                    {
    //                        foreach (DataColumn column in dt1.Columns)
    //                        {


    //                            if (j > 2)
    //                            {
    //                                string columnname = column.ColumnName;
    //                                k = k + 1;


    //                                string txtboxval = (row1.FindControl("txtboxid" + k) as TextBox).Text;

    //                                if (txtboxval != "" && !string.IsNullOrEmpty(txtboxval))
    //                                {

    //                                    ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductPreviousDemand",
    //                                   new string[] { "flag", "BoothId", "ItemCat_id", "ItemName", "ItemQty", "AdvCard", "TotalQty", "Demand_Date", "Shift_id", "Demand_Status", "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType", "Delivary_Date", "DelivaryShift_id", "RetailerTypeID", "RouteId", "DistributorId", "SuperStockistId" },
    //                                   new string[] { "2", lblBoothId.Text, ddlItemCategory.SelectedValue, columnname, txtboxval, "0", txtboxval, orderedate.ToString(), ddlShift.SelectedValue, "1", objdb.UserTypeID(), objdb.Office_ID(), objdb.createdBy(), IPAddress, "1", orderedate.ToString(), ddlShift.SelectedValue, lblRetailerTypeID.Text, lblRouteId.Text,"", objdb.createdBy() }, "dataset");
    //                                    if (ds6.Tables[0].Rows.Count > 0)
    //                                    {
    //                                        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                                        {

    //                                            // = "Record Saved Successfully";
    //                                            ds6.Dispose();
    //                                        }
    //                                        else if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Already")
    //                                        {

    //                                            // = "Record Already Exist";
    //                                            ds6.Dispose();
    //                                        }
    //                                        else
    //                                        {

    //                                            // = "Record Not Saved";
    //                                            ds6.Dispose();

    //                                        }
    //                                    }
    //                                    recordyn = recordyn + 1;
    //                                }


    //                            }
    //                            j = j + 1;


    //                        }
    //                    }

    //                }
    //                if (recordyn != 0)
    //                {
    //                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Order Placed Successfully");
    //                }
    //            }
    //            if (dt1 != null) { dt1.Dispose(); }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lbldModalMsgPreDemand.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Previous Demand : " + ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myRetailerListModal()", true);

    //    }
    //}
    //Insert multidemand for product()
    
}