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
using System.Web.Services;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;

public partial class mis_DemandSupply_GenerateProductInvoice_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds5, ds6, ds7, ds9, dsInvo = new DataSet();
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    int totalmilkqty = 0, totalcrate = 0, totalBox = 0, totalprodqty = 0;
    double Amount = 0, totalQtyInLtr = 0;
    string multi_demandId = "", multi_Distid = "";
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = Date;
                txtDate.Attributes.Add("readonly", "false");
                GetShift();
                GetDistributor();
                GetOfficeDetails();
              
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
    protected void GetOfficeDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_GST"] = ds2.Tables[0].Rows[0]["Office_Gst"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
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
    protected void GetDistributor()
    {
        try
        {
            pnloderdetails.Visible = false;
            pnldata.Visible = false;
            DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string ddate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            if(objdb.Office_ID()=="6")
            {
                ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                new string[] { "9", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetProductCatId(), ddate.ToString() }, "dataset");
            }
            else
            {
                ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                new string[] { "6", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetProductCatId(), ddate.ToString() }, "dataset");
             }
            
            ddlDistributorName.Items.Clear();
            if (ds7.Tables[0].Rows.Count > 0)
            {
                ddlDistributorName.DataTextField = "BName";
                ddlDistributorName.DataValueField = "BName";
                ddlDistributorName.DataSource = ds7.Tables[0];
                ddlDistributorName.DataBind();
                ddlDistributorName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDistributorName.Items.Insert(0, new ListItem("No Record Found.", "0"));
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
    private void GetOrderNo()
    {
        try
        {
            if (txtDate.Text != "" && ddlShift.SelectedValue != "0")
            {
                lblMsg.Text = string.Empty;
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                      new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "BName", "Office_ID" },
                        new string[] { "7", odat, ddlShift.SelectedValue, objdb.GetProductCatId(), ddlDistributorName.SelectedItem.Text, objdb.Office_ID() }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    pnloderdetails.Visible = true;
                    pnldata.Visible = false;
                    GridViewOrderDetails.DataSource = ds5.Tables[0];
                    GridViewOrderDetails.DataBind();

                }
                else
                {
                    GridViewOrderDetails.DataSource = null;
                    GridViewOrderDetails.DataBind();
                    pnloderdetails.Visible = false;
                    pnldata.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
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
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetDistributor();
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistributor();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetOrderNo();
        //get_distributor_mobileno();
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GetDMDetails();
            GetTcsTax();
            CalculateTCSTAx();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnldata.Visible = false;
        pnloderdetails.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        btnClear.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertProdInvoice();
        }
    }
    #endregion========================================================


    #region====================Code for Milk Details=========================
    private void GetDMDetails()
    {
        try
        {
            lblMsg.Text = "";
            DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string areaid="",routeid = "",distid="",vehicleid="",tmpMilkOrProductDemandId="",ssid="";
            foreach (GridViewRow gridrow in GridViewOrderDetails.Rows)
            {

                CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");

                if (chkSelect.Checked == true)
                {
                    
                Label lblMilkOrProductDemandId = (Label)gridrow.FindControl("lblMilkOrProductDemandId");
                Label lblRouteId = (Label)gridrow.FindControl("lblRouteId");
                Label lblDistributorId = (Label)gridrow.FindControl("lblDistributorId");
                Label lblAreaId = (Label)gridrow.FindControl("lblAreaId");
                Label lblVehicleMilkOrProduct_ID = (Label)gridrow.FindControl("lblVehicleMilkOrProduct_ID");
                Label lblSuperStockistId = (Label)gridrow.FindControl("lblSuperStockistId"); 

                routeid = lblRouteId.Text;
                distid = lblDistributorId.Text;
                tmpMilkOrProductDemandId = lblMilkOrProductDemandId.Text;
                areaid = lblAreaId.Text;
                vehicleid = lblVehicleMilkOrProduct_ID.Text;
                ssid = lblSuperStockistId.Text;
                }
            }
            ViewState["tmpSSId"] = ssid.ToString();
            ViewState["tmpDistId"] = distid.ToString();
            ViewState["tmpAreaId"] = areaid.ToString();
            ViewState["tmpRouteId"] = routeid.ToString();
            ViewState["tmpRouteId"] = routeid.ToString();
            ViewState["tmpvehicleid"] = vehicleid.ToString();
            ViewState["tmpDemandId"] = tmpMilkOrProductDemandId.ToString(); 
            ds1 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "RouteID", "MilkOrProductDemandId", "Office_ID" },
                     new string[] { "14", deliverydate, ddlShift.SelectedValue, objdb.GetProductCatId(), routeid.ToString(), tmpMilkOrProductDemandId.ToString(), objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
               
                pnldata.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-Warning", "alert-warning", "Warning! :", "No Record Found.");
                pnldata.Visible = false;
                pnloderdetails.Visible = false;
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
    private void InsertProdInvoice()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
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
                           
                            ds2 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_Insert",
                                 new string[] { "flag","AreaId", "RouteId", "ItemCat_id", "DeliveryShift_id", "Delivary_Date"
                                  ,"VehicleMilkOrProduct_ID","TotalIssueCrate","Office_ID","CreatedBy", "IPAddress"
                                  ,"DistributorId","MilkOrProductDemandId"
                                 ,"TcsTaxPer","TotalIssueBox","TotalQtyInLtr","SuperStockistId","CreatedRemark" },
                                 new string[] { "3",ViewState["tmpAreaId"].ToString(), ViewState["tmpRouteId"].ToString(),
                                     objdb.GetProductCatId(),ddlShift.SelectedValue, dat, ViewState["tmpvehicleid"].ToString(),
                                     ViewState["TotalIssueCrate"].ToString(),objdb.Office_ID(),objdb.createdBy()
                                     , IPAddress,ViewState["tmpDistId"].ToString(),ViewState["tmpDemandId"].ToString(),ViewState["Tval"].ToString(),
                                     ViewState["TotalIssueBox"].ToString(),ViewState["TotalQtyInLtr"].ToString(),
                                 ViewState["tmpSSId"].ToString(),txtRemark.Text.Trim()}, "type_ProductDispDeliveryChallanChild", dtInsertChild, "TableSave");

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


                                string success = ds2.Tables[0].Rows[0]["Msg"].ToString();

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

                                            Supmessage = "Your bill for order " + Invoice_No.ToString() + " date " + dat.ToString() + " shift " + ddlShift.SelectedItem.Text + " has been generated for amount " + ViewState["TAmount"].ToString() + " rs. Kindly make payment on time.";
                                            link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + lbldistributerMONO.Text + "&text=" + Server.UrlEncode(Supmessage) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162650801393194&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=0&camp_name=mpmilk_user";


                                            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                                            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                            Stream stream = response.GetResponseStream();
                                        }
                                    }
                               // }
                               
                                
                                dtInsertChild.Dispose();
                                PrintInvoiceCumBillDetails(ds2.Tables[0].Rows[0]["rowid"].ToString());
                                GetOrderNo();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                pnldata.Visible = false;
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                            }
                            else
                            {
                                 string msg = ds2.Tables[0].Rows[0]["Msg"].ToString();
                                 lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
                               
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
        ds5 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                                     new string[] { "Flag", "Office_ID", "BName" },
                                     new string[] { "12", objdb.Office_ID(), ddlDistributorName.SelectedItem.Text }, "dataset");
        if (ds5.Tables.Count > 0)
        {
            if (ds5.Tables[0].Rows.Count > 0)
            {
                string BoothId, RouteId, RetailerTypeID, AreaId;

                BoothId = ds5.Tables[0].Rows[0]["BoothId"].ToString(); ;
                RouteId = ds5.Tables[0].Rows[0]["RouteId"].ToString(); ;
                RetailerTypeID = ds5.Tables[0].Rows[0]["RetailerTypeID"].ToString(); 
                AreaId=ds5.Tables[0].Rows[0]["AreaId"].ToString(); 

                 ds3 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                                              new string[] { "Flag", "Office_ID", "BoothId", "RouteId", "RetailerTypeID", "AreaId", "ItemCat_id" },
                                              new string[] { "10", objdb.Office_ID(), BoothId.ToString(), RouteId.ToString(), RetailerTypeID.ToString(), AreaId.ToString(),objdb.GetProductCatId() }, "dataset");
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        lbldistributerMONO.Text = ds3.Tables[0].Rows[0]["SSCPersonMobileNo"].ToString();
                    }
                }
            }
        }

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
                totalmilkqty = 0;
                totalQtyInLtr = 0;
                totalcrate = 0;
                totalBox = 0;
                Amount = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
    }

    private void PrintInvoiceCumBillDetails(string dmid)
    {
        try
        {
            dsInvo = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
                     new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
                     new string[] { "4", objdb.Office_ID(), dmid }, "dataset");

            if (dsInvo.Tables[0].Rows.Count > 0)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<h2 style='text-align:center;text-align: center;font-size: 20px; margin-bottom: 0px;'>Invoice-cum-Bill of Supply</h2>");
                sb.Append("<table class='table1' style='width:100%'>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</b></td>");
                sb.Append("<td colspan='3'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Delivery Note</td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Shift : <b>" + dsInvo.Tables[0].Rows[0]["ShiftName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Mode/Terms of Payment</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Supplier's Ref</td>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Gate Pass No <b>" + dsInvo.Tables[0].Rows[0]["GatePassNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Other Reference(s)</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["BCName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                else
                {
                    sb.Append("<td colspan='3' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>DM No. <b>" + dsInvo.Tables[0].Rows[0]["OrderId"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Buyer's Order No.</td>");
                }

                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Vehicle Out Time <b>" + dsInvo.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dated</td>");
                }

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3'>Dispatch Document No.</td>");
                sb.Append("<td colspan='2'>Delivery Note Date</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='3'>Vehicle No.</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td colspan='3'>Dispatched through</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
                }

                sb.Append("<td colspan='3'>Destination</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6'>Terms of Delivery</td>");

                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>S.No</td>");
                sb.Append("<td colspan='2' style='text-align:center;width:120px !important;'>Description of Goods</td>");
                sb.Append("<td style='text-align:center'>HSN / SAC</td>");
                sb.Append("<td style='text-align:center'>Quantity</td>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Per</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("</tr>");

                int TCount = dsInvo.Tables[0].Rows.Count;
                for (int i = 0; i < TCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td colspan='2'><b>" + dsInvo.Tables[0].Rows[i]["ItemName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSNCode"].ToString() + "</td>");
                    sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["SupplyQty"].ToString() + "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</b></td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["ItemRate"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'><b>" + dsInvo.Tables[0].Rows[i]["Amount"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                }
                decimal TAmount = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal TCSTAX = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TCSTaxAmt"));
                decimal TCGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CentralTax"));
                decimal TSGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("StateTax"));

                decimal Total = TAmount + TCGST + TSGST + TCSTAX;
                string Amount = GenerateWordsinRs(Total.ToString("0.00"));
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2'></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'>" + TAmount.ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>CGST</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>SGST</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Tcs on Sales @</b></td>");
                sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX.ToString() : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td></td>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total<b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + Total.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' rowspan='2' style='text-align:center'>HSN/SAC</td>");
                sb.Append("<td rowspan='2' style='text-align:center'>Taxable Value</td>");
                sb.Append("<td colspan='2' style='text-align:center'>Central Tax</td>");
                sb.Append("<td colspan='2' style='text-align:center'>State Tax</td>");
                sb.Append("<td rowspan='2' style='text-align:center'>Total Tax Amount</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("<td style='text-align:center'>Rate</td>");
                sb.Append("<td style='text-align:center'>Amount</td>");
                sb.Append("</tr>");
                int TCount1 = dsInvo.Tables[1].Rows.Count;
                for (int i = 0; i < TCount1; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'>" + dsInvo.Tables[1].Rows[i]["HSNCode"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TaxableValue"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["CGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["CentralTax"].ToString() + "</td>");
                    sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["SGST"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["StateTax"].ToString() + "</td>");
                    sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TotalTaxAmount"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
                string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:right'><b>Total</b></td>");
                sb.Append("<td style='text-align:right'><b>" + TTaxableValue.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
                sb.Append("<td style='text-align:right'><b>" + TotalTaxAmount.ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + "</br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (objdb.Office_ID() == "2")
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                else if (objdb.Office_ID() == "6")
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'></br></br>for " + ViewState["Office_Name"].ToString() + "</br>Bank Name :<b>" + dsInvo.Tables[0].Rows[0]["BankName"].ToString() + "</b></br>Branch  :<b> " + dsInvo.Tables[0].Rows[0]["Branch"].ToString() + "</b></br>IFSC Code : <b> " + dsInvo.Tables[0].Rows[0]["IFSC"].ToString() + "</b></br>A/C No :  <b>" + dsInvo.Tables[0].Rows[0]["BankAccountNo"].ToString() + "</b></br></br></br><span style='padding-left:150px; padding-top:1100px;text-align:left;'>Authorised Signatory</td>");
                }
                else
                {
                    sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
                }
                sb.Append("</tr>");

                sb.Append("</table>");
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
            if (dsInvo != null) { dsInvo.Dispose(); }
        }

    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
    private void GetTcsTax()
    {
        try
        {
            if (ViewState["tmpDistId"].ToString() != "" && ViewState["tmpDistId"].ToString()!="0")
            { 
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string deld = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds7 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deld, ViewState["tmpDistId"].ToString() }, "dataset");

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

    private void CalculateTCSTAx()
    {
        try
        {
            if(ViewState["TAmount"].ToString()!="" && ViewState["Tval"].ToString()!="")
            {
                decimal paybleAmt = Convert.ToDecimal(ViewState["TAmount"].ToString());
                lblTcsTax.Text = ViewState["Tval"].ToString();
                decimal tcstamt = ((paybleAmt * Convert.ToDecimal(ViewState["Tval"].ToString())) / 100);
                lblTcsTaxAmt.Text = tcstamt.ToString("0.00");
                decimal finalpaybleamt = paybleAmt + tcstamt;
                lblFinalPaybleAmount.Text = finalpaybleamt.ToString("0.00");
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : New Item is not mapped ,please mapped item from Item Section Mapping Menu. ", ex.Message.ToString());
        }
    }
    #endregion=================End of product Details===========================

    public IFormatProvider cult { get; set; }
}