using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class mis_Demand_ProductOrderApprovalList : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds33, dsc, dsadd = new DataSet();

    string orderdate = "", demanddate = "";
    Int32 totalqty = 0;
    Int32 demandcount = 0;
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

                // GetRouteApproved();
                GetCategory();
                GetShift();
                GetItemByCategory();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
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
    private void GetRoute()
    {
        try
        {

            ddlRoute.Items.Clear();
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                     new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
    }
    protected void GetItemByCategory()
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                     new string[] { "flag", "Office_ID", "ItemCat_id" },
                       new string[] { "2", objdb.Office_ID(), objdb.GetProductCatId() }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataSource = ds2.Tables[0];
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemName.Items.Insert(0, new ListItem("No Record Found", "0"));
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
    //private void GetRouteApproved()
    //{
    //    try
    //    {

    //            ddlRouteApproved.DataSource = objdb.ByProcedure("USP_Mst_Route",
    //                 new string[] { "flag", "Office_ID" },
    //                 new string[] { "1", objdb.Office_ID() }, "dataset");
    //            ddlRouteApproved.DataTextField = "RNameOrNo";
    //            ddlRouteApproved.DataValueField = "RouteId";
    //            ddlRouteApproved.DataBind();
    //            ddlRouteApproved.Items.Insert(0, new ListItem("Select", "0"));


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //private void GetDisOrSSByRouteID()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //                 new string[] { "flag", "Office_ID", "RouteId" },
    //                   new string[] { "5" ,objdb.Office_ID(),ddlRouteApproved.SelectedValue}, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddlDistOrSSName.DataTextField = "DTName";
    //            ddlDistOrSSName.DataValueField = "DistributorId";
    //            ddlDistOrSSName.DataSource = ds.Tables[0];
    //            ddlDistOrSSName.DataBind();
    //            ddlDistOrSSName.Items.Insert(0, new ListItem("Select", "0"));
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error S ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds != null) { ds.Dispose(); }

    //    }
    //}
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error S ", ex.Message.ToString());
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
            dsc = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                      new string[] { "flag" },
                        new string[] { "1" }, "dataset");

            if (dsc.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = dsc.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.SelectedValue = objdb.GetProductCatId();
                ddlItemCategory.Enabled = false;
            }
            else
            {
                ddlItemCategory.Items.Insert(0, new ListItem("No Record Found", "0"));
                ddlItemCategory.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (dsc != null) { dsc.Dispose(); }
        }
    }
    protected void GetOrderDetails()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

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

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag", "Office_ID", "AreaId", "RouteId", "Demand_Date", "Shift_id", "ItemCat_id", "MultiProductDMStatus" },
                       new string[] { "13", objdb.Office_ID(), ddlLocation.SelectedValue, ddlRoute.SelectedValue, orderdate, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, multidmtype }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                lblMsg.Text = string.Empty;
                btnApp.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
                // pnlapproval.Visible = true;
            }
            else
            {
                btnApp.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
                // pnlapproval.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Data ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    //protected void ddlShift_Init(object sender, EventArgs e)
    //{
    //    GetShift();
    //}
    //protected void ddlRoute_Init(object sender, EventArgs e)
    //{
    //    GetRoute();
    //}
    //protected void ddlItemCategory_Init(object sender, EventArgs e)
    //{
    //    GetCategory();
    //}
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            GetRoute();
            // GetDatatableHeaderDesign();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetOrderDetails();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlRoute.SelectedIndex = 0;
        //  ddlItemCategory.SelectedIndex = 0;
        // ddlShift.SelectedIndex = 0;
        pnlsearch.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlLocation.SelectedIndex = 0;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lnkupdate.Visible = false;
        if (e.CommandName == "ItemOrdered")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblModalMsg1.Text = string.Empty;

                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                Label lblShiftName = (Label)row.FindControl("lblShiftName");
                Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                ViewState["rowid"] = e.CommandArgument.ToString();
                ViewState["rowitemcatid"] = lblItemCatid.Text;
                GetItemDetailByDemandID();

                modalBoothName.InnerHtml = lblBandOName.Text;
                modaldate.InnerHtml = lblDemandDate.Text;
                modalshift.InnerHtml = lblShiftName.Text;
                modalorderStatus.InnerHtml = lblDemandStatus.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                GetDatatableHeaderDesign();
                lblMilkOrProductDemandId.Text = e.CommandArgument.ToString();

            }
        }


        if (e.CommandName == "AddOrdered")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;

                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                ViewState["rowAddid"] = e.CommandArgument.ToString();

                modalpartyname.InnerHtml = lblBandOName.Text;
                partymodaldate.InnerHtml = lblDemandDate.Text;

                partymodalstatus.InnerHtml = lblDemandStatus.Text;
                txtTotalQty.Text = string.Empty;
                ddlItemName.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddItemDetailsModal()", true);
                GetDatatableHeaderDesign();

            }
        }
    }
    private void GetItemDetailByDemandID()
    {
        try
        {
            btnReject.Visible = true;
            lnkupdate.Visible = false;
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["rowid"].ToString(), ViewState["rowitemcatid"].ToString(), objdb.Office_ID() }, "dataset");
            GridView4.DataSource = ds1.Tables[0];
            GridView4.DataBind();
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
                    lblModalMsg.Text = string.Empty;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    Label lblRemarkAtOrderApproval = (Label)row.FindControl("lblRemarkAtOrderApproval");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    TextBox txtRemarkAtOrderApproval = (TextBox)row.FindControl("txtRemarkAtOrderApproval");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblItemQty = (Label)gvRow.FindControl("lblItemQty");
                        TextBox HtxtItemQty = (TextBox)gvRow.FindControl("txtItemQty");
                        TextBox HtxtRemarkAtOrderApproval = (TextBox)gvRow.FindControl("txtRemarkAtOrderApproval");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                        HlblItemQty.Visible = true;
                        HtxtItemQty.Visible = false;
                        HtxtRemarkAtOrderApproval.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrev1.Enabled = false;

                    }
                    txtItemQty.Text = "";
                    txtItemQty.Text = lblItemQty.Text;
                    lblItemQty.Visible = false;
                    lblRemarkAtOrderApproval.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    txtItemQty.Visible = true;
                    txtRemarkAtOrderApproval.Visible = true;
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
                    Label lblRemarkAtOrderApproval = (Label)row.FindControl("lblRemarkAtOrderApproval");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    TextBox txtRemarkAtOrderApproval = (TextBox)row.FindControl("txtRemarkAtOrderApproval");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;

                    lblItemQty.Visible = true;
                    lblAdvCard.Visible = true;
                    lblRemarkAtOrderApproval.Visible = true;
                    lnkEdit.Visible = true;
                    lblModalMsg.Text = string.Empty;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    txtItemQty.Visible = false;
                    txtRemarkAtOrderApproval.Visible = false;
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
                        TextBox txtRemarkAtOrderApproval = (TextBox)row.FindControl("txtRemarkAtOrderApproval");
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
                            totalqty = Convert.ToInt32(txtItemQty.Text); //+ Convert.ToInt32(lblAdvCard.Text);
                        }

                        if (txtItemQty.Text == "0" && txtRemarkAtOrderApproval.Text == "")
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                        else
                        {
                            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                                     new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RemarkAtOrderApproval" },
                                                     new string[] { "20", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour)", txtRemarkAtOrderApproval.Text.Trim() }, "TableSave");

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
                    e.Row.Cells[7].Visible = false;
                    GridView4.HeaderRow.Cells[7].Visible = false;
                    //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else if (lblDemandStatus.Text == "3")
                {
                    e.Row.Cells[7].Visible = false;
                    GridView4.HeaderRow.Cells[7].Visible = false;
                    //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    GridView4.HeaderRow.Cells[7].Visible = true;
                    e.Row.Cells[7].Visible = true;
                    //   btnApproved.Visible = true;
                    btnReject.Visible = true;
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
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 20 : " + ex.Message.ToString());
        }
    }
    private void ApprovedOrRejectedOrderd(string status, string routid, string distid)
    {
        ds33 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                           new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RouteId", "DistributorId" },
                           new string[] { "14", ViewState["rowid"].ToString(), status.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully.", routid, distid }, "TableSave");

        if (status == "3")
        {
            foreach (GridViewRow row in GridView4.Rows)
            {
                string did = GridView4.DataKeys[row.RowIndex].Value.ToString();
                objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                           new string[] { "flag", "MilkOrProductDemandChildId" },
                           new string[] { "1", did }, "TableSave");

            }
        }
        if (ds33.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
        {
            string success = ds33.Tables[0].Rows[0]["ErrorMsg"].ToString();
            GetOrderDetails();
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

        }
        else
        {
            string error = ds33.Tables[0].Rows[0]["ErrorMsg"].ToString();
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ApprovedOrRejectedOrderd("2", "", "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 21 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds33 != null) { ds33.Dispose(); }
        }
    }
    //protected void btnApproved_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if(Page.IsValid)
    //        {
    //            ApprovedOrRejectedOrderd("3",ddlModalRoute.SelectedValue,ddlModalDistOrSS.SelectedValue);
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 22 : " + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds33 != null) { ds33.Dispose(); }
    //    }
    //}
    protected void btnApp_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                foreach (GridViewRow gridrow in GridView1.Rows)
                {
                    //  string mpdid = GridView1.DataKeys[gridrow.RowIndex].Value.ToString();
                    CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");
                    Label lblItemCatid = (Label)gridrow.FindControl("lblItemCatid");
                    Label lblMilkOrProductDemandId = (Label)gridrow.FindControl("lblMilkOrProductDemandId");

                    ViewState["rowid"] = lblMilkOrProductDemandId.Text;
                    ViewState["rowitemcatid"] = lblItemCatid.Text;
                    if (chkSelect.Checked == true)
                    {
                        //ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                        //               new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RouteId", "DistributorId" },
                        //               new string[] { "14", lblMilkOrProductDemandId.Text, "3", objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully.", ddlRouteApproved.SelectedValue, ddlDistOrSSName.SelectedValue }, "TableSave");
                        ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                      new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                      new string[] { "14", lblMilkOrProductDemandId.Text, "3", objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully." }, "TableSave");

                        GetItemDetailByDemandID();

                        foreach (GridViewRow row in GridView4.Rows)
                        {
                            Label lblMilkOrProductDemandChildId = (Label)row.FindControl("lblMilkOrProductDemandChildId");
                            //string did = GridView4.DataKeys[row.RowIndex].Value.ToString();
                            objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                                       new string[] { "flag", "MilkOrProductDemandChildId" },
                                       new string[] { "1", lblMilkOrProductDemandChildId.Text }, "TableSave");

                        }
                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetOrderDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 23:" + error);
                        }
                    }

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

    //protected void ddlRouteApproved_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //GetDisOrSSByRouteID();
    //    GetDatatableHeaderDesign();
    //}
    //protected void ddlModalRoute_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetDisOrSSByModalRouteID();
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    //}
    private void AddItem()
    {
        try
        {
            dsadd = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                           new string[] { "flag", "MilkOrProductDemandId", "Item_id", "TotalQty" },
                          new string[] { "23", ViewState["rowAddid"].ToString(), ddlItemName.SelectedValue, txtTotalQty.Text.Trim() }, "dataset");
            if (dsadd.Tables[0].Rows.Count > 0)
            {
                if (dsadd.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string msg = dsadd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "" + msg.ToString());
                    txtTotalQty.Text = string.Empty;
                    ddlItemName.SelectedIndex = 0;
                }
                else if (dsadd.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                {
                    string msg = dsadd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg1.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "" + msg.ToString());
                }
                else
                {
                    string msg = dsadd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Add Item :" + msg.ToString());

                }
            }
        }
        catch (Exception ex)
        {

            lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Add Item :" + ex.ToString());
        }
        finally
        {
            if (dsadd != null) { dsadd.Dispose(); }
        }
    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ViewState["rowAddid"].ToString() != "")
            {
                AddItem();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddItemDetailsModal()", true);
            }

        }
    }

    protected void chkedit_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            int checkcount = 0;


            lblMsg.Text = string.Empty;
            lblModalMsg.Text = string.Empty;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

            foreach (GridViewRow row in GridView4.Rows)
            {
                CheckBox chkedit = (CheckBox)row.FindControl("chkedit");


                Label lblItemQty = (Label)row.FindControl("lblItemQty");
                Label lblRemarkAtOrderApproval = (Label)row.FindControl("lblRemarkAtOrderApproval");
                TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                TextBox txtRemarkAtOrderApproval = (TextBox)row.FindControl("txtRemarkAtOrderApproval");

                RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;


                Label HlblItemQty = (Label)row.FindControl("lblItemQty");
                TextBox HtxtItemQty = (TextBox)row.FindControl("txtItemQty");
                TextBox HtxtRemarkAtOrderApproval = (TextBox)row.FindControl("txtRemarkAtOrderApproval");

                RequiredFieldValidator Hrfv = row.FindControl("rfv1") as RequiredFieldValidator;
                RegularExpressionValidator Hrev1 = row.FindControl("rev1") as RegularExpressionValidator;
                HlblItemQty.Visible = true;
                HtxtItemQty.Visible = false;
                HtxtRemarkAtOrderApproval.Visible = false;

                Hrfv.Enabled = false;
                Hrev1.Enabled = false;

               
                if (chkedit.Checked == true)
                {
                    txtItemQty.Text = "";
                    txtItemQty.Text = lblItemQty.Text;
                    lblItemQty.Visible = false;
                    lblRemarkAtOrderApproval.Visible = false;

                    rfv.Enabled = true;
                    rev1.Enabled = true;
                    txtItemQty.Visible = true;
                    txtRemarkAtOrderApproval.Visible = true;
                    checkcount += 1;
                }
                else
                {
                    lblItemQty.Visible = true;
                    lblRemarkAtOrderApproval.Visible = true;
                    txtRemarkAtOrderApproval.Visible = false;
                    txtItemQty.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                }

            }
            lblcheckcount.Text = checkcount.ToString();
            if (checkcount > 0)
            {
                lnkupdate.Visible = true;
                btnReject.Visible = false;

            }
            else
            {
                lnkupdate.Visible = false;
                btnReject.Visible = true;

            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "select  : " + ex.Message.ToString());
        }
    }
    protected void lnkupdate_Click(object sender, EventArgs e)
    {
        try
        {


            if (Page.IsValid)
            {
                int checkcount = 0;
                DateTime odate = DateTime.ParseExact(modaldate.InnerHtml, "dd/MM/yyyy", culture);
                string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                lblMsg.Text = "";
                DataTable dtInsertChild = new DataTable();
                DataRow drIC;

                dtInsertChild.Columns.Add("MilkOrProductDemandChildId", typeof(int));
                //dtInsertChild.Columns.Add("TotalQty", typeof(int));
                dtInsertChild.Columns.Add("ItemQty", typeof(int));
                dtInsertChild.Columns.Add("TotalQty", typeof(int));
                dtInsertChild.Columns.Add("RemarkAtOrderApproval", typeof(string));


                drIC = dtInsertChild.NewRow();
                foreach (GridViewRow row in GridView4.Rows)
                {

                    CheckBox chkedit = (CheckBox)row.FindControl("chkedit");
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    lblMsg.Text = string.Empty;
                    Label lblItemQty = (Label)row.FindControl("lblItemQty");
                    Label lblMilkOrProductDemandChildId = (Label)row.FindControl("lblMilkOrProductDemandChildId");
                    Label lblAdvCard = (Label)row.FindControl("lblAdvCard");
                    Label lblRemarkAtOrderApproval = (Label)row.FindControl("lblRemarkAtOrderApproval");
                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");
                    TextBox txtRemarkAtOrderApproval = (TextBox)row.FindControl("txtRemarkAtOrderApproval");
                    if (chkedit.Checked == true)
                    {
                        int totalqty = 0;
                        if (txtItemQty.Text == "0")
                        {
                            totalqty = 0;
                        }
                        else
                        {
                            totalqty = Convert.ToInt32(txtItemQty.Text); //+ Convert.ToInt32(lblAdvCard.Text);
                        }
                        if (txtItemQty.Text == "0" && txtRemarkAtOrderApproval.Text == "")
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                       
                        drIC[0] = lblMilkOrProductDemandChildId.Text;
                        // drIC[1] = txtSupplyTotalQty.Text;
                        drIC[1] = txtItemQty.Text;
                        drIC[2] = totalqty;
                        drIC[3] = txtRemarkAtOrderApproval.Text;


                        dtInsertChild.Rows.Add(drIC.ItemArray);
                        checkcount += 1;
                    }
                }
                //else
                //{
                if (checkcount > 0)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                   // string tmpleakageqty = "";
                    ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                                                    new string[] { "flag", "MilkOrProductDemandId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                                    new string[] { "26", lblMilkOrProductDemandId.Text, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify MilkorProduct Page(Parlour)" }, "type_Trn_MilkOrProductDemandChildUpdate_Product", dtInsertChild, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetItemDetailByDemandID();
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "replace save : " + ex.Message.ToString());
        }
    }
}