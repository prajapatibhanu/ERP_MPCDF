using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_Payroll_Rpt_PayrollSectionWiseEmployeeWiseSummary : System.Web.UI.Page
{
    DataSet ds, ds_dynamic;
    AbstApiDBApi objdb = new APIProcedure();
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    CultureInfo cult = new CultureInfo("gu-IN", true);

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

                    ddlOffice.Enabled = false;
                    DivHead.Visible = false;
                    if (Session["Office_ID"].ToString() == "1")
                    {
                        ddlOffice.Enabled = true;
                    }
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

                        ddlOffice.SelectedValue = Session["Office_ID"].ToString();
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
            if (ddlOffice.SelectedIndex > 0 && ddlFinancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
            {
                


                FillSalary_Dynamic2();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalary_Dynamic2()
    {
        DataSet ds11 = new DataSet();
        DataTable dt1, dt2, dt3, dt4, dt5 = new DataTable();
        try
        {
            DivDetail.InnerHtml = "";
            string Office_ID = ddlOffice.SelectedValue.ToString();
            string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
            string Salary_MonthNo = ddlMonth.SelectedValue.ToString();
            string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

            lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();



            DivHead.Visible = true;
            ds11 = objdb.ByProcedure("SpPayrollSalaryDetail_New_2511221"
               , new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo"
                    , "Emp_TypeOfPost", "Year", "MonthNo" }
             , new string[] { "1", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString()
                  , Salary_Year, Salary_MonthNo }, "dataset");
           // ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "10", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds11.Tables[0].Rows.Count != 0)
            {




                int Count = ds11.Tables[0].Rows.Count;
                int k = 0;

                dt1 = ds11.Tables[1];
                dt2 = ds11.Tables[2];
                dt3 = ds11.Tables[3];
                dt4 = ds11.Tables[4];
                dt5 = ds11.Tables[5];



                if (ds11.Tables[6].Rows.Count > 0 != null)
                {
                    lblOffice.Text = ds11.Tables[6].Rows[0]["Office_Name"].ToString();
                }

              

                StringBuilder htmlStr = new StringBuilder();
                /****************/
                String MainHeader = GetHeaders(dt1, dt2);
                htmlStr.Append(MainHeader.ToString());
                /*****************/
                string Islast_heading = "";
                string SecNo = "";
                for (int i = 0; i < Count; i++)
                {
                    k++;
                    string Emp_ID = ds11.Tables[0].Rows[i]["Emp_ID"].ToString();

                    String EmpData = "";
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class=''>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th class='text-left'>" + k.ToString() + "</th>");
                    htmlStr.Append("<th class='text-left'>" + ds11.Tables[0].Rows[i]["SecNo"].ToString() + "</th>");
                    htmlStr.Append("<th class='text-left'>" + ds11.Tables[0].Rows[i]["EmpNo"].ToString() + "</th>");
                    htmlStr.Append("<th colspan='2' class='text-left'>" + ds11.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds11.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds11.Tables[0].Rows[i]["PrDay"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds11.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</th>");
                    htmlStr.Append("<th colspan='2'>'" + ds11.Tables[0].Rows[i]["SalaryAccount"].ToString() + "'</th>");
                    htmlStr.Append("<th>" + ds11.Tables[0].Rows[i]["EPF_No"].ToString() + "</th>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th class='text-left'></th>");
                    htmlStr.Append("<th class='text-left'>" + ds11.Tables[0].Rows[i]["UserName"].ToString() + "</th>");
                    htmlStr.Append("<th class='text-left'></th>");
                    htmlStr.Append("<th colspan='2' class='text-left'>" + ds11.Tables[0].Rows[i]["Emp_PanCardNo"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds11.Tables[0].Rows[i]["UAN_No"].ToString() + "</th>");

                    htmlStr.Append("<th colspan=''>" + ds11.Tables[0].Rows[i]["PayScale_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds11.Tables[0].Rows[i]["GradePay_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan='2'>'" + ds11.Tables[0].Rows[i]["Level_Name"].ToString() + "'</th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("</tr>");

                    EmpData = GetEmpData(Emp_ID, dt3, dt4, dt5);
                    htmlStr.Append(EmpData.ToString());

                    if (i == 0)
                        SecNo = ds11.Tables[0].Rows[i]["SecNo"].ToString();

                    if (SecNo != "")
                    {
                        if (i + 1 < Count)
                        {
                            if (ds11.Tables[0].Rows[i + 1]["SecNo"].ToString() != SecNo)
                            {
                                htmlStr.Append(GetSectionWiseTotal(SecNo).ToString());
                                htmlStr.Append("<p style='page-break-after: always'></p>");
                                SecNo = ds11.Tables[0].Rows[i + 1]["SecNo"].ToString();
                            }
                        }
                        else
                        {
                            if (i == Count - 1)
                                htmlStr.Append(GetSectionWiseTotal(SecNo).ToString());

                        }


                    }




                    //   SecNo = ds.Tables[0].Rows[i]["SecNo"].ToString();



                }
                /******Footer**********/
                //   htmlStr.Append(MainHeader.ToString());
                //String MainFooter = GetFooter();
                //htmlStr.Append(MainFooter.ToString());

                /*****************/

                DivDetail.InnerHtml = htmlStr.ToString();
                if (dt1 != null) { dt1.Dispose(); }
                if (dt2 != null) { dt2.Dispose(); }
                if (dt3 != null) { dt3.Dispose(); }
                if (dt4 != null) { dt4.Dispose(); }
                if (dt5 != null) { dt5.Dispose(); }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds11 != null) { ds11.Dispose(); }

        }
    }
    /***************************/

    public string GetHeaders(DataTable dt1,DataTable dt2)
    {
        String Headers = "";
        string Office_ID = ddlOffice.SelectedValue.ToString();
        string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
        string Salary_MonthNo = ddlMonth.SelectedValue.ToString();
        string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

        /****************/
        Headers += "<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>";
        Headers += "<thead  style='border-bottom: 0px solid white; display: table-header-group;' class='printmaindata'>";

        Headers += "<tr class='tblheadingslip'>";
        Headers += "<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>";
        Headers += "<th colspan='2' class='text-left'>Name Of Employee</th>";
        Headers += "<th colspan=''>Designation</th>";
        Headers += "<th colspan=''>Pr. Day</th>";
        Headers += "<th colspan=''>IFSC Code</th>";
        Headers += "<th colspan='2'>Bank Account No.</th>";
        Headers += "<th>EPF No./GPF No.</th>";
        Headers += "</tr>";

        Headers += "<tr class='tblheadingslip'>";
        Headers += "<th class='text-left'></th><th class='text-left'>Emp Code</th><th class='text-left'>EPF No.</th>";
        Headers += "<th colspan='2' class='text-left'>PAN No.</th>";
        Headers += "<th colspan=''>UAN No.</th>";
        Headers += "<th colspan=''>Pay Band</th>";
        Headers += "<th colspan=''>Grade Pay</th>";
        Headers += "<th colspan='2'>Level</th>";
        Headers += "<th></th>";
        Headers += "</tr>";

        /*****A Row*******/
        int ii, jj = 0;
        int lineno = 0;
       // ds_dynamic = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "MonthNo", "Emp_TypeOfPost" }, new string[] { "34", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
        DataSet ds12 = new DataSet();
        ds12.Merge(dt1);
        ds12.Merge(dt2);
        ds_dynamic = ds12;

        if (dt1 != null) { dt1.Dispose(); }
        if (dt2 != null) { dt2.Dispose(); }
        if (ds12 != null) { ds12.Dispose(); }
        if (ds_dynamic.Tables.Count > 0)
        {
            if (ds_dynamic.Tables[0].Rows.Count != 0)
            {
                int EarningCount = ds_dynamic.Tables[0].Rows.Count;
                int DeductionCount = ds_dynamic.Tables[1].Rows.Count;

                /**********/
                //Earning = new decimal[EarningCount-1];
                //Deductions = new decimal[DeductionCount - 1];
                /***********/
                Earning = new decimal[EarningCount];
                Deductions = new decimal[DeductionCount];
                /***********/

                for (ii = 0; ii < EarningCount; ii++)
                {

                    if (ii == 0 || ii == 9 || ii == 17 || ii == 27)
                    {
                        Headers += "<tr class='tblheadingslip'>";
                        Headers += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
                        lineno++;
                    }
                    if (ii == 0)
                    {
                        Headers += "<th class='empearning'>Basic Pay</th>";
                        Headers += "<th class='empearning'>" + ds_dynamic.Tables[0].Rows[ii]["EarnDeduction_Name"].ToString() + "</th>";
                    }
                    else
                    {
                        Headers += "<th class='empearning'>" + ds_dynamic.Tables[0].Rows[ii]["EarnDeduction_Name"].ToString() + "</th>";
                    }


                    if (ii == 8 || ii == 16 || ii == 26)
                    {
                        Headers += "</tr>";
                    }
                }
                /**********/
                for (ii = 0; ii < DeductionCount; ii++)
                {

                    if (ii == 0 || ii == 10 || ii == 20 || ii == 30)
                    {
                        Headers += "<tr class='tblheadingslip'>";
                        Headers += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
                        lineno++;
                    }

                    Headers += "<th class='empearning'>" + ds_dynamic.Tables[1].Rows[ii]["EarnDeduction_Name"].ToString() + "</th>";

                    if (ii == 9 || ii == 19 || ii == 29)
                    {
                        Headers += "</tr>";
                    }
                }
            }
        }

        Headers += "<tr class='tblheadingslip'>";
        Headers += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
        Headers += "<th></th>";
        Headers += "<th></th>";
        Headers += "<th></th>";
        Headers += "<th></th>";
        Headers += "<th></th>";
        Headers += "<th></th>";
        Headers += "<th></th>";
        Headers += "<th class='empearning'>Gross</th>";
        Headers += "<th class='empdeduction'>Tot. Ded</th>";
        Headers += "<th class='empbalance'>Net Amount</th>";
        Headers += "</tr>";
        Headers += "</thead>";
        Headers += "<tbody>";
        Headers += "<tr>";
        Headers += "<table>";
        return Headers;
    }
    private string GetEmpData(string Emp_ID, DataTable dt1, DataTable e, DataTable d)
    {
        string EmpData = "";
        string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

        DataSet ds111 = new DataSet();
        ds111.Merge(dt1);



        ds111.Tables[0].DefaultView.RowFilter = "Emp_ID = '" + Emp_ID + "'";
        DataTable dt111 = new DataTable();
        dt111 = ds111.Tables[0].DefaultView.ToTable();

        DataSet ds1 = new DataSet();
        ds1.Merge(dt111);
        if (dt111 != null) { dt111.Dispose(); }
       // DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "11", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {

            int ii = 0;
            int lineno = 0;
           // ds_dynamic = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "MonthNo", "Emp_TypeOfPost", "Emp_ID" }, new string[] { "35", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString(), Emp_ID.ToString() }, "dataset");
         

            DataSet ee = new DataSet();
            ee.Merge(e);
            DataSet eed = new DataSet();
            eed.Merge(d);
            if (e != null) { e.Dispose(); }
            if (d != null) { d.Dispose(); }

            ee.Tables[0].DefaultView.RowFilter = "Emp_ID = '" + Emp_ID + "'";
            DataTable eee = new DataTable();
            eee = ee.Tables[0].DefaultView.ToTable();

            eed.Tables[0].DefaultView.RowFilter = "Emp_ID = '" + Emp_ID + "'";
            DataTable edd = new DataTable();
            edd = eed.Tables[0].DefaultView.ToTable();

            DataRow[] dr = edd.Select("EarnDeduction_ID ='" + "50" + "'");
            if (dr.Length != 0)
            {
                DataRow newRow = edd.NewRow();
                // We "clone" the row
                newRow.ItemArray = dr[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr[0]);
                edd.Rows.InsertAt(newRow, 20);
            }
            DataRow[] dr1 = edd.Select("EarnDeduction_ID ='" + "16" + "'");
            if (dr1.Length != 0)
            {

                DataRow newRow1 = edd.NewRow();
                // We "clone" the row
                newRow1.ItemArray = dr1[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr1[0]);
                edd.Rows.InsertAt(newRow1, 21);
            }

            DataRow[] dr2 = edd.Select("EarnDeduction_ID ='" + "63" + "'");
            if (dr2.Length != 0)
            {
                DataRow newRow2 = edd.NewRow();
                // We "clone" the row
                newRow2.ItemArray = dr2[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr2[0]);
                edd.Rows.InsertAt(newRow2, 22);
            }

            DataRow[] dr3 = edd.Select("EarnDeduction_ID ='" + "83" + "'");
            if (dr3.Length != 0)
            {
                DataRow newRow3 = edd.NewRow();
                // We "clone" the row
                newRow3.ItemArray = dr3[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr3[0]);
                edd.Rows.InsertAt(newRow3, 23);
            }

            DataRow[] dr4 = edd.Select("EarnDeduction_ID ='" + "14" + "'");
            if (dr4.Length != 0)
            {
                DataRow newRow4 = edd.NewRow();
                // We "clone" the row
                newRow4.ItemArray = dr4[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr4[0]);
                edd.Rows.InsertAt(newRow4, 24);
            }

            DataRow[] dr5 = edd.Select("EarnDeduction_ID ='" + "80" + "'");
            if (dr5.Length != 0)
            {
                DataRow newRow5 = edd.NewRow();
                // We "clone" the row
                newRow5.ItemArray = dr5[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr5[0]);
                edd.Rows.InsertAt(newRow5, 25);
            }

            DataRow[] dr6 = edd.Select("EarnDeduction_ID ='" + "15" + "'");
            if (dr6.Length != 0)
            {
                DataRow newRow6 = edd.NewRow();
                // We "clone" the row
                newRow6.ItemArray = dr6[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr6[0]);
                edd.Rows.InsertAt(newRow6, 26);
            }

            DataRow[] dr7 = edd.Select("EarnDeduction_ID ='" + "10" + "'");
            if (dr7.Length != 0)
            {
                DataRow newRow7 = edd.NewRow();
                // We "clone" the row
                newRow7.ItemArray = dr7[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr7[0]);
                edd.Rows.InsertAt(newRow7, 27);
            }

            DataRow[] dr8 = edd.Select("EarnDeduction_ID ='" + "47" + "'");
            if (dr8.Length != 0)
            {
                DataRow newRow8 = edd.NewRow();
                // We "clone" the row
                newRow8.ItemArray = dr8[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr8[0]);
                edd.Rows.InsertAt(newRow8, 28);
            }

            DataRow[] dr9 = edd.Select("EarnDeduction_ID ='" + "35" + "'");
            if (dr9.Length != 0)
            {
                DataRow newRow9 = edd.NewRow();
                // We "clone" the row
                newRow9.ItemArray = dr9[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr9[0]);
                edd.Rows.InsertAt(newRow9, 29);
            }


            DataRow[] dr10 = edd.Select("EarnDeduction_ID ='" + "30" + "'");
            if (dr10.Length != 0)
            {
                DataRow newRow10 = edd.NewRow();
                // We "clone" the row
                newRow10.ItemArray = dr10[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr10[0]);
                edd.Rows.InsertAt(newRow10, 30);
            }

            DataRow[] dr11 = edd.Select("EarnDeduction_ID ='" + "54" + "'");
            if (dr11.Length != 0)
            {
                DataRow newRow11 = edd.NewRow();
                // We "clone" the row
                newRow11.ItemArray = dr11[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr11[0]);
                edd.Rows.InsertAt(newRow11, 31);
            }

            DataRow[] dr12 = edd.Select("EarnDeduction_ID ='" + "55" + "'");
            if (dr12.Length != 0)
            {
                DataRow newRow12 = edd.NewRow();
                // We "clone" the row
                newRow12.ItemArray = dr12[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr12[0]);
                edd.Rows.InsertAt(newRow12, 32);
            }

            DataRow[] dr13 = edd.Select("EarnDeduction_ID ='" + "78" + "'");
            if (dr13.Length != 0)
            {
                DataRow newRow13 = edd.NewRow();
                // We "clone" the row
                newRow13.ItemArray = dr13[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr13[0]);
                edd.Rows.InsertAt(newRow13, 33);
            }

            DataRow[] dr14 = edd.Select("EarnDeduction_ID ='" + "33" + "'");
            if (dr14.Length != 0)
            {
                DataRow newRow14 = edd.NewRow();
                // We "clone" the row
                newRow14.ItemArray = dr14[0].ItemArray;
                // We remove the old and insert the new
                edd.Rows.Remove(dr14[0]);
                edd.Rows.InsertAt(newRow14, 34);
            }


            DataSet ds222 = new DataSet();
            ds222.Merge(eee);
            ds222.Merge(edd);
            ds_dynamic = ds222;

            if (ee != null) { ee.Dispose(); }
            if (eee != null) { ee.Dispose(); }
            if (eed != null) { eed.Dispose(); }
            if (edd != null) { edd.Dispose(); }
            if (ds222 != null) { ds222.Dispose(); }
            if (ds_dynamic.Tables.Count > 0)
            {
                if (ds_dynamic.Tables[0].Rows.Count != 0)
                {
                    int EarningCount = ds_dynamic.Tables[0].Rows.Count;
                    int DeductionCount = ds_dynamic.Tables[1].Rows.Count;


                    /**********/
                    //Earning[0] = 0;
                    for (ii = 0; ii < EarningCount; ii++)
                    {
                        Earning[ii] = Earning[ii] + decimal.Parse(ds_dynamic.Tables[0].Rows[ii]["TotalAmount"].ToString());

                        if (ii == 0 || ii == 9 || ii == 17 || ii == 27)
                        {
                            EmpData += "<tr class='tblheadingslip'>";
                            EmpData += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
                            lineno++;
                        }
                        if (ii == 0)
                        {
                            EmpData += "<th class='empearning'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</th>";
                            EmpData += "<th class='empearning'>" + ds_dynamic.Tables[0].Rows[ii]["TotalAmount"].ToString() + "</th>";
                        }
                        else
                        {
                            EmpData += "<th class='empearning'>" + ds_dynamic.Tables[0].Rows[ii]["TotalAmount"].ToString() + "</th>";
                        }


                        if (ii == 8 || ii == 16 || ii == 26)
                        {
                            EmpData += "</tr>";
                        }
                    }
                    /**********/
                    //Deductions[0] = 0;
                    for (ii = 0; ii < DeductionCount; ii++)
                    {
                        Deductions[ii] = Deductions[ii] + decimal.Parse(ds_dynamic.Tables[1].Rows[ii]["TotalAmount"].ToString());
                        if (ii == 0 || ii == 10 || ii == 20 || ii == 30)
                        {
                            EmpData += "<tr class='tblheadingslip'>";
                            EmpData += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
                            lineno++;
                        }

                        EmpData += "<th class='empearning'>" + ds_dynamic.Tables[1].Rows[ii]["TotalAmount"].ToString() + "</th>";

                        if (ii == 9 || ii == 19 || ii == 29)
                        {
                            EmpData += "</tr>";
                        }
                    }
                }
            }
        }
        EmpData += "<tr>";
        EmpData += "<td class='alignright'></td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</td>";
        EmpData += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</td>";
        EmpData += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>";

        SectionBasic = SectionBasic + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
        SectionEarningTotal = SectionEarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString());
        SectionDeductionTotal = SectionDeductionTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString());
        SectionNetSalary = SectionNetSalary + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString());

        TotalBasic = TotalBasic + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
        TotalEarningTotal = TotalEarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString());
        TotalDeductionTotal = TotalDeductionTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString());
        TotalNetSalary = TotalNetSalary + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString());

        EmpData += "</tr>";
        EmpData += "</table>";
        EmpData += "</tr>";
        return EmpData;
    }
    public string GetFooter()
    {
        string EmpData = "";
        string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        int ii = 0;
        int lineno = 0;
        int EarningCount = Earning.Length;
        int DeductionCount = Deductions.Length;
        /**********/
        for (ii = 0; ii < EarningCount; ii++)
        {
            if (ii == 0 || ii == 9 || ii == 17 || ii == 27)
            {
                EmpData += "<tr class='tblheadingslip'>";
                EmpData += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
                lineno++;
            }

            if (ii == 0)
            {
                EmpData += "<th class='empearning'>" + TotalBasic.ToString() + "</th>";
            }

            EmpData += "<th class='empearning'>" + Earning[ii].ToString() + "</th>";


            if (ii == 8 || ii == 16 || ii == 26)
            {
                EmpData += "</tr>";
            }
        }
        /**********/
        for (ii = 0; ii < DeductionCount; ii++)
        {
            if (ii == 0 || ii == 10 || ii == 20 || ii == 30)
            {
                EmpData += "<tr class='tblheadingslip'>";
                EmpData += "<th class='empearning'>" + Alphabet[lineno].ToString() + "</th>";
                lineno++;
            }

            EmpData += "<th class='empearning'>" + Deductions[ii].ToString() + "</th>";

            if (ii == 9 || ii == 19 || ii == 29)
            {
                EmpData += "</tr>";
            }
        }
        EmpData += "<tr>";
        EmpData += "<td class='alignright'></td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'> </td>";
        EmpData += "<td class='alignright'>" + TotalEarningTotal.ToString() + "</td>";
        EmpData += "<td class='alignright'>" + TotalDeductionTotal.ToString() + "</td>";
        EmpData += "<td class='alignright'>" + TotalNetSalary.ToString() + "</td>";
        EmpData += "</tr>";
        EmpData += "</table>";
        EmpData += "</tr>";
        return EmpData;
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


    private string GetSectionWiseTotal(string secno)
    {
        string EmpSection = "";
        DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryFinalSummary",
                 new string[] { "flag", "Office_ID", "Year", "MonthNo", "SalarySection_No" },
                 new string[] { "3", ddlOffice.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), secno.ToString() }, "dataset");
        if (ds1.Tables.Count > 0)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                EmpSection += "</table>";
                EmpSection += "<table style='width:100%;'>";
                EmpSection += "<tr class='tblheadingslip'>";
                EmpSection += "<td class='alignright'>";
                EmpSection += "<table style='width:100%;background: beige;' class=''>";
                EmpSection += "<tr class='tblheadingslip'>";
                EmpSection += "<td class='empearning'>EARNINGS</td>";
                EmpSection += "<td class='alignright'></td>";
                EmpSection += "</tr>";
                EmpSection += "<tr>";
                EmpSection += "<th class='empearning'>BASIC :</th>";
                EmpSection += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["BasicSalary"].ToString() + "</td>";
                EmpSection += "</tr>";

                if (ds1.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
                    {
                        EmpSection += "<tr class='tblheadingslip'>";
                        EmpSection += "<th class='empearning'>" + ds1.Tables[1].Rows[i]["EarnDeduction_Name"].ToString() + "</th>";
                        EmpSection += "<td class='alignright'>" + ds1.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
                        EmpSection += "</tr>";
                    }

                }

                EmpSection += "<tr class='tblheadingslip'>";
                EmpSection += "<th class='empearning'>GROSS SALARY :</th>";
                EmpSection += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["GrossSalary"].ToString() + "</td>";
                EmpSection += "</tr>";

                EmpSection += "<tr class='tblheadingslip'>";
                EmpSection += "<th class='empearning'>NET AMOUNT :</th>";
                EmpSection += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["GrossSalary"].ToString() + "</td>";
                EmpSection += "</tr>";
                EmpSection += "</table>";
                EmpSection += "</td>";

                // decuction
                EmpSection += "<td class='alignright'>";
                EmpSection += "<table style='width:100%;background: beige;' class=''>";
                EmpSection += "<tr class='tblheadingslip'>";
                EmpSection += "<td class='empdeduction'>DEDUCTION</td>";
                EmpSection += "<td class='alignright'></td>";
                EmpSection += "</tr>";
                if (ds1.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
                    {
                        EmpSection += "<tr class='tblheadingslip'>";
                        EmpSection += "<th class='empdeduction'>" + ds1.Tables[2].Rows[i]["EarnDeduction_Name"].ToString() + "</th>";
                        EmpSection += "<td class='alignright'>" + ds1.Tables[2].Rows[i]["Amount"].ToString() + "</td>";
                        EmpSection += "</tr'>";
                    }

                }
                EmpSection += "<tr class='tblheadingslip'>";
                EmpSection += "<th class='empdeduction'>TOTAL DEDUCTION</th>";
                EmpSection += "<td class='alignright'>" + ds1.Tables[0].Rows[0]["TotalDeduction"].ToString() + "</td>";
                EmpSection += "</tr>";
                EmpSection += "</table>";
                EmpSection += "</td>";

                // end of deduction
                EmpSection += "</tr>";
                EmpSection += "</table>";
                
              
            }
            if (ds1 != null) { ds1.Dispose(); }
        }

        return EmpSection;
    }
}
