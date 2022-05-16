using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Payroll_PayRollEarnDedFixedHead : System.Web.UI.Page
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
                fillEarnDeductionHead();
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

            }
            else
            {
                ddlOffice_Name.DataSource = null;
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Clear();

            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }

   
    protected void FillGrid()
    {
        try
        {
            ds = null;

            string msg = "";
            lblDeductionDetails.Text = "";
            
            if (ddlEarnDeducHead.SelectedIndex <= 0)
            {
                msg += "Select Earning Deduction Head \\n";
            }
            
            if (msg == "")
            {
                lblDeductionDetails.Text = ddlEarnDeducHead.SelectedItem.ToString() + "  Master data for office " + ddlOffice_Name.SelectedItem.ToString();

                ds = objdb.ByProcedure("SpPayrollFixSingleHead",
                    new string[] { "flag", "Office_ID", "EarnDedution_ID" },
                    new string[] { "1", ddlOffice_Name.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    decimal NetDa = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    //decimal NetCalculated = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Emp_CalculatedHead"));

                    GridView1.FooterRow.Cells[2].Text = "Total";
                    GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[4].Text = NetDa.ToString("N2");
                    GridView1.FooterRow.Cells[4].CssClass = "TotalDa";
                    btnSave.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void fillEarnDeductionHead()
    {
        ds = null;
        ddlEarnDeducHead.Items.Clear();
        ds = objdb.ByProcedure("SpPayrollEarnDedOfficeWiseMaster", new string[] { "flag", "Office_ID" }, new string[] { "7", ddlOffice_Name.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlEarnDeducHead.DataSource = ds;
            ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
            ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
            ddlEarnDeducHead.DataBind();
            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void ddlOffice_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            //ddlFinancialYear.ClearSelection();
            //ddlMonth.ClearSelection();
            fillEarnDeductionHead();
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

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
                string Emp_ID = lblRowNumber.ToolTip.ToString();
                TextBox TotalAmount = (TextBox)row.FindControl("txtAmount");


                if (chkSelect.Checked == true)
                {
                    ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail",
                                      new string[] { "flag", "EarnDeduction_ID" },
                                      new string[] { "4", ddlEarnDeducHead.SelectedValue.ToString() }, "dataset");

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objdb.ByProcedure("SpPayrollFixSingleHead",
                                     new string[] { "flag", "Emp_ID", "EarnDedution_ID", "CalculationType", "EarnDeductionAmount", "EarnDeductionName", "UpdatedBy" },
                                     new string[] { "0", Emp_ID.ToString(), ddlEarnDeducHead.SelectedValue.ToString(), ds.Tables[0].Rows[0]["EarnDeduction_Calculation"].ToString(), TotalAmount.Text.ToString(), ds.Tables[0].Rows[0]["EarnDeduction_Type"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

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