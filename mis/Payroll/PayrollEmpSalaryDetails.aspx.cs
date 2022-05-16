using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpSalaryDetails : System.Web.UI.Page
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
                Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());
                DivDetail.Visible = false;
                FillDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ViewState["UPage"] = Session["Page"];
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
            DivDetail.Visible = false;
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Year", "MonthNo", "Office_ID", "Emp_TypeOfPost" }, new string[] { "1", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString(), ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                DivDetail.Visible = true;
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
            if (ddlYear.SelectedIndex > 0 && ddlOfficeName.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0 && ddlEmp_TypeOfPost.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnGenerated_Click(object sender, EventArgs e)
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

                string LoginUserID = ViewState["Emp_ID"].ToString();
                string Office_ID = ViewState["Office_ID"].ToString();
                if (ViewState["UPage"].ToString() == Session["Page"].ToString())
                {
                    foreach (GridViewRow gr in GridView1.Rows)
                    {

                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        Label lblRowNumber = (Label)gr.FindControl("lblRowNumber");
                        Label lblSalary_Basic = (Label)gr.FindControl("lblSalary_Basic");
                        Label lblSalary_BasicPayScale = (Label)gr.FindControl("lblSalary_BasicPayScale");
                        Label lblSalary_PayableDays = (Label)gr.FindControl("lblSalary_PayableDays");
                        Label lblSalary_EarningTotal = (Label)gr.FindControl("lblSalary_EarningTotal");
                        Label lblSalary_DeductionTotal = (Label)gr.FindControl("lblSalary_DeductionTotal");
                        Label lblSalary_NetSalary = (Label)gr.FindControl("lblSalary_NetSalary");
                        Label lblSalary_NoDayDeduAmt = (Label)gr.FindControl("lblSalary_NoDayDeduAmt");
                        Label lblSalary_NoDayEarnAmt = (Label)gr.FindControl("lblSalary_NoDayEarnAmt");
                        Label lblGenStatus = (Label)gr.FindControl("lblGenStatus");
                        if (chkSelect.Checked == true && lblGenStatus.Text != "Generated")
                        {
                            objdb.ByProcedure("SpPayrollSalaryDetail",
                            new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo", "Salary_Month", "Salary_Basic", "Salary_BasicPayScale", "Salary_PayableDays", "Salary_EarningTotal", "Salary_DeductionTotal", "Salary_NetSalary", "Salary_NoDayDeduAmt", "Salary_NoDayEarnAmt", "Salary_UpdatedBy" },
                            new string[] { "0", lblRowNumber.ToolTip.ToString(), Office_ID, Year, MonthNo, Month, lblSalary_Basic.Text, lblSalary_BasicPayScale.Text, lblSalary_PayableDays.Text, lblSalary_EarningTotal.Text, lblSalary_DeductionTotal.Text, lblSalary_NetSalary.Text, lblSalary_NoDayDeduAmt.Text, lblSalary_NoDayEarnAmt.Text, LoginUserID }, "dataset");
                        }
                    }

                    Session["Page"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
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
    /*******New PP**********/
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["Office_ID"] = ddlOfficeName.SelectedItem.Value;
        DivDetail.Visible = false;
        FillDropdown();

    }
}