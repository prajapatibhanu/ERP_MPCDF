using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;
public partial class mis_DemandSupply_Rpt_ProductDemandSupply : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds1, ds2, ds3, ds4, ds9, ds5, ds6, ds7, dsInvo = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
                txtsupplydate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                GetShift();
                GetLocation();
                GetRoute();
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
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
            ddlShift.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
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
    private void CClear()
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
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
    //private void GetVehicleNo()
    //{
    //    try
    //    {
    //        ddlVehicleNo.DataTextField = "VehicleNo";
    //        ddlVehicleNo.DataValueField = "VehicleMilkOrProduct_ID";
    //        ddlVehicleNo.DataSource = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
    //                       new string[] { "flag", "Office_ID" },
    //                       new string[] { "5", objdb.Office_ID() }, "dataset");
    //        ddlVehicleNo.DataBind();
    //        ddlVehicleNo.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Transportation ", ex.Message.ToString());
    //    }
    //}
    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
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
                GetDMDetails();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    private void GetDMDetails()
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string multidmtype = "", multitdmid = "";
            int dmdata = 0;
            foreach (ListItem itemss in ddlDMType.Items)
            {
                if (itemss.Selected)
                {

                    multitdmid = itemss.Value;

                    ++dmdata;
                    if (dmdata == 1)
                    {
                        multidmtype = multitdmid;

                    }
                    else
                    {
                        multidmtype += "," + multitdmid;

                    }
                }
            }

            ds1 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "AreaId", "RouteId", "Office_ID", "MultiProductDMStatus" },
                       new string[] { "24", fromdat, todat, ddlShift.SelectedValue, objdb.GetProductCatId(), ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID(), multidmtype }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                Divfooter.Visible = true;
            }
            else
            {
                pnldata.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                Divfooter.Visible = false;
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
    private void PrintDMDetails(string cid, string lblDMEditStatus)
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
                sb.Append("<td colspan='2' style='text-align:right:'><b> Phone No: " + ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString() + "</b></td>");
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
                if (ds3.Tables[0].Rows[0]["DName"].ToString() == ds3.Tables[0].Rows[0]["BName"].ToString())
                {
                    sb.Append("<td style='font-size:17px;'><b> To=>" + ds3.Tables[0].Rows[0]["DName"].ToString() + "</b></td>");
                }
                else
                {
                    sb.Append("<td style='font-size:17px;'><b> To=>" + ds3.Tables[0].Rows[0]["DName"].ToString() + " ( " + ds3.Tables[0].Rows[0]["BName"].ToString() + " )</b></td>");
                }

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
                sb.Append("<th style='text-align:center'>QTY (In Ltr/KG)</th>");
                sb.Append("<th style='text-align:center'>Crate</th>");
                sb.Append("<th style='text-align:center'>Box/Jar</th>");
                sb.Append("<th style='text-align:center'>Rate</th>");
                sb.Append("<th style='text-align:center'>Amount</th>");
                sb.Append("</tr>");
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                Double TotalIssuebox = 0;
                double TotalAmt = 0;
                double TCSTAX_Amt = 0, FinalAmt_withTCSTax = 0;
                for (int i = 0; i < Count; i++)
                {
                    TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["SupplyQty"].ToString());
                    if (ds3.Tables[0].Rows[i]["IssueBox"].ToString() == "")
                    {
                        TotalIssuebox += 0;
                    }
                    else
                    {
                        TotalIssuebox += Double.Parse(ds3.Tables[0].Rows[i]["IssueBox"].ToString());
                    }

                    //TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                    TotalAmt += Double.Parse(ds3.Tables[0].Rows[i]["Amount"].ToString());
                    sb.Append("<tr>");
                    sb.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyQty"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["QtyInLtr"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'></td>");
                    //sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueCrate"].ToString() + "</td>");
                    sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueBox"].ToString() + "</td>");
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
                    sb.Append("<td style='text-align:center'><b>" + (lblDMEditStatus.ToString() == "" ? "" : ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString()) + "</b></td>");
                }
                //sb.Append("<td style='text-align:center'><b>" + ds3.Tables[0].Rows[0]["TotalIssueBox"].ToString() + "</b></td>");
                sb.Append("<td style='text-align:center'><b>" + TotalIssuebox + "</b></td>");
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
                sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX_Amt.ToString("0.00") : "NA") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>Grand Total</b></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
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
                    sb.Append("<td style='text-align:left'>Product received as per<br>above details along with <b>  " + ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString() + " crates</b><br><br> Receiver Signature<br>(Party)</td>");
                }
                else
                {
                    sb.Append("<td style='text-align:left'>Product received as per<br>above details along with <b>  " + (lblDMEditStatus.ToString() == "" ? "" : ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString()) + " crates</b><br><br> Receiver Signature<br>(Party)</td>");
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
    protected void btnApp_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DateTime supplyDate = DateTime.ParseExact(txtsupplydate.Text, "dd/MM/yyyy", culture);
                string supplyDateAll = supplyDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string ProductDispDeliveryChallanIdAll = "";
                int checkcount=0;
                foreach (GridViewRow gridrow in GridView1.Rows)
                {
                    //  string mpdid = GridView1.DataKeys[gridrow.RowIndex].Value.ToString();
                    CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");
                    Label lblProductDispDeliveryChallanId = (Label)gridrow.FindControl("lblProductDispDeliveryChallanId");
                    //Label lblMilkOrProductDemandId = (Label)gridrow.FindControl("lblMilkOrProductDemandId");

                    //ViewState["rowid"] = lblMilkOrProductDemandId.Text;
                    //ViewState["rowitemcatid"] = lblItemCatid.Text;
                    if (chkSelect.Checked == true)
                    {
                        if(checkcount==0)
                        {
                            ProductDispDeliveryChallanIdAll = lblProductDispDeliveryChallanId.Text;
                        }
                        else
                        {
                            ProductDispDeliveryChallanIdAll = ProductDispDeliveryChallanIdAll + "," + lblProductDispDeliveryChallanId.Text;
                        }
                        checkcount += 1;
                    }

                }
                if (ProductDispDeliveryChallanIdAll!="")
                {
                    ds2 = objdb.ByProcedure("USP_Trn_ProductDM",
                                                 new string[] { "flag", "ProductDispDeliveryChallanIdAll", "Office_ID", "SupplyDate","CreatedBy" },
                                                 new string[] { "23", ProductDispDeliveryChallanIdAll, objdb.Office_ID(), supplyDateAll, objdb.createdBy()}, "dataset");

                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                       // dtInsertChild.Dispose();
                      //  GetOrderDetails();
                        btnSearch_Click( sender,  e);
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        
                    }
                    else
                    {
                        string msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
                    }
                }
                else
                {
                   // string msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Please select atleast 1 DM");
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 24 :" + ex.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "RecordPrint")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;

    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                Label lblDMEditStatus = (Label)row.FindControl("lblDMEditStatus");
    //                PrintDMDetails(e.CommandArgument.ToString(), lblDMEditStatus.Text);
    //                GetDatatableHeaderDesign();

    //            }
    //        }

    //        else if (e.CommandName == "RecordRedirected")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;

    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

    //                Label lblAreaId = (Label)row.FindControl("lblAreaId");
    //                Label lblRouteId = (Label)row.FindControl("lblRouteId");
    //                Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
    //                Label lblDelivaryShiftid = (Label)row.FindControl("lblDelivaryShiftid");
    //                Label lblMilkOrProductDemandId = (Label)row.FindControl("lblMilkOrProductDemandId");
    //                Label lblProductDMStatus = (Label)row.FindControl("lblProductDMStatus");

    //                if (lblProductDMStatus.Text == "1" || lblProductDMStatus.Text == "2")
    //                {
    //                    string url;
    //                    url = "../Demand/CurrentDayProductDMGenerate.aspx?DSA=" +
    //                       objdb.Encrypt(lblAreaId.Text) + "&DSR=" +
    //                        objdb.Encrypt(lblRouteId.Text) + "&DSD=" +
    //                         objdb.Encrypt(lblDelivary_Date.Text) + "&DSS=" +
    //                          objdb.Encrypt(lblDelivaryShiftid.Text) + "&DSMPId=" +
    //                        objdb.Encrypt(lblMilkOrProductDemandId.Text) + "&DSDMStatus=" +
    //                        objdb.Encrypt(lblProductDMStatus.Text);
    //                    Response.Redirect(url, false);
    //                }
    //                else
    //                {
    //                    string url;
    //                    url = "ProductDMGenerate.aspx?DSA=" +
    //                       objdb.Encrypt(lblAreaId.Text) + "&DSR=" +
    //                        objdb.Encrypt(lblRouteId.Text) + "&DSD=" +
    //                         objdb.Encrypt(lblDelivary_Date.Text) + "&DSS=" +
    //                          objdb.Encrypt(lblDelivaryShiftid.Text) + "&DSMPId=" +
    //                         objdb.Encrypt(lblMilkOrProductDemandId.Text);
    //                    Response.Redirect(url, false);
    //                }
    //            }
    //        }

    //        else if (e.CommandName == "RecordEdit")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;
    //                lblModalMsg.Text = string.Empty;
    //                lnkFinalSubmit.Visible = true;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

    //                Label lblVDChallanNo = (Label)row.FindControl("lblVDChallanNo");
    //                Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
    //                Label lblRName = (Label)row.FindControl("lblRName");
    //                Label lblVehicleNo = (Label)row.FindControl("lblVehicleNo");
    //                Label lblTotalIssueCrate = (Label)row.FindControl("lblTotalIssueCrate");
    //                Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
    //                Label lblVehicleMilkOrProduct_ID = (Label)row.FindControl("lblVehicleMilkOrProduct_ID");
    //                ViewState["rowidedit"] = e.CommandArgument.ToString();
    //                GetItemDetailByProductDispDeliveryChallanId();
    //                modalChallan.InnerHtml = lblVDChallanNo.Text;
    //                modaldate.InnerHtml = lblDelivary_Date.Text;
    //                modalroute.InnerHtml = lblRName.Text;
    //                modalVehicle.InnerHtml = lblVehicleNo.Text;
    //                txtTotalCrate.Text = lblTotalIssueCrate.Text;
    //                ViewState["DistId"] = lblDistributorId.Text;
    //                GetVehicleNo();
    //                ddlVehicleNo.SelectedValue = lblVehicleMilkOrProduct_ID.Text;
    //                txtRejectRemark.Text = string.Empty;
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);


    //            }
    //        }
    //        else if (e.CommandName == "RecordPrintInvoice")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;

    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                if (objdb.Office_ID() == "5")
    //                {
    //                    PrintInvoiceCumBillDetails_jbl(e.CommandArgument.ToString());
    //                }
    //                else
    //                {
    //                    PrintInvoiceCumBillDetails(e.CommandArgument.ToString());
    //                }
    //                GetDatatableHeaderDesign();

    //            }
    //        }
    //        else if (e.CommandName == "UpdateCrate")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;

    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                Label lblVDChallanNo = (Label)row.FindControl("lblVDChallanNo");
    //                Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
    //                Label lblRName = (Label)row.FindControl("lblRName");
    //                Label lblVehicleNo = (Label)row.FindControl("lblVehicleNo");
    //                Label lblTotalIssueCrate = (Label)row.FindControl("lblTotalIssueCrate");
    //                ViewState["updatecraterowid"] = e.CommandArgument.ToString();
    //                Crate_modalChallan.InnerHtml = lblVDChallanNo.Text;
    //                Crate_modaldate.InnerHtml = lblDelivary_Date.Text;
    //                Crate_modalroute.InnerHtml = lblRName.Text;
    //                Crate_modalVehicle.InnerHtml = lblVehicleNo.Text;
    //                txtUpdateTotalIssueCrate.Text = lblTotalIssueCrate.Text;
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModalForCrate()", true);

    //            }
    //        }
    //        else if (e.CommandName == "ItemReturn")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;
    //                lblModalReturnMsg.Text = string.Empty;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

    //                Label lblVDChallanNo = (Label)row.FindControl("lblVDChallanNo");
    //                Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
    //                Label lblRName = (Label)row.FindControl("lblRName");
    //                Label lblDName = (Label)row.FindControl("lblDName");
    //                Label lblShiftName = (Label)row.FindControl("lblShiftName");
    //                ViewState["ProductDispDeliveryChallanChildId"] = e.CommandArgument.ToString();
    //                GetItemDetailByProductDispDeliveryChallanId_Return();
    //                modalDistName.InnerHtml = lblDName.Text;
    //                modalroutename.InnerHtml = lblRName.Text;
    //                modalreturndelivarydate.InnerHtml = lblDelivary_Date.Text;
    //                modalshift.InnerHtml = lblShiftName.Text;
    //                modalChallanNo_Return.InnerHtml = lblVDChallanNo.Text;

    //                txtSalesReturnDate.Text = string.Empty;
    //                string Date = DateTime.Now.ToString("dd/MM/yyyy");
    //                txtSalesReturnDate.Text = Date;
    //                txtSalesReturnDate.Attributes.Add("readonly", "readonly");
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReturnItemModal()", true);


    //            }
    //        }
    //        else if (e.CommandName == "ItemReplace")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;
    //                lblModalMsgReplace.Text = string.Empty;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

    //                Label lblVDChallanNo = (Label)row.FindControl("lblVDChallanNo");
    //                Label lblDelivary_Date = (Label)row.FindControl("lblDelivary_Date");
    //                Label lblRName = (Label)row.FindControl("lblRName");
    //                Label lblDName = (Label)row.FindControl("lblDName");
    //                Label lblShiftName = (Label)row.FindControl("lblShiftName");
    //                ViewState["PDDCId"] = e.CommandArgument.ToString();
    //                GetItemDetailByProductDispDeliveryChallanId_Replace();
    //                modalDistNameReplace.InnerHtml = lblDName.Text;
    //                modalroutenameReplace.InnerHtml = lblRName.Text;
    //                modalreturndelivarydateReplace.InnerHtml = lblDelivary_Date.Text;
    //                modalshiftReplace.InnerHtml = lblShiftName.Text;
    //                modalChallanNoReplace.InnerHtml = lblVDChallanNo.Text;

    //                txtSalesReplaceDate.Text = string.Empty;
    //                string Date = DateTime.Now.ToString("dd/MM/yyyy");
    //                txtSalesReplaceDate.Text = Date;
    //                txtSalesReplaceDate.Attributes.Add("readonly", "readonly");
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReplaceItemModal()", true);


    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
    //    }
    //}
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlLocation.SelectedIndex = 0;
        GetRoute();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }

    //private void GetItemDetailByProductDispDeliveryChallanId()
    //{
    //    try
    //    {
    //        ds5 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //            new string[] { "flag", "ProductDispDeliveryChallanId", "Office_ID" },
    //              new string[] { "1", ViewState["rowidedit"].ToString(), objdb.Office_ID() }, "dataset");
    //        if (ds5.Tables[0].Rows.Count > 0)
    //        {
    //            GridView4.DataSource = ds5.Tables[0];
    //            GridView4.DataBind();
    //        }
    //        else
    //        {
    //            GridView4.DataSource = null;
    //            GridView4.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds5 != null) { ds5.Dispose(); }
    //    }
    //}
    //protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "RecordEdit")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;
    //                lblModalMsg.Text = string.Empty;
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
    //                TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
    //                LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
    //                LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
    //                LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
    //                RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
    //                RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

    //                foreach (GridViewRow gvRow in GridView4.Rows)
    //                {
    //                    Label HlblSupplyTotalQty = (Label)gvRow.FindControl("lblSupplyTotalQty");
    //                    TextBox HtxtSupplyTotalQty = (TextBox)gvRow.FindControl("txtSupplyTotalQty");
    //                    LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
    //                    LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
    //                    LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
    //                    RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
    //                    RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
    //                    HlblSupplyTotalQty.Visible = true;
    //                    HtxtSupplyTotalQty.Visible = false;
    //                    HlnkEdit.Visible = true;
    //                    HlnkUpdate.Visible = false;
    //                    HlnkReset.Visible = false;
    //                    Hrfv.Enabled = false;
    //                    Hrev1.Enabled = false;

    //                }
    //                txtSupplyTotalQty.Text = "";
    //                txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
    //                lblSupplyTotalQty.Visible = false;
    //                lnkEdit.Visible = false;

    //                lnkUpdate.Visible = true;
    //                lnkReset.Visible = true;
    //                rfv.Enabled = true;
    //                rev1.Enabled = true;
    //                txtSupplyTotalQty.Visible = true;
    //                lnkFinalSubmit.Visible = false;

    //            }

    //        }
    //        if (e.CommandName == "RecordReset")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    //                lblMsg.Text = string.Empty;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
    //                TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
    //                LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
    //                LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
    //                LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
    //                RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
    //                RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

    //                lblModalMsg.Text = string.Empty;

    //                lblSupplyTotalQty.Visible = true;
    //                lnkEdit.Visible = true;


    //                lnkUpdate.Visible = false;
    //                lnkReset.Visible = false;
    //                rfv.Enabled = false;
    //                rev1.Enabled = false;
    //                txtSupplyTotalQty.Visible = false;
    //                lnkFinalSubmit.Visible = true;
    //            }

    //        }
    //        //if (e.CommandName == "RecordUpdate")
    //        //{
    //        //    if (Page.IsValid)
    //        //    {
    //        //        Control ctrl = e.CommandSource as Control;
    //        //        if (ctrl != null)
    //        //        {
    //        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    //        //            lblMsg.Text = string.Empty;
    //        //            GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //        //            Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
    //        //            TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
    //        //            Label lblItem_id = (Label)row.FindControl("lblItem_id");
    //        //            HiddenField HFTotalAmt = (HiddenField)row.FindControl("HFTotalAmt");
    //        //            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
    //        //            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
    //        //            LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");

    //        //            //if (txtSupplyTotalQty.Text == "0" || txtSupplyTotalQty.Text == "0.00" && txtSupplyTotalQty.Text == "0.000")
    //        //            //{
    //        //            //    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Supply Qty");
    //        //            //    return;
    //        //            //}
    //        //            //else
    //        //            //{
    //        //                DateTime odate = DateTime.ParseExact(modaldate.InnerHtml, "dd/MM/yyyy", culture);
    //        //                string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //        //                ds4 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //        //                 new string[] { "flag", "ProductDispDeliveryChallanChildId", "SupplyQty", "Amount", "UpdatedBy", "Item_id", "Office_ID", "ItemCat_id", "Delivery_Date" },
    //        //                 new string[] { "2", e.CommandArgument.ToString(), txtSupplyTotalQty.Text, HFTotalAmt.Value, objdb.createdBy(), lblItem_id.Text, objdb.Office_ID(), objdb.GetProductCatId(), deliverydate}, "TableSave");

    //        //                if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        //                {
    //        //                    //gheestock updation
    //        //                    if (ds4.Tables.Count > 1)
    //        //                    {
    //        //                        if (ds4.Tables[1].Rows.Count > 0)
    //        //                        {
    //        //                            string ItemID = "", ItemQty = "", NetTotalValue = "", StockTxnID = "", StockDate = "";
    //        //                            for (int j = 0; j < ds4.Tables[1].Rows.Count; j++)
    //        //                            {
    //        //                                ItemID = ds4.Tables[1].Rows[j]["item_id"].ToString();
    //        //                                ItemQty = ds4.Tables[1].Rows[j]["SupplyQty"].ToString();
    //        //                                StockDate = ds4.Tables[1].Rows[j]["StockDate"].ToString();
    //        //                                NetTotalValue = ds4.Tables[1].Rows[j]["Amount"].ToString();
    //        //                                StockTxnID = ds4.Tables[1].Rows[j]["ProductDispDeliveryChallanChildId"].ToString();



    //        //                                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://45.114.143.215:8202//api/data/PostGheeStock");
    //        //                                httpWebRequest.ContentType = "application/json; charset=utf-8";
    //        //                                httpWebRequest.Method = "POST";

    //        //                                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    //        //                                {
    //        //                                    string json = "{\"DS_Office_ID\":\"" + objdb.Office_ID() + "\"," +
    //        //                                                   "\"StockDate\":\"" + StockDate + "\"," +
    //        //                                                  "\"UpdatedBy\":\"" + objdb.createdBy() + "\"," +
    //        //                                                  "\"dataOperation\":\"UPDATE\"," +
    //        //                                                  "\"ItemID\":\"" + ItemID + "\"," +
    //        //                                                  "\"ItemQty\":\"" + ItemQty + "\"," +
    //        //                                                  "\"NetTotalValue\":\"" + NetTotalValue + "\"," +
    //        //                                                  "\"StockTxnID\":\"" + StockTxnID + "\"}";


    //        //                                    streamWriter.Write(json);
    //        //                                    Debug.Write(json);
    //        //                                    streamWriter.Write(json);
    //        //                                    streamWriter.Flush();
    //        //                                    streamWriter.Close();
    //        //                                }

    //        //                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //        //                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //        //                                {
    //        //                                    var result = streamReader.ReadToEnd();
    //        //                                }



    //        //                            }

    //        //                        }
    //        //                    }

    //        //                    string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //        //                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //        //                    GetItemDetailByProductDispDeliveryChallanId();
    //        //                    lnkFinalSubmit.Visible = true;
    //        //                }
    //        //                else
    //        //                {
    //        //                    string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //        //                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
    //        //                }
    //        //}
    //        //}
    //        //}
    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6: " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds4 != null) { ds4.Dispose(); }
    //    }

    //}
    //protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;
    //            if (lblItemCatName.Text == "Milk")
    //            {
    //                e.Row.CssClass = "columnmilk";
    //            }
    //            else
    //            {
    //                e.Row.CssClass = "columnproduct";
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7 : " + ex.Message.ToString());
    //    }
    //}

    //private void UpdateEditStatus()
    //{
    //    try
    //    {
    //        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //        ds9 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                        new string[] { "flag", "ProductDispDeliveryChallanId", "TotalIssueCrate", "VehicleMilkOrProduct_ID", "CreatedBy", "CreatedByIP" },
    //                        new string[] { "3", ViewState["rowidedit"].ToString(), txtTotalCrate.Text.Trim(), ddlVehicleNo.SelectedValue, objdb.createdBy(), IPAddress }, "TableSave");

    //        if (ds9.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {
    //            string success = ds9.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //        }
    //        else
    //        {
    //            string error = ds9.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Update Edit Status : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds9 != null) { ds9.Dispose(); }
    //    }
    //}
    //private void RejectDM()
    //{
    //    try
    //    {
    //        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //        ds6 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                        new string[] { "flag", "ProductDispDeliveryChallanId", "RejectedRemark", "CreatedBy", "CreatedByIP" },
    //                        new string[] { "5", ViewState["rowidedit"].ToString(), txtRejectRemark.Text.Trim(), objdb.createdBy(), IPAddress }, "TableSave");

    //        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {

    //            ////gheestock updation
    //            //if (ds6.Tables.Count > 1)
    //            //{
    //            //    if (ds6.Tables[1].Rows.Count > 0)
    //            //    {
    //            //        string ItemID = "", ItemQty = "", NetTotalValue = "", StockTxnID = "", StockDate="";
    //            //        for (int j = 0; j < ds6.Tables[1].Rows.Count; j++)
    //            //        {
    //            //            ItemID = ds6.Tables[1].Rows[j]["item_id"].ToString();
    //            //            ItemQty = ds6.Tables[1].Rows[j]["TotalQty"].ToString();
    //            //            StockDate = ds6.Tables[1].Rows[j]["StockDate"].ToString();
    //            //            NetTotalValue = ds6.Tables[1].Rows[j]["Amount"].ToString();
    //            //            StockTxnID = ds6.Tables[1].Rows[j]["ProductDispDeliveryChallanChildId"].ToString();



    //            //            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://45.114.143.215:8202//api/data/PostGheeStock");
    //            //            httpWebRequest.ContentType = "application/json; charset=utf-8";
    //            //            httpWebRequest.Method = "POST";

    //            //            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    //            //            {
    //            //                string json = "{\"DS_Office_ID\":\"" + objdb.Office_ID() + "\"," +
    //            //                               "\"StockDate\":\"" + StockDate + "\"," +
    //            //                              "\"UpdatedBy\":\"" + objdb.createdBy() + "\"," +
    //            //                              "\"dataOperation\":\"REJECT\"," +
    //            //                              "\"ItemID\":\"" + ItemID + "\"," +
    //            //                              "\"ItemQty\":\"" + ItemQty + "\"," +
    //            //                              "\"NetTotalValue\":\"" + NetTotalValue + "\"," +
    //            //                              "\"StockTxnID\":\"" + StockTxnID + "\"}";


    //            //                streamWriter.Write(json);
    //            //                Debug.Write(json);
    //            //                streamWriter.Write(json);
    //            //                streamWriter.Flush();
    //            //                streamWriter.Close();
    //            //            }

    //            //            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //            //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //            //            {
    //            //                var result = streamReader.ReadToEnd();
    //            //            }



    //            //        }

    //            //    }
    //            //}

    //            string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //        }
    //        else
    //        {
    //            string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Reject DM : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds6 != null) { ds6.Dispose(); }
    //    }
    //}
    //private void UpdateCrate()
    //{
    //    try
    //    {
    //        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //        ds7 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                        new string[] { "flag", "ProductDispDeliveryChallanId", "TotalIssueCrate", "CreatedBy", "CreatedByIP" },
    //                        new string[] { "6", ViewState["updatecraterowid"].ToString(), txtUpdateTotalIssueCrate.Text.Trim(), objdb.createdBy(), IPAddress }, "TableSave");

    //        if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {
    //            string success = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //        }
    //        else
    //        {
    //            string error = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Update Crate : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds7 != null) { ds7.Dispose(); }
    //    }
    //}
    //protected void lnkFinalSubmit_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        lblMsg.Text = string.Empty;
    //        UpdateEditStatus();
    //        GetDMDetails();
    //    }

    //}

    //private void PrintInvoiceCumBillDetails_jbl(string dmid)
    //{
    //    try
    //    {
    //        dsInvo = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                 new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
    //                 new string[] { "4", objdb.Office_ID(), dmid }, "dataset");

    //        if (dsInvo.Tables[0].Rows.Count > 0)
    //        {
    //            long dno = long.Parse(dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString());
    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("<div>");
    //            if (objdb.Office_ID() == "5")
    //            {
    //                sb.Append("<h2 style='text-align:center'>Tax Invoice</h2>");
    //            }
    //            else
    //            {
    //                sb.Append("<h2 style='text-align:center'>Invoice-cum-Bill of Supply</h2>");
    //            }
    //            sb.Append("<table class='table1' style='width:100%'>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='7' rowspan='2'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='7' rowspan='2'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }

    //            //sb.Append("</tr>");
    //            //sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='1'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>PO No.</br><b>" + dsInvo.Tables[0].Rows[0]["PONumber"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='1'>PO Date</br><b>" + dsInvo.Tables[0].Rows[0]["PODdate"].ToString() + "</b></td>");
    //            sb.Append("</tr>");

    //            //sb.Append("<td colspan='3'>Other Reference(s)</td>");
    //            sb.Append("</tr>");
    //            //sb.Append("<tr>");
    //            //sb.Append("<td colspan='3'>Delivery Note</td>");
    //            //sb.Append("<td colspan='3'>Mode/Terms of Payment</td>");
    //            //sb.Append("</tr>");
    //            //sb.Append("<tr>");
    //            //sb.Append("<td colspan='3'>Supplier's Ref</td>");
    //            //sb.Append("<td colspan='3'>Other Reference(s)</td>");
    //            //sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (dsInvo.Tables[0].Rows[0]["DName"].ToString() == dsInvo.Tables[0].Rows[0]["BName"].ToString())
    //            {
    //                if (objdb.Office_ID() == "5")
    //                {
    //                    sb.Append("<td colspan='7' rowspan='4'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspPAN No. :" + dsInvo.Tables[0].Rows[0]["PANNo"].ToString() + "</br>Mobile No.:" + dsInvo.Tables[0].Rows[0]["MobileNo"].ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspSecurity Amt. :" + dsInvo.Tables[0].Rows[0]["SecurityDeposit"].ToString() + "</br> Bank Guarantee :-" + dsInvo.Tables[0].Rows[0]["BankGuarantee"].ToString() + "</br>State Name: Madhya Pradesh, Code :23 </td>");
    //                }
    //                else
    //                {
    //                    sb.Append("<td colspan='7' rowspan='4'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>Mobile No.:" + dsInvo.Tables[0].Rows[0]["MobileNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23 </td>");
    //                }

    //            }
    //            else
    //            {
    //                if (objdb.Office_ID() == "5")
    //                {
    //                    sb.Append("<td colspan='7' rowspan='4'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + " ( " + dsInvo.Tables[0].Rows[0]["BName"].ToString() + " )</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspPAN No. :" + dsInvo.Tables[0].Rows[0]["PANNo"].ToString() + "</br>Mobile No.:" + dsInvo.Tables[0].Rows[0]["MobileNo"].ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspSecurity Amt. :" + dsInvo.Tables[0].Rows[0]["SecurityDeposit"].ToString() + "</br> Bank Guarantee :-" + dsInvo.Tables[0].Rows[0]["BankGuarantee"].ToString() + "</br>State Name: Madhya Pradesh, Code :23 </td>");
    //                }
    //                else
    //                {
    //                    sb.Append("<td colspan='7' rowspan='4'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + " ( " + dsInvo.Tables[0].Rows[0]["BName"].ToString() + " )</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>Mobile No.:" + dsInvo.Tables[0].Rows[0]["MobileNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //                }
    //            }
    //            sb.Append("</tr>");
    //            //sb.Append("<tr>");
    //            //sb.Append("<td colspan='3'>Buyer's Order No.</td>");
    //            //sb.Append("<td colspan='3'>Dated</td>");
    //            //sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Dispatch Document No. :</br><b>" + dno.ToString() + "</td>");
    //            sb.Append("<td colspan='1'>Delivery Note Date</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Dispatched through</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='1'>Destination :</br><b>" + dsInvo.Tables[0].Rows[0]["destination"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3'>Terms of Delivery</td>");

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>S.No</td>");
    //            sb.Append("<td colspan='4' style='text-align:center'>Description of Goods</td>");
    //            sb.Append("<td style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td style='text-align:center'>Quantity</td>");
    //            sb.Append("<td style='text-align:center'>Rate</td>");
    //            sb.Append("<td style='text-align:center'>Per</td>");
    //            sb.Append("<td style='text-align:center'>Amount</td>");
    //            sb.Append("</tr>");

    //            int TCount = dsInvo.Tables[0].Rows.Count;
    //            for (int i = 0; i < TCount; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>" + (i + 1).ToString() + "</td>");
    //                sb.Append("<td colspan='4'><b>" + dsInvo.Tables[0].Rows[i]["ItemName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSNCode"].ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["SupplyQty"].ToString() + "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["ItemRate"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'><b>" + dsInvo.Tables[0].Rows[i]["Amount"].ToString() + "</b></td>");
    //                sb.Append("</tr>");
    //            }
    //            // decimal TAmount = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount1"));
    //            decimal TAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
    //            decimal TCSTAX = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TCSTaxAmt"));
    //            decimal TCGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CentralTax"));
    //            decimal TSGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("StateTax"));

    //            decimal Total = TAmount + TCGST + TSGST + TCSTAX;
    //            //decimal Total = TAmount + TCSTAX;
    //            string Amount = GenerateWordsinRs(Total.ToString("0.00"));
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4'></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'>" + TAmount.ToString() + "</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>CGST</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>SGST</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>Tcs on Sales @</b></td>");
    //            sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX.ToString() : "NA") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>Total<b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + Total.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='10'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3' rowspan='2' style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td rowspan='2' colspan='2' style='text-align:center'>Taxable Value</td>");
    //            sb.Append("<td colspan='2' style='text-align:center'>Central Tax</td>");
    //            sb.Append("<td colspan='2' style='text-align:center'>State Tax</td>");
    //            sb.Append("<td rowspan='2'  style='text-align:center'>Total Tax Amount</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>Rate</td>");
    //            sb.Append("<td style='text-align:center'>Amount</td>");
    //            sb.Append("<td style='text-align:center'>Rate</td>");
    //            sb.Append("<td style='text-align:center'>Amount</td>");
    //            sb.Append("</tr>");
    //            int TCount1 = dsInvo.Tables[1].Rows.Count;
    //            for (int i = 0; i < TCount1; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td colspan='3'>" + dsInvo.Tables[1].Rows[i]["HSNCode"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right' colspan='2'>" + dsInvo.Tables[1].Rows[i]["TaxableValue"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["CGST"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["CentralTax"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["SGST"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["StateTax"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right' >" + dsInvo.Tables[1].Rows[i]["TotalTaxAmount"].ToString() + "</td>");

    //                sb.Append("</tr>");
    //            }
    //            decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
    //            decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
    //            string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3' style='text-align:right'><b>Total</b></td>");
    //            sb.Append("<td style='text-align:right' colspan='2'><b>" + TTaxableValue.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TotalTaxAmount.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + " </br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            sb.Append("</tr>");

    //            sb.Append("</table>");
    //            Print.InnerHtml = sb.ToString();
    //            ClientScriptManager CSM = Page.ClientScript;
    //            string strScript = "<script>";
    //            strScript += "window.print();";

    //            strScript += "</script>";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (dsInvo != null) { dsInvo.Dispose(); }
    //    }

    //}
    //private void PrintInvoiceCumBillDetails(string dmid)
    //{
    //    try
    //    {
    //        dsInvo = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                 new string[] { "flag", "Office_ID", "ProductDispDeliveryChallanId" },
    //                 new string[] { "4", objdb.Office_ID(), dmid }, "dataset");

    //        if (dsInvo.Tables[0].Rows.Count > 0)
    //        {
    //            long dno = long.Parse(dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString());
    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("<div>");
    //            sb.Append("<h2 style='text-align:center'>Invoice-cum-Bill of Supply</h2>");
    //            sb.Append("<table class='table1' style='width:100%'>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='7' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + " - (From 1-Jul-2017)</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='7' rowspan='4'><b>" + dsInvo.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></br>" + dsInvo.Tables[0].Rows[0]["Office_Address"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["Office_Address1"].ToString() + "</br>GSTIN/UIN:" + dsInvo.Tables[0].Rows[0]["Office_Gst"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            }

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Invoice No.</br><b>" + dsInvo.Tables[0].Rows[0]["DMChallanNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='1'>Dated</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Delivery Note</td>");
    //            sb.Append("<td colspan='1'>Mode/Terms of Payment</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Supplier's Ref</td>");
    //            sb.Append("<td colspan='1'>Other Reference(s)</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='7' rowspan='5'>Buyer</br><b>" + dsInvo.Tables[0].Rows[0]["DName"].ToString() + "</b></br>Prod." + dsInvo.Tables[0].Rows[0]["RName"].ToString() + "</br>" + dsInvo.Tables[0].Rows[0]["DAddress"].ToString() + "</br>GSTIN/UIN   :" + dsInvo.Tables[0].Rows[0]["GSTNo"].ToString() + "</br>Mobile No.:" + dsInvo.Tables[0].Rows[0]["MobileNo"].ToString() + "</br>State Name: Madhya Pradesh, Code :23</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Buyer's Order No.</td>");
    //            sb.Append("<td colspan='1'>Dated</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Dispatch Document No.:</br><b>" + dno.ToString() + "</b></td>");
    //            sb.Append("<td colspan='1'>Delivery Note Date</br><b>" + dsInvo.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='2'>Dispatched through</br><b>" + dsInvo.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b></td>");
    //            sb.Append("<td colspan='1'>Destination :</br><b>" + dsInvo.Tables[0].Rows[0]["destination"].ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3'>Terms of Delivery</td>");

    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>S.No</td>");
    //            sb.Append("<td colspan='4' style='text-align:center'>Description of Goods</td>");
    //            sb.Append("<td style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td style='text-align:center'>Quantity</td>");
    //            sb.Append("<td style='text-align:center'>Rate</td>");
    //            sb.Append("<td style='text-align:center'>Per</td>");
    //            sb.Append("<td style='text-align:center'>Amount</td>");
    //            sb.Append("</tr>");

    //            int TCount = dsInvo.Tables[0].Rows.Count;
    //            for (int i = 0; i < TCount; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td>" + (i + 1).ToString() + "</td>");
    //                sb.Append("<td colspan='4'><b>" + dsInvo.Tables[0].Rows[i]["ItemName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["HSNCode"].ToString() + "</td>");
    //                sb.Append("<td><b>" + dsInvo.Tables[0].Rows[i]["SupplyQty"].ToString() + "&nbsp;&nbsp;&nbsp;" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</b></td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["ItemRate"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[0].Rows[i]["PackagingModeName"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'><b>" + dsInvo.Tables[0].Rows[i]["Amount"].ToString() + "</b></td>");
    //                sb.Append("</tr>");
    //            }
    //            decimal TAmount = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
    //            decimal TCSTAX = dsInvo.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TCSTaxAmt"));
    //            decimal TCGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CentralTax"));
    //            decimal TSGST = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("StateTax"));

    //            decimal Total = TAmount + TCGST + TSGST + TCSTAX;
    //            string Amount = GenerateWordsinRs(Total.ToString("0.00"));
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4'></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'>" + TAmount.ToString() + "</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>CGST</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>SGST</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>Tcs on Sales @</b></td>");
    //            sb.Append("<td>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() : "NA") + "</td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>" + (dsInvo.Tables[0].Rows[0]["TcsTaxPer"].ToString() != "0.000" ? TCSTAX.ToString() : "NA") + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");

    //            sb.Append("<td></td>");
    //            sb.Append("<td colspan='4' style='text-align:right'><b>Total<b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + Total.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='10'>Amount Chargeable(In Words)</br><b>INR " + Amount + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3' rowspan='2' style='text-align:center'>HSN/SAC</td>");
    //            sb.Append("<td rowspan='2' colspan='2' style='text-align:center'>Taxable Value</td>");
    //            sb.Append("<td colspan='2' style='text-align:center'>Central Tax</td>");
    //            sb.Append("<td colspan='2' style='text-align:center'>State Tax</td>");
    //            sb.Append("<td rowspan='2' style='text-align:center'>Total Tax Amount</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            sb.Append("<td style='text-align:center'>Rate</td>");
    //            sb.Append("<td style='text-align:center'>Amount</td>");
    //            sb.Append("<td style='text-align:center'>Rate</td>");
    //            sb.Append("<td style='text-align:center'>Amount</td>");
    //            sb.Append("</tr>");
    //            int TCount1 = dsInvo.Tables[1].Rows.Count;
    //            for (int i = 0; i < TCount1; i++)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td colspan='3'>" + dsInvo.Tables[1].Rows[i]["HSNCode"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right' colspan='2'>" + dsInvo.Tables[1].Rows[i]["TaxableValue"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["CGST"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["CentralTax"].ToString() + "</td>");
    //                sb.Append("<td>" + dsInvo.Tables[1].Rows[i]["SGST"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["StateTax"].ToString() + "</td>");
    //                sb.Append("<td style='text-align:right'>" + dsInvo.Tables[1].Rows[i]["TotalTaxAmount"].ToString() + "</td>");

    //                sb.Append("</tr>");
    //            }
    //            decimal TTaxableValue = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
    //            decimal TotalTaxAmount = dsInvo.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmount"));
    //            string TTaxAmount = GenerateWordsinRs(TotalTaxAmount.ToString("0.00"));
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='3' style='text-align:right'><b>Total</b></td>");
    //            sb.Append("<td style='text-align:right' colspan='2'><b>" + TTaxableValue.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TCGST.ToString() + "</b></td>");
    //            sb.Append("<td></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TSGST.ToString() + "</b></td>");
    //            sb.Append("<td style='text-align:right'><b>" + TotalTaxAmount.ToString() + "</b></td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            //sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br></br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
    //            sb.Append("<td  rowspan ='2' colspan='7' >Tax Amount (In Words)&nbsp;&nbsp;:&nbsp;&nbsp;<b>INR " + TTaxAmount + "</b></br></br>Remark : " + dsInvo.Tables[0].Rows[0]["CreatedRemark"].ToString() + " </br></br>Declaration:</br>We declare that tis invoice shows the actual price of the goods</br> described and that all particulars are true and correct.</td>");
    //            sb.Append("</tr>");
    //            sb.Append("<tr>");
    //            if (objdb.Office_ID() == "2")
    //            {
    //                sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for Bhopal Sah.Dugdha Sangh - (From 1-Jul-2017)</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td colspan='4' style='border-top:1px solid black; border-left:1px solid black;'>for " + ViewState["Office_Name"].ToString() + "</br></br></br></br></br><span style='padding-left:300px; padding-top:1100px;'>Authorised Signatory</span></td>");
    //            }
    //            sb.Append("</tr>");

    //            sb.Append("</table>");
    //            Print.InnerHtml = sb.ToString();
    //            ClientScriptManager CSM = Page.ClientScript;
    //            string strScript = "<script>";
    //            strScript += "window.print();";

    //            strScript += "</script>";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (dsInvo != null) { dsInvo.Dispose(); }
    //    }

    //}
    //private string GenerateWordsinRs(string value)
    //{
    //    decimal numberrs = Convert.ToDecimal(value);
    //    CultureInfo ci = new CultureInfo("en-IN");
    //    string aaa = String.Format("{0:#,##0.##}", numberrs);
    //    aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
    //    // label6.Text = aaa;


    //    string input = value;
    //    string a = "";
    //    string b = "";

    //    // take decimal part of input. convert it to word. add it at the end of method.
    //    string decimals = "";

    //    if (input.Contains("."))
    //    {
    //        decimals = input.Substring(input.IndexOf(".") + 1);
    //        // remove decimal part from input
    //        input = input.Remove(input.IndexOf("."));

    //    }
    //    string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

    //    if (!value.Contains("."))
    //    {
    //        a = strWords + " Rupees Only";
    //    }
    //    else
    //    {
    //        a = strWords + " Rupees";
    //    }

    //    if (decimals.Length > 0)
    //    {
    //        // if there is any decimal part convert it to words and add it to strWords.
    //        string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
    //        b = " and " + strwords2 + " Paisa Only ";
    //    }

    //    return a + b;
    //}
    //protected void lnkReject_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        lblMsg.Text = string.Empty;
    //        RejectDM();
    //        GetDMDetails();
    //    }
    //}
    //protected void lnkUpdateCrate_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        lblMsg.Text = string.Empty;
    //        UpdateCrate();
    //        GetDMDetails();
    //    }
    //}
    //// start code for return qty
    //private void GetItemDetailByProductDispDeliveryChallanId_Return()
    //{
    //    try
    //    {
    //        ds5 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //            new string[] { "flag", "ProductDispDeliveryChallanId", "Office_ID" },
    //              new string[] { "1", ViewState["ProductDispDeliveryChallanChildId"].ToString(), objdb.Office_ID() }, "dataset");
    //        if (ds5.Tables[0].Rows.Count > 0)
    //        {
    //            GridView2.DataSource = ds5.Tables[0];
    //            GridView2.DataBind();
    //        }
    //        else
    //        {
    //            GridView2.DataSource = null;
    //            GridView2.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds5 != null) { ds5.Dispose(); }
    //    }
    //}
    //private void InsertReturn()
    //{

    //    DateTime date3 = DateTime.ParseExact(modalreturndelivarydate.InnerHtml, "dd/MM/yyyy", culture);
    //    string delidate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //    DateTime redat = DateTime.ParseExact(txtSalesReturnDate.Text, "dd/MM/yyyy", culture);
    //    string redate = redat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //    DataTable dtInsertChild = new DataTable();
    //    DataRow drIC;
    //    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //    int iddata = 0, SupplyQty = 0, LastSupplyQtyBeforeReturn = 0;
    //    dtInsertChild.Columns.Add("ProductDispDeliveryChallanChildId", typeof(int));
    //    dtInsertChild.Columns.Add("ReturnQty", typeof(int));
    //    dtInsertChild.Columns.Add("ReturnDate", typeof(string));
    //    dtInsertChild.Columns.Add("ReturnRemark", typeof(string));
    //    dtInsertChild.Columns.Add("ReturnBy", typeof(int));
    //    dtInsertChild.Columns.Add("ReturnByIP", typeof(string));
    //    dtInsertChild.Columns.Add("SupplyQty", typeof(int));
    //    dtInsertChild.Columns.Add("LastSupplyQtyBeforeReturn", typeof(int));
    //    drIC = dtInsertChild.NewRow();
    //    foreach (GridViewRow gridrow in GridView2.Rows)
    //    {
    //        CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");
    //        Label lblProductDispDeliveryChallanChildId = (Label)gridrow.FindControl("lblProductDispDeliveryChallanChildId");
    //        TextBox txtTotalReturnQty = (TextBox)gridrow.FindControl("txtTotalReturnQty");
    //        HiddenField HFTotalAmtReturn = (HiddenField)gridrow.FindControl("HFTotalAmtReturn");
    //        TextBox txtReturnRemark = (TextBox)gridrow.FindControl("txtReturnRemark");
    //        TextBox txtSupplyQty = (TextBox)gridrow.FindControl("txtSupplyQty");
    //        Label lblLastSupplyQtyBeforeReturn = (Label)gridrow.FindControl("lblLastSupplyQtyBeforeReturn");

    //        if (chkSelect.Checked == true && txtTotalReturnQty.Text != "")
    //        {
    //            ++iddata;
    //            if (int.Parse(lblLastSupplyQtyBeforeReturn.Text) == 0)
    //            {
    //                SupplyQty = int.Parse(txtSupplyQty.Text) - int.Parse(txtTotalReturnQty.Text);
    //                LastSupplyQtyBeforeReturn = int.Parse(txtSupplyQty.Text);
    //            }
    //            else
    //            {
    //                SupplyQty = int.Parse(lblLastSupplyQtyBeforeReturn.Text) - int.Parse(txtTotalReturnQty.Text);
    //                LastSupplyQtyBeforeReturn = int.Parse(lblLastSupplyQtyBeforeReturn.Text);
    //            }


    //            drIC[0] = lblProductDispDeliveryChallanChildId.Text;
    //            drIC[1] = txtTotalReturnQty.Text;
    //            drIC[2] = redate;
    //            drIC[3] = txtReturnRemark.Text;
    //            drIC[4] = objdb.createdBy();
    //            drIC[5] = IPAddress;
    //            drIC[6] = SupplyQty.ToString();
    //            drIC[7] = LastSupplyQtyBeforeReturn.ToString();


    //            dtInsertChild.Rows.Add(drIC.ItemArray);
    //        }

    //    }
    //    if (dtInsertChild.Rows.Count > 0)
    //    {
    //        ds2 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_RR",
    //                                     new string[] { "Flag", "Office_ID", "ItemCat_id", "Delivery_Date" },
    //                                     new string[] { "1", objdb.Office_ID(), objdb.GetProductCatId(), delidate }
    //                                     , "type_Trn_ProductDispDeliveryChallanChild_Return", dtInsertChild, "TableSave");

    //        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {
    //            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            dtInsertChild.Dispose();

    //            lblModalReturnMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //            if (ds2 != null) { ds2.Dispose(); }
    //            GetItemDetailByProductDispDeliveryChallanId_Return();
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReturnItemModal()", true);
    //        }
    //        else
    //        {
    //            string msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblModalReturnMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
    //            if (ds2 != null) { ds2.Dispose(); }
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReturnItemModal()", true);
    //        }
    //    }
    //    else
    //    {
    //        lblModalMsgReplace.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Please Enter at least one return qty.");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReturnItemModal()", true);
    //    }
    //}
    //protected void btnReturnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Page.IsValid)
    //        {
    //            lblModalReturnMsg.Text = string.Empty;
    //            string myStringfromdat = modalreturndelivarydate.InnerHtml;
    //            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //            string myStringtodate = txtSalesReturnDate.Text;
    //            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //            if (fdate <= tdate)
    //            {
    //                lblModalReturnMsg.Text = string.Empty;
    //                InsertReturn();
    //            }
    //            else
    //            {
    //                lblModalReturnMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Sales Return Date must be greater than or equal to Date :  " + myStringfromdat);
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReturnItemModal()", true);
    //                return;
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalReturnMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "return save : " + ex.Message.ToString());
    //    }

    //}

    //private void FinalSaveForReturn()
    //{
    //    try
    //    {
    //        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //        ds7 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                        new string[] { "flag", "ProductDispDeliveryChallanId", "CreatedBy", "CreatedByIP" },
    //                        new string[] { "8", ViewState["ProductDispDeliveryChallanChildId"].ToString(), objdb.createdBy(), IPAddress }, "TableSave");

    //        if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {
    //            string success = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //            GetDMDetails();
    //        }
    //        else
    //        {
    //            string error = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Final Save for Return :" + error);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Final Save for Return : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds7 != null) { ds7.Dispose(); }
    //    }
    //}

    //protected void lnkFinalSubmitReturn_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        FinalSaveForReturn();
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Final Save for Return : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds7 != null) { ds7.Dispose(); }
    //    }
    //}
    //// end of code return qty

    ////code start for Replace qty


    //private void GetItemDetailByProductDispDeliveryChallanId_Replace()
    //{
    //    try
    //    {
    //        ds5 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //            new string[] { "flag", "ProductDispDeliveryChallanId", "Office_ID" },
    //              new string[] { "1", ViewState["PDDCId"].ToString(), objdb.Office_ID() }, "dataset");
    //        if (ds5.Tables[0].Rows.Count > 0)
    //        {
    //            GridView3.DataSource = ds5.Tables[0];
    //            GridView3.DataBind();
    //        }
    //        else
    //        {
    //            GridView3.DataSource = null;
    //            GridView3.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds5 != null) { ds5.Dispose(); }
    //    }
    //}

    //private void InsertReplace()
    //{

    //    DateTime date3 = DateTime.ParseExact(modalreturndelivarydateReplace.InnerHtml, "dd/MM/yyyy", culture);
    //    string delidate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //    DateTime placedat = DateTime.ParseExact(txtSalesReplaceDate.Text, "dd/MM/yyyy", culture);
    //    string replacedate = placedat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    //    DataTable dtInsertChild_Replace = new DataTable();
    //    DataRow drIC;
    //    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

    //    dtInsertChild_Replace.Columns.Add("ProductDispDeliveryChallanChildId", typeof(int));
    //    dtInsertChild_Replace.Columns.Add("ReplaceQty", typeof(int));
    //    dtInsertChild_Replace.Columns.Add("ReplaceDate", typeof(string));
    //    dtInsertChild_Replace.Columns.Add("ReplaceRemark", typeof(string));
    //    dtInsertChild_Replace.Columns.Add("ReplaceBy", typeof(int));
    //    dtInsertChild_Replace.Columns.Add("ReplaceByIP", typeof(string));
    //    drIC = dtInsertChild_Replace.NewRow();
    //    foreach (GridViewRow gridrow in GridView3.Rows)
    //    {
    //        CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");
    //        Label lblProductDispDeliveryChallanChildId = (Label)gridrow.FindControl("lblProductDispDeliveryChallanChildId");

    //        TextBox txtReplaceQty = (TextBox)gridrow.FindControl("txtReplaceQty");
    //        TextBox txtReplaceRemark = (TextBox)gridrow.FindControl("txtReplaceRemark");

    //        if (chkSelect.Checked == true && txtReplaceQty.Text != "")
    //        {

    //            drIC[0] = lblProductDispDeliveryChallanChildId.Text;
    //            drIC[1] = txtReplaceQty.Text;
    //            drIC[2] = replacedate;
    //            drIC[3] = txtReplaceRemark.Text;
    //            drIC[4] = objdb.createdBy();
    //            drIC[5] = IPAddress;

    //            dtInsertChild_Replace.Rows.Add(drIC.ItemArray);
    //        }

    //    }
    //    if (dtInsertChild_Replace.Rows.Count > 0)
    //    {
    //        ds2 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_RR",
    //                                     new string[] { "Flag" },
    //                                     new string[] { "2" }
    //                                     , "type_Trn_ProductDispDeliveryChallanChild_Replace", dtInsertChild_Replace, "TableSave");

    //        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {
    //            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            dtInsertChild_Replace.Dispose();

    //            lblModalMsgReplace.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //            if (ds2 != null) { ds2.Dispose(); }
    //            GetItemDetailByProductDispDeliveryChallanId_Replace();
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReplaceItemModal()", true);
    //        }
    //        else
    //        {
    //            string msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblModalMsgReplace.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + msg);
    //            if (ds2 != null) { ds2.Dispose(); }
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReplaceItemModal()", true);
    //        }
    //    }
    //    else
    //    {
    //        lblModalMsgReplace.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Please Enter at least one replace qty.");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReplaceItemModal()", true);
    //    }
    //}
    //private void FinalSaveForRplace()
    //{
    //    try
    //    {
    //        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //        ds7 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                        new string[] { "flag", "ProductDispDeliveryChallanId", "CreatedBy", "CreatedByIP" },
    //                        new string[] { "9", ViewState["PDDCId"].ToString(), objdb.createdBy(), IPAddress }, "TableSave");

    //        if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //        {
    //            string success = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //            GetDMDetails();
    //        }
    //        else
    //        {
    //            string error = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Final Save for Return :" + error);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Final Save for Return : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds7 != null) { ds7.Dispose(); }
    //    }
    //}
    //protected void btnReplaceSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Page.IsValid)
    //        {
    //            lblMsg.Text = string.Empty;
    //            lblModalMsgReplace.Text = string.Empty;
    //            string myStringfromdat = modalreturndelivarydateReplace.InnerHtml;
    //            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //            string myStringtodate = txtSalesReplaceDate.Text;
    //            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    //            if (fdate <= tdate)
    //            {
    //                lblModalMsgReplace.Text = string.Empty;
    //                InsertReplace();
    //            }
    //            else
    //            {

    //                lblModalMsgReplace.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Sales Replace Date must be greater than or equal to Date : " + myStringfromdat);
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myReplaceItemModal()", true);
    //                return;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalReturnMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "replace save : " + ex.Message.ToString());
    //    }

    //}

    //protected void lnkFinalSubmitReplace_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FinalSaveForRplace();
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Final Save for replace : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds7 != null) { ds7.Dispose(); }
    //    }
    //}

    //// end of replace qty


    //protected void chkedit_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        int checkcount = 0;
    //        foreach (GridViewRow gvRow in GridView4.Rows)
    //        {

    //            CheckBox chkedit = (CheckBox)gvRow.FindControl("chkedit");
    //            Label lblItem_id = (Label)gvRow.FindControl("lblItem_id");
    //            Label lblFiItemQtyByCarriageMode = (Label)gvRow.FindControl("lblFiItemQtyByCarriageMode");
    //            Label lblFiNotIssueQty = (Label)gvRow.FindControl("lblFiNotIssueQty");
    //            Label HlblSupplyTotalQty = (Label)gvRow.FindControl("lblSupplyTotalQty");
    //            TextBox HtxtSupplyTotalQty = (TextBox)gvRow.FindControl("txtSupplyTotalQty");
    //            LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
    //            LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
    //            LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
    //            RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
    //            RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
    //            int issuecaret = 0, issuebox = 0;
    //            if (chkedit.Checked == true)
    //            {
    //                DateTime odate = DateTime.ParseExact(modaldate.InnerHtml, "dd/MM/yyyy", culture);
    //                string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //                ds4 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                                new string[] { "flag", "Office_ID", "Item_id", "Delivery_Date", "ItemCat_id" },
    //                               new string[] { "11", objdb.Office_ID(), lblItem_id.Text, deliverydate, objdb.GetProductCatId() }, "dataset");
    //                if (ds4.Tables.Count > 1)
    //                {
    //                    if (ds4.Tables[1].Rows.Count > 0)
    //                    {
    //                        lblFiNotIssueQty.Text = ds4.Tables[1].Rows[0]["FiNotIssueQty"].ToString();
    //                        lblFiItemQtyByCarriageMode.Text = ds4.Tables[1].Rows[0]["FiItemQtyByCarriageMode"].ToString();
    //                    }
    //                    else
    //                    {
    //                        lblFiNotIssueQty.Text = "0";
    //                        lblFiItemQtyByCarriageMode.Text = "0";
    //                    }
    //                }

    //                HlblSupplyTotalQty.Visible = false;
    //                HtxtSupplyTotalQty.Visible = true;
    //                HlnkEdit.Visible = false;
    //                HlnkUpdate.Visible = false;
    //                HlnkReset.Visible = false;
    //                Hrfv.Enabled = false;
    //                Hrev1.Enabled = false;
    //                checkcount += 1;

    //            }
    //            else
    //            {
    //                HlblSupplyTotalQty.Visible = true;
    //                HtxtSupplyTotalQty.Visible = false;
    //                HlnkEdit.Visible = false;
    //                HlnkUpdate.Visible = false;
    //                HlnkReset.Visible = true;
    //                Hrfv.Enabled = true;
    //                Hrev1.Enabled = true;

    //            }


    //            lblcheckcount.Text = checkcount.ToString();
    //            if (checkcount > 0)
    //            {
    //                lnkupdate.Visible = true;
    //                lnkFinalSubmit.Visible = false;
    //            }
    //            else
    //            {
    //                lnkupdate.Visible = false;
    //                lnkFinalSubmit.Visible = true;
    //            }

    //        }
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    //        //txtSupplyTotalQty.Text = "";
    //        //txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
    //        //lblSupplyTotalQty.Visible = false;
    //        //lnkEdit.Visible = false;

    //        //lnkUpdate.Visible = true;
    //        //lnkReset.Visible = true;
    //        //rfv.Enabled = true;
    //        //rev1.Enabled = true;
    //        //txtSupplyTotalQty.Visible = true;

    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalReturnMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "select  : " + ex.Message.ToString());
    //    }
    //}
    //protected void lnkupdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {


    //        DateTime odate = DateTime.ParseExact(modaldate.InnerHtml, "dd/MM/yyyy", culture);
    //        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        lblMsg.Text = "";
    //        DataTable dtInsertChild = new DataTable();
    //        DataRow drIC;

    //        dtInsertChild.Columns.Add("ProductDispDeliveryChallanChildId", typeof(int));
    //        dtInsertChild.Columns.Add("Item_id", typeof(int));
    //        dtInsertChild.Columns.Add("SupplyQty", typeof(int));
    //        dtInsertChild.Columns.Add("Amount", typeof(decimal));
    //        dtInsertChild.Columns.Add("IssueCrate", typeof(int));
    //        dtInsertChild.Columns.Add("IssueBox", typeof(int));

    //        drIC = dtInsertChild.NewRow();
    //        foreach (GridViewRow row in GridView4.Rows)
    //        {

    //            CheckBox chkedit = (CheckBox)row.FindControl("chkedit");

    //            Label lblProductDispDeliveryChallanChildId = (Label)row.FindControl("lblProductDispDeliveryChallanChildId");
    //            Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
    //            TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
    //            Label lblissuecaret = (Label)row.FindControl("lblissuecaret");
    //            Label lblissueBox = (Label)row.FindControl("lblissueBox");
    //            Label lblCarriageModeID = (Label)row.FindControl("lblCarriageModeID");
    //            Label lblFiItemQtyByCarriageMode = (Label)row.FindControl("lblFiItemQtyByCarriageMode");
    //            Label lblFiNotIssueQty = (Label)row.FindControl("lblFiNotIssueQty");
    //            Label lblItem_id = (Label)row.FindControl("lblItem_id");
    //            HiddenField HFTotalAmt = (HiddenField)row.FindControl("HFTotalAmt");
    //            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
    //            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
    //            LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
    //            if (chkedit.Checked == true)
    //            {

    //                if (int.Parse(txtSupplyTotalQty.Text) > 0 && int.Parse(lblFiItemQtyByCarriageMode.Text) > 0)
    //                {
    //                    if (int.Parse(txtSupplyTotalQty.Text) % int.Parse(lblFiItemQtyByCarriageMode.Text) <= int.Parse(lblFiNotIssueQty.Text))
    //                    {
    //                        if (lblCarriageModeID.Text == "1")
    //                        {
    //                            lblissuecaret.Text = (int.Parse(txtSupplyTotalQty.Text) / int.Parse(lblFiItemQtyByCarriageMode.Text)).ToString();
    //                            lblissueBox.Text = "0";
    //                        }
    //                        else if (lblCarriageModeID.Text == "2")
    //                        {
    //                            lblissueBox.Text = (int.Parse(txtSupplyTotalQty.Text) / int.Parse(lblFiItemQtyByCarriageMode.Text)).ToString();
    //                            lblissuecaret.Text = "0";
    //                        }
    //                        else
    //                        {
    //                            lblissueBox.Text = "0";
    //                            lblissuecaret.Text = "0";

    //                        }
    //                    }
    //                    else
    //                    {
    //                        if (lblCarriageModeID.Text == "1")
    //                        {
    //                            lblissuecaret.Text = (int.Parse(txtSupplyTotalQty.Text) / int.Parse(lblFiItemQtyByCarriageMode.Text) + 1).ToString();
    //                            lblissueBox.Text = "0";
    //                        }
    //                        else if (lblCarriageModeID.Text == "2")
    //                        {
    //                            lblissueBox.Text = (int.Parse(txtSupplyTotalQty.Text) / int.Parse(lblFiItemQtyByCarriageMode.Text) + 1).ToString();
    //                            lblissuecaret.Text = "0";
    //                        }
    //                        else
    //                        {
    //                            lblissueBox.Text = "0";
    //                            lblissuecaret.Text = "0";
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    lblissueBox.Text = "0";
    //                    lblissuecaret.Text = "0";
    //                }
    //                drIC[0] = lblProductDispDeliveryChallanChildId.Text;
    //                drIC[1] = lblItem_id.Text;
    //                drIC[2] = txtSupplyTotalQty.Text;
    //                drIC[3] = HFTotalAmt.Value;
    //                drIC[4] = lblissuecaret.Text;
    //                drIC[5] = lblissueBox.Text;

    //                dtInsertChild.Rows.Add(drIC.ItemArray);



    //            }
    //        }
    //        ds4 = objdb.ByProcedure("USP_Trn_ProductDispDeliveryChallan_By_ChallanID_List",
    //                            new string[] { "flag", "UpdatedBy", "Office_ID", "ItemCat_id", "Delivery_Date", "ProductDispDeliveryChallanId" },
    //                           new string[] { "10", objdb.createdBy(), objdb.Office_ID(), objdb.GetProductCatId(), deliverydate, ViewState["rowidedit"].ToString() }, "type_ProductDispDeliveryChallanChildUpdate", dtInsertChild, "TableSave");

    //        if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "SUCCESS")
    //        {
    //            //gheestock updation
    //            if (ds4.Tables.Count > 2)
    //            {
    //                if (ds4.Tables[1].Rows.Count > 0 && ds4.Tables[2].Rows.Count > 0)
    //                {
    //                    //var httpWebRequestP = (HttpWebRequest)WebRequest.Create("http://45.114.143.215:8202//api/data/PostGheeStock");
    //                    var httpWebRequestP = (HttpWebRequest)WebRequest.Create("http://localhost:54453/api/data/PostGheeStock");
    //                    httpWebRequestP.ContentType = "application/json; charset=utf-8";
    //                    httpWebRequestP.Method = "POST";

    //                    string DistributorID = "", GatePass_No = "", Vehicle_No = "", GrandTotal = "", DMID = "", DMNO = "";

    //                    //DistributorID = ds4.Tables[2].Rows[0]["DistributorID"].ToString();
    //                    GatePass_No = ds4.Tables[2].Rows[0]["GatePass_No"].ToString();
    //                    Vehicle_No = ds4.Tables[2].Rows[0]["Vehicle_No"].ToString();
    //                    // GrandTotal = ds4.Tables[2].Rows[0]["GrandTotal"].ToString();
    //                    //DMNO = (Convert.ToDateTime(deliverydate.ToString(), cult).ToString("yyyy/MM/dd")).ToString();



    //                    string ItemID = "", ItemQty = "", NetTotalValue = "", ItemRate = "";
    //                    for (int j = 0; j < ds4.Tables[1].Rows.Count; j++)
    //                    {
    //                        ItemID = ds4.Tables[1].Rows[j]["item_id"].ToString();
    //                        ItemQty = ds4.Tables[1].Rows[j]["SupplyQty"].ToString();
    //                        NetTotalValue = ds4.Tables[1].Rows[j]["Amount"].ToString();
    //                        //ItemRate = ds4.Tables[1].Rows[j]["ItemRate"].ToString();



    //                        // var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://45.114.143.215:8202//api/data/PostGheeStock");
    //                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:54453/api/data/PostGheeStock");
    //                        httpWebRequest.ContentType = "application/json; charset=utf-8";
    //                        httpWebRequest.Method = "POST";

    //                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    //                        {
    //                            string json = "{\"Vehicle_No\":\"" + Vehicle_No + "\"," +
    //                                          "\"GatePass_No\":\"" + GatePass_No + "\"," +
    //                                          "\"dataOperation\":\"UPDATE_Child\"," +
    //                                          "\"ItemID\":\"" + ItemID + "\"," +
    //                                          "\"ItemQty\":\"" + ItemQty + "\"," +
    //                                          "\"NetTotalValue\":\"" + NetTotalValue + "\"}";


    //                            streamWriter.Write(json);
    //                            Debug.Write(json);
    //                            streamWriter.Write(json);
    //                            streamWriter.Flush();
    //                            streamWriter.Close();
    //                        }

    //                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //                        {
    //                            var result = streamReader.ReadToEnd();
    //                        }






    //                    }

    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblModalReturnMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "replace save : " + ex.Message.ToString());
    //    }
    //}

    public IFormatProvider cult { get; set; }
}