using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

 
public partial class mis_Masters_Tanker_Master : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();

    CommanddlFill objddl = new CommanddlFill();

    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {

                GetData();
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
    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblMsg.Text = "";
                int isactive = 1;
                if (txtSingleChamberCapacity.Text == "")
                {
                    txtSingleChamberCapacity.Text = "0";
                }
                if (txtDualChamberCapacity.Text == "")
                {
                    txtDualChamberCapacity.Text = "0";
                }
                if (chkIsActive.Checked)
                {
                    isactive = 1;
                }
                else
                {
                    isactive = 0;
                }

                if (btnSubmit.Text == "Save")
                {

                    ds = objdb.ByProcedure("Usp_TankerDetail", new string[] { "flag", "V_VehicleType"
				                                ,"V_VehicleNo"
				                                ,"I_OfficeID" 
				                                ,"I_OfficeTypeID" 
				                                ,"D_VehicleCapacity"
                                                ,"V_SingleChambCapacity"
                                                ,"V_DualChambCapacity"
				                                ,"V_VenderName"
				                                ,"V_VendorContactNo"
                                                ,"CreatedBy"
                                                ,"IsActive"
                                                ,"MilkCollectionFrom"
                },
                                                new string[] { "3",  ddlTankerDetail.SelectedValue,
                                                txtV_VehicleNo.Text,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                txtD_VehicleCapacity.Text,
                                                txtSingleChamberCapacity.Text,
                                                txtDualChamberCapacity.Text,
                                                txtV_VenderName.Text,
                                                txtV_VendorContactNo.Text,                                        
                                                objdb.createdBy(),isactive.ToString(),
                                                ddlMilkCollectionFrom.SelectedValue    }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        GetData();
                    }
                    else
                    {

                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Already Exists.")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle No Already Exists");

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                        }
                    }
                }
                if (btnSubmit.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_TankerDetail", new string[] { "flag", "V_VehicleType"
				                                ,"V_VehicleNo"
				                                ,"I_OfficeID" 
				                                ,"I_OfficeTypeID" 
				                                ,"D_VehicleCapacity"
                                                ,"V_SingleChambCapacity"
                                                ,"V_DualChambCapacity"
				                                ,"V_VenderName"
				                                ,"V_VendorContactNo"
                                                ,"CreatedBy"
                                                ,"IsActive"
                                                ,"I_TankerID"
                                                ,"MilkCollectionFrom"
                },
                                                   new string[] { "4",  ddlTankerDetail.SelectedValue,
                                                txtV_VehicleNo.Text,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                txtD_VehicleCapacity.Text,
                                                txtSingleChamberCapacity.Text,
                                                txtDualChamberCapacity.Text,
                                                txtV_VenderName.Text,
                                                txtV_VendorContactNo.Text,                                        
                                                objdb.createdBy(),isactive.ToString(),
                                                ViewState["rowid"].ToString(),
                                                ddlMilkCollectionFrom.SelectedValue
                                                   }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        GetData();
                        btnSubmit.Text = "Save";
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Already Exists.")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle No Already Exists");

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                        }
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());


            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tanker_Master.aspx");
    }

    protected void Clear()
    {
        ddlTankerDetail.SelectedIndex = 0;
        txtD_VehicleCapacity.Text = "";
        txtV_VenderName.Text = "";
        txtV_VehicleNo.Text = "";
        txtV_VendorContactNo.Text = "";
        chkIsActive.Checked = true;
        txtSingleChamberCapacity.Text = "";
        txtDualChamberCapacity.Text = "";
        txtD_VehicleCapacity.Attributes.Remove("readonly");
        divchamber.Visible = false;
    }
    protected void GetData()
    {
        try
        {
            gv_TankerDetails.DataSource = objdb.ByProcedure("Usp_TankerDetail",
                            new string[] { "flag", "MilkCollectionFrom", "I_OfficeID" },
                            new string[] { "1", "0", objdb.Office_ID() }, "dataset");
            gv_TankerDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gv_TankerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblTankerType_ID = (Label)row.FindControl("LblvendorType");
                    Label lblVehicle_No = (Label)row.FindControl("lblV_VehicleNo");
                    Label lblVehicle_Capacity = (Label)row.FindControl("lblV_VehicleCapacity");
                    Label lblVendor_Name = (Label)row.FindControl("lblV_Vendor");
                    Label lblVendor_ContactNo = (Label)row.FindControl("lblV_VendorContact");
                    Label lblIsActive = (Label)row.FindControl("lblTstatus");
                    Label lblMilkCollectionFrom = (Label)row.FindControl("lblMilkCollectionFrom");
                    Label lblV_SingleChambCapacity = (Label)row.FindControl("lblV_SingleChambCapacity");
                    Label lblV_DualChambCapacity = (Label)row.FindControl("lblV_DualChambCapacity");

                    if (lblIsActive.Text == "False")
                    {
                        chkIsActive.Checked = false;
                    }
                    else
                    {
                        chkIsActive.Checked = true;
                    }



                    ddlTankerDetail.SelectedValue = lblTankerType_ID.Text;
                    txtD_VehicleCapacity.Text = lblVehicle_Capacity.Text;

                    txtV_VehicleNo.Text = lblVehicle_No.Text;
                    txtD_VehicleCapacity.Text = lblVehicle_Capacity.Text;
                    txtSingleChamberCapacity.Text = lblV_SingleChambCapacity.Text;
                    txtDualChamberCapacity.Text = lblV_DualChambCapacity.Text;
                    txtV_VenderName.Text = lblVendor_Name.Text;
                    txtV_VendorContactNo.Text = lblVendor_ContactNo.Text;
                    ddlMilkCollectionFrom.SelectedValue = lblMilkCollectionFrom.Text;
                    ddlTankerDetail_SelectedIndexChanged(sender, e);
                    ViewState["rowid"] = e.CommandArgument;

                    btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in gv_TankerDetails.Rows)
                    {
                        if (gv_TankerDetails.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gv_TankerDetails.SelectedIndex = gvRow.DataItemIndex;
                            gv_TankerDetails.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected void gv_TankerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Finding label
                Label lblTankerStatus = (Label)e.Row.FindControl("lblTankerStatus"); 
                LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");

                //if (lblTankerStatus.Text == "False")
                //{
                //    lnkUpdate.Visible = false;
                //}

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlTankerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        divchamber.Visible = false;
        txtD_VehicleCapacity.Enabled = true;
        //txtD_VehicleCapacity.Text = "";
        if(ddlTankerDetail.SelectedValue == "D")
        {
            divchamber.Visible = true;
            txtD_VehicleCapacity.Attributes.Add("readonly", "readonly");
        }
        else
        {
            divchamber.Visible = false;
            txtD_VehicleCapacity.Attributes.Remove("readonly");

        }
    }
}