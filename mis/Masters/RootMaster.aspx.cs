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

public partial class mis_Masters_RootMaster : System.Web.UI.Page
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
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());      
                FillOffice();
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
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                  new string[] { "59", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                ddlDS.Enabled = false;

            }
            else
            {

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
            string IsActive = "1";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if(btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_Mst_BMCTankerRootDetails", new string[] { "flag", "Office_ID", "BMCTankerRootName", "BMCTankerRootDescription", "IsActive", "CreatedBy", "CreatedByIP" }, new string[] {"1", objdb.Office_ID(),txtRootName.Text,txtRootDescription.Text,IsActive,objdb.createdBy(),objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                            }

                            //FillGrid();
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_Mst_BMCTankerRootDetails", new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "BMCTankerRootName", "BMCTankerRootDescription", "CreatedBy", "CreatedByIP" }, new string[] { "5", ViewState["BMCTankerRoot_Id"].ToString(),objdb.Office_ID(), txtRootName.Text, txtRootDescription.Text, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                            }

                            //FillGrid();
                        }
                    }
                }
                FillGrid();
                txtRootName.Text = "";
                txtRootDescription.Text = "";
                btnSave.Text = "Save";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(e.CommandName == "EditRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblBMCTankerRootName = (Label)row.FindControl("lblBMCTankerRootName");
                Label lblBMCTankerRootDescription = (Label)row.FindControl("lblBMCTankerRootDescription");
                ViewState["BMCTankerRoot_Id"] = e.CommandArgument.ToString();
                txtRootName.Text = lblBMCTankerRootName.Text;
                txtRootDescription.Text = lblBMCTankerRootDescription.Text;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("Usp_Mst_BMCTankerRootDetails", new string[] { "flag", "Office_ID" }, new string[] {"3",objdb.Office_ID() }, "dataset");
             if (ds != null && ds.Tables.Count > 0)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     gvDetails.DataSource = ds;
                     gvDetails.DataBind();
                 }
                 else
                 {
                     gvDetails.DataSource = string.Empty;
                     gvDetails.DataBind();
                 }
             }
             else
             {
                 gvDetails.DataSource = string.Empty;
                 gvDetails.DataBind();
             }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}