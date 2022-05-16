using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

public partial class mis_Payroll_PayrollFYSalaryLedger2 : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();

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
            ddlOfficeName.Enabled = false;
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            //string selectedEmpDetail = "Advance Taxation Details for Financial Year  " + ddlYear.SelectedItem.Text + " , Officer/ Employee Name: " + ddlEmployee.SelectedItem.Text + "  , Office: " + ddlOfficeName.SelectedItem.Text;

            //string selectedEmpDetail = "Advance Taxation Details for Financial Year  " + ddlYear.SelectedItem.Text + " , Officer/ Employee Name: " + ddlEmployee.SelectedItem.Text + "  , Office: " + ddlOfficeName.SelectedItem.Text;


            ds = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Emp_ID" }, new string[] { "2", ddlEmployee.SelectedValue.ToString() }, "dataset");
            string selectedEmpDetail = "";
            if (ds.Tables[0].Rows.Count != 0)
            {
                selectedEmpDetail = "<div class='row print_heading'><div class='col-md-12 text-center'><p class='heading'>" + ds.Tables[0].Rows[0]["Office_Name"] + "</p><p class='subheading'>Salary Statement for the Financial Year " + ddlYear.SelectedItem.Text + "</p></div><div class='col-md-12'><div class='col-md-6 col-sm-6 col-xs-6 emp_detail'><p><b>Name of the Employee:</b> " + ds.Tables[0].Rows[0]["Emp_Name"] + "</p></div><div class='col-md-4 col-sm-4 col-xs-4 emp_detail2'><p><b>Designation:</b> " + ds.Tables[0].Rows[0]["Designation_Name"] + "</p></div><div class='col-md-2 col-sm-2 col-xs-2 emp_detail2'><p><b>Code:</b> " + ds.Tables[0].Rows[0]["SalaryEmp_No"] + "  " + ds.Tables[0].Rows[0]["SalarySec_No"] + "</p></div></div></div>";
                lblEmpDetail.Text = selectedEmpDetail.ToString();

            }



            ds = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Year", "Emp_ID", "Office_ID" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                decimal BasicSalary = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BasicSalary"));
                decimal DA = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DA"));
                decimal HRA = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("HRA"));
                decimal Conv = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Conv"));
                decimal Ord = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Ord"));
                decimal Wash = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Wash"));
                decimal OtherEarning = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OtherEarning"));
                decimal EarningTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EarningTotal"));
                decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));

                decimal ADA = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ADA"));
                decimal ITax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ITax"));
                decimal PTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PTax"));
                decimal OtherDeduction = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction"));
                decimal DeductionTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DeductionTotal"));

                GridView1.FooterRow.Cells[0].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[2].Text = BasicSalary.ToString();
                GridView1.FooterRow.Cells[3].Text = DA.ToString();
                GridView1.FooterRow.Cells[4].Text = HRA.ToString();
                GridView1.FooterRow.Cells[5].Text = Conv.ToString();
                GridView1.FooterRow.Cells[6].Text = Ord.ToString();
                GridView1.FooterRow.Cells[7].Text = Wash.ToString();
                GridView1.FooterRow.Cells[8].Text = OtherEarning.ToString();
                GridView1.FooterRow.Cells[9].Text = EarningTotal.ToString();
                GridView1.FooterRow.Cells[10].Text = EPF.ToString();
                GridView1.FooterRow.Cells[11].Text = ADA.ToString();
                GridView1.FooterRow.Cells[12].Text = ITax.ToString();
                GridView1.FooterRow.Cells[13].Text = PTax.ToString();
                GridView1.FooterRow.Cells[14].Text = OtherDeduction.ToString();
                GridView1.FooterRow.Cells[15].Text = DeductionTotal.ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      //  FillGrid();
        GetPayrollSalaryDetails();
    }


    //protected void Button1_Click1(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);
    //        for (int x = 0; x <= 3; x++)
    //        {
    //            //GridViewRow rows = (GridViewRow)GridView1.HeaderRow.Parent.Controls[x];
    //            //rows.BackColor = Color.White;
    //            //rows.Height = 15;
    //            //for (int i = 0; i <= rows.Cells.Count - 1; i++)
    //            //{
    //            //    rows.Cells[i].BackColor = Color.Maroon;
    //            //}
    //        }
    //        foreach (GridViewRow row in GridView1.Rows)
    //        {
    //            row.BackColor = Color.White;
    //            foreach (TableCell cell in row.Cells)
    //            {
    //                cell.VerticalAlign = VerticalAlign.Middle;
    //                cell.CssClass = "textmode";
    //            }
    //        }
    //        GridView1.RenderControl(hw);
    //        //style to format numbers to string
    //        string style = @"<style> .textmode { } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //    }
    //}
    //public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    //{
    //    // controller   
    //}
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=PayrollSalaryDetailsForAdvanceTaxDeduction.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
     private void GetPayrollSalaryDetails()
    {
        DataSet ds5 = new DataSet();
        try
        {
            ds = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Emp_ID" }, new string[] { "2", ddlEmployee.SelectedValue.ToString() }, "dataset");
           
           

            ds5 = objdb.ByProcedure("SpPayrollSalaryAdvanceTax_FY", new string[] { "flag", "Year", "Emp_ID", "Office_ID" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString() }, "dataset");
                StringBuilder sb1 = new StringBuilder();
                int Count1 = ds5.Tables[0].Rows.Count;
                int ColCount1 = ds5.Tables[0].Columns.Count;
            if(ds5!=null && ds5.Tables[0].Rows.Count>0)
            {
                decimal bs = 0, hra = 0, da = 0, Conv = 0, Ord = 0, Wash = 0, OtherEarning, et = 0;
                decimal epf = 0, itax = 0, ptax = 0, OtherDeduction = 0, DeductionTotal = 0;

                decimal at_bs = 0, at_hra = 0, at_da = 0, at_Conv = 0, at_Ord = 0, at_Wash = 0, at_OtherEarning=0, at_et = 0;
                decimal   at_epf =0  ,at_itax = 0  ,at_ptax =0 , at_OtherDeduction =0, at_DeductionTotal=0;

                lblMsg.Text=string.Empty;
                btnPrint.Visible = true;
                btnExcel.Visible = true;

                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='15'><b>" + ds.Tables[0].Rows[0]["Office_Name"] + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='15'><b>Salary Statement for the Financial Year</b> " + ddlYear.SelectedItem.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='6'><b>Name of the Employee: </b>" + ds.Tables[0].Rows[0]["Emp_Name"] + "</td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='4'><b>Designation: </b>" + ds.Tables[0].Rows[0]["Designation_Name"] + "</td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='4'><b>PAN No.: </b>" + ds.Tables[0].Rows[0]["Emp_PanCardNo"] + "</td>");
                 sb1.Append("<td style='border: 1px solid #000000;text-align:left;' colspan='1'><b>Code: </b>" + ds.Tables[0].Rows[0]["SalarySec_No"] + "  " + ds.Tables[0].Rows[0]["SalaryEmp_No"] + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Month<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Year<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>BASIC<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>DA<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>HRA<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Conv<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Ord<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Wash<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Other<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Total Earning<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>EPF<b></td>");
                //sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>ADA<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>ITax<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>PTax<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>LIC<b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>Total Deduction<b></td>");
                sb1.Append("</tr>");
                sb1.Append("</thead>");
                for (int i = 0; i < Count1;i++ )
                {
                    sb1.Append("<tr>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Particular"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["MYear"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["BasicSalary"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DA"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["HRA"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Conv"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Ord"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Wash"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["OtherEarning"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["EarningTotal"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["EPF"] + "</td>");
                    //sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["ADA"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["ITax"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["PTax"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["OtherDeduction"] + "</td>");
                    sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DeductionTotal"] + "</td>");
                    sb1.Append("</tr>");
                }
                 bs = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("BasicSalary") ?? 0);
                da = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DA")??0);
                hra = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("HRA") ?? 0);
                Conv = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Conv") ?? 0);
                Ord = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Ord") ?? 0);
                Wash = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Wash") ?? 0);
                OtherEarning = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("OtherEarning") ?? 0);
                et = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("EarningTotal") ?? 0);
                epf = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("EPF") ?? 0);
                //ada = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ADA")??0);
                itax = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("ITax") ?? 0);
                ptax = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("PTax") ?? 0);
                OtherDeduction = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("OtherDeduction") ?? 0);
                DeductionTotal = ds5.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DeductionTotal") ?? 0);
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='2'><b>| Total |</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + bs + "</b></td>");               
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + da + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + hra + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + Conv + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + Ord + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + Wash + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + OtherEarning + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + et + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + epf + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + itax + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + ptax + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + OtherDeduction + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + DeductionTotal + "</b></td>");
                sb1.Append("</tr>");
                  
                if(ds5.Tables[1].Rows.Count>0)
                {
                  int Count11= ds5.Tables[1].Rows.Count;
                  for (int i = 0; i < Count11; i++)
                    {
                        sb1.Append("<tr>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;' colspan='2'>" + ds5.Tables[1].Rows[i]["ArrearType"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["BasicSalary"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["DA"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["HRA"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["Conv"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["Ord"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["Wash"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["OtherEarning"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["EarningTotal"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["EPF"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["ITax"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["PTax"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["OtherDeduction"] + "</td>");
                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + ds5.Tables[1].Rows[i]["DeductionTotal"] + "</td>");
                        sb1.Append("</tr>");
                    }
                    at_bs = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("BasicSalary") ?? 0);
                  at_da = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("DA") ?? 0);
                  at_hra = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("HRA") ?? 0);

                  at_Conv = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Conv") ?? 0);
                  at_Ord = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Ord") ?? 0);
                  at_Wash = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Wash") ?? 0);
                  at_OtherEarning = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("OtherEarning") ?? 0);
                  at_et = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("EarningTotal") ?? 0);
                  at_epf = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("EPF") ?? 0);
                  //at_ada = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("ADA")??0);
                  at_itax = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("ITax") ?? 0);
                  at_ptax = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("PTax") ?? 0);
                  at_OtherDeduction = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("OtherDeduction") ?? 0);
                  at_DeductionTotal = ds5.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("DeductionTotal") ?? 0);
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='2'><b>| Total |</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_bs + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_da  + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_hra + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_Conv + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_Ord + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_Wash + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_OtherEarning + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_et + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_epf + "</b></td>");
                    //sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_ada + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_itax + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_ptax + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_OtherDeduction + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + at_DeductionTotal + "</b></td>");
                    sb1.Append("</tr>");
                }
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center' colspan='2'><b>| Grand Total |</b></td>");

                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (bs + at_bs) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (da + at_da)  + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (hra + at_hra) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (Conv+at_Conv) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (Ord+at_Ord) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (Wash+at_Wash) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (OtherEarning + at_OtherEarning) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (et+at_et) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (epf+at_epf) + "</b></td>");
                //sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ada+at_ada) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (itax+at_itax) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (ptax+at_ptax) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (OtherDeduction+at_OtherDeduction) + "</b></td>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center'><b>" + (DeductionTotal+at_DeductionTotal) + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
            }
            else
            {
                 div_page_content.InnerHtml = "";
                 Print.InnerHtml = "";
                btnPrint.Visible = false;
                btnExcel.Visible = false;
                 lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "No Record Found.");
            }
               
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds5 !=null) { ds5.Dispose(); }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}