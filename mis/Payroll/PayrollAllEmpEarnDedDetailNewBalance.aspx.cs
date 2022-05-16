using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollAllEmpEarnDedDetailNewBalance : System.Web.UI.Page
{
    DataSet ds, ds_dynamic;
    AbstApiDBApi objdb = new APIProcedure();

    decimal SectionBasic = 0;
    decimal SectionEarningTotal = 0;
    decimal SectionDeductionTotal = 0;
    decimal SectionNetSalary = 0;

    decimal TotalBasic = 0;
    decimal TotalEarningTotal = 0;
    decimal TotalDeductionTotal = 0;
    decimal TotalNetSalary = 0;

    decimal[] Earning;
    decimal[] Deductions;
    decimal[] EarningFooter;
    decimal[] DeductionsFooter;
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
                    ddlOffice.Enabled = false;
                    DivHead.Visible = false;
                    //if (ViewState["Office_ID"].ToString() == "1")
                    //{
                    //    ddlOffice.Enabled = true;
                    //}
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "23" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        ddlOffice.Items.Insert(0, new ListItem("Select", "0"));

                        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }
                    FillDropdown();
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
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DivDetail.InnerHtml = "";
            lblMsg.Text = "";
            lblSession.Text = "";
            lblPosttype.Text = "";
            lblOffice.Text = "";
            DivHead.Visible = false;
            if (ddlOffice.SelectedIndex > 0 && ddlFinancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                }
                else
                {
                    ViewState["OfficeName"] = "";
                }

                lblOffice.Text = ViewState["OfficeName"].ToString();
                lblPosttype.Text = "";

                //if (ddlOffice.SelectedValue.ToString() == "3")
                //{
                //    //FillSalary_GWL();
                //    //FillSalary_Dynamic2();
                //}
                //else if (ddlOffice.SelectedValue.ToString() == "1")
                //{
                //    //FillSalary_Dynamic2();
                //}
                //else
                //{
                    FillSalary();
                //}
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalary()
    {
        try
        {
            DivDetail.InnerHtml = "";
            string Office_ID = ddlOffice.SelectedValue.ToString();
            string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
            string Salary_MonthNo = ddlMonth.SelectedValue.ToString();

            lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();
            DivHead.Visible = true;

            StringBuilder htmlStr = new StringBuilder();
            htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
            htmlStr.Append("<thead  style='border-bottom: 0px solid white; display: table-header-group;' class='printmaindata'>");
            htmlStr.Append("<tr class='tblheadingslip'>");
            htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
            htmlStr.Append("<th colspan='4' class='text-left'>Name Of Employee</th>");
            htmlStr.Append("<th colspan='2'>Designation</th>");
            htmlStr.Append("<th colspan=''>ERP Code</th>");
            htmlStr.Append("<th></th>");
            //htmlStr.Append("<th>GI No.</th>");
            htmlStr.Append("</tr>");

            /*****A Row*******/
            htmlStr.Append("<tr class='tblheadingslip'>");
            htmlStr.Append("<th>PAY RECOVERY</th>");
            htmlStr.Append("<th class='empearning'>UNION CREDIT SOCIETY CD</th>");
            htmlStr.Append("<th class='empearning'>GRAIN ADVANCE</th>");
            htmlStr.Append("<th class='empearning'>FESTIVAL ADVANCE</th>");
            htmlStr.Append("<th class='empearning'>CREDIT SOC CD DEPOSIT</th>");
            htmlStr.Append("<th class='empearning'>CR SOCIETY MT LOAN</th>");
            htmlStr.Append("<th class='empearning'>CR SOCIETY RD</th>");
            htmlStr.Append("<th class='empearning'>CR SOCIETY ST LOAN</th>");
            htmlStr.Append("<th class='empearning'>UNION CREDIT SOCIETY LOAN</th>");
            htmlStr.Append("<th class='empearning'>UNION HRA DEDUCTION</th>");
            htmlStr.Append("<th class='empearning'>UNION REC</th>");
            htmlStr.Append("</tr>");
            /*****B Row*******/

            //PAY_RECOVERY	UNION_CREDIT_SOCIETY_CD	GRAIN_ADVANCE	FESTIVAL_ADVANCE	CREDIT_SOC_CD_DEPOSIT	CR_SOCIETY_MT_LOAN	CR_SOCIETY_RD	CR_SOCIETY_ST_LOAN	UNION_CREDIT_SOCIETY_LOAN	UNION_HRA_DEDUCTION	UNION_REC	


            htmlStr.Append("<tr class='tblheadingslip'>");
            htmlStr.Append("<th>BL PAY RECOVERY</th>");
            htmlStr.Append("<th class='empearning'>BL UNION CREDIT SOCIETY CD</th>");
            htmlStr.Append("<th class='empearning'>BL GRAIN ADVANCE</th>");
            htmlStr.Append("<th class='empearning'>BL FESTIVAL ADVANCE</th>");
            htmlStr.Append("<th class='empearning'>BL CREDIT SOC CD DEPOSIT</th>");
            htmlStr.Append("<th class='empearning'>BL CR SOCIETY MT LOAN</th>");
            htmlStr.Append("<th class='empearning'>BL CR_SOCIETY RD</th>");
            htmlStr.Append("<th class='empearning'>BL CR_SOCIETY ST LOAN</th>");
            htmlStr.Append("<th class='empearning'>BL UNION_CREDIT SOCIETY LOAN</th>");
            htmlStr.Append("<th class='empearning'>BL UNION HRA DEDUCTION</th>");
            htmlStr.Append("<th class='empearning'>BL UNION REC</th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</thead>");
            htmlStr.Append("<tbody>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<table>");

            //string repeadtCodePrintPage = htmlStr.ToString();
            // Row 1            
            decimal Tables1Rows1 = 0;
            decimal Tables1Rows2 = 0;
            decimal Tables1Rows3 = 0;
            decimal Tables1Rows4 = 0;
            decimal Tables1Rows5 = 0;
            decimal Tables1Rows6 = 0;
            decimal Tables1Rows7 = 0;
            decimal Tables1Rows8 = 0;
            decimal Tables1Rows9 = 0;
            decimal Tables1Rows10 = 0;
            decimal Tables1Rows11 = 0;
            // Row 2
            decimal Tables2Rows1 = 0;
            decimal Tables2Rows2 = 0;
            decimal Tables2Rows3 = 0;
            decimal Tables2Rows4 = 0;
            decimal Tables2Rows5 = 0;
            decimal Tables2Rows6 = 0;
            decimal Tables2Rows7 = 0;
            decimal Tables2Rows8 = 0;
            decimal Tables2Rows9 = 0;
            decimal Tables2Rows10 = 0;
            decimal Tables2Rows11 = 0;



            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "MonthNo" }, new string[] { "37", Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                int Count = ds.Tables[0].Rows.Count;                
                string Islast_heading = "";
                for (int i = 0; i < Count; i++)               
                {  
                        string pagebreak = "";
                        //if (i == 14 || i == 30 || i == 46 || i == 62)
                        //{
                        //    pagebreak = ".page-break";
                        //    Islast_heading = "Yes";
                        //}

                        htmlStr.Append("<tr class='" + pagebreak + "'>");
                        htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class='" + pagebreak + "'>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th class='text-left'>" + (i + 1).ToString() + "</th>");
                        htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["SalarySec_No"].ToString() + "</th>");
                        htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["SalaryEmp_No"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='3' class='text-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='3'>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["UserName"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'></th>");
                        htmlStr.Append("</tr>");

                        // Row 1
                        htmlStr.Append("<tr>");
                        Tables1Rows1 = Tables1Rows1 + decimal.Parse(ds.Tables[0].Rows[i]["PAY_RECOVERY"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["PAY_RECOVERY"].ToString() + "</td>");

                        Tables1Rows2 = Tables1Rows2 + decimal.Parse(ds.Tables[0].Rows[i]["UNION_CREDIT_SOCIETY_CD"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["UNION_CREDIT_SOCIETY_CD"].ToString() + "</td>");

                        Tables1Rows3 = Tables1Rows3 + decimal.Parse(ds.Tables[0].Rows[i]["GRAIN_ADVANCE"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["GRAIN_ADVANCE"].ToString() + "</td>");

                        Tables1Rows4 = Tables1Rows4 + decimal.Parse(ds.Tables[0].Rows[i]["FESTIVAL_ADVANCE"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["FESTIVAL_ADVANCE"].ToString() + " </td>");

                        Tables1Rows5 = Tables1Rows5 + decimal.Parse(ds.Tables[0].Rows[i]["CREDIT_SOC_CD_DEPOSIT"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["CREDIT_SOC_CD_DEPOSIT"].ToString() + "</td>");

                        Tables1Rows6 = Tables1Rows6 + decimal.Parse(ds.Tables[0].Rows[i]["CR_SOCIETY_MT_LOAN"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["CR_SOCIETY_MT_LOAN"].ToString() + "</td>");

                        Tables1Rows7 = Tables1Rows7 + decimal.Parse(ds.Tables[0].Rows[i]["CR_SOCIETY_RD"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["CR_SOCIETY_RD"].ToString() + "</td>");

                        Tables1Rows8 = Tables1Rows8 + decimal.Parse(ds.Tables[0].Rows[i]["CR_SOCIETY_ST_LOAN"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["CR_SOCIETY_ST_LOAN"].ToString() + "</td>");

                        Tables1Rows9 = Tables1Rows9 + decimal.Parse(ds.Tables[0].Rows[i]["UNION_CREDIT_SOCIETY_LOAN"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["UNION_CREDIT_SOCIETY_LOAN"].ToString() + "</td>");

                        Tables1Rows10 = Tables1Rows10 + decimal.Parse(ds.Tables[0].Rows[i]["UNION_HRA_DEDUCTION"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["UNION_HRA_DEDUCTION"].ToString() + "</td>");

                        Tables1Rows11 = Tables1Rows11 + decimal.Parse(ds.Tables[0].Rows[i]["UNION_REC"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["UNION_REC"].ToString() + "</td>");

                        htmlStr.Append("</tr>");



                        htmlStr.Append("<tr>");
                        Tables2Rows1 = Tables2Rows1 + decimal.Parse(ds.Tables[0].Rows[i]["BL_PAY_RECOVERY"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_PAY_RECOVERY"].ToString() + "</td>");

                        Tables2Rows2 = Tables2Rows2 + decimal.Parse(ds.Tables[0].Rows[i]["BL_UNION_CREDIT_SOCIETY_CD"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_UNION_CREDIT_SOCIETY_CD"].ToString() + "</td>");

                        Tables2Rows3 = Tables2Rows3 + decimal.Parse(ds.Tables[0].Rows[i]["BL_GRAIN_ADVANCE"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_GRAIN_ADVANCE"].ToString() + "</td>");

                        Tables2Rows4 = Tables2Rows4 + decimal.Parse(ds.Tables[0].Rows[i]["BL_FESTIVAL_ADVANCE"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_FESTIVAL_ADVANCE"].ToString() + "</td>");

                        Tables2Rows5 = Tables2Rows5 + decimal.Parse(ds.Tables[0].Rows[i]["BL_CREDIT_SOC_CD_DEPOSIT"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_CREDIT_SOC_CD_DEPOSIT"].ToString() + "</td>");

                        Tables2Rows6 = Tables2Rows6 + decimal.Parse(ds.Tables[0].Rows[i]["BL_CR_SOCIETY_MT_LOAN"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_CR_SOCIETY_MT_LOAN"].ToString() + "</td>");

                        Tables2Rows7 = Tables2Rows7 + decimal.Parse(ds.Tables[0].Rows[i]["BL_CR_SOCIETY_RD"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_CR_SOCIETY_RD"].ToString() + "</td>");

                        Tables2Rows8 = Tables2Rows8 + decimal.Parse(ds.Tables[0].Rows[i]["BL_CR_SOCIETY_ST_LOAN"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_CR_SOCIETY_ST_LOAN"].ToString() + "</td>");

                        Tables2Rows9 = Tables2Rows9 + decimal.Parse(ds.Tables[0].Rows[i]["BL_UNION_CREDIT_SOCIETY_LOAN"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_UNION_CREDIT_SOCIETY_LOAN"].ToString() + "</td>");

                        Tables2Rows10 = Tables2Rows10 + decimal.Parse(ds.Tables[0].Rows[i]["BL_UNION_HRA_DEDUCTION"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_UNION_HRA_DEDUCTION"].ToString() + "</td>");

                        Tables2Rows11 = Tables2Rows11 + decimal.Parse(ds.Tables[0].Rows[i]["BL_UNION_REC"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds.Tables[0].Rows[i]["BL_UNION_REC"].ToString() + "</td>");

                        htmlStr.Append("</tr>");

                        htmlStr.Append("</table>");
                        htmlStr.Append("</tr>");
                }
                if (Islast_heading == "Yes")
                {
                    /**********************************************************************/
                    //htmlStr.Append(repeadtCodePrintPage);

                    htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                    htmlStr.Append("<thead  style='border-bottom: 0px solid white;'>");
                    
                    
                    /*****A Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>PAY RECOVERY</th>");
                    htmlStr.Append("<th class='empearning'>UNION CREDIT SOCIETY CD</th>");
                    htmlStr.Append("<th class='empearning'>GRAIN ADVANCE</th>");
                    htmlStr.Append("<th class='empearning'>FESTIVAL ADVANCE</th>");
                    htmlStr.Append("<th class='empearning'>CREDIT SOC CD DEPOSIT</th>");
                    htmlStr.Append("<th class='empearning'>CR SOCIETY MT LOAN</th>");
                    htmlStr.Append("<th class='empearning'>CR SOCIETY RD</th>");
                    htmlStr.Append("<th class='empearning'>CR SOCIETY ST LOAN</th>");
                    htmlStr.Append("<th class='empearning'>UNION CREDIT SOCIETY LOAN</th>");
                    htmlStr.Append("<th class='empearning'>UNION HRA DEDUCTION</th>");
                    htmlStr.Append("<th class='empearning'>UNION REC</th>");
                    htmlStr.Append("</tr>");
                    /*****B Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>BL PAY RECOVERY</th>");
                    htmlStr.Append("<th class='empearning'>BL UNION CREDIT SOCIETY CD</th>");
                    htmlStr.Append("<th class='empearning'>BL GRAIN ADVANCE</th>");
                    htmlStr.Append("<th class='empearning'>BL FESTIVAL ADVANCE</th>");
                    htmlStr.Append("<th class='empearning'>BL CREDIT SOC CD DEPOSIT</th>");
                    htmlStr.Append("<th class='empearning'>BL CR SOCIETY MT LOAN</th>");
                    htmlStr.Append("<th class='empearning'>BL CR_SOCIETY RD</th>");
                    htmlStr.Append("<th class='empearning'>BL CR_SOCIETY ST LOAN</th>");
                    htmlStr.Append("<th class='empearning'>BL UNION_CREDIT SOCIETY LOAN</th>");
                    htmlStr.Append("<th class='empearning'>BL UNION HRA DEDUCTION</th>");
                    htmlStr.Append("<th class='empearning'>BL UNION REC</th>");
                    htmlStr.Append("</tr>");


                    htmlStr.Append("</thead>");
                    htmlStr.Append("<tbody>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<table>");
                    /**********************************************************************/
                }
                htmlStr.Append("<tr>");
                htmlStr.Append("<table style='margin-top: 10px;background: beige;'>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th colspan='11' style='text-align: center;font-weight: 700;'>Report Summary</th>");
                htmlStr.Append("</tr>");

                /*****A Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows9.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows11.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****B Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows9.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("</table>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                DivDetail.InnerHtml = htmlStr.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
