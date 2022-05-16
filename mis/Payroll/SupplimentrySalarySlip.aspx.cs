using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_SupplimentrySalarySlip : System.Web.UI.Page
{
    DataSet ds1 =new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Emp_ID"] != null && Request.QueryString["EmpSupplimentryID"] != null)
            {
                FillSalary();
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
            DivSlip.Visible = false;

            string sid = objdb.Decrypt(Request.QueryString["EmpSupplimentryID"].ToString());
            string empid = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
            ds1 = objdb.ByProcedure("USP_tblPayrollEmpSupplimentryDetail", new string[] { "flag", "EmpSupplimentryID", "Emp_ID" }, new string[] { "5", sid, empid }, "dataset");
                
                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            StringBuilder sb = new StringBuilder();

                            int ds1table1 = (ds1.Tables[1].Rows.Count) + 2;
                            int ds1table2 = ds1.Tables[2].Rows.Count;
                           

                            int[] anArray = { ds1table1, ds1table2 };
                            int max_elementinSlip = anArray.Max();


                          
                            sb.Append("<div style='width:20cm; height:13cm !important; display:block; border:1px dashed lightgrey; color: black; margin-bottom:5px; overflow:hidden;'> ");
                            sb.Append("<div class='text-center' style='padding:5px;' >");

                            sb.Append("<div class='row'>");

                            sb.Append("<div class='col-md-2 col-sm-2 col-xs-2'>");
                            sb.Append("<img src='../../mis/image/sanchi_logo_blue.png' class='' style='margin: 24px 3px 7px 15px;'>");

                            sb.Append("</div>");
                            sb.Append("<div class='col-md-8 col-sm-8 col-xs-8'>");
                            sb.Append("<h3 class='' style='font-weight:400'>");
                            sb.Append(ds1.Tables[0].Rows[0]["Office_Name"]);
                            sb.Append("<br/><span id='subheading-salary'>Supplementary Salary Slip</span></h3><br/>");
                            sb.Append("</div>");
                            sb.Append("<div class='col-md-2 col-sm-2 col-xs-2'>");
                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:9px; margin-top:20px;'>");
                            sb.Append("<div class='col-md-12'><b>Month:  </b>" + ds1.Tables[0].Rows[0]["FromMonthName"] + "</div>");

                            sb.Append("<div class='col-md-12'><b>Year:  </b>" + ds1.Tables[0].Rows[0]["FromYear"] + "</div>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</div>");

                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:10px;'>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  Name: </b>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</div>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b> Designation: </b>" + ds1.Tables[0].Rows[0]["Designation_Name"].ToString() + "</div>");
                            sb.Append("</div>");
                            /**************/
                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:10px;'>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  EMP CODE: </b>" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "</div>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b> Department: </b>" + ds1.Tables[0].Rows[0]["Department_Name"].ToString() + "</div>");
                            sb.Append("</div>");
                            /***************/

                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:10px;'>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  BANK A/C NO: </b>" + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "</div>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b> Pay Band: </b>" + ds1.Tables[0].Rows[0]["PayScale_Name"].ToString() + "</div>");
                            sb.Append("</div>");

                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:10px;'>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  EPF NO.: </b>" + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</div>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b> Grade Pay: </b>" + ds1.Tables[0].Rows[0]["GradePay_Name"].ToString() + "</div>");
                            sb.Append("</div>");

                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:10px;'>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  PAN NO. : </b>" + ds1.Tables[0].Rows[0]["Emp_PanCardNo"].ToString() + "</div>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>Level: </b>" + ds1.Tables[0].Rows[0]["Level_Name"].ToString() + "</div>");
                            sb.Append("</div>");

                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:10px;'>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'><b>  UAN NO.: </b>" + ds1.Tables[0].Rows[0]["UAN_No"].ToString() + "</div>");
                            sb.Append("<div class='col-md-6 col-sm-6 col-xs-6'></div>");
                            sb.Append("</div>");
                            /**************/

                            /************************************************/
                            /************************************************/
                            sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='font-family:verdana;font-size:9px;'><tbody>");

                            sb.Append("<tr> <th colspan='2'>EARNING</th> <th colspan='2'>DEDUCTION</th> <th colspan='2'>CONTRIBUTION</th> <th colspan='2'>DEPOSIT BALANCE</th> <th colspan='2'>NET PAYABLE SALARY</th> </tr>");
                            sb.Append("<tr> <th>A</th><td></td> <td>B</td><td></td> <td>C</td><td></td> <td>D</td><td></td> <td colspan='2'>(A-B-C)</td></tr>");


                            /************************************************/

                            /************************************************/
                            sb.Append("<tr> <th>PARTICULARS</th><th>AMOUNT RS.</th> <th>PARTICULARS</th><th>AMOUNT RS.</th> <th>PARTICULARS</th><th>AMOUNT RS.</th> <th>PARTICULARS</th><th>AMOUNT RS.</th> <th colspan='2'></th></tr>");



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
                                    sb.Append("<th>BASIC SALARY:</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["BasicSalary"].ToString() + "</td>");
                                    EarningTotal = EarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["BasicSalary"].ToString());
                                }
                                else
                                {
                                    if (ds1table1 > (var_i + 1))
                                    {
                                        sb.Append("<th>" + ds1.Tables[1].Rows[var_i - 1]["EarnDeductionName"].ToString() + ":</th>");
                                        sb.Append("<td style='text-align:right;'>" + ds1.Tables[1].Rows[var_i - 1]["RemainingAmount"].ToString() + "</td>");
                                        EarningTotal = EarningTotal + decimal.Parse(ds1.Tables[1].Rows[var_i - 1]["RemainingAmount"].ToString());
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                    }

                                }

                                /********table 2 - Deduction************/

                                sb.Append("<td></td>");
                                sb.Append("<td></td>");
                               
                                
                                /********table 3 - Contribution*********/
                                if (ds1table2 > var_i)
                                {
                                    sb.Append("<th>" + ds1.Tables[2].Rows[var_i]["EarnDeductionName"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[var_i]["RemainingAmount"].ToString() + "</td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td></td>");


                                    ContributionTotal = ContributionTotal + decimal.Parse(ds1.Tables[2].Rows[var_i]["RemainingAmount"].ToString());
                                }
                                else
                                {
                                    sb.Append("<td></td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td></td>");
                                }

                                sb.Append("<th></th><th></th>");
                                sb.Append("</tr>");
                            }
                            /*******/
                            GrandTotal = (EarningTotal - (DeductionTotal + ContributionTotal));
                            /*******/


                            sb.Append("<tr> <th>TOTAL:</th><th style='text-align:right;'>" + EarningTotal.ToString() + "</td> <td></td><th style='text-align:right;'>" + DeductionTotal.ToString() + "</td> <td></td><th style='text-align:right;'>" + ContributionTotal.ToString() + "</td> <td></td><td></td> <th colspan='2' style='text-align:right;'>" + GrandTotal.ToString() + " INR</td></tr>");
                            sb.Append("</tbody></table>");
                            /************************************************/

                            sb.Append("</div>");

                            sb.Append("<div style='font-family: verdana; font-size:12px;'>");
                            sb.Append("<div class='row'><div class='col-md-12'>");
                            sb.Append("<div class='col-md-12'>" + GenerateWordsinRs(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString()) + "</div>");
                            sb.Append("</div></div>");

                            sb.Append("<div class='row'> <div class='col-md-12'>");
                            /*****************/
                            if (ds1.Tables[0].Rows[0]["FromMonth"].ToString() == "12")
                            {
                                sb.Append("<div class='col-md-12 text-right' style='font-size:9px; margin-top:20px;'>Wish you a very Happy New Year.</div>");
                            }
                            /*****************/
                            sb.Append("<div class='col-md-12 text-right' style='font-size:11px; margin-top:20px;'>THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</div></div>");
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