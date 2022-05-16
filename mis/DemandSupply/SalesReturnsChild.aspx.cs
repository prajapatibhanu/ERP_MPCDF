using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class mis_DemandSupply_SalesReturnsChild : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds5, ds6,ds7 = new DataSet();
    double sum1 = 0;
    int sum11 = 0;
    int dsum11 = 0;
    int csum11 = 0;
    int cellIndexbooth = 4;
    IFormatProvider culture = new CultureInfo("en-US", true);
    string success = "", error = "";
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && Session["SDate"].ToString() != "" && Session["SShift"].ToString() != "" && Session["SCategory"].ToString() != "" && Session["SRDIType"].ToString() != "" && Session["SLocation"].ToString()!="")
        {

            if (!Page.IsPostBack)
            {
                GetBoothandOrganizationDetails();
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
    private void GetBoothandOrganizationDetails()
    {
        try
        {
            lblMsg.Text = "";
            string retailertypeid = "";
            spanRDIName.InnerHtml = Session["S_RDIName"].ToString();
            SpanDate.InnerHtml = Session["SDate"].ToString();
            spanShift.InnerHtml = Session["SShiftName"].ToString();
            spanCategory.InnerHtml = Session["SCategoryName"].ToString();

            DateTime odate = DateTime.ParseExact(Session["SDate"].ToString(), "dd/MM/yyyy", culture);
            string supplydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (Session["S_OrganizationId"].ToString() != "0")
            {
                retailertypeid = objdb.GetInstRetailerTypeId();
            }
            else
            {
                retailertypeid = "0";
            }

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date",
                                     "RouteId", "DistributorId", "OrganizationId" ,"RetailerTypeId","AreaId"},
                       new string[] { "4", objdb.Office_ID(), Session["SShift"].ToString(), Session["SCategory"].ToString(), supplydate
                           , Session["S_RouteId"].ToString(), Session["S_DistributorId"].ToString(), Session["S_OrganizationId"].ToString(),retailertypeid,Session["SLocation"].ToString() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }

    private void ReturnInserted()
    {
        try
        {
            string myStringfromdat = Session["SDate"].ToString(); // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtSalesReturnDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblModalMsg.Text = string.Empty;
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    DateTime salesretrundat = DateTime.ParseExact(txtSalesReturnDate.Text, "dd/MM/yyyy", culture);
                    string salesreturndate = salesretrundat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    foreach (GridViewRow gr in GridView2.Rows)
                    {

                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        TextBox txtTotalReturnQty = (TextBox)gr.FindControl("txtTotalReturnQty");
                        TextBox txtRemark = (TextBox)gr.FindControl("txtRemark");
                        Label lblItemid = (Label)gr.FindControl("lblItemid");
                        Label lblSupplyTotalQty = (Label)gr.FindControl("lblSupplyTotalQty");
                        Match match = Regex.Match(txtTotalReturnQty.Text, @"^[0-9]*$");
                         if (match.Success && chkSelect.Checked == true && txtTotalReturnQty.Text != "" && Convert.ToInt32(txtTotalReturnQty.Text) <= Convert.ToInt32(lblSupplyTotalQty.Text))
                            {
                                ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                                       new string[] { "flag", "MilkOrProductDemandId", "Item_id", "TotalReturnQty", "Remark","SalesReturnDate"
                                            , "Office_ID", "CreatedBy", "CreatedByIP", },
                                       new string[] { "6", ViewState["rowid"].ToString(),lblItemid.Text,txtTotalReturnQty.Text.Trim()
                                   ,txtRemark.Text.Trim() , salesreturndate.ToString(),objdb.Office_ID()
                                   , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                                if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    success = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();

                                    if (i == 0)
                                    {
                                        i = 1;
                                    }
                                    else
                                    {
                                        i = i + 1;
                                    }
                                }
                                else
                                {
                                    i = 0;
                                    error = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();

                                }
                            }
                    }
                    if (i > 0)
                    {
                        GetBoothandOrganizationDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Valid Number In Quantity Field & First digit can't be 0(Zero):" + error);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
            else
            {
                txtSalesReturnDate.Text = string.Empty;
                lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Sales Return Date must be greater than or equal to Delivery Date ");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                return;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
                string myStringfromdat = modaldate.InnerHtml;
                DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string myStringtodate = txtSalesReturnDate.Text;
                DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (fdate <= tdate)
                {
                    lblModalMsg.Text = string.Empty;
                    ReturnInserted();
                }
                else
                {
                    txtSalesReturnDate.Text = string.Empty;
                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Sales Return Date must be greater than or equal to Delivery Date ");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    return;
                }
           
           
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Challanno")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                ViewState["rowid"] = e.CommandArgument.ToString();
                GetItemDetailByID();
                modalBoothName.InnerHtml = lblBandOName.Text;
                modaldate.InnerHtml = Session["SDate"].ToString();
                modalshift.InnerHtml = Session["SShiftName"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);


            }
        }
        if (e.CommandName == "ChallannoUpdate")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                ViewState["rowmpid"] = e.CommandArgument.ToString();
                GetItemDetailByID_Retrun();
                modalBoothName1.InnerHtml = lblBandOName.Text;
                modaldate1.InnerHtml = Session["SDate"].ToString();
                modalshift1.InnerHtml = Session["SShiftName"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);


            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbMilkOrProductReturnId = e.Row.FindControl("lbMilkOrProductReturnId") as Label;
                LinkButton lnkActiveChallanNo = e.Row.FindControl("lnkActiveChallanNo") as LinkButton;
                LinkButton lnkDeActiveChallanNo = e.Row.FindControl("lnkDeActiveChallanNo") as LinkButton;
                if (lbMilkOrProductReturnId.Text == "")
                {
                    lnkActiveChallanNo.Visible = true;
                    lnkDeActiveChallanNo.Visible = false;
                }
                else
                {
                    lnkDeActiveChallanNo.Visible = true;
                    lnkActiveChallanNo.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 3 : " + ex.Message.ToString());
        }
    }
    private void GetItemDetailByID()
    {
        try
        {
            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                  new string[] { "5", ViewState["rowid"].ToString(), Session["SCategory"].ToString(), objdb.Office_ID() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds5.Tables[0];
                GridView2.DataBind();
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 4 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    private void GetItemDetailByID_Retrun()
    {
        try
        {
            ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                  new string[] { "10", ViewState["rowmpid"].ToString(), Session["SCategory"].ToString(), objdb.Office_ID() }, "dataset");
            if (ds6.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = ds6.Tables[0];
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = null;
                GridView3.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg1.Text = string.Empty;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblTotalReturnQty = (Label)row.FindControl("lblTotalReturnQty");

                    TextBox txtTotalReturnQty = (TextBox)row.FindControl("txtTotalReturnQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfvtrq = row.FindControl("rfvtrq") as RequiredFieldValidator;
                    RegularExpressionValidator revtrq = row.FindControl("revtrq") as RegularExpressionValidator;
                    CustomValidator cv1 = row.FindControl("CustomValidator1") as CustomValidator;

                    foreach (GridViewRow gvRow in GridView3.Rows)
                    {
                        Label HlblTotalReturnQty = (Label)gvRow.FindControl("lblTotalReturnQty");
                        Label HlblLastUpdatedStatus = (Label)gvRow.FindControl("lblLastUpdatedStatus");

                        TextBox HtxtTotalReturnQty = (TextBox)gvRow.FindControl("txtTotalReturnQty");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfvtrq = gvRow.FindControl("rfvtrq") as RequiredFieldValidator;
                        RegularExpressionValidator Hrevtrq = gvRow.FindControl("revtrq") as RegularExpressionValidator;
                        CustomValidator Hcv1 = gvRow.FindControl("CustomValidator1") as CustomValidator;
                        HlblTotalReturnQty.Visible = true;
                        HtxtTotalReturnQty.Visible = false;
                        if (HlblLastUpdatedStatus.Text == "")
                        {
                            HlnkEdit.Visible = true;
                        }
                        else
                        {
                            HlnkEdit.Visible = false;
                        }
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfvtrq.Enabled = false;
                        Hrevtrq.Enabled = false;
                        Hcv1.Enabled = false;
                    }
                    txtTotalReturnQty.Text = "";
                    txtTotalReturnQty.Text = lblTotalReturnQty.Text;
                    lblTotalReturnQty.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfvtrq.Enabled = true;
                    revtrq.Enabled = true;
                    cv1.Enabled = true;
                    txtTotalReturnQty.Visible = true;
                }

            }
            if (e.CommandName == "RecordReset")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblTotalReturnQty = (Label)row.FindControl("lblTotalReturnQty");

                    TextBox txtTotalReturnQty = (TextBox)row.FindControl("txtTotalReturnQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfvtrq = row.FindControl("rfvtrq") as RequiredFieldValidator;
                    RegularExpressionValidator revtrq = row.FindControl("revtrq") as RegularExpressionValidator;
                    CustomValidator cv1 = row.FindControl("CustomValidator1") as CustomValidator;

                    lblModalMsg1.Text = string.Empty;

                    lblTotalReturnQty.Visible = true;
                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfvtrq.Enabled = false;
                    revtrq.Enabled = false;
                    cv1.Enabled = false;
                    txtTotalReturnQty.Visible = false;
                }

            }
            if (e.CommandName == "RecordUpdate")
            {
                if (Page.IsValid)
                {
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);
                        lblModalMsg1.Text = string.Empty;
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblTotalReturnQty = (Label)row.FindControl("lblTotalReturnQty");

                        TextBox txtTotalReturnQty = (TextBox)row.FindControl("txtTotalReturnQty");
                        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                        LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");

                        if (txtTotalReturnQty.Text == "")
                        {
                            lblModalMsg1.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter Total Return Qty.");
                            return;
                        }
                        else
                        {
                            ds7 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                             new string[] { "flag", "MilkOrProductReturnChildId", "TotalReturnQty", "CreatedBy", "CreatedByIP" },
                             new string[] { "11", e.CommandArgument.ToString(), txtTotalReturnQty.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

                            if (ds7.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblModalMsg1.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                GetItemDetailByID_Retrun();
                            }
                            else
                            {
                                string error = ds7.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblModalMsg1.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  6:" + error);
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
        finally
        {
            if (ds7 != null) { ds7.Dispose(); }
        }

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
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

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5 : " + ex.Message.ToString());
        }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        GridViewRow gvr = (GridViewRow)cv.NamingContainer;
        TextBox txtTotalReturnQty = (TextBox)gvr.FindControl("txtTotalReturnQty");
        Label lblSupplyTotalQty = (Label)gvr.FindControl("lblSupplyTotalQty");
        if (Convert.ToInt32(txtTotalReturnQty.Text) <= Convert.ToInt32(lblSupplyTotalQty.Text))
        {
            args.IsValid = true;
        }
        else
            args.IsValid = false;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal1()", true);
    }

}