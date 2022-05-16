using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class mis_Masters_Mst_VehicleMilkOrProduct : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds,ds1,ds2=new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetVehicleMasterDetails();
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
    #region=======================user defined function========================
    protected void GetVendorType()
    {
        try
        {
            ddlVendorType.DataTextField = "VendorTypeName";
            ddlVendorType.DataValueField = "VendorTypeId";
            ddlVendorType.DataSource = objdb.ByProcedure("USP_Mst_VendorType",
                           new string[] { "flag" },
                           new string[] { "1" }, "dataset");
            ddlVendorType.DataBind();
            ddlVendorType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetName()
    {
        try
        {
            if(ddlVendorType.SelectedValue!="0")
            {
                ddlVendorName.DataTextField = "Contact_Person";
                ddlVendorName.DataValueField = "TransporterId";
                ddlVendorName.DataSource = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                               new string[] { "flag","Office_ID","VendorTypeId" },
                               new string[] { "7",objdb.Office_ID(),ddlVendorType.SelectedValue }, "dataset");
                ddlVendorName.DataBind();
                ddlVendorName.Items.Insert(0, new ListItem("Select", "0"));
           }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    private void Clear()
    {

        txtVehicleNo.Text = string.Empty;
        txtVehicleType.Text = string.Empty;
        ddlVendorType.SelectedIndex = 0;

        btnSave.Text = "Save";
        GridView1.SelectedIndex = -1;
    }
    private void GetVehicleMasterDetails()
    {
        try
        {
            GridView1.DataSource = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                    new string[] { "flag", "Office_ID" },
                    new string[] { "1", objdb.Office_ID() }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateVehicleMaster()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string isactive = "";
                    if(chkIsActive.Checked==true)
                    {
                        isactive = "1";
                    }
                    else
                    {
                        isactive = "0";
                    }
                    if (btnSave.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds2 = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                            new string[] { "flag", "VendorTypeId", "TransporterId", "VehicleType", "VehicleNo", "IsActive", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "2",ddlVendorType.SelectedValue,ddlVendorName.SelectedValue,txtVehicleType.Text.Trim(), txtVehicleNo.Text.Trim(),isactive, objdb.Office_ID(), objdb.createdBy(),
                                            objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            GetVehicleMasterDetails();
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle Number " + txtVehicleNo.Text + " " + error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }
                    if (btnSave.Text == "Update")
                    {
                        lblMsg.Text = "";

                        ds2 = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                           new string[] { "flag","VehicleMilkOrProduct_ID", "VendorTypeId", "TransporterId","VehicleType","VehicleNo","IsActive", "Office_ID", "CreatedBy"
                                , "CreatedByIP", "PageName", "Remark" },
                           new string[] { "3",ViewState["rowid"].ToString(),ddlVendorType.SelectedValue,ddlVendorName.SelectedValue,txtVehicleType.Text.Trim(), txtVehicleNo.Text.Trim(),isactive, objdb.Office_ID(), objdb.createdBy(),
                                            objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Vehicle Master Details Updated" }, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetVehicleMasterDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle Number " + txtVehicleNo.Text + " " + error);
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
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Vendor Type");
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
    #endregion====================================end of user defined function

    #region=============== changed event for controls =================
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
                    Label lblVendorTypeId = (Label)row.FindControl("lblVendorTypeId");
                    Label lblVehicleNo = (Label)row.FindControl("lblVehicleNo");
                    Label lblVehicleType = (Label)row.FindControl("lblVehicleType");
                    Label lblTransporterId = (Label)row.FindControl("lblTransporterId");


                    ddlVendorType.SelectedValue = lblVendorTypeId.Text;
                    GetName();
                    if (lblTransporterId.Text=="")
                    {
                        ddlVendorName.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlVendorName.SelectedValue = lblTransporterId.Text;
                    }
                    
                    txtVehicleNo.Text = lblVehicleNo.Text;
                    txtVehicleType.Text=lblVehicleType.Text;
                    ViewState["rowid"] = e.CommandArgument;
                    btnSave.Text = "Update";

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
    #endregion============ end of changed event for controls===========

    #region============ init,selectedindexchanged , button click event ============================
    protected void ddlVendorType_Init(object sender, EventArgs e)
    {
        GetVendorType();
    }
    protected void ddlVendorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetName();
        GetDatatableHeaderDesign();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateVehicleMaster();
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        GetDatatableHeaderDesign();
    }
    #endregion=============end of button click funciton==================


    
}