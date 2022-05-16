using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayRollPayBillMonth_Wise : System.Web.UI.Page
{
    DataSet ds;
    //AbstApiDBApi objdb = new APIProcedure();
	 APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                ds = objdb.ByProcedure("SpAdminOffice",
                       new string[] { "flag" },
                       new string[] { "23" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
					if(objdb.Office_ID()=="1")
                    {
                        ddlOffice.Enabled = true;
                    }
                    else
                    {
                        ddlOffice.Enabled = false;
                        ddlOffice.SelectedValue = objdb.Office_ID();
                    }
                    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));

                }
                //DivTextFileExport.Visible = false;
                btnTextFileExportNew.Visible = false;
                FillDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
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
    //protected void FillGrid()
    //{
    //    try
    //    {

    //        /*************ds.Tables[0] is changed To ds.Tables[0]***********/
    //        GridView1.DataSource = null;
    //        GridView1.DataBind();

    //        GridView2.DataSource = null;
    //        GridView2.DataBind();
    //        //DivTextFileExport.Visible = false;
    //        btnTextFileExportNew.Visible = false;
    //        hide_print_main.Visible = false;


    //        if (ddlOffice.SelectedValue.ToString() == "5")
    //        {
    //            GridView1.Visible = false;
    //            GridView2.Visible = true;
    //            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "21", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {

    //                GridView2.DataSource = ds.Tables[0];
    //                GridView2.DataBind();
    //                //DivTextFileExport.Visible = true;
    //                //btnTextFileExportNew.Visible = true;
    //                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                GridView2.UseAccessibleHeader = true;

    //                decimal Salary_EarningTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_EarningTotal"));
    //                decimal EPFWAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPFWAGES"));
    //                decimal EPS_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_WAGES"));
    //                decimal EDLI_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EDLI_WAGES"));
    //                decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
    //                decimal EPS_CONTRI_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_CONTRI_REMITTED"));
    //                decimal EPF_EPS_DIFF_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF_EPS_DIFF_REMITTED"));
    //                decimal NCP_DAYS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NCP_DAYS"));
    //                decimal REFUND_OF_ADVANCES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("REFUND_OF_ADVANCES"));


    //                GridView2.FooterRow.Cells[1].Text = "| TOTAL | ";
    //                GridView2.FooterRow.Cells[5].Text = Salary_EarningTotal.ToString();
    //                GridView2.FooterRow.Cells[6].Text = EPFWAGES.ToString();
    //                GridView2.FooterRow.Cells[7].Text = EPS_WAGES.ToString();
    //                GridView2.FooterRow.Cells[8].Text = EDLI_WAGES.ToString();
    //                GridView2.FooterRow.Cells[11].Text = EPF.ToString();
    //                GridView2.FooterRow.Cells[10].Text = EPS_CONTRI_REMITTED.ToString();
    //                GridView2.FooterRow.Cells[9].Text = EPF_EPS_DIFF_REMITTED.ToString();
    //                GridView2.FooterRow.Cells[12].Text = NCP_DAYS.ToString();
    //                GridView2.FooterRow.Cells[13].Text = REFUND_OF_ADVANCES.ToString();
    //            }
    //            else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
    //            {
    //                GridView2.DataSource = ds.Tables[0];
    //                GridView2.DataBind();
    //                btnTextFileExportNew.Visible = true;
    //                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                GridView2.UseAccessibleHeader = true;
    //            }
    //        }
    //        else
    //        {
    //            GridView2.Visible = false;
    //            GridView1.Visible = true;
    //            hide_print_main.Visible = true;
    //            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                GridView1.DataSource = ds.Tables[0];
    //                GridView1.DataBind();
    //                //DivTextFileExport.Visible = true;
    //                btnTextFileExportNew.Visible = true;
    //                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                GridView1.UseAccessibleHeader = true;

    //                decimal Salary_EarningTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_EarningTotal"));
    //                decimal EPFWAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPFWAGES"));
    //                decimal EPS_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_WAGES"));
    //                decimal EDLI_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EDLI_WAGES"));
    //                decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
    //                decimal EPS_CONTRI_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_CONTRI_REMITTED"));
    //                decimal EPF_EPS_DIFF_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF_EPS_DIFF_REMITTED"));
    //                decimal NCP_DAYS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NCP_DAYS"));
    //                decimal REFUND_OF_ADVANCES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("REFUND_OF_ADVANCES"));


    //                GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
    //                GridView1.FooterRow.Cells[3].Text = Salary_EarningTotal.ToString();
    //                //GridView1.FooterRow.Cells[4].Text = EPFWAGES.ToString();
    //                //GridView1.FooterRow.Cells[5].Text = EPS_WAGES.ToString();
    //                //GridView1.FooterRow.Cells[6].Text = EDLI_WAGES.ToString();
    //                GridView1.FooterRow.Cells[7].Text = EPF.ToString();
    //                GridView1.FooterRow.Cells[8].Text = EPS_CONTRI_REMITTED.ToString();
    //                GridView1.FooterRow.Cells[9].Text = EPF_EPS_DIFF_REMITTED.ToString();
    //                GridView1.FooterRow.Cells[10].Text = NCP_DAYS.ToString();
    //                GridView1.FooterRow.Cells[11].Text = REFUND_OF_ADVANCES.ToString();

    //                footerfirst.Visible = true;
    //                footersecond.Visible = true;


    //                lblEmployeeP.Text = EPF.ToString();
    //                lblEmployerP.Text = EPF_EPS_DIFF_REMITTED.ToString();
    //                lblPensionP.Text = EPS_CONTRI_REMITTED.ToString();
    //                lblADMCharge.Text = Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.005))).ToString();
    //                lblInspectionCharge.Text = Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.00005))).ToString();

    //                var lastDayOfMonth = DateTime.DaysInMonth(int.Parse(ddlFinancialYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()));
    //                lblSalaryMonth.Text = ddlMonth.SelectedItem.ToString() + " - " + ddlFinancialYear.SelectedValue.ToString();
    //                lblTotalEmployee.Text = ds.Tables[0].Rows.Count.ToString();
    //                lblPaymentDate.Text = lastDayOfMonth.ToString() + "-" + ddlMonth.SelectedValue.ToString() + "-" + ddlFinancialYear.SelectedValue.ToString();
    //                lblGrossSalary.Text = Salary_EarningTotal.ToString();


    //                lblTotal.Text = (double.Parse(EPF.ToString()) + double.Parse(EPF_EPS_DIFF_REMITTED.ToString()) + double.Parse(EPS_CONTRI_REMITTED.ToString()) + double.Parse(Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.005))).ToString()) + double.Parse(Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.00005))).ToString())).ToString();
    //            }
    //            else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
    //            {
    //                GridView1.DataSource = ds.Tables[0];
    //                GridView1.DataBind();
    //                //DivTextFileExport.Visible = true;
    //                btnTextFileExportNew.Visible = true;
    //                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                GridView1.UseAccessibleHeader = true;
    //            }
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void FillGrid()
    {
        try
        {

            /*************ds.Tables[0] is changed To ds.Tables[0]***********/
            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();
            //DivTextFileExport.Visible = false;
            btnTextFileExportNew.Visible = false;
            hide_print_main.Visible = false;


            //if (ddlOffice.SelectedValue.ToString() == "5")
            //{
            //    GridView1.Visible = false;
            //    GridView2.Visible = true;
            //    ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "21", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
            //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {

            //        GridView2.DataSource = ds.Tables[0];
            //        GridView2.DataBind();
            //        //DivTextFileExport.Visible = true;
            //        //btnTextFileExportNew.Visible = true;
            //        GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            //        GridView2.UseAccessibleHeader = true;

            //        decimal Salary_EarningTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_EarningTotal"));
            //        decimal EPFWAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPFWAGES"));
            //        decimal EPS_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_WAGES"));
            //        decimal EDLI_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EDLI_WAGES"));
            //        decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
            //        decimal EPS_CONTRI_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_CONTRI_REMITTED"));
            //        decimal EPF_EPS_DIFF_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF_EPS_DIFF_REMITTED"));
            //        decimal NCP_DAYS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NCP_DAYS"));
            //        decimal REFUND_OF_ADVANCES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("REFUND_OF_ADVANCES"));


            //        GridView2.FooterRow.Cells[1].Text = "| TOTAL | ";
            //        GridView2.FooterRow.Cells[5].Text = Salary_EarningTotal.ToString();
            //        GridView2.FooterRow.Cells[6].Text = EPFWAGES.ToString();
            //        GridView2.FooterRow.Cells[7].Text = EPS_WAGES.ToString();
            //        GridView2.FooterRow.Cells[8].Text = EDLI_WAGES.ToString();
            //        GridView2.FooterRow.Cells[11].Text = EPF.ToString();
            //        GridView2.FooterRow.Cells[10].Text = EPS_CONTRI_REMITTED.ToString();
            //        GridView2.FooterRow.Cells[9].Text = EPF_EPS_DIFF_REMITTED.ToString();
            //        GridView2.FooterRow.Cells[12].Text = NCP_DAYS.ToString();
            //        GridView2.FooterRow.Cells[13].Text = REFUND_OF_ADVANCES.ToString();
            //    }
            //    else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
            //    {
            //        GridView2.DataSource = ds.Tables[0];
            //        GridView2.DataBind();
            //        btnTextFileExportNew.Visible = true;
            //        GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            //        GridView2.UseAccessibleHeader = true;
            //    }
            //}
            //else
            //{
                GridView2.Visible = false;
                GridView1.Visible = true;
                hide_print_main.Visible = true;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    //DivTextFileExport.Visible = true;
                    btnTextFileExportNew.Visible = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                    decimal Salary_EarningTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_EarningTotal"));
                    decimal EPFWAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPFWAGES"));
                    decimal EPS_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_WAGES"));
                    decimal EDLI_WAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EDLI_WAGES"));
                    decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
                    decimal EPS_CONTRI_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPS_CONTRI_REMITTED"));
                    decimal EPF_EPS_DIFF_REMITTED = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF_EPS_DIFF_REMITTED"));
                    decimal NCP_DAYS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NCP_DAYS"));
                    decimal REFUND_OF_ADVANCES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("REFUND_OF_ADVANCES"));


                    GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
                    GridView1.FooterRow.Cells[3].Text = Salary_EarningTotal.ToString();
                    //GridView1.FooterRow.Cells[4].Text = EPFWAGES.ToString();
                    //GridView1.FooterRow.Cells[5].Text = EPS_WAGES.ToString();
                    //GridView1.FooterRow.Cells[6].Text = EDLI_WAGES.ToString();
                    GridView1.FooterRow.Cells[7].Text = EPF.ToString();
                    GridView1.FooterRow.Cells[8].Text = EPS_CONTRI_REMITTED.ToString();
                    GridView1.FooterRow.Cells[9].Text = EPF_EPS_DIFF_REMITTED.ToString();
                    GridView1.FooterRow.Cells[10].Text = NCP_DAYS.ToString();
                    GridView1.FooterRow.Cells[11].Text = REFUND_OF_ADVANCES.ToString();

                    footerfirst.Visible = true;
                    footersecond.Visible = true;


                    lblEmployeeP.Text = EPF.ToString();
                    lblEmployerP.Text = EPF_EPS_DIFF_REMITTED.ToString();
                    lblPensionP.Text = EPS_CONTRI_REMITTED.ToString();
                    lblADMCharge.Text = Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.005))).ToString();
                    lblInspectionCharge.Text = Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.00005))).ToString();

                    var lastDayOfMonth = DateTime.DaysInMonth(int.Parse(ddlFinancialYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()));
                    lblSalaryMonth.Text = ddlMonth.SelectedItem.ToString() + " - " + ddlFinancialYear.SelectedValue.ToString();
                    lblTotalEmployee.Text = ds.Tables[0].Rows.Count.ToString();
                    lblPaymentDate.Text = lastDayOfMonth.ToString() + "-" + ddlMonth.SelectedValue.ToString() + "-" + ddlFinancialYear.SelectedValue.ToString();
                    lblGrossSalary.Text = Salary_EarningTotal.ToString();


                    lblTotal.Text = (double.Parse(EPF.ToString()) + double.Parse(EPF_EPS_DIFF_REMITTED.ToString()) + double.Parse(EPS_CONTRI_REMITTED.ToString()) + double.Parse(Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.005))).ToString()) + double.Parse(Math.Round((double.Parse(Salary_EarningTotal.ToString()) * (0.00005))).ToString())).ToString();
                }
                else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    //DivTextFileExport.Visible = true;
                    btnTextFileExportNew.Visible = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
           // }


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
            //if (ddlFinancialYear.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            //{
            FillGrid();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnTextFileExport_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Write(ds.Tables[1]);

                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
                //DivTextFileExport.Visible = true;
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
            }
            else
            {
                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
                //DivTextFileExport.Visible = true;
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Write(DataTable dt)
    {
        string txt = string.Empty;

        //foreach (DataColumn column in dt.Columns)
        //{
        //    //Add the Header row for Text file.
        //    txt += column.ColumnName + "\t\t";
        //}

        //Add new line.
        // txt += "\r\n";

        foreach (DataRow row in dt.Rows)
        {
            int i = 1;
            int ColCount = dt.Columns.Count;
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Data rows.
                txt += row[column.ColumnName].ToString();
                if (i < ColCount)
                {
                    txt += "#~#";
                }
                i++;
            }

            //Add new line.
            txt += "\r\n";
        }

        //Download the Text file.
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=EpfReport-" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();

    }
    protected void btnTextFileExportNew_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                WriteNew(ds.Tables[0]);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void WriteNew(DataTable dt)
    {
        string txt = string.Empty;

        //foreach (DataColumn column in dt.Columns)
        //{
        //    //Add the Header row for Text file.
        //    txt += column.ColumnName + "\t\t";
        //}

        //Add new line.
        // txt += "\r\n";

        foreach (DataRow row in dt.Rows)
        {
            int i = 1;
            int ColCount = dt.Columns.Count;
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Data rows.
                txt += row[column.ColumnName].ToString();
                if (i < ColCount)
                {
                    txt += "#~#";
                }
                i++;
            }

            //Add new line.
            txt += "\r\n";
        }

        //Download the Text file.
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=EpfReport-" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();

    }
}