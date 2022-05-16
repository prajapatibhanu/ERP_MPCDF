using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTIDashboard_RTIDashboardDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdnParameterType.Value = "";
                ViewState["RTI_ByOfficeID"] = Session["Office_ID"].ToString();
                if (Request.QueryString["Parameter1"] != null)
                {
                    ViewState["Parameter1"] = Request.QueryString["Parameter1"].ToString();
                    string ReqType = "";
                    if (ViewState["Parameter1"].ToString().Length > 10)
                    {
                        ReqType = ViewState["Parameter1"].ToString().Substring(ViewState["Parameter1"].ToString().Length - 11);
                        hdnParameterType.Value = ReqType;
                    }
                    else
                    {
                        ReqType = ViewState["Parameter1"].ToString().Substring(ViewState["Parameter1"].ToString().Length - 3);
                        hdnParameterType.Value = ReqType;
                    }

                    FillDetail();
                }
                //else if (Request.QueryString["DistrictID"] != null && Request.QueryString["UserType"] != null && Request.QueryString["Parameter"] != null)
                else if (Request.QueryString["RTI_ByOfficeID"] != null && Request.QueryString["UserType"] != null && Request.QueryString["Parameter"] != null)
                {
                    //ViewState["DistrictID"] = objdb.Decrypt(Request.QueryString["DistrictID"].ToString());
                    ViewState["RTI_ByOfficeID"] = objdb.Decrypt(Request.QueryString["RTI_ByOfficeID"].ToString());
                    ViewState["UserType"] = objdb.Decrypt(Request.QueryString["UserType"].ToString());
                    ViewState["Parameter"] = objdb.Decrypt(Request.QueryString["Parameter"].ToString());
                    if (ViewState["UserType"].ToString() == "OtherOrganization")
                    {
                        ViewState["UserType"] = "Other Organization";
                    }
                    FillGrid();
                }
            }
        }

        else
        {
            Response.Redirect("RTIDashboard.aspx");
        }
    }
    protected void FillDetail()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID", "RequestFor" }, new string[] { "10", ViewState["RTI_ByOfficeID"].ToString(), hdnParameterType.Value }, "dataset");
            if (ViewState["Parameter1"].ToString() == "TotalRTI")
            {
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag" }, new string[] { "6" }, "dataset");
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID" }, new string[] { "10", ViewState["RTI_ByOfficeID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " TOTAL RTI ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " TOTAL RTI ";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "OpenRTI")
            {
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag" }, new string[] { "6" }, "dataset");
                // ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID" }, new string[] { "10", ViewState["RTI_ByOfficeID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN RTI ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN RTI ";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "CloseRTI")
            {
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID" }, new string[] { "10", ViewState["RTI_ByOfficeID"].ToString() }, "dataset"); 
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag" }, new string[] { "6" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[2].Rows.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[2];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " CLOSED RTI ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " CLOSED RTI ";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "TotalFirstAppeal")
            {
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag" }, new string[] { "6" }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[3];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " TOTAL FIRST APPEAL ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " TOTAL FIRST APPEAL ";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "OpenFirstAppeal")
            {
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag" }, new string[] { "6" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[4].Rows.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[4];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN FIRST APPEAL ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN FIRST APPEAL ";
                }
            }
            else if (ViewState["Parameter1"].ToString() == "CloseFirstAppeal")
            {
                // ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag" }, new string[] { "6" }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[5];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " CLOSE FIRST APPEAL ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " CLOSED FIRST APPEAL ";
                }
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                lblRTIType.Text = "";
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            if (ViewState["Parameter"].ToString() == "Open")
            {
                //ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "App_District", "App_UserType" }, new string[] { "2", ViewState["DistrictID"].ToString(), ViewState["UserType"].ToString() }, "dataset");
                hdnParameterType.Value = "RTI";
                ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID", "App_UserType", "RequestFor" }, new string[] { "2", ViewState["RTI_ByOfficeID"].ToString(), ViewState["UserType"].ToString(), hdnParameterType.Value }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN RTI ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN RTI ";
                }
            }
            else if (ViewState["Parameter"].ToString() == "Close")
            {
                hdnParameterType.Value = "RTI";
                ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID", "App_UserType", "RequestFor" }, new string[] { "3", ViewState["RTI_ByOfficeID"].ToString(), ViewState["UserType"].ToString(), hdnParameterType.Value }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " CLOSED RTI ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " CLOSED RTI ";
                }
            }
            else if (ViewState["Parameter"].ToString() == "Total")
            {
                hdnParameterType.Value = "FirstAppeal";
                ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID", "App_UserType", "RequestFor" }, new string[] { "4", ViewState["RTI_ByOfficeID"].ToString(), ViewState["UserType"].ToString(), hdnParameterType.Value }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " TOTAL FIRST APPEAL ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " TOTAL FIRST APPEAL ";
                }
            }
            else if (ViewState["Parameter"].ToString() == "AppealOpen")
            {
                hdnParameterType.Value = "FirstAppeal";
                ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID", "App_UserType", "RequestFor" }, new string[] { "5", ViewState["RTI_ByOfficeID"].ToString(), ViewState["UserType"].ToString(), hdnParameterType.Value }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN FIRST APPEAL ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN FIRST APPEAL ";
                }
            }
            else if (ViewState["Parameter"].ToString() == "AppealClose")
            {
                hdnParameterType.Value = "FirstAppeal";
                ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID", "App_UserType", "RequestFor" }, new string[] { "12", ViewState["RTI_ByOfficeID"].ToString(), ViewState["UserType"].ToString(), hdnParameterType.Value }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN FIRST APPEAL ";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    lblRTIType.Text = " OPEN FIRST APPEAL ";
                }
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                lblRTIType.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Hide = objdb.Encrypt("Hide");

        int index = Convert.ToInt32(e.CommandArgument.ToString());
        GridViewRow gvRow = GridView1.Rows[index];

        if (e.CommandName == "View")
        {
            Label lblRequestFor = (Label)gvRow.FindControl("lblRequestFor");
            Label lblRTIID = (Label)gvRow.FindControl("lblRTIID");
            string RTIID = objdb.Encrypt(lblRTIID.Text);
            string RequestFor = objdb.Encrypt(lblRequestFor.Text);

            if (lblRequestFor.Text == "FirstAppeal")
            {
                string url = "FirstAppealReply.aspx?RTI_ID=" + RTIID + "&ShowHide=" + RequestFor;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_self');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else
            {
                string url = "RTIDashboardCommentsDetail.aspx?RTI_ID=" + RTIID + "&ShowHide=" + Hide;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_self');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
        }
    }
}