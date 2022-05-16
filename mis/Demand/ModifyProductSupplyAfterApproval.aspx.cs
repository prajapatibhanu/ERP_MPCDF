using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Demand_ModifyProductSupplyAfterApproval : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds4, ds33, ds5, dsleak = new DataSet();

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
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
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
                ddlShift.Items.Insert(0, new ListItem("All", "0"));
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
                     new string[] { "flag", "Office_ID", "AreaId", "RouteId", "Demand_Date", "Shift_id", "ItemCat_id" },
                       new string[] { "25", objdb.Office_ID(), ddlLocation.SelectedValue, ddlRoute.SelectedValue, orderdate, ddlShift.SelectedValue, objdb.GetProductCatId() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                lblMsg.Text = string.Empty;

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
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

                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                ViewState["rowid"] = e.CommandArgument.ToString();
                GetItemDetailByDemandID();
                modalBoothName.InnerHtml = lblBandOName.Text;
                modaldate.InnerHtml = txtOrderDate.Text;
                modalshift.InnerHtml = ddlShift.SelectedItem.Text;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                GetDatatableHeaderDesign();

            }
        }
    }
    private void GetItemDetailByDemandID()
    {
        try
        {
            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                  new string[] { "2", ViewState["rowid"].ToString(), objdb.GetProductCatId(), objdb.Office_ID() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                GridView4.DataSource = ds5.Tables[0];
                GridView4.DataBind();
            }
            else
            {
                GridView4.DataSource = null;
                GridView4.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
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
                    Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                    Label lblRemarkAtSupply = (Label)row.FindControl("lblRemarkAtSupply");
                    TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                    TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;


                    foreach (GridViewRow gvRow in GridView4.Rows)
                    {
                        Label HlblFinalSupplQtyUpdatedBy = (Label)gvRow.FindControl("lblFinalSupplQtyUpdatedBy");
                        Label HlblSupplyTotalQty = (Label)gvRow.FindControl("lblSupplyTotalQty");
                        Label HlblRemarkAtSupply = (Label)gvRow.FindControl("lblRemarkAtSupply");
                        TextBox HtxtSupplyTotalQty = (TextBox)gvRow.FindControl("txtSupplyTotalQty");
                        TextBox HtxtRemarkAtSupply = (TextBox)gvRow.FindControl("txtRemarkAtSupply");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv = gvRow.FindControl("rfv1") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev1 = gvRow.FindControl("rev1") as RegularExpressionValidator;
                       
                        HlblSupplyTotalQty.Visible = true;
                        HlblRemarkAtSupply.Visible = true;
                        HtxtSupplyTotalQty.Visible = false;
                        HtxtRemarkAtSupply.Visible = false;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv.Enabled = false;
                        Hrev1.Enabled = false;

                        if (HlblFinalSupplQtyUpdatedBy.Text == "")
                        {
                            HlnkEdit.Visible = true;
                        }
                        else
                        {
                            HlnkEdit.Visible = false;
                        }

                    }
                    txtSupplyTotalQty.Text = "";
                    txtSupplyTotalQty.Text = lblSupplyTotalQty.Text;
                    lblSupplyTotalQty.Visible = false;
                    
                    lblRemarkAtSupply.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv.Enabled = true;
                    rev1.Enabled = true;
                  
                    txtSupplyTotalQty.Visible = true;
                    txtRemarkAtSupply.Visible = true;
                    
                    GetDatatableHeaderDesign();
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
                    Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                    Label lblRemarkAtSupply = (Label)row.FindControl("lblRemarkAtSupply");
                    TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                    TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv = row.FindControl("rfv1") as RequiredFieldValidator;
                    RegularExpressionValidator rev1 = row.FindControl("rev1") as RegularExpressionValidator;


                    lblModalMsg.Text = string.Empty;

                    lblSupplyTotalQty.Visible = true;
                    
                    lblRemarkAtSupply.Visible = true;
                    lnkEdit.Visible = true;

                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv.Enabled = false;
                    rev1.Enabled = false;
                    txtSupplyTotalQty.Visible = false;
                    txtRemarkAtSupply.Visible = false;
                    GetDatatableHeaderDesign();
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
                        Label lblSupplyTotalQty = (Label)row.FindControl("lblSupplyTotalQty");
                        TextBox txtSupplyTotalQty = (TextBox)row.FindControl("txtSupplyTotalQty");
                        TextBox txtRemarkAtSupply = (TextBox)row.FindControl("txtRemarkAtSupply");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");

                        if (txtSupplyTotalQty.Text == "0" && txtRemarkAtSupply.Text == "")
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                        else
                        {
                            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                             new string[] { "flag", "MilkOrProductDemandChildId", "SupplTotalQty", "RemarkAtSupply", "CreatedBy", "CreatedByIP" },
                             new string[] { "20", e.CommandArgument.ToString(), txtSupplyTotalQty.Text.Trim(), txtRemarkAtSupply.Text.Trim(), objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress() }, "TableSave");

                            if (ds4.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                GetItemDetailByDemandID();
                                GetDatatableHeaderDesign();
                            }
                            else
                            {
                                string error = ds4.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6: " + ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;
                if (lblItemCatName.Text == "Milk")
                {
                    e.Row.CssClass = "columnmilk";
                }
                else
                {
                    e.Row.CssClass = "columnproduct";
                }
                if (objdb.Office_ID() == "5")
                {
                    GridView4.HeaderRow.Cells[5].Visible = true;
                    e.Row.Cells[5].Visible = true;
                }
                else
                {
                    GridView4.HeaderRow.Cells[5].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7 : " + ex.Message.ToString());
        }
    }
    private void ApprovedOrRejectedOrderd(string status, string routid, string distid)
    {
        ds33 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                           new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RouteId", "DistributorId" },
                           new string[] { "14", ViewState["rowid"].ToString(), status.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Approved/Rejetecd) Updated Successfully.", routid, distid }, "TableSave");

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
}