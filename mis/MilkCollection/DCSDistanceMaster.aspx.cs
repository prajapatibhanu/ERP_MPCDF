using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_MilkCollection_DCSDistanceMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds2;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
            {
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillDCS();
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
    protected void FillDCS()
    {
        try
        {
            lblMsg.Text = "";

            string code = "6";
            
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                        new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" },
                                        new string[] { "32", code, objdb.Office_ID() }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDCS.DataTextField = "Office_Name";
                    ddlDCS.DataValueField = "Office_ID";
                    ddlDCS.DataSource = ds.Tables[0];
                    ddlDCS.DataBind();
                    ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }

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
                    ds = objdb.ByProcedure("Sp_Mst_DCSDistance", new string[] { "flag","DCS_ID", "OfficeType_ID", "Distance", "IsActive", "CreatedAt", "CreatedBy", "CreatedByIP" }, new string[] {"1",ddlDCS.SelectedValue,objdb.OfficeType_ID(),txtDistance.Text,IsActive,objdb.Office_ID(),objdb.createdBy(),objdb.GetLocalIPAddress() }, "dataset");
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
                    txtDistance.Text = "";
                    ddlDCS.ClearSelection();
                    FillGrid();
                }
                else if (btnSave.Text == "Update")
                {
                ds = objdb.ByProcedure("Sp_Mst_DCSDistance", new string[] { "flag","DCSDistance_ID", "DCS_ID",  "Distance", "CreatedAt", "CreatedBy", "CreatedByIP" }, new string[] { "4",ViewState["DCSDistance_ID"].ToString(), ddlDCS.SelectedValue, txtDistance.Text, objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
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
                txtDistance.Text = "";
                ddlDCS.ClearSelection();
                btnSave.Text = "Save";
                FillGrid();
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Mst_DCSDistance", new string[] { "flag", "OfficeType_ID" }, new string[] { "2",objdb.OfficeType_ID() }, "dataset");
            if(ds!= null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvReports.DataSource = ds;
                    gvReports.DataBind();
                }
                else
                {
                    gvReports.DataSource = string.Empty;
                    gvReports.DataBind();
                }
            }
            else
            {
                gvReports.DataSource = string.Empty;
                gvReports.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
    }
    protected void gvReports_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(e.CommandName == "EditRecord")
            {
                string DCSDistance_ID = e.CommandArgument.ToString();
                ViewState["DCSDistance_ID"] = DCSDistance_ID.ToString();
                ds = objdb.ByProcedure("Sp_Mst_DCSDistance", new string[] { "flag", "DCSDistance_ID" }, new string[] { "3", DCSDistance_ID.ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtDistance.Text = ds.Tables[0].Rows[0]["Distance"].ToString();
                        ddlDCS.ClearSelection();
                        ddlDCS.Items.FindByValue(ds.Tables[0].Rows[0]["DCS_ID"].ToString()).Selected = true;
                        btnSave.Text = "Update";
                    }
                    
                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
    }
}