using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Demand_MilkOrProductOrderApprovalList : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 ,ds2,ds33,ds5,dsleak= new DataSet();

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
                txtOrderDate.Attributes.Add("readonly", "false");
                GetLocation();
                GetRoute();
               // GetRouteApproved();
               // GetCategory();
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
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
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
            ds5 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds5.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
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
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataBind();
            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

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
    protected void GetOrderDetails()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag", "Office_ID", "AreaId", "RouteId", "Demand_Date", "Shift_id", "ItemCat_id", "Demand_Status" },
                       new string[] { "13", objdb.Office_ID(),ddlLocation.SelectedValue, ddlRoute.SelectedValue, orderdate, ddlShift.SelectedValue, objdb.GetMilkCatId(),ddlStatus.SelectedValue }, "dataset");

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
        GetRoute();
        GetDatatableHeaderDesign();
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
       // txtOrderDate.Text = string.Empty;
      //  ddlItemCategory.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        pnlsearch.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlLocation.SelectedIndex = 0;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ItemOrdered")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;

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

            }
        }
        else if (e.CommandName == "LekageEntry")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblMsgLeakage.Text = string.Empty;

                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                Label lblShiftName = (Label)row.FindControl("lblShiftName");
                Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                ViewState["leakrowid"] = e.CommandArgument.ToString();
                ViewState["leakrowitemcatid"] = lblItemCatid.Text;
                GetLeakItemDetailByDemandID();

                modalBoothNameleak.InnerHtml = lblBandOName.Text;
                modaldateleak.InnerHtml = lblDemandDate.Text;
                modalshiftleak.InnerHtml = lblShiftName.Text;
                modalorderStatusleak.InnerHtml = lblDemandStatus.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myLeakageItemDetailsModal()", true);
                GetDatatableHeaderDesign();

            }
        }
    }
    private void GetItemDetailByDemandID()
    {
        try
        {
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
    private void GetLeakItemDetailByDemandID()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["leakrowid"].ToString(), ViewState["leakrowitemcatid"].ToString(), objdb.Office_ID() }, "dataset");
            GridView2.DataSource = ds1.Tables[0];
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            lblMsgLeakage.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
                        if(txtItemQty.Text=="0")
                        {
                            totalqty = 0;
                        }
                        else
                        {
                            totalqty = Convert.ToInt32(txtItemQty.Text); //+ Convert.ToInt32(lblAdvCard.Text);
                        }

                        if (txtItemQty.Text == "0" && txtRemarkAtOrderApproval.Text=="")
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
                ds1 = objdb.ByProcedure("GridView4",
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
                Label lblMilkCurDMCrateIsueStatus = e.Row.FindControl("lblMilkCurDMCrateIsueStatus") as Label;
                Label lblProductDMStatus = e.Row.FindControl("lblProductDMStatus") as Label;

                if (objdb.Office_ID() == "5")
                {
                    GridView4.HeaderRow.Cells[7].Visible = true;
                    e.Row.Cells[7].Visible = true;
                }
                else
                {
                    GridView4.HeaderRow.Cells[7].Visible = false;
                    e.Row.Cells[7].Visible = false;
                }
                if (lblDemandStatus.Text == "2")
                {
                    e.Row.Cells[8].Visible = false;
                    GridView4.HeaderRow.Cells[8].Visible = false;
                  //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else if (lblDemandStatus.Text == "3" && lblProductDMStatus.Text!="1")
                {
                    e.Row.Cells[8].Visible = false;
                    GridView4.HeaderRow.Cells[8].Visible = false;
                  //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else if (lblDemandStatus.Text == "3" && lblMilkCurDMCrateIsueStatus.Text == "" && lblProductDMStatus.Text == "1")
                {
                    e.Row.Cells[8].Visible = true;
                    GridView4.HeaderRow.Cells[8].Visible = true;
                    //  btnApproved.Visible = false;
                     btnReject.Visible = true;
                }
                else if (lblDemandStatus.Text == "3" && lblMilkCurDMCrateIsueStatus.Text != "" && lblProductDMStatus.Text == "1")
                {
                    e.Row.Cells[8].Visible = false;
                    GridView4.HeaderRow.Cells[8].Visible = false;
                    //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    GridView4.HeaderRow.Cells[8].Visible = true;
                    e.Row.Cells[8].Visible = true;
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
    private void ApprovedOrRejectedOrderd(string status,string routid,string distid)
    {
        ds33 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                           new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RouteId", "DistributorId" },
                           new string[] { "14", ViewState["rowid"].ToString(), status.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully.", routid,distid }, "TableSave");

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
            ApprovedOrRejectedOrderd("2","","");
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
                DataTable dtInsertChild = new DataTable();
                DataRow drIC;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                dtInsertChild.Columns.Add("MilkOrProductDemandId", typeof(int));
                dtInsertChild.Columns.Add("Demand_UpdatedBy", typeof(int));
                dtInsertChild.Columns.Add("Demand_UpdatedByIP", typeof(string));
                drIC = dtInsertChild.NewRow();
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

                        drIC[0] = lblMilkOrProductDemandId.Text;
                        drIC[1] = objdb.createdBy();
                        drIC[2] = IPAddress;
                       

                        dtInsertChild.Rows.Add(drIC.ItemArray);
                        //ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                        //               new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RouteId", "DistributorId" },
                        //               new string[] { "14", lblMilkOrProductDemandId.Text, "3", objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully.", ddlRouteApproved.SelectedValue, ddlDistOrSSName.SelectedValue }, "TableSave");
                        //ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                        //              new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                        //              new string[] { "14", lblMilkOrProductDemandId.Text, "3", objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully." }, "TableSave");

                        //GetItemDetailByDemandID();

                        //foreach (GridViewRow row in GridView4.Rows)
                        //{
                        //    Label lblMilkOrProductDemandChildId = (Label)row.FindControl("lblMilkOrProductDemandChildId");
                        //    //string did = GridView4.DataKeys[row.RowIndex].Value.ToString();
                        //    objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                        //               new string[] { "flag", "MilkOrProductDemandChildId" },
                        //               new string[] { "1", lblMilkOrProductDemandChildId.Text }, "TableSave");

                        //}
                        //if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        //{
                        //    string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        //    GetOrderDetails();
                        //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                           
                        //}
                        //else
                        //{
                        //    string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 23:" + error);
                        //}
                    }

                }
                if (dtInsertChild.Rows.Count > 0)
                {
                    ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand_UpdateStatusOrChallan",
                                                 new string[] { "flag" },
                                                 new string[] { "1" }, "type_Trn_MilkOrProductDemand_DemandStatus", dtInsertChild, "TableSave");

                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        dtInsertChild.Dispose();
                        GetOrderDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string msg = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                       lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
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

    protected void btnleakSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                 DataTable dtInsertChild = new DataTable();
                DataRow drIC;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                dtInsertChild.Columns.Add("MilkOrProductDemandChildId", typeof(int));
                dtInsertChild.Columns.Add("LeakageQty", typeof(string));
                dtInsertChild.Columns.Add("LeakageUpdatedBy", typeof(int));
                dtInsertChild.Columns.Add("LeakageUpdatedByIP", typeof(string));
                drIC = dtInsertChild.NewRow();
                foreach (GridViewRow grl in GridView2.Rows)
                {
                    CheckBox chkSelectLeak = (CheckBox)grl.FindControl("chkSelectLeak");
                    Label lblMilkOrProductDemandChildId = (Label)grl.FindControl("lblMilkOrProductDemandChildId");
                    TextBox txtLeakageQty = (TextBox)grl.FindControl("txtLeakageQty");

                    if (chkSelectLeak.Checked == true)
                    {
                        drIC[0] = lblMilkOrProductDemandChildId.Text;
                        drIC[1] = txtLeakageQty.Text;
                        drIC[2] = objdb.createdBy();
                        drIC[3] = IPAddress;

                        dtInsertChild.Rows.Add(drIC.ItemArray);
                    }
                }
                if (dtInsertChild.Rows.Count > 0)
                {
                    dsleak = objdb.ByProcedure("USP_Trn_MilkOrProductDemandChild_Leakage",
                             new string[] { "Flag" },
                             new string[] { "1" }, "type_Trn_MilkOrProductDemandChild_Leakage", dtInsertChild, "TableSave");

                    if (dsleak.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = dsleak.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        dtInsertChild.Dispose();
                        GetLeakItemDetailByDemandID();
                        lblMsgLeakage.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string msg = dsleak.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsgLeakage.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error:" + msg);
                    }
                }
                
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myLeakageItemDetailsModal()", true);
            }

        }
        catch (Exception ex)
        {
            lblMsgLeakage.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Leakage : " + ex.Message.ToString());
        }
        finally
        {
            if (dsleak != null) { dsleak.Dispose(); }
        }
    }
}