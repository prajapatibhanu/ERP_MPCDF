using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_CattelFeed_CFP_OfficeRegistrations : System.Web.UI.Page
{
    DataSet ds1 = null;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Division();
        FillOfficeName();
        Fillgrd();

    }
    private void FillOfficeName()
    {
        try
        {
            ddlOfficeName.Items.Clear();
            ddlOfficeName.DataSource = objapi.ByProcedure("USP_CFP_OfficeRegistration",
                   new string[] { "Flag" },
                   new string[] { "0" }, "dataset");
            ddlOfficeName.DataTextField = "Office_Name";
            ddlOfficeName.DataValueField = "Office_ID";
            ddlOfficeName.DataBind();
            ddlOfficeName.Items.Insert(0, new ListItem("-- Select --", "0"));
            GC.SuppressFinalize(objapi);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Fillgrd()
    {
        try
        {
            grdCatlist.DataSource = objapi.ByProcedure("USP_CFP_OfficeRegistration",
                            new string[] { "Flag" },
                            new string[] { "1" }, "dataset");
            grdCatlist.DataBind();
            GC.SuppressFinalize(objapi);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Division()
    {
        try
        {
            ddlDivision.Items.Clear();
            ddlDivision.DataSource = objapi.ByProcedure("SP_CFPDivisionList",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_ID";
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlDistrict.Items.Insert(0, new ListItem("-- Select --", "0"));
            GC.SuppressFinalize(objapi);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void District()
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.DataSource = objapi.ByProcedure("SP_CFPDistrictByDivisionList",
                   new string[] { "flag", "DivisionID" },
                   new string[] { "0", ddlDivision.SelectedValue }, "dataset");
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("-- Select --", "0"));
            GC.SuppressFinalize(objapi);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        District();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertOrUpdateOffice();
    }
    private void InsertOrUpdateOffice()
    {
        StringBuilder sb = new StringBuilder();
        if (Page.IsValid)
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            ds1 = new DataSet();
            try
            {
                if (btnSave.Text == "Save")
                {
                    ds1 = objapi.ByProcedure("USP_CFP_OfficeRegistration",
                                        new string[] { "Flag", "Office_Parant_ID", "Office_Name_H", "Office_Code",
                                   "Office_ContactNo", "Office_Email", "Division_ID", "District_ID",
                                   "Office_Address", "Office_Pincode", "Office_Gst", 
                                   "Office_Pan", "Capacity", "CreatedBy", "CreatedBy_IP","Office_Name_E", "TAN_NO" },
                                        new string[] { "2", ddlOfficeName.SelectedValue, txtofficeNameHI.Text.Trim(), txtofficeCode.Text.Trim()
                                      , txtcontactno.Text , txtEmail.Text ,ddlDivision.SelectedValue, ddlDistrict.SelectedValue
                                      ,txtAddress.Text, txtPincode.Text.Trim(), txtGSTN.Text, txtPAN.Text
                                      , txtcapacity.Text, objapi.Office_ID(),IPAddress,txtOfficeName.Text.Trim(), txtTAN.Text }, "TableSave");
                    string success="";
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Fillgrd();
                        success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", success);
                    }
                    else
                    {
                        lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + success);
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds1 = objapi.ByProcedure("USP_CFP_OfficeRegistration",
                                        new string[] { "Flag","Office_ID", "Office_Parant_ID", "Office_Name_H", "Office_Code",
                                   "Office_ContactNo", "Office_Email", "Division_ID", "District_ID",
                                   "Office_Address", "Office_Pincode", "Office_Gst", 
                                   "Office_Pan", "Capacity", "CreatedBy", "CreatedBy_IP","Office_Name_E", "TAN_NO" },
                                        new string[] { "3",ViewState["rowid"].ToString(), ddlOfficeName.SelectedValue, txtofficeNameHI.Text.Trim(), txtofficeCode.Text.Trim()
                                      , txtcontactno.Text , txtEmail.Text ,ddlDivision.SelectedValue, ddlDistrict.SelectedValue
                                      ,txtAddress.Text, txtPincode.Text.Trim(), txtGSTN.Text, txtPAN.Text
                                      , txtcapacity.Text, objapi.Office_ID(),IPAddress,txtOfficeName.Text.Trim(), txtTAN.Text }, "TableSave");
                    string success = "";
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Fillgrd();
                        success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", success);
                       
                    }
                    else
                    {
                        lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + success);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds1 != null) { ds1.Dispose(); }
            }




        }
    }

    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblOffice_Parant_ID = (Label)row.FindControl("lblOffice_Parant_ID");
                    Label lblOffice_ContactNo = (Label)row.FindControl("lblOffice_ContactNo");
                    Label lblOffice_Email = (Label)row.FindControl("lblOffice_Email");
                    Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");
                    Label lblDistrict_ID = (Label)row.FindControl("lblDistrict_ID");
                    Label lblOffice_Address = (Label)row.FindControl("lblOffice_Address");
                    Label lblOffice_Pincode = (Label)row.FindControl("lblOffice_Pincode");
                    Label lblOffice_Gst = (Label)row.FindControl("lblOffice_Gst");
                    Label lblOffice_Pan = (Label)row.FindControl("lblOffice_Pan");
                    Label lblOffice_Name_E = (Label)row.FindControl("lblOffice_Name_E");
                    Label lblTAN_NO = (Label)row.FindControl("lblTAN_NO");
                    Label lblOffice_Name = (Label)row.FindControl("lblOffice_Name");
                    Label lblOffice_Code = (Label)row.FindControl("lblOffice_Code");
                    Label lblCapacity = (Label)row.FindControl("lblCapacity");

                    ddlOfficeName.SelectedValue = lblOffice_Parant_ID.Text;
                    txtcontactno.Text = lblOffice_ContactNo.Text;
                    txtEmail.Text = lblOffice_Email.Text;
                    ddlDivision.SelectedValue = lblDivision_ID.Text;
                    District();
                    ddlDistrict.SelectedValue = lblDistrict_ID.Text;
                    txtAddress.Text = lblOffice_Address.Text;
                    txtPincode.Text = lblOffice_Pincode.Text;
                    txtGSTN.Text = lblOffice_Gst.Text;
                    txtPAN.Text = lblOffice_Pan.Text;
                    txtOfficeName.Text = lblOffice_Name_E.Text;
                    txtTAN.Text = lblTAN_NO.Text;
                    txtofficeNameHI.Text = lblOffice_Name.Text;
                    txtofficeCode.Text = lblOffice_Code.Text;
                    txtcapacity.Text = lblCapacity.Text;


                    ViewState["rowid"] = e.CommandArgument;

                    btnSave.Text = "Update";

                    foreach (GridViewRow gvRow in grdCatlist.Rows)
                    {
                        if (grdCatlist.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            grdCatlist.SelectedIndex = gvRow.DataItemIndex;
                            grdCatlist.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private void Clear()
    {
        txtOfficeName.Text = string.Empty;
        txtofficeNameHI.Text = string.Empty;
        txtofficeCode.Text = string.Empty;
        txtofficeCode.Enabled = true;
        txtPincode.Text = string.Empty;
        txtcapacity.Text = string.Empty;
        txtcontactno.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlDivision.SelectedValue = "0";
        District();
        ddlDistrict.SelectedValue = "0";
        ddlOfficeName.SelectedValue = "0";
        txtAddress.Text = string.Empty;
        txtGSTN.Text = string.Empty;
        txtPAN.Text = string.Empty;
        txtTAN.Text = string.Empty;
        btnSave.Text = "Save";
        grdCatlist.SelectedIndex = -1;

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear();
    }

}