using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;


public partial class mis_Masters_Mst_Adv_AreaOrLocation : System.Web.UI.Page
{
    DataSet ds1, ds2 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    Int16 rouutebind = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!IsPostBack)
                {
                    rouutebind = 1;
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    Session["AreaData"] = null;
                    GetRouteandAreaDetails();
                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error : 1", ex.Message.ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetRouteandAreaDetails()
    {
        try
        {

            ds1 = objdb.ByProcedure("USP_Adv_Adv_Mst_Area",
                       new string[] { "flag", "Office_ID" },
                       new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if(rouutebind==1)
                    {
                        ddlRoute.DataTextField = "RName";
                        ddlRoute.DataValueField = "RouteId";
                        ddlRoute.DataSource = ds1.Tables[0];
                        ddlRoute.DataBind();
                        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
                    }
                 
                }
                else
                {
                    ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
                }

                if (ds1.Tables[1].Rows.Count > 0)
                {
                    ddlAreaSearch.DataTextField = "Adv_AreaName";
                    ddlAreaSearch.DataValueField = "Adv_AreaId";
                    ddlAreaSearch.DataSource = ds1.Tables[1];
                    ddlAreaSearch.DataBind();
                    ddlAreaSearch.Items.Insert(0, new ListItem("All", "0"));
                    DataTable dtrdata = new DataTable();
                    dtrdata = ds1.Tables[1];
                    Session["AreaData"] = dtrdata;
                    dtrdata.Dispose();

                }
                else
                {
                    ddlAreaSearch.Items.Clear();
                    ddlAreaSearch.Items.Insert(0, new ListItem("No Record Found", "0"));
                }


            }


        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error : 2", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void Clear()
    {
        txtAreaName.Text = string.Empty;
        btnSubmit.Text = "Save";
        hfiid.Value = "";
        foreach (RepeaterItem item in Repeater1.Items)
        {
            HtmlTableRow row = (HtmlTableRow)item.FindControl("row");
            row.Attributes["style"] = "background-color:white";
        }
    }
    private void InsertOrUpdateRoute()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (btnSubmit.Text == "Save")
                {
                    lblMsg.Text = "";
                    ds2 = objdb.ByProcedure("USP_Adv_Adv_Mst_Area",
                        new string[] { "flag", "Adv_AreaName", "RouteId", "Office_ID", "CreatedBy", "CreatedByIP" },
                        new string[] { "2", txtAreaName.Text.Trim(), ddlRoute.SelectedValue, objdb.Office_ID(), objdb.createdBy(), IPAddress }, "dataset");

                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        Clear();
                        ds2.Dispose();
                        GetRouteandAreaDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", txtAreaName.Text + "" + error);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                    }

                }
                else if (btnSubmit.Text == "Update")
                {
                    if (hfiid.Value != null || hfiid.Value != "")
                    {
                        lblMsg.Text = "";
                        ds2 = objdb.ByProcedure("USP_Adv_Adv_Mst_Area",
                            new string[] { "flag", "Adv_AreaId", "Adv_AreaName", "RouteId", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "3",hfiid.Value, txtAreaName.Text.Trim(),ddlRoute.SelectedValue,
                                           objdb.Office_ID(), objdb.createdBy(),IPAddress }, "dataset");

                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            ds2.Dispose();
                            GetRouteandAreaDetails();
                            GetAreaDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", txtAreaName.Text + "" + error);

                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Route Name");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error : Insert Or Update", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertOrUpdateRoute();
        }
    }
    protected void GetAreaDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            DataTable dt2 = (DataTable)Session["AreaData"];


            if (dt2 != null && dt2.Rows.Count > 0)
            {
                if (ddlAreaSearch.SelectedValue == "0")
                {
                    Repeater1.DataSource = dt2;
                    Repeater1.DataBind();
                }
                else
                {

                    DataTable tblFiltered = dt2.AsEnumerable()
                             .Where(r => r.Field<int>("Adv_AreaId") == Convert.ToInt32(ddlAreaSearch.SelectedValue))
                             .CopyToDataTable();
                    Repeater1.DataSource = tblFiltered;
                    Repeater1.DataBind();
                    tblFiltered.Dispose();
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error : Search", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetAreaDetails();
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                Label lblAreaName = e.Item.FindControl("lblAreaName") as Label;
                Label lblRouteId = e.Item.FindControl("lblRouteId") as Label;

                lblMsg.Text = string.Empty;
                txtAreaName.Text = lblAreaName.Text;
                ddlRoute.SelectedValue = lblRouteId.Text;
                btnSubmit.Text = "Update";
                hfiid.Value = e.CommandArgument.ToString();

                foreach (RepeaterItem item in Repeater1.Items)
                {
                    HtmlTableRow row = (HtmlTableRow)item.FindControl("row");
                    row.Attributes["style"] = "background-color:white";
                }

                RepeaterItem selectitem = (RepeaterItem)(((LinkButton)e.CommandSource).NamingContainer);
                HtmlTableRow currentrow = (HtmlTableRow)selectitem.FindControl("row");
                currentrow.Attributes["style"] = "background-color:#ffe680";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                Label lblIsActive = e.Item.FindControl("lblIsActive") as Label;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds2 = objdb.ByProcedure("USP_Adv_Adv_Mst_Area",
                            new string[] { "flag", "Adv_AreaId", "IsActive", "CreatedBy", "CreatedByIP" },
                            new string[] { "4", e.CommandArgument.ToString(), lblIsActive.Text, objdb.createdBy(), IPAddress }, "dataset");

                if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    ds2.Dispose();
                    GetRouteandAreaDetails();
                    GetAreaDetails();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error : Edit for Delete", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        ddlRoute.SelectedIndex = 0;
        ddlAreaSearch.SelectedIndex = 0;

    }
}