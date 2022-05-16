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

public partial class mis_DemandSupply_CrateMgmtAtDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetLocationSearch();
                GetShift();
              //  GetItemCategory();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //txtIssueCrate.Attributes.Add("readonly", "true");
                txtShortExcess.Attributes.Add("readonly", "true");
                txtSerchDate.Attributes.Add("readonly", "true");
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
    private void Clear()
    {
        txtIssueCrate.Text = string.Empty;
        txtReturnCrate.Text = string.Empty;
        txtShortExcess.Text = string.Empty;

        txtChallano.Text = string.Empty;
        txtCrateRemark.Text = string.Empty;
        btnSubmit.Text = "Save";
        GridView1.SelectedIndex = -1;
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
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));

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
            ddlLocationSearch.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Search Location:  ", ex.Message.ToString());
        }
    }

    private void GetRoute()
    {
        try
        {

            ddlRoute.Items.Clear();
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                     new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
    }
    private void GetRouteSerch()
    {
        try
        {

            ddlRouteSearch.Items.Clear();
            ddlRouteSearch.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                  new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "7", objdb.Office_ID(), ddlLocationSearch.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            ddlRouteSearch.DataTextField = "RName";
            ddlRouteSearch.DataValueField = "RouteId";
            ddlRouteSearch.DataBind();
            ddlRouteSearch.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
    }
    private void GetDisOrSSByRouteID()
    {
        try
        {
           
              ds4 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                   new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue, objdb.GetMilkCatId() }, "dataset");

            if(ds4!=null && ds4.Tables[0].Rows.Count>0)
            {
                ddlDitributor.DataTextField = "DTName";
                ddlDitributor.DataValueField = "DistributorId";
                ddlDitributor.DataSource = ds4.Tables[0];
                ddlDitributor.DataBind();
               
            }
            else
            {
                ddlDitributor.Items.Insert(0, new ListItem("No Record Found", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    private void GetCrateMgmtDetails()
    {
        try
        {
            lblMsg.Text = "";
            DateTime dateSearch = DateTime.ParseExact(txtSerchDate.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                     new string[] { "flag", "IRDate", "AreaId", "RouteId", "Office_ID" },
                     new string[] { "0", datSearch, ddlLocationSearch.SelectedValue, ddlRouteSearch.SelectedValue, objdb.Office_ID() }, "dataset");

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
    //protected void GetItemCategory()
    //{
    //    try
    //    {
    //        ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
    //              new string[] { "flag" },
    //             new string[] { "1" }, "dataset");

    //        if (ds1.Tables[0].Rows.Count != 0)
    //        {
    //            ddlItemCategory.DataTextField = "ItemCatName";
    //            ddlItemCategory.DataValueField = "ItemCat_id";
    //            ddlItemCategory.DataSource = ds1.Tables[0];
    //            ddlItemCategory.DataBind();
    //            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();

    //            ddlItemCat.DataTextField = "ItemCatName";
    //            ddlItemCat.DataValueField = "ItemCat_id";
    //            ddlItemCat.DataSource = ds1.Tables[0];
    //            ddlItemCat.DataBind();
    //            ddlItemCat.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlItemCat.SelectedValue = objdb.GetMilkCatId();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    private void InsertorUpdateCrateMgmt()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = "";
                    DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                    string dat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                            new string[] { "flag", "IRDate", "ShiftId","AreaId","RouteId","IssueCrate","ReturnCrate","ShortExcessCrate"
                                ,"ChallanNo","Office_ID", "CreatedBy", "CreatedByIP","DistributorId","ItemCat_id","CrateRemark" },
                            new string[] { "1",dat ,ddlShift.SelectedValue,ddlLocation.SelectedValue,ddlRoute.SelectedValue,
                                txtIssueCrate.Text.Trim(),txtReturnCrate.Text.Trim(),txtShortExcess.Text.Trim()
                                ,txtChallano.Text.Trim(),objdb.Office_ID(), objdb.createdBy(),
                                            objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                                            ddlDitributor.SelectedValue,objdb.GetMilkCatId(),txtCrateRemark.Text.Trim() }, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            // GetCrateMgmtDetails();
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Record " + error + " exists for Date : " + txtDate.Text + ", Shift : " + ddlShift.SelectedItem.Text + " and Route : " + ddlRoute.SelectedItem.Text);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }

                    }
                    if (btnSubmit.Text == "Update")
                    {
                        lblMsg.Text = "";
                        ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                            new string[] { "flag", "MilkCrateMgmtId", "IRDate", "ShiftId","AreaId","RouteId","IssueCrate","ReturnCrate","ShortExcessCrate"
                                ,"ChallanNo", "Office_ID", "CreatedBy", "CreatedByIP", "PageName", "Remark","DistributorId" ,"ItemCat_id","CrateRemark"},
                            new string[] { "2", ViewState["rowid"].ToString(),dat ,ddlShift.SelectedValue,ddlLocation.SelectedValue,ddlRoute.SelectedValue,
                                txtIssueCrate.Text.Trim(),txtReturnCrate.Text.Trim(),txtShortExcess.Text.Trim()
                                ,txtChallano.Text.Trim(),objdb.Office_ID(), objdb.createdBy()
                                , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Crate Management Details Updated" ,
                                    ddlDitributor.SelectedValue,objdb.GetMilkCatId(),txtCrateRemark.Text.Trim()}, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            // GetCrateMgmtDetails();
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetCrateMgmtDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Record " + error + " exists for Date : " + txtDate.Text + ", Shift : " + ddlShift.SelectedItem.Text + " and Route : " + ddlRoute.SelectedItem.Text);
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
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Date");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

            finally
            {
                if (ds2 != null) { ds2.Dispose(); }
            }
        }
    }
    #endregion========================================================
    #region=============== changed event for controls =================
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            GetRoute();
            // GetDatatableHeaderDesign();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblAreaID = (Label)row.FindControl("lblAreaID");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblShiftId = (Label)row.FindControl("lblShiftId");
                    Label lblIRDate = (Label)row.FindControl("lblIRDate");
                    Label lblIssueCrate = (Label)row.FindControl("lblIssueCrate");
                    Label lblReturnCrate = (Label)row.FindControl("lblReturnCrate");
                    Label lblShortExcess = (Label)row.FindControl("lblShortExcess");
                    Label lblChallanNo = (Label)row.FindControl("lblChallanNo");
                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                   // Label lblCatId = (Label)row.FindControl("lblCatId");
                    Label lblCrateRemark = (Label)row.FindControl("lblCrateRemark");

                    txtDate.Text = lblIRDate.Text;
                    ddlShift.SelectedValue = lblShiftId.Text;
                    ddlLocation.SelectedValue = lblAreaID.Text;
                    //if(lblCatId.Text!="")
                    //{
                    //    ddlItemCategory.SelectedValue = lblCatId.Text;
                    //}
                    //else
                    //{
                    //    ddlItemCategory.SelectedValue = objdb.GetMilkCatId();
                    //}
                    GetRoute();
                    ddlRoute.SelectedValue = lblRouteId.Text;
                    if (lblDistributorId.Text != "")
                    {
                        GetDisOrSSByRouteID();
                        ddlDitributor.SelectedValue = lblDistributorId.Text;
                    }

                    txtChallano.Text = lblChallanNo.Text;
                    txtIssueCrate.Text = lblIssueCrate.Text;
                    txtReturnCrate.Text = lblReturnCrate.Text;
                    txtShortExcess.Text = lblShortExcess.Text;
                    txtCrateRemark.Text = lblCrateRemark.Text;
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
                    //  GetDatatableHeaderDesign();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion============ end of changed event for controls===========

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateCrateMgmt();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtDate.Text = string.Empty;
        Clear();
       // ddlItemCategory.SelectedValue = "0";
        ddlLocation.SelectedIndex = 0;

        ddlShift.SelectedIndex = 0;
        // GetDatatableHeaderDesign();
        GetRoute();
        GetDisOrSSByRouteID();
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLocation.SelectedValue != "0" && ddlRoute.SelectedValue != "0" && ddlShift.SelectedValue != "0" && txtDate.Text.Trim() != "")
            {
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string dat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds3 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
                            new string[] { "Flag", "Delivary_Date", "DelivaryShift_id", "AreaId", "RouteId", "Office_ID" },
                            new string[] { "4", dat, ddlShift.SelectedValue, ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID() }, "dataset");

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    txtIssueCrate.Text = ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString();
                    txtShortExcess.Text = ds3.Tables[0].Rows[0]["TotalIssueCrate"].ToString();
                    txtReturnCrate.Text = string.Empty;
                   // txtShortExcess.Text = string.Empty;
                   // txtIssueCrate.Attributes.Add("readonly", "true");
                }
                else
                {
                    txtIssueCrate.Text = "0";
                    txtReturnCrate.Text = string.Empty;
                    txtShortExcess.Text = "0";
                   // txtIssueCrate.Attributes.Remove("readonly");
                }
                GetDisOrSSByRouteID();
                // GetDatatableHeaderDesign();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCrateMgmtDetails();
        }
    }
    protected void ddlLocationSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlLocationSearch.SelectedValue!="0")
        { 
        GetRouteSerch();
        GetDatatableHeaderDesign();
        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlLocationSearch.SelectedIndex = 0;
        txtSerchDate.Text = string.Empty;
        ddlRouteSearch.Items.Insert(0, new ListItem("All", "0"));
        GetRouteSerch();
        GridView1.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLocation.SelectedValue != "0")
    //    {
    //        GetRoute();
    //        // GetDatatableHeaderDesign();
    //    }
    //}
    //protected void ddlItemCat_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //    GetRouteSerch();

    //}
}