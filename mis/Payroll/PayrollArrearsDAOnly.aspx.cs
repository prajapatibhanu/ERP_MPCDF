using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Payroll_PayrollArrearsDAOnly : System.Web.UI.Page
{
    DataSet ds, ds2 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropdown();

                }
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
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

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

            ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlTYear.Items.Insert(0, new ListItem("Select", "0"));

            ds = null;
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // ==== From Year ====
                ddlFYear.DataSource = ds;
                ddlFYear.DataTextField = "Year";
                ddlFYear.DataValueField = "Year";
                ddlFYear.DataBind();
                ddlFYear.Items.Insert(0, new ListItem("Select", "0"));
                ddlFYear.Items.Insert(1, new ListItem("2006", "2006"));
                ddlFYear.Items.Insert(2, new ListItem("2007", "2007"));
                ddlFYear.Items.Insert(3, new ListItem("2008", "2008"));
                ddlFYear.Items.Insert(4, new ListItem("2009", "2009"));
                ddlFYear.Items.Insert(5, new ListItem("2010", "2010"));
                ddlFYear.Items.Insert(6, new ListItem("2011", "2011"));
                ddlFYear.Items.Insert(7, new ListItem("2012", "2012"));
                ddlFYear.Items.Insert(8, new ListItem("2013", "2013"));
                ddlFYear.Items.Insert(9, new ListItem("2014", "2014"));
                ddlFYear.Items.Insert(10, new ListItem("2015", "2015"));
                ddlFYear.Items.Insert(11, new ListItem("2016", "2016"));
                // ==== To Year ====
                ddlTYear.DataSource = ds;
                ddlTYear.DataTextField = "Year";
                ddlTYear.DataValueField = "Year";
                ddlTYear.DataBind();
                ddlTYear.Items.Insert(0, new ListItem("Select", "0"));
                ddlTYear.Items.Insert(1, new ListItem("2006", "2006"));
                ddlTYear.Items.Insert(2, new ListItem("2007", "2007"));
                ddlTYear.Items.Insert(3, new ListItem("2008", "2008"));
                ddlTYear.Items.Insert(4, new ListItem("2009", "2009"));
                ddlTYear.Items.Insert(5, new ListItem("2010", "2010"));
                ddlTYear.Items.Insert(6, new ListItem("2011", "2011"));
                ddlTYear.Items.Insert(7, new ListItem("2012", "2012"));
                ddlTYear.Items.Insert(8, new ListItem("2013", "2013"));
                ddlTYear.Items.Insert(9, new ListItem("2014", "2014"));
                ddlTYear.Items.Insert(10, new ListItem("2015", "2015"));
                ddlTYear.Items.Insert(11, new ListItem("2016", "2016"));
                // ==== Month ====
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
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
            /*************************/
            DateTime FDate = new DateTime(Convert.ToInt32(ddlFYear.SelectedValue),
                       Convert.ToInt32(ddlFMonth.SelectedValue),
                       1);
            DateTime TDate = new DateTime(Convert.ToInt32(ddlTYear.SelectedValue),
                       Convert.ToInt32(ddlTMonth.SelectedValue),
                       1);
            string FromDate = FDate.ToString("yyyy-MM-dd");
            string ToDate = TDate.ToString("yyyy-MM-dd");

            ViewState["ArrearFYear_No"] = ddlFYear.SelectedValue.ToString();
            ViewState["ArrearTYear_No"] = ddlTYear.SelectedValue.ToString();
            ViewState["ArrearYear_No"] = ddlYear.SelectedValue.ToString();

            ViewState["ArrearFMonth_No"] = ddlFMonth.SelectedValue.ToString();
            ViewState["ArrearFMonth_Name"] = ddlFMonth.SelectedItem.ToString();
            ViewState["ArrearTMonth_No"] = ddlTMonth.SelectedValue.ToString();
            ViewState["ArrearTMonth_Name"] = ddlTMonth.SelectedItem.ToString();
            ViewState["ArrearMonth_No"] = ddlMonth.SelectedValue.ToString();

            /*************************/


            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                new string[] { "flag", "DaRate", "FromDate", "ToDate", "CurrentMonth", "CurrentYear", "Office_ID" },
                new string[] { "9", txtdarate.Text.ToString(), FromDate.ToString(), ToDate.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

                int NetDa = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("DaRemainingArrearAmount"));
                int NetEpf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("RemainingEpfAmount"));
                int NetTotal = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("NetPayment"));

                GridView1.FooterRow.Cells[2].Text = "Total";
                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[8].Text = NetDa.ToString("N2");
                GridView1.FooterRow.Cells[8].CssClass = "TotalDa";
                GridView1.FooterRow.Cells[9].Text = NetEpf.ToString("N2");
                GridView1.FooterRow.Cells[9].CssClass = "TotalEpf";
                GridView1.FooterRow.Cells[10].Text = NetTotal.ToString("N2");
                GridView1.FooterRow.Cells[10].CssClass = "TotalNet";
                btnSave.Visible = true;
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
            FillGrid();
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
            string order_no = txtOrderNo.Text.ToString() ;
            string order_date = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");



            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
                string ArrearEmp_ID = lblRowNumber.ToolTip.ToString();

                TextBox BasicSalary = (TextBox)row.FindControl("txtBasicSalary");
                TextBox ArrearBasicSalary = (TextBox)row.FindControl("txtArrearBasicSalary");
                TextBox TotalBasicSalary = (TextBox)row.FindControl("txtTotalBasicSalary");
                TextBox PaidDa = (TextBox)row.FindControl("txtPaidDa");
                TextBox DaToBePaid = (TextBox)row.FindControl("txtDaToBePaid");
                TextBox DaRemainingArrearAmount = (TextBox)row.FindControl("txtDaRemainingArrearAmount");
                HiddenField RemainingEpfAmount = (HiddenField)row.FindControl("hfRemainingEpfAmount");
                HiddenField NetPayment = (HiddenField)row.FindControl("hfNetPayment");

                if (chkSelect.Checked == true && int.Parse(DaRemainingArrearAmount.Text) > 0)
                {

                    ds = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                        new string[] { "flag", "Office_ID", "Emp_ID", "CurrentYear", "CurrentMonth", "OrderNo", "OrderDate", "FromYear", "FromMonth", "FromMonthName", "ToYear", "ToMonth", "ToMonthName", "BasicSalary", "TotalEarning", "Policy", "TotalDeduction", "NetPaymentAmount", "Arrear_UpdatedBy", "Arrear_Type" },
                        new string[] { "2", ddlOfficeName.SelectedValue.ToString(), ArrearEmp_ID.ToString(),ViewState["ArrearYear_No"].ToString(),ViewState["ArrearMonth_No"].ToString(), order_no,order_date, ViewState["ArrearFYear_No"].ToString(), ViewState["ArrearFMonth_No"].ToString(),ViewState["ArrearFMonth_Name"].ToString(), ViewState["ArrearTYear_No"].ToString(), ViewState["ArrearTMonth_No"].ToString(),ViewState["ArrearTMonth_Name"].ToString()
                        ,"0", DaRemainingArrearAmount.Text, "0", RemainingEpfAmount.Value, NetPayment.Value, ViewState["Emp_ID"].ToString(),"DA" }, "dataset");

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string Arrear_ID = ds.Tables[0].Rows[0]["EmpArrearID"].ToString();
                        string LoginUserID = ViewState["Emp_ID"].ToString();

                        //int[] EarningDeductionId = new int[] { 1, 2, 3, 4, 5, 6, 7, 25, 27, 29, 8, 9, 30, 10, 11, 12, 13, 14, 16, 18, 19, 20, 22, 23, 28 };
                        int[] EarningDeductionId = new int[] { 1, 8 };
                        foreach (int ED_ID in EarningDeductionId)
                        {

                            string EarningDeductionType = "";
                            string EarningDeductionName = "";
                            ds2 = objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EarnDedution_ID" },
                                              new string[] { "4", ED_ID.ToString() }, "dataset");

                            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                            {
                                EarningDeductionType = ds2.Tables[0].Rows[0]["EarnDeduction_Type"].ToString();
                                EarningDeductionName = ds2.Tables[0].Rows[0]["EarnDeduction_Name"].ToString();
                            }

                            if (ED_ID == 1)
                            {
                                objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                                              new string[] { "3", Arrear_ID, ArrearEmp_ID, ED_ID.ToString(), EarningDeductionName, DaRemainingArrearAmount.Text, EarningDeductionType, LoginUserID }, "dataset");

                            }
                            else if (ED_ID == 8)
                            {
                                objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                                              new string[] { "3", Arrear_ID, ArrearEmp_ID, ED_ID.ToString(), EarningDeductionName, RemainingEpfAmount.Value, EarningDeductionType, LoginUserID }, "dataset");

                            }
                            else
                            {
                                objdb.ByProcedure("SpPayrollEmpArrearOnlyDA",
                                              new string[] { "flag", "EmpArrear_ID", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "RemainingAmount", "EarnDeductionType", "ArrearEarnDeduction_UpdatedBy" },
                                              new string[] { "3", Arrear_ID, ArrearEmp_ID, ED_ID.ToString(), EarningDeductionName, "0", EarningDeductionType, LoginUserID }, "dataset");
                            }

                        }


                    }
                }
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


}