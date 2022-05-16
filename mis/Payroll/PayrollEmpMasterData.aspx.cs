using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpMasterData : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                DivDetail.Visible = false;
                TextReadonly();
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
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
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

            lblMsg.Text = "";
            RepeaterEarning.DataSource = null;
            RepeaterEarning.DataBind();
            RepeaterDeduction.DataSource = null;
            RepeaterDeduction.DataBind();
            ds = objdb.ByProcedure("SpPayrollEmpMasterData", new string[] { "flag", "Office_ID", "Emp_ID" },
                new string[] { "0", ViewState["Office_ID"].ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                DivDetail.Visible = true;
                RepeaterEarning.DataSource = ds.Tables[0];
                RepeaterEarning.DataBind();
                RepeaterDeduction.DataSource = ds.Tables[1];
                RepeaterDeduction.DataBind();

                lblbankacc.Text = ds.Tables[2].Rows[0]["Bank_AccountNo"].ToString();
                lblbankname.Text = ds.Tables[2].Rows[0]["Bank_Name"].ToString();
                lblbasic.Text = ds.Tables[2].Rows[0]["BASICSALARY"].ToString();
                lblemp.Text = ds.Tables[2].Rows[0]["SalaryEmp_No"].ToString();
                lblgis.Text = ds.Tables[2].Rows[0]["GroupInsurance_No"].ToString();
                lblifsc.Text = ds.Tables[2].Rows[0]["IFSC"].ToString();
                lblpan.Text = ds.Tables[2].Rows[0]["PAN"].ToString();
                lblsec.Text = ds.Tables[2].Rows[0]["SalarySec_No"].ToString();
                lblpaycommision.Text = ds.Tables[2].Rows[0]["Emp_SalaryLevel"].ToString() + "th Pay";
                lbluan.Text = ds.Tables[2].Rows[0]["UAN_No"].ToString();
                txtEarnRemainingSalary_Basic.Text = ds.Tables[2].Rows[0]["BASICSALARY"].ToString();


                if (ds.Tables[2].Rows[0]["Emp_SalaryLevel"].ToString() == "7")
                {
                    lbldarate.Text = ds.Tables[3].Rows[0]["DA7th"].ToString();
                }
                else if (ds.Tables[2].Rows[0]["Emp_SalaryLevel"].ToString() == "6")
                {
                    lbldarate.Text = ds.Tables[3].Rows[0]["DA6th"].ToString();
                }
                else if (ds.Tables[2].Rows[0]["Emp_SalaryLevel"].ToString() == "4")
                {
                    lbldarate.Text = ds.Tables[3].Rows[0]["DA4th"].ToString();
                }
                else
                {
                    lbldarate.Text = ds.Tables[3].Rows[0]["DAOther"].ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            DivDetail.Visible = false;
            txtEarnRemainingSalary_Basic.Text = "0";
            txtETotalRemaining.Text = "0";
            txtDTotalRemaining.Text = "0";
            RepeaterEarning.DataSource = null;
            RepeaterEarning.DataBind();
            RepeaterDeduction.DataSource = null;
            RepeaterDeduction.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TextReadonly()
    {
        txtETotalRemaining.Attributes.Add("readonly", "readonly");
        txtDTotalRemaining.Attributes.Add("readonly", "readonly");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }

            if (msg.Trim() == "")
            {
                string EMP_ID = ddlEmployee.SelectedValue.ToString();
                /**********Update Basic Data********/
                objdb.ByProcedure("SpPayrollEmpMasterData",
                new string[] { "flag", "Office_ID", "Emp_ID", "BasicSalary", "UpdatedBy" },
                new string[] { "1", ddlOfficeName.SelectedValue.ToString(), EMP_ID, txtEarnRemainingSalary_Basic.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                string LoginUserID = ViewState["Emp_ID"].ToString();

                /**********Update Earning Master Data********/
                foreach (RepeaterItem item in RepeaterEarning.Items)
                {
                    Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                    Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                    Label lblCalculationType = (Label)item.FindControl("lblCalculationType");
                    TextBox txtRemainingEarning = (TextBox)item.FindControl("txtRemainingEarning");

                    objdb.ByProcedure("SpPayrollEmpMasterData",
                    new string[] { "flag", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "EarnDeductionType", "EarnDeductionAmount", "CalculationType", "UpdatedBy" },
                    new string[] { "2", EMP_ID, lblEarnDeduction_ID.Text, lblEarnDeduction_Name.Text, "Earning", txtRemainingEarning.Text, lblCalculationType.Text, LoginUserID }, "dataset");

                }

                /**********Update Deduction Master Data********/
                foreach (RepeaterItem item in RepeaterDeduction.Items)
                {
                    Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                    Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                    Label lblCalculationType = (Label)item.FindControl("lblCalculationType");
                    TextBox txtRemainingDeduction = (TextBox)item.FindControl("txtRemainingDeduction");
                    objdb.ByProcedure("SpPayrollEmpMasterData",
                    new string[] { "flag", "Emp_ID", "EarnDedution_ID", "EarnDeductionName", "EarnDeductionType", "EarnDeductionAmount", "CalculationType", "UpdatedBy" },
                    new string[] { "2", EMP_ID, lblEarnDeduction_ID.Text, lblEarnDeduction_Name.Text, "Deduction", txtRemainingDeduction.Text, lblCalculationType.Text, LoginUserID }, "dataset");
                }

                //ClearText();
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
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
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
            string msg = "";
            lblMsg.Text = "";
            DivDetail.Visible = false;
            ClearText();

            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }

            if (msg.Trim() == "")
            {
                FillGrid();
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

}