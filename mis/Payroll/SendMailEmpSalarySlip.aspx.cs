using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;


public partial class mis_Payroll_SendMailEmpSalarySlip : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
	NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        ddlOfficeName.Enabled = true;
                    }
                    else
                    {
                        ddlOfficeName.Enabled = false;
                    }
                    FillDropdown();
                    btnSendSMS.Visible = false;
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
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds.Reset();
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
            lblMsg.Text = "";
            string msg = "";
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name <br/>";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year <br/>";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month <br/>";
            }
            if (ddlEmpType.SelectedIndex == 0)
            {
                msg += "Select Employee Type <br/>";
            }
            if (msg == "")
            {
                FillGrid();
                if (GridView1.Rows.Count > 0)
                {
                    btnSendSMS.Visible = true;
                }
                else
                {
                    btnSendSMS.Visible = false;
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

    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID", "Emp_TypeOfPost" }, new string[] { "17", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), ddlEmpType.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }





    protected void FillSalary(string Emp_ID, string Emp_Email)
    {

        StringBuilder sb = new StringBuilder();
        DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID, ddlOfficeName.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {
            sb.Append("<div class='container'>");
            sb.Append("<div class='content-wrapper'>");
            sb.Append("<section class='content watermark' style='padding-top: 0px; height: 60px;'>");
            sb.Append("<div style='width:21cm;  display:block; border:1px dashed lightgrey; color: black; margin-bottom:5px; overflow:hidden;'> ");
            sb.Append("<div style='text-align:center'>");
            sb.Append("<h3 style='font-weight:100'>");
            sb.Append("<img src='http://erpdairy.com/mis/image/sanchi_logo_blue.png' class='salary-logo'>");
            sb.Append("&nbsp;&nbsp; MP STATE CO OPERATIVE DAIRY FEDERATION <br/>");
            sb.Append("<span class='subheading-salary'>PAY SLIP FOR THE MONTH OF <span id='lblMonth' runat='server'>" + ds1.Tables[0].Rows[0]["Month"] + " / " + ddlYear.SelectedValue.ToString() + "</span>&nbsp; <span id='lblFinancialYear' runat='server'></span>&nbsp; <span style='color: red;' id='lblGenStatus' runat='server'></span></span></h3>");
            sb.Append("<table class='table table-bordered' style='font-family: monospace;font-size: 13px;'>");
            sb.Append("<tbody>");

            sb.Append("<tr>");
            sb.Append("<th style='width: 106px !important; background-color:#eaeaea; style='text-align:left;'>EMPLOYEE NAME:</th>");
            sb.Append("<td style='text-align:left; '>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</td>");
            sb.Append("<th style='width: 77px !important; background-color:#eaeaea; style='text-align:left;'>BANK A/C:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "</td>");
            sb.Append("<th style='width: 84px !important; background-color:#eaeaea; style='text-align:left;'>EPF No:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>DESIGNATION:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Designation_Name"].ToString() + "</td>");
            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>BANK NAME:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_Name"].ToString() + "</td>");
            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>G.INS No:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["GroupInsurance_No"].ToString() + "</td>");

            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>EMPLOYEE CODE:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "</td>");
            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>IFSC CODE:</th>");
            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_IfscCode"].ToString() + "</td>");
            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>NET SALARY:</th>");
            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");
            sb.Append("</tr>");

            sb.Append("</tbody>");
            sb.Append("</table>");
            //Earning
            sb.Append("<table class='table table-bordered table-striped' style='margin-bottom: 0px;font-family: monospace;font-size: 13px; width: 100%;'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<td><h4 style='margin-bottom: 3px; font-weight:100'>PAY</h4></td>");
            sb.Append("<td><h4 style='margin-bottom: 3px; font-weight:100'>DEDUCTIONS</h4></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='width: 50%'>");
            sb.Append("<div class='table-responsive'>");
            sb.Append("<div>");
            sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='width: 100%; margin-bottom:110px;'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<th style='text-align: left; background-color:#eaeaea;'>BASIC SALARY :</th>");
            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
            sb.Append("</tr>");
            //Earning Repeater
            if (ds1.Tables[1].Rows.Count != 0)
            {
                for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                {
                    if (ds1.Tables[1].Rows[j]["Earning"].ToString() != "0")
                    {
                        sb.Append("<tr>");
                        sb.Append("<th style='text-align: left; background-color:#eaeaea;'>" + ds1.Tables[1].Rows[j]["EarnDeduction_Name"].ToString() + ":</th>");
                        sb.Append("<td style='text-align:right;'>" + ds1.Tables[1].Rows[j]["Earning"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }

                }
            }
            sb.Append("<tr class='total_salary'>");
            sb.Append("<th style='text-align: left; background-color:#eaeaea;'>TOTAL PAY :</th>");
            sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</th>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</td>");
            //Deduction
            sb.Append("<td style='width: 50%'>");
            sb.Append("<div class='table-responsive'>");
            sb.Append("<div>");
            sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='width: 100%;'>");
            sb.Append("<tbody>");
            if (ds1.Tables[2].Rows.Count != 0)
            {
                for (int k = 0; k < ds1.Tables[2].Rows.Count; k++)
                {
                    if (ds1.Tables[2].Rows[k]["Earning"].ToString() != "0")
                    {
                        sb.Append("<tr>");
                        sb.Append("<th style='text-align: left; background-color:#eaeaea;'>" + ds1.Tables[2].Rows[k]["EarnDeduction_Name"].ToString() + ":</th>");
                        sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[k]["Earning"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
            }
            //sb.Append("<tr>");
            //sb.Append("<th style='text-align:left; background-color:#eaeaea;'>POLICY :</th>");
            //sb.Append("<th style='text-align:right; background-color:#eaeaea;'>" + ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString() + "</th>");
            //sb.Append("</tr>");
            sb.Append("<tr class='total_salary'>");
            sb.Append("<th style='text-align: left; background-color:#eaeaea;'>TOTAL DEDUCTION:</th>");
            sb.Append("<th style='text-align:right; background-color:#eaeaea;'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</th>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th style='text-align: left; font-size: 11px; background-color:#eaeaea;'></th>");
            sb.Append("<th style='text-align: left; font-size: 11px; background-color:#eaeaea;'>THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</th>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</section>");
            sb.Append("</div>");
            sb.Append("</div>");

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("carempcdf@gmail.com");
            mail.To.Add(Emp_Email.ToString());

            mail.Subject = "Salary Slip";

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);

            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title></title><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> <style> .Grid td {             padding: 3px !important;         }              .Grid td input {                 padding: 3px 3px !important;                 text-align: right !important;                 font-size: 12px !important;                 height: 26px !important;             }          .Grid th {             text-align: center;         }          .ss {             text-align: left !important;         }          .bgcolor {             background-color: #eeeeee !important;         }          .box {             min-height: initial !important;         } .table-striped > tbody > tr:nth-of-type(odd) {   background-color: #f9f9f9; } .content {min-height: 700px; } .box { position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px; width: 100%;box-shadow: 0 1px 1px rgba(0,0,0,0.1);box-shadow: none;border-top: none; }.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #e1e1e1;}.text-center h3 {font-size: 15px; font-family: monospace;}.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {padding: 0px 2px;}#subheading-salary {font-size: 13px;}.salary-logo {-webkit-filter: grayscale(100%);filter: grayscale(100%);width: 40px;         }          .printbutton {             border-top: 1px dashed #838383;             margin-top: 5px;             padding-top: 5px;         }          table h4 {             font-size: 15px;         }          .table {             margin-bottom: 5px;         }          th, td, h3 {             text-transform: uppercase !important;         }         .watermark {   width: 300px;   height: 100px;   display: block;   position: relative; }  .watermark::after {   content:'';  background:url('http://erpdairy.com/mis/image/sanchi_logo_blue.png');   opacity: 0.2;   top: 0;   left: 0;   bottom: 0;   right: 0;   position: absolute;   z-index: -1;   }</style></head><body style='font-family: ' open sans', sans-serif;'>" + sb.ToString() + "</body></html>";
            mail.Body = htmlBody;

            SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        }
    }
	private void GetNewSallerySlip(string Emp_ID, string Emp_Email)
    {
        try
        {
            DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOfficeName.SelectedValue}, "dataset");
            if (ds2.Tables[0].Rows.Count != 0)
            {
                ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
            }
            else
            {
                ViewState["OfficeName"] = "";
            }
            StringBuilder sb = new StringBuilder();
            DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID, ddlOfficeName.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue }, "dataset");
            if (ds1.Tables[0].Rows.Count != 0)
            {


                int ds1table1 = (ds1.Tables[1].Rows.Count) + 2;
                int ds1table2 = ds1.Tables[2].Rows.Count;
                int ds1table3 = ds1.Tables[3].Rows.Count;

                int[] anArray = { ds1table1, ds1table2, ds1table3 };
                int max_elementinSlip = anArray.Max();


                sb.Append("<div style='width:20cm; height:13cm !important; display:block; border:1px dashed lightgrey; color: black; margin-bottom:5px; overflow:hidden;'> ");
                sb.Append("<div style='padding:5px;.text-center { text-align: center;}' >");

                sb.Append("<div style='margin-right: -10px;margin-left:-10px;'>");

                sb.Append("<div style='width: 16.66666667%;float: left;'>");
                sb.Append("<img src='http://erpdairy.com/mis/image/sanchi_logo_blue.png' class='' style='margin: 24px 3px 7px 15px;'>");

                sb.Append("</div>");
                sb.Append("<div style='float: left;width: 66.66666667%;'>");
                sb.Append("<h3 style='font-weight:400;font-size:14px;margin-top: 20px; margin-bottom: 10px;line-height: 1.1; color: inherit; padding-left: 100px; text-transform: uppercase !important;'>");
                sb.Append(ViewState["OfficeName"].ToString());
                sb.Append("<br/><span style='font-weight:400; margin-top: 20px; margin-bottom: 10px;line-height: 1.1; color: inherit; padding-left: 100px;'>Salary Slip</span></h3><br/>");
                sb.Append("</div>");
                sb.Append("<div style='width: 16.66666667%;float: left;'>");
                sb.Append("<div style='style='margin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:9px; margin-top:20px;'>");
                sb.Append("<div style='width: 100%;'><b>Month:  </b>" + ds1.Tables[0].Rows[0]["Month"] + "</div>");

                sb.Append("<div style='width: 100%;'><b>Year:  </b>" + ddlYear.SelectedItem.Text + "</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div style='argin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:10px;'>");
                sb.Append("<div style='float: left;width: 50%;'><b>  Name: </b>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</div>");
                sb.Append("<div style='float: left;width: 50%;'><b> Designation: </b>" + ds1.Tables[0].Rows[0]["Designation_Name"].ToString() + "</div>");
                sb.Append("</div>");
                /**************/
                sb.Append("<div style='margin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:10px;'>");
                sb.Append("<div style='float: left;width: 50%;'><b>  EMP CODE: </b>" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "</div>");
                sb.Append("<div style='float: left;width: 50%;'><b> Department: </b>" + ds1.Tables[0].Rows[0]["Department_Name"].ToString() + "</div>");
                sb.Append("</div>");
                /***************/

                sb.Append("<div style='margin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:10px;'>");
                sb.Append("<div style='float: left;width: 50%;'><b>  BANK A/C NO: </b>" + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "</div>");
                sb.Append("<div style='float: left;width: 50%;'><b> Pay Band: </b>" + ds1.Tables[0].Rows[0]["PayScale_Name"].ToString() + "</div>");
                sb.Append("</div>");

                sb.Append("<div style='margin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:10px;'>");
                sb.Append("<div style='float: left;width: 50%;'><b>  EPF NO.: </b>" + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</div>");
                sb.Append("<div style='float: left;width: 50%;'><b> Grade Pay: </b>" + ds1.Tables[0].Rows[0]["GradePay_Name"].ToString() + "</div>");
                sb.Append("</div>");

                sb.Append("<div style='margin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:10px;'>");
                sb.Append("<div style='float: left;width: 50%;'><b>  PAN NO. : </b>" + ds1.Tables[0].Rows[0]["Emp_PanCardNo"].ToString() + "</div>");
                sb.Append("<div style='float: left;width: 50%;'><b>Level: </b>" + ds1.Tables[0].Rows[0]["Level_Name"].ToString() + "</div>");
                sb.Append("</div>");

                sb.Append("<div style='margin-right: -10px;margin-left:-10px;font-family:verdana; text-align:left; font-size:10px;'>");
                sb.Append("<div style='float: left;width: 50%;'><b>  UAN NO.: </b>" + ds1.Tables[0].Rows[0]["UAN_No"].ToString() + "</div>");
                sb.Append("<div style='float: left;width: 50%;'></div>");
                sb.Append("</div>");
                /**************/

                /************************************************/
                /************************************************/
                sb.Append("<table style='border-spacing: 0;border-collapse: collapse;background-color: transparent;width: 100%;max-width: 100%;border: 1px solid #ddd;margin-bottom: 5px;font-family:verdana;font-size:9px;'><tbody>");

                sb.Append("<tr> <th colspan='2' style='border: 1px solid #999 !important;'>EARNING</th> <th colspan='2' style='border: 1px solid #999 !important;'>DEDUCTION</th> <th colspan='2'  style='border: 1px solid #999 !important;'>CONTRIBUTION</th> <th colspan='2'  style='border: 1px solid #999 !important;'>DEPOSIT BALANCE</th> <th colspan='2' style='border: 1px solid #999 !important;' >NET PAYABLE SALARY</th> </tr>");
                sb.Append("<tr> <th style='border: 1px solid #999 !important;text-align:center;'>A</th><td style='text-align:center;border: 1px solid #999 !important'></td> <td style='text-align:center;border: 1px solid #999 !important'>B</td><td style='border: 1px solid #999 !important'></td> <td style='text-align:center;border: 1px solid #999 !important'>C</td><td style='border: 1px solid #999 !important'></td> <td style='text-align:center;border: 1px solid #999 !important'>D</td style='border: 1px solid #999 !important'><td style='border: 1px solid #999 !important'></td> <td colspan='2' style='text-align:center;border: 1px solid #999 !important'>(A-B-C)</td></tr>");


                /************************************************/

                /************************************************/
                sb.Append("<tr> <th style='text-align:left;border: 1px solid #999 !important;'>PARTICULARS</th><th style='text-align:left;border: 1px solid #999 !important;'>AMOUNT RS.</th> <th style='text-align:left;border: 1px solid #999 !important;'>PARTICULARS</th><th style='text-align:left;border: 1px solid #999 !important;'>AMOUNT RS.</th> <th style='text-align:left;border: 1px solid #999 !important;'>PARTICULARS</th><th style='text-align:left;border: 1px solid #999 !important;'>AMOUNT RS.</th> <th style='text-align:left;border: 1px solid #999 !important;'>PARTICULARS</th><th style='text-align:left;border: 1px solid #999 !important;'>AMOUNT RS.</th> <th colspan='2' style='border: 1px solid #999 !important;'></th></tr>");



                /*********XXX***********/

                //sb.Append(max_elementinSlip.ToString());
                decimal EarningTotal = 0;
                decimal DeductionTotal = 0;
                decimal ContributionTotal = 0;
                decimal GrandTotal = 0;
                /***For Loop ****/
                for (int var_i = 0; var_i < max_elementinSlip; var_i++)
                {
                    sb.Append("<tr>");

                    /**ds1table0, ds1table1, ds1table2, ds1table3
                    /********table 1 - Earning************/
                    if (var_i == 0)
                    {
                        sb.Append("<th style='text-align:left;border: 1px solid #999 !important;'>BASIC SALARY:</th>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
                        EarningTotal = EarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    }
                    else
                    {
                        if (ds1table1 > (var_i + 1))
                        {
                            sb.Append("<th style='text-align:left;border: 1px solid #999 !important;'>" + ds1.Tables[1].Rows[var_i - 1]["EarnDeduction_Name"].ToString() + ":</th>");
                            sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'>" + ds1.Tables[1].Rows[var_i - 1]["Earning"].ToString() + "</td>");
                            EarningTotal = EarningTotal + decimal.Parse(ds1.Tables[1].Rows[var_i - 1]["Earning"].ToString());
                        }
                        else
                        {
                            sb.Append("<td style='border: 1px solid #999 !important;'></td>");
                            sb.Append("<td style='border: 1px solid #999 !important;'></td>");
                        }

                    }

                    /********table 2 - Deduction************/
                    if (ds1table2 > var_i)
                    {
                        sb.Append("<th style='text-align:left;border: 1px solid #999 !important;'>" + ds1.Tables[2].Rows[var_i]["EarnDeduction_Name"].ToString() + ":</th>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'>" + ds1.Tables[2].Rows[var_i]["Earning"].ToString() + "</td>");
                        DeductionTotal = DeductionTotal + decimal.Parse(ds1.Tables[2].Rows[var_i]["Earning"].ToString());
                    }
                    else
                    {
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                    }
                    /********table 3 - Contribution*********/
                    if (ds1table3 > var_i)
                    {
                        sb.Append("<th style='text-align:left;border: 1px solid #999 !important;'>" + ds1.Tables[3].Rows[var_i]["EarnDeduction_Name"].ToString() + ":</th>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'>" + ds1.Tables[3].Rows[var_i]["Earning"].ToString() + "</td>");

                        if (ds1.Tables[3].Rows[var_i]["ContributionType"].ToString() != "Contribution")
                        {
                            sb.Append("<th style='text-align:left;border: 1px solid #999 !important;'>" + ds1.Tables[3].Rows[var_i]["EarnDeduction_Name"].ToString() + ":</th>");
                            sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'>" + ds1.Tables[3].Rows[var_i]["FinalBalance"].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                            sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                        }

                        ContributionTotal = ContributionTotal + decimal.Parse(ds1.Tables[3].Rows[var_i]["Earning"].ToString());
                    }
                    else
                    {
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                        sb.Append("<td style='text-align:right;border: 1px solid #999 !important;'></td>");
                    }

                    sb.Append("<th style='border: 1px solid #999 !important;'></th><th style='border: 1px solid #999 !important;'></th>");
                    sb.Append("</tr>");
                }
                /*******/
                GrandTotal = (EarningTotal - (DeductionTotal + ContributionTotal));
                /*******/


                sb.Append("<tr> <th style='border: 1px solid #999 !important;'>TOTAL:</th><th style='text-align:right;border: 1px solid #999 !important;'>" + EarningTotal.ToString() + "</td> <td style='border: 1px solid #999 !important;'></td><th style='text-align:right;border: 1px solid #999 !important;'>" + DeductionTotal.ToString() + "</td> <td style='border: 1px solid #999 !important;'></td><th style='text-align:right;border: 1px solid #999 !important;'>" + ContributionTotal.ToString() + "</td> <td style='border: 1px solid #999 !important;'></td><td style='border: 1px solid #999 !important;'></td> <th colspan='2' style='text-align:right;border: 1px solid #999 !important;'>" + GrandTotal.ToString() + " INR</td></tr>");
                sb.Append("</tbody></table>");
                /************************************************/

                sb.Append("</div>");

                sb.Append("<div style='font-family: verdana; font-size:12px;'>");
                sb.Append("<div style='margin-right: -10px; margin-left: -10px;'><div style='width: 100%;float: left;position: relative; min-height: 1px;padding-right: 10px;padding-left: 10px;'>");
                sb.Append("<div style='width: 100%;float: left;position: relative; min-height: 1px;padding-right: 10px;padding-left: 10px;'>" + GenerateWordsinRs(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString()) + "</div>");
                sb.Append("</div></div>");

                sb.Append("<div style='margin-right: -10px; margin-left: -10px;'> <div style='width: 100%;float: left;position: relative; min-height: 1px;padding-right: 10px;padding-left: 10px;'>");
                /*****************/
                if (ddlMonth.SelectedValue == "12")
                {
                    sb.Append("<div style='text-align: right;width: 100%;float: left;position: relative; min-height: 1px;padding-right: 10px;padding-left: 10px;font-size:9px; margin-top:20px;'>Wish you a very Happy New Year.</div>");
                }
                /*****************/
                sb.Append("<div style='text-align: right;width: 100%;float: left;position: relative; min-height: 1px;padding-right: 10px;padding-left: 10px;font-size:11px; margin-top:20px;'>THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</div></div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("</div>");

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("carempcdf@gmail.com");
                mail.To.Add(Emp_Email.ToString());

                mail.Subject = "Salary Slip";

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);

                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head></head><body>" + sb.ToString() + "</body></html>";
               // htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title></title><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> <style> .Grid td {             padding: 3px !important;         }              .Grid td input {                 padding: 3px 3px !important;                 text-align: right !important;                 font-size: 12px !important;                 height: 26px !important;             }          .Grid th {             text-align: center;         }          .ss {             text-align: left !important;         }          .bgcolor {             background-color: #eeeeee !important;         }          .box {             min-height: initial !important;         } .table-striped > tbody > tr:nth-of-type(odd) {   background-color: #f9f9f9; } .content {min-height: 700px; } .box { position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px; width: 100%;box-shadow: 0 1px 1px rgba(0,0,0,0.1);box-shadow: none;border-top: none; }.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #e1e1e1;}.text-center h3 {font-size: 15px; font-family: monospace;}.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {padding: 0px 2px;}#subheading-salary {font-size: 13px;}.salary-logo {-webkit-filter: grayscale(100%);filter: grayscale(100%);width: 40px;         }          .printbutton {             border-top: 1px dashed #838383;             margin-top: 5px;             padding-top: 5px;         }          table h4 {             font-size: 15px;         }          .table {             margin-bottom: 5px;         }          th, td, h3 {             text-transform: uppercase !important;         }         .watermark {   width: 300px;   height: 100px;   display: block;   position: relative; }  .watermark::after {   content:'';  background:url('http://erpdairy.com/mis/image/sanchi_logo_blue.png');   opacity: 0.2;   top: 0;   left: 0;   bottom: 0;   right: 0;   position: absolute;   z-index: -1;   }</style></head><body style='font-family: ' open sans', sans-serif;'>" + sb.ToString() + "</body></html>";
                mail.Body = htmlBody;

                SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
               
            }

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        }
        catch (Exception ex)
        {
            
              lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {

        }
    }
	
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            int count = GridView1.Rows.Count;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                Label Emp_Name = (Label)gvrow.FindControl("Emp_Name");
                string Emp_Id = Emp_Name.ToolTip.ToString();
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    string Email = gvrow.Cells[2].Text;
                    string Salary_NetSalary = gvrow.Cells[3].Text;
                    if (Email != "sanchi@gmail.com" && Email != "SANCHI@GMAIL.COM" && Email != "")
                    {
						GetNewSallerySlip(Emp_Id, Email);

                       // FillSalary(Emp_Id, Email);

                        //ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "18", ddlOfficeName.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlEmpType.SelectedValue.ToString() }, "dataset");
                        //if (ds.Tables[0].Rows.Count != 0)
                        //{
                        //    //int Count = Convert.ToInt16(ds.Tables[1].Rows[0]["SalaryCount"].ToString());
                        //    int Count = ds.Tables[0].Rows.Count;                           
                        //    for (int i = 0; i < 1; i++)
                        //    {
                        //        string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                        //        string Emp_Email = ds.Tables[0].Rows[i]["Emp_Email"].ToString();


                        //        /**************/
                        //        //FillSalary(Emp_ID, Email);
                        //        /**************/

                        //    }
                        //}
                        //else
                        //{

                        //}

                    }
                }
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
	
	private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
}