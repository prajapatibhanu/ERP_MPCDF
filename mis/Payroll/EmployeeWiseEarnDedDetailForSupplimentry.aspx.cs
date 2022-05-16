using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_EmployeeWiseEarnDedDetailForSupplimentry : System.Web.UI.Page
{
    DataSet ds1=new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Emp_ID"] != null && Request.QueryString["EmpSupplimentryID"] != null)
        {
            FillEarning();
        }
        else
        {
            Response.Redirect("PayrollEmpSupplimentryDetail.aspx");
        }
    }
    protected void FillEarning()
    {
        string sid = objdb.Decrypt(Request.QueryString["EmpSupplimentryID"].ToString());
        string empid = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
        ds1 = objdb.ByProcedure("USP_tblPayrollEmpSupplimentryDetail", new string[] { "flag", "EmpSupplimentryID", "Emp_ID" }, new string[] { "4", sid, empid }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0 && ds1.Tables[1].Rows.Count != 0 && ds1.Tables[2].Rows.Count != 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered table-striped'>");
            sb.Append("<tr>");
            sb.Append("<th>Employee Name</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</td>");
            sb.Append("<th>Year</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["CurrentYear"].ToString() + "</td>");
            sb.Append("<th>Month</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["CurrentMonth"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th>From Date</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["Fromdate"].ToString() + "</td>");
            sb.Append("<th>To Date</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["ToDate"].ToString() + "</td>");
            sb.Append("<th>Basic Salary</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["BasicSalary"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th>Total Earning</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["TotalEarning"].ToString() + "</td>");
            sb.Append("<th>Policy</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["Policy"].ToString() + "</td>");
            sb.Append("<th>Total Deduction</th>");
            sb.Append("<td>" + ds1.Tables[0].Rows[0]["TotalDeduction"].ToString() + "</td>");

            sb.Append("</tr>");
            sb.Append("</table>");
            DivDetail.InnerHtml = sb.ToString();
            DivDetail.Visible = true;


            RepeaterEarn.DataSource = ds1.Tables[1];
            RepeaterEarn.DataBind();

            RepeaterDed.DataSource = ds1.Tables[2];
            RepeaterDed.DataBind();

            txtNetPayment.Text = ds1.Tables[0].Rows[0]["NetPaymentAmount"].ToString();
        }

        if (ds1 != null) { ds1.Dispose(); }
    }
}