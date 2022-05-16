using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_QCLabTest : System.Web.UI.Page
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
                FillOffice();
                FillShift();
                FillProductSection();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
                ddlTestReport.Enabled = false;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillShift()
    {
        try
        {
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProductSection()
    {
        try
        {

            lblMsg.Text = "";
            ddlProductSection.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillProductSection();
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            ViewState["SelectedOffice"] = ddlDS.SelectedValue.ToString();
            ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            ViewState["Shift_ID"] = ddlShift.SelectedValue.ToString();
            lblSelectedDate1.Text = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("dd-MM-yyyy");
            //ViewState["BatchNo"] = txtBatchNo.Text.ToString();
            //ViewState["LotNo"] = txtLotNo.Text.ToString();
            //ViewState["Remarks"] = txtRemark.Text.ToString();

            ds = objdb.ByProcedure("spProduction_LabTesting", new string[] { "flag", "SampleBy_Office", "SampleBy_Section", "SampleDate", "Shift_ID" }
                , new string[] { "1", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            if (ds != null)
            {
                btnSave.Visible = true;
            }

            /********************************/

            lblSelectedDate.Text = Convert.ToDateTime(ViewState["TranDt"], cult).ToString("dd-MM-yyyy");
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();
            ds = null;
            ds = objdb.ByProcedure("spProduction_LabTesting", new string[] { "flag", "SampleBy_Office", "SampleBy_Section", "SampleDate", "Shift_ID" }, new string[] { "2", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ItmStock_id = GridView2.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spProduction_LabTesting",
                   new string[] { "flag", "LabTest_ID" },
                   new string[] { "4", ItmStock_id }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
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
            string msg = "";
            string Result = "Pass";
            lblMsg.Text = "";
            foreach (GridViewRow gr in GridView4.Rows)
            {
                Label lblMinValueValid = (Label)gr.FindControl("lblMinValueValid");
                Label lblMaxValueValid = (Label)gr.FindControl("lblMaxValueValid");
                Label lblCalculationMethod = (Label)gr.FindControl("lblCalculationMethod");
                Label lblQCParameterName = (Label)gr.FindControl("lblQCParameterName");
                TextBox txtLabTestResult = (TextBox)gr.FindControl("txtLabTestResult");
                TextBox txtDateLabTestResult = (TextBox)gr.FindControl("txtDateLabTestResult");
                DropDownList ddlLabTestResult = (DropDownList)gr.FindControl("ddlLabTestResult");


                float MinValue = 0;
                float MaxValue = 0;
                if (lblMinValueValid.Text != "")
                    MinValue = float.Parse(lblMinValueValid.Text);
                if (lblMaxValueValid.Text != "")
                    MaxValue = float.Parse(lblMaxValueValid.Text);

                if (lblCalculationMethod.Text == "Value")
                {
                    if (txtLabTestResult.Text == "")
                    {
                        msg += "Enter " + lblQCParameterName.Text + " Result. \\n";
                        Result = "Fail";
                    }
                    else
                    {

                        float TestResult = float.Parse(txtLabTestResult.Text);
                        if ((TestResult < MinValue) || (TestResult > MaxValue))
                        {
                            Result = "Fail";
                        }

                    }

                }
                else if (lblCalculationMethod.Text == "Option")
                {
                    if (ddlLabTestResult.SelectedIndex <= 0)
                    {
                        msg += "Select " + lblQCParameterName.Text + " Result. \\n";
                        Result = "Fail";
                    }
                    else
                    {

                        float TestResult = float.Parse(ddlLabTestResult.SelectedIndex.ToString());
                        if ((TestResult < MinValue) || (TestResult > MaxValue))
                        {
                            Result = "Fail";
                        }
                    }
                }
                else if (lblCalculationMethod.Text == "Manufacturing Date")
                {
                    if (txtDateLabTestResult.Text == "")
                    {
                        msg += "Select Date for " + lblQCParameterName.Text + ". \\n";

                    }
                }
            }
            if (msg.Trim() == "")
            {
                ddlTestReport.ClearSelection();
                ddlTestReport.Items.FindByValue(Result).Selected = true;

                string SampleBy_Office = ViewState["Office_ID"].ToString();
                string LabTest_ID = ViewState["LabTest_ID"].ToString();
                string SampleBy_Section = ddlProductSection.SelectedValue.ToString();
                string Item_ID = ViewState["Item_ID"].ToString();
                string Tester_ID = ViewState["Emp_ID"].ToString();
                foreach (GridViewRow gr in GridView4.Rows)
                {
                    Label lblQCParameterID = (Label)gr.FindControl("lblQCParameterID");
                    Label lblQCParameterName = (Label)gr.FindControl("lblQCParameterName");
                    Label lblCalculationMethod = (Label)gr.FindControl("lblCalculationMethod");

                    Label lblMinValue = (Label)gr.FindControl("lblMinValue");
                    Label lblMaxValue = (Label)gr.FindControl("lblMaxValue");
                    TextBox txtLabTestResult = (TextBox)gr.FindControl("txtLabTestResult");
                    TextBox txtDateLabTestResult = (TextBox)gr.FindControl("txtDateLabTestResult");
                    DropDownList ddlLabTestResult = (DropDownList)gr.FindControl("ddlLabTestResult");

                    string TestResultValue = "";
                    if (lblCalculationMethod.Text == "Value")
                    {
                        TestResultValue = txtLabTestResult.Text;
                    }
                    else if (lblCalculationMethod.Text == "Manufacturing Date")
                    {
                        TestResultValue = txtDateLabTestResult.Text;
                    }
                    else
                    {
                        TestResultValue = ddlLabTestResult.SelectedValue.ToString();
                    }

                    objdb.ByProcedure("spProduction_LabTesting",
                            new string[] { "flag", "LabTest_ID", "SampleBy_Office", "SampleBy_Section", "Item_ID", "QCParameterID", "QCParameterName", "CalculationMethod", "MinValue", "MaxValue", "TestResultValue", "Tester_ID" },
                            new string[] { "6", LabTest_ID, SampleBy_Office, SampleBy_Section, Item_ID, lblQCParameterID.Text, lblQCParameterName.Text, lblCalculationMethod.Text, lblMinValue.Text, lblMaxValue.Text, TestResultValue, Tester_ID }, "dataset");
                }



                ds = objdb.ByProcedure("spProduction_LabTesting",
                    new string[] { "flag", "Test_Result", "Tester_ID", "Test_Type", "LabTest_ID" },
                    new string[] { "3", Result, ViewState["Emp_ID"].ToString(), "QC", ViewState["LabTest_ID"].ToString() }, "dataset");



                lblMsgTest.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun('')", true);

                FillGrid();
                btnSave.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun('')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun('" + msg + "')", true);

            }






        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblMsgTest.Text = "";
            //GridView3.DataSource = new string[] { };
            //GridView3.DataBind();
            GridView4.DataSource = null;
            GridView4.DataBind();
            lblProductName.Text = "";
            lblMsgTest.Text = "";
            btnSave.Visible = true;
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label lblGLabTest_ID = (Label)row.Cells[0].FindControl("lblGLabTest_ID");
                Label lblGItem_id = (Label)row.Cells[0].FindControl("lblGItem_id");
                Label lblGSampleBy_Section = (Label)row.Cells[0].FindControl("lblGSampleBy_Section");
                Label lblGSampleDate = (Label)row.Cells[0].FindControl("lblGSampleDate");
                Label lblGItemName = (Label)row.Cells[1].FindControl("lblGItemName");
                Label lblGSampleQuantity = (Label)row.Cells[2].FindControl("lblGSampleQuantity");

                ViewState["LabTest_ID"] = lblGLabTest_ID.Text;
                ViewState["Item_ID"] = lblGItem_id.Text;
                lblProductName.Text = "Product : " + lblGItemName.Text + "<br/>Sample Date : " + lblGSampleDate.Text + "<br/> SampleQuantity : " + lblGSampleQuantity.Text + "";

                ds = objdb.ByProcedure("spProduction_LabTesting",
                   new string[] { "flag", "LabTest_ID", "SampleBy_Office", "ProductID", "SampleBy_Section" },
                   new string[] { "5", ViewState["LabTest_ID"].ToString(), ViewState["Office_ID"].ToString(), lblGItem_id.Text, ddlProductSection.SelectedValue.ToString() }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    //GridView3.DataSource = ds.Tables[0];
                    //GridView3.DataBind();
                    GridView4.DataSource = ds.Tables[0];
                    GridView4.DataBind();
                    foreach (GridViewRow gr in GridView4.Rows)
                    {

                        Label lblCalculationMethod = (Label)gr.FindControl("lblCalculationMethod");
                        Label lblTestResultValue = (Label)gr.FindControl("lblTestResultValue");

                        TextBox txtLabTestResult = (TextBox)gr.FindControl("txtLabTestResult");
                        TextBox txtDateLabTestResult = (TextBox)gr.FindControl("txtDateLabTestResult");
                        DropDownList ddlLabTestResult = (DropDownList)gr.FindControl("ddlLabTestResult");
                        txtLabTestResult.Visible = false;
                        txtDateLabTestResult.Visible = false;
                        ddlLabTestResult.Visible = false;
                        if (lblCalculationMethod.Text == "Value")
                        {
                            txtLabTestResult.Visible = true;
                            txtLabTestResult.Text = lblTestResultValue.Text;
                        }
                        else if (lblCalculationMethod.Text == "Option")
                        {
                            ddlLabTestResult.Visible = true;
                            ddlLabTestResult.Items.Insert(0, new ListItem("Select", "0"));
                            ddlLabTestResult.Items.Insert(1, new ListItem("Curd", "Curd"));
                            ddlLabTestResult.Items.Insert(2, new ListItem("Sour", "Sour"));
                            ddlLabTestResult.Items.Insert(3, new ListItem("Slightly Off Taste", "Slightly Off Taste"));
                            ddlLabTestResult.Items.Insert(4, new ListItem("Off Taste", "Off Taste"));
                            ddlLabTestResult.Items.Insert(5, new ListItem("Satisfactory", "Satisfactory"));
                            ddlLabTestResult.Items.Insert(6, new ListItem("Good", "Good"));
                            if (txtDateLabTestResult.Text != "")
                                ddlLabTestResult.Items.FindByValue(lblTestResultValue.Text).Selected = true;
                        }
                        else if (lblCalculationMethod.Text == "Use By Date")
                        {
                            ddlLabTestResult.Visible = true;
                            ddlLabTestResult.Items.Insert(0, new ListItem("No", "No"));
                            ddlLabTestResult.Items.Insert(1, new ListItem("Yes", "Yes"));
                            if (txtDateLabTestResult.Text != "")
                                ddlLabTestResult.Items.FindByValue(lblTestResultValue.Text).Selected = true;
                        }
                        else if (lblCalculationMethod.Text == "Manufacturing Date")
                        {
                            txtDateLabTestResult.Visible = true;
                            txtDateLabTestResult.Text = lblTestResultValue.Text;

                        }
                    }

                }

                if (ds != null && ds.Tables[1].Rows.Count != 0)
                {
                    ddlTestReport.Items.Clear();
                    //ddlTestReport.Items.Insert(0, new ListItem("Select", "0"));
                    ddlTestReport.Items.Insert(0, new ListItem("Pending", "Pending"));
                    ddlTestReport.Items.Insert(1, new ListItem("Pass", "Pass"));
                    ddlTestReport.Items.Insert(2, new ListItem("Fail", "Fail"));
                    ddlTestReport.SelectedValue = ds.Tables[1].Rows[0]["Test_Result"].ToString();
                }

                //if (ds != null && ds.Tables[2].Rows.Count != 0)
                //{
                //    lblProductName.Text = "Product : " + ds.Tables[2].Rows[0]["ItemName"].ToString() + "<br/>Sample Date : " + ds.Tables[2].Rows[0]["SampleDate"].ToString() + "<br/> SampleQuantity : " + ds.Tables[2].Rows[0]["SampleQuantity"].ToString() + "";
                //}

                //lblMsgTest.Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun('')", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnCalculateResult_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string Result = "Pass";
            lblMsg.Text = "";
            foreach (GridViewRow gr in GridView4.Rows)
            {
                Label lblMinValueValid = (Label)gr.FindControl("lblMinValueValid");
                Label lblMaxValueValid = (Label)gr.FindControl("lblMaxValueValid");
                Label lblCalculationMethod = (Label)gr.FindControl("lblCalculationMethod");
                Label lblQCParameterName = (Label)gr.FindControl("lblQCParameterName");
                TextBox txtLabTestResult = (TextBox)gr.FindControl("txtLabTestResult");
                DropDownList ddlLabTestResult = (DropDownList)gr.FindControl("ddlLabTestResult");

                float MinValue=0;
                float MaxValue=0;
                if (lblMinValueValid.Text != "")
                    MinValue = float.Parse(lblMinValueValid.Text);
                if (lblMaxValueValid.Text != "")
                    MaxValue = float.Parse(lblMaxValueValid.Text);

                if (lblCalculationMethod.Text == "Value")
                {
                    if (txtLabTestResult.Text == "")
                    {
                        msg += "Enter " + lblQCParameterName.Text + " Result. \\n";
                        Result = "Fail";
                    }
                    else
                    {

                        float TestResult = float.Parse(txtLabTestResult.Text);
                        if ((TestResult < MinValue) || (TestResult > MaxValue))
                        {
                            Result = "Fail";
                        }

                    }

                }
                else if (lblCalculationMethod.Text == "Option")
                {
                    if (ddlLabTestResult.SelectedIndex <= 0)
                    {
                        msg += "Select " + lblQCParameterName.Text + " Result. \\n";
                        Result = "Fail";
                    }
                    else
                    {

                        float TestResult = float.Parse(ddlLabTestResult.SelectedIndex.ToString());
                        if ((TestResult < MinValue) || (TestResult > MaxValue))
                        {
                            Result = "Fail";
                        }
                    }
                }
            }
            if (msg != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun('" + msg + "')", true);
            }
            else
            {
                ddlTestReport.ClearSelection();
                ddlTestReport.Items.FindByValue(Result).Selected = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalAttendenceFun('')", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}