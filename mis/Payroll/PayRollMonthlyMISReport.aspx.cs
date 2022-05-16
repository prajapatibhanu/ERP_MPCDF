using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayRollMonthlyMISReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillFinancialYear();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "23" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_Name.DataSource = ds;
                ddlOffice_Name.DataTextField = "Office_Name";
                ddlOffice_Name.DataValueField = "Office_ID";
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
            }
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice_Name.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillFinancialYear()
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
    protected void FillGrid()
    {
        try
        {
            ds = null;
            lblMsg.Text = "";
            string msg = "";
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex <= 0)
            {
                msg += "Select Financial Year \\n";
            }
            if (ddlMonth.SelectedIndex <= 0)
            {
                msg += "Select Month \\n";
            }

            if (msg == "")
            {
                lblDeductionDetails.Text = " SALARY MIS REPORT MONTH  (" + ddlMonth.SelectedItem.ToString() + "-" + ddlFinancialYear.SelectedItem.ToString() + ") of " + ddlOffice_Name.SelectedItem.ToString();

                ds = objdb.ByProcedure("SpPayrollMISReport",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear" },
                    new string[] { "0", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    DataSet ds2 = objdb.ByProcedure("SpPayrollSalaryFinalSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice_Name.SelectedValue.ToString() }, "dataset");
                    if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                    {
                        ViewState["OfficeName"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                    }
                    else
                    {
                        ViewState["OfficeName"] = "";
                    }


                    lblDeductionDetails.ToolTip = "<p class='text-center' style='font-size:20px; text-decoration:underline;'>" + ViewState["OfficeName"].ToString() + " </br> SALARY MIS REPORT MONTH-   " + ddlMonth.SelectedItem.ToString() + " / " + ddlFinancialYear.SelectedItem.ToString() + " </p>";
                    //foreach (GridViewRow row in GridView1.Rows)
                    //{
                    //    Label lblEarn = (Label)row.FindControl("lblEarnDed");
                    //    if (lblEarn.Text == "")
                    //    {
                    //        lblEarn.Text = "0";
                    //    }
                    //}

                    //Decimal ArrearEarningAmt = 0;
                    int class1 = 0;
                    int class2 = 0;
                    int class3 = 0;
                    int class4 = 0;
                    int GrossSalary = 0;
                    int EpfSalary = 0;
                    int GrandTotal = 0;
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        class1 += int.Parse(ds.Tables[0].Rows[i]["class1"].ToString());
                        class2 += int.Parse(ds.Tables[0].Rows[i]["class2"].ToString());
                        class3 += int.Parse(ds.Tables[0].Rows[i]["class3"].ToString());
                        class4 += int.Parse(ds.Tables[0].Rows[i]["class4"].ToString());
                        GrossSalary += int.Parse(ds.Tables[0].Rows[i]["GrossSalary"].ToString());
                        EpfSalary += int.Parse(ds.Tables[0].Rows[i]["EpfSalary"].ToString());
                        GrandTotal += int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString());
                    }
                                        
                    GridView1.FooterRow.Cells[1].Text = "<b>| TOTAL |</b>";

                    GridView1.FooterRow.Cells[3].Text = "<b>" + class1.ToString() + "</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + class2.ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + class3.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + class4.ToString() + "</b>";
                    GridView1.FooterRow.Cells[7].Text = "<b>" + GrossSalary.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + EpfSalary.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + GrandTotal.ToString() + "</b>";

                }
                else if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlOffice_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            ddlFinancialYear.ClearSelection();
            ddlMonth.ClearSelection();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}