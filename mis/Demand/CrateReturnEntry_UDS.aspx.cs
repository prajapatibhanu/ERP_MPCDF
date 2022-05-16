using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_CrateReturnEntry_UDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, dsroute = new DataSet();
    int RCTotal = 0;

    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetCategory();
                GetCategorySearch();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtReturnDate.Text = Date;
                txtReturnDate.Attributes.Add("readonly", "readonly");
                txtSerchDate.Text = Date;
                txtSerchDate.Attributes.Add("readonly", "readonly");
                GetLocation();
                GetLocationSearch();
                GetRouteIdByLocationandCategory();
                GetRouteIdByLocationandCategory_Search();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
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
           // ddlItemCategory.SelectedValue = "2";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetCategorySearch()
    {
        try
        {
            ddlItemCategorySearch.DataTextField = "ItemCatName";
            ddlItemCategorySearch.DataValueField = "ItemCat_id";
            ddlItemCategorySearch.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategorySearch.DataBind();
            //ddlItemCategorySearch.SelectedValue = "2";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetLocationSearch()
    {
        try
        {
            ddlLocationSearch.DataTextField = "AreaName";
            ddlLocationSearch.DataValueField = "AreaId";
            ddlLocationSearch.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocationSearch.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetRouteIdByLocationandCategory()
    {
        try
        {
            //if (ddlLocation.SelectedValue == "2")
            //{
                //dsroute = objdb.ByProcedure("USP_Mst_RouteHeadandRouteMapping",
                // new string[] { "Flag", "AreaId", "ItemCat_id", "Office_ID" },
                //   new string[] { "5", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
                dsroute = objdb.ByProcedure("USP_Mst_RouteHeadandRouteMapping",
                 new string[] { "Flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "6", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
            //}
            //else
            //{
            //    //dsroute = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
            //    //    new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
            //    //    new string[] { "7", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            //    dsroute = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
            //        new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
            //        new string[] { "13", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");

            //}
            ddlRoute.Items.Clear();
            if (dsroute.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = dsroute.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (dsroute != null) { dsroute.Dispose(); }
        }
    }
    protected void GetRouteIdByLocationandCategory_Search()
    {
        try
        {

            if (ddlLocation.SelectedValue == "2")
            {
                //dsroute = objdb.ByProcedure("USP_Mst_RouteHeadandRouteMapping",
                // new string[] { "Flag", "AreaId", "ItemCat_id", "Office_ID" },
                //   new string[] { "5", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
                dsroute = objdb.ByProcedure("USP_Mst_RouteHeadandRouteMapping",
                 new string[] { "Flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "6", ddlLocation.SelectedValue, ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");
            }
            else
            {
                //dsroute = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                //    new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                //    new string[] { "7", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
                dsroute = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                    new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                    new string[] { "13", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");

            }
            ddlRouteSearch.Items.Clear();
            if (dsroute.Tables[0].Rows.Count > 0)
            {
                ddlRouteSearch.DataTextField = "RName";
                ddlRouteSearch.DataValueField = "RouteId";
                ddlRouteSearch.DataSource = dsroute.Tables[0];
                ddlRouteSearch.DataBind();
                ddlRouteSearch.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlRouteSearch.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (dsroute != null) { dsroute.Dispose(); }
        }
    }
    private void ClearText()
    {

        txtCrateRemark.Text = string.Empty;
        txtReturnCrate.Text = string.Empty;
        txtChallano.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        GridView2.SelectedIndex = -1;
        btnSubmit.Text = "Save";

    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRouteIdByLocationandCategory();
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            GetDatatableHeaderDesign();
        }
        else
        {
            GetDatatableHeaderDesign1();
        }

    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRouteIdByLocationandCategory();
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            GetDatatableHeaderDesign1();
        }
        else
        {
            GetDatatableHeaderDesign1();
        }
    }
    protected void ddlItemCategorySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRouteIdByLocationandCategory_Search();
        if (ddlItemCategorySearch.SelectedValue == objdb.GetMilkCatId())
        {
            GetDatatableHeaderDesign();
        }
        else
        {
            GetDatatableHeaderDesign1();
        }
    }
    protected void ddlLocationSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRouteIdByLocationandCategory_Search();
        if (ddlItemCategorySearch.SelectedValue == objdb.GetMilkCatId())
        {
            GetDatatableHeaderDesign();
        }
        else
        {
            GetDatatableHeaderDesign1();
        }
    }
    private void GetMilkCrateMgmtDetails()
    {
        try
        {
            lblMsg.Text = "";
            DateTime dateSearch = DateTime.ParseExact(txtSerchDate.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            ds1 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
               new string[] { "flag", "IRDate", "ItemCat_id", "AreaId", "RouteId", "Office_ID" },
               new string[] { "10", datSearch, ddlItemCategorySearch.SelectedValue, ddlLocationSearch.SelectedValue, ddlRouteSearch.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                GridView1.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void GetProductCrateMgmtDetails()
    {
        try
        {
            lblMsg.Text = "";
            DateTime dateSearch = DateTime.ParseExact(txtSerchDate.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_ProductCrateMgmt",
                     new string[] { "Flag", "IRDate", "ItemCat_id", "AreaId", "RouteId", "Office_ID" },
                     new string[] { "9", datSearch, ddlItemCategorySearch.SelectedValue, ddlLocationSearch.SelectedValue, ddlRouteSearch.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                GridView2.Visible = true;
                GridView2.DataSource = ds1.Tables[0];
                GridView2.DataBind();
                GetDatatableHeaderDesign1();
            }
            else
            {
                GridView2.Visible = false;
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
    private void GetDatatableHeaderDesign1()
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblReturnCrate = (e.Row.FindControl("lblReturnCrate") as Label);
                RCTotal += Convert.ToInt32(lblReturnCrate.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFTotalReturnCrate = (e.Row.FindControl("lblFTotalReturnCrate") as Label);

                lblFTotalReturnCrate.Text = RCTotal.ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblReturnCrate = (e.Row.FindControl("lblReturnCrate") as Label);
                RCTotal += Convert.ToInt32(lblReturnCrate.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFTotalReturnCrate = (e.Row.FindControl("lblFTotalReturnCrate") as Label);

                lblFTotalReturnCrate.Text = RCTotal.ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }

    #endregion========================================================





    #region=========== click event for grdiview row command event===========================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                    DateTime rdate = DateTime.ParseExact(txtReturnDate.Text, "dd/MM/yyyy", culture);
                    string rdat = rdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    if (btnSubmit.Text == "Save")
                    {
                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                        {

                            ds3 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                                new string[] { "flag", "IRDate", "ShiftId","AreaId","RouteId","ReturnCrate" 
                                        ,"ChallanNo","Office_ID", "CreatedBy", "CreatedByIP","ItemCat_id","CrateRemark" },
                                new string[] { "8",rdat,ddlShift.SelectedValue,ddlLocation.SelectedValue,ddlRoute.SelectedValue,txtReturnCrate.Text.Trim(),  
                                               txtChallano.Text.Trim(),objdb.Office_ID(), objdb.createdBy(),
                                           IPAddress + ":" + objdb.GetMACAddress(),ddlItemCategory.SelectedValue,txtCrateRemark.Text.Trim() }, "dataset");
                        }
                        else
                        {
                            ds3 = objdb.ByProcedure("USP_Trn_ProductCrateMgmt",
                           new string[] { "Flag", "IRDate", "ShiftId","ItemCat_id","AreaId","RouteId" ,"ReturnCrate","ChallanNo","CrateRemark"
                                ,"Office_ID", "CreatedBy", "CreatedByIP" },
                           new string[] { "7",rdat ,ddlShift.SelectedValue,ddlItemCategory.SelectedValue,ddlLocation.SelectedValue
                                ,ddlRoute.SelectedValue ,txtReturnCrate.Text.Trim(),txtChallano.Text.Trim()
                                ,txtCrateRemark.Text.Trim(),objdb.Office_ID(), objdb.createdBy(),IPAddress }, "dataset");
                        }
                        if (ds3 != null && ds3.Tables.Count > 0)
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());
                                    ClearText();
                                }
                                else if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString());
                                }
                                else
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                                }
                            }
                        }
                    }

                    else if (btnSubmit.Text == "Update")
                    {
                        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                        {
                            ds3 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                            new string[] { "flag", "MilkCrateMgmtId", "IRDate", "ShiftId","AreaId","RouteId","ReturnCrate"
                                ,"ChallanNo", "Office_ID", "CreatedBy", "CreatedByIP","ItemCat_id","CrateRemark"},
                            new string[] { "9", ViewState["rowid"].ToString(),rdat ,ddlShift.SelectedValue,ddlLocation.SelectedValue,ddlRoute.SelectedValue,
                               txtReturnCrate.Text.Trim(),txtChallano.Text.Trim(),objdb.Office_ID(), objdb.createdBy()
                                , IPAddress + ":" + objdb.GetMACAddress(),ddlItemCategory.SelectedValue,txtCrateRemark.Text.Trim()}, "dataset");
                        }
                        else
                        {
                            ds3 = objdb.ByProcedure("USP_Trn_ProductCrateMgmt",
                            new string[] { "Flag", "ProductCrateMgmtId", "IRDate", "ShiftId","AreaId","RouteId","ReturnCrate"
                                ,"ChallanNo","CrateRemark", "Office_ID", "CreatedBy", "CreatedByIP"},
                            new string[] { "8", ViewState["rowid"].ToString(),rdat,ddlShift.SelectedValue ,ddlLocation.SelectedValue,ddlRoute.SelectedValue
                                ,txtReturnCrate.Text.Trim(),txtChallano.Text.Trim(),txtCrateRemark.Text.Trim(),objdb.Office_ID(), objdb.createdBy()
                                , IPAddress }, "dataset");
                        }

                        if (ds3 != null && ds3.Tables.Count > 0)
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());
                                    ClearText();
                                    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                                    {
                                        GetMilkCrateMgmtDetails();
                                    }
                                    else
                                    {
                                        GetProductCrateMgmtDetails();
                                    }

                                }
                                else if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString());
                                }
                                else
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                                }
                            }
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "EditRecord")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblShiftId = (Label)row.FindControl("lblShiftId");
                    Label lblCatId = (Label)row.FindControl("lblCatId");
                    Label lblAreaID = (Label)row.FindControl("lblAreaID");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblIRDate = (Label)row.FindControl("lblIRDate");
                    Label lblReturnCrate = (Label)row.FindControl("lblReturnCrate");
                    Label lblChallanNo = (Label)row.FindControl("lblChallanNo");

                    Label lblCrateRemark = (Label)row.FindControl("lblCrateRemark");
                    ddlShift.SelectedValue = lblShiftId.Text;
                    ddlItemCategory.SelectedValue = lblCatId.Text;
                    txtReturnDate.Text = lblIRDate.Text;
                    ddlLocation.SelectedValue = lblAreaID.Text;
                    GetRouteIdByLocationandCategory();
                    ddlRoute.SelectedValue = lblRouteId.Text;
                    txtChallano.Text = lblChallanNo.Text;
                    txtReturnCrate.Text = lblReturnCrate.Text;
                    txtCrateRemark.Text = lblCrateRemark.Text;
                    ViewState["rowid"] = e.CommandArgument;


                    ViewState["rowid"] = e.CommandArgument;
                    btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblShiftId = (Label)row.FindControl("lblShiftId");
                    Label lblCatId = (Label)row.FindControl("lblCatId");
                    Label lblAreaID = (Label)row.FindControl("lblAreaID");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblIRDate = (Label)row.FindControl("lblIRDate");
                    Label lblReturnCrate = (Label)row.FindControl("lblReturnCrate");
                    Label lblChallanNo = (Label)row.FindControl("lblChallanNo");

                    Label lblCrateRemark = (Label)row.FindControl("lblCrateRemark");

                    txtReturnDate.Text = lblIRDate.Text;
                    ddlShift.SelectedValue = lblShiftId.Text;
                    ddlItemCategory.SelectedValue = lblCatId.Text;
                    ddlLocation.SelectedValue = lblAreaID.Text;
                    GetRouteIdByLocationandCategory();
                    ddlRoute.SelectedValue = lblRouteId.Text;
                    txtChallano.Text = lblChallanNo.Text;
                    txtReturnCrate.Text = lblReturnCrate.Text;
                    txtCrateRemark.Text = lblCrateRemark.Text;
                    ViewState["rowid"] = e.CommandArgument;



                    btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView2.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView2.SelectedIndex = gvRow.DataItemIndex;
                            GridView2.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign1();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        btnSubmit.Text = "Save";
        ClearText();
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            GetDatatableHeaderDesign1();
        }
        else
        {
            GetDatatableHeaderDesign1();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ddlItemCategorySearch.SelectedValue == objdb.GetMilkCatId())
            {
                GridView2.Visible = false;
                GetMilkCrateMgmtDetails();
            }
            else
            {
                GridView1.Visible = false;
                GetProductCrateMgmtDetails();
            }

        }
    }

    #endregion===========================
}