using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//CODE CHANGES START BY CHINMAY ON 11-JUL-2019
using System.Text;
//CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019

public partial class mis_filetracking_FTDashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Office_ID"] != null)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropdown();
                    ds = objdb.ByProcedure("SpFTDashboard", new string[] { "flag" }, new string[] { "2" }, "dataset");
                    if (ds != null && ds.Tables.Count != 0)
                    {
                        lbltotalFiles.Text = ds.Tables[0].Rows[0]["TotalFiles"].ToString();
                        lblTotalInward.Text = ds.Tables[1].Rows[0]["TotalInward"].ToString();
                        lblTotalOutward.Text = ds.Tables[2].Rows[0]["TotalOutward"].ToString();
                    }
                    if (ddlOfficeName.SelectedIndex > 0)
                    {
                        FillGrid();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
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
            ds = objdb.ByProcedure("SpFTDashboard", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                int InwardFile = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("InwardFile"));
                int createfiles = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CreteFiles"));
                int FileOnMyDesk = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("FilesOnMyDesk"));
                int Outward = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OutwardLetter"));
                GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[2].Text = "<b>" + InwardFile.ToString() + "</b>";
                GridView1.FooterRow.Cells[3].Text = "<b>" + createfiles.ToString() + "</b>";
                GridView1.FooterRow.Cells[4].Text = "<b>" + FileOnMyDesk.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + Outward.ToString() + "</b>";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
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
    //protected void lblInwardFiles_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton InwardFiles = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lblInwardFiles");
    //        string Emp_ID = InwardFiles.ToolTip.ToString();
    //        string Parameter = "Inward";
    //        string QueryString = "FTDashboardDetail.aspx?Emp_ID=" + objdb.Encrypt(Emp_ID) + "&Parameter=" + objdb.Encrypt(Parameter);
    //        Response.Write("<script>");
    //        Response.Write("window.open('" + QueryString + "','_blank')");
    //        Response.Write("</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void lblcreateFiles_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton CreateFiles = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lblcreateFiles");
    //        string Emp_ID = CreateFiles.ToolTip.ToString();
    //        string Parameter = "Create";
    //        string QueryString = "FTDashboardDetail.aspx?Emp_ID=" + objdb.Encrypt(Emp_ID) + "&Parameter=" + objdb.Encrypt(Parameter);
    //        Response.Write("<script>");
    //        Response.Write("window.open('" + QueryString + "','_blank')");
    //        Response.Write("</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void lblOnMyDesk_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton OnMyDesk = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lblOnMyDesk");
    //        string Emp_ID = OnMyDesk.ToolTip.ToString();
    //        string Parameter = "OnMyDesk";
    //        string QueryString = "FTDashboardDetail.aspx?Emp_ID=" + objdb.Encrypt(Emp_ID) + "&Parameter=" + objdb.Encrypt(Parameter);
    //        Response.Write("<script>");
    //        Response.Write("window.open('" + QueryString + "','_blank')");
    //        Response.Write("</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvRow = GridView1.Rows[index];

        if (e.CommandName == "Inward")
        {
            Label lblEmpID = (Label)gvRow.FindControl("lblEmpID");
            string strEmpID = objdb.Encrypt(lblEmpID.Text);

            string url = "FTDashboardDetail.aspx?Emp_ID=" + strEmpID + "&Parameter=Inward";

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        if (e.CommandName == "Create")
        {
            Label lblEmpID = (Label)gvRow.FindControl("lblEmpID");
            string strEmpID = objdb.Encrypt(lblEmpID.Text);

            string url = "FTDashboardDetail.aspx?Emp_ID=" + strEmpID + "&Parameter=Create";

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        if (e.CommandName == "MyDesk")
        {
            Label lblEmpID = (Label)gvRow.FindControl("lblEmpID");
            string strEmpID = objdb.Encrypt(lblEmpID.Text);

            string url = "FTDashboardDetail.aspx?Emp_ID=" + strEmpID + "&Parameter=OnMyDesk";

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        if (e.CommandName == "Outword")
        {
            Label lblEmpID = (Label)gvRow.FindControl("lblEmpID");
            string strEmpID = objdb.Encrypt(lblEmpID.Text);

            string url = "FTDashboardDetail.aspx?Emp_ID=" + strEmpID + "&Parameter=Outward";

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }
    //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019
}