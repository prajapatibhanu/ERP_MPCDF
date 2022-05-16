using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollAllEmpEarnDedDetailNew : System.Web.UI.Page
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
                    if (ViewState["Office_ID"].ToString() == "1")
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
            if (ddlOffice.SelectedIndex > 0 && ddlFinancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
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
                //    FillSalary_Dynamic2();
                //}
                //else if (ddlOffice.SelectedValue.ToString() == "1")
                //{
                //    FillSalary_Dynamic2();
                //}
                //else
                //{
                //    FillSalary();
                //}

                FillSalary_Dynamic2();
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
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "10", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                StringBuilder htmlStr = new StringBuilder();
                htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                htmlStr.Append("<thead  style='border-bottom: 0px solid white; display: table-header-group;' class='printmaindata'>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
                htmlStr.Append("<th colspan='2' class='text-left'>Name Of Employee</th>");
                htmlStr.Append("<th colspan=''>Designation</th>");
                htmlStr.Append("<th colspan=''>Pr. Day</th>");
                htmlStr.Append("<th colspan=''>IFSC Code</th>");
                htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                htmlStr.Append("<th>EPF No./GPF No.</th>");
                //htmlStr.Append("<th>GI No.</th>");
                htmlStr.Append("</tr>");

                /*****A Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>A</th>");
                htmlStr.Append("<th class='empearning'>Basic Pay</th>");
                htmlStr.Append("<th class='empearning'>D.A.</th>");
                htmlStr.Append("<th class='empearning'>H.R.A.</th>");
                htmlStr.Append("<th class='empearning'>C.C.A.</th>");
                htmlStr.Append("<th class='empearning'>Cnv All</th>");
                htmlStr.Append("<th class='empearning'>Spl Pay</th>");
                htmlStr.Append("<th class='empearning'>Wsh All</th>");
                htmlStr.Append("<th class='empearning'>Oth All</th>");
                htmlStr.Append("<th class='empearning'>Ord All</th>");
                htmlStr.Append("<th class='empearning'>Ofc All</th>");
                htmlStr.Append("</tr>");
                /*****B Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>B</th>");
                htmlStr.Append("<th class='empearning'>Drs.pay</th>");
                htmlStr.Append("<th class='empearning'>I.Relf</th>");
                htmlStr.Append("<th class='empearning'>Att.Bns</th>");
                htmlStr.Append("<th class='empearning'>Med.Aid</th>");
                htmlStr.Append("<th class='empearning'>DA-2</th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("</tr>");
                /*****C Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>C</th>");
                htmlStr.Append("<th class='empdeduction'>E.P.F.</th>");
                htmlStr.Append("<th class='empdeduction'>G.P.F.</th>");
                htmlStr.Append("<th class='empdeduction'>F.B.F</th>");
                htmlStr.Append("<th class='empdeduction'>G.I.S.</th>");
                htmlStr.Append("<th class='empdeduction'>P.Tax</th>");
                htmlStr.Append("<th class='empdeduction'>Security</th>");
                htmlStr.Append("<th class='empdeduction'>I.Tax</th>");
                htmlStr.Append("<th class='empdeduction'>Veh.Reg</th>");
                htmlStr.Append("<th class='empdeduction'>H.R.R.</th>");
                htmlStr.Append("<th class='empdeduction'>A.D.A.</th>");
                htmlStr.Append("</tr>");
                /*****D Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>D</th>");
                htmlStr.Append("<th class='empdeduction'>Grain</th>");
                htmlStr.Append("<th class='empdeduction'>Festival</th>");
                htmlStr.Append("<th class='empdeduction'>SCT.MPD</th>");
                htmlStr.Append("<th class='empdeduction'>Spl.Ins</th>");
                htmlStr.Append("<th class='empdeduction'>Pay.Ins</th>");
                htmlStr.Append("<th class='empdeduction'>HBL.Ins</th>");
                htmlStr.Append("<th class='empdeduction'>Car.BCB</th>");
                htmlStr.Append("<th class='empdeduction'>Tur.Ins</th>");
                htmlStr.Append("<th class='empdeduction'>Cyc.Adv</th>");
                htmlStr.Append("<th class='empdeduction'>LTC.Adv</th>");
                htmlStr.Append("</tr>");
                /*****E Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>E</th>");
                htmlStr.Append("<th class='empbalance'>Gr.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Fst.Bl</th>");
                htmlStr.Append("<th class='empbalance'>SCT.Bl</th>");
                htmlStr.Append("<th class='empbalance'Spl.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Pay.Bl</th>");
                htmlStr.Append("<th class='empbalance'>HBL.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Car.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Tur.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Cyc.Bl</th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("</tr>");
                /*****F Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>F</th>");
                htmlStr.Append("<th class='empdeduction'>Lubr.</th>");
                htmlStr.Append("<th class='empdeduction'>Misc.Rc</th>");
                htmlStr.Append("<th class='empdeduction'>Car-I</th>");
                htmlStr.Append("<th class='empdeduction'>Cr.Soc.</th>");
                htmlStr.Append("<th class='empdeduction'>RD</th>");
                htmlStr.Append("<th class='empdeduction'>Un Rec.</th>");
                htmlStr.Append("<th class='empdeduction'>SCT.BCB</th>");
                htmlStr.Append("<th class='empdeduction'>CR-Schm</th>");
                htmlStr.Append("<th class='empdeduction'>CR.Soc.</th>");
                htmlStr.Append("<th class='empdeduction'>Un.Soc.</th>");
                htmlStr.Append("</tr>");
                /*****G Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>G</th>");
                htmlStr.Append("<th class='empbalance'>Lubr.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Misc.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Car-I.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Cr.Soc.Bl</th>");
                htmlStr.Append("<th class='empbalance'>RD.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Un.Bl</th>");
                htmlStr.Append("<th class='empbalance'>BCB.Bl</th>");
                htmlStr.Append("<th class='empbalance'>CR-Schm.Bl</th>");
                htmlStr.Append("<th class='empbalance'>CR.Soc.Bl</th>");
                htmlStr.Append("<th class='empbalance'>Un.Soc.Bl</th>");
                htmlStr.Append("</tr>");
                /*****H Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>H</th>");
                htmlStr.Append("<th class='empdeduction'>MPD Soc</th>");
                htmlStr.Append("<th class='empdeduction'>Plot</th>");
                htmlStr.Append("<th class='empdeduction'>Car-II</th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("</tr>");
                /*****I Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>I</th>");
                htmlStr.Append("<th class='empbalance'>MPD Bl.</th>");
                htmlStr.Append("<th class='empbalance'>Plot Bl.</th>");
                htmlStr.Append("<th class='empbalance'>Car2 Bl.</th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th></th>");
                htmlStr.Append("<th class='empearning'>Gross</th>");
                htmlStr.Append("<th class='empdeduction'>Tot. Ded</th>");
                htmlStr.Append("<th class='empbalance'>Net Amount</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<table>");

                string repeadtCodePrintPage = htmlStr.ToString();

                // Row 1-A
                decimal Tables0Rows0Basic = 0;
                decimal Tables1Rows1 = 0;
                decimal Tables1Rows2 = 0;
                decimal Tables1Rows3 = 0;
                decimal Tables1Rows4 = 0;
                decimal Tables1Rows5 = 0;
                decimal Tables1Rows6 = 0;
                decimal Tables1Rows7 = 0;
                decimal Tables1Rows8 = 0;
                decimal Tables1Rows9 = 0;


                // Row 2-B
                decimal Tables1Rows10 = 0;
                decimal Tables1Rows11 = 0;
                decimal Tables1Rows12 = 0;
                decimal Tables1Rows13 = 0;
                decimal Tables1Rows14 = 0;

                // Row 3-C
                decimal Tables2Rows1 = 0;
                decimal Tables2Rows2 = 0;
                decimal Tables2Rows3 = 0;
                decimal Tables2Rows4 = 0;
                decimal Tables2Rows5 = 0;
                decimal Tables2Rows6 = 0;
                decimal Tables2Rows7 = 0;
                decimal Tables2Rows8 = 0;
                decimal Tables2Rows9 = 0;

                // Row 4-D
                decimal Tables2Rows10 = 0;
                decimal Tables2Rows11 = 0;
                decimal Tables2Rows12 = 0;
                decimal Tables2Rows13 = 0;
                decimal Tables2Rows14 = 0;
                decimal Tables2Rows15 = 0;
                decimal Tables2Rows16 = 0;
                decimal Tables2Rows17 = 0;
                decimal Tables2Rows18 = 0;
                decimal Tables2Rows19 = 0;

                // Row 5-E
                //float Tables3Rows1 = 0;
                //float Tables3Rows2 = 0;
                //float Tables3Rows3 = 0;
                //float Tables3Rows4 = 0;
                //float Tables3Rows5 = 0;
                //float Tables3Rows6 = 0;
                //float Tables3Rows7 = 0;
                //float Tables3Rows8 = 0;
                //float Tables3Rows9 = 0;

                // Row 6-F
                decimal Tables2Rows20 = 0;
                decimal Tables2Rows21 = 0;
                decimal Tables2Rows22 = 0;
                decimal Tables2Rows23 = 0;
                decimal Tables2Rows24 = 0;
                decimal Tables2Rows25 = 0;
                decimal Tables2Rows26 = 0;
                decimal Tables2Rows27 = 0;
                decimal Tables2Rows28 = 0;
                decimal Tables2Rows29 = 0;

                // Row 7-G


                // Row 8-H   
                decimal Tables2Rows30 = 0;
                decimal Tables2Rows31 = 0;
                decimal Tables2Rows32 = 0;


                // Row 9-I
                decimal Tables0Rows0EarningTotal = 0;
                decimal Tables2Rows13DeductionTotal = 0;
                decimal Tables0Rows0Salary_NetSalary = 0;





                string Islast_heading = "";
                for (int i = 0; i < Count; i++)
                {

                    string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();

                    DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "11", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        // Row 0
                        string pagebreak = "";
                        if (i == 14 || i == 30 || i == 46 || i == 62)
                        {
                            pagebreak = ".page-break";
                            Islast_heading = "Yes";
                            //htmlStr.Append(repeadtCodePrintPage);
                        }

                        htmlStr.Append("<tr class='" + pagebreak + "'>");
                        htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class='" + pagebreak + "'>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th class='text-left'>" + (i + 1).ToString() + "</th>");
                        htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["SecNo"].ToString() + "</th>");
                        htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["EmpNo"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2' class='text-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["PrDay"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'>'" + ds.Tables[0].Rows[i]["SalaryAccount"].ToString() + "'</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["EPF_No"].ToString() + "</th>");
                        htmlStr.Append("</tr>");

                        // Row 1
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");

                        Tables0Rows0Basic = Tables0Rows0Basic + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");

                        Tables1Rows1 = Tables1Rows1 + decimal.Parse(ds1.Tables[1].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[0]["Earning"].ToString() + "</td>");

                        Tables1Rows2 = Tables1Rows2 + decimal.Parse(ds1.Tables[1].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[1]["Earning"].ToString() + "</td>");

                        Tables1Rows3 = Tables1Rows3 + decimal.Parse(ds1.Tables[1].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[2]["Earning"].ToString() + " </td>");

                        htmlStr.Append("<td class='alignright'></td>");

                        Tables1Rows4 = Tables1Rows4 + decimal.Parse(ds1.Tables[1].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[3]["Earning"].ToString() + "</td>");

                        Tables1Rows5 = Tables1Rows5 + decimal.Parse(ds1.Tables[1].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[4]["Earning"].ToString() + "</td>");

                        Tables1Rows6 = Tables1Rows6 + decimal.Parse(ds1.Tables[1].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[5]["Earning"].ToString() + "</td>");

                        Tables1Rows7 = Tables1Rows7 + decimal.Parse(ds1.Tables[1].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[6]["Earning"].ToString() + "</td>");

                        Tables1Rows8 = Tables1Rows8 + decimal.Parse(ds1.Tables[1].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[7]["Earning"].ToString() + "</td>");

                        htmlStr.Append("</tr>");



                        // Row 2
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables1Rows9 = Tables1Rows9 + decimal.Parse(ds1.Tables[1].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[8]["Earning"].ToString() + "</td>");
                        Tables1Rows10 = Tables1Rows10 + decimal.Parse(ds1.Tables[1].Rows[9]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[9]["Earning"].ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables1Rows11 = Tables1Rows11 + decimal.Parse(ds1.Tables[1].Rows[10]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[10]["Earning"].ToString() + "</td>");

                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");



                        // Row 3

                        //float Tables2Rows1 = 0;
                        //float Tables2Rows2 = 0;
                        //float Tables2Rows3 = 0;
                        //float Tables2Rows4 = 0;
                        //float Tables2Rows5 = 0;
                        //float Tables2Rows6 = 0;
                        //float Tables2Rows7 = 0;
                        //float Tables2Rows8 = 0;
                        //float Tables2Rows9 = 0;


                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows1 = Tables2Rows1 + decimal.Parse(ds1.Tables[2].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[0]["Earning"].ToString() + "</td>");

                        htmlStr.Append("<td class='alignright'> </td>");

                        Tables2Rows2 = Tables2Rows2 + decimal.Parse(ds1.Tables[2].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[1]["Earning"].ToString() + "</td>");
                        Tables2Rows3 = Tables2Rows3 + decimal.Parse(ds1.Tables[2].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[2]["Earning"].ToString() + "</td>");

                        Tables2Rows4 = Tables2Rows4 + decimal.Parse(ds1.Tables[2].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[3]["Earning"].ToString() + "</td>");

                        Tables2Rows5 = Tables2Rows5 + decimal.Parse(ds1.Tables[2].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[4]["Earning"].ToString() + "</td>");
                        Tables2Rows6 = Tables2Rows6 + decimal.Parse(ds1.Tables[2].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[5]["Earning"].ToString() + "</td>");

                        Tables2Rows7 = Tables2Rows7 + decimal.Parse(ds1.Tables[2].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[6]["Earning"].ToString() + "</td>");

                        Tables2Rows8 = Tables2Rows8 + decimal.Parse(ds1.Tables[2].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[7]["Earning"].ToString() + "</td>");
                        Tables2Rows9 = Tables2Rows9 + decimal.Parse(ds1.Tables[2].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[8]["Earning"].ToString() + "</td>");
                        htmlStr.Append("</tr>");


                        // Row 4
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows10 = Tables2Rows10 + decimal.Parse(ds1.Tables[2].Rows[9]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[9]["Earning"].ToString() + "</td>");

                        Tables2Rows11 = Tables2Rows11 + decimal.Parse(ds1.Tables[2].Rows[10]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[10]["Earning"].ToString() + "</td>");
                        Tables2Rows12 = Tables2Rows12 + decimal.Parse(ds1.Tables[2].Rows[11]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[11]["Earning"].ToString() + "</td>");

                        Tables2Rows13 = Tables2Rows13 + decimal.Parse(ds1.Tables[2].Rows[12]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[12]["Earning"].ToString() + "</td>");
                        Tables2Rows14 = Tables2Rows14 + decimal.Parse(ds1.Tables[2].Rows[13]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[13]["Earning"].ToString() + "</td>");

                        Tables2Rows15 = Tables2Rows15 + decimal.Parse(ds1.Tables[2].Rows[14]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[14]["Earning"].ToString() + "</td>");

                        Tables2Rows16 = Tables2Rows16 + decimal.Parse(ds1.Tables[2].Rows[15]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[15]["Earning"].ToString() + "</td>");

                        Tables2Rows17 = Tables2Rows17 + decimal.Parse(ds1.Tables[2].Rows[16]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[16]["Earning"].ToString() + "</td>");

                        Tables2Rows18 = Tables2Rows18 + decimal.Parse(ds1.Tables[2].Rows[17]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[17]["Earning"].ToString() + "</td>");

                        Tables2Rows19 = Tables2Rows19 + decimal.Parse(ds1.Tables[2].Rows[18]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[18]["Earning"].ToString() + "</td>");

                        htmlStr.Append("</tr>");


                        // Row 5
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");

                        // Row 6
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows20 = Tables2Rows20 + decimal.Parse(ds1.Tables[2].Rows[19]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[19]["Earning"].ToString() + "</td>");

                        Tables2Rows21 = Tables2Rows21 + decimal.Parse(ds1.Tables[2].Rows[20]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[20]["Earning"].ToString() + "</td>");

                        Tables2Rows22 = Tables2Rows22 + decimal.Parse(ds1.Tables[2].Rows[21]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[21]["Earning"].ToString() + "</td>");
                        Tables2Rows23 = Tables2Rows23 + decimal.Parse(ds1.Tables[2].Rows[22]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[22]["Earning"].ToString() + "</td>");

                        Tables2Rows24 = Tables2Rows24 + decimal.Parse(ds1.Tables[2].Rows[23]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[23]["Earning"].ToString() + "</td>");
                        Tables2Rows25 = Tables2Rows25 + decimal.Parse(ds1.Tables[2].Rows[24]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[24]["Earning"].ToString() + "</td>");
                        Tables2Rows26 = Tables2Rows26 + decimal.Parse(ds1.Tables[2].Rows[25]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[25]["Earning"].ToString() + "</td>");
                        Tables2Rows27 = Tables2Rows27 + decimal.Parse(ds1.Tables[2].Rows[26]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[26]["Earning"].ToString() + "</td>");
                        Tables2Rows28 = Tables2Rows28 + decimal.Parse(ds1.Tables[2].Rows[27]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[27]["Earning"].ToString() + "</td>");
                        Tables2Rows29 = Tables2Rows29 + decimal.Parse(ds1.Tables[2].Rows[28]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[28]["Earning"].ToString() + "</td>");

                        htmlStr.Append("</tr>");

                        //float Tables2Rows20 = 0;
                        //float Tables2Rows21 = 0;
                        //float Tables2Rows22 = 0;
                        //float Tables2Rows23 = 0;
                        //float Tables2Rows24 = 0;
                        //float Tables2Rows25 = 0;
                        //float Tables2Rows26 = 0;
                        //float Tables2Rows27 = 0;
                        //float Tables2Rows28 = 0;
                        //float Tables2Rows29 = 0;

                        // Row 7
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");

                        // Row 8
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows30 = Tables2Rows30 + decimal.Parse(ds1.Tables[2].Rows[29]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[29]["Earning"].ToString() + "</td>");
                        Tables2Rows31 = Tables2Rows31 + decimal.Parse(ds1.Tables[2].Rows[30]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[30]["Earning"].ToString() + "</td>");
                        Tables2Rows32 = Tables2Rows32 + decimal.Parse(ds1.Tables[2].Rows[31]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[31]["Earning"].ToString() + "</td>");
                        //htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");



                        //// Row 9-I
                        //float Tables0Rows0EarningTotal = 0;
                        //float Tables2Rows13DeductionTotal = 0;
                        //float Tables0Rows0Salary_NetSalary = 0;


                        // Row 9
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");

                        Tables0Rows0EarningTotal = Tables0Rows0EarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</td>");

                        Tables2Rows13DeductionTotal = Tables2Rows13DeductionTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</td>");

                        Tables0Rows0Salary_NetSalary = Tables0Rows0Salary_NetSalary + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");

                        htmlStr.Append("</tr>");
                        htmlStr.Append("</table>");
                        htmlStr.Append("</tr>");


                    }
                }
                if (Islast_heading == "Yes")
                {
                    /**********************************************************************/
                    //htmlStr.Append(repeadtCodePrintPage);

                    htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                    htmlStr.Append("<thead  style='border-bottom: 0px solid white;'>");
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
                    htmlStr.Append("<th colspan='2' class='text-left'>Name Of Employee</th>");
                    htmlStr.Append("<th colspan=''>Designation</th>");
                    htmlStr.Append("<th colspan=''>Pr. Day</th>");
                    htmlStr.Append("<th colspan=''>IFSC Code</th>");
                    htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                    htmlStr.Append("<th>EPF No./GPF No.</th>");
                    //htmlStr.Append("<th>GI No.</th>");
                    htmlStr.Append("</tr>");

                    /*****A Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>A</th>");
                    htmlStr.Append("<th class='empearning'>Basic Pay</th>");
                    htmlStr.Append("<th class='empearning'>D.A.</th>");
                    htmlStr.Append("<th class='empearning'>H.R.A.</th>");
                    htmlStr.Append("<th class='empearning'>C.C.A.</th>");
                    htmlStr.Append("<th class='empearning'>Cnv All</th>");
                    htmlStr.Append("<th class='empearning'>Spl Pay</th>");
                    htmlStr.Append("<th class='empearning'>Wsh All</th>");
                    htmlStr.Append("<th class='empearning'>Oth All</th>");
                    htmlStr.Append("<th class='empearning'>Ord All</th>");
                    htmlStr.Append("<th class='empearning'>Ofc All</th>");
                    htmlStr.Append("</tr>");
                    /*****B Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>B</th>");
                    htmlStr.Append("<th class='empearning'>Drs.pay</th>");
                    htmlStr.Append("<th class='empearning'>I.Relf</th>");
                    htmlStr.Append("<th class='empearning'>Att.Bns</th>");
                    htmlStr.Append("<th class='empearning'>Med.Aid</th>");
                    htmlStr.Append("<th class='empearning'>DA-2</th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("</tr>");
                    /*****C Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>C</th>");
                    htmlStr.Append("<th class='empdeduction'>E.P.F.</th>");
                    htmlStr.Append("<th class='empdeduction'>G.P.F.</th>");
                    htmlStr.Append("<th class='empdeduction'>F.B.F</th>");
                    htmlStr.Append("<th class='empdeduction'>G.I.S.</th>");
                    htmlStr.Append("<th class='empdeduction'>P.Tax</th>");
                    htmlStr.Append("<th class='empdeduction'>Security</th>");
                    htmlStr.Append("<th class='empdeduction'>I.Tax</th>");
                    htmlStr.Append("<th class='empdeduction'>Veh.Reg</th>");
                    htmlStr.Append("<th class='empdeduction'>H.R.R.</th>");
                    htmlStr.Append("<th class='empdeduction'>A.D.A.</th>");
                    htmlStr.Append("</tr>");
                    /*****D Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>D</th>");
                    htmlStr.Append("<th class='empdeduction'>Grain</th>");
                    htmlStr.Append("<th class='empdeduction'>Festival</th>");
                    htmlStr.Append("<th class='empdeduction'>SCT.MPD</th>");
                    htmlStr.Append("<th class='empdeduction'>Spl.Ins</th>");
                    htmlStr.Append("<th class='empdeduction'>Pay.Ins</th>");
                    htmlStr.Append("<th class='empdeduction'>HBL.Ins</th>");
                    htmlStr.Append("<th class='empdeduction'>Car.BCB</th>");
                    htmlStr.Append("<th class='empdeduction'>Tur.Ins</th>");
                    htmlStr.Append("<th class='empdeduction'>Cyc.Adv</th>");
                    htmlStr.Append("<th class='empdeduction'>LTC.Adv</th>");
                    htmlStr.Append("</tr>");
                    /*****E Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>E</th>");
                    htmlStr.Append("<th class='empbalance'>Gr.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Fst.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>SCT.Bl</th>");
                    htmlStr.Append("<th class='empbalance'Spl.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Pay.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>HBL.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Car.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Tur.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Cyc.Bl</th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("</tr>");
                    /*****F Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>F</th>");
                    htmlStr.Append("<th class='empdeduction'>Lubr.</th>");
                    htmlStr.Append("<th class='empdeduction'>Misc.Rc</th>");
                    htmlStr.Append("<th class='empdeduction'>Car-I</th>");
                    htmlStr.Append("<th class='empdeduction'>Cr.Soc.</th>");
                    htmlStr.Append("<th class='empdeduction'>RD</th>");
                    htmlStr.Append("<th class='empdeduction'>Un Rec.</th>");
                    htmlStr.Append("<th class='empdeduction'>SCT.BCB</th>");
                    htmlStr.Append("<th class='empdeduction'>CR-Schm</th>");
                    htmlStr.Append("<th class='empdeduction'>CR.Soc.</th>");
                    htmlStr.Append("<th class='empdeduction'>Un.Soc.</th>");
                    htmlStr.Append("</tr>");
                    /*****G Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>G</th>");
                    htmlStr.Append("<th class='empbalance'>Lubr.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Misc.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Car-I.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Cr.Soc.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>RD.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Un.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>BCB.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>CR-Schm.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>CR.Soc.Bl</th>");
                    htmlStr.Append("<th class='empbalance'>Un.Soc.Bl</th>");
                    htmlStr.Append("</tr>");
                    /*****H Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>H</th>");
                    htmlStr.Append("<th class='empdeduction'>MPD Soc</th>");
                    htmlStr.Append("<th class='empdeduction'>Plot</th>");
                    htmlStr.Append("<th class='empdeduction'>Car-II</th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("</tr>");
                    /*****I Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>I</th>");
                    htmlStr.Append("<th class='empbalance'>MPD Bl.</th>");
                    htmlStr.Append("<th class='empbalance'>Plot Bl.</th>");
                    htmlStr.Append("<th class='empbalance'>Car2 Bl.</th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("<th class='empearning'>Gross</th>");
                    htmlStr.Append("<th class='empdeduction'>Tot. Ded</th>");
                    htmlStr.Append("<th class='empbalance'>Net Amount</th>");
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
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0Basic.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>  </td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows8.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****B Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows9.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("</tr>");

                /*****C Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows9.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****D Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows12.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows13.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows14.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows15.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows16.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows17.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows18.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows19.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****E Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("</tr>");

                /*****F Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows20.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows21.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows22.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows23.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows24.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows25.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows26.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows27.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows28.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows29.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****G Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("</tr>");

                /*****H Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows30.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows31.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("</tr>");

                /*****I Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0EarningTotal.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows13DeductionTotal.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0Salary_NetSalary.ToString() + "</td>");
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
    protected void FillSalary_GWL()
    {
        try
        {
            DivDetail.InnerHtml = "";
            string Office_ID = ddlOffice.SelectedValue.ToString();
            string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
            string Salary_MonthNo = ddlMonth.SelectedValue.ToString();

            lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();

            DivHead.Visible = true;
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "10", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                StringBuilder htmlStr = new StringBuilder();
                htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                htmlStr.Append("<thead  style='border-bottom: 0px solid white; display: table-header-group;' class='printmaindata'>");
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
                htmlStr.Append("<th colspan='2' class='text-left'>Name Of Employee</th>");
                htmlStr.Append("<th colspan=''>Designation</th>");
                htmlStr.Append("<th colspan=''>Pr. Day</th>");
                htmlStr.Append("<th colspan=''>IFSC Code</th>");
                htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                htmlStr.Append("<th>EPF No./GPF No.</th>");
                //htmlStr.Append("<th>GI No.</th>");
                htmlStr.Append("</tr>");

                /*****A Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>A</th>");
                htmlStr.Append("<th class='empearning'>Basic Pay</th>");
                //htmlStr.Append("<th class='empearning'>C.C.A.</th>");
                htmlStr.Append("<th class='empearning'>PAY SCALE FIXATION ARREAR</th>");
                htmlStr.Append("<th class='empearning'>CONVEYANCE ALLOWANCE</th>");
                htmlStr.Append("<th class='empearning'>DA-2</th>");
                htmlStr.Append("<th class='empearning'>DEARNESS ALLOWANCE</th>");
                htmlStr.Append("<th class='empearning'>GRADE PAY</th>");
                htmlStr.Append("<th class='empearning'>HOUSE RENT ALLOWANCE</th>");
                htmlStr.Append("<th class='empearning'>INTERIM RELIEF</th>");
                htmlStr.Append("<th class='empearning'>ORDERLY ALLOWANCE</th>");
                htmlStr.Append("<th class='empearning'>OTHER ALLOWANCE</th>");
                htmlStr.Append("</tr>");
                /*****B Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>B</th>");
                htmlStr.Append("<th class='empearning'>SPL PAY</th>");
                htmlStr.Append("<th class='empearning'>WASHING ALLOWANCE</th>");
                htmlStr.Append("<th class='empearning'></th>");
                htmlStr.Append("<th class='empearning'></th>");
                htmlStr.Append("<th class='empearning'></th>");
                htmlStr.Append("<th>A.D.A. DEDUCTION</th>");
                htmlStr.Append("<th>ARR EPF</th>");
                htmlStr.Append("<th>CELL DED</th>");
                htmlStr.Append("<th>CR SOCIETY</th>");
                htmlStr.Append("<th>CREDIT SOC. II</th>");
                htmlStr.Append("</tr>");
                /*****C Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>C</th>");
                htmlStr.Append("<th class='empdeduction'>EPF DEDUCTION</th>");
                htmlStr.Append("<th class='empdeduction'>ESI</th>");
                htmlStr.Append("<th class='empdeduction'>GHEE DED</th>");
                htmlStr.Append("<th class='empdeduction'>H.R.R.</th>");
                htmlStr.Append("<th class='empdeduction'>INCOME TAX</th>");
                htmlStr.Append("<th class='empdeduction'>MLK ADV</th>");
                htmlStr.Append("<th class='empdeduction'>OTHER ADVANCE</th>");
                htmlStr.Append("<th class='empdeduction'>PAY ADVANCE</th>");
                htmlStr.Append("<th class='empdeduction'>PLOT</th>");
                htmlStr.Append("<th class='empdeduction'>PROFESSIONAL TAX</th>");
                htmlStr.Append("</tr>");
                /*****D Row*******/
                htmlStr.Append("<tr class='tblheadingslip'>");
                htmlStr.Append("<th>D</th>");
                htmlStr.Append("<th class='empdeduction'>RD AC</th>");
                //htmlStr.Append("<th class='empdeduction'>SBI HAZ PROFESSIONAL TAX</th>");
                htmlStr.Append("<th class='empdeduction'>PROFESSIONAL TAX</th>");
                htmlStr.Append("<th class='empdeduction'>SCOOTER-BCCB</th>");
                htmlStr.Append("<th class='empdeduction'>VEHICLE</th>");
                htmlStr.Append("<th class='empdeduction'>YKK</th>");
                htmlStr.Append("<th class='empdeduction'></th>");
                htmlStr.Append("<th class='empdeduction'></th>");
                htmlStr.Append("<th class='empearning'>Gross</th>");
                htmlStr.Append("<th class='empdeduction'>Tot. Ded</th>");
                htmlStr.Append("<th class='empbalance'>Net Amount</th>");
                htmlStr.Append("</tr>");

                htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<table>");

                string repeadtCodePrintPage = htmlStr.ToString();

                // Row 1-A
                decimal Tables0Rows0Basic = 0;
                decimal Tables1Rows1 = 0;
                decimal Tables1Rows2 = 0;
                decimal Tables1Rows3 = 0;
                decimal Tables1Rows4 = 0;
                decimal Tables1Rows5 = 0;
                decimal Tables1Rows6 = 0;
                decimal Tables1Rows7 = 0;
                decimal Tables1Rows8 = 0;
                decimal Tables1Rows9 = 0;


                // Row 2-B
                decimal Tables1Rows10 = 0;
                decimal Tables1Rows11 = 0;
                decimal Tables2Rows1 = 0;
                decimal Tables2Rows2 = 0;
                decimal Tables2Rows3 = 0;
                decimal Tables2Rows4 = 0;
                decimal Tables2Rows5 = 0;

                // Row 3-C
                decimal Tables2Rows6 = 0;
                decimal Tables2Rows7 = 0;
                decimal Tables2Rows8 = 0;
                decimal Tables2Rows9 = 0;
                decimal Tables2Rows10 = 0;
                decimal Tables2Rows11 = 0;
                decimal Tables2Rows12 = 0;
                decimal Tables2Rows13 = 0;
                decimal Tables2Rows14 = 0;
                decimal Tables2Rows15 = 0;
                // Row 4-D

                decimal Tables2Rows16 = 0;
                decimal Tables2Rows17 = 0;
                decimal Tables2Rows18 = 0;
                decimal Tables2Rows19 = 0;
                decimal Tables2Rows20 = 0;


                // Row 4-D
                decimal Tables0Rows0EarningTotal = 0;
                decimal Tables2Rows13DeductionTotal = 0;
                decimal Tables0Rows0Salary_NetSalary = 0;





                string Islast_heading = "";
                for (int i = 0; i < Count; i++)
                {

                    string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();

                    DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "31", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        // Row 0
                        string pagebreak = "";
                        if (i == 14 || i == 30 || i == 46 || i == 62)
                        {
                            pagebreak = ".page-break";
                            Islast_heading = "Yes";
                            //htmlStr.Append(repeadtCodePrintPage);
                        }

                        htmlStr.Append("<tr class='" + pagebreak + "'>");
                        htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class='" + pagebreak + "'>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th class='text-left'>" + (i + 1).ToString() + "</th>");
                        htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["SecNo"].ToString() + "</th>");
                        htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["EmpNo"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2' class='text-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["PrDay"].ToString() + "</th>");
                        htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</th>");
                        htmlStr.Append("<th colspan='2'>'" + ds.Tables[0].Rows[i]["SalaryAccount"].ToString() + "'</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["EPF_No"].ToString() + "</th>");
                        htmlStr.Append("</tr>");

                        // Row 1
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");

                        Tables0Rows0Basic = Tables0Rows0Basic + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");

                        Tables1Rows1 = Tables1Rows1 + decimal.Parse(ds1.Tables[1].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[0]["Earning"].ToString() + "</td>");

                        Tables1Rows2 = Tables1Rows2 + decimal.Parse(ds1.Tables[1].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[1]["Earning"].ToString() + "</td>");

                        Tables1Rows3 = Tables1Rows3 + decimal.Parse(ds1.Tables[1].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[2]["Earning"].ToString() + " </td>");

                        Tables1Rows4 = Tables1Rows4 + decimal.Parse(ds1.Tables[1].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[3]["Earning"].ToString() + "</td>");

                        Tables1Rows5 = Tables1Rows5 + decimal.Parse(ds1.Tables[1].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[4]["Earning"].ToString() + "</td>");

                        Tables1Rows6 = Tables1Rows6 + decimal.Parse(ds1.Tables[1].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[5]["Earning"].ToString() + "</td>");

                        Tables1Rows7 = Tables1Rows7 + decimal.Parse(ds1.Tables[1].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[6]["Earning"].ToString() + "</td>");

                        Tables1Rows8 = Tables1Rows8 + decimal.Parse(ds1.Tables[1].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[7]["Earning"].ToString() + "</td>");

                        Tables1Rows9 = Tables1Rows9 + decimal.Parse(ds1.Tables[1].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[8]["Earning"].ToString() + "</td>");

                        htmlStr.Append("</tr>");



                        // Row 2
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");

                        Tables1Rows10 = Tables1Rows10 + decimal.Parse(ds1.Tables[1].Rows[9]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[9]["Earning"].ToString() + "</td>");

                        Tables1Rows11 = Tables1Rows11 + decimal.Parse(ds1.Tables[1].Rows[10]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[10]["Earning"].ToString() + "</td>");


                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows1 = Tables2Rows1 + decimal.Parse(ds1.Tables[2].Rows[0]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[0]["Earning"].ToString() + "</td>");

                        Tables2Rows2 = Tables2Rows2 + decimal.Parse(ds1.Tables[2].Rows[1]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[1]["Earning"].ToString() + "</td>");

                        Tables2Rows3 = Tables2Rows3 + decimal.Parse(ds1.Tables[2].Rows[2]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[2]["Earning"].ToString() + "</td>");

                        Tables2Rows4 = Tables2Rows4 + decimal.Parse(ds1.Tables[2].Rows[3]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[3]["Earning"].ToString() + "</td>");

                        Tables2Rows5 = Tables2Rows5 + decimal.Parse(ds1.Tables[2].Rows[4]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[4]["Earning"].ToString() + "</td>");
                        htmlStr.Append("</tr>");


                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows6 = Tables2Rows6 + decimal.Parse(ds1.Tables[2].Rows[5]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[5]["Earning"].ToString() + "</td>");

                        Tables2Rows7 = Tables2Rows7 + decimal.Parse(ds1.Tables[2].Rows[6]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[6]["Earning"].ToString() + "</td>");

                        Tables2Rows8 = Tables2Rows8 + decimal.Parse(ds1.Tables[2].Rows[7]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[7]["Earning"].ToString() + "</td>");

                        Tables2Rows9 = Tables2Rows9 + decimal.Parse(ds1.Tables[2].Rows[8]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[8]["Earning"].ToString() + "</td>");

                        Tables2Rows10 = Tables2Rows10 + decimal.Parse(ds1.Tables[2].Rows[9]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[9]["Earning"].ToString() + "</td>");

                        Tables2Rows11 = Tables2Rows11 + decimal.Parse(ds1.Tables[2].Rows[10]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[10]["Earning"].ToString() + "</td>");
                        Tables2Rows12 = Tables2Rows12 + decimal.Parse(ds1.Tables[2].Rows[11]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[11]["Earning"].ToString() + "</td>");

                        Tables2Rows13 = Tables2Rows13 + decimal.Parse(ds1.Tables[2].Rows[12]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[12]["Earning"].ToString() + "</td>");

                        Tables2Rows14 = Tables2Rows14 + decimal.Parse(ds1.Tables[2].Rows[13]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[13]["Earning"].ToString() + "</td>");

                        Tables2Rows15 = Tables2Rows15 + decimal.Parse(ds1.Tables[2].Rows[14]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[14]["Earning"].ToString() + "</td>");
                        htmlStr.Append("</tr>");


                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        Tables2Rows16 = Tables2Rows16 + decimal.Parse(ds1.Tables[2].Rows[15]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[15]["Earning"].ToString() + "</td>");

                        Tables2Rows17 = Tables2Rows17 + decimal.Parse(ds1.Tables[2].Rows[16]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[16]["Earning"].ToString() + "</td>");

                        Tables2Rows18 = Tables2Rows18 + decimal.Parse(ds1.Tables[2].Rows[17]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[17]["Earning"].ToString() + "</td>");

                        Tables2Rows19 = Tables2Rows19 + decimal.Parse(ds1.Tables[2].Rows[18]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[18]["Earning"].ToString() + "</td>");
                        Tables2Rows20 = Tables2Rows20 + decimal.Parse(ds1.Tables[2].Rows[19]["Earning"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[19]["Earning"].ToString() + "</td>");

                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");

                        Tables0Rows0EarningTotal = Tables0Rows0EarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</td>");

                        Tables2Rows13DeductionTotal = Tables2Rows13DeductionTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</td>");

                        Tables0Rows0Salary_NetSalary = Tables0Rows0Salary_NetSalary + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString());
                        htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");

                        htmlStr.Append("</tr>");
                        htmlStr.Append("</table>");
                        htmlStr.Append("</tr>");


                    }
                }
                if (Islast_heading == "Yes")
                {
                    /**********************************************************************/
                    //htmlStr.Append(repeadtCodePrintPage);

                    htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                    htmlStr.Append("<thead  style='border-bottom: 0px solid white; display: table-header-group;' class='printmaindata'>");
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
                    htmlStr.Append("<th colspan='2' class='text-left'>Name Of Employee</th>");
                    htmlStr.Append("<th colspan=''>Designation</th>");
                    htmlStr.Append("<th colspan=''>Pr. Day</th>");
                    htmlStr.Append("<th colspan=''>IFSC Code</th>");
                    htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                    htmlStr.Append("<th>EPF No./GPF No.</th>");
                    //htmlStr.Append("<th>GI No.</th>");
                    htmlStr.Append("</tr>");

                    /*****A Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>A</th>");
                    htmlStr.Append("<th class='empearning'>Basic Pay</th>");
                    htmlStr.Append("<th class='empearning'>C.C.A.</th>");
                    htmlStr.Append("<th class='empearning'>CONVEYANCE ALLOWANCE</th>");
                    htmlStr.Append("<th class='empearning'>DA-2</th>");
                    htmlStr.Append("<th class='empearning'>DEARNESS ALLOWANCE</th>");
                    htmlStr.Append("<th class='empearning'>GRADE PAY</th>");
                    htmlStr.Append("<th class='empearning'>HOUSE RENT ALLOWANCE</th>");
                    htmlStr.Append("<th class='empearning'>INTERIM RELIEF</th>");
                    htmlStr.Append("<th class='empearning'>ORDERLY ALLOWANCE</th>");
                    htmlStr.Append("<th class='empearning'>OTHER ALLOWANCE</th>");
                    htmlStr.Append("</tr>");
                    /*****B Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>B</th>");
                    htmlStr.Append("<th class='empearning'>SPL PAY</th>");
                    htmlStr.Append("<th class='empearning'>WASHING ALLOWANCE</th>");
                    htmlStr.Append("<th class='empearning'></th>");
                    htmlStr.Append("<th class='empearning'></th>");
                    htmlStr.Append("<th class='empearning'></th>");
                    htmlStr.Append("<th>A.D.A. DEDUCTION</th>");
                    htmlStr.Append("<th>ARR EPF</th>");
                    htmlStr.Append("<th>CELL DED</th>");
                    htmlStr.Append("<th>CR SOCIETY</th>");
                    htmlStr.Append("<th>CREDIT SOC. II</th>");
                    htmlStr.Append("</tr>");
                    /*****C Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>C</th>");
                    htmlStr.Append("<th class='empdeduction'>EPF DEDUCTION</th>");
                    htmlStr.Append("<th class='empdeduction'>ESI</th>");
                    htmlStr.Append("<th class='empdeduction'>GHEE DED</th>");
                    htmlStr.Append("<th class='empdeduction'>H.R.R.</th>");
                    htmlStr.Append("<th class='empdeduction'>INCOME TAX</th>");
                    htmlStr.Append("<th class='empdeduction'>MLK ADV</th>");
                    htmlStr.Append("<th class='empdeduction'>OTHER ADVANCE</th>");
                    htmlStr.Append("<th class='empdeduction'>PAY ADVANCE</th>");
                    htmlStr.Append("<th class='empdeduction'>PLOT</th>");
                    htmlStr.Append("<th class='empdeduction'>PROFESSIONAL TAX</th>");
                    htmlStr.Append("</tr>");
                    /*****D Row*******/
                    htmlStr.Append("<tr class='tblheadingslip'>");
                    htmlStr.Append("<th>D</th>");
                    htmlStr.Append("<th class='empdeduction'>RD AC</th>");
                    htmlStr.Append("<th class='empdeduction'>SBI HAZ</th>");
                    htmlStr.Append("<th class='empdeduction'>SCOOTER-BCCB</th>");
                    htmlStr.Append("<th class='empdeduction'>VEHICLE</th>");
                    htmlStr.Append("<th class='empdeduction'>YKK</th>");
                    htmlStr.Append("<th class='empdeduction'></th>");
                    htmlStr.Append("<th class='empdeduction'></th>");
                    htmlStr.Append("<th class='empearning'>Gross</th>");
                    htmlStr.Append("<th class='empdeduction'>Tot. Ded</th>");
                    htmlStr.Append("<th class='empbalance'>Net Amount</th>");
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
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0Basic.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows5.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows9.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****B Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables1Rows11.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows1.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows2.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows3.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows4.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows5.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****C Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows6.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows7.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows8.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows9.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows10.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows12.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows13.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows14.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows15.ToString() + "</td>");
                htmlStr.Append("</tr>");

                /*****D Row*******/
                htmlStr.Append("<tr>");
                htmlStr.Append("<td class='alignright'></td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows16.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows17.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows18.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows19.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows20.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'> </td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0EarningTotal.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables2Rows13DeductionTotal.ToString() + "</td>");
                htmlStr.Append("<td class='alignright'>" + Tables0Rows0Salary_NetSalary.ToString() + "</td>");
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
    /*************Dynamic Paysheet**************/
    protected void FillSalary_Dynamic()
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
            int k = 0;
            ds_dynamic = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "32", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds_dynamic.Tables[0].Rows.Count != 0)
            {
                int CountSection = ds_dynamic.Tables[0].Rows.Count;
                /**********/
                for (int p = 0; p < CountSection; p++)
                {


                    ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost", "SalarySectionNo" }, new string[] { "33", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString(), ds_dynamic.Tables[0].Rows[p]["SecNo"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        int Count = ds.Tables[0].Rows.Count;

                        htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                        htmlStr.Append("<thead  style='border-bottom: 0px solid white; display: table-header-group;' class='printmaindata'>");
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
                        htmlStr.Append("<th colspan='2' class='text-left'>Name Of Employee</th>");
                        htmlStr.Append("<th colspan=''>Designation</th>");
                        htmlStr.Append("<th colspan=''>Pr. Day</th>");
                        htmlStr.Append("<th colspan=''>IFSC Code</th>");
                        htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                        htmlStr.Append("<th>EPF No./GPF No.</th>");
                        //htmlStr.Append("<th>GI No.</th>");
                        htmlStr.Append("</tr>");

                        /*****A Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>A</th>");
                        htmlStr.Append("<th class='empearning'>Basic Pay</th>");
                        htmlStr.Append("<th class='empearning'>D.A.</th>");
                        htmlStr.Append("<th class='empearning'>H.R.A.</th>");
                        htmlStr.Append("<th class='empearning'>C.C.A.</th>");
                        htmlStr.Append("<th class='empearning'>Cnv All</th>");
                        htmlStr.Append("<th class='empearning'>Spl Pay</th>");
                        htmlStr.Append("<th class='empearning'>Wsh All</th>");
                        htmlStr.Append("<th class='empearning'>Oth All</th>");
                        htmlStr.Append("<th class='empearning'>Ord All</th>");
                        htmlStr.Append("<th class='empearning'>Ofc All</th>");
                        htmlStr.Append("</tr>");
                        /*****B Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>B</th>");
                        htmlStr.Append("<th class='empearning'>Drs.pay</th>");
                        htmlStr.Append("<th class='empearning'>I.Relf</th>");
                        htmlStr.Append("<th class='empearning'>Att.Bns</th>");
                        htmlStr.Append("<th class='empearning'>Med.Aid</th>");
                        htmlStr.Append("<th class='empearning'>DA-2</th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("</tr>");
                        /*****C Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>C</th>");
                        htmlStr.Append("<th class='empdeduction'>E.P.F.</th>");
                        htmlStr.Append("<th class='empdeduction'>G.P.F.</th>");
                        htmlStr.Append("<th class='empdeduction'>F.B.F</th>");
                        htmlStr.Append("<th class='empdeduction'>G.I.S.</th>");
                        htmlStr.Append("<th class='empdeduction'>P.Tax</th>");
                        htmlStr.Append("<th class='empdeduction'>Security</th>");
                        htmlStr.Append("<th class='empdeduction'>I.Tax</th>");
                        htmlStr.Append("<th class='empdeduction'>Veh.Reg</th>");
                        htmlStr.Append("<th class='empdeduction'>H.R.R.</th>");
                        htmlStr.Append("<th class='empdeduction'>A.D.A.</th>");
                        htmlStr.Append("</tr>");
                        /*****D Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>D</th>");
                        htmlStr.Append("<th class='empdeduction'>Grain</th>");
                        htmlStr.Append("<th class='empdeduction'>Festival</th>");
                        htmlStr.Append("<th class='empdeduction'>SCT.MPD</th>");
                        htmlStr.Append("<th class='empdeduction'>Spl.Ins</th>");
                        htmlStr.Append("<th class='empdeduction'>Pay.Ins</th>");
                        htmlStr.Append("<th class='empdeduction'>HBL.Ins</th>");
                        htmlStr.Append("<th class='empdeduction'>Car.BCB</th>");
                        htmlStr.Append("<th class='empdeduction'>Tur.Ins</th>");
                        htmlStr.Append("<th class='empdeduction'>Cyc.Adv</th>");
                        htmlStr.Append("<th class='empdeduction'>LTC.Adv</th>");
                        htmlStr.Append("</tr>");
                        /*****E Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>E</th>");
                        htmlStr.Append("<th class='empbalance'>Gr.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Fst.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>SCT.Bl</th>");
                        htmlStr.Append("<th class='empbalance'Spl.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Pay.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>HBL.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Car.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Tur.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Cyc.Bl</th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("</tr>");
                        /*****F Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>F</th>");
                        htmlStr.Append("<th class='empdeduction'>Lubr.</th>");
                        htmlStr.Append("<th class='empdeduction'>Misc.Rc</th>");
                        htmlStr.Append("<th class='empdeduction'>Car-I</th>");
                        htmlStr.Append("<th class='empdeduction'>Cr.Soc.</th>");
                        htmlStr.Append("<th class='empdeduction'>RD</th>");
                        htmlStr.Append("<th class='empdeduction'>Un Rec.</th>");
                        htmlStr.Append("<th class='empdeduction'>SCT.BCB</th>");
                        htmlStr.Append("<th class='empdeduction'>CR-Schm</th>");
                        htmlStr.Append("<th class='empdeduction'>CR.Soc.</th>");
                        htmlStr.Append("<th class='empdeduction'>Un.Soc.</th>");
                        htmlStr.Append("</tr>");
                        /*****G Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>G</th>");
                        htmlStr.Append("<th class='empbalance'>Lubr.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Misc.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Car-I.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Cr.Soc.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>RD.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Un.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>BCB.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>CR-Schm.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>CR.Soc.Bl</th>");
                        htmlStr.Append("<th class='empbalance'>Un.Soc.Bl</th>");
                        htmlStr.Append("</tr>");
                        /*****H Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>H</th>");
                        htmlStr.Append("<th class='empdeduction'>MPD Soc</th>");
                        htmlStr.Append("<th class='empdeduction'>Plot</th>");
                        htmlStr.Append("<th class='empdeduction'>Car-II</th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("</tr>");
                        /*****I Row*******/
                        htmlStr.Append("<tr class='tblheadingslip'>");
                        htmlStr.Append("<th>I</th>");
                        htmlStr.Append("<th class='empbalance'>MPD Bl.</th>");
                        htmlStr.Append("<th class='empbalance'>Plot Bl.</th>");
                        htmlStr.Append("<th class='empbalance'>Car2 Bl.</th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th></th>");
                        htmlStr.Append("<th class='empearning'>Gross</th>");
                        htmlStr.Append("<th class='empdeduction'>Tot. Ded</th>");
                        htmlStr.Append("<th class='empbalance'>Net Amount</th>");
                        htmlStr.Append("</tr>");
                        htmlStr.Append("</thead>");
                        htmlStr.Append("<tbody>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<table>");

                        string repeadtCodePrintPage = htmlStr.ToString();

                        // Row 1-A
                        decimal Tables0Rows0Basic = 0;
                        decimal Tables1Rows1 = 0;
                        decimal Tables1Rows2 = 0;
                        decimal Tables1Rows3 = 0;
                        decimal Tables1Rows4 = 0;
                        decimal Tables1Rows5 = 0;
                        decimal Tables1Rows6 = 0;
                        decimal Tables1Rows7 = 0;
                        decimal Tables1Rows8 = 0;
                        decimal Tables1Rows9 = 0;


                        // Row 2-B
                        decimal Tables1Rows10 = 0;
                        decimal Tables1Rows11 = 0;
                        decimal Tables1Rows12 = 0;
                        decimal Tables1Rows13 = 0;
                        decimal Tables1Rows14 = 0;

                        // Row 3-C
                        decimal Tables2Rows1 = 0;
                        decimal Tables2Rows2 = 0;
                        decimal Tables2Rows3 = 0;
                        decimal Tables2Rows4 = 0;
                        decimal Tables2Rows5 = 0;
                        decimal Tables2Rows6 = 0;
                        decimal Tables2Rows7 = 0;
                        decimal Tables2Rows8 = 0;
                        decimal Tables2Rows9 = 0;

                        // Row 4-D
                        decimal Tables2Rows10 = 0;
                        decimal Tables2Rows11 = 0;
                        decimal Tables2Rows12 = 0;
                        decimal Tables2Rows13 = 0;
                        decimal Tables2Rows14 = 0;
                        decimal Tables2Rows15 = 0;
                        decimal Tables2Rows16 = 0;
                        decimal Tables2Rows17 = 0;
                        decimal Tables2Rows18 = 0;
                        decimal Tables2Rows19 = 0;

                        // Row 5-E
                        //float Tables3Rows1 = 0;
                        //float Tables3Rows2 = 0;
                        //float Tables3Rows3 = 0;
                        //float Tables3Rows4 = 0;
                        //float Tables3Rows5 = 0;
                        //float Tables3Rows6 = 0;
                        //float Tables3Rows7 = 0;
                        //float Tables3Rows8 = 0;
                        //float Tables3Rows9 = 0;

                        // Row 6-F
                        decimal Tables2Rows20 = 0;
                        decimal Tables2Rows21 = 0;
                        decimal Tables2Rows22 = 0;
                        decimal Tables2Rows23 = 0;
                        decimal Tables2Rows24 = 0;
                        decimal Tables2Rows25 = 0;
                        decimal Tables2Rows26 = 0;
                        decimal Tables2Rows27 = 0;
                        decimal Tables2Rows28 = 0;
                        decimal Tables2Rows29 = 0;

                        // Row 7-G


                        // Row 8-H   
                        decimal Tables2Rows30 = 0;
                        decimal Tables2Rows31 = 0;
                        decimal Tables2Rows32 = 0;


                        // Row 9-I
                        decimal Tables0Rows0EarningTotal = 0;
                        decimal Tables2Rows13DeductionTotal = 0;
                        decimal Tables0Rows0Salary_NetSalary = 0;





                        string Islast_heading = "";
                        for (int i = 0; i < Count; i++)
                        {

                            string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();

                            DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "11", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
                            if (ds1.Tables[0].Rows.Count != 0)
                            {
                                // Row 0
                                string pagebreak = "";
                                if (i == 14 || i == 30 || i == 46 || i == 62)
                                {
                                    pagebreak = ".page-break";
                                    Islast_heading = "Yes";
                                    //htmlStr.Append(repeadtCodePrintPage);
                                }

                                k++;
                                htmlStr.Append("<tr class='" + pagebreak + "'>");
                                htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class='" + pagebreak + "'>");
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<th class='text-left'>" + (k).ToString() + "</th>");
                                htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["SecNo"].ToString() + "</th>");
                                htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["EmpNo"].ToString() + "</th>");
                                htmlStr.Append("<th colspan='2' class='text-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                                htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                                htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["PrDay"].ToString() + "</th>");
                                htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</th>");
                                htmlStr.Append("<th colspan='2'>'" + ds.Tables[0].Rows[i]["SalaryAccount"].ToString() + "'</th>");
                                htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["EPF_No"].ToString() + "</th>");
                                htmlStr.Append("</tr>");

                                // Row 1
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'> </td>");

                                Tables0Rows0Basic = Tables0Rows0Basic + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_Basic"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");

                                Tables1Rows1 = Tables1Rows1 + decimal.Parse(ds1.Tables[1].Rows[0]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[0]["Earning"].ToString() + "</td>");

                                Tables1Rows2 = Tables1Rows2 + decimal.Parse(ds1.Tables[1].Rows[1]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[1]["Earning"].ToString() + "</td>");

                                Tables1Rows3 = Tables1Rows3 + decimal.Parse(ds1.Tables[1].Rows[2]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[2]["Earning"].ToString() + " </td>");

                                htmlStr.Append("<td class='alignright'></td>");

                                Tables1Rows4 = Tables1Rows4 + decimal.Parse(ds1.Tables[1].Rows[3]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[3]["Earning"].ToString() + "</td>");

                                Tables1Rows5 = Tables1Rows5 + decimal.Parse(ds1.Tables[1].Rows[4]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[4]["Earning"].ToString() + "</td>");

                                Tables1Rows6 = Tables1Rows6 + decimal.Parse(ds1.Tables[1].Rows[5]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[5]["Earning"].ToString() + "</td>");

                                Tables1Rows7 = Tables1Rows7 + decimal.Parse(ds1.Tables[1].Rows[6]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[6]["Earning"].ToString() + "</td>");

                                Tables1Rows8 = Tables1Rows8 + decimal.Parse(ds1.Tables[1].Rows[7]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[7]["Earning"].ToString() + "</td>");

                                htmlStr.Append("</tr>");



                                // Row 2
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                Tables1Rows9 = Tables1Rows9 + decimal.Parse(ds1.Tables[1].Rows[8]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[8]["Earning"].ToString() + "</td>");
                                Tables1Rows10 = Tables1Rows10 + decimal.Parse(ds1.Tables[1].Rows[9]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[9]["Earning"].ToString() + "</td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                Tables1Rows11 = Tables1Rows11 + decimal.Parse(ds1.Tables[1].Rows[10]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[1].Rows[10]["Earning"].ToString() + "</td>");

                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("</tr>");



                                // Row 3

                                //float Tables2Rows1 = 0;
                                //float Tables2Rows2 = 0;
                                //float Tables2Rows3 = 0;
                                //float Tables2Rows4 = 0;
                                //float Tables2Rows5 = 0;
                                //float Tables2Rows6 = 0;
                                //float Tables2Rows7 = 0;
                                //float Tables2Rows8 = 0;
                                //float Tables2Rows9 = 0;


                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                Tables2Rows1 = Tables2Rows1 + decimal.Parse(ds1.Tables[2].Rows[0]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[0]["Earning"].ToString() + "</td>");

                                htmlStr.Append("<td class='alignright'> </td>");

                                Tables2Rows2 = Tables2Rows2 + decimal.Parse(ds1.Tables[2].Rows[1]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[1]["Earning"].ToString() + "</td>");
                                Tables2Rows3 = Tables2Rows3 + decimal.Parse(ds1.Tables[2].Rows[2]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[2]["Earning"].ToString() + "</td>");

                                Tables2Rows4 = Tables2Rows4 + decimal.Parse(ds1.Tables[2].Rows[3]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[3]["Earning"].ToString() + "</td>");

                                Tables2Rows5 = Tables2Rows5 + decimal.Parse(ds1.Tables[2].Rows[4]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[4]["Earning"].ToString() + "</td>");
                                Tables2Rows6 = Tables2Rows6 + decimal.Parse(ds1.Tables[2].Rows[5]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[5]["Earning"].ToString() + "</td>");

                                Tables2Rows7 = Tables2Rows7 + decimal.Parse(ds1.Tables[2].Rows[6]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[6]["Earning"].ToString() + "</td>");

                                Tables2Rows8 = Tables2Rows8 + decimal.Parse(ds1.Tables[2].Rows[7]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[7]["Earning"].ToString() + "</td>");
                                Tables2Rows9 = Tables2Rows9 + decimal.Parse(ds1.Tables[2].Rows[8]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[8]["Earning"].ToString() + "</td>");
                                htmlStr.Append("</tr>");


                                // Row 4
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                Tables2Rows10 = Tables2Rows10 + decimal.Parse(ds1.Tables[2].Rows[9]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[9]["Earning"].ToString() + "</td>");

                                Tables2Rows11 = Tables2Rows11 + decimal.Parse(ds1.Tables[2].Rows[10]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[10]["Earning"].ToString() + "</td>");
                                Tables2Rows12 = Tables2Rows12 + decimal.Parse(ds1.Tables[2].Rows[11]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[11]["Earning"].ToString() + "</td>");

                                Tables2Rows13 = Tables2Rows13 + decimal.Parse(ds1.Tables[2].Rows[12]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[12]["Earning"].ToString() + "</td>");
                                Tables2Rows14 = Tables2Rows14 + decimal.Parse(ds1.Tables[2].Rows[13]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[13]["Earning"].ToString() + "</td>");

                                Tables2Rows15 = Tables2Rows15 + decimal.Parse(ds1.Tables[2].Rows[14]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[14]["Earning"].ToString() + "</td>");

                                Tables2Rows16 = Tables2Rows16 + decimal.Parse(ds1.Tables[2].Rows[15]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[15]["Earning"].ToString() + "</td>");

                                Tables2Rows17 = Tables2Rows17 + decimal.Parse(ds1.Tables[2].Rows[16]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[16]["Earning"].ToString() + "</td>");

                                Tables2Rows18 = Tables2Rows18 + decimal.Parse(ds1.Tables[2].Rows[17]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[17]["Earning"].ToString() + "</td>");

                                Tables2Rows19 = Tables2Rows19 + decimal.Parse(ds1.Tables[2].Rows[18]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[18]["Earning"].ToString() + "</td>");

                                htmlStr.Append("</tr>");


                                // Row 5
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("</tr>");

                                // Row 6
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                Tables2Rows20 = Tables2Rows20 + decimal.Parse(ds1.Tables[2].Rows[19]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[19]["Earning"].ToString() + "</td>");

                                Tables2Rows21 = Tables2Rows21 + decimal.Parse(ds1.Tables[2].Rows[20]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[20]["Earning"].ToString() + "</td>");

                                Tables2Rows22 = Tables2Rows22 + decimal.Parse(ds1.Tables[2].Rows[21]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[21]["Earning"].ToString() + "</td>");
                                Tables2Rows23 = Tables2Rows23 + decimal.Parse(ds1.Tables[2].Rows[22]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[22]["Earning"].ToString() + "</td>");

                                Tables2Rows24 = Tables2Rows24 + decimal.Parse(ds1.Tables[2].Rows[23]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[23]["Earning"].ToString() + "</td>");
                                Tables2Rows25 = Tables2Rows25 + decimal.Parse(ds1.Tables[2].Rows[24]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[24]["Earning"].ToString() + "</td>");
                                Tables2Rows26 = Tables2Rows26 + decimal.Parse(ds1.Tables[2].Rows[25]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[25]["Earning"].ToString() + "</td>");
                                Tables2Rows27 = Tables2Rows27 + decimal.Parse(ds1.Tables[2].Rows[26]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[26]["Earning"].ToString() + "</td>");
                                Tables2Rows28 = Tables2Rows28 + decimal.Parse(ds1.Tables[2].Rows[27]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[27]["Earning"].ToString() + "</td>");
                                Tables2Rows29 = Tables2Rows29 + decimal.Parse(ds1.Tables[2].Rows[28]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[28]["Earning"].ToString() + "</td>");

                                htmlStr.Append("</tr>");

                                //float Tables2Rows20 = 0;
                                //float Tables2Rows21 = 0;
                                //float Tables2Rows22 = 0;
                                //float Tables2Rows23 = 0;
                                //float Tables2Rows24 = 0;
                                //float Tables2Rows25 = 0;
                                //float Tables2Rows26 = 0;
                                //float Tables2Rows27 = 0;
                                //float Tables2Rows28 = 0;
                                //float Tables2Rows29 = 0;

                                // Row 7
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("</tr>");

                                // Row 8
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                Tables2Rows30 = Tables2Rows30 + decimal.Parse(ds1.Tables[2].Rows[29]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[29]["Earning"].ToString() + "</td>");
                                Tables2Rows31 = Tables2Rows31 + decimal.Parse(ds1.Tables[2].Rows[30]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[30]["Earning"].ToString() + "</td>");
                                Tables2Rows32 = Tables2Rows32 + decimal.Parse(ds1.Tables[2].Rows[31]["Earning"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[2].Rows[31]["Earning"].ToString() + "</td>");
                                //htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("</tr>");



                                //// Row 9-I
                                //float Tables0Rows0EarningTotal = 0;
                                //float Tables2Rows13DeductionTotal = 0;
                                //float Tables0Rows0Salary_NetSalary = 0;


                                // Row 9
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td class='alignright'></td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");
                                htmlStr.Append("<td class='alignright'> </td>");

                                Tables0Rows0EarningTotal = Tables0Rows0EarningTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</td>");

                                Tables2Rows13DeductionTotal = Tables2Rows13DeductionTotal + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</td>");

                                Tables0Rows0Salary_NetSalary = Tables0Rows0Salary_NetSalary + decimal.Parse(ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString());
                                htmlStr.Append("<td class='alignright'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");

                                htmlStr.Append("</tr>");
                                htmlStr.Append("</table>");
                                htmlStr.Append("</tr>");


                            }
                        }
                        if (Islast_heading == "Yes")
                        {
                            /**********************************************************************/
                            //htmlStr.Append(repeadtCodePrintPage);

                            htmlStr.Append("<table class='main-heading-print' style='margin-bottom:5px; font-family: monospace; line-height: 14px;' id='SalaryTable'>");
                            htmlStr.Append("<thead  style='border-bottom: 0px solid white;'>");
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th class='text-left'>SNo.</th><th class='text-left'>Sec.</th><th class='text-left'>Emp No.</th>");
                            htmlStr.Append("<th colspan='2' class='text-left'>Name Of Employee</th>");
                            htmlStr.Append("<th colspan=''>Designation</th>");
                            htmlStr.Append("<th colspan=''>Pr. Day</th>");
                            htmlStr.Append("<th colspan=''>IFSC Code</th>");
                            htmlStr.Append("<th colspan='2'>Bank Account No.</th>");
                            htmlStr.Append("<th>EPF No./GPF No.</th>");
                            //htmlStr.Append("<th>GI No.</th>");
                            htmlStr.Append("</tr>");

                            /*****A Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>A</th>");
                            htmlStr.Append("<th class='empearning'>Basic Pay</th>");
                            htmlStr.Append("<th class='empearning'>D.A.</th>");
                            htmlStr.Append("<th class='empearning'>H.R.A.</th>");
                            htmlStr.Append("<th class='empearning'>C.C.A.</th>");
                            htmlStr.Append("<th class='empearning'>Cnv All</th>");
                            htmlStr.Append("<th class='empearning'>Spl Pay</th>");
                            htmlStr.Append("<th class='empearning'>Wsh All</th>");
                            htmlStr.Append("<th class='empearning'>Oth All</th>");
                            htmlStr.Append("<th class='empearning'>Ord All</th>");
                            htmlStr.Append("<th class='empearning'>Ofc All</th>");
                            htmlStr.Append("</tr>");
                            /*****B Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>B</th>");
                            htmlStr.Append("<th class='empearning'>Drs.pay</th>");
                            htmlStr.Append("<th class='empearning'>I.Relf</th>");
                            htmlStr.Append("<th class='empearning'>Att.Bns</th>");
                            htmlStr.Append("<th class='empearning'>Med.Aid</th>");
                            htmlStr.Append("<th class='empearning'>DA-2</th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("</tr>");
                            /*****C Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>C</th>");
                            htmlStr.Append("<th class='empdeduction'>E.P.F.</th>");
                            htmlStr.Append("<th class='empdeduction'>G.P.F.</th>");
                            htmlStr.Append("<th class='empdeduction'>F.B.F</th>");
                            htmlStr.Append("<th class='empdeduction'>G.I.S.</th>");
                            htmlStr.Append("<th class='empdeduction'>P.Tax</th>");
                            htmlStr.Append("<th class='empdeduction'>Security</th>");
                            htmlStr.Append("<th class='empdeduction'>I.Tax</th>");
                            htmlStr.Append("<th class='empdeduction'>Veh.Reg</th>");
                            htmlStr.Append("<th class='empdeduction'>H.R.R.</th>");
                            htmlStr.Append("<th class='empdeduction'>A.D.A.</th>");
                            htmlStr.Append("</tr>");
                            /*****D Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>D</th>");
                            htmlStr.Append("<th class='empdeduction'>Grain</th>");
                            htmlStr.Append("<th class='empdeduction'>Festival</th>");
                            htmlStr.Append("<th class='empdeduction'>SCT.MPD</th>");
                            htmlStr.Append("<th class='empdeduction'>Spl.Ins</th>");
                            htmlStr.Append("<th class='empdeduction'>Pay.Ins</th>");
                            htmlStr.Append("<th class='empdeduction'>HBL.Ins</th>");
                            htmlStr.Append("<th class='empdeduction'>Car.BCB</th>");
                            htmlStr.Append("<th class='empdeduction'>Tur.Ins</th>");
                            htmlStr.Append("<th class='empdeduction'>Cyc.Adv</th>");
                            htmlStr.Append("<th class='empdeduction'>LTC.Adv</th>");
                            htmlStr.Append("</tr>");
                            /*****E Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>E</th>");
                            htmlStr.Append("<th class='empbalance'>Gr.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Fst.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>SCT.Bl</th>");
                            htmlStr.Append("<th class='empbalance'Spl.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Pay.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>HBL.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Car.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Tur.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Cyc.Bl</th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("</tr>");
                            /*****F Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>F</th>");
                            htmlStr.Append("<th class='empdeduction'>Lubr.</th>");
                            htmlStr.Append("<th class='empdeduction'>Misc.Rc</th>");
                            htmlStr.Append("<th class='empdeduction'>Car-I</th>");
                            htmlStr.Append("<th class='empdeduction'>Cr.Soc.</th>");
                            htmlStr.Append("<th class='empdeduction'>RD</th>");
                            htmlStr.Append("<th class='empdeduction'>Un Rec.</th>");
                            htmlStr.Append("<th class='empdeduction'>SCT.BCB</th>");
                            htmlStr.Append("<th class='empdeduction'>CR-Schm</th>");
                            htmlStr.Append("<th class='empdeduction'>CR.Soc.</th>");
                            htmlStr.Append("<th class='empdeduction'>Un.Soc.</th>");
                            htmlStr.Append("</tr>");
                            /*****G Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>G</th>");
                            htmlStr.Append("<th class='empbalance'>Lubr.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Misc.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Car-I.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Cr.Soc.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>RD.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Un.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>BCB.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>CR-Schm.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>CR.Soc.Bl</th>");
                            htmlStr.Append("<th class='empbalance'>Un.Soc.Bl</th>");
                            htmlStr.Append("</tr>");
                            /*****H Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>H</th>");
                            htmlStr.Append("<th class='empdeduction'>MPD Soc</th>");
                            htmlStr.Append("<th class='empdeduction'>Plot</th>");
                            htmlStr.Append("<th class='empdeduction'>Car-II</th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("</tr>");
                            /*****I Row*******/
                            htmlStr.Append("<tr class='tblheadingslip'>");
                            htmlStr.Append("<th>I</th>");
                            htmlStr.Append("<th class='empbalance'>MPD Bl.</th>");
                            htmlStr.Append("<th class='empbalance'>Plot Bl.</th>");
                            htmlStr.Append("<th class='empbalance'>Car2 Bl.</th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th></th>");
                            htmlStr.Append("<th class='empearning'>Gross</th>");
                            htmlStr.Append("<th class='empdeduction'>Tot. Ded</th>");
                            htmlStr.Append("<th class='empbalance'>Net Amount</th>");
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
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>" + Tables0Rows0Basic.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows1.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows2.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows3.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>  </td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows4.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows5.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows6.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows7.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows8.ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        /*****B Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows9.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables1Rows10.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");

                        /*****C Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows1.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows2.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows3.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows4.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows5.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows6.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows7.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows8.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows9.ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        /*****D Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows10.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows11.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows12.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows13.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows14.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows15.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows16.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows17.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows18.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows19.ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        /*****E Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");

                        /*****F Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows20.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows21.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows22.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows23.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows24.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows25.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows26.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows27.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows28.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows29.ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        /*****G Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>&nbsp;</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");

                        /*****H Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows30.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows31.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("</tr>");

                        /*****I Row*******/
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='alignright'></td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'> </td>");
                        htmlStr.Append("<td class='alignright'>" + Tables0Rows0EarningTotal.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables2Rows13DeductionTotal.ToString() + "</td>");
                        htmlStr.Append("<td class='alignright'>" + Tables0Rows0Salary_NetSalary.ToString() + "</td>");
                        htmlStr.Append("</tr>");

                        htmlStr.Append("</table>");
                        htmlStr.Append("</tr>");
                        htmlStr.Append("</tbody>");
                        htmlStr.Append("</table>");

                        DivDetail.InnerHtml = htmlStr.ToString();
                    }
                }
                /*****/
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalary_Dynamic2()
    {
        try
        {
            DivDetail.InnerHtml = "";
            string Office_ID = ddlOffice.SelectedValue.ToString();
            string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
            string Salary_MonthNo = ddlMonth.SelectedValue.ToString();
            string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

            lblSession.Text = ddlMonth.SelectedItem.ToString() + " " + ddlFinancialYear.SelectedValue.ToString();



            DivHead.Visible = true;
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "10", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {


                int Count = ds.Tables[0].Rows.Count;
                int k = 0;

                StringBuilder htmlStr = new StringBuilder();
                /****************/
                String MainHeader = GetHeaders();
                htmlStr.Append(MainHeader.ToString());
                /*****************/
                string Islast_heading = "";
                for (int i = 0; i < Count; i++)
                {
                    k++;
                    string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                    String EmpData = "";
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<table  style='margin-top: 10px;background: beige;' class=''>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th class='text-left'>" + k.ToString() + "</th>");
                    htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["SecNo"].ToString() + "</th>");
                    htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["EmpNo"].ToString() + "</th>");
                    htmlStr.Append("<th colspan='2' class='text-left'>" + ds.Tables[0].Rows[i]["Emp_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Designation_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["PrDay"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</th>");
                    htmlStr.Append("<th colspan='2'>'" + ds.Tables[0].Rows[i]["SalaryAccount"].ToString() + "'</th>");
                    htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["EPF_No"].ToString() + "</th>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<th class='text-left'></th>");
                    htmlStr.Append("<th class='text-left'>" + ds.Tables[0].Rows[i]["UserName"].ToString() + "</th>");
                    htmlStr.Append("<th class='text-left'></th>");
                    htmlStr.Append("<th colspan='2' class='text-left'>" + ds.Tables[0].Rows[i]["Emp_PanCardNo"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["UAN_No"].ToString() + "</th>");

                    htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["PayScale_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan=''>" + ds.Tables[0].Rows[i]["GradePay_Name"].ToString() + "</th>");
                    htmlStr.Append("<th colspan='2'>'" + ds.Tables[0].Rows[i]["Level_Name"].ToString() + "'</th>");
                    htmlStr.Append("<th></th>");
                    htmlStr.Append("</tr>");

                    EmpData = GetEmpData(Emp_ID, Office_ID, Salary_Year, Salary_MonthNo);
                    htmlStr.Append(EmpData.ToString());

                }
                /******Footer**********/
                htmlStr.Append(MainHeader.ToString());
                String MainFooter = GetFooter();
                htmlStr.Append(MainFooter.ToString());

                /*****************/

                DivDetail.InnerHtml = htmlStr.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    /***************************/

    public string GetHeaders()
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
        ds_dynamic = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "MonthNo", "Emp_TypeOfPost" }, new string[] { "34", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
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
    private string GetEmpData(string Emp_ID, string Office_ID, string Salary_Year, string Salary_MonthNo)
    {
        string EmpData = "";
        string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

        DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "11", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {

            int ii = 0;
            int lineno = 0;
            ds_dynamic = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "MonthNo", "Emp_TypeOfPost", "Emp_ID" }, new string[] { "35", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString(), Emp_ID.ToString() }, "dataset");
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
}
