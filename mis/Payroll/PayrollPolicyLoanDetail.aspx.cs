using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollPolicyLoanDetail : System.Web.UI.Page
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
                ViewState["Loan_ID"] = "0";
                txtLoan_StartDate.Attributes.Add("readonly", "readonly");
                txtLoan_EndDate.Attributes.Add("readonly", "readonly");
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
            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
                if (ddlOfficeName.SelectedIndex > 0)
                {
                    FillEmployee();
                    FillEarnDeduction();
                }

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
            lblMsg.Text = "";
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollLoanDetail", new string[] { "flag", "Emp_ID" }, new string[] { "2", ddlEmployee.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            FillEmployee();
            FillEarnDeduction();
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            if (ddlOfficeName.SelectedIndex > 0)
            {
                DataSet ds1 = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.DataSource = ds1;
                    ddlEmployee.DataTextField = "Emp_Name";
                    ddlEmployee.DataValueField = "Emp_ID";
                    ddlEmployee.DataBind();
                    ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEarnDeduction()
    {
        try
        {
            if (ddlOfficeName.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpPayrollLoanDetail", new string[] { "flag", "Office_ID" }, new string[] { "0", ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlEarnDeducHead.Items.Clear();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }


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
            string msg = "";
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name. \\n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (ddlEarnDeducHead.SelectedIndex == 0)
            {
                msg += "Select Loan Head. \\n";
            }
            if (txtLoan_No.Text == "")
            {
                msg += "Enter Loan No. \\n";
            }

            if (txtLoan_PremiumAmount.Text == "")
            {
                msg += "Enter Premium Amount. \\n";
            }
            else if (!objdb.isDecimal(txtLoan_PremiumAmount.Text))
            {
                msg += "Enter Correct Premium Amount. \\n";
            }

            if (txtRemainingLoanAmount.Text == "")
            {
                msg += "Enter Remaining Amount. \\n";
            }
            else if (!objdb.isDecimal(txtRemainingLoanAmount.Text))
            {
                msg += "Enter Correct Remainig Amount. \\n";
            }

            if (txtLoan_StartDate.Text == "")
            {
                msg += "Select Remainig As On Date. \\n";
            }
            if (txtLoan_EndDate.Text == "")
            {
                msg += "Enter End Date. \\n";
            }
            if (ddlLoan_IsActive.SelectedIndex == 0)
            {
                msg += "Select Status. \\n";
            }


            if (msg.Trim() == "")
            {
                //int Status = 0;
                //ds = objdb.ByProcedure("SpPayrollLoanDetail", new string[] { "flag", "Loan_ID", "Loan_No" }, new string[] { "4", ViewState["Loan_ID"].ToString(), txtLoan_No.Text }, "dataset");
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                //}

                if (btnSave.Text == "Save" && ViewState["Loan_ID"].ToString() == "0")
                {
                    objdb.ByProcedure("SpPayrollLoanDetail",
                    new string[] { "flag", "Loan_IsActive", "Emp_ID", "Office_ID", "Loan_Type", "Loan_No", "Loan_Head", "Loan_TotalAmount", "Loan_PaidInstallment", "Loan_BalanceAmount", "Loan_StartDate", "Loan_EndDate", "Loan_Remark","Loan_Premium", "Loan_UpdatedBy" },
                    new string[] { "1", ddlLoan_IsActive.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(),"",txtLoan_No.Text, ddlEarnDeducHead.SelectedValue.ToString(),
                        txtTotalAmount.Text,txtNoOfPaidInstallment.Text, txtRemainingLoanAmount.Text, Convert.ToDateTime(txtLoan_StartDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtLoan_EndDate.Text, cult).ToString("yyyy/MM/dd"), txtLoanRemark.Text,txtLoan_PremiumAmount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    ClearText();
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else if (btnSave.Text == "Edit" && ViewState["Loan_ID"].ToString() != "0")
                {
                    objdb.ByProcedure("SpPayrollLoanDetail",
                    new string[] { "flag","Loan_ID", "Loan_IsActive","Loan_Head", "Loan_TotalAmount", "Loan_PaidInstallment", "Loan_BalanceAmount", "Loan_StartDate", "Loan_EndDate", "Loan_Remark","Loan_Premium", "Loan_UpdatedBy" },
                    new string[] { "5",ViewState["Loan_ID"].ToString(), ddlLoan_IsActive.SelectedValue.ToString(),ddlEarnDeducHead.SelectedValue.ToString(),
                        txtTotalAmount.Text,txtNoOfPaidInstallment.Text, txtRemainingLoanAmount.Text, Convert.ToDateTime(txtLoan_StartDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtLoan_EndDate.Text, cult).ToString("yyyy/MM/dd"), txtLoanRemark.Text ,txtLoan_PremiumAmount.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    ClearText();
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Policy No already exist.');", true);
                }
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            ViewState["Loan_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpPayrollLoanDetail", new string[] { "flag", "Loan_ID" }, new string[] { "3", ViewState["Loan_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                ddlEarnDeducHead.ClearSelection();
                ddlEarnDeducHead.Items.FindByValue(ds.Tables[0].Rows[0]["Loan_Head"].ToString()).Selected = true;
                txtLoan_No.Text = ds.Tables[0].Rows[0]["Loan_No"].ToString();
                txtLoan_PremiumAmount.Text = ds.Tables[0].Rows[0]["Loan_Premium"].ToString();
                txtLoan_StartDate.Text = ds.Tables[0].Rows[0]["Loan_StartDate"].ToString();
                txtLoan_EndDate.Text = ds.Tables[0].Rows[0]["Loan_EndDate"].ToString();
                txtTotalAmount.Text = ds.Tables[0].Rows[0]["Loan_TotalAmount"].ToString();
                txtNoOfPaidInstallment.Text = ds.Tables[0].Rows[0]["Loan_PaidInstallment"].ToString();
                txtRemainingLoanAmount.Text = ds.Tables[0].Rows[0]["Loan_BalanceAmount"].ToString();
                txtLoanRemark.Text = ds.Tables[0].Rows[0]["Loan_Remark"].ToString(); 
          
            

                btnSave.Text = "Edit";
                FunDesable();
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

            txtLoan_No.Text = "";
            txtLoan_PremiumAmount.Text = "";
            txtLoan_StartDate.Text = "";
            txtLoan_EndDate.Text = "";
            ddlLoan_IsActive.ClearSelection();            
            FunEnable();
            ViewState["Policy_ID"] = "0";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FunEnable()
    {
        try
        {
            ddlOfficeName.Enabled = true;
            ddlEmployee.Enabled = true;
            txtLoan_No.ReadOnly = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FunDesable()
    {
        try
        {
            ddlOfficeName.Enabled = false;
            ddlEmployee.Enabled = false;
            txtLoan_No.ReadOnly = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}