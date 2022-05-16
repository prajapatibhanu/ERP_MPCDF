using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class mis_Payroll_SalarySlipSingle : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {
                if (Request.QueryString["Year"] != null && Request.QueryString["Month"] != null)
                {
                    ViewState["Emp_ID"] = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
                    ViewState["Office_ID"] = objdb.Decrypt(Request.QueryString["Office"].ToString());
                    ViewState["Year"] = objdb.Decrypt(Request.QueryString["Year"].ToString());
                    ViewState["MonthNo"] = objdb.Decrypt(Request.QueryString["Month"].ToString());
                    FillSalary();
                }
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillSalary()
    {
        try
        {
            DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds2.Tables[0].Rows.Count != 0)
            {
                ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
            }
            else
            {
                ViewState["OfficeName"] = "";
            }

            StringBuilder sb = new StringBuilder();
            DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), ViewState["Year"].ToString(), ViewState["MonthNo"].ToString() }, "dataset");
            if (ds1.Tables[0].Rows.Count != 0)
            {

                sb.Append("<p style='page-break-before: always'>");
                sb.Append("<div style='width:20cm; height:13cm !important; display:block; border:1px dashed lightgrey; color: black; margin-bottom:5px; overflow:hidden;'> ");
                sb.Append("<div class='text-center' style='padding:5px;' >");

                sb.Append("<div class='row'>");

                sb.Append("<div class='col-md-2 col-sm-2 col-xs-2'>");
                sb.Append("<img src='../../mis/image/sanchi_logo_blue.png' class='' style='margin: 24px 3px 7px 15px;'>");

                sb.Append("</div>");
                sb.Append("<div class='col-md-8 col-sm-8 col-xs-8'>");
                sb.Append("<h3 class='' style='font-weight:400'>");
                sb.Append(ViewState["OfficeName"].ToString());
                sb.Append("<br/><span id='subheading-salary'>Salary Slip</span></h3><br/>");
                sb.Append("</div>");
                sb.Append("<div class='col-md-2 col-sm-2 col-xs-2'>");
                sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:11px; margin-top:20px;'>");
                sb.Append("<div class='col-md-12'><b>Month:  </b>" + ds1.Tables[0].Rows[0]["Month"] + "</div>");
                sb.Append("<div class='col-md-12'><b>Year:  </b>" + ViewState["Year"] + "</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("</div>");

                sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:12px;'>");
                sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  Name: </b>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</div>");
                sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>Designation: </b>" + ds1.Tables[0].Rows[0]["Designation_Name"].ToString() + "</div>");
                sb.Append("</div>");

                sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:12px;'>");

                sb.Append("<div class='col-md-3 col-sm-3  col-xs-3'><b>  Emp Code: </b>" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "</div>");
                sb.Append("<div class='col-md-3 col-sm-3  col-xs-3'><b>Level: </b> " + ds1.Tables[0].Rows[0]["Level_Name"].ToString() + "</div>");
                sb.Append("<div class='col-md-3 col-sm-3 col-xs-3'><b>Pay Band: </b> " + ds1.Tables[0].Rows[0]["PayScale_Name"].ToString() + "</div>");
                sb.Append("<div class='col-md-3 col-sm-3 col-xs-3'><b>Grade Pay: </b> " + ds1.Tables[0].Rows[0]["GradePay_Name"].ToString() + "</div>");

                sb.Append("</div>");
                sb.Append("<table class='table table-striped' style='margin-bottom: 0px;font-family: verdana;font-size: 11px;'>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td style='width: 40%'>");
                sb.Append("<div class='table-responsive'>");
                sb.Append("<div>");
                sb.Append("<table class='table table-bordered table-striped Grid earning-table'>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center;  width:200px;' class='slipheadings'>Earnings</th>");
                sb.Append("<th style='text-align:center;' class='slipheadings'>Amount</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>BASIC SALARY :</th>");
                sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
                sb.Append("</tr>");
                if (ds1.Tables[1].Rows.Count != 0)
                {
                    for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                    {
                        if (ds1.Tables[1].Rows[j]["Earning"].ToString() != "0")
                        {
                            sb.Append("<tr>");
                            sb.Append("<th>" + ds1.Tables[1].Rows[j]["EarnDeduction_Name"].ToString() + ":</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[1].Rows[j]["Earning"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }

                    }
                }
                sb.Append("<tr class='total_salary'>");
                sb.Append("<th>Total Salary :</th>");
                sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</th>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div></div>");
                sb.Append("</td>");
                sb.Append("<td style='width: 60%'>");
                sb.Append("<div class=''>");
                sb.Append("<div class=''>");
                sb.Append("<div class='table-responsive'>");
                sb.Append("<div>");
                sb.Append("<table class='table table-bordered table-striped Grid earning-table'>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<th style='text-align:center; width:200px;' class='slipheadings'>DEDUCTIONS</th>");
                sb.Append("<th style='text-align:center;' class='slipheadings'>Amount</th>");
                sb.Append("<th style='text-align:center;' class='slipheadings'>BALANCE</th>");
                sb.Append("</tr>");
                if (ds1.Tables[2].Rows.Count != 0)
                {
                    for (int k = 0; k < ds1.Tables[2].Rows.Count; k++)
                    {
                        if (ds1.Tables[2].Rows[k]["Earning"].ToString() != "0")
                        {
                            sb.Append("<tr>");
                            sb.Append("<th>" + ds1.Tables[2].Rows[k]["EarnDeduction_Name"].ToString() + ":</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[k]["Earning"].ToString() + "</td>");
                            if (ds1.Tables[2].Rows[k]["FinalBalance"].ToString() != "0")
                            {
                                sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[k]["FinalBalance"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td style='text-align:right;'></td>");
                            }

                            sb.Append("</tr>");
                        }
                    }
                }
                sb.Append("<tr>");
                sb.Append("<tr class='total_salary'>");
                sb.Append("<th>Total Deduction:</th>");
                sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</th>");
                sb.Append("<td style='text-align:right;'></td>");
                sb.Append("</tr>");
                sb.Append("<th>Net Salary :</th>");
                sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</th>");
                sb.Append("<td style='text-align:right;'></td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div></div></div>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div>");

                sb.Append("<div style='font-family: verdana;line-height: 17px;padding: 10px; font-size:12px;'>");
                sb.Append("<div class='row'><div class='col-md-12'>");
                sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'>" + GenerateWordsinRs(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString()) + "</div>");
                sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align: left; width:30%;'>Bank A/c No. </td>");
                sb.Append("<td style='text-align: left;  width:70%;'>: " + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'>EPF No. </td>");
                sb.Append("<td style='text-align: left;'>: " + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'>UAN No. </td>");
                sb.Append("<td style='text-align: left;'>: " + ds1.Tables[0].Rows[0]["UAN_No"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td style='text-align: left;'>PAN No. </td>");
                sb.Append("<td style='text-align:left;'>: " + ds1.Tables[0].Rows[0]["Emp_PanCardNo"].ToString() + "</td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div></div>");

                sb.Append("<div class='row'> <div class='col-md-12'>");
                sb.Append("<div class='col-md-12 text-center' style='font-size:12px; margin-top:20px;'>THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</div></div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("</div>");
                DivSlip.InnerHtml = sb.ToString();
                DivSlip.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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