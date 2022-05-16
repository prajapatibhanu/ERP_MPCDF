using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Masters_TransporterRouteHeadMapping : System.Web.UI.Page
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
                FillRouteHead();
                FillSearchRouteHead();
                GetRuralTransporter();
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
    protected void GetRuralTransporter()
    {
        try
        {
            ddlTransporter.DataTextField = "Contact_Person";
            ddlTransporter.DataValueField = "TransporterId";
            ddlTransporter.DataSource = objdb.ByProcedure("USP_Mst_TransporterMaster",
                   new string[] { "flag", "Office_ID", "AreaId" },
                   new string[] { "5", objdb.Office_ID(), "2" }, "dataset");
            ddlTransporter.DataBind();
            ddlTransporter.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
    }
    protected void FillRouteHead()
    {
        try
        {
            ddlHeadRoute.DataTextField = "RouteHeadName";
            ddlHeadRoute.DataValueField = "RouteHeadId";
            ddlHeadRoute.DataSource = objdb.ByProcedure("USP_Mst_TransporterandRouteHeadMapping", new string[] { "Flag", "Office_ID" },
                        new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlHeadRoute.DataBind();
            ddlHeadRoute.Items.Insert(0, new ListItem("All", "0"));

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
            ddlSearchRouteHead.DataTextField = "RouteHeadName";
            ddlSearchRouteHead.DataValueField = "RouteHeadId";
            ddlSearchRouteHead.DataSource = objdb.ByProcedure("USP_Mst_TransporterandRouteHeadMapping", new string[] { "Flag", "Office_ID" },
                        new string[] { "1", objdb.Office_ID() }, "dataset");
            ddlSearchRouteHead.DataBind();
            ddlSearchRouteHead.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TransporterandRouteHeadMappingDetails()
    {
        try
        {

            GridView1.DataSource = objdb.ByProcedure("USP_Mst_TransporterandRouteHeadMapping",
                         new string[] { "flag", "Office_ID", "RouteHeadId" },
                        new string[] { "3", objdb.Office_ID(), ddlSearchRouteHead.SelectedValue }, "dataset");
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
                        ds6 = objdb.ByProcedure("USP_Mst_TransporterandRouteHeadMapping",
                                    new string[] { "flag", "TransandRouteHeadId", "CreatedBy", "CreatedByIP", "IsActive" },
                                    new string[] { "4", e.CommandArgument.ToString(), objdb.createdBy(), IPAddress, isactive }, "TableSave");


                        if (ds6.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds6.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            TransporterandRouteHeadMappingDetails();
                            FillRouteHead();
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
              

                
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds5 = objdb.ByProcedure("USP_Mst_TransporterandRouteHeadMapping",
                              new string[] { "flag", "TransporterId", "RouteHeadId", "Office_ID", "CreatedBy", "CreatedByIP" },
                             new string[] { "2",ddlTransporter.SelectedValue, ddlHeadRoute.SelectedValue, objdb.Office_ID(), objdb.createdBy(), IPAddress }, "dataset");

                    if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        FillRouteHead();
                    }
                    else
                    {
                        string error = ds5.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
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
        ddlHeadRoute.SelectedIndex = 0;
        ddlTransporter.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            TransporterandRouteHeadMappingDetails();

        }
    }
}