using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEPFExternal : System.Web.UI.Page
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
                ViewState["Emp_IDExt"] = "0";
                txtDOB.Attributes.Add("readonly", "readonly");
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblMsg.Text = "";

            ds = objdb.ByProcedure("spPayrollExternalEmployee", new string[] { "flag", "Office_ID" }, new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
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

            if (txtDOB.Text == "")
            {
                msg += "Select Remainig As On Date. \\n";
            }
            if (msg.Trim() == "")
            {
                if (btnSave.Text == "Save" && ViewState["Emp_IDExt"].ToString() == "0")
                {
                    objdb.ByProcedure("spPayrollExternalEmployee",
                    new string[] { "flag", "IsActive", "Emp_Name", "Office_ID", "UAN", "EPF", "OtherInfo", "UpdatedBy", "DOB", "EmpNo", "SectionNo", "IsPension" },
                    new string[] { "0", ddlIsEPF.SelectedValue.ToString(), txtEmp_Name.Text.ToString(), ViewState["Office_ID"].ToString(), txtUAN.Text.ToString(), txtEPF.Text.ToString(), txtOtherInfo.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtDOB.Text, cult).ToString("yyyy/MM/dd"), txtEMP.Text.ToString(), txtSection.Text.ToString(), ddlIsEPS.SelectedValue.ToString() }, "dataset");
                    ClearText();
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else if (btnSave.Text == "Edit" && ViewState["Emp_IDExt"].ToString() != "0")
                {
                    objdb.ByProcedure("spPayrollExternalEmployee",
                new string[] { "flag", "IsActive", "Emp_Name", "Emp_ID", "UAN", "EPF", "OtherInfo", "UpdatedBy", "DOB", "Office_ID", "EmpNo", "SectionNo", "IsPension" },
                new string[] { "5", ddlIsEPF.SelectedValue.ToString(), txtEmp_Name.Text.ToString(), ViewState["Emp_IDExt"].ToString(), txtUAN.Text.ToString(), txtEPF.Text.ToString(), txtOtherInfo.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtDOB.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), txtEMP.Text.ToString(), txtSection.Text.ToString(), ddlIsEPS.SelectedValue.ToString() }, "dataset");
                    ClearText();
                    FillGrid();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
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
            ViewState["Emp_IDExt"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("spPayrollExternalEmployee", new string[] { "flag", "Emp_ID", "Office_ID" }, new string[] { "4", ViewState["Emp_IDExt"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlIsEPF.ClearSelection();
                ddlIsEPF.Items.FindByValue(ds.Tables[0].Rows[0]["IsActive"].ToString()).Selected = true;
                txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                txtEmp_Name.Text = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                txtEPF.Text = ds.Tables[0].Rows[0]["EPF"].ToString();
                txtOtherInfo.Text = ds.Tables[0].Rows[0]["OtherInfo"].ToString();
                txtUAN.Text = ds.Tables[0].Rows[0]["UAN"].ToString();
                txtEMP.Text = ds.Tables[0].Rows[0]["EmpNo"].ToString();
                txtSection.Text = ds.Tables[0].Rows[0]["SectionNo"].ToString();
                ddlIsEPS.ClearSelection();
                ddlIsEPS.Items.FindByValue(ds.Tables[0].Rows[0]["IsPension"].ToString()).Selected = true;
                btnSave.Text = "Edit";
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

            txtEmp_Name.Text = "";
            txtUAN.Text = "";
            txtDOB.Text = "";
            txtEPF.Text = "";
            txtSection.Text = "";
            txtEMP.Text = "";
            txtOtherInfo.Text = "";
            ViewState["Emp_IDExt"] = "0";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}