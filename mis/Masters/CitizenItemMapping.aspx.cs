using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CitizenItemMapping : System.Web.UI.Page
{


    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                CitizenItemMappingDetails();
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

    protected void GetItemCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                  new string[] { "flag" },
                 new string[] { "6" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

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

    protected void GetItem()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                       new string[] { "flag", "ItemCat_id" },
                       new string[] { "7", ddlItemCategory.SelectedValue}, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataSource = ds.Tables[0];
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
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


    protected void GetCitizen()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_AdvanceCard",
                      new string[] { "flag", "CreatedBy" },
                       new string[] { "1", objdb.createdBy() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlCitizen.DataTextField = "CitizenName";
                ddlCitizen.DataValueField = "CitizenId";
                ddlCitizen.DataSource = ds.Tables[0];
                ddlCitizen.DataBind();
                ddlCitizen.Items.Insert(0, new ListItem("Select", "0"));
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


    protected void CitizenItemMappingDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                        new string[] { "flag", "Office_ID","CreatedBy" },
                       new string[] { "1", objdb.Office_ID(),objdb.createdBy() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        ddlCitizen.SelectedIndex = -1;
        ddlItem.SelectedIndex = -1;
      
        ddlItemCategory.SelectedIndex = -1;

        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void InsertorUpdateCitizenItemMapping()
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
                        ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                            new string[] { "flag", "CitizenId", "ItemCat_id", "Item_id", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "2", ddlCitizen.SelectedValue, ddlItemCategory.SelectedValue, ddlItem.SelectedValue, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            CitizenItemMappingDetails();
                            Clear();
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }

                    }
                    if (btnSubmit.Text == "Update")
                    {
                        ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                             new string[] { "flag", "CitizenItemMappingId", "CitizenId", "ItemCat_id", "Item_id", "Office_ID", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                             new string[] { "3",ViewState["rowid"].ToString(), ddlCitizen.SelectedValue, ddlItemCategory.SelectedValue, ddlItem.SelectedValue, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                             Path.GetFileName(Request.Url.AbsolutePath), "Citizen Item Mapping Details Updated Successfully"}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            CitizenItemMappingDetails();
                            Clear();
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists.")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Bank Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    #endregion====================================end of user defined function

    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetItemCategory();
    }

    protected void ddlCitizen_Init(object sender, EventArgs e)
    {
        GetCitizen();
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItem();
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
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    Label lblCitizenId = (Label)row.FindControl("lblCitizenId");
                    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");

                    ddlCitizen.SelectedValue = lblCitizenId.Text;

                    ddlItemCategory.SelectedValue = lblItemCat_id.Text;
                    GetItem();
                    ddlItem.SelectedValue = lblItem_id.Text;




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
                }
            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("USP_Mst_CitizenItemMapping",
                                new string[] { "flag", "CitizenItemMappingId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), " Citizen Item Mapping Details Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        CitizenItemMappingDetails();
                        Clear();
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    ds.Clear();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

  

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateCitizenItemMapping();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
    }
    
   
}
