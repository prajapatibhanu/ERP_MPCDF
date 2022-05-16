using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarnDeductionDetailFullNew : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    //static decimal Permanent_DARate = 9;
    //static decimal Fixed_DARate = 148;
    //static decimal Permanent_DARate = 12;  // 23Sep2019
	// static decimal Permanent_DARate = 20;  // 1 Oct 2021
	 static decimal Permanent_DARate = 31;  // 26 APR 2022
    static decimal Fixed_DARate = 154; // 23Sep2019

   // static decimal Permanent_6thpay_DARate = 154; // 18Oct2021
   static decimal Permanent_6thpay_DARate = 171; // 29nov2021

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                lblPermanent_DARate.Text = Permanent_DARate.ToString();
                lblFixed_DARate.Text = Fixed_DARate.ToString();
                divDetail.Visible = false;
                FillDropdown();
                // FillGrid();

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds = null;

            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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


            DataSet dsNew;
            decimal Amount = 0;
            decimal Jan_AmountDA = 0;
            decimal Feb_AmountDA = 0;
            decimal Mar_AmountDA = 0;
            decimal Apr_AmountDA = 0;
            decimal May_AmountDA = 0;
            decimal Jun_AmountDA = 0;
            decimal Jul_AmountDA = 0;
            decimal Aug_AmountDA = 0;
            decimal Sep_AmountDA = 0;
            decimal Oct_AmountDA = 0;
            decimal Nov_AmountDA = 0;
            decimal Dec_AmountDA = 0;

            decimal Jan_AmountEPF = 0;
            decimal Feb_AmountEPF = 0;
            decimal Mar_AmountEPF = 0;
            decimal Apr_AmountEPF = 0;
            decimal May_AmountEPF = 0;
            decimal Jun_AmountEPF = 0;
            decimal Jul_AmountEPF = 0;
            decimal Aug_AmountEPF = 0;
            decimal Sep_AmountEPF = 0;
            decimal Oct_AmountEPF = 0;
            decimal Nov_AmountEPF = 0;
            decimal Dec_AmountEPF = 0;

            decimal Jan_AmountBASIC = 0;
            decimal Feb_AmountBASIC = 0;
            decimal Mar_AmountBASIC = 0;
            decimal Apr_AmountBASIC = 0;
            decimal May_AmountBASIC = 0;
            decimal Jun_AmountBASIC = 0;
            decimal Jul_AmountBASIC = 0;
            decimal Aug_AmountBASIC = 0;
            decimal Sep_AmountBASIC = 0;
            decimal Oct_AmountBASIC = 0;
            decimal Nov_AmountBASIC = 0;
            decimal Dec_AmountBASIC = 0;

            decimal Deduction_PTax = 0;

            string Year_2 = (Int32.Parse(ddlYear.SelectedValue.ToString()) + 1).ToString();

            decimal Jan_AmountHRA = 0;
            decimal Feb_AmountHRA = 0;
            decimal Mar_AmountHRA = 0;
            decimal Apr_AmountHRA = 0;
            decimal May_AmountHRA = 0;
            decimal Jun_AmountHRA = 0;
            decimal Jul_AmountHRA = 0;
            decimal Aug_AmountHRA = 0;
            decimal Sep_AmountHRA = 0;
            decimal Oct_AmountHRA = 0;
            decimal Nov_AmountHRA = 0;
            decimal Dec_AmountHRA = 0;


            decimal Jan_AmountCONVEYANCE = 0;
            decimal Feb_AmountCONVEYANCE = 0;
            decimal Mar_AmountCONVEYANCE = 0;
            decimal Apr_AmountCONVEYANCE = 0;
            decimal May_AmountCONVEYANCE = 0;
            decimal Jun_AmountCONVEYANCE = 0;
            decimal Jul_AmountCONVEYANCE = 0;
            decimal Aug_AmountCONVEYANCE = 0;
            decimal Sep_AmountCONVEYANCE = 0;
            decimal Oct_AmountCONVEYANCE = 0;
            decimal Nov_AmountCONVEYANCE = 0;
            decimal Dec_AmountCONVEYANCE = 0;

            decimal Jan_AmountWashing = 0;
            decimal Feb_AmountWashing = 0;
            decimal Mar_AmountWashing = 0;
            decimal Apr_AmountWashing = 0;
            decimal May_AmountWashing = 0;
            decimal Jun_AmountWashing = 0;
            decimal Jul_AmountWashing = 0;
            decimal Aug_AmountWashing = 0;
            decimal Sep_AmountWashing = 0;
            decimal Oct_AmountWashing = 0;
            decimal Nov_AmountWashing = 0;
            decimal Dec_AmountWashing = 0;

            decimal Jan_AmountAttendanceBonus = 0;
            decimal Feb_AmountAttendanceBonus = 0;
            decimal Mar_AmountAttendanceBonus = 0;
            decimal Apr_AmountAttendanceBonus = 0;
            decimal May_AmountAttendanceBonus = 0;
            decimal Jun_AmountAttendanceBonus = 0;
            decimal Jul_AmountAttendanceBonus = 0;
            decimal Aug_AmountAttendanceBonus = 0;
            decimal Sep_AmountAttendanceBonus = 0;
            decimal Oct_AmountAttendanceBonus = 0;
            decimal Nov_AmountAttendanceBonus = 0;
            decimal Dec_AmountAttendanceBonus = 0;


            decimal Jan_AmountMedicalBonus = 0;
            decimal Feb_AmountMedicalBonus = 0;
            decimal Mar_AmountMedicalBonus = 0;
            decimal Apr_AmountMedicalBonus = 0;
            decimal May_AmountMedicalBonus = 0;
            decimal Jun_AmountMedicalBonus = 0;
            decimal Jul_AmountMedicalBonus = 0;
            decimal Aug_AmountMedicalBonus = 0;
            decimal Sep_AmountMedicalBonus = 0;
            decimal Oct_AmountMedicalBonus = 0;
            decimal Nov_AmountMedicalBonus = 0;
            decimal Dec_AmountMedicalBonus = 0;

            decimal Jan_OrderlyAllowance = 0;
            decimal Feb_OrderlyAllowance = 0;
            decimal Mar_OrderlyAllowance = 0;
            decimal Apr_OrderlyAllowance = 0;
            decimal May_OrderlyAllowance = 0;
            decimal Jun_OrderlyAllowance = 0;
            decimal Jul_OrderlyAllowance = 0;
            decimal Aug_OrderlyAllowance = 0;
            decimal Sep_OrderlyAllowance = 0;
            decimal Oct_OrderlyAllowance = 0;
            decimal Nov_OrderlyAllowance = 0;
            decimal Dec_OrderlyAllowance = 0;

            decimal Jan_GradePay = 0;
            decimal Feb_GradePay = 0;
            decimal Mar_GradePay = 0;
            decimal Apr_GradePay = 0;
            decimal May_GradePay = 0;
            decimal Jun_GradePay = 0;
            decimal Jul_GradePay = 0;
            decimal Aug_GradePay = 0;
            decimal Sep_GradePay = 0;
            decimal Oct_GradePay = 0;
            decimal Nov_GradePay = 0;
            decimal Dec_GradePay = 0;

			btnReset.Visible = true;
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "1" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'"; // hra
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jan_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Jan_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jan_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Jan_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Jan_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jan_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jan_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jan_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Jan_GradePay;
                            Jan_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Jan_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }                       
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Jan_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Jan_AmountEPF = Math.Round(((Amount + Jan_GradePay + Jan_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Jan_AmountEPF = Math.Round(((Amount + Jan_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Jan_AmountEPF = Math.Round(((Amount + Jan_AmountDA) * 12) / 100);
                    //}
                    Jan_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "2" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Feb_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                //string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Feb_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Feb_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Feb_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Feb_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }


                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Feb_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Feb_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Feb_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Feb_GradePay;
                            Feb_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Feb_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }                       
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Feb_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Feb_AmountEPF = Math.Round(((Amount + Feb_GradePay + Feb_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Feb_AmountEPF = Math.Round(((Amount + Feb_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Feb_AmountEPF = Math.Round(((Amount + Feb_AmountDA) * 12) / 100);
                    //}
                    Feb_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", Year_2, ddlEmployee.SelectedValue.ToString(), "3" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Mar_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Mar_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Mar_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Mar_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Mar_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Mar_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Mar_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Mar_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }
                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Mar_GradePay;
                            Mar_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Mar_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }                       
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Mar_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Mar_AmountEPF = Math.Round(((Amount + Mar_GradePay + Mar_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Mar_AmountEPF = Math.Round(((Amount + Mar_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Mar_AmountEPF = Math.Round(((Amount + Mar_AmountDA) * 12) / 100);
                    //}
                    Mar_AmountBASIC = Amount;

                }

            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "4" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Apr_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }


                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                //  string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Apr_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Apr_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Apr_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Apr_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Apr_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Apr_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Apr_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }



                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    { 
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Apr_GradePay;
                            Apr_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Apr_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }                      
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Apr_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Apr_AmountEPF = Math.Round(((Amount + Apr_GradePay + Apr_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Apr_AmountEPF = Math.Round(((Amount + Apr_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Apr_AmountEPF = Math.Round(((Amount + Apr_AmountDA) * 12) / 100);
                    //}
                    Apr_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "5" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                May_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                May_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                May_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    May_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    May_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                May_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                May_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                May_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + May_GradePay;
                            May_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            May_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        May_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        May_AmountEPF = Math.Round(((Amount + May_GradePay + May_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        May_AmountEPF = Math.Round(((Amount + May_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    May_AmountEPF = Math.Round(((Amount + May_AmountDA) * 12) / 100);
                    //}
                    May_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "6" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jun_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Jun_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jun_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Jun_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Jun_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jun_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jun_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jun_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Jun_GradePay;
                            Jun_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Jun_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Jun_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Jun_AmountEPF = Math.Round(((Amount + Jun_GradePay + Jun_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Jun_AmountEPF = Math.Round(((Amount + Jun_AmountDA) * 12) / 100);
                    }
                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Jun_AmountEPF = Math.Round(((Amount + Jun_AmountDA) * 12) / 100);
                    //}
                    Jun_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "7" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jul_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                //  string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Jul_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jul_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Jul_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Jul_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jul_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jul_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Jul_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Jul_GradePay;
                            Jul_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Jul_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Jul_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Jul_AmountEPF = Math.Round(((Amount + Jul_GradePay + Jul_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Jul_AmountEPF = Math.Round(((Amount + Jul_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Jul_AmountEPF = Math.Round(((Amount + Jul_AmountDA) * 12) / 100);
                    //}
                    Jul_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "8" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());
                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Aug_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Aug_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Aug_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Aug_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Aug_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Aug_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Aug_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Aug_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }


                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Aug_GradePay;
                            Aug_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Aug_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Aug_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);


                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Aug_AmountEPF = Math.Round(((Amount + Aug_GradePay + Aug_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Aug_AmountEPF = Math.Round(((Amount + Aug_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Aug_AmountEPF = Math.Round(((Amount + Aug_AmountDA) * 12) / 100);
                    //}
                    Aug_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "9" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Sep_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Sep_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Sep_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {

                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                   || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Sep_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Sep_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Sep_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Sep_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Sep_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Sep_GradePay;
                            Sep_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Sep_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }

                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Sep_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Sep_AmountEPF = Math.Round(((Amount + Sep_GradePay + Sep_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Sep_AmountEPF = Math.Round(((Amount + Sep_AmountDA) * 12) / 100);
                    }
                   

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Sep_AmountEPF = Math.Round(((Amount + Sep_AmountDA) * 12) / 100);
                    //}
                    Sep_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "10" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Oct_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                //string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Oct_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Oct_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                  || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Oct_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Oct_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }
                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Oct_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Oct_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Oct_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Oct_GradePay;
                            Oct_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Oct_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Oct_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Oct_AmountEPF = Math.Round(((Amount + Oct_GradePay + Oct_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Oct_AmountEPF = Math.Round(((Amount + Oct_AmountDA) * 12) / 100);
                    }
                 
                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Oct_AmountEPF = Math.Round(((Amount + Oct_AmountDA) * 12) / 100);
                    //}
                    Oct_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "11" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Nov_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                //string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Nov_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Nov_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Nov_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Nov_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Nov_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Nov_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Nov_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Nov_GradePay;
                            Nov_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Nov_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Nov_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Nov_AmountEPF = Math.Round(((Amount + Nov_GradePay + Nov_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Nov_AmountEPF = Math.Round(((Amount + Nov_AmountDA) * 12) / 100);
                    }
                 
                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Nov_AmountEPF = Math.Round(((Amount + Nov_AmountDA) * 12) / 100);
                    //}
                    Nov_AmountBASIC = Amount;
                }
            }
            dsNew = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "MonthNo" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), "12" }, "dataset");
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    Amount = decimal.Parse(dsNew.Tables[0].Rows[0]["Salary_Basic"].ToString());

                    if (dsNew.Tables[1].Rows.Count > 0)
                    {
                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '2'";
                        DataTable dt = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["EarnDedution_ID"].ToString() == "2")
                            {
                                decimal dxt = (Convert.ToDecimal(dt.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Dec_AmountHRA = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt != null) { dt.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '3'"; // conveyance
                        DataTable dt1 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            if (dt1.Rows[0]["EarnDedution_ID"].ToString() == "3")
                            {
                                decimal dxt = (Convert.ToDecimal(dt1.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                // string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                string thra = (dxt * (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) - Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()))).ToString("0");
                                Dec_AmountCONVEYANCE = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt1 != null) { dt1.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '5'"; // Washing
                        DataTable dt2 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt2.Rows[0]["EarnDedution_ID"].ToString() == "5")
                            {
                                decimal dxt = (Convert.ToDecimal(dt2.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Dec_AmountWashing = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt2 != null) { dt2.Dispose(); }


                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '64'"; // Attendance Bonus
                        DataTable dt3 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt3.Rows.Count > 0)
                        {
                            if (dt3.Rows[0]["EarnDedution_ID"].ToString() == "64")
                            {
                                if (Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysEL"].ToString()) > 0
                                 || Convert.ToDecimal(dsNew.Tables[0].Rows[0]["LeaveDaysML"].ToString()) > 0)
                                {
                                    Dec_AmountAttendanceBonus = Convert.ToDecimal("0");
                                }
                                else
                                {
                                    decimal dxt = (Convert.ToDecimal(dt3.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                    string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                    Dec_AmountAttendanceBonus = Convert.ToDecimal(thra);
                                }

                            }
                        }
                        if (dt3 != null) { dt3.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '65'"; // Medical Bonus
                        DataTable dt4 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt4.Rows.Count > 0)
                        {
                            if (dt4.Rows[0]["EarnDedution_ID"].ToString() == "65")
                            {
                                decimal dxt = (Convert.ToDecimal(dt4.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Dec_AmountMedicalBonus = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt4 != null) { dt4.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '4'"; // Orderly Allowance Bonus
                        DataTable dt5 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt5.Rows.Count > 0)
                        {
                            if (dt5.Rows[0]["EarnDedution_ID"].ToString() == "4")
                            {
                                decimal dxt = (Convert.ToDecimal(dt5.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Dec_OrderlyAllowance = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt5 != null) { dt5.Dispose(); }

                        dsNew.Tables[1].DefaultView.RowFilter = "EarnDedution_ID = '66'"; // Grady Pay
                        DataTable dt6 = (dsNew.Tables[1].DefaultView).ToTable();
                        if (dt6.Rows.Count > 0)
                        {
                            if (dt6.Rows[0]["EarnDedution_ID"].ToString() == "66")
                            {
                                decimal dxt = (Convert.ToDecimal(dt6.Rows[0]["EarnDeductionAmount"].ToString()) / Convert.ToDecimal(dsNew.Tables[0].Rows[0]["TotalDayOfMonth"].ToString()));
                                string thra = (dxt * Convert.ToDecimal(dsNew.Tables[0].Rows[0]["PayableDays"].ToString())).ToString("0");
                                Dec_GradePay = Convert.ToDecimal(thra);
                            }
                        }
                        if (dt6 != null) { dt6.Dispose(); }
                    }

                    if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Permanent")
                    {
                        if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // 6 th pay commission
                        {
                            decimal basic_plus_gpay = Amount + Dec_GradePay;
                            Dec_AmountDA = Math.Round((basic_plus_gpay * Permanent_6thpay_DARate) / 100);
                        }
                        else
                        {
                            Dec_AmountDA = Math.Round((Amount * Permanent_DARate) / 100);
                        }
                    }                       
                    else if (dsNew.Tables[0].Rows[0]["Emp_TypeOfPost"].ToString() == "Fixed Employee")
                        Dec_AmountDA = Math.Round((Amount * Fixed_DARate) / 100);

                    if (dsNew.Tables[0].Rows[0]["Emp_SalaryLevel"].ToString() == "6") // EPF Calulation of 6th pay commisison
                    {
                        Dec_AmountEPF = Math.Round(((Amount + Dec_GradePay + Dec_AmountDA) * 12) / 100);
                    }
                    else
                    {
                        Dec_AmountEPF = Math.Round(((Amount + Dec_AmountDA) * 12) / 100);
                    }

                    //if (dsNew.Tables[0].Rows[0]["epfStatus"].ToString() == "Active")
                    //{
                    //    Dec_AmountEPF = Math.Round(((Amount + Dec_AmountDA) * 12) / 100);
                    //}
                    Dec_AmountBASIC = Amount;
                }
            }





            lblMsg.Text = "";
            Clear();

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            divDetail.Visible = false;
            string Monthcount = "";
            ViewState["Monthcount"] = "";
            ViewState["EmployeeID"] = ddlEmployee.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID", "Office_Id" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    Monthcount += ds.Tables[2].Rows[i]["Salary_MonthNo"].ToString() + ", ";
                    ViewState["Monthcount"] = Monthcount;
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                divDetail.Visible = true;

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");

                        TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                        TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                        TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                        TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                        TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                        TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                        TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                        TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                        TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                        TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                        TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                        TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                        if (Monthcount != "")
                        {
                            string[] monthNo = Monthcount.Split(',');
                            for (int i = 0; i < monthNo.Length - 1; i++)
                            {
                                if (monthNo[i].ToString() != "")
                                {
                                    int aa = int.Parse(monthNo[i]);
                                    if (aa == 1)
                                        Jan_Amount.Enabled = false;
                                    else if (aa == 2)
                                        Feb_Amount.Enabled = false;
                                    else if (aa == 3)
                                        Mar_Amount.Enabled = false;
                                    else if (aa == 4)
                                        Apr_Amount.Enabled = false;
                                    else if (aa == 5)
                                        May_Amount.Enabled = false;
                                    else if (aa == 6)
                                        Jun_Amount.Enabled = false;
                                    else if (aa == 7)
                                        Jul_Amount.Enabled = false;
                                    else if (aa == 8)
                                        Aug_Amount.Enabled = false;
                                    else if (aa == 9)
                                        Sep_Amount.Enabled = false;
                                    else if (aa == 10)
                                        Oct_Amount.Enabled = false;
                                    else if (aa == 11)
                                        Nov_Amount.Enabled = false;
                                    else if (aa == 12)
                                        Dec_Amount.Enabled = false;
                                }

                            }
                        }
                        // New code
                        if (lblEarnDeduction_ID.Text == "1")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountDA.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountDA.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountDA.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountDA.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountDA.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountDA.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountDA.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountDA.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountDA.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountDA.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountDA.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountDA.ToString();
                        }
                        else if (lblEarnDeduction_ID.Text == "2")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountHRA.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountHRA.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountHRA.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountHRA.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountHRA.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountHRA.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountHRA.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountHRA.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountHRA.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountHRA.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountHRA.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountHRA.ToString();
                        }

                        else if (lblEarnDeduction_ID.Text == "3")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountCONVEYANCE.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountCONVEYANCE.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountCONVEYANCE.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountCONVEYANCE.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountCONVEYANCE.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountCONVEYANCE.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountCONVEYANCE.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountCONVEYANCE.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountCONVEYANCE.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountCONVEYANCE.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountCONVEYANCE.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountCONVEYANCE.ToString();
                        }
                        else if (lblEarnDeduction_ID.Text == "5")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountWashing.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountWashing.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountWashing.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountWashing.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountWashing.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountWashing.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountWashing.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountWashing.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountWashing.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountWashing.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountWashing.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountWashing.ToString();
                        }
                        else if (lblEarnDeduction_ID.Text == "64")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountAttendanceBonus.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountAttendanceBonus.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountAttendanceBonus.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountAttendanceBonus.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountAttendanceBonus.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountAttendanceBonus.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountAttendanceBonus.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountAttendanceBonus.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountAttendanceBonus.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountAttendanceBonus.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountAttendanceBonus.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountAttendanceBonus.ToString();
                        }
                        else if (lblEarnDeduction_ID.Text == "65")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountMedicalBonus.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountMedicalBonus.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountMedicalBonus.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountMedicalBonus.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountMedicalBonus.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountMedicalBonus.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountMedicalBonus.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountMedicalBonus.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountMedicalBonus.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountMedicalBonus.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountMedicalBonus.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountMedicalBonus.ToString();
                        }
                        else if (lblEarnDeduction_ID.Text == "4")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_OrderlyAllowance.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_OrderlyAllowance.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_OrderlyAllowance.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_OrderlyAllowance.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_OrderlyAllowance.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_OrderlyAllowance.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_OrderlyAllowance.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_OrderlyAllowance.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_OrderlyAllowance.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_OrderlyAllowance.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_OrderlyAllowance.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_OrderlyAllowance.ToString();
                        }

                        else if (lblEarnDeduction_ID.Text == "66")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_GradePay.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_GradePay.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_GradePay.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_GradePay.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_GradePay.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_GradePay.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_GradePay.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_GradePay.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_GradePay.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_GradePay.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_GradePay.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_GradePay.ToString();
                        }
                    }
                }

                /************************************/
                txtBasicApr.Text = Apr_AmountBASIC.ToString();
                txtBasicApr.Enabled = false;
                txtBasicMay.Text = May_AmountBASIC.ToString();
                txtBasicMay.Enabled = false;
                txtBasicJun.Text = Jun_AmountBASIC.ToString();
                txtBasicJun.Enabled = false;
                txtBasicJul.Text = Jul_AmountBASIC.ToString();
                txtBasicJul.Enabled = false;
                txtBasicAug.Text = Aug_AmountBASIC.ToString();
                txtBasicAug.Enabled = false;
                txtBasicSep.Text = Sep_AmountBASIC.ToString();
                txtBasicSep.Enabled = false;
                txtBasicOct.Text = Oct_AmountBASIC.ToString();
                txtBasicOct.Enabled = false;
                txtBasicNov.Text = Nov_AmountBASIC.ToString();
                txtBasicNov.Enabled = false;
                txtBasicDec.Text = Dec_AmountBASIC.ToString();
                txtBasicDec.Enabled = false;
                txtBasicJan.Text = Jan_AmountBASIC.ToString();
                txtBasicJan.Enabled = false;
                txtBasicFeb.Text = Feb_AmountBASIC.ToString();
                txtBasicFeb.Enabled = false;
                txtBasicMar.Text = Mar_AmountBASIC.ToString();
                txtBasicMar.Enabled = false;
                //decimal Feb_AmountBASIC = 0;
                //decimal Mar_AmountBASIC = 0;
                //decimal Apr_AmountBASIC = 0;
                //decimal May_AmountBASIC = 0;
                //decimal Jun_AmountBASIC = 0;
                //decimal Jul_AmountBASIC = 0;
                //decimal Aug_AmountBASIC = 0;
                //decimal Sep_AmountBASIC = 0;
                //decimal Oct_AmountBASIC = 0;
                //decimal Nov_AmountBASIC = 0;
                //decimal Dec_AmountBASIC = 0;
                /*************************************/

            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();

                foreach (GridViewRow gr in GridView2.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                        TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                        TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                        TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                        TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                        TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                        TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                        TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                        TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                        TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                        TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                        TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                        TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                        string[] monthNo = Monthcount.Split(',');
                        for (int i = 0; i < monthNo.Length - 1; i++)
                        {

                            if (monthNo[i] != "")
                            {
                                int aa = int.Parse(monthNo[i]);
                                if (aa == 1)
                                    Jan_Amount.Enabled = false;
                                else if (aa == 2)
                                    Feb_Amount.Enabled = false;
                                else if (aa == 3)
                                    Mar_Amount.Enabled = false;
                                else if (aa == 4)
                                    Apr_Amount.Enabled = false;
                                else if (aa == 5)
                                    May_Amount.Enabled = false;
                                else if (aa == 6)
                                    Jun_Amount.Enabled = false;
                                else if (aa == 7)
                                    Jul_Amount.Enabled = false;
                                else if (aa == 8)
                                    Aug_Amount.Enabled = false;
                                else if (aa == 9)
                                    Sep_Amount.Enabled = false;
                                else if (aa == 10)
                                    Oct_Amount.Enabled = false;
                                else if (aa == 11)
                                    Nov_Amount.Enabled = false;
                                else if (aa == 12)
                                    Dec_Amount.Enabled = false;
                            }
                        }

                        // New code
                        if (lblEarnDeduction_ID.Text == "8")
                        {
                            if (Jan_Amount.Enabled != false)
                                Jan_Amount.Text = Jan_AmountEPF.ToString();

                            if (Feb_Amount.Enabled != false)
                                Feb_Amount.Text = Feb_AmountEPF.ToString();

                            if (Mar_Amount.Enabled != false)
                                Mar_Amount.Text = Mar_AmountEPF.ToString();

                            if (Apr_Amount.Enabled != false)
                                Apr_Amount.Text = Apr_AmountEPF.ToString();

                            if (May_Amount.Enabled != false)
                                May_Amount.Text = May_AmountEPF.ToString();

                            if (Jun_Amount.Enabled != false)
                                Jun_Amount.Text = Jun_AmountEPF.ToString();

                            if (Jul_Amount.Enabled != false)
                                Jul_Amount.Text = Jul_AmountEPF.ToString();

                            if (Aug_Amount.Enabled != false)
                                Aug_Amount.Text = Aug_AmountEPF.ToString();

                            if (Sep_Amount.Enabled != false)
                                Sep_Amount.Text = Sep_AmountEPF.ToString();

                            if (Oct_Amount.Enabled != false)
                                Oct_Amount.Text = Oct_AmountEPF.ToString();

                            if (Nov_Amount.Enabled != false)
                                Nov_Amount.Text = Nov_AmountEPF.ToString();

                            if (Dec_Amount.Enabled != false)
                                Dec_Amount.Text = Dec_AmountEPF.ToString();
                        }

                        //if (lblEarnDeduction_ID.Text == "12")
                        //{
                        //    if (Jan_Amount.Enabled != false)
                        //        Jan_Amount.Text = "250";

                        //    if (Feb_Amount.Enabled != false)
                        //        Feb_Amount.Text = "0";

                        //    if (Mar_Amount.Enabled != false)
                        //        Mar_Amount.Text = "0";

                        //    if (Apr_Amount.Enabled != false)
                        //        Apr_Amount.Text = "250";

                        //    if (May_Amount.Enabled != false)
                        //        May_Amount.Text = "250";

                        //    if (Jun_Amount.Enabled != false)
                        //        Jun_Amount.Text = "250";

                        //    if (Jul_Amount.Enabled != false)
                        //        Jul_Amount.Text = "250";

                        //    if (Aug_Amount.Enabled != false)
                        //        Aug_Amount.Text = "250";

                        //    if (Sep_Amount.Enabled != false)
                        //        Sep_Amount.Text = "250";

                        //    if (Oct_Amount.Enabled != false)
                        //        Oct_Amount.Text = "250";

                        //    if (Nov_Amount.Enabled != false)
                        //        Nov_Amount.Text = "250";

                        //    if (Dec_Amount.Enabled != false)
                        //        Dec_Amount.Text = "250";
                        //}
                    }

                }
                Label lblRowNumber = (Label)GridView2.FooterRow.FindControl("lblRowNumber");
                lblRowNumber.Text = (ds.Tables[1].Rows.Count + 1).ToString();

                TextBox FApr_Amount = (TextBox)GridView2.FooterRow.FindControl("txtApr_Amount");
                TextBox FMay_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMay_Amount");
                TextBox FJun_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJun_Amount");
                TextBox FJul_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJul_Amount");
                TextBox FAug_Amount = (TextBox)GridView2.FooterRow.FindControl("txtAug_Amount");
                TextBox FSep_Amount = (TextBox)GridView2.FooterRow.FindControl("txtSep_Amount");
                TextBox FOct_Amount = (TextBox)GridView2.FooterRow.FindControl("txtOct_Amount");
                TextBox FNov_Amount = (TextBox)GridView2.FooterRow.FindControl("txtNov_Amount");
                TextBox FDec_Amount = (TextBox)GridView2.FooterRow.FindControl("txtDec_Amount");
                TextBox FJan_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJan_Amount");
                TextBox FFeb_Amount = (TextBox)GridView2.FooterRow.FindControl("txtFeb_Amount");
                TextBox FMar_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMar_Amount");
                if (ds.Tables[3].Rows.Count > 0)
                {
                    FApr_Amount.Text = ds.Tables[3].Rows[0]["Apr_Amount"].ToString();
                    FMay_Amount.Text = ds.Tables[3].Rows[0]["May_Amount"].ToString();
                    FJun_Amount.Text = ds.Tables[3].Rows[0]["Jun_Amount"].ToString();
                    FJul_Amount.Text = ds.Tables[3].Rows[0]["Jul_Amount"].ToString();
                    FAug_Amount.Text = ds.Tables[3].Rows[0]["Aug_Amount"].ToString();
                    FSep_Amount.Text = ds.Tables[3].Rows[0]["Sep_Amount"].ToString();
                    FOct_Amount.Text = ds.Tables[3].Rows[0]["Oct_Amount"].ToString();
                    FNov_Amount.Text = ds.Tables[3].Rows[0]["Nov_Amount"].ToString();
                    FDec_Amount.Text = ds.Tables[3].Rows[0]["Dec_Amount"].ToString();
                    FJan_Amount.Text = ds.Tables[3].Rows[0]["Jan_Amount"].ToString();
                    FFeb_Amount.Text = ds.Tables[3].Rows[0]["Feb_Amount"].ToString();
                    FMar_Amount.Text = ds.Tables[3].Rows[0]["Mar_Amount"].ToString();
                }
                else
                {
                    FApr_Amount.Text = "0";
                    FMay_Amount.Text = "0";
                    FJun_Amount.Text = "0";
                    FJul_Amount.Text = "0";
                    FAug_Amount.Text = "0";
                    FSep_Amount.Text = "0";
                    FOct_Amount.Text = "0";
                    FNov_Amount.Text = "0";
                    FDec_Amount.Text = "0";
                    FJan_Amount.Text = "0";
                    FFeb_Amount.Text = "0";
                    FMar_Amount.Text = "0";
                }

                if (ds.Tables[4].Rows.Count != 0)
                {
                    lblBank_AccountNo.Text = ds.Tables[4].Rows[0]["Bank_AccountNo"].ToString();
                    lblSalary_NetSalary.Text = ds.Tables[4].Rows[0]["Emp_BasicSalery"].ToString();
                    lblBank_Name.Text = ds.Tables[4].Rows[0]["Bank_Name"].ToString();
                    lblIFSCCode.Text = ds.Tables[4].Rows[0]["Bank_IfscCode"].ToString();
                    lblGroupInsurance_No.Text = ds.Tables[4].Rows[0]["GroupInsurance_No"].ToString();
                    lblEPF_No.Text = ds.Tables[4].Rows[0]["EPF_No"].ToString();
                    if (ds.Tables[4].Rows[0]["epfStatus"].ToString() == "Not Active")
                    {
                        //lblepfstatus.Text = "<span style='color:tomato; font-weight:bold'>As per the date of birth '" + ds.Tables[4].Rows[0]["Emp_Dob"].ToString() + "', employee's  is more than 58 years. Hence selected employee is not eligible for epf deduction as per corporation rule.</span>";

                        //lblepfstatus.Text = "<span style='color:tomato; font-weight:bold'>As per the date of birth '" + ds.Tables[4].Rows[0]["Emp_Dob"].ToString() + "', employee's  is more than 58 years.</span>";
                    }
                    else
                    {
                        lblepfstatus.Text = "";
                    }


                }

            }
            FillGridTotal();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridTotal()
    {
        try
        {
            // Earning Detail Total
            #region Earning Total

            decimal TotApr_Amount = 0;
            decimal TotMay_Amount = 0;
            decimal TotJun_Amount = 0;
            decimal TotJul_Amount = 0;
            decimal TotAug_Amount = 0;
            decimal TotSep_Amount = 0;
            decimal TotOct_Amount = 0;
            decimal TotNov_Amount = 0;
            decimal TotDec_Amount = 0;
            decimal TotJan_Amount = 0;
            decimal TotFeb_Amount = 0;
            decimal TotMar_Amount = 0;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");

                if (Apr_Amount.Text == "" || !objdb.isDecimal(Apr_Amount.Text))
                {
                    Apr_Amount.Text = "0.00";
                }

                if (May_Amount.Text == "" || !objdb.isDecimal(May_Amount.Text))
                {
                    May_Amount.Text = "0.00";
                }
                if (Jun_Amount.Text == "" || !objdb.isDecimal(Jun_Amount.Text))
                {
                    Jun_Amount.Text = "0.00";
                }
                if (Jul_Amount.Text == "" || !objdb.isDecimal(Jul_Amount.Text))
                {
                    Jul_Amount.Text = "0.00";
                }
                if (Aug_Amount.Text == "" || !objdb.isDecimal(Aug_Amount.Text))
                {
                    Aug_Amount.Text = "0.00";
                }
                if (Sep_Amount.Text == "" || !objdb.isDecimal(Sep_Amount.Text))
                {
                    Sep_Amount.Text = "0.00";
                }
                if (Oct_Amount.Text == "" || !objdb.isDecimal(Oct_Amount.Text))
                {
                    Oct_Amount.Text = "0.00";
                }
                if (Nov_Amount.Text == "" || !objdb.isDecimal(Nov_Amount.Text))
                {
                    Nov_Amount.Text = "0.00";
                }
                if (Dec_Amount.Text == "" || !objdb.isDecimal(Dec_Amount.Text))
                {
                    Dec_Amount.Text = "0.00";
                }
                if (Jan_Amount.Text == "" || !objdb.isDecimal(Jan_Amount.Text))
                {
                    Jan_Amount.Text = "0.00";
                }
                if (Feb_Amount.Text == "" || !objdb.isDecimal(Feb_Amount.Text))
                {
                    Feb_Amount.Text = "0.00";
                }
                if (Mar_Amount.Text == "" || !objdb.isDecimal(Mar_Amount.Text))
                {
                    Mar_Amount.Text = "0.00";
                }

                TotApr_Amount = TotApr_Amount + decimal.Parse(Apr_Amount.Text);
                TotMay_Amount = TotMay_Amount + decimal.Parse(May_Amount.Text);
                TotJun_Amount = TotJun_Amount + decimal.Parse(Jun_Amount.Text);
                TotJul_Amount = TotJul_Amount + decimal.Parse(Jul_Amount.Text);
                TotAug_Amount = TotAug_Amount + decimal.Parse(Aug_Amount.Text);
                TotSep_Amount = TotSep_Amount + decimal.Parse(Sep_Amount.Text);
                TotOct_Amount = TotOct_Amount + decimal.Parse(Oct_Amount.Text);
                TotNov_Amount = TotNov_Amount + decimal.Parse(Nov_Amount.Text);
                TotDec_Amount = TotDec_Amount + decimal.Parse(Dec_Amount.Text);
                TotJan_Amount = TotJan_Amount + decimal.Parse(Jan_Amount.Text);
                TotFeb_Amount = TotFeb_Amount + decimal.Parse(Feb_Amount.Text);
                TotMar_Amount = TotMar_Amount + decimal.Parse(Mar_Amount.Text);
            }
            /****************************/
            TotApr_Amount = TotApr_Amount + decimal.Parse(txtBasicApr.Text);
            TotMay_Amount = TotMay_Amount + decimal.Parse(txtBasicMay.Text);
            TotJun_Amount = TotJun_Amount + decimal.Parse(txtBasicJun.Text);
            TotJul_Amount = TotJul_Amount + decimal.Parse(txtBasicJul.Text);
            TotAug_Amount = TotAug_Amount + decimal.Parse(txtBasicAug.Text);
            TotSep_Amount = TotSep_Amount + decimal.Parse(txtBasicSep.Text);
            TotOct_Amount = TotOct_Amount + decimal.Parse(txtBasicOct.Text);
            TotNov_Amount = TotNov_Amount + decimal.Parse(txtBasicNov.Text);
            TotDec_Amount = TotDec_Amount + decimal.Parse(txtBasicDec.Text);
            TotJan_Amount = TotJan_Amount + decimal.Parse(txtBasicJan.Text);
            TotFeb_Amount = TotFeb_Amount + decimal.Parse(txtBasicFeb.Text);
            TotMar_Amount = TotMar_Amount + decimal.Parse(txtBasicMar.Text);
            /****************************/
            txtEarTotApr.Text = TotApr_Amount.ToString();
            txtEarTotMay.Text = TotMay_Amount.ToString();
            txtEarTotJun.Text = TotJun_Amount.ToString();
            txtEarTotJul.Text = TotJul_Amount.ToString();
            txtEarTotAug.Text = TotAug_Amount.ToString();
            txtEarTotSep.Text = TotSep_Amount.ToString();
            txtEarTotOct.Text = TotOct_Amount.ToString();
            txtEarTotNov.Text = TotNov_Amount.ToString();
            txtEarTotDec.Text = TotDec_Amount.ToString();
            txtEarTotJan.Text = TotJan_Amount.ToString();
            txtEarTotFeb.Text = TotFeb_Amount.ToString();
            txtEarTotMar.Text = TotMar_Amount.ToString();
            #endregion
            // Deduction Detail Total
            #region Deduction Total
            TotApr_Amount = 0;
            TotMay_Amount = 0;
            TotJun_Amount = 0;
            TotJul_Amount = 0;
            TotAug_Amount = 0;
            TotSep_Amount = 0;
            TotOct_Amount = 0;
            TotNov_Amount = 0;
            TotDec_Amount = 0;
            TotJan_Amount = 0;
            TotFeb_Amount = 0;
            TotMar_Amount = 0;

            foreach (GridViewRow gr in GridView2.Rows)
            {
                TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");

                if (Apr_Amount.Text == "" || !objdb.isDecimal(Apr_Amount.Text))
                {
                    Apr_Amount.Text = "0.00";
                }

                if (May_Amount.Text == "" || !objdb.isDecimal(May_Amount.Text))
                {
                    May_Amount.Text = "0.00";
                }
                if (Jun_Amount.Text == "" || !objdb.isDecimal(Jun_Amount.Text))
                {
                    Jun_Amount.Text = "0.00";
                }
                if (Jul_Amount.Text == "" || !objdb.isDecimal(Jul_Amount.Text))
                {
                    Jul_Amount.Text = "0.00";
                }
                if (Aug_Amount.Text == "" || !objdb.isDecimal(Aug_Amount.Text))
                {
                    Aug_Amount.Text = "0.00";
                }
                if (Sep_Amount.Text == "" || !objdb.isDecimal(Sep_Amount.Text))
                {
                    Sep_Amount.Text = "0.00";
                }
                if (Oct_Amount.Text == "" || !objdb.isDecimal(Oct_Amount.Text))
                {
                    Oct_Amount.Text = "0.00";
                }
                if (Nov_Amount.Text == "" || !objdb.isDecimal(Nov_Amount.Text))
                {
                    Nov_Amount.Text = "0.00";
                }
                if (Dec_Amount.Text == "" || !objdb.isDecimal(Dec_Amount.Text))
                {
                    Dec_Amount.Text = "0.00";
                }
                if (Jan_Amount.Text == "" || !objdb.isDecimal(Jan_Amount.Text))
                {
                    Jan_Amount.Text = "0.00";
                }
                if (Feb_Amount.Text == "" || !objdb.isDecimal(Feb_Amount.Text))
                {
                    Feb_Amount.Text = "0.00";
                }
                if (Mar_Amount.Text == "" || !objdb.isDecimal(Mar_Amount.Text))
                {
                    Mar_Amount.Text = "0.00";
                }

                TotApr_Amount = TotApr_Amount + decimal.Parse(Apr_Amount.Text);
                TotMay_Amount = TotMay_Amount + decimal.Parse(May_Amount.Text);
                TotJun_Amount = TotJun_Amount + decimal.Parse(Jun_Amount.Text);
                TotJul_Amount = TotJul_Amount + decimal.Parse(Jul_Amount.Text);
                TotAug_Amount = TotAug_Amount + decimal.Parse(Aug_Amount.Text);
                TotSep_Amount = TotSep_Amount + decimal.Parse(Sep_Amount.Text);
                TotOct_Amount = TotOct_Amount + decimal.Parse(Oct_Amount.Text);
                TotNov_Amount = TotNov_Amount + decimal.Parse(Nov_Amount.Text);
                TotDec_Amount = TotDec_Amount + decimal.Parse(Dec_Amount.Text);
                TotJan_Amount = TotJan_Amount + decimal.Parse(Jan_Amount.Text);
                TotFeb_Amount = TotFeb_Amount + decimal.Parse(Feb_Amount.Text);
                TotMar_Amount = TotMar_Amount + decimal.Parse(Mar_Amount.Text);
            }




            TextBox FApr_Amount = (TextBox)GridView2.FooterRow.FindControl("txtApr_Amount");
            TextBox FMay_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMay_Amount");
            TextBox FJun_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJun_Amount");
            TextBox FJul_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJul_Amount");
            TextBox FAug_Amount = (TextBox)GridView2.FooterRow.FindControl("txtAug_Amount");
            TextBox FSep_Amount = (TextBox)GridView2.FooterRow.FindControl("txtSep_Amount");
            TextBox FOct_Amount = (TextBox)GridView2.FooterRow.FindControl("txtOct_Amount");
            TextBox FNov_Amount = (TextBox)GridView2.FooterRow.FindControl("txtNov_Amount");
            TextBox FDec_Amount = (TextBox)GridView2.FooterRow.FindControl("txtDec_Amount");
            TextBox FJan_Amount = (TextBox)GridView2.FooterRow.FindControl("txtJan_Amount");
            TextBox FFeb_Amount = (TextBox)GridView2.FooterRow.FindControl("txtFeb_Amount");
            TextBox FMar_Amount = (TextBox)GridView2.FooterRow.FindControl("txtMar_Amount");

            if (FApr_Amount.Text == "" || !objdb.isDecimal(FApr_Amount.Text))
            {
                FApr_Amount.Text = "0.00";
            }

            if (FMay_Amount.Text == "" || !objdb.isDecimal(FMay_Amount.Text))
            {
                FMay_Amount.Text = "0.00";
            }
            if (FJun_Amount.Text == "" || !objdb.isDecimal(FJun_Amount.Text))
            {
                FJun_Amount.Text = "0.00";
            }
            if (FJul_Amount.Text == "" || !objdb.isDecimal(FJul_Amount.Text))
            {
                FJul_Amount.Text = "0.00";
            }
            if (FAug_Amount.Text == "" || !objdb.isDecimal(FAug_Amount.Text))
            {
                FAug_Amount.Text = "0.00";
            }
            if (FSep_Amount.Text == "" || !objdb.isDecimal(FSep_Amount.Text))
            {
                FSep_Amount.Text = "0.00";
            }
            if (FOct_Amount.Text == "" || !objdb.isDecimal(FOct_Amount.Text))
            {
                FOct_Amount.Text = "0.00";
            }
            if (FNov_Amount.Text == "" || !objdb.isDecimal(FNov_Amount.Text))
            {
                FNov_Amount.Text = "0.00";
            }
            if (FDec_Amount.Text == "" || !objdb.isDecimal(FDec_Amount.Text))
            {
                FDec_Amount.Text = "0.00";
            }
            if (FJan_Amount.Text == "" || !objdb.isDecimal(FJan_Amount.Text))
            {
                FJan_Amount.Text = "0.00";
            }
            if (FFeb_Amount.Text == "" || !objdb.isDecimal(FFeb_Amount.Text))
            {
                FFeb_Amount.Text = "0.00";
            }
            if (FMar_Amount.Text == "" || !objdb.isDecimal(FMar_Amount.Text))
            {
                FMar_Amount.Text = "0.00";
            }

            TotApr_Amount = TotApr_Amount + decimal.Parse(FApr_Amount.Text);
            TotMay_Amount = TotMay_Amount + decimal.Parse(FMay_Amount.Text);
            TotJun_Amount = TotJun_Amount + decimal.Parse(FJun_Amount.Text);
            TotJul_Amount = TotJul_Amount + decimal.Parse(FJul_Amount.Text);
            TotAug_Amount = TotAug_Amount + decimal.Parse(FAug_Amount.Text);
            TotSep_Amount = TotSep_Amount + decimal.Parse(FSep_Amount.Text);
            TotOct_Amount = TotOct_Amount + decimal.Parse(FOct_Amount.Text);
            TotNov_Amount = TotNov_Amount + decimal.Parse(FNov_Amount.Text);
            TotDec_Amount = TotDec_Amount + decimal.Parse(FDec_Amount.Text);
            TotJan_Amount = TotJan_Amount + decimal.Parse(FJan_Amount.Text);
            TotFeb_Amount = TotFeb_Amount + decimal.Parse(FFeb_Amount.Text);
            TotMar_Amount = TotMar_Amount + decimal.Parse(FMar_Amount.Text);





            txtDedTotApr.Text = TotApr_Amount.ToString();
            txtDedTotMay.Text = TotMay_Amount.ToString();
            txtDedTotJun.Text = TotJun_Amount.ToString();
            txtDedTotJul.Text = TotJul_Amount.ToString();
            txtDedTotAug.Text = TotAug_Amount.ToString();
            txtDedTotSep.Text = TotSep_Amount.ToString();
            txtDedTotOct.Text = TotOct_Amount.ToString();
            txtDedTotNov.Text = TotNov_Amount.ToString();
            txtDedTotDec.Text = TotDec_Amount.ToString();
            txtDedTotJan.Text = TotJan_Amount.ToString();
            txtDedTotFeb.Text = TotFeb_Amount.ToString();
            txtDedTotMar.Text = TotMar_Amount.ToString();
            #endregion
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Clear()
    {
        lblBank_AccountNo.Text = "";
        lblSalary_NetSalary.Text = "";
        lblBank_Name.Text = "";
        lblIFSCCode.Text = "";
        lblGroupInsurance_No.Text = "";
        lblEPF_No.Text = "";

        // Earning Detail Total
        txtEarTotApr.Text = "";
        txtEarTotMay.Text = "";
        txtEarTotJun.Text = "";
        txtEarTotJul.Text = "";
        txtEarTotAug.Text = "";
        txtEarTotSep.Text = "";
        txtEarTotOct.Text = "";
        txtEarTotNov.Text = "";
        txtEarTotDec.Text = "";
        txtEarTotJan.Text = "";
        txtEarTotFeb.Text = "";
        txtEarTotMar.Text = "";
        // Deduction Detail Total
        txtDedTotApr.Text = "";
        txtDedTotMay.Text = "";
        txtDedTotJun.Text = "";
        txtDedTotJul.Text = "";
        txtDedTotAug.Text = "";
        txtDedTotSep.Text = "";
        txtDedTotOct.Text = "";
        txtDedTotNov.Text = "";
        txtDedTotDec.Text = "";
        txtDedTotJan.Text = "";
        txtDedTotFeb.Text = "";
        txtDedTotMar.Text = "";


    }
    protected void MonthWiseReport(string MonthNo)
    {
        try
        {
            GridView3.DataSource = null;
            GridView3.DataBind();
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpPayrollPolicyMonthWsDetail", new string[] { "flag", "Emp_ID", "Year", "MonthNo" }, new string[] { "0", ddlEmployee.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), MonthNo }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MonthDetail();", true);
                }
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
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnApr_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("4");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMay_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("5");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJun_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("6");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJul_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("7");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAug_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("8");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSep_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("9");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnOct_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("10");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnNov_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("11");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnDec_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("12");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJan_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("1");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnFeb_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("2");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMar_Amount_Click(object sender, EventArgs e)
    {
        try
        {
            MonthWiseReport("3");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            divDetail.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee. \\n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (msg.Trim() == "")
            {
                string Year = ddlYear.SelectedValue.ToString();

                string Employee = ddlEmployee.SelectedValue.ToString();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                    TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                    TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                    TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                    TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                    TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                    TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                    TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                    TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                    TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                    TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                    TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                    Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                    Label lblCalculation_Type = (Label)gr.FindControl("lblCalculation_Type");
                    if (Apr_Amount.Text == "")
                    {
                        Apr_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Apr_Amount.Text))
                        {
                            Apr_Amount.Text = "0.00";
                        }
                    }

                    if (May_Amount.Text == "")
                    {
                        May_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(May_Amount.Text))
                        {
                            May_Amount.Text = "0.00";
                        }
                    }
                    if (Jun_Amount.Text == "")
                    {
                        Jun_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jun_Amount.Text))
                        {
                            Jun_Amount.Text = "0.00";
                        }
                    }
                    if (Jul_Amount.Text == "")
                    {
                        Jul_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jul_Amount.Text))
                        {
                            Jul_Amount.Text = "0.00";
                        }
                    }
                    if (Aug_Amount.Text == "")
                    {
                        Aug_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Aug_Amount.Text))
                        {
                            Aug_Amount.Text = "0.00";
                        }
                    }
                    if (Sep_Amount.Text == "")
                    {
                        Sep_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Sep_Amount.Text))
                        {
                            Sep_Amount.Text = "0.00";
                        }
                    }
                    if (Oct_Amount.Text == "")
                    {
                        Oct_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Oct_Amount.Text))
                        {
                            Oct_Amount.Text = "0.00";
                        }
                    }
                    if (Nov_Amount.Text == "")
                    {
                        Nov_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Nov_Amount.Text))
                        {
                            Nov_Amount.Text = "0.00";
                        }
                    }
                    if (Dec_Amount.Text == "")
                    {
                        Dec_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Dec_Amount.Text))
                        {
                            Dec_Amount.Text = "0.00";
                        }
                    }
                    if (Jan_Amount.Text == "")
                    {
                        Jan_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jan_Amount.Text))
                        {
                            Jan_Amount.Text = "0.00";
                        }
                    }
                    if (Feb_Amount.Text == "")
                    {
                        Feb_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Feb_Amount.Text))
                        {
                            Feb_Amount.Text = "0.00";
                        }
                    }
                    if (Mar_Amount.Text == "")
                    {
                        Mar_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Mar_Amount.Text))
                        {
                            Mar_Amount.Text = "0.00";
                        }
                    }


                    //objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                    //   new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                    //         new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    string Year_2 = (Int32.Parse(Year) + 1).ToString();
                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                       new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "UpdatedBy" },
                       new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                      new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                      new string[] { "3", Employee, Year_2, lblEarnDeduction_ID.Text, "Earning", lblCalculation_Type.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                }
                foreach (GridViewRow gr in GridView2.Rows)
                {
                    TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                    TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                    TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                    TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                    TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                    TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                    TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                    TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                    TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                    TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                    TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                    TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");
                    Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                    Label lblCalculation_Type = (Label)gr.FindControl("lblCalculation_Type");
                    if (Apr_Amount.Text == "")
                    {
                        Apr_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Apr_Amount.Text))
                        {
                            Apr_Amount.Text = "0.00";
                        }
                    }

                    if (May_Amount.Text == "")
                    {
                        May_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(May_Amount.Text))
                        {
                            May_Amount.Text = "0.00";
                        }
                    }
                    if (Jun_Amount.Text == "")
                    {
                        Jun_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jun_Amount.Text))
                        {
                            Jun_Amount.Text = "0.00";
                        }
                    }
                    if (Jul_Amount.Text == "")
                    {
                        Jul_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jul_Amount.Text))
                        {
                            Jul_Amount.Text = "0.00";
                        }
                    }
                    if (Aug_Amount.Text == "")
                    {
                        Aug_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Aug_Amount.Text))
                        {
                            Aug_Amount.Text = "0.00";
                        }
                    }
                    if (Sep_Amount.Text == "")
                    {
                        Sep_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Sep_Amount.Text))
                        {
                            Sep_Amount.Text = "0.00";
                        }
                    }
                    if (Oct_Amount.Text == "")
                    {
                        Oct_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Oct_Amount.Text))
                        {
                            Oct_Amount.Text = "0.00";
                        }
                    }
                    if (Nov_Amount.Text == "")
                    {
                        Nov_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Nov_Amount.Text))
                        {
                            Nov_Amount.Text = "0.00";
                        }
                    }
                    if (Dec_Amount.Text == "")
                    {
                        Dec_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Dec_Amount.Text))
                        {
                            Dec_Amount.Text = "0.00";
                        }
                    }
                    if (Jan_Amount.Text == "")
                    {
                        Jan_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Jan_Amount.Text))
                        {
                            Jan_Amount.Text = "0.00";
                        }
                    }
                    if (Feb_Amount.Text == "")
                    {
                        Feb_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Feb_Amount.Text))
                        {
                            Feb_Amount.Text = "0.00";
                        }
                    }
                    if (Mar_Amount.Text == "")
                    {
                        Mar_Amount.Text = "0.00";
                    }
                    else
                    {
                        if (!objdb.isDecimal(Mar_Amount.Text))
                        {
                            Mar_Amount.Text = "0.00";
                        }
                    }


                    //objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                    //   new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                    //         new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    string Year_2 = (Int32.Parse(Year) + 1).ToString();
                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                       new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Apr_Amount", "May_Amount", "Jun_Amount", "Jul_Amount", "Aug_Amount", "Sep_Amount", "Oct_Amount", "Nov_Amount", "Dec_Amount", "UpdatedBy" },
                             new string[] { "0", Employee, Year, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Apr_Amount.Text, May_Amount.Text, Jun_Amount.Text, Jul_Amount.Text, Aug_Amount.Text, Sep_Amount.Text, Oct_Amount.Text, Nov_Amount.Text, Dec_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY",
                      new string[] { "flag", "Emp_ID", "Year", "EarnDeduction_ID", "EarnDeduction_Type", "Calculation_Type", "Jan_Amount", "Feb_Amount", "Mar_Amount", "UpdatedBy" },
                            new string[] { "3", Employee, Year_2, lblEarnDeduction_ID.Text, "Deduction", lblCalculation_Type.Text, Jan_Amount.Text, Feb_Amount.Text, Mar_Amount.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                SavePolicyDetail();

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

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
    protected void SavePolicyDetail()
    {
        try
        {

            string Year = ddlYear.SelectedValue.ToString();
            string Year_2 = (Int32.Parse(Year) + 1).ToString();
            string Employee = ddlEmployee.SelectedValue.ToString();

            string Monthcount = ViewState["Monthcount"].ToString();
            string[] monthNo = Monthcount.Split(',');
            for (int j = 1; j <= 12; j++)
            {
                string k = "0";
                for (int i = 0; i < monthNo.Length - 1; i++)
                {
                    int aa = int.Parse(monthNo[i]);
                    if (aa == j)
                    {
                        k = "1";
                    }
                }
                if (k == "0")
                {
                    //objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY_FY",
                    //  new string[] { "flag", "Emp_ID", "Office_ID", "Salary_Year", "PolicyDed_UpdatedBy" },
                    //  new string[] { j.ToString(), Employee, ViewState["Office_ID"].ToString(), Year, ViewState["Emp_ID"].ToString() }, "dataset");
                    string Year_FY = "";
                    if (j < 4)
                        Year_FY = Year_2.ToString();
                    else
                        Year_FY = Year.ToString();

                    objdb.ByProcedure("SpPayrollPolicyDeduction",
                      new string[] { "flag", "Emp_ID", "Office_ID", "Salary_Year", "PolicyDed_UpdatedBy" },
                      new string[] { j.ToString(), Employee, ViewState["Office_ID"].ToString(), Year_FY, ViewState["Emp_ID"].ToString() }, "dataset");
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    /*******New PP**********/
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        divDetail.Visible = false;
        FillDropdown();
        FillGrid();

    }
	
	protected void btnReset_Click(object sender, EventArgs e)
    {
        if(GridView1.Rows.Count>0 && GridView2.Rows.Count>0)
        {
            foreach(GridViewRow gr in GridView1.Rows)
            {
                Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");

                if (Apr_Amount.Enabled == true && (Apr_Amount.Text != "0" || Apr_Amount.Text != "0.00") && lblEarnDeduction_ID.Text!="1")
                {
                    Apr_Amount.Text = "0.00";
                }
                else if (May_Amount.Enabled == true && (May_Amount.Text != "0" || May_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    May_Amount.Text = "0.00";
                }
                else if (Jun_Amount.Enabled == true && (Jun_Amount.Text != "0" || Jun_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Jun_Amount.Text = "0.00";
                }
                else if (Jul_Amount.Enabled == true && (Jul_Amount.Text != "0" || Jul_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Jul_Amount.Text = "0.00";
                }
                else if (Aug_Amount.Enabled == true && (Aug_Amount.Text != "0" || Aug_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Aug_Amount.Text = "0.00";
                }
                else if (Sep_Amount.Enabled == true && (Sep_Amount.Text != "0" || Sep_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Sep_Amount.Text = "0.00";
                }
                else if (Oct_Amount.Enabled == true && (Oct_Amount.Text != "0" || Oct_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Oct_Amount.Text = "0.00";
                }
                else if (Nov_Amount.Enabled == true && (Nov_Amount.Text != "0" || Nov_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Nov_Amount.Text = "0.00";
                }
                else if (Dec_Amount.Enabled == true && (Dec_Amount.Text != "0" || Dec_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Dec_Amount.Text = "0.00";
                }
                else if (Jan_Amount.Enabled == true && (Jan_Amount.Text != "0" || Jan_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Jan_Amount.Text = "0.00";
                }
                else if (Feb_Amount.Enabled == true && (Feb_Amount.Text != "0" || Feb_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Feb_Amount.Text = "0.00";
                }
                else if (Mar_Amount.Enabled == true && (Mar_Amount.Text != "0" || Mar_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "1")
                {
                    Mar_Amount.Text = "0.00";
                }
            }

            foreach (GridViewRow gr in GridView2.Rows)
            {
                Label lblEarnDeduction_ID = (Label)gr.FindControl("lblEarnDeduction_ID");
                TextBox Apr_Amount = (TextBox)gr.FindControl("txtApr_Amount");
                TextBox May_Amount = (TextBox)gr.FindControl("txtMay_Amount");
                TextBox Jun_Amount = (TextBox)gr.FindControl("txtJun_Amount");
                TextBox Jul_Amount = (TextBox)gr.FindControl("txtJul_Amount");
                TextBox Aug_Amount = (TextBox)gr.FindControl("txtAug_Amount");
                TextBox Sep_Amount = (TextBox)gr.FindControl("txtSep_Amount");
                TextBox Oct_Amount = (TextBox)gr.FindControl("txtOct_Amount");
                TextBox Nov_Amount = (TextBox)gr.FindControl("txtNov_Amount");
                TextBox Dec_Amount = (TextBox)gr.FindControl("txtDec_Amount");
                TextBox Jan_Amount = (TextBox)gr.FindControl("txtJan_Amount");
                TextBox Feb_Amount = (TextBox)gr.FindControl("txtFeb_Amount");
                TextBox Mar_Amount = (TextBox)gr.FindControl("txtMar_Amount");

                if (Apr_Amount.Enabled == true && (Apr_Amount.Text != "0" || Apr_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Apr_Amount.Text = "0.00";
                }
                else if (May_Amount.Enabled == true && (May_Amount.Text != "0" || May_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    May_Amount.Text = "0.00";
                }
                else if (Jun_Amount.Enabled == true && (Jun_Amount.Text != "0" || Jun_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Jun_Amount.Text = "0.00";
                }
                else if (Jul_Amount.Enabled == true && (Jul_Amount.Text != "0" || Jul_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Jul_Amount.Text = "0.00";
                }
                else if (Aug_Amount.Enabled == true && (Aug_Amount.Text != "0" || Aug_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Aug_Amount.Text = "0.00";
                }
                else if (Sep_Amount.Enabled == true && (Sep_Amount.Text != "0" || Sep_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Sep_Amount.Text = "0.00";
                }
                else if (Oct_Amount.Enabled == true && (Oct_Amount.Text != "0" || Oct_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Oct_Amount.Text = "0.00";
                }
                else if (Nov_Amount.Enabled == true && (Nov_Amount.Text != "0" || Nov_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Nov_Amount.Text = "0.00";
                }
                else if (Dec_Amount.Enabled == true && (Dec_Amount.Text != "0" || Dec_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Dec_Amount.Text = "0.00";
                }
                else if (Jan_Amount.Enabled == true && (Jan_Amount.Text != "0" || Jan_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Jan_Amount.Text = "0.00";
                }
                else if (Feb_Amount.Enabled == true && (Feb_Amount.Text != "0" || Feb_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Feb_Amount.Text = "0.00";
                }
                else if (Mar_Amount.Enabled == true && (Mar_Amount.Text != "0" || Mar_Amount.Text != "0.00") && lblEarnDeduction_ID.Text != "8")
                {
                    Mar_Amount.Text = "0.00";
                }
            }
        }
    }
}