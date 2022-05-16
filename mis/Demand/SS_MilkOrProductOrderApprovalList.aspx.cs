using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class mis_Demand_SS_MilkOrProductOrderApprovalList : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds33, ds5, dsadd = new DataSet();

    string orderdate = "",currentdate = "", currrentime = "";
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && ddlItemCategory.SelectedValue != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetCategory();
                DisplayMilkTime();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "readonly");
                GetItemByCategory();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void DisplayMilkTime()
    {
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "2")
        {
            pnlmilktimeline.Visible = true;
            pnlmilkMD.InnerHtml = "Note : Morning Demand -(12:00 pm to 04:30 pm).";
            pnlmilkED.InnerHtml = "Note : Evening Demand -(08:00 am to 11:30 am).";
            pnlproducttimeline.Visible = false;
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "2")
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = true;
            pnlproductMD.InnerHtml = "Note : Product Morning Demand -(07:00 am to 10:00 am)";
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4")
        {
            pnlmilktimeline.Visible = true;
            pnlmilkMD.InnerHtml = "Note : Morning Demand -(03:00 pm to 10:00 pm).";
            pnlmilkED.InnerHtml = "Note : Evening Demand -(09:00 am to 02:00 pm).";
            pnlproducttimeline.Visible = false;
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "4")
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = true;
            pnlproductMD.InnerHtml = "Note : Product Morning Demand -(09:00 am to 02:30 pm)";
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjain Milk
        {
            pnlmilktimeline.Visible = true;
            pnlmilkMD.InnerHtml = "Note : Morning Demand -(06:00 pm to 9:00 pm).";
            pnlmilkED.InnerHtml = "Note : Evening Demand -(09:00 am to 01:00 pm).";
            pnlproducttimeline.Visible = false;
        }
        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "6") // Ujjain Product
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = true;
            pnlproductMD.InnerHtml = "Note : Product Morning Demand -(09:00 am to 01 :00 pm)";
        }
        else
        {
            pnlmilktimeline.Visible = false;
            pnlproducttimeline.Visible = false;
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error S ", ex.Message.ToString());
        }
    }
    protected void GetOrderDetails()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                     new string[] { "flag", "Office_ID", "SuperStockistId", "Demand_Date", "Shift_id", "ItemCat_id", "Demand_Status" },
                       new string[] { "4", objdb.Office_ID(), objdb.createdBy(), orderdate, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlStatus.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                lblMsg.Text = string.Empty;
                btnApp.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
            }
            else
            {
                btnApp.Visible = false;
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
       // txtOrderDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        pnlsearch.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();

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
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["rowid"].ToString(), ViewState["rowitemcatid"].ToString(), objdb.Office_ID() }, "dataset");
        
            if(ds1.Tables[0].Rows.Count>0 && ds1!=null)
            {
                GridView4.DataSource = ds1.Tables[0];
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
                if (lblDemandStatus.Text == "2")
                {
                    e.Row.Cells[7].Visible = false;
                    GridView4.HeaderRow.Cells[7].Visible = false;
                    //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else if (lblDemandStatus.Text == "3" && lblProductDMStatus.Text != "1")
                {
                    e.Row.Cells[7].Visible = false;
                    GridView4.HeaderRow.Cells[7].Visible = false;
                    //  btnApproved.Visible = false;
                    btnReject.Visible = false;
                }
                else if (lblDemandStatus.Text == "3" && lblMilkCurDMCrateIsueStatus.Text == "" && lblProductDMStatus.Text == "1")
                {
                    e.Row.Cells[7].Visible = true;
                    GridView4.HeaderRow.Cells[7].Visible = true;
                    //  btnApproved.Visible = false;
                    btnReject.Visible = true;
                }
                else if (lblDemandStatus.Text == "3" && lblMilkCurDMCrateIsueStatus.Text != "" && lblProductDMStatus.Text == "1")
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
    private void CheckDemandOrderTime()
    {
        try
        {
            ds5 = objdb.ByProcedure("USP_GetServerDatetime",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                string myStringfromdat = txtOrderDate.Text; // From Database
                DateTime demanddate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime demanddateplus = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string myStringcurrentdate = ds5.Tables[0].Rows[0]["currentDate"].ToString();
                DateTime currentdate = DateTime.ParseExact(myStringcurrentdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                currrentime = ds5.Tables[0].Rows[0]["currentTime"].ToString();
                string[] s = currrentime.Split(':');
                if (demanddate == currentdate)
                {
                    if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedValue==objdb.GetMilkCatId())
                    {
                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4") // Indore
                        {

                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                            return;
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjain
                        {

                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                            return;
                        }
                        else
                        {
                            ForwardDemand();
                        }
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedValue==objdb.GetProductCatId())
                    {
                        
                        if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "4") // Indore
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + objdb.GetProductCategoryName() + " can not place on Date: " + txtOrderDate.Text + "");
                            return;
                        }
                        else if (ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "6") // Ujjain
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + objdb.GetProductCategoryName() + " can not place on Date: " + txtOrderDate.Text + "");
                            return;
                        }
                        else
                        {
                            ForwardDemand();
                        }

                    }
                    else if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                    {
                        
                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4") // indore  9am to 2 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 14 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 14 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 14 && Convert.ToInt32(s[1]) <= 59)))
                            {
                                ForwardDemand();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                return;
                            }
                        }
                       else if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjan  9am to 1 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 15 && ddlShift.SelectedItem.Text == "Evening") && ((Convert.ToInt32(s[0]) == 15 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 15 && Convert.ToInt32(s[1]) <= 59)))
                            {
                                ForwardDemand();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                return;
                            }
                        }
                        else
                        {
                            ForwardDemand();
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + objdb.GetProductCategoryName() + " can be done in Evening shift.");
                        return;
                    }
                }
                else if (demanddate >= currentdate)
                {
                    if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedValue == objdb.GetProductCatId())
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + objdb.GetProductCategoryName() + " can be done in Morning shift.");
                        return;
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedValue == objdb.GetProductCatId())
                    {
                        demanddateplus = demanddateplus.AddDays(-1);
                        if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "4") // indore  9 am to 2:30 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 14 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 14 && Convert.ToInt32(s[1]) <= 31) || (Convert.ToInt32(s[0]) < 14 && Convert.ToInt32(s[1]) <= 59)))
                            {
                                ForwardDemand();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                return;
                            }
                        }
                        else if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetProductCatId() && objdb.Office_ID() == "6") // Ujjan 9 am to 1:00 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 9 && Convert.ToInt32(s[0]) <= 15 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 15 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 15 && Convert.ToInt32(s[1]) <= 59)))
                            {
                                ForwardDemand();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                return;
                            }
                        }
                        else
                        {
                            ForwardDemand();
                        }
                    }
                    else if (ddlShift.SelectedItem.Text == "Morning" && ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                    {
                        demanddateplus = demanddateplus.AddDays(-1);
                        if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "4") // indore morning 3 pm to 10 pm
                        {
                            if ((Convert.ToInt32(s[0]) >= 15 && Convert.ToInt32(s[0]) <= 22 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 22 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 22 && Convert.ToInt32(s[1]) <= 59)))
                            {
                                ForwardDemand();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                return;
                            }
                        }
                       else if (currentdate == demanddateplus && ddlItemCategory.SelectedValue == objdb.GetMilkCatId() && objdb.Office_ID() == "6") // Ujjan morning 6 pm to 9 pm
                        {
                            //if ((Convert.ToInt32(s[0]) >= 18 && Convert.ToInt32(s[0]) <= 21 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 21 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 21 && Convert.ToInt32(s[1]) <= 59)))
                            if ((Convert.ToInt32(s[0]) >= 18 && Convert.ToInt32(s[0]) <= 23 && ddlShift.SelectedItem.Text == "Morning") && ((Convert.ToInt32(s[0]) == 23 && Convert.ToInt32(s[1]) < 31) || (Convert.ToInt32(s[0]) < 23 && Convert.ToInt32(s[1]) <= 59))) // temprorary 
                            {
                                ForwardDemand();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", " Submission time exceeded then actual time.");
                                return;
                            }
                        }
                        else
                        {
                            ForwardDemand();
                        }
                    }
                    else
                    {
                        ForwardDemand();
                    }
                }
                else
                {
                    ForwardDemand();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnApp_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (Page.IsValid)
            {
                CheckDemandOrderTime();
               
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 24 :" + ex.ToString());
        }
       
    }
    private void ForwardDemand()
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
            }

        }
        if (dtInsertChild.Rows.Count > 0)
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand_UpdateStatusOrChallan",
                                         new string[] { "flag" },
                                         new string[] { "3" }, "type_Trn_MilkOrProductDemand_DSandCC", dtInsertChild, "TableSave");

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
         if (ds2 != null) { ds2.Dispose(); }
        
    }
    protected void GetItemByCategory()
    {
        try
        {

            ds2 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                     new string[] { "flag", "Office_ID", "ItemCat_id" },
                       new string[] { "2", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
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
    private void AddItem()
    {
        try
        {
            dsadd = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                           new string[] { "flag", "MilkOrProductDemandId", "Item_id", "TotalQty" },
                          new string[] { "24", ViewState["rowAddid"].ToString(), ddlItemName.SelectedValue, txtTotalQty.Text.Trim() }, "dataset");
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
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayMilkTime();
		GetItemByCategory();
    }
}