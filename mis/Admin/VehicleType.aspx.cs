using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Common_VehicleType : System.Web.UI.Page
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
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("Sp_tblVehicleType",
                             new string[] { "flag" },
                             new string[] { "0" }, "dataset");
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertOrUpdateVehicleType()
    {
        string IsActive = "1";
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("Sp_tblVehicleType",
                            new string[] { "flag", "VehicleType_Name", "ContainerType_Name", "ContainerType_Id", "IsActive", "Office_ID" },
                            new string[] { "1", txtVehicleTypeName.Text, RblContainerTypeName.SelectedItem.Text, RblContainerTypeName.SelectedValue, IsActive, objdb.Office_ID(), }, "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Clear();
                            FillGrid();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            FillGrid();
                        }
                        
                        ds.Clear();
                    }
                    else if (btnSubmit.Text == "Update" && ViewState["VehicleType_ID"].ToString() != "0")
                      {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("Sp_tblVehicleType",
                            new string[] { "flag", "VehicleType_ID", "VehicleType_Name", "ContainerType_Name", "ContainerType_Id", "Office_ID" },
                            new string[] { "2", ViewState["VehicleType_ID"].ToString(), txtVehicleTypeName.Text, RblContainerTypeName.SelectedItem.Text, RblContainerTypeName.SelectedValue, objdb.Office_ID(), "Vehicle Type Record Updated" }, "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                             Clear();
                            FillGrid();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            FillGrid();
                        }
                       }
                        ds.Clear();
                }
                     else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Something wents wrong!");
                    }
                
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }

    }
    protected void Clear()
    {
        txtVehicleTypeName.Text = "";
        RblContainerTypeName.ClearSelection();
        ViewState["VehicleType_ID"] = "0";
        btnSubmit.Text = "Save";
    }
    #endregion====================================end of user defined function==========

    #region============ button click and gridview events event ============================
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrUpdateVehicleType();
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
                    Label lblVehicleTypeID = (Label)row.FindControl("lblVehicleTypeID");
                    Label lblVehicleTypeName = (Label)row.FindControl("lblVehicleTypeName");
                    Label lblContainerTypeName = (Label)row.FindControl("lblContainerTypeName");
                    Label lblContainerTypeId = (Label)row.FindControl("lblContainerTypeId");
                    txtVehicleTypeName.Text = lblVehicleTypeName.Text;
                    RblContainerTypeName.SelectedValue = lblContainerTypeName.Text;
                    RblContainerTypeName.SelectedValue = lblContainerTypeId.Text;
                    ViewState["VehicleType_ID"] = lblVehicleTypeID.Text;
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
                    string IsActive = "0";
                    lblMsg.Text = string.Empty;
                    ViewState["rowid"] = e.CommandArgument;
                    ds = objdb.ByProcedure("Sp_tblVehicleType",
                                new string[] { "flag", "VehicleType_ID", "IsActive" },
                                new string[] { "4", ViewState["rowid"].ToString(), IsActive, "Dept. Designation Mapping Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        FillGrid();
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
    #endregion=============end of button click function==================

}