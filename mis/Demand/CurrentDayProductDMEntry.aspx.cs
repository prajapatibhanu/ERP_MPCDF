using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.Net;

public partial class mis_Demand_CurrentDayProductDMEntry : System.Web.UI.Page
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
                GetShift();
                GetLocation();
                //  GetRoute();
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
    private void GetRoute()
    {
        try
        {
            if (ddlLocation.SelectedValue != "0")
            {
                // ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     // new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                     // new string[] { "7", objdb.Office_ID(), objdb.GetProductCatId(), ddlLocation.SelectedValue }, "dataset");
					 
					 ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                       new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                       new string[] { "15", objdb.Office_ID(), objdb.GetProductCatId(), ddlLocation.SelectedValue }, "dataset");
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
                GetDatatableHeaderDesign();
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
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 15 ", ex.Message.ToString());
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
                    new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "CreatedBy", "Office_ID", "RouteId", "OrganizationId" },
                      new string[] { "3", orderdate.ToString(), ddlShift.SelectedValue, objdb.GetProductCatId(), ddlRetailer.SelectedValue, objdb.Office_ID(), ddlRoute.SelectedValue, ddlRetailer.SelectedValue }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {

                lblCartMsg.Text = "<i class='fa fa-cart-plus fa-2x'></i> Cart details for Product";
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
            //DateTime odate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            //string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            //ds1 = objdb.ByProcedure("USP_Trn_Paymentandsecuritycompare",
            //    new string[] { "Flag", "RouteId", "Office_ID", "DistributorId", "Delivary_Date" },
            //    new string[] { "1", ddlRoute.SelectedValue, objdb.Office_ID(), ddlDitributor.SelectedValue, deliverydate.ToString() }, "dataset");
            //if (ds1.Tables.Count > 0)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        decimal Paiamount = decimal.Parse(ds1.Tables[0].Rows[0]["Opening"].ToString()) + decimal.Parse(ViewState["TAmount"].ToString());
            //        if (Paiamount <= decimal.Parse(ds1.Tables[0].Rows[0]["SecurityDeposit"].ToString()))
            //        {
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

                            ds5 = objdb.ByProcedure("USP_Trn_ProductDM",
                                      new string[] { "flag", "BoothId", "ItemCat_id", "Demand_Date", "Shift_id", "Demand_Status"
                                    , "UserTypeId", "Office_ID", "CreatedBy", "CreatedByIP", "PlatformType"
                                    , "Delivary_Date", "DelivaryShift_id", "RetailerTypeID", "RouteId", "DistributorId","ProductDMStatus" },
                                     new string[] { "1", ddlRetailer.SelectedValue, objdb.GetProductCatId(), odat.ToString(), 
                             ddlShift.SelectedValue, "1", objdb.UserTypeID(), objdb.Office_ID(), 
                             objdb.createdBy(), IPAddress, "1", odat.ToString(), ddlShift.SelectedValue, ViewState["RetailerTypeID"].ToString(), 
                             ddlRoute.SelectedValue,"",ddlDMType.SelectedValue },
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
                            }
                            else
                            {
                                string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                            pnlClear.Visible = true;
                            pnlProduct.Visible = false;
                            pnlSubmit.Visible = false;
                            if (GridView3.Rows.Count > 0)
                            {
                                GetDemandDetailsOfParlour();
                            }

                        }
            //        }
            //        else
            //        {
            //            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Warning :" + "Security Amount Should be greater than or equal to Total Amount to pay");
            //        }
            //    }
            //}
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
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void GetDemandDetailsOfParlour()
    {
        try
        {
            DateTime date5 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            demanddate = date5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "Office_ID", "AreaId" },
                       new string[] { "12", demanddate.ToString(), ddlShift.SelectedValue, objdb.GetProductCatId(), objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                lblSearchMsg.Text = "";
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
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error View Demand: " + ex.Message.ToString());
        }

        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
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
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            if (txtOrderDate.Text != "" && ddlRetailer.SelectedValue != "0" && ddlRoute.SelectedValue != "0")
            {
                GetItem();
                GetRetailerTypeID();
                lblMsg.Text = string.Empty;
                GetDatatableHeaderDesign();

            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetDatatableHeaderDesign();
            InsertOrder();

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

        lblMsg.Text = string.Empty;
        pnlSubmit.Visible = false;
        pnlProduct.Visible = false;
        pnlClear.Visible = false;
        GetDatatableHeaderDesign();
    }
    #endregion=============end of button click funciton==================


    #region=====================code for search parlour wise==========================
    private void GetItemDetailByDemandID()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["rowid"].ToString(), ViewState["rowitemcatid"].ToString(), objdb.Office_ID() }, "dataset");
            GridView4.DataSource = ds.Tables[0];
            GridView4.DataBind();
        }
        catch (Exception ex)
        {
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
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
            lblSearchMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
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
                    pnlClear.Visible = false;
                    lblMsg.Text = string.Empty;
                    lblSearchMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblPAI = (Label)row.FindControl("lblPAI");
                    Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                    Label lbltmpDStatus = (Label)row.FindControl("lbltmpDStatus");
                    ViewState["rowid"] = e.CommandArgument.ToString();
                    ViewState["rowitemcatid"] = lblItemCatid.Text;
                    GetItemDetailByDemandID();

                    GetDatatableHeaderDesign();

                    modalBoothName.InnerHtml = lblPAI.Text;
                    modaldate.InnerHtml = lblDemandDate.Text;
                    modelCategory.InnerHtml = objdb.GetProductCategoryName();
                    modelorderstatus.InnerHtml = lbltmpDStatus.Text;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
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
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
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
                    GetDatatableHeaderDesign();

                }

            }
            if (e.CommandName == "RecordUpdate")
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
                        totalqty = Convert.ToInt32(txtItemQty.Text) + Convert.ToInt32(lblAdvCard.Text);
                    }

                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "5", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour) " }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetItemDetailByDemandID();
                        // GridView4.SelectedIndex = -1;
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

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
                    lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion=======================================================================

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDatatableHeaderDesign();
    }
    //private void GetRoute()
    //{
    //    try
    //    {
    //        if (objdb.GetProductCatId() != "0")
    //        {
    //            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //                 new string[] { "flag", "Office_ID", "ItemCat_id" },
    //                 new string[] { "6", objdb.Office_ID(), objdb.GetProductCatId() }, "dataset");
    //            ddlRoute.DataTextField = "RNameOrNo";
    //            ddlRoute.DataValueField = "RouteId";
    //            ddlRoute.DataBind();
    //            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
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
            GetDatatableHeaderDesign();
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