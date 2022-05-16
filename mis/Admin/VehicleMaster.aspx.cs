using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Globalization;

public partial class mis_Common_VehicleMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
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
    private void GetTransCompName()
    {
        try
        {
            ddlTransCompName.DataSource = objdb.ByProcedure("sp_tblPUTransporterMaster",
                                    new string[] { "flag" },
                                      new string[] { "5" }, "dataset");
            ddlTransCompName.DataTextField = "Transporter_Company";
            ddlTransCompName.DataValueField = "TransporterId";
            ddlTransCompName.DataBind();
            ddlTransCompName.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1", ex.Message.ToString());
        }
    }
    private void GetVehicleType()
    {
        try
        {
            ddlVehicleType.DataSource = objdb.ByProcedure("Sp_tblVehicleType",
                                    new string[] { "flag" },
                                      new string[] { "5" }, "dataset");
            ddlVehicleType.DataTextField = "VehicleType_Name";
            ddlVehicleType.DataValueField = "VehicleType_ID";
            ddlVehicleType.DataBind();
            ddlVehicleType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1", ex.Message.ToString());
        }
    }
    private void GetVehicleMasterDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("Sp_tblVehicleMaster",
                    new string[] { "flag", "CreatedBy" },
                    new string[] { "1", objdb.createdBy() }, "dataset");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Clear()
    {
        ddlTransCompName.ClearSelection();
        ddlVehicleType.ClearSelection();
        txtVehicleNo.Text = string.Empty;
        txtVehicleRCValidity_Date.Text = string.Empty;
        txtInsuranceNo.Text = string.Empty;
        txtInsuranceValidity_date.Text = string.Empty;
        txtDriverName.Text = string.Empty;
        txtDriverMobileNo.Text = string.Empty;
        txtDriverLicenseNo.Text = string.Empty;
        txtLicenseValidity.Text = string.Empty;
        txtVehicleLoadCapcity.Text = string.Empty;
      
        btnSubmit.Text = "Save";
        ViewState["rowid"] = null;
        GridView1.SelectedIndex = -1;
    }
    private void InsertorUpdateVehicleMaster()
    {
        lblMsg.Text = "";
        string Is_Active = "1";
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    DateTime date1 = DateTime.ParseExact(txtVehicleRCValidity_Date.Text.Trim(), "dd/MM/yyyy", culture);
                    DateTime date2 = DateTime.ParseExact(txtInsuranceValidity_date.Text.Trim(), "dd/MM/yyyy", culture);
                    DateTime date3 = DateTime.ParseExact(txtLicenseValidity.Text.Trim(), "dd/MM/yyyy", culture);
                    if (btnSubmit.Text == "Save")
                    {
                        ds = objdb.ByProcedure("Sp_tblVehicleMaster",
                            new string[] { "flag","TransporterId", "VehicleType_ID", "Vehicle_RC_Number", "Vehicle_RC_Validity_date", "Insurance_No", 
		"Insurance_Validity_date", "Driver_Name", "Driver_Mobile_Number", "Driver_License_No", "License_Validity_date","Unit_id", "Vehicle_Load_Capcity", "Is_Active", 
		"CreatedBy", "CreatedBy_IP" },
                            new string[] { "2", ddlTransCompName.SelectedValue,ddlVehicleType.SelectedValue, txtVehicleNo.Text,date1.ToString(), txtInsuranceNo.Text,
                          date2.ToString(), txtDriverName.Text, txtDriverMobileNo.Text, txtDriverLicenseNo.Text, date3.ToString(),ddlUnit.SelectedValue, txtVehicleLoadCapcity.Text, 
                        Is_Active, objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            GetVehicleMasterDetails();
                            Clear();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            GetVehicleMasterDetails();
                        }

                    }
                    else if (btnSubmit.Text == "Update" && ViewState["VehicleTypeMaster_ID"].ToString() != "0")
                    {
                        ds = objdb.ByProcedure("Sp_tblVehicleMaster",
                             new string[] { "flag", "VehicleTypeMaster_ID", "TransporterId", "VehicleType_ID", "Vehicle_RC_Number", "Vehicle_RC_Validity_date", "Insurance_No", 
		"Insurance_Validity_date", "Driver_Name", "Driver_Mobile_Number", "Driver_License_No", "License_Validity_date","Unit_id", "Vehicle_Load_Capcity", "Is_Active", 
		"CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                             new string[] { "3",ViewState["rowid"].ToString(), ddlTransCompName.SelectedValue,ddlVehicleType.SelectedValue, txtVehicleNo.Text, date1.ToString()
                                            ,txtInsuranceNo.Text , date2.ToString(), txtDriverName.Text, txtDriverMobileNo.Text, txtDriverLicenseNo.Text
                                            , date3.ToString() , ddlUnit.SelectedValue, txtVehicleLoadCapcity.Text, Is_Active,  objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),
                                         Path.GetFileName(Request.Url.AbsolutePath), "Vehicle Master Details Updated"}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            GetVehicleMasterDetails();
                            Clear();
                        }
                        else
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", success);
                            GetVehicleMasterDetails();
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Something wents wrong!");
                    }

                    ds.Clear();
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
    protected void ddlTransCompName_Init(object sender, EventArgs e)
    {
        GetTransCompName();
    }
    protected void ddlVehicleType_Init(object sender, EventArgs e)
    {
        GetVehicleType();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertorUpdateVehicleMaster();
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
                    Label lblVehicleTypeMaster_ID = (Label)row.FindControl("lblVehicleTypeMaster_ID");
                    Label lblTransporterId = (Label)row.FindControl("lblTransporterId");
                    Label lblTransporterCompany = (Label)row.FindControl("lblTransporterCompany");
                    Label lblVehicleType_ID = (Label)row.FindControl("lblVehicleType_ID");
                    Label lblVehicleTypeName = (Label)row.FindControl("lblVehicleTypeName");
                    Label lblVehicleRCNumber = (Label)row.FindControl("lblVehicleRCNumber");
                    Label lblVehicleRCValiditydate = (Label)row.FindControl("lblVehicleRCValiditydate");
                    Label lblInsuranceNo = (Label)row.FindControl("lblInsuranceNo");
                    Label lblInsuranceValiditydate = (Label)row.FindControl("lblInsuranceValiditydate");
                    Label lblDriverName = (Label)row.FindControl("lblDriverName");
                    Label lblDriverMobileNumber = (Label)row.FindControl("lblDriverMobileNumber");
                    Label lblDriverLicenseNo = (Label)row.FindControl("lblDriverLicenseNo");
                    Label lblLicenseValiditydate = (Label)row.FindControl("lblLicenseValiditydate");
                    Label lblUnitid = (Label)row.FindControl("lblUnitid");
                    Label lblVehicleLoadCapcity = (Label)row.FindControl("lblVehicleLoadCapcity");

                    ddlTransCompName.SelectedValue = lblTransporterId.Text;
                    ddlVehicleType.SelectedValue = lblVehicleType_ID.Text;
                    txtVehicleNo.Text = lblVehicleRCNumber.Text;
                    txtVehicleRCValidity_Date.Text = lblVehicleRCValiditydate.Text;
                    txtInsuranceNo.Text = lblInsuranceNo.Text;
                    txtInsuranceValidity_date.Text = lblInsuranceValiditydate.Text;
                    txtDriverName.Text = lblDriverName.Text;
                    txtDriverMobileNo.Text = lblDriverMobileNumber.Text;
                    txtDriverLicenseNo.Text = lblDriverLicenseNo.Text;
                    txtLicenseValidity.Text = lblLicenseValiditydate.Text;
                    ddlUnit.SelectedValue = lblUnitid.Text;
                    txtVehicleLoadCapcity.Text = lblVehicleLoadCapcity.Text;

                    ViewState["VehicleTypeMaster_ID"] = lblVehicleTypeMaster_ID.Text;
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
                    ds = objdb.ByProcedure("Sp_tblVehicleMaster",
                                new string[] { "flag", "VehicleTypeMaster_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Vehicle Master Record Deleted" }, "TableSave");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetVehicleMasterDetails();
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
}