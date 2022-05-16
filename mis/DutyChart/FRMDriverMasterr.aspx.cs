using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class mis_DutyChart_FRMDriverMasterr : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
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
    protected void FillGrid()
    {
        try
        {
            gvDriverDetail.DataSource = null;
            ds = objdb.ByProcedure("Sp_tblDriverMaster", new string[] { "flag", "Office_ID" },
                new string[] { "4", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDriverDetail.DataSource = ds;
                }
            }
            gvDriverDetail.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Sp_tblDriverMaster", new string[] { "flag", "Driver_Name", "MobileNo", "Office_ID", "CreatedBY", "CreatedIP" },
                        new string[] { "1", txtDriverName.Text.Trim(), txtMobileNo.Text, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());

                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Danger.ToString());
                            }
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("Sp_tblDriverMaster", new string[] { "flag", "Driver_ID", "MobileNo", "Office_ID", "Driver_Name", "CreatedBY", "CreatedIP" },
                        new string[] { "2", ViewState["Driver_ID"].ToString(), txtMobileNo.Text, objdb.Office_ID(), txtDriverName.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());

                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Danger.ToString());
                            }
                        }
                    }
                }
                FillGrid();
                txtDriverName.Text = "";
                btnSave.Text = "Save";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDriverDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Driver_ID"] = e.CommandArgument.ToString();
            if (e.CommandName == "EditRecord")
            {
                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //Label lblBMCTankerRootName = (Label)row.FindControl("lblBMCTankerRootName");
                //Label lblBMCTankerRootDescription = (Label)row.FindControl("lblBMCTankerRootDescription");
                ds = objdb.ByProcedure("Sp_tblDriverMaster", new string[] { "flag", "Driver_ID" },
                       new string[] { "5", ViewState["Driver_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtDriverName.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    btnSave.Text = "Update";
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                ds = objdb.ByProcedure("Sp_tblDriverMaster", new string[] { "flag", "Driver_ID", "CreatedBy", "CreatedIP" },
                       new string[] { "3", ViewState["Driver_ID"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                            FillGrid();
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
}