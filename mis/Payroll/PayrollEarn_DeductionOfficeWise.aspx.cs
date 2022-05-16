using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEarn_DeductionOfficeWise : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    DivDetail.Visible = false;
                    FillDropDown();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
                ddlOfficeName.Enabled = false;
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOfficeName.Enabled = true;
                }
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
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office.\\n";
            }
            if (ddlEarnDeduction_Type.SelectedIndex == 0)
            {
                msg += "Select Earning Or Deduction.\\n";
            }
            if (msg == "")
            {
                string EarnDeduction_Office = ddlOfficeName.SelectedValue.ToString();
                string EarnDeduction_Type = ddlEarnDeduction_Type.SelectedValue.ToString();

                string LoginUserID = ViewState["Emp_ID"].ToString();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    Label lblRowNumber = (Label)gr.FindControl("lblRowNumber");
                    Label lblEarnDeduction_Name = (Label)gr.FindControl("lblEarnDeduction_Name");
                    Label lblEarnDeduction_Calculation = (Label)gr.FindControl("lblEarnDeduction_Calculation");
                    Label lblCalculationMethod = (Label)gr.FindControl("lblCalculationMethod");
                    TextBox txtHeadNameInOffice = (TextBox)gr.FindControl("txtHeadNameInOffice");
                    TextBox txtHeadNameOrderNo = (TextBox)gr.FindControl("txtHeadNameOrderNo");

                    string EarnDeduction_IsActive = "0";
                    if (chkSelect.Checked == true)
                    {
                        EarnDeduction_IsActive = "1";
                    }

                    string ContributionType = "";
                    if (EarnDeduction_Type == "Deduction")
                    {
                        //GridView1.Columns[10].Visible = false;
                        DropDownList ddlContribution = (DropDownList)gr.FindControl("ddlContribution");
                        ContributionType = ddlContribution.SelectedValue.ToString();
                    }
                    else
                    {
                        ContributionType = "Earning";
                    }

                    string CalculationMethod = "";
                    DropDownList ddlCalculationType = (DropDownList)gr.FindControl("ddlCalculationType");
                    CalculationMethod = ddlCalculationType.SelectedValue.ToString();

                    objdb.ByProcedure("SpPayrollEarnDedOfficeWiseMaster",
                    new string[] { "flag", "EarnDeduction_ID", "EarnDeduction_IsActive", "Office_Id", "EarnDeduction_Type", "EarnDeduction_NameActual", "EarnDeduction_Calculation", "CalculationMethod", "EarnDeduction_UpdatedBy", "EarnDeduction_Name", "EarnDeduction_OrderNo", "ContributionType" },
                    new string[] { "0", lblRowNumber.ToolTip.ToString(), EarnDeduction_IsActive, EarnDeduction_Office, EarnDeduction_Type, lblEarnDeduction_Name.Text.ToString(), lblEarnDeduction_Calculation.Text, CalculationMethod, LoginUserID, txtHeadNameInOffice.Text.ToString(), txtHeadNameOrderNo.Text.ToString(), ContributionType }, "dataset");

                }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DivDetail.Visible = false;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Columns[3].Visible = true;
            if (ddlOfficeName.SelectedIndex > 0 && ddlEarnDeduction_Type.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollEarnDedOfficeWiseMaster",
                new string[] { "flag", "EarnDeduction_Type", "Office_Id" },
                new string[] { "1", ddlEarnDeduction_Type.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    if (ddlEarnDeduction_Type.SelectedValue.ToString() == "Earning")
                    {
                       GridView1.Columns[3].Visible = false;
                    }
                    else
                    {
                        int i = 0;
                        foreach (GridViewRow gr in GridView1.Rows)
                        {
                            DropDownList ddlContribution = (DropDownList)gr.FindControl("ddlContribution");
                            ddlContribution.Items.FindByValue(ds.Tables[0].Rows[i]["ContributionType"].ToString()).Selected = true;                           

                            i++;
                        }

                    }

                     int j = 0;
                     foreach (GridViewRow gr in GridView1.Rows)
                     {
                         DropDownList CalculationType = (DropDownList)gr.FindControl("ddlCalculationType");
                         CalculationType.Items.FindByValue(ds.Tables[0].Rows[j]["CalculationMethod"].ToString()).Selected = true;
                        j++;
                     }
                    
                    Label header = (Label)GridView1.HeaderRow.FindControl("lblHeader");
                    header.Text = ddlEarnDeduction_Type.SelectedValue.ToString();
                    DivDetail.Visible = true;
                }
            }

            /*******************************************************/
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            if (ddlOfficeName.SelectedIndex > 0 && ddlEarnDeduction_Type.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpPayrollEarnDedOfficeWiseMaster",
                new string[] { "flag", "EarnDeduction_Type", "Office_Id" },
                new string[] { "2", ddlEarnDeduction_Type.SelectedValue.ToString(), ddlOfficeName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridView3.DataSource = ds.Tables[1];
                    GridView3.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}