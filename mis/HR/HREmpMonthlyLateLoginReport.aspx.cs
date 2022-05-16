﻿using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

public partial class mis_HR_HREmpMonthlyLateLoginReport : System.Web.UI.Page
{
    DataSet ds, dsRecord;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {

                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillYear();
                ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("Select Office", "0"));
                }
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    //ddlOldOffice.Attributes.Add("readonly", "readonly");
                    ddlOffice.Enabled = true;
                }
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillYear()
    {
        //try
        //{
        //    ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddlYear.DataSource = ds;
        //        ddlYear.DataTextField = "Year";
        //        ddlYear.DataValueField = "Year";
        //        ddlYear.DataBind();
        //        ddlYear.Items.Insert(0, new ListItem("Select Year", "0"));
        //    }
        //    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        //    //ddlYear.Enabled = false;
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }

    protected void FillDetail()
    {
        try
        {


            lblMsg.Text = "";
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
            lblGridHeading.Text = "<h5><b>Office:</b> " + ddlOffice.SelectedItem.ToString() + ",  <b>Month:</b> " + ddlMonth.SelectedItem.ToString() + ",  <b>Year:</b> " + ddlYear.SelectedItem.ToString() + "</h5><p><b>Report Generation Time:</b>  " + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt") + ", <b>IP: </b>" + Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString() + "</p>";

            string msg = "";

            if (ddlOffice.SelectedIndex == 0)
            {
                msg += "Please Select Office. \\n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Please Select Year. \\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Please Select Month. \\n";
            }
            if (msg.Trim() == "")
            {


                ds = objdb.ByProcedure("SpHREmpMonthlyLoginLogoutReport", new string[] { "flag", "Month", "Year", "Office_ID" }, new string[] { "1", ddlMonth.SelectedValue, ddlYear.SelectedValue, ddlOffice.SelectedValue }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //GridView1.DataSource = ds.Tables[0];
                    //GridView1.DataBind();
                    //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //GridView1.UseAccessibleHeader = true;

                    int colCount = ds.Tables[0].Columns.Count;
                    int rowCount = ds.Tables[0].Rows.Count;
                    StringBuilder htmlStr = new StringBuilder();
                    htmlStr.Append("<table  id='DetailGrid' class='dataTable table table-hover table-bordered' >");
                    htmlStr.Append("<thead>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th>Emp_Name</th>");
                    int i = 0; int Col = 1;
                    for (i = 3; i < colCount; i++)
                    {
                        htmlStr.Append("<th>" + Col.ToString() + "</th>");
                        Col++;
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append(" </thead>");
                    htmlStr.Append("<tbody>");
                                      
                    int j = 0;
                    for (j = 0; j < rowCount; j++)
                    {
                        htmlStr.Append("<tr>");
                        for (i = 2; i < colCount; i++)
                        {
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[j][i].ToString() + "</td>");
                        }
                        htmlStr.Append("</tr>");
                    }
                   

                 
                    htmlStr.Append(" </tbody>");
                    htmlStr.Append("</table>");


                    DivTable.InnerHtml = htmlStr.ToString();

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillDetail();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            for (int i = 0; i < e.Row.Cells.Count; i++)
    //            {
    //                //if (e.Row.Cells[i].Text == "General Holiday")
    //                //{
    //                //    e.Row.Cells[i].BackColor = Color.LightSkyBlue;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "Gen-Holiday";
    //                //}
    //                //if (e.Row.Cells[i].Text == "Tour" || e.Row.Cells[i].Text == "On Tour")
    //                //{
    //                //    e.Row.Cells[i].BackColor = Color.CornflowerBlue;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "Tour";
    //                //}
    //                //if (e.Row.Cells[i].Text == "Tuesday Holiday")
    //                //{
    //                //    e.Row.Cells[i].BackColor = Color.LightSkyBlue;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "Tue-Holiday";
    //                //}
    //                //if (e.Row.Cells[i].Text == "Sunday Holiday" || e.Row.Cells[i].Text == "SUNDAY")
    //                //{
    //                //    e.Row.Cells[i].BackColor = Color.LightSkyBlue;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "Sun-Holiday";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "II-Saturday Holiday")
    //                //{
    //                //    e.Row.Cells[i].BackColor = Color.LightSkyBlue;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "II-Sat";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "III-Saturday Holiday")
    //                //{
    //                //    e.Row.Cells[i].BackColor = Color.LightSkyBlue;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "III-Sat";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "Present")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.ForestGreen;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "P";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "Absent")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Red;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "A";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "Medical Leave")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Yellow;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "ML";
    //                //}

    //                //else if (e.Row.Cells[i].Text == "Late Login")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.BlueViolet;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "Late";
    //                //}

    //                //else if (e.Row.Cells[i].Text == "Casual Leave")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Yellow;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "CL";
    //                //}

    //                //else if (e.Row.Cells[i].Text == "Earn Leave")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Yellow;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "EL";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "Optional Leave")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Yellow;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "OL";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "Second-Half Leave")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Orange;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "S-Half Day";
    //                //}
    //                //else if (e.Row.Cells[i].Text == "First-half Leave")
    //                //{
    //                //    //e.Row.Cells[i].ForeColor = Color.White;
    //                //    e.Row.Cells[i].BackColor = Color.Orange;
    //                //    e.Row.Cells[i].Font.Bold = true;
    //                //    e.Row.Cells[i].Text = "F-Half Day";
    //                //}                    
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
}