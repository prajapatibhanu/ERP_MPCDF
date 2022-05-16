using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_dailyplan_ProductQTMaster : System.Web.UI.Page
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
                ViewState["QCParameterID"] = "0";
                FillDropdown();

                ddlMinValue.Visible = false;
                ddlMaxValue.Visible = false;
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

            ds = objdb.ByProcedure("spProductionQCProductMapping", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlProduct.DataSource = ds;
                ddlProduct.DataTextField = "Product_Name";
                ddlProduct.DataValueField = "Product_ID";
                ddlProduct.DataBind();
            }
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlCalculationMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DivUnit.Visible = false;
            txtMinValue.Enabled = true;
            txtMaxValue.Enabled = true;
            ddlMinValue.Enabled = true;
            ddlMaxValue.Enabled = true;
            if (ddlCalculationMethod.SelectedValue.ToString() == "Value")
            {
                txtMinValue.Visible = true;
                txtMaxValue.Visible = true;
                ddlMinValue.Visible = false;
                ddlMaxValue.Visible = false;
                DivUnit.Visible = true;

            }
            else if (ddlCalculationMethod.SelectedValue.ToString() == "Option")
            {
                txtMinValue.Visible = false;
                txtMaxValue.Visible = false;
                ddlUnit.ClearSelection();
                DivUnit.Visible = false;
                ddlMinValue.Visible = true;
                ddlMaxValue.Visible = true;
            }
            else
            {
                txtMinValue.Enabled = false;
                txtMaxValue.Enabled = false;
                ddlMinValue.Enabled = false;
                ddlMaxValue.Enabled = false;

                txtMinValue.Text ="";
                txtMaxValue.Text ="";
                ddlMinValue.ClearSelection();
                ddlMaxValue.ClearSelection();
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
            // ddlProduct.ClearSelection();
            txtMinValue.Enabled = true;
            txtMaxValue.Enabled = true;
            ddlMinValue.Enabled = true;
            ddlMaxValue.Enabled = true;


            ddlCalculationMethod.ClearSelection();
            txtParameter.Text = "";
            txtMinValue.Text = "";
            txtMaxValue.Text = "";
            ddlMinValue.ClearSelection();
            ddlMaxValue.ClearSelection();
            txtMinValue.Visible = true;
            txtMaxValue.Visible = true;
            ddlMinValue.Visible = false;
            ddlMaxValue.Visible = false;
            ddlUnit.ClearSelection();
            DivUnit.Visible = true;
            btnSubmit.Text = "Submit";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlProduct.SelectedIndex <= 0)
            {
                msg = "Select Product Name";
            }
            if (txtParameter.Text == "")
            {
                msg += "Enter Test Parameter Name. \\n";
            }
            if (ddlCalculationMethod.SelectedValue.ToString() == "Value")
            {
                if (txtMinValue.Text == "")
                {
                    msg += "Enter Min. Value. \\n";
                }
                if (txtMaxValue.Text == "")
                {
                    msg += "Enter Max. Value. \\n";
                }
                if (ddlUnit.SelectedIndex <= 0)
                {
                    msg += "Select Unit. \\n";
                }
            }
            else if (ddlCalculationMethod.SelectedValue.ToString() == "Option")
            {
                if (ddlMinValue.SelectedIndex <= 0)
                {
                    msg += "Select Min. Value. \\n";
                }
                if (ddlMaxValue.SelectedIndex <= 0)
                {
                    msg += "Select Max. Value. \\n";
                }
            }
            if (msg.Trim() == "")
            {
                string MinValue = "";
                string MaxValue = "";
                string Unit = "";
                if (ddlCalculationMethod.SelectedValue.ToString() == "Value")
                {
                    MinValue = txtMinValue.Text;
                    MaxValue = txtMaxValue.Text;
                    Unit = ddlUnit.SelectedValue.ToString();
                    if (float.Parse(MinValue) > float.Parse(MaxValue))
                    {
                        msg += "Details not submitted because available Max. Value is less than Min. Value. \\n";
                    }
                }
                else if (ddlCalculationMethod.SelectedValue.ToString() == "Option")
                {
                    MinValue = ddlMinValue.SelectedValue.ToString();
                    MaxValue = ddlMaxValue.SelectedValue.ToString();
                    if (ddlMinValue.SelectedIndex > ddlMaxValue.SelectedIndex)
                    {
                        msg += "Details not submitted because available Max. Value is less than Min. Value. \\n";
                    }
                }
                if (msg.Trim() == "")
                {
                    if (btnSubmit.Text == "Submit" && ViewState["QCParameterID"].ToString() == "0")
                    {
                        ds = objdb.ByProcedure("spProductionQCProductMapping",
                        new string[] { "flag", "Office_ID", "ProductID", "QCParameterName", "CalculationMethod", "MinValue", "MaxValue", "Unit", "UpdatedBy", "IsActive" },
                        new string[] { "0", ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString(), txtParameter.Text, ddlCalculationMethod.SelectedValue.ToString(), MinValue, MaxValue, Unit, ViewState["Emp_ID"].ToString(), "1" }, "dataset");

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                            {
                                ViewState["QCParameterID"] = "0";
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                                ClearText();
                                FillGrid();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Parameter Name already exist.');", true);
                            }
                        }



                    }
                    else if (btnSubmit.Text == "Update" && ViewState["QCParameterID"].ToString() != "0")
                    {
                        ds = objdb.ByProcedure("spProductionQCProductMapping",
                       new string[] { "flag", "QCParameterID", "Office_ID", "ProductID", "QCParameterName", "CalculationMethod", "MinValue", "MaxValue", "Unit", "UpdatedBy", "IsActive" },
                       new string[] { "3", ViewState["QCParameterID"].ToString(), ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString(), txtParameter.Text, ddlCalculationMethod.SelectedValue.ToString(), MinValue, MaxValue, Unit, ViewState["Emp_ID"].ToString(), "1" }, "dataset");

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                            {
                                ViewState["QCParameterID"] = "0";
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                                ClearText();
                                FillGrid();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Test Parameter Name already exist.');", true);
                            }
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddlProduct.SelectedIndex > 0)
        {

            FillGrid();
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlProduct.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("spProductionQCProductMapping"
                , new string[] { "flag", "Office_ID", "ProductID" }
                , new string[] { "6", ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString() }, "dataset");
                if (ds != null)
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            ViewState["QCParameterID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("spProductionQCProductMapping", new string[] { "flag", "QCParameterID" }, new string[] { "5", ViewState["QCParameterID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtParameter.Text = ds.Tables[0].Rows[0]["QCParameterName"].ToString();
                ddlCalculationMethod.Items.FindByValue(ds.Tables[0].Rows[0]["CalculationMethod"].ToString()).Selected = true;

                if (ddlCalculationMethod.SelectedValue.ToString() == "Value")
                {
                    txtMinValue.Visible = true;
                    txtMaxValue.Visible = true;
                    DivUnit.Visible = true;
                    ddlMinValue.Visible = false;
                    ddlMaxValue.Visible = false;
                    txtMinValue.Text = ds.Tables[0].Rows[0]["MinValue"].ToString();
                    txtMaxValue.Text = ds.Tables[0].Rows[0]["MaxValue"].ToString();
                    if (ds.Tables[0].Rows[0]["Unit"].ToString() != "")
                        ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit"].ToString()).Selected = true;

                }
                else if (ddlCalculationMethod.SelectedValue.ToString() == "Option")
                {
                    txtMinValue.Visible = false;
                    txtMaxValue.Visible = false;
                    DivUnit.Visible = false;
                    ddlMinValue.Visible = true;
                    ddlMaxValue.Visible = true;
                    ddlMinValue.Items.FindByValue(ds.Tables[0].Rows[0]["MinValue"].ToString()).Selected = true;
                    ddlMaxValue.Items.FindByValue(ds.Tables[0].Rows[0]["MaxValue"].ToString()).Selected = true;
                }
                else
                {
                    txtMinValue.Enabled = false;
                    txtMaxValue.Enabled = false;
                    ddlMinValue.Enabled = false;
                    ddlMaxValue.Enabled = false;
                }


                btnSubmit.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string QCParameterID = chk.ToolTip.ToString();
            string IsActive = "0";
            if (chk != null & chk.Checked)
            {
                IsActive = "1";
            }
            objdb.ByProcedure("spProductionQCProductMapping",
                       new string[] { "flag", "IsActive", "QCParameterID", "UpdatedBy" },
                       new string[] { "4", IsActive, QCParameterID, ViewState["Emp_ID"].ToString() }, "dataset");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

}
