using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpSetAttendance : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    int ColCount = 11;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                DivDetail.Visible = false;
                btnSave.Visible = false;
                CountSunday();
                FillDropdown();

            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void CountSunday()
    {
        try
        {

            //First We find out last date of mont
            DateTime today = DateTime.Today;
            DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            //get only last day of month
            int day = endOfMonth.Day;

            DateTime now = DateTime.Now;
            int count;
            count = 0;
            for (int i = 0; i < day; ++i)
            {
                DateTime d = new DateTime(now.Year, now.Month, i + 1);
                //Compare date with sunday
                if (d.DayOfWeek == DayOfWeek.Sunday)
                {
                    count = count + 1;
                }
            }
            ViewState["SundayCoun"] = count.ToString();

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
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
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
            // ddlOfficeName.Attributes.Add("readonly", "readonly");

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

            Int32 iDays = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue));
            //if (ddlMonth.SelectedValue.ToString()=="01")
            //    iDays = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue)-1, 12);
            //else
            //    iDays = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue)-1);


            ViewState["days"] = iDays.ToString();
            ds = objdb.ByProcedure("SpPayrollEmpAttendance", new string[] { "flag", "Year", "MonthNo", "Office_ID", "Emp_TypeOfPost" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                /***new data table****/
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
                /****new***/
                DivDetail.Visible = true;
                btnSave.Visible = true;


                GridView1.Columns[31].Visible = true;
                GridView1.Columns[32].Visible = true;
                GridView1.Columns[33].Visible = true;
                if (iDays == 28)
                {
                    GridView1.Columns[31].Visible = false;
                    GridView1.Columns[32].Visible = false;
                    GridView1.Columns[33].Visible = false;
                }
                else if (iDays == 29)
                {
                    GridView1.Columns[32].Visible = false;
                    GridView1.Columns[33].Visible = false;
                }
                else if (iDays == 30)
                {
                    GridView1.Columns[33].Visible = false;
                }
                // GridView1.Columns[33].HeaderText = "1/10";

                /*************/
                lblSelectedItems.Text = "Attendance of office <b>" + ddlOfficeName.SelectedItem.ToString() + "</b> for month  " + ddlMonth.SelectedItem.ToString() + ", " + ddlYear.SelectedItem.ToString() + "  ( " + ddlEmp_TypeOfPost.SelectedItem.ToString() + " )";
                lblAttDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                /*************/

            }
            else
            {
                lblMsg.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
                DivDetail.Visible = false;
                btnSave.Visible = false;
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
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillGrid();

            }

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
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year.\\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month.\\n";
            }
            if (msg == "")
            {
                //StringBuilder sbSet_Attendance = new StringBuilder();
                string Year = ddlYear.SelectedValue.ToString();
                string MonthNo = ddlMonth.SelectedValue.ToString();
                string Month = ddlMonth.SelectedItem.ToString();
                int TotalDays = int.Parse(ViewState["days"].ToString());
                string LoginUserID = ViewState["Emp_ID"].ToString();
                string Office_ID = ViewState["Office_ID"].ToString();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    Label lblGenerateStatus = (Label)gr.FindControl("lblGenerateStatus");


                    if (chkSelect.Checked == true && lblGenerateStatus.Text == "Not Generated")
                    {
                        Label Emp_ID = (Label)gr.FindControl("lblEmp_ID");
                        TextBox PayableDays = (TextBox)gr.FindControl("txtPayableDays");
                        string day11 = gr.FindControl("ddlDay1").ToString();
                        DropDownList Day1 = (DropDownList)gr.FindControl("ddlDay1");
                        DropDownList Day2 = (DropDownList)gr.FindControl("ddlDay2");
                        DropDownList Day3 = (DropDownList)gr.FindControl("ddlDay3");
                        DropDownList Day4 = (DropDownList)gr.FindControl("ddlDay4");
                        DropDownList Day5 = (DropDownList)gr.FindControl("ddlDay5");
                        DropDownList Day6 = (DropDownList)gr.FindControl("ddlDay6");
                        DropDownList Day7 = (DropDownList)gr.FindControl("ddlDay7");
                        DropDownList Day8 = (DropDownList)gr.FindControl("ddlDay8");
                        DropDownList Day9 = (DropDownList)gr.FindControl("ddlDay9");
                        DropDownList Day10 = (DropDownList)gr.FindControl("ddlDay10");
                        DropDownList Day11 = (DropDownList)gr.FindControl("ddlDay11");
                        DropDownList Day12 = (DropDownList)gr.FindControl("ddlDay12");
                        DropDownList Day13 = (DropDownList)gr.FindControl("ddlDay13");
                        DropDownList Day14 = (DropDownList)gr.FindControl("ddlDay14");
                        DropDownList Day15 = (DropDownList)gr.FindControl("ddlDay15");
                        DropDownList Day16 = (DropDownList)gr.FindControl("ddlDay16");
                        DropDownList Day17 = (DropDownList)gr.FindControl("ddlDay17");
                        DropDownList Day18 = (DropDownList)gr.FindControl("ddlDay18");
                        DropDownList Day19 = (DropDownList)gr.FindControl("ddlDay19");
                        DropDownList Day20 = (DropDownList)gr.FindControl("ddlDay20");
                        DropDownList Day21 = (DropDownList)gr.FindControl("ddlDay21");
                        DropDownList Day22 = (DropDownList)gr.FindControl("ddlDay22");
                        DropDownList Day23 = (DropDownList)gr.FindControl("ddlDay23");
                        DropDownList Day24 = (DropDownList)gr.FindControl("ddlDay24");
                        DropDownList Day25 = (DropDownList)gr.FindControl("ddlDay25");
                        DropDownList Day26 = (DropDownList)gr.FindControl("ddlDay26");
                        DropDownList Day27 = (DropDownList)gr.FindControl("ddlDay27");
                        DropDownList Day28 = (DropDownList)gr.FindControl("ddlDay28");
                        DropDownList Day29 = (DropDownList)gr.FindControl("ddlDay29");
                        DropDownList Day30 = (DropDownList)gr.FindControl("ddlDay30");
                        DropDownList Day31 = (DropDownList)gr.FindControl("ddlDay31");

                        int CountAbsentDays = 0;
                        int LeaveDaysEL = 0;
                        int LeaveDaysML = 0;
                        int LeaveDaysCL = 0;
                        int LeaveDaysOL = 0;

                        int LeaveDaysHoliday = 0;
                        int LeaveDaysRH = 0;
                        int LeaveDaysCOFF = 0;
                        int LeaveDaysPresent = 0;

                        if (PayableDays.Text == "")
                        {
                            PayableDays.Text = "0";
                        }

                        /**************Day 1****************************************/
                        if (Day1.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day1.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day1.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day1.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day1.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day1.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day1.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day1.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day1.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }



                        /******************************************************/

                        if (Day2.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day2.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day2.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day2.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day2.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day2.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day2.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day2.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day2.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/

                        if (Day3.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day3.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day3.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day3.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day3.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day3.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day3.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day3.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day3.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day4.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day4.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day4.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day4.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day4.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day4.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day4.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day4.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day4.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day5.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day5.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day5.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day5.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day5.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day5.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day5.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day5.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day5.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day6.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day6.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day6.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day6.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day6.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day6.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day6.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day6.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day6.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day7.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day7.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day7.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day7.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day7.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day7.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day7.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day7.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day7.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day8.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day8.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day8.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day8.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day8.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day8.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day8.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day8.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day8.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day9.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day9.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day9.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day9.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day9.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day9.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day9.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day9.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day9.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day10.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day10.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day10.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day10.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day10.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day10.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day10.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day10.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day10.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day11.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day11.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day11.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day11.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day11.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day11.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day11.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day11.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day11.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day12.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day12.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day12.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day12.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day12.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day12.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day12.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day12.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day12.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day13.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day13.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day13.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day13.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day13.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day13.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day13.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day13.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day13.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day14.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day14.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day14.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day14.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day14.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day14.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day14.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day14.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day14.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day15.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day15.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day15.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day15.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day15.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day15.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day15.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day15.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day15.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day16.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day16.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day16.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day16.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day16.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day16.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day16.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day16.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day16.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day17.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day17.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day17.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day17.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day17.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day17.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day17.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day17.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day17.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day18.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day18.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day18.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day18.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day18.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day18.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day18.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day18.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day18.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day19.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day19.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day19.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day19.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day19.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day19.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day19.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day19.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day19.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day20.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day20.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day20.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day20.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day20.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day20.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day20.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day20.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day20.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }

                        /******************************************************/
                        if (Day21.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day21.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day21.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day21.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day21.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day21.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day21.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day21.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day21.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day22.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day22.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day22.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day22.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day22.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day22.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day22.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day22.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day22.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day23.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day23.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day23.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day23.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day23.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day23.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day23.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day23.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day23.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day24.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day24.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day24.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day24.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day24.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day24.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day24.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day24.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day24.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day25.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day25.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day25.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day25.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day25.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day25.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day25.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day25.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day25.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day26.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day26.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day26.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day26.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day26.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day26.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day26.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day26.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day26.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day27.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day27.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day27.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day27.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day27.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day27.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day27.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day27.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day27.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (Day28.SelectedValue == "A")
                        {
                            CountAbsentDays += 1;
                        }
                        else if (Day28.SelectedValue == "ML")
                        {
                            LeaveDaysML += 1;
                        }
                        else if (Day28.SelectedValue == "EL")
                        {
                            LeaveDaysEL += 1;
                        }
                        else if (Day28.SelectedValue == "CL")
                        {
                            LeaveDaysCL += 1;
                        }
                        else if (Day28.SelectedValue == "OL")
                        {
                            LeaveDaysOL += 1;
                        }
                        else if (Day28.SelectedValue == "H")
                        {
                            LeaveDaysHoliday += 1;
                        }
                        else if (Day28.SelectedValue == "RH")
                        {
                            LeaveDaysRH += 1;
                        }
                        else if (Day28.SelectedValue == "CoF")
                        {
                            LeaveDaysCOFF += 1;
                        }
                        else if (Day28.SelectedValue == "P")
                        {
                            LeaveDaysPresent += 1;
                        }
                        /******************************************************/
                        if (int.Parse(ViewState["days"].ToString()) >= 29)
                        {
                            if (Day29.SelectedValue == "A")
                            {
                                CountAbsentDays += 1;
                            }
                            else if (Day29.SelectedValue == "ML")
                            {
                                LeaveDaysML += 1;
                            }
                            else if (Day29.SelectedValue == "EL")
                            {
                                LeaveDaysEL += 1;
                            }
                            else if (Day29.SelectedValue == "CL")
                            {
                                LeaveDaysCL += 1;
                            }
                            else if (Day29.SelectedValue == "OL")
                            {
                                LeaveDaysOL += 1;
                            }
                            else if (Day29.SelectedValue == "H")
                            {
                                LeaveDaysHoliday += 1;
                            }
                            else if (Day29.SelectedValue == "RH")
                            {
                                LeaveDaysRH += 1;
                            }
                            else if (Day29.SelectedValue == "CoF")
                            {
                                LeaveDaysCOFF += 1;
                            }
                            else if (Day29.SelectedValue == "P")
                            {
                                LeaveDaysPresent += 1;
                            }
                        }
                        /******************************************************/
                        if (int.Parse(ViewState["days"].ToString()) >= 30)
                        {
                            if (Day30.SelectedValue == "A")
                            {
                                CountAbsentDays += 1;
                            }
                            else if (Day30.SelectedValue == "ML")
                            {
                                LeaveDaysML += 1;
                            }
                            else if (Day30.SelectedValue == "EL")
                            {
                                LeaveDaysEL += 1;
                            }
                            else if (Day30.SelectedValue == "CL")
                            {
                                LeaveDaysCL += 1;
                            }
                            else if (Day30.SelectedValue == "OL")
                            {
                                LeaveDaysOL += 1;
                            }
                            else if (Day30.SelectedValue == "H")
                            {
                                LeaveDaysHoliday += 1;
                            }
                            else if (Day30.SelectedValue == "RH")
                            {
                                LeaveDaysRH += 1;
                            }
                            else if (Day30.SelectedValue == "CoF")
                            {
                                LeaveDaysCOFF += 1;
                            }
                            else if (Day30.SelectedValue == "P")
                            {
                                LeaveDaysPresent += 1;
                            }
                        }
                        /****************31st day**************************************/
                        if (int.Parse(ViewState["days"].ToString()) >=31)
                        {
                            if (Day31.SelectedValue == "A")
                            {
                                CountAbsentDays += 1;
                            }
                            else if (Day31.SelectedValue == "ML")
                            {
                                LeaveDaysML += 1;
                            }
                            else if (Day31.SelectedValue == "EL")
                            {
                                LeaveDaysEL += 1;
                            }
                            else if (Day31.SelectedValue == "CL")
                            {
                                LeaveDaysCL += 1;
                            }
                            else if (Day31.SelectedValue == "OL")
                            {
                                LeaveDaysOL += 1;
                            }
                            else if (Day31.SelectedValue == "H")
                            {
                                LeaveDaysHoliday += 1;
                            }
                            else if (Day31.SelectedValue == "RH")
                            {
                                LeaveDaysRH += 1;
                            }
                            else if (Day31.SelectedValue == "CoF")
                            {
                                LeaveDaysCOFF += 1;
                            }
                            else if (Day31.SelectedValue == "P")
                            {
                                LeaveDaysPresent += 1;
                            }
                        }
                        /******************************************************/

                        if (TotalDays < CountAbsentDays)
                        {
                            CountAbsentDays = TotalDays;
                        }
                        PayableDays.Text = (TotalDays - CountAbsentDays).ToString();
                        /******************************************************/
                        //int SundayCount = int.Parse(ViewState["SundayCoun"].ToString());
                        //if ((SundayCount + CountAbsentDays) == TotalDays)
                        //{
                        //    PayableDays.Text = "0";
                        //}


                        objdb.ByProcedure("SpPayrollEmpAttendance",
                        new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo", "Month", "Day1", "Day2", "Day3", "Day4", "Day5", "Day6", "Day7", "Day8", "Day9", "Day10", "Day11", "Day12", "Day13", "Day14", "Day15", "Day16", "Day17", "Day18", "Day19", "Day20", "Day21", "Day22", "Day23", "Day24", "Day25", "Day26", "Day27", "Day28", "Day29", "Day30", "Day31", "TotalDayOfMonth", "PayableDays", "NotPayableDays", "Attendance_UpdatedBy", "LeaveDaysEL", "LeaveDaysML", "LeaveDaysOL", "LeaveDaysCL", "LeaveDaysHoliday", "LeaveDaysRH", "LeaveDaysCoF", "LeaveDaysPresent" },
                        new string[] { "0", Emp_ID.Text, Office_ID, Year, MonthNo, Month, Day1.SelectedValue, Day2.SelectedValue, Day3.SelectedValue, Day4.SelectedValue, Day5.SelectedValue, Day6.SelectedValue, Day7.SelectedValue, Day8.SelectedValue, Day9.SelectedValue, Day10.SelectedValue, Day11.SelectedValue, Day12.SelectedValue, Day13.SelectedValue, Day14.SelectedValue, Day15.SelectedValue, Day16.SelectedValue, 
                                    Day17.SelectedValue, Day18.SelectedValue, Day19.SelectedValue, Day20.SelectedValue, Day21.SelectedValue, Day22.SelectedValue, Day23.SelectedValue, Day24.SelectedValue, Day25.SelectedValue, Day26.SelectedValue, Day27.SelectedValue, Day28.SelectedValue, Day29.SelectedValue, Day30.SelectedValue, Day31.SelectedValue, TotalDays.ToString(), PayableDays.Text, CountAbsentDays.ToString(),LoginUserID,LeaveDaysEL.ToString(),LeaveDaysML.ToString(),LeaveDaysOL.ToString(),LeaveDaysCL.ToString(),LeaveDaysHoliday.ToString(),LeaveDaysRH.ToString(),LeaveDaysCOFF.ToString(),LeaveDaysPresent.ToString()}, "dataset");
                    }
                }
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                FillGrid();
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            DivDetail.Visible = false;
            btnSave.Visible = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        try
        {
            Int32 iDays1 = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue));
            if (ddlMonth.SelectedValue.ToString() == "01")
                iDays1 = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue) - 1, 12);
            else
                iDays1 = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue) - 1);
            if (iDays1 == 28)
                ColCount = 8;
            else if (iDays1 == 29)
                ColCount = 9;
            else if (iDays1 == 30)
                ColCount = 10;


            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Current Month";
            cell.ColumnSpan = 20;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Previous Month";
            cell.ColumnSpan = ColCount;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);





            GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            lblMsg.Text = "";
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    /*******New PP**********/
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        DivDetail.Visible = false;
        btnSave.Visible = false;
        FillDropdown();

    }
}