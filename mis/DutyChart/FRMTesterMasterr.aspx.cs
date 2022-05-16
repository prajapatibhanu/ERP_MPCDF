using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DutyChart_FRMTesterMasterr : System.Web.UI.Page
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
            gvTesterDetail.DataSource = null;
            ds = objdb.ByProcedure("Sp_tblTesterMaster", new string[] { "flag", "Office_ID" },
                new string[] { "4", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvTesterDetail.DataSource = ds;
                }
            }
            gvTesterDetail.DataBind();
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
            string IsActive = "1";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Sp_tblTesterMaster", new string[] { "flag", "Office_ID", "Tester_Name", "MobileNo", "CreatedBY", "CreatedIP" },
                        new string[] { "1", objdb.Office_ID(), txtTesterName.Text.Trim(), txtMobileNo.Text, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
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
                    ds = objdb.ByProcedure("Sp_tblTesterMaster", new string[] { "flag", "Tester_ID", "Tester_Name", "MobileNo", "CreatedBY", "CreatedIP" },
                        new string[] { "2", ViewState["Tester_ID"].ToString(), txtTesterName.Text.Trim(), txtMobileNo.Text, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank Y ou!", Success.ToString());

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
                txtTesterName.Text = "";
                txtMobileNo.Text = "";
                btnSave.Text = "Save";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvTesterDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Tester_ID"] = e.CommandArgument.ToString();
            if (e.CommandName == "EditRecord")
            {
                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //Label lblBMCTankerRootName = (Label)row.FindControl("lblBMCTankerRootName");
                //Label lblBMCTankerRootDescription = (Label)row.FindControl("lblBMCTankerRootDescription");
                ds = objdb.ByProcedure("Sp_tblTesterMaster", new string[] { "flag", "Tester_ID" },
                       new string[] { "5", ViewState["Tester_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtTesterName.Text = ds.Tables[0].Rows[0]["Tester_Name"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    btnSave.Text = "Update";
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                ds = objdb.ByProcedure("Sp_tblTesterMaster", new string[] { "flag", "Tester_ID", "CreatedBy", "CreatedIP" },
                       new string[] { "3", ViewState["Tester_ID"].ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());

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