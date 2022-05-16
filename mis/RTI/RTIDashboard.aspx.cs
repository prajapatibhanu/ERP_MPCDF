using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_RTIDashboard_RTIDashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDetail();
                    //FillGridApp_UserType();
                    FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID" }, new string[] { "8", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0)
            {
                lbltotalRTI.Text = ds.Tables[0].Rows[0]["TotalRTI"].ToString();
                lblOpenRTI.Text = ds.Tables[1].Rows[0]["OpenRTI"].ToString();
                lblcloseRTI.Text = ds.Tables[2].Rows[0]["CloseRTI"].ToString();
                lblTotalAppeal.Text = ds.Tables[3].Rows[0]["TotalFirstAppeal"].ToString();
                lblOpenAppeal.Text = ds.Tables[4].Rows[0]["OpenFirstAppeal"].ToString();
                lblCloseAppeal.Text = ds.Tables[5].Rows[0]["CloseFirstAppeal"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridApp_UserType()
    {
        try
        {
            //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
            //DivTable.InnerHtml = "";
            //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019

            StringBuilder htmlStr = new StringBuilder();
            ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "App_UserType", "RTI_ByOfficeID" }, new string[] { "11", ddlApp_UserType.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int OpenRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenRTI"));
                int CloseRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseRTI"));
                int TotalAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalFirstAppeal"));
                int OpenAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenFirstAppeal"));
                int CloseFirstAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseFirstAppeal"));
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    GridView1.FooterRow.Visible = true;
                }
                else
                {
                    GridView1.FooterRow.Visible = false;
                }
                GridView1.FooterRow.Cells[3].Text = "<b>| TOTAL |</b> ";
                GridView1.FooterRow.Cells[4].Text = "<b>" + OpenRTI.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + CloseRTI.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + TotalAppeal.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + OpenAppeal.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + CloseFirstAppeal.ToString() + "</b>";
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

                //htmlStr.Append("<table  id='DetailGridNew' class='datatable table table-hover table-bordered' >");
                //htmlStr.Append("<thead>");
                //htmlStr.Append("<tr>");
                //htmlStr.Append("<th>S.No.</th>");
                //htmlStr.Append("<th>STATE NAME</th>");
                //htmlStr.Append("<th>DISTRICT NAME</th>");
                //htmlStr.Append("<th>OFFICE NAME</th>");
                //htmlStr.Append("<th>OPEN RTI</th>");
                //htmlStr.Append("<th>CLOSED RTI</th>");
                //htmlStr.Append("<th>TOTAL FIRST APPEAL</th>");
                //htmlStr.Append("<th>OPEN FIRST APPEAL</th>");
                //htmlStr.Append("<th>CLOSED FIRST APPEAL</th>");
                //htmlStr.Append("</tr>");
                //htmlStr.Append("</thead>");

                //htmlStr.Append("<tbody>");

                //string Open = objdb.Encrypt("Open");
                //string Close = objdb.Encrypt("Close");
                //string Total = objdb.Encrypt("Total");
                //string AppealOpen = objdb.Encrypt("AppealOpen");
                //string AppealClose = objdb.Encrypt("AppealClose");

                //int Count = ds.Tables[0].Rows.Count;
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<tr>");
                //    htmlStr.Append("<td>" + (i + 1).ToString() + "</td>");
                //    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["State_Name"].ToString() + "</td>");
                //    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["District_Name"].ToString() + "</td>");
                //    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                //    string Office_ID = objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString());
                //    string App_UserType = objdb.Encrypt(ds.Tables[0].Rows[i]["App_UserType"].ToString());
                // //   string App_UserType = objdb.Encrypt(ds.Tables[0].Rows[i]["App_UserType"].ToString());


                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-warning' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + Open + "' target='_self'>" + ds.Tables[0].Rows[i]["OpenRTI"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-success' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + Close + "' target='_self'>" + ds.Tables[0].Rows[i]["CloseRTI"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-warning' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + Total + "' target='_self'>" + ds.Tables[0].Rows[i]["TotalFirstAppeal"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-success' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + AppealOpen + "' target='_self'>" + ds.Tables[0].Rows[i]["OpenFirstAppeal"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-warning' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + AppealClose + "' target='_self'>" + ds.Tables[0].Rows[i]["CloseFirstAppeal"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("</tr>");
                //}


                //int OpenRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenRTI"));
                //int CloseRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseRTI"));
                //int TotalAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalFirstAppeal"));
                //int OpenAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenFirstAppeal"));
                //int CloseAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseFirstAppeal"));

                //htmlStr.Append("</tbody>");
                //if (ViewState["Office_ID"].ToString() == "1")
                //{
                //    htmlStr.Append("<tfoot>");
                //    htmlStr.Append("<tr>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td><b>| TOTAL |</b></td>");
                //    htmlStr.Append("<td><b>" + OpenRTI.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + CloseRTI.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + TotalAppeal.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + OpenAppeal.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + CloseAppeal.ToString() + "</b></td>");
                //    htmlStr.Append("</tr>");
                //    htmlStr.Append("</tfoot>");
                //}

                //htmlStr.Append("</table>");
                //DivTable.InnerHtml = htmlStr.ToString();
                //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019
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
            //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
            //DivTable.InnerHtml = "";
            //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019

            StringBuilder htmlStr = new StringBuilder();
            ds = objdb.ByProcedure("SpRTIDashboard", new string[] { "flag", "RTI_ByOfficeID" }, new string[] { "9", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int OpenRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenRTI"));
                int CloseRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseRTI"));
                int TotalAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalFirstAppeal"));
                int OpenAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenFirstAppeal"));
                int CloseFirstAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseFirstAppeal"));
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    GridView1.FooterRow.Visible = true;
                }
                else
                {
                    GridView1.FooterRow.Visible = false;
                }
                GridView1.FooterRow.Cells[3].Text = "<b>| TOTAL |</b> ";
                GridView1.FooterRow.Cells[4].Text = "<b>" + OpenRTI.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + CloseRTI.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + TotalAppeal.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + OpenAppeal.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + CloseFirstAppeal.ToString() + "</b>";
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                //htmlStr.Append("<table  id='DetailGridNew' class='datatable table table-hover table-bordered' >");
                //htmlStr.Append("<thead>");
                //htmlStr.Append("<tr>");
                //htmlStr.Append("<th>S.No.</th>");
                //htmlStr.Append("<th>STATE NAME</th>");
                //htmlStr.Append("<th>DISTRICT NAME</th>");
                //htmlStr.Append("<th>OFFICE NAME</th>");
                //htmlStr.Append("<th>OPEN RTI</th>");
                //htmlStr.Append("<th>CLOSED RTI</th>");
                //htmlStr.Append("<th>TOTAL FIRST APPEAL</th>");
                //htmlStr.Append("<th>OPEN FIRST APPEAL</th>");
                //htmlStr.Append("<th>CLOSED FIRST APPEAL</th>");
                //htmlStr.Append("</tr>");
                //htmlStr.Append("</thead>");

                //htmlStr.Append("<tbody>");

                //string Open = objdb.Encrypt("Open");
                //string Close = objdb.Encrypt("Close");
                //string Total = objdb.Encrypt("Total");
                //string AppealOpen = objdb.Encrypt("AppealOpen");
                //string AppealClose = objdb.Encrypt("AppealClose");


                //int Count = ds.Tables[0].Rows.Count;
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<tr>");
                //    htmlStr.Append("<td>" + (i + 1).ToString() + "</td>");
                //    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["State_Name"].ToString() + "</td>");
                //    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["District_Name"].ToString() + "</td>");
                //    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                //    string Office_ID = objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString());
                //    string App_UserType = objdb.Encrypt(ds.Tables[0].Rows[i]["App_UserType"].ToString());


                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-warning text-center' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + Open + "' target='_self'>" + ds.Tables[0].Rows[i]["OpenRTI"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-success' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + Close + "' target='_self'>" + ds.Tables[0].Rows[i]["CloseRTI"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-warning' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + Total + "' target='_self'>" + ds.Tables[0].Rows[i]["TotalFirstAppeal"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-success' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + AppealOpen + "' target='_self'>" + ds.Tables[0].Rows[i]["OpenFirstAppeal"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("<td>");
                //    htmlStr.Append("<a  class='label label-warning' href='RTIDashboardDetail.aspx?RTI_ByOfficeID=" + Office_ID + "&UserType=" + App_UserType + "&Parameter=" + AppealClose + "' target='_self'>" + ds.Tables[0].Rows[i]["CloseFirstAppeal"].ToString() + "</a>");
                //    htmlStr.Append("</td>");

                //    htmlStr.Append("</tr>");
                //}
                //int OpenRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenRTI"));
                //int CloseRTI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseRTI"));
                //int TotalAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalFirstAppeal"));
                //int OpenAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenFirstAppeal"));
                //int CloseAppeal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseFirstAppeal"));
                //htmlStr.Append("</tbody>");
                //if (ViewState["Office_ID"].ToString() == "1")
                //{
                //    htmlStr.Append("<tfoot>");
                //    htmlStr.Append("<tr>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td><b>| TOTAL |</b></td>");
                //    htmlStr.Append("<td><b>" + OpenRTI.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + CloseRTI.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + TotalAppeal.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + OpenAppeal.ToString() + "</b></td>");
                //    htmlStr.Append("<td><b>" + CloseAppeal.ToString() + "</b></td>");
                //    htmlStr.Append("</tr>");
                //    htmlStr.Append("</tfoot>");
                //}

                //htmlStr.Append("</table>");
                //DivTable.InnerHtml = htmlStr.ToString();
                //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019
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
            if (ddlApp_UserType.SelectedValue == "0")
            {
                FillGrid();
            }
            else
            {
                FillGridApp_UserType();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Open = objdb.Encrypt("Open");
        string Close = objdb.Encrypt("Close");
        string Total = objdb.Encrypt("Total");
        string AppealOpen = objdb.Encrypt("AppealOpen");
        string AppealClose = objdb.Encrypt("AppealClose");

        int index = Convert.ToInt32(e.CommandArgument.ToString());
        GridViewRow gvRow = GridView1.Rows[index];

        if (e.CommandName == "Open")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            Label lblApp_UserType = (Label)gvRow.FindControl("lblApp_UserType");
            string OfficeID = objdb.Encrypt(lblOfficeID.Text);
            string App_UserType = objdb.Encrypt(lblApp_UserType.Text);

            string url = "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + OfficeID + "&UserType=" + App_UserType + "&Parameter=" + Open;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_self');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else if (e.CommandName == "Close")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            Label lblApp_UserType = (Label)gvRow.FindControl("lblApp_UserType");
            string OfficeID = objdb.Encrypt(lblOfficeID.Text);
            string App_UserType = objdb.Encrypt(lblApp_UserType.Text);

            string url = "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + OfficeID + "&UserType=" + App_UserType + "&Parameter=" + Close;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_self');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else if (e.CommandName == "Total")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            Label lblApp_UserType = (Label)gvRow.FindControl("lblApp_UserType");
            string OfficeID = objdb.Encrypt(lblOfficeID.Text);
            string App_UserType = objdb.Encrypt(lblApp_UserType.Text);

            string url = "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + OfficeID + "&UserType=" + App_UserType + "&Parameter=" + Total;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_self');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else if (e.CommandName == "AppealOpen")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            Label lblApp_UserType = (Label)gvRow.FindControl("lblApp_UserType");
            string OfficeID = objdb.Encrypt(lblOfficeID.Text);
            string App_UserType = objdb.Encrypt(lblApp_UserType.Text);

            string url = "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + OfficeID + "&UserType=" + App_UserType + "&Parameter=" + AppealOpen;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_self');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else if (e.CommandName == "AppealClose")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            Label lblApp_UserType = (Label)gvRow.FindControl("lblApp_UserType");
            string OfficeID = objdb.Encrypt(lblOfficeID.Text);
            string App_UserType = objdb.Encrypt(lblApp_UserType.Text);

            string url = "RTIDashboardDetail.aspx?RTI_ByOfficeID=" + OfficeID + "&UserType=" + App_UserType + "&Parameter=" + AppealClose;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_self');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }
    //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019
}