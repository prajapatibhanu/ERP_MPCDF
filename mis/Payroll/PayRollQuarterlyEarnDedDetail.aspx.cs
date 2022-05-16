using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


public partial class mis_Payroll_PayRollQuarterlyEarnDedDetail : System.Web.UI.Page
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
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice_Name.Enabled = true;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                else
                {
                    ddlOffice_Name.Enabled = false;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                //FillEmptyGrid();

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
            }
            else
            {
                ddlOffice_Name.DataSource = null;
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Clear();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
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
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlFinancialYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblDeductionDetails.Text = "";
    //        if (ddlFinancialYear.SelectedIndex > 0)
    //        {
    //            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlEarnDeducHead.Items.Insert(1, new ListItem("Income Tax & Professional Tax", "1"));
    //        }
    //        else
    //        {
    //            ddlEarnDeducHead.Items.Clear();
    //            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
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
                msg += "Select Quarter \\n";
            }
            if (ddlEarnDeducHead.SelectedIndex <= 0)
            {
                msg += "Select Earning Deduction Head \\n";
            }
            if (msg == "")
            {
                lblDeductionDetails.Text = ddlEarnDeducHead.SelectedItem.ToString() + " Details (" + ddlMonth.SelectedItem.ToString() + "-" + ddlFinancialYear.SelectedItem.ToString() + ") of " + ddlOffice_Name.SelectedItem.ToString();

                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
                    new string[] { "13", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    /*****************/
                    if (ddlMonth.SelectedValue.ToString() == "1")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Jan";
                        GridView1.HeaderRow.Cells[6].Text = "Feb";
                        GridView1.HeaderRow.Cells[7].Text = "Mar";
                        GridView1.HeaderRow.Cells[10].Text = "Jan EarnDed";
                        GridView1.HeaderRow.Cells[11].Text = "Feb EarnDed";
                        GridView1.HeaderRow.Cells[12].Text = "Mar EarnDed";

                    }
                    else if (ddlMonth.SelectedValue.ToString() == "2")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Apr";
                        GridView1.HeaderRow.Cells[6].Text = "May";
                        GridView1.HeaderRow.Cells[7].Text = "Jun";
                        GridView1.HeaderRow.Cells[10].Text = "Apr EarnDed";
                        GridView1.HeaderRow.Cells[11].Text = "May EarnDed";
                        GridView1.HeaderRow.Cells[12].Text = "Jun EarnDed";

                    }
                    else if (ddlMonth.SelectedValue.ToString() == "3")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Jul";
                        GridView1.HeaderRow.Cells[6].Text = "Aug";
                        GridView1.HeaderRow.Cells[7].Text = "Sep";
                        GridView1.HeaderRow.Cells[10].Text = "Jul EarnDed";
                        GridView1.HeaderRow.Cells[11].Text = "Aug EarnDed";
                        GridView1.HeaderRow.Cells[12].Text = "Sep EarnDed";

                    }
                    else if (ddlMonth.SelectedValue.ToString() == "4")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Oct";
                        GridView1.HeaderRow.Cells[6].Text = "Nov";
                        GridView1.HeaderRow.Cells[7].Text = "Dec";
						
						string[] arr = ddlEarnDeducHead.SelectedItem.Text.Split('(');
                        if(ddlEarnDeducHead.SelectedValue=="11")
                        {
                            GridView1.HeaderRow.Cells[10].Text = "Oct ITax";
                            GridView1.HeaderRow.Cells[11].Text = "Nov ITax";
                            GridView1.HeaderRow.Cells[12].Text = "Dec ITax";
                        }
                        else if(ddlEarnDeducHead.SelectedValue=="12")
                        {
                            GridView1.HeaderRow.Cells[10].Text = "Oct PTax";
                            GridView1.HeaderRow.Cells[11].Text = "Nov PTax";
                            GridView1.HeaderRow.Cells[12].Text = "Dec PTax";
                        }
                        else
                        {
                            GridView1.HeaderRow.Cells[10].Text = "Oct " + arr[0];
                            GridView1.HeaderRow.Cells[11].Text = "Nov " + arr[0];
                            GridView1.HeaderRow.Cells[12].Text = "Dec " + arr[0];
                        }
                        // GridView1.HeaderRow.Cells[10].Text = "Oct PTax";
                        // GridView1.HeaderRow.Cells[11].Text = "Nov PTax";
                        // GridView1.HeaderRow.Cells[12].Text = "Dec PTax";
                    }
                    /*****************/
                    Decimal EarningAmt_Arr = 0;
                    Decimal EarningAmt_1 = 0;
                    Decimal EarningAmt_2 = 0;
                    Decimal EarningAmt_3 = 0;
                    Decimal EarningAmt_Total = 0;

                    Decimal PTax_Total_Arr = 0;
                    Decimal PTax_Total_1 = 0;
                    Decimal PTax_Total_2 = 0;
                    Decimal PTax_Total_3 = 0;
                    Decimal PTax_Total = 0;

                    /*******************/
                    int i = 0;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        decimal EarningAmt = 0;
                        decimal PTax_Amt = 0;
                        if (ds.Tables[0].Rows[i]["ArrearEarnings"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnings"].ToString()); // Column Value
                            EarningAmt_Arr += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnings"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnings"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["EarningTotal1"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal1"].ToString()); // Column Value
                            EarningAmt_1 += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal1"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal1"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["EarningTotal2"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal2"].ToString()); // Column Value
                            EarningAmt_2 += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal2"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal2"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["EarningTotal3"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal3"].ToString()); // Column Value
                            EarningAmt_3 += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal3"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal3"].ToString());
                        }

                        /********************/
                                                
                        if (ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString()); // Column Value
                            PTax_Total_Arr += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["PTaxDed1"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed1"].ToString()); // Column Value
                            PTax_Total_1 += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed1"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed1"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["PTaxDed2"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed2"].ToString()); // Column Value
                            PTax_Total_2 += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed2"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed2"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["PTaxDed3"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed3"].ToString()); // Column Value
                            PTax_Total_3 += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed3"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed3"].ToString());
                        }
                        Label lblEarningTotal = (Label)row.FindControl("lblEarningTotal");
                        Label lblPTaxDedTotal = (Label)row.FindControl("lblPTaxDedTotal");
                        lblEarningTotal.Text = EarningAmt.ToString();
                        lblPTaxDedTotal.Text = PTax_Amt.ToString();
                        i++;
                    }
                    /*********************/


                    GridView1.FooterRow.Cells[0].Text = "<b>| TOTAL |</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + EarningAmt_Arr.ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + EarningAmt_1.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + EarningAmt_2.ToString() + "</b>";
                    GridView1.FooterRow.Cells[7].Text = "<b>" + EarningAmt_3.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + EarningAmt_Total.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + PTax_Total_Arr.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + PTax_Total_1.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + PTax_Total_2.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + PTax_Total_3.ToString() + "</b>";
                    GridView1.FooterRow.Cells[13].Text = "<b>" + PTax_Total.ToString() + "</b>";

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
            FillEmptyGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmptyGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollPolicyDetail",
                   new string[] { "flag" },
                   new string[] { "6" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
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

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlFinancialYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}