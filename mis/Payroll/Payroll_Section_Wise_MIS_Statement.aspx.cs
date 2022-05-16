using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Payroll_Payroll_Section_Wise_MIS_Statement : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillFinancialYear();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "23" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_Name.DataSource = ds;
                ddlOffice_Name.DataTextField = "Office_Name";
                ddlOffice_Name.DataValueField = "Office_ID";
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void FillFinancialYear()
    {
        try
        {

            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetSectionWiseMIS_StatementReport()
    {
        DataSet ds5 = new DataSet();

        try
        {
            lblMsg.Text = string.Empty;
            ds5 = objdb.ByProcedure("USP_Payroll_SectionWise_MIS_Statement",
            new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear" },
           new string[] { "1", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, 
             }, "dataset");

            if (ds5.Tables[0].Rows.Count > 0)
            {
                pnlshow.Visible = true;
                StringBuilder sb = new StringBuilder();

                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8' style='text-align: center;border:1px solid black;'><b>" + ddlOffice_Name.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='6' style='text-align: left;border:1px solid black;'>Section wise MIS Statement<b></b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Month <b>:" + ddlMonth.SelectedItem.Text + "</b></td>");
                sb.Append("<td style='text-align: left;border:1px solid black;'>Year <b>:" + ddlFinancialYear.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table class='table1' style='width:100%; height:100%'>");

                sb.Append("<thead>");
                int Count1 = ds5.Tables[0].Rows.Count;
                sb.Append("<tr>");
                sb.Append("<td style='border:1px solid black;break-after: auto'><b>S.No</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>SECTION</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>NO. OF EMP</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>FIX TA</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>GROSS </br>(a)</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>E.P.F </br>(b)</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>E.S.I CONTRIBUTION </br>(c)</b></td>");
                sb.Append("<td style='border:1px solid black;overflow-wrap: break-word;'><b>TOTAL </br>(a+b+c)</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");
                for (int i = 0; i < Count1; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + (i + 1).ToString() + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["SalarySec_No"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["EMP_Count"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["FIXTA"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["GrossSalary"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["EPF"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["ESI"] + "</td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds5.Tables[0].Rows[i]["Total"] + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td colspan='3' style='border:1px solid black;text-align:center;'><b>Total</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToInt64(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<Int64?>("FIXTA") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToInt64(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<Int64?>("GrossSalary") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToInt64(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<Int64?>("EPF") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToInt64(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<Int64?>("ESI") ?? 0)) + "</b></td>");
                sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Convert.ToInt64(ds5.Tables[0].AsEnumerable().Sum(r => r.Field<Int64?>("Total") ?? 0)) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                div_page_content.Visible = true;
                div_page_content.InnerHtml = sb.ToString();
                Print.InnerHtml = sb.ToString();
            }
            else
            {
                pnlshow.Visible = false;
                div_page_content.Visible = false;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetSectionWiseMIS_StatementReport();
        }
      
    }

    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlOffice_Name.SelectedItem.Text + "-" + ddlMonth.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + "-" + "Section_iseMIS_Statement.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}