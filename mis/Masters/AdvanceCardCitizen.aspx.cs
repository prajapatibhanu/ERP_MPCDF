using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;


public partial class mis_AdvanceCardCitizen : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    string currentdate = "", currrentime = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCitizenAdvanceCardDetails(); // citizen registration details
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                SetEffectiveDate();
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

    #region======== Citizen registration entry Code

    #region=======================user defined function========================

    private void ClearCitizenEnry()
    {
        lblMappingMsg.Text = string.Empty;
        txtCitizenName.Text = string.Empty;
        txtMobNo.Text = string.Empty;
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }


    private void GetCitizenAdvanceCardDetails()
    {
        try
        {

            ds = objdb.ByProcedure("USP_Mst_AdvanceCard",
                         new string[] { "flag", "CreatedBy", "Office_ID" },
                        new string[] { "1", objdb.createdBy(), objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                pnlItemMapping.Visible = true;
                GetCitizenName();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlItemMapping.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void InsertOrUpdateAdvanceCard()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {

                    ds2 = objdb.ByProcedure("USP_GetServerDatetime",
                    new string[] { "flag" },
                      new string[] { "1" }, "dataset");
                    if (ds2.Tables[0].Rows.Count > 0)
                    {

                        currentdate = ds2.Tables[0].Rows[0]["currentDate"].ToString();
                        string[] s = currentdate.Split('/');



                        if (btnSubmit.Text == "Save")
                        {
                            if (Convert.ToInt16(s[0]) >= 10 && Convert.ToInt16(s[0]) <= 15)
                            {
                                lblMsg.Text = "";
                                ds = objdb.ByProcedure("USP_Mst_AdvanceCard",
                                    new string[] { "flag", "CitizenName", "MobNo", "Office_ID", "CreatedBy", "CreatedByIP" },

                                    new string[] { "2", txtCitizenName.Text.Trim(), txtMobNo.Text.Trim(), objdb.Office_ID(), 
                                                        objdb.createdBy(),  objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    ClearCitizenEnry();
                                    GetCitizenAdvanceCardDetails();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                }
                                else
                                {
                                    string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                    {
                                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                                    }
                                    else
                                    {
                                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                    }
                                }
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Registration of Citizen allowed between 10th to 15th of Every Month");
                                return;
                            }
                        }
                        if (btnSubmit.Text == "Update")
                        {
                            lblMsg.Text = "";
                            ds = objdb.ByProcedure("USP_Mst_AdvanceCard",
                                new string[] { "flag","CitizenId", "CitizenName", "MobNo",
                                                    "Office_ID", "CreatedBy", "CreatedByIP","PageName","Remark" },
                                new string[] { "3", ViewState["rowid"].ToString(),
                                txtCitizenName.Text.Trim(), txtMobNo.Text.Trim(), objdb.createdBy(),objdb.Office_ID(), 
                                          objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Citizen advance card registration record Updated" }, "TableSave");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                ClearCitizenEnry();
                                GetCitizenAdvanceCardDetails();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                if (error == "Already Exists")
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                }
                            }

                        }

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Date not Set.Record Save after sometime.");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Citizen Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null) { ds.Dispose(); }
                if (ds2 != null) { ds2.Dispose(); }
            }
        }

    }
    #endregion====================================end of user defined function

    #region============ button or gridview row command click event ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateAdvanceCard();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ClearCitizenEnry();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    btnSubmit.Text = "Update";
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblCitizenId = (Label)row.FindControl("lblCitizenId");
                    Label lblCitizenName = (Label)row.FindControl("lblCitizenName");
                    Label lblMobNo = (Label)row.FindControl("lblMobNo");
                    Label lblCitizenCode = (Label)row.FindControl("lblCitizenCode");
                    Label lblEffectiveFromDate = (Label)row.FindControl("lblEffectiveFromDate");
                    Label lblEffectiveToDate = (Label)row.FindControl("lblEffectiveToDate");
                    Label lblBoothId = (Label)row.FindControl("lblBoothId");

                    txtCitizenName.Text = lblCitizenName.Text;
                    txtMobNo.Text = lblMobNo.Text;
                    //txtCitizenCode.Text = lblCitizenCode.Text;
                    //txtEffectiveFromDate.Text = lblEffectiveFromDate.Text;
                    //txtEffectiveToDate.Text = lblEffectiveToDate.Text;
                    //ddlParlour.SelectedValue = lblBoothId.Text;

                    ViewState["rowid"] = e.CommandArgument;
                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("USP_Mst_AdvanceCard",
                                new string[] { "flag", "CitizenId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Citizen registration record deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMappingMsg.Text = string.Empty;
                        ClearCitizenEnry();
                        GetCitizenAdvanceCardDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    #endregion=============end of button click funciton==================



    #endregion==================end of citizen Citizen registration entry Code

    #region==========Item Citizen Mapping code========================

    #region=======================user defined function========================

    private void MappingClear()
    {
        lblMappingMsg.Text = string.Empty;
        ddlCitizenName.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;
    }
    private void SetEffectiveDate()
    {
        try
        {

            ds3 = objdb.ByProcedure("USP_GetServerDatetime",
                    new string[] { "flag" },
                      new string[] { "1" }, "dataset");
            if (ds3.Tables[0].Rows.Count > 0)
            {

                currentdate = ds3.Tables[0].Rows[0]["currentDate"].ToString();
                DateTime cdate = DateTime.ParseExact(currentdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string nextmonthdate = cdate.AddMonths(1).ToString("dd/MM/yyyy");
                string[] cmd = currentdate.Split('/');
                string[] nmd = nextmonthdate.Split('/');
                string effectfivefdate = "16" + "/" + cmd[1] + "/" + cmd[2];
                string effectfivetdate = "15" + "/" + nmd[1] + "/" + nmd[2];
                txtEffectiveFromDate.Text = effectfivefdate.ToString();
                txtEffectiveToDate.Text = effectfivetdate.ToString();
                txtEffectiveFromDate.Attributes.Add("disabled", "disabled");
                txtEffectiveToDate.Attributes.Add("disabled", "disabled");
            }
        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Set Effective FromDate and ToDate: ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtEffectiveFromDate.Text;
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtEffectiveToDate.Text;
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMappingMsg.Text = string.Empty;
            }
            else
            {
                txtEffectiveToDate.Text = string.Empty;
                lblMappingMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must less than FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
        }
    }
    private void GetCitizenName()
    {


        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                     new string[] { "flag", "Office_ID", "CreatedBy" },
                       new string[] { "1", objdb.Office_ID(), objdb.createdBy() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlCitizenName.DataTextField = "CNameCode";
                ddlCitizenName.DataValueField = "CitizenId";
                ddlCitizenName.DataSource = ds1.Tables[0];
                ddlCitizenName.DataBind();
                ddlCitizenName.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }

    protected void GetItemCategory()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                  new string[] { "flag" },
                 new string[] { "6" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds1.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetItem()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                       new string[] { "flag", "ItemCat_id", "Office_ID" },
                       new string[] { "7", ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataSource = ds1.Tables[0];
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetShift()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds1.Tables[0];
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void GetCitizenItemMappingDetails()
    {
        try
        {

            GridView2.DataSource = objdb.ByProcedure("USP_Mst_AdvanceCard",
                         new string[] { "flag", "CreatedBy", "Office_ID" },
                        new string[] { "9", objdb.createdBy(), objdb.Office_ID() }, "dataset");
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 13 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void InsertMappingRecordOfAdvanceCard()
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime date3 = DateTime.ParseExact(txtEffectiveFromDate.Text, "dd/MM/yyyy", culture);
                DateTime date4 = DateTime.ParseExact(txtEffectiveToDate.Text, "dd/MM/yyyy", culture);

                ds2 = objdb.ByProcedure("USP_GetServerDatetime",
                   new string[] { "flag" },
                     new string[] { "1" }, "dataset");
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    currentdate = ds2.Tables[0].Rows[0]["currentDate"].ToString();
                    string[] im = currentdate.Split('/');

                    if (Convert.ToInt16(im[0]) >= 10 && Convert.ToInt16(im[0]) <= 15)
                    {
                        if (btnMSubmit.Text == "Save")
                        {
                            lblMappingMsg.Text = "";
                            ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                                new string[] { "flag", "CitizenId", "ItemCat_id", "Item_id", "Total_ItemQty","Shift_id",
                                "EffectiveFromDate","EffectiveToDate", "Office_ID", "CreatedBy", "CreatedByIP","PlatformType" },

                                new string[] { "8", ddlCitizenName.SelectedValue, ddlItemCategory.SelectedValue,ddlItem.SelectedValue, 
                                txtItemQty.Text.Trim(),ddlShift.SelectedValue,date3.ToString("yyyy/MM/dd"),date4.ToString("yyyy/MM/dd")
                                ,objdb.Office_ID(), objdb.createdBy(),  objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),"1" }, "TableSave");

                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                MappingClear();
                                GetCitizenItemMappingDetails();
                                lblMappingMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            }
                            else
                            {
                                string msg = ds1.Tables[0].Rows[0]["Msg"].ToString();
                                string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                if (msg == "Already")
                                {
                                    lblMappingMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                                }
                                else
                                {
                                    lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                }
                            }
                        }
                    }
                    else
                    {
                        lblMappingMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Item Mapping of Citizen allowed between 10th to 15th of Every Month");
                    }
                }
                else
                {
                    lblMappingMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Date not Set.Record Save after sometime.");
                }
            }
            else
            {
                lblMappingMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Citizen Name from List");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion=============== end user defined function
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetItemCategory();
    }
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlCitizenName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCitizenName.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            lblMappingMsg.Text = string.Empty;
            pnlCitizenItemMapping.Visible = true;
            GetCitizenItemMappingDetails();
        }
        else
        {
            pnlCitizenItemMapping.Visible = false;
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
    }


    protected void txtEffectiveFromDate_TextChanged(object sender, EventArgs e)
    {
        lblMappingMsg.Text = string.Empty;
        txtEffectiveToDate.Text = string.Empty;

    }
    protected void txtEffectiveToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtEffectiveFromDate.Text != "" || txtEffectiveFromDate.Text != string.Empty)
        {
            GetCompareDate();
        }
        else
        {
            lblMappingMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select From Date First");
        }
    }
    protected void btnMSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertMappingRecordOfAdvanceCard();
        }
    }
    protected void btnMClear_Click(object sender, EventArgs e)
    {
        lblMappingMsg.Text = string.Empty;
        pnlCitizenItemMapping.Visible = false;
        MappingClear();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "RecordEdit")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblMappingMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblTotalItemQty");
                    TextBox txtTotalItemQty = (TextBox)row.FindControl("txtTotalItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv10 = row.FindControl("rfv10") as RequiredFieldValidator;
                    RegularExpressionValidator rev10 = row.FindControl("rev10") as RegularExpressionValidator;

                    foreach (GridViewRow gvRow in GridView2.Rows)
                    {
                        Label HlblItemQty = (Label)gvRow.FindControl("lblTotalItemQty");
                        TextBox HtxtTotalItemQty = (TextBox)gvRow.FindControl("txtTotalItemQty");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        RequiredFieldValidator Hrfv10 = gvRow.FindControl("rfv10") as RequiredFieldValidator;
                        RegularExpressionValidator Hrev10 = gvRow.FindControl("rev10") as RegularExpressionValidator;
                        HlblItemQty.Visible = true;
                        HtxtTotalItemQty.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        Hrfv10.Enabled = false;
                        Hrev10.Enabled = false;

                    }

                    lblItemQty.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    rfv10.Enabled = true;
                    rev10.Enabled = true;
                    txtTotalItemQty.Visible = true;
                    foreach (GridViewRow gvRow in GridView2.Rows)
                    {
                        if (GridView2.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView2.SelectedIndex = gvRow.DataItemIndex;
                            GridView2.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }
            if (e.CommandName == "RecordReset")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {

                    lblMsg.Text = string.Empty;
                    lblMappingMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblTotalItemQty");
                    TextBox txtTotalItemQty = (TextBox)row.FindControl("txtTotalItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    RequiredFieldValidator rfv10 = row.FindControl("rfv10") as RequiredFieldValidator;
                    RegularExpressionValidator rev10 = row.FindControl("rev10") as RegularExpressionValidator;

                    lblItemQty.Visible = true;

                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    rfv10.Enabled = false;
                    rev10.Enabled = false;
                    txtTotalItemQty.Visible = false;

                    GridView2.SelectedIndex = -1;

                }

            }
            if (e.CommandName == "RecordUpdate")
            {

                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    lblMsg.Text = string.Empty;
                    lblMappingMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblTotalItemQty");
                    TextBox txtTotalItemQty = (TextBox)row.FindControl("txtTotalItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");



                    ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                                new string[] { "flag", "CitizenItemMappingId", "Total_ItemQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "11", e.CommandArgument.ToString(), txtTotalItemQty.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Advanced Card Mapping " }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMappingMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetCitizenItemMappingDetails();
                        GridView2.SelectedIndex = -1;
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }


            }
            if (e.CommandName == "MappingRecordDel")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMappingMsg.Text = string.Empty;
                    ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                                new string[] { "flag", "CitizenItemMappingId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "10", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Citizen Item Mapping registration record deleted" }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = string.Empty;
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetCitizenItemMappingDetails();
                        lblMappingMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMappingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    #endregion========================================================


}