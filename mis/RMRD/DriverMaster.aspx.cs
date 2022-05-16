using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;

public partial class mis_RMRD_DriverMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
            {
                lblMsg.Text = "";

                if (!Page.IsPostBack)
                {
                    FillGrid();
                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            if(btnSubmit.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_Mst_Driver", new string[] { "flag", 
                                                                  "Driver_Name", 
                                                                  "Driver_MobileNo", 
                                                                  "Driver_LicenceNo", 
                                                                  "Office_ID",
                                                                  "IsActive", 
                                                                  "CreatedBy", 
                                                                  "CreatedByIP" }, new string[] 
                                                                  {"0",
                                                                    txtV_DriverName.Text,
                                                                    txtV_DriverMobileNo.Text,
                                                                    txtNV_DriverDrivingLicenceNo.Text,
                                                                    objdb.Office_ID(),
                                                                    IsActive,
                                                                    objdb.createdBy(),
                                                                    objdb.GetLocalIPAddress()
                                                                  }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                        FillGrid();                    
                        txtV_DriverName.Text = "";
                        txtV_DriverMobileNo.Text = "";
                        txtNV_DriverDrivingLicenceNo.Text = "";
                    }
                    else
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
                    }
                }
            }
            else if(btnSubmit.Text == "Update")
            {
                ds = objdb.ByProcedure("Usp_Mst_Driver", new string[] { "flag", 
                                                                  "Driver_ID",
                                                                  "Driver_Name", 
                                                                  "Driver_MobileNo", 
                                                                  "Driver_LicenceNo",                                                                 
                                                                  "CreatedBy", 
                                                                  "CreatedByIP" }, new string[] 
                                                                  {"3",
                                                                    ViewState["Driver_ID"].ToString(),
                                                                    txtV_DriverName.Text,
                                                                    txtV_DriverMobileNo.Text,
                                                                    txtNV_DriverDrivingLicenceNo.Text,                                                                    
                                                                    objdb.createdBy(),
                                                                    objdb.GetLocalIPAddress()
                                                                  }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                        FillGrid();
                        btnSubmit.Text = "Save";
                        txtV_DriverName.Text = "";
                        txtV_DriverMobileNo.Text = "";
                        txtNV_DriverDrivingLicenceNo.Text = "";
                    }
                    else
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            gvDriverDetails.DataSource = new string[] { };
            gvDriverDetails.DataBind();
            DataSet ds1 = objdb.ByProcedure("Usp_Mst_Driver", new string[] { "flag", "Office_ID" }, new string[] { "1", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                gvDriverDetails.DataSource = ds1.Tables[0];
                gvDriverDetails.DataBind();
            }
            else
            {
                gvDriverDetails.DataSource = new string[] { };
                gvDriverDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void gvDriverDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Driver_ID = e.CommandArgument.ToString();
            ViewState["Driver_ID"] = Driver_ID.ToString();
            ds = objdb.ByProcedure("Usp_Mst_Driver", new string[] { "flag", "Driver_ID" }, new string[] { "2", Driver_ID.ToString() }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtV_DriverName.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                txtV_DriverMobileNo.Text = ds.Tables[0].Rows[0]["Driver_MobileNo"].ToString();
                txtNV_DriverDrivingLicenceNo.Text = ds.Tables[0].Rows[0]["Driver_LicenceNo"].ToString();
                btnSubmit.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}