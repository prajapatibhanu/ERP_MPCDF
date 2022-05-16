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

public partial class mis_Demand_GenerateMilkOrProductGatePassAtDock : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds5, ds6, ds7, ds8, ds9, dsv = new DataSet();
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
                txtDate.Attributes.Add("readonly", "true");
                GetShift();
                GetCategory();
                GetVehicleList();
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
            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    #region=========== User Defined function======================

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

    protected void GetVehicleList()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string ddate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            GridViewOrderDetails.DataSource = null;
            GridViewOrderDetails.DataBind();
            pnloderdetails.Visible = false;
            dsv = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                new string[] { "5", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddate.ToString() }, "dataset");
            ddlVehicleNoList.Items.Clear();
            if (dsv.Tables[0].Rows.Count > 0)
            {
                ddlVehicleNoList.DataTextField = "VehicleNo";
                ddlVehicleNoList.DataValueField = "VehicleNo";
                ddlVehicleNoList.DataSource = dsv.Tables[0];
                ddlVehicleNoList.DataBind();
                ddlVehicleNoList.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlVehicleNoList.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
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
                      new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "VehicleNo", "Office_ID" },
                        new string[] { "1", odat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlVehicleNoList.SelectedValue, objdb.Office_ID() }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    pnloderdetails.Visible = true;
                    GetVehicleNo();
                    GridViewOrderDetails.DataSource = ds5.Tables[0];
                    GridViewOrderDetails.DataBind();

                }
                else
                {
                    GridViewOrderDetails.DataSource = null;
                    GridViewOrderDetails.DataBind();
                    pnloderdetails.Visible = false;
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
        GetVehicleList();
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetVehicleList();
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetVehicleList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOrderNo();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnloderdetails.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iddata = 0;
            multi_demandId = ""; multi_Distid = "";
            DataRow dr;
            DataTable dtdistid = new DataTable();
            dtdistid.Columns.Add("MilkOrProductDemandId", typeof(string));
            dr = dtdistid.NewRow();
            int pariyojastatus_count = 0, checkedcount = 0;
            foreach (GridViewRow gridrow in GridViewOrderDetails.Rows)
            {

                CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");
                Label lblMilkOrProductDemandId = (Label)gridrow.FindControl("lblMilkOrProductDemandId");
                Label lblDistributorId = (Label)gridrow.FindControl("lblDistributorId");
                Label lblpariyojastatus = (Label)gridrow.FindControl("lblpariyojastatus");

                if (chkSelect.Checked == true)
                {
                    dr[0] = lblMilkOrProductDemandId.Text;
                    dtdistid.Rows.Add(dr.ItemArray);
                    ++iddata;
                    if (iddata == 1)
                    {
                        multi_demandId = lblMilkOrProductDemandId.Text;
                        multi_Distid = lblDistributorId.Text;
                    }
                    else
                    {
                        multi_demandId += "," + lblMilkOrProductDemandId.Text;
                        multi_Distid += "," + lblDistributorId.Text;
                    }

                    if (lblpariyojastatus.ToString() == "1")
                    {
                        pariyojastatus_count += 1;
                    }
                    checkedcount += 1;

                }
            }

            //sadhana 
            if (objdb.Office_ID() == "4")
            {
                if (pariyojastatus_count == checkedcount || pariyojastatus_count == 0)
                {
                    ViewState["MilkOrProductDemandId"] = dtdistid;

                    ViewState["multi_demandId"] = multi_demandId;
                    ViewState["multi_Distid"] = multi_Distid;

                    if (dtdistid.Rows.Count > 0)
                    {

                        dtdistid.Dispose();
                        InsertGatePass();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "All selected Recods Priyojna Formate should be 'True' or All selected Recods Priyojna Formate should be 'False'");
                    return;
                }
            }
            else
            {
                ViewState["MilkOrProductDemandId"] = dtdistid;

                ViewState["multi_demandId"] = multi_demandId;
                ViewState["multi_Distid"] = multi_Distid;

                if (dtdistid.Rows.Count > 0)
                {

                    dtdistid.Dispose();
                    InsertGatePass();
                }
            }




        }
    }
    #endregion========================================================


    #region====================Code for Milk Details=========================
    //private void GetDemandDetails()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
    //        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        multi_demandId = ""; multi_Distid = "";
    //        int iddata = 0;
    //        DataRow dr;
    //        DataTable dtdistid = new DataTable();
    //        dtdistid.Columns.Add("MilkOrProductDemandId", typeof(string));
    //        dr = dtdistid.NewRow();
    //        foreach (GridViewRow gridrow in GridViewOrderDetails.Rows)
    //        {

    //            CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");
    //            Label lblMilkOrProductDemandId = (Label)gridrow.FindControl("lblMilkOrProductDemandId");
    //            Label lblDistributorId = (Label)gridrow.FindControl("lblDistributorId");

    //            if (chkSelect.Checked == true)
    //            {
    //                dr[0] = lblMilkOrProductDemandId.Text;
    //                dtdistid.Rows.Add(dr.ItemArray);

    //                ++iddata;
    //                if (iddata == 1)
    //                {
    //                    multi_demandId = lblMilkOrProductDemandId.Text;
    //                    multi_Distid = lblDistributorId.Text;
    //                }
    //                else
    //                {
    //                    multi_demandId += "," + lblMilkOrProductDemandId.Text;
    //                    multi_Distid += "," + lblDistributorId.Text;
    //                }
    //            }
    //        }
    //        ViewState["MilkOrProductDemandId"] = dtdistid;
    //        ViewState["multi_demandId"] = multi_demandId;
    //        ViewState["multi_Distid"] = multi_Distid;
    //        dtdistid.Dispose();
    //        ds1 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
    //                 new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "MilkOrProductDemandId_multi" },
    //                 new string[] { "7", deliverydate, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), multi_demandId.ToString() }, "dataset");

    //        if (ds1.Tables[0].Rows.Count > 0)
    //        {
    //            GetVehicleNo();

    //            pnlMilkOrProductSection.Visible = true;
    //            btnSubmit.Visible = true;
    //            btnClear.Visible = true;


    //        }
    //        else
    //        {

    //            btnSubmit.Visible = false;
    //            btnClear.Visible = false;
    //            pnlMilkOrProductSection.Visible = false;

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    private void InsertGatePass()
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

                    DataTable dttmpDemand = (DataTable)ViewState["MilkOrProductDemandId"];
                    if (ViewState["multi_Distid"].ToString() != "" && ViewState["multi_demandId"].ToString() != "" && dttmpDemand.Rows.Count > 0)
                    {
                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                        {

                            ds2 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan_Insert",
                                  new string[] { "Flag", "ItemCat_id", "DeliveryShift_id", "Delivary_Date"
                                              , "VehicleIn_Time","VehicleOut_Time","VehicleMilkOrProduct_ID","SupervisorName"
                                              , "TotalIssueCrate","CreatedBy"
                                              , "CreatedByIP","Office_ID","MilkCurDMCrateIsueStatus","VehicleNo"
                                              ,"Multi_DistributorId","Multi_MilkOrProductDemandId","Driver_name","Driver_Mobile_No","Remark"
                              },
                                  new string[] { "2", ddlItemCategory.SelectedValue, ddlShift.SelectedValue, dat, 
                                                 it,ot,ddlVehicleNo.SelectedValue,txtSupervisorName.Text.Trim(),
                                                 "0",objdb.createdBy()
                                                 , IPAddress,objdb.Office_ID(),ddlCrateStatus.SelectedValue,ddlVehicleNoList.SelectedValue
                                                 ,ViewState["multi_Distid"].ToString(),ViewState["multi_demandId"].ToString(),txtDriver_name.Text,Driver_Mobile_No.Text,txtRemark.Text
                               },
                               new string[] { "type_Trn_MilkOrProductDemandId" },
                           new DataTable[] { dttmpDemand }, "TableSave");
                        }
                        else
                        {
                            ds2 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan_Insert",
                                                         new string[] { "Flag", "ItemCat_id", "DeliveryShift_id", "Delivary_Date"
                                              , "VehicleIn_Time","VehicleOut_Time","VehicleMilkOrProduct_ID","SupervisorName"
                                              , "TotalIssueCrate","CreatedBy"
                                              , "CreatedByIP","Office_ID","MilkCurDMCrateIsueStatus","VehicleNo"
                                              ,"Multi_DistributorId","Multi_MilkOrProductDemandId","Driver_name","Driver_Mobile_No","Remark"
                              },
                                                         new string[] { "3", ddlItemCategory.SelectedValue, ddlShift.SelectedValue, dat, 
                                                 it,ot,ddlVehicleNo.SelectedValue,txtSupervisorName.Text.Trim(),
                                                 "0",objdb.createdBy()
                                                 , IPAddress,objdb.Office_ID(),ddlCrateStatus.SelectedValue,ddlVehicleNoList.SelectedValue
                                                 ,ViewState["multi_Distid"].ToString(),ViewState["multi_demandId"].ToString(),txtDriver_name.Text,Driver_Mobile_No.Text,txtRemark.Text
                             
                                           },
                               new string[] { "type_Trn_MilkOrProductDemandId" },
                           new DataTable[] { dttmpDemand }, "TableSave");
                        }

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            dttmpDemand.Dispose();
                            GetOrderNo();
                            GetVehicleList();
                            GetPendingList();
                            if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                            {
                                GetMilkChallanDetails(ds2.Tables[0].Rows[0]["VehicleDispId"].ToString());
                            }
                            else
                            {
                                GetProductChallanDetails(ds2.Tables[0].Rows[0]["ProductGatePassId"].ToString());
                            }
                            GetOrderNo();
                            GetVehicleList();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                        }
                        else
                        {
                            string msg = ds2.Tables[0].Rows[0]["Msg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  5:" + msg);

                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Please enter valid Qty. ");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Insertion : ", ex.Message.ToString());
        }

        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }

    }
    private void GetMilkChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
                     new string[] { "Flag", "VehicleDispId", "Office_ID" },
                     new string[] { "8", cid, objdb.Office_ID() }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                int count2 = ds3.Tables[1].Rows.Count;
                int rowcount = count2 + 1;
                StringBuilder sb = new StringBuilder();
                StringBuilder sbP1 = new StringBuilder();
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                decimal TotalofTotalSupplyQtyInLTR = 0;
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');

                DataTable tabledemand = (DataTable)ViewState["MilkOrProductDemandId"];
                if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() != "1")
                {
                    sb.Append("<div class='invoice'>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b style='font-size:17px;'>" + Dairyplant[0] + " डेरी प्लांट</b></br><b style='font-size:15px;'>दूध एवं दुग्ध पदार्थ गेट पास</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sb.Append("<tr>");

                    sb.Append("<th style='text-align:center'></th>");

                    sb.Append("<th style='text-align:center'>क्रैट</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स</th>");
                    sb.Append("<th style='text-align:center'>डी.एम.नं .</th>");
                    sb.Append("<th style='text-align:center' rowspan='" + rowcount + "'><b>गेट पास नं . :" + ds3.Tables[0].Rows[0]["VDChallanNo"].ToString() +
                            "</b></br>दिनांक&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</br>समय&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</br>शिफ्ट&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</th>");
                    sb.Append("</tr>");


                    for (int i = 0; i < count2; i++)
                    {
                        sb.Append("<tr>");
                        if (objdb.Office_ID() == "6")
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["BCName"].ToString() + "</br>(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["DName"].ToString() + "(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }

                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["IssueCrate"].ToString() + (Convert.ToInt32(ds3.Tables[1].Rows[i]["ExtraPacket"]) >= 0 ? ("+" + ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) : ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["Box"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["DMNo"].ToString() + "</td>");
                        sb.Append("</tr>");


                    }
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='1'>वाहन क्रं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='3'>वाहन चालक :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_name"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='1'>मो. नं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_Mobile_No"].ToString() + "</span></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table  class='table table1-bordered' style='width:100%; margin-top:-20px'>");
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
                    sb.Append("<th style='text-align:center'>केन/बॉक्स/जार</th>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<th style='text-align:center'>थप्पी</th>");
                    }
                    sb.Append("</tr>");
                    //int TotalofTotalSupplyQty = 0;
                    //int TotalissueCrate = 0;
                    //decimal TotalofTotalSupplyQtyInLTR = 0;


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
                        sb.Append("<td style='text-align:center'></td>");
                        if (objdb.Office_ID() == "6")
                        {

                        }
                        else
                        {
                            sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["CrateBunch"].ToString() + "+" + ds3.Tables[0].Rows[i]["ExtraCrate"].ToString() + "</td>");
                        }
                        sb.Append("</tr>");

                    }
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>टोटल</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["MilkCurDMCrateIsueStatus"].ToString() == "1" ? TotalissueCrate.ToString() : "0") + "</b></td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td <span style='text-align:left; padding-top:5px; font-weight:500;'><b>Remark :-</b>" + ds3.Tables[0].Rows[0]["Remark"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर उत्पादन शाखा </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>विपणन सहायक</span></td>");
                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्राप्तकर्ता </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                    }
                    sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; सामग्री प्रदायकर्ता</span></td>");
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;भंडार शाखा</span></td>");

                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्रदायकर्ता</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(उत्पादन /भंडार शाखा)&nbsp;&nbsp;</span></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

                    if (tabledemand.Rows.Count > 1)
                    {
                        ViewState["PStatus"] = "";

                        ViewState["MergeData"] = sb.ToString();
                        int tmprow = tabledemand.Rows.Count;
                        for (int i = 0; i < tmprow; i++)
                        {
                            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                            GetDemandWiseDMDetails(dmid);
                        }

                        Print.InnerHtml = ViewState["MergeData"].ToString();

                    }
                    else
                    {

                        Print.InnerHtml = sb.ToString();
                    }
                }
                else if (ds3.Tables[0].Rows[0]["Priyojna_status"].ToString() == "1")
                {
                    string[] place = ds3.Tables[1].Rows[0]["SSName"].ToString().Split(' ');
                    //for priyojna adhikari
                    //    if (ViewState["PStatus"].ToString() != "")
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    //else
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    sbP1.Append("<div class='invoice'>");
                    sbP1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:50px;'/></td>");
                    sbP1.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>" + ds3.Tables[0].Rows[0]["Seller_Address"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>GSTIN :" + ds3.Tables[0].Rows[0]["Office_Gst"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State Code : 0 - MP</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr></tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td style='font-size:18px;'><span style='font-weight:700;'><U>|| गेटपास - सह पावती ||</U></span></td>");
                    sbP1.Append("<td style='width:70px;'></td>");

                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;'>प्रति,</span></td>");
                    sbP1.Append("<td style='font-size:17px;width:200px;'><span style='font-weight:700;'>दिनांक : " + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   परियोजना अधिकारी ,CDPO </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   समन्वित  बाल विकास योजना  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   महिला / बाल विकास विभाग  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'><b>   सावेर  </b></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   जिला इंदौर (म. प्र.) </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>   महोदय , </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;' colspan='2'><span style='font-weight:500;padding-left:60px'>समन्वित बाल विकास योजना के अंतर्गत निम्न विवरण अनुसार सामग्री प्रदाय की जा रही है - </span></td><br/>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  1. वाहन क्रमांक :  " + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  2. वाहन चालक का नाम : </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>मोबाइल नं. : </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  3. प्रदायित सामग्री का विवरण  </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>Tally D.M. No. : 4703  </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");


                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<th style='text-align:center'>क्र.म.</th>");

                    sbP1.Append("<th style='text-align:center'>सामग्री का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>पैक साईज</th>");

                    sbP1.Append("<th style='text-align:center'>प्रदाय नग</th>");
                    sbP1.Append("<th style='text-align:center'>मात्रा (" + ds3.Tables[0].Rows[0]["UnitName"].ToString() + ")</th>");
                    sbP1.Append("<th style='text-align:center'>प्रदाय स्थान का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>उत्पादन दिनांक</th>");
                    sbP1.Append("<th style='text-align:center'>समाप्ति  दिनांक</th>");
                    sbP1.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                        TotalofTotalSupplyQtyInLTR += decimal.Parse(ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString());
                        //if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        //{
                        //    TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        //}
                        sbP1.Append("<tr>");
                        sbP1.Append("<td>" + (i + 1) + "</td>");
                        sbP1.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["PackagingSize"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + place[2] + "</td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("</tr>");

                    }
                    sbP1.Append("<tr>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td><b>योग </b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQtyInLTR.ToString() + "</b></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  style='width:100%; '>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span >कृपया उपरोक्त सामग्री प्राप्त कर पावती दिलाने का कष्ट करें |</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");


                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर वाहन चालक</td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर विवरण  सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर भंडार सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर नोडल अधिकृत  अधिकारी</td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<hr>");

                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;text-align:center;'><span style='font-weight:700;'><U>|| पावती ||</U></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपरोक्तानुसार वाहन क्रमांक <b>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b> से आज दिनांक <b>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b> समय <b>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b>  बजे पर सही स्थिति में प्राप्त किया गया है</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'>वाहन चालक का नाम वहस्ताक्षर दिनांक सहित </td>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'> </td>");
                    sbP1.Append("<td   style='text-align:left;height:120px;font-size:17px;width:54%'>परियोजना अधिकारी ,CDPO<br/>समन्वित  बाल विकास योजना <br/>महिला / बाल विकास विभाग <br/>सावेर <br/>जिला इंदौर (म. प्र.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:13px;'>हस्ताक्षर सील व  दिनांक सहित </span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");
                    sbP1.Append("</div>");


                    if (tabledemand.Rows.Count > 1)
                    {
                        ViewState["PStatus"] = "";

                        ViewState["MergeData"] = sbP1.ToString();
                        int tmprow = tabledemand.Rows.Count;
                        for (int i = 0; i < tmprow; i++)
                        {
                            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                            GetDemandWiseDMDetails(dmid);
                        }

                        Print.InnerHtml = ViewState["MergeData"].ToString();

                    }
                    else
                    {

                        Print.InnerHtml = sbP1.ToString();
                    }

                }

                //DataTable tabledemand = (DataTable)ViewState["MilkOrProductDemandId"];
                //if (tabledemand.Rows.Count > 1)
                //{
                //    ViewState["PStatus"] = "";
                //     ViewState["MergeData"] = sb.ToString();
                //  //  ViewState["MergeData"] = sbP1.ToString();
                //    int tmprow = tabledemand.Rows.Count;
                //    for (int i = 0; i < tmprow; i++)
                //    {
                //        string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                //        GetDemandWiseDMDetails(dmid);
                //    }

                //    Print.InnerHtml = ViewState["MergeData"].ToString();

                //}
                //else
                //{
                //    Print.InnerHtml = sb.ToString();
                //    //Print.InnerHtml = sbP1.ToString();
                //}

                if (tabledemand != null)
                {
                    tabledemand.Dispose();
                    ViewState["MultiDemandId"] = "";
                    ViewState["MergeData"] = "";
                    ViewState["PStatus"] = "";
                }
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
    private void GetProductChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_ProductGatePass",
                     new string[] { "Flag", "ProductGatePassId", "Office_ID" },
                     new string[] { "1", cid, objdb.Office_ID() }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                int count2 = ds3.Tables[1].Rows.Count;
                int rowcount = count2 + 1;
                StringBuilder sb = new StringBuilder();
                StringBuilder sbP1 = new StringBuilder();
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                decimal TotalofTotalSupplyQtyInLTR = 0;
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');

                DataTable tabledemand = (DataTable)ViewState["MilkOrProductDemandId"];
                if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() != "1")
                {
                    sb.Append("<div class='invoice'>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b style='font-size:17px;'>" + Dairyplant[0] + " डेरी प्लांट</b></br><b style='font-size:15px;'>दूध एवं दुग्ध पदार्थ गेट पास</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sb.Append("<tr>");

                    sb.Append("<th style='text-align:center'></th>");

                    sb.Append("<th style='text-align:center'>क्रैट</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स</th>");
                    sb.Append("<th style='text-align:center'>डी.एम.नं .</th>");
                    sb.Append("<th style='text-align:center' rowspan='" + rowcount + "'><b>गेट पास नं . :" + ds3.Tables[0].Rows[0]["GatePassNo"].ToString() +
                            "</b></br>दिनांक&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</br>समय&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</br>शिफ्ट&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</th>");
                    sb.Append("</tr>");


                    for (int i = 0; i < count2; i++)
                    {
                        sb.Append("<tr>");
                        if (objdb.Office_ID() == "6")
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["BCName"].ToString() + "</br>(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["DName"].ToString() + "(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }

                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["IssueCrate"].ToString() + (Convert.ToInt32(ds3.Tables[1].Rows[i]["ExtraPacket"]) >= 0 ? ("+" + ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) : ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["Cane_Jar_Box"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["DMNo"].ToString() + "</td>");
                        sb.Append("</tr>");


                    }
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='1'>वाहन क्रं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='3'>वाहन चालक :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_name"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='1'>मो. नं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_Mobile_No"].ToString() + "</span></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table  class='table table1-bordered' style='width:100%; margin-top:-20px'>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center'>क्र.म.</th>");
                    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                    {
                        sb.Append("<th style='text-align:center'>दूध का नाम</th>");
                    }
                    else
                    {
                        sb.Append("<th style='text-align:center'>उत्पाद प्रकार</th>");
                    }

                    sb.Append("<th style='text-align:center'>मात्रा</th>");
                    sb.Append("<th style='text-align:center'>क्रैट की संख्या</th>");
                    sb.Append("<th style='text-align:center'>अन्य</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स/जार</th>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<th style='text-align:center'>थप्पी</th>");
                    }
                    sb.Append("</tr>");
                    //int TotalofTotalSupplyQty = 0;
                    //int TotalissueCrate = 0;
                    //decimal TotalofTotalSupplyQtyInLTR = 0;

                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                        if (ds3.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        {
                            TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                        }
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + (ds3.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1" ? ds3.Tables[0].Rows[i]["IssueCrate"].ToString() : "0") + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["ExtraPacket"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + (ds3.Tables[0].Rows[i]["CarriageModeID"].ToString() == "2" ? ds3.Tables[0].Rows[i]["Cane_Jar_Box"].ToString() : "0") + "</td>");
                        if (objdb.Office_ID() == "6")
                        {

                        }
                        else
                        {
                            sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["CrateBunch"].ToString() + "+" + ds3.Tables[0].Rows[i]["ExtraCrate"].ToString() + "</td>");
                        }
                        sb.Append("</tr>");

                    }
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>टोटल</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["MilkCurDMCrateIsueStatus"].ToString() == "1" ? TotalissueCrate.ToString() : "0") + "</b></td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td <span style='text-align:left; padding-top:5px; font-weight:500;'><b>Remark :-</b>" + ds3.Tables[0].Rows[0]["Remark"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                   
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर उत्पादन शाखा </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>विपणन सहायक</span></td>");
                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्राप्तकर्ता </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                    }
                    sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                    if (objdb.Office_ID() == "6")
                    {

                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; सामग्री प्रदायकर्ता</span></td>");
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;भंडार शाखा</span></td>");

                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्रदायकर्ता</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(उत्पादन /भंडार शाखा)&nbsp;&nbsp;</span></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");


                    if (tabledemand.Rows.Count > 1)
                    {
                        ViewState["PStatus"] = "";
                        ViewState["MergeData"] = sb.ToString();

                        int tmprow = tabledemand.Rows.Count;
                        for (int i = 0; i < tmprow; i++)
                        {
                            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                            GetDemandWiseDMDetails(dmid);
                        }

                        Print1.InnerHtml = ViewState["MergeData"].ToString();

                    }
                    else
                    {
                        Print1.InnerHtml = sb.ToString();

                    }
                }
                else if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() == "1")
                {

                    string[] place = ds3.Tables[1].Rows[0]["SSName"].ToString().Split(' ');

                    //for priyojna adhikari
                    //if (ViewState["PStatus"].ToString() != "")
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    //else
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    sbP1.Append("<div class='invoice'>");
                    sbP1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:50px;'/></td>");
                    sbP1.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>" + ds3.Tables[0].Rows[0]["Seller_Address"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>GSTIN :" + ds3.Tables[0].Rows[0]["Office_Gst"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State Code : 0 - MP</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr></tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td style='font-size:18px;'><span style='font-weight:700;'><U>|| गेटपास - सह पावती ||</U></span></td>");
                    sbP1.Append("<td style='width:70px;'></td>");

                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;'>प्रति,</span></td>");
                    sbP1.Append("<td style='font-size:17px;width:200px;'><span style='font-weight:700;'>दिनांक : " + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   परियोजना अधिकारी ,CDPO </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   समन्वित  बाल विकास योजना  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   महिला / बाल विकास विभाग  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'><b>   सावेर  </b></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   जिला इंदौर (म. प्र.) </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>   महोदय , </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;' colspan='2'><span style='font-weight:500;padding-left:60px'>समन्वित बाल विकास योजना के अंतर्गत निम्न विवरण अनुसार सामग्री प्रदाय की जा रही है - </span></td><br/>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  1. वाहन क्रमांक :  " + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  2. वाहन चालक का नाम : </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>मोबाइल नं. : </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  3. प्रदायित सामग्री का विवरण  </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>Tally D.M. No. : 4703  </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");



                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<th style='text-align:center'>क्र.म.</th>");

                    sbP1.Append("<th style='text-align:center'>सामग्री का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>पैक साईज</th>");

                    sbP1.Append("<th style='text-align:center'>प्रदाय नग</th>");
                    sbP1.Append("<th style='text-align:center'>मात्रा (" + ds3.Tables[0].Rows[0]["UnitName"].ToString() + ")</th>");
                    sbP1.Append("<th style='text-align:center'>प्रदाय स्थान का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>उत्पादन दिनांक</th>");
                    sbP1.Append("<th style='text-align:center'>समाप्ति  दिनांक</th>");
                    sbP1.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                        TotalofTotalSupplyQtyInLTR += decimal.Parse(ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString());
                        //if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        //{
                        //    TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        //}
                        sbP1.Append("<tr>");
                        sbP1.Append("<td>" + (i + 1) + "</td>");
                        sbP1.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["PackagingSize"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + place[2] + "</td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("</tr>");

                    }
                    sbP1.Append("<tr>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td><b>योग </b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQtyInLTR.ToString() + "</b></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  style='width:100%; '>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span >कृपया उपरोक्त सामग्री प्राप्त कर पावती दिलाने का कष्ट करें |</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");


                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर वाहन चालक</td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर विवरण  सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर भंडार सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर नोडल अधिकृत  अधिकारी</td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<hr>");

                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;text-align:center;'><span style='font-weight:700;'><U>|| पावती ||</U></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपरोक्तानुसार वाहन क्रमांक <b>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b> से आज दिनांक <b>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b> समय <b>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b>  बजे पर सही स्थिति में प्राप्त किया गया है</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'>वाहन चालक का नाम वहस्ताक्षर दिनांक सहित </td>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'> </td>");
                    sbP1.Append("<td   style='text-align:left;height:120px;font-size:17px;width:54%'>परियोजना अधिकारी ,CDPO<br/>समन्वित  बाल विकास योजना <br/>महिला / बाल विकास विभाग <br/>सावेर <br/>जिला इंदौर (म. प्र.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:13px;'>हस्ताक्षर सील व  दिनांक सहित </span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");
                    sbP1.Append("</div>");




                    if (tabledemand.Rows.Count > 1)
                    {
                        ViewState["PStatus"] = "";
                        ViewState["MergeData"] = sbP1.ToString();

                        int tmprow = tabledemand.Rows.Count;
                        for (int i = 0; i < tmprow; i++)
                        {
                            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                            GetDemandWiseDMDetails(dmid);
                        }

                        Print1.InnerHtml = ViewState["MergeData"].ToString();

                    }
                    else
                    {
                        Print1.InnerHtml = sbP1.ToString();

                    }
                }

                //DataTable tabledemand = (DataTable)ViewState["MilkOrProductDemandId"];
                //if (tabledemand.Rows.Count > 1)
                //{
                //    ViewState["PStatus"] = "";
                //    ViewState["MergeData"] = sb.ToString();
                //    //ViewState["MergeData"] = sbP1.ToString();
                //    int tmprow = tabledemand.Rows.Count;
                //    for (int i = 0; i < tmprow; i++)
                //    {
                //        string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                //        GetDemandWiseDMDetails(dmid);
                //    }

                //    Print1.InnerHtml = ViewState["MergeData"].ToString();

                //}
                //else
                //{
                //    Print1.InnerHtml = sb.ToString();
                //    //Print1.InnerHtml = sbP1.ToString();
                //}

                if (tabledemand != null)
                {
                    tabledemand.Dispose();
                    ViewState["MultiDemandId"] = "";
                    ViewState["MergeData"] = "";
                    ViewState["PStatus"] = "";
                }

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

    private void GetDemandWiseDMDetails(string dmid)
    {
        try
        {

            ds4 = objdb.ByProcedure("USP_Trn_ProductGatePass",
                     new string[] { "Flag", "MilkOrProductDemandId", "Office_ID" },
                     new string[] { "4", dmid, objdb.Office_ID() }, "dataset");

            if (ds4.Tables[0].Rows.Count > 0)
            {

                int Count = ds4.Tables[0].Rows.Count;
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sbP1 = new StringBuilder();
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                decimal TotalofTotalSupplyQtyInLTR = 0;
                string OfficeName = ds4.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');

                //DataTable tabledemand = (DataTable)ViewState["MilkOrProductDemandId"];
                if (ds4.Tables[0].Rows[0]["Priyojna_status"].ToString() != "1")
                {
                    if (ViewState["PStatus"].ToString() != "")
                    {
                        sb1.Append("<p style='page-break-after: always'>");
                    }
                    else
                    {
                        sb1.Append("<p style='page-break-after: always'>");
                    }
                    sb1.Append("<div class='invoice'>");
                    sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb1.Append("<tr>");
                    sb1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb1.Append("<td style='font-size:21px;'><b>" + ds4.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sb1.Append("<td></td>");
                    sb1.Append("</tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td style='font-size:17px;'><b>" + Dairyplant[0] + " डेरी प्लांट</b></td>");
                    sb1.Append("<td style='width:70px;'></td>");
                    sb1.Append("</tr>");
                    sb1.Append("<tr></tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td style='text-align:left'><span style='font-weight:700;'>" + "" + "</span></td>");
                    sb1.Append("<td style='font-size:17px;'><b>वाहन वितरण चालान</b></td>");
                    sb1.Append("<td style='width:70px;'></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    sb1.Append("<table class='table table1-bordered' style='width:100%; '>");
                    sb1.Append("<tr>");
                    if (objdb.Office_ID() == "6")
                    {
                        sb1.Append("<td>" + (ds4.Tables[0].Rows[0]["BCName"].ToString() + "</br>(" + ds4.Tables[0].Rows[0]["SSName"].ToString() + ")") + "</td>");
                    }
                    else
                    {
                        sb1.Append("<td>" + (ds4.Tables[0].Rows[0]["DName"].ToString() == ds4.Tables[0].Rows[0]["SSName"].ToString() ? ds4.Tables[0].Rows[0]["DName"].ToString() : ds4.Tables[0].Rows[0]["DName"].ToString() + "(" + ds4.Tables[0].Rows[0]["SSName"].ToString() + ")") + "</td>");
                    }
                    sb1.Append("<td style='text-align:center' ><b>डी.एम.नं . :" + ds4.Tables[0].Rows[0]["OrderId"].ToString()
                       + "</b></br><b>गेट पास नं .&nbsp;&nbsp;:<span >" + (ds4.Tables[0].Rows[0]["ItemCat_id"].ToString() == objdb.GetMilkCatId() ? ds4.Tables[0].Rows[0]["VDChallanNo"].ToString() : ds4.Tables[0].Rows[0]["GatePassNo"].ToString())
                        + "</b></br>दिनांक&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["Delivary_Date"].ToString()
                            + "</br>समय&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["DemandTime"].ToString()
                            + "</br>रूट&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["RName"].ToString()
                            + "</br>शिफ्ट&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");

                    sb1.Append("</tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td  colspan='2'>वाहन क्रं&nbsp;&nbsp;<span>" + ds4.Tables[0].Rows[1]["VehicleNo"].ToString() + "</span></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");

                    sb1.Append("<table class='table table1-bordered' style='width:100%; margin-top:-20px'>");
                    sb1.Append("<tr>");
                    sb1.Append("<th style='text-align:center'>क्र.म.</th>");
                    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                    {
                        sb1.Append("<th style='text-align:center'>दूध का नाम</th>");
                    }
                    else
                    {
                        sb1.Append("<th style='text-align:center'>उत्पाद प्रकार</th>");
                    }

                    sb1.Append("<th style='text-align:center'>मात्रा</th>");

                    sb1.Append("<th style='text-align:center'>क्रैट की संख्या</th>");
                    sb1.Append("<th style='text-align:center'>अन्य</th>");
                    sb1.Append("<th style='text-align:center'>केन/बॉक्स/जार</th>");
                    sb1.Append("</tr>");
                    //int TotalofTotalSupplyQty = 0;
                    //int TotalissueCrate = 0;
                    //decimal TotalofTotalSupplyQtyInLTR = 0;

                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString());
                        if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        {
                            TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        }
                        sb1.Append("<tr>");
                        sb1.Append("<td>" + (i + 1) + "</td>");
                        sb1.Append("<td>" + (ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds4.Tables[0].Rows[i]["ItemName"].ToString() : ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sb1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + "</td>");
                        sb1.Append("<td style='text-align:center'>" + (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1" ? ds4.Tables[0].Rows[i]["IssueCrate"].ToString() : "0") + "</td>");
                        sb1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["ExtraPacket"].ToString() + "</td>");
                        sb1.Append("<td style='text-align:center'>" + (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "2" ? ds4.Tables[0].Rows[i]["Box"].ToString() : "0") + "</td>");
                        sb1.Append("</tr>");

                    }
                    sb1.Append("<tr>");
                    sb1.Append("<td></td>");
                    sb1.Append("<td><b>टोटल</b></td>");
                    sb1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sb1.Append("<td style='text-align:center'><b>" + TotalissueCrate.ToString() + "</b></td>");
                    sb1.Append("<td></td>");
                    sb1.Append("<td></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb1.Append("<tr>");
                    sb1.Append("<td colspan='3' <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर विपणन प्रभारी</br></span></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    sb1.Append("</div>");

                    ViewState["MergeData"] += sb1.ToString();

                }

                else if (ds4.Tables[0].Rows[0]["Priyojna_status"].ToString() == "1")
                {

                    string[] place = ds4.Tables[0].Rows[0]["SSName"].ToString().Split(' ');
                    //for priyojna adhikari
                    if (ViewState["PStatus"].ToString() != "")
                    {
                        sbP1.Append("<p style='page-break-after: always'>");
                    }
                    else
                    {
                        sbP1.Append("<p style='page-break-after: always'>");
                    }
                    sbP1.Append("<div class='invoice'>");
                    sbP1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:50px;'/></td>");
                    sbP1.Append("<td style='font-size:21px;'><b>" + ds4.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>" + ds4.Tables[0].Rows[0]["Seller_Address"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>GSTIN :" + ds4.Tables[0].Rows[0]["Office_Gst"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State Code : 0 - MP</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr></tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td style='font-size:18px;'><span style='font-weight:700;'><U>|| गेटपास - सह पावती ||</U></span></td>");
                    sbP1.Append("<td style='width:70px;'></td>");

                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;'>प्रति,</span></td>");
                    sbP1.Append("<td style='font-size:17px;width:200px;'><span style='font-weight:700;'>दिनांक : " + ds4.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   परियोजना अधिकारी ,CDPO </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   समन्वित  बाल विकास योजना  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   महिला / बाल विकास विभाग  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'><b>   सावेर  </b></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   जिला इंदौर (म. प्र.) </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>   महोदय , </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;' colspan='2'><span style='font-weight:500;padding-left:60px'>समन्वित बाल विकास योजना के अंतर्गत निम्न विवरण अनुसार सामग्री प्रदाय की जा रही है - </span></td><br/>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  1. वाहन क्रमांक :  " + ds4.Tables[0].Rows[1]["VehicleNo"].ToString() + "</span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  2. वाहन चालक का नाम : </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>मोबाइल नं. : </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  3. प्रदायित सामग्री का विवरण  </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>Tally D.M. No. : 4703  </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");



                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<th style='text-align:center'>क्र.म.</th>");

                    sbP1.Append("<th style='text-align:center'>सामग्री का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>पैक साईज</th>");

                    sbP1.Append("<th style='text-align:center'>प्रदाय नग</th>");
                    sbP1.Append("<th style='text-align:center'>मात्रा" + ds4.Tables[0].Rows[0]["UnitName"].ToString() + "</th>");
                    sbP1.Append("<th style='text-align:center'>प्रदाय स्थान का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>उत्पादन दिनांक</th>");
                    sbP1.Append("<th style='text-align:center'>समाप्ति  दिनांक</th>");
                    sbP1.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString());
                        TotalofTotalSupplyQtyInLTR += decimal.Parse(ds4.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString());
                        //if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        //{
                        //    TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        //}
                        sbP1.Append("<tr>");
                        sbP1.Append("<td>" + (i + 1) + "</td>");
                        sbP1.Append("<td>" + (ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds4.Tables[0].Rows[i]["ItemName"].ToString() : ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["PackagingSize"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + place[2] + "</td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("</tr>");

                    }
                    sbP1.Append("<tr>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td><b>योग </b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQtyInLTR.ToString() + "</b></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  style='width:100%; '>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span >कृपया उपरोक्त सामग्री प्राप्त कर पावती दिलाने का कष्ट करें |</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");


                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर वाहन चालक</td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर विवरण  सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर भंडार सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर नोडल अधिकृत  अधिकारी</td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<hr>");

                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;text-align:center;'><span style='font-weight:700;'><U>|| पावती ||</U></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपरोक्तानुसार वाहन क्रमांक <b>" + ds4.Tables[0].Rows[1]["VehicleNo"].ToString() + "</b> से आज दिनांक <b>" + ds4.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b> समय <b>" + ds4.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b>  बजे पर सही स्थिति में प्राप्त किया गया है</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'>वाहन चालक का नाम वहस्ताक्षर दिनांक सहित </td>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'> </td>");
                    sbP1.Append("<td   style='text-align:left;height:120px;font-size:17px;width:54%'>परियोजना अधिकारी ,CDPO<br/>समन्वित  बाल विकास योजना <br/>महिला / बाल विकास विभाग <br/>सावेर <br/>जिला इंदौर (म. प्र.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:13px;'>हस्ताक्षर सील व  दिनांक सहित </span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");
                    sbP1.Append("</div>");
                    ViewState["MergeData"] += sbP1.ToString();
                }

                //  ViewState["MergeData"] += sb1.ToString();

                ViewState["PStatus"] = "1";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }

    }

    private void GetPendingList()
    {
        try
        {
            if (txtDate.Text != "" && ddlShift.SelectedValue != "0")
            {
                lblMsg.Text = string.Empty;
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                      new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "Office_ID" },
                        new string[] { "8", odat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = ds5.Tables[0];
                    GridView1.DataBind();

                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
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

    #endregion=================End of Milk Details===========================

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

        txtMVehicleNo.Text = string.Empty;
        txtVehicleType.Text = string.Empty;
        ddlVendorType.SelectedIndex = 0;
        ddlVendorName.SelectedValue = "0";
        txtDriver_name.Text = "";
        txtRemark.Text = "";
        Driver_Mobile_No.Text = "";

    }
    protected void ddlVendorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetName();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myVehicleDetailsModal()", true);
    }
    protected void lnkVehicle_Click(object sender, EventArgs e)
    {
        GetVendorType();
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
                            new string[] { "2",ddlVendorType.SelectedValue,ddlVendorName.SelectedValue,txtVehicleType.Text.Trim(), txtMVehicleNo.Text.Trim(),isactive, objdb.Office_ID(), objdb.createdBy(),
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
                                lblModalMsg1.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle Number " + txtMVehicleNo.Text + " " + error);
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
    protected void btnShowPendingGatePass_Click(object sender, EventArgs e)
    {
        GetPendingList();
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridViewOrderDetails.Rows)
            {

                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                Label lblAreaId = (Label)row.FindControl("lblAreaId");
                Label lblRouteId = (Label)row.FindControl("lblRouteId");
                Label lblmsg1 = (Label)row.FindControl("lblmsg1");
                //if (chk.Checked == true)
                //{
                ds3 = objdb.ByProcedure("USP_Trn_Paymentandsecuritycompare",
                     new string[] { "Flag", "Office_ID", "SuperStockistId" },
                     new string[] { "4", objdb.Office_ID(), lblDistributorId.Text }, "dataset");
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        string superstockistid = ds3.Tables[0].Rows[0]["SuperStockistId"].ToString();
                        decimal finalamount = 0, tcstax = 0, PaybleAmtWithTcsTax = 0, tcstaxAmt = 0, Totalpaybleamount = 0;
                        DateTime odate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        //if (objdb.Office_ID() == "6")
                        //{
                        //    ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                        //     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "SuperStockistId", "RouteId", "OrganizationId", "AreaId" },
                        //       new string[] { "12", deliverydate.ToString(), ddlShift.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID(), superstockistid.ToString(), lblRouteId.Text, "0", lblAreaId.Text }, "dataset");
                        //}
                        //else
                        //{
                        ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                         new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "Office_ID", "SuperStockistId", "RouteId", "OrganizationId", "AreaId" },
                           new string[] { "11", deliverydate.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID(), superstockistid.ToString(), lblRouteId.Text, "0", lblAreaId.Text }, "dataset");
                        //}
                        if (ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                                {
                                    finalamount = finalamount + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount"]) - Convert.ToDecimal(ds2.Tables[0].Rows[i]["AdvCardAmt"]));
                                }
                                lblFinalAmount.Text = finalamount.ToString();
                                GetTcsTax(lblDistributorId.Text);
                                tcstax = Convert.ToDecimal(ViewState["Tval"].ToString());
                                tcstaxAmt = ((tcstax * finalamount) / 100);
                                PaybleAmtWithTcsTax = tcstaxAmt + finalamount;
                                // ViewState["PaybleAmtWithTCSTax"] = PaybleAmtWithTcsTax.ToString("0.000");
                                lblTcsTax.Text = ViewState["Tval"].ToString();
                                lblTcsTaxAmt.Text = tcstaxAmt.ToString("0.000");
                                lblFinalPaybleAmount.Text = PaybleAmtWithTcsTax.ToString("0.000");
                            }
                        }
                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                        {
                            ds1 = objdb.ByProcedure("USP_Trn_Paymentandsecuritycompare",
                                 new string[] { "Flag", "RouteId", "Office_ID", "SuperStockistId", "Delivary_Date" },
                                 new string[] { "3", lblRouteId.Text, objdb.Office_ID(), lblDistributorId.Text, deliverydate.ToString() }, "dataset");
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId())
                        {
                            ds1 = objdb.ByProcedure("USP_Trn_Paymentandsecuritycompare",
                                 new string[] { "Flag", "RouteId", "Office_ID", "SuperStockistId", "Delivary_Date" },
                                 new string[] { "5", lblRouteId.Text, objdb.Office_ID(), superstockistid.ToString(), deliverydate.ToString() }, "dataset");
                        }
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                Totalpaybleamount = PaybleAmtWithTcsTax + decimal.Parse(ds1.Tables[0].Rows[0]["Opening"].ToString());
                                //if (Totalpaybleamount <= decimal.Parse(ds1.Tables[0].Rows[0]["SecurityDeposit"].ToString()))
                                //{


                                //}
                                //else
                                //{
                                //    chk.Checked = false;
                                //    chk.Enabled = false;
                                //    lblmsg1.Text = "Security Amount Should be greater than or equal to Total Payble Amount ";
                                //    lblmsg1.Visible = true;
                                //}
                            }
                        }
                    }
                }
                //}
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
        }


    }
    private void GetTcsTax(string DistributorId)
    {
        try
        {
            ViewState["Tval"] = "";
            DateTime Ddate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds8 = objdb.ByProcedure("USP_Mst_TcsTax",
                 new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                   new string[] { "0", objdb.Office_ID(), deliverydate, DistributorId.ToString() }, "dataset");

            if (ds8.Tables[0].Rows.Count > 0)
            {
                ViewState["Tval"] = ds8.Tables[0].Rows[0]["Tval"].ToString();
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
}
