using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;


public partial class mis_DistOrStockistParlourMapping : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    string bid="", oid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetType();
                DistOrStockistRetailerMappingDetails();
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

    protected void GetType()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                  new string[] { "flag" },
                 new string[] { "5" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDSType.DataTextField = "OfficeTypeName";
                ddlDSType.DataValueField = "OfficeType_ID";
                ddlDSType.DataSource = ds;
                ddlDSType.DataBind();
                ddlDSType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDSType.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
        }
    }


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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
    }


    protected void GetSubDistributor()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_SubDistributorReg",
                      new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSubDist.DataTextField = "SDTName";
                ddlSubDist.DataValueField = "SubDistributorId";
                ddlSubDist.DataSource = ds;
                ddlSubDist.DataBind();
                ddlSubDist.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSubDist.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3:", ex.Message.ToString());
        }
    }
    private void GetRetailerType()
    {
        try
        {
            ddlRetailerType.DataSource = objdb.ByProcedure("USP_Mst_RetailerType",
                     new string[] { "flag" },
                     new string[] { "3", objdb.Office_ID() }, "dataset");
            ddlRetailerType.DataTextField = "RetailerTypeName";
            ddlRetailerType.DataValueField = "RetailerTypeID";
            ddlRetailerType.DataBind();
            ddlRetailerType.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4:", ex.Message.ToString());
        }
    }
    protected void GetRetailer()
    {
        try
        {
            if(ddlRetailerType.SelectedValue!="3" && ddlRetailerType.SelectedValue!="0")
            {
                ds = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "RetailerTypeID", "Office_ID" },
                      new string[] { "11", ddlRetailerType.SelectedValue, objdb.Office_ID() }, "dataset");
            }
            else if (ddlRetailerType.SelectedValue == "3")
            {
                ds = objdb.ByProcedure("USP_Mst_Organization",
                   new string[] { "flag", "RetailerTypeID", "Office_ID" },
                     new string[] { "7", ddlRetailerType.SelectedValue, objdb.Office_ID() }, "dataset");
            }
           else
            {
                ds = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "Office_ID" },
                      new string[] { "1", objdb.Office_ID() }, "dataset");
            }

            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ddlRetailerType.SelectedValue != "3" && ddlRetailerType.SelectedValue != "0")
                {
                    ddlRetailer.DataTextField = "BoothName";
                    ddlRetailer.DataValueField = "BoothId";
                }
               else if (ddlRetailerType.SelectedValue == "3")
                {
                    ddlRetailer.DataTextField = "InstName";
                    ddlRetailer.DataValueField = "OrganizationId";
                }
                else
                {
                     ddlRetailer.DataTextField = "BoothName";
                    ddlRetailer.DataValueField = "BoothId";
                }
               
                ddlRetailer.DataSource = ds.Tables[0];
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5:", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    protected void DistOrStockistRetailerMappingDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                        new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6:", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        ddlDSType.SelectedIndex = -1;
        ddlRetailer.SelectedIndex = -1;

        ddlDist.SelectedIndex = -1;
        ddlSubDist.SelectedIndex = -1;
        panelDist.Visible = false;
        panelSubDist.Visible = false;
        GetDatatableHeaderDesign();
        
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
        bid=""; oid = "";
        ddlRetailerType.SelectedIndex = 0;
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : 7 " + ex.Message.ToString());
        }
    }
    private void InsertorUpdateVehicleRouteMapping()
    {
        lblMsg.Text = "";
        
      
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (ddlRetailerType.SelectedValue != "3")
                    {
                        bid = ddlRetailer.SelectedValue;
                        oid = "";
                    }
                    else
                    {
                        bid = "";
                        oid = ddlRetailer.SelectedValue;
                    }
                    if (btnSubmit.Text == "Save")
                    {
                        ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                            new string[] { "flag", "OfficeType_ID", "DistributorId", "SubDistributorId", "BoothId", "Office_ID", "CreatedBy", "CreatedBy_IP", "RetailerTypeID", "OrganizationId" },
                            new string[] { "2", ddlDSType.SelectedValue, ddlDist.SelectedValue, ddlSubDist.SelectedValue, bid, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), ddlRetailerType.SelectedValue, oid }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {   
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            DistOrStockistRetailerMappingDetails();
                            GetDatatableHeaderDesign();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                           
                            
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Retailer "+ ddlRetailer.SelectedItem.Text +" "+error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 8:" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                             new string[] { "flag", "DistOrSubDistParlour_Id", "OfficeType_ID", "DistributorId", "SubDistributorId", "BoothId", "Office_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark", "RetailerTypeID", "OrganizationId" },
                             new string[] { "3",ViewState["rowid"].ToString(), ddlDSType.SelectedValue, ddlDist.SelectedValue, ddlSubDist.SelectedValue, bid, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Distributor/Superstockist Details Updated",ddlRetailerType.SelectedValue,oid}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            DistOrStockistRetailerMappingDetails();
                            GetDatatableHeaderDesign();
                            Clear();
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Retailer " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 9:" + error);
                            }
                        }
                        ds.Clear();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter ");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 10:", ex.Message.ToString());
            }
        }
    }
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================
    protected void ddlRetailerType_Init(object sender, EventArgs e)
    {
        GetRetailerType();
    }
    protected void ddlDist_Init(object sender, EventArgs e)
    {
        GetDistributor();
    }
    protected void ddlSubDist_Init(object sender, EventArgs e)
    {
        GetSubDistributor();
    }

    //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlType.SelectedValue != "0")
    //    {
    //        if (ddlType.SelectedValue == "8")
    //        {

    //            panelDist.Visible = true;
    //            panelSubDist.Visible = false;

    //        }
    //        else
    //        {

    //            if (ddlType.SelectedValue == "9")
    //                panelDist.Visible = false;
    //            panelSubDist.Visible = true;

    //        }

    //    }
    //    else
    //    {
    //        panelDist.Visible = false;
    //        panelSubDist.Visible = false;
    //    }

    //}
    #endregion============ end of changed event for controls===========

    #region============ button click  ============================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        InsertorUpdateVehicleRouteMapping();
    }

    #endregion=============end of button click function==================


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


                    Label lblOfficeType_ID = (Label)row.FindControl("lblOfficeType_ID");

                    Label lblDistributorId = (Label)row.FindControl("lblDistributorId");
                    Label lblSubDistributorId = (Label)row.FindControl("lblSubDistributorId");
                    Label lblBoothId = (Label)row.FindControl("lblBoothId");
                    Label lblOrganizationId = (Label)row.FindControl("lblOrganizationId");
                    Label lblRetailerTypeID = (Label)row.FindControl("lblRetailerTypeID");
                    //Label lblOfficeType_Title = (Label)row.FindControl("lblOfficeType_Title");

                    ddlDSType.SelectedValue = lblOfficeType_ID.Text;
                    if (lblRetailerTypeID.Text=="")
                    {
                        ddlRetailerType.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlRetailerType.SelectedValue = lblRetailerTypeID.Text;
                    }
                    if (lblRetailerTypeID.Text != "3" && lblRetailerTypeID.Text != "")
                    {
                        GetRetailer();
                        ddlRetailer.SelectedValue = lblBoothId.Text;
                    }
                    else if (lblRetailerTypeID.Text =="3")
                    {
                        GetRetailer();
                        ddlRetailer.SelectedValue = lblOrganizationId.Text;
                    }
                    else
                    {
                        GetRetailer();
                    }
                    GetDatatableHeaderDesign();
                    if (lblOfficeType_ID.Text == "8")
                    {
                        panelDist.Visible = true;
                        panelSubDist.Visible = false;
                        ddlDist.SelectedValue = lblDistributorId.Text;
                        ddlSubDist.SelectedIndex = 0;
                    }
                    else
                    {
                        panelDist.Visible = false;
                        panelSubDist.Visible = true;
                        ddlSubDist.SelectedValue = lblSubDistributorId.Text;
                        ddlDist.SelectedIndex = 0;
                    }
                    
                    //ViewState["VehicleRouteMapping_ID"] = lblVehicleRouteMappingID.Text;
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
            if (e.CommandName == "RecordDelete")
            {
                lblMsg.Text = string.Empty;
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    //ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                                new string[] { "flag", "DistOrSubDistParlour_Id", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Dept. Designation Mapping Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        DistOrStockistRetailerMappingDetails();
                        GetDatatableHeaderDesign();
                        Clear();
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 11:" + error);
                    }
                    ds.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  12: " + ex.Message.ToString());
        }
    }

    //protected void ddlType_Init(object sender, EventArgs e)
    //{
    //    GetType();
    //}
    protected void ddlDSType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDatatableHeaderDesign();
        if (ddlDSType.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            if (ddlDSType.SelectedValue == "8")
            {
                panelDist.Visible = true;
                panelSubDist.Visible = false;
                ddlSubDist.SelectedIndex = 0;


            }
            else
            {
                panelDist.Visible = false;
                panelSubDist.Visible = true;
                ddlDist.SelectedIndex = 0;
            }

        }
 

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    protected void ddlRetailerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRetailerType.SelectedValue != "0")
        {
            GetRetailer();
        }
    }
}