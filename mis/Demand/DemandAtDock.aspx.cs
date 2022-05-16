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

public partial class mis_Demand_DemandAtDock : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds5, ds6, ds7 = new DataSet();
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
                if (objdb.Office_ID() == "4")
                {
                    divstatus.Visible = true;
                    chkstatus.Visible = true;
                }
                else
                {
                    chkstatus.Visible = false;
                    divstatus.Visible = false;
                }
                GetCategory();
                GetShift();
                GetLocation();
                GetVehicleNo();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "true");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Transportation ", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        ddlItemCategory.SelectedIndex = 0;
        ddlDistributor.SelectedIndex = 0;
        btnSubmit.Text = "Save";

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
    }
    private void GetItem()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string[] BRR_Id = ddlDistributor.SelectedValue.Split('-');
            ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "CreatedBy", "Office_ID" },
                       new string[] { "3", orderdate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, BRR_Id[0], objdb.Office_ID() }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {

                lblCartMsg.Text = "<i class='fa fa-cart-plus'></i> Cart details for " + ddlItemCategory.SelectedItem.Text;
                GridView1.DataSource = ds4.Tables[0];
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
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    private void GetDistributor()
    {
        try
        {
            lblMsg.Text = string.Empty;
            ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                     new string[] { "Flag", "Office_ID", "ItemCat_id", "AreaId" },
                       new string[] { "6", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            ddlDistributor.Items.Clear();
            if (ds3.Tables[0].Rows.Count != 0)
            {
                ddlDistributor.DataTextField = "BName";
                ddlDistributor.DataValueField = "BRRId";
                ddlDistributor.DataSource = ds3.Tables[0];
                ddlDistributor.DataBind();
                ddlDistributor.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDistributor.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
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

                    if (!string.IsNullOrEmpty(gvtxtQty.Text) && gvtxtQty.Text != "0")
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
                    string[] BRR_Id1 = ddlDistributor.SelectedValue.Split('-');

                    string Priyojna_status = "0";
                    if (chkstatus.Checked == true)
                    {
                        Priyojna_status = "1";
                    }
                    else
                    {
                        Priyojna_status = "0";
                    }

                    ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                           new string[] { "flag", "BoothId", "ItemCat_id", "Demand_Date", "Shift_id", "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType"
                                    , "Delivary_Date", "DelivaryShift_id", "RetailerTypeID", "RouteId","ProductDMStatus","VehicleMilkOrProduct_ID","Priyojna_status" },
                           new string[] { "5", BRR_Id1[0].ToString(),ddlItemCategory.SelectedValue, odat.ToString(), 
                             ddlShift.SelectedValue, objdb.UserTypeID(), objdb.Office_ID(), objdb.createdBy(), IPAddress, "1"
                             , odat.ToString(), ddlShift.SelectedValue,  BRR_Id1[2].ToString(),BRR_Id1[1].ToString(),ddlDMType.SelectedValue,ddlVehicleNo.SelectedValue ,Priyojna_status},
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
                               // lbldistributerMONO.Text = "8962389494";

                                Supmessage = "Your order " + Order_ID.ToString() + " for date " + odat.ToString() + " shift " + ddlShift.SelectedItem.Text + " has been placed successfully.";
                                link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + lbldistributerMONO.Text + "&text=" + Server.UrlEncode(Supmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162650796309365&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";


                                HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                Stream stream = response.GetResponseStream();
                            }
                       // }

                        string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success + " for " + ddlDistributor.SelectedItem.Text);
                        ddlDistributor.SelectedIndex = 0;
                        pnlProduct.Visible = false;
                        pnlSubmit.Visible = false;
                        chkstatus.Checked = false;
                    }
                    else if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        string already = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Warning :" + already);
                        ddlDistributor.SelectedIndex = 0;
                    }
                    else
                    {
                        string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {

            GetDistributor();
        }

    }
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
        pnlClear.Visible = false;
    }
    #endregion=============end of button click funciton==================



    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            GetDistributor();
        }
    }
    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlProduct.Visible = true;

        string[] BRR_Id1 = ddlDistributor.SelectedValue.Split('-');
        ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                    new string[] { "Flag", "Office_ID", "BoothId", "RouteId", "RetailerTypeID", "AreaId", "ItemCat_id" },
                      new string[] { "10", objdb.Office_ID(), BRR_Id1[0], BRR_Id1[1], BRR_Id1[2],ddlLocation.SelectedValue,ddlItemCategory.SelectedValue }, "dataset");
        if (ds3.Tables.Count > 0)
        {
            if (ds3.Tables[0].Rows.Count > 0)
            {
                lbldistributerMONO.Text = ds3.Tables[0].Rows[0]["SSCPersonMobileNo"].ToString();
            }
        }

        GetItem();
    }
}