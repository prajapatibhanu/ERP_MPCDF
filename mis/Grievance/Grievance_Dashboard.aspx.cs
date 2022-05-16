using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Grievance_Grievance_Dashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                }
            }

            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag" }, new string[] { "0" }, "dataset");
            if (ds.Tables.Count != 0)
            {
                //lblOpenGrievance.Text = ds.Tables[0].Rows[0]["OpenGrv"].ToString();
                //lblCloseGrievance.Text = ds.Tables[1].Rows[0]["CloseGrv"].ToString();
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
            ds = objdb.ByProcedure("SpGrvDashboard", new string[] { "flag", "Office_ID", "Application_GrievanceType" }, new string[] { "1", ViewState["Office_ID"].ToString(), ddlGrvType.SelectedItem.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int OpenGrv = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenGrv"));
                int CloseGrv = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseGrv"));
                GridView1.FooterRow.Cells[1].Text = "| TOTAL |";
                GridView1.FooterRow.Cells[2].Text = "<b>" + OpenGrv.ToString() + "</b>";
                GridView1.FooterRow.Cells[3].Text = "<b>" + CloseGrv.ToString() + "</b>";
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lblOpenGrv_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton OpenGrv = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lblOpenGrv");
            string Office_ID = OpenGrv.ToolTip.ToString();
            Session["UserType"] = ddlGrvType.SelectedItem.Text;
            string Parameter = "Open";
            string QueryString = "GrvDashboardDetail.aspx?Office_ID=" + objdb.Encrypt(Office_ID) + "&Parameter=" + objdb.Encrypt(Parameter);
            Response.Write("<script>");
            Response.Write("window.open('" + QueryString + "','_blank')");
            Response.Write("</script>");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lblCloseGrv_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton CloseGrv = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lblCloseGrv");
            string Office_ID = CloseGrv.ToolTip.ToString();
            Session["UserType"] = ddlGrvType.SelectedValue.ToString();
            string Parameter = "Close";
            string QueryString = "GrvDashboardDetail.aspx?Office_ID=" + objdb.Encrypt(Office_ID) + "&Parameter=" + objdb.Encrypt(Parameter);
            Response.Write("<script>");
            Response.Write("window.open('" + QueryString + "','_blank')");
            Response.Write("</script>");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}