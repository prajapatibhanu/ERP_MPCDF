using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_EngneeringDepartment_Head_SubHeadMapping : System.Web.UI.Page
{
    DataSet ds1, ds2, ds5, ds6 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillHead();
                //FillSearchRouteHead();
                FillSubHead();
                RouteHeadandRouteMappingDetails();
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

    protected void FillHead()
    {
        try
        {
            string office_ID = objdb.Office_ID();
            string createdBy = objdb.createdBy();

            ddlHeadName.DataTextField = "ENGHeadName";
            ddlHeadName.DataValueField = "ENGHeadId";
            ddlHeadName.DataSource = objdb.ByProcedure("USP_Mst_ENGHead", new string[] { "Flag", "Office_ID", "CreatedBy" },
                        new string[] { "5", objdb.Office_ID().ToString(), objdb.createdBy().ToString() }, "dataset");
            ddlHeadName.DataBind();
            ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSubHead()
    {
        try
        {
            ddlSubHeadName.DataTextField = "ENGSubHeadName";
            ddlSubHeadName.DataValueField = "ENGSubHeadId";
            ddlSubHeadName.DataSource = objdb.ByProcedure("USP_Mst_ENGSubHead",
                new string[] { "Flag", "Office_ID", "CreatedBy" },
                           new string[] { "5", objdb.Office_ID(), objdb.createdBy() }, "dataset");
            ddlSubHeadName.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void FillSearchRouteHead()
    {
        try
        {
            ddlSearchRouteHead.DataTextField = "ENGHeadName";
            ddlSearchRouteHead.DataValueField = "ENGHeadId";
            ddlSearchRouteHead.DataSource = objdb.ByProcedure("USP_Mst_RouteHead", new string[] { "Flag", "Office_ID" },
                        new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlSearchRouteHead.DataBind();
            ddlSearchRouteHead.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void RouteHeadandRouteMappingDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("USP_Mst_ENGHeadandSubHeadMapping",
                         new string[] { "flag", "Office_ID", "ENGHeadId", "CreatedBy" },
                        new string[] { "3", objdb.Office_ID(), ddlSearchRouteHead.SelectedValue,objdb.createdBy() }, "dataset");
            GridView1.DataBind();
            GetDatatableHeaderDesign();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4:", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblIsActive = (Label)row.FindControl("lblIsActive");

                        string isactive = "";
                        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                        if (lblIsActive.Text == "True")
                        {
                            isactive = "0";
                        }
                        else
                        {
                            isactive = "1";
                        }
                        lblMsg.Text = string.Empty;
                        ds6 = objdb.ByProcedure("USP_Mst_ENGHeadandSubHeadMapping",
                                    new string[] { "flag", "ENGHeadandSubHeadId", "CreatedBy", "CreatedByIP", "IsActive" },
                                    new string[] { "4", e.CommandArgument.ToString(), objdb.createdBy(), IPAddress, isactive }, "TableSave");


                        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            FillHead();
                            //FillSearchRouteHead();
                            FillSubHead();
                            RouteHeadandRouteMappingDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  delete record: " + ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
        }
    }
    private void InsertSSDistMapping()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataRow dr;
                DataTable ENGSubHeadID = new DataTable();
                ENGSubHeadID.Columns.Add("ENGSubHeadId", typeof(string));


                dr = ENGSubHeadID.NewRow();
                foreach (ListItem item in ddlSubHeadName.Items)
                {
                    if (item.Selected)
                    {
                        dr[0] = item.Value;
                        ENGSubHeadID.Rows.Add(dr.ItemArray);
                    }
                }

                if (ENGSubHeadID.Rows.Count > 0)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds5 = objdb.ByProcedure("USP_Mst_ENGHeadandSubHeadMapping",
                              new string[] { "flag", "ENGHeadId","ENGSubHeadId", "Office_ID", "CreatedBy", "CreatedByIP" },
                             new string[] { "2", ddlHeadName.SelectedValue,ddlSubHeadName.SelectedValue, objdb.Office_ID(), objdb.createdBy(), IPAddress },
                               new string[] { "type_ENGHeadandSubHeadMapping" },
                               new DataTable[] { ENGSubHeadID }, "dataset");

                    if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        FillSubHead();
                        FillHead();
                        RouteHeadandRouteMappingDetails();
                    }
                    else
                    {
                        string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Data not found ");
                }

                if (ENGSubHeadID != null)
                {
                    ENGSubHeadID.Dispose();
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  insert record: " + ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertSSDistMapping();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDatatableHeaderDesign();
        ddlHeadName.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            RouteHeadandRouteMappingDetails();

        }
    }
}