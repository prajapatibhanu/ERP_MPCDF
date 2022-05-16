using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class mis_DemandSupply_CrateReceiving : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                GetRoute();
                GetInstitution();
                GetCrateReceivingDetails();
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {

            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlRoute.DataTextField = "RNameOrNo";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
        }
    }
    private void GetInstitution()
    {
        try
        {

            ddlInstitution.DataSource = objdb.ByProcedure("USP_Mst_Organization",
                 new string[] { "flag", "Office_ID", "RouteId" },
                 new string[] { "8", objdb.Office_ID(), ddlRoute.SelectedValue }, "dataset");
            ddlInstitution.DataTextField = "InstName";
            ddlInstitution.DataValueField = "OrganizationId";
            ddlInstitution.DataBind();
            ddlInstitution.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
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
                ddlShift.DataSource = ds;
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void Clear()
    {
        txtRemark.Text = string.Empty;
      //  txtNotReceived.Text = string.Empty;
        txttotalcratebroken.Text = string.Empty;
        txttotalcratemissing.Text = string.Empty;
        txttotalcratereceived.Text = string.Empty;
        btnSubmit.Text = "Save";
        GridView1.SelectedIndex = -1;

    }
    private void GetCrateReceivingDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_tblSpItemStockCrateMgmt",
                                   new string[] { "flag", "Office_Id" },
                              new string[] {"0", objdb.Office_ID() }, "TableSave");

            if(ds2.Tables[0].Rows.Count>0)
            {
                GridView1.DataSource = ds2.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void InsertorUpdate()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                string dat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string orgid = "";
                if (ddlType.SelectedValue == "1")
                {
                    orgid = ddlInstitution.SelectedValue;
                }
                else
                {
                    orgid = "";
                }

                if (btnSubmit.Text == "Save")
                {
                   

                    ds1 = objdb.ByProcedure("USP_tblSpItemStockCrateMgmt",
                                new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", 
                            "Cr", "Dr","TransactionID","Remark","CreatedBy","TranDt","Office_Id","ipaddress","Shift_id"
                            , "missing_crate","broken_crate","not_received_crate","mpitemcat_id","TransactionFrom","OrganizationId" },
                                new string[] { "1", objdb.GetCrateItemCat_Id(),objdb.GetCrateType_Id(),objdb.GetCrateItem_Id()
                            ,txttotalcratereceived.Text.Trim(),"0",ddlRoute.SelectedValue,txtRemark.Text.Trim(), objdb.createdBy(), dat.ToString(),
                           objdb.Office_ID(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),ddlShift.SelectedValue
                        ,txttotalcratemissing.Text.Trim(),txttotalcratebroken.Text.Trim(),"0"
                        ,ddlItemCategory.SelectedValue,"Crate Receiving",orgid}, "TableSave");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            
                            GetCrateReceivingDetails();
                            Clear();
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  6:" + error);

                        }
                    }
                }
                if (btnSubmit.Text == "Update")
                {


                    ds1 = objdb.ByProcedure("USP_tblSpItemStockCrateMgmt",
                                new string[] { "flag","ItmStock_id", 
                            "Cr", "Dr","TransactionID","Remark","TranDt","Shift_id","ipaddress"
                            , "missing_crate","broken_crate","not_received_crate","mpitemcat_id","OrganizationId"
                            ,"PageName","URemark","CreatedBy" },
                                new string[] { "2",ViewState["rowid"].ToString(),txttotalcratereceived.Text.Trim(),"0",ddlRoute.SelectedValue
                        ,txtRemark.Text.Trim(), dat.ToString(), ddlShift.SelectedValue,objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                        ,txttotalcratemissing.Text.Trim(),txttotalcratebroken.Text.Trim(),"0"
                        ,ddlItemCategory.SelectedValue,orgid,Path.GetFileName(Request.Url.AbsolutePath)
                        , "Crate Registration Details Updated",objdb.createdBy()}, "TableSave");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                            GetCrateReceivingDetails();
                            Clear();
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  6:" + error);

                        }
                    }
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlRoute.SelectedValue!="0")
        {
            GetInstitution();
            GetDatatableHeaderDesign();
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue != "0" && ddlRoute.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
           
            if (ddlType.SelectedValue == "1")
            {

                pnlinst.Visible = true;
                GetInstitution();
            }
            else
            {
                pnlinst.Visible = false;
                ddlInstitution.SelectedIndex = 0;

            }
        }
        else
        {
            pnlinst.Visible = false;
            ddlInstitution.SelectedIndex = 0;
         //   lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "First Select Route");

        }
        GetDatatableHeaderDesign();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertorUpdate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        txtDate.Text = string.Empty;
        ddlInstitution.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        lblMsg.Text = string.Empty;
        btnSubmit.Text = "Save";
        GridView1.SelectedIndex = -1;
        GetDatatableHeaderDesign();
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

                     Label lblShiftid = (Label)row.FindControl("lblShiftid");
                     Label lblRouteID = (Label)row.FindControl("lblRouteID");
                     Label lblmpitemcatid = (Label)row.FindControl("lblmpitemcatid");
                     Label lblOrganizationId = (Label)row.FindControl("lblOrganizationId");
                     Label lblTranDt = (Label)row.FindControl("lblTranDt");
                     Label lblReceivedCrate = (Label)row.FindControl("lblReceivedCrate");
                     Label lblbrokencrate = (Label)row.FindControl("lblbrokencrate");
                     Label lblmissingcrate = (Label)row.FindControl("lblmissingcrate");
                     Label lblnoteceivedcrate = (Label)row.FindControl("lblnoteceivedcrate");
                     Label lblRemark = (Label)row.FindControl("lblRemark");


                     ddlRoute.SelectedValue = lblRouteID.Text;
                     if(lblOrganizationId.Text=="" || lblOrganizationId.Text==null)
                     {
                         ddlType.SelectedValue = "2";
                         pnlinst.Visible = false;
                     }
                     else
                     {
                         ddlType.SelectedValue = "1";
                         pnlinst.Visible = true;
                         GetInstitution();
                         ddlInstitution.SelectedValue = lblOrganizationId.Text;
                     }
                     txtDate.Text = lblTranDt.Text;
                     ddlItemCategory.SelectedValue = lblmpitemcatid.Text;
                     ddlShift.SelectedValue = lblShiftid.Text;
                   
                     txttotalcratereceived.Text = lblReceivedCrate.Text;
                     txttotalcratemissing.Text = lblmissingcrate.Text;
                     txttotalcratebroken.Text = lblbrokencrate.Text;
                   //  txtNotReceived.Text = lblnoteceivedcrate.Text;
                     txtRemark.Text = lblRemark.Text;
                     btnSubmit.Text = "Update";

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
                     GetDatatableHeaderDesign();
                 }
             }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 9 ", ex.Message.ToString());
        }
    }
   
}