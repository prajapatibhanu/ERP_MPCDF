using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Net;


public partial class mis_Demand_CurrentDayMilkDMEntry : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4, ds5 = new DataSet();
    string orderdate = "", demanddate = "", currentdate = "", currrentime = "", deliverydat = "", predemanddate = ""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "true");
                GetLocation();
                GetShift();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Location", ex.Message.ToString());
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
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRoute()
    {
        try
        {
            if (ddlLocation.SelectedValue != "0")
            {
                ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                     new string[] { "7", objdb.Office_ID(), objdb.GetMilkCatId(), ddlLocation.SelectedValue }, "dataset");
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetRetailerTypeID()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Mst_BoothReg",
                     new string[] { "flag", "BoothId", "Office_ID" },
                       new string[] { "14", ddlRetailer.SelectedValue, objdb.Office_ID() }, "dataset");


            if (ds3.Tables[0].Rows.Count != 0)
            {
                ViewState["RetailerTypeID"] = "";
                ViewState["RetailerTypeID"] = ds3.Tables[0].Rows[0]["RetailerTypeID"].ToString();
                lblMsg.Text = string.Empty;
                pnlProduct.Visible = true;
                pnlClear.Visible = false;
                btnSubmit.Visible = true;

            }
            else
            {
                ViewState["RetailerTypeID"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", " RetailerTypeID not found.");
                pnlProduct.Visible = false;
                btnSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 15 ", ex.Message.ToString());
        }

        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }


    private void GetItem()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                    new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "CreatedBy", "Office_ID" },
                      new string[] { "3", orderdate.ToString(), objdb.GetShiftMorId(), objdb.GetMilkCatId(), ddlRetailer.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {

                lblCartMsg.Text = "<i class='fa fa-cart-plus fa-2x'></i> Cart details for Milk";
                GridView1.DataSource = ds2.Tables[0];
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }

    private void InsertOrder()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

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

                    ds5 = objdb.ByProcedure("USP_Trn_MilkCurrentDM",
                              new string[] { "flag", "BoothId", "ItemCat_id", "Demand_Date", "Shift_id", "Demand_Status"
                                    , "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType"
                                    , "Delivary_Date", "DelivaryShift_id", "RetailerTypeID", "RouteId", "DistributorId","ProductDMStatus" },
                             new string[] { "1", ddlRetailer.SelectedValue, objdb.GetMilkCatId(), odat.ToString(), 
                             ddlShift.SelectedValue, "3", objdb.UserTypeID(), objdb.Office_ID(), 
                             objdb.createdBy(), IPAddress, "1", odat.ToString(), ddlShift.SelectedValue, ViewState["RetailerTypeID"].ToString(), 
                             ddlRoute.SelectedValue,"","1" },
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
                        //}
                        string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    pnlClear.Visible = true;
                    pnlProduct.Visible = false;
                    pnlSubmit.Visible = false;


                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    
    #region============ button click event ============================
    
    protected void lnkAddDemand_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtOrderDate.Text != "" && ddlRetailer.SelectedValue != "0" && ddlRoute.SelectedValue != "0")
            {
                GetItem();
                lblMsg.Text = string.Empty;

            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertOrder();

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

        lblMsg.Text = string.Empty;
        pnlSubmit.Visible = false;
        pnlProduct.Visible = false;
        pnlClear.Visible = false;
    }
    #endregion=============end of button click funciton==================


    
    protected void GetRetailer()
    {
        try
        {

            ddlRetailer.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "RouteId", "Office_ID" },
                     new string[] { "13", ddlRoute.SelectedValue, objdb.Office_ID() }, "dataset");
            ddlRetailer.DataTextField = "BoothName";
            ddlRetailer.DataValueField = "BoothId";
            ddlRetailer.DataBind();
            ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5:", ex.Message.ToString());
        }
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoute.SelectedValue != "0")
        {
            GetRetailer();
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            GetRoute();
        }
    }
    protected void ddlRetailer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlRetailer.SelectedValue!="0")
        {
            GetRetailerTypeID();
            ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                 new string[] { "Flag", "Office_ID", "DistributorId" },
                   new string[] { "11", objdb.Office_ID(), ddlRetailer.SelectedValue }, "dataset");
            if (ds3.Tables.Count > 0)
            {
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    lbldistributerMONO.Text = ds3.Tables[0].Rows[0]["DCPersonMobileNo"].ToString();
                }
            }
        }
       
    }
}