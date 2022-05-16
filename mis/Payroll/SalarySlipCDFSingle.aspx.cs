using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_SalarySlipCDFSingle : System.Web.UI.Page
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
                    ViewState["Year"] = objdb.Decrypt(Request.QueryString["Year"].ToString());
                    ViewState["Month"] = objdb.Decrypt(Request.QueryString["Month"].ToString());
					
                    /*************/
					// by pawan  9 march 2022
                    // DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Salary_Year", "Salary_MonthNo" }, new string[] { "36", ViewState["Emp_ID"].ToString(), ViewState["Year"].ToString(), ViewState["Month"].ToString() }, "dataset");
                    // if (ds2.Tables[0].Rows.Count != 0)
                    // {
                        // ViewState["Office"] = ds2.Tables[0].Rows[0]["Office_ID"].ToString();
                    // }
                    // else
                    // {
                        // ViewState["Office"] = objdb.Decrypt(Request.QueryString["Office"].ToString());
                    // }
					ViewState["Office"] = objdb.Decrypt(Request.QueryString["Office"].ToString());
					// end by pawan  9 march 2022
                    /*************/
                    
                  
                    //ViewState["EmpType"] = objdb.Decrypt(Request.QueryString["EmpType"].ToString());
                    //if (ViewState["EmpType"].ToString() == "FixedEmployee")
                    //{
                    //    ViewState["EmpType"] = "Fixed Employee";
                    //}
                    //else if (ViewState["EmpType"].ToString() == "ContigentEmployee")
                    //{
                    //    ViewState["EmpType"] = "Contigent Employee";
                    //}
                    //else if (ViewState["EmpType"].ToString() == "SamvidaEmployee")
                    //{
                    //    ViewState["EmpType"] = "Samvida Employee";
                    //}
                    //else if (ViewState["EmpType"].ToString() == "ThekaShramik")
                    //{
                    //    ViewState["EmpType"] = "Theka Shramik";
                    //}
                    //else
                    //{

                    //}
                    DivSlip.Visible = false;
                    //lblNotGenerated.Visible = false;
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
            DivSlip.Visible = false;
            //lblNotGenerated.Visible = false;





            if (ViewState["Year"].ToString() != null && ViewState["Month"].ToString() != null)
            {

                DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ViewState["Office"].ToString() }, "dataset");
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                }
                else
                {
                    ViewState["OfficeName"] = "";
                }

                    StringBuilder sb = new StringBuilder();
                    string Emp_ID = ViewState["Emp_ID"].ToString();

                        DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID, ViewState["Office"].ToString(), ViewState["Year"].ToString(), ViewState["Month"].ToString() }, "dataset");
                        if (ds1.Tables[0].Rows.Count != 0)
                        {

                        
                            int ds1table1 = (ds1.Tables[1].Rows.Count)+2;
                            int ds1table2 = ds1.Tables[2].Rows.Count;
                            int ds1table3 = ds1.Tables[3].Rows.Count;

                            int[] anArray = { ds1table1, ds1table2, ds1table3 };
                            int max_elementinSlip = anArray.Max();

                            int i = 1;
                            if (i % 2 == 0)
                            {
                                sb.Append("<p style='page-break-before: always'>");
                            }
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
                            sb.Append("<div class='row' style='font-family:verdana; text-align:left; font-size:9px; margin-top:20px;'>");
                            sb.Append("<div class='col-md-12'><b>Month:  </b>" + ds1.Tables[0].Rows[0]["Month"] + "</div>");
                            
                            sb.Append("<div class='col-md-12'><b>Year:  </b>" + ViewState["Year"] + "</div>");
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
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
                                    EarningTotal = EarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
                                }
                                else
                                {
                                    if (ds1table1 > (var_i + 1))
                                    {
                                        sb.Append("<th>" + ds1.Tables[1].Rows[var_i - 1]["EarnDeduction_Name"].ToString() + ":</th>");
                                        sb.Append("<td style='text-align:right;'>" + ds1.Tables[1].Rows[var_i - 1]["Earning"].ToString() + "</td>");
                                        EarningTotal = EarningTotal + decimal.Parse(ds1.Tables[1].Rows[var_i - 1]["Earning"].ToString());
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                    }

                                }

                                /********table 2 - Deduction************/
                                if (ds1table2 > var_i)
                                {
                                    sb.Append("<th>" + ds1.Tables[2].Rows[var_i]["EarnDeduction_Name"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[var_i]["Earning"].ToString() + "</td>");
                                    DeductionTotal = DeductionTotal + decimal.Parse(ds1.Tables[2].Rows[var_i]["Earning"].ToString());
                                }
                                else
                                {
                                    sb.Append("<td></td>");
                                    sb.Append("<td></td>");
                                }
                                /********table 3 - Contribution*********/
                                if (ds1table3 > var_i)
                                {
                                    sb.Append("<th>" + ds1.Tables[3].Rows[var_i]["EarnDeduction_Name"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[var_i]["Earning"].ToString() + "</td>");

                                    if (ds1.Tables[3].Rows[var_i]["ContributionType"].ToString() != "Contribution")
                                    {
                                        sb.Append("<th>" + ds1.Tables[3].Rows[var_i]["EarnDeduction_Name"].ToString() + ":</th>");
                                        sb.Append("<td style='text-align:right;'>" + ds1.Tables[3].Rows[var_i]["FinalBalance"].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                    }

                                    ContributionTotal = ContributionTotal + decimal.Parse(ds1.Tables[3].Rows[var_i]["Earning"].ToString());
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
                            if (ViewState["Month"].ToString() == "12")
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
                else
                {
                    //lblNotGenerated.Visible = true;
                }
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