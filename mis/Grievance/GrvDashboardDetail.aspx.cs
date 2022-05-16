using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Grievance_GrvDashboardDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Parameter1"] != null)
        {
            ViewState["Parameter1"] = Request.QueryString["Parameter1"].ToString();
            FillDetail();
        }
        else if (Request.QueryString["Office_ID"] != null && Request.QueryString["Parameter"] != null)
        {
            ViewState["Office_ID"] = objdb.Decrypt(Request.QueryString["Office_ID"].ToString());
            ViewState["UserType"] = Session["UserType"].ToString();
            ViewState["Parameter"] = objdb.Decrypt(Request.QueryString["Parameter"].ToString());
            FillGrid();
        }
        else
        {
            Response.Redirect("Grievance_Dashboard.aspx");
        }
    }
    protected void FillDetail()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            if (ViewState["Parameter1"].ToString() == "OpenGrv")
            {
                ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "4" }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "OPEN GRIEVANCE";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "OPEN GRIEVANCE";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "CloseGrv")
            {
                ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "4" }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CLOSE GRIEVANCE";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CLOSE GRIEVANCE";
                }
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                lblMsg.Text = "";
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
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            if (ViewState["Parameter"].ToString() == "Open")
            {
                ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType" }, new string[] { "6", ViewState["Office_ID"].ToString(), ViewState["UserType"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "OPEN GRIEVANCE";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "OPEN GRIEVANCE";
                }
            }
            else if (ViewState["Parameter"].ToString() == "Close")
            {
                ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType" }, new string[] { "6", ViewState["Office_ID"].ToString(), ViewState["UserType"].ToString() }, "dataset");
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CLOSE GRIEVANCE";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblMsg.Text = "CLOSE GRIEVANCE";
                }
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}