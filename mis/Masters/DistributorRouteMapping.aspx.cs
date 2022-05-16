using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class mis_Masters_DistributorRouteMapping : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, dsr,ds3 ,ds4,dsd= new DataSet();
    string bid = "", oid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                GetSearchCategory();
                GetSearchLocation();
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

    protected void GetDistributor()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_DistributorReg",
                       new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDist.DataTextField = "DTName";
                ddlDist.DataValueField = "DistributorId";
                ddlDist.DataSource = ds;
                ddlDist.DataBind();
                ddlDist.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDist.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetSearchCategory()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds3.Tables[0].Rows.Count != 0)
            {
                ddlSearchItemCategory.DataTextField = "ItemCatName";
                ddlSearchItemCategory.DataValueField = "ItemCat_id";
                ddlSearchItemCategory.DataSource = ds3.Tables[0];
                ddlSearchItemCategory.DataBind();
                ddlSearchItemCategory.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlSearchItemCategory.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    protected void GetCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetSearchLocation()
    {
        try
        {
            ds4 = objdb.ByProcedure("USP_Mst_Area",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {
                ddlSearchLocation.DataTextField = "AreaName";
                ddlSearchLocation.DataValueField = "AreaId";
                ddlSearchLocation.DataSource = ds4.Tables[0];
                ddlSearchLocation.DataBind();
                ddlSearchLocation.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlLocation.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    protected void GetLocation()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_Area",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLocation.DataTextField = "AreaName";
                ddlLocation.DataValueField = "AreaId";
                ddlLocation.DataSource = ds.Tables[0];
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRoute()
    {
        try
        {
            ddlRoute.Items.Clear();
            dsr = objdb.ByProcedure("USP_Mst_Route",
                     new string[] { "flag", "Office_ID", "AreaId" },
                     new string[] { "6", objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");
            if (dsr.Tables[0].Rows.Count != 0)
            {
                ddlRoute.DataSource = dsr.Tables[0];
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
            }
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
        finally
        {
            if (dsr != null) { dsr.Dispose(); }
        }
    }
    //private void GetRoute()
    //{
    //    try
    //    {
    //        ddlRoute.Items.Clear();
    //        if (ddlItemCategory.SelectedValue != "0")
    //        {
    //            dsr = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
    //                         new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
    //                         new string[] { "7", objdb.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
    //            if (dsr.Tables[0].Rows.Count != 0)
    //            {
    //                ddlRoute.DataSource = dsr.Tables[0];
    //                ddlRoute.DataTextField = "RName";
    //                ddlRoute.DataValueField = "RouteId";
    //                ddlRoute.DataBind();
    //                ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //            else
    //            {
    //                ddlRoute.Items.Insert(0, new ListItem("No Record Found.", "0"));
    //            }
    //        }
    //        else
    //        {
    //            ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
    //        }
          
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (dsr != null) { dsr.Dispose(); }
    //    }
    //}
    protected void DistOrStockistRouteMappingDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                         new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                        new string[] { "1", objdb.Office_ID(),ddlSearchLocation.SelectedValue,ddlSearchItemCategory.SelectedValue }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4:", ex.Message.ToString());
        }
    }
    private void Clear()
    {


       // ddlDist.SelectedIndex = -1;
        GetDatatableHeaderDesign();

        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        bid = ""; oid = "";
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 5 " + ex.Message.ToString());
        }
    }
    private void InsertorUpdateRouteMapping()
    {
        lblMsg.Text = "";


        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        ds2 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                            new string[] { "flag", "DistributorId", "SubDistributorId", "RouteId", "Office_ID", "CreatedBy", "CreatedByIP", "AreaId", "ItemCat_id" },
                            new string[] { "2", ddlDist.SelectedValue, "", ddlRoute.SelectedValue, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), ddlLocation.SelectedValue,ddlItemCategory.SelectedValue }, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            DistOrStockistRouteMappingDetails();
                            GetDatatableHeaderDesign();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);


                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Record already exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error + ddlDist.SelectedItem.Text);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6:" + error);
                            }
                        }
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        ds2 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                            new string[] { "flag", "DistributorRouteMapping_Id", "DistributorId", "SubDistributorId", "RouteId", "Office_ID", "CreatedBy", "CreatedByIP", "AreaId", "ItemCat_id" },
                            new string[] { "3", ViewState["rowid"].ToString(), ddlDist.SelectedValue, "", ddlRoute.SelectedValue, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), ddlLocation.SelectedValue,ddlItemCategory.SelectedValue }, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            DistOrStockistRouteMappingDetails();
                            GetDatatableHeaderDesign();
                            Clear();
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Record already exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error + ddlDist.SelectedItem.Text);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7:" + error);
                            }
                        }

                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Location ");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 8:", ex.Message.ToString());
            }
            finally
            {
                if (ds2 != null) { ds2.Dispose(); }
            }
        }
    }
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================

    protected void ddlDist_Init(object sender, EventArgs e)
    {
        GetDistributor();
    }
    protected void ddlLocation_Init(object sender, EventArgs e)
    {
        GetLocation();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
        GetDatatableHeaderDesign();
    }

    #endregion============ end of changed event for controls===========

    #region============ button click  ============================
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


                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblAreaId = (Label)row.FindControl("lblAreaId");
                    Label lblRouteId = (Label)row.FindControl("lblRouteId");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");

                    ViewState["rowid"] = e.CommandArgument;
                    btnSubmit.Text = "Update";
                    ddlDist.SelectedValue = lblDistributorId.Text;
                    ddlLocation.SelectedValue = lblAreaId.Text;
                    GetRoute();
                    ddlRoute.SelectedValue = lblRouteId.Text;
                    if (lblItemCat_id.Text!="")
                    {
                        ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    }

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
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblIsActive = (Label)row.FindControl("lblIsActive");

                        string isactive = "";
                        if (lblIsActive.Text == "True")
                        {
                            isactive = "0";
                        }
                        else
                        {
                            isactive = "1";
                        }
                        lblMsg.Text = string.Empty;
                        dsd = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                                    new string[] { "flag", "DistributorRouteMapping_Id", "CreatedBy", "CreatedByIP", "IsActive" },
                                    new string[] { "10", e.CommandArgument.ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),isactive }, "TableSave");
                        

                        if (dsd.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = dsd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            DistOrStockistRouteMappingDetails();
                            GetDatatableHeaderDesign();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = dsd.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        dsd.Dispose();
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  12: " + ex.Message.ToString());
        }
        finally
        {
            if (dsd != null) { dsd.Dispose(); }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        InsertorUpdateRouteMapping();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        ddlLocation.SelectedIndex = 0;
       // ddlRoute.SelectedIndex = 0;
        ddlDist.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        btnSubmit.Text = "Save";
        GetDatatableHeaderDesign();
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DistOrStockistRouteMappingDetails();
        }
    }
    #endregion=============end of button click function==================

}