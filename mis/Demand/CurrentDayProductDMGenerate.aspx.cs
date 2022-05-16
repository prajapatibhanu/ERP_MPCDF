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
using System.Net;
using System.Diagnostics;


public partial class mis_Demand_CurrentDayProductDMGenerate : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds9, ds5, ds6, ds7 = new DataSet();
    int totalmilkqty = 0, totalcrate = 0, totalBox = 0;
    double Amount = 0, totalQtyInLtr = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = Date;
				 txtPODate.Text = Date;
                txtDate.Attributes.Add("readonly", "true");
                GetShift();
                GetLocation();
                GetRoute();
                GetVendorType();
                // GetVehicleNo();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtDate.Attributes.Add("readonly", "true");
                if (Request.QueryString["DSA"] != null && Request.QueryString["DSR"] != null && Request.QueryString["DSD"] != null && Request.QueryString["DSS"] != null && Request.QueryString["DSMPId"] != null && Request.QueryString["DSDMStatus"] != null)
                {
                    GetDMDetailsByViewChallanList();
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
string Date = DateTime.Now.ToString("dd/MM/yyyy");
        txtPODate.Text = Date;
        pnldata.Visible = false;
        ddlVehicleNo.SelectedIndex = 0;

        GridView1.DataSource = null;
        GridView1.DataBind();
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
    private void InvisibeDistOrSS()
    {
        if (ddlRoute.SelectedValue != "0")
        {
            pnldistss.Visible = true;
            pnlorderno.Visible = true;
            ddlDitributor.SelectedIndex = 0;
        }
        else
        {
            ddlDitributor.SelectedIndex = 0;
            pnldistss.Visible = false;
            pnlorderno.Visible = false;
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
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }

    //private void GetRoute()
    //{
    //    try
    //    {
    //        ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //             new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
    //             new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
    //        ddlRoute.DataTextField = "RName";
    //        ddlRoute.DataValueField = "RouteId";
    //        ddlRoute.DataBind();
    //        ddlRoute.Items.Insert(0, new ListItem("All", "0"));



    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    private void GetRoute()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ddlRoute.DataSource = objdb.ByProcedure("USP_Trn_ProductDM",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id", "Shift_id", "ProductDMStatus", "Demand_Date" },
                 new string[] { "17", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId(), ddlShift.SelectedValue, ddlDMType.SelectedValue, odat.ToString() }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("All", "0"));

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

                ds3 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                       new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                         new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue, objdb.GetProductCatId() }, "dataset");

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    ddlDitributor.DataTextField = "DTName";
                    ddlDitributor.DataValueField = "DistributorId";
                    ddlDitributor.DataSource = ds3.Tables[0];
                    ddlDitributor.DataBind();
                }
                else
                {
                    ddlDitributor.Items.Clear();
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
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    private void GetOrderNo()
    {
        try
        {
            if (txtDate.Text != "" && ddlRoute.SelectedValue != "0")
            {
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                      new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteId", "Office_ID", "ProductDMStatus" },
                        new string[] { "2", odat, ddlShift.SelectedValue, objdb.GetProductCatId(), ddlRoute.SelectedValue, objdb.Office_ID(), ddlDMType.SelectedValue }, "dataset");

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
    private void GetDMDetails()
    {
        try
        {
            lblMsg.Text = "";
            DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteID", "MilkOrProductDemandId", "Office_ID", "ProductDMStatus" },
                     new string[] { "3", deliverydate, ddlShift.SelectedValue, objdb.GetProductCatId(), ddlRoute.SelectedValue, ddlOrderNo.SelectedValue, objdb.Office_ID(), ddlDMType.SelectedValue }, "dataset");

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
                lblMsg.Text = objdb.Alert("fa-Warning", "alert-warning", "Warning! :", "No Record Found.");
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
    private void GetDMDetailsByViewChallanList()
    {
        try
        {

            lblMsg.Text = "";
            string redirectedAreaID = objdb.Decrypt(Request.QueryString["DSA"].ToString());
            string redirectedRouteId = objdb.Decrypt(Request.QueryString["DSR"].ToString());
            string redirectedDeliverDate = objdb.Decrypt(Request.QueryString["DSD"].ToString());
            string redirectedDeliverShiftid = objdb.Decrypt(Request.QueryString["DSS"].ToString());
            string redirectedPMId = objdb.Decrypt(Request.QueryString["DSMPId"].ToString());
            string redirectedDMstatuss = objdb.Decrypt(Request.QueryString["DSDMStatus"].ToString());

            txtDate.Text = redirectedDeliverDate;
            ddlDMType.SelectedValue = redirectedDMstatuss;
            ddlLocation.SelectedValue = redirectedAreaID;
            ddlShift.SelectedValue = redirectedDeliverShiftid;

            GetRoute();
            ddlRoute.SelectedValue = redirectedRouteId;
            InvisibeDistOrSS();
            GetDisOrSSByRouteID();
            GetOrderNo();
            ddlOrderNo.SelectedValue = redirectedPMId;
            GetDMDetails();
            GetTcsTax();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    private void InsertVehicleChallan()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                ds1 = objdb.ByProcedure("USP_Trn_Paymentandsecuritycompare",
                   new string[] { "Flag", "RouteId", "Office_ID", "DistributorId" },
                   new string[] { "1", ddlRoute.SelectedValue, objdb.Office_ID(), ddlDitributor.SelectedValue }, "dataset");
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        decimal Paiamount = decimal.Parse(ds1.Tables[0].Rows[0]["Opening"].ToString()) + decimal.Parse(ViewState["TAmount"].ToString());
                        //if (Paiamount <= decimal.Parse(ds1.Tables[0].Rows[0]["SecurityDeposit"].ToString()))
                        //{
                        lblMsg.Text = "";
                        DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                        //DateTime intime = Convert.ToDateTime(txtInTime.Text.Trim());
                        //string it = intime.ToString("hh:mm tt");
                        //DateTime outtime = Convert.ToDateTime(txtOutTime.Text.Trim());
                        //string ot = outtime.ToString("hh:mm tt");
                        string dat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        string IPAddress = Request.UserHostAddress;
                        if (btnSubmit.Text == "Save")
                        {
                            lblMsg.Text = "";
                            DataTable dtInsertChild = new DataTable();
                            DataRow drIC;

                            dtInsertChild.Columns.Add("Item_id", typeof(int));
                            dtInsertChild.Columns.Add("TotalQty", typeof(int));
                            dtInsertChild.Columns.Add("IssueCrate", typeof(int));
                            dtInsertChild.Columns.Add("ItemRate", typeof(decimal));
                            dtInsertChild.Columns.Add("RateincludingGST", typeof(decimal));
                            dtInsertChild.Columns.Add("IntegratedTax", typeof(decimal));
                            dtInsertChild.Columns.Add("CGST", typeof(decimal));
                            dtInsertChild.Columns.Add("SGST", typeof(decimal));
                            dtInsertChild.Columns.Add("CGSTAmt", typeof(decimal));
                            dtInsertChild.Columns.Add("SGSTAmt", typeof(decimal));
                            dtInsertChild.Columns.Add("Amount", typeof(decimal));
                            dtInsertChild.Columns.Add("IssueBox", typeof(int));
                            dtInsertChild.Columns.Add("QtyInLtr", typeof(decimal));
							dtInsertChild.Columns.Add("ExtraShort", typeof(Int16)); 
                            drIC = dtInsertChild.NewRow();
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                Label lblItem_id = (Label)row.FindControl("lblItem_id");
                                Label lblQty = (Label)row.FindControl("lblQty");
                                Label lblCrate = (Label)row.FindControl("lblCrate");
                                Label lblRate = (Label)row.FindControl("lblRate");
                                Label lblRateincludingGST = (Label)row.FindControl("lblRateincludingGST");
                                Label lblIntegratedTax = (Label)row.FindControl("lblIntegratedTax");
                                Label lblCGST = (Label)row.FindControl("lblCGST");
                                Label lblSGST = (Label)row.FindControl("lblSGST");
                                Label lblCGSTAmt = (Label)row.FindControl("lblCGSTAmt");
                                Label lblSGSTAmt = (Label)row.FindControl("lblSGSTAmt");
                                Label lblAmount = (Label)row.FindControl("lblAmount");
                                TextBox txtFTotalCrate = (TextBox)row.FindControl("txtFTotalCrate");
                                TextBox txtFTotalBox = (TextBox)row.FindControl("txtFTotalBox");
                                Label lblBox = (Label)row.FindControl("lblBox");
                                Label lblQtyInLtr = (Label)row.FindControl("lblQtyInLtr");
								Label lblExtraShort = (Label)row.FindControl("lblExtraShort");
                                drIC[0] = lblItem_id.Text;
                                drIC[1] = lblQty.Text;
                                drIC[2] = lblCrate.Text;
                                drIC[3] = lblRate.Text;
                                drIC[4] = lblRateincludingGST.Text;
                                drIC[5] = lblIntegratedTax.Text;
                                drIC[6] = lblCGST.Text;
                                drIC[7] = lblSGST.Text;
                                drIC[8] = lblCGSTAmt.Text;
                                drIC[9] = lblSGSTAmt.Text;
                                drIC[10] = lblAmount.Text;
                                drIC[11] = lblBox.Text;
                                drIC[12] = lblQtyInLtr.Text;
								drIC[13] = lblExtraShort.Text;

                                dtInsertChild.Rows.Add(drIC.ItemArray);
                            }
                            if (dtInsertChild.Rows.Count > 0)
                            {
                                if (ViewState["TAmount"].ToString() != "0" && ViewState["TAmount"].ToString() != "" && ViewState["Tval"].ToString() != "")
                                {
                                    ViewState["TotalIssueCrate"] = "";
                                    ViewState["TotalIssueBox"] = "";
                                    ViewState["TotalQtyInLtr"] = "";
                                    string TICrate = (GridView1.FooterRow.FindControl("txtFTotalCrate") as TextBox).Text;
                                    string TIBox = (GridView1.FooterRow.FindControl("txtFTotalBox") as TextBox).Text;
                                    string TIInLtr = (GridView1.FooterRow.FindControl("txtFQtyInLtre") as TextBox).Text;
                                    ViewState["TotalIssueCrate"] = TICrate;
                                    ViewState["TotalIssueBox"] = TIBox;
                                    ViewState["TotalQtyInLtr"] = TIInLtr;
                                    if (txtPODate.Text == "") { txtPODate.Text = "01/01/1900"; }
                                    DateTime pod1 = DateTime.ParseExact(txtPODate.Text, "dd/MM/yyyy", culture);
                                    string ppod1 = pod1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                                    ds2 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_Insert",
                                                         new string[] { "flag","AreaId", "RouteId", "ItemCat_id", "DeliveryShift_id",
                                                     "Delivary_Date","VehicleMilkOrProduct_ID","TotalIssueCrate",
                                                     "Office_ID","CreatedBy", "IPAddress","DistributorId",
                                                     "ProductDMStatus","MilkOrProductDemandId","TcsTaxPer",
                                                     "TotalIssueBox","TotalQtyInLtr","PONumber" ,"PODdate","CreatedRemark","EMP_Name" },
                                                         new string[] { "1",ddlLocation.SelectedValue, ddlRoute.SelectedValue, 
                                                     objdb.GetProductCatId(), ddlShift.SelectedValue, dat, 
                                                ddlVehicleNo.SelectedValue, ViewState["TotalIssueCrate"].ToString()
                                                ,objdb.Office_ID(),objdb.createdBy(), IPAddress,ddlDitributor.SelectedValue
                                                ,ddlDMType.SelectedValue,ddlOrderNo.SelectedValue,ViewState["Tval"].ToString(),
                                                ViewState["TotalIssueBox"].ToString(),ViewState["TotalQtyInLtr"].ToString()
                                                ,txtPONumber.Text.Trim(),ppod1.ToString(),txtRemark.Text.Trim(),txtname.Text.Trim()
                                                 }, "type_ProductDispDeliveryChallanChild", dtInsertChild, "TableSave");

                                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "SUCCESS")
                                    {
                                        //gheestock updation
                                        if (ds2.Tables.Count > 2)
                                        {
                                            if (ds2.Tables[1].Rows.Count > 0 && ds2.Tables[2].Rows.Count > 0)
                                            {
                                                var httpWebRequestP = (HttpWebRequest)WebRequest.Create("http://45.114.143.215:8202//api/data/PostGheeStock");
                                               // var httpWebRequestP = (HttpWebRequest)WebRequest.Create("http://localhost:54453/api/data/PostGheeStock");
                                                httpWebRequestP.ContentType = "application/json; charset=utf-8";
                                                httpWebRequestP.Method = "POST";

                                                string DistributorID = "", GatePass_No = "", Vehicle_No = "", GrandTotal = "", DMID = "", DMNO = "";

                                                DistributorID = ds2.Tables[2].Rows[0]["DistributorID"].ToString();
                                                GatePass_No = ds2.Tables[2].Rows[0]["GatePass_No"].ToString();
                                                Vehicle_No = ds2.Tables[2].Rows[0]["Vehicle_No"].ToString();
                                                GrandTotal = ds2.Tables[2].Rows[0]["GrandTotal"].ToString();
                                                DMNO = (Convert.ToDateTime(dat.ToString(), cult).ToString("yyyy/MM/dd")).ToString();

                                                using (var streamWriter = new StreamWriter(httpWebRequestP.GetRequestStream()))
                                                {
                                                    string json = "{\"DS_Office_ID\":\"" + objdb.Office_ID() + "\"," +
                                                                   "\"StockDate\":\"" + dat.ToString() + "\"," +
                                                                  "\"UpdatedBy\":\"" + objdb.createdBy() + "\"," +
                                                                  "\"dataOperation\":\"CREATE_Parent\"," +
                                                                  "\"DistributorID\":\"" + DistributorID + "\"," +
                                                                  "\"GatePass_No\":\"" + GatePass_No + "\"," +
                                                                  "\"Vehicle_No\":\"" + Vehicle_No + "\"," +
                                                                  "\"DMNO\":\"" + DMNO + "\"," +
                                                                  "\"GrandTotal\":\"" + GrandTotal + "\"}";


                                                    streamWriter.Write(json);
                                                    Debug.Write(json);
                                                    streamWriter.Write(json);
                                                    streamWriter.Flush();
                                                    streamWriter.Close();
                                                }

                                                var httpResponseP = (HttpWebResponse)httpWebRequestP.GetResponse();
                                                using (var streamReader = new StreamReader(httpResponseP.GetResponseStream()))
                                                {
                                                    var result = streamReader.ReadToEnd();
                                                    //DMID = 
                                                }

                                                string ItemID = "", ItemQty = "", NetTotalValue = "", ItemRate = "";
                                                for (int j = 0; j < ds2.Tables[1].Rows.Count; j++)
                                                {
                                                    ItemID = ds2.Tables[1].Rows[j]["item_id"].ToString();
                                                    ItemQty = ds2.Tables[1].Rows[j]["TotalQty"].ToString();
                                                    NetTotalValue = ds2.Tables[1].Rows[j]["Amount"].ToString();
                                                    ItemRate = ds2.Tables[1].Rows[j]["ItemRate"].ToString();



                                                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://45.114.143.215:8202//api/data/PostGheeStock");
                                                   // var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:54453/api/data/PostGheeStock");
                                                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                                                    httpWebRequest.Method = "POST";

                                                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                                    {
                                                        string json = "{\"Vehicle_No\":\"" + Vehicle_No + "\"," +
                                                                      "\"GatePass_No\":\"" + GatePass_No + "\"," +
                                                                      "\"dataOperation\":\"CREATE_Child\"," +
                                                                      "\"ItemID\":\"" + ItemID + "\"," +
                                                                      "\"ItemQty\":\"" + ItemQty + "\"," +
                                                                      "\"NetTotalValue\":\"" + NetTotalValue + "\"," +
                                                                      "\"ItemRate\":\"" + ItemRate + "\"}";


                                                        streamWriter.Write(json);
                                                        Debug.Write(json);
                                                        streamWriter.Write(json);
                                                        streamWriter.Flush();
                                                        streamWriter.Close();
                                                    }

                                                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                                    {
                                                        var result = streamReader.ReadToEnd();
                                                    }



                                                }

                                            }
                                        }

                                        //if (objdb.Office_ID() == "2")
                                        //{
                                        get_distributor_mobileno();
                                        if (lbldistributerMONO.Text != "0000000000" && lbldistributerMONO.Text != "9999999999" && lbldistributerMONO.Text != null && lbldistributerMONO.Text != "")
                                        {
                                            if (decimal.Parse(ViewState["TAmount"].ToString()) > 0)
                                            {
                                                string Supmessage = "";
                                                string link = "";
                                                string Invoice_No = ds2.Tables[0].Rows[0]["DMChallanNo"].ToString();
                                                ServicePointManager.Expect100Continue = true;
                                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                                //lbldistributerMONO.Text = "8962389494";

                                                Supmessage = "Your bill for order " + Invoice_No.ToString() + " date " + txtDate.Text + " shift " + ddlShift.SelectedItem.Text + " has been generated for amount " + ViewState["TAmount"].ToString() + " rs. Kindly make payment on time.";
                                                link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + lbldistributerMONO.Text + "&text=" + Server.UrlEncode(Supmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162650801393194&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";


                                                HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                                                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                                Stream stream = response.GetResponseStream();
                                            }
                                        }
                                        //}
                                        string success = ds2.Tables[0].Rows[0]["Msg"].ToString();
                                        CClear();
                                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                        dtInsertChild.Dispose();
                                        GetChallanDetails(ds2.Tables[0].Rows[0]["rowid"].ToString());

                                        pnldata.Visible = false;
                                        pnlvehicledetail.Visible = false;
                                        GridView1.DataSource = null;
                                        GridView1.DataBind();
                                    }
                                    else
                                    {
                                        string msg = ds2.Tables[0].Rows[0]["Msg"].ToString();
                                        if (msg == "Already")
                                        {
                                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Record " + msg + " exists for Date : " + txtDate.Text + " and Route : " + ddlRoute.SelectedItem.Text);
                                        }
                                        else
                                        {
                                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
                                        }
                                    }
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Amount Can't be Empty or Zero, please set Product Rate first . ");
                                    return;
                                }
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Please enter valid Qty and select Vehicle. ");
                                return;
                            }



                        }

                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        //}
                        //else
                        //{
                        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Warning :" + "Security Amount Should be greater than or equal to Total Payable Amount");
                        //}
                    }
                }
            }
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
    private void get_distributor_mobileno()
    {

        ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                                     new string[] { "Flag", "Office_ID", "DistributorId" },
                                     new string[] { "11", objdb.Office_ID(), ddlDitributor.SelectedValue }, "dataset");
        if (ds3.Tables.Count > 0)
        {
            if (ds3.Tables[0].Rows.Count > 0)
            {
                lbldistributerMONO.Text = ds3.Tables[0].Rows[0]["DCPersonMobileNo"].ToString();
            }
        }


    }
    private void GetChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
                     new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
                     new string[] { "0", objdb.Office_ID(), cid }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                StringBuilder sb = new StringBuilder();
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');
                sb.Append("<div class='invoice'>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td  style='text-align:center:'> <b>GSTIN NO: " + ds3.Tables[0].Rows[0]["Office_Gst"].ToString() + "</b></td>");
                if (objdb.Office_ID() == "5")
                {
                    sb.Append("<td  style='text-align:center:'> <b>FSSAI Lic No.: 10013026000522</b></td>");
                    sb.Append("<td colspan='1' style='text-align:right:'><b> Phone No: " + ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='2' style='text-align:right:'><b> Phone No: " + ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString() + "</b></td>");
                }
                    sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td><b>Date</b>&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='font-size:17px;'><b>" + Dairyplant[0] + " </b></td>");
                sb.Append("<td><b>" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr></tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'><span style='font-weight:600;'>D.M Cum/ Gate Pass no." + ds3.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</span></td>");
                sb.Append("<td style='font-size:17px;'><b> To=>" + ds3.Tables[0].Rows[0]["DName"].ToString() + "</b></td>");
                sb.Append("<td><b>Vehicle No:</b> &nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                sb.Append("</tr>");
                // sb.Append("<tr>");
                //sb.Append("<td colspan='3' style='text-align:left'>सुपरवाइजर का नाम&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["SupervisorName"].ToString() + "</span></td>");
                // sb.Append("</tr>");
                sb.Append("<tr>");
                // sb.Append("<td style='text-align:left'>आने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleIn_Time"].ToString() + "</span></td>");
                sb.Append("<td><b>Route No:</b> &nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["RName"].ToString() + "</span></td>");
                //  sb.Append("<td>जाने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</span></td>");
                sb.Append("<td style='font-size:17px;'><b> Party GST No : " + ds3.Tables[0].Rows[0]["partygstn"].ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:center'>Please receive the follwing goods and acknowledge</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='table table1-bordered'>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center'>Name of Product</th>");
                sb.Append("<th style='text-align:center'>Unit</th>");
                sb.Append("<th style='text-align:center'>QTY(In Ltr/KG)</th>");
                sb.Append("<th style='text-align:center'>Crate</th>");
                sb.Append("<th style='text-align:center'>Box/Jar</th>");
				if (objdb.Office_ID() == "5")
                {
                    sb.Append("<th style='text-align:center'>Extra/Short Pkt</th>");
                }
                sb.Append("<th style='text-align:center'>Rate</th>");
                sb.Append("<th style='text-align:center'>Amount</th>");
                sb.Append("</tr>");
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                double TotalAmt = 0;
                double TCSTAX_Amt = 0, FinalAmt_withTCSTax = 0;
                for (int i = 0; i < Count; i++)
                {
                    TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["SupplyQty"].ToString());
                    // TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                    TotalAmt += Double.Parse(ds3.Tables[0].Rows[i]["Amount"].ToString());
                    sb.Append("<tr>");
                    sb.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["QtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'></td>");
                    //sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueCrate"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueBox"].ToString() + "</td>");
					 if (objdb.Office_ID() == "5")
                    {
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["ExtraShort"].ToString() + "</td>");
                    }
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["RateincludingGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["Amount"].ToString() + "</td>");
                    sb.Append("</tr>");

                }
                TCSTAX_Amt = ((TotalAmt * Convert.ToDouble(ds3.Tables[0].Rows[0]["TcsTaxPer"])) / 100);
                FinalAmt_withTCSTax = TotalAmt + TCSTAX_Amt;
                sb.Append("<tr>");
                sb.Append("<td><b>Total</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + ds3.Tables[0].Rows[0]["TotalQtyInLtr"].ToString() + "</b></td>");
                if (objdb.Office_ID() != "2")
                {
                    sb.Append("<td style='text-align:center'><b>" + ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td></td>");
                }
                sb.Append("<td style='text-align:center'><b>" + ds3.Tables[0].Rows[0]["TotalIssueBox"].ToString() + "</b></td>");
				 if (objdb.Office_ID() == "5")
                {
                    sb.Append("<td></td>");
                }
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:center'><b>" + TotalAmt.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Tcs on Sales @ " + (ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
				if (objdb.Office_ID() == "5")
                {
                    sb.Append("<td></td>");
                }
                sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX_Amt.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
				if (objdb.Office_ID() == "5")
                {
                    sb.Append("<td></td>");
                }
                sb.Append("<td style='text-align:center'><b>" + FinalAmt_withTCSTax.ToString("0.00") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                //sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वाहन सुपरवाइजर</span></td>");
                //sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                //sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                sb.Append("<td colspan='3' <span style='text-align:right; padding-top:20px; font-weight:700;'>GM/AGM/SPO/DC</br>&nbsp;&nbsp;(MKIG)&nbsp;&nbsp;</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='width:100%;' ></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() != "2")
                {
                    sb.Append("<td style='text-align:left'>Product received as per<br>above details along with <b>  " + ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString() + " </b><br><br> Receiver Signature<br>(Party)</td>");
                }
                else
                {
                    sb.Append("<td style='text-align:left'>Product received as per<br>above details along with crates<br><br> Receiver Signature<br>(Party)</td>");
                }
                sb.Append("<td >&nbsp;</td>");
                sb.Append("<td style='text-align:right'>Issued By:<br><br><br><br> GM/AGM/MGR<br>(Product Section)</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
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
            if (ds3 != null) { ds3.Dispose(); }
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
        }
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        InvisibeDistOrSS();
        GetDisOrSSByRouteID();
        GetOrderNo();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMilkQty = (e.Row.FindControl("lblQty") as Label);
                Label lblCrate = (e.Row.FindControl("lblCrate") as Label);
                Label lblBox = (e.Row.FindControl("lblBox") as Label);
                Label lblQtyInLtr = (e.Row.FindControl("lblQtyInLtr") as Label);
                Label lblAmount = (e.Row.FindControl("lblAmount") as Label);
                totalmilkqty += Convert.ToInt32(lblMilkQty.Text);
                totalQtyInLtr += Convert.ToDouble(lblQtyInLtr.Text);
                totalcrate += Convert.ToInt32(lblCrate.Text);
                totalBox += Convert.ToInt32(lblBox.Text);
                Amount += Convert.ToDouble(lblAmount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ViewState["TotalIssueQty"] = "";
                //  ViewState["TotalIssueCrate"] = "";
                ViewState["TAmount"] = "";
                Label lblFTotalMilkQty = (e.Row.FindControl("lblFTotalMilkQty") as Label);
                TextBox txtFTotalCrate = (e.Row.FindControl("txtFTotalCrate") as TextBox);
                TextBox txtFTotalBox = (e.Row.FindControl("txtFTotalBox") as TextBox);
                TextBox txtFQtyInLtre = (e.Row.FindControl("txtFQtyInLtre") as TextBox);
                Label lblFAmount = (e.Row.FindControl("lblFAmount") as Label);

                lblFTotalMilkQty.Text = totalmilkqty.ToString();
                txtFQtyInLtre.Text = totalQtyInLtr.ToString("0.00");
                txtFTotalCrate.Text = totalcrate.ToString();
                txtFTotalBox.Text = totalBox.ToString();
                lblFAmount.Text = Amount.ToString("0.00");
                ViewState["TotalIssueQty"] = lblFTotalMilkQty.Text;
                // ViewState["TotalIssueCrate"] = txtFTotalCrate.Text;
                ViewState["TAmount"] = lblFAmount.Text;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
    }
    #endregion============ end of changed event for controls===========
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetDMDetails();
            GetTcsTax();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        CClear();
        pnlvehicledetail.Visible = false;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;

        ddlDitributor.SelectedIndex = 0;
        pnldistss.Visible = false;
        pnlorderno.Visible = false;
        string url = "CurrentDayProductDMGenerate.aspx";
        Response.Redirect(url);

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertVehicleChallan();
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

    private void GetTcsTax()
    {
        try
        {
            if (ddlDitributor.SelectedValue != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string deld = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds7 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deld, ddlDitributor.SelectedValue }, "dataset");

                if (ds7.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds7.Tables[0].Rows[0]["Tval"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
        }
        finally
        {
            if (ds7 != null) { ds7.Dispose(); }
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetRoute();
    }

    public IFormatProvider cult { get; set; }
}