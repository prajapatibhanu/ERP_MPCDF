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

public partial class mis_Demand_ModifyMilkorProductDemandBeforeGatePass : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6,ds7, dsadd = new DataSet();
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
                GetDistributor();
                
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
            ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS", 
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" },
                new string[] { "7", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddate.ToString() }, "dataset");
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


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static List<string> SearchPartyName(string PartyName, string DDate, string DShift, string Ditemcat)
    //{
    //    List<string> partylist = new List<string>();
    //    try
    //    {
    //        APIProcedure objdb = new APIProcedure();
    //        CommanddlFill objddl = new CommanddlFill();
    //        IFormatProvider culture = new CultureInfo("en-US", true);
    //        DateTime date3 = DateTime.ParseExact(DDate, "dd/MM/yyyy", culture);
    //        string ddate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        DataSet ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS", new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date" }, new string[] { "7", objdb.Office_ID(), DShift, Ditemcat, ddate.ToString() }, "dataset");

    //        DataView dv = new DataView();
    //        dv = ds7.Tables[0].DefaultView;
    //        if (ds7 != null) { ds7.Dispose(); }
    //        dv.RowFilter = "BName like '%" + PartyName + "%'";
    //        DataTable dt = dv.ToTable();

    //        foreach (DataRow rs in dt.Rows)
    //        {
    //            foreach (DataColumn col in dt.Columns)
    //            {
    //                partylist.Add(rs[col].ToString());
    //            }
    //        }

    //    }
    //    catch { }
    //    return partylist;
    //}
    private void GetOrderNo()
    {
        try
        {
            if (txtDate.Text != "" && ddlShift.SelectedValue != "0")
            {
                lblMsg.Text = string.Empty;
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string odat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                pnldata.Visible = false;

                ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                      new string[] { "flag", "Demand_Date", "Shift_id", "ItemCat_id", "BName", "Office_ID" },
                        new string[] { "6", odat, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlDistributorName.SelectedItem.Text, objdb.Office_ID() }, "dataset");

                if (ds5.Tables[0].Rows.Count > 0)
                {
                    pnloderdetails.Visible = true;

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
    private void GetItemDetailByDemandID()
    {
        try
        {
            string tmpMilkOrProductDemandId = "", orderno = "";
            lblMsg.Text = string.Empty;
            foreach (GridViewRow gridrow in GridViewOrderDetails.Rows)
            {

                CheckBox chkSelect = (CheckBox)gridrow.FindControl("chkSelect");

                if (chkSelect.Checked == true)
                {

                    Label lblMilkOrProductDemandId = (Label)gridrow.FindControl("lblMilkOrProductDemandId");
                    Label lblOrderId = (Label)gridrow.FindControl("lblOrderId");
                    tmpMilkOrProductDemandId = lblMilkOrProductDemandId.Text;
                    orderno = lblOrderId.Text;

                }
            }
            ViewState["tmpMPDemandId"] = tmpMilkOrProductDemandId.ToString();
            ViewState["tmporderno"] = orderno.ToString();
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                   new string[] { "flag", "MilkOrProductDemandId" },
                     new string[] { "2", tmpMilkOrProductDemandId.ToString() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                pnldata.Visible = true;
                GridView4.DataSource = ds1.Tables[0];
                GridView4.DataBind();
            }
            else
            {
                pnldata.Visible = false;
                GridView4.DataSource = null;
                GridView4.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistributor();
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GetItemDetailByDemandID();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
    }

    protected void GridViewOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DMReject")
        {
            try
            {
                lblMsg.Text = string.Empty;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                               new string[] { "flag", "MilkOrProductDemandId", "Demand_Status", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                               new string[] { "14", e.CommandArgument.ToString(), "2", objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + IPAddress, Path.GetFileName(Request.Url.AbsolutePath), "Demand Status(Rejetecd) Updated Successfully." }, "TableSave");

                if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    GetOrderNo();
                    pnldata.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                }
                else
                {
                    string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }
            catch (Exception ex)
            {

                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Rejection Error ", ex.Message.ToString());
            }
            finally
            {
                if (ds6 != null) { ds6.Dispose(); }
            }

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
                    lblMsg.Text = string.Empty;


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

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
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
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Remark");
                            return;
                        }
                        else
                        {
                            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandAtDoc",
                                                     new string[] { "flag", "MilkOrProductDemandChildId", "ItemQty", "TotalQty", "CreatedBy", "CreatedByIP", "PageName", "Remark", "RemarkAtOrderApproval" },
                                                     new string[] { "3", e.CommandArgument.ToString(), txtItemQty.Text.Trim(), totalqty.ToString(), objdb.createdBy(), IPAddress + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Modify Modify Demand at Doc Page", txtRemarkAtOrderApproval.Text.Trim() }, "TableSave");

                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                GetItemDetailByDemandID();
                            }
                            else
                            {
                                string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
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
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    GetItemDetailByDemandID();
                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 20 : " + ex.Message.ToString());
        }
    }
    #endregion========================================================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetOrderNo();
        }
    }
    protected void lnkAddItem_Click(object sender, EventArgs e)
    {
        lblModalMsg1.Text = string.Empty;
        modalparyname.InnerHtml = ddlDistributorName.SelectedItem.Text;
        partymodaldate.InnerHtml = txtDate.Text;
        modalshift.InnerHtml = ddlShift.SelectedItem.Text;
        partymodalOrderNo.InnerHtml = ViewState["tmporderno"].ToString();
        txtTotalQty.Text = string.Empty;
        GetItemByCategory();
        ddlItemName.SelectedIndex = 0;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddItemDetailsModal()", true);
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
            if (ViewState["tmpMPDemandId"].ToString() != "" && ViewState["tmpMPDemandId"].ToString() != "0")
            {

                dsadd = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                               new string[] { "flag", "MilkOrProductDemandId", "Item_id", "TotalQty" },
                              new string[] { "24", ViewState["tmpMPDemandId"].ToString(), ddlItemName.SelectedValue, txtTotalQty.Text.Trim() }, "dataset");
                if (dsadd.Tables[0].Rows.Count > 0)
                {
                    if (dsadd.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string msg = dsadd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-success", "Success!", "" + msg.ToString());
                        txtTotalQty.Text = string.Empty;
                        ddlItemName.SelectedIndex = 0;
                        GetItemDetailByDemandID();
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
            AddItem();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddItemDetailsModal()", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddItemDetailsModal()", true);
        }
    }



    
}