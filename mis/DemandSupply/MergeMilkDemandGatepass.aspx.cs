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

public partial class mis_DemandSupply_MergeMilkDemandGatepass : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3,ds4, ds9 = new DataSet();
    int totalmilkqty = 0, totalcrate = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetSearchLocation();
                GetShift();
                GetSearchShift();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtDate.Attributes.Add("readonly", "true");
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
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void CClear()
    {
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        txtVehicleNo.Text = string.Empty;
        txtSupervisorName.Text = string.Empty;
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
    protected void GetSearchShift()
    {
        try
        {

            ddlSearchShift.DataTextField = "ShiftName";
            ddlSearchShift.DataValueField = "Shift_id";
            ddlSearchShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlSearchShift.DataBind();
            ddlSearchShift.Items.Insert(0, new ListItem("All", "0"));

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
    protected void GetSearchLocation()
    {
        try
        {
            ddlSearchLocation.DataTextField = "AreaName";
            ddlSearchLocation.DataValueField = "AreaId";
            ddlSearchLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlSearchLocation.DataBind();
            ddlSearchLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }

    private void GetDistributor()
    {
        try
        {
            ddlDistributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "12", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlDistributor.DataTextField = "DRName";
            ddlDistributor.DataValueField = "RouteId";
            ddlDistributor.DataBind();



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    private void GetChallanDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string routid = "";
            int iddata = 0;
            foreach (ListItem item in ddlDistributor.Items)
            {
                if (item.Selected)
                {
                    ++iddata;
                    if (iddata == 1)
                    {
                        routid = item.Value;
                        
                    }
                    else
                    {
                        routid += "," + item.Value;
                    }


                }
            }
            ViewState["MergeRouteId"] = routid;
            ds1 = objdb.ByProcedure("USP_Trn_VehicleMergeDelivChallan",
                     new string[] { "Flag", "Delivary_Date", "DeliveryShift_id", "ItemCat_id", "AreaId", "MultiRouteId", "Office_ID" },
                       new string[] { "1", fromdat, ddlShift.SelectedValue,objdb.GetMilkCatId(), ddlLocation.SelectedValue, routid, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0 && ds1.Tables[1].Rows.Count != 0)
            {
                lblMsg.Text = string.Empty;

              
                if (ds1.Tables[1].Rows.Count == 1)
                {
                    pnldata.Visible = true;
                    pnlvehicledetail.Visible = true;
                    GridView1.DataSource = ds1.Tables[0];
                    GridView1.DataBind();
                    txtVehicleNo.Text = ds1.Tables[1].Rows[0]["VehicleNo"].ToString();
                    ViewState["VehicleMilkOrProduct_ID"] = ds1.Tables[1].Rows[0]["VehicleMilkOrProduct_ID"].ToString();
                }
                else
                {
                    txtVehicleNo.Text = "";
                    ViewState["VehicleMilkOrProduct_ID"] = "";
                    pnldata.Visible = false;
                    pnlvehicledetail.Visible = false;
                    lblMsg.Text = lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Multiple Vehichle or Superisor Found.");
                }
            }
            else
            {
                pnldata.Visible = true;
                pnlvehicledetail.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
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
                    if (dtInsertChild.Rows.Count > 0 && ViewState["MergeRouteId"].ToString() != "" && ViewState["VehicleMilkOrProduct_ID"].ToString() != "0" && ViewState["VehicleMilkOrProduct_ID"].ToString() != "")
                    {
                       
                        ds2 = objdb.ByProcedure("USP_Trn_VehicleMergeDelivChallan_Insert",
                                             new string[] { "Flag","AreaId", "MergeRouteId", "ItemCat_id", "DeliveryShift_id", "Delivary_Date"
                                                          , "VehicleIn_Time","VehicleOut_Time","VehicleMilkOrProduct_ID","SupervisorName",
                                                          "TotalIssueCrate","CreatedBy", "CreatedByIP","Office_ID" },
                                             new string[] { "1",ddlLocation.SelectedValue, ViewState["MergeRouteId"].ToString(), "3", ddlShift.SelectedValue, dat, 
                                                 it,ot,ViewState["VehicleMilkOrProduct_ID"].ToString(),txtSupervisorName.Text.Trim(),
                                                 ViewState["TotalIssueCrate"].ToString(),objdb.createdBy(), IPAddress,objdb.Office_ID()
                                             }, "type_Trn_VehicleMergeDelivChallanChild", dtInsertChild, "TableSave");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            CClear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            dtInsertChild.Dispose();
                            GetChallanDetails(ds2.Tables[0].Rows[0]["MergeVehicleChallanId"].ToString());
                        }
                        else if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                        {
                            string msg = ds2.Tables[0].Rows[0]["Msg"].ToString();
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                          

                            string[] alreadyroute = new string[ds2.Tables[0].Rows.Count];

                            //loopcounter
                            for (int loopcounter = 0; loopcounter < ds2.Tables[0].Rows.Count; loopcounter++)
                            {
                                //assign dataset values to array
                                alreadyroute[loopcounter] = ds2.Tables[0].Rows[loopcounter]["DName"].ToString();
                            }
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Challan Merged already for " + string.Join(", ", alreadyroute));
                        }
                        else
                        {
                            string msg = ds2.Tables[0].Rows[0]["Msg"].ToString();
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  5:" + error);

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
    private void GetChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_VehicleMergeDelivChallan",
                     new string[] { "Flag", "MergeVehicleChallanId", "Office_ID" },
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
                sb.Append("<td style='text-align:left'><span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["MergeVChallanNo"].ToString() + "</span></td>");
                sb.Append("<td style='font-size:17px;'><b>वाहन वितरण चालान</b></td>");
                sb.Append("<td>वाहन क्रं&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='text-align:left'>सुपरवाइजर का नाम&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["SupervisorName"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left'>आने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleIn_Time"].ToString() + "</span></td>");
                //sb.Append("<td>मार्ग क्रं&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[1]["RName"].ToString() + "</span></td>");
                sb.Append("<td>जाने का समय&nbsp;&nbsp;<span style='font-weight:700;'>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='text-align:left'>विक्रेता : " + ds3.Tables[0].Rows[0]["DistName"].ToString() + "</td>");
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
                sb.Append("<td style='text-align:center'><b>" + TotalissueCrate.ToString() + "</b></td>");

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
    #endregion========================================================
    #region=============== changed event and click for controls =================
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            GetDistributor();
            GetDatatableHeaderDesign();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty; ;
            GetChallanDetails();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        CClear();
        ddlLocation.SelectedIndex = 0;
        txtDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ViewState["TotalIssueQty"].ToString() != "" && ViewState["TotalIssueQty"].ToString() != "0" && ViewState["TotalIssueCrate"].ToString() != "")
            { 
                    InsertVehicleChallan();

            }

        }
    }
    #endregion============ end of changed event for controls===========

    #region=================cdoe merge report==========================
    private void GetMergeChallanDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_VehicleMergeDelivChallan",
                     new string[] { "Flag", "FromDate", "ToDate", "DeliveryShift_id", "ItemCat_id", "AreaId", "Office_ID" },
                       new string[] { "3", fromdat, todat, ddlSearchShift.SelectedValue,objdb.GetMilkCatId(), ddlSearchLocation.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
               
                GridView2.Visible = true;
                GridView2.DataSource = ds1.Tables[0];
                GridView2.DataBind();
            }
            else
            {
                GridView2.Visible = false;
                GridView2.DataSource = null;
                GridView2.DataBind();
                lblSearchMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning! : ", "No Record Found.");
            }

        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordPrint")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;

                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    GetChallanDetails(e.CommandArgument.ToString());
                    GetDatatableHeaderDesign();

                }
            }
            if (e.CommandName == "DeleteChallan")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    lblMsg.Text = string.Empty;
                    ds4 = objdb.ByProcedure("USP_Trn_VehicleMergeDelivChallan",
                                new string[] { "flag", "MergeVehicleChallanId", "CreatedBy", "CreatedByIP" },
                                new string[] { "4", e.CommandArgument.ToString(), objdb.createdBy()
                                    , IPAddress + ":" + objdb.GetMACAddress() }, "TableSave");


                    if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetMergeChallanDetails();
                        GetDatatableHeaderDesign();
                        lblSearchMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds4.Dispose();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                }
            }
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }

    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                GetMergeChallanDetails();
                GetDatatableHeaderDesign();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    protected void btnReportSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            lblSearchMsg.Text = string.Empty;
            GetCompareDate();
        }
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        lblSearchMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlSearchLocation.SelectedIndex = 0;
        ddlSearchShift.SelectedIndex = 0;
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.Visible = false;
    }
    #endregion============end of code merge report=====================
}