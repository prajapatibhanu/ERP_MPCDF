using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Garage_GrgVehicleMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    //DateTime FromDate = new DateTime();
                    //txtDate.Text = FromDate.ToString("dd/MM/yyyy");

                    txtDate.Attributes.Add("readonly", "readonly");
                    txtInsuranceValidTill.Attributes.Add("readonly", "readonly");
                    txtMonthlyRent.Enabled = false;
                    txtAgency.Enabled = false;
                    ViewState["VehicleID"] = "0";
                    if (Request.QueryString["Action"] != null)
                    {
                        ViewState["VehicleID"] = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        FillDetail();
                    }
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillDetail()
    {
        try
        {
            ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "VehicleID" }, new string[] { "10", ViewState["VehicleID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {


                txtDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                txtVehicleCategory.Text = ds.Tables[0].Rows[0]["VehicleCategory"].ToString();
                txtVehicleType.Text = ds.Tables[0].Rows[0]["VehicleType"].ToString();
                txtVehicleMake.Text = ds.Tables[0].Rows[0]["VehicleMake"].ToString();
                txtVehicleModel.Text = ds.Tables[0].Rows[0]["VehicleModel"].ToString();
                rbtFuelType.ClearSelection();
                rbtFuelType.SelectedValue = ds.Tables[0].Rows[0]["FuelType"].ToString();

                txtVehicleAverage.Text = ds.Tables[0].Rows[0]["VehicleAverage"].ToString();

                rbtVehicleOwnedType.ClearSelection();
                rbtVehicleOwnedType.SelectedValue = ds.Tables[0].Rows[0]["VehicleOwnedType"].ToString();

                txtMonthlyRent.Enabled = false;
                txtAgency.Enabled = false;
                if (rbtVehicleOwnedType.SelectedValue.ToString() == "Hired")
                {
                    txtMonthlyRent.Enabled = true;
                    txtAgency.Enabled = true;
                }


                txtMonthlyRent.Text = ds.Tables[0].Rows[0]["MonthlyRent"].ToString();
                txtAgency.Text = ds.Tables[0].Rows[0]["Agency"].ToString();

                txtAllot_Incharge.Text = ds.Tables[0].Rows[0]["Allot_Incharge"].ToString();
                txtVehicleNo.Text = ds.Tables[0].Rows[0]["VehicleNo"].ToString();
                txtVehicleChechisNo.Text = ds.Tables[0].Rows[0]["VehicleChechisNo"].ToString();
                txtVehicleRegistrationNo.Text = ds.Tables[0].Rows[0]["VehicleRegistrationNo"].ToString();
                txtVehicleInsuranceNo.Text = ds.Tables[0].Rows[0]["VehicleInsuranceNo"].ToString();
                txtInsuranceValue.Text = ds.Tables[0].Rows[0]["InsuranceValue"].ToString();
                txtInsuranceValidTill.Text = ds.Tables[0].Rows[0]["InsuranceValidTill"].ToString();
                txtInsuranceCompany.Text = ds.Tables[0].Rows[0]["InsuranceCompany"].ToString();
                txtVehicleSummary.Text = ds.Tables[0].Rows[0]["VehicleSummary"].ToString();
                txtInCharge.Text = ds.Tables[0].Rows[0]["InCharge"].ToString();
                txtDriverName.Text = ds.Tables[0].Rows[0]["DriverName"].ToString();
                txtDriverLicenseNo.Text = ds.Tables[0].Rows[0]["DriverLicenseNo"].ToString();
                btnSave.Text = "Update";

            }
            else
            {
                btnSave.Text = "Save";
                Response.Redirect("GrgVehicleMaster.aspx");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtDate.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            if (txtVehicleCategory.Text == "")
            {
                msg += "Enter Vehicle Category. \\n";
            }
            if (txtVehicleType.Text == "")
            {
                msg += "Enter Vehicle Type. \\n";
            }
            if (txtVehicleMake.Text == "")
            {
                msg += "Enter Vehicle Make. \\n";
            }
            if (txtVehicleModel.Text == "")
            {
                msg += "Enter Vehicle Model. \\n";
            }
            if (txtVehicleAverage.Text == "")
            {
                msg += "Enter Vehicle Average. \\n";
            }
            if (rbtVehicleOwnedType.SelectedValue.ToString() == "Hired")
            {
                if (txtMonthlyRent.Text == "")
                {
                    msg += "Enter Monthly Rent. \\n";
                }
                if (txtAgency.Text == "")
                {
                    msg += "Enter Agency. \\n";
                }
            }
            if (txtAllot_Incharge.Text == "")
            {
                msg += "Enter Allot Incharge. \\n";
            }
            if (txtVehicleNo.Text == "")
            {
                msg += "Enter Vehicle No. \\n";
            }
            if (txtVehicleChechisNo.Text == "")
            {
                msg += "Enter Vehicle Chechis No. \\n";
            }
            if (txtVehicleRegistrationNo.Text == "")
            {
                msg += "Enter Vehicle Registration No. \\n";
            }
            if (txtVehicleInsuranceNo.Text == "")
            {
                msg += "Enter Vehicle Insurance No. \\n";
            }
            if (txtInsuranceValue.Text == "")
            {
                msg += "Enter Insurance Value. \\n";
            }
            if (txtInsuranceValidTill.Text == "")
            {
                msg += "Enter Insurance Valid Till. \\n";
            }
            if (txtInsuranceCompany.Text == "")
            {
                msg += "Enter Insurance Company. \\n";
            }
            if (txtVehicleSummary.Text == "")
            {
                msg += "Enter Vehicle Summary. \\n";
            }
            if (txtInCharge.Text == "")
            {
                msg += "Enter InCharge (section/officer). \\n";
            }
            if (txtDriverName.Text == "")
            {
                msg += "Enter Driver Name. \\n";
            }
            if (txtDriverLicenseNo.Text == "")
            {
                msg += "Enter Driver License No. \\n";
            }

            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("spGrgVehicleMaster", new string[] { "flag", "VehicleNo", "Office_ID", "VehicleID" }, new string[] { "1", txtVehicleNo.Text, ViewState["Office_ID"].ToString(), ViewState["VehicleID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (Status == 0 && ViewState["VehicleID"].ToString() == "0" && btnSave.Text == "Save")
                {
                    string MonthlyRent = "0.00";
                    if (txtMonthlyRent.Text != "")
                    {
                        MonthlyRent = txtMonthlyRent.Text;
                    }
                    objdb.ByProcedure("spGrgVehicleMaster",
                    new string[] { "flag", "Date", "Office_ID", "VehicleCategory"
                            , "VehicleType", "VehicleMake", "VehicleModel", "FuelType", "VehicleAverage", "VehicleOwnedType"
                            , "MonthlyRent", "Agency", "Allot_Incharge", "VehicleNo", "VehicleChechisNo", "VehicleRegistrationNo"
                            , "VehicleInsuranceNo", "InsuranceValue", "InsuranceValidTill"
                            , "InsuranceCompany", "VehicleSummary", "InCharge", "DriverName", "DriverLicenseNo", "UpdatedBy" },
                    new string[] { "0", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), txtVehicleCategory.Text
                            , txtVehicleType.Text, txtVehicleMake.Text, txtVehicleModel.Text, rbtFuelType.SelectedValue.ToString(), txtVehicleAverage.Text, rbtVehicleOwnedType.SelectedValue.ToString()
                            , MonthlyRent, txtAgency.Text, txtAllot_Incharge.Text, txtVehicleNo.Text, txtVehicleChechisNo.Text, txtVehicleRegistrationNo.Text
                            , txtVehicleInsuranceNo.Text, txtInsuranceValue.Text, Convert.ToDateTime(txtInsuranceValidTill.Text, cult).ToString("yyyy/MM/dd")
                            , txtInsuranceCompany.Text, txtVehicleSummary.Text, txtInCharge.Text, txtDriverName.Text, txtDriverLicenseNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    ViewState["VehicleID"] = "0";
                    btnSave.Text = "Save";
                    ClearText();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Saved.");


                }
                else if (Status == 0 && ViewState["VehicleID"].ToString() != "0" && btnSave.Text == "Update")
                {
                    string MonthlyRent = "0.00";
                    if (txtMonthlyRent.Text != "")
                    {
                        MonthlyRent = txtMonthlyRent.Text;
                    }
                    objdb.ByProcedure("spGrgVehicleMaster",
                    new string[] { "flag", "VehicleID","Date", "Office_ID", "VehicleCategory"
                            , "VehicleType", "VehicleMake", "VehicleModel", "FuelType", "VehicleAverage", "VehicleOwnedType"
                            , "MonthlyRent", "Agency", "Allot_Incharge", "VehicleNo", "VehicleChechisNo", "VehicleRegistrationNo"
                            , "VehicleInsuranceNo", "InsuranceValue", "InsuranceValidTill"
                            , "InsuranceCompany", "VehicleSummary", "InCharge", "DriverName", "DriverLicenseNo", "UpdatedBy" },
                    new string[] { "11",ViewState["VehicleID"].ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), txtVehicleCategory.Text
                            , txtVehicleType.Text, txtVehicleMake.Text, txtVehicleModel.Text, rbtFuelType.SelectedValue.ToString(), txtVehicleAverage.Text, rbtVehicleOwnedType.SelectedValue.ToString()
                            , MonthlyRent, txtAgency.Text, txtAllot_Incharge.Text, txtVehicleNo.Text, txtVehicleChechisNo.Text, txtVehicleRegistrationNo.Text
                            , txtVehicleInsuranceNo.Text, txtInsuranceValue.Text, Convert.ToDateTime(txtInsuranceValidTill.Text, cult).ToString("yyyy/MM/dd")
                            , txtInsuranceCompany.Text, txtVehicleSummary.Text, txtInCharge.Text, txtDriverName.Text, txtDriverLicenseNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    ViewState["VehicleID"] = "0";
                    btnSave.Text = "Save";
                    ClearText();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Updated.");


                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Vehicle No. already exist.');", true);
                }


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void rbtVehicleOwnedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtMonthlyRent.Text = "";
            txtAgency.Text = "";
            txtMonthlyRent.Enabled = false;
            txtAgency.Enabled = false;
            if (rbtVehicleOwnedType.SelectedValue.ToString() == "Hired")
            {
                txtMonthlyRent.Enabled = true;
                txtAgency.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ClearText()
    {
        try
        {
            // txtDate.Text = "";
            txtVehicleCategory.Text = "";
            txtVehicleType.Text = "";
            txtVehicleMake.Text = "";
            txtVehicleModel.Text = "";
            rbtFuelType.SelectedValue = "Diesel";
            //txtFuelType.Text = "";
            txtVehicleAverage.Text = "";
            rbtVehicleOwnedType.SelectedValue = "Owned";
            //txtVehicleOwnedType.Text = "";
            txtMonthlyRent.Text = "";
            txtAgency.Text = "";
            //txtMonthlyRent.Enabled = true;
            //txtAgency.Enabled = true;
            txtMonthlyRent.Enabled = false;
            txtAgency.Enabled = false;

            txtAllot_Incharge.Text = "";
            txtVehicleNo.Text = "";
            txtVehicleChechisNo.Text = "";
            txtVehicleRegistrationNo.Text = "";
            txtVehicleInsuranceNo.Text = "";
            txtInsuranceValue.Text = "";
            // txtInsuranceValidTill.Text = "";
            txtInsuranceCompany.Text = "";
            txtVehicleSummary.Text = "";
            txtInCharge.Text = "";
            txtDriverName.Text = "";
            txtDriverLicenseNo.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}